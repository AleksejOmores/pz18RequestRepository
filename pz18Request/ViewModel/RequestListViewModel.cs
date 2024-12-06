using pz18Request.Model;
using pz18Request.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz18Request.ViewModel
{
    class RequestListViewModel : BindableBase
    {
        private IRequestRepository _repository;

        public RequestListViewModel(IRequestRepository repository)
        {
            _repository = repository;
            Requests = new ObservableCollection<Request>();
            LoadRequest();
        }

        private ObservableCollection<Request>? _request;
        public ObservableCollection<Request>? Requests
        {
            get => _request;
            set => SetProperty(ref _request, value);
        }

        private List<Request>? _requestList;

        public async void LoadRequest()
        {
            _requestList = await _repository.GetRequestAsync();
            Requests = new ObservableCollection<Request>(_requestList);
        }


    }
}
