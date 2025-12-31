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


public partial class AR_Process_VerifikasiPinjaman : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BindGridEntity();

            btnTolak.Enabled = true;
            btnSimpan.Enabled = true;

            // --- Combo Jenis Kelamin
            ge.ListDataset = gb.GetDataFunction("dbo.GetMasterAsuransi()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAsuransi asc";

            ddlkodeasuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkodeasuransi.DataTextField = "asuransi";
            ddlkodeasuransi.DataValueField = "KodeAsuransi";
            ddlkodeasuransi.DataBind();
            ddlkodeasuransi.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }


        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnSimpan);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("VerifikasiAsuransi.aspx");
    }
    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VerifikasiAsuransi.aspx");
    }

  
    protected void btnSimpan_Click(object sender, EventArgs e)
    {

        if(ddlkodeasuransi.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Asuransi Belum Di pilih !');", true);

        }
        else
        {
            ProsesVerifikasi("2", SessLoginID);
             

            double selisihasuransideb = 0; 

            selisihasuransideb = double.Parse(txtadmiasuransi.Text) - double.Parse(txtpremigross.Text);

                       
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var[1] = gb.CreateSPParameter("@RateAsuransi", ParameterDirection.Input, SqlDbType.Money, 50, txtrateasuransi.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnonasabah.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@PremiGross", ParameterDirection.Input, SqlDbType.Money, 200, txtpremigross.Text.Replace(".", ""), typeof(string));
            var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATERATEASURANSI", typeof(string));
            var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[6] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodeasuransi.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@selisihasuransideb", ParameterDirection.Input, SqlDbType.VarChar, 50, selisihasuransideb, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");


            ReportDocument rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Kresun/Report/CetakValidasi.rpt"));
            rprt.SetParameterValue("NamaNasabah", txtnamanasabah.Text);
            rprt.SetParameterValue("TanggalLahir", txttgllahir.Text);
            rprt.SetParameterValue("UsiaMasuk", txtusiamasuk.Text);
            rprt.SetParameterValue("UsiaJatuhTempo", txtusiajt.Text);
            rprt.SetParameterValue("Plafon", gb.ToRupiah(txtplafon.Text));
            rprt.SetParameterValue("Jangka", txtjangka.Text);
            rprt.SetParameterValue("HP", txthp.Text);
            rprt.SetParameterValue("cabang", txtcabang.Text);
            rprt.SetParameterValue("SPV", SessLoginID);
            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


            Response.End();
        }
      
    }

    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        ProsesVerifikasi("2B", SessLoginID);
    }

    protected void btnsimpanedit_Click(object sender, EventArgs e)
    {
        try

        { 
            btnsimpanedit.Enabled = false;
            //btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var11 = new DefaultClass.Global.SqlHelper[7];
            var11[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnonasabah.Text.Trim(), typeof(string));
            var11[1] = gb.CreateSPParameter("@idpermohonan", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var11[2] = gb.CreateSPParameter("@alasanedit", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalasanedit.Text, typeof(string));
            var11[3] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
            var11[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATE", typeof(string));
            var11[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var11[6] = gb.CreateSPParameter("@flagedit", ParameterDirection.Input, SqlDbType.VarChar, 50, "1", typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var11, "constrmf");

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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        PnlFront.Visible = false;
        pnladd.Visible = true;
        pnlUpdAsuransi.Visible = false;
    }

    protected void btnBackForm_Click(object sender, EventArgs e)
    {
        PnlFront.Visible = true;
        pnladd.Visible = false;
        pnlUpdAsuransi.Visible = false;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if(ddlkodeasuransi.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Asuransi belum di pilih');", true);

        }
        else
        {
            PnlFront.Visible = false;
            pnladd.Visible = false;
            pnlUpdAsuransi.Visible = true;
        }
       
    }
    

    protected void btnBackTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true; 
        pnlAlasanTolak.Visible = false;
    }
    protected void btnbackedit_Click(object sender, EventArgs e)
    {
        pnlUpdAsuransi.Visible = true;
        pnlEdit.Visible = false;
    }
    
    protected void btnTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlAlasanTolak.Visible = true;
        pnlUpdAsuransi.Visible = false;
    }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlEdit.Visible = true;
        pnlUpdAsuransi.Visible = false;
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

                    HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    HDUsia.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblusia")).Text;
                    HDJangka.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;
                    string ambilcabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblcabang")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                    var[1] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ambilcabang, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[5] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                    {//Panel Data Diri Nasabah
                        txttglpengajuan.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_pengajuan"].ToString();
                        txtnip.Text = ge.ListDataset.Tables[0].Rows[0]["nip"].ToString();
                        txtnopen.Text = ge.ListDataset.Tables[0].Rows[0]["no_pensiun"].ToString();
                        txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                        txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                        txtusiamasuk.Text = ge.ListDataset.Tables[0].Rows[0]["usia"].ToString();
                        txtusiajt.Text = ge.ListDataset.Tables[0].Rows[0]["usiajt"].ToString();
                        txtcabang.Text = ge.ListDataset.Tables[0].Rows[0]["cabang"].ToString();
                        txtarea.Text = ge.ListDataset.Tables[0].Rows[0]["area"].ToString();


                        txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                        txtkodepos.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                        txtnamaibu.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                        txtnonasabah.Text = HDNoNasabah.Value;
                        txtplafon.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString().Replace(".00", "").Replace(" ", "")).Replace("Rp", "");

                        txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                        txthp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                        txtkelurahan.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                        txtkecamatan.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
                        txtkoderefferal.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                        txtpinjamanke.Text = ge.ListDataset.Tables[0].Rows[0]["datanasabah"].ToString();

                        txttgljt.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_jatuhtempo"].ToString();
                        imgfoto1.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["path_foto1"].ToString();
                        imgfoto2.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["path_foto2"].ToString();
                        imgfoto3.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["path_foto3"].ToString();
                        imgfoto4.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["path_foto4"].ToString();
                        imgfoto5.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["path_foto5"].ToString();
                        txtbgc.Text = ge.ListDataset.Tables[0].Rows[0]["rateasu"].ToString();
                        txtadmiasuransi.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_admasuransi"].ToString()).Replace("Rp", "");
                    }else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan, periksa kembali inputan anda');", true);
                         
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

    public void ProsesVerifikasi(string flag, string user)
    {

        try

        {
            btnSimpan.Enabled = false;
            //btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, user, typeof(string));
            var[2] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnonasabah.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 200, txtalasantolak.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VERIFIKASISTATUS", typeof(string));
            var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[6] = gb.CreateSPParameter("@siklus_trx", ParameterDirection.Input, SqlDbType.VarChar, 50, flag, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

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

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "1", typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLISTVERIFIKASI", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id_permohonan asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void ddlkodeasuransi_SelectedIndexChanged(object sender, EventArgs e)
    {
        
            double rate = 0;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.VarChar, 50, HDUsia.Value, typeof(string));
        var[1] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 50, HDJangka.Value, typeof(string));
        var[2] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodeasuransi.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETRATEASURANSI", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");

        if(ge.ListDataset.Tables[0].DefaultView.Count != 0)
        {
            if (ddlkodeasuransi.SelectedIndex != 0)
            {
                txtrateasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();

                rate = double.Parse(txtplafon.Text) * double.Parse(txtrateasuransi.Text) / 100;

                txtpremigross.Text = gb.ToRupiah(rate.ToString()).Replace("Rp", "");

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan');", true);

                txtpremigross.Text = "";
                txtrateasuransi.Text = "";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan, periksa kembali inputan anda');", true);

            txtpremigross.Text = "";
            txtrateasuransi.Text = "";
        }



    }
        
}