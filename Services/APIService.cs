using Microsoft.Extensions.Options;
using DotNetCoreWebAPI.Models;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.AppRole;
using VaultSharp.V1.SecretsEngines.Transit;
using System;
using System.Text;
using VaultSharp.V1.Commons;
using VaultSharp;
using System.Threading.Tasks;

namespace DotNetCoreWebAPI.Services
{
    public class APIService
    {

        private readonly AppConfig _appConfig;
        private IVaultClient _vaultClient;

        public APIService(IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig.Value;
        }

        public void AuthenVault()
        {
            IAuthMethodInfo authMethod = new AppRoleAuthMethodInfo(_appConfig.APPROLE_ROLE_ID, _appConfig.APPROLE_SECRET_ID);
            var vaultClientSettings = new VaultClientSettings(_appConfig.VAULT_ADDR, authMethod)
            {
                Namespace = "admin"
            };
            _vaultClient = new VaultClient(vaultClientSettings);
        }

        public async Task<string> EncryptText(AppRequest appRequest)
        {
            AuthenVault();
            var keyName = _appConfig.KEY_NAME;
            var plainText = appRequest.PLAINTEXT;
            var encodedPlainText = Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
            var encodedContext = Convert.ToBase64String(Encoding.UTF8.GetBytes("currentContext"));

            var encryptOptions = new EncryptRequestOptions
            {
                Base64EncodedPlainText = encodedPlainText,
                Base64EncodedContext = encodedContext,
            };

            Secret<EncryptionResponse> encryptionResponse = await _vaultClient.V1.Secrets.Transit.EncryptAsync(keyName, encryptOptions);
            string cipherText = encryptionResponse.Data.CipherText;
            return cipherText;
        }

        public async Task<string> DecryptText(AppRequest appRequest)
        {
            AuthenVault();
            var keyName = _appConfig.KEY_NAME;
            var encodedContext = Convert.ToBase64String(Encoding.UTF8.GetBytes("currentContext"));

            var decryptOptions = new DecryptRequestOptions
            {
                CipherText = appRequest.CIPHERTEXT,
                Base64EncodedContext = encodedContext,
            };

            Secret<DecryptionResponse> decryptionResponse = await _vaultClient.V1.Secrets.Transit.DecryptAsync(keyName, decryptOptions);
            var encodedPlainText = decryptionResponse.Data.Base64EncodedPlainText;
            var plainText = Encoding.UTF8.GetString(Convert.FromBase64String(encodedPlainText));
            return plainText;
        }
    }
}