using System;
using System.Collections.Generic;
using BludataTest.Enums;
using BludataTest.Models;
using BludataTest.Services;
using BludataTest.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace BludataTest.Controllers
{
    [Route("api/[Controller]")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public IEnumerable<Supplier> GetAll()
        {
            return _supplierService.GetAll();
        }

        [HttpGet("{id}", Name="GetSupplier")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var supplier = _supplierService.Read(id);
                return new ObjectResult(supplier); 

            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
        
        [HttpGet("{name}", Name="FindNameSupplier")]
        public IActionResult FindByName(string name)
        {
            try
            {
                var supplier = _supplierService.FindByName(name);
                return new ObjectResult(supplier); 
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{CPF}", Name="FindCPFSupplier")]
        public IActionResult FindByCPF(string cpf)
        {
            try
            {
                var supplier = _supplierService.FindByDocument(new Document(cpf, EDocumentType.CPF));
                return new ObjectResult(supplier); 
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{CNPJ}", Name="FindCNPJSupplier")]
        public IActionResult FindByCNPJ(string cnpj)
        {
            try
            {
                var supplier = _supplierService.FindByDocument(new Document(cnpj, EDocumentType.CNPJ));
                return new ObjectResult(supplier); 
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{registerTime}", Name="FindRegisterTimeSupplier")]
        public IActionResult FindByRegsiterTime(DateTime registerTime)
        {
            try
            {
                var supplier = _supplierService.FindByRegisterTime(registerTime);
                return new ObjectResult(supplier); 
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Supplier supplier)
        {
            try
            {
                _supplierService.Create(supplier);
                return Accepted();

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Supplier supplier)
        {
            try
            {
                _supplierService.Update(id, supplier);
                return new NoContentResult(); 
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _supplierService.Delete(id);
                return new NoContentResult(); 
            }
            catch (System.Exception)
            {
                return BadRequest();
            }            
        }
    }
}