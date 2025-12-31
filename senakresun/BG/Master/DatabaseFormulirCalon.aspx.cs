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
public partial class BIGDATA_DatabaseFormulirCalon : System.Web.UI.Page
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
    public string SessIsLevel
    {
        get { return Session["SessIsLevel"].ToString(); }
        set { ViewState["SessIsLevel"] = value; }

    }
    public string SessLevelUser
    {
        get { return Session["SessLevelUser"].ToString(); }
        set { ViewState["SessLevelUser"] = value; }

    }

    public string SessAreaUser
    {
        get { return Session["SessAreaUser"].ToString(); }
        set { ViewState["SessAreaUser"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (SessLevelUser == "X03" || SessLevelUser == "X10")
                {


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "FOLLOWUP", typeof(string));
                    var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", var, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "idfollowup asc";
                    ddlfollowup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlfollowup.DataTextField = "namafollowup";
                    ddlfollowup.DataValueField = "idfollowup";
                    ddlfollowup.DataBind();
                    ddlfollowup.Items.Insert(0, new ListItem("Filter Followup", "ALL"));

                    ddlfollowupsudah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlfollowupsudah.DataTextField = "namafollowup";
                    ddlfollowupsudah.DataValueField = "idfollowup";
                    ddlfollowupsudah.DataBind();
                    ddlfollowupsudah.Items.Insert(0, new ListItem("Filter Followup", "ALL"));


                    if (SessIsLevel == "A02" || SessIsLevel == "ALL")
                {
                    if (SessLevelUser == "X10" || SessLevelUser == "X06" || SessLevelUser == "X04")
                    {
                       
                        BindGridEntityALL();


                            DefaultClass.Global.SqlHelper[] varr1 = new DefaultClass.Global.SqlHelper[4];
                            varr1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTA", typeof(string));
                            varr1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                            varr1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                            varr1[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr1, "constrmf");

                            Lbltotal.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

                            DefaultClass.Global.SqlHelper[] varr2 = new DefaultClass.Global.SqlHelper[4];
                            varr2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAPENDING", typeof(string));
                            varr2[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                            varr2[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                            varr2[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr2, "constrmf");

                            Lblpending.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();


                            DefaultClass.Global.SqlHelper[] varr3 = new DefaultClass.Global.SqlHelper[4];
                            varr3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAREJECT", typeof(string));
                            varr3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                            varr3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                            varr3[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr3, "constrmf");

                            lblreject.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();



                    }

                    else if (SessLevelUser == "X03")
                    {
                       
                        BindGridEntityreferallALL();

                            DefaultClass.Global.SqlHelper[] varr1 = new DefaultClass.Global.SqlHelper[4];
                            varr1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTA", typeof(string));
                            varr1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                            varr1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                            varr1[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr1, "constrmf");

                            Lbltotal.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

                            DefaultClass.Global.SqlHelper[] varr2 = new DefaultClass.Global.SqlHelper[4];
                            varr2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAPENDING", typeof(string));
                            varr2[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                            varr2[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                            varr2[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr2, "constrmf");

                            Lblpending.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();


                            DefaultClass.Global.SqlHelper[] varr3 = new DefaultClass.Global.SqlHelper[4];
                            varr3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAREJECT", typeof(string));
                            varr3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                            varr3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                            varr3[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr3, "constrmf");

                            lblreject.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

                        }
                    }

                else
                {
                        Response.Redirect("~/BG/Master/Home_Block.aspx");
                    }
                }

                else
                {
                    Response.Redirect("~/BG/Master/Home_Block.aspx");
                }

            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void BindGridEntityALL()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRIDALL", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLevelUser, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "referal asc";


        GrdALL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdALL.DataBind();

        GrdALL.UseAccessibleHeader = true;

        GrdALL.HeaderRow.TableSection = TableRowSection.TableHeader;

        
    }

    public void BindGridEntityreferallALL()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRIDALLREFERALL", typeof(string));
            var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[2] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLevelUser, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "referal asc";


            GrdALL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GrdALL.DataBind();

            GrdALL.UseAccessibleHeader = true;

            GrdALL.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
       



    }

    public void BindGridEntity()
    {
        

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRIDALL1", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLevelUser, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "referal asc";


        GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRUser.DataBind();

        GRUser.UseAccessibleHeader = true;

        GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;


      

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);
    }
   
    public void BindGrddatacabangEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRIDALL2", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLevelUser, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "referal asc";


        Grddatacabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grddatacabang.DataBind();

        Grddatacabang.UseAccessibleHeader = true;

        Grddatacabang.HeaderRow.TableSection = TableRowSection.TableHeader;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);
    }

    public void BindGrddatawilayahEntity()
    {

        if (SessIsLevel == "A02" || SessIsLevel == "ALL")
        {



            if (SessLevelUser == "X10" || SessLevelUser == "X06" || SessLevelUser == "X04")
            {
                string query;
                BindGridEntityALL();
                DefaultClass.Global.SqlHelper[] varr1 = new DefaultClass.Global.SqlHelper[4];
                varr1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTA", typeof(string));
                varr1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                varr1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                varr1[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr1, "constrmf");

                Lbltotal.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

                DefaultClass.Global.SqlHelper[] varr2 = new DefaultClass.Global.SqlHelper[4];
                varr2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAPENDING", typeof(string));
                varr2[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                varr2[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                varr2[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr2, "constrmf");

                Lblpending.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();


                DefaultClass.Global.SqlHelper[] varr3 = new DefaultClass.Global.SqlHelper[4];
                varr3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAREJECT", typeof(string));
                varr3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                varr3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                varr3[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr3, "constrmf");

                lblreject.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

            }

            else if (SessLevelUser == "X03")

            {
                string query;
                BindGridEntityreferallALL();
                DefaultClass.Global.SqlHelper[] varr1 = new DefaultClass.Global.SqlHelper[4];
                varr1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTA", typeof(string));
                varr1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                varr1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                varr1[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr1, "constrmf");

                Lbltotal.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

                DefaultClass.Global.SqlHelper[] varr2 = new DefaultClass.Global.SqlHelper[4];
                varr2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAPENDING", typeof(string));
                varr2[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                varr2[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                varr2[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr2, "constrmf");

                Lblpending.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();


                DefaultClass.Global.SqlHelper[] varr3 = new DefaultClass.Global.SqlHelper[4];
                varr3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAREJECT", typeof(string));
                varr3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                varr3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                varr3[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr3, "constrmf");

                lblreject.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

            }
        }
        

}


    protected void lnkCari_Click(object sender, EventArgs e)
    {

        //BindGridEntity();
        Pnldatabase.Visible = true;

    }



    

    protected void btnCancelsudah_Click(object sender, EventArgs e)
    {
        //Pnlsearch.Visible = true;
        PnlHistory.Visible = false;
        Pnldatabase.Visible = true;
        Pnlcabang.Visible = false;
        Pnldatawilayah.Visible = false;
        pnlfollowupsudah.Visible = false;
        //Lbltaspen.Text = "FOLLOW-UP";
        BindGridEntity();
    }


    protected void btnCancelHst_Click(object sender, EventArgs e)
    {
        //Pnlsearch.Visible = true;
        PnlHistory.Visible = false;
        Pnldatabase.Visible = true;
        Pnlcabang.Visible = false;
        Pnldatawilayah.Visible = false;

        //Lbltaspen.Text = "FOLLOW-UP";
        BindGridEntity();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Pnldatabase.Visible = false;
        Pnlcabang.Visible = true;
        Pnldatawilayah.Visible = false;
        pnlfollowup.Visible = false;
        BindGrddatacabangEntity();
    }

    protected void btndatabase_Click(object sender, EventArgs e)
    {
        //Pnlsearch.Visible = true;
        Pnldatabase.Visible = true;
        Pnlcabang.Visible = false;
        Pnldatawilayah.Visible = false;
        //Lbltaspen.Text = "FOLLOW-UP";
        BindGridEntity();
    }
    protected void btnwilayah_Click(object sender, EventArgs e)
    {
        //Pnlsearch.Visible = false;
        Pnldatabase.Visible = false;
        Pnlcabang.Visible = false;
        Pnldatawilayah.Visible = true;
        BindGrddatawilayahEntity();
        //Lbltaspen.Text = "DATA CALON ANGGOTA";
    }

    protected void btncabang_Click(object sender, EventArgs e)
    {
        //Pnlsearch.Visible = false;
        Pnldatabase.Visible = false;
        Pnlcabang.Visible = true;
        Pnldatawilayah.Visible = false;
        BindGrddatacabangEntity();
        //Lbltaspen.Text = "UN-FOLLOW";
    }


    //protected void btnhstfollow_Click(object sender, EventArgs e)
    //{
    //    //Pnlsearch.Visible = false;
    //    Pnldatabase.Visible = false;
    //    Pnlcabang.Visible = true;
    //    Pnldatawilayah.Visible = false;
    //    BindGrddatacabangEntity();
    //    Lbltaspen.Text = "UN-FOLLOW";
    //}

    public void BindGrddatafollowEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRIDALL3", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnidcalon.Text, typeof(string));
        var[2] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLevelUser, typeof(string));
        var[3] = gb.CreateSPParameter("@idanggota", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnidcalon.Text, typeof(string));
        
        ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "idcalonanggota asc";


        GrdFollow.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdFollow.DataBind();

        GrdFollow.UseAccessibleHeader = true;

        GrdFollow.HeaderRow.TableSection = TableRowSection.TableHeader;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);
    }

    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "APPROVE":
               
                hdnidcalon.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourut")).Text;
                Pnldatabase.Visible = false;
                pnlfollowupsudah.Visible = true;
                break;
             
        }
        switch (e.CommandName)
        {
            case "REJECT":
              
                hdnidcalon.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourut")).Text;
                Pnldatabase.Visible = false;
                pnlfollowupsudah.Visible = false;
                PnlHistory.Visible = true;
                BindGrddatafollowEntity();
                break;

        }



    }
    protected void Grddatacabang_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "APPROVE":
                    
                    hdnidcalon.Text = ((Label)Grddatacabang.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourutb")).Text;
                    Pnlcabang.Visible = false;
                    pnlfollowup.Visible = true;
                    break;
                   
            }



        }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTHISTORY", typeof(string));
            var[3] = gb.CreateSPParameter("@followup", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlfollowup.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@idcalon", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnidcalon.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@catatan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtcatatan.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online_Pro", var, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','3');", true);
                Pnldatabase.Visible = false;
                Pnlcabang.Visible = true;
                Pnldatawilayah.Visible = false;
                pnlfollowup.Visible = false;
                BindGrddatacabangEntity();
                if (SessIsLevel == "A10" || SessIsLevel == "A2")
                {



                    if (SessIsLevel == "A10")
                    {
                        DefaultClass.Global.SqlHelper[] varr1 = new DefaultClass.Global.SqlHelper[4];
                        varr1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTA", typeof(string));
                        varr1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                        varr1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                        varr1[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr1, "constrmf");

                        Lbltotal.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

                        DefaultClass.Global.SqlHelper[] varr2 = new DefaultClass.Global.SqlHelper[4];
                        varr2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAPENDING", typeof(string));
                        varr2[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                        varr2[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                        varr2[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr2, "constrmf");

                        Lblpending.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();


                        DefaultClass.Global.SqlHelper[] varr3 = new DefaultClass.Global.SqlHelper[4];
                        varr3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAREJECT", typeof(string));
                        varr3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                        varr3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                        varr3[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr3, "constrmf");

                        lblreject.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

                    }
                }
                else if (SessIsLevel == "A2")

                {
                    DefaultClass.Global.SqlHelper[] varr1 = new DefaultClass.Global.SqlHelper[4];
                    varr1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTA", typeof(string));
                    varr1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                    varr1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    varr1[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr1, "constrmf");

                    Lbltotal.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();

                    DefaultClass.Global.SqlHelper[] varr2 = new DefaultClass.Global.SqlHelper[4];
                    varr2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAPENDING", typeof(string));
                    varr2[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                    varr2[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    varr2[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr2, "constrmf");

                    Lblpending.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();


                    DefaultClass.Global.SqlHelper[] varr3 = new DefaultClass.Global.SqlHelper[4];
                    varr3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTANGGOTAREJECT", typeof(string));
                    varr3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                    varr3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    varr3[3] = gb.CreateSPParameter("@SessLevelUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLevelUser, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online", varr3, "constrmf");

                    lblreject.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();


                }

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
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnSimpansudah_Click(object sender, EventArgs e)
    {
        try
        {

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTHISTORY", typeof(string));
            var[3] = gb.CreateSPParameter("@followup", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlfollowupsudah.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@idcalon", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnidcalon.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@catatan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtcatatansudah.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("BG_GetFormulir_Online_Pro", var, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','3');", true);
                //Pnlsearch.Visible = true;
                PnlHistory.Visible = false;
                Pnldatabase.Visible = true;
                Pnlcabang.Visible = false;
                Pnldatawilayah.Visible = false;
                pnlfollowupsudah.Visible = false;
                //Lbltaspen.Text = "FOLLOW-UP";
                BindGridEntity();

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
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
}
