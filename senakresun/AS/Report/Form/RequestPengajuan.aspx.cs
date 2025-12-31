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
public partial class AS_Report_Form_RequestPengajuan : System.Web.UI.Page
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
        DefaultClass.Global.SqlHelper[] Paramsx1 = new DefaultClass.Global.SqlHelper[3];
        Paramsx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "SELECTNOBUKTI", typeof(string));
        Paramsx1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        Paramsx1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.AS_Report", Paramsx1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlnobuktiawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlnobuktiakhir, "None", "");
        ddlnobuktiawal.Items.Insert(0, new ListItem("-", "-"));
        ddlnobuktiakhir.Items.Insert(0, new ListItem("-", "-"));


        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;  
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



                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
                Paramsc[0] = gb.CreateSPParameter("@kodeawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnobuktiawal.SelectedValue, typeof(string));
                Paramsc[1] = gb.CreateSPParameter("@kodeakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnobuktiakhir.SelectedValue, typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
                Paramsc[3] = gb.CreateSPParameter("@tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime)); 
                Paramsc[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPENGAJUANPOLIS", typeof(string));
                Paramsc[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(String));
                Paramsc[6] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));
                ge.ListDataTable = gb.GetDataJustOutPut("dbo.AS_REPORT", Paramsc, "constrmf").Tables[0];

                if (ge.ListDataTable.Rows.Count > 0)
                {


                    rprt.Load(Server.MapPath("~/AS/Report/CR/PengajuanPolis/AS.PengajuanPolis.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);
                    rprt.SetParameterValue("NamaPrsh", "GIGASENA");
                    //rprt.SetParameterValue("Nama", SessNamaProfil);
                    rprt.SetParameterValue("Judul", "Report Data Request Pengajuan Polis");
                    rprt.SetParameterValue("User", SessLoginID);
                    rprt.SetParameterValue("Periode", txtStart.Text + " - " + txtEnd.Text);
                    //rprt.SetParameterValue("Alamat1", "Jl. Raya Pasar Minggu No.Kav.16, RT.7/RW.9, Pancoran");
                    //rprt.SetParameterValue("Alamat2", "Kec. Pancoran, Kota Jakarta Selatan, Daerah Khusus Ibukota Jakarta");
                    //rprt.SetParameterValue("Alamat3", "Kode Pos : 12780 Telp : 0857-8525-8267");
                    //rprt.SetParameterValue("TglJthTmpAwalReport", txtStart.Text);
                    //rprt.SetParameterValue("TglJthTmpAkhirReport", txtEnd.Text);

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