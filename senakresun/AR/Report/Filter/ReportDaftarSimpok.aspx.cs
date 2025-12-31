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

public partial class AR_ReportSimpok : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                DateTime now = DateTime.Now;
                txtStart.Text = now.ToString("yyyy-MM-dd");
                txtEnd.Text = now.ToString("yyyy-MM-dd");
                DateKurs.Text = now.ToString("yyyy-MM-dd");
                Txtkurs.Text = "0";
                BindGridEntity();

                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KOPERASI", typeof(string));
                var3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                ddlKoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKoperasi.DataTextField = "Text";
                ddlKoperasi.DataValueField = "value";
                ddlKoperasi.DataBind();
                //ddlKoperasi.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddlcabang.Enabled = true;
                var Koperasi = ddlKoperasi.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CABANG", typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "Text";
                ddlcabang.DataValueField = "value";
                ddlcabang.DataBind();
                rdbtnGender.Focus();
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
    protected void ddltgl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Txtrptperiode.Text = "Periode : "  + DateTime.Parse(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + DateTime.Parse(txtEnd.Text).ToString("dd-MM-yyyy");
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        //Txtrptperiode.Text = "Periode : " + Convert.ToDateTime(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + Convert.ToDateTime(txtEnd.Text).ToString("dd-MM-yyyy");
    }
    protected void ddlcurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == "IDR")
        {
            Txtkurs.Text = "1";
            Txtkurs.Enabled = false;
        }
        else
        {
            //Txtkurs.Text = "1";

            Txtkurs.Enabled = true;

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            Params[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlCurrency.SelectedValue, typeof(string));
            Params[1] = gb.CreateSPParameter("@TglKurs", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateKurs.Text, typeof(DateTime));
            Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("GL_GetKurs", Params, "constrmf");
            DataTable dt = new DataTable();
            dt = ge.ListDataset.Tables[0].DefaultView.ToTable();


            if (dt.Rows.Count > 0)
            {
                Txtkurs.Text = dt.Rows[0]["kursstd"].ToString();
            }
            else
            {
                Txtkurs.Text = "1";
            }
        }

    }
    protected void ddlKoperasi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlcabang.Enabled = true;
            var Koperasi = ddlKoperasi.SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CABANG", typeof(string));
            var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
            var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabang.DataTextField = "Text";
            ddlcabang.DataValueField = "value";
            ddlcabang.DataBind();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcabang.SelectedValue == "-")
            {
                ddlstatus.Enabled = false;
                ddlstatus.SelectedValue = "-";
            }
            else
            {
                ddlstatus.Enabled = true;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] Paramt = new DefaultClass.Global.SqlHelper[1];
        Paramt[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCurrencyReport", Paramt, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlCurrency, "None", "");
        ddlCurrency.Items.Insert(0, new ListItem("-", "-"));

        if (SessSystemKurs == "False")
        {
            ddlCurrency.SelectedValue = "IDR";
            ddlCurrency.Enabled = false;
            Txtkurs.Text = "1";
            Txtkurs.Enabled = false;
            DateKurs.Enabled = false;
        }
        else
        {
            ddlCurrency.SelectedValue = "IDR";
            ddlCurrency.Enabled = true;
            Txtkurs.Text = "1";
            Txtkurs.Enabled = true;
            DateKurs.Enabled = true;
        }

        txtrpttitle.Text = "Laporan Daftar Anggota";
        ddlSP.SelectedValue = SessFiskalBi;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        txtStart.Text = date.ToString("yyyy-MM-dd");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        Txtrptperiode.Text = "Periode : "  + DateTime.Parse(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + DateTime.Parse(txtEnd.Text).ToString("dd-MM-yyyy");
        txtStart.Enabled = true;
        txtEnd.Enabled = true;
        Txtrptperiode.Enabled = false;
        ddlSP.Enabled = true;
        ddlCurrency.SelectedValue = "IDR";
        Txtkurs.Text = "1";
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {

            ddltgl_SelectedIndexChanged(sender, e);

            DateTime tes = DateTime.Parse(txtStart.Text + " 00:00:00");
            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[8];
            Paramsc[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@flag", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlstatus.SelectedValue, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@tglmasuk", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Parse(txtStart.Text + " 00:00:00"), typeof(DateTime));
            Paramsc[3] = gb.CreateSPParameter("@tglkeluar", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Parse(txtEnd.Text + " 23:59:59"), typeof(DateTime));
            Paramsc[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTANGGOTAPOKJIB", typeof(string));
            Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramsc[6] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKoperasi.SelectedValue, typeof(string));
            Paramsc[7] = gb.CreateSPParameter("@partgl", ParameterDirection.Input, SqlDbType.VarChar, 50, rdbtnGender.SelectedItem.Text, typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("View_Cetak_Cabang", Paramsc, "constrmf");
            ge.ListDataTable = gb.GetDataJustOutPut("View_Cetak_Cabang", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Peride Pada Tanggal yang anda tidak ada Daftar Anggota, Silahkan Pilih Tanggal Lain.');", true);
            }
            if (ge.ListDataTable.Rows.Count > 0)
            {
                string ambilcabang = ddlcabang.SelectedItem.Text;
                rprt.Load(Server.MapPath("~/AR/Report/Report/rptDaftarAnggotaPokJib.rpt"));
                rprt.SetDataSource(ge.ListDataTable);
                //rprt.SetParameterValue("usersearch", SessLoginID);
                rprt.SetParameterValue("Jenisperiode", rdbtnGender.SelectedItem.Text);
                rprt.SetParameterValue("Periode", Txtrptperiode.Text);
                rprt.SetParameterValue("koperasi", ddlKoperasi.SelectedItem.Text);
                rprt.SetParameterValue("usersearch", SessLoginID);
                rprt.SetParameterValue("Judul", "Daftar Anggota Dengan Simpanan Pokok & Wajib");
                rprt.SetParameterValue("cabang", ambilcabang);
                rprt.SetParameterValue("xstatus", ddlstatus.SelectedItem.Text);
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
        catch (Exception X)
        {
            if (X.Message!="Thread was being aborted.")
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
    protected void Group1_CheckedChanged(Object sender, EventArgs e)
    {
        try
        {
            if (rdbtnGender.SelectedValue == "tglmasuk" || rdbtnGender.SelectedValue == "tglkeluar")
            {
                pnlFilter.Visible = true;
                Panel1.Visible = true;
                Txtrptperiode.Text = "Periode"+ rdbtnGender.SelectedItem.Text + " : " + DateTime.Parse(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + DateTime.Parse(txtEnd.Text).ToString("dd-MM-yyyy");
                //string a = rdbtnGender.SelectedItem.Text;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
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
            DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
            txtStart.Text = date.ToString("yyyy-MM-dd");
            DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
            xdate = date.AddMonths(1);
            xdate = xdate.AddDays(-1);
            txtEnd.Text = xdate.ToString("yyyy-MM-dd");
            Txtrptperiode.Text = "Periode : "  + DateTime.Parse(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + DateTime.Parse(txtEnd.Text).ToString("dd-MM-yyyy");
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