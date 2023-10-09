using API.DTOs.Educations;
using API.DTOs.Universities;
using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.Utilities.Handlers;
using API.Utilities.Handlers.Exceptions;
using API.DTOs.AccountRoles;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _educationRepository.GetAll();
            if (!result.Any())
            {
                return  NotFound(new ResponseNotFoundHandler("Data NotFound"));
            }
            var data = result.Select(i => (EducationDto)i);

            return Ok(new ResponseOkHandler<IEnumerable<EducationDto>>(data));
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _educationRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data NotFound"));

            }
            return Ok(new ResponseOkHandler<EducationDto>((EducationDto) result));
        }
        [HttpPost]
        public IActionResult Create(CreateEducationDto createEducationDto)
        {
            try
            {
                Education toCreate = createEducationDto;
                var result = _educationRepository.Create(toCreate);

                return Ok(new ResponseOkHandler<EducationDto>((EducationDto)result));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
                
        
        }

        [HttpPut]
        public IActionResult Update(EducationDto educationDto)
        {
            try
            {
                var entity = _educationRepository.GetByGuid(educationDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                Education toUpdate = educationDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                var result = _educationRepository.Update(educationDto);

                return Ok(new ResponseOkHandler<String>("Data Updated"));

            }  catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            { 
                var education = _educationRepository.GetByGuid(guid);
                if (education is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                var result = _educationRepository.Delete(education);
                return Ok(new ResponseOkHandler<String>("Data Deleted"));

            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }
    }
}
