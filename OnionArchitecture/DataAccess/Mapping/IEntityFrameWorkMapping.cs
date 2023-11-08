using Microsoft.EntityFrameworkCore;

namespace DataAccess.Mapping
{
    public interface IEntityFrameWorkMapping
    {
        void Map(ModelBuilder builder);
    }
}