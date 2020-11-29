using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Core
{
    /// <summary>
    /// Api error object
    /// </summary>
    [DataContract]
    public class ApiError
    {
        public ApiError() : this(null)
        {
        }
        public ApiError(string message, IList<ApiError> errors = null)
        {
            Errors = errors;
            Message = message;
        }
        
        /// <summary>
        /// Localized message
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Message { get; set; }

        /// <summary>
        /// List of errors if multiple error codes are applicable
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public IList<ApiError> Errors { get; private set; }
    }
}