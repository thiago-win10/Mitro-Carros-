namespace BusinessInfo.Domain.ValueObjects
{
    public class Person
    {
        public string FullName { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Person(string fullName, string occupation, string email, string phone)
        {
            FullName = fullName;
            Occupation = occupation;
            Email = email;
            Phone = phone;
        }
    }

}
