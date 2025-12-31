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
using System.Globalization;

public partial class AP_Report_Transaksi_Hutang : System.Web.UI.Page
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

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {


                DateTime now = DateTime.Now;
                txtStartTransaksi.Text = now.ToString("yyyy-MM-dd");
                txtEndTransaksi.Text = now.ToString("yyyy-MM-dd");
                txtStartFaktur.Text = now.ToString("yyyy-MM-dd");
                txtEndFaktur.Text = now.ToString("yyyy-MM-dd");
                BindGridEntity();
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
    public void BindGridEntity()
    {
        //DefaultClass.Global.SqlHelper[] Paramsx1 = new DefaultClass.Global.SqlHelper[1];
        //Paramsx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "SELECTKODELOKASI", typeof(string));
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.FA_report_bulan", Paramsx1, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddllokasiawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddllokasiakhir, "None", "");
        //ddllokasiawal.Items.Insert(0, new ListItem("-", "-"));
        //ddllokasiakhir.Items.Insert(0, new ListItem("-", "-"));

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[1];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEGROUP", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.FA_report_bulan", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlgroupawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlgroupakhir, "None", "");
        ddlgroupawal.Items.Insert(0, new ListItem("-", "-"));
        ddlgroupakhir.Items.Insert(0, new ListItem("-", "-"));

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[1];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEGOLONGAN", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.FA_report_bulan", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlgolonganawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlgolonganakhir, "None", "");
        ddlgolonganawal.Items.Insert(0, new ListItem("-", "-"));
        ddlgolonganakhir.Items.Insert(0, new ListItem("-", "-"));

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[1];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEAKTIVA", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.FA_report_bulan", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaktivaawal, "None", "");
        ddlaktivaawal.Items.Insert(0, new ListItem("-", "-"));
        
        //txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        //ddllokasiawal.SelectedValue = SessKdpersh;
        //ddllokasiakhir.SelectedValue = SessKdpersh;
        ddlgolonganawal.SelectedValue = SessKdpersh;
        ddlgolonganakhir.SelectedValue = SessKdpersh;
        ddlgroupawal.SelectedValue = SessKdpersh;
        ddlgroupakhir.SelectedValue = SessKdpersh;
        ddlaktivaawal.SelectedValue = SessKdpersh;
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Upload berhasil!','Data anda sudah berhasil di proses','fixed','success','1');", true);

                try
                {
                    DateTime ambilawal = Convert.ToDateTime(txtStartTransaksi.Text);


                    ReportDocument rprt = new ReportDocument();

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[9];
                    Paramsc[0] = gb.CreateSPParameter("@KodeGolonganawal", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlgolonganawal.SelectedValue, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@KodeGolonganakhir", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlgolonganakhir.SelectedValue, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@KodeAktivaawal", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlaktivaawal.SelectedValue, typeof(string));
                    //Paramsc[4] = gb.CreateSPParameter("@KodeLokasiawal", ParameterDirection.Input, SqlDbType.VarChar, 6, ddllokasiawal.SelectedValue, typeof(string));
                    //Paramsc[5] = gb.CreateSPParameter("@KodeLokasiakhir", ParameterDirection.Input, SqlDbType.VarChar, 6, ddllokasiakhir.SelectedValue, typeof(string));
                    Paramsc[6] = gb.CreateSPParameter("@TglAssetAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtStartTransaksi.Text, typeof(String));
                    //Paramsc[7] = gb.CreateSPParameter("@TglAssetAkhir", ParameterDirection.Input, SqlDbType.VarChar, 50, txtEnd.Text, typeof(String));
                    Paramsc[8] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTREPORTASSETGOL", typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.FA_report_bulan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/FA/Report/Report/Fa/GL.STD.RPT.ASSETGOLONGAN.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("NamaPerusahaan", SessNamaProfil);
                        rprt.SetParameterValue("User", SessLoginID);
                        //rprt.SetParameterValue("ReportNama", txtrpttitle.Text);
                    
                        Response.ClearContent();
                        Response.ClearHeaders();
                        ExportFormatType formatType = ExportFormatType.NoFormat;

                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                        Response.End();


                    }
                    else if (ge.ListDataTable.Rows.Count <= 0)

                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Pada Rentang " + "" + "" + Convert.ToString(Convert.ToDateTime(txtStartTransaksi.Text).ToString("dd/MM/yyyy")) + "" + "" + "  s/d  " + "" + "" + Convert.ToString(Convert.ToDateTime(txtEndTransaksi.Text).ToString("dd/MM/yyyy")) + " Yang anda Pilih Tidak ada')", true);
                    }




                }

                catch (Exception X)
                {
                    if (X.Message != "Thread was being aborted.")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    }

                }
        }
        catch (Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            }
        }
    }

    protected void txtStart_Onchange(object sender, EventArgs e)
    {
        string a = txtStartTransaksi.Text;
        string tanggalstart = DateTime.ParseExact(txtStartTransaksi.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        string tanggalend = DateTime.ParseExact(txtEndTransaksi.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        txtperiode.Text = "Periode : " + tanggalstart + ' ' + "s/d" + ' ' + tanggalend;
    }
    protected void ddljenis_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;
        //string nowx;
        //nowx = gb.ReadOrderData();
        //DateTime now = DateTime.Parse(nowx);
        //int tahun;
        //tahun = now.Year;
        //DateTime date = new DateTime(now.Year, Int32.Parse(txtStart.Text), 1);
        //txtStart.Text = date.ToString("yyyy-MM-dd");
        //DateTime xdate = new DateTime(now.Year, Int32.Parse(txtStart.Text), 1);
        //xdate = date.AddMonths(1);
        //xdate = xdate.AddDays(-1);
        //txtperiode.Text = "Periode : " + txtStart.Text;
        string tanggalstart = DateTime.ParseExact(txtStartTransaksi.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        string tanggalend = DateTime.ParseExact(txtEndTransaksi.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        txtperiode.Text = "Periode : " + tanggalstart + ' ' + "s/d" + ' ' + tanggalend;
        txtrpttitle.Text = "Report " + ddljenisreport.SelectedItem;
        //txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        pnlFilter.Visible = true;
    }



}