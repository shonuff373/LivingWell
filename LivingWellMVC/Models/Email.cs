using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Text;

namespace LivingWellMVC.Models {

    public abstract class Email {

        #region Properties

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

        private const string defaultFrom = "noreply@livingwellrehabilitation.com";
        private const string defaultMailingAddress = "123 Main St., Philadelphia, PA 19191";

        #endregion  

        #region Constructors

        public Email() {
            this.FromAddress = defaultFrom;
        }

        public Email(string to, string from) {
            this.ToAddress = to;
            this.FromAddress = from;
        }

        public virtual void CalculateBodyKeys(SubmissionInfo info){
            this.BodyKeys = new ListDictionary();

            this.BodyKeys.Add("<%PHONE%>", info.Phone);
            this.BodyKeys.Add("<%EMAILADDRESS%>", info.EmailAddress);
            this.BodyKeys.Add("<%LWLADDRESS%>", defaultMailingAddress);

        }

        #endregion

        #region Methods

        public void DefaultParameters(string to, string from, string subject) {
            this.ToAddress = to;
            this.FromAddress = from;
            this.Subject = subject;
        }

        #endregion
    }

    public class EmailList : List<Email> {
        public Status Status { get; set; }
    }

    public class AnalysisEmail : Email {

        public override void CalculateBodyKeys(SubmissionInfo tmp) {
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
    }

    public class ContactEmail : Email {
        public override void CalculateBodyKeys(SubmissionInfo tmp) {
            base.CalculateBodyKeys(tmp);

            ContactSubmissionInfo info = (ContactSubmissionInfo)tmp;
            
            this.BodyKeys.Add("<%FULLNAME%>", info.Name);
            this.BodyKeys.Add("<%SERVICE%>", info.Service);
            this.BodyKeys.Add("<%MESSAGE%>", info.Message);

        }

    }

    public class ApplicationEmail : Email {
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
            this.BodyKeys.Add("<%POSITIONTYPE%>", Shared.GetEnumDescription((PositionTypeEnum)Convert.ToInt16(info.PositionType)));
            this.BodyKeys.Add("<%POSITIONSTATUS%>", Shared.GetEnumDescription((PositionStatusEnum)Convert.ToInt16(info.PositionStatus)));
            this.BodyKeys.Add("<%WEEKLYHOURS%>", info.WeeklyHours);
            this.BodyKeys.Add("<%REFERRAL%>", info.Referral);
            this.BodyKeys.Add("<%RESUMEFILENAME%>", info.ResumeFileName);
            this.BodyKeys.Add("<%MESSAGE%>", info.Message);
        }
    }


    public enum EmailType {
        Unknown = 0,
        Submission = 1,
        Response = 2
    }
}