using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using teresa.business;
using teresa.dataaccess;
using teresa.information;

namespace API.Controllers
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public TestMasterInfo Get(string id)
        {
            var biz = new TestBiz.Master();

            return biz.Load(SID: null, ID: id, NO: null);
        }

        // POST api/<controller>
        public dynamic Post(string id, [FromBody] dynamic value)
        {
            bool result = false;
            if (id == "load")
            {
                ModelRequest model = JsonConvert.DeserializeObject<ModelRequest>(JsonConvert.SerializeObject(value));
                var biz = new TestBiz.Master();
                PageInationInfo pagination = model.pagination;

                ModelResponse resultLoad = new ModelResponse { conditions = model.conditions };
                resultLoad.data = biz.Load(ref pagination, model.conditions).AsEnumerable()
                    .Select(i => new TestMasterInfo
                    {
                        SID = i.Field<int>("SID"),
                        ID = i.Field<string>("ID"),
                        NO = i.Field<string>("NO"),
                        Name = i.Field<string>("Name"),
                        Address = i.Field<string>("Address"),
                        Phone = i.Field<string>("Phone"),
                        Age = i.Field<decimal?>("Age"),
                        Birthday = i.Field<DateTime?>("Birthday"),
                        CreateTime = i.Field<DateTime>("CreateTime"),
                        UpdaueTime = i.Field<DateTime>("UpdaueTime")
                    })
                    .ToList();
                resultLoad.pagination = new ModelResponse.PaginationInfo { Index = pagination.Index, Size = pagination.Size, Total = pagination.Total };
                return resultLoad;
            }
            if (id == "create")
            {
                TestMasterInfo model = JsonConvert.DeserializeObject<TestMasterInfo>(JsonConvert.SerializeObject(value));
                if (ModelState.IsValid)
                {
                    var biz = new TestBiz.Master();
                    return biz.Insert(model);
                }
                return "資料驗證錯誤";
            }
            return result;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        //模板JSON轉換
        public class ModelRequest
        {
            public ModelRequest()
            {
                pagination = new PageInationInfo { Size = 10 };
                conditions = new TestMasterInfo.Condtions();
            }
            public PageInationInfo pagination { get; set; }
            public TestMasterInfo.Condtions conditions { get; set; }
        }

        //
        public class ModelResponse
        {
            public ModelResponse()
            {
                pagination = new PaginationInfo();
                data = new List<TestMasterInfo>();
            }
            public PaginationInfo pagination { get; set; }
            public List<TestMasterInfo> data { get; set; }
            public TestMasterInfo.Condtions conditions { get; set; }
            public class PaginationInfo
            {
                public PaginationInfo()
                {
                    Index = 1;
                    Size = 3;
                    Total = 0;
                }
                /// <summary>
                /// 目前頁面
                /// </summary>
                public int @Index { get; set; }
                /// <summary>
                /// 每頁筆數
                /// </summary>
                public int @Size { get; set; }
                /// <summary>
                /// 總筆數
                /// </summary>
                public int @Total { get; set; }
            }
        }
    }
}