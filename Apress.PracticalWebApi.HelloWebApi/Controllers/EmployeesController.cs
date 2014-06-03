using Apress.PracticalWebApi.HelloWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace Apress.PracticalWebApi.HelloWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        private static IList<Employee> list = new List<Employee>()
        {
            new Employee()
            {
                Id = 12345,
                FirstName = "Jhon",
                LastName = "Human",
                Department = 2
            },
            new Employee()
            {
                Id = 12346,
                FirstName = "Jane",
                LastName = "Public",
                Department = 3
            },
            new Employee()
            {
                Id = 12347,
                FirstName = "Joseph",
                LastName = "Law",
                Department = 2
            }
        };

        #region exercise 1
        // GET api/employees
        public IEnumerable<Employee> Get()
        {
            return list;
        }

        // GET api/employees/12345
        public Employee Get(int id)
        {
            return list.First(e => e.Id == id);
        }

        // POST api/employees
        public void Post(Employee employee)
        {
            int maxId = list.Max(e => e.Id);
            employee.Id = maxId + 1;

            list.Add(employee);
        }

        // PUT api/employees/12345
        public void Put(int id, Employee employee)
        {
            int index = list.ToList().FindIndex(e => e.Id == id);
            list[index] = employee;
        }

        // DELETE api/employees/12345
        public void Delete(int id)
        {
            Employee employee = Get(id);
            list.Remove(employee);
        }
        #endregion

        #region exercise 2
        //[HttpGet]
        //public Employee RetrieveEmployeeById(int id)
        //{
        //    return list.First(e => e.Id == id);
        //}
        #endregion

        #region exercise 3
        //[HttpGet]
        //public IEnumerable<Employee> RpcStyleGet()
        //{
        //    return list;
        //}

        //[HttpGet]
        //public Employee GetEmployeeRpcStyle(int id)
        //{
        //    return list.First(e => e.Id == id);
        //}
        #endregion

        #region exercise 4
        //public Employee Get(int orgid, int id)
        //{
        //    return list.First(e => e.Id == id);
        //}
        #endregion

        #region exercise 5
        //public Employee Get(int id)
        //{
        //    var employee = list.FirstOrDefault(e => e.Id == id);

        //    if (employee == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }

        //    return employee;
        //}

        //public IEnumerable<Employee> GetByDepartment(int department)
        //{
        //    int[] validDepartment = { 1, 2, 3, 5, 8, 13 };

        //    if (!validDepartment.Any(d => d == department))
        //    {
        //        var response = new HttpResponseMessage()
        //        {
        //            StatusCode = (HttpStatusCode)422, //Unprocessable Entity
        //            ReasonPhrase = "Invalid Department"
        //        };

        //        throw new HttpResponseException(response);
        //    }

        //    return list.Where(e => e.Department == department);
        //}
        #endregion

        #region exercise 6
        //public IEnumerable<Employee> Get([FromUri]Filter filter)
        //{
        //    return list.Where(e => e.Department == filter.Department && e.LastName == filter.LastName);
        //}
        #endregion

        #region exercise 7
        //public HttpResponseMessage Post(Employee employee)
        //{
        //    int maxId = list.Max(e => e.Id);
        //    employee.Id = maxId + 1;

        //    list.Add(employee);

        //    var response = Request.CreateResponse<Employee>(HttpStatusCode.Created, employee);

        //    string uri = Url.Link("DefaultApi", new { id = employee.Id });
        //    response.Headers.Location = new Uri(uri);
        //    return response;
        //}

        //public HttpResponseMessage Put(int id, Employee employee)
        //{
        //    int index = list.ToList().FindIndex(e => e.Id == id);
        //    if (index >= 0)
        //    {
        //        list[index] = employee; // overwrite the existing resource
        //        return Request.CreateResponse(HttpStatusCode.NoContent);
        //    }
        //    else
        //    {
        //        list.Add(employee);

        //        var response = Request.CreateResponse<Employee>(HttpStatusCode.Created, employee);

        //        string uri = Url.Link("DefaultApi", new { id = employee.Id });
        //        response.Headers.Location = new Uri(uri);
        //        return response;
        //    }
        //}

        ////public HttpResponseMessage Put(int id, Employee employee)
        ////{
        ////    int index = list.ToList().FindIndex(e => e.Id == id);
        ////    if (index >= 0)
        ////    {
        ////        list[index] = employee;
        ////        return Request.CreateResponse(HttpStatusCode.NoContent);
        ////    }

        ////    return Request.CreateResponse(HttpStatusCode.NotFound);
        ////}

        public HttpResponseMessage Patch(int id, Delta<Employee> deltaEmployee)
        {
            var employee = list.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            deltaEmployee.Patch(employee);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        //public void Delete(int id)
        //{
        //    Employee employee = list.FirstOrDefault(e => e.Id == id);
        //    if (employee != null)
        //    {
        //        list.Remove(employee);
        //    }
        //}
        #endregion
    }
}
