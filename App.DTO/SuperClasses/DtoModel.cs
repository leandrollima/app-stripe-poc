using App.DTO.Interfaces;
using System.Text.Json.Serialization;

namespace App.DTO.SuperClasses
{
	public class DtoModel : IDtoModel
    {
        public List<string> Error { get { return _errors; } }
        public Dictionary<string, string> ModelErrors { get { return _modelErrors; } }

        protected List<string> _errors { get; }
        protected Dictionary<string, string> _modelErrors { get; }

        public DtoModel()
        {
            _errors = new List<string>();
            _modelErrors = new Dictionary<string, string>();
        }

        public DtoModel(string error)
        {
            _errors = new List<string>() { error };
            _modelErrors = new Dictionary<string, string>();
        }
        public DtoModel(List<string> errors)
        {
            _errors = errors;
            _modelErrors = new Dictionary<string, string>();
        }

        public void AddError(string error) => _errors.Add(error);
        public void AddErrors(IEnumerable<string> errors) => _errors.AddRange(errors);
       
        [JsonIgnore]
        public bool Success { get { return _errors.Any() == false && _modelErrors.Any() == false; } }

        public List<string> Errors()
        {
            return _errors;
        }

        public override string ToString()
        {
            return string.Join("\n", Errors());
        }
    }
}
