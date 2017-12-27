using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LivingWellMVC.Models {
    public class Status {

        #region Properties 

        public StatusEnum StatusType { get; set; }
        public List<Result> Results { get; set; }

        #endregion

        #region Constructors
        public Status() {
            this.StatusType = StatusEnum.Success;
            this.Results = new List<Result>();
        }
         
        #endregion

        #region Methods

        public void AddResult(Result result){
            this.Results.Add(result);
            CalculateResults();
        }

        private void CalculateResults() {
            foreach (Result res in this.Results) {
                if (this.StatusType < res.StatusType) {
                    this.StatusType = res.StatusType;
                }
            }
        }

         public string GetResultMessage(){
            StringBuilder message = new StringBuilder();

            if (this.Results.Count == 0) {
                switch (this.StatusType) {               
                    case StatusEnum.Invalid:
                    case StatusEnum.Error:
                    case StatusEnum.Unknown:
                        this.Results.Add(new Result("This submission was not successful. Please try again or contact us by phone and/or email to confirm this submission. Thank you.", this.StatusType));
                        break;
                    default:
                        this.Results.Add(new Result("Submission successful, thank you!", this.StatusType));
                        break;
                }
            }

            foreach (Result res in this.Results) {
                if (res.StatusType == this.StatusType) {
                    message.AppendLine(res.Message);
                }
            }
            

            return message.ToString();
        }

        #endregion
    }

    public class Result {

        #region Properties

        public StatusEnum StatusType { get; set; }
        public string Message { get; set; }

        #endregion

        #region Constructors

        public Result(string message, StatusEnum type) {
            this.StatusType = type;
            this.Message = message;
        }

        #endregion

        #region Methods

       

        #endregion

    }

    public enum StatusEnum {
        Unknown = 0,
        Success = 1,
        Invalid = 2,
        Error = 3
    }
}