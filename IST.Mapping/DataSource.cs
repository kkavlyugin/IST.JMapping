using IST.JMapping.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping
{
    public class DataSource : DefiningDML, IDataSource
    {
        private DataSet _source;

        public DataSource(List<JsonUrlTable> jsons)
        {
            string ret;
            _source = GetData(jsons, out ret);
            if (ret != "1")
                throw new NotImplementedException(ret);
        }

        public void AddTable(DataTable table)
        {
            _source.Tables.Add(table);
        }

        public DataTableCollection GetData()
        {
            return _source.Tables;
        }

        public DataTable GetData(string tableName)
        {
            return _source.Tables[_source.Tables.IndexOf(tableName)];
        }

        public DataSet GetDataSet()
        {
            return _source;
        }

        protected DataSet GetData(List<JsonUrlTable> jsons, out string ret)
        {
            ret = "1";
            DataSet result = new DataSet();
            string md5;
            foreach (var json in jsons)
            {
                DataTable dt = new DataTable(json.TableName);
                json.Json = JsonResponse.GetJson(json.Url, json.TableName);
                if (json.Json == "")
                {
                    result.Tables.Add(JMemoryDataSource.GetDataTable(json.TableName));
                    result.Tables[result.Tables.Count - 1].TableName = json.TableName;
                }
                //else if (JMemoryDataSource.GetContains(json.TableName))
                //{
                //    result.Tables.Add(JMemoryDataSource.GetDataTable(json.TableName));
                //    result.Tables[result.Tables.Count - 1].TableName = json.TableName;
                //}
                else
                {
                    dt = JsonResponse.JsonToDataTableMemory(json.Json);
                    foreach (DataRow item in dt.Rows)
                    {
                        item.AcceptChanges();
                    }
                    result.Tables.Add(dt);
                    result.Tables[result.Tables.Count - 1].TableName = json.TableName;
                    //if (md5 != "")
                    //{
                    //    json.md5 = md5;
                    //    JMemoryDataSource.Remove(json.TableName);
                    //    JMemoryDataSource.Add(result.Tables[result.Tables.Count - 1],md5);
                    //}
                }
            }            

            return result;
        }
    }
}
