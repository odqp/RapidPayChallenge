namespace RapidPay.API.Models.Dto.Authorization
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
