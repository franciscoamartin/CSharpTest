using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ResponseModels;
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
        public IEnumerable<SupplierResponseModel> GetAll()
        {
            return _supplierService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var supplier = _supplierService.GetById(id);
                return new ObjectResult(supplier);

            }
            catch (Exception)
            {
                return NotFound("Fornecedor não encontrado");
            }
        }

        [HttpGet]
        [Route("company/{companyId}")]
        public IActionResult GetSuppliersByCompany(Guid companyId)
        {
            try
            {
                var suppliers = _supplierService.GetSuppliersByCompany(companyId);
                return new ObjectResult(suppliers);
            }
            catch (Exception)
            {
                return NotFound("Fornecedor não encontrado");
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(Guid id, [FromBody] Supplier supplier)
        {
            try
            {
                _supplierService.Update(id, supplier);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _supplierService.Delete(id);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("name/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var supplier = _supplierService.GetByName(name);
                return new ObjectResult(supplier);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("name/{name}/{companyId}")]
        public IActionResult GetByNameAndCompany([FromRoute] string name, [FromRoute] Guid companyId)
        {
            try
            {
                var supplier = _supplierService.GetByNameAndCompany(name, companyId);
                return new ObjectResult(supplier);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("document/{document}")]
        public IActionResult GetByDocument(string document)
        {
            //try
            //{
            var supplier = _supplierService.GetByDocument(document);
            return new ObjectResult(supplier);
            //}
            // catch (Exception ex)
            // {
            //     return NotFound(ex.Message);
            // }
        }
        [HttpGet]
        [Route("document/{document}/{companyId}")]
        public IActionResult GetByDocumentAndCompany([FromRoute] string document, [FromRoute] Guid companyId)
        {
            try
            {
                var supplier = _supplierService.GetByDocumentAndCompany(document, companyId);
                return new ObjectResult(supplier);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("registerTime/{registerTime}")]
        public IActionResult GetByRegisterTime(string registerTime)
        {
            try
            {
                var supplier = _supplierService.GetByRegisterTime(registerTime);
                return new ObjectResult(supplier);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("registerTime/{registerTime}/{companyId}")]
        public IActionResult GetByRegisterTimeAndCompany([FromRoute] string registerTime, [FromRoute] Guid companyId)
        {
            try
            {
                var supplier = _supplierService.GetByRegisterTimeAndCompany(registerTime, companyId);
                return new ObjectResult(supplier);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}