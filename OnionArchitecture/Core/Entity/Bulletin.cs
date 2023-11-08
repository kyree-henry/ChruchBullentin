namespace ChruchBulletin.Core.Entity
{
    public class Bulletin : EntityBase<Bulletin>
    {
        public override Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Place { get; set; } 
        public DateTime Date { get; set; }
    }
}
