namespace DWX.PortableApps.SQLite
{
    public class BaseSqLiteRepository
    {
        protected SQliteContext _context;

        public BaseSqLiteRepository(SQliteContext context)
        {
            _context = context;
        }
    }
}