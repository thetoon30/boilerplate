using System.Collections.Generic;
using SimpleInjectorExample.Models;

namespace SimpleInjectorExample.Repository
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersonList();
        Person GetPersonById(int id);
        IEnumerable<Person> AddPerson(Person p);
        IEnumerable<Person> UpdatePerson(Person p);
        IEnumerable<Person> DeletePerson(int id);
    }
}