using System;
using System.Collections.Generic;
using BludataTest.Enums;
using BludataTest.Models;
using BludataTest.ResponseModels;
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
        public IEnumerable<SupplierResponseModel> GetAll()
        {
            return _supplierService.GetAll();
        }

        [HttpGet]
        [Route("id/{id}")]
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

        [HttpGet]
        [Route("name/{name}")]
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

        [HttpGet]
        [Route("cpf/{CPF}")]
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

        [HttpGet]
        [Route("cnpj/{CNPJ}")]
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

        [HttpGet]
        [Route("registerTime/{registerTime}")]
        public IActionResult FindByRegisterTime(DateTime registerTime)
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

        [HttpGet]
        [Route("company/{companyID}")]
        public IActionResult FindSuppliersByCompany(Guid id)
        {
            try
            {
                var suppliers = _supplierService.FindSuppliersByCompany(id);
                return new ObjectResult(suppliers);
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

        [HttpPut]
        [Route("id/{id}")]
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

        [HttpDelete]
        [Route("id/{id}")]
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