using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using teresa.business;
using teresa.information;

namespace Web
{
    public partial class _default : System.Web.UI.Page
    {
        protected PageInationInfo pagination
        {
            set
            {
                ViewState["pagination"] = value;
            }
            get
            {
                if (ViewState["pagination"] == null) ViewState["pagination"] = new PageInationInfo() ;
                return (PageInationInfo)ViewState["pagination"];
            }
        }

         protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return; //第一次進網頁 
            bindMaster(null);
            bindMasterDetails(null, null);
            bindMasterForm(null, null);
        }
        #region fun
   

      
        //查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindMaster(txtName.Text); 
            bindMasterDetails( null,null);
            bindMasterForm(null, null);

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            dvMaster.ChangeMode(DetailsViewMode.Insert);
            bindMasterDetails(null, null);
            fvMaster.ChangeMode(FormViewMode.Insert);
            bindMasterForm(null, null);
            upDetailsView.Update();
            upFormView.Update();
            
        }
        #endregion


        #region gv

        protected void bindMaster(string name)
        {
            TestBiz.Master biz = new TestBiz.Master();
            var _pagination = pagination;
            DataTable dt = biz.Load(ref _pagination, Name: name);
            pagination = _pagination;
            gvMaster.DataSource = dt; //先存他的SOURCE
            gvMaster.DataBind(); //在BIND
            Pager.pagination = pagination;
            upGridView.Update();
        }
        //選取
        protected void gvMaster_SelectedIndexChanged1(object sender, EventArgs e)
        {
            int SID = Convert.ToInt32(gvMaster.SelectedDataKey.Values[0]);
            string ID = Convert.ToString(gvMaster.SelectedDataKey.Values[1]);
            dvMaster.ChangeMode(DetailsViewMode.ReadOnly);
            bindMasterDetails(SID, ID);
            bindMasterForm(SID, ID);
          
            
        }
        protected void gvMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvMaster.PageIndex = e.NewPageIndex;
            //bindMaster(null);
        }

        //protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
        //{
        //    var btn = (Button)sender;

        //    int lastPage = pagination.Total / pagination.Size;
        //    if ((pagination.Total % pagination.Size) > 0) lastPage = lastPage + 1;
        //    if (btn.CommandName == "pageFirst") pagination.Index = 1;
        //    if (btn.CommandName == "pagePrev") pagination.Index = (pagination.Index - 1) == 0 ? 1 : pagination.Index - 1;
        //    if (btn.CommandName == "pageNext") pagination.Index = (pagination.Index + 1) > lastPage ? lastPage : (pagination.Index + 1);
        //    if (btn.CommandName == "pageLast") pagination.Index = lastPage;

        //    bindMaster(null);
        //}

        protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime? Birthday = DataBinder.Eval(e.Row.DataItem, "Birthday") == DBNull.Value ? default(DateTime?)
                     : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Birthday"));

                double? age = null;
                if (Birthday.HasValue)
                {
                    age = (DateTime.Today.Year - Birthday.Value.Date.Year) + (DateTime.Today.Month - Birthday.Value.Date.Month) / 12.0;
                }
                if (age.HasValue)
                {
                    e.Row.Cells[5].Text = age.Value.ToString("#,0.0");

                }

            }
        }
        #endregion

        #region dv
        protected void bindMasterDetails(int? SID, string ID)
        {
            TestBiz.Master biz = new TestBiz.Master();
            TestMasterInfo e = biz.Load(SID, ID, null);

            //防呆 e == null ? new List<TestMasterInfo>()

            dvMaster.DataSource = e == null ? new List<TestMasterInfo>(): new List<TestMasterInfo> { e };
            dvMaster.DataBind();

    
            //gvMaster.DataSource = dt;
            //gvMaster.DataBind();
        }


