using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MVC_CRUD_Application.Models;

namespace MVC_CRUD_Application.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index()
        {
            ViewBag.searchresult = "";
            ViewBag.updateresult = "";
            StudentData sd = new StudentData();
            sd.rollno = "";
            sd.sname = "";
            sd.fname = "";
            sd.mname = "";
            ViewBag.cancelbutton = "disabled";
            ViewBag.updatebutton = "disabled";
            ViewBag.deletebutton = "disabled";
            ViewBag.savebutton = "disabled";
            ViewBag.searchbutton = "";
            ViewBag.addnewbutton = "";
            return View(sd);
          
        }
        public String mycon = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=MyTestPhaseDB;Integrated Security=True;Pooling=False";
        [HttpPost]
        public ActionResult Index(string rollno, string cbutton, string sname, string mname, string fname)

            
        {
            StudentData sd = new StudentData();
            if (cbutton == "Search")
            {

               
                String myquery = "Select * from studentdata where rollno=" + Convert.ToInt32(rollno);
                SqlConnection con = new SqlConnection(mycon);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = myquery;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.searchresult = "Roll Number Has Been Found";
                    sd.rollno = rollno;
                    sd.sname = ds.Tables[0].Rows[0]["sname"].ToString();
                    sd.fname = ds.Tables[0].Rows[0]["fathername"].ToString();
                    sd.mname = ds.Tables[0].Rows[0]["mothername"].ToString();
                    ViewBag.updateresult = "Note : You can perform delete or update or cancel operation";
                    ViewBag.cancelbutton = "";
                    ViewBag.updatebutton = "";
                    ViewBag.deletebutton = "";
                    ViewBag.savebutton = "disabled";
                    ViewBag.addnewbutton = "disabled";

                }
                else
                {
                    ViewBag.updateresult = "";
                    ViewBag.searchresult = "Roll Number Has Not Found";
                    ViewBag.cancelbutton = "disabled";
                    ViewBag.updatebutton = "disabled";
                    ViewBag.deletebutton = "disabled";
                    ViewBag.savebutton = "disabled";
                    ViewBag.addnewbutton = "";
                }
                con.Close();

              
            }
            else if (cbutton == "Update")
            {
                
                String updatedata = "Update studentdata set sname='" + sname + "', fathername='" + fname + "', mothername='" + mname + "' where rollno=" + Convert.ToInt32(rollno);
                SqlConnection con = new SqlConnection(mycon);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = updatedata;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                sd.rollno = "";
                sd.sname = "";
                sd.fname = "";
                sd.mname = "";
                ViewBag.updateresult = "Data Has Been Updated Successfully with Rollno " + rollno;
                ViewBag.cancelbutton = "disabled";
                ViewBag.updatebutton = "disabled";
                ViewBag.deletebutton = "disabled";
                ViewBag.savebutton = "disabled";
                ViewBag.addnewbutton = "";
                
            }
            else if (cbutton == "Cancel")
            {
                ViewBag.cancelbutton = "disabled";
                ViewBag.updatebutton = "disabled";
                ViewBag.deletebutton = "disabled";
                ViewBag.savebutton = "disabled";
                ViewBag.searchbutton = "";
                ViewBag.addnewbutton = "";
                ViewBag.updateresult = "Add New Form and Search Cancelled";
                
            }
            else if (cbutton == "AddNew")
            {
                
                String query = "insert into studentdata(rollno,sname,fathername,mothername) values('" + rollno + "','" + sname + "','" + fname + "','" + mname + "')";
                SqlConnection con = new SqlConnection(mycon);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                ViewBag.cancelbutton = "disabled";
                ViewBag.updatebutton = "disabled";
                ViewBag.deletebutton = "disabled";
                ViewBag.savebutton = "disabled";
                ViewBag.searchbutton = "";
                ViewBag.addnewbutton = "";
                ViewBag.updateresult = "New Data Has Been saved Successfully";
                
            }
            else if (cbutton == "Save")
            {
              

                String query = "insert into studentdata(rollno,sname,fathername,mothername) values('" + rollno + "','" + sname + "','" + fname + "','" + mname + "')";
                SqlConnection con = new SqlConnection(mycon);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                ViewBag.cancelbutton = "disabled";
                ViewBag.updatebutton = "disabled";
                ViewBag.deletebutton = "disabled";
                ViewBag.savebutton = "disabled";
                ViewBag.searchbutton = "";
                ViewBag.addnewbutton = "";
                ViewBag.updateresult = "New Data Has Been saved Successfully";
                
            }
            else if (cbutton == "Delete")
            {
             
                String updatedata = "delete from studentdata where rollno=" + rollno;
                SqlConnection con = new SqlConnection(mycon);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = updatedata;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                ViewBag.cancelbutton = "disabled";
                ViewBag.updatebutton = "disabled";
                ViewBag.deletebutton = "disabled";
                ViewBag.savebutton = "disabled";
                ViewBag.searchbutton = "";
                ViewBag.addnewbutton = "";
                ViewBag.updateresult = "Data Has Been Deleted Successfully with Rollno " + rollno;
               
            }

            else if (cbutton == "View")
            {
               
            }



           return View(sd);
        }



        public ActionResult populateData() {

            String query = "Select * from studentdata";
            SqlConnection con = new SqlConnection(mycon);

            con.Close();
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con);


            da.Fill(dt);

            return View(dt);
        }
    }
}
