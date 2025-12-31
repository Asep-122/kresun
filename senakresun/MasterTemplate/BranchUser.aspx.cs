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

public partial class MasterTemplate_BranchUser : System.Web.UI.Page
{

    public string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }

    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }

    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                DefaultClass.Global.SqlHelper[] dlrMNC = new DefaultClass.Global.SqlHelper[1];

                dlrMNC[0] = gb.CreateSPParameter("@User", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(String));

                ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetUserBranch", dlrMNC, "constrmf");


                ge.ListDataset.Tables[0].DefaultView.Sort = "[Text] asc";

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlCabang, "None", "");

                ddlCabang.SelectedValue = SessBranchID;
            }
        }

        catch (Exception X)
        {
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Session.Contents.Remove("SessBranchID");
    //    Session.Contents.Add("SessBranchID", "BGR");
    //    Request.UrlReferrer.OriginalString.ToString();
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session.Contents.Remove("SessBranchID");
        Session.Contents.Add("SessBranchID", ddlCabang.SelectedValue);
        try
        {


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            Params[0] = gb.CreateSPParameter("@Username", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[1] = gb.CreateSPParameter("@BranchID", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlCabang.SelectedValue, typeof(string));
            Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 4000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("MF_spInserOrUpdateSessionBranch", Params, "constrmf");


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
}