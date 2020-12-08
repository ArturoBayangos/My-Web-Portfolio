using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Portfolio.Models;
using static Web_Portfolio.DatabaseSettings;

namespace Web_Portfolio.Services
{
    public class ProjectsService
    {
        private readonly IMongoCollection<Post> _proj;

        public ProjectsService(IDatabaseSetting settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _proj = database.GetCollection<Post>(settings.CollectionName);
        }

        public List<Post> Get() =>
            _proj.Find(proj => proj.Type == PostType.Project).ToList();

        public Post Get(string id) =>
            _proj.Find<Post>(proj => proj.Id == id).FirstOrDefault();

        public Post Create(Post proj)
        {
            _proj.InsertOne(proj);
            return proj;
        }

        public void Update(string id, Post projIn) =>
            _proj.ReplaceOne(proj => proj.Id == id, projIn);

        public void Remove(Post projIn) =>
            _proj.DeleteOne(proj => proj.Id == projIn.Id);

        public void Remove(string id) =>
            _proj.DeleteOne(proj => proj.Id == id);
    }

}
