using System;
using System.Collections.Generic;
using System.Linq;
using ExpressMapper;
using ExpressMapperExampleTests.EntityFramework;
using ExpressMapperExampleTests.Model;
using ExpressMapperExampleTests.Repository;
using ExpressMapperExampleTests.Utility;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ExpressMapperExampleTests
{
    public class TestExpressMapperWithEF
    {
        private PeopleRepository _peopleRepository;
        private PersonDetailRepository _personDetailRepository;
        private AddressDetailRepository _addressDetailRepository;

        [SetUp]
        public void TestSetUp()
        {
            _peopleRepository = new PeopleRepository(new ExpressTestMapperEntities());
            _personDetailRepository = new PersonDetailRepository(new ExpressTestMapperEntities());
            _addressDetailRepository = new AddressDetailRepository(new ExpressTestMapperEntities());

            RegisterMapping();
        }

        [TearDown]
        public void TestTearDown()
        {
            Mapper.Reset();
        }

        [Test]
        public void TestPeople()
        {
            // GetList
            var peopleList = _peopleRepository.GetList();
            var mappedArray = peopleList.ConvertToViewModel();
            Assert.AreEqual(mappedArray.Count(), 3);
            Assert.AreEqual(mappedArray[0].FullName, "Tom Cruise");
            Assert.AreEqual(mappedArray[1].FullName, "Drew Barrymore");
            Assert.AreEqual(mappedArray[2].FullName, "Adam Sandler");

            var mappedViewModelArray = peopleList.ConvertToViewModel();
            Assert.AreEqual(mappedViewModelArray.Count(), 3);

            //GetById
            var people = _peopleRepository.GetById(1);
            var mappedResult = people.Convert();
            Assert.AreEqual(mappedResult.FullName, "Tom Cruise");

            people = _peopleRepository.GetById(2);
            mappedResult = people.Convert();
            Assert.AreEqual(mappedResult.FullName, "Drew Barrymore");

            people = _peopleRepository.GetById(3);
            mappedResult = people.Convert();
            Assert.AreEqual(mappedResult.FullName, "Adam Sandler");
        }

        [Test]
        public void TestPersonDetail()
        {
            //GetList
            var peopleDetailList = _personDetailRepository.GetList();
            var mappedArray = peopleDetailList.Convert();
            Assert.AreEqual(mappedArray.Count(), 3);
            Assert.AreEqual(mappedArray[0].DateOfBirth, "Sunday, August 25, 1968");
            Assert.AreEqual(mappedArray[0].Gender, "Male");
            Assert.AreEqual(mappedArray[0].Height, "187");
            Assert.AreEqual(mappedArray[0].Weight, "88.6");

            Assert.AreEqual(mappedArray[1].DateOfBirth, "Tuesday, September 6, 1977");
            Assert.AreEqual(mappedArray[1].Gender, "Female");
            Assert.AreEqual(mappedArray[1].Height, "168");
            Assert.AreEqual(mappedArray[1].Weight, "60");

            Assert.AreEqual(mappedArray[2].DateOfBirth, "Wednesday, May 5, 1965");
            Assert.AreEqual(mappedArray[2].Gender, "Male");
            Assert.AreEqual(mappedArray[2].Height, "175.5");
            Assert.AreEqual(mappedArray[2].Weight, "78.5");

            //GetById
            var personDetail = _personDetailRepository.GetById(1);
            var mappedResult = personDetail.Convert();
            Assert.AreEqual(mappedResult.DateOfBirth, "Sunday, August 25, 1968");
            Assert.AreEqual(mappedResult.Gender, "Male");
            Assert.AreEqual(mappedResult.Height, "187");
            Assert.AreEqual(mappedResult.Weight, "88.6");

            personDetail = _personDetailRepository.GetById(3);
            mappedResult = personDetail.Convert();
            Assert.AreEqual(mappedResult.DateOfBirth, "Wednesday, May 5, 1965");
            Assert.AreEqual(mappedResult.Gender, "Male");
            Assert.AreEqual(mappedResult.Height, "175.5");
            Assert.AreEqual(mappedResult.Weight, "78.5");
        }

        [Test]
        public void TestAddressDetail()
        {
            //GetList
            var addressDetailList = _addressDetailRepository.GetList();
            var mappedArray = addressDetailList.Convert();
            Assert.AreEqual(mappedArray.Count(), 5);
            Assert.AreEqual(mappedArray[0].Address, "Bangkok, Thailand");
            Assert.AreEqual(mappedArray[1].Address, "California, USA");
            Assert.AreEqual(mappedArray[2].Address, "Paris, France");
            Assert.AreEqual(mappedArray[3].Address, "NewYork, USA");
            Assert.AreEqual(mappedArray[4].Address, "Rome, Italy");

            //GetByPersonId
            var addressDetail = _addressDetailRepository.GetByPersonId(1);
            var mappedResult = addressDetail.Convert();
            Assert.AreEqual(mappedResult.Count(), 1);
            Assert.AreEqual(mappedResult[0].Address, "Bangkok, Thailand");

            addressDetail = _addressDetailRepository.GetByPersonId(2);
            mappedResult = addressDetail.Convert();
            Assert.AreEqual(mappedResult.Count(), 2);
            Assert.AreEqual(mappedResult[0].Address, "California, USA");
            Assert.AreEqual(mappedResult[1].Address, "Paris, France");

            addressDetail = _addressDetailRepository.GetByPersonId(3);
            mappedResult = addressDetail.Convert();
            Assert.AreEqual(mappedResult.Count(), 2);
            Assert.AreEqual(mappedResult[0].Address, "NewYork, USA");
            Assert.AreEqual(mappedResult[1].Address, "Rome, Italy");
        }

        [Test]
        public void TestPersonViewModel()
        {
            // GetList - from DB
            var peopleList = _peopleRepository.GetList();

            var mappedArray = peopleList.ConvertToViewModel();
            Assert.AreEqual(mappedArray.Count(), 3);

            Assert.AreEqual(mappedArray[0].FullName, "Tom Cruise");
            Assert.AreEqual(mappedArray[0].PersonDetail.DateOfBirth, "Sunday, August 25, 1968");
            Assert.AreEqual(mappedArray[0].PersonDetail.Gender, "Male");
            Assert.AreEqual(mappedArray[0].PersonDetail.Height, "187");
            Assert.AreEqual(mappedArray[0].PersonDetail.Weight, "88.6");
            Assert.AreEqual(mappedArray[0].AddressDetails.Count(), 1);
            Assert.AreEqual(mappedArray[0].AddressDetails[0].Address, "Bangkok, Thailand");

            Assert.AreEqual(mappedArray[2].FullName, "Adam Sandler");
            Assert.AreEqual(mappedArray[2].PersonDetail.DateOfBirth, "Wednesday, May 5, 1965");
            Assert.AreEqual(mappedArray[2].PersonDetail.Gender, "Male");
            Assert.AreEqual(mappedArray[2].PersonDetail.Height, "175.5");
            Assert.AreEqual(mappedArray[2].PersonDetail.Weight, "78.5");
            Assert.AreEqual(mappedArray[2].AddressDetails.Count(), 2);
            Assert.AreEqual(mappedArray[2].AddressDetails[0].Address, "NewYork, USA");
            Assert.AreEqual(mappedArray[2].AddressDetails[1].Address, "Rome, Italy");

            // JSON Object
            var person = JsonConvert.DeserializeObject<Person>(Utils.GetPersonJsonString());
            var mappedViewModel = person.ConvertToViewModel();

            Assert.AreEqual(mappedViewModel.FullName, "Hello World");
            Assert.AreEqual(mappedViewModel.PersonDetail.DateOfBirth, "Tuesday, May 5, 2015");
            Assert.AreEqual(mappedViewModel.PersonDetail.Gender, "Female");
            Assert.IsNull(mappedViewModel.PersonDetail.Height);
            Assert.IsNull(mappedViewModel.PersonDetail.Weight);
            Assert.AreEqual(mappedViewModel.AddressDetails.Count(), 2);
            Assert.AreEqual(mappedViewModel.AddressDetails[0].Address, "Tokyo, Japan");
            Assert.AreEqual(mappedViewModel.AddressDetails[1].Address, "Kyoto, Japan");

            var persons = JsonConvert.DeserializeObject<List<Person>>(Utils.GetMultiplePersonJsonString());
            var mappedViewModelArray = persons.ConvertToViewModel();
            Assert.AreEqual(mappedViewModelArray.Count(), 3);

            Assert.AreEqual(mappedViewModelArray[0].FullName, "Hello World");
            Assert.AreEqual(mappedViewModelArray[0].PersonDetail.DateOfBirth, "Tuesday, May 5, 2015");
            Assert.AreEqual(mappedViewModelArray[0].PersonDetail.Gender, "Female");
            Assert.IsNull(mappedViewModelArray[0].PersonDetail.Height);
            Assert.IsNull(mappedViewModelArray[0].PersonDetail.Weight);
            Assert.AreEqual(mappedViewModelArray[0].AddressDetails.Count(), 2);
            Assert.AreEqual(mappedViewModelArray[0].AddressDetails[0].Address, "Tokyo, Japan");
            Assert.AreEqual(mappedViewModelArray[0].AddressDetails[1].Address, "Kyoto, Japan");

            Assert.AreEqual(mappedViewModelArray[1].FullName, "ABCD EFGH");
            Assert.AreEqual(mappedViewModelArray[1].PersonDetail.DateOfBirth, "Wednesday, June 9, 2010");
            Assert.AreEqual(mappedViewModelArray[1].PersonDetail.Gender, "Male");
            Assert.IsNull(mappedViewModelArray[1].PersonDetail.Height);
            Assert.IsNull(mappedViewModelArray[1].PersonDetail.Weight);
            Assert.AreEqual(mappedViewModelArray[1].AddressDetails.Count(), 0);

            Assert.AreEqual(mappedViewModelArray[2].FullName, "IJKL MNOP");
            Assert.AreEqual(mappedViewModelArray[2].PersonDetail.DateOfBirth, "Friday, August 14, 2009");
            Assert.AreEqual(mappedViewModelArray[2].PersonDetail.Gender, "Female");
            Assert.AreEqual(mappedViewModelArray[2].PersonDetail.Height, "100");
            Assert.AreEqual(mappedViewModelArray[2].PersonDetail.Weight, "30.5");
            Assert.AreEqual(mappedViewModelArray[2].AddressDetails.Count(), 3);
            Assert.AreEqual(mappedViewModelArray[2].AddressDetails[0].Address, "ttt, yyy");
            Assert.AreEqual(mappedViewModelArray[2].AddressDetails[1].Address, "abcd, efgh");
            Assert.AreEqual(mappedViewModelArray[2].AddressDetails[2].Address, "blah, blah");

        }

        private static void RegisterMapping()
        {
            Mapper.Register<Person, PersonModel>().Member(dest => dest.FullName, src => src.FirstName + " " + src.LastName);
            Mapper.Register<PersonDetail, PersonDetailModel>()
                .Member(dest => dest.Gender, src => Utils.ResolveGender(src.Gender))
                .Member(dest => dest.DateOfBirth, src => (DateTime.Parse(src.DateOfBirth)).ToLongDateString());
            Mapper.Register<AddressDetail, AddressDetailModel>();
            Mapper.Register<Person, PersonViewModel>()
                .Member(dest => dest.FullName, src => src.FirstName + " " + src.LastName);
        }
    }
}
