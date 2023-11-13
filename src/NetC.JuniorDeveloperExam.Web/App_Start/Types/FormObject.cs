using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Types
{
    /// <summary>
    /// Stores HTML Form data
    /// 
    /// <br/><br/>
    /// Enables extraction of, and enforces structure on, 
    /// Form data from a POST request
    /// </summary>
    public class FormObject
    {
        public string name;
        public string emailAddress;
        public string message;
    }
}
