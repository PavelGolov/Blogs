﻿using Blogs.Infrastructure.Data;
using Blogs.SharedKernel.Models;
using System.Collections.Generic;
using System.Linq;

namespace Blogs.Web.Managers
{
    public class PostManager : BaseManager
    {
        public PostManager(BlogsContext context) : base(context)
        { }
        public List<Post> List()
        {
            return _context.Posts.ToList();
        }
        public Post GetPost(int id)
        {
            return _context.Posts.Find(id);
        }
        public void Insert(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public void Update(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
        public void Delete(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
        public bool Exists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
