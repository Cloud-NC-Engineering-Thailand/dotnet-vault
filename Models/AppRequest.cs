namespace DotNetCoreWebAPI.Models
{

     public record AppRequest
     {
          public string PLAINTEXT { get; init; }
          public string CIPHERTEXT { get; init; }
     }

}