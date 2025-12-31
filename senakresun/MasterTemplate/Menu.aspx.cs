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

public partial class MasterTemplate_Menu : System.Web.UI.Page
{

    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }


    StringBuilder sb = new StringBuilder();
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    public string strpathmenu
    {
        get { return (string)ViewState["strpathmenu"]; }
        set { ViewState["strpathmenu"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            if (!this.IsPostBack)
            {

                string ParentMenuID = "null";

                CallMenu(ParentMenuID);

                strpathmenu = HttpContext.Current.Server.MapPath("~"); //ConfigurationManager.AppSettings["PathMenu"].ToString();

                GenerateTreeView(strpathmenu);

            }

        }

        catch (Exception X)
        {
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    #region "Path Menu"
    private void GenerateTreeView(string rootPath)
    {
        GetFolders(System.IO.Path.GetFullPath(rootPath), TreeViewFolders.Nodes);
        TreeViewFolders.ExpandDepth = 1;
    }

    // recursive method to load all folders and files into tree
    private void GetFolders(string path, TreeNodeCollection nodes)
    {
        try
        {
            // add nodes for all directories (folders)
            string[] dirs = Directory.GetDirectories(path);
            foreach (string p in dirs)
            {
                string dp = p.Substring(path.Length);
                nodes.Add(Node("", p.Substring(path.Length), "folder"));
            }

            // add nodes for all files in this directory (folder)
            string[] files = Directory.GetFiles(path, "*.aspx");
            foreach (string p in files)
            {
                nodes.Add(Nodefile(p, p.Substring(path.Length), "file"));
            }

            // add all subdirectories for each directory (recursive)
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Value == "folder")
                    GetFolders(dirs[i] + "\\", nodes[i].ChildNodes);
            }
        }
        catch (System.Exception)
        {
            //ltlMsg.Text = " <div class=\"alert bg-green alert-dismissible\" role=\"alert\"> <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + X.Message.ToString() + "</div>";

        }
    }

    // create a TreeNode from the specified path, text and type
    private TreeNode Node(string path, string text, string type)
    {
        TreeNode n = new TreeNode();
        n.Value = type;
        n.Text = text;
        return n;
    }

    private TreeNode Nodefile(string path, string text, string type)
    {
        TreeNode n = new TreeNode();
        n.Value = path;
        n.Text = text;
        return n;
    }
    #endregion

    private void CallMenu(string ParentMenuID)
    {



        var sym = ParentMenuID.Contains("null") ? " is " : " = ";

        string WhereCond = "where a.ParentMenuId " + sym + ParentMenuID;

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 300, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetMenu", Params, "constrmf");


        int i = 1;

        sb.Append("<div class=\"row\">");

        foreach (DataRow row in ge.ListDataset.Tables[0].Rows)
        {


            sb.Append("<div class=\"col-md-3\">");
            sb.Append("<div class=\"text-center text-white card-box bg-success\">");
            sb.Append("");
            sb.Append("<div class=\"member-card pt-2 pb-2\">");
            sb.Append("");
            sb.Append("<div class=\"\">");
            sb.Append("<h4 class=\"m-b-5\">" + row["MenuName"].ToString() + "</h4>");
            sb.Append("</div>");
            sb.Append("");
            sb.Append("<ul class=\"social-links list-inline m-t-20\">");
            sb.Append("<li class=\"list-inline-item\">");
            sb.Append("<a href=\"#\" title=\"\" data-placement=\"top\" data-toggle=\"tooltip\" class=\"tooltips\" onclick=\"callbtn('Edit','" + row["MenuID"].ToString() + "')\" data-original-title=\"Edit\"><i class=\"dripicons-document-edit\"></i></a>");
            sb.Append("</li>");
            sb.Append("<li class=\"list-inline-item\">");
            sb.Append("<a href=\"#\" title=\"\" data-placement=\"top\" data-toggle=\"tooltip\" class=\"tooltips\"  onclick=\"callbtn('show','" + row["MenuID"].ToString() + "')\" data-original-title=\"Sub Menu\"><i class=\"mdi mdi-folder-outline\"></i></a>");
            sb.Append("</li>");
            sb.Append("</ul>");
            sb.Append("");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");

            i++;

        }

        sb.Append("</div>");

        litMenu.Text = sb.ToString();


    }


    protected void lnEdit_Click(object sender, EventArgs e)
    {

        var WhereCond = "where a.MenuID = " + hdnMenuid.Value;

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 300, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetMenu", Params, "constrmf");

        txtNamaMenu.Text = ge.ListDataset.Tables[0].Rows[0]["MenuName"].ToString();
        txtorder.Text = ge.ListDataset.Tables[0].Rows[0]["order"].ToString();
        chkrooaktiv.Checked = Boolean.Parse(ge.ListDataset.Tables[0].Rows[0]["IsActive"].ToString());

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

    protected string btnEdit()
    {
        return Page.ClientScript.GetPostBackEventReference(lnEdit, string.Empty, true);
    }

    protected void lnAdd_Click(object sender, EventArgs e)
    {
        string WhereCond;

        hdnMenuid.Value = "";
        txtNamaMenu.Text = "";

        var prntID = hdnMenuid.Value == "" ? "null" : hdnMenuid.Value;

        var sym = prntID.Contains("null") ? " is " : " = ";


        WhereCond = "where ParentMenuId " + sym + prntID;


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 2000, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("MF_spMaxOrder", Params, "constrmf");

        txtorder.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
        txtorder.Enabled = false;
        chkrooaktiv.Checked = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

    protected string btnAdd()
    {
        return Page.ClientScript.GetPostBackEventReference(lnAdd, string.Empty, true);
    }

    protected void lnSave_Click(object sender, EventArgs e)
    {
        try
        {


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[8];
            Params[0] = gb.CreateSPParameter("@MenuID", ParameterDirection.Input, SqlDbType.Int, 0, hdnMenuid.Value == "" ? "0" : hdnMenuid.Value, typeof(Int32));
            Params[1] = gb.CreateSPParameter("@MenuName", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNamaMenu.Text, typeof(String));
            Params[2] = gb.CreateSPParameter("@ParentMenuId", ParameterDirection.Input, SqlDbType.Int, 0, (object)DBNull.Value, typeof(Int32));
            Params[3] = gb.CreateSPParameter("@URL", ParameterDirection.Input, SqlDbType.VarChar, 300, null, typeof(String));
            Params[4] = gb.CreateSPParameter("@order", ParameterDirection.Input, SqlDbType.Int, 0, txtorder.Text, typeof(Int32));
            Params[5] = gb.CreateSPParameter("@IsActive", ParameterDirection.Input, SqlDbType.Bit, 0, chkrooaktiv.Checked, typeof(Boolean));
            Params[6] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            Params[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 4000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("MF_spInserOrUpdateMenu", Params, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

            }

            else
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Ini Pesan Error');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }

        }

        catch (Exception X)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected string btnsave()
    {
        return Page.ClientScript.GetPostBackEventReference(lnSave, string.Empty, true);
    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue == "1")
        {

            txtmn.Text = "";
            txtmn.Focus();

            txtPM.Text = "";

            lnlLoop.Visible = false;


        }

        else if (ddltype.SelectedValue == "2")
        {

            txtmn.Text = "";
            txtmn.Focus();

            txtPM.Text = "";

            lnlLoop.Visible = true;
        }
    }




    protected void lnShow_Click(object sender, EventArgs e)
    {
        pnMenu.Visible = false;
        pnlsubmenu.Visible = true;

        PopulateTreeview();
    }

    protected string btnShow()
    {
        return Page.ClientScript.GetPostBackEventReference(lnShow, string.Empty, true);
    }

    #region "Treeview Menu Andi 05-12-2013"
    private void PopulateTreeview()
    {
        DataSet ds = new DataSet();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@menuID", ParameterDirection.Input, SqlDbType.BigInt, 0, hdnParentMenuID.Value == "" ? "0" : hdnParentMenuID.Value, typeof(int));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetALLMenu", Params, "constrmf");

        ds = ge.ListDataset;

        ds.Relations.Add("children", ds.Tables[0].Columns["MenuID"], ds.Tables[0].Columns["ParentMenuId"], false);

        try
        {
            var parentID = hdnParentMenuID.Value == "" ? "0" : hdnParentMenuID.Value;
            DataRow[] Rows = ds.Tables[0].Select("ParentMenuId = " + parentID + ""); // Get all parents nodes

            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode root = new TreeNode(Rows[i]["MenuName"].ToString(), Rows[i]["MenuID"].ToString());

                string urlnya = Rows[i]["URL"].ToString().Replace("~/", "") != "" ? Rows[i]["URL"].ToString().Replace("~/", "") : "";


                if (urlnya == "")
                {
                    sb.Append("<li> " + Rows[i]["MenuName"].ToString() + " &nbsp;");
                    sb.Append("<a href=\"#\" title=\"\" data-placement=\"top\" data-toggle=\"tooltip\" class=\"tooltips\" onclick=\"callbtn('EditSM','" + Rows[i]["MenuID"].ToString() + "')\" data-original-title=\"Edit\"><i class=\"dripicons-document-edit\"></i></a>");
                    sb.Append("<a href=\"#\" title=\"\" data-placement=\"top\" data-toggle=\"tooltip\" class=\"tooltips\" onclick=\"callbtn('AddSM','" + Rows[i]["MenuID"].ToString() + "')\"  data-original-title=\"Add\"><i class=\"mdi mdi-folder-plus\"></i></a>");

                }
                else
                {
                    sb.Append("<li> " + Rows[i]["MenuName"].ToString() + " &nbsp;");
                    sb.Append("<a href=\"#\" title=\"\" data-placement=\"top\" data-toggle=\"tooltip\" class=\"tooltips\" data-original-title=\"Edit\"><i class=\"dripicons-document-edit\"></i></a></li>");
                }


                CreateNode(root, ds.Tables[0]);

                sb.Append("</li>");

            }



            ltlSubMenu.Text = sb.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root_x();", true);
        }

        catch (Exception ex)
        {
            throw new Exception("Unable to populate treeview" + ex.Message);
        }

    }
    public void CreateNode(TreeNode node, DataTable Dt)
    {
        DataRow[] Rows = Dt.Select("ParentMenuId =" + node.Value);

        if (Rows.Length == 0)
        {
            sb.Append("</li>");
            return;
        }

        sb.Append("<ul class=\"submenu\">");

        for (int i = 0; i < Rows.Length; i++)
        {
            TreeNode Childnode = new TreeNode(Rows[i]["MenuName"].ToString(), Rows[i]["MenuID"].ToString());
            node.ChildNodes.Add(Childnode);


            string urlnya = Rows[i]["URL"].ToString().Replace("~/", "") != "" ? Rows[i]["URL"].ToString().Replace("~/", "") : "";

            if (urlnya == "")
            {
                sb.Append("<li> " + Rows[i]["MenuName"].ToString() + " &nbsp;");
                sb.Append("<a href=\"#\" title=\"\" data-placement=\"top\" data-toggle=\"tooltip\" class=\"tooltips\" onclick=\"callbtn('EditSM','" + Rows[i]["MenuID"].ToString() + "')\" data-original-title=\"Edit\"><i class=\"dripicons-document-edit\"></i></a>");
                sb.Append("<a href=\"#\" title=\"\" data-placement=\"top\" data-toggle=\"tooltip\" class=\"tooltips\" onclick=\"callbtn('AddSM','" + Rows[i]["MenuID"].ToString() + "')\" data-original-title=\"Add\"><i class=\"mdi mdi-folder-plus\"></i></a>");
            }
            else
            {
                sb.Append("<li> " + Rows[i]["MenuName"].ToString() + " &nbsp;");
                sb.Append("<a href=\"#\" title=\"\" data-placement=\"top\" data-toggle=\"tooltip\" class=\"tooltips\" onclick=\"callbtn('EditSM','" + Rows[i]["MenuID"].ToString() + "')\" data-original-title=\"Edit\"><i class=\"dripicons-document-edit\"></i></a>");
            }

            CreateNode(Childnode, Dt);


        }

        sb.Append("</ul>");

    }

    #endregion

    protected void lnEditSM_Click(object sender, EventArgs e)
    {
        var WhereCond = "where a.MenuID = " + hdnMenuid.Value;

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 300, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetMenu", Params, "constrmf");


        txtmn.Focus();

        txtmn.Text = ge.ListDataset.Tables[0].Rows[0]["MenuName"].ToString();
        txtParent.Text = ge.ListDataset.Tables[0].Rows[0]["ParentMenuId"].ToString();
        txtPname.Text = ge.ListDataset.Tables[0].Rows[0]["ParentMenuName"].ToString();

        txtPM.Text = ge.ListDataset.Tables[0].Rows[0]["URL"].ToString();

        if (txtParent.Text == "")
        {
            ddltype.Enabled = false;
        }

        if (txtPM.Text != "")
        {
            ddltype.SelectedValue = "2";
            lnlLoop.Visible = true;
        }

        else
        {
            ddltype.SelectedValue = "1";
            lnlLoop.Visible = false;
        }

        txturutan.Text = ge.ListDataset.Tables[0].Rows[0]["order"].ToString();
        chkIsActive.Checked = Boolean.Parse(ge.ListDataset.Tables[0].Rows[0]["IsActive"].ToString());

        btnSimpan.Enabled = true;
    }

    protected string btnEditSM()
    {
        return Page.ClientScript.GetPostBackEventReference(lnEditSM, string.Empty, true);
    }

    protected void lnkAddSB_Click(object sender, EventArgs e)
    {
        txtmn.Text = "";
        txtPM.Text = "";

        ddltype.SelectedIndex = 0;
        lnlLoop.Visible = false;


        var WhereCond = "where a.MenuID = " + hdnParentMenuID.Value;

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 300, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetMenu", Params, "constrmf");


        //PanelMenu.Visible = false;
        //PanelForm.Visible = true;

        txtmn.Focus();


        txtParent.Text = ge.ListDataset.Tables[0].Rows[0]["MenuID"].ToString();
        txtPname.Text = ge.ListDataset.Tables[0].Rows[0]["MenuName"].ToString();



        WhereCond = "where ParentMenuId = " + hdnParentMenuID.Value;


        DefaultClass.Global.SqlHelper[] Params2 = new DefaultClass.Global.SqlHelper[1];
        Params2[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 2000, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("MF_spMaxOrder", Params2, "constrmf");

        txturutan.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
        txturutan.Enabled = false;

        hdnMenuid.Value = "";
        chkIsActive.Checked = true;

        btnSimpan.Enabled = true;
    }

    protected void lnkAddSM_Click(object sender, EventArgs e)
    {
        var WhereCond = "where a.ParentMenuId = " + hdnMenuid.Value;

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 300, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetMenu", Params, "constrmf");


        //PanelMenu.Visible = false;
        //PanelForm.Visible = true;

        txtmn.Focus();

        txtmn.Text = "";
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            txtParent.Text = ge.ListDataset.Tables[0].Rows[0]["ParentMenuId"].ToString();
            txtPname.Text = ge.ListDataset.Tables[0].Rows[0]["ParentMenuName"].ToString();
        }

        else
        {


            WhereCond = "where a.MenuID = " + hdnMenuid.Value;

            DefaultClass.Global.SqlHelper[] Params3 = new DefaultClass.Global.SqlHelper[1];
            Params3[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 300, WhereCond, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetMenu", Params3, "constrmf");

            txtParent.Text = ge.ListDataset.Tables[0].Rows[0]["MenuId"].ToString();
            txtPname.Text = ge.ListDataset.Tables[0].Rows[0]["MenuName"].ToString();
        }

        txtPM.Text = "";


        WhereCond = "where ParentMenuId = " + hdnMenuid.Value;


        DefaultClass.Global.SqlHelper[] Params2 = new DefaultClass.Global.SqlHelper[1];
        Params2[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 2000, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("MF_spMaxOrder", Params2, "constrmf");

        txturutan.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
        txturutan.Enabled = false;

        hdnMenuid.Value = "";

        chkIsActive.Checked = true;

        btnSimpan.Enabled = true;

    }

    protected string btnAddSM()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkAddSM, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void TreeViewFolders_SelectedNodeChanged(object sender, EventArgs e)
    {
        txtPM.Text = TreeViewFolders.SelectedNode.Value.Replace(strpathmenu, "~/").Replace(@"\", @"/");
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[8];
            Params[0] = gb.CreateSPParameter("@MenuID", ParameterDirection.Input, SqlDbType.Int, 0, hdnMenuid.Value == "" ? "0" : hdnMenuid.Value, typeof(Int32));
            Params[1] = gb.CreateSPParameter("@MenuName", ParameterDirection.Input, SqlDbType.VarChar, 50, txtmn.Text, typeof(String));
            Params[2] = gb.CreateSPParameter("@ParentMenuId", ParameterDirection.Input, SqlDbType.Int, 0, txtParent.Text == string.Empty ? (object)DBNull.Value : txtParent.Text, typeof(Int32));
            Params[3] = gb.CreateSPParameter("@URL", ParameterDirection.Input, SqlDbType.VarChar, 300, txtPM.Text == string.Empty ? null : txtPM.Text, typeof(String));
            Params[4] = gb.CreateSPParameter("@order", ParameterDirection.Input, SqlDbType.Int, 0, txturutan.Text, typeof(Int32));
            Params[5] = gb.CreateSPParameter("@IsActive", ParameterDirection.Input, SqlDbType.Bit, 0, chkIsActive.Checked, typeof(Boolean));
            Params[6] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            Params[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 4000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("MF_spInserOrUpdateMenu", Params, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                PopulateTreeview();

                ClearText();

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

    private void ClearText()
    {
        hdnMenuid.Value = "";
        ddltype.SelectedIndex = 0;
        txtParent.Text = "";
        txtPname.Text = "";
        txtPM.Text = "";
        txtmn.Text = "";
        txtmn.Text = "";
        lnlLoop.Visible = false;
        txturutan.Text = "";
        btnSimpan.Enabled = false;
        chkIsActive.Checked = false;
    }


    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkReload, string.Empty, true);
    }


    protected void lnkReload_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}