//變換模式之前
        protected void dvMaster_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            
        
            int? SID = null;
            string ID = string.Empty;
            if(dvMaster.CurrentMode == DetailsViewMode.Insert)
            {
                if (gvMaster.SelectedDataKey != null)
                {
                    SID = Convert.ToInt32(gvMaster.SelectedDataKey.Values[0]);
                    ID = gvMaster.SelectedDataKey.Values[1].ToString();
                }
            }
            else
            {
                SID = Convert.ToInt32(dvMaster.DataKey.Values[0]);
                ID = dvMaster.DataKey.Values[1].ToString();
  
            }   
            dvMaster.ChangeMode(e.NewMode);
            bindMasterDetails(SID, ID);
       

        }

        protected void dvMaster_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {      
                         
            var entity = new TestMasterInfo
            {
               //NO = e.Values["NO"] == null ? null: e.Values["NO"].ToString(), //原本寫法
                NO = e.Values["NO"]?.ToString(), //精簡寫法
                Name = e.Values["Name"]?.ToString(),
                Address = e.Values["Address"]?.ToString(),
                Phone = e.Values["Phone"]?.ToString(),
                Birthday = e.Values[" Birthday"] == null ? default(DateTime?) : Convert.ToDateTime(e.Values["Birthday"])
            };  

            var biz = new TestBiz.Master();
            int i=  biz.Insert(entity);

            if (i == 0)
            {
                e.Cancel = true;
                //失敗的話執行這個
            }
            else
            {
                dvMaster.ChangeMode(DetailsViewMode.ReadOnly);
                bindMasterDetails(null, entity.ID);
                bindMaster(null);
            }

            //精簡寫法
            //            var biz = new TestBiz.Master();
            //            biz.Insert(new TestMasterInfo
            //            {
            //                Name = e.Values["Name"].ToString(),
            //                Address = e.Values["Address"].ToString(),
            //                Phone = e.Values["Phone"].ToString(),
            //                Birthday = Convert.ToDateTime(e.Values["Birthday"])
            //            }
            //            );

        }



  

        protected void dvMaster_ModeChanged(object sender, EventArgs e)
        {

        }

        protected void dvMaster_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            int SID = Convert.ToInt32(dvMaster.DataKey.Values[0]);
            string ID = dvMaster.DataKey.Values[1].ToString();

            var entity = new TestMasterInfo
            { SID =SID,
                ID = ID,
            //NO = e.Values["NO"] == null ? null: e.Values["NO"].ToString(), //原本寫法
            NO = e.NewValues["NO"]?.ToString(), //精簡寫法
                Name = e.NewValues["Name"]?.ToString(),
                Address = e.NewValues["Address"]?.ToString(),
                Phone = e.NewValues["Phone"]?.ToString(),
                Birthday = e.NewValues["Birthday"] == null ? default(DateTime?) : Convert.ToDateTime(e.NewValues["Birthday"])
            };

            var biz = new TestBiz.Master();
            int i = biz.Update(entity);

            if (i == 0)
            {
                e.Cancel = true;
                //失敗的話執行這個
            }
            else
            {
                dvMaster.ChangeMode(DetailsViewMode.ReadOnly);
                bindMasterDetails(SID, ID);
                bindMaster(null);
            }
        }

        protected void dvMaster_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
        {
            int  SID = Convert.ToInt32(dvMaster.DataKey.Values[0]);
            string   ID = dvMaster.DataKey.Values[1].ToString();
            var biz = new TestBiz.Master();

        
            int i = biz.Delete(SID, ID, null);

            if (i == 0)
            {
                e.Cancel = true;
                //失敗的話執行這個
            }
            else
            {
                bindMasterDetails(null, null);
                bindMaster(null);
            }
        }


        #endregion

        #region Master FormView

        protected void bindMasterForm(int? SID, string ID)
        {
            TestBiz.Master biz = new TestBiz.Master();
            TestMasterInfo e = biz.Load(SID, ID, null);
            fvMaster.DataSource = e == null ? new List<TestMasterInfo>() : new List<TestMasterInfo> { e };
            fvMaster.DataBind();
            upFormView.Update();
        }

        protected void fvMaster_DataBound(object sender, EventArgs e)
        {
            var control = fvMaster.FindControl("ddlAddress");
            if (control != null)
            {
                var ddl = ((DropDownList)control);
                ddl.DataSource = Address();
                ddl.DataBind();

                TestMasterInfo drv = (TestMasterInfo)fvMaster.DataItem;
                if (drv != null && !DBNull.Value.Equals(drv.Address))
                    ddl.SelectedValue = drv.Address;
            }
        }
        #endregion

        //    protected void fvMaster_DoCommand(object sender, EventArgs e)
        //    {
        //        int? SID = null;
        //        string ID = string.Empty;
        //        if (fvMaster.CurrentMode == FormViewMode.Insert)
        //        {
        //            if(gvMaster.SelectedDataKey !=null)

        //        {
        //            SID = Convert.ToInt32(gvMaster.SelectedDataKey.Values[0]);
        //            ID = gvMaster.SelectedDataKey.Values[1].ToString();

        //        }
        //        }
        //        else
        //        {
        //            SID = Convert.ToInt32(dvMaster.DataKey.Values[0]);
        //            ID = dvMaster.DataKey.Values[1].ToString();

        //        }
        //        Button btn = (Button)sender;

        //if (btn.CommandName == "Create") {
        //        //TestBiz biz == new TestBiz.Master();
        //        //    No=e.
        //                }
        //        if (btn.CommandName == "Canel") {
        //            fvMaster.ChangeMode(FormViewMode.ReadOnly);
        //            bindMasterForm(SID, ID);
        //        }
        //    }

        protected void fvMaster_ItemInserting(object sender, FormViewInsertEventArgs e)
        {

            
            var entity = new TestMasterInfo
            {
                //NO = e.Values["NO"] == null ? null: e.Values["NO"].ToString(), //原本寫法
                NO = e.Values["NO"]?.ToString(), //精簡寫法
                Name = e.Values["Name"]?.ToString(),
                //Address = e.Values["Address"]?.ToString(),
                //Address = ((DropDownList)fvMaster.FindControl("ddlAddress")).SelectedValue,
                Address = e.Values["Address"]?.ToString().Split(new string[] { ":::" }, System.StringSplitOptions.None)[0],
                Phone = e.Values["Phone"]?.ToString(),
                Birthday = e.Values[" Birthday"] == null ? default(DateTime?) : Convert.ToDateTime(e.Values["Birthday"])
            };
            if (Page.IsValid)
            {

      
            var biz = new TestBiz.Master();
            int i = biz.Insert(entity);

            if (i == 0)
            {
                e.Cancel = true;
                //失敗的話執行這個
            }
            else
            {
                fvMaster.ChangeMode(FormViewMode.ReadOnly);
                bindMasterDetails(null, entity.ID);
                bindMaster(null);
                    upGridView.Update();
            }

              }
        }

        protected void fvMaster_ModeChanging(object sender, FormViewModeEventArgs e)
        {

            int? SID = null;
            string ID = string.Empty;
            if (fvMaster.CurrentMode == FormViewMode.Insert)
            {
              
                    if (gvMaster.SelectedDataKey != null)
                    {
                SID = Convert.ToInt32(gvMaster.SelectedDataKey.Values[0]);
                ID = gvMaster.SelectedDataKey.Values[1].ToString();
                }
            

            }
            else
            {
                SID = Convert.ToInt32(fvMaster.DataKey.Values[0]);
                ID = fvMaster.DataKey.Values[1].ToString();

            }
            fvMaster.ChangeMode(e.NewMode);
            bindMasterForm(SID, ID);

        }

        protected void fvMaster_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {

            int SID = Convert.ToInt32(fvMaster.DataKey.Values[0]);
            string ID = fvMaster.DataKey.Values[1].ToString();

            var entity = new TestMasterInfo
            {
                
                SID = SID,
                ID = ID,
                //NO = e.Values["NO"] == null ? null: e.Values["NO"].ToString(), //原本寫法
                NO = e.NewValues["NO"]?.ToString(), //精簡寫法
                Name = e.NewValues["Name"]?.ToString(),
                //Address = e.NewValues["Address"]?.ToString(),
                Address = e.NewValues["Address"]?.ToString().Split(new string[] { ":::" }, System.StringSplitOptions.None)[0],
                //Address = ((DropDownList)fvMaster.FindControl("ddlAddress")).SelectedValue,
                Phone = e.NewValues["Phone"]?.ToString(),
                Birthday =string.IsNullOrEmpty( e.NewValues["Birthday"].ToString())? default(DateTime?) : Convert.ToDateTime(e.NewValues["Birthday"])
            };

            var biz = new TestBiz.Master();
            int i = biz.Update(entity);

            if (i == 0)
            {
                e.Cancel = true;
                //失敗的話執行這個
            }
            else
            {
                fvMaster.ChangeMode(FormViewMode.ReadOnly);
                bindMasterForm(null, entity.ID);
                bindMaster(null);
            }
        }

        protected void fvMaster_ItemDeleting(object sender, FormViewDeleteEventArgs e)
        {
            int SID = Convert.ToInt32(fvMaster.DataKey.Values[0]);
            string ID = fvMaster.DataKey.Values[1].ToString();
            var biz = new TestBiz.Master();


            int i = biz.Delete(SID, ID, null);

            if (i == 0)
            {
                e.Cancel = true;
                //失敗的話執行這個
            }
            else
            {
                bindMasterDetails(null, null);
                bindMaster(null);
            }
        }

        //客製化
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            var cv = (CustomValidator)source;
            var ctl = fvMaster.FindControl(cv.ControlToValidate);
            if(ctl != null)
            {
                var c = (TextBox)ctl;
                if (!string.IsNullOrEmpty(c.Text))
                {
                    var biz = new TestBiz.Master();
                    var e = biz.Load(null, null, c.Text.Trim());

                    if (e == null)
                        args.IsValid = true;
                    else
                        cv.ErrorMessage = "重複了";
                }
                else
                    cv.ErrorMessage = "請輸入";
            }
        }

        protected DataTable Address()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");


            var dr = dt.NewRow();
            dr["Id"] = Guid.NewGuid().ToString();
            dr["Text"] = "----請選擇--------";
            dr["Value"] = "";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = Guid.NewGuid().ToString();
            dr["Text"] = "aaa";
            dr["Value"] = "1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = Guid.NewGuid().ToString();
            dr["Text"] = "bbb";
            dr["Value"] = "2";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = Guid.NewGuid().ToString();
            dr["Text"] = "ccc";
            dr["Value"] = "3";
            dt.Rows.Add(dr);
            return dt;          
        }

        protected void Pager_PageIndexChanged(object sender, EventArgs e, PageInationInfo pagination)
        {
            this.pagination = pagination;
            bindMaster(null);
        }

    }
}
