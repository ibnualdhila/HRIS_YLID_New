using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class expired_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string _url = Request.QueryString["url"];
            ////int start = url.LastIndexOf('=');
            ////int length = url.Length - url.LastIndexOf('=');
            //string _pageName = _url.Substring(35, _url.Length - 35);
            //////Response.Write(pageName);
            //lblUrl.Value = _pageName;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }
    }
}