using System;
using System.Collections.Generic;
using System.Linq;
using ExpressMapper;
using ExpressMapperExampleTests.Model;
using ExpressMapperExampleTests.Utility;
using NUnit.Framework;

namespace ExpressMapperExampleTests
{
    public class TestExpressMapperFunctions
    {
        [TearDown]
        public void TestTearDown()
        {
            //Reset compiled mapping code’s cache - for further unit test to run with same registering
            Mapper.Reset();
        }

        [Test]
        public void TestPropertyMapping()
        {
            // Registering Mapping
            Mapper.Register<GeneralModel, GeneralViewModel>()
                .Member(dest => dest.FullName, src => src.FirstName + ' ' + src.LastName)
                .Member(dest => dest.RoleName, src => src.Role.Name)
                .Ignore(dest => dest.Id);
            Mapper.Compile();

            var personViewModel = (Utils.GetSingleGeneralModel()).Convert();

            Assert.AreEqual(personViewModel.Id, 0, "The value is 0 since it is ignored.");
            Assert.AreEqual(personViewModel.FullName, "Adam Levine");
            Assert.AreEqual(personViewModel.Age, "38", "It supports type conversion - here int to string.");
            Assert.AreEqual(personViewModel.Gender, "True", "It supports type conversion - here bool to string.");
            Assert.AreEqual(personViewModel.RoleName, "Teacher");

            //Supports Collection
            var personArray = (Utils.GetMultipleGeneralModel()).Convert();
            Assert.AreEqual(personArray.Count(), 3);
            Assert.AreEqual(personArray[0].FullName, "Adam Levine");
            Assert.AreEqual(personArray[1].FullName, "Chester Bennington");
            Assert.AreEqual(personArray[2].FullName, "Avril Lavigne");
        }

        [Test]
        public void TestFunctionMapping()
        {
            //Registering Mapping - function can be defined implicit/explicit
            Mapper.Register<GeneralModel, GeneralViewModel>()
                .Function(dest => dest.FullName,
                    src =>
                    {
                        switch (src.Role.Name)
                        {
                            case "Teacher":
                                return "Teacher " + src.FirstName + " " + src.LastName;
                            case "Student":
                                return "Student " + src.FirstName + " " + src.LastName;
                            default:
                                return string.Empty;
                        }
                    })
                .Member(dest => dest.Gender, src => Utils.ResolveGender(src.Gender));

            var person = Utils.GetSingleGeneralModel();
            var personViewModel = person.Convert();
            Assert.AreEqual(personViewModel.FullName, "Teacher Adam Levine");
            Assert.AreEqual(personViewModel.Gender, "Male");

            person.Role.Name = "Student";
            person.Gender = false;
            personViewModel = person.Convert();
            Assert.AreEqual(personViewModel.FullName, "Student Adam Levine");
            Assert.AreEqual(personViewModel.Gender, "Female");

            person.Role.Name = "Something Else";
            personViewModel = person.Convert();
            Assert.AreEqual(personViewModel.FullName, string.Empty);
        }

        [Test]
        public void TestCustomMapping()
        {
            //Registering Mapping
            Mapper.RegisterCustom<DateTime, string>(src => src.ToLongDateString());

            var getLongDate = Mapper.Map<DateTime, string>(DateTime.Parse("2015-10-07T07:34:42-5:00"));
            Assert.AreEqual(getLongDate, "Wednesday, October 7, 2015");
        }

        [Test]
        public void TestComplexCustomMapping()
        {
            //Registering Mapping - for custom mapping
            Mapper.RegisterCustom<GeneralModel, GeneralViewModel, CustomMapper>();

            var person = Utils.GetSingleGeneralModel();
            var personViewModel = person.Convert();
            Assert.AreEqual(personViewModel.Id, 1);
            Assert.AreEqual(personViewModel.FullName, "Adam Levine");
            Assert.AreEqual(personViewModel.Gender, "Male");
            Assert.IsNull(personViewModel.Age, "It is null since no value is assigned in Map Model.");
            Assert.IsNull(personViewModel.RoleName, "It is null since no value is assigned in Map Model.");
        }

