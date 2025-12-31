using DefaultClass;
using EntitiesClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_status_cabang : System.Web.UI.Page
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
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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
                int year = DateTime.Now.Year;
                for (int i = year - 1; i <= year + 1; i++)
                {
                    ListItem li = new ListItem(i.ToString());
                    ddltahun.Items.Add(li);
                }
                ddltahun.Items.FindByText(year.ToString()).Selected = true;

                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKOPERASI", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("View_Status_Cabang", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";
                ddlKoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKoperasi.DataTextField = "nama_koperasi";
                ddlKoperasi.DataValueField = "kode_koperasi";
                ddlKoperasi.DataBind();
                ddlKoperasi.Items.Insert(0, new ListItem("Pilih Koperasi", "-"));

                var Koperasi = ddlKoperasi.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                var[0] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "nama asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "Text";
                ddlcabang.DataValueField = "value";
                ddlcabang.DataBind();
                ddlcabang.Items.Insert(0, new ListItem("Pilih Cabang", "-"));
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlKoperasi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var Koperasi = ddlKoperasi.SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
            ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabang.DataTextField = "nama_cabang";
            ddlcabang.DataValueField = "kode_cabang";
            ddlcabang.DataBind();
            ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));
            if (ddlKoperasi.SelectedValue == "-")
            {
                ddlcabang.SelectedValue = "-";
                ddlcabang.Enabled = false;
            }
            else
            {
                ddlcabang.SelectedValue = "-";
                ddlcabang.Enabled = true;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlcabang.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Pilih Cabang Terlebih dahulu!!');", true);
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbulan.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 30, ddltahun.SelectedValue, typeof(string));
                var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STATUSCABANG", typeof(string));
                var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("View_Status_Cabang", var, "constrmf");

                if (ge.ListDataset.Tables.Count < 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Data Status Cabang " + ddlcabang.SelectedItem.Text + " Pada Bulan " + ddlbulan.SelectedItem.Text + " Tahun " + ddltahun.SelectedItem.Text + " Belum Ada Data.');", true);
                }
                else
                {
                    pnlgrid.Visible = true;
                    pnladd.Visible = true;
                    titlegrid.InnerText = "Data View Status Cabang " + ddlcabang.SelectedItem.Text;
                    GRDD.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    GRDD.DataBind();

                    GRDD.UseAccessibleHeader = true;

                    GRDD.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
}