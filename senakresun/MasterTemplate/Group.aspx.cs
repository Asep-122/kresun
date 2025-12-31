using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterTemplate_Group : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            if (!IsPostBack)
            {
                tvTables.Attributes.Add("onclick", "OnTreeClick(event)");

                PopulateTreeview("");

                BindGridEntity();
            }


            GRGroup.UseAccessibleHeader = true;

            GRGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[0];

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_SpMenuGroup", Params, "constrmf");

        GRGroup.DataSource = ge.ListDataset.Tables[0];
        GRGroup.DataBind();

    }

    #region "Treeview Menu Andi 05-12-2013"
    private void PopulateTreeview(string wherecond)
    {
        try
        {
            DataSet ds = new DataSet();
        tvTables.Nodes.Clear();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 2000, wherecond, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("MF_spOtoritasMenu", Params, "constrmf");

        ds = ge.ListDataset;


            //Debug.WriteLine(ds.Tables[0].Columns["ParentMenuId"]);
            //Debug.WriteLine(ds.Tables[0].Columns["MenuID"]);
            ds.Relations.Add("children", ds.Tables[0].Columns["MenuID"], ds.Tables[0].Columns["ParentMenuId"]);


            DataRow[] Rows = ds.Tables[0].Select("ParentMenuId IS NULL"); // Get all parents nodes
            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode root = new TreeNode(Rows[i]["MenuName"].ToString(), Rows[i]["MenuID"].ToString());
                if (Rows[i]["ischeckbox"].ToString() == "1")
                {
                    root.Checked = true;
                }
                else
                {
                    root.Checked = false;
                }
                root.ShowCheckBox = true;
                root.SelectAction = TreeNodeSelectAction.None;



                CreateNode(root, ds.Tables[0]);
                tvTables.Nodes.Add(root);

            }

            tvTables.ExpandAll();
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }

    }
    public void CreateNode(TreeNode node, DataTable Dt)
    {
        DataRow[] Rows = Dt.Select("ParentMenuId =" + node.Value);
        if (Rows.Length == 0) { return; }
        for (int i = 0; i < Rows.Length; i++)
        {
            TreeNode Childnode = new TreeNode(Rows[i]["MenuName"].ToString(), Rows[i]["MenuID"].ToString());
            if (Rows[i]["ischeckbox"].ToString() == "1")
            {
                Childnode.Checked = true;
            }
            else
            {
                Childnode.Checked = false;
            }

            Childnode.ShowCheckBox = true;
            Childnode.SelectAction = TreeNodeSelectAction.None;

            node.ChildNodes.Add(Childnode);
            CreateNode(Childnode, Dt);
        }
    }
    #endregion

    protected void GRGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                txtGN.Text = ((Label)GRGroup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblGroupName")).Text;
                hdnID.Value = ((Label)GRGroup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblIdGroup")).Text;
                chkIsActive.Checked = ((CheckBox)GRGroup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("chklist")).Checked;

                PopulateTreeview("where gmp.IdGroup = " + ((Label)GRGroup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblIdGroup")).Text + "");

                pnlFront.Visible = false;
                pnlGroup.Visible = true;
                tvTables.Enabled = true;
                tvTables.Visible = true;

                break;

            case "DEL":

                //oGE.MenuID = ((Label)GRGroup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblIdGroup")).Text;
                //oGE.SPName = "DBEIS_spDeleteGroupMenu";
                //oGE = CM.CompSpMenuID(oGE);

                //BindGridEntity();
                //txtgroup.Text = "";
                //hdnID.Value = "";
                //txtSearch.Text = "";
                //btnSave.Text = "Add Group";
                //PopulateTreeview("");
                //tvTables.Enabled = false;
                break;

                //case "GM":
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('GroupMember.aspx?ID=" + ((Label)GRGroup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblIdGroup")).Text + "&nama=" + ((Label)GRGroup.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblGroupName")).Text + "','_blank','status=1,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1,width=700,height=500');", true);
                //    break;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dtdetail = new DataTable("Groupdetail");

            dtdetail.Columns.Add(new DataColumn("MenuID", typeof(Int32)));
            dtdetail.Columns.Add(new DataColumn("usrupd", typeof(string)));

            if (tvTables.CheckedNodes.Count > 0)
            {
                foreach (TreeNode node in tvTables.CheckedNodes)
                {

                    DataRow drdetail = dtdetail.NewRow();


                    drdetail["MenuID"] = node.Value;
                    drdetail["usrupd"] = SessLoginID;

                    dtdetail.Rows.Add(drdetail);

                }
            }

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
            Params[0] = gb.CreateSPParameter("@IdGroup", ParameterDirection.Input, SqlDbType.Int, 0, hdnID.Value == "" ? "0" : hdnID.Value, typeof(Int32));
            Params[1] = gb.CreateSPParameter("@GroupName", ParameterDirection.Input, SqlDbType.VarChar, 50, txtGN.Text, typeof(String));
            Params[2] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            Params[3] = gb.CreateSPParameter("@isactive", ParameterDirection.Input, SqlDbType.Bit, 0, chkIsActive.Checked, typeof(Boolean));
            Params[4] = gb.CreateSPParameter("@MF_Type_GroupMenu", ParameterDirection.Input, SqlDbType.Structured, 0, dtdetail, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("MF_spInsertMenuGroupHeaderdetail", Params, "constrmf");

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

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlFront.Visible = false;
        pnlGroup.Visible = true;
        chkIsActive.Checked = true;
    }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
}