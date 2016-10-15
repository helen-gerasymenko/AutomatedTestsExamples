using System;
using System.Collections.Generic;
using System.Net.Http;
using Api.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Ui.Tests;

namespace Api.Tests.Steps
{
    [Binding]
    public class BrandSteps
    {
        protected string AdminApiUrl { get; set; }
        protected string Token { get; set; }
        protected AdminApiProxy AdminApiProxy
        {
            get
            {
                return ScenarioContext.Current.ContainsKey("AdminApiProxy") ? ScenarioContext.Current.Get<AdminApiProxy>("AdminApiProxy") : null;
            }

            set
            {
                ScenarioContext.Current.Set(value, "AdminApiProxy");
            }
        }
        private BrandTestHelper BrandTestHelper { get; set; }

        [Given(@"I am logged in and have access token")]
        public void GivenIAmLoggedInAndHaveAccessToken()
        {
            var username = ScenarioContext.Current.ContainsKey("username") ? ScenarioContext.Current.Get<string>("username") : "XXX";
            var password = ScenarioContext.Current.ContainsKey("password") ? ScenarioContext.Current.Get<string>("password") : "YYY";

            LogInAdminApi(username, password);
        }
        
        [When(@"New deactivated brand is created")]
        public void WhenNewDeactivatedBrandIsCreated(string activationStatus)
        {
            var isActive = activationStatus.Equals("activated");
            var brand = BrandTestHelper.CreateBrand( isActive);
            ScenarioContext.Current.Add("brandId", brand.Id);
        }

        [Then(@"New brand is successfully added")]
        public void ThenNewBrandIsSuccessfullyAdded()
        {
            var data = new BrandTestHelper()
            {
                Code = TestDataGenerator.GetRandomString(),
                Name = TestDataGenerator.GetRandomString(),
                EnablePlayerPrefix = true,
                PlayerPrefix = TestDataGenerator.GetRandomString(3),
                InternalAccounts = 1,
                Email = TestDataGenerator.GetRandomEmail(),
                SmsNumber = TestDataGenerator.GetRandomPhoneNumber(useDashes: false),
                WebsiteUrl = TestDataGenerator.GetRandomWebsiteUrl()
            };

            var result = AdminApiProxy.AddBrand(data);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
        }
        
        [Then(@"Brand is successfully activated")]
        public void ThenBrandIsSuccessfullyActivated()
        {
            ScenarioContext.Current.Should().ContainKey("brandId");
            var brandId = ScenarioContext.Current.Get<Guid>("brandId");

            var data = new ActivateBrandRequest
            {
                BrandId = brandId,
                Remarks = "Some remark"
            };

            var result = AdminApiProxy.ActivateBrand(data);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
        }

        [Then(@"Available brands are visible to me")]
        protected void ThenAvailableBrandsAreVisibleToMe()
        {
            var result = AdminApiProxy.GetUserBrands();

            result.Should().NotBeNull();
            result.Brands.Should().NotBeEmpty();
        }
        protected void LogInAdminApi(string username, string password)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            });

            var response = new HttpClient().PostAsync(AdminApiUrl + "token", formContent).Result;
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Content.ReadAsStringAsync().Result);
            Token = tokenResponse.AccessToken;

            AdminApiProxy = new AdminApiProxy(AdminApiUrl, Token);
        }
    }
}
