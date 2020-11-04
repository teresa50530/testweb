using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teresa.information
{
    public class ExpLogInfo
    {
        public ExpLogInfo()
        {
            UDate=DateTime.Now;
        }
        /// <summary>
        /// SID:PRIMARY
        /// </summary>
        public int SID { get;set; }
        /// <summary>
        /// ClassName
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        ///  MethoddName
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrMsg { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? UDate { get; set; }
    }
}
