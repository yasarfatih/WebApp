using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tugce.Utils
{
    public static class ModelExtensions
    {
        public static string ParseModelStateErrors(this ModelStateDictionary stateDic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var state in stateDic.Values)
            {
                foreach (var error in state.Errors)
                {
                    sb.AppendFormat("{0}<br/>", error.ErrorMessage);
                }
            }
            return sb.ToString();
        }
    }
}
