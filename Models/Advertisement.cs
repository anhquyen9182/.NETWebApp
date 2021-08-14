using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2NET.Models
{
    public class Advertisement 
    {
        public int AdvertisementId
        {
            get;
            set;
        }
        public String CommunityID
        {
            get;
            set;
        }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        [DisplayName("File Name")]
        public string FileName
        {
            get;
            set;
        }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        [Url]
        public string Url
        {
            get;
            set;
        }

        public Community Community
        {
            get;
            set;
        }
    }
}
