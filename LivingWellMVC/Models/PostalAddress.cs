using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LivingWellMVC.Models {
    public class PostalAddress {
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public Region Region { get; set; }
        public string PostalCode { get; set; }

        public PostalAddress() {
            this.Region = new Region();
        }

        public PostalAddress(string addressOne, string addressTwo, string city, string region, string postalCode) {
            this.AddressLineOne = addressOne;
            this.AddressLineTwo = addressTwo;
            this.City = city;
            this.Region = new Region(region);
            this.PostalCode = postalCode;
        }

        public string AddressOnOneLine() {
            return ValueCheck(this.AddressLineOne, ", ") +
                   ValueCheck(this.AddressLineTwo, ", ") +
                   ValueCheck(this.City, ", ") +
                   ValueCheck(this.Region.Code, ", ") +
                   ValueCheck(this.PostalCode, "");
        }

        public string AddressOnTwoLines(){
            return AddressOnTwoLines(Environment.NewLine);
        }

        public string AddressOnTwoLines(string newLine) {
            return ValueCheck(this.AddressLineOne, ", ") +
                   ValueCheck(this.AddressLineTwo, newLine) +
                   ValueCheck(this.City, ", ") +
                   ValueCheck(this.Region.Code, ", ") +
                   ValueCheck(this.PostalCode, "");
        }

        public string AddressOnThreeLines() {
            return AddressOnThreeLines(Environment.NewLine);
        }

        public string AddressOnThreeLines(string newLine) {
            return ValueCheck(this.AddressLineOne, newLine) +
                   ValueCheck(this.AddressLineTwo, newLine) +
                   ValueCheck(this.City, ", ") +
                   ValueCheck(this.Region.Code, newLine) +
                   ValueCheck(this.PostalCode, "");
        }

        public string CityStateZip() {
            return ValueCheck(this.City, ", ") +
                   ValueCheck(this.Region.Code, " ") +
                   ValueCheck(this.PostalCode, "");
        }

        public string ValueCheck(string value, string separator) {
            string concatenatedString = "";
            if (!string.IsNullOrWhiteSpace(value)) {
                concatenatedString = value + separator;
            }

            return concatenatedString;
        }
    }

    public class Region {
        public string Code { get; set; }
        public string Name { get { return GetRegionName(); } }

        public Region() { }

        public Region(string code) {
            this.Code = code;
        }

        private string GetRegionName(){
            switch(this.Code.ToUpper()){
                case "PA":
                default: 
                    return "Pennsylvania";
            }
        }
    }
}