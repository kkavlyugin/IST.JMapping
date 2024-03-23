using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping.Interface
{
    public interface IDataSource
    {
        DataTable GetData(string tableName);
        DataTableCollection GetData();
        DataSet GetDataSet();
        void AddTable(DataTable table);
    }
}
