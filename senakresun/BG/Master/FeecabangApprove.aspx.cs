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

public partial class mikro_FeecabangApprove : System.Web.UI.Page
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
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
   

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;



                BindGridEntity();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

          //  gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWAPPROVECANCEL", typeof(string));
        var[1] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@SessAreaUser", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("BG_APPROVE_CANCEL", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "produk asc";


        GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRUser.DataBind();

        GRUser.UseAccessibleHeader = true;

        GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;







    }

   
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    
  
  
    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlFront.Visible = false;


       

    }

    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string query;
        string urut,jenis;
       
        
        
        switch (e.CommandName)
        {
              
              case "GRUPAPPROVE1":
                query = "";
                 urut = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspen")).Text;
                jenis = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljenis")).Text;



                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.VarChar, 10, jenis, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRUPAPPROVE1", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(urut), typeof(int));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_APPROVE_CANCEL_Pro", var, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Approve Done!','You successfully Approve','top-right','success','1');", true);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','You successfully Approve this Project','top-right','success','1');", true);
                        BindGridEntity();

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }

                break;
            case "GRUPAPPROVE2":
               
                query = "";
                    
                urut = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspen")).Text;
                    DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[4];
                   
                    vart[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    vart[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRUPAPPROVE2", typeof(string));
                    vart[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    vart[3] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(urut), typeof(int));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_APPROVE_CANCEL_Pro", vart, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','You successfully Approve this Project','top-right','success','1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cancel Done!','You successfully Cancel','top-right','success','1');", true);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','You successfully Approve this Project','top-right','success','1');", true);
                        BindGridEntity();

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }



                    break;
            case "GRUPAPPROVE3":

                query = "";

                urut = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspen")).Text;

                    DefaultClass.Global.SqlHelper[] varr = new DefaultClass.Global.SqlHelper[4];

                    varr[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    varr[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GRUPAPPROVE3", typeof(string));
                    varr[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varr[3] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(urut), typeof(int));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_APPROVE_CANCEL_Pro", varr, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','You successfully Approve this Project','top-right','success','1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tolak Done!','You successfully Tolak','top-right','success','1');", true);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','You successfully Approve this Project','top-right','success','1');", true);
                        BindGridEntity();

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }


                    break;
        }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

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
//    Function CVCur(Anyvalue As Variant)
//On Error Resume Next
//   Dim a, B As Variant
//   a = Anyvalue
//   B = Len(CStr(a))
//   CVCur = Replace(CStr(a), ",", ".", 1, B)
//End Function
    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }



   
    protected void lnkRefreshModal_Click(object sender, EventArgs e)
    {

        BindGridEntity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    protected string btnremodal()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    }
   
}