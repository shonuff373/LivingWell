using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LivingWellMVC.Models {
    public class AnalysisEmail {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CommunityName { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public AnalysisEmail() { }

        public AnalysisEmail(string firstName, string lastname, string communityName, string addressOne, string addressTwo, string city, string state, string zip, string phone, string email) {
            this.FirstName = firstName;
            this.LastName = lastname;
            this.CommunityName = communityName;
            this.AddressOne = addressOne;
            this.AddressTwo = addressTwo;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.Phone = phone;
            this.Email = email;
        }
    }
}