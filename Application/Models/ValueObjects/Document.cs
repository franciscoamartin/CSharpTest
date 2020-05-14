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

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }

        public override string ToString()
        {
            return Number;
        }
    }
}