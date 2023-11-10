namespace ChruchBulletin.Core.Entity
{
    public abstract class EntityBase<T> where T : EntityBase<T>, new()
    {
        public abstract Guid Id { get; set; }

        public bool Equals(T? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id.Equals(other.Id);
        }
        
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals (this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((T)obj);
        }

        public T GetIdClone() => new() { Id = Id };

        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => base.ToString() + "-" + Id;
       
        public static bool operator ==(EntityBase<T> left, EntityBase<T> right) => Equals(left, right);

        public static bool operator !=(EntityBase<T> left, EntityBase<T> right) => !Equals(left, right);
        
    }
}