using ChruchBulletin.Core.Queries;

namespace ChruchBulletin.DataAccess.Test
{
    [TestFixture]
    public class BulletinItemByDateQueryTester
    {
        [Test]
        public void ShouldGetWithinDate()
        {
            var query = new BulletinItemByDateAndTimeQuery(new DateTime(1, 1, 2000));
            var handler = new BulletinItemByDateeHandler();
            handler.Handle(query);
        }
    }
}