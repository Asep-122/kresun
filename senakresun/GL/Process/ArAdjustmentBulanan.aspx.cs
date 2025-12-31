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
using System.DirectoryServices.ActiveDirectory;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Web.Services.Description;
using System.Data.SqlClient;
public partial class AE_Transaksi_ArAdjustmentBulanan : System.Web.UI.Page
{

    Global gbsearch = new Global();
    GlobalEntities gesearch = new GlobalEntities();
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Get_uploaded_data()
    {

        // grd_uploaded.DataSource = null;

        string periode;

        periode = DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString();

        DefaultClass.Global.SqlHelper[] par = new DefaultClass.Global.SqlHelper[1];
        par[0] = gb.CreateSPParameter("@Periode", ParameterDirection.Input, SqlDbType.VarChar, 6, periode, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Ar_GetUploadedAdjustment", par, "constrmf");

        // ge.ListDataset.AcceptChanges();
        grd_uploaded.DataSource = ge.ListDataset;
        grd_uploaded.DataBind();

        grd_uploaded.UseAccessibleHeader = true;

        grd_uploaded.HeaderRow.TableSection = TableRowSection.TableHeader;

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

            periode = DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString();

            file1.PostedFile.SaveAs(targetPath);

            XLWorkbook wb = new XLWorkbook(targetPath);

            var worksheet = wb.Worksheet("Sheet1");

            int rows = worksheet.Rows().Count();

            gb.BeginTran("constrmf");

            try
            {
                for (int i = 1; i < rows; i++)
                {

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, worksheet.Cell(i, 1).Value, typeof(string));
                    var[1] = gb.CreateSPParameter("@KodePerusahaan", ParameterDirection.Input, SqlDbType.VarChar, 2, "01", typeof(string));
                    var[2] = gb.CreateSPParameter("@Nominal", ParameterDirection.Input, SqlDbType.Decimal, 100, worksheet.Cell(i, 3).Value, typeof(decimal));
                    var[3] = gb.CreateSPParameter("@Periode", ParameterDirection.Input, SqlDbType.VarChar, 6, periode, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Insert_Adjustment", var, "constrmf");
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