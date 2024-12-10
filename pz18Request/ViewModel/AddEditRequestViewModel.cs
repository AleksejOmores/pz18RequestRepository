using Microsoft.EntityFrameworkCore;
using pz18Request.Model;
using pz18Request.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pz18Request.ViewModel
{
    public class AddEditRequestViewModel : BindableBase
    {
        private readonly IRequestRepository _repository = new RequestRepository();
        public AddEditRequestViewModel()
        {
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            ValidationRequest = new ValidationRequest();
            LoadDeviceModelIds();
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
        private ObservableCollection<int> _deviceModelIds;
        public ObservableCollection<int> DeviceModelIds
        {
            get => _deviceModelIds;
            set => SetProperty(ref _deviceModelIds, value);
        }

        private int _selectedDeviceModelId;
        public int SelectedDeviceModelId
        {
            get => _selectedDeviceModelId;
            set
            {
                SetProperty(ref _selectedDeviceModelId, value);
                ValidationRequest.DeviceModelID = value;
            }
        }

        private async void LoadDeviceModelIds()
        {
            try
            {
                var requests = await _repository.GetRequestAsync();
                var ids = requests.Select(r => r.DeviceModelId).Distinct().ToList();
                DeviceModelIds = new ObservableCollection<int>(ids);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки идентификаторов моделей устройств: {ex.Message}");
            }
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public event Action Done;

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
            ShowMessage("Заявка успешно сохранена");
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

        private void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
