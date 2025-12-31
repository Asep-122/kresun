 
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

public partial class Kresun_Master_SukuBungaProduk : System.Web.UI.Page
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
        ddlpropin.Enabled = true;
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
        varkdbank[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPROPIN", typeof(string));
        varkdbank[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varkdbank[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varkdbank[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_KPSUKUBUNGAPRODUK", varkdbank, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";
        ddlpropin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpropin.DataTextField = "nmkd";
        ddlpropin.DataValueField = "kode";
        ddlpropin.DataBind();
        ddlpropin.Items.Insert(0, new ListItem("PILIH DATA", "-"));



        DefaultClass.Global.SqlHelper[] varkdbank1 = new DefaultClass.Global.SqlHelper[4];
        varkdbank1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEBANK", typeof(string));
        varkdbank1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varkdbank1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varkdbank1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_KPSUKUBUNGAPRODUK", varkdbank1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";
        ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKdBank.DataTextField = "nmkd";
        ddlKdBank.DataValueField = "kode";
        ddlKdBank.DataBind();
        ddlKdBank.Items.Insert(0, new ListItem("PILIH DATA", "-"));


        DefaultClass.Global.SqlHelper[] varkdbank2 = new DefaultClass.Global.SqlHelper[4];
        varkdbank2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEPRODUK", typeof(string));
        varkdbank2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varkdbank2[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varkdbank2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_KPSUKUBUNGAPRODUK", varkdbank2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";
        ddlkdProduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdProduk.DataTextField = "nmkd";
        ddlkdProduk.DataValueField = "kode";
        ddlkdProduk.DataBind();
        ddlkdProduk.Items.Insert(0, new ListItem("PILIH DATA", "-"));

        DefaultClass.Global.SqlHelper[] varkdbank4 = new DefaultClass.Global.SqlHelper[4];
        varkdbank4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEAREA", typeof(string));
        varkdbank4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varkdbank4[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varkdbank4[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_KPSUKUBUNGAPRODUK", varkdbank4, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";
        ddlarea.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlarea.DataTextField = "nmkd";
        ddlarea.DataValueField = "kode";
        ddlarea.DataBind();
        ddlarea.Items.Insert(0, new ListItem("PILIH DATA", "-"));


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_KPSUKUBUNGAPRODUK", var, "constrmf");

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
                    txtJangka.Enabled = false;
                    ddlpropin.Enabled = false;
                    ddlKdBank.Enabled = false;
                    ddlkdProduk.Enabled = false;

                    txtJangka.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;
                    txtUsia.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblumur")).Text;
                    txtbunga.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbunga")).Text;
                    txtpergaj.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperGaj")).Text;
                    txtprovisi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblprovisi")).Text;

                    var kdbank = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    var kdproduk = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdprod")).Text;
                    var propin = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpropin")).Text;

                    ddlKdBank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    ddlkdProduk.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdprod")).Text;
                    ddlpropin.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpropin")).Text; 
                    ddlarea.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdWil")).Text;


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
                    ddlKdBank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    ddlkdProduk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdprod")).Text;
                    ddlpropin.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpropin")).Text;
                    txtJangka.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text; 

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                    var[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 50, txtJangka.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpropin.SelectedValue, typeof(string));
                    var[2] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKdBank.SelectedValue, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[6] = gb.CreateSPParameter("@kdProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkdProduk.SelectedValue, typeof(string));



                    ge.ListDataset = gb.GetDataJustOutPut("KP_KPSUKUBUNGAPRODUK", var, "constrmf");
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
            if (ddlpropin.SelectedValue != "-")
            {

                if (txtJangka.Enabled == true)
                {
                    STATE = "INSERT";
                }
                else
                {
                    STATE = "UPDATE";
                }

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[14];
                var1[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 3, txtJangka.Text, typeof(int));
                var1[1] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpropin.SelectedValue, typeof(string));
                var1[2] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Float, 0, txtbunga.Text, typeof(float));
                var1[3] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
                var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var1[7] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.Int, 3, txtUsia.Text, typeof(int));
                var1[8] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKdBank.SelectedValue, typeof(string));
                var1[9] = gb.CreateSPParameter("@kdproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkdProduk.SelectedValue, typeof(string));
                var1[10] = gb.CreateSPParameter("@pergaj", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpergaj.Text, typeof(decimal));
                var1[11] = gb.CreateSPParameter("@provisi", ParameterDirection.Input, SqlDbType.Float, 50, txtprovisi.Text, typeof(float));
                var1[12] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[13] = gb.CreateSPParameter("@area", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlarea.SelectedValue, typeof(string));


                ge.ListDataset = gb.GetDataJustOutPut("KP_KPSUKUBUNGAPRODUK", var1, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Save this Project','top-right','success','2');", true);
                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Duplicate"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Jangka: " + txtJangka.Text + ", Kode Program Pinjaman: " + ddlpropin.SelectedValue + ", Kode Produk : " + ddlkdProduk.SelectedValue + ", Kode Wilayah : " + ddlarea.SelectedValue + " dan Kode Bank: " + ddlKdBank.SelectedValue + " already Exists!');", true);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Isi Data Program Pinjaman');", true);
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
