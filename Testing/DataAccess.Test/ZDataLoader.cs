using ChruchBulletin.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace ChruchBulletin.DataAccess.Test
{
    [TestFixture]
    public class ZDataLoader
    {
        [Test]
        public void LoadData()
        {
            new DatabaseEmptier(TestHost.GetRequiredService<DbContext>().Database).DeleteAllData();

            var item1 = new BulletinItem { Date = new DateTime(2000, 1, 1), Name = "one" };
            var item2 = new BulletinItem { Date = new DateTime(2000, 1, 1), Name = "two" };
            var item3 = new BulletinItem { Date = new DateTime(2000, 1, 1), Name = "three" };
            var item4 = new BulletinItem { Date = new DateTime(2000, 1, 1), Name = "four" };

            using (var context = TestHost.GetRequiredService<DbContext>())
            {
                context.AddRange(item1, item2, item3, item4);
                context.SaveChanges();
            }

            Assert.Pass();
        }
    }
}
