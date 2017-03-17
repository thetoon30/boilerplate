using System.Collections.Generic;
using System.Linq;
using SimpleInjectorExample.Models;

namespace SimpleInjectorExample.Repository
{
    public class ActorRepository : IRepository<Actor>
    {
        private List<Actor> _actors = new List<Actor>()
            {
                new Actor() { Id = 1, FirstName = "Tom", LastName = "Cruise", MovieName = "Mission Impossible"},
                new Actor() { Id = 2, FirstName = "Orlando", LastName = "Bloom", MovieName = "Lord of the Rings" },
                new Actor() { Id = 3, FirstName = "Drew", LastName = "Barrymore", MovieName = "50 First Dates" }
            };

        public IEnumerable<Actor> GetList()
        {
            return _actors;
        }

        public Actor GetById(int id)
        {
            return FindById(id);
        }

        public IEnumerable<Actor> Add(Actor entity)
        {
            if (entity == null)
                return _actors;

            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName))
                return _actors;

            var maxId = _actors.Select(x => x.Id).Concat(new[] { 0 }).Max();
            maxId++;

            entity.Id = maxId;
            _actors.Add(entity);

            return _actors;
        }

        public IEnumerable<Actor> Update(Actor entity)
        {
            if (entity == null)
                return _actors;

            var person = _actors.Find(x => x.Id == entity.Id);

            if (person == null)
                return _actors;

            person.FirstName = entity.FirstName;
            person.LastName = entity.LastName;
            person.MovieName = entity.MovieName;

            return _actors;
        }

        public IEnumerable<Actor> Delete(int id)
        {
            var actor = FindById(id);

            if (actor != null)
                _actors.Remove(actor);

            return _actors;
        }

        private Actor FindById(int id)
        {
            return _actors.Find(x => x.Id == id);
        }
    }
}