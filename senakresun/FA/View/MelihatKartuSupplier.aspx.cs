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

public partial class FA_View_MelihatKartuSupplier : System.Web.UI.Page
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        txtjenisgol.Enabled = true;
        txtjenisgol.Focus();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, "-", typeof(string));
        var[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kd_jenis_gol asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

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

                    txtjenisgol.Enabled = false;
                    txtjenisgol.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljenisgol")).Text;
                    txtketerangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketerangan")).Text;

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
                    txtjenisgol.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljenisgol")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                    var1[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, txtjenisgol.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDGOLONGAN", typeof(string));
                    var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Golongan sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                        var[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, txtjenisgol.Text, typeof(string));
                        var[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketerangan.Text, typeof(string));
                        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);

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
            if (txtjenisgol.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, txtjenisgol.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketerangan.Text, typeof(string));

            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            if (txtjenisgol.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");
                ClientScript.RegisterStartupScript(GetType(), "hwa", "simpanalert();", true);
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(position: top - end, icon: success, title: 'Data berhasil disimpan', text: 'Nomer nasabah anda :' , showConfirmButton: false, timer: 1500);", true);
                ClientScript.RegisterStartupScript(GetType(), "hwa", "simpanalert();", true);
            }


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Data berhasil disimpan','Nomer nasabah anda :" + txtjenisgol + "' ,'top-end','success','1500');", true);


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