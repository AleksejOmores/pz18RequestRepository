using Microsoft.EntityFrameworkCore;
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
    public class AddEditRequestViewModel : BindableBase
    {
        private IRequestRepository _repository;
        public AddEditRequestViewModel(IRequestRepository repository)
        {
            _repository = repository;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            ValidationRequest = new ValidationRequest();
            LoadDeviceModels();
        }

        public bool _isEditMode;

        public bool isEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        private Request _editRequest;

        private ValidationRequest _validationRequest;
        public ValidationRequest ValidationRequest
        {
            get => _validationRequest;
            set => SetProperty(ref _validationRequest, value);
        }
        public ObservableCollection<DeviceModel> DeviceModels { get; set; } = new ObservableCollection<DeviceModel>();


        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public event Action Done;

        // Метод для загрузки моделей устройств
        private async void LoadDeviceModels()
        {
            var models = await _repository.GetDeviceModelsAsync(); 
            DeviceModels = new ObservableCollection<DeviceModel>(models);
        }

        private void OnCanExecuteChanges(object sender, EventArgs e)
        {
            SaveCommand.OnCanExecuteChanged();
        }
        private void CopyRequest(Request request, ValidationRequest validRequest)
        {
            validRequest.RequestID = request.RequestId;
            validRequest.DateAdded = request.DateAdded;
            validRequest.DeviceModelID = request.DeviceModelId;
            validRequest.ProblemDescription = request.ProblemDescription;
        }

        private void OnCancel()
        {
            Done?.Invoke();
        }


        private void UpdateRequest(ValidationRequest request, Request target)
        {
            target.RequestId = request.RequestID;
            target.DateAdded = request.DateAdded;
            target.DeviceModelId = request.DeviceModelID;
            target.ProblemDescription = request.ProblemDescription;
        }

        private bool CanSave() => !ValidationRequest.HasErrors;
        private async void OnSave()
        {
            UpdateRequest(ValidationRequest, _editRequest);
            if (isEditMode)
                await _repository.UpdateRequestAsync(_editRequest);
            else
                await _repository.AddRequestAsync(_editRequest);
            Done?.Invoke();
               
        }

        public void SetCustomer(Request request)
        {
            _editRequest = request;
            if (ValidationRequest != null)
                ValidationRequest.ErrorsChanged -= OnCanExecuteChanges;
            ValidationRequest = new ValidationRequest();
            ValidationRequest.ErrorsChanged += OnCanExecuteChanges;
            CopyRequest(request, ValidationRequest);
        }
    }
}
