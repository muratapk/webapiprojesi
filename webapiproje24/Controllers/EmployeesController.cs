using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapiproje24.Repository;
namespace webapiproje24.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> GetEmployees()
        {
            try {

                return Ok(await _employeeRepository.GetEmployees());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Hata Oluştu veya veri gelmiyor");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int Id)
        {
            try
            {

                var result = await _employeeRepository.GetEmployee(Id);
                if (result == null)
                {
                    return NotFound();

                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Hata Oluştu veya veri gelmiyor");
            }

        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();

                }
                else
                {
                    var CreateEmployee = await _employeeRepository.AddEmployee(employee);
                    return CreatedAtAction(nameof(GetEmployee), employee);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Hata Oluştu veya veri gelmiyor");
            }
        }


        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee, int Id)
        {
            try
            {
                if (Id != employee.Id)
                {
                    return BadRequest("Id Hatası");

                }

                var employeeupdate = await _employeeRepository.GetEmployee(Id);

                if (employeeupdate == null)
                {

                    return NotFound($"Personel  Id={Id} bulunamadı");
                }
                return await _employeeRepository.UpdateEmployee(employee, Id);


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Hata Oluştu veya veri gelmiyor");
            }
        }


        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int Id)
        {
            try
            {

                var employeesearch = await _employeeRepository.GetEmployee(Id);


                if (employeesearch != null)
                {
                    var employeeDelete = await _employeeRepository.DeleteEmployee(Id);

                }
                else
                {
                    return NotFound($"Personel  Id={Id} bulunamadı");
                }



                return await _employeeRepository.DeleteEmployee(Id);


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Hata Oluştu veya veri gelmiyor");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search (string name)
         {

            try {

                var result =await _employeeRepository.Search(name);
                //if(result==null)
                //{
                //    return NotFound();
                //}
                if(result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Hata Oluştu veya veri gelmiyor");
            }
        }
    }
}
