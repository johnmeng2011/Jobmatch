using Jobmatch.Controllers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace JobMatchTesting
{
    [TestFixture]
    public class MatchControllerTest
    {

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        private string _jobAdderBaseUrl;

        private MatchController _matchController;

        [SetUp]
        public void Setup()
        {
            var config = InitConfiguration();
            _jobAdderBaseUrl = config["JobAdderApiBaseUrl"];

            _matchController = new MatchController(new Jobmatch.Models.JobAdderApi
            {
                BaseUrl = _jobAdderBaseUrl
            });

        }

        [Test]
        public void GetCandidateShouldWork()
        {
            var result = _matchController.GetCandidates().ConfigureAwait(false).GetAwaiter().GetResult();
            if(result.GetType() == typeof(Microsoft.AspNetCore.Mvc.OkObjectResult))
            {
                var finalResult = ((Microsoft.AspNetCore.Mvc.OkObjectResult)result).Value;
                Assert.IsNotNull(finalResult);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetJobsShouldWork()
        {
            var result = _matchController.GetJobs().ConfigureAwait(false).GetAwaiter().GetResult();
            if (result.GetType() == typeof(Microsoft.AspNetCore.Mvc.OkObjectResult))
            {
                var finalResult = ((Microsoft.AspNetCore.Mvc.OkObjectResult)result).Value;
                Assert.IsNotNull(finalResult);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}