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

public partial class SD_Report_Transaksi_Hutang : System.Web.UI.Page
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
                //txtStartTransaksi.Text = now.ToString("yyyy-MM-dd");
                //txtEndTransaksi.Text = now.ToString("yyyy-MM-dd");
                //txtStartFaktur.Text = now.ToString("yyyy-MM-dd");
                //txtEndFaktur.Text = now.ToString("yyyy-MM-dd");
                DateTime date = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
                txtStartTransaksi.Text = date.ToString("yyyy-MM-dd");
                txtStartFaktur.Text = date.ToString("yyyy-MM-dd");
                DateTime xdate = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
                xdate = date.AddMonths(1);
                xdate = xdate.AddDays(-1);
                txtEndTransaksi.Text = xdate.ToString("yyyy-MM-dd");
                txtEndFaktur.Text = xdate.ToString("yyyy-MM-dd");
                ddlsemua();
                BindGridEntity();
                ddlcrc.SelectedValue = "IDR";
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

    public void ddlsemua()
    {
        //DefaultClass.Global.SqlHelper[] Paramsx1 = new DefaultClass.Global.SqlHelper[3];
        //Paramsx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "SELECTKODEWILAYAH", typeof(string));
        //Paramsx1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        //Paramsx1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_REPORT", Paramsx1, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlwilayahawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlwilayahakhir, "None", "");
        //ddlwilayahawal.Items.Insert(0, new ListItem("-", "-"));
        //ddlwilayahakhir.Items.Insert(0, new ListItem("-", "-"));

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODESUPPLIER", typeof(string));
        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_REPORT", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddllanggananawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddllanggananakhir, "None", "");
        ddllanggananawal.Items.Insert(0, new ListItem("-", "-"));
        ddllanggananakhir.Items.Insert(0, new ListItem("-", "-"));

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEPEMBELI", typeof(string));
        var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_REPORT", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlpembeliawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlpembeliakhir, "None", "");
        ddlpembeliawal.Items.Insert(0, new ListItem("-", "-"));
        ddlpembeliakhir.Items.Insert(0, new ListItem("-", "-"));

        if (ddljenisreport.SelectedValue == "All")
        {
            ddlTypeTransaksi.Items.Clear();
            ddlTypeTransaksi.Items.Add(new ListItem("All", "-"));
        }
        //else if(ddljenisreport.SelectedValue == "-")
        //{
        //    ddlTypeTransaksi.Items.Add(new ListItem("All", ""));
        //}
        else
        {
            DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
            var2[0] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisreport.SelectedValue, typeof(string));
            var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTYPETRN", typeof(string));
            var2[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
            var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_REPORT", var2, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            ddlTypeTransaksi.DataValueField = "value";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlTypeTransaksi, "None", "");
        }
        
        //ddlTypeTransaksi.Items.Insert(1, new ListItem("-", "-"));

        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
        var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODECRC", typeof(string));
        var3[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        var3[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_REPORT", var3, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcrc, "None", "");
        ddlcrc.Items.Insert(0, new ListItem("-", "-"));
        ddlcrc.SelectedValue = "IDR";
    }
    public void BindGridEntity()
    {
        //ddlsemua();

        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        //DateTime date = new DateTime(now.Year, Int32.Parse(txtStartFaktur.Text), 1);
        //txtStartFaktur.Text = date.ToString("yyyy-MM-dd"); 
        //DateTime date1 = new DateTime(now.Year, Int32.Parse(txtStartFaktur.Text), 1);
        //txtStartTransaksi.Text = date.ToString("yyyy-MM-dd");
        //DateTime xdate = new DateTime(now.Year, Int32.Parse(txtEndFaktur.Text), 1);
        //DateTime xdate1 = new DateTime(now.Year, Int32.Parse(txtEndTransaksi.Text), 1);
        //xdate = date.AddMonths(1);
        //xdate = xdate.AddDays(-1);
        //txtEndFaktur.Text = xdate.ToString("yyyy-MM-dd");
        //xdate1 = date1.AddMonths(1);
        //xdate1 = xdate1.AddDays(-1);
        //txtEndTransaksi.Text = xdate1.ToString("yyyy-MM-dd");
        //ddlpembeliawal.SelectedValue = SessKdpersh;
        //ddlpembeliakhir.SelectedValue = SessKdpersh;
        //ddlwilayahawal.SelectedValue = SessKdpersh;
        //ddlwilayahakhir.SelectedValue = SessKdpersh;
        //ddllanggananawal.SelectedValue = SessKdpersh;
        //ddllanggananakhir.SelectedValue = SessKdpersh;
        //ddlTypeTransaksi.SelectedValue = SessKdpersh;
        //ddlcrc.SelectedValue = SessKdpersh;
        if (SessSystemKurs == "False")
        {
            ddlcrc.SelectedValue = "IDR";
            ddlcrc.Enabled = false;

        }
        else
        {
            //ddlCrc.SelectedValue = "USD";
            ddlcrc.Enabled = true;
        }
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Upload berhasil!','Data anda sudah berhasil di proses','fixed','success','1');", true);

                try
                {
                    DateTime ambilawal = Convert.ToDateTime(txtStartTransaksi.Text);


                 
                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[11];
                //Paramsc[0] = gb.CreateSPParameter("@KodeWilawal", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlwilayahawal.SelectedValue, typeof(string));
                //Paramsc[1] = gb.CreateSPParameter("@KodeWilakhir", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlwilayahakhir.SelectedValue, typeof(string));
                Paramsc[0] = gb.CreateSPParameter("@KodeLgnawal", ParameterDirection.Input, SqlDbType.VarChar, 20, ddllanggananawal.SelectedValue, typeof(string));
                Paramsc[1] = gb.CreateSPParameter("@KodeLgnakhir", ParameterDirection.Input, SqlDbType.VarChar, 20, ddllanggananakhir.SelectedValue, typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(String));
                Paramsc[3] = gb.CreateSPParameter("@KodeCrcAwal", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlcrc.SelectedValue, typeof(string));
                Paramsc[4] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTypeTransaksi.SelectedValue, typeof(String));
                Paramsc[5] = gb.CreateSPParameter("@TglTransaksiawal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtStartTransaksi.Text, typeof(String));
                Paramsc[6] = gb.CreateSPParameter("@TglTransaksiakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, txtEndTransaksi.Text, typeof(String));
                Paramsc[7] = gb.CreateSPParameter("@TglTrnFakturawal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtStartFaktur.Text, typeof(String));
                Paramsc[8] = gb.CreateSPParameter("@TglTrnFakturakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, txtEndFaktur.Text, typeof(String));
                Paramsc[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRNHUTANG", typeof(string)); 
                Paramsc[10] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                ge.ListDataTable = gb.GetDataJustOutPut("dbo.SD_REPORT", Paramsc, "constrmf").Tables[0];

                if (ge.ListDataTable.Rows.Count > 0)
                    {
                    rprt.Load(Server.MapPath("~/SD/Report/CR/CrETransaksi/SD.CrTransaksi.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);
                    //rprt.SetParameterValue("Persh", "GIGASENA");
                    //rprt.SetParameterValue("NamaPrsh", SessNamaProfil);
                    rprt.SetParameterValue("User", SessLoginID);
                    rprt.SetParameterValue("Periode", txtperiode.Text);
                    rprt.SetParameterValue("Judul", txtrpttitle.Text);

                    Response.ClearContent();
                    Response.ClearHeaders();
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

    protected void txtStart_Onchange(object sender, EventArgs e)
    {
        TampilPanel();
    }

    protected void txtEnd_Onchange(object sender, EventArgs e)
    {
        TampilPanel();
    }

    protected void TampilPanel()
    {
        string tanggalstart = DateTime.ParseExact(txtStartTransaksi.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        string tanggalend = DateTime.ParseExact(txtEndTransaksi.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        txtperiode.Text = "Periode : " + tanggalstart + ' ' + "s/d" + ' ' + tanggalend;
        txtrpttitle.Text = "Report " + ddljenisreport.SelectedItem;
        //txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        pnlFilter.Visible = true;
    }
    protected void ddljenis_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddljenisreport.SelectedValue == "-")
        {
            pnlFilter.Visible = false;
        }
        else 
        {
            ddlsemua();
            TampilPanel();
        }

    }



}