using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;
//using DocumentFormat.OpenXml.InkML;
using System.IdentityModel.Protocols.WSTrust;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Asn1.Tsp;
using System.Activities.Statements;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Org.BouncyCastle.Asn1.Ocsp;
using ClosedXML.Excel;


public partial class SD_Master_SumberDanaJangka : System.Web.UI.Page
{
    int counter_nonasabah = 0;
    long nosekarang = 0, nonasabah = 0;
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    Global gb1 = new DefaultClass.Global();
    GlobalEntities ge1 = new GlobalEntities();
    Global gb2 = new DefaultClass.Global();
    GlobalEntities ge2 = new GlobalEntities();
    Global gb3 = new DefaultClass.Global();
    GlobalEntities ge3 = new GlobalEntities();

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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
            if (!Page.IsPostBack)
            {
                string ambilbranch = SessBranchID;
                BindGridEntity();


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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEBANK", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[Sd_BungaKantorBayar]", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";
        ddlKantorBayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKantorBayar.DataTextField = "NamaBank";
        ddlKantorBayar.DataValueField = "KodeBank";
        ddlKantorBayar.DataBind();
        ddlKantorBayar.Items.Insert(0, new ListItem("Pilih Kantor Bayar", "-"));


        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEPROGPINJ", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("[Sd_BungaKantorBayar]", var2, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";
        ddlProgramPinjam.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlProgramPinjam.DataTextField = "keterangan";
        ddlProgramPinjam.DataValueField = "kode";
        ddlProgramPinjam.DataBind();
        ddlProgramPinjam.Items.Insert(0, new ListItem("Pilih Program Pinjam", "-"));


        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
        var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEPRODUKKATEGORI", typeof(string));
        var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var3[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("[Sd_BungaKantorBayar]", var3, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "Kode asc";
        ddlKategori.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKategori.DataTextField = "nama";
        ddlKategori.DataValueField = "Kode";
        ddlKategori.DataBind();
        ddlKategori.Items.Insert(0, new ListItem("Pilih Kategori", "-"));

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("SumberBungaKantorBayar.aspx");
    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            pnlButtonForm.Visible = true;

            if(int.Parse(txtStart.Text) <= int.Parse(txtLast.Value))
            {
                int jangka = int.Parse(txtLast.Value) + 1;
                txtStart.Text = jangka.ToString();
                btnSave.Visible = false;
                btnDelete.Visible = false;
                string pesan = "Jangka Bulan " + int.Parse(txtLast.Value).ToString() + " sudah ada di database mohon periksa kembali";
                pesan = pesan.Replace("'", "\\'"); 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + pesan+ "');", true);
                return;
            }

            if (int.Parse(txtEnd.Text) < int.Parse(txtStart.Text))
            {
                int jangka = int.Parse(txtLast.Value) + 1;
                txtStart.Text = jangka.ToString();
                txtEnd.Text = jangka.ToString();
                btnSave.Visible = false;
                btnDelete.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Range jangka start bulan tidak boleh lebih besar dari range jangka akhir');", true);
                return;
            }

            int start = int.Parse(txtStart.Text);
            int end = int.Parse(txtEnd.Text);
            btnSave.Visible = true;
            btnDelete.Visible = false;
            pnlGenerateTable.Visible = true;
            decimal thn = 0;
            List<ListRepeater> list = new List<ListRepeater>();

            for (int i = start; i <= end; i++)
            {
                thn = (i-1)/12+1;
                list.Add(new ListRepeater
                {
                    JangkaBulan = i,
                    KodeBank = ddlKantorBayar.SelectedValue,
                    NamaBank = ddlKantorBayar.SelectedItem.Text,
                    KodeProgramPinjam = ddlProgramPinjam.SelectedValue,
                    ProgramPinjam = ddlProgramPinjam.SelectedItem.Text,
                    KodeStatusPensiun = ddlStatusPensiun.SelectedValue,
                    StatusPensiun = ddlStatusPensiun.SelectedItem.Text,
                    KodeKategori = ddlKategori.SelectedValue,
                    Kategori = ddlKategori.SelectedItem.Text,
                    JangkaTahun = Math.Floor(thn)
                });
            }

            rptInputs.DataSource = list;
            rptInputs.DataBind();

        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlKantorBayar.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
                return;
            }

            if (ddlProgramPinjam.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Program Pinjam terlebih dahulu');", true);
                return;
            }
            if (ddlStatusPensiun.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih status Pensiun terlebih dahulu');", true);
                return;
            }
            if (ddlKategori.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kategori terlebih dahulu');", true);
                return;
            }
            pnlButtonForm.Visible = false;
            pnlGenerate.Visible = true;
            pnlGenerateTable.Visible = false;
            VIEWSTATE.Value = "Add";
            btnSave.Text = "Save";
            rptInputs.DataSource = null;
            rptInputs.DataBind();
            int jangka = 0;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTRANGEJANGKA", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlProgramPinjam.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@statuspensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlStatusPensiun.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@kategori", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKategori.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("[Sd_BungaKantorBayar]", var, "constrmf");

            if(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString().Count() > 0)
            {

                jangka = int.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString()) + 1;
                txtLast.Value = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                txtStart.Text = jangka.ToString();
                txtEnd.Text = jangka.ToString();
            }
            else
            {

                txtStart.Text = "1";
                txtEnd.Text = "1";
                txtLast.Value = "0";

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlKantorBayar.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
                return;
            }

