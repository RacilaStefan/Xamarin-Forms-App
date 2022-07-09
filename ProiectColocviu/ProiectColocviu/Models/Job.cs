using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EJobsMarket.Models
{
    public class Job
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public JobTypes Type { get; set; }
        public float Compensation { get; set; }
        public CompensationTypes CompensationType { get; set; }
        public ExperienceTypes ExperienceRequired { get; set; }
        public bool Remote { get; set; }

        public int MakerId { get; set; }
        public int TakerId { get; set; }
    }
}
