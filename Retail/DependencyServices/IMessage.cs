using System;
namespace Retail.DependencyServices
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
        void DismissAlert();
    }
}
