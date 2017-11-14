using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using LivingWellMVC.Models;

namespace LivingWellMVC {
    public static class EmailService {

        public static Status ValidateForm(Template template) {
            Status validation = new Status();
            
            if (string.IsNullOrWhiteSpace(template.EmailAddress)) {
                validation.Results.Add(new Result(ResultMessages.EmailAddressIsEmpty, StatusEnum.Invalid));
            } else {
                Regex rgx = new Regex(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$");

                if (!rgx.Match(template.EmailAddress).Success){                
                    validation.Results.Add(new Result(ResultMessages.EmailAddressInvalidFormat, StatusEnum.Invalid));
                }

            }

            return validation;

        }

        public static Status ProcessEmail(AnalysisTemplate template){
            Status status = new Status();

            status = ValidateForm(template);
            if (status.StatusType == StatusEnum.Success){
                Email email = SetupAnalysisEmailProperties(template, status);
                status = SendAnalysisEmail(email);
            }

            return status;
        }

        private static Email SetupAnalysisEmailProperties(AnalysisTemplate template, Status status) {
            AnalysisEmail email = new AnalysisEmail();

            email.To = template.EmailAddress;
            //email.From = "info@livingwellrehabilitation.com";
            email.Subject = "Living Well Submission Analysis";
            email.EmailType = EmailType.Analysis;
            email.CalculateBodyKeys(template);

            email.Status = status;

            return email;
        }

        private static Status SendAnalysisEmail(Email email){
            try {
                //Generate Email
                MailMessage msg = GenerateEmail(email);

                //sending prepared email
                SendEmail(msg);
            } catch (Exception ex) {
                email.Status.StatusType = StatusEnum.Error;
                email.Status.Results.Add(new Result(ex.ToString(), StatusEnum.Error));
            }

            return email.Status;

        }

        private static MailMessage GenerateEmail(Email email) {
            // first we create a plain text version and set it to the AlternateView
            // then we create the HTML version
            MailMessage msg = new MailMessage(new MailAddress(email.From, "Living Well Rehabilitation"),
                new MailAddress("ilj373@gmail.com", "Recipient"));

            msg.From = new MailAddress(email.From, "Living Well Rehabilitation");
            msg.Subject = email.Subject;
            msg.To.Add(email.To);
            msg.BodyEncoding = Encoding.UTF8;

            String plainBody = "Body of plain email";

            //first we create the text version
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainBody, Encoding.UTF8, "text/plain");
            msg.AlternateViews.Add(plainView);

            //now create the HTML version
            MailDefinition message = new MailDefinition();
            message.BodyFileName = "~/MailTemplates/AnalysisEmailTemplate.html";
            message.IsBodyHtml = true;
            message.From = email.From;
            message.Subject = email.Subject;

            //Build replacement collection to replace fields in template1.htm file
            //ListDictionary replacements = new ListDictionary();
            //replacements.Add("<%FIRSTNAME%>", "ToUsername");//example of dynamic content for Username

            //now create mail message using the mail definition object
            //the CreateMailMessage object takes a source control object as the last parameter,
            //if the object you are working with is webcontrol then you can just pass "this",
            //otherwise create a dummy control as below.
            MailMessage msgHtml = message.CreateMailMessage(email.To, email.BodyKeys, new LiteralControl());

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msgHtml.Body, Encoding.UTF8, "text/html");

            //example of a linked image 
            //LinkedResource imgRes = new LinkedResource(Server.MapPath("~/MailTemplates/images/imgA.jpg"), System.Net.Mime.MediaTypeNames.Image.Jpeg);
            //imgRes.ContentId = "imgA";
            //imgRes.ContentType.Name = "imgA.jpg";
            //imgRes.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            //htmlView.LinkedResources.Add(imgRes);

            msg.AlternateViews.Add(htmlView);

            return msg;
        }

        private static void SendEmail(MailMessage msg) {
            SmtpClient smtp = new SmtpClient();//It reads the SMPT params from Web.config
            smtp.Send(msg);
        }
    }

    public class ResultMessages {
        public const string EmailAddressIsEmpty = "Email address is empty.";
        public const string EmailAddressInvalidFormat = "Email address is in an invalid format.";

    }
}