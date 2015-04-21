using DebtManager.Application.Services.Interfaces;
using DebtManager.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DebtManager.Web.Controllers
{
    public class DebtsController : ApiController
    {
        private IDebtService _debtService;

        public DebtsController(IDebtService debtService)
        {
            _debtService = debtService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<DebtDto> Get()
        {
            return _debtService.GetAll();
        }

        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post(DebtDto debt)
        {
            var result = _debtService.Create(debt);
            
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, result);
            return response;
        }
    }
}
