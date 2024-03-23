using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping
{
    public struct JMemoryDataSource
    {
        static Dictionary<string,DTSourceEx> ListDTSourceEx = new Dictionary<string, DTSourceEx>();

        //static DataTableCollection _DTSourceCollection;
        
        public static DataTable GetDataTable(string tableName)
        {
            DTSourceEx source;
            if(ListDTSourceEx.TryGetValue(tableName, out source))
                return source.data;

            return null;
            //return DTSourceCollection[tableName];
        }
        /// <summary>
        /// Сводка:
        ///     Возвращает значение, указывающее, является ли DataTable объект с
        ///     указанным именем существует в коллекции.
        ///
        /// Параметры:
        ///   name:
        ///     Имя System.Data.DataTable для поиска.
        ///
        /// Возврат:
        ///     true Если указанная таблица существует; в противном случае false.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns></returns>
        public static bool GetContains(string tableName)
        {
            DTSourceEx source;
            return ListDTSourceEx.TryGetValue(tableName, out source);
            //if (DTSourceCollection is null)
            //    return false;

            //return DTSourceCollection.Contains(tableName);
        }

        public static string GetMD5(string tableName)
        {
            DTSourceEx source;
            if (ListDTSourceEx.TryGetValue(tableName, out source))
            {
                return source.md5;
            }
            return "";
        }

        public static void Add(DataTable dt, string md5)
        {
            DTSourceEx source;
            if (ListDTSourceEx.TryGetValue(dt.TableName, out source))
            {
                if (source.md5 != md5)
                {
                    source.data = dt;
                    source.md5 = md5;
                }
            }
            else
            {
                ListDTSourceEx.Add(dt.TableName, new DTSourceEx { data = dt, md5 = md5, tableName = dt.TableName });
            }
        }

        //public static void AddRange(DataTableCollection dtSource,bool refresh = false)
        //{
        //    if (DTSourceCollection is null)
        //    {
        //        DTSourceCollection = dtSource;
        //        return;
        //    }
        //    foreach (DataTable item in dtSource)
        //    {
        //        if (refresh == true)
        //        {
        //            DTSourceCollection.Remove(item.TableName);
        //            DTSourceCollection.Add(item);
        //        }
        //        else
        //        {
        //            if (!DTSourceCollection.Contains(item.TableName))
        //                DTSourceCollection.Add(item);
        //        }
        //    }
        //}

        public static void Remove(string tableName)
        {
            DTSourceEx source;
            if (ListDTSourceEx.TryGetValue(tableName,out source))
            {
                ListDTSourceEx.Remove(tableName);
            }
            //DataTable dtSource = DTSourceCollection[tableName];
            //if (dtSource is null)
            //{
            //    DTSourceCollection.na
            //    DTSourceCollection.Remove(tableName);
            //}
        }
    }

    public class DTSourceEx
    {
        public DataTable data;
        public string md5;
        public string tableName;
    }

}
 