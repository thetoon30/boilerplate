using System.Collections.Generic;
using ExpressMapperExampleTests.EntityFramework;
using ExpressMapperExampleTests.Model;
using Newtonsoft.Json;

namespace ExpressMapperExampleTests.Utility
{
    public class Utils
    {
        public static GeneralModel GetSingleGeneralModel()
        {
            return new GeneralModel()
            {
                Id = 1,
                FirstName = "Adam",
                LastName = "Levine",
                Age = 38,
                Gender = true,
                Role = new Role() { Id = 1, Name = "Teacher" }
            };
        }

        public static List<GeneralModel> GetMultipleGeneralModel()
        {
            return new List<GeneralModel>
            {
                GetSingleGeneralModel(),
                new GeneralModel() { Id = 2, FirstName = "Chester", LastName = "Bennington", Age = 40, Gender = true, Role = new Role() {Id = 1, Name = "Teacher"}},
                new GeneralModel() { Id = 3, FirstName = "Avril", LastName = "Lavigne", Age = 30, Gender = false, Role = new Role() {Id = 2, Name = "Student"}}
            };
        }

        public static string ResolveGender(bool gender)
        {
            switch (gender)
            {
                case false:
                    return "Female";
                case true:
                    return "Male";
                default:
                    return string.Empty;
            }
        }

        public static string ResolveGender(string gender)
        {
            switch (gender)
            {
                case "F":
                    return "Female";
                case "M":
                    return "Male";
                default:
                    return string.Empty;
            }
        }

        public static string GetPersonJsonString()
        {
            var person = GetPerson("Hello", "World", "2015-5-5", "F", null, null, new List<string>() { "Tokyo, Japan", "Kyoto, Japan" });
            return JsonConvert.SerializeObject(person);
        }

        public static string GetMultiplePersonJsonString()
        {
            var persons = new List<Person>()
            {
                GetPerson("Hello", "World", "2015-5-5", "F", null, null, new List<string>() {"Tokyo, Japan", "Kyoto, Japan"}),
                GetPerson("ABCD", "EFGH", "2010-6-9", "M", null, null, new List<string>() {}),
                GetPerson("IJKL", "MNOP", "2009-8-14", "F", 100, 30.5, new List<string>() {"ttt, yyy", "abcd, efgh", "blah, blah"})
            };

            return JsonConvert.SerializeObject(persons);
        }

        private static Person GetPerson(string firstname, string lastname, string dob, string gender, double? height, double? weight, List<string> addresses)
        {
            var person = new Person()
            {
                FirstName = firstname,
                LastName = lastname,
                PersonDetail = new PersonDetail()
                {
                    DateOfBirth = dob,
                    Gender = gender,
                    Height = height,
                    Weight = weight
                }
            };

            foreach (var address in addresses)
            {
                person.AddressDetails.Add(new AddressDetail() { Address = address });
            }

            return person;
        }
    }
}
