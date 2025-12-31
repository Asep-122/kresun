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


public partial class GL_Master_Glmst : System.Web.UI.Page
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
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
                Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                Params[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoa", Params, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlNoAwalGrup, "None", "");

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlNoAhirGrup, "None", "");

                DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[0];

                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_Typerek", Paramsx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlTypeRek, "None", "");



                DefaultClass.Global.SqlHelper[] Paramsy = new DefaultClass.Global.SqlHelper[2];
                Paramsy[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                Paramsy[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_Kategori", Paramsy, "constrmf");
                         // Params[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
     
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlKategori, "None", "");


                ddlNoAwalGrup.Items.Insert(0, new ListItem("-", "-"));
                ddlNoAhirGrup.Items.Insert(0, new ListItem("-", "-"));


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
        Paramsarea[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_GRIDGLMST", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";


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
        txtKodeGl.Enabled = true;
        txtKodeGl.Focus();
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

                    txtKodeGl.Enabled = false;
                    txtKodeGl.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeGl")).Text;
                    txtNamaGl.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaGl")).Text;
                    ddlKategori.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKategori")).Text;
                    ddlTypeRek.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTypeRek")).Text;
                    ddlKolom.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKolom")).Text;
                    ddlKodeMaster.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeMaster")).Text;
                    ddlNoAwalGrup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoAwalGrup")).Text;
                    ddlNoAhirGrup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoAhirGrup")).Text;
                    ddlcostcenter.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblccflag")).Text;
                    ddlcashflow.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Lblcfflag")).Text;
                    ddlbankid.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Lblbankid")).Text;
                    ddlAPid.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("LblApid")).Text;
                    ddlARid.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("LblArid")).Text;

                 


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
                    txtKodeGl.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeGl")).Text;

                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
                    Params[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 8, txtKodeGl.Text, typeof(string));
                    Params[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("GL_DELETEGLMST", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("dataada"))
                    {
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Akun sudah ada transaksi!');", true);

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
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[17];
            Params[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 8, txtKodeGl.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@NamaGl", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNamaGl.Text, typeof(string));
            Params[2] = gb.CreateSPParameter("@Kategori", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlKategori.SelectedValue, typeof(Int32));
            Params[3] = gb.CreateSPParameter("@TypeRek", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlTypeRek.SelectedValue, typeof(Int32));
            Params[4] = gb.CreateSPParameter("@Kolom", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlKolom.SelectedValue, typeof(Int32));
            Params[5] = gb.CreateSPParameter("@KodeMaster", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlKodeMaster.SelectedValue, typeof(Int32));
            Params[6] = gb.CreateSPParameter("@NoAwalGrup", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlNoAwalGrup.SelectedValue, typeof(Int32));
            Params[7] = gb.CreateSPParameter("@NoAhirGrup", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlNoAhirGrup.SelectedValue, typeof(Int32));
            Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[9] = gb.CreateSPParameter("@USERUPD", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
            Params[10] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            Params[11] = gb.CreateSPParameter("@ccflag", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcostcenter.SelectedValue, typeof(string));
            Params[12] = gb.CreateSPParameter("@cfflag", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcashflow.SelectedValue, typeof(string));
            Params[13] = gb.CreateSPParameter("@clflag", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbankid.SelectedValue, typeof(string));
            Params[14] = gb.CreateSPParameter("@arflag", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlARid.SelectedValue, typeof(string));
            Params[15] = gb.CreateSPParameter("@apflag", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlAPid.SelectedValue, typeof(string));
            Params[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            if (txtKodeGl.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("GL_INSERTGLMST", Params, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("GL_UPDATEGLMST", Params, "constrmf");
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