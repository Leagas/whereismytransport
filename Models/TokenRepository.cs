using Microsoft.EntityFrameworkCore;

namespace whereismytransport
{
    public class TokenContext: DbContext
    {
        public DbSet<Token> Tokens { get; set; }

        public TokenContext(DbContextOptions options): base(options) { }
    }
}
