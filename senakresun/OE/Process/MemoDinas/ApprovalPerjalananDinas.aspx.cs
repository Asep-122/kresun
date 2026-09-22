using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DefaultClass;
using EntitiesClass;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OE_Process_MemoDinas_ApprovalPerjalananDinas : System.Web.UI.Page
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

                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");

                BindGridEntity();

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
                    gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, HdnKodecabang.Value, typeof(string)),
                    gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string)),
                    gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string))
                };

                ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Biaya_Perjalanan_Dinas_Advance", Params.ToArray(), "constrmf");

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
                    gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, HdnKodecabang.Value, typeof(string)),
                    gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string)),
                    gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string))               
                };

                ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Biaya_Perjalanan_Dinas_Advance", Params.ToArray(), "constrmf");

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

    public void BindGridEntity()
    {

        var var = new List<DefaultClass.Global.SqlHelper>
        {
            gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTAPPPST", typeof(string)),
            gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)),
            gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string))
        };

        //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Cash_Advance", var, "constrmf");
        ge.ListDataset = gb.GetDataJustOutPut("OE_Trn_Biaya_Perjalanan_Dinas_Advance", var.ToArray(), "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindGridEntity();
    }

    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
                    pnlHeader.Visible=false;

                    blnadd = 1;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;

                    txttgltrn.Enabled = false;
                    txttransAP.Enabled = false;

                    txttransAP.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnote")).Text;
                    txtKeterangan.Enabled = false;
                    HdnKodecabang.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    Pnlfooter4.Visible = true;

                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
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

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

}