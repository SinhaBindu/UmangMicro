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
                    //if (HttpContext.Current.Session["User"] != null)
                    //{
                    //    return (UserViewModel)HttpContext.Current.Session["User"];
                    //}
                   //else
                   // {
                    var u = dbe.AspNetUsers.Single(x => x.UserName == HttpContext.Current.User.Identity.Name);
                    var dis = (from d in dbe.Dist_Mast
                               join un in dbe.AspNetUsers on d.ID equals un.DistrictId
                             //  join b in dbe.Block_Mast on
                              // new { ID = d.ID, BlockId = un.BlockId } equals new { b.DistId_fk, b.ID }
                               where ((u.DistrictId != 0) || u.DistrictId == 0 && un.LockoutEnabled == true)
                               select d);

                    var role =CommonModel.GetUserRole();
                        var forAll = new List<string>() { "All", "Admin" };

                        var user = new UserViewModel
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Email = u.Email,
                            DistrictId = u.DistrictId.Value,
                            District = string.Join(", ", dis.Select(x => x.DistName)),
                            PhoneNumber = u.PhoneNumber,
                            RoleId = u.AspNetRoles.First().Id,
                            Role = u.AspNetRoles.First()?.Name,
                        };
                        //HttpContext.Current.Session["User"] = user;
                        return user;
                    //}
                }
                else
                {
                    HttpContext.Current.RewritePath("~/Account/Login");
                    return  null;
                }
            }
        }
    }
}
