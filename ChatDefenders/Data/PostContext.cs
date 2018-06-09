using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChatDefenders.Data
{
    public class PostContext : DbContext
    {
		public PostContext(DbContextOptions<PostContext> options) : base(options)
		{ }

		public DbSet<PostAuthor> PostAuthors { get; set; }
		public DbSet<Post> Posts { get; set; }
	}
}
