using ChruchBulletin.Core.Entity;
using ChruchBulletin.Core.Queries;
using ChruchBulletin.DataAccess.Mapping;

namespace ChruchBulletin.DataAccess.Handlers
{
    public class BulletinItemByDateHandler : IBulletinItemByDateHandler
    {
        private readonly DataContext _dbContext;

        public BulletinItemByDateHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<BulletinItem> Handle(BulletinItemByDateAndTimeQuery query)
        {
            IEnumerable<BulletinItem> items = _dbContext.Set<BulletinItem>().Where(item => item.Date == query.TargetDate).AsEnumerable();
            return items;
        }
    }
}