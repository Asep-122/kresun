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
using System.Activities.Debugger;

public partial class MappingSumberDanaKantorBayar : System.Web.UI.Page
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

    public string fotopathktp = "", tanggal = "", fotopathnpwp = "", fotopathKK = "";
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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("MappingSumberDanaKantorBayar.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        PnlFront.Visible = false;
        pnlHeader.Visible = false;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnSave.Visible = true;
    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] varSumberDana = new DefaultClass.Global.SqlHelper[3];
        varSumberDana[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSUMBERDANA", typeof(string));
        varSumberDana[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varSumberDana[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[Sd_Mapping_SumberDanaKantorBayar]", varSumberDana, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";
        ddlSumberDana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlSumberDana.DataTextField = "NamaBank";
        ddlSumberDana.DataValueField = "KodeBank";
        ddlSumberDana.DataBind();
        ddlSumberDana.Items.Insert(0, new ListItem("Pilih Sumber Dana", "-"));


        DefaultClass.Global.SqlHelper[] varKantorBayar = new DefaultClass.Global.SqlHelper[3];
        varKantorBayar[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKANTORBAYAR", typeof(string));
        varKantorBayar[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varKantorBayar[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[Sd_Mapping_SumberDanaKantorBayar]", varKantorBayar, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";
        ddlKantorBayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKantorBayar.DataTextField = "NamaBank";
        ddlKantorBayar.DataValueField = "KodeBank";
        ddlKantorBayar.DataBind();
        ddlKantorBayar.Items.Insert(0, new ListItem("Pilih KantorBayar Dana", "-"));


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("Sd_Mapping_SumberDanaKantorBayar", var, "constrmf");
        GRData.DataSource = ge.ListDataset.Tables[0];
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
                    pnladd.Visible = true;
                    PnlFront.Visible = false;
                    pnlHeader.Visible = false;
                    btnSave.Visible = false;
                    btnDelete.Visible = false;

                    ddlSumberDana.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesumberdana")).Text;
                    ddlKantorBayar.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodekantorbayar")).Text;

                    hidden_retSumberDana.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesumberdana")).Text;
                    hidden_retKantorBayar.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodekantorbayar")).Text;

                    string Status = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    ddlStatus.SelectedValue = (Status == "Non Aktif" ? "0" : "1");

                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;
            case "DEL":
                try
                {


                    pnladd.Visible = true;
                    PnlFront.Visible = false;
                    pnlHeader.Visible = false;
                    btnSave.Visible = false;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = true;

                    ddlKantorBayar.Enabled = false;
                    ddlSumberDana.Enabled = false;

                    ddlSumberDana.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesumberdana")).Text;
                    ddlKantorBayar.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodekantorbayar")).Text;

                    hidden_retSumberDana.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesumberdana")).Text;
                    hidden_retKantorBayar.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodekantorbayar")).Text;

                    string Status = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    ddlStatus.SelectedValue = (Status == "Non Aktif" ? "0" : "1");

                //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                //var[0] = gb2.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKdBank.Text, typeof(string));
                //var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSETDEL", typeof(string));
                //var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //ge2.ListDataset = gb2.GetDataJustOutPut("Sd_Supplier", var, "constrmf");

                    //txtNamaBank.Text = ge2.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                    //ddlGroup.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeGroup"].ToString();
                    //ddlSumberDana.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["sumberdana"].ToString();
                    //ddlKantorBayar.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["kantorbayar"].ToString();
                    //ddlKodeSimulasi.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeSimulasi"].ToString();
                    //txtContactPerson.Text = ge2.ListDataset.Tables[0].Rows[0]["HubungDengan"].ToString();
                    //txtJabatan.Text = ge2.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                    //txtNoHp.Text = ge2.ListDataset.Tables[0].Rows[0]["nohp"].ToString();
                    //txtCatatan.Text = ge2.ListDataset.Tables[0].Rows[0]["Memo"].ToString();
                    //txtAlamat.Text = ge2.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
                    //ddlStatus.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["NonAktifFlag"].ToString();


                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

                break;

        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("MappingSumberDanaKantorBayar.aspx");
    }

    protected void GRData_RowCommand1(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string STATE = "INSERT";
        try
        {
            if (ddlSumberDana.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Sumber Dana terlebih dahulu');", true);
                return;
            }

            if (ddlKantorBayar.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
                return;
            }

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[1] = gb2.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb2.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb2.CreateSPParameter("@KodekantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedValue, typeof(string));
            var[4] = gb2.CreateSPParameter("@KodesumberDana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSumberDana.SelectedValue, typeof(string));
            var[5] = gb2.CreateSPParameter("@NamaKantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedItem.Text, typeof(string));
            var[6] = gb2.CreateSPParameter("@NamaSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSumberDana.SelectedItem.Text, typeof(string));
            var[7] = gb2.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlStatus.SelectedValue, typeof(string));


            ge2.ListDataset = gb2.GetDataJustOutPut("Sd_Mapping_SumberDanaKantorBayar", var, "constrmf");

            if (ge2.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully save this Simulasi','top-right','success','true');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);
            }

        }
        catch(Exception ex)
        {
            string safeMessage = ex.Message
            .Replace("\\", "\\\\")     // Escape backslash
            .Replace("'", "\\'")       // Escape single quote
            .Replace("\"", "\\\"")     // Escape double quote
            .Replace("\r", "")         // Remove carriage return
            .Replace("\n", "\\n");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + safeMessage + "','success','true');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string STATE = "DELETE";
        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[10];
            var[0] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[1] = gb2.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb2.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb2.CreateSPParameter("@KodekantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedValue, typeof(string));
            var[4] = gb2.CreateSPParameter("@KodesumberDana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSumberDana.SelectedValue, typeof(string));
            var[5] = gb2.CreateSPParameter("@NamaKantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedItem.Text, typeof(string));
            var[6] = gb2.CreateSPParameter("@NamaSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSumberDana.SelectedItem.Text, typeof(string));
            var[7] = gb2.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlStatus.SelectedValue, typeof(string));
            var[8] = gb2.CreateSPParameter("@ret_KodesumberDana", ParameterDirection.Input, SqlDbType.VarChar, 50, hidden_retSumberDana.Value.ToString(), typeof(string));
            var[9] = gb2.CreateSPParameter("@ret_KodeKantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 50, hidden_retKantorBayar.Value.ToString(), typeof(string));

            ge2.ListDataset = gb2.GetDataJustOutPut("Sd_Mapping_SumberDanaKantorBayar", var, "constrmf");

            if (ge2.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Delete Done!','You successfully save this Simulasi','top-right','success','true');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string STATE = "UPDATE";
        try
        {

            if (ddlSumberDana.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Sumber Dana terlebih dahulu');", true);
                return;
            }

            if (ddlKantorBayar.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
                return;
            }


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[10];
            var[0] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[1] = gb2.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb2.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb2.CreateSPParameter("@KodekantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedValue, typeof(string));
            var[4] = gb2.CreateSPParameter("@KodesumberDana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSumberDana.SelectedValue, typeof(string));
            var[5] = gb2.CreateSPParameter("@NamaKantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedItem.Text, typeof(string));
            var[6] = gb2.CreateSPParameter("@NamaSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSumberDana.SelectedItem.Text, typeof(string));
            var[7] = gb2.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlStatus.SelectedValue, typeof(string));
            var[8] = gb2.CreateSPParameter("@ret_KodesumberDana", ParameterDirection.Input, SqlDbType.VarChar, 50, hidden_retSumberDana.Value.ToString(), typeof(string));
            var[9] = gb2.CreateSPParameter("@ret_KodeKantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 50, hidden_retKantorBayar.Value.ToString(), typeof(string));
            ge2.ListDataset = gb2.GetDataJustOutPut("Sd_Mapping_SumberDanaKantorBayar", var, "constrmf");

            if (ge2.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Update Done!','You successfully save this Simulasi','top-right','success','true');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);
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

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + safeMessage + "','success','true');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
}