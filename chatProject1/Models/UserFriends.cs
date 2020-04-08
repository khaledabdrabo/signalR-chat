using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatProject1.Models
{
    public class UserFriend
    {
        
        public string userId { get; set; }
        public string friendId { get; set; }
        public AppUser User { get; set; }
        public Friend Friend { get; set; }
    }
}
