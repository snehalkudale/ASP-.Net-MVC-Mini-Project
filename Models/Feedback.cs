﻿using System.ComponentModel.DataAnnotations;

namespace SoleMates.Models
{
    public class Feedback
    {
        [Key]
        public int feedback_id { get; set; }
        public string user_name { get; set; }
        public string user_message { get; set; }
    }
}
