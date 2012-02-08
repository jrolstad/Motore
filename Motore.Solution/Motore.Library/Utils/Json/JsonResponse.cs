using System.Runtime.Serialization;
using Motore.Library.Utils.Entities;

namespace Motore.Library.Utils.Json
{
    [DataContract(Name="response")]
    public class JsonResponse
    {
        private bool _success = false;
        private string _message = "";

        public JsonResponse(GenericResponse genericResponse)
        {
            _success = genericResponse.Success;
            _message = genericResponse.Message;
        }

        public JsonResponse(bool success, string message)
        {
            _success = success;
            _message = message;
        }

        [DataMember(EmitDefaultValue=false)]
        public virtual string exception
        {
            get { return _success ? null : _message; }
            set
            {
                _success = false;
                _message = value;
            }
        }

        [DataMember(EmitDefaultValue=false)]
        public virtual string message
        {
            get { return _success ? _message : null; }
            set
            {
                _success = true;
                _message = value;
            }
        }

        [DataMember]
        public virtual bool success
        {
            get { return _success; }
            set { _success = value; }
        }

    }
}
