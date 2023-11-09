namespace ChruchBulletin.Core.Queries
{
    public class BulletinItemByDateAndTimeQuery
    {
        public DateTime TargetDate { get; }

        public BulletinItemByDateAndTimeQuery(DateTime dateTime)
        {
            TargetDate = dateTime;
        }
    }
}
