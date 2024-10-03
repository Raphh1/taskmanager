using System.ComponentModel.DataAnnotations;

namespace TaskManagerRaph.Models;

public class Favoris

{
    [Key] public int Id { get; set; }
    
    public string TaskId { get; set; }
    
    public ICollection<Tache> Tasks { get; set; }
    
    
}