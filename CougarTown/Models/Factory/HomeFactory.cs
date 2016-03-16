using CougarTown.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CougarTown.Models.Factory
{
    public class HomeFactory : AutoFactory<Home>
    {
        public HomeFactory(HttpContextBase context) : base(context)
        {

        }


        protected override void SeedEntities()
        {
            allEntities.Add(new Home()
            {
                ID = 1,
                Text = "At CougarTown, you can find all your Cougar needs.",
                Image = "placeholder.jpg",
                MetaDescription = "",
                PageTitle = "Welcome to CougarTown"
            });
        }

        public Home GetHome(int id)
        {
            return allEntities.Find(x => x.ID == id);
        }
    }
}