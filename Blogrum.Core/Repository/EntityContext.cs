
namespace Blogrum.Core.Repository
{
    public class EntityContext
    {
        private static BlogrumDbContext _context = null;

        public static BlogrumDbContext GetContext()
        {
            return _context ?? (_context = new BlogrumDbContext());
        }
    }
}
