using Finbourne.Identity.Sdk.Api;
using Finbourne.Identity.Sdk.Client;
using Finbourne.Identity.Sdk.Model;
using NUnit.Framework;
using System;

namespace Finbourne.Identity.Sdk.Extensions.IntegrationTests
{
    public class ApiFactoryTest
    {
        private IApiFactory _factory;

        [OneTimeSetUp]
        public void SetUp()
        {
            _factory = IntegrationTestApiFactoryBuilder.CreateApiFactory("secrets.json");
        }

        [Test]
        public void Create_ApplicationMetadataApi()
        {
            var api = _factory.Api<ApplicationMetadataApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<ApplicationMetadataApi>());
        }

        [Test]
        public void Create_ApplicationsApi()
        {
            var api = _factory.Api<ApplicationsApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<ApplicationsApi>());
        }

        [Test]
        public void Create_AuthenticationApi()
        {
            var api = _factory.Api<AuthenticationApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<AuthenticationApi>());
        }

        [Test]
        public void Create_DomainsApi()
        {
            var api = _factory.Api<DomainsApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<DomainsApi>());
        }

        [Test]
        public void Create_PersonalAuthenticationTokensApi()
        {
            var api = _factory.Api<PersonalAuthenticationTokensApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<PersonalAuthenticationTokensApi>());
        }

        [Test]
        public void Create_RolesApi()
        {
            var api = _factory.Api<RolesApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<RolesApi>());
        }

        [Test]
        public void Create_TokensApi()
        {
            var api = _factory.Api<TokensApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<TokensApi>());
        }

        [Test]
        public void Create_UsersApi()
        {
            var api = _factory.Api<UsersApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<UsersApi>());
        }

        [Test]
        public void Api_From_Interface()
        {
            var api = _factory.Api<IUsersApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<IUsersApi>());
        }

        [Test]
        public void NetworkConnectivityErrors_ThrowsException()
        {
            var apiConfig = ApiConfigurationBuilder.Build("secrets.json");
            // nothing should be listening on this, so we should get a "No connection could be made" error...
            apiConfig.IdentityUrl = "https://localhost:56789/insights"; 

            var factory = new ApiFactory(apiConfig);
            var api = factory.Api<IApplicationsApi>();

            // Can't be more specific as we get different exceptions locally vs in the build pipeline
            var expectedMsg = "Internal SDK error occurred when calling GetApplication";

            Assert.That(
                () => api.GetApplicationWithHttpInfo("app_does_not_exist"),
                Throws.InstanceOf<ApiException>()
                    .With.Message.Contains(expectedMsg));

            // Note: these non-"WithHttpInfo" methods just unwrap the `Data` property from the call above.
            // But these were the problematic ones, as they would previously just return a null value in this scenario.
            Assert.That(
                () => api.GetApplication("app_does_not_exist"),
                Throws.InstanceOf<ApiException>()
                    .With.Message.Contains(expectedMsg));
        }

        [Test]
        public void Invalid_Requested_Api_Throws()
        {
            Assert.That(() => _factory.Api<InvalidApi>(), Throws.TypeOf<InvalidOperationException>());
        }

        class InvalidApi : IApiAccessor
        {
            public IReadableConfiguration Configuration { get; set; }
            public string GetBasePath()
            {
                throw new NotImplementedException();
            }

            public ExceptionFactory ExceptionFactory { get; set; }
        }
    }
}
