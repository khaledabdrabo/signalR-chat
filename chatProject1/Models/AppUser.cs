using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatProject1.Models
{
    public class AppUser:IdentityUser
    {
        public AppUser()
        {
            messages = new HashSet<Message>();
            Friends = new HashSet<Friend>();
        }
        public string image { get; set; }
        public virtual ICollection<Message> messages { get; set; }
        //public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
    }
}
