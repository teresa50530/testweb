using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teresa.information
{
    public class ErrInfo
    {
        public ErrInfo()
        {
         ErrFlag = true;
          ErrMethodName = "";
          ErrMsg = "";
        }

        //錯誤檢查
        public bool ErrFlag { get; set; }

        //錯誤Method
        public string ErrMethodName { get; set; }

        //錯誤訊息
        public string ErrMsg { get; set; }


    }
}
