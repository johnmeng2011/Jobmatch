using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobmatch.Models
{
    public class JobMatch
    {
        public Job Job{ get; set; }
        public Candidate Candidate{ get; set; }
    }
}
