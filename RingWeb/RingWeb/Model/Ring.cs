using System.ComponentModel.DataAnnotations;

public class Ring
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do anel é obrigatório.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O poder do anel é obrigatório.")]
    public string Power { get; set; }

    [Required(ErrorMessage = "O nome do portador é obrigatório.")]
    public string OwnerName { get; set; }

    [Range(1, 4, ErrorMessage = "Selecione uma raça válida.")]
    public int OwnerRace { get; set; }

    [Required(ErrorMessage = "O forjador é obrigatório.")]
    public string ForgedBy { get; set; }

    [Required(ErrorMessage = "A imagem é obrigatória.")]
    public string ImageUrl { get; set; }
}
