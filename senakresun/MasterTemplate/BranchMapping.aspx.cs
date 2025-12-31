using DefaultClass;
using EntitiesClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterTemplate_BranchMapping : System.Web.UI.Page
{
    string tanggal = "";
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
                //DefaultClass.Global.SqlHelper[] dlrMNC = new DefaultClass.Global.SqlHelper[0];

                //ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetBranch", dlrMNC, "constrmf");

                //ge.ListDataset.Tables[0].DefaultView.Sort = "[Text] asc";

                //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlbranch, "None", "");

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

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Params[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 20, "ALLDATA", typeof(string));
        Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("MF_BranchMapping", Params, "constrmf");
        ge.ListDataTable = gb.GetDataJustOutPut("MF_BranchMapping", Params, "constrmf").Tables[0];
        
        GRUser.DataSource = ge.ListDataset.Tables[0];
        
        GRUser.DataBind();

        

        GRUser.UseAccessibleHeader = true;
        GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        lblEMPNAME.Text = SessLoginID;
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTUSER", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("MF_BranchMapping", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), lblEMPID, "None", "");
        lblEMPID.Items.Insert(0, new ListItem("-", "-"));
        //lblEMPNAME.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblUpd")).Text;

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Params[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(String));
        Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("MF_BranchMapping", Params, "constrmf");
        DGGroup.DataSource = ge.ListDataset.Tables[0];
        DGGroup.DataBind();

        DGGroup.UseAccessibleHeader = true;
        DGGroup.HeaderRow.TableSection = TableRowSection.TableHeader;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        //pnlFront.Visible = false;
        //pnlUserEdit.Visible = true;
    }

    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "GRUPEDT":


                string ambilddlid = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblUpd")).Text;
                string ambilddlid1 = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
                Params[0] = gb.CreateSPParameter("@Username", ParameterDirection.Input, SqlDbType.VarChar, 20, ambilddlid1, typeof(string));
                Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Params[2] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "AMBILCABANG2", typeof(String));
                Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //Params[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("MF_BranchMapping", Params, "constrmf");
                ge.ListDataTable = gb.GetDataJustOutPut("MF_BranchMapping", Params, "constrmf").Tables[0];
                DGGroup.DataSource = ge.ListDataset.Tables[0];
                DGGroup.DataBind();

                DGGroup.UseAccessibleHeader = true;
                DGGroup.HeaderRow.TableSection = TableRowSection.TableHeader;

                lblEMPNAME.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblUpd")).Text;
                lblEMPID.Items.Insert(0, new ListItem(ambilddlid1, ambilddlid1));
                lblEMPID.Enabled = false;

                //int ambildata = ge.ListDataTable.Rows.Count;
                //for (int i = 0; ambildata > i; i++)
                //{
                //    if(ge.ListDataset.Tables[0].Rows[i]["checkbranch"].ToString() == "1")
                //    {
                //        ((CheckBox)DGGroup.Rows[i].FindControl("chklist")).Checked = Convert.ToBoolean(true);
                //    }
                //    else if(ge.ListDataset.Tables[0].Rows[i]["checkbranch"].ToString() == "0")
                //    {
                //        ((CheckBox)DGGroup.Rows[i].FindControl("chklist")).Checked = Convert.ToBoolean(false);
                //    }
                    
                //    //bool cobamasuk = 
                    
                //}
                pnlFront.Visible = true;
                // pnlUserEdit.Visible = true;


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
        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
        Params[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Params[2] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(String));
        Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("MF_BranchMapping", Params, "constrmf");

        DGGroup.DataSource = ge.ListDataset.Tables[0];
        DGGroup.DataBind();

        DGGroup.UseAccessibleHeader = true;
        DGGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void GRUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //if (((HiddenField)e.Row.FindControl("hdnIslocal")).Value == "0")
            //{
            //    ((LinkButton)e.Row.FindControl("lnkEditUser")).Visible = false;
            //}
            //if (((HiddenField)e.Row.FindControl("UserID")).Value == "1")
            //{
            //    ((LinkButton)e.Row.FindControl("lnkGR")).Visible = true;
            //}

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnclose, string.Empty, true);
    }

    //function simpan
    protected void lnkSaveGR_Click(object sender, EventArgs e)
    {
        try
        {
            string State = "";
            if (lblEMPID.Enabled == true) 
            {
                State = "INSERT";
            }
            else
            {
                State = "UPDATE";
            }
            DateTime now = DateTime.Now;
            tanggal = now.ToString("yyyy/MM/dd HH:MM:ss");
            DataTable dtRP = new DataTable("Transaction");
            dtRP.Columns.Clear();

            dtRP.Columns.Add(new DataColumn("UserID", typeof(string)));
            dtRP.Columns.Add(new DataColumn("KodeCabang", typeof(string)));
            dtRP.Columns.Add(new DataColumn("kdpersh", typeof(string)));
            dtRP.Columns.Add(new DataColumn("UsrUpd", typeof(string)));
            dtRP.Columns.Add(new DataColumn("DtmUpd", typeof(DateTime)));

            dtRP.Rows.Clear();


            for (int i = 0; i < DGGroup.Rows.Count; i++)
            {
                DataRow dr = dtRP.NewRow();
                if (((CheckBox)DGGroup.Rows[i].FindControl("chklist")).Checked == true)
                { 
                     dr["UserID"] = lblEMPID.Text;
                     dr["KodeCabang"] = ((Label)DGGroup.Rows[i].FindControl("lblkodecabang")).Text;
                     dr["kdpersh"] = SessKdpersh;
                     dr["UsrUpd"] = SessLoginID;
                     dr["DtmUpd"] = Convert.ToDateTime(tanggal);

                     dtRP.Rows.Add(dr);
                }
            }
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@UserID", ParameterDirection.Input, SqlDbType.VarChar, 50, lblEMPID.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@MF_BRANCH_MAPPING_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
            var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "INSERT", typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            if (lblEMPID.SelectedValue != null)
            {
                ge.ListDataset = gb.GetDataJustOutPut("MF_BranchMapping", var, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("MF_BranchMapping", var, "constrmf");
            }
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
            //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[11];

            //Params[0] = gb.CreateSPParameter("@NoBuktiKns", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkdsupplier.SelectedValue, typeof(string));
            //Params[1] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypetrans.SelectedValue, typeof(string));
            //Params[2] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlCurrency.SelectedValue, typeof(string));
            //Params[3] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.Decimal, 0, txtKurs.Text, typeof(decimal));
            //Params[4] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKet.Text, typeof(string));
            //Params[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglTransaksi.Text, typeof(DateTime));
            //Params[6] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            //Params[7] = gb.CreateSPParameter("@Nourut", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
            //Params[8] = gb.CreateSPParameter("@AP_Transaction_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
            //Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //Params[10] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            //DateTime now = DateTime.Now;
            //tanggal = now.ToString("yyyy/MM/dd HH:MM:ss");
            //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            //var1[0] = gb.CreateSPParameter("@UserID", ParameterDirection.Input, SqlDbType.VarChar, 50, lblEMPID.SelectedValue, typeof(string));
            //var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "SELECTCABANGAKTIF", typeof(string));
            //var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var1[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            //var1[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh.ToString(), typeof(string));
            //ge.ListDataTable = gb.GetDataJustOutPut("MF_BranchMapping", var1, "constrmf").Tables[0];
            //if(ge.ListDataTable.Rows.Count < 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Baru Akan disimpan');", true);
            //}
            //else
            //{
            //    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
            //    var2[0] = gb.CreateSPParameter("@UserID", ParameterDirection.Input, SqlDbType.VarChar, 50, lblEMPID.SelectedValue, typeof(string));
            //    var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "DELETECABANGMAPPING", typeof(string));
            //    var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //    ge.ListDataTable = gb.GetDataJustOutPut("MF_BranchMapping", var2, "constrmf").Tables[0];
            //}
            //for (int i = 0; i < DGGroup.Rows.Count; i++)
            //{
            //    CheckBox txtb = (DGGroup.Rows[i].FindControl("chklist")) as CheckBox;
            //    //Boolean v = Convert.ToBoolean(txtb);
            //    if (txtb.Checked == true)
            //    {
            //        Label txta = ((Label)DGGroup.Rows[i].FindControl("lblkodecabang")) as Label;
            //        string a = txta.Text;
            //        string b = lblEMPNAME.Text;
            //        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            //        var[0] = gb.CreateSPParameter("@UserID", ParameterDirection.Input, SqlDbType.VarChar, 50, lblEMPID.Text, typeof(string));
            //        var[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 5, a, typeof(string));
            //        var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh.ToString(), typeof(string));
            //        var[3] = gb.CreateSPParameter("@UsrUpd", ParameterDirection.Input, SqlDbType.VarChar, 50, b, typeof(string));
            //        var[4] = gb.CreateSPParameter("@DtmUpd", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tanggal, typeof(DateTime));
            //        var[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "INSERT", typeof(string));
            //        var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //        if (lblEMPID.SelectedValue != null)
            //        {
            //            ge.ListDataset = gb.GetDataJustOutPut("MF_BranchMapping", var, "constrmf");
            //        }
            //        else
            //        {
            //            ge.ListDataset = gb.GetDataJustOutPut("MF_BranchMapping", var, "constrmf");
            //        }

            //    }
            //}

            //if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            //{

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

            //}

            //else
            //{

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
            //    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            //}
            //Response.Redirect("~/MasterTemplate/BranchMapping.aspx");
        }

        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
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