using ChruchBulletin.Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class BulletinMap : EntityMapBase<Bulletin>
    {
        protected override void MapMembers(EntityTypeBuilder<Bulletin> modelBuilder)
        {
            
        }
    }
}
