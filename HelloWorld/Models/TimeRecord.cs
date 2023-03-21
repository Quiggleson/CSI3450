using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Models;
public class TimeRecord
{
   
    [Key]
    public int timeId { get; set; }
    public DateTime timeIn { get; set; }
    public DateTime? timeOut { get; set; }
    public int eId { get; set; }

    
}