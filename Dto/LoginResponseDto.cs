namespace task_management_system.Dto
{
    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
