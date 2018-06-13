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

namespace ChatDefenders.Data
{
    public partial class Account
    {
		public int ID { get; set; }
		public string Username { get; set; }
		public string NameIdentifier { get; set; }
		public string AvatarUrl { get; set; }
	}
	[Serializable]
	public partial class Account : ISetable<Account>, ISerializable
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
			else if (!PropertiesEqualExcludeID(databaseAccount))
			{
				WriteLine("youch");
				databaseAccount.SetTo(this);
				context.Entry(databaseAccount).State = EntityState.Modified;
				context.Update(databaseAccount);

				context.SaveChanges();
			}
			else
			{
				WriteLine("zippeee!");
			}
			return this;
		}

		public bool IsRegistered() => 
			DataUtilities.GetContextInstance<PostContext>()
				.Accounts
				.FirstOrDefault(_ => _.NameIdentifier.Equals(NameIdentifier)) != null;

		// Check for equality between all properties, except for
		// ID.
		public bool PropertiesEqualExcludeID(Account account)
		{
			Type objType = GetType();
			foreach(var property in objType.GetProperties())
			{
				if (property.Name == "ID")
				{
					continue;
				}

				object currentPropertyValue = property.GetValue(this, null);
				object comparingPropertyValue = property.GetValue(account, null);

				if (!currentPropertyValue.Equals(comparingPropertyValue))
				{
					return false;
				}
			}
			return true;
		}

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
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Username", Username);
			info.AddValue("NameIdentifier", NameIdentifier);
			info.AddValue("AvatarUrl", AvatarUrl);
		}

		public static Account GetByUserIdentity(ClaimsIdentity userIdentity)
		{
			var userIdentifier = userIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if(userIdentifier == null)
			{
				return null;
			}

			return DataUtilities.GetContextInstance<PostContext>()
					.Accounts
					.FirstOrDefault(_ => _.NameIdentifier.Equals(userIdentifier));
		}
	}
}