using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using OutlookSocialProvider;
using System.Xml;

namespace Fujitsu.GravatarOSCP
{
	/// <summary>
	/// http://msdn.microsoft.com/es-ar/library/ff759383(en-us).aspx
	/// </summary>
	public class Person : ISocialPerson
	{
		private Session session;
		public string EmailHash
		{
			get;
			private set;
		}
		
		public Person(Session session, string emailHash)
		{
			this.session = session;
			this.EmailHash = emailHash;
		}

		public string GetActivities(DateTime startTime)
		{
			throw new NotImplementedException();
		}

		public string GetDetails()
		{
			string profileXmlUri = string.Format("http://en.gravatar.com/{0}.xml", this.EmailHash); // www だと en にリダイレクトされるので事前に en にする
			personType detail;
			WebClient wc = new WebClient();

			try
			{
				using (var resStream = wc.OpenRead(profileXmlUri))
				{
					XmlDocument doc = new XmlDocument();
					doc.Load(resStream);

					detail = new personType()
					{
						userID = this.EmailHash,
						webProfilePage = profileXmlUri,
						fullName = GetTextOrDefault(doc.DocumentElement, "formatted") ?? "NO FULLNAME",
						pictureUrl = GetTextOrDefault(doc.DocumentElement, "thumbnailUrl"),
					};
					var elemListEmail = doc.GetElementsByTagName("emails");
					var listEmail = new List<string>(elemListEmail.Count);
					for (var i = 0; i < elemListEmail.Count; i++)
					{
						listEmail.Add(GetTextOrDefault(elemListEmail[i] as XmlElement, "value"));
					}
					if (listEmail.Count >= 3) detail.emailAddress3 = listEmail[2];
					if (listEmail.Count >= 2) detail.emailAddress3 = listEmail[1];
					if (listEmail.Count >= 1) detail.emailAddress3 = listEmail[0];


				}
			}
			catch (WebException ex)
			{
				detail = new personType()
				{
					userID = this.EmailHash,
					fullName = ex.Message,
					pictureUrl = string.Format("http://www.gravatar.com/avatar/{0}?d=identicon", this.EmailHash),
				};
			}
			return Utilities.SerializeObjectToString(detail);
		}

		private static string GetTextOrDefault(XmlElement elem, string tagName)
		{
			var elems = elem.GetElementsByTagName(tagName);
			if (elems.Count > 0)
			{
				return elems[0].InnerText;
			}
			else
			{
				return default(string);
			}
		}

		public string GetFriendsAndColleagues()
		{
			throw new NotImplementedException();
		}

		public string[] GetFriendsAndColleaguesIDs()
		{
			throw new NotImplementedException();
		}

		public byte[] GetPicture()
		{
			var uri = string.Format("http://www.gravatar.com/avatar/{0}?default=identicon", this.EmailHash);
			WebClient wc = new WebClient();
			return wc.DownloadData(uri);
		}

		public string GetStatus()
		{
			throw new NotImplementedException();
		}
	}
}
