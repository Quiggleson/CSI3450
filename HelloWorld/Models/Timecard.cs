using System.ComponentModel.DataAnnotations;

public class Timecard
{
   
    // Implement this once the schema is finalized
    [Key]
    public string timeId { get; set; }
    public int empId { get; set; }
    public DateTime timeIn { get; set; }
    public DateTime timeOut { get; set; }
    
}