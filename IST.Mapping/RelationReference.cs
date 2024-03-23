using IST.JMapping.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping
{
    class RelationReference : DataEntity<IDataSource>
    {
        public RelationReference()
        {
        }
        public RelationReference(string tableName, string[] dbFieldsTable) :
            base(tableName, dbFieldsTable)
        {
        }
        public RelationReference(IDataSource source, string tableName) :
            base(source, tableName)
        {
        }

        public RelationReference(string tableName) :
            base(tableName)
        {
        }
        /*public RelationReference(string query, TypeQuery type) :
            base(query, type)
        {
        }*/
        protected override void ReInitColumns()
        {
            try
            {
                foreach (DataColumn col in DTSource.Columns)
                {
                    if (col.ColumnName.ToLower() == "id")
                    {
                        col.Caption = col.ColumnName;
                        col.ColumnName = "код";
                    }
                    else if (col.ColumnName.ToLower() == "title" ||
                        col.ColumnName.ToLower() == "name")
                    {
                        col.Caption = col.ColumnName;
                        col.ColumnName = "наименование";
                    }
                    else if (col.ColumnName.ToLower() == "title2")
                    {
                        col.Caption = col.ColumnName;
                        col.ColumnName = "додатково";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }

        }
        public void ReNameColumns()
        {
            ReInitColumns();
        }

    }
}
