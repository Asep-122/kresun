using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BIGDATA_DatabaseFormulirCalonManage : System.Web.UI.Page
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
  

   

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                BindGrddatawilayahEntity();
                string query;



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ALLAREA", typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_Manage_Calon", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_area asc";
                Dblwilayah2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                Dblwilayah2.DataTextField = "nama";
                Dblwilayah2.DataValueField = "kode_area";
                Dblwilayah2.DataBind();
                Dblwilayah2.Items.Insert(0, new ListItem("Filter Area", "ALL"));
                Dblwilayah2.Enabled = true;




                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                varx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MARKETING", typeof(string));
                varx[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                varx[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_Manage_Calon", varx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "username asc";
                DblMarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                DblMarketing.DataTextField = "name";
                DblMarketing.DataValueField = "username";
                DblMarketing.DataBind();
                DblMarketing.Items.Insert(0, new ListItem("Filter Marketing", "ALL"));
                DblMarketing.Enabled = true;

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }



    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRID1", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 10, "", typeof(string));
        var[3] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 50, DblMarketing.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@Branch", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@Area", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_Manage_Calon", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "data asc";


        GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRUser.DataBind();

        GRUser.UseAccessibleHeader = true;

        GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;


       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);
    }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

    //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    }

}
    
    public void BindGrddatacabangEntity()
    {

        string query;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRID2", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 10, "", typeof(string));
        var[3] = gb.CreateSPParameter("@wilayah", ParameterDirection.Input, SqlDbType.VarChar, 50, Dblwilayah2.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@Branch", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@Area", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_Manage_Calon", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "area asc";


        Grddatacabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grddatacabang.DataBind();

        Grddatacabang.UseAccessibleHeader = true;

        Grddatacabang.HeaderRow.TableSection = TableRowSection.TableHeader;


      

         ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);

    }
    protected void GrdWilayah_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Wilayah":

                try

                {

                 
                    Pnldatabase.Visible = false;
                    Pnlcabang.Visible = true;
                    Pnldatawilayah.Visible = false;
                    //Dblwilayah2.SelectedValue = e.ToString;
                    Dblwilayah2.SelectedValue = ((Label)GrdWilayah.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblxNO")).Text;
                    BindGrddatacabangEntity();

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                }
                break;
        }
    }
    public void BindGrddatawilayahEntity()
    {


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRID3", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 10, "", typeof(string));
        var[3] = gb.CreateSPParameter("@wilayah", ParameterDirection.Input, SqlDbType.VarChar, 50, Dblwilayah2.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@Branch", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@Area", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_Manage_Calon", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "area asc";


        GrdWilayah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdWilayah.DataBind();

        GrdWilayah.UseAccessibleHeader = true;

        GrdWilayah.HeaderRow.TableSection = TableRowSection.TableHeader;




       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);

    }


    protected void lnkCari_Click(object sender, EventArgs e)
    {

        BindGridEntity();
        Pnldatabase.Visible = true;

    }





    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btndatabase_Click(object sender, EventArgs e)
    {
     
        Pnldatabase.Visible = true;
        Pnlcabang.Visible = false;
        Pnldatawilayah.Visible = false;

        BindGridEntity();
    }
    protected void btnwilayah_Click(object sender, EventArgs e)
    {
       
        Pnldatabase.Visible = false;
        Pnlcabang.Visible = false;
        Pnldatawilayah.Visible = true;
        BindGrddatawilayahEntity();

    }

    protected void btncabang_Click(object sender, EventArgs e)
    {
       
        Pnldatabase.Visible = false;
        Pnlcabang.Visible = true;
        Pnldatawilayah.Visible = false;
        BindGrddatacabangEntity();

    }
    
    protected void DblMarketing_SelectedIndexChanged(object sender, EventArgs e)
    {


        BindGridEntity();



    }
    protected void ddlwilayah2_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (Dblwilayah2.SelectedValue != "")
        {
            string query;


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRID4", typeof(string));
            var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[2] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 10, "", typeof(string));
            var[3] = gb.CreateSPParameter("@wilayah", ParameterDirection.Input, SqlDbType.VarChar, 50, Dblwilayah2.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@Branch", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[5] = gb.CreateSPParameter("@Area", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("BG_Manage_Calon", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "area asc";


            Grddatacabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            Grddatacabang.DataBind();

            Grddatacabang.UseAccessibleHeader = true;

            Grddatacabang.HeaderRow.TableSection = TableRowSection.TableHeader;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);


        }

    }


   
}