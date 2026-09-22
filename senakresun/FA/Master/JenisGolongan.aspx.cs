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

public partial class FA_Master_JenisGolongan : System.Web.UI.Page
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
         
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, "-", typeof(string));
        var[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
        var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


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
                    txtketerangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketerangan")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, txtjenisgol.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDGOLONGAN", typeof(string));
                    var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Golongan sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, txtjenisgol.Text, typeof(string));
                        var[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketerangan.Text, typeof(string));
                        var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Delete('" + textkode.InnerText + "','" + txtjenisgol.Text + "','" + textnama.InnerText + "','" + txtketerangan.Text + "');", true);
                            //Response.Redirect(Request.Url.AbsoluteUri);
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

    public void cekkode_change(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, txtjenisgol.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKODEGOLONGAN", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var1[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketerangan.Text, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("FA_JENISGOL", var1, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Jenis Golongan yang anda masukkan sudah ada..!!');", true);
                txtjenisgol.Text = "";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void cekketerangan_change(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 5, txtketerangan.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKETERANGAN", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var1[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketerangan.Text, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("FA_JENISGOL", var1, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Keterangan yang anda masukkan sudah ada..!!');", true);
                txtketerangan.Text = "";
            }
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
            btnSimpan.Enabled = false;
            if (txtjenisgol.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }
            if(txtjenisgol.Text.Trim() == string.Empty || txtketerangan.Text.Trim() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Jenis Golongan atau Keterangan Tidak Boleh Kosong..!!');", true);
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@Kdjenisgol", ParameterDirection.Input, SqlDbType.VarChar, 5, txtjenisgol.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketerangan.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                if (txtjenisgol.Enabled == true)
                { 
                    ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");
                    //ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");
                }
                else
                { 
                    ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");
                    //ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", var, "constrmf");
                }


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Simpan('" + textkode.InnerText + "','" + txtjenisgol.Text + "','" + textnama.InnerText + "','" + txtketerangan.Text + "');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(title('Data berhasil disimpan'),'" + txtketerangan.Text + "', 'success','top-end','1500' );", true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

}