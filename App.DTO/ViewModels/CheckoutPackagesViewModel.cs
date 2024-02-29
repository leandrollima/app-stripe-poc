using App.DTO.Response;
using App.DTO.SuperClasses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.DTO.ViewModels
{
    public class CheckoutPackagesViewModel : DtoModel//, IValidatableObject
    {
        //[HiddenInput]
        //[Required]
        //[DisplayName("Character")]
        //public int PlayerId { get; set; }

        [HiddenInput]
        public string PriceId { get; set; } = default!;

        [HiddenInput]
        public string Currency { get; set; } = "brl";

        public string? Coupon { get; set; } 

        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
        public List<CharacterViewModel> Players { get; set; } = new List<CharacterViewModel>();


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (PlayerId < 1)
        //    {
        //        yield return new ValidationResult("Invalid PlayerId.", new[] { nameof(PlayerId) });
        //    }
        //}
    }
}
