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

public partial class mikro_faktorbm : System.Web.UI.Page
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
                //pnlFront.Visible = true;
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";

                InsertCombo();

                BindGridEntity();
                //GRUser.Visible = true;
                //GRUser.UseAccessibleHeader = true;
                //GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            string STATUSCABANG = "";
            if((SessBranchID == "000" && ddlcabangx.SelectedValue == "") || ddlcabangx.SelectedValue == "000")
            {
                STATUSCABANG = "SELECTALL";
            }
            else
            {
                STATUSCABANG = "SELECTALLBYCABANG";
            }
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATUSCABANG, typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangx.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeFaktorBM", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

            GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRUser.DataBind();

            GRUser.UseAccessibleHeader = true;
            GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;
            string jumlah = Convert.ToString(GRUser.Rows.Count);
            if(ddlcabangx.SelectedValue != "" && jumlah == "0")
            {
                string tes = ddlcabangx.Text.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Pada Cabang " + ddlcabangx.SelectedItem + " belum ada. Silahkan Add Faktor BM Cabang tersebut.');", true);
                ddlcabangx.SelectedValue = "";
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
        try
        {
            pnlFront.Visible = false;
            pnlFilter.Visible = true;

            ////hdnsave.Value = "true";
            //ddlcabang.Visible = true;
            //ddljabatan.Visible = true;
            ddlcabang.Enabled = true;
            ddljabatan.Enabled = true;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "GRUPEDT":
                string query;
                query = "";
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GRUser.Rows[index];
                    hdnsave.Value = "false";
                    ddlcabang.SelectedValue = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    ddljabatan.SelectedValue = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejabatan")).Text;
                    txtfaktor.Text=((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblFaktornilai")).Text;

                    ddlcabang.Enabled = false;
                    ddljabatan.Enabled = false;
                    pnlFront.Visible = false;
                    pnlFilter.Visible = true;
                    hdnsave.Value = "false";
                break;

            case "GRUPSET":
                try
                {
                    ddljabatan.SelectedValue = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejabatan")).Text;
                    ddlcabang.SelectedValue = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
                    var[0] = gb.CreateSPParameter("@KodeJab", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljabatan.SelectedValue, typeof(string));
                    var[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                    var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@Faktornilai", ParameterDirection.Input, SqlDbType.Money, 0, txtfaktor.Text, typeof(decimal));
                    var[4] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[5] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AE_FeeFaktorBM", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Enabled = false;
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                BindGridEntity();
               
                break;
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

    #region "Button"
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (ddljabatan.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@KodeJab", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljabatan.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@Faktornilai", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(txtfaktor.Text), typeof(decimal));
            var[4] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[5] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeFaktorBM", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void lnkRefreshModal_Click(object sender, EventArgs e)
    {
        BindGridEntity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected string btnremodal()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("faktorbm.aspx");
    }
    #endregion "Button"

    #region"onchang"
    protected void ddlcabangx_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridEntity();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion"onchange"

    #region "Insertdata"
    public void InsertCombo()
    {
        try
        {
            // --- Combo Kode Cabang
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeFaktorBM", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

            ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabang.DataTextField = "nama";
            ddlcabang.DataValueField = "KodeCabang";
            ddlcabang.DataBind();
            ddlcabang.Items.Insert(0, new ListItem("Pilih Cabang", ""));

            ddlcabangx.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabangx.DataTextField = "nama";
            ddlcabangx.DataValueField = "KodeCabang";
            ddlcabangx.DataBind();
            ddlcabangx.Items.Insert(0, new ListItem("Pilih Cabang", ""));

            // --- Combo Kode Jabatan
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJABATAN", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeFaktorBM", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

            ddljabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljabatan.DataTextField = "nama";
            ddljabatan.DataValueField = "KodeJab";
            ddljabatan.DataBind();
            ddljabatan.Items.Insert(0, new ListItem("Pilih Jabatan", ""));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion "Insertdata"
}