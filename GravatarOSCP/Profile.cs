using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OutlookSocialProvider;

namespace Fujitsu.GravatarOSCP
{
	/// <summary>
	/// http://msdn.microsoft.com/es-ar/library/ff759399(en-us).aspx
	/// </summary>
	public class Profile : Person, ISocialProfile
	{
		public Profile(Session session, string emailHash)
			: base(session, emailHash)
		{

		}

		public bool[] AreFriendsOrColleagues(string[] userIDs)
		{
			var list = this.GetFriendsAndColleaguesIDs().ToList();
			list.Sort();
			
			return userIDs.Select(id => list.BinarySearch(id) >= 0).ToArray();
		}

		public string GetActivitiesOfFriendsAndColleagues(DateTime startTime)
		{
			throw new NotImplementedException();
		}

		public void SetStatus(string status)
		{
			throw new NotImplementedException();
		}
	}
}
