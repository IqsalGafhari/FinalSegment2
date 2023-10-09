using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.Utilities.Handlers;
using API.DTOs.Accounts;
using API.DTOs.Rooms;
using API.Utilities.Handlers.Exceptions;
using API.DTOs.AccountRoles;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;

    public EmployeeController(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
    {
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }

    [HttpGet("details")]
    public IActionResult GetDetails()
    {
        var employees = _employeeRepository.GetAll();
        var educations = _educationRepository.GetAll();
        var universities = _universityRepository.GetAll();

        if (!(employees.Any() && educations.Any() && universities.Any()))
        {
            return NotFound(new ResponseErrorHandler {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        var employeeDetails = from emp in employees
                              join edu in educations on emp.Guid equals edu.Guid
                              join unv in universities on edu.UniversityGuid equals unv.Guid
                              select new EmployeeDetailDto {
                                  Guid = emp.Guid,
                                  Nik = emp.NIK,
                                  FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                                  BirthDate = emp.BirthDate,
                                  Gender = emp.Gender.ToString(),
                                  HiringDate = emp.HiringDate,
                                  Email = emp.Email,
                                  PhoneNumber = emp.PhoneNumber,
                                  Major = edu.Major,
                                  Degree = edu.Degree,
                                  Gpa = edu.GPA,
                                  University = unv.Name
                              };
        
        return Ok(new ResponseOkHandler<IEnumerable<EmployeeDetailDto>>(employeeDetails));
    }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var data = result.Select(i => (EmployeeDto)i);
            return Ok(new ResponseOkHandler<IEnumerable<EmployeeDto>>(data));
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            }
            return Ok(new ResponseOkHandler<EmployeeDto>((EmployeeDto)result));
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                Employee toCreate = createEmployeeDto;
                toCreate.NIK = GenerateNIKHandler.GenerateNIK(_employeeRepository);
                var result = _employeeRepository.Create(toCreate);
                return Ok(new ResponseOkHandler<EmployeeDto>((EmployeeDto)result));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }

        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            try
            {
                var entity = _employeeRepository.GetByGuid(employeeDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                Employee toUpdate = employeeDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                var result = _employeeRepository.Update(employeeDto);
                return Ok(new ResponseOkHandler<String>("Data Updated"));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        { try { 
                var employee = _employeeRepository.GetByGuid(guid);
                if (employee is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                var result = _employeeRepository.Delete(employee);
                return Ok(new ResponseOkHandler<String>("Data Deleted"));
        }catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
    }
}
