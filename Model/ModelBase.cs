using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Face_Recognition_App.Model
{
    public abstract class ModelBase
    {
        public string urlGetAll { get; set; }
        public string urlGet { get; set; }
        public string urlPut { get; set; }
        public string urlDelete { get; set; }

        public ModelBase(string GetAll="", string GetID = "", string Put = "", string Delete = "") {
            this.urlGetAll = GetAll;
            this.urlGet = GetID;
            this.urlPut = Put;
            this.urlDelete = Delete;
        }


    }
}
