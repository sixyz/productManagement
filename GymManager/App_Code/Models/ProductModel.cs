using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductModel
/// </summary>
public class ProductModel
{
    public string InsertProduct(Product product)
    {
        try
        {
            //Add new object to database
            GymDBEntities db = new GymDBEntities();
            db.Product.Add(product);
            db.SaveChanges();

            return product.Name + " was successfully inserted";
        }
        catch (Exception e)
        {
            return "Error: " + e;   
        }
    }

    public string UpdateProduct(int id, Product product)
    {
        try
        {
            GymDBEntities db = new GymDBEntities();

            Product p = db.Product.Find(id);

            p.Name = product.Name;
            p.Price = product.Price;
            p.TypeId = product.TypeId;
            p.Description = product.Description;
            p.Image = product.Image;

            db.SaveChanges();
            return product.Name + " was successfully updated";
        }
        catch (Exception e)
        {
            return "Error: " + e;
        }
    }

    public string DeleteProduct(int id)
    {
        try
        {
            GymDBEntities db = new GymDBEntities();
            Product product = db.Product.Find(id);

            db.Product.Attach(product);
            db.Product.Remove(product);
            db.SaveChanges();

            return product.Name + " was successfully deleted";
        }
        catch (Exception e)
        {
            return "Error: " + e;
        }
    }

    public Product GetProduct(int id)
    {
        try
        {
            using(GymDBEntities db = new GymDBEntities())
            {
                Product product = db.Product.Find(id);
                return product;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    public List<Product> GetAllProducts()
    {
        try
        {
            using(GymDBEntities db = new GymDBEntities())
            {
                List<Product> products = (from x in db.Product
                                          select x).ToList();

                return products;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }


    public List<Product> GetProductsByType(int typeId)
    {
        try
        {
            using (GymDBEntities db = new GymDBEntities())
            {
                List<Product> products = (from x in db.Product
                                          where x.TypeId == typeId
                                          select x).ToList();

                return products;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }


}