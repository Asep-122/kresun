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
public partial class Report_viewfeereport : System.Web.UI.Page
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
    decimal plafon = 0, nasabah = 0, baki = 0;
    decimal Total = 0, jmlbaru = 0, plafonbaru = 0, jmllama = 0, plafonlama=0,jmltotal=0, plafontotal=0;
    protected void Grdtagihank_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Total += 1;
            jmlbaru += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
            jmltotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "terima"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbltotal = (Label)e.Row.FindControl("lblTotal");
            lbltotal.Text = Total.ToString();
            Label lbljmlbaru = (Label)e.Row.FindControl("lbljmlbaru");
            lbljmlbaru.Text = jmlbaru.ToString("#,##0.00");

            Label lblterimabaru = (Label)e.Row.FindControl("lblterimabaru");
            lblterimabaru.Text = jmltotal.ToString("#,##0.00");


        }
    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];

            var[0] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbulan.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTahun.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTREPORTVIEWFEE", typeof(string));
            var[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("AE_FeeReport", var, "constrmf").Tables[0];
            rprt.Load(Server.MapPath("~/AE/fee/report/rptfeecabang_01.rpt"));
            rprt.SetDataSource(ge.ListDataTable);
            rprt.SetParameterValue("NamaPrsh", "Unit Bisnis Mikro");
            rprt.SetParameterValue("Judul", "Report Fee Marketing (BM) Cabang " + ddlcabang.SelectedItem.Text);
            rprt.SetParameterValue("User", SessLoginID);
            rprt.SetParameterValue("Periode", "Periode Data " + DateTime.Now.ToString("dd/MM/yyyy"));

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
                    rprt.ExportToHttpResponse(formatType, Response, true, "Daftarfeecabangword");
                    break;
                case "3":
                    formatType = ExportFormatType.PortableDocFormat;
                    rprt.ExportToHttpResponse(formatType, Response, true, "Daftarfeecabangpdf");
                    break;
                case "4":
                    formatType = ExportFormatType.Excel;
                    rprt.ExportToHttpResponse(formatType, Response, true, "Daftarfeecabangexcel");
                    break;
                case "5":
                    formatType = ExportFormatType.CharacterSeparatedValues;
                    rprt.ExportToHttpResponse(formatType, Response, true, "Daftarfeecabangcsv");
                    break;
            }


            Response.End();
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
    //System.Threading.Thread.Sleep(5000);
    //string query;
            //ReportDocument rprt = new ReportDocument();
            //query = "         SELECT ";
            //query = query + " x.nik, x.nama, x.tanggal_masuk,  ";
            //query = query + "  x.ptkp, x.kode_jabatan, x.kode_cabang, x.group,";
            //query = query + " x.jenis_karyawn, x.flag,x.resign, x.kd_staff, x.bos_id, ";
            //query = query + "  x.gaji_pokok, x.tunjangan_makan, x.tunjangan_transport, x.total_gaji,";
            //query = query + "  j.keterangan,g.nama_group,c.nama_cabang,z.psnfee,";
            //query = query + " case when x.kode_jabatan = 'A2' then(select r.total_storting from tbl_collection_cabang_marketing r where";
            //query = query + "   r.tahun = z.tahun and r.bulan = z.bulan and r.kd_cabang = z.kode_cabang and r.nik = x.nik) else case when x.kode_jabatan = 'A1' then";
            //query = query + "             (select sum(r.total_storting) from tbl_collection_cabang_marketing r inner join tbl_karyawanx s on r.nik = s.nik where r.tahun = z.tahun and r.bulan = z.bulan and r.kd_cabang = z.kode_cabang and s.bos_id = x.nik)";
            //query = query + " else case when x.group in ('C', 'D') then(select r.jumlah_collection from tbl_collection_cabang r where";
            //query = query + "  r.tahun = z.tahun and r.bulan = z.bulan and r.kd_cabang = z.kode_cabang) else case when x.group in ('E') then z.total end";
            //query = query + "  end end end as storting,";
            //query = query + "  case when(select r.bmditahan from tbl_fee_bm_ditahan r where r.tahun = z.tahun and r.bulan = z.bulan and r.kode_cabang = z.kode_cabang and r.nik = x.nik) is null then 0 else (select r.bmditahan from tbl_fee_bm_ditahan r where r.tahun = z.tahun and r.bulan = z.bulan and r.kode_cabang = z.kode_cabang and r.nik = x.nik) end as bmditahan, ";
            //query = query + "  case when(select r.bmditahan from tbl_fee_bm_dibayar r inner join kalender h on h.bulanbl = r.bulan and h.tahunbl = r.tahun where h.tahun = z.tahun and h.bulan = z.bulan and r.kode_cabang = z.kode_cabang and r.nik = x.nik) is null then 0 else  (select r.bmditahan from tbl_fee_bm_dibayar r inner join kalender h on h.bulanbl = r.bulan and h.tahunbl = r.tahun where h.tahun = z.tahun and h.bulan = z.bulan and r.kode_cabang = z.kode_cabang and r.nik = x.nik)  end as bmditahanbl,";
            //query = query + "  case when(select r.bayarbm from tbl_fee_bm_dibayar r inner join kalender h on h.bulanbl = r.bulan and h.tahunbl = r.tahun where h.tahun = z.tahun and h.bulan = z.bulan and r.kode_cabang = z.kode_cabang and r.nik = x.nik) is null then 0 else (select r.bayarbm from tbl_fee_bm_dibayar r inner join kalender h on h.bulanbl = r.bulan and h.tahunbl = r.tahun where h.tahun = z.tahun and h.bulan = z.bulan and r.kode_cabang = z.kode_cabang and r.nik = x.nik) end as bmdibayarbl";
            //query = query + " FROM tbl_karyawan_closing x";
            //query = query + "    inner join tbl_fee_jabatan_bulan z on z.kd_jabatan = x.kode_jabatan";
            //query = query + "    inner join tbl_jabatan j on j.kode_jabatan = x.kode_jabatan";
            //query = query + "    inner join tbl_jabatan_group g on g.kode_group = x.group ";
            //query = query + "    inner join tbl_status_cabang c on trim(c.kode_cabang)= x.kode_cabang ";
            //query = query + " where z.tahun = '" + ddlTahun.SelectedValue + "' and z.bulan = '" + ddlbulan.SelectedValue + "' and z.kode_cabang = trim('" + ddlcabang.SelectedValue + "')";
            //DataTable dt = GetData(query);

            //rprt.Load(Server.MapPath("~/mikro/Report/rptfeecabang.rpt"));

            //rprt.SetDataSource(dt);
            //rprt.VerifyDatabase();
            //rprt.SetParameterValue("NamaPrsh", "Unit Bisnis Mikro");
            //rprt.SetParameterValue("Judul", "Report Fee Marketing (BM) Cabang " + ddlcabang.SelectedItem.Text);
            //rprt.SetParameterValue("User", SessLoginID);
            //rprt.SetParameterValue("Periode", "Periode Data " + DateTime.Now.ToString("dd/MM/yyyy"));

            //Response.ClearContent();
            //Response.ClearHeaders();
            //ExportFormatType formatType = ExportFormatType.NoFormat;
            //switch (DdlReport.SelectedItem.Value)
            //{
            //    case "1":
            //        Response.ContentType = "application/pdf";
            //        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
            //        break;
            //    case "2":
            //        formatType = ExportFormatType.WordForWindows;
            //        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarfeecabangword");
            //        break;
            //    case "3":
            //        formatType = ExportFormatType.PortableDocFormat;
            //        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldopdf");
            //        break;
            //    case "4":
            //        formatType = ExportFormatType.Excel;
            //        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldoexcel");
            //        break;
            //    case "5":
            //        formatType = ExportFormatType.CharacterSeparatedValues;
            //        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldocsv");
            //        break;
            //}


            //Response.End();

    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    #region "Insertdata"
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
    #endregion "Insertdata"
}