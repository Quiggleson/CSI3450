using Microsoft.EntityFrameworkCore;
using HelloWorld.Models;

namespace HelloWorld.Data;
public class TimecardContext : DbContext
{
    public TimecardContext(DbContextOptions<TimecardContext> options) : base(options)
    {
    }
    public DbSet<TimeRecord> TimeRecord { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<tester> tester { get; set; }

}