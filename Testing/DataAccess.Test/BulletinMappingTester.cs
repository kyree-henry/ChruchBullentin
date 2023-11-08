using ChruchBulletin.Core.Entity;
using DataAccess.Mapping;
using Shouldly;

namespace ChruchBulletin.DataAccess.Test
{
    public class BulletinMappingTester
    {
        [Test]
        public void ShouldMapBulletin()
        {
            Bulletin? bulletin = new();
            bulletin.Name = "Worship service";
            bulletin.Place = "Sanctuary";
            bulletin.Date = new DateTime(2024, 1, 1);

            using (DataContext dbContext = new(new TestDataConfiguration()))
            {
                dbContext.Add(bulletin);
                dbContext.SaveChanges();
            }

            Bulletin? rehydratedEntity;
            using (DataContext dbContext = new(new TestDataConfiguration()))
            {
                rehydratedEntity = dbContext.Set<Bulletin>().Single(b => b == bulletin);
            }

            //rehydratedEntity.ShouldBe(bulletin);
            rehydratedEntity?.Id.ShouldBe(bulletin.Id);
            rehydratedEntity?.Name.ShouldBe(bulletin.Name);
            rehydratedEntity?.Date.ShouldBe(bulletin.Date);
            rehydratedEntity?.Place.ShouldBe(bulletin.Place);
        }

    }

}