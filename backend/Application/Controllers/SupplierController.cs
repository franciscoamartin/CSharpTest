using BludataTest.Models;
using BludataTest.ResponseModels;
using BludataTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

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
            var supplier = _supplierService.GetById(id);
            return new ObjectResult(supplier);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Supplier supplier)
        {
            _supplierService.Create(supplier);
            return Accepted();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(Guid id, [FromBody] Supplier supplier)
        {
            _supplierService.Update(id, supplier);
            return new NoContentResult();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            _supplierService.Delete(id);
            return new NoContentResult();
        }

        [HttpGet]
        [Route("company/{companyId}")]
        public IActionResult GetSuppliersByCompany(Guid companyId)
        {
            var suppliers = _supplierService.GetSuppliersByCompany(companyId);
            return new ObjectResult(suppliers);
        }

        [HttpGet]
        [Route("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var supplier = _supplierService.GetByName(name);
            return new ObjectResult(supplier);
        }
        [HttpGet]
        [Route("name/{name}/{companyId}")]
        public IActionResult GetByNameAndCompany([FromRoute] string name, [FromRoute] Guid companyId)
        {
            var supplier = _supplierService.GetByNameAndCompany(name, companyId);
            return new ObjectResult(supplier);
        }

        [HttpGet]
        [Route("document/{document}")]
        public IActionResult GetByDocument(string document)
        {
            document = WebUtility.UrlDecode(document);
            var supplier = _supplierService.GetByDocument(document);
            return new ObjectResult(supplier);
        }
        [HttpGet]
        [Route("document/{document}/{companyId}")]
        public IActionResult GetByDocumentAndCompany([FromRoute] string document, [FromRoute] Guid companyId)
        {
            var supplier = _supplierService.GetByDocumentAndCompany(document, companyId);
            return new ObjectResult(supplier);
        }

        [HttpGet]
        [Route("registerTime/{registerTime}")]
        public IActionResult GetByRegisterTime(string registerTime)
        {
            var supplier = _supplierService.GetByRegisterTime(registerTime);
            return new ObjectResult(supplier);
        }
        [HttpGet]
        [Route("registerTime/{registerTime}/{companyId}")]
        public IActionResult GetByRegisterTimeAndCompany([FromRoute] string registerTime, [FromRoute] Guid companyId)
        {
            var supplier = _supplierService.GetByRegisterTimeAndCompany(registerTime, companyId);
            return new ObjectResult(supplier);
        }
    }
}