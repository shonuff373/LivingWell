using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Text;

namespace LivingWellMVC.Models {

    public abstract class Email {

        #region Properties
        private const string _uploadFilePath = "~/App_Data/Uploads";
        public string ToAddress { get; set; }
        public string ToDisplayName { get; set; }
        public string FromAddress { get; set; }
        public string FromDisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public ListDictionary BodyKeys;
        public string TemplatePath { get; set; }
        public EmailType EmailType { get; set; }
        public WorkflowType Workflow { get; set; }
        public Status Status { get; set; }
        public string AttachmentFilePath { get; set; }
        public List<string> AttachmentFileNames;
        public CompanyInfo LivingWellInfo;
        protected string UploadFilePath {
            get { return HttpContext.Current.Server.MapPath(_uploadFilePath); } 
        }


        #endregion  

        #region Constructors

        public Email() {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.FromAddress = LivingWellInfo.NoReplyEmailAddress;
        }

        public Email(string to, string from) {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.ToAddress = to;
            this.FromAddress = from;
        }

        public virtual void CalculateBodyKeys(SubmissionInfo info){
            ////https://www.html5rocks.com/en/tutorials/webcomponents/imports/
            this.BodyKeys = new ListDictionary();

            this.BodyKeys.Add("<%PHONE%>", info.Phone);
            this.BodyKeys.Add("<%EMAILADDRESS%>", info.EmailAddress);
            this.BodyKeys.Add("<%LWLADDRESS%>", LivingWellInfo.Address.AddressOnOneLine());

        }

        public virtual void CalculateSubject(){
            string subject =  this.LivingWellInfo.ShortName + " " + 
                Enum.GetName(typeof(Models.WorkflowType), this.Workflow).ToString();

            switch (this.Workflow) {
                case WorkflowType.Contact:
                    subject += " Request";
                    break;
                default:
                    subject += " Submission";
                    break;
            }
            
            switch(this.EmailType){
                case Models.EmailType.Submission:
                    subject += " Notification";
                    break;
                case Models.EmailType.Response:
                    subject += " Received!";
                    break;
            }

            this.Subject = subject;
        }

        #endregion

        #region Methods

        public void DefaultParameters(string to, string from, string subject) {
            this.ToAddress = to;
            this.FromAddress = from;
            this.Subject = subject;
        }

        public string GetEmailTemplateFilePath(WorkflowType workflow, EmailType emailType) {
            this.Workflow = workflow;
            this.EmailType = EmailType;

            return GetEmailTemplateFilePath();
        }

        protected string GetEmailTemplateFilePath() {
            string path = "";
            //https://www.quora.com/Is-there-any-way-to-include-an-HTML-page-in-an-HTML-page
            switch (this.Workflow) {
                case WorkflowType.Analysis:
                    switch (this.EmailType) {
                        case EmailType.Response:
                            path = "~/MailTemplates/AnalysisResponseEmailTemplate.html";
                            break;
                        case EmailType.Submission:
                            path = "~/MailTemplates/AnalysisSubmissionNotificationEmailTemplate.html";
                            break;
                    }
                    break;
                case WorkflowType.Application:
                    switch (this.EmailType) {
                        case EmailType.Response:
                            path = "~/MailTemplates/ApplicationResponseEmailTemplate.html";
                            break;
                        case EmailType.Submission:
                            path = "~/MailTemplates/ApplicationSubmissionNotificationEmailTemplate.html";
                            break;
                    }
                    break;
                case WorkflowType.Contact:
                    switch (this.EmailType) {
                        case  EmailType.Response:
                            path = "~/MailTemplates/ContactResponseEmailTemplate.html";
                            break;
                        case EmailType.Submission:
                            path = "~/MailTemplates/ContactRequestNotificationEmailTemplate.html";
                            break;
                    }
                    break;
                default:

                    break;
            }

            return path;
        }

        #endregion
    }

    public class EmailList : List<Email> {
        public Status Status { get; set; }  

        public EmailList() {
            this.Status = new Status();
        }
    }

    public class AnalysisEmail : Email {
        #region Constructors 

        public AnalysisEmail() {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.FromAddress = LivingWellInfo.NoReplyEmailAddress;
            this.Workflow = WorkflowType.Analysis;
        }

        public AnalysisEmail(EmailType emailType) {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.FromAddress = LivingWellInfo.NoReplyEmailAddress;
            this.EmailType = emailType;
            this.Workflow = WorkflowType.Analysis;
            this.TemplatePath = this.GetEmailTemplateFilePath(this.Workflow, emailType);

        }

        public AnalysisEmail(string to, string from) {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.ToAddress = to;
            this.FromAddress = from;
            this.Workflow = WorkflowType.Analysis;
        }

        #endregion

