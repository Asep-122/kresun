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


public partial class RateBungaAsuransiGroup : System.Web.UI.Page
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
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGROUP", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[As_RateBungaAsuransiGroup]", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "GroupAsuransi asc";
        ddlGroupAsuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlGroupAsuransi.DataTextField = "NamaGroup";
        ddlGroupAsuransi.DataValueField = "GroupAsuransi";
        ddlGroupAsuransi.DataBind();
        ddlGroupAsuransi.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Pilih Group Asuransi", "-"));

        ddlJangkaTahun.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Pilih Jangka Tahun","0"));

        for (int i = 1; i <= 35; i++) {
            ddlJangkaTahun.Items.Insert(i, i.ToString());
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


            if(int.Parse(ddlJangkaTahun.SelectedValue) == 0)
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


            int JangkaTahun = int.Parse(ddlJangkaTahun.SelectedValue);          
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

                    JangkaTahun = JangkaTahun,
                    Usia = x,
                    GroupAsuransi = ddlGroupAsuransi.SelectedValue,
                    NamaGroupAsuransi = ddlGroupAsuransi.SelectedItem.Text,
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

            if (ddlGroupAsuransi.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Group Asuransi terlebih dahulu');", true);
                return;
            }

            pnlButtonForm.Visible = false;
            pnlGenerate.Visible = true;
            pnlGenerateTable.Visible = false;
            btnGenerate.Visible = true;
            PnlFront.Visible = false;
            VIEWSTATE.Value = "Add";
            btnSave.Text = "Save";
            ddlJangkaTahun.SelectedIndex = 0;
            rptInputs.DataSource = null;
            rptInputs.DataBind();
            int Usia = 0;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTRANGEJANGKA", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@GroupAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroupAsuransi.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("[As_RateBungaAsuransiGroup]", var, "constrmf");

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
            if (ddlGroupAsuransi.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Group Asuransi terlebih dahulu');", true);
                return;
            }

            pnlGenerate.Visible = false;
            VIEWSTATE.Value = "Search";
            btnSave.Text =  "Update All";
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@GroupAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroupAsuransi.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("As_RateBungaAsuransiGroup", var, "constrmf");

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
            List<ListRepeater> listRepeater = new List<ListRepeater>();
            foreach (DataRow row in ge.ListDataset.Tables[0].Rows)
            {
                listRepeater.Add(new ListRepeater
                {

                    JangkaTahun = Convert.ToDecimal(row["jangka"].ToString()),
                    Usia = Convert.ToInt32(row["Umur"]),
                    GroupAsuransi = row["GroupAsuransi"].ToString(),
                    NamaGroupAsuransi = row["NamaGroup"].ToString(),
                    bunga1 = Convert.ToDecimal(row["bunga1"].ToString()),
                    bunga2 = Convert.ToDecimal(row["bunga2"].ToString()),
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
                HiddenField HiddenGroupAsuransi = item.FindControl("GroupAsuransi") as HiddenField;
                HiddenField HiddenNamaGroup = item.FindControl("NamaGroupAsuransi") as HiddenField;
                HiddenField HiddenJangkaTahun = item.FindControl("JangkaTahun") as HiddenField;
                HiddenField HiddenUsia = item.FindControl("Usia") as HiddenField;
                TextBox txtBunga1 = item.FindControl("txtBunga1") as TextBox;
                TextBox txtBunga2 = item.FindControl("txtBunga2") as TextBox;


                string Bank = HiddenGroupAsuransi.Value.ToString();
                string ProgramPinjam = HiddenNamaGroup.Value.ToString();
                string JangkaTahun = HiddenJangkaTahun.Value.ToString();
                string Usia = HiddenUsia.Value.ToString();

                decimal s1 = 0;
                decimal Bunga1 = decimal.TryParse(txtBunga1.Text.Replace(",", "."), System.Globalization.NumberStyles.Any,System.Globalization.CultureInfo.InvariantCulture, out s1) ? s1 : 0;
                decimal Bunga2= decimal.TryParse(txtBunga2.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out s1) ? s1 : 0;



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, state, typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@groupasuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroupAsuransi.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@jangkatahun", ParameterDirection.Input, SqlDbType.VarChar, 50, JangkaTahun, typeof(string));
                var[5] = gb.CreateSPParameter("@bunga1", ParameterDirection.Input, SqlDbType.Decimal, 50, Bunga1, typeof(decimal));
                var[6] = gb.CreateSPParameter("@bunga2", ParameterDirection.Input, SqlDbType.Decimal, 50, Bunga2, typeof(decimal));
                var[7] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[8] = gb.CreateSPParameter("@usia", ParameterDirection.Input, SqlDbType.VarChar, 50, Usia, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("As_RateBungaAsuransiGroup", var, "constrmf");

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
        public string GroupAsuransi { get; set; }
        public string NamaGroupAsuransi { get; set; }
        public int Usia{ get; set; }
        public decimal bunga1 { get; set; }
        public decimal bunga2 { get; set; }


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@GroupAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroupAsuransi.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("As_RateBungaAsuransiGroup", var, "constrmf");
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

    protected void ddlJangkaTahun_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTRANGEJANGKA", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@groupasuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroupAsuransi.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@jangkatahun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJangkaTahun.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("As_RateBungaAsuransiGroup", var, "constrmf");
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
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
}