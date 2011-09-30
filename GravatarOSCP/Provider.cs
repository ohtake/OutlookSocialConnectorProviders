using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Fujitsu.GravatarOSCP.Properties;
using OutlookSocialProvider;

namespace Fujitsu.GravatarOSCP
{
	/// <summary>
	/// http://msdn.microsoft.com/es-ar/library/ff759387(en-us).aspx
	/// </summary>
	[ComVisible(true)]
	public class Provider : ISocialProvider
	{
		private const string NETWORK_NAME = "Gravatar";
		private const string VERSION = "1.1";
		private const string GUID = "{F36CEC74-F324-43BD-A30B-26EDFA23185E}";

		public string[] DefaultSiteUrls
		{
			get { return new[] { "http://www.gravatar.com/" }; }
		}

		public ISocialSession GetAutoConfiguredSession()
		{
			return new Session();
		}

		public string GetCapabilities()
		{
			var capabilities = new capabilities()
			{
				cacheActivities = false,
				cacheFriends = false,
				contactSyncRestartInterval = 10,
				contactSyncRestartIntervalSpecified = false,
				createAccountUrl = "http://www.gravatar.com/site/signup/",
				displayUrl = false,
				doNotFollowPerson = false,
				dynamicActivitiesLookup = false,
				dynamicActivitiesLookupSpecified = true,
				dynamicActivitiesLookupEx = false,
				dynamicActivitiesLookupExSpecified = true,
				dynamicContactsLookup = true,
				dynamicContactsLookupSpecified = true,
				followPerson = false,
				forgotPasswordUrl = "http://www.gravatar.com/accounts/forgot-password",
				getActivities = false,
				getFriends = true,
				hashFunction = "MD5",
				hideHyperlinks = false,
				hideHyperlinksSpecified = true,
				hideRememberMyPassword = false,
				hideRememberMyPasswordSpecified = true,
				showOnDemandActivitiesWhenMinimized = false,
				showOnDemandActivitiesWhenMinimizedSpecified = false,
				showOnDemandContactsWhenMinimized = true,
				showOnDemandContactsWhenMinimizedSpecified = true,
				supportsAutoConfigure = true,
				supportsAutoConfigureSpecified = true,
				useLogonCached = false,
				useLogonCachedSpecified = true,
				useLogonWebAuth = false,
			};
			return Utilities.SerializeObjectToString(capabilities);
		}

		public ISocialSession GetSession()
		{
			return new Session();
		}

		public void GetStatusSettings(out string statusDefault, out int maxStatusLength)
		{
			throw new NotImplementedException();
		}

		public void Load(string socialProviderInterfaceVersion, string languageTag)
		{
			
		}

		public Guid SocialNetworkGuid
		{
			get { return new Guid(GUID); }
		}

		public byte[] SocialNetworkIcon
		{
			get {
				var icon = Resources.Icon;
				using (MemoryStream s = new MemoryStream())
				{
					icon.Save(s, ImageFormat.Png);
					return s.GetBuffer();
				}
			}
		}

		public string SocialNetworkName
		{
			get { return NETWORK_NAME; }
		}

		public string Version
		{
			get { return VERSION; }
		}
	}
}
