using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LivingWellMVC.Models {
    public class CompanyInfo {
        #region Private Properties

        private string _fullName;
        private string _shortName;
        private string _phone;
        private string _fax;
        private string _noReplyEmailAddress;
        private string _infoEmailAddress;
        private PostalAddress _address;
        #endregion

        #region Public Properties
        public string FullName {
            get { return _fullName; }
        }
        public string ShortName {
            get { return _shortName; }
        }
        public string Phone {
            get { return _phone; }
        }

        public string Fax {
            get { return _fax; }
        }

        public string NoReplyEmailAddress {
            get { return _noReplyEmailAddress; }
        }

        public string InfoEmailAddress {
            get { return _infoEmailAddress; }
        }

        public PostalAddress Address {
            get { return _address; }
        }

        #endregion

        #region Constructors 

        public CompanyInfo() {
            _fullName = "Living Well Rehabilitation";
            _shortName = "Living Well";
            _phone = "1833OTPTSLP";//"18336878757";
            _fax = "0987654321";
            _noReplyEmailAddress = "noreply@livingwellrehab.com";
            _infoEmailAddress = "info@livingwellrehab.com";
            _address = new PostalAddress("123 Main St.", "", "Philadelphia", "PA", "19191");
        }

        public string GetPhoneNumberWithDash(string value) {
            return Utility.GetPhoneNumberWithDash(value);
        }
        public string GetPhoneNumberWithPeriod(string value) {
            return Utility.GetPhoneNumberWithPeriod(value);
        }
        public string GetPhoneNumberWithLettersAndSpaces(string value) {
            return Utility.GetPhoneNumberWithLettersAndSpaces(value);
        }
        #endregion
    }

}