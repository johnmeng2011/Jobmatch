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
                IEnumerable<Job> jobs = JsonConvert.DeserializeObject<List<Job>>(response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult());
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
