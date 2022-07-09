using EJobsMarket.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SQLite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EJobsMarket.Database.Repositories
{
    public class UserService
    {
        private SQLiteAsyncConnection Database;
        private AsyncTableQuery<User> UserTable;

        public UserService(SQLiteAsyncConnection database)
        {
            Database = database;
            UserTable = Database.Table<User>();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return UserTable.ToListAsync();
        }

        public Task<User> GetUserAsync(int id)
        {
            return UserTable.Where(user => user.Id == id).FirstOrDefaultAsync();
        }

        public Task<User> GetUserAsync(string username)
        {
            return UserTable.Where(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            bool value = await UserTable.DeleteAsync(user => user.Id == id) != 0;
            if (value)
            {
                Database.Table<Job>().DeleteAsync(job => job.TakerId == id || job.MakerId == id).DoNotAwait();
            }

            return value;
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.Id != 0)
            {
                return Database.UpdateAsync(user);
            } else
            {
                return Database.InsertAsync(user);
            }
        }

        public async Task<bool> SaveUserAsync(string username, string password, string contact, string description, ExperienceTypes experience)
        {
            if ((await GetUserAsync(username)) is null)
            {
                byte[] salt = GetSalt();
                User user = new User()
                {
                    Username = username,
                    Password = GetHashedPassword(password, salt),
                    Salt = Convert.ToBase64String(salt),
                    Contact = contact,
                    Description = description,
                    Experience = experience,
                };

                return await SaveUserAsync(user) != 0;
            }

            return false;
        }

        public async Task<bool> LoginUserAsync(string username, string password)
        {
            User user = await GetUserAsync(username);
            if (user is null)
            {
                return false;
            }
            else if (user.Password != GetHashedPassword(password, Convert.FromBase64String(user.Salt)))
            {
                return false;
            }

            SecureStorage.SetAsync("LoggedUserId", user.Id.ToString()).DoNotAwait();
            App.ActiveUser = user;

            return true;
        }

        public async Task<bool> AddMockDataAsync()
        {
            return await SaveUserAsync("admin1", "admin1", "admin1@admin.com", "Eu sunt admin 1", ExperienceTypes.LeadDeveloper) &&
            await SaveUserAsync("admin2", "admin2", "admin2@admin.com", "Eu sunt admin 2", ExperienceTypes.SeniorDeveloper) &&
            await SaveUserAsync ("admin3", "admin3", "admin3@admin.com", "Eu sunt admin 3", ExperienceTypes.MiddleDeveloper) &&
            await SaveUserAsync ("admin4", "admin4", "admin4@admin.com", "Eu sunt admin 4", ExperienceTypes.JuniorDeveloper);
        }

        private string GetHashedPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
             password: password,
             salt: salt,
             prf: KeyDerivationPrf.HMACSHA256,
             iterationCount: 100000,
             numBytesRequested: 256 / 8));

            return hashed;
        }

        private byte[] GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            return salt;
        }
    }
}
