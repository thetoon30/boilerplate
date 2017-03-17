using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjectorExample.Models;
using SimpleInjectorExample.Repository;

namespace SimpleInjectorExampleTests.Repository
{
    [TestClass()]
    public class SingerRepositoryTests
    {
        private IRepository<Singer> _singerRepository;

        public SingerRepositoryTests()
        {
            _singerRepository = new SingerRepository();
        }

        [TestMethod]
        public void TestGetSingerList()
        {
            var singers = GetArrayofsingers(_singerRepository.GetList());

            Assert.AreEqual(singers.Count(), 3);
            Assert.AreEqual(singers[0].Id, 1);
            Assert.AreEqual(singers[0].FirstName, "Chester");
            Assert.AreEqual(singers[0].LastName, "Bennington");
            Assert.AreEqual(singers[0].Genre, "Alternative Rock");

            Assert.AreEqual(singers[1].Id, 2);
            Assert.AreEqual(singers[1].FirstName, "Adam");
            Assert.AreEqual(singers[1].LastName, "Levine");
            Assert.AreEqual(singers[1].Genre, "Pop Rock");

            Assert.AreEqual(singers[2].Id, 3);
            Assert.AreEqual(singers[2].FirstName, "Marshall");
            Assert.AreEqual(singers[2].LastName, "Mathers");
            Assert.AreEqual(singers[2].Genre, "HipHop");
        }

        [TestMethod]
        public void TestGetsingerById()
        {
            var singer = _singerRepository.GetById(-1);
            Assert.IsNull(singer, "It returns null if no such singer exists.");

            singer = _singerRepository.GetById(2);
            Assert.IsNotNull(singer);
            Assert.AreEqual(singer.FirstName, "Adam");
            Assert.AreEqual(singer.LastName, "Levine");
            Assert.AreEqual(singer.Genre, "Pop Rock");
        }

        [TestMethod]
        public void TestAdd()
        {
            var singers = GetArrayofsingers(_singerRepository.Add(null));
            Assert.AreEqual(singers.Count(), 3, "Doesn't add any singer when null is sent.");

            singers = GetArrayofsingers(_singerRepository.Add(new Singer()));
            Assert.AreEqual(singers.Count(), 3, "Doesn't add any singer when if FirstName and LastName is null.");

            var singer = new Singer() { FirstName = "", LastName = "", Genre = "blah blah" };
            singers = GetArrayofsingers(_singerRepository.Add(singer));
            Assert.AreEqual(singers.Count(), 3, "Doesn't add any singer when if FirstName or LastName is empty.");

            singer = new Singer() { FirstName = "Chad", LastName = "Kroeger", Genre = "Rock" };
            singers = GetArrayofsingers(_singerRepository.Add(singer));
            Assert.AreEqual(singers.Count(), 4);
            Assert.AreEqual(singers[3].Id, 4);
            Assert.AreEqual(singers[3].FirstName, "Chad");
            Assert.AreEqual(singers[3].LastName, "Kroeger");
            Assert.AreEqual(singers[3].Genre, "Rock");

            // adding singer with an empty list
            GetArrayofsingers(_singerRepository.Delete(1));
            GetArrayofsingers(_singerRepository.Delete(2));
            GetArrayofsingers(_singerRepository.Delete(3));
            singers = GetArrayofsingers(_singerRepository.Delete(4));

            Assert.AreEqual(singers.Count(), 0);

            singers = GetArrayofsingers(_singerRepository.Add(singer));
            Assert.AreEqual(singers.Count(), 1);
            Assert.AreEqual(singers[0].Id, 1);
            Assert.AreEqual(singers[0].FirstName, "Chad");
            Assert.AreEqual(singers[0].LastName, "Kroeger");
            Assert.AreEqual(singers[0].Genre, "Rock");
        }

        [TestMethod]
        public void TestUpdate()
        {
            var singers = GetArrayofsingers(_singerRepository.Update(null));
            Assert.AreEqual(singers.Count(), 3, "Doesn't update any data when null is sent.");

            var singer = new Singer() { Id = 5, FirstName = "Avril", LastName = "Lavigne", Genre = "Bellevilla, Canada" };
            singers = GetArrayofsingers(_singerRepository.Update(singer));
            Assert.AreEqual(singers.Count(), 3, "Doesn't update any data for id which doesn't exist is requested to update.");

            //Before update
            Assert.AreEqual(singers[0].Id, 1);
            Assert.AreEqual(singers[0].FirstName, "Chester");
            Assert.AreEqual(singers[0].LastName, "Bennington");
            Assert.AreEqual(singers[0].Genre, "Alternative Rock");

            singer = new Singer() { Id = 1, FirstName = "Avril", LastName = "Lavigne", Genre = "Pop Punk" };
            singers = GetArrayofsingers(_singerRepository.Update(singer));

            //After update
            Assert.AreEqual(singers.Count(), 3);
            Assert.AreEqual(singers[0].Id, 1);
            Assert.AreEqual(singers[0].FirstName, "Avril");
            Assert.AreEqual(singers[0].LastName, "Lavigne");
            Assert.AreEqual(singers[0].Genre, "Pop Punk");
        }

        [TestMethod]
        public void TestDelete()
        {
            var singers = GetArrayofsingers(_singerRepository.Delete(-1));
            Assert.AreEqual(singers.Count(), 3, "Doesn't delete any singer when id which doesn't exist.");

            singers = GetArrayofsingers(_singerRepository.Delete(2));
            Assert.AreEqual(singers.Count(), 2);
            Assert.AreEqual(singers[1].Id, 3);
            Assert.AreEqual(singers[1].FirstName, "Marshall");
            Assert.AreEqual(singers[1].LastName, "Mathers");
            Assert.AreEqual(singers[1].Genre, "HipHop");

            singers = GetArrayofsingers(_singerRepository.Delete(1));
            Assert.AreEqual(singers.Count(), 1);

            singers = GetArrayofsingers(_singerRepository.Delete(3));
            Assert.AreEqual(singers.Count(), 0);
        }

        private Singer[] GetArrayofsingers(IEnumerable<Singer> singers)
        {
            return singers as Singer[] ?? singers.ToArray();
        }
    }
}