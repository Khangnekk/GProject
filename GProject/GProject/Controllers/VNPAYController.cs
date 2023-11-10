using Gp.Services;
using GProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers
{
    public class VNPAYController : Controller
    {
        private readonly IVnPayService _vnPayService;

        public VNPAYController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost(Name = "create-payment")]
        public IActionResult CreatePaymentUrl(string OrderType, double Amount, string OrderDescription, string Name)
        {
            PaymentInformationModel model = new PaymentInformationModel();
            model.OrderType = OrderType;
            model.Amount = Amount;
            model.OrderDescription = OrderDescription;
            model.Name = Name;
            return View(_vnPayService.CreatePaymentUrl(model, HttpContext));
        }
        [HttpGet]
        public IActionResult PaymentCallback()
        {
            PaymentResponseModel response = (PaymentResponseModel)_vnPayService.PaymentExecute(Request.Query);
            ViewBag.Response = response;
            return View("/Views/Result.cshtml", response);
        }
    }

}
