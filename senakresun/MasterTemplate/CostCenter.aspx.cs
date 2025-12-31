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


public partial class GL_Master_CostCenter : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                
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

    public void BindGridEntity()
    {


        //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[0];
        DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[1];
        //Paramsarea[0] = gb.CreateSPParameter("@reg_id", ParameterDirection.Input, SqlDbType.Int, 0, ddlRegional.SelectedValue, typeof(Int32));
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_GRIDCOSTCENTER", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCc asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnAdd_ServerClick(object sender, EventArgs e)
    {
        pnlHeader.Visible = true;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        txtKodeCc.Enabled = true;
        txtKodeCc.Focus();
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    //ddlRegional.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegID")).Text != string.Empty ? ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegID")).Text : ddlRegional.SelectedValue;


                    //DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[1];
                    //Paramsarea[0] = gb.CreateSPParameter("@reg_id", ParameterDirection.Input, SqlDbType.Int, 0, ddlRegional.SelectedValue, typeof(Int32));

                    //ge.ListDataset = gb.GetDataJustOutPut("dbo.HCIS_SP_Area", Paramsarea, "constrhrms");

                    //ge.ListDataset.Tables[0].DefaultView.Sort = "[TEXT] asc";

                    //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlArea, "None", "");

                    //ddlArea.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAreaID")).Text != string.Empty ? ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAreaID")).Text : ddlArea.SelectedValue;
                    txtKodeCc.Enabled = false;
                    txtKodeCc.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeCc")).Text;
                    txtNamaCC.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaCC")).Text;
                    txtKetCC.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKetCC")).Text;

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":

                try
                {
                    txtKodeCc.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeCc")).Text;

                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
                    Params[0] = gb.CreateSPParameter("@KodeCc", ParameterDirection.Input, SqlDbType.VarChar, 5, txtKodeCc.Text, typeof(string));
                    Params[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("GL_DELETECOSTCENTER", Params, "constrPOS");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','1');", true);


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

                break;
        }
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];
            Params[0] = gb.CreateSPParameter("@KodeCc", ParameterDirection.Input, SqlDbType.VarChar, 5, txtKodeCc.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@NamaCC", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNamaCC.Text, typeof(string));
            Params[2] = gb.CreateSPParameter("@KetCC", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKetCC.Text, typeof(string));
            Params[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[4] = gb.CreateSPParameter("@USERUPD", ParameterDirection.Input, SqlDbType.VarChar, 15, SessLoginID, typeof(string));
            Params[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            if (txtKodeCc.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("GL_INSERTCOSTCENTER", Params, "constrPOS");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("GL_UpdateCOSTCENTER", Params, "constrPOS");
            }


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);


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

}