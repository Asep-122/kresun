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
public partial class Report_viewrewardbayarrepot : System.Web.UI.Page
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
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                ambilBulan();
                ambilTahun();
                InsertCombo();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void InsertCombo()
    {
        try
        {
            // --- Combo Kode Cabang
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeePerkiraanPetugas", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

            ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabang.DataTextField = "nama";
            ddlcabang.DataValueField = "KodeCabang";
            ddlcabang.DataBind();
            ddlcabang.Items.Insert(0, new ListItem("Pilih Cabang", ""));

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void ambilTahun()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 3; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddlTahun.Items.Add((currentYear - i).ToString());
        }
    }

    public void ambilBulan()
    {
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length; i++)
        {
            ddlbulan.Items.Add(new ListItem { Text = months[i].ToString(), Value = (i + 1).ToString() });
        }
    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {
//        try
//        {
//            //System.Threading.Thread.Sleep(5000);
//            string query;
//            ReportDocument rprt = new ReportDocument();
//            query = " SELECT ";
//query = query + " f.kode_cabang, f.nik, f.norekening,f.plafon, f.reward, ";

//            query = query + " f.tgl_insert, f.user_insert,c.nama_cabang,x.nama,j.keterangan ";
//            query = query + " FROM ";

//            query = query + " tbl_reward_sm_bulan_bayar f ";

//            query = query + " inner join tbl_status_cabang c on c.kode_cabang = f.kode_cabang ";

//            query = query + " inner join tbl_karyawanx x on x.nik = f.nik ";

//            query = query + " inner join tbl_jabatan j on j.kode_jabatan = x.kode_jabatan ";
//           if (ddlcabang.SelectedValue=="ALL")
//            {
//                query = query + " where to_char(f.tgl_insert, 'yyyy') = '" + ddlTahun.SelectedValue + "' and cast(to_char(f.tgl_insert, 'mm')AS INTEGER)::varchar = '" + ddlbulan.SelectedValue + "'";
              
//            }
//           else
//            {
//                query = query + " where to_char(f.tgl_insert, 'yyyy') = '" + ddlTahun.SelectedValue + "' and cast(to_char(f.tgl_insert, 'mm')AS INTEGER)::varchar = '" + ddlbulan.SelectedValue + "' and f.kode_cabang = trim('" + ddlcabang.SelectedValue + "')";
//            }
//            rprt.Load(Server.MapPath("~/mikro/Report/rptBayarreward1%.rpt"));
            try
            {

                ReportDocument rprt = new ReportDocument();

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTREPORTREWARDBAYAR", typeof(string));
                var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_FeeReport", var, "constrmf");
                ge.ListDataTable = gb.GetDataJustOutPut("AE_FeeReport", var, "constrmf").Tables[0];

                if (ge.ListDataTable.Rows.Count > 0)
                {
                    rprt.Load(Server.MapPath("~/ae/fee/Report/rptBayarreward1.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);
                    rprt.SetParameterValue("NamaPrsh", "Unit Bisnis Mikro");
                    rprt.SetParameterValue("Judul", "Report Bayar Reward 1% Cabang " + ddlcabang.SelectedItem.Text);
                    rprt.SetParameterValue("User", SessLoginID);
                    rprt.SetParameterValue("Periode", "Bulan: " + ddlbulan.SelectedItem.Text + " Tahun: " + ddlTahun.SelectedValue);
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
                            rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanapasaldoword");
                            break;
                        case "3":
                            formatType = ExportFormatType.PortableDocFormat;
                            rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldopdf");
                            break;
                        case "4":
                            formatType = ExportFormatType.Excel;
                            rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldoexcel");
                            break;
                        case "5":
                            formatType = ExportFormatType.CharacterSeparatedValues;
                            rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldocsv");
                            break;
                    }
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Pada Cabang yang anda Pilih Tidak ada')", true);
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





    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
   


}