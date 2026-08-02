using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Automation.Framework.Utilities
{
	public class TripleDesCryptor : ICryptoGraphy
	{
		public string Encrypt(string message, string key, string iv)
		{
			return Encrypt(message, key, iv, CipherMode.ECB);
		}

		public string Encrypt(string message, string key, string iv, CipherMode cipherMode)
		{
			var keyArray = Convert.FromBase64String(key);
			var ivArray = Convert.FromBase64String(iv);
			var bytes = Encoding.UTF8.GetBytes(message);

			var tripleDesCrypto = GetTripleDesCrypto(keyArray, ivArray, cipherMode);

			var cryptoTransform = tripleDesCrypto.CreateEncryptor();
			var resultArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			tripleDesCrypto.Clear();

			return Convert.ToBase64String(resultArray, 0, resultArray.Length);
		}

		public string Decrypt(string message, string key, string iv)
		{
			return Decrypt(message, key, iv, CipherMode.ECB);
		}

		public string Decrypt(string message, string key, string iv, CipherMode cipherMode)
		{
			var keyArray = Convert.FromBase64String(key);
			var ivArray = Convert.FromBase64String(iv);
			var bytes = Convert.FromBase64String(message);

			var tripleDesCrypto = GetTripleDesCrypto(keyArray, ivArray, cipherMode);
			var cryptoTransform = tripleDesCrypto.CreateDecryptor();
			var resultArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			tripleDesCrypto.Clear();

			return Encoding.UTF8.GetString(resultArray);
		}

		public string UrlSafeEncrypt(long value, string key, string iv)
		{
			var keyArray = Convert.FromBase64String(key);
			var ivArray = Convert.FromBase64String(iv);
			var bytes = Encoding.UTF8.GetBytes(value.ToString(CultureInfo.InvariantCulture));

			var tripleDesCrypto = GetTripleDesCrypto(keyArray, ivArray, CipherMode.ECB);

			var cryptoTransform = tripleDesCrypto.CreateEncryptor();
			var resultArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			tripleDesCrypto.Clear();

			return ToHexString(resultArray);
		}

		public string UrlSafeDecrypt(string value, string key, string iv)
		{
			var keyArray = Convert.FromBase64String(key);
			var ivArray = Convert.FromBase64String(iv);
			var inputBuffer = FromHexString(value);

			var tripleDesCrypto = GetTripleDesCrypto(keyArray, ivArray, CipherMode.ECB);

			var decryptor = tripleDesCrypto.CreateDecryptor();
			byte[] outputBuffer = decryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

			tripleDesCrypto.Clear();

			return Encoding.UTF8.GetString(outputBuffer);
		}


		static string ToHexString(byte[] value)
		{
			var sb = new StringBuilder();
			foreach (var b in value)
				sb.AppendFormat("{0:x2}", b);
			return sb.ToString();
		}

		public static byte[] FromHexString(string hexString)
		{
			var bytes = new byte[hexString.Length / 2];
			for (var i = 0; i < bytes.Length; i++)
			{
				bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
			}

			return bytes;
		}

		private static TripleDESCryptoServiceProvider GetTripleDesCrypto(byte[] keyArray, byte[] ivArray, CipherMode cipherMode)
		{
			return new TripleDESCryptoServiceProvider
			{
				Key = keyArray,
				IV = ivArray,
				Padding = PaddingMode.PKCS7,
				Mode = cipherMode
			};
		}
	}
}