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

public partial class FA_Master_Lokasi : System.Web.UI.Page
{

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BindGridEntity();
                txtalmtcabang.Attributes["maxlength"] = "150";
            }
            txtalmtcabang.Attributes["maxlength"] = "150";
            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        txtkodelokasi.Enabled = true;
        txtkodelokasi.Focus();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }


    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        //var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
        //var[1] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
        //var[2] = gb.CreateSPParameter("@alamatcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, "-", typeof(string));
        //var[3] = gb.CreateSPParameter("@user", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
        var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("FA_LOKASI", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeLokasi asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void cekkode_change(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, txtkodelokasi.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKODEGROUP", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("FA_LOKASI", var1, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Kode Lokasi yang anda masukkan sudah ada..!!');", true);
                txtkodelokasi.Text = "";
                txtkodelokasi.Focus();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
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

                    txtkodelokasi.Enabled = false;
                    txtkodelokasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdcabang")).Text;
                    txtnamalokasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnmcabang")).Text;
                    txtalmtcabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalmtcabang")).Text;

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

                    txtkodelokasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdcabang")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[7];
                    var1[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 6, txtkodelokasi.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[2] = gb.CreateSPParameter("@alamatcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, "-", typeof(string));
                    var1[3] = gb.CreateSPParameter("@user", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var1[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDLOKASI", typeof(string));
                    var1[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_LOKASI", var1, "constrmf");
                     

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    
                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Lokasi sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {


                        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[7];
                        var2[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 6, txtkodelokasi.Text, typeof(string));
                        var2[1] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var2[2] = gb.CreateSPParameter("@alamatcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, "-", typeof(string));
                        var2[3] = gb.CreateSPParameter("@user", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                        var2[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var2[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var2[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("FA_LOKASI", var2, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);

                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        }
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

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (txtkodelokasi.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@KodeLokasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodelokasi.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamalokasi.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@alamatcabang", ParameterDirection.Input, SqlDbType.VarChar, 150, txtalmtcabang.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@user", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            if (txtkodelokasi.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("FA_LOKASI", var, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("FA_LOKASI", var, "constrmf");
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


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

}