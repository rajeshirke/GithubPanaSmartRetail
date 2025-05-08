using System;
namespace Retail.DependencyServices
{
    public interface IEWProgress
    {
        void Show();
        void Show(string message);
        void Dismiss();        
    }
}
