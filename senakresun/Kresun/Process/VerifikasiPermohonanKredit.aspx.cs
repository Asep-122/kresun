using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Globalization;


public partial class Kresun_Process_VerifikasiPermohonanKredit : System.Web.UI.Page
{

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    DateTime tgl_libur, tgl_realisasi, tgldrop;
    double angsuranperbulan = 0;
    string no_nasabah = "", agama = "", usia = "", pokok = "", bunga = "", namafileJaminan = "", fotopathJaminan = "";

    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
            BindGridEntity();

            btnTolak.Enabled = true;
            btnSimpan.Enabled = true;
            PnlFront.Visible = true;
            
            ge.ListDataset = gb.GetDataFunction("dbo.Getmasterkodejiwa()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_jiwa asc";

            ddlkodejiwa.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkodejiwa.DataTextField = "kdjiwa";
            ddlkodejiwa.DataValueField = "kode_jiwa";
            ddlkodejiwa.DataBind();


            ge.ListDataset = gb.GetDataFunction("dbo.KPGetNominalTransfer()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_bank asc";
            ddlBankNasabah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlBankNasabah.DataTextField = "nama_bank";
            ddlBankNasabah.DataValueField = "nama_bank";
            ddlBankNasabah.DataBind();



            ge.ListDataset = gb.GetDataFunction("dbo.GetPenggunaan()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_penggunaan asc";

            ddlPenggunaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlPenggunaan.DataTextField = "penggunaan";
            ddlPenggunaan.DataValueField = "kode_penggunaan";
            ddlPenggunaan.DataBind();
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnSimpan);

            //Page.Form.Attributes.Add("enctype", "multipart/form-data");


        }
    }
    public string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }

    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("VerifikasiPermohonanKredit.aspx");
    }
    //protected void btnBackTolak_Click(object sender, EventArgs e)
    //{
    //    pnladd.Visible = true;
    //    pnlAlasanTolak.Visible = false;
    //}
    protected void btnback_Click(object sender, EventArgs e)
    {
        PnlFront.Visible = true;
        pnl1.Visible = false;
    }

    protected void btnBack2_Click(object sender, EventArgs e)
    {
        pnl1.Visible = true;
        pnl2.Visible = false;
        pnlSK.Visible = false;
    }

    protected void btnBack3_Click(object sender, EventArgs e)
    {
        pnl5.Visible = true; 
        pnl3.Visible = false;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnl2.Visible = true;
        pnlSK.Visible = true;
        pnl1.Visible = false;
    }
    protected void btnNext2_Click(object sender, EventArgs e)
    {
        pnl5.Visible = true;
        pnl2.Visible = false;
        pnlSK.Visible = false;
    }
    protected void btnNext3_Click(object sender, EventArgs e)
    {
        pnl4.Visible = true;
        pnl3.Visible = false;
    }
    protected void btnnext5_Click(object sender, EventArgs e)
    {
        pnl3.Visible = true;
        pnl5.Visible = false;
    }

    protected void btnBack4_Click(object sender, EventArgs e)
    {
        pnl3.Visible = true;
        pnl4.Visible = false;
    }

    protected void btnback5_Click(object sender, EventArgs e)
    {
        pnl2.Visible = true;
        pnlSK.Visible = true;
        pnl5.Visible = false;
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        pnl4.Visible = false;
        pnlEdit.Visible = true;
    }
    protected void btnbackedit_Click(object sender, EventArgs e)
    {
        pnl4.Visible = true;
        pnlEdit.Visible = false;
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
    protected void btnTolak_Click(object sender, EventArgs e)
    {
        pnl1.Visible = false;
        pnl2.Visible = false;
        pnl3.Visible = false;
        pnl4.Visible = false;
        pnlAlasanTolak.Visible = true;
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        ProsesVerifikasi("3", SessLoginID);
    }
    
    protected void btnBackPendapatan_Click(object sender, EventArgs e)
    {
        Response.Redirect("VerifikasiPermohonanKredit.aspx");
    }
    
    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        ProsesVerifikasi("3B", SessLoginID);
    } 
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "2", typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLISTVERIFIKASI", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id_permohonan asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void btnBackTolak_Click(object sender, EventArgs e)
    {
        pnl4.Visible = true;
        pnlAlasanTolak.Visible = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("VerifikasiPermohonanKredit.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VerifikasiPermohonanKredit.aspx");
    }

    public void ProsesVerifikasi(string flag, string user)
    {

        try

        {
            btnSimpan.Enabled = false;
            btnSimpanTolakan.Enabled = false;
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
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {
                    // --- Combo  Sumber Dana
                    ge.ListDataset = gb.GetDataFunction("dbo.getSumberDana()", "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "kdpersh asc";

                    ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlsumberdana.DataTextField = "sumberdana";
                    ddlsumberdana.DataValueField = "kodebank";
                    ddlsumberdana.DataBind();
                    ddlsumberdana.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    ge.ListDataset = gb.GetDataFunction("dbo.Getmasterkodejiwa()", "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "kode_jiwa asc";

                    ddlkodejiwa.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlkodejiwa.DataTextField = "kdjiwa";
                    ddlkodejiwa.DataValueField = "kode_jiwa";
                    ddlkodejiwa.DataBind();


                    ge.ListDataset = gb.GetDataFunction("dbo.KPGetNominalTransfer()", "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "kode_bank asc";
                    ddlBankNasabah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlBankNasabah.DataTextField = "nama_bank";
                    ddlBankNasabah.DataValueField = "nama_bank";
                    ddlBankNasabah.DataBind();



                    ge.ListDataset = gb.GetDataFunction("dbo.GetPenggunaan()", "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "kode_penggunaan asc";

                    ddlPenggunaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlPenggunaan.DataTextField = "penggunaan";
                    ddlPenggunaan.DataValueField = "kode_penggunaan";
                    ddlPenggunaan.DataBind();
                    ddlPenggunaan.Items.Insert(0, new ListItem("Pilih Data", "-"));
                    PnlFront.Visible = false;
                    pnl1.Visible = true;

                    HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    string ambilcabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblcabang")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                    var[1] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ambilcabang, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[5] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 20, "2", typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");


                    //Panel 1
                    txtnonasabah.Text = HDNoNasabah.Value;
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                    txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                    txtjenkel.Text = ge.ListDataset.Tables[0].Rows[0]["jenkel"].ToString(); 
                    txtempatlahir.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                    txtnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                    txtkelurahan.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                    txtkecamatan.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
                    txtkota.Text = ge.ListDataset.Tables[0].Rows[0]["nama_provinsi"].ToString();
                    txtkodepos.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                    txtkdproduk.Text = "35 - KREDIT PENSIUN";
                    txtkodeproduk.Text = "35 - KREDIT PENSIUN";
                    txtprogrampinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["programpinjaman"].ToString();
                    txtrealisasi.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_pengajuan"].ToString();
                    txttgllahirasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                    txtAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    txtnonAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff_non"].ToString();
                    txttgllahir.Text = txttgllahirasuransi.Text;

                    //Panel 2

                    txtstspens.Text = ge.ListDataset.Tables[0].Rows[0]["stspens"].ToString();
                    txtktp.Text = txtidentitas.Text;
                    txtnamapensiun.Text = txtnamanasabah.Text;
                    txtnip.Text = ge.ListDataset.Tables[0].Rows[0]["nip"].ToString();
                    txtnopen.Text = ge.ListDataset.Tables[0].Rows[0]["no_pensiun"].ToString();
                    txtjeniskelamin.Text = txtjenkel.Text;
                    txtstatunikah.Text = ge.ListDataset.Tables[0].Rows[0]["sts_nikah"].ToString();
                    ddlkodejiwa.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_jiwa"].ToString();
                    txttgllahirpensiun.Text = txttgllahirasuransi.Text;
                    txtpensiun.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_pensiun"].ToString();
                    txtmenikah.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_nikah"].ToString();
                    txtanak1.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_anak1"].ToString();
                    txtanak2.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_anak2"].ToString();
                    txtanak3.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_anak3"].ToString();
 
                    txtnosk.Text = ge.ListDataset.Tables[0].Rows[0]["no_sk"].ToString();
                    txtnamasesuaisk.Text = txtnamanasabah.Text;
                    txttglsk.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_sk"].ToString();
                    txtnodapem.Text = ge.ListDataset.Tables[0].Rows[0]["kd_dapem"].ToString();

                    //panel 3
                    
                    txtgapok.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["GajiPokok"].ToString()).Replace("Rp", "")));
                    txttistri.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjIstri"].ToString()).Replace("Rp", "")));
                    txttkd.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TKD"].ToString()).Replace("Rp", "")));
                    txttpp.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TPP"].ToString()).Replace("Rp", "")));
                    txttberas.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjBeras"].ToString()).Replace("Rp", "")));
                    txttcacat.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjCacat"].ToString()).Replace("Rp", "")));
                    txtpembulatan.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["Pembulatan"].ToString()).Replace("Rp", "")));
                    txttpm.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TPM"].ToString()).Replace("Rp", "")));
                    txttkhusus.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjKhusus"].ToString()).Replace("Rp", "")));
                    txttpajak.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjPajak"].ToString()).Replace("Rp", "")));
                    txttkematian.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjKematian"].ToString()).Replace("Rp", "")));
                    txtgajikotor.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["GajiKotor"].ToString()).Replace("Rp", "")));
                    txtpotaskes.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotAskes"].ToString()).Replace("Rp", "")));
                    txtpotassos.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotAssos"].ToString()).Replace("Rp", "")));
                    txtpotpajak.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotPajak"].ToString()).Replace("Rp", "")));
                    txtpotlain.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotLain"].ToString()).Replace("Rp", "")));
                    txtpotkasda.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotKasda"].ToString()).Replace("Rp", "")));
                    txttotal.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TotPotongan"].ToString()).Replace("Rp", "")));
                    txtgajibersih.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["GajiBersih"].ToString()).Replace("Rp", "")));
                    txtpotsewarmh.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["SewaRumah"].ToString()).Replace("Rp", "")));
                    
                    //Panel 4

                    txtkdproduk.Text = txtkodeproduk.Text;
                    txtprogpinj.Text = txtprogrampinjaman.Text;
                    string sumber = ge.ListDataset.Tables[0].Rows[0]["kdpersh2"].ToString().Replace("                 ", "");
                    ddlsumberdana.SelectedValue = sumber;
                    txtplafon.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()).Replace("Rp", "")));
                    txtbendahara.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()).Replace("Rp", "")));
                    txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                    txtpersengaji.Text = ge.ListDataset.Tables[0].Rows[0]["persen_gaji"].ToString();
                    txtusia.Text = ge.ListDataset.Tables[0].Rows[0]["usia"].ToString();
                    if(ge.ListDataset.Tables[0].Rows[0]["status_take_over"].ToString().Equals(1))
                    {
                        txtTO.Visible = false;
                    }else
                    {
                        txtTO.Visible = true;
                        txtTO.Text = ge.ListDataset.Tables[0].Rows[0]["status_to"].ToString();

                    }
                    ddlPenggunaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_pengguna"].ToString();
                    txttglrealisasi.Text = txtrealisasi.Text;
                    txttgljt.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_jatuhtempo"].ToString();
                    txtProvisi.Text = ge.ListDataset.Tables[0].Rows[0]["persenprv"].ToString();
                    txtktrbyr.Text = ge.ListDataset.Tables[0].Rows[0]["ktrbyr"].ToString();
                    ddlBankNasabah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["BankRekening"].ToString();
                    txtrekeningnamadeb.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekening"].ToString();
                    txtjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                    txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_dimuka"].ToString()));
                    txtbyangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_angsurandimuka"].ToString()).Replace("Rp", "")));
                    txtbyto.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_takeover"].ToString()).Replace("Rp", "")));
                    txtangsuranperbulan.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_angsuranperbulan"].ToString()).Replace("Rp", "")));
                    txtbaserate.Text = ge.ListDataset.Tables[0].Rows[0]["baserate"].ToString();
                    txtbgc.Text = ge.ListDataset.Tables[0].Rows[0]["rateasu"].ToString();
                    txtspread.Text = ge.ListDataset.Tables[0].Rows[0]["spread"].ToString();
                    txtbungas.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    txtbypanjar.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_panjar"].ToString()).Replace("Rp", "")));
                    txtadmiasuransi.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_admasuransi"].ToString()).Replace("Rp", "")));
                    txtadmprovisi.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_admprovisi"].ToString()).Replace("Rp", "")));
                    txtadmpencairan.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_admpencairan"].ToString()).Replace("Rp", "")));
                    txtadmangsuran.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_adminangsuran"].ToString()).Replace("Rp", "")));
                    txttaksasi.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()).Replace("Rp", "")));
                    txtangsblokir.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["blokir_angsuran"].ToString()).Replace("Rp", ""))); 
                    TxtAo1.Text = txtAO.Text;
                    txtnonao1.Text = txtnonAO.Text;
                    txtalasanpeminjaman.Text = ge.ListDataset.Tables[0].Rows[0]["input_pembiayaan"].ToString();
                    txtrekeningbankdeb.Text = ge.ListDataset.Tables[0].Rows[0]["Norekening"].ToString();
                    txtstatussk.Text = ge.ListDataset.Tables[0].Rows[0]["sk"].ToString();
                    txtpathsk.Text = ge.ListDataset.Tables[0].Rows[0]["path_sk"].ToString();
                    TextBox43.Text = ge.ListDataset.Tables[0].Rows[0]["jenis_usaha"].ToString();
                    TextBox44.Text = ge.ListDataset.Tables[0].Rows[0]["lokasi_usaha"].ToString();
                    TextBox45.Text = ge.ListDataset.Tables[0].Rows[0]["jumlah_tk"].ToString();
                    TextBox46.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["pendapatan_usaha"].ToString()).Replace("Rp", "")));
                    TextBox47.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_tk"].ToString()).Replace("Rp","")));
                    TextBox48.Text = string.Format("{0:N0}", decimal.Parse(gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_produksi"].ToString()).Replace("Rp", "")));

                    //panel 5 
                    txtpenerimapensiun.Text =  ge.ListDataset.Tables[0].Rows[0]["penerimapens"].ToString();
                    txtPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penerima_pensiun"].ToString();
                    TextBox1.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir_penerima"].ToString();
                    txtstatuskwn.Text = ge.ListDataset.Tables[0].Rows[0]["sts_penerima"].ToString();
                    TextBox2.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_penerima_pensiun"].ToString();
                    TextBox3.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_penerima_pensiun"].ToString();
                    TextBox4.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_penerima_pensiun"].ToString();
                    txtkotapekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kota_penerima_pensiun"].ToString();
                    txtkodepospekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_penerima_pensiun"].ToString();
                    txttlp.Text = ge.ListDataset.Tables[0].Rows[0]["telepon_penerima_pensiun"].ToString();
                    


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