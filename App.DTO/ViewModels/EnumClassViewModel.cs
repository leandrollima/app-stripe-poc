using App.DTO.SuperClasses;

namespace App.DTO.ViewModels
{
    public class EnumClassViewModel : DtoModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public EnumClassViewModel() { }
        public EnumClassViewModel(int id, string name) { Id = id; Name = name; }
        public EnumClassViewModel(string error) : base(error) { }
    }
}
