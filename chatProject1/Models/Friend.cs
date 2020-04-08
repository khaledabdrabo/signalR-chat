using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chatProject1.Models
{
    public class Friend
    {
        
       
        public int Id { get; set; }
        public bool active { get; set; }
        [ForeignKey("user")]
        public string friendID { get; set; }
        public AppUser user { get; set; }
        
        public string userid { get; set; }

    }
}
