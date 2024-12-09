﻿using pz18Request.Model;
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
            AddRequestCommand = new RelayCommand(OnAddRequest);
            EditRequestCommand = new RelayCommand<Request>(OnEditRequest);
            LoadRequest();
        }

        private bool _isEditMode;

        public bool isEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
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

        public RelayCommand AddRequestCommand { get; private set; }

        public RelayCommand<Request> EditRequestCommand { get; private set; }

        public event Action AddRequestRequested = delegate { };
        public event Action<Request> EditRequestRequested = delegate { };

        private void OnAddRequest()
        {
            AddRequestRequested?.Invoke();
        }
        private void OnEditRequest(Request request)
        {
            EditRequestRequested(request);
        }

    }
}
