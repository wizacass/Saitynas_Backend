using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Saitynas_API.Services.HeadersValidator;

namespace Saitynas_API_Tests
{
    public class HeadersValidatorTests
    {
        private IHeadersValidator _headersValidator;

        [SetUp]
        public void SetUp()
        {
            _headersValidator = new HeadersValidator();
        }

        [Test]
        public void TestEmptyHeader()
        {
            var headers = new HeaderDictionary();
            
            Assert.IsFalse(IsValid(headers));
        }
        
        private bool IsValid(IHeaderDictionary headers)
        {
            return _headersValidator.IsRequestHeaderValid(headers);
        }
    }
}
