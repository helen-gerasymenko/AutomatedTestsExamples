using System;
using Api.Tests.Data;

namespace Api.Tests
{
    public class BrandTestHelper
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string SmsNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public bool EnablePlayerPrefix { get; set; }
        public string PlayerPrefix { get; set; }
        public int InternalAccounts { get; set; }

        public BrandId CreateBrand(bool isActive = false)
        {
            //not implemented, for demonstration purposes only
            return new BrandId();
        }

    }
}
