using System.Collections.Generic;

namespace ExpressMapperExampleTests.Model
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public PersonDetailModel PersonDetail { get; set; }
        public List<AddressDetailModel> AddressDetails { get; set; }
    }
}
