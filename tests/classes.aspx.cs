using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class class_tests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write("<h1>Passing Tests:</h1>");
        ////test password class
        //if (Password.Hash("password", "1234") == "bdc87b9c894da5168059e00ebffb9077")
        //{
        //    Response.Write("<span class='pass'>PASS: Password.hash()</span>");
        //}
        //else
        //{
        //    Response.Write("<span class='fail'>FAIL: Password.hash()</span>");
        //}
        
        ////test user class

        //User u = new User(1);
        ////Username
        //Response.Write(u.GetUsername() == "Username"
        //                   ? "<span class='pass'>PASS: User.getUsername()</span>"
        //                   : "<span class='fail'>FAIL: User.getUsername()</span>");
        ////Privilege
        //Response.Write(u.IsPrivilege(4)
        //                   ? "<span class='pass'>PASS: User.isPrivilege()</span>"
        //                   : "<span class='fail'>FAIL: User.isPrivilege()</span>");

        ////auth class

        ////create user
        //Response.Write(Auth.CreateUser("username", "password", 3)
        //                   ? "<span class='pass'>PASS: Auth.createUser()</span>"
        //                   : "<span class='fail'>FAIL: Auth.createUser()</span>");
        ////Privilege
        //Response.Write(Auth.DeleteUser(3)
        //                   ? "<span class='pass'>PASS: Auth.deleteUser()</span>"
        //                   : "<span class='fail'>FAIL: Auth.deleteUser()</span>");

        ////Location
        //Location l = new Location("South Room", 100, 200);
        //Response.Write(l.GetLocation() == "South Room"
        //                   ? "<span class='pass'>PASS: Location.getLocation()</span>"
        //                   : "<span class='fail'>FAIL: Location.getLocation()</span>");

        //Response.Write(l.GetX() == 100
        //                   ? "<span class='pass'>PASS: Location.getX()</span>"
        //                   : "<span class='fail'>FAIL: Location.getX()</span>");

        //Response.Write(l.GetY() == 200
        //                   ? "<span class='pass'>PASS: Location.getY()</span>"
        //                   : "<span class='fail'>FAIL: Location.getY()</span>");

        ////Container

        //Container c = new Container(15, new Location("South Room", 100, 200), 350.5f, "Canned Beans");

        //Response.Write(c.GetLocation() == "South Room"
        //                   ? "<span class='pass'>PASS: Container.getLocation()</span>"
        //                   : "<span class='fail'>FAIL: Container.getLocation()</span>");

        //Response.Write(c.GetX() == 100
        //                   ? "<span class='pass'>PASS: Container.getX()</span>"
        //                   : "<span class='fail'>FAIL: Container.getX()</span>");

        //Response.Write(c.GetY() == 200
        //                   ? "<span class='pass'>PASS: Container.getY()</span>"
        //                   : "<span class='fail'>FAIL: Container.getY()</span>");

        //Response.Write(c.GetType() == "Canned Beans"
        //                   ? "<span class='pass'>PASS: Container.getType()</span>"
        //                   : "<span class='fail'>FAIL: Container.getType()</span>");


        //Response.Write("<h1>Failing Tests:</h1>");
        //Response.Write("<h2>(On Purpose)</h2>");






        ////test password class
        //Response.Write(Password.Hash("password", "1235") == "bdc87b9c894da5168059e00ebffb9077"
        //                   ? "<span class='pass'>PASS: Password.hash()</span>"
        //                   : "<span class='fail'>FAIL: Password.hash()</span>");

        ////test user class

        //u = new User(1);
        ////Username
        //Response.Write(u.GetUsername() == "Usrname"
        //                   ? "<span class='pass'>PASS: User.getUsername()</span>"
        //                   : "<span class='fail'>FAIL: User.getUsername()</span>");
        ////Privilege
        //Response.Write(u.IsPrivilege(2)
        //                   ? "<span class='pass'>PASS: User.isPrivilege()</span>"
        //                   : "<span class='fail'>FAIL: User.isPrivilege()</span>");


        ////Location
        //l = new Location("South Room", 100, 200);
        //Response.Write(l.GetLocation() == "South oom"
        //                   ? "<span class='pass'>PASS: Location.getLocation()</span>"
        //                   : "<span class='fail'>FAIL: Location.getLocation()</span>");

        //Response.Write(l.GetX() == 150
        //                   ? "<span class='pass'>PASS: Location.getX()</span>"
        //                   : "<span class='fail'>FAIL: Location.getX()</span>");

        //Response.Write(l.GetY() == 250
        //                   ? "<span class='pass'>PASS: Location.getY()</span>"
        //                   : "<span class='fail'>FAIL: Location.getY()</span>");

        ////Container

        //c = new Container(15, new Location("South Room", 100, 200), 350.5f, "Canned Beans");

        //Response.Write(c.GetLocation() == "South Rom"
        //                   ? "<span class='pass'>PASS: Container.getLocation()</span>"
        //                   : "<span class='fail'>FAIL: Container.getLocation()</span>");

        //Response.Write(c.GetX() == 1500
        //                   ? "<span class='pass'>PASS: Container.getX()</span>"
        //                   : "<span class='fail'>FAIL: Container.getX()</span>");

        //Response.Write(c.GetY() == 250
        //                   ? "<span class='pass'>PASS: Container.getY()</span>"
        //                   : "<span class='fail'>FAIL: Container.getY()</span>");

        //Response.Write(c.GetType() == "Canned eans"
        //                   ? "<span class='pass'>PASS: Container.getType()</span>"
        //                   : "<span class='fail'>FAIL: Container.getType()</span>");
    }
}