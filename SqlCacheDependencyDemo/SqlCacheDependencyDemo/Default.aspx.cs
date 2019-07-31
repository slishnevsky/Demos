using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlCacheDependency
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isDataFromCache = false;

            grvCategory.DataSource = DbManager.GetCategory(out isDataFromCache);
            grvCategory.DataBind();

            lblMessage.Text = isDataFromCache ? "Cache Data : " : "Data refreshed at ";
            lblMessage.Text += DateTime.Now.ToString();

        }
    }
}