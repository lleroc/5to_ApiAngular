using Microsoft.EntityFrameworkCore;

namespace ApiAngular
{
    public class AngularDbContext : DbContext
    {
        public AngularDbContext(DbContextOptions op): base(op)
        {
                
        }

    }
}
