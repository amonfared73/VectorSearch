using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Services
{
    public class ModalNavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Func<TViewModel> _createViewModel;
        public ModalNavigationService(ModalNavigationStore modalNavigationStore, Func<TViewModel> createViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _modalNavigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
