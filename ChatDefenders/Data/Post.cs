using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatDefenders.Attributes;

namespace ChatDefenders.Data
{
    public class Post : DbObject
    {
		public int ID { get; set; }
		[DTOInclude]
		public DateTime PostDate { get; set; }
		[DTOInclude]
		public virtual Account Author { get; set; }
		[DTOInclude]
		public string Contents { get; set; }
		[DTOInclude]
		public string Title { get; set; }
	}
}