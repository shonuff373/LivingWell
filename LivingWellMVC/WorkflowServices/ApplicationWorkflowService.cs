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
            this.email = new ApplicationEmail(EmailType.Submission);

            base.SetupSubmissionEmailProperties(info, status);
            email.CalculateBodyKeys(info); 

            email.Status = status;

            return email;
        }

        public override Email SetupResponseEmailProperties(SubmissionInfo info, Status status) {
            this.email = new ApplicationEmail(EmailType.Response);

            base.SetupResponseEmailProperties(info, status);
            email.CalculateBodyKeys(info);

            email.Status = status;

            return email;
        }
    }
}