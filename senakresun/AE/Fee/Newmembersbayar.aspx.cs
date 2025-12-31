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

public partial class mikro_Newmembersbayar : System.Web.UI.Page
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
    private Boolean SessIsSuperAdmin
    {
        get { return Boolean.Parse(Session["SessIsSuperAdmin"].ToString()); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }
    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessIsLevel
    {
        get { return Session["SessIsLevel"].ToString(); }
        set { ViewState["SessIsLevel"] = value; }
    }
    public string SessAreaUser
    {
        get { return Session["SessAreaUser"].ToString(); }
        set { ViewState["SessAreaUser"] = value; }
    }
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";

                InsertCombo();
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
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTALL", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@Kodesales", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlmarketing.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeRewardSmBulan", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_drop asc";

            GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRUser.DataBind();

            GRUser.UseAccessibleHeader = true;
            GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;
            string jumlah = Convert.ToString(GRUser.Rows.Count);
            if (ddlcabang.SelectedValue != "" && jumlah == "0" && ddlmarketing.SelectedValue != "" )
            {
                string tes = ddlcabang.Text.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Pada Cabang " + ddlcabang.SelectedItem + " dengan marketing " + ddlmarketing.SelectedItem + " Belum ada.');", true);
                ddlcabang.SelectedValue = "";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlFront.Visible = false;
        ddlcabang.Enabled = true;
    }

    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "GRUPEDT":
                    string query;
                    query = "";
                    //int index = Convert.ToInt32(e.CommandArgument);
                    //GridViewRow row = GRUser.Rows[index];
                    //String constr = ConfigurationManager.ConnectionStrings["postgresmikro"].ConnectionString;
                    //NpgsqlConnection con = new NpgsqlConnection(constr);
                    //con.Open();

                    string plafon;
                    string reward;
                    string no_rekening;
                    string no_nasabah;
                    string nama_nasabah;
                    no_rekening = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_rekening")).Text;
                    no_nasabah = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_nasabah")).Text;
                    nama_nasabah = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama_nasabah")).Text;
                    //tgldrop = (Convert.ToDateTime((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgl_drop")));
                    plafon = Convert.ToString(Convert.ToDecimal(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text));
                    reward = Convert.ToString(Convert.ToDecimal(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblreward")).Text));


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[10];
                    var[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, no_rekening, typeof(string));
                    var[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                    var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlmarketing.SelectedValue, typeof(string));
                    var[4] = gb.CreateSPParameter("@plafond", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(plafon), typeof(decimal));
                    var[5] = gb.CreateSPParameter("@reward", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(reward), typeof(decimal));
                    var[6] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[7] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[8] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                    var[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AE_FeeRewardSmBulan", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        //btnSimpan.Enabled = false;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Bayar Success!','You successfully save this Project','top-right','success','1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }

                    //query = " insert into tbl_reward_sm_bulan_bayar";
                    //query = query + "  (norekening, kode_cabang, nik, plafon, reward, user_insert,tgl_insert) values ";
                    //query = query + "(";
                    //query = query + "'" + no_rekening + "',";
                    //query = query + "'" + ddlcabang.SelectedValue + "',";
                    //query = query + "'" + ddlmarketing.SelectedValue + "',";
                    //query = query + "'" + plafon.Replace(",", ".") + "',";
                    //query = query + "'" + reward.Replace(",", ".") + "',";
                    //query = query + "'" + SessLoginID + "',";
                    //query = query + "now())";
                    //NpgsqlCommand command = new NpgsqlCommand(query, con);
                    //command.ExecuteReader();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bayar Success!','You successfully save this Project','top-right','success','1');", true);
                    //BindGridEntity();
                    break;

            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
        BindGridEntity();
    }
    protected void GRUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }
    }

    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {


        ////if (ddlcabang.SelectedValue != "ALL")

        ////{
        //string query;
        //query = "";
        //query = "select nik ,nik || ' - ' || nama as nama  from tbl_karyawanx x where x.group='A' and x.kode_cabang='" + ddlcabang.SelectedValue + "'";
        ////query = "select * from aspnet_report_marketing_mikro_kode_ref('" + ddlcabang.SelectedValue + "')";
        //DataTable dx = GetDataMikro(query);
        //ddlmarketing.DataSource = dx;
        //ddlmarketing.DataTextField = "nama";
        //ddlmarketing.DataValueField = "nik";
        //ddlmarketing.DataBind();
        ////ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));
        //ddlmarketing.Enabled = true;

        ////}
        ////else
        ////{
        ////    ddlmarketing.SelectedValue = "ALL";
        ////    ddlmarketing.Enabled = false;

        ////}
        try
        {

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void lnkRefreshModal_Click(object sender, EventArgs e)
    {
        BindGridEntity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected string btnremodal()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("faktorbm.aspx");
    }

    #region "Insertdata"
    public void InsertCombo()
    {
        try
        {
            // --- Combo Kode Cabang
            DefaultClass.Global.SqlHelper[] varB = new DefaultClass.Global.SqlHelper[3];
            varB[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varB[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
            varB[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //varB[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeRewardSmBulan", varB, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

            ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabang.DataTextField = "nama";
            ddlcabang.DataValueField = "KodeCabang";
            ddlcabang.DataBind();
            ddlcabang.Items.Insert(0, new ListItem("Pilih Cabang", ""));

            // --- Combo Kode Marketing
            DefaultClass.Global.SqlHelper[] varM = new DefaultClass.Global.SqlHelper[4];
            varM[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varM[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMARKETING", typeof(string));
            varM[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varM[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeRewardSmBulan", varM, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "nik asc";

            ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlmarketing.DataTextField = "nama";
            ddlmarketing.DataValueField = "nik";
            ddlmarketing.DataBind();
            ddlmarketing.Items.Insert(0, new ListItem("Pilih Marketing", ""));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion "Insertdata"
}