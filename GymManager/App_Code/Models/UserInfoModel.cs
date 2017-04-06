using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInfoModel
/// </summary>
public class UserInfoModel
{
	public Userinfo GetUserInformation(string guId)
    {
        GymDBEntities db = new GymDBEntities();
        Userinfo info = (from x in db.Userinfo
                         where x.GUID == guId
                         select x).FirstOrDefault();

        return info;
    }

    public void InsertUserInfo(Userinfo info)
    {
        GymDBEntities db = new GymDBEntities();
        db.Userinfo.Add(info);
        db.SaveChanges();
    }
}