using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chacal.Client.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class FollowInfo
    {
        public int followingCount { get; set; }
        public int followerCount { get; set; }
        public int followStatus { get; set; }
        public int pushStatus { get; set; }
    }

    public class Comment
    {
        public string comment { get; set; }
        public string userId { get; set; }
        public string secUid { get; set; }
        public string uniqueId { get; set; }
        public string nickname { get; set; }
        public string profilePictureUrl { get; set; }
        public int followRole { get; set; }
        public List<UserBadge> userBadges { get; set; }
        public UserDetails userDetails { get; set; }
        public FollowInfo followInfo { get; set; }
        public bool isModerator { get; set; }
        public bool isNewGifter { get; set; }
        public bool isSubscriber { get; set; }
        public object topGifterRank { get; set; }
        public string msgId { get; set; }
        public string createTime { get; set; }
    }

    public class UserBadge
    {
        public string type { get; set; }
        public string name { get; set; }
        public int? displayType { get; set; }
        public string url { get; set; }
    }

    public class UserDetails
    {
        public string createTime { get; set; }
        public string bioDescription { get; set; }
        public List<string> profilePictureUrls { get; set; }
    }


}
