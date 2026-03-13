namespace RealEstate.WebUILayer.DTOs.EmployeeDTOs
{
    public class UpdateEmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeNameSurname { get; set; }
        public string EmployeeTitle { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public bool EmployeeStatus { get; set; }
        public string EmployeeImageUrl { get; set; }
    }
}
