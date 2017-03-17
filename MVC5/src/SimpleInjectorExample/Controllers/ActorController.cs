using System.Collections.Generic;
using System.Web.Http;
using SimpleInjectorExample.Models;
using SimpleInjectorExample.Repository;

namespace SimpleInjectorExample.Controllers
{
    [RoutePrefix("api/actor")]
    public class ActorController : ApiController
    {
        private IRepository<Actor> _actorRepository;

        public ActorController(IRepository<Actor> actorRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpGet]
        public IEnumerable<Actor> GetList()
        {
            return _actorRepository.GetList();
        }

        [HttpGet]
        [Route("{id}")]
        public Person GetById(int id)
        {
            return _actorRepository.GetById(id);
        }

        [HttpPost]
        [Route("add")]
        public IEnumerable<Actor> Add(Actor a)
        {
            return _actorRepository.Add(a);
        }

        [HttpPut]
        [Route("update")]
        public IEnumerable<Actor> Update(Actor a)
        {
            return _actorRepository.Update(a);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IEnumerable<Actor> Delete(int id)
        {
            return _actorRepository.Delete(id);
        }
    }
}
