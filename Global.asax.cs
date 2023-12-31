using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UmangMicro.Models;
using UmangMicro.Manager;
using System.Web.UI;
using SubSonic.Schema;
using System.Data;
using UmangMicro.Controllers;
using System.Web.Security;

namespace UmangMicro
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static UM_DBEntities dbe = new UM_DBEntities();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public static UserViewModel CUser
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var User = new UserViewModel();
                    if (HttpContext.Current.Session["CUser"] != null)
                    {
                        return (UserViewModel)HttpContext.Current.Session["CUser"];
                    }
                    else
                    {
                        StoredProcedure sp = new StoredProcedure("Get_SPCurrentLogin");
                        sp.Command.AddParameter("@UN", HttpContext.Current.User.Identity.Name, DbType.String);
                        DataTable dt = sp.ExecuteDataSet().Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                User.Id = dr["UId"].ToString();
                                User.Name = dr["Name"].ToString();
                                User.Email = dr["Email"].ToString();
                                User.UserName = dr["UserName"].ToString();
                                User.PhoneNumber = dr["PhoneNumber"].ToString();
                                User.LockoutEnabled = dr["LockoutEnabled"].ToString();

                                User.RoleId = dr["RId"].ToString();
                                User.Role = dr["RoleN"].ToString();

                                User.DistrictId = dr["DId"].ToString();
                                User.District = dr["DistName"].ToString();
                                User.BlockId = dr["BkId"].ToString();
                                User.Block = dr["BlockName"].ToString();
                                User.SchoolId = dr["SId"].ToString();
                                User.SchoolN = dr["school_name"].ToString();

                            }
                        }
                        HttpContext.Current.Session["CUser"] = User;
                        return CUser;
                    }
                    //if (HttpContext.Current.Session["User"] != null)
                    //{
                    //    return (UserViewModel)HttpContext.Current.Session["User"];
                    //}
                    //else
                    //{
                    //    var u = dbe.AspNetUsers.Single(x => x.UserName == HttpContext.Current.User.Identity.Name);
                    //    var dis = (from l in dbe.Dist_Mast
                    //               join un in dbe.AspNetUsers on l.ID equals un.DistrictId
                    //               where ((u.DistrictId != 0) || u.DistrictId == 0 && un.LockoutEnabled == true)
                    //               select l);

                    //    var role = GetUserRole();
                    //    var forAll = new List<string>() { "All", "Admin" };

                    //    var user = new UserViewModel
                    //    {
                    //        Id = u.Id,
                    //        Name = u.Name,
                    //        Email = u.Email,
                    //        DistrictId = u.DistrictId.Value,
                    //        District = string.Join(", ", dis.Select(x => x.DistName)),
                    //        PhoneNumber = u.PhoneNumber,
                    //        Role = u.AspNetRoles.First()?.Name,
                    //    };
                    //    HttpContext.Current.Session["User"] = user;
                    //    return user;
                    //}
                }
                else
                {
                    RouteCollection routes=new RouteCollection();
                    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                    routes.MapRoute(
                        name: "Default",
                        url: "{controller}/{action}/{id}",
                        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                    );
                    //HttpContext.Current.Response.RedirectToRoute("~/Account/Login", false);

                    //HttpContext.Current.Response.Redirect("~/Account/Login", false);
                    //RewritePath
                    return null;
                }
            }
        }
        protected void Session_Start(Object sender, EventArgs e)
        {

            Session["CUser"] = null;
            if (HttpContext.Current != null)
            {
                //HttpContext.Current.Session.Add("CurrentCompany", "Company 1");
                // HttpContext.Current.Session.Add("Connectionstring", "");
            }
            if (Session["CUser"] == null)
            {
                RouteCollection routes = new RouteCollection();
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            }
        }


    }
}
