using System;
using System.Threading.Tasks;
using Retail.ViewModels.MonthYearPickerViewModel;

namespace Retail.DependencyServices
{
    public interface INumberPickerDialog
    {
        Task<(bool, int)> ShowPicker(string title, string okButtonText, string cancelButtonText, NumberPickerOptions options);
    }
}
