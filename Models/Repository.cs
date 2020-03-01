using Microsoft.EntityFrameworkCore;

namespace whereismytransport.Models
{
    public class WIMTDataContext: DbContext
    {
		// Im sure this is wrong, there shouldn't be more than one DbSet type here
        public DbSet<Token> Tokens { get; set; }
		public DbSet<Line> Lines { get; set; }

        public WIMTDataContext(DbContextOptions options): base(options) { }
    }
}
