using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatDefenders.Data;

namespace ChatDefenders.Models
{
    public class IndexViewModel : Model
    {
		public IndexViewModel(Account userAccount) : base(userAccount) { }
    }
}
