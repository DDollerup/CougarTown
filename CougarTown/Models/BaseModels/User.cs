using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CougarTown.Models.BaseModels
{
    public class User : BaseModel
    {
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }

        public string Gender { get; set; }
        public int Age { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}