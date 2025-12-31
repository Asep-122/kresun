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

public partial class MasterTemplate_User : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    private Boolean SessIsSuperAdmin
    {
        get { return Boolean.Parse(Session["SessIsSuperAdmin"].ToString()); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[1];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetBranch", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlbranch, "None", "");
                ddlbranch.Items.Insert(0, new ListItem("-", "-"));

                BindGridEntity();
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


        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("MF_GetUser", Params, "constrmf");

        GRUser.DataSource = ge.ListDataset.Tables[0];
        GRUser.DataBind();


        GRUser.UseAccessibleHeader = true;
        GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlFront.Visible = false;
        pnlUserEdit.Visible = true;
    }

    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "GRUPEDT":

                txtUsername.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblempid")).Text;
                txtName.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblempname")).Text;


                ListItem lstItem = ddlbranch.Items.FindByValue(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBranch")).Text);
                if (lstItem != null)
                {
                    ddlbranch.SelectedValue = lstItem.Value;
                }

                txtMail.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblEmail")).Text;

                txtPass1.Attributes.Add("value", gb.Decrypt(((HiddenField)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("hdnPassword")).Value.Trim()));
                txtpassc.Attributes.Add("value", gb.Decrypt(((HiddenField)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("hdnPassword")).Value.Trim()));


                chkIsActive.Checked = ((CheckBox)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("chkIsActive")).Checked;

                pnlFront.Visible = false;
                pnlUserEdit.Visible = true;



                break;
            case "GRUPSET":

                lblEMPID.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblempid")).Text;
                lblEMPNAME.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblempname")).Text;

                BindGridEntityGroup();

                GRUser.UseAccessibleHeader = true;
                GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                break;
        }
    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {

        BindGridEntity();

    }

    public void BindGridEntityGroup()
    {
        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@UserID", ParameterDirection.Input, SqlDbType.VarChar, 50, lblEMPID.Text, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("MF_spAllGroupMenu", Params, "constrmf");

        DGGroup.DataSource = ge.ListDataset.Tables[0];
        DGGroup.DataBind();

        DGGroup.UseAccessibleHeader = true;
        DGGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void GRUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (((HiddenField)e.Row.FindControl("hdnIslocal")).Value == "0")
            {
                ((LinkButton)e.Row.FindControl("lnkEditUser")).Visible = false;
            }
            if (((HiddenField)e.Row.FindControl("hdnIsSuperAdmin")).Value == "1")
            {
                ((LinkButton)e.Row.FindControl("lnkGR")).Visible = false;
            }

        }
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            if (gb.Encrypt(txtPass1.Text.Trim()) != gb.Encrypt(txtpassc.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Password Tidak Sama');", true);
                return;
            }
            else
            {
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[8];
                Params[0] = gb.CreateSPParameter("@UserName", ParameterDirection.Input, SqlDbType.VarChar, 30, txtUsername.Text, typeof(string));
                Params[1] = gb.CreateSPParameter("@Password", ParameterDirection.Input, SqlDbType.VarChar, 35, gb.Encrypt(txtPass1.Text), typeof(string));
                Params[2] = gb.CreateSPParameter("@Name", ParameterDirection.Input, SqlDbType.VarChar, 50, txtName.Text, typeof(string));
                Params[3] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 30, txtMail.Text, typeof(string));
                Params[4] = gb.CreateSPParameter("@BranchID", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlbranch.SelectedValue, typeof(string));
                Params[5] = gb.CreateSPParameter("@Usrupd", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                Params[6] = gb.CreateSPParameter("@IsActive", ParameterDirection.Input, SqlDbType.Bit, 0, chkIsActive.Checked, typeof(Boolean));
                Params[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));


                ge.ListDataset = gb.GetDataJustOutPut("MF_spInsertUpdateUserLocal", Params, "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Berhasil Disimpan');", true);
                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void lnkSaveGR_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtdetail = new DataTable("usergroupmenu");

            dtdetail.Columns.Clear();
            dtdetail.Columns.Add(new DataColumn("ID", typeof(Int32)));
            dtdetail.Columns.Add(new DataColumn("EmpID", typeof(string)));
            dtdetail.Columns.Add(new DataColumn("usrupd", typeof(string)));

            for (int i = 0; i < DGGroup.Rows.Count; i++)
            {
                if (((CheckBox)DGGroup.Rows[i].FindControl("chklist")).Checked)
                {

                    DataRow drdetail = dtdetail.NewRow();

                    drdetail["ID"] = Int32.Parse(((Label)DGGroup.Rows[i].FindControl("lblIdGroup")).Text);
                    drdetail["EmpID"] = lblEMPID.Text;
                    drdetail["usrupd"] = SessLoginID;

                    dtdetail.Rows.Add(drdetail);

                }
            }

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
            Params[0] = gb.CreateSPParameter("@userID", ParameterDirection.Input, SqlDbType.VarChar, 50, lblEMPID.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@MF_Type_UserGroup", ParameterDirection.Input, SqlDbType.Structured, 50, dtdetail, typeof(string));
            Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("MF_spInsertUserGroupdetail", Params, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Berhasil Disimpan');", true);
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }


        }

        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void lnkRefreshModal_Click(object sender, EventArgs e)
    {

        BindGridEntity();
        BindGridEntityGroup();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    protected string btnremodal()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    }
}