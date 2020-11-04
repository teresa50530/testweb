using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teresa.information
{
    public class TestMasterInfo
    {

        public TestMasterInfo()
        {
            DateTime cur = DateTime.Now;
            ID = Guid.NewGuid().ToString();
            CreateTime = cur;
            UpdaueTime = cur;
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "索引直")]
        public int SID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        [Display(Name = "ID")]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "編號")]
        public string NO { get; set; }

        [Required(ErrorMessage ="笨蛋要輸名字")]
        [StringLength(10)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "電話")]
        public string Phone { get; set; }

        [StringLength(256)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd} ")]
        public DateTime? Birthday { get; set; }
        [Display(Name = "年齡")]
        [DisplayFormat(DataFormatString = "{0:#.#} ")]
        public decimal? Age { get; set; }
        [Display(Name = "建立")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "更新")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss} ")]
        public DateTime UpdaueTime { get; set; }

        public class Condtions
        {

            [Display(Name = "索引直")]
            public int? SID { get; set; }

            [Display(Name = "ID")]
            public string ID { get; set; }

            [Display(Name = "編號")]
            public string NO { get; set; }


            [Display(Name = "姓名")]
            public string Name { get; set; }


            [Display(Name = "電話")]
            public string Phone { get; set; }


            [Display(Name = "地址")]
            public string Address { get; set; }

            [Display(Name = "生日從")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd} ")]
            public DateTime? BirthdayFrom { get; set; }

            [Display(Name = "生日結束")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd} ")]
            public DateTime? BirthdayTo{ get; set; }

            [Display(Name = "年齡從")]
            [DisplayFormat(DataFormatString = "{0:#.#} ")]
            public decimal? AgeFrom { get; set; }

            [Display(Name = "年齡到")]
            [DisplayFormat(DataFormatString = "{0:#.#} ")]
            public decimal? AgeTo{ get; set; }
            [Display(Name = "建立")]
            public DateTime CreateTime { get; set; }

            [Display(Name = "更新")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss} ")]
            public DateTime UpdaueTime { get; set; }
        }

    }
}
    

