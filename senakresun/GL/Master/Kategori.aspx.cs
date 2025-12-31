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


public partial class GL_Master_Kategori : System.Web.UI.Page
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

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
                Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoa", Params, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlNoAwalGrup, "-", "0");

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlNoAhirGrup, "-", "0");
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
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        Paramsarea[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("GL_GRIDKATEGORI", Paramsarea, "constrmf");
       
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeKat asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();



    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        txtKodeKat.Enabled = true;
        txtKodeKat.Focus();
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
                    pnlHeader.Visible = false;
                    txtKodeKat.Enabled = false;
                    txtKodeKat.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeKat")).Text;
                    txtKetKategori.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKetKategori")).Text;
                    ddlNoAwalGrup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoAwalGrup")).Text;
                    ddlNoAhirGrup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoAhirGrup")).Text;

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
                    txtKodeKat.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeKat")).Text;

                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
                    Params[0] = gb.CreateSPParameter("@KodeKat", ParameterDirection.Input, SqlDbType.VarChar, 5, txtKodeKat.Text, typeof(string));
                    Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                    Params[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("GL_DELETEKATEGORI", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("dataada"))
                    {
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:cancelalert('Kategori sudah ada transaksi!');", true);

                    }
                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
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
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected string btnremodal()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    }
    protected void lnkRefreshModal_Click(object sender, EventArgs e)
    {

        BindGridEntity();
          ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[8];
            Params[0] = gb.CreateSPParameter("@KodeKat", ParameterDirection.Input, SqlDbType.VarChar, 5, txtKodeKat.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            Params[2] = gb.CreateSPParameter("@KetKategori", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKetKategori.Text, typeof(string));
            Params[3] = gb.CreateSPParameter("@NoAwalGrup", ParameterDirection.Input, SqlDbType.Int, 0, ddlNoAwalGrup.SelectedValue, typeof(Int32));
            Params[4] = gb.CreateSPParameter("@NoAhirGrup", ParameterDirection.Input, SqlDbType.Int, 0, ddlNoAhirGrup.SelectedValue, typeof(Int32));
            Params[5] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[6] = gb.CreateSPParameter("@USERUPD", ParameterDirection.Input, SqlDbType.VarChar, 15, SessLoginID, typeof(string));
            Params[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            if (txtKodeKat.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("GL_INSERTKATEGORI", Params, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("GL_UPDATEKATEGORI", Params, "constrmf");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'","") + "');", true);
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