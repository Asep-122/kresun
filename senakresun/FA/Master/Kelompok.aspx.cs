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

public partial class FA_Master_Kelompok : System.Web.UI.Page
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

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGOLONGAN", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                ddlgol.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlgol.DataTextField = "TEXT";
                ddlgol.DataValueField = "value";
                ddlgol.DataBind();
                ddlgol.Items.Insert(0, new ListItem("Pilih Kode Golongan...", ""));
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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
        var[0] = gb.CreateSPParameter("@KodeGolongan", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
        var[2] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
        var[3] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
        var[4] = gb.CreateSPParameter("@SL", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
        var[5] = gb.CreateSPParameter("@DB", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
        var[6] = gb.CreateSPParameter("@User", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
        var[7] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
        var[8] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeKel asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

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

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;

        txtkdKel.Enabled = true;
        txtkdKel.Focus();

        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        //var[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, "-", typeof(string));
        //var[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
        //var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        //var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ////var[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));


        //ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "kd_jenis_gol asc";


        //ddlgol.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlgol.DataTextField = "jns";
        //ddlgol.DataValueField = "kd_jenis_gol";
        //ddlgol.DataBind();
    }

    protected void txtmasa_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int bulan = 0;
            if (txtmasa.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masa Manfaat Bulan Harus Di Isi');", true);
                txtbulan.Focus();
            }
            else if(Convert.ToInt32(txtmasa.Text) != 0)
            {
                bulan = int.Parse(txtmasa.Text) * 12;
                txtbulan.Text  = bulan.ToString();
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void cekkode_change(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 5, txtkdKel.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKODEKELOMPOK", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var1[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketerangan.Text, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("FA_KELOMPOK", var1, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Kode Kelompok yang anda masukkan sudah ada..!!');", true);
                txtkdKel.Text = "";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void cekbulan_change(object sender, EventArgs e)
    {
        try
        {
            //if(Convert.ToInt32(txtbulan.Text) < 3)
            //{
            //    txtbulan.Text = "0";
            //}
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (txtkdKel.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
                var[0] = gb.CreateSPParameter("@KodeGolongan", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlgol.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkdKel.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnama.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Float, 10, txtmasa.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@SL", ParameterDirection.Input, SqlDbType.Float, 10, txtSL.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@DB", ParameterDirection.Input, SqlDbType.Float, 10, txtDB.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@User", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[7] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Int, 10, txtbulan.Text, typeof(string));
                var[8] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            if (txtkdKel.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var, "constrmf");
            }

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            { 
				ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
				//ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Simpan('" + textkelompok.InnerText + "','" + txtkdKel.Text + "','" + textgolongan.InnerText + "','" + ddlgol.SelectedItem + "');", true);
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
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;

                    txtkdKel.Enabled = false;
                    txtkdKel.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdKel")).Text;
                    ddlgol.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdGol")).Text;
                    txtnama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNama")).Text;
                    txtmasa.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblmasa")).Text;
                    txtSL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblSL")).Text; 
                    txtbulan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbulan")).Text;


              
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
                    string kodegolongan = "";
                    txtkdKel.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdKel")).Text;
                    kodegolongan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdGol")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[12];
                    var1[0] = gb.CreateSPParameter("@KodeGolongan", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
                    var1[1] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkdKel.Text, typeof(string));
                    var1[2] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                    var1[3] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
                    var1[4] = gb.CreateSPParameter("@SL", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
                    var1[5] = gb.CreateSPParameter("@DB", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
                    var1[6] = gb.CreateSPParameter("@User", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var1[7] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
                    var1[8] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[9] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var1[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDKELOMPOK", typeof(string));
                    var1[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kelompok sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];

                        var[0] = gb.CreateSPParameter("@KodeGolongan", ParameterDirection.Input, SqlDbType.VarChar, 20, kodegolongan, typeof(string));
                        var[1] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkdKel.Text, typeof(string));
                        //var[2] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                        //var[3] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                        //var[4] = gb.CreateSPParameter("@SL", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                        //var[5] = gb.CreateSPParameter("@DB", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                        //var[6] = gb.CreateSPParameter("@User", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                        //var[7] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
                        var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        //var[9] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var, "constrmf");
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

