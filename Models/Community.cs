using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2NET.Models
{
    public class Community 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Registration Number")]
        [Required]
        public String ID
        {
            get;
            set;
        }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public String Title
        {
            get;
            set;
        }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget
        {
            get;
            set;
        }
        public Advertisement Advertisements
        {
            get;
            set;
        }

        public ICollection<CommunityMembership> CommunityMemberships
        {
            get;
            set;
        }
    }
}
