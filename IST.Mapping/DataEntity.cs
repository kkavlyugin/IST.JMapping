using IST.JMapping.Interface;
using IST.JMapping.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping
{
    public struct FieldValue
    {
        public string fieldName;
        public object value;
    }
    public enum TypeQuery
    {
        Procedure = 1,
        SqlQuery
    }

    public struct Urls
    {
        public string _urlAdd;
        public string _urlEdit;
        public string _urlDel;
    }

    public abstract class DataEntity<T> : DefiningDML where T : IDataSource
    {
        Urls _urls;
        protected T DSource;
        public DataTableCollection DTSourceCollection { get; set; }
        private DataTable _dtsource;
        public DataTable DTSource { get { return _dtsource; } set { _dtsource = value; ExDTSourceChanged(); } }
        bool _notSave = false;
        public delegate void EventDTSourceChanged();
        public event EventDTSourceChanged DTSourceChanged;

        protected DataEntity()
        {

        }
        protected DataEntity(T source)
        {
            DSource = source;
        }
        protected DataEntity(string tableName)
        {
            _tableName = tableName;
            InitObject();
        }
        protected DataEntity(T source, string tableName)
        {
            _tableName = tableName;
            DSource = source;
            InitObject();
        }

        /*
        
        protected DataEntity(DataTable source, string tableName)
        {
            _tableName = tableName;
            DTSource = source;
            if (DSource != null)
            {
                //DTSource = DSource.GetData(_tableName);
                DTSource.TableName = _tableName;
                ReInitColumns();
            }
        }
        */
        public DataEntity(string tableName, string[] dbFieldsTable)
        {
            //_tableName = tableName;
            //SqlSelect = SqlSelectFromArray(dbFieldsTable, _tableName);
            //string ret;
            //DataTable dt = GetDataTable(SelectCommandText(), out ret);
            //if (ret != "1")
            //    throw new NotImplementedException(ret);
            //InitObject(dt);
        }

        private void InitObject()
        {
            if (DSource != null)
            {
                DTSource = DSource.GetData(_tableName);
                if (DTSource is null)
                {
                    DTSource = JMemoryDataSource.GetDataTable(_tableName);
                }
                DTSource.TableName = _tableName;
                ReInitColumns();                
            }
        }

        public void InitUrl(string urlAdd,string urlEdit, string urlDel)
        {
            _urls._urlAdd = urlAdd;
            _urls._urlEdit = urlEdit;
            _urls._urlDel = urlDel;
        }

        //public void InitDataSource(DataTable dataTable)
        public void InitDataSource(DataTableCollection dtCollection)        
        {
            DTSourceCollection = dtCollection;
            if(DTSource is null)
            {
                DTSource = new DataTable(_tableName);
                foreach (DataTable dtableIndex in DTSourceCollection)
                {
                    _dtsource.DataSet.Tables.Add(dtableIndex.TableName);
                }
            }            
            
            //DTSource = dataTable;
            //DTSourceCollection = dtCollection;
            //if (dtCollection.Count != 0)
            //{
            //    DTSourceCollection = dtCollection;
            //    DTSource = new DataTable(_tableName);
            //    DTSource = dtCollection[0];
            //    DTSource.TableName = _tableName;
            //    ReInitColumns();
            //}
        }

        //string SqlSelectFromArray(string[] array, string tableName)
        //{
        //    StringBuilder sb = new StringBuilder("select ");
        //    foreach (string str in array)
        //    {
        //        sb.Append(str);
        //        sb.Append(",");
        //    }
        //    sb.Replace(",", " ", sb.Length - 1, 1);
        //    if (array.Length > 1)
        //    {
        //        if (array[1].IndexOf("title") != -1)
        //            sb.Append("from " + tableName + " order by title;");
        //        else
        //            sb.Append("from " + tableName);
        //    }
        //    else
        //        sb.Append("from " + tableName);
        //    return sb.ToString();
        //}

        protected virtual void ReInitColumns() { }
        public bool IsDataChanges()
        {
            if (DTSource == null)
            {
                DevExMessage.MessageBoxWarning("Источник данных DataTable не инициализирован.");
                return false;
            }
            if (DTSource.GetChanges() != null)
                return true;
            else
                return false;
        }

        //protected override NpgsqlCommand SelectCommandText()
        //{
        //    NpgsqlCommand command = new NpgsqlCommand(SqlSelect);
        //    return command;
        //}

        /// <summary>
        /// Сохраняет все изменения при помощи указанных процедур
        /// </summary>
        //public bool SaveDataProcedure()
        //{
        //    if (ProcedureDMLName == string.Empty)
        //    {
        //        DevExMessage.MessageBoxWarning("Для сохранения данных необходимо указать имена DML процедур.");
        //        return false;
        //    }
        //    if (DTSource.GetChanges() == null)
        //        return true;
        //    bool isCatch = false;
        //    foreach (DataRow dr in DTSource.Rows)
        //    {
        //        try
        //        {
        //            switch (dr.RowState)
        //            {
        //                case DataRowState.Unchanged:
        //                    continue;
        //                case DataRowState.Added:
        //                    decimal id = ExecuteDMLCommand(dr, "insert");
        //                    dr.SetField<decimal>("id", id);
        //                    break;
        //                case DataRowState.Modified:
        //                    ExecuteDMLCommand(dr, "update");
        //                    break;
        //                case DataRowState.Deleted:
        //                    if (dr.Field<object>("ID", DataRowVersion.Original) == DBNull.Value ||
        //                        dr.Field<object>("ID", DataRowVersion.Original) == null)
        //                        break;
        //                    else
        //                        ExecuteDMLCommand(dr, "delete");
        //                    break;
        //                default:
        //                    break;
        //            }
        //            dr.ClearErrors();
        //        }
        //        catch (Exception ex)
        //        {
        //            dr.SetColumnError("id", ex.Message);
        //            if (ex.Message.IndexOf("23505") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Вносимые данные уже существуют.\n" +
        //                    ex.Message);
        //            else if (ex.Message.IndexOf("null value in column") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Необходимо внести значение.\n" +
        //                    ex.Message);
        //            else
        //            {
        //                if (!isCatch)
        //                    DevExMessage.MessageBoxError(ex.Message, "Ошибка сохранения данных");
        //            }
        //            isCatch = true;
        //        }

        //    }
        //    if (!isCatch)
        //    {
        //        DTSource.AcceptChanges();
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        //public bool SaveDataProcedure(List<FieldValue> fieldsValues)
        //{
        //    if (ProcedureDMLName == string.Empty)
        //    {
        //        DevExMessage.MessageBoxWarning("Для сохранения данных необходимо указать имена DML процедур.");
        //        return false;
        //    }
        //    if (DTSource.GetChanges() == null)
        //        return true;
        //    bool isCatch = false;
        //    foreach (DataRow dr in DTSource.Rows)
        //    {
        //        try
        //        {
        //            switch (dr.RowState)
        //            {
        //                case DataRowState.Unchanged:
        //                    continue;
        //                case DataRowState.Added:
        //                    foreach (FieldValue fieldVal in fieldsValues)
        //                    {
        //                        dr[fieldVal.fieldName] = fieldVal.value;
        //                    }
        //                    decimal id = ExecuteDMLCommand(dr, "insert");
        //                    dr.SetField<decimal>("id", id);
        //                    break;
        //                case DataRowState.Modified:
        //                    foreach (FieldValue fieldVal in fieldsValues)
        //                    {
        //                        dr[fieldVal.fieldName] = fieldVal.value;
        //                    }
        //                    ExecuteDMLCommand(dr, "update");
        //                    break;
        //                case DataRowState.Deleted:
        //                    if (dr.Field<object>("ID", DataRowVersion.Original) == DBNull.Value ||
        //                        dr.Field<object>("ID", DataRowVersion.Original) == null)
        //                        break;
        //                    else
        //                        ExecuteDMLCommand(dr, "delete");
        //                    break;
        //                default:
        //                    break;
        //            }
        //            dr.ClearErrors();
        //        }
        //        catch (Exception ex)
        //        {
        //            dr.SetColumnError("id", ex.Message);
        //            if (ex.Message.IndexOf("23505") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Вносимые данные уже существуют.\n" +
        //                    ex.Message);
        //            else if (ex.Message.IndexOf("null value in column") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Необходимо внести значение.\n" +
        //                    ex.Message);
        //            else
        //            {
        //                if (!isCatch)
        //                    DevExMessage.MessageBoxError(ex.Message, "Ошибка сохранения данных");
        //            }
        //            isCatch = true;
        //        }

        //    }
        //    if (!isCatch)
        //    {
        //        DTSource.AcceptChanges();
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        //public bool SaveDataProcedure(List<FieldValue> fieldsValues,
        //    List<string> nameRetFields)
        //{
        //    if (ProcedureDMLName == string.Empty)
        //    {
        //        DevExMessage.MessageBoxWarning("Для сохранения данных необходимо указать имена DML процедур.");
        //        return false;
        //    }
        //    if (DTSource.GetChanges() == null)
        //        return true;
        //    bool isCatch = false;
        //    foreach (DataRow dr in DTSource.Rows)
        //    {
        //        try
        //        {
        //            switch (dr.RowState)
        //            {
        //                case DataRowState.Unchanged:
        //                    continue;
        //                case DataRowState.Added:
        //                    if (fieldsValues != null)
        //                    {
        //                        foreach (FieldValue fieldVal in fieldsValues)
        //                        {
        //                            dr[fieldVal.fieldName] = fieldVal.value;
        //                        }
        //                    }
        //                    if (nameRetFields != null)
        //                    {
        //                        DataTable dt = ExecuteDMLCommand(dr, "insert", true);
        //                        foreach (string field in nameRetFields)
        //                            dr[field] = dt.Rows[0][field];
        //                        dr["id"] = dt.Rows[0]["ID"];
        //                    }
        //                    else
        //                    {
        //                        decimal id = ExecuteDMLCommand(dr, "insert");
        //                        dr.SetField<decimal>("id", id);
        //                    }
        //                    break;
        //                case DataRowState.Modified:
        //                    if (fieldsValues != null)
        //                    {
        //                        foreach (FieldValue fieldVal in fieldsValues)
        //                        {
        //                            dr[fieldVal.fieldName] = fieldVal.value;
        //                        }
        //                    }
        //                    if (nameRetFields != null)
        //                    {
        //                        DataTable dt = ExecuteDMLCommand(dr, "update", true);
        //                        foreach (string field in nameRetFields)
        //                            dr[field] = dt.Rows[0][field];
        //                        dr["id"] = dt.Rows[0]["ID"];
        //                    }
        //                    else
        //                    {
        //                        ExecuteDMLCommand(dr, "update");
        //                    }
        //                    break;
        //                case DataRowState.Deleted:
        //                    if (dr.Field<object>("ID", DataRowVersion.Original) == DBNull.Value ||
        //                        dr.Field<object>("ID", DataRowVersion.Original) == null)
        //                        break;
        //                    else
        //                    {
        //                        if (fieldsValues != null)
        //                        {
        //                            foreach (FieldValue fieldVal in fieldsValues)
        //                            {
        //                                dr[fieldVal.fieldName] = fieldVal.value;
        //                            }
        //                        }
        //                        if (nameRetFields != null)
        //                        {
        //                            DataTable dt = ExecuteDMLCommand(dr, "delete", true);
        //                            foreach (string field in nameRetFields)
        //                                dr[field] = dt.Rows[0][field];
        //                            dr["id"] = dt.Rows[0]["ID"];
        //                        }
        //                        else
        //                        {
        //                            ExecuteDMLCommand(dr, "delete");
        //                        }
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //            dr.ClearErrors();
        //        }
        //        catch (Exception ex)
        //        {
        //            dr.SetColumnError("id", ex.Message);
        //            if (ex.Message.IndexOf("23505") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Вносимые данные уже существуют.\n" +
        //                    ex.Message);
        //            else if (ex.Message.IndexOf("null value in column") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Необходимо внести значение.\n" +
        //                    ex.Message);
        //            else
        //            {
        //                if (!isCatch)
        //                    DevExMessage.MessageBoxError(ex.Message, "Ошибка сохранения данных");
        //            }
        //            isCatch = true;
        //        }

        //    }
        //    if (!isCatch)
        //    {
        //        DTSource.AcceptChanges();
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        /// <summary>
        /// Сохраняет все изменения при помощи сформированных sql запросов
        /// </summary>
        public bool SaveData()
        {
            if (_tableName == string.Empty)
            {
                DevExMessage.MessageBoxWarning("Не указано имя таблицы.");
                return false;
            }
            if (_notSave)
            {
                DevExMessage.MessageBoxWarning("Для сохранения данных необходимо использовать другую инициализацию объекта.");
                return false;
            }
            if (DTSource.GetChanges() == null)// "Нет данных для сохранения.";
                return true;
            bool isCatch = false;

            foreach (DataRow dr in DTSource.Rows)
            {
                try
                {
                    switch (dr.RowState)
                    {
                        case DataRowState.Unchanged:
                            continue;
                        case DataRowState.Added:                            
                            string id = ExecuteInsertCommand(dr,_urls._urlAdd, null);
                            //int id = (int)ExecuteInsertCommand(dr, null)["p_id"].Value;
                            dr.SetField<int>("id", Convert.ToInt32(id));
                            break;
                        case DataRowState.Modified:
                            ExecuteUpdateCommand(dr, dr["id"], _urls._urlEdit);
                            //ExecuteUpdateCommand(UpdateCommandText(dr));
                            break;
                        case DataRowState.Deleted:
                            ExecuteDeleteCommand(dr["id"], _urls._urlDel);
                            //if (dr.Field<object>("id", DataRowVersion.Original) == DBNull.Value)
                            //    DTSource.Rows.Remove(dr);
                            //else
                            //    ExecuteCommand(DeleteCommandText(dr));
                            break;
                        default:
                            break;
                    }
                    dr.ClearErrors();
                }
                catch (Exception ex)
                {
                    dr.SetColumnError(dr.Table.Columns.Count - 1, ex.Message);
                    if (ex.Message.IndexOf("23505") != -1 && !isCatch)
                        DevExMessage.MessageBoxWarning("Вносимые данные уже существуют.\n" +
                            ex.Message);
                    else if (ex.Message.IndexOf("null value in column") != -1 && !isCatch)
                        DevExMessage.MessageBoxWarning("Необходимо внести значение.\n" +
                            ex.Message);
                    else
                    {
                        if (!isCatch)
                            DevExMessage.MessageBoxError(ex.Message);
                    }
                    isCatch = true;
                }
            }
                //foreach (DataRow dr in DTSource.Rows)
                //{
                //    try
                //    {
                //        switch (dr.RowState)
                //        {
                //            case DataRowState.Unchanged:
                //                continue;
                //            case DataRowState.Added:
                //                int id = (int)ExecuteInsertCommand(dr, null)["p_id"].Value;
                //                dr.SetField<int>("id", id);
                //                break;
                //            case DataRowState.Modified:
                //                ExecuteCommand(UpdateCommandText(dr));
                //                break;
                //            case DataRowState.Deleted:
                //                if (dr.Field<object>("id", DataRowVersion.Original) == DBNull.Value)
                //                    DTSource.Rows.Remove(dr);
                //                else
                //                    ExecuteCommand(DeleteCommandText(dr));
                //                break;
                //            default:
                //                break;
                //        }
                //        dr.ClearErrors();
                //    }
                //    catch (Exception ex)
                //    {
                //        dr.SetColumnError(dr.Table.Columns.Count - 1, ex.Message);
                //        if (ex.Message.IndexOf("23505") != -1 && !isCatch)
                //            DevExMessage.MessageBoxWarning("Вносимые данные уже существуют.\n" +
                //                ex.Message);
                //        else if (ex.Message.IndexOf("null value in column") != -1 && !isCatch)
                //            DevExMessage.MessageBoxWarning("Необходимо внести значение.\n" +
                //                ex.Message);
                //        else
                //        {
                //            if (!isCatch)
                //                DevExMessage.MessageBoxError(ex.Message);
                //        }
                //        isCatch = true;
                //    }
                //}
                //if (!isCatch)
                //{
                //    DTSource.AcceptChanges();
                //    return true;
                //}
                //else
                //    return false;
                return false;
        }
        /// <summary>
        /// Сохранение данных
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="field">Имя поля для устанавливаемого значения</param>
        /// <param name="ret"></param>
        //public bool SaveData(object value, string field)
        //{
        //    if (_notSave)
        //    {
        //        DevExMessage.MessageBoxWarning("Для сохранения данных необходимо использовать другую инициализацию объекта.");
        //        return false;
        //    }
        //    if (DTSource.GetChanges() == null)
        //        return true;// "Нет данных для сохранения.";
        //    bool isCatch = false;
        //    foreach (DataRow dr in DTSource.Rows)
        //    {
        //        try
        //        {
        //            switch (dr.RowState)
        //            {
        //                case DataRowState.Unchanged:
        //                    continue;
        //                case DataRowState.Added:
        //                    dr[field] = value;
        //                    int id = (int)ExecuteInsertCommand(dr, null)["p_id"].Value;
        //                    dr.SetField<int>("id", id);
        //                    break;
        //                case DataRowState.Modified:
        //                    ExecuteCommand(UpdateCommandText(dr));
        //                    break;
        //                case DataRowState.Deleted:
        //                    ExecuteCommand(DeleteCommandText(dr));
        //                    break;
        //                default:
        //                    break;
        //            }
        //            dr.ClearErrors();

        //        }
        //        catch (Exception ex)
        //        {
        //            dr.SetColumnError(dr.Table.Columns.Count - 1, ex.Message);
        //            if (ex.Message.IndexOf("23505") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Вносимые данные уже существуют.\n" +
        //                    ex.Message);
        //            else if (ex.Message.IndexOf("null value in column") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Необходимо внести значение.\n" +
        //                    ex.Message);
        //            else
        //            {
        //                if (!isCatch)
        //                    DevExMessage.MessageBoxError(ex.Message);
        //            }
        //            isCatch = true;
        //        }
        //    }
        //    if (!isCatch)
        //    {
        //        DTSource.AcceptChanges();
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        /// <summary>
        /// Сохранение данных объекта
        /// </summary>
        /// <param name="fieldValue">Массив структур FieldValue. FieldValue хранит имя поля и устанавливаемое значение </param>
        /// <param name="ret">Результат выполнения</param>
        //public bool SaveData(List<FieldValue> fieldsValues, List<string> retFields)
        //{
        //    if (_notSave)
        //    {
        //        DevExMessage.MessageBoxWarning("Для сохранения данных необходимо использовать другую инициализацию объекта.");
        //        return false;
        //    }
        //    if (DTSource.GetChanges() == null)
        //        return true;// "Нет данных для сохранения.";
        //    bool isCatch = false;
        //    foreach (DataRow dr in DTSource.Rows)
        //    {
        //        try
        //        {
        //            switch (dr.RowState)
        //            {
        //                case DataRowState.Unchanged:
        //                    continue;
        //                case DataRowState.Added:
        //                    foreach (FieldValue fieldVal in fieldsValues)
        //                    {
        //                        dr[fieldVal.fieldName] = fieldVal.value;
        //                    }
        //                    foreach (Npgsql.NpgsqlParameter p in ExecuteInsertCommand(dr, retFields))
        //                    {
        //                        string name = p.ParameterName.Replace("p_", string.Empty);
        //                        if (dr.Table.Columns.IndexOf(name) == -1)
        //                        {
        //                            dr.Table.Columns.Add(name); //, typeof(object));
        //                        }
        //                        dr[name] = p.Value; // SetField<int>("id", p.Value);
        //                    }
        //                    break;
        //                case DataRowState.Modified:
        //                    ExecuteCommand(UpdateCommandText(dr));
        //                    break;
        //                case DataRowState.Deleted:
        //                    ExecuteCommand(DeleteCommandText(dr));
        //                    break;
        //                default:
        //                    break;
        //            }
        //            dr.ClearErrors();

        //        }
        //        catch (Exception ex)
        //        {
        //            dr.SetColumnError(dr.Table.Columns.Count - 1, ex.Message);
        //            if (ex.Message.IndexOf("23505") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Вносимые данные уже существуют.\n" +
        //                    ex.Message);
        //            else if (ex.Message.IndexOf("null value in column") != -1 && !isCatch)
        //                DevExMessage.MessageBoxWarning("Необходимо внести значение.\n" +
        //                    ex.Message);
        //            else
        //            {
        //                if (!isCatch)
        //                    DevExMessage.MessageBoxError(ex.Message);
        //            }
        //            isCatch = true;
        //        }
        //    }
        //    if (!isCatch)
        //    {
        //        DTSource.AcceptChanges();
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        public void UndoChange()
        {
            DTSource.RejectChanges();
        }
        void ExDTSourceChanged()
        {
            if (DTSourceChanged != null)
                DTSourceChanged();
        }
    }
}
