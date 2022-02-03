The solution works to fetch jobs and candidates from Job Adder's api endpoints and use an algorithm to find the best candidate for each job. 

The algorithm used is: for all the skills required by a job, calculate each skills weight based on an inverse febonacci array. For example, 

a job may require 'mobile, aws , java, communication,' as skills, then the algorithm first calculates an inverse febonacci array as 

{5,3,2,1}, then calculates each skill's weight as {5/11, 3/11, 2/11 , 1/11} , with 11 being the sum of all numbers in the array. 

For a candidate's skills, the algorithm will do the same thing to give each skill a weight. Then the matching algorithm will find the matching 

key words in a candidate's skill set with the required skills in a job, multiply the corresponding weights in the candidate's skill and the job's

required skill, then add them up to get a final quantitative metric of the candidate's matching to the job. The candidate with the highest matching

rate is the best match for the job. 


Assumptions and notes:

1. The skills returned in the /jobs and /candidates api may include duplicate key words, the duplicates will be filtered out. 

2. The view is a simple web page with a single function to display the right candidate for each job. 

3. Unit testings have been added. 

4. The solution need .netcore3.1 to run. 