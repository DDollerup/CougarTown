using CougarTown.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CougarTown.Models.Factory
{
    public class UserFactory : AutoFactory<User>
    {
        protected override void SeedEntities()
        {
            allEntities.Add(new User()
            {
                ID = 1,
                DisplayName = "Chanel62",
                Password = "youngAdults1990",
                Name = "Dorte Nyholm",
                ProfileImage = "noprofileimageyet.jpg",
                Age = 62,
                Gender = "Female",
                Email = "dnyholm@live.dk"
            });

            allEntities.Add(new User()
            {
                ID = 1,
                DisplayName = "Hanne55",
                Password = "youngAdults1990",
                Name = "Hanne Fisker",
                ProfileImage = "noprofileimageyet.jpg",
                Age = 32,
                Gender = "Female",
                Email = "hanne.fisker@live.dk"
            });
        }
    }
}