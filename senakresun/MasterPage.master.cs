using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    StringBuilder sb = new StringBuilder();
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string Sessemail
    {
        get { return Session["Sessemail"].ToString(); }
        set { ViewState["Sessemail"] = value; }
    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessName
    {
        get { return Session["SessName"].ToString(); }
        set { ViewState["SessName"] = value; }
    }
    public string SessNamaProfil
    {
        get { return Session["SessNamaProfil"].ToString(); }
        set { ViewState["SessNamaProfil"] = value; }
    }
    public string SessKoperasiNama
    {
        get { return Session["SessKoperasiNama"].ToString(); }
        set { ViewState["SessKoperasiNama"] = value; }
    }
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
            Params[0] = gb.CreateSPParameter("@UserName", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetAksesMenu", Params, "constrmf");

            gb.FillDDL(ge.ListDataset.Tables[0], ddlMenuMaster, "NONE", "");
            //ddlMenuMaster.SelectedValue = "2";
            //ddlMenuMaster.Visible = false;

            PopulateTreeview();
        }
        if (SessKoperasiID == "I")
        {
            idnama1.InnerText = "    " + SessKoperasiNama;
            idnama2.InnerText = " " + SessKoperasiNama;
            idimage1.ImageUrl = "assets/sena.png";
            idimage2.ImageUrl = "assets/sena.png";
        }
        else if (SessKoperasiID == "II")
        {
            idnama1.InnerText = "    " + SessKoperasiNama;
            idnama2.InnerText = "    " + SessKoperasiNama;
            idimage1.ImageUrl = "assets/sena.png";
            idimage2.ImageUrl = "assets/sena.png";
        }
        else if (SessKoperasiID == "III")
        {
            idnama1.InnerText = " " + SessKoperasiNama;
            idnama2.InnerText = " " + SessKoperasiNama;
            idimage1.ImageUrl = "assets/sena.png";
            idimage2.ImageUrl = "assets/sena.png";
        }
        else if (SessKoperasiID == "IV")
        {
            idnama1.InnerText = " " + SessKoperasiNama;
            idnama2.InnerText = " " + SessKoperasiNama;
            idimage1.ImageUrl = "assets/sena.png";
            idimage2.ImageUrl = "assets/logosemerbakcitra.png";
        }
        else if (SessKoperasiID == "V")
        {
            idnama1.InnerText = " " + SessKoperasiNama;
            idnama2.InnerText = " " + SessKoperasiNama;
            idimage1.ImageUrl = "assets/sena.png";
            idimage2.ImageUrl = "assets/sena.png";
        }
    }

    #region "Treeview Menu Andi 05-12-2013"
    private void PopulateTreeview()
    {
        DataSet ds = new DataSet();


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@menuID", ParameterDirection.Input, SqlDbType.Int, 0, ddlMenuMaster.SelectedValue == "" ? "0" : ddlMenuMaster.SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@UserName", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_spMenu", Params, "constrmf");

        ds = ge.ListDataset;

        ds.Relations.Add("children", ds.Tables[0].Columns["MenuID"], ds.Tables[0].Columns["ParentMenuId"], false);

        try
        {
            var parentID = ddlMenuMaster.SelectedValue == "" ? "0" : ddlMenuMaster.SelectedValue;
            DataRow[] Rows = ds.Tables[0].Select("ParentMenuId = " + parentID + ""); // Get all parents nodes

            for (int i = 0; i < Rows.Length; i++)
            {

                if (Rows.Length == 0)
                {
                    sb.Append("</li>");
                    return;
                }

                TreeNode root = new TreeNode(Rows[i]["MenuName"].ToString(), Rows[i]["MenuID"].ToString());

                string urlnya = Rows[i]["URL"].ToString().Replace("~/", "") != "" ? Rows[i]["URL"].ToString().Replace("~/", "") : "";


                if (urlnya == "")
                {
                    sb.Append("<li class=\"has-submenu\"> <a href = \"#\"><i class=\"mdi mdi-folder-open\"></i>" + Rows[i]["MenuName"].ToString() + "</a>");

                }
                else
                {
                    sb.Append("<li><a href = \"" + urlnya + "\" onclick = \"return go(this);\" ><label>" + Rows[i]["MenuName"].ToString() + "</label></a></li>");

                }


                CreateNode(root, ds.Tables[0]);

                // sb.Append("</li>");

            }



            litMenu.Text = sb.ToString();
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
                sb.Append("<li class=\"has-submenu\"><a href = \"#\">" + Rows[i]["MenuName"].ToString() + "</a>");
            }
            else
            {
                sb.Append("<li><a href = \"" + urlnya + "\" onclick = \"return go(this);\"><label>" + Rows[i]["MenuName"].ToString() + "</label></a>");

            }

            CreateNode(Childnode, Dt);


        }

        sb.Append("</ul></li>");

    }

    #endregion

    protected void ddlMenuMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

       
        PopulateTreeview();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to populate treeview" + ex.Message);
        }
    }
}
