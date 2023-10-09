using API.DTOs.Employees;

namespace API.Controllers
{
    internal class ResponseOKHandler<T>
    {
        private IEnumerable<EmployeeDetailDto> employeeDetails;

        public ResponseOKHandler(IEnumerable<EmployeeDetailDto> employeeDetails)
        {
            this.employeeDetails = employeeDetails;
        }
    }
}