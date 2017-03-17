namespace SimpleInjectorExample.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Singer : Person
    {
        public string Genre { get; set; }
    }

    public class Actor : Person
    {
        public string MovieName { get; set; }
    }
}