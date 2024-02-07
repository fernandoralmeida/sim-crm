
namespace Sim.Identity.Config;
public static class AccountType
{
    public const string Adm_Global = "Adm_Global";
    public const string Adm_Account = "Adm_Account";
    public const string Adm_Settings = "Adm_Settings";

    public static IEnumerable<string> ToList()
    {
        return new List<string>() { Adm_Global, Adm_Account, Adm_Settings };
    }
}