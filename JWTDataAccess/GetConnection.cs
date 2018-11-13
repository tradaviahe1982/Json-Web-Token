using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace JWTDataAccess
{
    public static class GetConnection
    {
        public static SqlConnection CreateConnect()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["JWT"].ConnectionString);
        }
    }
}
