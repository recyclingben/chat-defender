using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatDefenders.Data;

namespace ChatDefenders.Models
{
    public class UserModel
    {
		public Account UserAccount { get; set; }

		public UserModel(Account userAccount) => 
			UserAccount = userAccount;
    }
}
