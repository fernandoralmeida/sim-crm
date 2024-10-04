namespace Sim.Identity.Policies;

public static class PolicyTypes
{
    public const string Permission = "Permission";
    public const string Adm_Global = "Adm_Global";
    public const string Adm_Account = "Adm_Account";
    public const string Adm_Settings = "Adm_Settings";

    public static IEnumerable<string> ToList() => new List<string>() { Adm_Account, Adm_Settings };
}