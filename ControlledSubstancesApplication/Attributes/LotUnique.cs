using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlledSubstancesApplication
{
    [AttributeUsage(AttributeTargets.Class,
      AllowMultiple = true, Inherited = true)]
    public class LotUniqueAttribute : ValidationAttribute, IClientValidatable
    {
        private Lot _dependentprop;
        //private double _dependentpropvalue;

        public LotUniqueAttribute()
            : base("Lot Already Exists")
        {
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return null;

            _dependentprop = (Lot)value;
            Lot lot = new Lot();
            string ErrorMessage = FormatErrorMessage("Lot Already Exists");

            using (Db db = new Db())
            {
                lot =
                    (from a in db.Lots
                     where a.lot_number == _dependentprop.lot_number &&
                           a.site_number == _dependentprop.site_number &&
                           a.entry_code == _dependentprop.entry_code
                     select a).FirstOrDefault();

                if (lot != null)
                    return new ValidationResult(ErrorMessage);
                else
                    return ValidationResult.Success;
            }
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("dependentproperty", _dependentprop);
            rule.ValidationType = "lotunique";
            yield return rule;
        }
    }
}