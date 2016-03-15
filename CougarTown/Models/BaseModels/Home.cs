using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CougarTown.Models.BaseModels
{
    public class Home : BaseModel
    {
        public int HomeID { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}