using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloWorld.Models;
public class tester
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int numba { get; set; }
    public string? text { get; set; }
}