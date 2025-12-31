using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Linq;
using System.Data;
using DefaultClass;
using EntitiesClass;
using System.Globalization;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class AE_Transaksi_UploadPL : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    Global gbsearch = new Global();
    GlobalEntities gesearch = new GlobalEntities();
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
               
                ambilTahun();
                ambilBulan();
                
            }
            //GRData.UseAccessibleHeader = true;

            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void ambilTahun()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 3; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddlTahun.Items.Add((currentYear - i).ToString());
        }
        ddlTahun.SelectedValue = currentYear.ToString();
        //ddlTahun.Enabled = false;
        //if (SessBranchID != "000")
        //{
        //    ddlTahun.Enabled = false;
        //}
        //else
        //{
        //    ddlTahun.Enabled = true;
        //}
    }

    public void ambilBulan()
    {
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length; i++)
        {
            ddlbulan.Items.Add(new ListItem { Text = months[i].ToString(), Value = (i + 1).ToString() });
        }
        var currentmonth = DateTime.Today.Month - 1;
        ddlbulan.SelectedValue = currentmonth.ToString();
        //ddlbulan.Enabled = false;
        //if (SessBranchID != "000")
        //{
        //    ddlbulan.Enabled = false;
        //}Get_uploaded_data
        //else
        //{
        //    ddlbulan.Enabled = true;
        //}
    }

    protected void Get_uploaded_data()
    {

        // grd_uploaded.DataSource = null;

     

        DefaultClass.Global.SqlHelper[] par = new DefaultClass.Global.SqlHelper[2];
        par[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlbulan.SelectedValue, typeof(string));
        par[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlTahun.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("Ae_GetUploadedPL", par, "constrmf");

        // ge.ListDataset.AcceptChanges();
        grd_uploaded.DataSource = ge.ListDataset;
        grd_uploaded.DataBind();

        grd_uploaded.UseAccessibleHeader = true;

        grd_uploaded.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
       
           

            try
            {
                   DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                    var[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlbulan.SelectedValue, typeof(string));
                    var[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlTahun.SelectedValue, typeof(string));
                 
                    ge.ListDataset = gb.GetDataJustOutPut("AE_DELETE_PL", var, "constrmf");
                    if (!ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','1');", true);

                    }
 
            }
            catch (Exception X)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
                Response.Write("<div class='alert alert-danger p-3'>Ada kesalahan.</div>");

                // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }

            // section_upload.Visible=false;

            Get_uploaded_data();

        }
    

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string nama = file1.PostedFile.FileName;

        if (nama == "")
        {
            Response.Write("<div class='alert alert-danger p-3'>File cannot empty</div>");
        }
        else
        {
            string targetFolder = HttpContext.Current.Server.MapPath("/App_Data");
            string targetPath = Path.Combine(targetFolder, nama);
            string periode;

            //periode = DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString();

            file1.PostedFile.SaveAs(targetPath);

            XLWorkbook wb = new XLWorkbook(targetPath);

            var worksheet = wb.Worksheet("Sheet1");

            int rows = worksheet.Rows().Count();

            gb.BeginTran("constrmf");

            try
            {
                for (int i = 1; i < rows; i++)
                {

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, worksheet.Cell(i, 1).Value, typeof(string));
                    var[1] = gb.CreateSPParameter("@KodePerusahaan", ParameterDirection.Input, SqlDbType.VarChar, 2, "01", typeof(string));
                    var[2] = gb.CreateSPParameter("@Nominal", ParameterDirection.Input, SqlDbType.Decimal, 100, worksheet.Cell(i, 3).Value, typeof(decimal));
                    var[3] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlbulan.SelectedValue, typeof(string));
                    var[4] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlTahun.SelectedValue, typeof(string));
                    var[5] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, worksheet.Cell(i, 2).Value, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_Insert_PL", var, "constrmf");
                    if (!ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:alert('Error: " + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.RollBackTran();
                        break;
                        // gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    } 
                }

                Response.Write("<div class='alert alert-success p-3'>File sukses diimport</div>");
            }
            catch (Exception X)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
                Response.Write("<div class='alert alert-danger p-3'>Ada kesalahan.</div>");
               
                // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }

             // section_upload.Visible=false;

            Get_uploaded_data();
           
        }
    }

    protected void btnDelete_click(string kode_cabang)
    {
        var del = kode_cabang;
        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[1];
        var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, del, typeof(string));
        
        ge.ListDataset = gb.GetDataJustOutPut("Ar_DeleteUploadedAdjustment", var, "constrmf");
       
        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        {
            Get_uploaded_data();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
            // gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    protected void grd_uploaded_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "SETDEL":

                try
                {
                    string cabang = ((Label)grd_uploaded.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKode")).Text;

                    DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[1];
                    x[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, cabang, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("Ar_DeleteUploadedAdjustment", x, "constrmf");

                    btnDelete_click(((Label)grd_uploaded.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKode")).Text);

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        
        section_data.Visible = true;

        Get_uploaded_data();
    }

}