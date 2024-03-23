using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping
{
    public struct ProcedureParameter
    {
        public string Name;
        public object Value;
        public ParameterDirection Direction;
    }

    public abstract class DefiningDML //: DBPostgreSQL
    {
        protected string SqlSelect { get; set; }

        //protected abstract NpgsqlCommand SelectCommandText();
        public string _tableName { get; protected set; }
        protected string _procedureSelectName;
        protected string ProcedureSelectName { get { return _procedureSelectName; } }
        protected string ProcedureDMLName { get; set; }

        //public static NpgsqlDbType ConvertToDbType(DbType dbType)
        //{
        //    switch (dbType)
        //    {
        //        case DbType.AnsiString:
        //            return NpgsqlDbType.Varchar;
        //        case DbType.Boolean:
        //            return NpgsqlDbType.Char;
        //        case DbType.Date:
        //            return NpgsqlDbType.Date;
        //        case DbType.DateTime:
        //            return NpgsqlDbType.Date;
        //        case DbType.Double:
        //            return NpgsqlDbType.Double;
        //        case DbType.String:
        //            return NpgsqlDbType.Varchar;
        //        case DbType.Int32:
        //            return NpgsqlDbType.Integer;
        //        case DbType.Decimal:
        //            return NpgsqlDbType.Numeric;
        //        default:
        //            throw new NotImplementedException("Не определен передаваемый тип данных.");
        //    }
        //}
        //public static NpgsqlDbType ConvertToDbType(string dataType)
        //{
        //    switch (dataType)
        //    {
        //        case "String":
        //            return NpgsqlDbType.Varchar;
        //        case "DateTime":
        //            return NpgsqlDbType.Timestamp;
        //        case "Boolean":
        //            return NpgsqlDbType.Char;
        //        case "Int32":
        //            return NpgsqlDbType.Integer;
        //        case "Decimal":
        //            return NpgsqlDbType.Numeric;
        //        default:
        //            throw new NotImplementedException("Не определен передаваемый тип данных.");
        //    }
        //}
        //protected NpgsqlCommand SelectCommandText(string sql)
        //{
        //    NpgsqlCommand command = new NpgsqlCommand(sql);
        //    return command;
        //}
        //protected NpgsqlCommand UpdateCommandText(DataRow row)
        //{
        //    try
        //    {
        //        NpgsqlCommand command = new NpgsqlCommand();
        //        StringBuilder sbVal = new StringBuilder("UPDATE " + _tableName + " SET ");
        //        string str = null;
        //        foreach (DataColumn dataColumn in row.Table.Columns)
        //        {
        //            if (dataColumn.Caption != "id")
        //            {
        //                sbVal.Append(dataColumn.Caption);
        //                sbVal.Append(" = ");

        //                str = dataColumn.DataType.Name;

        //                if (str == "String")
        //                {
        //                    if (dataColumn.ExtendedProperties.ContainsKey("DataType"))
        //                    {
        //                        string property = dataColumn.ExtendedProperties["DataType"].ToString();
        //                        if (property == "String[]")
        //                        {
        //                            if (row[dataColumn] != DBNull.Value)
        //                            {
        //                                string val = row[dataColumn].ToString().Replace(';', ',');
        //                                val = val.Replace('\'', '"');
        //                                char[] ch = new char[1];
        //                                ch[0] = ',';
        //                                val = val.TrimEnd(ch);
        //                                sbVal.Append("ARRAY['" + val + "']");
        //                                //sbVal.Append("'{" + val + "}'");
        //                            }
        //                            else
        //                                sbVal.Append("null");
        //                        }
        //                        else if (dataColumn.ExtendedProperties["DataType"].ToString() == "Int32[]")
        //                        {
        //                            if (row[dataColumn] != DBNull.Value)
        //                            {
        //                                string val = row[dataColumn].ToString().Replace(';', ',');
        //                                val = val.Replace('\'', '"');
        //                                char[] ch = new char[1];
        //                                ch[0] = ',';
        //                                val = val.TrimEnd(ch);
        //                                sbVal.Append("ARRAY[" + val + "]");
        //                                //sbVal.Append("'{" + val + "}'");
        //                            }
        //                            else
        //                                sbVal.Append("null");
        //                        }
        //                        else
        //                        {
        //                            if (row[dataColumn] != DBNull.Value)
        //                            {
        //                                sbVal.Append("'" + row[dataColumn].ToString().Replace("'", "\"") + "'");
        //                            }
        //                            else
        //                                sbVal.Append("null");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (row[dataColumn] != DBNull.Value)
        //                        {
        //                            sbVal.Append("'" + row[dataColumn].ToString().Replace("'", "\"") + "'");
        //                        }
        //                        else
        //                            sbVal.Append("null");
        //                    }
        //                }
        //                else if (str == "String[]")
        //                {
        //                    if ((row[dataColumn] as string[]).Length != 0)
        //                    {
        //                        StringBuilder val = new StringBuilder();

        //                        foreach (string data in (string[])row[dataColumn])
        //                        {
        //                            if (data == null)
        //                                continue;
        //                            val.Append("'" + data.Replace("'", "\"") + "'");
        //                            val.Append(",");
        //                        }
        //                        if (val.Length != 0)
        //                            val.Remove(val.Length - 1, 1);
        //                        //ARRAY['"ASD"','ff'],
        //                        sbVal.Append("ARRAY[" + val + "]");
        //                        //sbVal.Append("'{" + val + "}'");
        //                    }
        //                    else
        //                        sbVal.Append("null");
        //                }
        //                else if (str == "DateTime")
        //                {
        //                    //string date;
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        //date = Convert.ToDateTime(row[dataColumn]).ToString("yyyy.MM.dd HH:mm:ss");
        //                        //sbVal.Append("to_date('" + row[dataColumn].ToString() + "','DD.MM.YYYY HH24:MI:SS')");
        //                        sbVal.Append("'" + row[dataColumn].ToString() + "'");
        //                    }
        //                    else
        //                        sbVal.Append("null");
        //                }
        //                else if (str == "Boolean")
        //                {
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        sbVal.Append(row[dataColumn]);
        //                    }
        //                    else
        //                        sbVal.Append("false");
        //                }
        //                else if (str == "Byte[]")
        //                {
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        string paramName = "@" + dataColumn.Caption;
        //                        sbVal.Append(paramName);
        //                        NpgsqlParameter param =
        //                            new NpgsqlParameter(paramName, NpgsqlDbType.Bytea);
        //                        param.Value = row[dataColumn];
        //                        command.Parameters.Add(param);
        //                    }
        //                    else
        //                        sbVal.Append("null");
        //                }
        //                else if (str == "Int32[]")
        //                {
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        int[] arr = (int[])row[dataColumn];
        //                        string val = "";//row[dataColumn].ToString().Replace(';', ',');
        //                        foreach (int item in arr)
        //                        {
        //                            if (val.Length != 0) val += ",";
        //                            val += item;
        //                        }
        //                        val = val.Replace('\'', '"');
        //                        char[] ch = new char[1];
        //                        ch[0] = ',';
        //                        val = val.TrimEnd(ch);
        //                        sbVal.Append("ARRAY[" + val + "]");
        //                        //sbVal.Append("'{" + val + "}'");
        //                    }
        //                    else
        //                        sbVal.Append("null");
        //                }
        //                else
        //                {
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        string val = row[dataColumn].ToString().Replace(',', '.');
        //                        sbVal.Append(val);
        //                    }
        //                    else
        //                        sbVal.Append("null");// ("0");
        //                }
        //                sbVal.Append(",");
        //            }
        //        }

        //        sbVal.Remove(sbVal.Length - 1, 1);
        //        sbVal.Append(" WHERE id = " + row["id"]);
        //        //sbVal.Append(" returning id into :pid");
        //        string sql = sbVal.ToString();
        //        command.CommandText = sql;
        //        return command;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NotImplementedException(ex.Message);
        //    }
        //}
        //private NpgsqlCommand InsertCommandText(DataRow row, List<string> retFields)
        //{
        //    try
        //    {
        //        NpgsqlCommand command = new NpgsqlCommand();
        //        StringBuilder sbIns = new StringBuilder("INSERT INTO " + _tableName + "(");
        //        StringBuilder sbVal = new StringBuilder("VALUES (");
        //        string str = null;
        //        foreach (DataColumn dataColumn in row.Table.Columns)
        //        {
        //            if (dataColumn.ColumnName.ToLower() != "id")
        //            {
        //                sbIns.Append(dataColumn.ColumnName);
        //                sbIns.Append(",");

        //                str = dataColumn.DataType.Name;
        //                if (str == "String")
        //                {
        //                    if (dataColumn.ExtendedProperties.ContainsKey("DataType"))
        //                    {
        //                        if (dataColumn.ExtendedProperties["DataType"].ToString() == "String[]")
        //                        {
        //                            if (row[dataColumn] != DBNull.Value)
        //                            {
        //                                string val = row[dataColumn].ToString().Replace(';', ',');
        //                                val = val.Replace('\'', '"');
        //                                char[] ch = new char[1];
        //                                ch[0] = ',';
        //                                val = val.TrimEnd(ch);
        //                                sbVal.Append("ARRAY['" + val + "']");
        //                                //sbVal.Append("'{" + val + "}'");
        //                            }
        //                            else
        //                                sbVal.Append("null");
        //                        }
        //                        else if (dataColumn.ExtendedProperties["DataType"].ToString() == "Int32[]")
        //                        {
        //                            if (row[dataColumn] != DBNull.Value)
        //                            {
        //                                string val = row[dataColumn].ToString().Replace(';', ',');
        //                                val = val.Replace('\'', '"');
        //                                char[] ch = new char[1];
        //                                ch[0] = ',';
        //                                val = val.TrimEnd(ch);
        //                                sbVal.Append("ARRAY[" + val + "]");
        //                                //sbVal.Append("'{" + val + "}'");
        //                            }
        //                            else
        //                                sbVal.Append("null");
        //                        }
        //                        else
        //                        {
        //                            if (row[dataColumn] != DBNull.Value)
        //                            {
        //                                sbVal.Append("'" + row[dataColumn].ToString().Replace("'", "''") + "'");
        //                            }
        //                            else
        //                                sbVal.Append("null");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (row[dataColumn] != DBNull.Value)
        //                        {
        //                            sbVal.Append("'" + row[dataColumn].ToString().Replace("'", "''") + "'");
        //                        }
        //                        else
        //                            sbVal.Append("null");
        //                    }
        //                }
        //                else if (str == "String[]")
        //                {
        //                    if ((row[dataColumn] as string[]).Length != 0)
        //                    {
        //                        StringBuilder val = new StringBuilder("");
        //                        foreach (string data in (string[])row[dataColumn])
        //                        {
        //                            if (data == null)
        //                                continue;
        //                            val.Append("'" + data.Replace("'", "\"") + "'");
        //                            val.Append(",");
        //                        }
        //                        if (val.Length != 0)
        //                            val.Remove(val.Length - 1, 1);
        //                        sbVal.Append("ARRAY[" + val + "]");
        //                        //sbVal.Append("'{" + val + "}'");
        //                    }
        //                    else
        //                        sbVal.Append("null");
        //                }
        //                else if (str == "DateTime")
        //                {
        //                    //string date;
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        //date = Convert.ToDateTime(row[dataColumn]).ToString("yyyy.MM.dd HH:mm:ss"); 
        //                        //sbVal.Append("to_date('" + row[dataColumn].ToString() + "','DD.MM.YYYY HH24:MI:SS')"); DB
        //                        sbVal.Append("'" + row[dataColumn].ToString() + "'");
        //                    }
        //                    else
        //                        sbVal.Append("null");
        //                }
        //                else if (str == "Boolean")
        //                {
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        sbVal.Append(row[dataColumn]);
        //                    }
        //                    else
        //                        sbVal.Append("false");
        //                }
        //                else if (str == "Byte[]")
        //                {
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        string paramName = "@" + dataColumn.ColumnName;
        //                        sbVal.Append(paramName);
        //                        NpgsqlParameter param =
        //                            new NpgsqlParameter(paramName, NpgsqlDbType.Bytea);
        //                        //OracleType.
        //                        param.Value = row[dataColumn];
        //                        command.Parameters.Add(param);
        //                    }
        //                    else
        //                        sbVal.Append("null");
        //                }
        //                else if (str == "Int32[]")
        //                {
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        int[] arr = (int[])row[dataColumn];
        //                        string val = "";//row[dataColumn].ToString().Replace(';', ',');
        //                        foreach (int item in arr)
        //                        {
        //                            if (val.Length != 0) val += ",";
        //                            val += item;
        //                        }
        //                        val = val.Replace('\'', '"');
        //                        char[] ch = new char[1];
        //                        ch[0] = ',';
        //                        val = val.TrimEnd(ch);
        //                        sbVal.Append("ARRAY[" + val + "]");
        //                        //sbVal.Append("'{" + val + "}'");
        //                    }
        //                    else
        //                        sbVal.Append("null");
        //                }
        //                else
        //                {
        //                    if (row[dataColumn] != DBNull.Value)
        //                    {
        //                        string val = row[dataColumn].ToString().Replace(',', '.');
        //                        sbVal.Append(val);
        //                    }
        //                    else
        //                        sbVal.Append("null");
        //                }
        //                sbVal.Append(",");
        //            }
        //        }

        //        sbIns.Remove(sbIns.Length - 1, 1);
        //        sbIns.Append(") ");
        //        sbVal.Remove(sbVal.Length - 1, 1);
        //        sbVal.Append(") ");
        //        sbIns.Append(sbVal);
        //        if (retFields != null)
        //        {
        //            sbIns.Append(" returning id,");
        //            foreach (string field in retFields)
        //            {
        //                sbIns.Append(field);
        //                sbIns.Append(",");
        //            }
        //            sbIns.Remove(sbIns.Length - 1, 1);
        //            sbIns.Append(";");
        //        }
        //        else
        //            sbIns.Append(" returning id;");
        //        /*if(TableName == "objects")
        //            sbIns.Append(" returning id,unik_kode_type;");
        //        else
        //            sbIns.Append(" returning id;");*/
        //        string sql = sbIns.ToString();
        //        command.CommandText = sql;
        //        return command;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NotImplementedException(ex.Message);
        //    }
        //}

        protected string ExecuteInsertCommand(DataRow row,string url, List<string> retFields)
        {
            StringBuilder sbVal = JsonBodyCommand(row);

            string jsonText = JsonResponse.PostJson(url, sbVal.ToString());

            return jsonText;
        }

        protected string ExecuteUpdateCommand(DataRow row,object id, string url)
        {
            StringBuilder sbVal = JsonBodyCommand(row);

            string jsonText = JsonResponse.PutJson(url, id, sbVal.ToString());

            return jsonText;
        }

        protected string ExecuteDeleteCommand(/*DataRow row,*/ object id, string url)
        {
            //StringBuilder sbVal = JsonBodyCommand(row);

            string jsonText = JsonResponse.DelJson(url, id/*, sbVal.ToString()*/);

            return jsonText;
        }

        private static StringBuilder JsonBodyCommand(DataRow row)
        {
            StringBuilder sbVal = new StringBuilder("");
            string strType = "";

            foreach (DataColumn dataColumn in row.Table.Columns)
            {
                if (dataColumn.ColumnName.ToLower() != "id")
                {
                    sbVal.Append($"\"{dataColumn.ColumnName}\": ");
                    strType = dataColumn.DataType.Name;

                    if (strType == "String")
                    {
                        if (row[dataColumn] != DBNull.Value)
                        {
                            string val = row[dataColumn].ToString().Replace(',', '.');
                            sbVal.Append($"\"{val}\" ");
                        }
                        else
                            sbVal.Append("null");
                    }
                    else if (strType == "String[]")
                    {
                    }
                    else if (strType == "DateTime")
                    {
                        if (row[dataColumn] != DBNull.Value)
                        {
                            string val = row[dataColumn].ToString().Replace(',', '.');
                            sbVal.Append($"\"{val}\" ");
                        }
                        else
                            sbVal.Append("null");
                    }
                    else if (strType == "Boolean")
                    {
                        if (row[dataColumn] != DBNull.Value)
                        {
                            sbVal.Append(row[dataColumn]);
                        }
                        else
                            sbVal.Append("false");
                    }
                    else if (strType == "Byte[]")
                    {
                    }
                    else if (strType == "Int32[]")
                    {
                    }
                    else
                    {
                        if (row[dataColumn] != DBNull.Value)
                        {
                            string val = row[dataColumn].ToString().Replace(',', '.');
                            sbVal.Append(val);
                        }
                        else
                            sbVal.Append("null");
                    }
                    if (row.Table.Columns.Count == (dataColumn.Ordinal+1))
                        break;
                    sbVal.Append(",");
                }
            }

            return sbVal;
        }

        //protected NpgsqlParameterCollection ExecuteInsertCommand(DataRow dr, List<string> retFields)
        //{
        //    NpgsqlCommand command = InsertCommandText(dr, retFields);
        //    command.Parameters.Add("p_id", NpgsqlDbType.Integer).Direction = ParameterDirection.Output;
        //    if (retFields != null)
        //    {
        //        foreach (string field in retFields)
        //        {
        //            NpgsqlParameter p = new NpgsqlParameter();
        //            p.ParameterName = "p_" + field;
        //            p.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(p);
        //        }
        //    }
        //    string ret;
        //    ExecuteNonQuery(command, out ret);
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //    //int newId = (Int32)command.Parameters["p_id"].Value;
        //    return command.Parameters; // newId;
        //}
        //protected NpgsqlCommand DeleteCommandText(DataRow row)
        //{
        //    try
        //    {
        //        //decimal id = row.Field<int>("id", DataRowVersion.Default);
        //        string sql = string.Format("delete from {0} where id = {1}",
        //            _tableName, row.Field<int>("id", DataRowVersion.Original));
        //        NpgsqlCommand command = new NpgsqlCommand(sql);
        //        return command;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NotImplementedException(ex.Message);
        //    }
        //}
        //protected NpgsqlCommand SelectCommandProcedure()
        //{
        //    if (ProcedureSelectName == null)
        //        throw new NotImplementedException("Не указана процедура на выбор данных.");
        //    NpgsqlCommand command = new NpgsqlCommand(ProcedureSelectName);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add("cur_out", NpgsqlDbType.Refcursor).Direction =
        //            ParameterDirection.Output;
        //    return command;
        //}
        //protected NpgsqlCommand SelectCommandProcedure(int cursorCount)
        //{
        //    if (ProcedureSelectName == null)
        //        throw new NotImplementedException("Не указана процедура на выбор данных.");
        //    NpgsqlCommand command = new NpgsqlCommand(ProcedureSelectName);
        //    //int cursorCount = 2 + relationCount;
        //    for (int i = 0; i < cursorCount; i++)
        //    {
        //        command.Parameters.Add("cur_out" + i.ToString(), NpgsqlDbType.Refcursor).Direction =
        //            ParameterDirection.Output;
        //    }
        //    command.CommandType = CommandType.StoredProcedure;

        //    return command;
        //}
        //protected NpgsqlCommand SelectCommandProcedure(object id, int cursorCount)
        //{
        //    if (ProcedureSelectName == null)
        //        throw new NotImplementedException("Не указана процедура на выбор данных.");
        //    NpgsqlCommand command = new NpgsqlCommand(ProcedureSelectName);
        //    command.Parameters.Add("p_id", NpgsqlDbType.Integer).Direction =
        //            ParameterDirection.Input;
        //    command.Parameters["p_id"].Value = id;
        //    for (int i = 0; i < cursorCount; i++)
        //    {
        //        command.Parameters.Add("cur_out" + i.ToString(), NpgsqlDbType.Refcursor).Direction =
        //            ParameterDirection.Output;
        //    }
        //    command.CommandType = CommandType.StoredProcedure;

        //    return command;
        //}
        //protected NpgsqlCommand SelectCommandProcedure(List<object> ids, int cursorCount)
        //{
        //    if (ProcedureSelectName == null)
        //        throw new NotImplementedException("Не указана процедура на выбор данных.");
        //    NpgsqlCommand command = new NpgsqlCommand(ProcedureSelectName);
        //    for (int i = 0; i < ids.Count; i++)
        //    {
        //        command.Parameters.Add("p_id" + i.ToString(), NpgsqlDbType.Integer).Direction =
        //                ParameterDirection.Input;
        //        command.Parameters["p_id" + i.ToString()].Value = ids[i];
        //    }
        //    for (int i = 0; i < cursorCount; i++)
        //    {
        //        command.Parameters.Add("cur_out" + i.ToString(), NpgsqlDbType.Refcursor).Direction =
        //            ParameterDirection.Output;
        //    }
        //    command.CommandType = CommandType.StoredProcedure;

        //    return command;
        //}
        //private NpgsqlCommand DMLCommandProcedure(DataRow row, string mode, bool withCurOut)
        //{
        //    try
        //    {
        //        if (ProcedureDMLName == string.Empty)
        //            throw new NotImplementedException("Не указана процедура для DML инструкций.");
        //        NpgsqlCommand command = new NpgsqlCommand(ProcedureDMLName);
        //        command.CommandType = CommandType.StoredProcedure;
        //        NpgsqlParameter parameter;
        //        foreach (DataColumn dataColumn in row.Table.Columns)
        //        {
        //            string paramName = "p_" + dataColumn.ColumnName;
        //            NpgsqlDbType DBType = ConvertToDbType(dataColumn.DataType.Name);
        //            parameter = new NpgsqlParameter(paramName, DBType);
        //            parameter.Direction = ParameterDirection.Input;
        //            if (row.RowState == DataRowState.Deleted)
        //            {
        //                if (row.Field<object>(dataColumn, DataRowVersion.Original) != null)
        //                    parameter.Value = row.Field<object>(dataColumn, DataRowVersion.Original);
        //                else
        //                    parameter.Value = DBNull.Value;
        //            }
        //            else
        //            {
        //                if (row.Field<object>(dataColumn) != null)
        //                    parameter.Value = row.Field<object>(dataColumn);
        //                else
        //                    parameter.Value = DBNull.Value;
        //            }
        //            command.Parameters.Add(parameter);
        //        }
        //        command.Parameters["p_id"].Direction = ParameterDirection.InputOutput;

        //        parameter = new NpgsqlParameter("ret_result", NpgsqlDbType.Varchar);
        //        parameter.Direction = ParameterDirection.InputOutput;
        //        parameter.Value = mode;
        //        command.Parameters.Add(parameter);
        //        if (withCurOut)
        //        {
        //            parameter = new NpgsqlParameter("cur_out", NpgsqlDbType.Refcursor);
        //            parameter.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(parameter);
        //        }
        //        return command;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NotImplementedException(ex.Message);
        //    }
        //}
        //protected DataTable ExecuteDMLCommand(DataRow dr, string mode, bool withCurOut)
        //{
        //    NpgsqlCommand oraCommand = DMLCommandProcedure(dr, mode, withCurOut);
        //    string ret;
        //    DataTable dt = base.ExecuteCommandDataTable(oraCommand, out ret);
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //    string retResult = (string)oraCommand.Parameters["ret_result"].Value.ToString();
        //    if (retResult != "1")
        //        throw new NotImplementedException(ret);
        //    // = (Int32)oraCommand.Parameters["p_ID"].Value;
        //    return dt;
        //}
        //protected int ExecuteDMLCommand(DataRow dr, string mode)
        //{
        //    NpgsqlCommand command = DMLCommandProcedure(dr, mode, false);
        //    string ret;
        //    base.ExecuteNonQuery(command, out ret);
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //    string retResult = (string)command.Parameters["ret_result"].Value.ToString();
        //    if (retResult != "1")
        //        throw new NotImplementedException(ret);
        //    int id = (Int32)command.Parameters["p_ID"].Value;
        //    return id;
        //}
        ///*protected OracleCommand UpdateCommandProcedure(DataRow row)
        //{
        //    try
        //    {
        //        OracleCommand command = new OracleCommand(ProcedureDMLName);
        //        command.CommandType = CommandType.StoredProcedure;
        //        foreach (DataColumn dataColumn in row.Table.Columns)
        //        {
        //            string paramName = "p_" + dataColumn.Caption;
        //            OracleType oracleType = ConvertToOracleDbType(dataColumn.DataType.Name);
        //            OracleParameter parameter = new OracleParameter(paramName, oracleType);
        //            parameter.Direction = ParameterDirection.Input;
        //            if (row[dataColumn] != DBNull.Value)
        //                parameter.Value = row[dataColumn];
        //            else
        //                parameter.Value = null;
        //            command.Parameters.Add(parameter);
        //        }
        //        command.Parameters.Add("ret_result", OracleType.VarChar).Direction = 
        //            ParameterDirection.InputOutput;   
        //        return command;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NotImplementedException(ex.Message);
        //    }
        //}
        //protected OracleCommand InsertCommandProcedure(DataRow row)
        //{
        //    try
        //    {
        //        OracleCommand command = new OracleCommand(ProcedureInsertName);
        //        command.CommandType = CommandType.StoredProcedure;
        //        foreach (DataColumn dataColumn in row.Table.Columns)
        //        {
        //            if (dataColumn.Caption != "id")
        //            {
        //                string paramName = "p_" + dataColumn.Caption;
        //                OracleType oracleType = ConvertToOracleDbType(dataColumn.DataType.Name);
        //                OracleParameter parameter = new OracleParameter(paramName, oracleType);
        //                parameter.Direction = ParameterDirection.Input;
        //                if (row[dataColumn] != DBNull.Value)
        //                    parameter.Value = row[dataColumn];
        //                else
        //                    parameter.Value = null;
        //                command.Parameters.Add(parameter);
        //            }
        //        }
        //        command.Parameters.Add("p_id", OracleType.Int32).Direction =
        //            ParameterDirection.InputOutput;
        //        return command;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NotImplementedException(ex.Message);
        //    }
        //}
        //protected OracleCommand DeleteCommandProcedure(DataRow row)
        //{
        //    try
        //    {
        //        OracleCommand command = new OracleCommand(ProcedureDeleteName);
        //        command.CommandType = CommandType.StoredProcedure;
        //        OracleParameter oraParameter = new OracleParameter("p_id", OracleType.Int32);
        //        oraParameter.Direction = ParameterDirection.Input;
        //        oraParameter.Value = row.Field<int>("id", DataRowVersion.Original);
        //        command.Parameters.Add(oraParameter);
        //        return command;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NotImplementedException(ex.Message);
        //    }
        //}
        //protected int ExecuteInsertCommand(DataRow dr)
        //{
        //    OracleCommand oraCommand = InsertCommandText(dr);
        //    oraCommand.Parameters.Add("p_id", OracleType.Int32).Direction = ParameterDirection.Output;
        //    string ret;
        //    ExecuteNonQuery(oraCommand, out ret);
        //    int newId = (Int32)oraCommand.Parameters["p_id"].Value;
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //    return newId;
        //}*/
        //protected void ExecuteCommand(NpgsqlCommand command)
        //{
        //    string ret;
        //    ExecuteNonQuery(command, out ret);
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //}
        //#region ExecuteSQL
        //protected object ExecProcedureResult(string procedureName, List<ProcedureParameter> listParams)
        //{
        //    string ret;
        //    object result = null;
        //    NpgsqlCommand command = new NpgsqlCommand();
        //    command.CommandText = procedureName;
        //    command.CommandType = CommandType.StoredProcedure;
        //    if (listParams != null)
        //    {
        //        foreach (ProcedureParameter param in listParams)
        //        {
        //            NpgsqlParameter p = new NpgsqlParameter(param.Name, param.Value);
        //            //p.OracleType = OracleType.Cursor;// DbType; // = param.DBType;
        //            //p.OracleType = OracleType.
        //            p.DbType = DbType.Object;
        //            p.Direction = param.Direction;
        //            command.Parameters.Add(p);
        //            //command.Parameters.Add(param);
        //            //command.Connection.Container.
        //        }
        //    }
        //    result = ExecuteCommandScalar(command, out ret);
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //    else
        //        return result;
        //}

        //protected object GetResultQuery(string sql)
        //{
        //    string ret;
        //    object result = null;
        //    NpgsqlCommand command = new NpgsqlCommand();
        //    command.CommandText = sql;
        //    result = ExecuteCommandScalar(command, out ret);
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //    else
        //        return result;
        //}

        //protected void GetResultQueryDontCatch(string sql)
        //{
        //    try
        //    {
        //        string ret;
        //        NpgsqlCommand command = new NpgsqlCommand();
        //        command.CommandText = sql;
        //        ExecuteCommandScalar(command, out ret);
        //    }
        //    catch { }
        //}

        //protected DataTable GetResultTableQuery(string sql)
        //{
        //    //object result;
        //    string ret;
        //    DataTable dt = GetDataSql(sql, out ret);
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //    else
        //        return dt;
        //}
        ///// <summary>
        ///// column data = byteSize and byteData
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <param name="bytes"></param>
        ///// <returns></returns>
        //protected string ExecQueryByte(string sql, byte[] bytes)
        //{
        //    string ret = "1";
        //    NpgsqlConnection npgConn = GetConn();
        //    NpgsqlCommand command = command = npgConn.CreateCommand();
        //    command.CommandText = sql;

        //    try
        //    {
        //        npgConn.Open();
        //        command.Parameters.Add("@byteData", NpgsqlDbType.Bytea).Value = bytes;
        //        command.Parameters.Add("@byteSize", NpgsqlDbType.Integer).Value = bytes.Length;
        //        command.ExecuteNonQuery();
        //        npgConn.Close();
        //    }
        //    catch (ArgumentException ae)
        //    {
        //        ret = ae.Message;
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        ret = ex.Message;
        //    }
        //    finally
        //    {
        //        command.Dispose();
        //        npgConn.Dispose();
        //    }
        //    return ret;
        //}

        //protected DataTable GetDataSqlTransaction(List<string> listSql)
        //{
        //    string ret;
        //    DataTable result = null;
        //    result = ExecuteSqlTransactionDB(listSql, out ret);
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //    else
        //        return result;
        //}
        //protected void ExecuteSqlTransaction(List<string> listSql)
        //{
        //    string ret = "1";
        //    ExecuteSqlTransactionDB(listSql, out ret);
        //    if (ret != "1")
        //        throw new NotImplementedException(ret);
        //}
        //#endregion
        //static Predicate<NpgsqlParameter> ByParamName(string name)
        //{
        //    return delegate (NpgsqlParameter param)
        //    {
        //        return param.ParameterName == name;
        //    };
        //}

    }    
}
