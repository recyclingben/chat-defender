using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ChatDefenders.Interfaces;
using static System.Diagnostics.Debug;
using ChatDefenders.Extensions;
using Newtonsoft.Json;
using ChatDefenders.Attributes;

namespace ChatDefenders.Data
{
	// Contains main account information passed through OAuth.
    public partial class Account
    {
		public int ID { get; set; }
		[DTOInclude]
		public string Username { get; set; }
		[DTOInclude]
		public string NameIdentifier { get; set; }
		[DTOInclude]
		public string AvatarUrl { get; set; }
	}
	[Serializable]
	public partial class Account : DbObject, ISetable<Account>
	{
		// Returns the default instance of an account.
		public static Account Default =>
			new Account
			{
				Username = "Unknown",
				NameIdentifier = "0",
				AvatarUrl = "https://discordapp.com/assets/dd4dbc0016779df1378e7812eabaa04d.png"
			};

		protected Account() { }

		/* Returns a new instance of Account that is setup with
		 * the identity's values. */
		public static Account Create(ClaimsIdentity userIdentity)
		{
			string name = userIdentity.Name;
			string nameIdentifier = userIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			string urlId = userIdentity.FindFirst("urn:discord:avatar")?.Value;
			string avatarUrl = $"https://cdn.discordapp.com/avatars/{nameIdentifier}/{urlId}?size=512";

			return new Account
			{
				Username = name,
				NameIdentifier = nameIdentifier,
				AvatarUrl = avatarUrl
			};
		}

		/* If the account doesn't exist within the database, add it.
		 * Otherwise, check if any information has been changed
		 * since its creation. */
		public static void UpdateOrRegister(ClaimsIdentity userIdentity)
		{
			if (!userIdentity.IsAuthenticated)
				throw new ArgumentException("Identity provided has not been authenticated.");

			var context = DataUtilities.GetContextInstance<PostContext>();

			/* Current instance of account in database. Null if
			 * none was found. */
			var userIdentifier = userIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var databaseAccount = context.Accounts.FirstOrDefault
													(
														_ => _.NameIdentifier.Equals(userIdentifier ?? "")
													);
			var currentAccount = Create(userIdentity);
			// Register account into db if it doesn't exist.
			if (databaseAccount == null)
			{
				context.Accounts.Add(currentAccount);

				context.SaveChanges();
			}
			// Update account data if the instances don't match.
			else if (!currentAccount.PropertiesEqualExclude(databaseAccount, "ID"))
			{
				databaseAccount.SetTo(currentAccount);
				context.Entry(databaseAccount).State = EntityState.Modified;
				context.Update(databaseAccount);

				context.SaveChanges();
			}
		}

		// This is self-documenting, idiot.
		public bool IsRegistered() => 
			DataUtilities.GetContextInstance<PostContext>()
				.Accounts
				.FirstOrDefault(_ => _.NameIdentifier.Equals(NameIdentifier)) != null;

		/* Set all properties equal to another account,
		 * EXCEPT the ID. */
		public void SetTo(Account account)
		{
			Username = account.Username;
			NameIdentifier = account.NameIdentifier;
			AvatarUrl = account.AvatarUrl;
		}

		// Returns a user that can be identified by the user's identity.
		public static Account GetByUserIdentity(ClaimsIdentity userIdentity)
		{
			var userIdentifier = userIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if(userIdentifier == null)
				return null;

			return DataUtilities.GetContextInstance<PostContext>()
					.Accounts
					.FirstOrDefault
					(_ => _.NameIdentifier.Equals(userIdentifier));
		}
	}
}