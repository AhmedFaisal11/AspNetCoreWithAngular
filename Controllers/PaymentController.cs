using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;

namespace PaymentAPI.Controllers
{
    [Route("api/Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
        public PaymentController(PaymentDetailContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getPaymentDetails")]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> getdetail()
        {
            return await _context.PaymentDetail.ToListAsync();
        }

        [HttpGet]
        [Route("getDetailById/{id}")]
        public async Task<ActionResult<PaymentDetails>> getdetailbyid(int id)
        {
            var pay = await _context.PaymentDetail.FindAsync(id);

            if(pay == null)
            {
                return NotFound();
            }

            return StatusCode(200 , pay);
        }

        [HttpPost]
        [Route("createPayment")]
        public async Task<ActionResult<PaymentDetails>> createPayment(PaymentDetails model)
        {
            _context.PaymentDetail.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(getdetail) , new { id = model.PaymentDetailsId} , model);
        }

        [HttpPut]
        [Route("updatePayment/{id}")]
        public async Task<IActionResult> updatePayment(int id, PaymentDetails model)
        {
        }

        [HttpDelete]
        [Route("deletePayment/{id}")]
        public async Task<ActionResult<PaymentDetails>> deletePayment(int id)
        {
            var payment = await _context.PaymentDetail.FindAsync(id);

            if (payment == null)
            {
                
            }
        }
    }
}