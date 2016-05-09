using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Security.Policy;
using System.Security.Principal;
using System.Configuration;
using System.Globalization;
using log4net;


namespace Quality.WebUI.Controllers
{
    //[HandleError(ExceptionType = typeof(ScopeNotSelectedException), View="ScopeNotSelectedException" )]

    public abstract class BaseController : Controller
    {

        public static readonly log4net.ILog Logger
             = log4net.LogManager.GetLogger(
                     System.Reflection.MethodBase.GetCurrentMethod()
                      .DeclaringType);




        public BaseController()
        {
        }
        //public string UserComputername
        //{
        //    get { return GetUsersComputerName(); }




        //}

        public string username { get; set; }
        public bool UserCanApprove
        {
          
             get
            {
            return CanUserApprove();
            }
        }

        public  bool UserCanEdit
        {
            get
            {
                return CanUserEdit();
            }
        }
        public List<string> groups { get; set; }
        public List<string> usergroupmembership { get; set; }
        public string currentloggedinuser { get; set; }
        public void ShowSaveSuccessfull()
        {
            ShowMessage("Save Successful");
        }

        public void ShowDeleteSuccessfull()
        {
            ShowMessage("Delete Successful");
        }

        public void ShowMessage(string message_)
        {
            this.TempData["_shouldShowMessage"] = true;
            this.TempData["_messageToShow"] = message_;
        }

//commented out to see if this is the source of dns slowness.

        //public string GetUsersComputerName()
        //{
        //    try
        //    {
        //        string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
        //        String ecn = System.Environment.MachineName;
        //        string computername = computer_name[0].ToString();



        //        return computername;
        //    }
        //    catch (Exception Ex)
        //    {

        //        GetUserInfo();
        //        string user = username;
        //        Logger.Info(String.Format("Unable to retrieve user machine name for user: {0}. This sometimes occurs when the user is accessing via VPN. Error Message:{1}", user, Ex.Message.ToString()));

        //        return "";
        //    }

        //}




        public bool CanUserEdit()
        {
            bool usercanedit = false;
            GetUserInfo();
            if (usergroupmembership.Contains("IDEAL\\TravelCardAdmin"))
            {
                usercanedit = true;
            }
            if (usergroupmembership.Contains("IDEAL\\TravelCardMaintenance"))
            {
                usercanedit = true;
            }
            if (usergroupmembership.Contains("IDEAL\\TravelCardApprover"))
            {
                usercanedit = true;
            }
           
            return usercanedit;
        }

        public bool CanUserApprove()
        {
            bool usercanapprove = false;
            GetUserInfo();
         
           
            if (usergroupmembership.Contains("IDEAL\\TravelCardApprover"))
            {
                usercanapprove = true;
            }
            if (usergroupmembership.Contains("IDEAL\\TravelCardAdmin"))
            {
                usercanapprove = true;
            }

            return usercanapprove;
        }


        public static CultureInfo ResolveCulture()
        {

            string[] langauges = System.Web.HttpContext.Current.Request.UserLanguages;



            if (langauges == null || langauges.Length == 0)

                return null;



            try
            {

                string language = langauges[0].ToLowerInvariant().Trim();

                return CultureInfo.CreateSpecificCulture(language);

            }

            catch (ArgumentException)
            {

                return null;

            }

        }




        public  void GetUserInfo()
        {
            try
            {
                currentloggedinuser = System.Web.HttpContext.Current.User.Identity.Name;
                string admin = string.Empty;
                string user = string.Empty;
                string approver = string.Empty;
                string maintenance = string.Empty;
                string testuser = string.Empty;


                user = ConfigurationManager.AppSettings["TravelCardUser.AD_GroupName"];
                approver = ConfigurationManager.AppSettings["TravelCardApprover.AD_GroupName"];
                maintenance = ConfigurationManager.AppSettings["TravelCardMaintenance.AD_GroupName"];
                admin = ConfigurationManager.AppSettings["TravelCardAdmin.AD_GroupName"];
                testuser = ConfigurationManager.AppSettings["TestUser"];


                List<string> adgroups = new List<string>();
                adgroups.Add(admin);
                adgroups.Add(approver);
                adgroups.Add(maintenance);
                adgroups.Add(user);
                this.groups = adgroups;


                List<string> groupmembership = new List<string>();

                foreach (var group in groups)
                {

                    if (!String.IsNullOrEmpty(testuser))
                    {
                        this.username = testuser;
                    }
                    else
                    {
                        this.username = currentloggedinuser;
                    }
                    using (var ctx = new PrincipalContext(ContextType.Domain))
                    using (var groupPrincipal = GroupPrincipal.FindByIdentity(ctx, group))

                    using (var userPrincipal = UserPrincipal.FindByIdentity(ctx, username))
                    {
                        if (groupPrincipal != null)
                        {
                            try
                            {
                                if (userPrincipal.IsMemberOf(groupPrincipal))
                                {
                                    groupmembership.Add(group);

                                }

                               
                                groupPrincipal.Dispose();
                                userPrincipal.Dispose();
                            }
                            catch (Exception ex)
                            {
                                string theexception = ex.ToString();
                            }
                        }


                    }

                }

                this.usergroupmembership = groupmembership;

            }
            catch (Exception ex)
            {

                GetUserInfo();
                string user = username;
                Logger.Info(String.Format("Error occured while retrieving group membership. User:{0}.  Error Message:{1}", user, ex.Message.ToString()));
            }



        }
    }
}
