using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2NET.Models.ViewModels
{
    public class StudentViewModel 
    {
        public IEnumerable<Student> Students { 
            get; 
            set; 
        }
        public IEnumerable<Community> Communities { 
            get; 
            set; 
        }
        public IEnumerable<CommunityMembership> CommunityMemberships { 
            get; 
            set; 
        }
    }
}
