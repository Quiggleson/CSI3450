using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Data;
public class TimecardContext : DbContext {
    public TimecardContext(DbContextOptions<TimecardContext> options) : base(options)
    {
    }
    public virtual DbSet<Timecard> Timecard { get; set; }

}