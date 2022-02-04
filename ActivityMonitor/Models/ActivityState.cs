using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityMonitor.Models
{
    public class ActivityState
    {
        public List<User> Users { 
            get; 
            set;
        }

        public ActivityState()
        {
            if(Users == null) { Users = new List<User>(); }
            GetUsers();
        }

        public async Task<List<User>> UpdateUser(User user)
        {
            try
            {
                await GetUsers();
                int index = Users.FindIndex(u => u.Name == user.Name);
                user.IsOnline = true;
                if (index < 0)
                {
                    Users.Add(user);
                }
                else
                {
                    Users[index] = user;
                }
                await SetUsers();
                return await Task.FromResult<List<User>>(Users);
            } catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<User>> Logoff(User user)
        {
            await GetUsers();
            int index = Users.FindIndex(u => u.Name == user.Name);
            user.IsOnline = false;
            await SetUsers();
            return await Task.FromResult<List<User>>(Users);
        }

        //Couldn't get session working so settled on a json file instead. Could also consider DB
        private async Task GetUsers()
        {
            using (StreamReader r = new StreamReader("currentUsers.json"))
            {
                string json = r.ReadToEnd();
                Users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            await Task.CompletedTask;
        }

        private async Task SetUsers()
        {
            File.WriteAllText("currentUsers.json", JsonConvert.SerializeObject(Users));
            await Task.CompletedTask;
        }
    }

    public class User
    {
        public string Name { get; set; }
        public bool? IsOnline { get; set; }
    }
}
