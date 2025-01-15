using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Services
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
}
