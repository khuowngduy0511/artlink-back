using System.Security.Cryptography;
using System.Text;

namespace Application.Commons;

public class CryptoHelper
{
    public static string HMacCompute(string key = "", string message = "")
    {
        byte[] keyByte = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
        var hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
        return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
    }

    //public static string RSASign(string privateKeyBase64, string message)
    //{
    //    // Convert the private key from base64 string to byte array
    //    byte[] privateKeyBytes = Convert.FromBase64String(privateKeyBase64);

    //    // Create an instance of RSA
    //    using (RSA rsa = RSA.Create())
    //    {
    //        // Import the private key
    //        rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

    //        // Convert message to bytes
    //        byte[] messageBytes = Encoding.UTF8.GetBytes(message);

    //        // Sign the message
    //        byte[] signedBytes = rsa.SignData(messageBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

    //        // Convert the signed bytes to base64 string
    //        string signedMessage = Convert.ToBase64String(signedBytes);

    //        return signedMessage;
    //    }
    //}


    public static string RSASign(string privateKey, string message)
    {
        // Convert mac string to byte array
        byte[] msgBytes = Encoding.UTF8.GetBytes(message);

        // Convert private key string to RSA parameters
        var rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);

        // Sign the data
        byte[] signatureBytes = rsa.SignData(msgBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        // Convert signature to base64 string
        string signature = Convert.ToBase64String(signatureBytes);

        return signature;
    }
}
