using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OE_Process_MemoDinas_ApprovalPerjalananDinas : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
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

    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

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

    DataTable dtBayar = new DataTable("TransactionBayar");

    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "M1";
    string kodesumber = "OE";
    //string KodeKonter = "AR06";
    public string nobuktidb;


    //private DataSet _listKaryawan;
    private DataSet GetCachedListKaryawan()
    {
        if (ViewState["ListKaryawan"] == null)
        {
            ViewState["ListKaryawan"] = getListKaryawan();
        }
        return (DataSet)ViewState["ListKaryawan"];
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindGridEntity();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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

            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                //txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");


                //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                ////var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("GetArbiayatypeApproval", var, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "tipebiaya asc";

                //ddltypememo.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddltypememo.DataTextField = "nama";
                //ddltypememo.DataValueField = "tipebiaya";
                //ddltypememo.DataBind();

            }

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + X.Message.ToString() + "`);", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTAPPROVALPERJALANANDINAS", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Perjalanan_Dinas", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try
                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    pnlHeader.Visible = false;
                    blnadd = 1;

                    /*
                    HDNKODECABANG.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                    txtmasamanfaat.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblmasamanfaat")).Text;
                    txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    txtbayar.Enabled = false;
                    txtmasamanfaat.Enabled = false;
                    txtbayar.Enabled = false;
                    btnSimpan.Visible = true;
                    txtnote.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotememo")).Text;
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    ddltypememo.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkode")).Text;
                    txtnote.Enabled = false;
                    ddltypememo.Enabled = false;
                    txtnoteApproval.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotepusat")).Text;
                    */

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    //txtTglL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    //txtnote.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    //txtnotekasbon.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoteadvance")).Text;
                    txttransAP.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txttambahanbiaya.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltambahanbiaya")).Text));
                    //txtpengembalian.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPengembalianbiaya")).Text));
                    //txtbiaya.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTotalbiaya")).Text));
                    //HDNBUKTI.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobuktikasbon")).Text;

                    DataSet lisKaryawan = GetCachedListKaryawan();
                    gb.FillDDL(lisKaryawan.Tables[0].DefaultView.ToTable(), ddlPIC, "-", "-");
                    ddlPIC.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpic")).Text.Trim();

                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    BindGridAnggotaFromGRData(rowIndex);

                    if (statusclosing != "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Memo Internal Sudah diapprove manager cabang!');", true);
                        btndelete.Enabled = false;
                    }

                    Pnlfooter4.Visible = true;

                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + X.Message.ToString() + "`);", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

                break;
            case "SETDEL":


                break;
            case "PDF":

                try
                {
                    ReportDocument rprt = new ReportDocument();
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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_AR", Paramsc, "constrmf").Tables[0];

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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + X.Message.ToString() + "`);", true);
                    }
                }

                break;
            case "SLIP":

                try
                {
                    ReportDocument rprt = new ReportDocument();
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    typetrn = "B";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAR", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashAR.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        if (typetrn == "B")
                        {
                            rprt.SetParameterValue("Reportname", "Terima Cash Bank");
                        }
                        else
                        {
                            rprt.SetParameterValue("Reportname", "Keluar Cash Bank");
                        }

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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + X.Message.ToString() + "`);", true);
                    }
                }

            break;
        }
    }
    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {

            btndelete.Enabled = false;
            //if (txtnoteApproval.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Note Tolakan Masih Kosong!');", true);
            //    btndelete.Enabled = true;
            //}
            //else
            //{

            //    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];

            //    Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            //    Params[1] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            //    Params[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, HDNKODECABANG.Value, typeof(string));
            //    Params[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            //    Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //    Params[5] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 150, txtnoteApproval.Text, typeof(string));
            //    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Biaya_Tolakan_MC_X", Params, "constrmf");
            //    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            //    {
            //        btnSimpan.Visible = false;

            //        nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Tolak Done!','zzzzzz','bottom-center','success','1');", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "`);", true);
            //        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            //    }
            //}
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + X.Message.ToString().Replace("'", "") + "`);", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if ((ddltypememo.SelectedValue == "C1") || (ddltypememo.SelectedValue == "E1"))
        //{
        //    e.Row.Cells[3].Visible = false;
        //    //e.Row.Cells[7].Visible = false;
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pokok"));
            bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bunga"));
            angsuran += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));

            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 0)
            {

            }

            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 1)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            }

            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 2)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalPokok = (Label)e.Row.FindControl("lbltotalPokok");
            lbltotalPokok.Text = string.Format("{0:N2}", pokok);

            Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
            lbltotalBunga.Text = string.Format("{0:N2}", bunga);

            Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
            lbltotalAngsuran.Text = string.Format("{0:N2}", angsuran);

            Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
            lbltotaldenda.Text = string.Format("{0:N2}", denda);

        }
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            //////invoce yg dibayarkan
            ////decimal jlhdibayar = decimal.Parse(txtbayar.Text);
            ////decimal jlhcadangan = decimal.Parse(txtcadangan.Text);

            //btnSimpan.Enabled = false;

            //DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            //varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
            //varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            //varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            //varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            //varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            //ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            //string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            //string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            //if (xsf != "0")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + xnotifikasi + "`);", true);
            //    btnSimpan.Enabled = true;
            //}
            //else
            //{
            //    //txtnoteApproval.Text = "ACC " + SessLoginID;
            //    if (txtnoteApproval.Text == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Note Approval Masih Kosong!');", true);
            //        btnSimpan.Enabled = true;
            //    }
            //    else
            //    {
            //        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];

            //        Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            //        Params[1] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            //        Params[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, HDNKODECABANG.Value, typeof(string));
            //        Params[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            //        Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //        Params[5] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 150, txtnoteApproval.Text, typeof(string));
            //        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Biaya_Approve_MC_X", Params, "constrmf");
            //        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            //        {
            //            btnSimpan.Visible = false;

            //            nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','zzzzzz','bottom-center','success','1');", true);
            //        }

            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "`);", true);
            //            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            //        }
            //    }
            //}
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(`" + X.Message.ToString().Replace("'", "") + "`);", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        //varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        //varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        //varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        //ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        //string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        //if (xstatus == "1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
        //    txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
        //}
    }
}