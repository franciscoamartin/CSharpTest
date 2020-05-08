using System;

namespace BludataTest.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Company Company { get; set; }
        public string CNPJ { get; set; }
    }
}