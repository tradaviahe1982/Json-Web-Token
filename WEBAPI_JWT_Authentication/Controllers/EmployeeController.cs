using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JWTDataAccess;
using JWTDataAccess.Employee;
using JWTDataAccess.Employee.Entity;
using Newtonsoft.Json;
//

namespace WEBAPI_JWT_Authentication.Controllers
{
    [Authorize]
    public class EmployeeController : ApiController
    {
        //
        // GET api/employee
        public string Get()
        {
            //
            string ketqua = "";
            IList<EmployeeEntity> listEmployee = Employee.EmployeeAll();
            ketqua = JsonConvert.SerializeObject(listEmployee);
            return ketqua;
            //
        }

        // GET api/employee/5
        public string Get(int id)
        {
            string ketqua = "";
            EmployeeEntity employee = Employee.EmployeeAtId(id);
            ketqua = JsonConvert.SerializeObject(employee);
            return ketqua;
        }

        // POST api/employee
        public int Post([FromBody]EmployeeEntity value)
        {
            //
            int iKq = Employee.EmployeeInsert(value.sothenv, value.tennv, value.donvinv, value.ngaykyhd);
            return iKq;
        }

        // PUT api/employee/5
        public int Put(int id, [FromBody]EmployeeEntity value)
        {
            //
            int iKq = Employee.EmployeeUpdate(id, value.sothenv, value.tennv, value.donvinv, value.ngaykyhd);
            return iKq;
            //
        }

        // DELETE api/employee/5

        public int Delete(int id)
        {
            int iKq = Employee.EmployeeDelete(id);
            return iKq;
        }
        //
    }
}
