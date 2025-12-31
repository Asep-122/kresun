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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Org.BouncyCastle.Asn1.Ocsp;
public partial class Kresun_Master_MasterKriteriaSumberDana : System.Web.UI.Page
{
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
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    DataTable dtRP = new DataTable("Transaction");
    DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "XXX";
    string kodesumber = "KP";
    string KodeKonter = "KP11";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
      
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        

        pnladd.Visible = true;
        txtKeterangan.Text = "-";
        Pnlfooter4.Visible = true;

     
    }
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterKriteriaSumberDana.aspx");
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
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTKRITERIA", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("KP_Mstr_Kriteria_SumberDana", var, "constrmf");      


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTSUMBERDANA", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_Mstr_Kriteria_SumberDana", var1, "constrmf");
        ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberdana.DataTextField = "namabank";
        ddlsumberdana.DataValueField = "KodeBank";
        ddlsumberdana.DataBind();
        ddlsumberdana.Items.Insert(0, new ListItem("Pilih Data", "-"));

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTKANTORBAYAR", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_Mstr_Kriteria_SumberDana", var2, "constrmf");
        ddlkantorbayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkantorbayar.DataTextField = "namabank";
        ddlkantorbayar.DataValueField = "KodeBank";
        ddlkantorbayar.DataBind();
        ddlkantorbayar.Items.Insert(0, new ListItem("Pilih Data", "-"));

        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
        var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTAREA", typeof(string));
        var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var3[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_Mstr_Kriteria_SumberDana", var3, "constrmf");
        ddlarea.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlarea.DataTextField = "nama_area";
        ddlarea.DataValueField = "kode_area";
        ddlarea.DataBind();
        ddlarea.Items.Insert(0, new ListItem("Pilih Data", "-"));

        ddljangkawaktu.Items.Insert(0, new ListItem("Pilih Data", "-"));
        for (int i = 30; i >= 1; i--)
        {
            ddljangkawaktu.Items.Insert(1, new ListItem(i.ToString(),i.ToString()));
        }        
        
        ddlusia.Items.Insert(0, new ListItem("Pilih Data", "-"));
        for (int i = 90; i >=30; i--)
        {
            ddlusia.Items.Insert(1, new ListItem(i.ToString(),i.ToString()));
        }

    }


    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "SETDEL":

                try
                {
                    txtrate.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblarea")).Text;
                    ddlsumberdana.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesumberdana")).Text;
                    ddlkantorbayar.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkantorbayar")).Text;
                    txtplafon.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text;
                    txtrate.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrate")).Text;
                    ddljangkawaktu.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangkawaktu")).Text;
                    ddlusia.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblusia")).Text;
                    string flag = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeflag")).Text;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketerangan")).Text;
                    ddlflag.SelectedValue = (flag == "True" ? "1" : "0");
                    ddlarea.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodearea")).Text;
                    PnlFront.Visible = false;
                    pnlHeader.Visible = false;
                    pnladd.Visible = true;
                    Pnlfooter4.Visible = true;
                    btnDelete.Visible = true;
                    btnSimpan.Visible = false;
                    btnUpdate.Visible = false;
                    pnladd.Enabled = false;

                }
                catch(Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
                    gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;
            case "SET":
                try
                {
                    txtrate.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblarea")).Text;
                    ddlsumberdana.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesumberdana")).Text;
                    ddlkantorbayar.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkantorbayar")).Text;
                    txtplafon.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text;
                    txtrate.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrate")).Text;
                    ddljangkawaktu.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangkawaktu")).Text;
                    ddlusia.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblusia")).Text;
                    string flag = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeflag")).Text;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketerangan")).Text;
                    ddlarea.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodearea")).Text;
                    ddlarea.Enabled = false;
                    ddlsumberdana.Enabled = false;
                    ddlflag.SelectedValue = (flag == "True" ? "1" : "0") ;
                    PnlFront.Visible = false;
                    pnlHeader.Visible = false;
                    pnladd.Visible = true;
                    Pnlfooter4.Visible = true;
                    btnUpdate.Visible = true;
                    btnSimpan.Visible = false;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
                    gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));


                }
                break;

        }
    }

    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {



            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEKRITERIA", typeof(string));
            var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[3] = gb.CreateSPParameter("@area", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlarea.SelectedValue, typeof(string));
            var1[4] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_Mstr_Kriteria_SumberDana", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);

            }


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

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
  

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0;
    


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[12];
            Params[0] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.SelectedValue, typeof(string));
            Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Params[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTKRITERIA", typeof(string));
            Params[3] = gb.CreateSPParameter("@area", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlarea.SelectedValue, typeof(string));
            Params[4] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafon.Text, typeof(decimal));
            Params[5] = gb.CreateSPParameter("@jangkawaktu", ParameterDirection.Input, SqlDbType.Int, 0, ddljangkawaktu.SelectedValue, typeof(int));
            Params[6] = gb.CreateSPParameter("@rate", ParameterDirection.Input, SqlDbType.Int, 0, txtrate.Text, typeof(int));
            Params[7] = gb.CreateSPParameter("@usia", ParameterDirection.Input, SqlDbType.Int, 0, ddlusia.SelectedValue, typeof(int));
            Params[8] = gb.CreateSPParameter("@kantorbayar", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkantorbayar.SelectedValue, typeof(string));
            Params[9] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKeterangan.Text, typeof(string));
            Params[10] = gb.CreateSPParameter("@flag", ParameterDirection.Input, SqlDbType.Bit, 2, Convert.ToByte(ddlflag.SelectedValue), typeof(bool));
            Params[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("KP_Mstr_Kriteria_SumberDana", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);

            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnupdate_click(object sender, EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[12];
            Params[0] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.SelectedValue, typeof(string));
            Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Params[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEKRITERIA", typeof(string));
            Params[3] = gb.CreateSPParameter("@area", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlarea.SelectedValue, typeof(string));
            Params[4] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafon.Text, typeof(decimal));
            Params[5] = gb.CreateSPParameter("@jangkawaktu", ParameterDirection.Input, SqlDbType.Int, 0, ddljangkawaktu.SelectedValue, typeof(int));
            Params[6] = gb.CreateSPParameter("@rate", ParameterDirection.Input, SqlDbType.Int, 0, txtrate.Text, typeof(int));
            Params[7] = gb.CreateSPParameter("@usia", ParameterDirection.Input, SqlDbType.Int, 0, ddlusia.SelectedValue, typeof(int));
            Params[8] = gb.CreateSPParameter("@kantorbayar", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkantorbayar.SelectedValue, typeof(string));
            Params[9] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKeterangan.Text, typeof(string));
            Params[10] = gb.CreateSPParameter("@flag", ParameterDirection.Input, SqlDbType.Bit, 2, Convert.ToByte(ddlflag.SelectedValue), typeof(bool));
            Params[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("KP_Mstr_Kriteria_SumberDana", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnUpdate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Update Done!','zzzzzz','bottom-center','success','1');", true);

            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
}
