using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz18Request.ViewModel
{
    public class ValidationRequest : ValidableBindableBase
    {
        private int _requestId;
        public int RequestID { get => _requestId; set => SetProperty(ref _requestId, value); }

        private DateOnly _dateAdded;
        [Required]
        public DateOnly DateAdded { get => _dateAdded; set => SetProperty(ref _dateAdded, value); }

        private int _deviceModelId;
        [Required]
        public int DeviceModelID { get => _deviceModelId; set => SetProperty(ref _deviceModelId, value); }

        private string _problemDescription;
        [Required]
        public string ProblemDescription { get => _problemDescription; set => SetProperty(ref _problemDescription, value); }

        private int _statusId;
        [Required]
        public int StatusId { get => _statusId; set => SetProperty(ref _statusId, value); }

        private int _clientId;
        [Required]
        public int ClientId { get => _clientId; set => SetProperty(ref _clientId, value); }

        private int _technicianId;
        [Required]
        public int TechnicianId { get => _technicianId; set => SetProperty(ref _technicianId, value); }

    }
}
