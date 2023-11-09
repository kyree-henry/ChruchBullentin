using ChruchBulletin.Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class BulletinItemMap : EntityMapBase<BulletinItem>
    {
        protected override void MapMembers(EntityTypeBuilder<BulletinItem> modelBuilder)
        {
            
        }
    }
}
