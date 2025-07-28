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

    }

}
