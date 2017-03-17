using InternalModule.Boilerplate.Controllers;
using InternalModule.Boilerplate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InternalModule.Boilerplate.Helpers
{
    public class EmailHelper
    {
        public static void sendEmail(EmailModel email, string emailType)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(email.sender, email.senderPassword);

            MailMessage mail = new MailMessage(email.sender, email.receiver);
            mail.Subject = email.subject;
            mail.Body = RenderPartialViewToString(emailType, email);
            mail.IsBodyHtml = true;
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mail);
        }

        private static string RenderPartialViewToString(string viewName, object model)
        {
            var controllerContext = CreateController<EmailController>();

            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controllerContext.RouteData.GetRequiredString("action");
            }

            controllerContext.ViewData.Model = model;

            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext.ControllerContext, viewName);
                var viewContext = new ViewContext(controllerContext.ControllerContext, viewResult.View, controllerContext.ViewData, controllerContext.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);

                return stringWriter.GetStringBuilder().ToString();
            }
        }

        private static T CreateController<T>(RouteData routeData = null) where T : Controller, new()
        {
            T controller = new T();

            HttpContextBase wrapper = null;

            if (HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException("Can't create Controller Context if no active HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }
    }
}