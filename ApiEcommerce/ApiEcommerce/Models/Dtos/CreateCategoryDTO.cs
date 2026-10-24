using System;
using System.ComponentModel.DataAnnotations;
namespace ApiEcommerce.Models.Dtos;

public class CreateCategoryDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El nombre no puede tener mas de 50 caracteres.")]
    [MinLength(3, ErrorMessage = "El nombre tiene que tener un minimo de 3 caracteres.")]
    public string Name { get; set; } = string.Empty;
}
