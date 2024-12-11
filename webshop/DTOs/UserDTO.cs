namespace webshopAPI.DTOs
{
    public class UserDTO
    {
        public int IDUser { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
    }
}
