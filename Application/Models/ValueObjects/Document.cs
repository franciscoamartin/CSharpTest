using BludataTest.Enums;

namespace BludataTest.ValueObject
{
    public struct Document 
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;
        }

        public string Number { private get; set; }
        public EDocumentType Type { get; set; }

        public override string ToString()
        {
            return Number;
        }
    }
}