using ChruchBulletin.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace ChruchBulletin.DataAccess.Test
{
    public class BulletinItemMappingTester
    {
        [Test]
        public void ShouldMapBulletin()
        {
            BulletinItem? bulletin = new();
            bulletin.Name = "Worship service";
            bulletin.Place = "Sanctuary";
            bulletin.Date = new DateTime(2024, 1, 1);

            using (DbContext context = TestHost.GetRequiredService<DbContext>())
            {
                context.Add(bulletin);
                context.SaveChanges();
            }

            BulletinItem? rehydratedEntity;
            using (DbContext context = TestHost.GetRequiredService<DbContext>())
            {
                rehydratedEntity = context.Set<BulletinItem>().Single(b => b == bulletin);
            }

            rehydratedEntity.ShouldBe(bulletin);
            rehydratedEntity?.Id.ShouldBe(bulletin.Id);
            rehydratedEntity?.Name.ShouldBe(bulletin.Name);
            rehydratedEntity?.Date.ShouldBe(bulletin.Date);
            rehydratedEntity?.Place.ShouldBe(bulletin.Place);
        }

    }

}