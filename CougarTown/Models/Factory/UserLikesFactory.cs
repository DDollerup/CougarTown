using CougarTown.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CougarTown.Models.Factory
{
    public class UserLikesFactory : AutoFactory<UserLikes>
    {
        public UserLikesFactory(HttpContextBase context) : base(context)
        {

        }

        protected override void SeedEntities()
        {
            
        }


    }
}