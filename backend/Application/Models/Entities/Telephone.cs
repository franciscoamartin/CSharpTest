namespace BludataTest.Models
{
    public class Telephone : BaseEntity
    {
        public Telephone(string number)
        {
            Number = number;
        }
        public Telephone()
        { }
        public string Number { get; set; }
    }

}