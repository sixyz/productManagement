using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();

        userStore.Context.Database.Connection.ConnectionString =
            System.Configuration.ConfigurationManager.
            ConnectionStrings["GymDBConnectionString"].ConnectionString;

        UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);

        //skapa ny user
        IdentityUser user = new IdentityUser();
        user.UserName = txtUsername.Text;
        if(txtPassword.Text == txtConfirmPassword.Text)
        {
            try
            {
                //Skapa userObject. Databasen expanderas automatiskt
                IdentityResult result = manager.Create(user, txtPassword.Text);

                if(result.Succeeded)
                {

                    Userinfo info = new Userinfo
                    {
                        Address = txtAddress.Text,
                        Firstname = txtFirstname.Text,
                        Lastname = txtLastname.Text,
                        Postalcode = Convert.ToInt32(txtPostalcode.Text),
                        GUID = user.Id
                    };
                    UserInfoModel model = new UserInfoModel();
                    model.InsertUserInfo(info);

                    //lagra användare i db
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    //logga in ny användare med cookie
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    //Logga in användaren och redirect'a till webshopen.
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                    Response.Redirect("~/Index.aspx");
                }
                else
                {
                    litStatus.Text = result.Errors.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                litStatus.Text = ex.ToString();
            }
        }
        else
        {
            litStatus.Text = "Passwords doesnt match";
        }
    }
}