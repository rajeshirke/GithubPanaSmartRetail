using System;
using System.Threading.Tasks;

namespace Retail.DependencyServices
{
    public interface IDeviceTimeService
    {
        Task<bool> IsDeviceTimeValid();

        // bool HasDeviceTimeChanged();
        //DateTime GetReferenceTime();
        //bool HasDeviceTimeChanged();

        //bool IsDeviceTimeAutomatic();

        //bool IsDeviceTimeAutomatic();
        //DateTime GetCurrentDeviceTime();
        //event EventHandler DeviceTimeChanged;

        //DateTime GetReferenceTime();
        //bool HasDeviceTimeChanged();
    }
}
