namespace BludataTest.Models
{
    public class Telephones : BaseEntity
    {
        public Telephones(string number)
        {
            Number = number;
        }
        public Telephones()
        { }
        public string Number { get; set; }
    }

}