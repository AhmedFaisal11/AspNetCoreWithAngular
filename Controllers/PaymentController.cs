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
                return StatusCode(404 , "Not Found");
            }

            return StatusCode(200 , pay);
        }

        [HttpPost]
        [Route("createPayment")]
        public async Task<ActionResult<PaymentDetails>> createPayment(PaymentDetails model)
        {
            _context.PaymentDetail.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(getdetailbyid) , new { id = model.PaymentDetailsId} , model);
        }

        [HttpPut]
        [Route("updatePayment/{id}")]
        public async Task<IActionResult> updatePayment(int id, PaymentDetails model)
        {
            if(id != model.PaymentDetailsId)
            {
                return StatusCode(400 , "Bad Request");     
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!PaymentDetailExist(id))
                {
                    return NotFound();
                }else{
                    throw;
                }
            }

            return StatusCode(200 , "Updated !!!!!");
        }

        private bool PaymentDetailExist(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("deletePayment/{id}")]
        public async Task<ActionResult<PaymentDetails>> deletePayment(int id)
        {
            var payment = await _context.PaymentDetail.FindAsync(id);

            if (payment == null)
            {
                return StatusCode(404 , "Not found");
            }

            _context.PaymentDetail.Remove(payment);

            await _context.SaveChangesAsync();

            return StatusCode(200 , "Payment Deleted Successfully");
        }
    }
}