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
public partial class AS_Report_Form_ApprovalInvoice : System.Web.UI.Page
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
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

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
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            //txtnote.Text = CultureName;
            CultureName = "id-ID";
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
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;


        DefaultClass.Global.SqlHelper[] Paramsx1 = new DefaultClass.Global.SqlHelper[3];
        Paramsx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "SELECTNOBUKTI", typeof(string));
        Paramsx1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        Paramsx1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        //Paramsx1[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlsiklus.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.AS_Report", Paramsx1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlnobuktiawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlnobuktiakhir, "None", "");
        ddlnobuktiawal.Items.Insert(0, new ListItem("-", "-"));
        ddlnobuktiakhir.Items.Insert(0, new ListItem("-", "-"));
    }
    protected void onsiklus_selectedindexchanged(object sender, EventArgs e)
    {
        var STATE = "";
        if (ddlsiklus.SelectedValue == "6")
        {
            STATE = "SELECTNOBUKTI";
        }
        else
        {
            STATE = "SELECTNOBUKTI2";
        }
        DefaultClass.Global.SqlHelper[] Paramsx1 = new DefaultClass.Global.SqlHelper[3];
        Paramsx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, STATE, typeof(string));
        Paramsx1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        Paramsx1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        //Paramsx1[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlsiklus.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.AS_Report", Paramsx1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlnobuktiawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlnobuktiakhir, "None", "");
        ddlnobuktiawal.Items.Insert(0, new ListItem("-", "-"));
        ddlnobuktiakhir.Items.Insert(0, new ListItem("-", "-"));
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Upload berhasil!','Data anda sudah berhasil di proses');", true);

            try
            {
                DateTime ambilawal = Convert.ToDateTime(txtStart.Text);
                var STATE = "";
                if(ddlsiklus.SelectedValue == "6")
                {
                    STATE = "SELECTPENGAJUANPOLIS";
                }
                else
                { 
                    STATE = "SELECTAPPROVALINVOICE";
                }

                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                Paramsc[0] = gb.CreateSPParameter("@tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
                Paramsc[1] = gb.CreateSPParameter("@tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime)); 
                Paramsc[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                Paramsc[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(String));
                Paramsc[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));
                ge.ListDataTable = gb.GetDataJustOutPut("dbo.AS_REPORT", Paramsc, "constrmf").Tables[0];

                if (ge.ListDataTable.Rows.Count > 0)
                {

                    if (ddlsiklus.SelectedValue == "7")


                    {


                        rprt.Load(Server.MapPath("~/AS/Report/CR/ApprovalInvoice/AS.ApprovalInvoice.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("NamaPrsh", "GIGASENA");
                        //rprt.SetParameterValue("Nama", SessNamaProfil);
                        rprt.SetParameterValue("Judul", "Report Tagihan Pembayaran Asuransi");
                        rprt.SetParameterValue("User", SessLoginID);
                        rprt.SetParameterValue("Periode", txtStart.Text + " - " + txtEnd.Text);

                    }
                    else
                    {
                        rprt.Load(Server.MapPath("~/AS/Report/CR/PengajuanPolis/AS.PengajuanPolis.rpt"));
                        rprt.SetParameterValue("Judul", "Report Tagihan Pembayaran Asuransi non Approve");
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("NamaPrsh", "GIGASENA");
                        //rprt.SetParameterValue("Nama", SessNamaProfil);
                        rprt.SetParameterValue("Judul", "Report Data Request Pengajuan Polis");
                        rprt.SetParameterValue("User", SessLoginID);
                        rprt.SetParameterValue("Periode", txtStart.Text + " - " + txtEnd.Text);
                    }

                    Response.ClearContent();
                    Response.ClearHeaders();
                    //ExportFormatType formatType = ExportFormatType.NoFormat;

                    Response.ContentType = "application/pdf";
                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    Response.End();


                }
                else if (ge.ListDataTable.Rows.Count <= 0)

                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "Sweetalert('Data Pada Rentang " + "" + "" + Convert.ToString(Convert.ToDateTime(txtStart.Text).ToString("dd/MM/yyyy")) + "" + "" + "  s/d  " + "" + "" + Convert.ToString(Convert.ToDateTime(txtEnd.Text).ToString("dd/MM/yyyy")) + " Yang anda Pilih Tidak ada')", true);
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


    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(txtStart.Text), 1);
        txtStart.Text = date.ToString("yyyy-MM-dd");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(txtStart.Text), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("yyyy-MM-dd");

    }



}