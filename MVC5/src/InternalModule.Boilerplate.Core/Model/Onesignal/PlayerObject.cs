using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Model.Onesignal
{
    public class PlayerObjectResponse
    {
        public int total_count { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public List<PlayerObject> players { get; set; }
    }

    public class PlayerObject
    {
        public string identifier { get; set; }
        public int session_count { get; set; }
        public string language { get; set; }
        public int timezone { get; set; }
        public string game_version { get; set; }
        public string device_os { get; set; }
        public int device_type { get; set; }
        public string device_model { get; set; }
        public string ad_id { get; set; }
        public Dictionary<string, string> tags { get; set; }
        public int last_active { get; set; }
        public string amount_spent { get; set; }
        public int created_at { get; set; }
        public bool invalid_identifier { get; set; }
        public int badge_count { get; set; }
    }
}
