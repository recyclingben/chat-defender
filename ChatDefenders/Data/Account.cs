using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChatDefenders.Interfaces;

namespace ChatDefenders.Data
{
    public partial class Account
    {
		public int ID { get; set; }
		public string Username { get; set; }
		public string NameIdentifier { get; set; }
		public string AvatarUrl { get; set; }
	}

	public partial class Account : IEquatable<Account>, ISetable<Account>
	{
		protected Account() { }

		// Returns a new instance of Account that is setup with
		// identity values.
		public static Account Create(ClaimsIdentity userIdentity)
		{
			System.Diagnostics.Debug.WriteLine(userIdentity.IsAuthenticated);

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

		// If the account doesn't exist within the database, add it.
		// Otherwise, check if any information has been changed
		// since its creation.
		public Account UpdateOrRegister()
		{
			var context = DataUtilities.GetContextInstance<PostContext>();

			// Current instance of account in database. Null
			// if none was found.
			var dbAcc = context.Accounts.FirstOrDefault(_ => _.NameIdentifier.Equals(NameIdentifier));
			// Add account into db if it doesn't exist.
			if (dbAcc == null)
			{
				context.Accounts.Add(this);

				context.SaveChanges();
			}
			// Update account data if the instances don't match.
			else if (!Equals(dbAcc))
			{
				dbAcc.SetTo(this);
				context.Entry(dbAcc).State = EntityState.Modified;
				context.Update(dbAcc);

				context.SaveChanges();
			}
			return this;
		}

		public bool IsRegistered() => 
			DataUtilities.GetContextInstance<PostContext>()
				.Accounts
				.FirstOrDefault(_ => _.NameIdentifier.Equals(NameIdentifier)) != null;

		public bool Equals(Account acc)
		{
			if (!Username.Equals(acc.Username)) return false;
			if (!NameIdentifier.Equals(acc.NameIdentifier.Trim())) return false;
			if (!AvatarUrl.Equals(acc.AvatarUrl)) return false;

			return true;
		}

		public void SetTo(Account acc)
		{
			Username = acc.Username;
			NameIdentifier = acc.NameIdentifier;
			AvatarUrl = acc.AvatarUrl;
		}
	}
}