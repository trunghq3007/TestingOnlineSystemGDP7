using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class SearchPaging
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public SearchPaging()
        {
            PageIndex = 1;
            PageSize = 10;
            SearchString = "";
        }
    }
}
