using angular2inter.Core.Infrastructure.Repository;
using angular2inter.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace angular2inter.Controllers.Api
{
    [RoutePrefix("api/Person")]
    public class PersonController : ApiController
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET: api/Person
        public IHttpActionResult Get()
        {
            var persons = _personRepository.GetList();
            var output = new List<PersonModel>();
            foreach (var item in persons)
            {
                var person = new PersonModel();
                person.Id = item.ID;
                person.FirstMidName = item.FirstMidName;
                person.LastName = item.LastName;
                output.Add(person);
            }
            //string json = JsonConvert.SerializeObject(persons);

            return Ok(output);
        }

        // GET: api/Person/5
        public IHttpActionResult Get(string id)
        {
            return Ok();
        }

        // POST: api/Person
        public IHttpActionResult Post([FromBody]string value)
        {
            return Ok();
        }

        // PUT: api/Person/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            return Ok();
        }

        // DELETE: api/Person/5
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
