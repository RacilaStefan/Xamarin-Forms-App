using EJobsMarket.Database.Repositories;
using EJobsMarket.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace EJobsMarket.Database
{
    public class EJobsMarketDatabase
    {
        private readonly string dbPath = Path.Combine(DependencyService.Get<IExternalStorage>().GetPath(), "EJobsMarket.db3");
        public static SQLiteConnection databaseConn;
        public static SQLiteAsyncConnection databaseAsyncConn;

        public UserService userService;
        public JobService jobService;

        public EJobsMarketDatabase()
        {
            databaseConn = new SQLiteConnection(dbPath);
            databaseAsyncConn = new SQLiteAsyncConnection(dbPath);
            databaseConn.DropTable<User>();
            databaseConn.DropTable<Job>();
            databaseConn.CreateTables<User, Job>();

            userService = new UserService(databaseAsyncConn);
            jobService = new JobService(databaseAsyncConn);
        }
    }
}
