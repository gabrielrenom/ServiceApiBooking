using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Exceptions
{
    [Serializable]
    public class ValidationErrorsException : Exception
    {
        public IEnumerable<ValidationResult> ValidationErrors { get; set; }

        public ValidationErrorsException()
            : base() { }

        public ValidationErrorsException(string message)
            : base(message) { }

        public ValidationErrorsException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ValidationErrorsException(string message, Exception innerException)
            : base(message, innerException) { }

        public ValidationErrorsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        public ValidationErrorsException(IEnumerable<ValidationResult> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        protected ValidationErrorsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
