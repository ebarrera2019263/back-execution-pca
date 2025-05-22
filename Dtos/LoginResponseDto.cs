namespace ExecutionPca.Api.Dtos
{
    public class LoginResponseDto
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
