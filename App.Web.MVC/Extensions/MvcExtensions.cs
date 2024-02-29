using App.DTO.SuperClasses;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.Web.MVC.Extensions
{
    public static class MvcExtensions
    {
        public static void AddModelErrors(this ModelStateDictionary modelState, DtoModel responseModel)
        {
            if (responseModel.ModelErrors.Any())
            {
                foreach (var err in responseModel.ModelErrors)
                {
                    modelState.AddModelError(err.Key, err.Value);
                }
            }

            if (responseModel.Error.Any())
            {
                foreach (var err in responseModel.Errors())
                {
                    modelState.AddModelError(string.Empty, err);
                }
            }
        }
    }
}
