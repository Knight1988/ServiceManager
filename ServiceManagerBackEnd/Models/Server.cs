using ServiceManagerBackEnd.Interfaces.Models;

namespace ServiceManagerBackEnd.Models;

public class Server : IBaseModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public DateTime LastPing { get; set; }
    public bool IsDeleted { get; set; }
    public User User { get; set; }
}