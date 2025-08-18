using Client.Domain.Validation.Common;
using System.Net;
using System.Net.Http.Json;

namespace Client.Domain.Acceptance.Tests.Tests
{

    [Collection("Acceptance")]
    public class ValidationRulesTests
    {
        private readonly HttpClient _client;

        public ValidationRulesTests(ApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GET_validationRules_returns_four_policies()
        {
            var response = await _client.GetAsync("/validationRules");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var policies = await response.Content.ReadFromJsonAsync<List<ValidationPolicyDescriptor>>();
            Assert.NotNull(policies);
            Assert.Equal(4, policies!.Count);
        }
    }
}