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


public partial class Kresun_Master_MasterProvisi : System.Web.UI.Page
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
                PnlFront.Visible = true;
                divtotalrequest.Visible = true;

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

        PnlFront.Visible = true;
        pnlGenerate.Visible = false;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTPROVISI", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[3] = gb.CreateSPParameter("@Grup", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroup.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_MasterProvisi", var, "constrmf");
        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;


    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":

                try
                {
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEPROVISI", typeof(string));
                    var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@Grup", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroup.SelectedValue, typeof(string));
                    var[4] = gb.CreateSPParameter("@Start", ParameterDirection.Input, SqlDbType.VarChar, 50, Convert.ToInt32(txtStart.Text), typeof(string));
                    var[5] = gb.CreateSPParameter("@End", ParameterDirection.Input, SqlDbType.VarChar, 50, Convert.ToInt32(txtEnd.Text), typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_MasterProvisi", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully delete data','top-right','success','true');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);

                    }
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;
            case "SETDEL":
                try
                {

                    string Group = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblGroup")).Text;
                    decimal Provisi = decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblProvisi")).Text.ToString());

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEPROVISI", typeof(string));
                    var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@Grup", ParameterDirection.Input, SqlDbType.VarChar, 50, Group, typeof(string));
                    var[4] = gb.CreateSPParameter("@Start", ParameterDirection.Input, SqlDbType.Decimal, 50, Provisi, typeof(decimal));
                    var[5] = gb.CreateSPParameter("@End", ParameterDirection.Input, SqlDbType.VarChar, 50, Convert.ToInt32(txtEnd.Text), typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_MasterProvisi", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Delete Done!','You successfully delete data','top-right','success','true');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);

                    }
                }
                catch(Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;
        }
    }





    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProvisi.aspx");
    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
  

            if(int.Parse(txtEnd.Text) < int.Parse(txtStart.Text))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Range Akhir tidak boleh lebih besar dari Range Awal');", true);
                return;
            }

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTPROVISI", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@Grup", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroup.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@Start", ParameterDirection.Input, SqlDbType.VarChar, 50, Convert.ToInt32(txtStart.Text), typeof(string));
            var[5] = gb.CreateSPParameter("@End", ParameterDirection.Input, SqlDbType.VarChar, 50, Convert.ToInt32(txtEnd.Text), typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_MasterProvisi", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully delete data','top-right','success','true');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);

            }

        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
  

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTPROVISI2", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@Grup", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroup.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@Start", ParameterDirection.Input, SqlDbType.VarChar, 50, txtprovisi.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_MasterProvisi", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully save data','top-right','success','true');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);

            }

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
            if (ddlGroup.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Group terlebih dahulu');", true);
                return;
            }

            PnlFront.Visible = false;
            pnlGenerate.Visible = true;

            txtStart.Text = "1";
            txtEnd.Text = "1";
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
            if (ddlGroup.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
                return;
            }

            PnlFront.Visible = true;
            pnlGenerate.Visible = false;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTPROVISI", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@Grup", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroup.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_MasterProvisi", var, "constrmf");
            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

   /* protected void btnrequest_Click(object sender, EventArgs e)
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
*/
    public class ListRepeater
    {
        public int No { get; set; }
        public int Provisi { get; set; }
        public string Group { get; set; }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlGroup.SelectedValue, typeof(string));
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