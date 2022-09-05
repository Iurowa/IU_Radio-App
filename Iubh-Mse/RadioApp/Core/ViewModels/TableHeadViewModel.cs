using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class TableHeadViewModel: MvxViewModel, IServiceTableViewModel
    {
        public string Title { get; set; }
    }
}
