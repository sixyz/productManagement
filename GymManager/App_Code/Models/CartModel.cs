using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartModel
/// </summary>
public class CartModel
{
    public string InsertCart(Cart cart)
    {
        try
        {
            //Add new object to database
            GymDBEntities db = new GymDBEntities();
            db.Cart.Add(cart);
            db.SaveChanges();

            return "Item was successfully inserted";
        }
        catch (Exception e)
        {
            return "Error: " + e;
        }
    }

    public string UpdateCart(int id, Cart cart)
    {
        try
        {
            GymDBEntities db = new GymDBEntities();

            Cart c = db.Cart.Find(id);

            c.DatePurchased = cart.DatePurchased;
            c.ClientID = cart.ClientID;
            c.Amount = cart.Amount;
            c.IsInCart = cart.IsInCart;
            c.ProductID = cart.ProductID;

            db.SaveChanges();
            return cart.DatePurchased + " was successfully updated";
        }
        catch (Exception e)
        {
            return "Error: " + e;
        }
    }

    public string DeleteCart(int id)
    {
        try
        {
            GymDBEntities db = new GymDBEntities();
            Cart cart = db.Cart.Find(id);

            db.Cart.Attach(cart);
            db.Cart.Remove(cart);
            db.SaveChanges();

            return cart.DatePurchased + " was successfully deleted";
        }
        catch (Exception e)
        {
            return "Error: " + e;
        }
    }

    private Product GetProduct(int id)
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

    private List<Product> GetAllProducts()
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


    private List<Product> GetProductsByType(int typeId)
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


    public List<Cart> GetOrdersInCart(string userId)
    {
        GymDBEntities db = new GymDBEntities();
        List<Cart> orders = (from x in db.Cart
                             where x.ClientID == userId
                             && x.IsInCart
                             orderby x.DatePurchased
                             select x).ToList();
        return orders;
    }

    public int GetAmountOfOrders(string userId)
    {
        GymDBEntities db = new GymDBEntities();

        int amount = (from x in db.Cart
                      where x.ClientID == userId
                      && x.IsInCart
                      select x.Amount).Sum();

        return amount;
    }

    public void UpdateQuantity(int id, int quantity)
    {
        GymDBEntities db = new GymDBEntities();
        Cart cart = db.Cart.Find(id);
        cart.Amount = quantity;

        db.SaveChanges();
    }

    public void MarkOrderAsPaid(List<Cart> carts)
    {
        GymDBEntities db = new GymDBEntities();

        if(carts != null)
        {
            foreach(Cart cart in carts)
            {
                Cart oldCart = db.Cart.Find(cart.ID);
                oldCart.DatePurchased = DateTime.Now;
                oldCart.IsInCart = false;
            }
            db.SaveChanges();
        }
    }


}