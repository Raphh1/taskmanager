using System.ComponentModel.DataAnnotations;

namespace TaskManagerRaph.Models;

public class Tache
{
    
    [Key] public int Id { get; set; }
    
    [Required(ErrorMessage = "Il faut donner un nom a votre tache")]
    public string task { get; set; }
    
    [Required(ErrorMessage = "date de la tache requise")]
    public DateTime date { get; set; }
    
    [Required(ErrorMessage = "veuillez entrer une categorie")]
    public string categorie { get; set; }
    
    public string description { get; set; }
    
    public int? FavorisId { get; set; }
    public Favoris Favoris { get; set; }
}