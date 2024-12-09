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
        public int RequestId { get => _requestId; set => SetProperty(ref _requestId, value); }

        [Required]
        private DateOnly _dateAdded;
        public DateOnly DateAdded { get => _dateAdded; set => SetProperty(ref _dateAdded, value); }

        [Required]
        private int _deviceModelId;
        public int DeviceModelId { get => _deviceModelId; set => SetProperty(ref _deviceModelId, value); }

        [Required]
        private string _problemDescription;
        public string ProblemDescription { get => _problemDescription; set => SetProperty(ref _problemDescription, value); }

    }
}
