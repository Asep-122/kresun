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

public partial class SD_Master_SdSumberJangka : System.Web.UI.Page
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
        txtJangka.Enabled = true;
        ddlkodebank.Enabled = true;
        ddlprogpinj.Enabled = true;
        txtJangka.Focus();
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


        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEPROGPINJ", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterSumberJangka", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";
        ddlprogpinj.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprogpinj.DataTextField = "nmkd";
        ddlprogpinj.DataValueField = "kode";
        ddlprogpinj.DataBind();
        ddlprogpinj.Items.Insert(0, new ListItem("Pilih Data", "-"));


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterSumberJangka", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
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
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;

                    string kodebank;

                    txtumur.Enabled = false;
                    txtJangka.Enabled = false;
                    ddlkodebank.Enabled = false;
                    ddlprogpinj.Enabled = false;
                    txtBunga.Enabled = true;
                    txtJangka.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;
                    txtumur.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblumur")).Text;
                    txtBunga.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbunga")).Text;
                    //txtuserid.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbluserid")).Text;
                    ddlkodebank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    //txtId.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    //ddlkodebank.SelectedValue = kodebank;
                    ddlprogpinj.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblprogpin")).Text;

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
                    txtJangka.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;
                    ddlkodebank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    ddlprogpinj.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblprogpin")).Text;
                    txtumur.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblumur")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[8];
                    var1[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 15, txtJangka.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlkodebank.SelectedValue, typeof(string));
                    var1[2] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.VarChar, 15, txtumur.Text, typeof(string));
                    var1[3] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlprogpinj.Text, typeof(string));
                    var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterSumberJangka", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
                    }
                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Enable"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Already Used, Please Contact Admin. Thank You.');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
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
                    // txtJangka.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;
                    // ddlkodebank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    // ddlprogpinj.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblprogpin")).Text;
                    // txtumur.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblumur")).Text;
                    
                    // DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[8];
                    // var1[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 15, txtJangka.Text, typeof(string));
                    // var1[1] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlkodebank.SelectedValue, typeof(string));
                    // var1[2] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.VarChar, 15, txtumur.Text, typeof(string));
                    // var1[3] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlprogpinj.Text, typeof(string));
                    // var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKSDJANGKA", typeof(string));
                    // var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    // var1[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    // var1[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    // ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterSumberJangka", var1, "constrmf");

                    // if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                    // {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sumberdana sudah ada di transaksi tidak boleh di hapus!');", true);

                    // }
                    // else
                    // {
                        
                    // DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[8];
                    // var1[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 15, txtJangka.Text, typeof(string));
                    // var1[1] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlkodebank.SelectedValue, typeof(string));
                    // var1[2] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.VarChar, 15, txtumur.Text, typeof(string));
                    // var1[3] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlprogpinj.Text, typeof(string));
                    // var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    // var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    // var1[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    // var1[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
					
                        // ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterSumberJangka", var1, "constrmf");
                        // if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        // {
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
                        // }
						// else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Enable"))
						// {
							// ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Already used. Please Contact Admin, Thank You.');", true);
							// gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
						// }
                        // else
                        // {
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            // gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        // }
                    // }
                // // }
                // catch (Exception X)
                // {
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                // }

                // break;

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

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[9];
            var1[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 3, txtJangka.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodebank.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlprogpinj.SelectedValue, typeof(string));
            var1[3] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Money, 0, txtBunga.Text, typeof(decimal));
            //var1[4] = gb.CreateSPParameter("@user_id", ParameterDirection.Input, SqlDbType.VarChar, 40, userid, typeof(string));
            //var1[5] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 40, txtId.Text, typeof(string));
            var1[4] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
            var1[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var1[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[7] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[8] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.VarChar, 3, txtumur.Text, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Sd_MasterSumberJangka", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Save this Project','top-right','success','2');", true);
            }
            else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Duplicate"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank " + txtJangka.Text + ", " + ddlkodebank.SelectedValue + ", " + ddlprogpinj.SelectedValue + " already Exists!');", true);
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