using Jobmatch.Helpers;
using Jobmatch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jobmatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        private HttpClient _httpClient;

        public MatchController(JobAdderApi api)
        {

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(api.BaseUrl)
            };

        }

        [HttpGet("candidates")]
        public async Task<IActionResult> GetCandidates()
        {
            try
            {
                var response = await _httpClient.GetAsync("candidates");
                response.EnsureSuccessStatusCode();
                IEnumerable<Candidate> candidates = JsonConvert.DeserializeObject<List<Candidate>>(response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult());
                return Ok(candidates);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("jobs")]
        public async Task<IActionResult> GetJobs()
        {
            try
            {
                var response = await _httpClient.GetAsync("jobs");
                response.EnsureSuccessStatusCode();
                IEnumerable<Job> jobs = JsonConvert.DeserializeObject<List<Job>>(response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult());
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("go")]
        public async Task<IActionResult> MatchJobs()
        {
            var jobsResult = GetJobs().ConfigureAwait(false).GetAwaiter().GetResult();

            List<Job> jobs = null;
            List<Candidate> candidates = null;
            if (jobsResult.GetType() == typeof(OkObjectResult))
            {
                jobs = (List<Job>)((OkObjectResult)jobsResult).Value;
            }

            var candidateResult = GetCandidates().ConfigureAwait(false).GetAwaiter().GetResult();
            if (candidateResult.GetType() == typeof(OkObjectResult))
            {
                candidates = (List<Candidate>)((OkObjectResult)candidateResult).Value;
            }

            if (jobs == null || candidates == null)
                return BadRequest("Invalid Jobs Or Candidates List");


             List<JobMatch> matches = new List<JobMatch>();
            foreach(var job in jobs)
            {
                var candidate  = JobMatchHelper.Match(job, candidates);
                matches.Add(new JobMatch
                {
                    Job = job,
                    Candidate = candidate,
                });
            }


            return Ok(matches);
        }
    }
}
