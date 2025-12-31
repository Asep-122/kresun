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

public partial class FA_BiayaSewa : System.Web.UI.Page
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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[3] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Fa_Sewa", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_Sewa asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CABANG", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Fa_Sewa", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
        ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabang.DataTextField = "nama_cabang";
        ddlcabang.DataValueField = "kode_cabang";
        ddlcabang.DataBind();
        ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));
 

    }
    protected void txtnilai_TextChanged(object sender, EventArgs e)
    {
        decimal jlhbayar = 0;
        if (decimal.Parse(txtBiaya_Sewa.Text) > 0)
        {


            jlhbayar = ((decimal.Parse(txtPajak.Text)/100) * decimal.Parse(txtBiaya_Sewa.Text));
            txtnilaipajak.Text = string.Format("{0:N2}", jlhbayar);
        }
        else
        if (decimal.Parse(txtPajak.Text) > 0)
        {


            jlhbayar = ((decimal.Parse(txtPajak.Text) / 100) * decimal.Parse(txtBiaya_Sewa.Text));
            txtnilaipajak.Text = string.Format("{0:N2}", jlhbayar);
        }
        else
        {
            txtnilaipajak.Text = string.Format("{0:N6}", jlhbayar);
        }


    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        btnSimpan.Enabled = true;
        txtkode_Sewa.Enabled = false;
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "", createuser = "", createddate = "", updateuser = "", updatedate = "";
        try
        {
            if (txtkode_Sewa.Text == "")
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

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[13];
            var[0] = gb.CreateSPParameter("@kode_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkode_Sewa.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@Nama_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 150, txtnamaswa.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@NamaCabang", ParameterDirection.Input, SqlDbType.VarChar, 150, ddlcabang.SelectedItem.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.Int,0,int.Parse(ddlstatus.SelectedValue), typeof(int));
            var[5] = gb.CreateSPParameter("@Biaya_Sewa", ParameterDirection.Input, SqlDbType.Decimal, 0, txtBiaya_Sewa.Text, typeof(decimal));
            var[6] = gb.CreateSPParameter("@Pajak", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtPajak.Text), typeof(float));
            var[7] = gb.CreateSPParameter("@NilaiPajak", ParameterDirection.Input, SqlDbType.Decimal, 0,txtnilaipajak.Text, typeof(decimal));
            var[8] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, createuser, typeof(string));
            var[9] = gb.CreateSPParameter("@updateuser", ParameterDirection.Input, SqlDbType.VarChar, 50, updateuser, typeof(string));
            var[10] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));        
            var[11] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[12] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Fa_Sewa", var, "constrmf");
             


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

                    txtkode_Sewa.Enabled = false;

                    txtkode_Sewa.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeSewa")).Text.Trim();
                    ddlcabang.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKode_cabang")).Text.Trim();

                    txtnamaswa.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamaBiaya")).Text.Trim();

                   

                    txtBiaya_Sewa.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBiaya_Sewa")).Text.Trim();

                    txtPajak.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPajak")).Text.Trim();

                    txtnilaipajak.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNilaiPajak")).Text.Trim();

                    ddlstatus.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text.Trim();


                    
                    
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
                    txtkode_Sewa.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeSewa")).Text;
                    ddlcabang.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKode_cabang")).Text;               
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5]; 
                    var1[0] = gb.CreateSPParameter("@kode_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkode_Sewa.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                    var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                    var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("Fa_Sewa", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Sewa sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {                    

                        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                        var2[0] = gb.CreateSPParameter("@kode_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkode_Sewa.Text, typeof(string));
                        var2[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));
                        var2[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                        var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("Fa_Sewa", var2, "constrmf");

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