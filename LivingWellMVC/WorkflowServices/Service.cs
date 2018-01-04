using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using LivingWellMVC.Models;

namespace LivingWellMVC.WorkflowServices {
    public interface Service {
         Status ProcessWorkflow(SubmissionInfo info);
         void ValidateInfo(SubmissionInfo info);
         Email SetupEmailProperties(SubmissionInfo info, Status status, EmailType type);
         MailMessage GenerateEmail(Email email);
         Status SendEmails(EmailList email);
    }

    public class BaseService : Service {
        protected Status validation;
        protected string submissionEmailTemplate;
        protected string responseEmailTemplate;
        protected Email email;

        public virtual Status ProcessWorkflow(SubmissionInfo info) {
            Status status = new Status();

            ValidateInfo(info);

            if (status.StatusType == StatusEnum.Success) {
                EmailList emailList = new EmailList();
                emailList.Add(SetupEmailProperties(info, status, EmailType.Submission));
                emailList.Add(SetupEmailProperties(info, status, EmailType.Response));
                status = SendEmails(emailList);
            }

            return status;
        }

        public virtual void ValidateInfo(SubmissionInfo info) { }

        public virtual Email SetupEmailProperties(SubmissionInfo info, Status status, EmailType type) {
            switch (type) {
                case EmailType.Submission:
                    return SetupSubmissionEmailProperties(info, status);
                case EmailType.Response:
                    return SetupResponseEmailProperties(info, status);
                default:
                    return new AnalysisEmail();
            }
        }

        public virtual Email SetupSubmissionEmailProperties(SubmissionInfo info, Status status){

            email.ToAddress = email.LivingWellInfo.InfoEmailAddress;
            email.ToDisplayName = email.LivingWellInfo.FullName;
            email.FromAddress = info.EmailAddress;
            email.FromDisplayName = info.Name;
            email.Status = status;

            email.CalculateSubject();

            return email;
        }

        public virtual Email SetupResponseEmailProperties(SubmissionInfo info, Status status) {
            email.ToAddress = info.EmailAddress;
            email.ToDisplayName = info.Name;
            email.FromAddress = email.LivingWellInfo.NoReplyEmailAddress;
            email.FromDisplayName = email.LivingWellInfo.FullName;
            email.Status = status;

            email.CalculateSubject();

            return email;
        }
        
        public virtual MailMessage GenerateEmail(Email email) {
            // first we create a plain text version and set it to the AlternateView
            // then we create the HTML version
            MailMessage msg = new MailMessage(new MailAddress(email.FromAddress, email.FromDisplayName),
                new MailAddress(email.ToAddress, email.ToDisplayName));

            msg.From = new MailAddress(email.FromAddress, email.FromDisplayName);
            msg.Subject = email.Subject;
            msg.To.Add(email.ToAddress);
            msg.BodyEncoding = Encoding.UTF8;

            String plainBody = "Let's live well!";

            //first we create the text version
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainBody, Encoding.UTF8, "text/plain");
            msg.AlternateViews.Add(plainView);

            //now create the HTML version
            MailDefinition message = new MailDefinition();
            message.BodyFileName = email.TemplatePath;
            message.IsBodyHtml = true;
            message.From = email.FromAddress;
            message.Subject = email.Subject;

            //Build replacement collection to replace fields in template1.htm file
            //ListDictionary replacements = new ListDictionary();
            //replacements.Add("<%FIRSTNAME%>", "ToUsername");//example of dynamic content for Username

            //now create mail message using the mail definition object
            //the CreateMailMessage object takes a source control object as the last parameter,
            //if the object you are working with is webcontrol then you can just pass "this",
            //otherwise create a dummy control as below.
            MailMessage msgHtml = message.CreateMailMessage(email.ToAddress, email.BodyKeys, new LiteralControl());

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
        
        public virtual Status SendEmails(EmailList emailList) {
            try {
                foreach (Email email in emailList) {

                    //Generate Email
                    MailMessage msg = GenerateEmail(email);

                    //sending prepared email
                    SendEmail(msg);
                }
            } catch (Exception ex) {
                emailList.Status.StatusType = StatusEnum.Error;
                emailList.Status.Results.Add(new Result(ex.ToString(), StatusEnum.Error));
            }

            return emailList.Status;
        }

        protected virtual void SendEmail(MailMessage msg) {
            SmtpClient smtp = new SmtpClient();//It reads the SMPT params from Web.config
            smtp.Send(msg);

        }

        protected void ValidateEmptyString(string value) {
            if (string.IsNullOrWhiteSpace(value)) {
                validation.Results.Add(new Result(ResultMessages.EmailAddressIsEmpty, StatusEnum.Invalid));
            }
        }

        protected void ValidateEmailFormat(string email) {
            Regex rgx = new Regex(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$");
            if (!string.IsNullOrEmpty(email)) {
                if (!rgx.Match(email).Success) {
                    validation.Results.Add(new Result(ResultMessages.EmailAddressInvalidFormat, StatusEnum.Invalid));
                }
            }
        }

    }

    public class ResultMessages {
        public const string EmailAddressIsEmpty = "Email address is empty.";
        public const string EmailAddressInvalidFormat = "Email address is in an invalid format.";

    }
}
