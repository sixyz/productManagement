using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_ProductTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductTypeModel model = new ProductTypeModel();
        ProductTypes pt = createProductType();

        lblResult.Text = model.InsertProductType(pt);
    }

    private ProductTypes createProductType()
    {
        ProductTypes p = new ProductTypes();
        p.Name = txtProductType.Text;

        return p;
    }

}