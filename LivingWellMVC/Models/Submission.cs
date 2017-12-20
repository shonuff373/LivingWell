using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LivingWellMVC.Models {
    public interface ISubmissionInfo {
        private string GetEnumDescription();
    }

    public class SubmissionInfo : ISubmissionInfo {
        public virtual string Name { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }

        public string GetEnumDescription(this Enum value) {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
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
        public PositionTypeEnum PositionType { get; set; }
        public PositionStatusEnum PositionStatus { get; set; }
        public string WeeklyHours { get; set; }
        public string Referral { get; set; }
        public string ResumeFileName { get; set; }

    }

    public enum PositionTypeEnum {
        [Description("Unknown")]
        Unknown = 0,
        [Description("Occupational Therapy")]
        Occupational = 1,
        [Description("Physical Therapy")]
        Physical = 2,
        [Description("Speech Therapy")]
        Speech = 3
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
