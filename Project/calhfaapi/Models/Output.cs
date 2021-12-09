using System;

namespace calhfaapi.Models {
    public class Output{//changed order to match expected return from calhfa
        //1
        public int ComplianceLoansInLine { get; set; }
        public String ComplianceDate { get; set; }

        //2
        public int SuspenseLoansInLine { get; set; }
        public String SuspenseDate { get; set; } 
        //3
        public int PostClosingLoansInLine { get; set; }
        public String PostClosingDate { get; set; }
        //4
        public int PostClosingSuspenseLoansInLine { get; set; }
        public String PostClosingSuspenseDate { get; set; } 
  }
}