using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retail.DependencyServices
{
    public interface IGMMultiImagePicker
    {
        Task<List<string>> PickMultiImage();
        Task<List<string>> PickMultiImage(bool needsHighQuality);
        void ClearFileDirectory();
    }
}
