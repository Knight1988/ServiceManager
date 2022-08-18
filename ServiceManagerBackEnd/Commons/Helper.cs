using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ServiceManagerBackEnd.Exceptions;

namespace ServiceManagerBackEnd.Commons;

public static class Helper
{
    public static string EncryptPassword(string username, string password)
    {
        const string salt = "sV9hgMD0soUF1oU7bxY6";
        var combined = $"{username.ToLowerInvariant()}_{password}_{salt}";
        var encryptedPassword = ComputeSha256Hash(combined);
        return encryptedPassword;
    }
    
    static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using var sha256Hash = SHA256.Create();
        // ComputeHash - returns byte array  
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
        // Convert byte array to a string   
        var builder = new StringBuilder();  
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }  
        return builder.ToString();
    }
}