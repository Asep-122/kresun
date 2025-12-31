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

public partial class SD_Report_Statement : System.Web.UI.Page
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
                //txtStart.Text = now.ToString("yyyy-MM-dd");
                //txtEnd.Text = now.ToString("yyyy-MM-dd");
                DateTime date = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
                txtStart.Text = date.ToString("yyyy-MM-dd");
                DateTime xdate = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
                xdate = date.AddMonths(1);
                xdate = xdate.AddDays(-1);
                txtEnd.Text = xdate.ToString("yyyy-MM-dd");
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

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODESUPPLIER", typeof(string));
        var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        var2[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_REPORT", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddllanggananawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddllanggananakhir, "None", "");
        ddllanggananawal.Items.Insert(0, new ListItem("-", "-"));
        ddllanggananakhir.Items.Insert(0, new ListItem("-", "-"));

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODECRC", typeof(string));
        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_REPORT", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcrc, "None", "");
        ddlcrc.Items.Insert(0, new ListItem("-", "-"));

        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        //DateTime date = new DateTime(now.Year, Int32.Parse(txtStart.Text), 1);
        //txtStart.Text = date.ToString("yyyy-MM-dd");
        //DateTime xdate = new DateTime(now.Year, Int32.Parse(txtStart.Text), 1);
        //xdate = date.AddMonths(1);
        //xdate = xdate.AddDays(-1);
        //txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        //ddlwilayahawal.SelectedValue = SessKdpersh;
        //ddlwilayahakhir.SelectedValue = SessKdpersh;
        ddllanggananawal.SelectedValue = SessKdpersh;
        ddllanggananakhir.SelectedValue = SessKdpersh;
        ddlcrc.SelectedValue = SessKdpersh;


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
                    DateTime ambilawal = Convert.ToDateTime(txtStart.Text);


                //rprt.SetParameterValue("NamaPerusahaan", SessNamaProfil);
                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    //Paramsc[0] = gb.CreateSPParameter("@KodeWilawal", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlwilayahawal.SelectedValue, typeof(string));
                    //Paramsc[1] = gb.CreateSPParameter("@KodeWilakhir", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlwilayahakhir.SelectedValue, typeof(string));
                    Paramsc[0] = gb.CreateSPParameter("@KodeLgnawal", ParameterDirection.Input, SqlDbType.VarChar, 20, ddllanggananawal.SelectedValue, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@KodeLgnakhir", ParameterDirection.Input, SqlDbType.VarChar, 20, ddllanggananakhir.SelectedValue, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@KodeCrcAwal", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlcrc.SelectedValue, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(String));
                    if (rdbtnType.SelectedValue == "0")
                    {
                    Paramsc[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STTLUNASDBELUM", typeof(string));
                    }
                    else
                    {
                    Paramsc[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STTBELUM", typeof(string));
                    }
                 
                    Paramsc[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
     
                ge.ListDataTable = gb.GetDataJustOutPut("dbo.SD_REPORT", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                {
                    rprt.Load(Server.MapPath("~/SD/Report/CR/CrCStatement/AP.CrStatement.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);
                    //rprt.SetParameterValue("Persh", "GIGASENA");
                    //rprt.SetParameterValue("NamaPrsh", SessNamaProfil);
                        rprt.SetParameterValue("User", SessLoginID);
                        rprt.SetParameterValue("Periode", txtper.Text);
                        rprt.SetParameterValue("Judul", txtrpttitle.Text);
                        rprt.SetParameterValue("Komentar", txtkomen.Text);

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                        Response.End();

                    }
                    else if (ge.ListDataTable.Rows.Count <= 0)

                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Pada Rentang " + "" + "" + Convert.ToString(Convert.ToDateTime(txtStart.Text).ToString("dd/MM/yyyy")) + "" + "" + "  s/d  " + "" + "" + Convert.ToString(Convert.ToDateTime(txtEnd.Text).ToString("dd/MM/yyyy")) + " Yang anda Pilih Tidak ada')", true);
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

    protected void rdbtnType_Click(Object sender, EventArgs e)
    {
            pnlFilter.Visible = true;
            string tanggalstart = DateTime.ParseExact(txtStart.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string tanggalend = DateTime.ParseExact(txtEnd.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtper.Text = "Periode : " + tanggalstart + ' ' + "s/d" + ' ' + tanggalend;
            if(rdbtnType.SelectedValue == "1")
            {
                txtrpttitle.Text = "Report Statement yang belum lunas";
            }
            else
            {

                txtrpttitle.Text = "Report Statement yang lunas dan belum lunas";
            }
           
    }
    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now =DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(txtStart.Text),1 );
        txtStart.Text = date.ToString("yyyy-MM-dd");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(txtStart.Text), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("yyyy-MM-dd");

    }



}