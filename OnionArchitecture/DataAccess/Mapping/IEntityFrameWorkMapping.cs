using Microsoft.EntityFrameworkCore;

namespace ChruchBulletin.DataAccess.Mapping
{
    public interface IEntityFrameWorkMapping
    {
        void Map(ModelBuilder builder);
    }
}