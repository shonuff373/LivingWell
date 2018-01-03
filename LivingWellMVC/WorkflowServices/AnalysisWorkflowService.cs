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
    public class AnalysisWorkflowService : BaseService {
        private AnalysisSubmissionInfo info;

        public AnalysisWorkflowService() {
            this.validation = new Status(); 
        }
        public AnalysisWorkflowService(SubmissionInfo info) {
            this.info = (AnalysisSubmissionInfo)info; 
            this.validation = new Status();
        }

        public override Status ProcessWorkflow(SubmissionInfo info) {
            return base.ProcessWorkflow(info);
        }

        public override void ValidateInfo(SubmissionInfo info) {
            ValidateEmptyString(info.EmailAddress);
            ValidateEmailFormat(info.EmailAddress);
        }

        public override Email SetupSubmissionEmailProperties(SubmissionInfo info, Status status) {
            AnalysisEmail email = new AnalysisEmail(EmailType.Response);

            email.ToAddress = email.LivingWellInfo.InfoEmailAddress;
            email.ToDisplayName = email.LivingWellInfo.FullName;
            email.FromAddress = info.EmailAddress;
            email.FromDisplayName = info.Name;
            email.Subject = "Living Well Submission Analysis";

            email.CalculateBodyKeys(info);

            email.Status = status;

            return email;
        }

        public override Email SetupResponseEmailProperties(SubmissionInfo info, Status status){
            AnalysisEmail email = new AnalysisEmail(EmailType.Response);

            email.ToAddress = info.EmailAddress;
            email.ToDisplayName = info.Name;
            email.FromAddress = email.LivingWellInfo.NoReplyEmailAddress;
            email.FromDisplayName = email.LivingWellInfo.FullName;
            email.Subject = "Analysis Submission Recieved!";

            email.CalculateBodyKeys(info);

            email.Status = status;

            return email;
        }
    }


}