using System.Security.Cryptography;

namespace Automation.Framework.Utilities
{
	public interface ICryptoGraphy
	{
		string Encrypt(string message, string key, string iv);
		string Encrypt(string message, string key, string iv, CipherMode cipherMode);
		string Decrypt(string message, string key, string iv);
		string Decrypt(string message, string key, string iv, CipherMode cipherMode);
		string UrlSafeEncrypt(long value, string key, string iv);
		string UrlSafeDecrypt(string value, string key, string iv);
	}
}
