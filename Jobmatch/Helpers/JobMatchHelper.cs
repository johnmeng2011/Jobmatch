using Jobmatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobmatch.Helpers
{
    public static class JobMatchHelper
    {
        public static Candidate Match(Job job, List<Candidate> candidates)
        {
            var requiredSkills = PurgeDuplicates(job.Skills.Split(','));

            //find weighted required skills based on inverse febonacci array
            var weightedRequiredSkills = GetWeiths(requiredSkills);

            var candidateMatchRates = new Dictionary<Candidate, decimal>();

            foreach(var candidate in candidates)
            {
                var rate = (decimal)0.0;
                var candidateSkillSets = PurgeDuplicates( candidate.SkillTags.Split(','));

                //get weighted candidate skills based on inverse febonacci array 
                var weightedCandidateSkillSets = GetWeiths(candidateSkillSets);
                var requiredSkillKeys = weightedRequiredSkills.Keys.ToList();
                var candidateSkillSetKeys = weightedCandidateSkillSets.Keys.ToList();

                foreach(var skillKey in requiredSkillKeys)
                {
                    if (candidateSkillSetKeys.Contains(skillKey))
                    {
                        rate += weightedRequiredSkills.GetValueOrDefault(skillKey) * weightedCandidateSkillSets.GetValueOrDefault(skillKey);
                    }
                }

                candidateMatchRates.Add(candidate, rate);

            }

            var maxRate = (decimal)0.0;
            Candidate theOne = candidates[0];

            foreach(var candidate in candidates)
            {
                if(candidateMatchRates.GetValueOrDefault(candidate) > maxRate)
                {
                    theOne =    candidate;
                    maxRate = candidateMatchRates.GetValueOrDefault(candidate);
                }
            }

            return theOne;

        }

        private static Dictionary<string, decimal> GetWeiths(string[] array)
        {
            var result = new Dictionary<string, decimal>();
            var length = array.Count();

            var febInv = GetFebonacciInverse(length);

            var total = febInv.Sum();


            for (int i = 0; i < length; i++)
            {
                result.Add(array[i],febInv[i] / (decimal)total);
            }

            return result;
        }

        public static int[] GetFebonacciInverse(int n)
        {
            int[] febonacci = new int[n];
            var i = 1;
            var j = 2;
            for (var k = n - 1; k >= 0; k--)
            {
                if (k == n - 1)
                {
                    febonacci[k] = i;
                    continue;
                }
                if (k == n - 2)
                {
                    febonacci[k] = j;
                    continue;
                }

                var l = i;
                i = j;
                j = i + l;

                febonacci[k] = j;
            }

            return febonacci;
        }


        public static string[] PurgeDuplicates(string[] input)
        {
            var set = new HashSet<string>();
            foreach(string s in input)
            {
                if (!set.Contains(s.Trim()))
                {
                    set.Add(s.Trim());
                }
            }
            return set.ToArray();
        }
    }
}
