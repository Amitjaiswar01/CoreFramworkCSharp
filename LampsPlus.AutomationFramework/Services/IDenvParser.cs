using LampsPlus.AutomationFramework.Utilities.Environment;

namespace LampsPlus.AutomationFramework.Services
{
    public interface IDenvParser
    {
        EnvironmentInformation Parse(string devPageUrl);
    }
}
