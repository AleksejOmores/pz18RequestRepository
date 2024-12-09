using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pz18Request
{
    public class ValidableBindableBase : BindableBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors 
            = new Dictionary<string, List<string>>();

        #region INotifyDataError
        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            else
                return null;
        }
        #endregion

        protected override void SetProperty<T>(ref T member, T value, [CallerMemberName] string property = "")
        {
            base.SetProperty(ref member, value, property);
            //validation
            ValidateProperty(property, value);
        }

        private void ValidateProperty<T>(string property, T value)
        {
            var results = new List<ValidationResult>();

            ValidationContext context = new ValidationContext(this);
            context.MemberName = property;
            Validator.TryValidateProperty(value, context, results);

            if (results.Any())
            {
                _errors[property] = results.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                _errors.Remove(property);
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs (property));
        }
    }
}
