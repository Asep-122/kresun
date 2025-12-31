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


public partial class GL_Master_GL_GenGlCabang : System.Web.UI.Page
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
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
                Params[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetPersh", Params, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlCabang, "None", "");

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabangtujuan, "None", "");

                ddlCabang.SelectedValue = SessBranchID;
                ddlCabang.Enabled = false;
                BindGridEntity();
            }
            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

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
        //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[0];
        DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[2];
        //Paramsarea[0] = gb.CreateSPParameter("@reg_id", ParameterDirection.Input, SqlDbType.Int, 0, ddlRegional.SelectedValue, typeof(Int32));
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsarea[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlCabang.SelectedValue, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_GRIDGLMST", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();




    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }


  
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
            Params[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            Params[1] = gb.CreateSPParameter("@Kodecabangsumber", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlCabang.SelectedValue, typeof(String));
            Params[2] = gb.CreateSPParameter("@Kodecabangtujuan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabangtujuan.SelectedValue, typeof(String));
            Params[3] = gb.CreateSPParameter("@SessLoginID", ParameterDirection.Input, SqlDbType.VarChar,20, SessLoginID, typeof(String));
            Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("GL_GenerateCabang", Params, "constrmf");
            

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Generate Done!','You successfully save this Cabang','top-right','success','2');", true);


            }

            else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Full"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Data Sudah Ada!','Generate dibatalkan','top-right','success','2');", true);
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
}