using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using JWTDataAccess;
using JWTDataAccess.Employee;
using JWTDataAccess.Employee.Entity;
namespace JWTDataAccess.Employee
{
    public class Employee
    {
        //
        private static EmployeeEntity CreateReader(IDataReader reader)
        {
            EmployeeEntity obj = new EmployeeEntity();
            obj.id = (int)reader["id"];
            obj.sothenv = (string)reader["sothenv"];
            obj.tennv = (string)reader["tennv"];
            obj.donvinv = (string)reader["donvinv"];
            obj.ngaykyhd = (DateTime)reader["ngaykyhd"];
            return obj;
        }
        //
        public static IList<EmployeeEntity> EmployeeAll()
        {
            using (SqlConnection conn = GetConnection.CreateConnect())
            {
                SqlCommand com = new SqlCommand("employee_all", conn);
                com.CommandType = CommandType.StoredProcedure;
                IList<EmployeeEntity> employees = new List<EmployeeEntity>();
                try
                {
                    conn.Open();
                    using (IDataReader reader = com.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            employees.Add(CreateReader(reader));
                        }
                    }
                    return employees;
                }
                finally
                {
                    com.Dispose();
                    conn.Close();
                }
            }
        }
        //
        public static EmployeeEntity EmployeeAtId(int id)
        {
            using (SqlConnection conn = GetConnection.CreateConnect())
            {
                SqlCommand com = new SqlCommand("employee_at_id", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                try
                {
                    conn.Open();
                    EmployeeEntity employee = new EmployeeEntity();
                    employee = null;
                    using (IDataReader reader = com.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            employee = CreateReader(reader);
                        }
                    }
                    return employee;
                }
                finally
                {
                    com.Dispose();
                    conn.Close();
                }
            }
        }
        //
        public static int EmployeeInsert(string sothenv,
           string tennv,
           string donvinv,
           DateTime ngaykyhd)
        {
            using (SqlConnection conn = GetConnection.CreateConnect())
            {
                SqlCommand com = new SqlCommand("employee_insert", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@sothenv", sothenv);
                com.Parameters.AddWithValue("@tennv", tennv);
                com.Parameters.AddWithValue("@donvinv", donvinv);
                com.Parameters.AddWithValue("@ngaykyhd", ngaykyhd);
                try
                {
                    conn.Open();
                    int iKq = com.ExecuteNonQuery();
                    if (iKq == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                finally
                {
                    com.Dispose();
                    conn.Close();
                }
            }
        }
        //
        public static int EmployeeUpdate(int id,
           string sothenv,
           string tennv,
           string donvinv,
           DateTime ngaykyhd)
        {
            using (SqlConnection conn = GetConnection.CreateConnect())
            {
                SqlCommand com = new SqlCommand("employee_update", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                com.Parameters.AddWithValue("@sothenv", sothenv);
                com.Parameters.AddWithValue("@tennv", tennv);
                com.Parameters.AddWithValue("@donvinv", donvinv);
                com.Parameters.AddWithValue("@ngaykyhd", ngaykyhd);
                try
                {
                    conn.Open();
                    int iKq = com.ExecuteNonQuery();
                    if (iKq == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                finally
                {
                    com.Dispose();
                    conn.Close();
                }
            }
        }
        //
        public static int EmployeeDelete(int id)
        {
            using (SqlConnection conn = GetConnection.CreateConnect())
            {
                SqlCommand com = new SqlCommand("employee_delete", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                try
                {
                    conn.Open();
                    int iKq = com.ExecuteNonQuery();
                    if (iKq == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                finally
                {
                    com.Dispose();
                    conn.Close();
                }
            }
        }
        //
    }
}
