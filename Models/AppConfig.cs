namespace DotNetCoreWebAPI.Models
{

     public record AppConfig
     {
          public string VAULT_ADDR { get; init; }
          public string APPROLE_ROLE_ID { get; init; }
          public string APPROLE_SECRET_ID { get; init; }

          public string KEY_NAME { get; init; }
     }

}