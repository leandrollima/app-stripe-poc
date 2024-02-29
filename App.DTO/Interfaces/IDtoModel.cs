namespace App.DTO.Interfaces
{
	public interface IDtoModel
	{
		void AddError(string error);
		void AddErrors(IEnumerable<string> errors);
		List<string> Errors();
	}
}
