using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LivingWellMVC.Models;
namespace LivingWellMVC.Services {
    public interface Service {
         Status ProcessWorkflow(SubmissionInfo info);
         void ValidateInfo(SubmissionInfo info);
         Email SetupEmailProperties(SubmissionInfo info, Status status, EmailType type);
         MailMessage GenerateEmail(Email email);
         Status SendEmails(EmailList email);
    }

    public class BaseService : Service {
        protected Status validation;

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
            return new AnalysisEmail();
        }

        public virtual Email SetupResponseEmailProperties(SubmissionInfo info, Status status) {

            return new AnalysisEmail();
        }
        
        public virtual MailMessage GenerateEmail(Email email) {
            return new MailMessage();
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
