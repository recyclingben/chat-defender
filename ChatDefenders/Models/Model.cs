using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatDefenders.Data;

namespace ChatDefenders.Models
{
    public class Model
    {
		public Account UserAccount { get; set; }

		public Model(Account userAccount) => 
			UserAccount = userAccount;
    }
}