        [Test]
        public void TestPreMapping()
        {
            //Registering pre-mapping
            Mapper.Register<GeneralModel, GeneralViewModel>()
                .Before((src, dest) => src.Id = src.Id + 10);

            var personViewModel = (Utils.GetSingleGeneralModel()).Convert();
            Assert.AreEqual(personViewModel.Id, 11);
            Assert.IsNull(personViewModel.FullName, "It is null since no value is assigned.");
            Assert.AreEqual(personViewModel.Age, "38", "It supports type conversion - here int to string.");
            Assert.AreEqual(personViewModel.Gender, "True", "It supports type conversion - here bool to string.");
            Assert.IsNull(personViewModel.RoleName, "It is null since no value is assigned.");

            //Reset compiled mapping code’s cache - for further unit test to run with same registering
            Mapper.Reset();

            //Registering post-mapping
            Mapper.Register<GeneralModel, GeneralViewModel>()
                .After((src, dest) => dest.Id = dest.Id + 20);

            personViewModel = (Utils.GetSingleGeneralModel()).Convert();
            Assert.AreEqual(personViewModel.Id, 21);
            Assert.IsNull(personViewModel.FullName, "It is null since no value is assigned.");
            Assert.AreEqual(personViewModel.Age, "38", "It supports type conversion - here int to string.");
            Assert.AreEqual(personViewModel.Gender, "True", "It supports type conversion - here bool to string.");
            Assert.IsNull(personViewModel.RoleName, "It is null since no value is assigned.");
        }

        [Test]
        public void TestPostMapping()
        {
            //Registering post-mapping
            Mapper.Register<GeneralModel, GeneralViewModel>()
                .After((src, dest) => dest.Id = dest.Id + 20);

            var personViewModel = (Utils.GetSingleGeneralModel()).Convert();
            Assert.AreEqual(personViewModel.Id, 21);
            Assert.IsNull(personViewModel.FullName, "It is null since no value is assigned.");
            Assert.AreEqual(personViewModel.Age, "38", "It supports type conversion - here int to string.");
            Assert.AreEqual(personViewModel.Gender, "True", "It supports type conversion - here bool to string.");
            Assert.IsNull(personViewModel.RoleName, "It is null since no value is assigned.");
        }

        [Test]
        public void TestConstantorValueMapping()
        {
            //Registering Mapping
            Mapper.Register<GeneralModel, GeneralViewModel>()
                .Value(dest => dest.FullName, "Hello World")
                .Value(dest => dest.Id, 123);

            var personArray = Mapper.Map<List<GeneralModel>, GeneralViewModel[]>(Utils.GetMultipleGeneralModel());
            Assert.AreEqual(personArray.Count(), 3);
            Assert.AreEqual(personArray[0].Id, 123);
            Assert.AreEqual(personArray[0].FullName, "Hello World");
            Assert.AreEqual(personArray[1].Id, 123);
            Assert.AreEqual(personArray[1].FullName, "Hello World");
            Assert.AreEqual(personArray[2].Id, 123);
            Assert.AreEqual(personArray[2].FullName, "Hello World");
        }

        [Test]
        public void TestNullMapping()
        {
            //Registering Mapping
            Mapper.Register<GeneralModel, GeneralViewModel>().Member(dest => dest.FullName, src => src.FirstName + src.LastName);

            var person = new GeneralModel();
            Assert.IsNull(person.FirstName);
            Assert.IsNull(person.LastName);

            var personViewModel = (new GeneralModel()).Convert();
            Assert.IsNotNull(personViewModel.FullName);
            Assert.AreEqual(personViewModel.FullName, string.Empty);
        }
    }

    public class CustomMapper : ICustomTypeMapper<GeneralModel, GeneralViewModel>
    {
        public GeneralViewModel Map(IMappingContext<GeneralModel, GeneralViewModel> context)
        {
            return new GeneralViewModel
            {
                Id = context.Source.Id,
                Gender = Utils.ResolveGender(context.Source.Gender),
                FullName = context.Source.FirstName + " " + context.Source.LastName
            };
        }
    }
}
