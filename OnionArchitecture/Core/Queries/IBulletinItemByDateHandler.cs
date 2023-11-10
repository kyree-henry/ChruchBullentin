using ChruchBulletin.Core.Entity;

namespace ChruchBulletin.Core.Queries
{
    public interface IBulletinItemByDateHandler
    {
        IEnumerable<BulletinItem> Handle(BulletinItemByDateAndTimeQuery query);
    }
}
