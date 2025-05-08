using System;
using System.Threading.Tasks;

namespace Retail.DependencyServices
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
