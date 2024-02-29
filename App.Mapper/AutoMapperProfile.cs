using App.DTO.Response;
using App.DTO.ViewModels;
using App.Repository.Entity;

namespace App.Mapper
{
	public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Player, CharacterViewModel>().ReverseMap();   
            CreateMap<Payment, PaymentDto>().ReverseMap();        
        }
    }
}
