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

public partial class SN_Master_JangkaWaktuBunga : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string tanggal = "";
    int nourut;
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

    protected void txtbungapertahun_TextChanged(object sender, EventArgs e)
    {
        decimal jlhbayar = 0;
        if (decimal.Parse(txtbungapertahun.Text) >= 0)
        {


            jlhbayar = ((decimal.Parse(txtbungapertahun.Text) / 100) / 365) * 100;
            txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
        }
        else
        {
            txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
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
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEPRODUK", typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK_JANGKA_BUNGA", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "Value asc";

                ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlproduk.DataTextField = "Text";
                ddlproduk.DataValueField = "Value";
                ddlproduk.DataBind();
                ddlproduk.Items.Insert(0, new ListItem("Pilih Data", "-"));
                
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
    protected void ddljangka_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

        
        txtnamaproduk.Text = ddlproduk.SelectedItem.Text;





        DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];

        varx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEPRODUKJANGKA", typeof(string));
        varx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varx[2] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlproduk.SelectedValue, typeof(string));
        varx[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK_JANGKA_BUNGA", varx, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

        ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljangka.DataTextField = "jangka";
        ddljangka.DataValueField = "jangka";
        ddljangka.DataBind();
        ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));
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


        ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK_JANGKA_BUNGA", var, "constrmf");

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
        hdnnourut.Value = "";
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "", createuser = "", createddate = "", updateuser = "", updatedate = "";
        try
        {
            if (hdnnourut.Value == "")
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
                nourut = Convert.ToInt32(hdnnourut.Value);
            }
            btnSimpan.Enabled = false;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[14];
            var[0] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlproduk.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@NamaProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaproduk.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, createuser, typeof(string));
            var[3] = gb.CreateSPParameter("@updateuser", ParameterDirection.Input, SqlDbType.VarChar, 50, updateuser, typeof(string));
            var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[5] = gb.CreateSPParameter("@statusaktif", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatus.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@Jangka", ParameterDirection.Input, SqlDbType.Int, 0, ddljangka.SelectedValue, typeof(int));
            var[7] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[9] = gb.CreateSPParameter("@Minimal_Saldo", ParameterDirection.Input, SqlDbType.Decimal, 0, txtminimalsaldo.Text, typeof(decimal));
            var[10] = gb.CreateSPParameter("@BungaTahunan", ParameterDirection.Input, SqlDbType.Float, 0, txtbungapertahun.Text, typeof(float));
            var[11] = gb.CreateSPParameter("@BungaHarian", ParameterDirection.Input, SqlDbType.Float, 0, txtbungaperhari.Text, typeof(float));
            var[12] = gb.CreateSPParameter("@urut", ParameterDirection.Input, SqlDbType.Int, 0, nourut, typeof(int));
            var[13] = gb.CreateSPParameter("@update", ParameterDirection.Input, SqlDbType.Int, 0,int.Parse(ddlupdate.SelectedValue), typeof(int));


            ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK_JANGKA_BUNGA", var, "constrmf");
             


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

                    ddlproduk.Enabled = false;
                    ddlproduk.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeproduk")).Text.Trim();
                    txtnamaproduk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamaproduk")).Text.Trim();
                    status = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    
                    ddljangka.SelectedValue = string.Format("{0:N0}", int.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblJangka")).Text));
                    ddljangka.Enabled = false;
                    txtminimalsaldo.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblMinimal_Saldo")).Text));
                    txtbungapertahun.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBungaTahunan")).Text));
                    txtbungaperhari.Text = string.Format("{0:N6}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBungaHarian")).Text));
                    hdnnourut.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblurut")).Text;
                    if (status == "AKTIF")
                    {
                        ddlstatus.SelectedValue = "0";
                    }
                    else
                    {
                        ddlstatus.SelectedValue = "1";

                    }
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];

                    varx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEPRODUKJANGKA", typeof(string));
                    varx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varx[2] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlproduk.SelectedValue, typeof(string));
                    varx[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK_JANGKA_BUNGA", varx, "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

                    ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljangka.DataTextField = "jangka";
                    ddljangka.DataValueField = "jangka";
                    ddljangka.DataBind();
                    ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    txtbungaperhari.Enabled = false;
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
                    ddlproduk.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeproduk")).Text;
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];

                    varx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKTRNPRODUKJANGKA", typeof(string));
                    varx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varx[2] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlproduk.SelectedValue, typeof(string));
                    varx[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK_JANGKA_BUNGA", varx, "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

                    ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljangka.DataTextField = "jangka";
                    ddljangka.DataValueField = "jangka";
                    ddljangka.DataBind();
                    ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));
                    string asd = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblJangka")).Text;
                    ddljangka.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblJangka")).Text;
                    hdnnourut.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblurut")).Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6]; 
                    var1[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string)); ;
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[3] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0,ddljangka.SelectedValue, typeof(int));
                    var1[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var1[5] = gb.CreateSPParameter("@urut", ParameterDirection.Input, SqlDbType.VarChar, 10, hdnnourut.Value, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK_JANGKA_BUNGA", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Produk Jangka sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {
                        //DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
                        //var2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                        //var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        //var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[6];
                        var2[0] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlproduk.SelectedValue, typeof(string));
                        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var2[3] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, ddljangka.SelectedValue, typeof(int)); ;
                        var2[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var2[5] = gb.CreateSPParameter("@urut", ParameterDirection.Input, SqlDbType.Int, 0, hdnnourut.Value, typeof(int));

                        ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK_JANGKA_BUNGA", var2, "constrmf");

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