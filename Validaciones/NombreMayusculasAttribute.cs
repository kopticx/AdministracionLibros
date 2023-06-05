using System.ComponentModel.DataAnnotations;

namespace AdministracionLibros.Validaciones
{
    public class NombreMayusculasAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var mayusculas = value.ToString().Split(' ').GroupBy(x => x).Select(x => x.Key[0]).ToList();

            foreach (var letra in mayusculas)
            {
                if(letra.ToString() != letra.ToString().ToUpper())
                {
                    return new ValidationResult("La primera letra de cada nombre debe ser mayuscula");
                }
            }

            return ValidationResult.Success;
        }
    }
}
