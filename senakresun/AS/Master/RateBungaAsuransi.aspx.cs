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
using DocumentFormat.OpenXml.Wordprocessing;


public partial class RateBungaAsuransi : System.Web.UI.Page
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
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTASURANSI", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[As_RateBungaAsuransi]", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kodeasuransi asc";
        ddlAsuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAsuransi.DataTextField = "NamaAsuransi";
        ddlAsuransi.DataValueField = "KodeAsuransi";
        ddlAsuransi.DataBind();
        ddlAsuransi.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Pilih Asuransi", "-"));

        ddlJangkaBulan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Pilih Jangka Bulan","0"));

        for (int i = 1; i <= 300; i++) {
            ddlJangkaBulan.Items.Insert(i, i.ToString());
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RateBungaAsuransi.aspx");
    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            pnlButtonForm.Visible = true;


            if(int.Parse(ddlJangkaBulan.SelectedValue) == 0)
            {
                btnSave.Visible = false;
                btnDelete.Visible = false;
                string pesan = "Mohon pilih Jangka Tahun terlebih dahulu";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + pesan + "');", true);
                rptInputs.DataSource = null;
                rptInputs.DataBind();
                return;

            }

            if (int.Parse(txtStartUsia.Text) <= int.Parse(txtLastUsia.Value))
            {
                int jangka = int.Parse(txtLastUsia.Value) + 1;                
                btnSave.Visible = false;
                btnDelete.Visible = false;
                string pesan = "Usia " + int.Parse(txtStartUsia.Text).ToString() + " Tahun sudah ada di database mohon periksa kembali";
                pesan = pesan.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + pesan + "');", true);
                txtStartUsia.Text = jangka.ToString();
                rptInputs.DataSource = null;
                rptInputs.DataBind();
                return;
            }

            if (int.Parse(txtEndUsia.Text) < int.Parse(txtStartUsia.Text))
            {
                btnSave.Visible = false;
                btnDelete.Visible = false;
                int jangka = int.Parse(txtLastUsia.Value) + 1;
                txtStartUsia.Text = jangka.ToString();
                txtEndUsia.Text = jangka.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Range start usia tidak boleh lebih besar dari range usia akhir');", true);
                rptInputs.DataSource = null;
                rptInputs.DataBind();
                return;
            }


            int JangkaBulan = int.Parse(ddlJangkaBulan.SelectedValue);          
            int startUsia = int.Parse(txtStartUsia.Text);
            int endUsia = int.Parse(txtEndUsia.Text);
            btnSave.Visible = true;
            btnDelete.Visible = false;
            pnlGenerateTable.Visible = true;
            decimal thn = 0;
            List<ListRepeater> list = new List<ListRepeater>();

            for (int x = startUsia; x <= endUsia; x++)
            {
                
                list.Add(new ListRepeater
                {

                    JangkaBulan = JangkaBulan,
                    Usia = x,
                    KodeAsuransi = ddlAsuransi.SelectedValue,
                    NamaAsuransi = ddlAsuransi.SelectedItem.Text,
                    JangkaTahun =  Convert.ToDecimal(txtJangkaTahun.Text)
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

            if (ddlAsuransi.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Asuransi terlebih dahulu');", true);
                return;
            }

            pnlButtonForm.Visible = false;
            pnlGenerate.Visible = true;
            pnlGenerateTable.Visible = false;
            btnGenerate.Visible = true;
            PnlFront.Visible = false;
            VIEWSTATE.Value = "Add";
            btnSave.Text = "Save";
            ddlJangkaBulan.SelectedIndex = 0;
            rptInputs.DataSource = null;
            rptInputs.DataBind();
            int Usia = 0;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTRANGEJANGKA", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlAsuransi.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("[As_RateBungaAsuransi]", var, "constrmf");

            if(ge.ListDataset.Tables[0].Rows[0]["Usia"].ToString().Count() > 0)
            {

                Usia = int.Parse(ge.ListDataset.Tables[0].Rows[0]["Usia"].ToString()) + 1;
                txtLastUsia.Value = ge.ListDataset.Tables[0].Rows[0]["Usia"].ToString();
                txtStartUsia.Text = Usia.ToString();
                txtEndUsia.Text = Usia.ToString();
            }
            else
            {

                txtStartUsia.Text = "1";
                txtEndUsia.Text = "1";
                txtLastUsia.Value = "0";

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
            if (ddlAsuransi.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Asuransi terlebih dahulu');", true);
                return;
            }

            pnlGenerate.Visible = false;
            VIEWSTATE.Value = "Search";
            btnSave.Text =  "Update All";
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlAsuransi.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("As_RateBungaAsuransi", var, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                pnlButtonForm.Visible = true;
                pnlGenerateTable.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                pnlButtonForm.Visible = false;
                pnlGenerateTable.Visible = true;
            }
            List<ListRepeater> listRepeater = new List<ListRepeater>();
            foreach (DataRow row in ge.ListDataset.Tables[0].Rows)
            {
                listRepeater.Add(new ListRepeater
                {

                    JangkaTahun = Convert.ToDecimal(row["jangka"].ToString()),
                    JangkaBulan = Convert.ToInt32(row["jangkabulan"].ToString()),
                    Usia = Convert.ToInt32(row["Umur"]),
                    KodeAsuransi = row["KodeAsuransi"].ToString(),
                    NamaAsuransi = row["NamaAsuransi"].ToString(),
                    bunga = Convert.ToDecimal(row["bunga"].ToString()),
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
            
            List<ListRepeater> List = new List<ListRepeater>();
            foreach(RepeaterItem item in rptInputs.Items)
            {
                HiddenField HiddenKodeAsuransi = item.FindControl("KodeAsuransi") as HiddenField;
                HiddenField HiddenNamaAsuransi = item.FindControl("NamaAsuransi") as HiddenField;
                HiddenField HiddenJangkaTahun = item.FindControl("JangkaTahun") as HiddenField;
                HiddenField HiddenJangkaBulan = item.FindControl("JangkaBulan") as HiddenField;
                HiddenField HiddenUsia = item.FindControl("Usia") as HiddenField;
                TextBox txtBunga = item.FindControl("txtBunga") as TextBox;


                string Bank = HiddenKodeAsuransi.Value.ToString();
                string ProgramPinjam = HiddenNamaAsuransi.Value.ToString();
                string JangkaTahun = HiddenJangkaTahun.Value.ToString();
                string JangkaBulan = HiddenJangkaBulan.Value.ToString();
                string Usia = HiddenUsia.Value.ToString();

                decimal s1 = 0;
                decimal Bunga = decimal.TryParse(txtBunga.Text.Replace(",", "."), System.Globalization.NumberStyles.Any,System.Globalization.CultureInfo.InvariantCulture, out s1) ? s1 : 0;



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, state, typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlAsuransi.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@jangkatahun", ParameterDirection.Input, SqlDbType.VarChar, 50, JangkaTahun, typeof(string));
                var[5] = gb.CreateSPParameter("@jangkabulan", ParameterDirection.Input, SqlDbType.VarChar, 50, JangkaBulan, typeof(string));
                var[6] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Decimal, 50, Bunga, typeof(decimal));
                var[7] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[8] = gb.CreateSPParameter("@usia", ParameterDirection.Input, SqlDbType.VarChar, 50, Usia, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("As_RateBungaAsuransi", var, "constrmf");

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
        public string KodeAsuransi { get; set; }
        public string NamaAsuransi { get; set; }
        public int Usia{ get; set; }
        public decimal bunga { get; set; }


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlAsuransi.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("As_RateBungaAsuransi", var, "constrmf");
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

    protected void ddlJangkaBulan_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTRANGEJANGKA", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlAsuransi.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@jangkabulan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJangkaBulan.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("As_RateBungaAsuransi", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                int Usia = 0;
                Usia = int.Parse(ge.ListDataset.Tables[0].Rows[0]["Usia"].ToString()) + 1;
                txtStartUsia.Text = Usia.ToString();
                txtEndUsia.Text = Usia.ToString();
                txtLastUsia.Value = ge.ListDataset.Tables[0].Rows[0]["Usia"].ToString();
            }
            else
            {
                txtStartUsia.Text = "1";
                txtEndUsia.Text = "1";
                txtLastUsia.Value = "0";
            }
            decimal thn = 0;
            thn = (Convert.ToDecimal(ddlJangkaBulan.SelectedValue) - 1) / 12 + 1;
            txtJangkaTahun.Text = Math.Floor(thn).ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
}