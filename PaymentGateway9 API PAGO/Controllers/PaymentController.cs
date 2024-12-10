using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Models;
using PaymentGateway.Services;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] PaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var response = _paymentService.ProcessPayment(request);

            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}