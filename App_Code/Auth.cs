/// <summary>
/// Auth class - manages user authentication, creation, deletion
/// </summary>
/// 
using System.Linq;

public static class Auth
{

    /* Authenticate users
    =====================*/

    public static UserData Authenticate(string username, string password)
    {
        using (CCSEntities db = new CCSEntities())
        {

            var pwQuery = (from c in db.Users
                           where c.UserName.Equals(username)
                           select c.Password).FirstOrDefault();

            if (password.Equals(pwQuery))
            {
                var q = (from u in db.Users
                            where u.UserName.Equals(username)
                            select new { u.UserID, u.UserName, u.Admin, u.FirstName, u.LastName }).First();
                

                //Return user id if successful authentication
                return new UserData(q.UserID, q.UserName, q.Admin, q.FirstName + " " + q.LastName);
            }
            else
            {
                return new UserData(-1, "", false, "");  //return -1 if user not found
            }
        }
    }
}

public struct UserData
{
    public string name, username;
    public int id;
    public bool admin;

    public UserData(short UserID, string UserName, bool Admin, string Name)
    {
        this.username = UserName;
        this.id = UserID;
        this.admin = Admin;
        this.name = Name;
    }
};