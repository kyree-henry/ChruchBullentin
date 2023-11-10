using ChruchBulletin.Core.Entity;
using ChruchBulletin.Core.Queries;
using ChruchBulletin.DataAccess.Handlers;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace ChruchBulletin.DataAccess.Test
{
    [TestFixture]
    public class BulletinItemByDateQueryTester
    {
        [Test]
        public void ShouldGetWithinDate()
        {
            EmptyDatabase();
            IEnumerable<BulletinItem> entries = new List<BulletinItem>()
            {
              new BulletinItem() { Date = new DateTime(2000, 1, 1) },
              new BulletinItem() { Date = new DateTime(2001, 1, 1) },
              new BulletinItem() { Date = new DateTime(1999, 1, 1) },
              new BulletinItem() { Date = new DateTime(2000, 1, 1) },
            };

            using (DbContext context = TestHost.GetRequiredService<DbContext>() )
            {
                context.AddRange(entries);
                context.SaveChanges();
            }

            //arrange
            BulletinItemByDateAndTimeQuery query = new (new DateTime(2000, 1, 1));
            BulletinItemByDateHandler handler = TestHost.GetRequiredService<BulletinItemByDateHandler>();

            // act
            IEnumerable<BulletinItem> items = handler.Handle(query);

            // assert
            items.Count().ShouldBe(2);
            items.ShouldContain(entries.ElementAt(0));
            items.ShouldContain(entries.ElementAt(3));
            items.ShouldNotContain(entries.ElementAt(1));
            items.ShouldNotContain(entries.ElementAt(2));
        }

        private void EmptyDatabase()
        {
            new DatabaseEmptier(TestHost.GetRequiredService<DbContext>().Database).DeleteAllData();
        }
    }
}