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
using System.Web.Script.Serialization;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Charts;

public partial class AR_View_MutasiCandangan : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

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
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    public string SessNamaProfil
    {
        get { return Session["SessNamaProfil"].ToString(); }
        set { ViewState["SessNamaProfil"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtno_anggota.Enabled = true;
            pnlCategory.Visible = false;
            hdnCabangCode.Value = SessBranchID.ToString();
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());



                //txtStart.Text = xtanggal.ToString("dd-MM-yyyy");
                txtStart.Text = xtanggal.ToString("dd/MM/yyyy");
                txtEnd.Text = xtanggal.ToString("dd/MM/yyyy");
                Txtrptperiode.Text = "Periode : " + txtEnd.Text + " s/d " + txtEnd.Text;

                ddlbulan.SelectedValue = xtanggal.ToString("MM");

                txtkodecabang.Text = SessBranchID + " - " + SessNamaProfil;
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void BindGridEntity()
    {
        try
        {
            string[] splitdata = txtno_anggota.Text.Split('-');
            string no_rekening = splitdata[0];
            string State = "";

            if (ddltype.SelectedValue == "All")
            {
                State = "SELECTALLMUTASI";
            }
            else if (txtno_anggota.Text.Trim() == "" && (ddltype.SelectedValue == "U" || ddltype.SelectedValue == "T"))
            {
                State = "SELECTINOUTMUTASI";
            }
            else if (txtno_anggota.Text.Trim() != "" && ddltype.SelectedValue == "All")
            {
                State = "SELECTALLMUTASIANGOTA";
            }
            else if (txtno_anggota.Text.Trim() != "" && (ddltype.SelectedValue == "U" || ddltype.SelectedValue == "T"))
            {
                State = "SELECTINOUTMUTASIANGOTA";
            }

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@Noanggota", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, State, typeof(string));
            var[3] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddltype.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));
            var[5] = gb.CreateSPParameter("@tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            var[6] = gb.CreateSPParameter("@tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            var[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_View_MutasiCA", var, "constrmf");
            if(ge.ListDataset.Tables.Count == 2 && ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data dengan Type Cadangan Atau Periode yang Anda pilih tidak ada!!');", true);
            }
            else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success") ) //(ge.ListDataset.Tables[0].DefaultView.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data dengan Type Cadangan Atau Periode yang Anda pilih tidak ada!!');", true);
            }
            else
            {
                ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn asc";

                GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GRData.DataBind();

                GRData.UseAccessibleHeader = true;

                GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

                txtmutasidebit.Text = string.Format("{0:N2}", xjlhdebit);
                txtmutasikredit.Text = string.Format("{0:N2}", xjlhkredit);
                txtsaldoakhir.Text = string.Format("{0:N2}", (decimal.Parse(txtmutasidebit.Text) + decimal.Parse(txtmutasikredit.Text)));
                pnlFilter.Visible = true;
                PnlFront.Visible = true;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    decimal xjlhdebit = 0, xjlhkredit = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            xjlhdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "debit"));
            xjlhkredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "kredit"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbldebit = (Label)e.Row.FindControl("lbldebit");
            lbldebit.Text = string.Format("{0:N0}", xjlhdebit);

            Label lblkredit = (Label)e.Row.FindControl("lblkredit");
            lblkredit.Text = string.Format("{0:N0}", xjlhkredit);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string[] splitdata = txtno_anggota.Text.Trim().Split('-');
            string no_rekening = splitdata[0];

            if (txtno_anggota.Text == "" && rdbtnjenis.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Anggota belum di isi');", true);
            }
            else if (rdbtnjenis.SelectedValue == "0" && hdnCabangCode.Value != txtno_anggota.Text.Substring(0, 3))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Anggota yang di input tidak terdapat di cabang Anda');", true);
            }
            else
            {
                BindGridEntity();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void Group1_CheckedChanged(Object sender, EventArgs e)
    {
        try
        {
            if (rdbtnjenis.SelectedValue == "0" )
            {
                pnlFilter.Visible = true;
                txtno_anggota.Enabled = true;
                PnlFront.Visible = false;
            }
            else if (rdbtnjenis.SelectedValue == "1")
            {
                pnlFilter.Visible =true;
                txtno_anggota.Enabled = false;
                txtno_anggota.Text = "";
                hdnZipCode.Value = "";
                PnlFront.Visible = false;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlbulan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(ddlbulan.SelectedValue), 1);
        txtStart.Text = date.ToString("dd-MM-yyyy");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlbulan.SelectedValue), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("dd-MM-yyyy");
        txtStart.Enabled = true;
        txtEnd.Enabled = true;
        ddlbulan.Enabled = true;
        Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");
    }
    protected void txtdate_TextChanged(object sender, EventArgs e)
    {
        if (DateTime.Parse(txtStart.Text) > DateTime.Parse(txtEnd.Text))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Awal tidak boleh lebih dari Tanggal Akhir');", true);

            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
            DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

            txtStart.Text = xtanggal.ToString("dd/MM/yyyy");
            txtStart.Focus();
        }
        else if (DateTime.Parse(txtEnd.Text) < DateTime.Parse(txtEnd.Text))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Akhir tidak boleh kurang dari Tanggal Awal');", true);

            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
            DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

            txtEnd.Text = xtanggal.ToString("dd/MM/yyyy");
            txtEnd.Focus();
        }
        else
        {
            DateTime date = DateTime.Parse(txtStart.Text);
            DateTime xdate = DateTime.Parse(txtEnd.Text);
            Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");
        }
    }
    [WebMethod]
    public static string GetNoNasabah(string InputText, string SessionBranchID)
    {
        List<string[]> listResult = new List<string[]>();

        string no_nasabah = InputText;

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessionBranchID, typeof(String));
        Params[1] = gb.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah.Trim(), typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_GetNoRekeningMutasi", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;
    }
}