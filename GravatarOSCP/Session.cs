using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using OutlookSocialProvider;
using System.Security.Cryptography;

namespace Fujitsu.GravatarOSCP
{
	/// <summary>
	/// http://msdn.microsoft.com/es-ar/library/ff759378(en-us).aspx
	/// http://msdn.microsoft.com/es-ar/library/ff759461(en-us).aspx
	/// </summary>
	public class Session : ISocialSession, ISocialSession2
	{
		private ISocialProfile loggedOnUser;

		public string FindPerson(string userID)
		{
			throw new NotImplementedException();
		}

		public void FollowPerson(string emailAddress)
		{
			throw new NotImplementedException();
		}

		public string GetActivities(string[] emailAddresses, DateTime startTime)
		{
			throw new NotImplementedException();
		}

		public ISocialProfile GetLoggedOnUser()
		{
			return this.loggedOnUser;
		}

		public string GetLogonUrl()
		{
			throw new NotImplementedException();
		}

		public string GetNetworkIdentifier()
		{
			return "Gravatar Network Identifier";
		}

		public ISocialPerson GetPerson(string userID)
		{
			throw new NotImplementedException();
		}

		public string LoggedOnUserID
		{
			get;
			private set;
		}

		public string LoggedOnUserName
		{
			get;
			private set;
		}

		public void Logon(string userName, string password)
		{
			HashAlgorithm md5 = MD5.Create();
			byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(userName.Trim().ToLower()));
			string hashStr = string.Join(string.Empty, hash.Select(b => b.ToString("x2")).ToArray());
			
			this.LoggedOnUserName = userName;
			this.loggedOnUser = new Profile(this, hashStr);
		}

		public void LogonWeb(string connectIn, out string connectOut)
		{
			throw new NotImplementedException();
		}

		public string SiteUrl
		{
			set { throw new NotImplementedException(); }
		}

		public void UnFollowPerson(string userID)
		{
			throw new NotImplementedException();
		}

		public void FollowPersonEx(string[] emailAddresses, string displayName)
		{
			throw new NotImplementedException();
		}

		public string GetActivitiesEx(string[] hashedAddresses, DateTime startTime)
		{
			throw new NotImplementedException();
		}

		public string GetPeopleDetails(string personsAddresses)
		{
			// Outlook が送ってくる XML にはネームスペースがついていないのでつけておく
			var fixedXml = personsAddresses.Replace("<hashedAddresses>", @"<hashedAddresses xmlns=""http://schemas.microsoft.com/office/outlook/2010/06/socialprovider.xsd"">");
			var hashedAddrs = Utilities.DeserializeStringToObject<hashedAddresses>(fixedXml);
			var result = new List<personType>();
			foreach (var h in hashedAddrs.personAddresses)
			{
				var p = new Person(this, h.hashedAddress[0]);
				try
				{
					var detail = Utilities.DeserializeStringToObject<personType>(p.GetDetails());
					detail.index = h.index;
					detail.indexSpecified = true;
					result.Add(detail);
				}
				catch
				{
				}
			}
			return Utilities.SerializeObjectToString(new friends(){
				person = result.ToArray(),
			});
		}

		public void LogonCached(string connectIn, string userName, string password, out string connectOut)
		{
			throw new NotImplementedException();
		}
	}
}
