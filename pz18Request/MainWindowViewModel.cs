using Microsoft.EntityFrameworkCore.Query;
using pz18Request.Services;
using pz18Request.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz18Request
{
    internal class MainWindowViewModel : BindableBase
    {
        private RequestListViewModel _requestListVM;

        public MainWindowViewModel()
        {
            NavigationCommand = new RelayCommand<string>(OnNavigation);
            _requestListVM = new RequestListViewModel(new RequestRepository());
        }

        public BindableBase _currentViewModel;
        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }
        public RelayCommand<string> NavigationCommand { get; private set; }

        private void OnNavigation(string dest)
        {
            switch (dest)
            {
                case "requestList":
                    CurrentViewModel = _requestListVM; break;
            }
        }
    }
}
