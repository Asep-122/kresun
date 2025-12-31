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

public partial class MasterTemplate_BrachAktif : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindGridEntity();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    //protected void lnkSaveGR_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dtdetail = new DataTable("usergroupmenu");

    //        dtdetail.Columns.Clear();
    //        dtdetail.Columns.Add(new DataColumn("ID", typeof(Int32)));
    //        dtdetail.Columns.Add(new DataColumn("EmpID", typeof(string)));
    //        dtdetail.Columns.Add(new DataColumn("usrupd", typeof(string)));

    //        for (int i = 0; i < DGGroup.Rows.Count; i++)
    //        {
    //            if (((CheckBox)DGGroup.Rows[i].FindControl("chklist")).Checked)
    //            {

    //                DataRow drdetail = dtdetail.NewRow();

    //                drdetail["ID"] = Int32.Parse(((Label)DGGroup.Rows[i].FindControl("lblIdGroup")).Text);
    //                drdetail["EmpID"] = lblEMPID.Text;
    //                drdetail["usrupd"] = SessLoginID;

    //                dtdetail.Rows.Add(drdetail);

    //            }
    //        }

    //        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
    //        Params[0] = gb.CreateSPParameter("@userID", ParameterDirection.Input, SqlDbType.VarChar, 50, lblEMPID.Text, typeof(string));
    //        Params[1] = gb.CreateSPParameter("@MF_Type_UserGroup", ParameterDirection.Input, SqlDbType.Structured, 50, dtdetail, typeof(string));
    //        Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("MF_spInsertUserGroupdetail", Params, "constrmf");

    //        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //        {

    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);

    //        }

    //        else
    //        {

    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //        }


    //    }

    //    catch (System.Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //    }
    //}

    public void BindGridEntity()
    {
        if (SessIsSuperAdmin == true)
        {
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            //Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            //Params[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Params[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 0, "SELECTAKTIFADMIN", typeof(String));
            Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("MF_Branchaktif", Params, "constrmf");

            GRUser.DataSource = ge.ListDataset.Tables[0];
            GRUser.DataBind();


            GRUser.UseAccessibleHeader = true;
            GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Params[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 0, "SELECTAKTIF", typeof(String));
            Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("MF_Branchaktif", Params, "constrmf");

            GRUser.DataSource = ge.ListDataset.Tables[0];
            GRUser.DataBind();


            GRUser.UseAccessibleHeader = true;
            GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void username_change(object sender, EventArgs e)
    {
        datadropdowncabang();
        ddlbranch.Enabled = true;
        
    }
    public void datadropdowncabang()
    {
        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
        var2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //var2[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var2[1] = gb.CreateSPParameter("@UserID", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlUsername.SelectedValue, typeof(string));
        var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANGAKTIF", typeof(string));
        var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("MF_Branchaktif", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlbranch, "None", "");
        ddlbranch.Items.Insert(0, new ListItem("-", "-"));
         

    }
    public void datadropdown()
    {
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //var1[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTUSERAKTIF", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            
        ge.ListDataset = gb.GetDataJustOutPut("MF_Branchaktif", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlUsername, "None", "");
        ddlUsername.Items.Insert(0, new ListItem("-", "-"));

    }

    public void datacabang()
    {
        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //var2[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //var2[1] = gb.CreateSPParameter("@UserID", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlUsername.SelectedValue, typeof(string));
        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("MF_Branchaktif", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlbranch, "None", "");
        ddlbranch.Items.Insert(0, new ListItem("-", "-"));
    }
    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        //ddlUsername.Text = SessLoginID;
        datadropdown();

        datacabang();

        pnlFront.Visible = false;
        pnlUserEdit.Visible = true;
    }

    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "GRUPEDT":

                //txtUsername.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblempid")).Text;

                //datadropdown();
                //ListItem lstItem = ddlbranch.Items.FindByValue(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBranch")).Text);
                //ddlUsername.SelectedValue = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblempname")).ToString();
                string asafaf = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblempname")).Text;
                ddlUsername.Items.Insert(0, new ListItem(asafaf, asafaf));
                ddlUsername.Enabled = false;
                datadropdowncabang();
                string ambilcabang = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBranch")).Text;
                ddlbranch.SelectedValue = ambilcabang;
                ddlbranch.Enabled = true;
                //if (lstItem != null)
                //{
                //    ddlbranch.SelectedValue = lstItem.Value;
                //}
                ddlbranch.Enabled = true;
                pnlFront.Visible = false;
                pnlUserEdit.Visible = true;



                break;
            //case "GRUPSET":

            //    //lblEMPID.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblempid")).Text;
            //    //lblEMPNAME.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblempname")).Text;

            //    //BindGridEntityGroup();

            //    GRUser.UseAccessibleHeader = true;
            //    GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
            //    break;
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

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            if(ddlUsername.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pilih User Untuk Anda Setting..!!');", true);
            }
            else if(ddlbranch.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pilih Barnch..!!');", true);
            }
            else
            {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTUSERAKTIF", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("MF_Branchaktif", var1, "constrmf").Tables[0];

            if(ge.ListDataTable.Rows.Count < 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Sudah ada, pilih user lain');", true);
            }
            else
            {
                if (ddlUsername.Enabled == false)
                {

                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
                    Params[0] = gb.CreateSPParameter("@UserName", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlUsername.SelectedValue, typeof(string));
                    Params[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 35, ddlbranch.SelectedValue, typeof(string));
                    Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 35, SessKdpersh, typeof(string));
                    Params[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 35, "UPDATEAKTIF", typeof(string));
                    Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("MF_Branchaktif", Params, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
                else
                {
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
                    Params[0] = gb.CreateSPParameter("@UserName", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlUsername.SelectedValue, typeof(string));
                    Params[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 35, ddlbranch.SelectedValue, typeof(string));
                    Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 35, SessKdpersh, typeof(string));
                    Params[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 35, "INSERTAKTIF", typeof(string));
                    Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("MF_Branchaktif", Params, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

                    }

                    else
                    {
                        string err = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
            }
            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
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

    //protected void lnkRefreshModal_Click(object sender, EventArgs e)
    //{

    //    BindGridEntity();
    //   // BindGridEntityGroup();
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    //}

    //protected string btnremodal()
    //{
    //    return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    //}
}