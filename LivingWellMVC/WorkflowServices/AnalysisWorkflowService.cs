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
            this.email = new AnalysisEmail(EmailType.Submission);

            base.SetupSubmissionEmailProperties(info, status);
            email.CalculateBodyKeys(info);

            email.Status = status;

            return email;
        }

        public override Email SetupResponseEmailProperties(SubmissionInfo info, Status status){
            this.email = new AnalysisEmail(EmailType.Response);

            base.SetupResponseEmailProperties(info, status);
            email.CalculateBodyKeys(info);

            email.Status = status;

            return email;
        }
    }


}