using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}", Name = "GetCompany")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var company = _companyService.Read(id);
                return new ObjectResult(company);

            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Company company)
        {
            try
            {
                _companyService.Create(company);
                return Accepted();

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Company company)
        {
            try
            {
                _companyService.Update(id, company);
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
                _companyService.Delete(id);
                return new NoContentResult();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}