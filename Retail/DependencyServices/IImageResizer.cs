using System;
namespace Retail.DependencyServices
{
    public interface IImageResizer
    {
        byte[] ResizeImage(byte[] imageData, double width, double height);
        
    }
}
