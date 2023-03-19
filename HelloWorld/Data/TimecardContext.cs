using Microsoft.EntityFrameworkCore;
using HelloWorld.Models;

namespace HelloWorld.Data;
public class TimecardContext : DbContext {
    public TimecardContext(DbContextOptions<TimecardContext> options) : base(options)
    {
    }
    public virtual DbSet<TimeRecord> TimeRecord { get; set; }
    public virtual DbSet<Employee> Employee { get; set; }

}