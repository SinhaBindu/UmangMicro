using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Manager;

namespace UmangMicro.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCoursesDetail(string Parame)
        {
            try
            {
                //dt = ds.Tables.Contains("Tables[0]") == true ? dt : ds.Tables[0];//dt1 = ds.Tables.Contains("Tables[1]") == true ? dt1 : ds.Tables[1];
                bool IsCheck = false;
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataSet ds = CommonModel.GetSPCourselist(Parame);
                if (ds.Tables.Count > 0)
                {
                    IsCheck = true;
                    if (ds.Tables[0].Rows.Count>0)
                    {
                        dt = ds.Tables[0];
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        dt1 = ds.Tables[1];
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        dt2= ds.Tables[2];
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        dt3 = ds.Tables[3];
                    }
                }
                var html_1 = ConvertViewToString("_SkillTrain", dt);
                var html_2 = ConvertViewToString("_Scheme", dt1);
                var html_3 = ConvertViewToString("_Scholarship", dt2);
                //var html3 = ConvertViewToString("_UserDetailData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, html1 = html_1, html2 = html_2, html3 = html_3 }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
    }
}