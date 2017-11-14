using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LivingWellMVC.Models {
    public interface ITemplate {

    }

    public class Template: ITemplate {
        public string Name { get; set; }
        public string Phone { get; set; }        
        public string EmailAddress { get; set; }
        public string Message { get; set; }
    }

    public class AnalysisTemplate : Template {

        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CommunityName { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }



        #endregion
        
        #region Constructors

        public AnalysisTemplate() {
            
        }

        public AnalysisTemplate(string firstName, string lastname, string communityName, string addressOne, string addressTwo, string city, string state, string zip, string phone, string email) {
            this.FirstName = firstName;
            this.LastName = lastname;
            this.CommunityName = communityName;
            this.AddressOne = addressOne;
            this.AddressTwo = addressTwo;
            this.City = city;
            this.State = state;
            this.PostalCode = zip;
            this.Phone = phone;
            this.EmailAddress = email;

        }

        #endregion


        #region Methods

        

        #endregion

    }

    public class ContactTemplate : Template {
        public string Service { get; set; }
    }
}