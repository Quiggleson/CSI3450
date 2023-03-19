using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Models;
public class Employee
{
   
    [Key]
    public int eId { get; set; }
    public string? eName { get; set; }
    public string? ePassword { get; set; }
    public string eEmail { get; set; }
    public string? eTitle { get; set; }
    public int? managerId { get; set; }
    
}