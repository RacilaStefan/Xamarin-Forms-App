using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EJobsMarket.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }
        public ExperienceTypes Experience { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public override string ToString()
        {
            return $"Username: {Username}, Password: {Password}, Salt: {Salt}";
        }
    }
}
