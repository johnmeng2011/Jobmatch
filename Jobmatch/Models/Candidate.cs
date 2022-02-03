using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobmatch.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string SkillTags { get; set; }
    }
}
