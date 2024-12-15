using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using pz18Request.Model;
using pz18Request.Services;

namespace pz18Request.ViewModel
{
    public class AddEditRequestViewModel : BindableBase
    {
        private readonly IRequestRepository _repository;

        public AddEditRequestViewModel(IRequestRepository repository)
        {
            _repository = repository;

            SaveCommand = new RelayCommand(OnSave, CanSave);
            CancelCommand = new RelayCommand(OnCancel);
            LoadRequestIds();

            ValidationRequest = new ValidationRequest();
        }

        private ValidationRequest _validationRequest;
        public ValidationRequest ValidationRequest
        {
            get => _validationRequest;
            set => SetProperty(ref _validationRequest, value);
        }

        private ObservableCollection<int> _requestIds;
        public ObservableCollection<int> RequestIds
        {
            get => _requestIds;
            set => SetProperty(ref _requestIds, value);
        }

        private int _selectedRequestId;
        public int SelectedRequestId
        {
            get => _selectedRequestId;
            set
            {
                SetProperty(ref _selectedRequestId, value);
                ValidationRequest.RequestID = value;
            }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public event Action Done;

        private async void LoadRequestIds()
        {
            try
            {
                var requests = await _repository.GetRequestAsync();
                RequestIds = new ObservableCollection<int>(requests.Select(r => r.RequestId).Distinct());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ID заявок: {ex.Message}");
            }
        }

        private bool CanSave() => !ValidationRequest.HasErrors;

        private Request _editRequest;

        private async void OnSave()
        {
            try
            {
                if (IsEditMode)
                {
                    var existingRequest = await _repository.GetRequstByIdAsync(ValidationRequest.RequestID);

                    if (existingRequest != null)
                    {
                        existingRequest.DateAdded = ValidationRequest.DateAdded;
                        existingRequest.ProblemDescription = ValidationRequest.ProblemDescription;
                        existingRequest.DeviceModelId = ValidationRequest.DeviceModelID;
                        existingRequest.StatusId = ValidationRequest.StatusId;
                        existingRequest.ClientId = ValidationRequest.ClientId;
                        existingRequest.TechnicianId = ValidationRequest.TechnicianId;

                        await _repository.UpdateRequestAsync(existingRequest);
                    }
                    else
                    {
                        ShowMessage("Ошибка: Заявка не найдена.");
                        return;
                    }
                }
                else
                {
                    await _repository.AddRequestAsync(new Request
                    {
                        DateAdded = ValidationRequest.DateAdded,
                        ProblemDescription = ValidationRequest.ProblemDescription,
                        DeviceModelId = ValidationRequest.DeviceModelID,
                        StatusId = ValidationRequest.StatusId,
                        ClientId = ValidationRequest.ClientId,
                        TechnicianId = ValidationRequest.TechnicianId,
                    });
                }

                ShowMessage("Данные успешно сохранены.");
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                ShowMessage($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private void OnCancel()
        {
            Done?.Invoke();
        }

        public void SetRequest(Request request)
        {
            ValidationRequest.RequestID = request.RequestId;
            ValidationRequest.DateAdded = request.DateAdded;
            ValidationRequest.ProblemDescription = request.ProblemDescription;
            ValidationRequest.DeviceModelID = request.DeviceModelId;
            ValidationRequest.StatusId = request.StatusId;
            ValidationRequest.ClientId = request.ClientId;
            ValidationRequest.TechnicianId = request.TechnicianId;
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnCanExecuteChanges(object sender, EventArgs e)
        {
            SaveCommand.OnCanExecuteChanged();
        }
    }
}