        #region Methods
        public override void CalculateBodyKeys(SubmissionInfo tmp) {
            base.CalculateBodyKeys(tmp);

            AnalysisSubmissionInfo info = (AnalysisSubmissionInfo)tmp;
 
            this.BodyKeys.Add("<%FIRSTNAME%>",  info.FirstName);
            this.BodyKeys.Add("<%LASTNAME%>", info.LastName);
            this.BodyKeys.Add("<%FULLNAME%>", info.FirstName + " " + info.LastName);
            this.BodyKeys.Add("<%COMMUNITYNAME%>", info.CommunityName);
            this.BodyKeys.Add("<%ADDRESSONE%>", info.AddressOne);
            this.BodyKeys.Add("<%ADDRESSTWO%>", info.AddressTwo);
            this.BodyKeys.Add("<%CITY%>", info.City);
            this.BodyKeys.Add("<%STATE%>", info.State);
            this.BodyKeys.Add("<%POSTALCODE%>", info.PostalCode);
            this.BodyKeys.Add("<%MESSAGE%>", info.Message);

        }
        #endregion

    }

    public class ContactEmail : Email {
        #region Constructors

        public ContactEmail() { 
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.FromAddress = LivingWellInfo.NoReplyEmailAddress;
            this.Workflow = WorkflowType.Contact;
        }

        public ContactEmail(EmailType emailType) {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.FromAddress = LivingWellInfo.NoReplyEmailAddress;
            this.EmailType = emailType;
            this.Workflow = WorkflowType.Contact;
            this.TemplatePath = this.GetEmailTemplateFilePath(this.Workflow, emailType);

        }

        public ContactEmail(string to, string from) {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.ToAddress = to;
            this.FromAddress = from;
            this.Workflow = WorkflowType.Contact;
        }

        #endregion

        #region Methods

        public override void CalculateBodyKeys(SubmissionInfo tmp) {
            base.CalculateBodyKeys(tmp);

            ContactSubmissionInfo info = (ContactSubmissionInfo)tmp;
            
            this.BodyKeys.Add("<%FULLNAME%>", info.Name);
            this.BodyKeys.Add("<%SERVICE%>", info.Service);
            this.BodyKeys.Add("<%MESSAGE%>", info.Message);

        }

        #endregion
    }

    public class ApplicationEmail : Email {
        #region Constructors

        public ApplicationEmail() {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.FromAddress = LivingWellInfo.NoReplyEmailAddress;
            this.Workflow = WorkflowType.Application;
        }

        public ApplicationEmail(EmailType emailType) {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.FromAddress = LivingWellInfo.NoReplyEmailAddress;
            this.EmailType = emailType;
            this.Workflow = WorkflowType.Application;
            this.TemplatePath = this.GetEmailTemplateFilePath(this.Workflow, emailType);
        }

        public ApplicationEmail(string to, string from) {
            this.LivingWellInfo = new CompanyInfo();
            this.AttachmentFileNames = new List<string>();
            this.AttachmentFilePath = UploadFilePath;
            this.ToAddress = to;
            this.FromAddress = from;
            this.Workflow = WorkflowType.Application;
        }

        #endregion

        #region Methods

        public override void CalculateBodyKeys(SubmissionInfo tmp) {
            base.CalculateBodyKeys(tmp);

            ApplicationSubmissionInfo info = (ApplicationSubmissionInfo)tmp;

            this.BodyKeys.Add("<%FIRSTNAME%>", info.FirstName);
            this.BodyKeys.Add("<%LASTNAME%>", info.LastName);
            this.BodyKeys.Add("<%FULLNAME%>", info.FirstName + " " + info.LastName);
            this.BodyKeys.Add("<%SECONDARYPHONE%>", info.SecondaryPhone);
            this.BodyKeys.Add("<%ADDRESSONE%>", info.AddressOne);
            this.BodyKeys.Add("<%ADDRESSTWO%>", info.AddressTwo);
            this.BodyKeys.Add("<%CITY%>", info.City);
            this.BodyKeys.Add("<%STATE%>", info.State);
            this.BodyKeys.Add("<%POSTALCODE%>", info.PostalCode);
            this.BodyKeys.Add("<%POSITIONTYPE%>", Utility.GetEnumDescription((PositionTypeEnum)Convert.ToInt16(info.PositionType)));
            this.BodyKeys.Add("<%POSITIONSTATUS%>", Utility.GetEnumDescription((PositionStatusEnum)Convert.ToInt16(info.PositionStatus)));
            this.BodyKeys.Add("<%WEEKLYHOURS%>", info.WeeklyHours);
            this.BodyKeys.Add("<%REFERRAL%>", info.Referral);
            this.BodyKeys.Add("<%RESUMEFILENAME%>", info.ResumeFileName);
            this.BodyKeys.Add("<%MESSAGE%>", info.Message);
        }

        #endregion
    }


    public enum EmailType {
        Unknown = 0,
        Submission = 1,
        Response = 2
    }
}