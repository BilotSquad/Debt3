using DebtManager.Application.Services.Interfaces;
using DebtManager.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DebtManager.Web.Controllers
{
    public class PaymentsController : ApiController
    {
        private IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<PaymentDto> Get()
        {
            return _paymentService.GetAll();
        }

        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post(PaymentDto payment)
        {
            var result = _paymentService.Create(payment);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, result);
            return response;
        }
    }
}
