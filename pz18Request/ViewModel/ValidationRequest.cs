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

    }
}
