using API.Models;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid Guid {  get; set; }
        public int Otp {  get; set; }
        public bool IsUsed { get; set; }
        public string Password { get; set; }
        public DateTime ExpiredTime {  get; set; }
        public static explicit operator AccountDto(Account account)
        {
            return new AccountDto
            {
                Guid = account.Guid,
                Otp = account.OTP,
                IsUsed = account.IsUsed,
                Password = account.Password,
                ExpiredTime = account.ExpiredTime
            };
        }
        
        //membuat implicit operator untuk update
        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account{ 
                Guid = accountDto.Guid,
                OTP = accountDto.Otp,
                IsUsed = accountDto.IsUsed,
                Password = accountDto.Password,
                ExpiredTime = accountDto.ExpiredTime,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
