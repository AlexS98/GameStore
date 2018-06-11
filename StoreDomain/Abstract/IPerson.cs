using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebUI")]
namespace StoreDomain.Abstract
{
    public enum UserRole
    {
        Outsider = 0,
        User = 1,
        Moderator = 2,
        Admin = 3
    }

    public interface IPerson
    {
        string Nickname { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        UserRole Role { get; set; }
        int GetPersonId();
    }
}
