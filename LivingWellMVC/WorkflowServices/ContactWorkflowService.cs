using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using LivingWellMVC.Models;
using LivingWellMVC.Services;

namespace LivingWellMVC.Services {
    public class ContactWorkflowService : BaseService {
        private ContactSubmissionInfo info;

        public ContactWorkflowService(){
            this.validation = new Status();
        }

        public ContactWorkflowService(SubmissionInfo info) {
            this.info = (ContactSubmissionInfo)info;
            this.validation = new Status();
        }
        
        public override Models.Status ProcessWorkflow(Models.SubmissionInfo info) {
            return base.ProcessWorkflow(info);
        }

        public override void ValidateInfo(SubmissionInfo info) {
            ValidateEmptyString(info.EmailAddress);
            ValidateEmailFormat(info.EmailAddress);
        }

        public override Email SetupSubmissionEmailProperties(SubmissionInfo info, Status status) {
            ContactEmail email = new ContactEmail();

            email.ToAddress = info.EmailAddress;
            email.FromDisplayName = "Living Well Rehabilitation";
            email.Subject = "Living Well Contact Request";
            email.EmailType = EmailType.Submission;
            email.Workflow = WorkflowType.Contact;
            email.TemplatePath = "~/MailTemplates/ContactRequestNotificationEmailTemplate.html";
            email.CalculateBodyKeys(info);

            email.Status = status;

            return email;
        }

        public override Email SetupResponseEmailProperties(SubmissionInfo info, Status status) {
            AnalysisEmail email = new AnalysisEmail();

            email.ToAddress = info.EmailAddress;
            email.ToDisplayName = info.Name;
            email.FromDisplayName = "Living Well Rehabilitation";
            email.Subject = "Analysis Submission Recieved!";
            email.EmailType = EmailType.Response;
            email.Workflow = WorkflowType.Analysis;
            email.TemplatePath = "~/MailTemplates/ContactResponseEmailTemplate.html";

            email.CalculateBodyKeys(info);

            email.Status = status;

            return email;
        }
    }
}