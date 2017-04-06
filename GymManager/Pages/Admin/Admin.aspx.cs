using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grdProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Få vald rad
        GridViewRow row = grdProducts.Rows[e.NewEditIndex];

        //Få id på vald rad
        int rowId = Convert.ToInt32(row.Cells[1].Text);

        //Redirect'a användaren till Products och få med det valda id't.
        Response.Redirect("~/Pages/Admin/Products.aspx?id=" + rowId);
    }
}