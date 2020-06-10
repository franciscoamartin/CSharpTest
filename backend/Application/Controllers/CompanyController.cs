using BludataTest.Models;
using BludataTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BludataTest.Controllers
{
    [Route("api/[Controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IEnumerable<Company> GetAll()
        {
            return _companyService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var company = _companyService.Read(id);
            return new ObjectResult(company);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Company company)
        {
            _companyService.Create(company);
            return Accepted();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] Company company)
        {
            _companyService.Update(id, company);
            return new NoContentResult();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            _companyService.Delete(id);
            return new NoContentResult();
        }
    }
}