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

public partial class SN_Image_View : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    Global gb1 = new DefaultClass.Global();
    GlobalEntities ge1 = new GlobalEntities();
    Global gb2 = new DefaultClass.Global();
    GlobalEntities ge2 = new GlobalEntities();
    Global gb3 = new DefaultClass.Global();
    GlobalEntities ge3 = new GlobalEntities();
    Global gb4 = new DefaultClass.Global();
    GlobalEntities ge4 = new GlobalEntities();
    Global gb5 = new DefaultClass.Global();
    GlobalEntities ge5 = new GlobalEntities();
    Global gb6 = new DefaultClass.Global();
    GlobalEntities ge6 = new GlobalEntities();

    string tanggal = "";
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
                for (int i = year - 15; i <= year + 15; i++)
                {
                    ListItem li = new ListItem(i.ToString());
                    ddltahun.Items.Add(li);
                }
                ddltahun.Items.FindByText(year.ToString()).Selected = true;

              
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
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, "000" , typeof(string));
            var[1] = gb.CreateSPParameter("@month", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbulan.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@year", ParameterDirection.Input, SqlDbType.VarChar, 30, ddltahun.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("View_image_Database_mikro", var, "constrmf");

            if (ge.ListDataset.Tables.Count < 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:simpan('Data Jurnal Berhasil Disimpan Pada Bulan Ini.');", true);
            }

            else
            {
                pnlgrid.Visible = true;
                pnladd.Visible = true;
                titlegrid.InnerText = "Data View Image";
                GRDD.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GRDD.DataBind();

                GRDD.UseAccessibleHeader = true;

                GRDD.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void GRDD_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "SET":
                try
                {
                    pnldata.Visible = false;
                    pnlgrid.Visible = true;
                    txtnonasabah.Text = ((Label)GRDD.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtnama.Text = ((Label)GRDD.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                    txtjenis.Text = ((Label)GRDD.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljenis")).Text;
                    txtproses.Text = ((Label)GRDD.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsimpanan")).Text;
                    txtpath1.Text = ((Label)GRDD.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblimage1")).Text;
                    txtpath2.Text = ((Label)GRDD.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblimage2")).Text;
                    Image1.ImageUrl = Server.MapPath(txtpath1.Text);
                    Image2.ImageUrl = Server.MapPath(txtpath2.Text);

                    //LinkButton lnkbtnTest = GRDD.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnedit") as LinkButton;

                    //btnedit.Text = dsRequiredDAta.rows[rowindex]["testvalue"].toString();
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;
        }
    }
}