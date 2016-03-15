using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CougarTown.Models.BaseModels;

namespace CougarTown.Models.Factory
{
    public abstract class AutoFactory<T> where T : BaseModel
    {
        protected List<T> allEntities = new List<T>();
        abstract protected void SeedEntities();

        public AutoFactory()
        {
            SeedEntities();
        }

        public List<T> GetAll()
        {
            return allEntities;
        }
    }
}