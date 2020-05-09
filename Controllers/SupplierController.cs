using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.Repositorio;
using BludataTest.Services;
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