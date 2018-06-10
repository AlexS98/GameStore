namespace StoreDomain.Abstract
{
    enum UserRole
    {
        User = 0,
        Moderator = 1,
        Admin = 2
    }

    interface IPerson
    {
        string Nickname { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        UserRole Role { get; set; }
        int GetPersonId();
    }
}
