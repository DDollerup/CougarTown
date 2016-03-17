using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CougarTown.Models.BaseModels
{
    public class UserLikes : BaseModel
    {
        public int UserID { get; set; }
        public int OtherUserID { get; set; }
        public bool UserLike { get; set; }
    }
}