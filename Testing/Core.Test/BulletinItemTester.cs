using ChruchBulletin.Core.Entity;
using Shouldly;

namespace ChruchBulletin.Core.Test
{
    public class BulletinItemTester
    {
        [Test]
        public void ShouldHaveEntityEqualitySemantics()
        {
            new BulletinItem() { Id = Guid.NewGuid() }
               .ShouldNotBe(new BulletinItem() { Id = Guid.NewGuid() });

            BulletinItem item2 = new ();
            BulletinItem item1 = new ();
            item1.Id = Guid.NewGuid();
            item2.Id = item1.Id;
            item1.ShouldBe(item2);
            item2.ShouldBe(item1);
            Assert.True(item1 == item2);
        }
    }
}