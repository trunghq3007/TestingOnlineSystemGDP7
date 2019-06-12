using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class UserContext
    {
        
        public User CurrentUser { get; set; }

        public List<int> ListActionId { get; set; }

        //public bool HasPermission(string actionName)
        //{
        //    //if("".Equals(actionName))
        //    foreach( var action in ListActionId)
        //    {
        //        if (actionName.ToLower().Equals(action.ActionName.ToLower())) return true;
        //    }
        //    return false;
        //}
        public bool HasPermission(int actionId)
        {
            //if("".Equals(actionName))
            foreach (var action in ListActionId)
            {
                if (actionId == action) return true;
            }
            return false;
        }

    }
}
