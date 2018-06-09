using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChatDefenders.Data
{
    public partial class Account
    {
		public int ID { get; set; }
		public string Username { get; set; }
		public string NameIdentifier { get; set; }
		public string AvatarUrl { get; set; }
	}
	public partial class Account
	{
		// If the account doesn't exist within the database, add it.
		// Otherwise, check if any information has been changed
		// since its creation.
		public static void UpdateOrAdd(Account acc, PostContext context)
		{
			// Current value of equivalent database instance. Null
			// if none was found.


			var dbAcc = context.Accounts.FirstOrDefault(
				x => x.NameIdentifier == acc.NameIdentifier
			);

			// Add account into db if it doesnt exist.
			if (dbAcc == null)
			{
				context.Accounts.Add(acc);
				context.SaveChanges();
			}
			// Update account data if the instances don't match.
			else if(!acc.Equals(dbAcc))
			{
				dbAcc = acc;
				context.SaveChanges();
			}
		}
	}
}