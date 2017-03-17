using System.Collections.Generic;
using System.Web.Http;
using SimpleInjectorExample.Models;
using SimpleInjectorExample.Repository;

namespace SimpleInjectorExample.Controllers
{
    [RoutePrefix("api/singer")]
    public class SingerController : ApiController
    {
        private IRepository<Singer> _singerRepository;

        public SingerController(IRepository<Singer> SingerRepository)
        {
            _singerRepository = SingerRepository;
        }

        [HttpGet]
        public IEnumerable<Singer> GetList()
        {
            return _singerRepository.GetList();
        }

        [HttpGet]
        [Route("{id}")]
        public Person GetById(int id)
        {
            return _singerRepository.GetById(id);
        }

        [HttpPost]
        [Route("add")]
        public IEnumerable<Singer> Add(Singer a)
        {
            return _singerRepository.Add(a);
        }

        [HttpPut]
        [Route("update")]
        public IEnumerable<Singer> Update(Singer a)
        {
            return _singerRepository.Update(a);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IEnumerable<Singer> Delete(int id)
        {
            return _singerRepository.Delete(id);
        }
    }
}
