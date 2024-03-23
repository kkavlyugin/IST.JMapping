using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping
{
    public class JsonUrlTable
    {
        public DataTable Data;
        public string Json;
        public string Url;
        public string TableName;
        public string md5;

        //public void Parse<T>()
        //{
        //    List<T> list = IST.JMapping.JsonResponse.JSONParseObject<T>(Json);
        //    DataTable dt = new DataTable();
        //    foreach (object item in list)
        //    {                                
        //        //dt.Rows.Add(item.id, item.title, item.rnOblID, item.countryID);
        //    }
        //    Data = dt;
        //}

        public JsonUrlTable(string url, string tableName)
        {
            Url = url;
            Json = "";
            TableName = tableName;
            Data = null;
            md5 = "";
        }

    }
}
