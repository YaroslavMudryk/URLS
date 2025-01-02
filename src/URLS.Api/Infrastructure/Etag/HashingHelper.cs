using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;

namespace URLS.Api.Infrastructure.Etag;

public static class HashingHelper
{
    public static string CalculateHash(MemoryStream ms)
    {
        string checksum = "";

        using (var algo = SHA1.Create())
        {
            byte[] bytes = algo.ComputeHash(ms);
            checksum = $"W/\"{WebEncoders.Base64UrlEncode(bytes)}\"";
        }

        return checksum;
    }
}