﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobmatch.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Skills { get; set; }
    }
}
