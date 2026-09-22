using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class OE_Process_TransaksiPerjalananDinasKembaliBayar : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }

    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }

    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }

    DataTable dtRP = new DataTable("Transaction");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();
    string form = "L4";
    string kodesumber = "OE";
    string KodeKonter = "CL13";
    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();


    //private DataSet _listKaryawan;
    private DataSet GetCachedListKaryawan()
    {
        if (ViewState["ListKaryawan"] == null)
        {
            ViewState["ListKaryawan"] = getListKaryawan();
        }
        return (DataSet)ViewState["ListKaryawan"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (string.IsNullOrEmpty(SessLoginID))
            {
                //Response.Redirect(Request.Url.AbsoluteUri);
                Response.Redirect("~/Login.aspx");
            }

            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            //txtnote.Text = CultureName;
            CultureName = "id-ID";
            if (!Page.IsPostBack)
            {
                //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
                //Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                //Params[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

                //BindGridEntity();
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txtTglL.Text = xtanggal.ToString("dd-MM-yyyy");
                txttglbyr.Text = xtanggal.ToString("dd-MM-yyyy");

                //KODEBANK
                DefaultClass.Global.SqlHelper[] varx5 = new DefaultClass.Global.SqlHelper[4];
                varx5[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx5[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                varx5[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
                varx5[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varx5, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "namabank";
                ddlKdBank.DataValueField = "KodeBank";
                ddlKdBank.DataBind();

                ddlKdBank.SelectedValue = "110191";

                BindGridEntity();

            }
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            //txtnote.Text = CultureInfo.CurrentCulture.TextInfo.CultureName;
            //get tanggal server

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    private void InitializeGridAnggota()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("AnggotaID", typeof(string)); // kolom dummy untuk data anggota

        // Tambahkan satu baris kosong
        DataRow dr = dt.NewRow();
        dr["AnggotaID"] = "";
        dt.Rows.Add(dr);

        ViewState["dtAnggota"] = dt;

        GridViewAnggota.DataSource = dt;
        GridViewAnggota.DataBind();
    }

    // helper: ambil DataTable dari ViewState (jika null buat baru)
    private DataTable GetDtFromViewState()
    {
        var dt = ViewState["dtAnggota"] as DataTable;
        if (dt == null)
        {
            dt = new DataTable();
            dt.Columns.Add("AnggotaID", typeof(string));
            DataRow dr = dt.NewRow();
            dr["AnggotaID"] = "";
            dt.Rows.Add(dr);
            ViewState["dtAnggota"] = dt;
        }
        return dt;
    }

    // helper: simpan semua pilihan ddl ke DataTable di ViewState
    private void SaveGridSelectionsToViewState()
    {
        DataTable dt = GetDtFromViewState();

        // pastikan ukuran dt minimal sebanyak baris grid
        int rowCount = GridViewAnggota.Rows.Count;
        while (dt.Rows.Count < rowCount)
        {
            DataRow newr = dt.NewRow();
            newr["AnggotaID"] = "";
            dt.Rows.Add(newr);
        }

        for (int i = 0; i < GridViewAnggota.Rows.Count; i++)
        {
            GridViewRow gvr = GridViewAnggota.Rows[i];
            DropDownList ddl = gvr.FindControl("ddlAnggota") as DropDownList;
            if (ddl != null)
            {
                string sel = ddl.SelectedValue ?? "";
                dt.Rows[i]["AnggotaID"] = sel;
            }
        }

        ViewState["dtAnggota"] = dt;
    }

    protected void btnAddAnggota_Click(object sender, EventArgs e)
    {
        // Simpan dulu pilihan user agar tidak hilang
        SaveGridSelectionsToViewState();

        DataTable dt = GetDtFromViewState();

        DataRow dr = dt.NewRow();
        dr["AnggotaID"] = "";
        dt.Rows.Add(dr);

        ViewState["dtAnggota"] = dt;
        GridViewAnggota.DataSource = dt;
        GridViewAnggota.DataBind();
    }

    protected void GridViewAnggota_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            // Simpan pilihan sebelum hapus
            SaveGridSelectionsToViewState();

            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = GetDtFromViewState();

            if (dt != null && dt.Rows.Count > 1)
            {
                if (index >= 0 && index < dt.Rows.Count)
                {
                    dt.Rows.RemoveAt(index);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Minimal 1 anggota harus ada.');", true);
            }

            ViewState["dtAnggota"] = dt;
            GridViewAnggota.DataSource = dt;
            GridViewAnggota.DataBind();
        }
    }

    /*
    protected void GridViewAnggota_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlAnggota");

            if (ddl != null)
            {
                DataSet lisKaryawan = getListKaryawan();
                gb.FillDDL(lisKaryawan.Tables[0].DefaultView.ToTable(), ddl, "-", "-");

                // restore selected value jika ada di ViewState
                DataTable dt = GetDtFromViewState();
                if (dt != null && e.Row.RowIndex < dt.Rows.Count)
                {
                    string val = dt.Rows[e.Row.RowIndex]["AnggotaID"].ToString();
                    if (!string.IsNullOrEmpty(val))
                    {
                        ListItem item = ddl.Items.FindByValue(val);
                        if (item != null)
                            ddl.SelectedValue = val; // hanya set jika nilai ada di ddl
                    }
                }
            }
        }
    } */

    //protected void GridViewAnggota_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DropDownList ddl = (DropDownList)e.Row.FindControl("ddlAnggota");

    //        if (ddl != null)
    //        {
    //            // Ambil dari cache, bukan langsung query DB
    //            DataSet listKaryawan = GetCachedListKaryawan();
    //            gb.FillDDL(listKaryawan.Tables[0].DefaultView.ToTable(), ddl, "-", "");

    //            // restore selected value
    //            DataTable dt = GetDtFromViewState();
    //            if (dt != null && e.Row.RowIndex < dt.Rows.Count)
    //            {
    //                string val = dt.Rows[e.Row.RowIndex]["AnggotaID"].ToString();
    //                if (!string.IsNullOrEmpty(val))
    //                {
    //                    ListItem item = ddl.Items.FindByValue(val);
    //                    if (item != null)
    //                        ddl.SelectedValue = val;
    //                }
    //            }
    //        }
    //    }
    //}

    protected void GridViewAnggota_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = e.Row.FindControl("ddlAnggota") as DropDownList;
            if (ddl != null)
            {
                // ambil data karyawan dari cache (agar tidak query berulang)
                DataSet listKaryawan = GetCachedListKaryawan();
                gb.FillDDL(listKaryawan.Tables[0].DefaultView.ToTable(), ddl, "-", "");

                // ambil data anggota dari ViewState
                DataTable dt = ViewState["dtAnggota"] as DataTable;
                if (dt != null && e.Row.RowIndex < dt.Rows.Count)
                {
                    string val = dt.Rows[e.Row.RowIndex]["AnggotaID"].ToString().Trim();
                    if (!string.IsNullOrEmpty(val))
                    {
                        ListItem item = ddl.Items.FindByValue(val);
                        if (item != null)
                            ddl.SelectedValue = val;
                    }
                }
            }
        }
    }

    private List<string> GetSelectedAnggotaValues()
    {
        List<string> selectedValues = new List<string>();

        foreach (GridViewRow row in GridViewAnggota.Rows)
        {
            DropDownList ddl = row.FindControl("ddlAnggota") as DropDownList;
            if (ddl != null)
            {
                string selected = ddl.SelectedValue.ToString().Trim();
                selectedValues.Add(selected);
            }
        }

        return selectedValues;
    }

    private void BindGridAnggota(string value)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("AnggotaID", typeof(string));

        if (!string.IsNullOrEmpty(value))
        {
            string[] anggotaList = value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string anggota in anggotaList)
            {
                DataRow dr = dt.NewRow();
                dr["AnggotaID"] = anggota.Trim();
                dt.Rows.Add(dr);
            }
        }
        else
        {
            DataRow dr = dt.NewRow();
            dr["AnggotaID"] = "";
            dt.Rows.Add(dr);
        }

        ViewState["dtAnggota"] = dt;

        GridViewAnggota.DataSource = dt;
        GridViewAnggota.DataBind();
    }

    private void BindGridAnggotaFromGRData(int rowIndex)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("AnggotaID", typeof(string));

        Label lblAnggota = GRData.Rows[rowIndex].FindControl("lblanggota") as Label;
        if (lblAnggota != null)
        {
            // contoh isi lblanggota: "309.072025.2945;309.092025.7822;309.072025.1986;309.072025.2646"
            string rawValue = lblAnggota.Text.Trim();

            if (!string.IsNullOrEmpty(rawValue))
            {
                string[] anggotaList = rawValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string anggota in anggotaList)
                {
                    DataRow dr = dt.NewRow();
                    dr["AnggotaID"] = anggota.Trim();
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["AnggotaID"] = "";
                dt.Rows.Add(dr);
            }
        }

        ViewState["dtAnggota"] = dt;

        GridViewAnggota.DataSource = dt;
        GridViewAnggota.DataBind();
    }

    private DataSet getListKaryawan()
    {

        var paramsList = new List<DefaultClass.Global.SqlHelper> {
            gb.CreateSPParameter("@kodePerusahaan", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKdpersh, typeof(string)),
            gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 4, SessBranchID, typeof(string)),
            gb.CreateSPParameter("@kodeKoperasi", ParameterDirection.Input, SqlDbType.VarChar, 6, SessKoperasiID, typeof(string))
        };

        //ddlKaryawanByKoperasiCabang

        return gbx.GetDataJustOutPut("dbo.ddlKaryawanByKoperasiCabang", paramsList.ToArray(), "constrmf");
    }


    private void Cetak_MemoPerjalananDinas(string nomorBukti)
    {
        string reportPath = Server.MapPath("~/OE/Report/MemoDinas/CetakMemoPerjalananDinas.rpt");
        if (!System.IO.File.Exists(reportPath))
            throw new FileNotFoundException("Report file not found", reportPath);

        using (ReportDocument rprt = new ReportDocument())
        {
            // === MAIN DATA ===
            var paramMain = new List<DefaultClass.Global.SqlHelper> {
                gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "CETAKMEMOPERJALANANDINAS", typeof(string)),
                gb.CreateSPParameter("@kode_koperasi", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKoperasiID, typeof(string)),
                gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nomorBukti, typeof(string)),
                gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string))
            };

            DataTable mainDataTable = gb.GetDataJustOutPut("OE_Trn_Perjalanan_Dinas", paramMain.ToArray(), "constrmf").Tables[0];
            mainDataTable.TableName = "Main";

            // === LOAD REPORT ===
            rprt.Load(reportPath);

            // === SET DATA SOURCE ===
            rprt.SetDataSource(mainDataTable);

            // Pastikan sinkronisasi database
            rprt.VerifyDatabase();
            rprt.Refresh();

            Response.ClearContent();
            Response.ClearHeaders();
            ReportHelper.ExportPdfInline(rprt, "Cetak_MemoPerjalananDinas", Response);
        }
    }


    public void BindGridEntity()
    {
        var var = new List<DefaultClass.Global.SqlHelper>
        {
            gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string)),
            gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTXBAYAR", typeof(string)),
            gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)),
            gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)),
            gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string)),

        };

        ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Perjalanan_Dinas", var.ToArray(), "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglbyr.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttglbyr.Text = xtanggal.ToString("dd-MM-yyyy");
        }
    }



    protected void ddlteller_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridEntity();
    }

    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txtTglL.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txtTglL.Text = xtanggal.ToString("dd-MM-yyyy");
          
        }
        else
        {
          
        }
    }

    decimal total = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));


            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnedit");
            LinkButton btnPDF = (LinkButton)e.Row.FindControl("BtnPDF");

            if (btnEdit != null)
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnEdit);

            if (btnPDF != null)
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnPDF);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltxttotals");
            lbltotal.Text = string.Format("{0:N2}", total);

        }

    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        //PnlReport.Visible = false;
        loaddata();
        Pnlcustomer.Visible = true;
        lnkAddRP2_Click();
        PnlGrid.Visible = true;
        Pnlfooter.Visible = true;
        //txtnote.Text = CultureInfo.CurrentCulture.TextInfo.CultureName;
        //txtnote.Text = "KasBon Perjalanan Dinas";
        txtnote.Text = "Kembali Perjalanan Dinas";

        DataSet lisKaryawan = GetCachedListKaryawan(); //getListKaryawan();
        gb.FillDDL(lisKaryawan.Tables[0].DefaultView.ToTable(), ddlPIC, "-", "-");
        InitializeGridAnggota();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {

        decimal saldoawal;
        saldoawal = (decimal.Parse(txttransAP.Text) + decimal.Parse(txttambahanbiaya.Text)) - decimal.Parse(txtpengembalian.Text);
        if (saldoawal < 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pengembalian lebih dari Jumlah Kasbon!!');", true);
            txtbiaya.Text = "0";
            txtpengembalian.Text = "0";
        }
        else
        {
            txtbiaya.Text = string.Format("{0:N2}", saldoawal);
        }

    }

    protected void Btnnextjournal_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = false;
        PnlJournal.Visible = false;
        //PnlReport.Visible = false;

        PnlGrid.Visible = true;
        Pnlfooter.Visible = true;
        //PnlReport.Visible = false;

    }

    protected void btnbackheader_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        PnlJournal.Visible = true;

        PnlGrid.Visible = false;
        Pnlfooter.Visible = false;

    }

    protected void btnbackheader2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    private void SetDetailRP()
    {

        DataRow dr = null;

        dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));

        dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));


        dr = dtRP.NewRow();
        dr["nomor"] = 1;
        dr["NamaGL"] = string.Empty;
        dr["Description"] = txtnote.Text;
        dr["Costcenter"] = string.Empty;
        dr["jlhdebit"] = decimal.Zero;
        dr["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRP"] = dtRP;


        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();

    }

    private void loaddata()
    {

        //DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[0];
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetTypeTrn", Paramsx, "constrmf");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypetrn, "None", "");
        //ddltypetrn.Items.Insert(0, new ListItem("-", "-"));

        txtnobukti.Text = string.Empty;
        txtnote.Text = string.Empty;
    }

    private void lnkAddRP2_Click()
    {

        int dgcount = GRRP.Rows.Count;
        if (dgcount > 0)
        {
            dtRP.Columns.Clear();

            dtRP = (DataTable)ViewState["ListDetailRP"];

            dtRP.Rows.Clear();

            for (int i = 0; i < dgcount; i++)
            {
                DataRow dr = dtRP.NewRow();

                dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                dtRP.Rows.Add(dr);
            }

            //DataRow drdnew = dtRP.NewRow();
            //drdnew["nomor"] = dgcount + 1;
            //drdnew["KodeGL"] = string.Empty;
            //drdnew["NamaGL"] = string.Empty;
            //drdnew["Description"] = string.Empty;
            //drdnew["Costcenter"] = string.Empty;
            //drdnew["jlhdebit"] = decimal.Zero;
            //drdnew["jlhkredit"] = decimal.Zero;

            //dtRP.Rows.Add(drdnew);

            ViewState["ListDetailRP"] = dtRP;

            GRRP.DataSource = dtRP;
            GRRP.DataBind();

            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

            cssGridRP();

        }
        else
        {
            SetDetailRP();
        }

    }

    private void cssGridRP()
    {

        // Attribute to show the Plus Minus Button.
        GRRP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRRP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");

    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "SET":

                try
                {

                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    //PnlJournal.Visible = true;
                    loaddata();
                    PnlGrid.Visible = true;
                    Pnlfooter.Visible = true;
                    
                    txtNoteApproval.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoteApprovalPusat")).Text;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txtTglL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txtnote.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    //txtnotekasbon.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoteadvance")).Text;
                    txttransAP.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    txttambahanbiaya.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltambahanbiaya")).Text));
                    txtpengembalian.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPengembalianbiaya")).Text));
                    txtbiaya.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTotalbiaya")).Text));
                    //HDNBUKTI.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobuktikasbon")).Text;
                    txtnotekasbon.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnote")).Text;
                    DataSet lisKaryawan = GetCachedListKaryawan();
                    gb.FillDDL(lisKaryawan.Tables[0].DefaultView.ToTable(), ddlPIC, "-", "-");
                    ddlPIC.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpic")).Text.Trim();
                    txtnote.Enabled = false;
                    txtTglL.Enabled = false;
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    BindGridAnggotaFromGRData(rowIndex);



                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTINVOICE", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.OE_Trn_Kembali_Perjalanan_Dinas", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        ViewState["ListDetailRP"] = ge.ListDataTable;

                        GRRP.DataSource = ge.ListDataTable;
                        GRRP.DataBind();

                        GRRP.UseAccessibleHeader = true;
                        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                        cssGridRP();
                    }
                    else
                    {
                        SetDetailRP();
                    }

                    btnSimpan.Visible = true;

                    //DefaultClass.Global.SqlHelper[] ParamPosting = new DefaultClass.Global.SqlHelper[4];
                    //ParamPosting[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnnourut.Text, typeof(string));
                    //ParamPosting[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                    //ParamPosting[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                    //ParamPosting[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKPOSTING", typeof(String));
                    //ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", ParamPosting, "constrmf");
                    //if (ge.ListDataTable.Rows.Count > 0)
                    //{
                    //    var pubposting = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                    //    if (pubposting == "1")
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah di Posting!');", true);
                    //        btndelete.Visible = false;
                    //    }
                    //}

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

                break;
            case "SETDEL":

                //try
                //{
                //    hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                //    DefaultClass.Global.SqlHelper[] ParamPosting = new DefaultClass.Global.SqlHelper[4];
                //    ParamPosting[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnnourut.Text, typeof(string));
                //    ParamPosting[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                //    ParamPosting[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                //    ParamPosting[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKPOSTING", typeof(String));
                //    ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", ParamPosting, "constrmf");
                //    string pubposting = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                //    if (pubposting == "1")
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting!');", true);
                //        //btnSimpan.Enabled = false;
                //    }
                //    else
                //    {

                //        hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                //        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                //        Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnnourut.Text, typeof(string));
                //        Paramsc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                //        Paramsc[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                //        Paramsc[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_Delete_Transaksi", Paramsc, "constrmf");
                //        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                //        {
                //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                //        }
                //        else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Posting"))
                //        {
                //            GRData.UseAccessibleHeader = true;

                //            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut sudah di posting,izin Pusat!');", true);
                //        }


                //        else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                //        {
                //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
                //        }

                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                //            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                //        }
                //    }
                //}

                //catch (Exception X)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                //}

                break;
            case "PDF":

                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_Antar_Bank", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

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
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    //if (e.CommandName!="PDF")
                    //    {
                    //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    //                    }

                }

            break;

            case "MEMO":

                try
                {
                    string noBukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    Cetak_MemoPerjalananDinas(noBukti);
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

                break;
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {


            string no_bukti = txtNoteApproval.Text.Trim();
            if (string.IsNullOrEmpty(no_bukti))
                throw new Exception("Note Approval tidak boleh kosong!");

            else
            {

                var Params = new List<DefaultClass.Global.SqlHelper> {
                    gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SAVEAPPROVE", typeof(string)),
                    gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string)),
                    gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 500, txtNoteApproval.Text, typeof(string)),
                    gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string)),
                    gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, HdnKodeCabang.Value, typeof(string)),
                    gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string)),
                    gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string))
                };

                ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Perjalanan_Dinas", Params.ToArray(), "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','Approve Pusat Pengajuan Sukses','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }





            }




        }
        catch (Exception X)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + X.Message.ToString() + "`);", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }




    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {


            string no_bukti = txtNoteApproval.Text.Trim();
            if (string.IsNullOrEmpty(no_bukti))
                throw new Exception("Note Approval tidak boleh kosong!");

            else
            {

                var Params = new List<DefaultClass.Global.SqlHelper> {
                    gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SAVEATOLAK", typeof(string)),
                    gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string)),
                    gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 500, txtNoteApproval.Text, typeof(string)),
                    gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string)),
                    gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, HdnKodeCabang.Value, typeof(string)),
                    gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string)),
                    gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string))
                };

                ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Perjalanan_Dinas", Params.ToArray(), "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Reject Done!','Approve Pusat Pengajuan Sukses','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }





            }




        }
        catch (Exception X)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + X.Message.ToString() + "`);", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }




    }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    private void subgridbayar_Click()
    {

        int dgcount = GRRP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        if (dgcount > 0)
        {
            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                jlhgl = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                sumjlhgl = sumjlhgl + jlhgl;
            }
        }
        else
        {
            sumjlhgl = 0;
        }
        string formatted = string.Format("{0:N2}", sumjlhgl);
        txttotal.Text = formatted;

        Label lbltotal = (Label)GRRP.FooterRow.FindControl("lbltxttotalsx");
        lbltotal.Text = string.Format("{0:N2}", sumjlhgl);


        decimal totalall = 0;
        totalall = decimal.Parse(txttransAP.Text);

        if (totalall > sumjlhgl)
        {
            txttambahanbiaya.Text = string.Format("{0:N2}", 0);
            txtpengembalian.Text = string.Format("{0:N2}", totalall - sumjlhgl);
            txttotal.Text = string.Format("{0:N2}", decimal.Parse(txttransAP.Text) + decimal.Parse(txtpengembalian.Text));

        }
        else if (totalall < sumjlhgl)
        {
            txttambahanbiaya.Text = string.Format("{0:N2}", sumjlhgl - totalall);
            txtpengembalian.Text = string.Format("{0:N2}", 0);
            txttotal.Text = string.Format("{0:N2}", decimal.Parse(txttransAP.Text) + decimal.Parse(txttambahanbiaya.Text));

        }
        else if (totalall == sumjlhgl)
        {
            txtpengembalian.Text = string.Format("{0:N2}", 0);
            txttambahanbiaya.Text = string.Format("{0:N2}", 0);
            txttotal.Text = string.Format("{0:N2}", sumjlhgl);
        }
        txtbiaya.Text = string.Format("{0:N2}", sumjlhgl);


    }


    //protected void btnSimpan_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ////invoce yg dibayarkan
    //        //decimal jlhdibayar = decimal.Parse(txtbayar.Text);
    //        //decimal jlhcadangan = decimal.Parse(txtcadangan.Text);

    //        //if (txtnoteApproval.Text == "")
    //        //{
    //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Note Pusat Masih Kosong!');", true);
    //        //}          
    //        //else
    //        //{
    //        btnSimpan.Enabled = false;

    //        var varz = new List<DefaultClass.Global.SqlHelper>
    //        {
    //            gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglbyr.Text, typeof(DateTime)),
    //            gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string)),
    //            gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String)),
    //            gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String)),
    //            gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String))
    //        };

    //        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz.ToArray(), "constrmf");

    //        string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
    //        string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

    //        if (xsf != "0")
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
    //            btnSimpan.Enabled = true;
    //        }
    //        else
    //        {

    //            //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[7];
    //                var Params = new List<DefaultClass.Global.SqlHelper> {
    //                gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SAVEABAYAR", typeof(string)),
    //                gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string)),
    //                gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string)),
    //                gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string)),
    //                gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string)),
    //                gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string)),
    //                gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)),
    //                 gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglbyr.Text, typeof(DateTime)),
    //                gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal)),
    //                 gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKdBank.SelectedValue, typeof(string))


    //            };

    //            //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Insert_Biaya_Acrual_Approve_X", Params.ToArray(), "constrmf");
    //            ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Biaya_Perjalanan_Dinas_Advance", Params.ToArray(), "constrmf");

    //            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //            {
    //                btnSimpan.Visible = false;

    //                //nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Bayar Done!','zzzzzz','bottom-center','success','1');", true);
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //            }

    //        }

    //    }
    //    catch (Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //    }
    //}

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtnobukti.ToString().Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nomor bukti tidak boleh kosong!');", true);
                return;
            }

            List<string> anggotaTerpilih = GetSelectedAnggotaValues();

            string anggotaString = (anggotaTerpilih != null && anggotaTerpilih.Any())
                ? string.Join(";", anggotaTerpilih.Where(x => !string.IsNullOrWhiteSpace(x)))
                : string.Empty;

            string pic = ddlPIC.SelectedValue.ToString().Trim();

            //return;

            decimal datac = 0, datax = 0;
            subgridbayar_Click();
            datac = decimal.Parse(txttotal.Text);
            datax = decimal.Parse(txtbiaya.Text);
            //if (datac != datax)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Total Pembiayaan melebih dari semestinya!');", true);
            //}
            //else
            //{
                dtRP.Columns.Clear();

                dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
                dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
                dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));
                dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));

                dtRP.Rows.Clear();

                decimal Jlhdebit = 0, Jlhkredit = 0;
                int x1 = 0;

                for (int i = 0; i < GRRP.Rows.Count; i++)
                {

                    x1 = x1 + 1;
                    DataRow dr = dtRP.NewRow();

                    dr["nomor"] = x1;
                    dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                    dr["NamaGL"] = "";
                    dr["Description"] = "";
                    dr["Costcenter"] = "";
                    dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                    dr["jlhkredit"] = 0;

                    Jlhdebit = Jlhdebit + decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                    //Jlhkredit = Jlhkredit + decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);

                    dtRP.Rows.Add(dr);

                }

                //selisih = Jlhdebit - jlhkredit;
                if (Jlhdebit == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah  Biaya masih Kosong!');", true);
                }
                else
                {
                    var Params = new List<DefaultClass.Global.SqlHelper> {
                        gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string)),
                        gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string)),
                        gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string)),
                        gb.CreateSPParameter("@namabank", ParameterDirection.Input, SqlDbType.VarChar, 100, ddlKdBank.SelectedItem.Text, typeof(string)),
                        gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, "IDR", typeof(string)),
                        gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnote.Text, typeof(string)),
                        gb.CreateSPParameter("@Notekasbon", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnotekasbon.Text, typeof(string)),
                        gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglbyr.Text, typeof(DateTime)),
                        gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal)),
                        gb.CreateSPParameter("@jumlahpengembalian", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpengembalian.Text, typeof(decimal)),
                        gb.CreateSPParameter("@jumlahtambahan", ParameterDirection.Input, SqlDbType.Decimal, 0, txttambahanbiaya.Text, typeof(decimal)),
                        gb.CreateSPParameter("@jumlahbiaya", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbiaya.Text, typeof(decimal)),
                        gb.CreateSPParameter("@nobuktiadvance", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNBUKTI.Value, typeof(string)),
                        gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string)),
                        gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string)),
                        gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string)),
                        gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string)),
                        gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)),
                        gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string)),
                        gb.CreateSPParameter("@GL_Transaction_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable))





                        //gb.CreateSPParameter("@pic", ParameterDirection.Input, SqlDbType.VarChar, 20, pic, typeof(string)),
                        //gb.CreateSPParameter("@anggota", ParameterDirection.Input, SqlDbType.VarChar, 0, anggotaString, typeof(string))
                    };

                    //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Insert_Pengembalian_Kasbon_Operational", Params.ToArray(), "constrmf");
                    ge.ListDataset = gb.GetDataJustOutPut("OE_KASBON_KEMBALI_SAVE_BAYAR_OK", Params.ToArray(), "constrmf");

                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        //btnSimpan.Visible = false;
                        //btnCancel.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
            //}
      
        
        
        
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("OE_Biaya_DINAS_KEMBALI_DELETE", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                //btndelete.Visible = false;

                //nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    private static int ConvertToInt(string number)
    {
        number = number.Replace(",", "").Replace(".", "");
        return int.Parse(number);
    }

    private static decimal ConvertToDecimal(string number)
    {
        var ns = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
        ns.NumberDecimalSeparator = ".";

        return decimal.Parse(number, ns);
    }

    protected void GRRP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DEL":

                int dgcount = GRRP.Rows.Count;


                ((Label)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRP"];

                DataRow drCurrentRow = null;

                dtRP.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRRP.Rows.Count; i++)
                {

                    if (((Label)GRRP.Rows[i].FindControl("lblNumber")).Text != "")
                    {

                        drCurrentRow = dtRP.NewRow();

                        drCurrentRow["nomor"] = j;
                        drCurrentRow["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["jlhdebit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;

                        dtRP.Rows.Add(drCurrentRow);

                        j++;
                    }
                }

                if (dgcount == 1)
                {
                    DataRow drdnew = dtRP.NewRow();

                    drdnew["nomor"] = 1;
                    drdnew["KodeGL"] = string.Empty;
                    drdnew["NamaGL"] = string.Empty;
                    drdnew["Description"] = txtnote.Text;
                    drdnew["Costcenter"] = string.Empty;
                    drdnew["jlhdebit"] = decimal.Zero;
                    drdnew["jlhkredit"] = decimal.Zero;
                    dtRP.Rows.Add(drdnew);
                }

                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRP();

                break;
        }
    }

    decimal totalx = 0;
    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[2];
            xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            xParams[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            gex.ListDataset = gbx.GetDataJustOutPut("dbo.OE_GetBiaya", xParams, "constrmf");
            gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");

            ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

            totalx += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jlhdebit"));



         




            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {


            Label lbltotal = (Label)e.Row.FindControl("lbltxttotalsx");
            lbltotal.Text = string.Format("{0:N2}", totalx);

            txtbiaya.Text = string.Format("{0:N2}", totalx);

        }
    }

    protected void ddlKodeGL_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Params[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        ges.ListDataset = gbs.GetDataJustOutPut("dbo.OE_GetBiaya", Params, "constrmf");

        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";

        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlnamaGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
        xParams[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count != 0)
        {
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataTextField = "Text";
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataValueField = "Value";
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataBind();

            //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), (DropDownList)grow.FindControl("ddlcostcenter")), "", "");
            ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = true;
        }
        else
        {
            ((DropDownList)grow.FindControl("ddlcostcenter")).Items.Insert(0, new ListItem("-", "-"));
            ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = false;
        }

        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");

        ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue;

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void lnkBT_Click(object sender, EventArgs e)
    {
        int dgcount = GRRP.Rows.Count;

        dtRP.Columns.Clear();

        dtRP = (DataTable)ViewState["ListDetailRP"];

        dtRP.Rows.Clear();

        for (int i = 0; i < dgcount; i++)
        {
            DataRow dr = dtRP.NewRow();
            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
            dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
            dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            dtRP.Rows.Add(dr);
        }

        DataRow drdnew = dtRP.NewRow();
        drdnew["nomor"] = dgcount + 1;
        drdnew["KodeGL"] = string.Empty;
        drdnew["jlhdebit"] = decimal.Zero;

        dtRP.Rows.Add(drdnew);

        ViewState["ListDetailRP"] = dtRP;

        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();
       
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    protected void ddlnamaGL_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Params[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_xSGetCoacode", Params, "constrmf");

        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";

        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlKodeGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        xParams[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");

        if (ge.ListDataset.Tables[0].Rows.Count != 0)
        {
            //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataTextField = "Text";
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataValueField = "Value";
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataBind();
            ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = true;
        }
        else
        {
            ((DropDownList)grow.FindControl("ddlcostcenter")).Items.Insert(0, new ListItem("-", "-"));
            ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = false;
        }

        ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue;

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        // UpdatePanel1.Update();
    }

    protected void XXXTxt012_TextChanged(object sender, EventArgs e)
    {
        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
        subgridbayar_Click();
    }

    private void subgridGL_Click()
    {
        int dgcount = GRRP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        if (dgcount > 0)
        {
            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();
                jlhgl = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                sumjlhgl = sumjlhgl + jlhgl;
            }
        }
        else
        {
            sumjlhgl = 0;
        }
         
    }

    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhdebit;
        jlhdebit = decimal.Parse(((TextBox)grow.FindControl("jlhdebit")).Text);
        if (jlhdebit > 0)
        {

            ((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
            ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
        }
        else
        {

            ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
            ((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
        }
        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void XXXTxt02_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhkredit;
        jlhkredit = decimal.Parse(((TextBox)grow.FindControl("jlhkredit")).Text);
        if (jlhkredit > 0)
        {

            ((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
        }
        else
        {

            ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
        }
        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    public void btnSerach_Click(object sender, EventArgs e)
    {
        var kodelgn = HDNBUKTI.Value;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 15, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIOEPERJALANANDINASMST", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.[OE_Trn_Perjalanan_Dinas]", var, "constrmf"); 
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            DataSet lisKaryawan = GetCachedListKaryawan();
            gb.FillDDL(lisKaryawan.Tables[0].DefaultView.ToTable(), ddlPIC, "-", "-");

            string pic = ge.ListDataset.Tables[0].Rows[0]["pic"].ToString().Trim();

            if (!string.IsNullOrEmpty(pic))
            {
                if (ddlPIC.Items.FindByValue(pic) != null)
                    ddlPIC.SelectedValue = pic;
                else
                    ddlPIC.SelectedIndex = 0;
            }

            string anggota = ge.ListDataset.Tables[0].Rows[0]["anggota"].ToString();
            BindGridAnggota(anggota);

            txtnobukti.Text = kodelgn;
            txtnotekasbon.Text = ge.ListDataset.Tables[0].Rows[0]["NotePengajuan"].ToString();
            txttransAP.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JumlahTrn"].ToString()));
            txtpengembalian.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtbiaya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JumlahTrn"].ToString()));

            //btnSimpan.Visible = true;
            //btnSimpan.Enabled = true;


            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
            Paramsc[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "OPENDATA", typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Paramsc[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[5] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_Kembali_Perjalanan_Dinas", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {

                ViewState["ListDetailRP"] = ge.ListDataTable;

                GRRP.DataSource = ge.ListDataTable;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRP();
            }
            else
            {
                SetDetailRP();
            }

        }
        //else
        //{
        //    txtnotekasbon.Text = "";
        //    txttransAP.Text = "0";
        //    txtpengembalian.Text = "0";
        //    txtbiaya.Text = "0";

        //    btnSimpan.Enabled = false;
        //}

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.OE_getBukti_Perjalanan_Dinas", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "redirect",
            "window.location.href='TransaksiPerjalananDinasKembaliBayar.aspx';",
            true
        );
    }

}