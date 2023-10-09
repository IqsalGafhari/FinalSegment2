using API.DTOs.Universities;
using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Accounts;
using API.Utilities.Handlers;
using API.Utilities.Handlers.Exceptions;
using API.DTOs.AccountRoles;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;
        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universityRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var data = result.Select(u => (UniversityDto) u);
            /*var universityDto = new List<UniversityDto>();
               foreach (var university in result)
               {
                   universityDto.Add((UniversityDto) university);
               }*/
            return Ok(new ResponseOkHandler<IEnumerable<UniversityDto>>(data));
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _universityRepository.GetByGuid(guid);
            if(result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            }
            return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result));
        }
        [HttpPost]
        public IActionResult Create(CreateUniversityDto createUniversityDto)
        {
            try
            {
                University toCreate = createUniversityDto;
                toCreate.Guid = new Guid();
                var result = _universityRepository.Create(toCreate);
                return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result));

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }

        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            try
            {
                var entity = _universityRepository.GetByGuid(universityDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                University toUpdate = universityDto;
                toUpdate.CreatedDate = entity.CreatedDate;

                var result = _universityRepository.Update(toUpdate);
                return Ok(new ResponseOkHandler<String>("Data Updated"));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid) {
            try
            {
                var university = _universityRepository.GetByGuid(guid);
                if (university is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                var result = _universityRepository.Delete(university);

                return Ok(new ResponseOkHandler<String>("Data Deleted"));

            } catch (Exception e) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }
    }
}
