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
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using ClosedXML.Excel;

public partial class AR_Report_ReportUndanganRAT : System.Web.UI.Page
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
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    GlobalEntities dt2 = new GlobalEntities();
    GlobalEntities dt3 = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {


                DateTime now = DateTime.Now;

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_REPORT_KODEMARKETING", var1, "constrmf");

                ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketing.DataTextField = "namaAO";
                ddlmarketing.DataValueField = "NIK";
                ddlmarketing.DataBind();
                ddlmarketing.Items.Insert(0, new ListItem("Pilih Kode AO Terlebih Dahulu", "ALL"));


                DefaultClass.Global.SqlHelper[] Paramstx = new DefaultClass.Global.SqlHelper[2];

                Paramstx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                Paramstx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("RAT_Report", Paramstx, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtnoundangan.Text = ge.ListDataset.Tables[0].Rows[0]["Noundangan"].ToString();
                    txttglundangan.Text = ge.ListDataset.Tables[0].Rows[0]["tanggalacara"].ToString();
                    txtjudulundangan.Text = ge.ListDataset.Tables[0].Rows[0]["Acara"].ToString();
                    txtwaktuacara.Text = ge.ListDataset.Tables[0].Rows[0]["pukul"].ToString();
                }
                else
                {
                    txtnoundangan.Text = "";
                    txttglundangan.Text = "";
                    txtjudulundangan.Text = "";
                    txtwaktuacara.Text = "";
                }


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


    decimal xjlhdebit = 0, xjlhkredit = 0;

    protected void btnSerach_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {

            if (ddlpilihreport.SelectedValue =="A")
            {



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];

                var[0] = gb.CreateSPParameter("@noanggota", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlanggotadari.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@noanggotasd", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlanggotasd.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RATUNDANGAN", typeof(string));
                var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[4] = gb.CreateSPParameter("@kodesales", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlmarketing.SelectedValue, typeof(string));
                
                ge.ListDataTable = gb.GetDataJustOutPut("RAT_Report", var, "constrmf").Tables[0];
                //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", var, "constrmf");

                //rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih.rpt"));

                if (ge.ListDataTable.Rows.Count > 0)
                {

                    if (SessKoperasiID == "I")
                    {

                        rprt.Load(Server.MapPath("~/AR/Report/rptundanganSerambi.rpt"));


                    }
                    else if (SessKoperasiID == "II")
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptundanganSentra.rpt"));
                    }
                    else if (SessKoperasiID == "III")
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptundangansemarak.rpt"));
                    }
                    else if (SessKoperasiID == "IV")
                    {

                        rprt.Load(Server.MapPath("~/AR/Report/rptundangansemerbak.rpt"));
                    }
                    else if (SessKoperasiID == "V")
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptundanganpati.rpt"));
                    }


                    rprt.SetDataSource(ge.ListDataTable);


                    Response.ClearContent();
                    Response.ClearHeaders();

                    Response.ContentType = "application/pdf";
                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    Response.End();
                }



            }
            else
            {



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];

                var[0] = gb.CreateSPParameter("@noanggota", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlanggotadari.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@noanggotasd", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlanggotasd.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RATLABEL", typeof(string));
                var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@kodesales", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlmarketing.SelectedValue, typeof(string));
                ge.ListDataTable = gb.GetDataJustOutPut("RAT_Report", var, "constrmf").Tables[0];
                //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", var, "constrmf");

                //rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih.rpt"));

                if (ge.ListDataTable.Rows.Count > 0)
                {

                   
                        rprt.Load(Server.MapPath("~/AR/Report/rptundanganLabel.rpt"));
                   


                    rprt.SetDataSource(ge.ListDataTable);


                    Response.ClearContent();
                    Response.ClearHeaders();

                    Response.ContentType = "application/pdf";
                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    Response.End();
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






    protected void ddlAO_SelectedIndexChanged(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTANGGOTA", typeof(string));
        var1[2] = gb.CreateSPParameter("@noanggotasd", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlmarketing.SelectedValue, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("RAT_Report", var1, "constrmf");

        ddlanggotadari.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlanggotadari.DataTextField = "noanggota";
        ddlanggotadari.DataValueField = "noanggota";
        ddlanggotadari.DataBind();

        ddlanggotasd.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlanggotasd.DataTextField = "noanggota";
        ddlanggotasd.DataValueField = "noanggota";
        ddlanggotasd.DataBind();




    }
}