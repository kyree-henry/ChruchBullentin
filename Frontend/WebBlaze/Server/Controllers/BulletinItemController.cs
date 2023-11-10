using ChruchBulletin.Core.Entity;
using ChruchBulletin.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ChruchBulletin.WebBlaze.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BulletinItemController : ControllerBase
    {
        private readonly IBulletinItemByDateHandler _handler;

        public BulletinItemController(IBulletinItemByDateHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IEnumerable<BulletinItem> Get()
        {
            IEnumerable<BulletinItem> items = _handler.Handle(new BulletinItemByDateAndTimeQuery(new DateTime(2000, 1, 1)));
            return items;
        }
    }
}
