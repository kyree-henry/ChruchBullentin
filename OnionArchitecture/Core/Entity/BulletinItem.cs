namespace ChruchBulletin.Core.Entity
{
    public class BulletinItem : EntityBase<BulletinItem>
    {
        public override Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Place { get; set; } 
        public DateTime Date { get; set; }
    }
}
