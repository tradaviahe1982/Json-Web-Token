using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
//
namespace JWTDataAccess
{
    public class clsSqlServerClient
    {
        //
        public SqlConnection cn = new SqlConnection();
        public SqlCommand cm = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        //
        private string strErr;
        public string getError
        {
            get { return strErr; }
            set { strErr = value; }
        }

        public SqlDataReader ExeSQL(string strSQL)
        {
            try
            {
                SqlDataReader drResu;

                cm.CommandType = CommandType.Text;
                cm.CommandText = strSQL;
                cm.Parameters.Clear();
                drResu = cm.ExecuteReader();

                return drResu;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        public int ExeSQLCmm(string strSQL)
        {
            try
            {

                cm.Parameters.Clear();
                cm.CommandType = CommandType.Text;
                cm.CommandText = strSQL;
                cm.ExecuteNonQuery();

                return 1;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return -1;
            }
        }

        public DataSet GetDs(string strSQL)
        {

            try
            {
                DataSet ds = new DataSet();

                cm.CommandType = CommandType.Text;
                cm.Parameters.Clear();
                cm.CommandText = strSQL;
                da.SelectCommand = cm;
                da.Fill(ds);
                return ds;
            }
            catch (InvalidOperationException ex)
            {
                if (strErr == null)
                {
                    strErr = clsCommon.strLoithaotac;
                }
                string loi = ex.Message;
            }
            catch (TimeoutException ex)
            {
                if (strErr == null)
                {
                    strErr = clsCommon.strLoiquatg;
                }
                string loi = ex.Message;
            }
            catch (SqlException ex)
            {
                string loi = ex.Message;
                if (strErr == null)
                {
                    strErr = clsCommon.strLoiDb;
                }
            }
            catch (Exception ex)
            {
                if (strErr == null)
                {
                    strErr = clsCommon.strLoiCm;
                }
                string loi = ex.Message;
            }
            return null;
        }

        public DataTable GetDsFromStoredP(string strSQL)
        {
            DataSet ds = new DataSet();
            try
            {

                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = strSQL;

                da.SelectCommand = cm;
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                string loi = ex.Message;
                if (strErr == null)
                {
                    strErr = clsCommon.strLoiCm;
                }
                return null;
            }
        }

        public int execStoredProc()
        {
            try
            {

                cm.Connection = cn;
                cm.CommandType = CommandType.StoredProcedure;
                cm.ExecuteNonQuery();

                return 1;
            }
            catch (Exception ex)
            {
                string loi = ex.Message;
                if (strErr == null)
                {
                    strErr = clsCommon.strLoiCm;
                }
                return 0;
            }
        }

        public clsSqlServerClient(string strcn)
        {
            try
            {
                cn.ConnectionString = strcn;
                cn.Open();
                cm.Connection = cn;
            }
            catch (Exception ex)
            {
                string loi = ex.Message;
                strErr = clsCommon.strLoikhongKN;
            }
        }
        //
    }
}
