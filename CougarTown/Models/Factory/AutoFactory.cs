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


        private string session = typeof(T).ToString();
        private HttpContextBase context;

        public AutoFactory(HttpContextBase context)
        {
            this.context = context;

            // if there's an existing session, use that one
            if (this.context.Session[session] != null)
            {
                allEntities = this.context.Session[session] as List<T>;
            }
            else // if there is no session, we seed our list, and create a session
            {
                SeedEntities();
                this.context.Session[session] = allEntities;
            }
        }

        public List<T> GetAll()
        {
            return allEntities;
        }

        public void Add(T entity)
        {
            allEntities.Add(entity);
            this.context.Session[session] = allEntities;
        }

        public void Remove(T entity)
        {
            allEntities.Remove(entity);
            this.context.Session[session] = allEntities;
        }

        public void Update(T entity)
        {
            
        }
    }
}