using EFMarket.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoMaket.Models
{
    public class SkipTakeModel
    {
        [SkipCheck]
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class SkipCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IUnitOfWork)validationContext
                                 .GetService(typeof(IUnitOfWork));

            var model = validationContext.ObjectInstance as SkipTakeModel;
            var test = service.UserRepository.GetUserById(10);

            if (model == null)
                throw new ArgumentException("Attribute not applied on Employee");

            if (model.Take < 2)
                return new ValidationResult(GetErrorMessage(validationContext));

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            // Message that was supplied
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;

            // Use generic message: i.e. The field {0} is invalid
            //return this.FormatErrorMessage(validationContext.DisplayName);

            // Custom message
            return $"{validationContext.DisplayName} can't be smaller than 10";
        }
    }
}
