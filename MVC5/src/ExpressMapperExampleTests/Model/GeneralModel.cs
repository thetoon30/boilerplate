namespace ExpressMapperExampleTests.Model
{
    public class GeneralModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public bool Gender { get; set; } // 1= Male, 0 = Female

        public Role Role { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
