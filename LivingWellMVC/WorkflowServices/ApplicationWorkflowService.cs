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

namespace LivingWellMVC.WorkflowServices {
    public class ApplicationWorkflowService : BaseService{
        private ApplicationSubmissionInfo info;

        public ApplicationWorkflowService() {
            this.validation = new Status();
        }

        public ApplicationWorkflowService(SubmissionInfo info) {
            this.info = (ApplicationSubmissionInfo)info;
            this.validation = new Status();
        }

        public override void ValidateInfo(SubmissionInfo info) {
            base.ValidateInfo(info);
            ValidateEmptyString(info.EmailAddress);
            ValidateEmailFormat(info.EmailAddress);
        }

        public override Email SetupSubmissionEmailProperties(SubmissionInfo info, Status status) {
            ApplicationEmail email = new ApplicationEmail();

            email.ToAddress = info.EmailAddress;
            email.ToDisplayName = info.Name;
            email.FromDisplayName = "Living Well Rehabilitation";
            email.Subject = "Living Well Application Submission";
            email.EmailType = EmailType.Submission;
            email.Workflow = WorkflowType.Application;
            email.TemplatePath = "~/MailTemplates/ApplicationSubmissionNotificationEmailTemplate.html"; //https://www.quora.com/Is-there-any-way-to-include-an-HTML-page-in-an-HTML-page
            email.CalculateBodyKeys(info); //https://www.html5rocks.com/en/tutorials/webcomponents/imports/

            email.Status = status;

            return email;
        }

        public override Email SetupResponseEmailProperties(SubmissionInfo info, Status status) {
            ApplicationEmail email = new ApplicationEmail();

            email.ToAddress = info.EmailAddress;
            email.FromDisplayName = "Living Well Rehabilitation";
            email.Subject = "Application Submission Recieved!";
            email.EmailType = EmailType.Response;
            email.Workflow = WorkflowType.Application;
            email.TemplatePath = "~/MailTemplates/ApplicationResponseEmailTemplate.html";

            email.CalculateBodyKeys(info);

            email.Status = status;

            return email;
        }
    }
}