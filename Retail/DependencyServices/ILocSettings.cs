using System;
namespace Retail.DependencyServices
{
    public interface ILocSettings
    {
        void OpenSettings();
        bool isGpsAvailable();
    }
}
