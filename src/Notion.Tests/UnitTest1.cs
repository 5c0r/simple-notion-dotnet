using Notion.Net.Service;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NotionTests
{
    public sealed class SmokeTest
    {
        private readonly NotionClient clientUnderTest;
        private readonly string testBearerToken = Environment.GetEnvironmentVariable("NOTION_KEY");

        public SmokeTest()
        {
            var envvars = Environment.GetEnvironmentVariables();
            clientUnderTest = new NotionClient(new HttpClient(), testBearerToken);
        }

        [Fact]
        public async Task Test_NotionClient_ShouldWork()
        {
            var database = await clientUnderTest.GetDatabaseAsync(Guid.Parse("6d4051a70d2b478a9a60712b08280aff"), CancellationToken.None);
            var page = await clientUnderTest.GetPageAsync(Guid.Parse("5af2caf4-2062-44aa-bc63-c1e194d44b4b"), CancellationToken.None);

            Assert.NotNull(database);
            Assert.NotNull(page);

        }
    }
}
