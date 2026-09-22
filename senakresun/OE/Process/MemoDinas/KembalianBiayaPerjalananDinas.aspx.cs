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
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OE_Process_MemoDinas_KembalianBiayaPerjalananDinas : System.Web.UI.Page
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
    string KodeKonter = "AR06";
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
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");

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
            }

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

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

    protected void GridViewAnggota_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = e.Row.FindControl("ddlAnggota") as DropDownList;
            if (ddl != null)
            {
                // ambil data karyawan dari cache (agar tidak hit db berulang)
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
        HashSet<string> unique = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        List<string> selectedValues = new List<string>();

        foreach (GridViewRow row in GridViewAnggota.Rows)
        {
            DropDownList ddl = row.FindControl("ddlAnggota") as DropDownList;
            if (ddl != null)
            {
                string selected = (ddl.SelectedValue ?? "").Trim();

                if (!string.IsNullOrEmpty(selected) && unique.Add(selected))
                {
                    selectedValues.Add(selected);
                }
            }
        }

        return selectedValues;
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
    private void BindGridAnggotaFromGRData(int rowIndex)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("AnggotaID", typeof(string));

        Label lblAnggota = GRData.Rows[rowIndex].FindControl("lblanggota") as Label;
        if (lblAnggota != null)
        {
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


    public void BindGridEntity()
    {
        var var = new List<DefaultClass.Global.SqlHelper> {
            //gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string)),
            gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTBIAYAPERJALANANDINASKEMBALI", typeof(string)),
            gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)),
            gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)),
            gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string))
        };

        ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Perjalanan_Dinas", var.ToArray(), "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindGridEntity();
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":
                try
                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    pnlHeader.Visible = false;


                    Pnlfooter4.Visible = true;


                    DataSet lisKaryawan = GetCachedListKaryawan();
                    gb.FillDDL(lisKaryawan.Tables[0].DefaultView.ToTable(), ddlPIC, "-", "-");
                    string pic = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpic")).Text.Trim();

                    if (!string.IsNullOrEmpty(pic))
                    {
                        if (ddlPIC.Items.FindByValue(pic) != null)
                            ddlPIC.SelectedValue = pic;
                        else
                            ddlPIC.SelectedIndex = 0;
                    }

                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    BindGridAnggotaFromGRData(rowIndex);

                    PnlFront.Visible = false;

                    pnlAnggota.Enabled = false;
                    btnAddAnggota.Visible = false;
                    ddlPIC.Enabled = false;
                    pnladd.Visible = true;

                    blnadd = 1;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;

                    txttgltrn.Enabled = false;
                    txttransAP.Enabled = false;

                    txttransAP.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnote")).Text;
                    txtKeterangan.Enabled = false;

                    HdnKodeCabang.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;

                    Pnlfooter4.Visible = true;

                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

                break;

            case "SETDEL":


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
                        Response.End();

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

                break;

            case "SLIP":

                try
                {
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
                        Response.End();

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
                break;
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
        //switch (e.CommandName)
        //{
        //    case "DEL":
        //        int dgcount = GRRP.Rows.Count;


        //        ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

        //        dtRP.Columns.Clear();

        //        dtRP = (DataTable)ViewState["ListDetailRTP"];

        //        DataRow drCurrentRow = null;

        //        dtRP.Rows.Clear();

        //        int j = 1;

        //        for (int i = 0; i < GRTP.Rows.Count; i++)
        //        {

        //            if (((Label)GRTP.Rows[i].FindControl("lblNumber")).Text != "")
        //            {


        //                drCurrentRow = dtRP.NewRow();

        //                drCurrentRow["nomor"] = j;
        //                drCurrentRow["TglTrnFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lbltglfaktur")).Text;
        //                drCurrentRow["NoFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
        //                drCurrentRow["TypeTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
        //                drCurrentRow["TglJthTmp"] = ((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text;
        //                drCurrentRow["Keterangan"] = ((TextBox)GRTP.Rows[i].FindControl("lblketJPF")).Text;
        //                drCurrentRow["JumlahTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbljmlhtrn")).Text;
        //                drCurrentRow["KursStd"] = ((TextBox)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text;

        //                dtRP.Rows.Add(drCurrentRow);

        //                j++;
        //            }
        //        }

        //        if (dgcount == 1)
        //        {
        //            DataRow drdnew = dtRP.NewRow();

        //            drdnew["nomor"] = 1;
        //            drdnew["TglTrnFaktur"] = string.Empty;
        //            drdnew["NoFaktur"] = string.Empty;
        //            drdnew["TypeTrn"] = string.Empty;
        //            drdnew["TglJthTmp"] = string.Empty;
        //            drdnew["Keterangan"] = string.Empty;
        //            drdnew["JumlahTrn"] = decimal.Zero;
        //            drdnew["KursStd"] = decimal.Zero;
        //            dtRP.Rows.Add(drdnew);
        //        }


        //        ViewState["ListDetailRTP"] = dtRP;

        //        GRTP.DataSource = dtRP;
        //        GRTP.DataBind();

        //        GRTP.UseAccessibleHeader = true;
        //        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //        cssGridBayar();



        //        break;


        //}
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

            btnSimpan.Enabled = false;

            var varz = new List<DefaultClass.Global.SqlHelper>
            {
                gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglbyr.Text, typeof(DateTime)),
                gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string)),
                gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String)),
                gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String)),
                gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String))
            };

            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz.ToArray(), "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                btnSimpan.Enabled = true;
            }
            else
            {

                //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[7];
                var Params = new List<DefaultClass.Global.SqlHelper> {
                    gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KEMBALIANPERJALANANDINAS", typeof(string)),
                    gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string)),
                    gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string)),
                    gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, HdnKodeCabang.Value.ToString().Trim(), typeof(string)),
                    gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string)),
                    gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string)),
                    gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)),
                    gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglbyr.Text, typeof(DateTime)),
                    gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal)),
                    gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKdBank.SelectedValue, typeof(string))

                };

                //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Insert_Biaya_Acrual_Approve_X", Params.ToArray(), "constrmf");
                ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Perjalanan_Dinas", Params.ToArray(), "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;

                    //nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Bayar Done!','zzzzzz','bottom-center','success','1');", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglbyr.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");

        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

}