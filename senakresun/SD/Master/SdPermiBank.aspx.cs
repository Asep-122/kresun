using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SD_Master_SdPermiBank : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; } 
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

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true; 
        txtUmur.Enabled = true;
        txtUmur.Focus();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
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
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] varkdbank = new DefaultClass.Global.SqlHelper[4];
        varkdbank[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEBANK", typeof(string));
        varkdbank[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varkdbank[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varkdbank[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterSumberJangka", varkdbank, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kodebank asc";
        ddlkodebank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkodebank.DataTextField = "nmkdbank";
        ddlkodebank.DataValueField = "kodebank";
        ddlkodebank.DataBind();
        ddlkodebank.Items.Insert(0, new ListItem("Pilih Data", "-"));

        DefaultClass.Global.SqlHelper[] varkdasuransi = new DefaultClass.Global.SqlHelper[3];
        varkdasuransi[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTASURANSI", typeof(string));
        varkdasuransi[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varkdasuransi[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //varkdasuransi[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterPremiBank", varkdasuransi, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kodeasuransi asc";
        ddlkdasuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdasuransi.DataTextField = "kdket";
        ddlkdasuransi.DataValueField = "kodeasuransi";
        ddlkdasuransi.DataBind();
        ddlkdasuransi.Items.Insert(0, new ListItem("Pilih Data", "-"));

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterPremiBank", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
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

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;

                    txtPremi.Enabled = false;
                    txtUmur.Enabled = false;
                    txtJangka.Enabled = false;
                    //txtuserid.Enabled = false;
                    ddlkodebank.Enabled = false;
                    ddlstatusdeb.Enabled = false;
                    ddlkdasuransi.Enabled = false;
                    //txtId.Enabled = false;
                    ddlstspenerima.Enabled = false;


                    txtPremi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;
                    txtUmur.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblumur")).Text;
                    txtJangka.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpremi")).Text;
                    //txtuserid.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbluserid")).Text;
                    ddlkodebank.SelectedValue= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    ddlstatusdeb.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstsdeb")).Text;
                    ddlkdasuransi.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdasuransi")).Text;
                    //txtId.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    ddlstspenerima.SelectedValue= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstspen")).Text;

                    btnSimpan.Visible = false;

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

                    txtPremi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;
                    txtUmur.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblumur")).Text;
                    txtJangka.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpremi")).Text;
                    //txtuserid.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbluserid")).Text;
                    ddlkodebank.SelectedValue= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    ddlstatusdeb.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstsdeb")).Text;
                    ddlkdasuransi.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdasuransi")).Text;
                    //txtId.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    ddlstspenerima.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstspen")).Text;


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
                    var[0] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.VarChar, 3, txtUmur.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 3, txtJangka.Text, typeof(string));
                    var[2] = gb.CreateSPParameter("@premi", ParameterDirection.Input, SqlDbType.Money, 0, txtPremi.Text, typeof(decimal));
                    var[3] = gb.CreateSPParameter("@user_id", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(decimal));
                    var[4] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkodebank.SelectedValue, typeof(string));
                    var[5] = gb.CreateSPParameter("@sts_debitur", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlstatusdeb.SelectedValue, typeof(string));
                    var[6] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkdasuransi.SelectedValue, typeof(string));
                    //var[7] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 10, txtId.Text, typeof(string));
                    var[7] = gb.CreateSPParameter("@sts_penerima", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstspenerima.SelectedValue, typeof(string));
                    var[8] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[10] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterPremiBank", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
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
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (txtJangka.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[12];
            var1[0] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.VarChar, 3, txtUmur.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 3, txtJangka.Text, typeof(string));
            var1[2] = gb.CreateSPParameter("@premi", ParameterDirection.Input, SqlDbType.Money, 0, txtPremi.Text, typeof(decimal));
            var1[3] = gb.CreateSPParameter("@user_id", ParameterDirection.Input, SqlDbType.VarChar, 10, SessLoginID, typeof(decimal));
            var1[4] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkodebank.SelectedValue, typeof(string));
            var1[5] = gb.CreateSPParameter("@sts_debitur", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlstatusdeb.SelectedValue, typeof(string));
            var1[6] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkdasuransi.SelectedValue, typeof(string));
            //var1[7] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 10, txtId.Text, typeof(string));
            var1[7] = gb.CreateSPParameter("@sts_penerima", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstspenerima.SelectedValue, typeof(string)); 
            var1[8] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
            var1[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var1[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[11] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterPremiBank", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Save this Project','top-right','success','2');", true);
            }
            else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Duplicate"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank " + txtJangka.Text + ", " + ddlstspenerima.SelectedValue + " already Exists!');", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

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

}