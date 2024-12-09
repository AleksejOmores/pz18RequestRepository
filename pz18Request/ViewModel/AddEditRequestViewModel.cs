using pz18Request.Model;
using pz18Request.Services;
using System;
using System.Collections.Generic;
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
            SaveCommand = new RelayCommand(OnSave);
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

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public event Action Done;

        private void CopyRequest(Request request, ValidationRequest validRequest)
        {
            validRequest.RequestId = request.RequestId;
            if (isEditMode)
            {
                validRequest.RequestId = request.RequestId;
                validRequest.DateAdded = request.DateAdded;
                validRequest.DeviceModelId = request.DeviceModelId;
                validRequest.ProblemDescription = request.ProblemDescription;
            }
        }

        private void OnCancel()
        {
            Done?.Invoke();
        }

        private void UpdateRequest(ValidationRequest request, Request target)
        {
            target.RequestId = request.RequestId;
            target.DateAdded = request.DateAdded;
            target.DeviceModelId = request.DeviceModelId;
            target.ProblemDescription = request.ProblemDescription;
        }

        private async void OnSave()
        {
            UpdateRequest(ValidationRequest, _editRequest);
            if (isEditMode)
                await _repository.UpdateRequestAsync(_editRequest);
            else
                await _repository.AddRequestAsync(_editRequest);
            Done?.Invoke();
               
        }
    }
}
