using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChatDefenders.Interfaces;
using System.Runtime.Serialization;
using static System.Diagnostics.Debug;
using ChatDefenders.Extensions;
using System.Reflection;
using ChatDefenders.Models;
using Newtonsoft.Json;
using ChatDefenders.Attributes;
using System.Dynamic;

namespace ChatDefenders.Data
{
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

		public static Account GetDefault() =>
			new Account
			{
				Username = "Unknown",
				NameIdentifier = "0",
				AvatarUrl = "https://discordapp.com/assets/dd4dbc0016779df1378e7812eabaa04d.png"
			};

		protected Account() { }

		// Returns a new instance of Account that is setup with
		// identity values.
		public static Account Create(ClaimsIdentity userIdentity)
		{
			WriteLine(userIdentity.IsAuthenticated);

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

			// Current instance of account in database. Null if
			// none were found.
			var databaseAccount = context.Accounts.FirstOrDefault(_ => _.NameIdentifier.Equals(NameIdentifier));
			WriteLine(databaseAccount.Username);
			// Register account into db if it doesn't exist.
			if (databaseAccount == null)
			{
				WriteLine("zipoo");
				context.Accounts.Add(this);

				context.SaveChanges();
			}
			// Update account data if the instances don't match.
			else if (!this.PropertiesEqualExclude(databaseAccount, "ID"))
			{
				databaseAccount.SetTo(this);
				context.Entry(databaseAccount).State = EntityState.Modified;
				context.Update(databaseAccount);

				context.SaveChanges();
			}
			return this;
		}

		public bool IsRegistered() => 
			DataUtilities.GetContextInstance<PostContext>()
				.Accounts
				.FirstOrDefault(_ => _.NameIdentifier.Equals(NameIdentifier)) != null;

		// Set all properties equal to another account,
		// EXCEPT the ID.
		public void SetTo(Account account)
		{
			Username = account.Username;
			NameIdentifier = account.NameIdentifier;
			AvatarUrl = account.AvatarUrl;
		}

		// Only return the needed object properties on serialization. This
		// is to avoid including the auto-implemented lazy loading methods
		// when serializing to JSON with client.
		public string ToDTOJSON()
		{
			var propertiesDict = new Dictionary<string, object>();
			foreach(var prop in DTOProperties)
			{
				propertiesDict[prop.Name] = prop.GetValue(this);
			}

			return JsonConvert.SerializeObject(propertiesDict);
		}

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