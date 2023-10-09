using API.DTOs.Accounts;
using API.Utilities.Handlers;
using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Utilities.Handlers.Exceptions;
using API.DTOs.AccountRoles;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IEmailHandler _emailHandler;

        public AccountController(IAccountRepository accountRepository IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository, IEmailHandler emailHandler)
        {//melakukan injeksi dependen
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _emailHandler = emailHandler;
        }

        [HttpPost("Forgot_Password")]//method post utk lupa password
        public IActionResult ForgotPassword(ForgotPasswordDto forgotpasswordDto)
        {
            var employee = _employeeRepository.GetByEmail(forgotPasswordDto.Email);
            if (employee is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("Account Not Found"));
            }
            account.OTP = new Random().Next(100000, 1000000);
            account.ExpiredTime = DateTime.Now.AddMinutes(5);
            account.IsUsed = false;
            _accountRepository.Update(account);
            _emailHandler.Send("Forgot Password", $"Your OTP is {account.OTP}", forgotPasswordDto.Email);
            return Ok(new ResponseOkHandler<ForgotPasswordResponseDto>((ForgotPasswordResponseDto)account));
        }

        [HttpPost("Change_Password")]//method post utk ubah password
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var employee = _employeeRepository.GetByEmail(changePasswordDto.Email);
            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("Akun tidak ditemukan"));
            }
            if (account.OTP != changePasswordDto.Otp)
            {
                return BadRequest(new ResponseBadRequestHandler("kode otp salah"));
            }
            if (DateTime.Now > account.ExpiredTime)
            {
                return BadRequest(new ResponseBadRequestHandler("kode OTP sudah kedaluarsa"));
            }
            if (account.IsUsed == true)
            {
                return BadRequest(new ResponseBadRequestHandler("kode otp sudah digunakan"));
            }
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                return BadRequest(new ResponseBadRequestHandler("password tidak sesuai"));
            }
            account.Password = HashHandler.HashPassword(changePasswordDto.NewPassword);
            account.IsUsed = true;
            _accountRepository.Update(account);
            return Ok(new ResponseOkHandler<AccountDto>((AccountDto)account));
        }

        [HttpPost("Login")]//method post utk login
        public IActionResult Login(LoginDto loginDto)
        {
            var employee = _employeeRepository.GetByEmail(loginDto.Email);
            if (employee is null)
            {
                return BadRequest(new ResponseBadRequestHandler("email atau password salah"));
            }
            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account is null)
            {
                return NotFound(new ResponseNotFoundHandler("akun tidak ditemukan"));
            }
            var isValidPassword = HashHandler.verifvyPassword(loginDto.Password, account.Password);
            if (!isValidPassword)
            {
                return BadRequest(new ResponseBadRequestHandler("email atau password salah"));
            }
            return Ok(new ResponseOkHandler<string>("Login berhasil"));
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var employee = _employeeRepository.GetByEmail(registerDto.Email);
            if (employee is null)
            {
                employee = registerDto;
                employee.NIK = GenerateNIKHandler.GenerateNIK(_employeeRepository.GetLastNik());
                employee = _employeeRepository.Create(employee);
            }
            else
            {
                return BadRequest(new ResponseBadRequestHandler("Email sudah digunakan"));
            }

            var university = _universityRepository.GetUniversityNameByCode(registerDto.UniversityCode);
            if (university is null)
            {
                university = _universityRepository.Create(registerDto);
            }

            var education = _educationRepository.GetByGuid(employee.Guid);
            if (education is null)
            {
                Education educationcreate = registerDto;
                educationcreate.Guid = employee.Guid;
                educationcreate.UniversityGuid = university.Guid;
                _educationRepository.Create(educationcreate);
            }
            Account account = registerDto;
            account.Guid = employee.Guid;
            account.Password = HashHandler.HashPassword(registerDto.Password);

            EmployeeDetailsDto responseRegister = (EmployeeDetailsDto)registerDto;
            responseRegister.Guid = employee.Guid;
            responseRegister.Nik = employee.NIK;
            responseRegister.University = university.Name;
            return Ok(new ResponseOkHandler<EmployeeDetailsDto>(responseRegister));
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var data = result.Select(i => (AccountDto) i);
            return Ok(new ResponseOkHandler<IEnumerable<AccountDto>>(data));
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            }
            return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result));
        }
        [HttpPost]
        public IActionResult Create(CreateAccountDto createAccountDto)
        {
            try
            {
                Account toCreate = createAccountDto;
                toCreate.Password = HashHandler.HashPassword(createAccountDto.Password);
            
                var result = _accountRepository.Create(toCreate);
                return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result));

            }
            
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }

        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {
                var entity = _accountRepository.GetByGuid(accountDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                Account toUpdate = accountDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                var result = _accountRepository.Update(toUpdate);
                return Ok(new ResponseOkHandler<String>("Data Updated"));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var account = _accountRepository.GetByGuid(guid);
                if (account is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                var result = _accountRepository.Delete(account);

                return Ok(new ResponseOkHandler<String>("Data Deleted"));
            } catch ( Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }
    }
}
