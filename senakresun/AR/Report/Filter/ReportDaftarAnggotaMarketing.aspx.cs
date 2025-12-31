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

public partial class AR_Report_Filter_ReportDaftarAnggotaMarketing : System.Web.UI.Page
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
    Global gb1 = new Global();
    GlobalEntities ge1 = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
       

                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ANGGOTABYMARKETING", typeof(string));
                var3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketing.DataTextField = "Text";
                ddlmarketing.DataValueField = "value";
                ddlmarketing.DataBind();
                //ddlmarketing.Items.Insert(0, new ListItem("ALL","-"));
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
      

    public void btnCetakAnggota_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();

        try
        {
            Button button = (Button)sender;
            //string buttonId = button.ID;
            if (button.ID.ToString() == "btnCetakAnggota")
            {
                // DateTime tes = DateTime.Parse(txtStart.Text + " 00:00:00");
                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                //Paramsc[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                //Paramsc[1] = gb.CreateSPParameter("@flag", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlstatus.SelectedValue, typeof(string));
                //Paramsc[2] = gb.CreateSPParameter("@tglmasuk", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Parse(txtStart.Text + " 00:00:00"), typeof(DateTime));
                Paramsc[0] = gb.CreateSPParameter("@KodeAO", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlmarketing.SelectedValue, typeof(string));
                Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTMARKETINGANGGOTA", typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //Paramsc[6] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKoperasi.SelectedValue, typeof(string));
                //Paramsc[7] = gb.CreateSPParameter("@partgl", ParameterDirection.Input, SqlDbType.VarChar, 50, rdbtnGender.Text, typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("View_Cetak_Cabang", Paramsc, "constrmf");
                ge.ListDataTable = gb.GetDataJustOutPut("View_Cetak_Cabang", Paramsc, "constrmf").Tables[0];

                if (ge.ListDataTable.Rows.Count > 0)
                {
                    //string ambilcabang = ddlcabang.SelectedItem.Text;
                    rprt.Load(Server.MapPath("~/AR/Report/Report/rptDaftarAnggotaMarketing.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);                   
                    rprt.SetParameterValue("usersearch", SessLoginID);
                    rprt.SetParameterValue("Judul", "Laporan Daftar Anggota Marketing");
                   Response.ClearContent();
                    Response.ClearHeaders();

                    ExportFormatType formatType = ExportFormatType.NoFormat;
                    switch (DdlReport.SelectedItem.Value)
                    {
                        case "1":
                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            break;
                        case "2":
                            formatType = ExportFormatType.WordForWindows;
                            rprt.ExportToHttpResponse(formatType, Response, true, "DaftarAnggotadoword");
                            break;
                        case "3":
                            formatType = ExportFormatType.PortableDocFormat;
                            rprt.ExportToHttpResponse(formatType, Response, true, "DaftarAnggotapdf");
                            break;
                        case "4":
                            formatType = ExportFormatType.Excel;
                            rprt.ExportToHttpResponse(formatType, Response, true, "DaftarAnggotaexcel");
                            break;
                        case "5":
                            formatType = ExportFormatType.CharacterSeparatedValues;
                            rprt.ExportToHttpResponse(formatType, Response, true, "DaftarAnggotacsv");
                            break;
                    }
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Daftar Account Empty');", true);
                }
            }
            else
            {
                Global.SqlHelper[] Paramsc = new Global.SqlHelper[4];
                Paramsc[0] = gb1.CreateSPParameter("@KodeAO", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlmarketing.SelectedValue, typeof(string));
                Paramsc[1] = gb1.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTMARKETINGANGGOTAPENJAMIN", typeof(string));
                Paramsc[2] = gb1.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Paramsc[3] = gb1.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                ge1.ListDataTable = gb1.GetDataJustOutPut("View_Cetak_Cabang", Paramsc, "constrmf").Tables[0];

                 if (ge1.ListDataTable.Rows.Count > 0)
                {
                    rprt.Load(Server.MapPath("~/AR/Report/Report/rptDftrAnggotaPenjamin.rpt"));
                    rprt.SetDataSource(ge1.ListDataTable);
                    rprt.SetParameterValue("usersearch", SessLoginID);
                    rprt.SetParameterValue("Judul", "Laporan Daftar Anggota Marketing Dengan Penjamin");
                    Response.ClearContent();
                    Response.ClearHeaders();

                    ExportFormatType formatType = ExportFormatType.NoFormat;
                    switch (DdlReport.SelectedItem.Value)
                    {
                        case "1":
                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            break;
                        case "2":
                            formatType = ExportFormatType.WordForWindows;
                            rprt.ExportToHttpResponse(formatType, Response, true, "DaftarAnggotasdoword");
                            break;
                        case "3":
                            formatType = ExportFormatType.PortableDocFormat;
                            rprt.ExportToHttpResponse(formatType, Response, true, "DaftarAnggotaspdf");
                            break;
                        case "4":
                            formatType = ExportFormatType.Excel;
                            rprt.ExportToHttpResponse(formatType, Response, true, "DaftarAnggotasexcel");
                            break;
                        case "5":
                            formatType = ExportFormatType.CharacterSeparatedValues;
                            rprt.ExportToHttpResponse(formatType, Response, true, "DaftarAnggotascsv");
                            break;
                    }
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Daftar Account Empty');", true);
                }
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
                GC.Collect();
            }
        }

    }
     
    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string nowx;
            nowx = gb.ReadOrderData();
            DateTime now = DateTime.Parse(nowx);
            int tahun;
            tahun = now.Year;
            //DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
            //txtStart.Text = date.ToString("yyyy-MM-dd");
            //DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
            //xdate = date.AddMonths(1);
            //xdate = xdate.AddDays(-1);
            //txtEnd.Text = xdate.ToString("yyyy-MM-dd");
            //Txtrptperiode.Text = "Periode : " + DateTime.Parse(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + DateTime.Parse(txtEnd.Text).ToString("dd-MM-yyyy");
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        //DateTime now = DateTime.Now;

        //Txtrptperiode.Text = "Periode : "  + date.ToString("dd-MM-yyyy") +  " s/d " + xdate.ToString("dd-MM-yyyy");
    }
}