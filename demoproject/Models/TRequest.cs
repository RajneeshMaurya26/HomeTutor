using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demoproject.Models
{
    public class TRequest
    {
        public string RequestId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Course { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Mob_Number { get; set; }
        public string Address { get; set; }
        public string Budget { get; set; }
        public string ExpectedDate { get; set; }
        public string Description { get; set; }
    }
}