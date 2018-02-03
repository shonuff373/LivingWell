using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LivingWellMVC.Models {
    public interface ISubmissionInfo {
    }

    public class SubmissionInfo : ISubmissionInfo {
        public virtual string Name { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }

    }

    public class AnalysisSubmissionInfo : SubmissionInfo {

        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CommunityName { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public override string Name {
            get {
                if (string.IsNullOrWhiteSpace(base.Name)) {
                    base.Name = FirstName + " " + LastName;
                }

                return base.Name;
            }
            set { base.Name = value; }
        }


        #endregion

        #region Constructors

        public AnalysisSubmissionInfo() {

        }

        public AnalysisSubmissionInfo(string firstName, string lastname, string communityName, string addressOne, string addressTwo, string city, string state, string zip, string phone, string email) {
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

    public class ContactSubmissionInfo : SubmissionInfo {
        public string Service { get; set; }
    }

    public class ApplicationSubmissionInfo : SubmissionInfo {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondaryPhone { get; set; }

        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string PositionType { get; set; }
        public string PositionStatus { get; set; }
        public string WeeklyHours { get; set; }
        public string Referral { get; set; }
        public string ResumeFileName { get; set; }

        public ApplicationSubmissionInfo() { }

        public ApplicationSubmissionInfo(string firstName, string lastName, string name, string phone, string secondaryPhone, string emailAddress,
                         string addressOne, string addressTwo, string city, string state, string postalCode, string positionType,
                         string positionStatus, string weeklyHours, string referral, string resumeFileName, string message) {
                             this.FirstName = firstName;
                             this.LastName = lastName;
                             this.Name = firstName + " " + lastName;
                             this.Phone = phone;
                             this.SecondaryPhone = secondaryPhone;
                             this.EmailAddress = emailAddress;
                             this.AddressOne = addressOne;
                             this.AddressTwo = addressTwo;
                             this.City = city;
                             this.State = state;
                             this.PostalCode = postalCode;
                             this.PositionType = positionType;
                             this.PositionStatus = positionStatus;
                             this.WeeklyHours = weeklyHours;
                             this.Referral = referral;
                             this.ResumeFileName = resumeFileName;
                             this.Message = message;
        }

    }

    public enum PositionTypeEnum {
        [Description("Unknown")]
        Unknown = 0,
        [Description("Occupational Therapist")] 
        OccupationalTherapist = 1,
        [Description("Occupational Therapy Assistant (OTA)")] 
        OccupationalTherapistAssistant = 2,
        [Description("Physical Therapist")] 
        PhysicalTherapist = 3,
        [Description("Physical Therapy Assistant (PTA)")] 
        PhysicalTherapyAssistant = 4,
        [Description("Speech Therapist")]
        SpeechTherapist = 5

    }

    public enum PositionStatusEnum {
        [Description("Unknown")]
        Unknown = 0,
        [Description("Full Time")]
        FullTime = 1,
        [Description("Part Time")]
        PartTime = 2,
        [Description("Per Diem days / PRN")]
        PRN = 3
    }
}
