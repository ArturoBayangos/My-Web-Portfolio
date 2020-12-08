﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Web_Portfolio.Models;
using static Web_Portfolio.DatabaseSettings;

namespace Web_Portfolio.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> _user;

        public AuthService(IDatabaseSetting settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>("users");
        }

        public List<User> Get() =>
            _user.Find(user => true).ToList();

        public User Get(string id) =>
            _user.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Get(Expression<Func<User, bool>> query)
        { 
          return _user.Find<User>(query).FirstOrDefault();
        }

        public User Create(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _user.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _user.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _user.DeleteOne(user => user.Id == id);
    }
}

