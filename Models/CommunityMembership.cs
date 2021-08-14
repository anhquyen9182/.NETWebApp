using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2NET.Models
{
    public class CommunityMembership 
    {
        public int StudentID
        {
            get;
            set;
        }

        public String CommunityID
        {
            get;
            set;
        }

        public Community Community
        {
            get;
            set;
        }

        public Student Student
        {
            get;
            set;
        }
    }
}
