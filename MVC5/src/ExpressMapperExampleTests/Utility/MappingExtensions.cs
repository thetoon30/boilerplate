using System.Collections.Generic;
using ExpressMapper;
using ExpressMapperExampleTests.EntityFramework;
using ExpressMapperExampleTests.Model;

namespace ExpressMapperExampleTests.Utility
{
    public static class MappingExtensions
    {
        public static GeneralViewModel Convert(this GeneralModel targets)
        {
            return Mapper.Map<GeneralModel, GeneralViewModel>(targets);
        }

        public static GeneralViewModel[] Convert(this IEnumerable<GeneralModel> targets)
        {
            return Mapper.Map<IEnumerable<GeneralModel>, GeneralViewModel[]>(targets);
        }

        public static PersonModel Convert(this Person targets)
        {
            return Mapper.Map<Person, PersonModel>(targets);
        }

        public static PersonModel[] Convert(this IEnumerable<Person> targets)
        {
            return Mapper.Map<IEnumerable<Person>, PersonModel[]>(targets);
        }

        public static PersonDetailModel Convert(this PersonDetail targets)
        {
            return Mapper.Map<PersonDetail, PersonDetailModel>(targets);
        }

        public static PersonDetailModel[] Convert(this IEnumerable<PersonDetail> targets)
        {
            return Mapper.Map<IEnumerable<PersonDetail>, PersonDetailModel[]>(targets);
        }

        public static AddressDetailModel[] Convert(this IEnumerable<AddressDetail> targets)
        {
            return Mapper.Map<IEnumerable<AddressDetail>, AddressDetailModel[]>(targets);
        }

        public static PersonViewModel ConvertToViewModel(this Person targets)
        {
            return Mapper.Map<Person, PersonViewModel>(targets);
        }

        public static PersonViewModel[] ConvertToViewModel(this IEnumerable<Person> targets)
        {
            return Mapper.Map<IEnumerable<Person>, PersonViewModel[]>(targets);
        }
    }
}
