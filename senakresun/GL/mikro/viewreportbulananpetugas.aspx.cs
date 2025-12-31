using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text.RegularExpressions;
using System.Globalization;
public partial class Report_viewreportbulananpetugas : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessNamaProfil
    {
        get { return Session["SessNamaProfil"].ToString(); }
        set { ViewState["SessNamaProfil"] = value; }
    }
    private string SessAlamatProfil1
    {
        get { return Session["SessAlamatProfil1"].ToString(); }
        set { ViewState["SessAlamatProfil1"] = value; }
    }
    private string SessAlamatProfil2
    {
        get { return Session["SessAlamatProfil2"].ToString(); }
        set { ViewState["SessAlamatProfil2"] = value; }
    }
    private string SessKotaProfil
    {
        get { return Session["SessKotaProfil"].ToString(); }
        set { ViewState["SessKotaProfil"] = value; }
    }

    private string SessNegaraProfil
    {
        get { return Session["SessNegaraProfil"].ToString(); }
        set { ViewState["SessNegaraProfil"] = value; }
    }

    private string SessTeleponProfil
    {
        get { return Session["SessTeleponProfil"].ToString(); }
        set { ViewState["SessTeleponProfil"] = value; }
    }
    private string SessFaxProfil
    {
        get { return Session["SessFaxProfil"].ToString(); }
        set { ViewState["SessFaxProfil"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ambilTahun();
                ambilBulan();
                ddlbulan.Enabled = true;
                ddlTahun.Enabled = true;
                DdlReport.SelectedValue = "2";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    //'int total = 0;
    decimal plafon = 0, nasabah = 0, baki = 0;
    decimal Total = 0, jmlbaru = 0, plafonbaru = 0, jmllama = 0, plafonlama = 0, jmltotal = 0, plafontotal = 0;
    //protected void Grdtagihank_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        Total += 1;
    //        jmlbaru += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
    //        jmltotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "terima"));

    //    }
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        Label lbltotal = (Label)e.Row.FindControl("lblTotal");
    //        lbltotal.Text = Total.ToString();
    //        Label lbljmlbaru = (Label)e.Row.FindControl("lbljmlbaru");
    //        lbljmlbaru.Text = jmlbaru.ToString("#,##0.00");

    //        Label lblterimabaru = (Label)e.Row.FindControl("lblterimabaru");
    //        lblterimabaru.Text = jmltotal.ToString("#,##0.00");


    //    }
    //}
    public void ambilTahun()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 3; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddlTahun.Items.Add((currentYear - i).ToString());
        }
        ddlTahun.SelectedValue = currentYear.ToString();
        if (SessBranchID != "000")
        {
            ddlTahun.Enabled = false;
        }
        else
        {
            ddlTahun.Enabled = true;
        }
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
        if (SessBranchID != "000")
        {
            ddlbulan.Enabled = false;
        }
        else
        {
            ddlbulan.Enabled = true;
        }
    }

    public void lnkCari_Click(object sender, EventArgs e)
    {

        ReportDocument rprt = new ReportDocument();

        try
        {


           

            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
            Paramsc[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTPETUGASBULANAN", typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
            Paramsc[4] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.GL_BAHAN_CABANG", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {




                rprt.Load(Server.MapPath("~/GL/mikro/Report/rptpetugasbulanan.rpt"));
                rprt.SetDataSource(ge.ListDataTable);
                rprt.SetParameterValue("Bulan", ddlbulan.SelectedItem.Text);
                rprt.SetParameterValue("Tahun", ddlTahun.SelectedValue);
                rprt.SetParameterValue("Userprint", SessLoginID);
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                Response.End();


            }
            else

            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Daftar Account Empty');", true);
            }


        }

        catch (Exception X)
        {


            if (X.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            }
            else
            {

                rprt.Close();
                rprt.Dispose();
            }


        }

    }




    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

   

}

