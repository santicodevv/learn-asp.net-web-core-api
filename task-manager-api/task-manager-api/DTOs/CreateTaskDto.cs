using System.ComponentModel.DataAnnotations;

public class CreateTaskDto : IValidatableObject
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = "";

    [MaxLength(400)]
    public string? Description { get; set; }

    [Range(1, 3)]
    public int Priority { get; set; }

    public DateTime DueDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (DueDate < DateTime.Now)
            yield return new ValidationResult("La hora de vencimiento no puede ser atrasada", [nameof(DueDate)]);
        
        if (Title == Description)
            yield return new ValidationResult("El titulo no puede ser igual a la descripcion", [nameof(Title), nameof(Description)]);
        
    } 
}