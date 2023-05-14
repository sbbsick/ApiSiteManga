namespace TesteApi.DTOs;

public class UserDTO
{
    public UserDTO()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}