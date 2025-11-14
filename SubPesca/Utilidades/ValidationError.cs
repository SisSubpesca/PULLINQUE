using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SubPesca.Utilidades
{
    public class ValidationError : CustomValidator
    {
        public ValidationError(string group, string msg)
            : base()
        {
            base.ValidationGroup = group;
            base.ErrorMessage = msg;
            base.IsValid = false;
        }


    }
}