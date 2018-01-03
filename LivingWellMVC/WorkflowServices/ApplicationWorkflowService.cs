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
            ApplicationEmail email = new ApplicationEmail(EmailType.Submission);

            email.ToAddress = email.LivingWellInfo.InfoEmailAddress;
            email.ToDisplayName = email.LivingWellInfo.FullName;
            email.FromAddress = info.EmailAddress;
            email.FromDisplayName = info.Name;
            email.Subject = "Living Well Application Submission";
            email.CalculateBodyKeys(info); 

            email.Status = status;

            return email;
        }

        public override Email SetupResponseEmailProperties(SubmissionInfo info, Status status) {
            ApplicationEmail email = new ApplicationEmail(EmailType.Response);

            email.ToAddress = info.EmailAddress;
            email.ToDisplayName = info.Name;
            email.FromAddress = email.LivingWellInfo.NoReplyEmailAddress;
            email.FromDisplayName = email.LivingWellInfo.FullName;
            email.Subject = "Application Submission Recieved!";
            email.CalculateBodyKeys(info);

            email.Status = status;

            return email;
        }
    }
}