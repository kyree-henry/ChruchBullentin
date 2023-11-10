using ChruchBulletin.Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChruchBulletin.DataAccess.Mapping
{
    public class BulletinItemMap : EntityMapBase<BulletinItem>
    {
        protected override void MapMembers(EntityTypeBuilder<BulletinItem> entity)
        {

        }
    }
}
