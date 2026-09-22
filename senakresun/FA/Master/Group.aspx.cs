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

public partial class FA_Master_Group : System.Web.UI.Page
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
        txtkodegroup.Enabled = true;
        txtkodegroup.Focus();
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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("FA_Group", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_group asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void cekkode_change(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 3, txtkodegroup.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKODEGROUP", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("FA_Group", var1, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Kode Group yang anda masukkan sudah ada..!!');", true);
                txtkodegroup.Text = "";
                txtkodegroup.Focus();
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

                    txtkodegroup.Enabled = false;
                    txtkodegroup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdgroup")).Text;
                    txtnmgroup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnmgroup")).Text;
                 
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
                    txtkodegroup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdgroup")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@Kdgroup", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodegroup.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDGROUP", typeof(string));
                    var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("FA_Group", var1, "constrmf");
                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Group sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@Kdgroup", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodegroup.Text, typeof(string));
                        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("FA_Group", var, "constrmf");
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
            if (txtkodegroup.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }

           
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@Kdgroup", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodegroup.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@Nmgroup", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnmgroup.Text, typeof(string));
               
                var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                if (txtkodegroup.Enabled == true)
                {
                    ge.ListDataset = gb.GetDataJustOutPut("FA_Group", var, "constrmf");
                }
                else
                {
                    ge.ListDataset = gb.GetDataJustOutPut("FA_Group", var, "constrmf");
                }


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Simpan('" + textkode.InnerText + "','" + txtkodegroup.Text + "','" + textnama.InnerText + "','" + txtnmgroup.Text + "');", true);
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