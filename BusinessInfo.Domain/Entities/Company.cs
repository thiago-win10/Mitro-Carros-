using BusinessInfo.Domain.ValueObjects;

namespace BusinessInfo.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Segment { get; set; }
        public Address Address { get; set; }
        public Person ContactPerson { get; set; }

        protected Company() { }

        public Company(Address address, Person person, string name, string cnpj, string segment)
        {
            Address = address;
            ContactPerson = person;
            Name = name;
            Cnpj = cnpj;
            Segment = segment;
        }

        public void UpdateData(Address address, Person person, string name, string cnpj, string segment)
        {
            Address = address;
            ContactPerson = person;
            Name = name;
            Cnpj = cnpj;
            Segment = segment;
        }
    }

}
