using App.DTO.Abstracts;

namespace App.DTO.Response
{
    public class CharacterViewModel : AnimOutFit
    {
        public uint AccountId { get; set; }  
        public int Id { get; set; }
    }
}
