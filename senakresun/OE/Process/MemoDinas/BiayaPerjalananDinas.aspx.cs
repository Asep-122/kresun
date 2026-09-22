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

public partial class OE_Process_MemoDinas_BiayaPerjalananDinas : System.Web.UI.Page
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

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    int blnadd = 0;

    string form = "L3";
    string kodesumber = "OE";
    string KodeKonter = "CL12";

    //string xkodejournal = "HTGAS01";
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
                //BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttgljthtmp.Text = txttgltrn.Text;

                //get tanggal server


            

               
                BindGridEntity();


            }

            //pwcabang system kurs single default IDR kurs 1
          
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

    /* this can make duplications
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
    } */


    /* clean from duplications version */
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














    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;

        pnlHeader.Visible = false;
        PnlFront.Visible = false;

        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        pnladd.Visible = true;


        txtKeterangan.Text = "Perjalanan Dinas";
        Pnlfooter4.Visible = true;

        DataSet lisKaryawan = GetCachedListKaryawan(); //getListKaryawan();
        gb.FillDDL(lisKaryawan.Tables[0].DefaultView.ToTable(), ddlPIC, "-", "-");
        InitializeGridAnggota();
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            List<string> anggotaTerpilih = GetSelectedAnggotaValues();

            string anggotaString = (anggotaTerpilih != null && anggotaTerpilih.Any())
                ? string.Join(";", anggotaTerpilih.Where(x => !string.IsNullOrWhiteSpace(x)))
                : string.Empty;

            string pic = ddlPIC.SelectedValue.ToString().Trim();

            decimal jumlahAP = decimal.Parse(txttransAP.Text);

            if (jumlahAP <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi Masih Kosong');", true);
            }
            else if (pic =="-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('PIC Masih Kosong');", true);
            }


            else
            {

                DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
                varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
                varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
                varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

                string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

                if (xsf != "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                    btnSimpan.Enabled = true;
                }
                else
                {

                    var Params = new List<DefaultClass.Global.SqlHelper> {
                    gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTPERJALANANDINAS", typeof(string)),
                    gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string)),
                    gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtKeterangan.Text, typeof(string)),
                    gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 500, txtNote.Text, typeof(string)),
                    gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime)),
                    gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal)),
                    gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string)),
                    gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string)),
                    gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string)),
                    gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string)),
                    gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)),
                    gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string)),
                    gb.CreateSPParameter("@pic", ParameterDirection.Input, SqlDbType.VarChar, 20, pic, typeof(string)),
                    gb.CreateSPParameter("@anggota", ParameterDirection.Input, SqlDbType.VarChar, 0, anggotaString, typeof(string))
                };

                    //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Insert_Cash_Advance", Params.ToArray(), "constrmf");
                    //ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Insert_Biaya_Perjalanan_Dinas", Params.ToArray(), "constrmf");
                    ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Perjalanan_Dinas", Params.ToArray(), "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

                    }
                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);

                    }

                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;
                        btnCancel.Visible = false;
                        //nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }

            }


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


  
    public void BindGridEntity()
    {
        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];

        var var = new List<DefaultClass.Global.SqlHelper>
        {
            gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string)),
            gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTX", typeof(string)),
            gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)),
            gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)),
            gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string)),
            //gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string)),
            //gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime))
        };

        //OE_Trn_Biaya_Perjalanan_Dinas_Advance
        //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Cash_Advance", var, "constrmf");
        ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Biaya_Perjalanan_Dinas_Advance", var.ToArray(), "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void ddlteller_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridEntity();
    }
   
    decimal total = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltxttotals");
            lbltotal.Text = string.Format("{0:N2}", total);

        }
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "SET":

                try
                {

                    DataSet lisKaryawan = GetCachedListKaryawan();
                    gb.FillDDL(lisKaryawan.Tables[0].DefaultView.ToTable(), ddlPIC, "-", "-");
                    ddlPIC.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpic")).Text.Trim();

                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    BindGridAnggotaFromGRData(rowIndex);

                    PnlFront.Visible = false;

                    pnladd.Enabled = false;
                    pnladd.Visible = true;

                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;

                  

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;



                    txttransAP.Enabled = false;

                    txttransAP.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtKeterangan.Enabled = false;

                    txtNote.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnote")).Text;

                    //pnltrn.Visible = true;
                    Pnlfooter4.Visible = true;
                    btndelete.Visible = true;
                    btnSimpan.Visible = false;
                    int xdatasatus= int.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text); 

                    if (xdatasatus ==5){
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Perjalanan Dinas Tersebut Sudah dibayar teller!');", true);
                        btndelete.Visible = false;
                    }
                    else
                    {
                        btndelete.Visible = true;

                    }

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

                    int xdatasatus = int.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text);

                    if (xdatasatus != 5)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Perjalanan Dinas Tersebut Belum Dibayar Teller!');", true);
                       
                    }
                    else
                    {
                       

                   

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

                break;
            case "SLIP":

                //try
                //{
                //    string nobukti;
                //    string typetrn;
                //    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                //    typetrn = "K";

                //    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                //    Paramsc[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
                //    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSIKASBON", typeof(string));
                //    Paramsc[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //    Paramsc[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //    Paramsc[4] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                //    Paramsc[5] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                //    //Paramsc[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

                //    if (ge.ListDataTable.Rows.Count > 0)
                //    {
                //        rprt.Load(Server.MapPath("~/CL/Transaksi/Report/CLKwitansiKeluar.rpt"));
                //        rprt.SetDataSource(ge.ListDataTable);

                //        //Response.Buffer = false;
                //        // Clear the response content and headers
                //        Response.ClearContent();
                //        Response.ClearHeaders();
                //        Response.ContentType = "application/pdf";
                //        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                //        Response.End();
                //    }
                //}
                //catch (Exception X)
                //{

                //    if (X.Message != "Thread was being aborted.")
                //    {

                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                //    }
                //    else
                //    {
                //        rprt.Close();
                //        rprt.Dispose();
                //    }
                //}

            break;

        }
    }
    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {
            decimal jumlahAP = decimal.Parse(txttransAP.Text);

            if (jumlahAP <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi Masih Kosong');", true);
            }
          

            else
            {

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[11];
                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[2] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[3] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                Params[6] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[9] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));

                Params[10] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Delete_Cash_Advcance", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);

                }

                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    btnCancel.Visible = false;
                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);

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

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }



    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
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
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
        }
    }


 



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

}