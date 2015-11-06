namespace DWX.PortableApp.SQLite
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