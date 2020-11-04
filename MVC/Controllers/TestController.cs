using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using teresa.business;
using teresa.information;
using static teresa.information.TestMasterInfo;

namespace MVC.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            ViewBag.Title = "ABC";


            return View();
        }

        public async Task <ActionResult> List(PageInationInfo pagination, Condtions condtions, string view = "")
        {
            #region Pager

            //if (pagination == null) pagination = new PageInationInfo();
            //if (pager.HasValue) pagination.Index = pager.Value;
            //pagination.Index = Index;
            //pagination.Size = Size;
            //pagination.Total = Total;
            //int lastPage = pagination.Total / pagination.Size;
            //if ((pagination.Total % pagination.Size) != 0) lastPage += 1;
            //if (pager == "first") pagination.Index = 1;
            //if (pager == "prev" && pagination.Index > 1) pagination.Index -= 1;
            //if (pager == "next" && ((pagination.Index + 1) <= lastPage)) pagination.Index += 1;
            //if (pager == "last") pagination.Index = lastPage;
            //if (!string.IsNullOrEmpty(pager) && pager.StartsWith("page"))
            //{
            //    pagination.Index = Convert.ToInt32(pager.Replace("page", ""));
            //}

            #endregion
            {
                #region 取資料

                //var biz = new teresa.business.TestBiz.Master();
                var request = new ApiRequest
                {
                    pagination = pagination,
                    condtions = condtions
                };
                var biz = new ApiBiz();
             var response =await biz.Post<ApiResponse>("http://localhost/API/api/","test", "load", request);
              
                //DataTable dt = biz.Load(ref pagination, Condtions: condtions);
              
                #region 註解
                //Linq
                //List<TestMasterInfo> model = new List<TestMasterInfo>();
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    model =
                //    dt.AsEnumerable()
                //    .Select(i => new TestMasterInfo
                //    {
                //        SID = i.Field<int>("SID"),
                //        ID = i.Field<string>("ID"),
                //        NO = i.Field<string>("NO"),
                //        Name = i.Field<string>("Name"),
                //        Address = i.Field<string>("Address"),
                //        Phone = i.Field<string>("Phone"),
                //        Age = i.Field<decimal?>("Age"),
                //        Birthday = i.Field<DateTime?>("Birthday"),
                //        CreateTime = i.Field<DateTime>("CreateTime"),
                //        UpdaueTime = i.Field<DateTime>("UpdaueTime")
                //    })
                //    .ToList();
                //}
                #endregion
                #endregion 註解尾

                #region ViewBag
                //ViewBag.Index = pagination.Index;
                //ViewBag.Size = pagination.Size;
                //ViewBag.Total = pagination.Total;
                //ViewBag.ConditonName = condtions.Name;
                ViewBag.condtions =response.condtions;
                ViewBag.Pager= response.pagination;
                #endregion

                //return View(model);

                string path = "~/Views/Shared/Test/_List.cshtml";
                if (!string.IsNullOrEmpty(view)) path = $"~/Views/Shared/Test/_List_{view}.cshtml";

                return PartialView(path, response.data);

            }
        }

        public ActionResult Read(Condtions condtions)
        {
            var biz = new teresa.business.TestBiz.Master();
            var model = biz.Load(SID: condtions.SID, ID: condtions.ID, NO: condtions.NO);
            //return View(model);
            return PartialView("~/Views/Shared/Test/_Read.cshtml", model);
        }
        //創建
        public ActionResult Create(bool isPartialView = false)
        {
            if (isPartialView)
                return PartialView("~/Views/Shared/Test/_Create.cshtml");
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(TestMasterInfo entity, bool isPartialView = false, string fun = null)
        {
            if (!string.IsNullOrEmpty(fun) && fun == "Cancel")
            {
                return Content("");
            }
            var biz = new TestBiz.Master();
            if (biz.Load(null, null, entity.NO) != null)
            {
                ModelState.AddModelError("error", "編號已存在");
            }
            if (ModelState.IsValid)
            {
                if (biz.Insert(entity) > 0)
                {
                    //return Read(new TestMasterInfo.Conditions { ID = entity.ID, NO = entity.NO });
                    string js = string.Format("$('#dumyID').val('{0}');$('#dumyList').submit();$('#dumyRead').submit();", entity.ID);
                    return JavaScript(js);
                }
            }
            if (isPartialView)
                return PartialView("~/Views/Shared/Test/_Create.cshtml", entity);
            else
                return View(entity);
        }
        public ActionResult Edit(TestMasterInfo entity, bool isPartialView = true)
        {
            if (isPartialView)
                return PartialView("~/Views/Shared/Test/_Edit.cshtml", entity);
            else
                return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TestMasterInfo entity, bool isPartialView = true, string fun = null)
        {
            if (!string.IsNullOrWhiteSpace(fun))
            {
                if (fun == "Cancel")
                {
                    return Content("");
                }
                if (fun == "Close")
                {
                    return Read(new TestMasterInfo.Condtions { SID = entity.SID, ID = entity.ID });
                }
            }
            if (ModelState.IsValid)
            {
                var biz = new TestBiz.Master();
                if (biz.Update(entity) > 0)
                {
                    //return Read(new TestMasterInfo.Conditions { ID = entity.ID });
                    string js = string.Format("$('#dumyID').val('{0}');$('#dumyList').submit();$('#dumyRead').submit();", entity.ID);
                    return JavaScript(js);
                }
            }
            if (isPartialView)
                return PartialView("~/Views/Shared/Test/_Edit.cshtml", entity);
            else
                return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var biz = new TestBiz.Master();
            int i = biz.Delete(SID: null, ID: id, null);
            if (i > 0)
            {
                string js = string.Format("$('#dumyList').submit();$('#divAjax').html('');");
                return JavaScript(js);
            }
            else
            {
                return JavaScript("alert('刪除失敗');");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Do(string mode, int? sid = null, string id = null)
        {
            switch (mode)
            {
                case "Insert":
                    return Create(true);
                case "Edit":
                    var biz = new TestBiz.Master();
                    return Edit(biz.Load(SID: sid, ID: id, null));
                case "Delete":
                    return Delete(id);
            }
            return JavaScript("");
        }

        public class ApiRequest
        {
            public PageInationInfo pagination{ get; set; }
            public TestMasterInfo.Condtions condtions { get; set; }
           
        }
        public class ApiResponse
        {
            public PageInationInfo pagination { get; set; }
            public TestMasterInfo.Condtions condtions { get; set; }
            public List<TestMasterInfo> data { get; set; }
        }
    }
}
