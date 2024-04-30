namespace Tokobaju.Utils;

public class BcryptUtil
{
    public string HashPassword(string password)
    {
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
        return hashPassword;
    }

    public bool Validate(string password, string hashPassword)
    {
        var result  = BCrypt.Net.BCrypt.Verify(password, hashPassword);
        return result;
    }
}