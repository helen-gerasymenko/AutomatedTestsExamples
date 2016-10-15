using System;

namespace Api.Tests.Data
{
    public class AddBrandResponse
    {
        public bool Success { get; set; }
        public BrandId Data { get; set; }
    }

    public class BrandId
    {
        public Guid Id { get; set; }
    }
}
