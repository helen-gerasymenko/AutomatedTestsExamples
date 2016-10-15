using System;

namespace Api.Tests.Data
{
    public class ActivateBrandRequest
    {
        public Guid BrandId { get; set; }
        public string Remarks { get; set; }
    }
}
