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

public partial class SD_Master_SumberDana : System.Web.UI.Page
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
        Response.Redirect("SumberDana.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        pnlButtonForm.Visible = true;
        PnlFront.Visible = false;
        pnlHeader.Visible = false;
        btnDelete.Visible = false;
    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[Sd_Supplier]", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";
        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;


        DefaultClass.Global.SqlHelper[] varGroup = new DefaultClass.Global.SqlHelper[4];
        varGroup[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGROUP", typeof(string));
        varGroup[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varGroup[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varGroup[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[Sd_Supplier]", varGroup, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGroup asc";
        ddlGroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlGroup.DataTextField = "NamaGroup";
        ddlGroup.DataValueField = "KodeGroup";
        ddlGroup.DataBind();
        ddlGroup.Items.Insert(0, new ListItem("Pilih Group", "-"));

    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try
                {
                    pnladd.Visible = true;
                    pnlButtonForm.Visible = true;
                    PnlFront.Visible = false;
                    pnlHeader.Visible = false;
                    btnDelete.Visible = false;
                    txtKdBank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodebank")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb2.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKdBank.Text, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSETDEL", typeof(string));
                    var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge2.ListDataset = gb2.GetDataJustOutPut("Sd_Supplier", var, "constrmf");

                    txtNamaBank.Text = ge2.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                    ddlGroup.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeGroup"].ToString();
                    ddlSumberDana.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["sumberdana"].ToString();
                    ddlKantorBayar.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["kantorbayar"].ToString();
                    ddlKodeSimulasi.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeSimulasi"].ToString();
                    txtContactPerson.Text = ge2.ListDataset.Tables[0].Rows[0]["HubungDengan"].ToString();
                    txtJabatan.Text = ge2.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                    txtNoHp.Text = ge2.ListDataset.Tables[0].Rows[0]["nohp"].ToString();
                    txtCatatan.Text = ge2.ListDataset.Tables[0].Rows[0]["Memo"].ToString();
                    txtAlamat.Text = ge2.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
                    ddlStatus.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["NonAktifFlag"].ToString();


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
                    pnlButtonForm.Visible = true;
                    PnlFront.Visible = false;
                    pnlHeader.Visible = false;
                    btnSave.Visible = false;
                    txtKdBank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodebank")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb2.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKdBank.Text, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSETDEL", typeof(string));
                    var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge2.ListDataset = gb2.GetDataJustOutPut("Sd_Supplier", var, "constrmf");

                    txtNamaBank.Text = ge2.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                    ddlGroup.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeGroup"].ToString();
                    ddlSumberDana.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["sumberdana"].ToString();
                    ddlKantorBayar.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["kantorbayar"].ToString();
                    ddlKodeSimulasi.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeSimulasi"].ToString();
                    txtContactPerson.Text = ge2.ListDataset.Tables[0].Rows[0]["HubungDengan"].ToString();
                    txtJabatan.Text = ge2.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                    txtNoHp.Text = ge2.ListDataset.Tables[0].Rows[0]["nohp"].ToString();
                    txtCatatan.Text = ge2.ListDataset.Tables[0].Rows[0]["Memo"].ToString();
                    txtAlamat.Text = ge2.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
                    ddlStatus.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["NonAktifFlag"].ToString();


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
        Response.Redirect("SumberDana.aspx");
    }

    protected void GRData_RowCommand1(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string STATE = string.Empty;
        try
        {
            if (string.IsNullOrEmpty(txtNamaBank.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Nama Bank terlebih dahulu');", true);
                return;
            }
            else if (ddlGroup.Text == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Group terlebih dahulu');", true);
                return;
            }
            else if (ddlSumberDana.Text == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Sumber Dana terlebih dahulu');", true);
                return;
            }
            else if (ddlKantorBayar.Text == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
                return;
            }
            else if (ddlKodeSimulasi.Text == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Simulasi terlebih dahulu');", true);
                return;
            }
            else if (ddlStatus.Text == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Status Aktif/Non Aktif terlebih dahulu');", true);
                return;
            }
            else if (string.IsNullOrEmpty(txtJabatan.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Jabatan terlebih dahulu');", true);
                return;
            }

            STATE = (string.IsNullOrWhiteSpace(txtKdBank.Text) ? "INSERT" : "UPDATE");

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[17];
            var[0] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 15, txtKdBank.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlGroup.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@NamaBank", ParameterDirection.Input, SqlDbType.VarChar, 200, txtNamaBank.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@Alamat1", ParameterDirection.Input, SqlDbType.VarChar, 200, txtAlamat.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@Memo", ParameterDirection.Input, SqlDbType.VarChar, 300, txtCatatan.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@HubungDengan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtContactPerson.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[7] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
            var[8] = gb.CreateSPParameter("@NonAktifFlag", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlStatus.SelectedValue, typeof(string));
            var[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[11] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[12] = gb.CreateSPParameter("@Jabatan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtJabatan.Text, typeof(string));
            var[13] = gb.CreateSPParameter("@NoHp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoHp.Text, typeof(string));
            var[14] = gb.CreateSPParameter("@KodeSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKodeSimulasi.SelectedValue, typeof(string));
            var[15] = gb.CreateSPParameter("@KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantorBayar.SelectedValue, typeof(string));
            var[16] = gb.CreateSPParameter("@SumberDana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSumberDana.SelectedValue, typeof(string));

            ge.ListDataset = gb2.GetDataJustOutPut("Sd_Supplier", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb2.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKdBank.Text, typeof(string));
            var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb2.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge2.ListDataset = gb2.GetDataJustOutPut("Sd_Supplier", var, "constrmf");

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
}