            if (ddlProgramPinjam.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Program Pinjam terlebih dahulu');", true);
                return;
            }
            if (ddlStatusPensiun.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih status Pensiun terlebih dahulu');", true);
                return;
            }
            if (ddlKategori.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kategori terlebih dahulu');", true);
                return;
            }

            pnlGenerate.Visible = false;
            VIEWSTATE.Value = "Search";
            btnSave.Text = "Update All";

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlProgramPinjam.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@statuspensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlStatusPensiun.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@kategori", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKategori.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Sd_BungaKantorBayar", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                pnlButtonForm.Visible = true;
                pnlGenerateTable.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                pnlButtonForm.Visible = false;

            }


            int start = int.Parse(txtStart.Text);
            int end = int.Parse(txtEnd.Text);
            decimal thn = 0;
            List<ListRepeater> listRepeater = new List<ListRepeater>();
            foreach (DataRow row in ge.ListDataset.Tables[0].Rows)
            {
                listRepeater.Add(new ListRepeater
                {
                    JangkaBulan = Convert.ToInt32(row["Jangka"]),
                    KodeBank = ddlKantorBayar.SelectedValue,
                    NamaBank = ddlKantorBayar.SelectedItem.Text,
                    KodeProgramPinjam = ddlProgramPinjam.SelectedValue,
                    ProgramPinjam = ddlProgramPinjam.SelectedItem.Text,
                    KodeStatusPensiun = ddlStatusPensiun.SelectedValue,
                    StatusPensiun = ddlStatusPensiun.SelectedItem.Text,
                    KodeKategori = ddlKategori.SelectedValue,
                    Kategori = ddlKategori.SelectedItem.Text,
                    JangkaTahun = Convert.ToInt32(row["JangkaTahun"]),
                    bungaflat1 = Convert.ToDecimal(row["bungaflat1"].ToString()),
                    bungaefektif1 = Convert.ToDecimal(row["bungaefektif1"].ToString()),
                    bungaflat2 = Convert.ToDecimal(row["bungaflat2"].ToString()),
                    bungaefektif2 = Convert.ToDecimal(row["bungaefektif2"].ToString()),
                });
            }

            rptInputs.DataSource = listRepeater;
            rptInputs.DataBind();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnrequest_Click(object sender, EventArgs e)
    {
        try
        {
            string state = (VIEWSTATE.Value == "Search" ? "UPDATE" : "INSERT");
            if (int.Parse(txtEnd.Text) < int.Parse(txtStart.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!',' Range start bulan tidak boleh lebih kecil dari range terakhir bulan ,'success','true');", true);
                return;
            }

            List<ListRepeater> List = new List<ListRepeater>();
            foreach(RepeaterItem item in rptInputs.Items)
            {
                HiddenField HiddenJangkaBulan = item.FindControl("JangkaBulan") as HiddenField;
                HiddenField HiddenKodeBank = item.FindControl("KodeBank") as HiddenField;
                HiddenField HiddenProgramPinjam = item.FindControl("KodeProgramPinjam") as HiddenField;
                HiddenField HiddenStatusPensiun = item.FindControl("KodeStatusPensiun") as HiddenField;
                HiddenField HiddenKategori = item.FindControl("KodeKategori") as HiddenField;
                HiddenField HiddenJangkaTahun = item.FindControl("JangkaTahun") as HiddenField;
                TextBox txtBungaFlat1 = item.FindControl("txtBungaFlat1") as TextBox;
                TextBox txtBungaEfektif1 = item.FindControl("txtBungaEfektif1") as TextBox;
                TextBox txtBungaFlat2 = item.FindControl("txtBungaFlat2") as TextBox;
                TextBox txtBungaEfektif2 = item.FindControl("txtBungaEfektif2") as TextBox;

                string JangkaBulan = HiddenJangkaBulan.Value.ToString();
                string Bank = HiddenKodeBank.Value.ToString();
                string ProgramPinjam = HiddenProgramPinjam.Value.ToString();
                string StatusPensiun = HiddenStatusPensiun.Value.ToString();
                string Kategori = HiddenKategori.Value.ToString();
                string JangkaTahun = HiddenJangkaTahun.Value.ToString();

                decimal s1 = 0;
                decimal BungaFlat1 = decimal.TryParse(txtBungaFlat1.Text.Replace(",", "."), System.Globalization.NumberStyles.Any,System.Globalization.CultureInfo.InvariantCulture, out s1) ? s1 : 0;
                decimal BungaEfektif1 = decimal.TryParse(txtBungaEfektif1.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out s1) ? s1 : 0;
                decimal BungaFlat2 = decimal.TryParse(txtBungaFlat2.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out s1) ? s1 : 0;
                decimal BungaEfektif2 = decimal.TryParse(txtBungaEfektif2.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out s1) ? s1 : 0;



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[14];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, state, typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, Bank, typeof(string));
                var[4] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 50, ProgramPinjam, typeof(string));
                var[5] = gb.CreateSPParameter("@statuspensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, StatusPensiun, typeof(string));
                var[6] = gb.CreateSPParameter("@kategori", ParameterDirection.Input, SqlDbType.VarChar, 50, Kategori, typeof(string));
                var[7] = gb.CreateSPParameter("@jangkatahun", ParameterDirection.Input, SqlDbType.VarChar, 50, JangkaTahun, typeof(string));
                var[8] = gb.CreateSPParameter("@bungaflat1", ParameterDirection.Input, SqlDbType.Decimal, 50, BungaFlat1, typeof(decimal));
                var[9] = gb.CreateSPParameter("@bungaefektif1", ParameterDirection.Input, SqlDbType.Decimal, 50, BungaEfektif1, typeof(decimal));
                var[10] = gb.CreateSPParameter("@bungaflat2", ParameterDirection.Input, SqlDbType.Decimal, 50, BungaFlat2, typeof(decimal));
                var[11] = gb.CreateSPParameter("@bungaefektif2", ParameterDirection.Input, SqlDbType.Decimal, 50, BungaEfektif2, typeof(decimal));
                var[12] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[13] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 50, JangkaBulan, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("Sd_BungaKantorBayar", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully save this Data','top-right','success','true');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);

                }
            }
        }
        catch (Exception ex)
        {
            string safeMessage = ex.Message
            .Replace("\\", "\\\\")     // Escape backslash
            .Replace("'", "\\'")       // Escape single quote
            .Replace("\"", "\\\"")     // Escape double quote
            .Replace("\r", "")         // Remove carriage return
            .Replace("\n", "\\n");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','"+ safeMessage +"','success','true');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    public class ListRepeater
    {
        public int JangkaBulan { get; set; }
        public decimal JangkaTahun { get; set; }
        public string KodeBank { get; set; }
        public string NamaBank { get; set; }
        public string KodeProgramPinjam { get; set; }
        public string ProgramPinjam { get; set; }
        public string KodeStatusPensiun { get; set; }
        public string StatusPensiun { get; set; }
        public string KodeKategori { get; set; }
        public string Kategori { get; set; }
        public decimal bungaflat1 { get; set; }
        public decimal bungaefektif1 { get; set; }
        public decimal bungaflat2 { get; set; }
        public decimal bungaefektif2 { get; set; }


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlProgramPinjam.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@statuspensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlStatusPensiun.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@kategori", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKategori.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("Sd_BungaKantorBayar", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully delete data','top-right','success','true');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
}