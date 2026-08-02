using System;
using System.Globalization;
using Automation.Framework.Utilities;
using Xunit;

namespace Automation.FrameworkTests.Tests.Framework.Utilities
{
	[Trait("Category", "Encryption")]
	public class EncryptionTest
	{
		private ICryptoGraphy _cryptoGraphy;
		public ICryptoGraphy CryptoGraphy
		{
			get { return _cryptoGraphy ?? (_cryptoGraphy = new TripleDesCryptor()); }
		}
		[Fact]
		public void CanEncryptDecrypt()
		{
			const int id = 123;
			var key = "rXL+xSfK5qOpWHruvKo8AKGD/Avk6c5y";
			var iVect = "UG6ggmZXX9g=";
			var token = CryptoGraphy.Encrypt(id.ToString(CultureInfo.InvariantCulture), key, iVect);
			var idFromToken = CryptoGraphy.Decrypt(token, key, iVect);
			Assert.Equal(id, Convert.ToInt64(idFromToken));
		}
	}
}
