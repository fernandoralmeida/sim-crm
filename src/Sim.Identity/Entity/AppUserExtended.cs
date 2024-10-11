using Microsoft.AspNetCore.Identity;

namespace Sim.Identity.Entity;

public class AppUserExtended{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public string? Name { get; set; }
    public bool LockoutEnabled { get; set; }
    public IEnumerable<string>? Roles { get; set; }
    public IEnumerable<KeyValuePair<string, string>>? Claims { get; set; }
}