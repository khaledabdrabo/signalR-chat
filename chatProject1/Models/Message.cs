using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chatProject1.Models
{
    public class Message
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string text { get; set; }

        public string userName { get; set; }
        public DateTime when { get; set; }
        [ForeignKey("User")]
        public string userID { get; set; }
        public AppUser sender { get; set; }
        
        public string senderID { get; set; }
        
    }
}
