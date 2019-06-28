using System;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public abstract class Model
    {
        public int ID { get; set; }

        public Model() {}

        public Model(int ID)
        {
            this.ID = ID;   
        }

        public virtual void UpdateDB(DBTable dbModel)
        {
            dbModel.Id = ID;
        }
    }
}
