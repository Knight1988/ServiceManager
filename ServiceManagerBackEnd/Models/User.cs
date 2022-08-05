using ServiceManagerBackEnd.Interfaces.Models;

namespace ServiceManagerBackEnd.Models;

public class User : IBaseModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}