using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Products : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetImages();

            if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                FillPage(id);
            }
        }
    }

    private void FillPage(int id)
    {
        //Hämta vald produkt från databasen
        ProductModel productModel = new ProductModel();
        Product product = productModel.GetProduct(id);

        txtText.Text = product.Description;
        txtName.Text = product.Name;
        txtPrice.Text = product.Price.ToString();

        ddlImage.SelectedValue = product.Image;
        ddlProductType.SelectedValue = product.TypeId.ToString();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductModel productModel = new ProductModel();
        Product product = CreateProduct();

        //Kolla om url innehåller en id parameter
        if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            //om id existerar så uppdatera rad
            int id = Convert.ToInt32(Request.QueryString["id"]);
            lblResult.Text = productModel.UpdateProduct(id, product);
        }
        else
        {
            //om id inte finns, skapa ny rad
            lblResult.Text = productModel.InsertProduct(product);
        }
    }

    private void GetImages()
    {
        try
        {
            //Hämta alla filepaths
            string[] images = Directory.GetFiles(Server.MapPath("~/Images/Products/"));

            //Lägg till alla filepaths till en ArrayList
            ArrayList imageList = new ArrayList();
            foreach (string image in images)
            {
                string imageName = image.Substring(image.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                imageList.Add(imageName);
            }

            //sätt arrayList som Datasource
            ddlImage.DataSource = imageList;
            ddlImage.AppendDataBoundItems = true;
            ddlImage.DataBind();
        }
        catch (Exception e)
        {
            lblResult.Text = e.ToString();
        }
    }


    private Product CreateProduct()
    {
        Product product = new Product();

        product.Name = txtName.Text;
        product.Price = Convert.ToInt32(txtPrice.Text);
        product.TypeId = Convert.ToInt32(ddlProductType.SelectedValue);
        product.Description = txtText.Text;
        product.Image = ddlImage.SelectedValue;

        return product;

    }

}