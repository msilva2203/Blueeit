namespace Blueeit.Api.Models;

/*
 *
 */
public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public DateTime Date { get; set; }
}

public class UserCreationDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }

}
