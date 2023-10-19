# DotNetWebAPI - HCP Vault Integration (POC)

This repository contains a .NET web API project that provides encryption and decryption services using HashiCorp's Vault. It integrates with the Transit secrets engine of Vault to perform encryption and decryption operations.

## Features:

1. **Health Check Endpoint**: A health check endpoint (`/health`) to quickly verify if the service is up and running.
2. **Encryption**: Encrypt plaintext and get the ciphertext as the response.
3. **Decryption**: Decrypt ciphertext and get the plaintext as the response.

## Setup

### Prerequisites:

- .NET 7.0
- An instance of HashiCorp's Vault with the Transit secrets engine enabled.

### Configuration:

The application needs specific configuration values from the `appsettings.[environment].json` file or environment variables:

- `VAULT_ADDR`: Vault server address.
- `APPROLE_ROLE_ID`: Role ID for AppRole authentication.
- `APPROLE_SECRET_ID`: Secret ID for AppRole authentication.
- `KEY_NAME`: Name of the key in the Transit engine to be used for encryption and decryption.

For environment-specific settings, make sure to add an `appsettings.[environment].json` where `[environment]` is the name of the environment, for example, `appsettings.Production.json`.

### Running the application:

```bash
dotnet run Program.cs
