using System.ComponentModel.DataAnnotations;

namespace TaskManagerRaph.Models;

public class Tache
{
    
    [Key] public int Id { get; set; }
    
    public string task { get; set; }
    
    [Required(ErrorMessage = "date de la tache requise")]
    public DateTime date { get; set; }
    
    public string categorie { get; set; }
}