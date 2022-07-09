using EJobsMarket.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EJobsMarket.Database.Repositories
{
    public class JobService
    {
        private SQLiteAsyncConnection Database;
        private AsyncTableQuery<Job> JobTable;

        public JobService(SQLiteAsyncConnection database)
        {
            Database = database;
            JobTable = Database.Table<Job>();
            //AddMockData(30);
        }

        public Task<List<Job>> GetJobsAsync()
        {
            return JobTable.ToListAsync();
        }

        public Task<List<Job>> GetJobsAsync(JobTypes type)
        {
            return JobTable.Where(job => job.Type == type).ToListAsync();
        }

        public Task<Job> GetJobAsync(int id)
        {
            return JobTable.Where(job => job.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Job>> GetJobsByMakerIdAsync(int makerId)
        {
            List<Job> jobs = await JobTable.Where(job => job.MakerId == makerId).ToListAsync();

            return jobs is null ? new List<Job>() : jobs;
        }

        public async Task<List<Job>> GetJobsByTakerIdAsync(int takerId)
        {
            List<Job> jobs = await JobTable.Where(job => job.TakerId == takerId).ToListAsync();

            return jobs is null ? new List<Job>() : jobs;
        }

        public Task<List<Job>> GetRecommendedJobsAsync(User user)
        {
            Task<List<Job>> jobs;
            if (user != null)
            {
                jobs = JobTable.Where(job =>
                    job.ExperienceRequired <= user.Experience &&
                    job.TakerId == 0 &&
                    job.MakerId != user.Id).ToListAsync();

                return jobs;
            }

            return new Task<List<Job>>(() => new List<Job>());
        }

        public Task<int> SaveJobAsync(Job job)
        {
            return job.Id != 0 ? Database.UpdateAsync(job) : Database.InsertAsync(job);
        }

        public async Task<bool> SaveJobAsync(string title, string description, ExperienceTypes experienceRequired, JobTypes jobType, CompensationTypes compensationType, float compensation, bool remote)
        {
            Job newJob = new Job()
            {
                Title = title,
                Description = description,
                ExperienceRequired = experienceRequired,
                Type = jobType,
                CompensationType = compensationType,
                Compensation = compensation,
                Remote = remote,
                MakerId = App.ActiveUser.Id,
            };

            return await SaveJobAsync(newJob) != 0;
        }

        public async Task<bool> UpdateJobTakerAsync(int takerId, int jobId)
        {
            Job job = await GetJobAsync(jobId);
            if (job != null)
            {
                job.TakerId = takerId;
                return await SaveJobAsync(job) != 0;
            }

            return false;
        }

        public async Task<bool> DeleteJobAsync(int jobId)
        {
            return await JobTable.DeleteAsync(job => job.Id == jobId) != 0;
        }

        public async void AddMockDataAsync(int count)
        {
            List<Job> jobs = new List<Job>();
            Random random = new Random();
            int numberOfMockUsers = await Database.Table<User>().CountAsync();
            for (int i = 0; i < count; i++)
            {
                jobs.Add(
                    new Job()
                    {
                        Title = "Title",
                        Description = "Description",
                        Type = (JobTypes)random.Next(1, 4),
                        Compensation = random.Next(1000, 10000),
                        CompensationType = (CompensationTypes)random.Next(1, Enum.GetValues(typeof(CompensationTypes)).Length),
                        ExperienceRequired = (ExperienceTypes)random.Next(1, Enum.GetValues(typeof(ExperienceTypes)).Length),
                        Remote = random.Next(2) == 1,
                        MakerId = random.Next(1, numberOfMockUsers),
                        TakerId = random.Next(1, numberOfMockUsers),
                    }
                    );
            }

            Database.InsertAllAsync(jobs).DoNotAwait();
        }

        public void AddMockDataFromFileAsync()
        {
            string jsonString;
            List<Job> jobs;
            string fileName = "Jobs.json";

            Assembly assembly = typeof(JobService).GetTypeInfo().Assembly;
            string resource = $"{assembly.GetName().Name}.{fileName}";
            Stream stream = assembly.GetManifestResourceStream(resource);
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
   
                jobs = JsonConvert.DeserializeObject<List<Job>>(jsonString);
            }
            
            Database.InsertAllAsync(jobs).DoNotAwait();
        }
    }
}
