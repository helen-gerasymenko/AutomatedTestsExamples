using System;

namespace Api.Tests.Data
{
    public class AdminApiProxy
    {
        public AdminApiProxy(string url, string token = null) 
        {
            //not implemented
        }
        public UserBrandsResponse GetUserBrands()
        {
            throw new NotImplementedException(); //not implemented
        }
        public AddBrandResponse AddBrand(BrandTestHelper request)
        {
           throw new NotImplementedException(); //not implemented
        }
        public ActivateBrandResponse ActivateBrand(ActivateBrandRequest request)
        {
            throw new NotImplementedException(); //not implemented
        }
    }
}
