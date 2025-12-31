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
using System.Web.Script.Serialization;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Text.RegularExpressions;



public partial class Kresun_Process_EditAsuransi : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string id = "", no_nasabah = "", flag = "";

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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditAsuransi.aspx");
    }
    
    protected void btnBackForm_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        PnlFront.Visible = true;
    } 
    protected void btnSimpan_Click(object sender, EventArgs e)
    {

        try

        {
            pnladd.Visible = false;
            PnlFront.Visible = true;
            btnSimpan.Enabled = false;
            //btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[14];
            var[0] = gb.CreateSPParameter("@nopen", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnopen.Text.Trim(), typeof(string));
            var[1] = gb.CreateSPParameter("@idpermohonan", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var[2] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 250,  txtnip.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text.Trim(), typeof(string));
            var[4] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 50, txthp.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamat.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkelurahan.Text, typeof(string));
            var[7] = gb.CreateSPParameter("@kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkecamatan.Text.Trim(), typeof(string));
            var[8] = gb.CreateSPParameter("@kodepos", ParameterDirection.Input, SqlDbType.VarChar,250, txtkodepos.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@ibukandung", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamaibu.Text, typeof(string));
            var[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SIMPANEDIT", typeof(string));
            var[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[12] = gb.CreateSPParameter("@flagedit", ParameterDirection.Input, SqlDbType.VarChar, 50, "2", typeof(string));
            var[13] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
            

            ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Edit Done!','You successfully save this Project','top-right','success','1');", true);

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
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditAsuransi.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BindGridEntity(); 
            //btnSimpan.Enabled = true; 
        }


        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.btnSimpan);
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

                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    HDNoNasabah.Value = no_nasabah;
                    //HDUsia.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblusia")).Text;
                    //HDJangka.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTEDITVERIFPINJAMAN", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[5] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");
                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan');", true);

                    }
                    else
                    {
                        //Panel Data Diri Nasabah 
                        txtnopen.Text = ge.ListDataset.Tables[0].Rows[0]["no_pensiun"].ToString();
                        txtnip.Text = ge.ListDataset.Tables[0].Rows[0]["nip"].ToString(); 
                        txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                        txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                        txtusiamasuk.Text = ge.ListDataset.Tables[0].Rows[0]["usia"].ToString();
                        txtusiajt.Text = ge.ListDataset.Tables[0].Rows[0]["usiajt"].ToString();

                        txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                        txtkodepos.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                        txtnamaibu.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                        txtnonasabah.Text = no_nasabah;
                        txthp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                        txtkelurahan.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                        txtkecamatan.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
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

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "1", typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTEDITDATA", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id_permohonan asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

}