using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDefenders.Data
{
    public class Post
    {
		public int ID { get; set; }
		public DateTime PostDate { get; set; }
		public virtual PostAuthor PostAuthor { get; set; }
		public string Contents { get; set; }
		public string Title { get; set; }
	}
}
