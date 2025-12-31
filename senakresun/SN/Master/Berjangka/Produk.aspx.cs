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

public partial class AR_Master_Produk : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string tanggal = "";
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
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }


    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        btnSimpan.Enabled = true;
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "", createuser = "", createddate = "", updateuser = "", updatedate = "";
        try
        {
            if (txtkodeproduk.Enabled == true)
            {
                STATE = "INSERT";
                createuser = SessLoginID;
                createddate = DateTime.Now.ToString();
                updatedate = "";
                updateuser = "";
            }
            else
            {
                STATE = "UPDATE";
                createuser = "";
                createddate = "";
                updatedate = DateTime.Now.ToString();
                updateuser = SessLoginID;
            }
            btnSimpan.Enabled = false;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
            var[0] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodeproduk.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@NamaProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaproduk.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, createuser, typeof(string));
            var[3] = gb.CreateSPParameter("@updateuser", ParameterDirection.Input, SqlDbType.VarChar, 50, updateuser, typeof(string));
            var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[5] = gb.CreateSPParameter("@statusaktif", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatus.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@Minimal_Saldo", ParameterDirection.Input, SqlDbType.Decimal, 0, txtminimalsaldo.Text, typeof(decimal));
            var[7] = gb.CreateSPParameter("@BungaTahunan", ParameterDirection.Input, SqlDbType.Float, 0, "0", typeof(float));
            var[8] = gb.CreateSPParameter("@BungaHarian", ParameterDirection.Input, SqlDbType.Float, 0, "0", typeof(float));
            var[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

          
                ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK", var, "constrmf");
             


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

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string   status = "", typeproduk = "";

        switch (e.CommandName)
        {
            case "SET":

                try

                { 

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;

                    txtkodeproduk.Enabled = false;
                    txtkodeproduk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeproduk")).Text.Trim();
                    txtnamaproduk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamaproduk")).Text.Trim();
                    status = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    
                    txtminimalsaldo.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblMinimal_Saldo")).Text));
                    //txtbungapertahun.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBungaTahunan")).Text));
                    //txtbungaperhari.Text = string.Format("{0:N6}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBungaHarian")).Text));
                    if (status == "AKTIF")
                    {
                        ddlstatus.SelectedValue = "0";
                    }
                    else
                    {
                        ddlstatus.SelectedValue = "1";

                    }

                    
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
                    txtkodeproduk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeproduk")).Text; 
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3]; 
                    var1[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodeproduk.Text, typeof(string)); ;
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Produk sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {

                     

                        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                        var2[0] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodeproduk.Text, typeof(string));
                        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK", var2, "constrmf");

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

}