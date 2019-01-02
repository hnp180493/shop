using AutoMapper;
using BotDetect.Web.Mvc;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class ContactController : Controller
    {
        IFeedbackService _feebackService;
        public ContactController(IFeedbackService feebackService)
        {
            _feebackService = feebackService;
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [SimpleCaptchaValidation("CaptchaCode", "ContactCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Feeback(FeedbackViewModel feedbackView)
        {
            if (ModelState.IsValid)
            {
                var feedbackModel = Mapper.Map<Feedback>(feedbackView);
                _feebackService.Create(feedbackModel);
                _feebackService.Save();

                string content = System.IO.File.ReadAllText(Server.MapPath("/Content/Client/Template/ContactEmail.html"));
                content = content.Replace("{{name}}", feedbackModel.Name);
                content = content.Replace("{{email}}", feedbackModel.Email);
                content = content.Replace("{{content}}", feedbackModel.Message);

                Common.MailHelper.SendMail("hnp180493@gmail.com", "Thông tin liên hệ", content);

            }
            return RedirectToAction("Index");
        }
    }
}