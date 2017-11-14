using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Text;

namespace LivingWellMVC.Models {

    public abstract class Email {

        #region Properties

        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public ListDictionary BodyKeys;
        public EmailType EmailType { get; set; }
        public Status Status { get; set; }

        private const string defaultFrom = "noreply@livingwellrehabilitation.com";

        #endregion  

        #region Constructors

        public Email() {
            this.From = defaultFrom;
        }

        public Email(string to, string from) {
            this.To = to;
            this.From = from;
        }

        public virtual void CalculateBodyKeys(Template tmp){ }

        #endregion

        #region Methods

        public void DefaultParameters(string to, string from, string subject) {
            this.To = to;
            this.From = from;
            this.Subject = subject;
        }

        #endregion
    }

    public class AnalysisEmail : Email {

        public override void CalculateBodyKeys(Template tmp) {
            AnalysisTemplate template = (AnalysisTemplate)tmp;

            this.BodyKeys = new ListDictionary();
 
            this.BodyKeys.Add("<%FIRSTNAME%>",  template.FirstName);
            this.BodyKeys.Add("<%LASTNAME%>",  template.LastName);
            this.BodyKeys.Add("<%COMMUNITYNAME%>",  template.CommunityName);
            this.BodyKeys.Add("<%ADDRESSONE%>",  template.AddressOne);
            this.BodyKeys.Add("<%ADDRESSTWO%>",  template.AddressTwo);
            this.BodyKeys.Add("<%CITY%>",  template.City);
            this.BodyKeys.Add("<%STATE%>",  template.State);
            this.BodyKeys.Add("<%POSTALCODE%>",  template.PostalCode);
            this.BodyKeys.Add("<%PHONE%>",  template.Phone);
            this.BodyKeys.Add("<%EMAILADDRESS%>",  template.EmailAddress);
            this.BodyKeys.Add("<%MESSAGE%>",  template.Message);

        }
    }

    public enum EmailType {
        Unknown = 0,
        Analysis = 1,
        Contact = 2,
        Response = 3
    }
}