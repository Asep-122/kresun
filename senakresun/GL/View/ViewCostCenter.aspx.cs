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


public partial class GL_View_ViewCostCenter : System.Web.UI.Page
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

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {

       
                DateTime now = DateTime.Now;
                txtStart.Text = now.ToString("yyyy-MM-dd");
                txtEnd.Text = now.ToString("yyyy-MM-dd");
               BindGridEntity();

            }


            //GRData.UseAccessibleHeader = true;
            //if (GRData.HeaderRow != null)
            //{
            //    GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}



        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[5];
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsarea[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        Paramsarea[2] = gb.CreateSPParameter("@CostCenter", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcostcenter.SelectedValue, typeof(String));
        Paramsarea[3] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        Paramsarea[4] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("GL_View_CostCenter", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();


        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[2];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsx[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_costcenter", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcostcenter, "None", "");

        ddlcostcenter.Items.Insert(0, new ListItem("-", "-"));

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[5];
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        Paramsarea[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        Paramsarea[2] = gb.CreateSPParameter("@CostCenter", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcostcenter.SelectedValue, typeof(String));
        Paramsarea[3] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        Paramsarea[4] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("GL_View_CostCenter", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();
        //GRData.HeaderRow.Disposed = 1;
        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
       

    }
    

   


}