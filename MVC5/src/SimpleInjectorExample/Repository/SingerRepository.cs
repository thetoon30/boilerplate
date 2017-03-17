using System.Collections.Generic;
using System.Linq;
using SimpleInjectorExample.Models;

namespace SimpleInjectorExample.Repository
{
    public class SingerRepository : IRepository<Singer>
    {
        private List<Singer> _singers = new List<Singer>()
            {
                new Singer() { Id = 1, FirstName = "Chester", LastName = "Bennington", Genre = "Alternative Rock"},
                new Singer() { Id = 2, FirstName = "Adam", LastName = "Levine", Genre = "Pop Rock" },
                new Singer() { Id = 3, FirstName = "Marshall", LastName = "Mathers", Genre = "HipHop" }
            };

        public IEnumerable<Singer> GetList()
        {
            return _singers;
        }

        public Singer GetById(int id)
        {
            return FindById(id);
        }

        public IEnumerable<Singer> Add(Singer entity)
        {
            if (entity == null)
                return _singers;

            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName))
                return _singers;

            var maxId = _singers.Select(x => x.Id).Concat(new[] { 0 }).Max();
            maxId++;

            entity.Id = maxId;
            _singers.Add(entity);

            return _singers;
        }

        public IEnumerable<Singer> Update(Singer entity)
        {
            if (entity == null)
                return _singers;

            var person = _singers.Find(x => x.Id == entity.Id);

            if (person == null)
                return _singers;

            person.FirstName = entity.FirstName;
            person.LastName = entity.LastName;
            person.Genre = entity.Genre;

            return _singers;
        }

        public IEnumerable<Singer> Delete(int id)
        {
            var singer = FindById(id);

            if (singer != null)
                _singers.Remove(singer);

            return _singers;
        }

        private Singer FindById(int id)
        {
            return _singers.Find(x => x.Id == id);
        }
    }
}