using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Portfolio.Models;
using static Web_Portfolio.DatabaseSettings;

namespace Web_Portfolio
{
    public class BlogsService
    {
        private readonly IMongoCollection<Post> _blogs;

        public BlogsService(IDatabaseSetting settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _blogs = database.GetCollection<Post>(settings.CollectionName);
        }

        public List<Post> Get() =>
            _blogs.Find(blog => blog.Type == PostType.Blogs).ToList();

        public Post Get(string id) =>
            _blogs.Find<Post>(blog => blog.Id == id).FirstOrDefault();

        public Post Create(Post blog)
        {
            _blogs.InsertOne(blog);
            return blog;
        }

        public void Update(string id, Post blogIn) =>
            _blogs.ReplaceOne(blog => blog.Id == id, blogIn);

        public void Remove(Blogs blogIn) =>
            _blogs.DeleteOne(blog => blog.Id == blogIn.Id);

        public void Remove(string id) =>
            _blogs.DeleteOne(blog => blog.Id == id);
    }
}

