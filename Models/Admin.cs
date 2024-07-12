﻿using System.ComponentModel.DataAnnotations;

namespace SoleMates.Models
{
    public class Admin
    {
        [Key]
        public int admin_id { get; set; }

        [Required]
        public string admin_name { get; set; }
        [Required]
        public string admin_email { get; set; }
        [Required]
        public string admin_password { get; set; }
        [Required]
        public string admin_image { get; set; }
       
        




        
      
    }
}
