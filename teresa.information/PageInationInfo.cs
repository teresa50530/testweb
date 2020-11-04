using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teresa.information
{       [Serializable]
    public class PageInationInfo
    {
       
        public PageInationInfo()
        {
            Index = 1;
            Size = 3;
            Total = 0;
        } 
 
        ///目前頁面
        public int Index { get; set; }

        ///每頁比數

        public int Size { get; set; }
        ///總比數

        public int Total { get; set; }
    }
}
