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

        private async void OnSave()
        {
            try
            {
                var request = new Request
                {
                    RequestId = ValidationRequest.RequestID,
                    DateAdded = ValidationRequest.DateAdded,
                    DeviceModelId = ValidationRequest.DeviceModelID,
                    ProblemDescription = ValidationRequest.ProblemDescription
                };

                if (IsEditMode)
                {
                    await _repository.UpdateRequestAsync(request);
                    MessageBox.Show("Заявка успешно обновлена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    await _repository.AddRequestAsync(request);
                    MessageBox.Show("Заявка успешно добавлена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                Done?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnCancel()
        {
            Done?.Invoke();
        }

        public void SetRequest(Request request)
        {
            if (ValidationRequest != null)
            {
                ValidationRequest.ErrorsChanged -= OnCanExecuteChanges;
            }

            ValidationRequest = new ValidationRequest
            {
                RequestID = request.RequestId,
                DateAdded = request.DateAdded,
                DeviceModelID = request.DeviceModelId,
                ProblemDescription = request.ProblemDescription
            };

            ValidationRequest.ErrorsChanged += OnCanExecuteChanges;
            IsEditMode = true;
        }

        private void OnCanExecuteChanges(object sender, EventArgs e)
        {
            SaveCommand.OnCanExecuteChanged();
        }
    }
}