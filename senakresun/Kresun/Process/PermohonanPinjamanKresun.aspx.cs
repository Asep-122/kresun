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

public partial class AR_Process_PermohonanPinjamanKreditMultiguna : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    Global gb2 = new DefaultClass.Global();
    GlobalEntities ge2 = new GlobalEntities();

    DateTime tgl_libur, tgl_realisasi, tgldrop;
    double angsuranperbulan = 0;
    string nonasabah = "", agama = "", usia = "", pokok = "", bunga = "", namafileJaminan = "", fotopathJaminan = "", fotopathKTP1 = "",
           fotopathKTP2 = "", fotopathKTP3 = "", fotopathKTP4 = "", fotopathKTP5 = "";

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermohonanPinjamanKresun.aspx");
    }
  

    
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (ddlstatusnikah.SelectedValue == "1" && txttempatlahir.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Menikah belum di pilih');", true);

        }
        else
        {

            pnladd.Visible = false;
            pnlSK.Visible = false;
            pnlPekerjaan.Visible = true;
        }
    }

    protected void btnNextPekerjaan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlSK.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlPendapatan.Visible = true;

    }
    protected void btnNextPendapatan_Click(object sender, EventArgs e)
    {

        decimal gajibersih = 0;
        gajibersih = decimal.Parse(txtgajibersih.Text);
        if(gajibersih <0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Gaji Bersih debitur minus proses tidak dapat di lanjutkan!');", true);

        }
        else
        {
            pnladd.Visible = false;
            pnlSK.Visible = false;
            pnlPekerjaan.Visible = false;

            pnlPendapatan.Visible = false;
            pnlPendapatan.Visible = false;

            pnlPengajuan.Visible = true;
        }

    }
    protected void btnBackPendapatan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlSK.Visible = false;
        pnlPekerjaan.Visible = true;
        pnlPendapatan.Visible = false;
        pnlPengajuan.Visible = false;
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[4];
        x[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        x[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string)); 
        x[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWSTATUSTRX", typeof(string));
        x[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "idpermohonan desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //BindGridEntity();
            BindGridEntity();
            txtkodecabang.Text = SessBranchID;

            ////ddljenispermohonan.Items.Add("Pilih Data");
            ////ddljenispermohonan.Items.Add("0","Permohonan Normal");
            //ddljenispermohonan.Items.Add(new { Text = "Permohonan Normal", Value = "0" });
            //ddljenispermohonan.Items.Add(new { Text = "Text", Value = "Value" });
            //ddljenispermohonan.Items.Add("Permohonan Deviasi");
            //ddljenispermohonan.Items.Add("Permohonan Take Over/Dantal");
            //ddljenispermohonan.Items.Add("Permohonan Deviasi & Dantal");


            ////ddlstatusSK.Items.Add("Pilih Data");
            //ddlstatusSK.Items.Add("SK Sudah Di Terima");
            //ddlstatusSK.Items.Add("SK Belum Di Terima");



            //ddlpenerimapensiun.Items.Add("Pilih Data");
            //ddlpenerimapensiun.Items.Add("Pensiunan");
            //ddlpenerimapensiun.Items.Add("Bukan Pensiunan");


            //ddlstatusperkawinan4.Items.Add("Pilih Data");
            //ddlstatusperkawinan4.Items.Add("Belum Menikah");
            //ddlstatusperkawinan4.Items.Add("Menikah");
            //ddlstatusperkawinan4.Items.Add("Janda");
            //ddlstatusperkawinan4.Items.Add("Duda");
             
            //ddlstatusnikah.Items.Add("Tidak Kawin");
            //ddlstatusnikah.Items.Add("Kawin");


            //ddljeniskelamin.Items.Add("Pilih Data");
            //ddljeniskelamin.Items.Add("Pria");
            //ddljeniskelamin.Items.Add("Wanita");


            //ddlstatupensiun.Items.Insert(0, new ListItem("Pilih Data", "-"));
            //ddlstatupensiun.Items.Add("Sendiri");
            //ddlstatupensiun.Items.Add("Janda");
            //ddlstatupensiun.Items.Add("Duda");


        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");


        CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
        CultureName = "id-ID";

        txtrealisasi.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd"); 
        txtjangka.Attributes["onchange"] = "if (IsValid(this) == false ) return;";


        lblGajiKotor.Font.Bold = true;
        lblsisapenghasilan.Font.Bold = true;
        lbltotalPengeluaran.Font.Bold = true;

    }


    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnbackawal_Click(object sender, EventArgs e)
    {
        pnlFilter.Visible = true;
        //PnlFront.Visible = true;
        pnladd.Visible = false;
        pnlSK.Visible = false;
    }
    protected void btnBackPekerjaan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        pnlSK.Visible = true;
        pnlPekerjaan.Visible = false;
    }

    protected void btnBackTambahan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlSK.Visible = false;
        pnlPekerjaan.Visible = true;

    }
    protected void ddlstatusSK_SelectedIndexChanged(object sender, EventArgs e)
    {
       if(ddlstatusSK.SelectedValue == "0")
        {
            btnUploadktp.Enabled = false;
        }
        else
        {

            btnUploadktp.Enabled = true;
        }
    }

    protected void ddlkali_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal angsurandimuka = 0;

        if(ddlkali.SelectedValue == "-" && txtangsuranperbulan.Text == "0")
        {

        }
        else
        {
            angsurandimuka = decimal.Parse(ddlkali.SelectedValue) * decimal.Parse(txtangsuranperbulan.Text);
            txtangsurandimuka.Text = string.Format("{0:N2}", angsurandimuka);

        }

    }

    protected void ddlblokir_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal blokir = 0;

        blokir = decimal.Parse(ddlblokir.SelectedValue == "-" ? "0" : ddlblokir.SelectedValue) * decimal.Parse(txtangsuranperbulan.Text == "0" ? "0" : txtangsuranperbulan.Text);
        txtangsblokir.Text = string.Format("{0:N2}", blokir);

    }
    

    protected void ddlMemo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try

        {
            if(ddlMemo.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Memo Internal belum di pilih!');", true);

            }
            else
            {
                string[] splitdata1 = ddlMemo.Text.Split('|');
                string memointernal = splitdata1[0];
                string simulasi = splitdata1[1];
                HDMemo.Value = memointernal;

                decimal totaltunjangan, totalpotongan = 0, totalbersih = 0;
                InsertCombo();

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                var[0] = gb.CreateSPParameter("@nosimulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, simulasi, typeof(string));
                var[1] = gb.CreateSPParameter("@nomemo", ParameterDirection.Input, SqlDbType.VarChar, 50, memointernal, typeof(string));
                var[2] = gb.CreateSPParameter("@kode_Cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETDEVIASI", typeof(string));
                var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var, "constrmf");

                //string[] splitdataprogram = ge.ListDataset.Tables[0].Rows[0]["Product"].ToString().Split('-');
                string program = ge.ListDataset.Tables[0].Rows[0]["kodeProduct"].ToString();

                ddljenispensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["instansi"].ToString(); ddljenispensiun.Enabled = false;
                //ddljenispensiunsk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["instansi"].ToString(); ddljenispensiunsk.Enabled = false;
                HDJenisPensiun.Value = ge.ListDataset.Tables[0].Rows[0]["jenispensiun"].ToString();
                txtnip.Text = ge.ListDataset.Tables[0].Rows[0]["nip"].ToString(); txtnip.Enabled = false;
                ddlkodejiwa.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeJiwa"].ToString(); ddlkodejiwa.Enabled = false;
                HDKodeJiwa.Value = ge.ListDataset.Tables[0].Rows[0]["kodeJiwa"].ToString();
                txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["TglAnak1"].ToString();
                ddlstatusnikah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusNikah"].ToString(); ddlstatusnikah.Enabled = false;
                if(ddlstatusnikah.SelectedValue !="0")
                {
                    txttempatlahir.Text = "1900-01-01 00:00:00";
                    txttempatlahir.Enabled = false;
                }
                ddlstatupensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusPensiun"].ToString(); ddlstatupensiun.Enabled = false;
                ddlstatusSK.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Status_Sk_diterima"].ToString(); ddlstatusSK.Enabled = false;
                ddlprogrampinjaman.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodeproduct"].ToString(); ddlprogrampinjaman.Enabled = false;
                ddlsumberdana.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeSumberDana"].ToString();
                txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafond"].ToString()));

                txtbyto.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PelunasanTO"].ToString())); txtbyto.Enabled = false;
                txtbaserate.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungadeb"].ToString()));
                txtbungas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungadeb"].ToString()));
                ddlblokir.SelectedValue =  ge.ListDataset.Tables[0].Rows[0]["jumlahblokir"].ToString() ;
                ddlblokir.Enabled = false;
                txtangsblokir.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["AngsBlokir"].ToString()));
               
                txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString();
                txtProvisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["abc"].ToString()));
                txtProvisi.Enabled = false;
                txtbgc.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["RateAsu"].ToString()));
                txtbgc.Enabled = false;

                DateTime dtgllahir = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahir"].ToString());
                txttgllahirpensiun.Text = dtgllahir.ToString("yyyy-MM-dd"); txttgllahirpensiun.Enabled = false;

                DateTime dtanak2 = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglAnak2"].ToString());
                txtanak2.Text = dtanak2.ToString("dd-MM-yyyy"); txtanak2.Enabled = false;

                DateTime dtanak1 = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglAnak1"].ToString());
                txttgllahir.Text = dtanak1.ToString("dd-MM-yyyy"); txttgllahir.Enabled = false;

                DateTime dtanak3 = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglAnak3"].ToString());
                txtanak3.Text = dtanak3.ToString("dd-MM-yyyy"); txtanak3.Enabled = false;

                //txtgapok.Text = ge.ListDataset.Tables[0].Rows[0]["GajiPokok"].ToString(); txtgapok.Enabled = false;
                //txttistri.Text = ge.ListDataset.Tables[0].Rows[0]["TunjIstri"].ToString(); txttistri.Enabled = false;
                //txttberas.Text = ge.ListDataset.Tables[0].Rows[0]["TunjBeras"].ToString(); txttberas.Enabled = false;
                //txtpotlain.Text = ge.ListDataset.Tables[0].Rows[0]["Potongan"].ToString(); txtpotlain.Enabled = false; 
                HDKodeProduk.Value = ge.ListDataset.Tables[0].Rows[0]["kodeProduct"].ToString();
                ddlsumberdana.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeSumberDana"].ToString(); ddlsumberdana.Enabled = false;
                HDSumberDana.Value = ge.ListDataset.Tables[0].Rows[0]["KodeSumberDana"].ToString();

                txtplafon.Enabled = false;
                txtbendahara.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafond"].ToString()));
                txtbendahara.Enabled = false;
                txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString(); txtjangka.Enabled = false;
                txtangsurandimuka.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["AngsDimuka"].ToString()));
                txtangsurandimuka.Enabled = false;
                //ddlkali.SelectedIndex = int.Parse(ge.ListDataset.Tables[0].Rows[0]["devangs"].ToString()); ddlkali.Enabled = false;
                //txtbyto.Text = ge.ListDataset.Tables[0].Rows[0]["PelunasanTO"].ToString(); txtbyto.Enabled = false;
                txtangsuranperbulan.Enabled = false;
                txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()));
                decimal angsdmk = decimal.Parse(txtangsurandimuka.Text) / decimal.Parse(txtangsuranperbulan.Text);
                ddlkali.SelectedValue = angsdmk.ToString(); ddlkali.Enabled = false;

                decimal bungasrn = decimal.Parse(txtbgc.Text) / 100;
                decimal admasuransi = decimal.Parse(txtplafon.Text) * bungasrn;
                admasuransi = Math.Round(admasuransi);

                txtadmiasuransi.Text = string.Format("{0:N2}", admasuransi);



                decimal persenprovisi = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["abc"].ToString()) / 100;

                 
                decimal provisi = decimal.Parse(txtplafon.Text) * (persenprovisi);
               
                txtadmprovisi.Text = string.Format("{0:N2}", provisi);

                totaltunjangan = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttberas.Text);
                txtgajikotor.Text = string.Format("{0:N2}", totaltunjangan); //totaltunjangan.ToString();

                totalpotongan = decimal.Parse(txtpotlain.Text);
                txttotal.Text = string.Format("{0:N2}", totalpotongan);
               
                totalbersih = totaltunjangan - totalpotongan;
                txtgajibersih.Text = string.Format("{0:N2}", totalbersih);



                int jangkabulat = int.Parse(txtjangka.Text) / 12;




                //string[] splitdata = ddlprogrampinjaman.Text.Split('-');
                string programpinjaman = ddlprogrampinjaman.SelectedValue;


              //  string[] splitdata2 = ddlsumberdana.Text.Split('-');
                string kodebank = ddlsumberdana.SelectedValue;



                DefaultClass.Global.SqlHelper[] varxx = new DefaultClass.Global.SqlHelper[7];
                varxx[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangkabulat, typeof(int));
                varxx[1] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, kodebank, typeof(string));
                varxx[2] = gb.CreateSPParameter("@programpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, programpinjaman, typeof(string));
                varxx[3] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                varxx[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGA", typeof(string));
                varxx[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varxx[6] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", varxx, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                { 
                    txtpersengaji.Text = string.Format("{0:N2}",decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persen_gaji"].ToString()));
                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3]; 
                    var2[0] = gb.CreateSPParameter("@kodeprogram", ParameterDirection.Input, SqlDbType.VarChar, 50, programpinjaman, typeof(string));
                    var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETSTATUSTO", typeof(string));
                    var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var2, "constrmf");

                    txtTO.Text = ge.ListDataset.Tables[0].Rows[0]["STATUS_TO"].ToString();


                }

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
    protected void UploadFileSK(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/Files/Kresun/SK/");
        DateTime now = DateTime.Now;

        namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "SK";

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        fileuploadSK.SaveAs(folderPath + namafileJaminan + ".jpg");

        fotopathJaminan = folderPath + namafileJaminan;

        txtpathsk.Text = "~/Files/Kresun/SK/" + namafileJaminan + ".jpg";
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string jenispensiun, programpinjaman, sumberdana = "";

        try
        {
            if (ddljenispermohonan.SelectedValue =="0")
            {
                jenispensiun = ddljenispensiun.SelectedValue;
                programpinjaman = ddlprogrampinjaman.SelectedValue;
                sumberdana = ddlsumberdana.SelectedValue;
            }
            else
            {
                jenispensiun = HDJenisPensiun.Value;
                programpinjaman = HDKodeProduk.Value;
                sumberdana = HDSumberDana.Value;
            }
            DateTime start = DateTime.Parse(txttgljt.Text, System.Globalization.CultureInfo.InvariantCulture);
            txttgljt.Text = start.ToString("yyyy-MM-dd");

            DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
            var4[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnonasabah.Text, typeof(string));
            var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("GetKPMasterKredit", var4, "constrmf");


            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                HDStatusDebitur.Value = "B";
            }
            else
            {
                HDStatusDebitur.Value = "L";
            }


            //btnSimpan.Enabled = false; 

            usia = gb.HitungTahunUsia(txttgllahirpensiun.Text);

            string nobuktipelunasan = "";
            decimal nominalpelunasan = 0;

            if (HDnobukti.Value == "1")
            {
                string[] splitdata = ddlnobukti.Text.Split('-');
                nobuktipelunasan = (splitdata[0]);
                nominalpelunasan = decimal.Parse(splitdata[1]);
            }else
            {
                nobuktipelunasan = "0";
                nominalpelunasan = 0;
            }
           

            //btnSimpan.Enabled = false;

            //INSERT INTO KPPermohonanKredit
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[170];
            var1[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
            var1[1] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlkdproduk.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnip.Text, typeof(string));
            var1[3] = gb.CreateSPParameter("@nama", ParameterDirection.Input, SqlDbType.VarChar, 200, txtnamanasabah.Text, typeof(string));
            var1[4] = gb.CreateSPParameter("@kd_dapem", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnodapem.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@jenis_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 20, jenispensiun, typeof(string));
            var1[6] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));
            var1[7] = gb.CreateSPParameter("@kd_golongan", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[8] = gb.CreateSPParameter("@masa_kerja ", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[9] = gb.CreateSPParameter("@sisa_masa_dinas ", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[10] = gb.CreateSPParameter("@tipe_jabatan ", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[11] = gb.CreateSPParameter("@kd_instansi", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlkodeinstansi.SelectedValue, typeof(string));
            var1[12] = gb.CreateSPParameter("@pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[13] = gb.CreateSPParameter("@tipe_instansi", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[14] = gb.CreateSPParameter("@status_debitur", ParameterDirection.Input, SqlDbType.VarChar, 20, HDStatusDebitur.Value, typeof(string));
            var1[15] = gb.CreateSPParameter("@gaji_bersih", ParameterDirection.Input, SqlDbType.Money, 0, txtgajibersih.Text, typeof(decimal));
            var1[16] = gb.CreateSPParameter("@permohonan_plafon", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(decimal));
            var1[17] = gb.CreateSPParameter("@permohonan_jangka", ParameterDirection.Input, SqlDbType.Int, 0,int.Parse(txtjangka.Text), typeof(int));
            var1[18] = gb.CreateSPParameter("@permohonan_bendahara", ParameterDirection.Input, SqlDbType.Money, 0, txtbendahara.Text, typeof(decimal));
            var1[19] = gb.CreateSPParameter("@angsuran_bulanan", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranperbulan.Text, typeof(decimal));
            var1[20] = gb.CreateSPParameter("@tplafon ", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(decimal));
            var1[21] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(decimal));
            var1[22] = gb.CreateSPParameter("@persen_gaji", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtpersengaji.Text), typeof(float));
            var1[23] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int,0,int.Parse(txtjangka.Text), typeof(int));
            var1[24] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtbungas.Text), typeof(float));
            var1[25] = gb.CreateSPParameter("@jns_pgn ", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[26] = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[27] = gb.CreateSPParameter("@kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[28] = gb.CreateSPParameter("@kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[29] = gb.CreateSPParameter("@kota", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[30] = gb.CreateSPParameter("@kodepos ", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[31] = gb.CreateSPParameter("@telepon ", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[32] = gb.CreateSPParameter("@kd_area ", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[33] = gb.CreateSPParameter("@tempat_lahir ", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[34] = gb.CreateSPParameter("@tgl_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 20, txttgllahirpensiun.Text, typeof(string));
            var1[35] = gb.CreateSPParameter("@usia", ParameterDirection.Input, SqlDbType.Int, 0, usia, typeof(int));
            var1[36] = gb.CreateSPParameter("@gender", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljeniskelamin.SelectedValue, typeof(string));
            var1[37] = gb.CreateSPParameter("@kdpersh1", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
            var1[38] = gb.CreateSPParameter("@kdpersh2", ParameterDirection.Input, SqlDbType.VarChar, 30, sumberdana, typeof(string));
            var1[39] = gb.CreateSPParameter("@no_spk1 ", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnosk.Text, typeof(string));
            var1[40] = gb.CreateSPParameter("@no_spk2 ", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[41] = gb.CreateSPParameter("@tgl_pk", ParameterDirection.Input, SqlDbType.SmallDateTime, 20, "1900-01-01", typeof(string));
            var1[42] = gb.CreateSPParameter("@no_registrasi", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[43] = gb.CreateSPParameter("@no_rekening1 ", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[44] = gb.CreateSPParameter("@no_rekening2 ", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[45] = gb.CreateSPParameter("@no_rek_tabungan ", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[46] = gb.CreateSPParameter("@biaya_provisi", ParameterDirection.Input, SqlDbType.Money, 0, txtadmprovisi.Text, typeof(decimal));
            var1[47] = gb.CreateSPParameter("@persen_prov", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtProvisi.Text), typeof(float));
            var1[48] = gb.CreateSPParameter("@biaya_administrasi ", ParameterDirection.Input, SqlDbType.Money, 0, txtadmpencairan.Text, typeof(decimal));
            var1[49] = gb.CreateSPParameter("@meterai ", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(decimal));
            var1[50] = gb.CreateSPParameter("@meterai2", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(decimal));
            var1[51] = gb.CreateSPParameter("@notaris ", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(decimal));
            var1[52] = gb.CreateSPParameter("@asuransi", ParameterDirection.Input, SqlDbType.Money, 0, txtadmiasuransi.Text, typeof(decimal));
            var1[53] = gb.CreateSPParameter("@tab_drop", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(decimal));
            var1[54] = gb.CreateSPParameter("@blokir_tabungan ", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(decimal));
            var1[55] = gb.CreateSPParameter("@biaya_pelunasan ", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(decimal));
            var1[56] = gb.CreateSPParameter("@biaya_angsuran1 ", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranperbulan.Text, typeof(decimal));
            var1[57] = gb.CreateSPParameter("@no_bukti", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[58] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 20, txtktp.Text, typeof(string));
            var1[59] = gb.CreateSPParameter("@agama", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[60] = gb.CreateSPParameter("@bgc ", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtbgc.Text), typeof(float));
            var1[61] = gb.CreateSPParameter("@spv ", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            var1[62] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));
            var1[63] = gb.CreateSPParameter("@tglupd", ParameterDirection.Input, SqlDbType.SmallDateTime, 20, txtrealisasi.Text, typeof(string));
            var1[64] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            var1[65] = gb.CreateSPParameter("@jaminan ", ParameterDirection.Input, SqlDbType.VarChar, 200, txtjaminan.Text, typeof(string));
            var1[66] = gb.CreateSPParameter("@tanggal_pengajuan", ParameterDirection.Input, SqlDbType.SmallDateTime, 20, txtrealisasi.Text, typeof(string));
            var1[67] = gb.CreateSPParameter("@base_rate", ParameterDirection.Input, SqlDbType.Float, 20,float.Parse(txtbaserate.Text), typeof(float));
            var1[68] = gb.CreateSPParameter("@spread", ParameterDirection.Input, SqlDbType.Float, 20,float.Parse(txtspread.Text), typeof(float));
            var1[69] = gb.CreateSPParameter("@status_pelunasan", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[70] = gb.CreateSPParameter("@status_cetak_persetujuan ", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[71] = gb.CreateSPParameter("@status_cetak_analisa", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[72] = gb.CreateSPParameter("@plafon1 ", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(decimal));
            var1[73] = gb.CreateSPParameter("@plafon2 ", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(decimal));
            var1[74] = gb.CreateSPParameter("@status_pencairan", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[75] = gb.CreateSPParameter("@jns_pinjaman ", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[76] = gb.CreateSPParameter("@no_rek_giro", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[77] = gb.CreateSPParameter("@status_sk", ParameterDirection.Input, SqlDbType.VarChar, 20, programpinjaman, typeof(string));
            var1[78] = gb.CreateSPParameter("@user_gol", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
            var1[79] = gb.CreateSPParameter("@tgl_transfer ", ParameterDirection.Input, SqlDbType.SmallDateTime, 20, "1900-01-01", typeof(string));
            var1[80] = gb.CreateSPParameter("@status_transfer ", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
            var1[81] = gb.CreateSPParameter("@no_sk", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnosk.Text, typeof(string));
            var1[82] = gb.CreateSPParameter("@status_take_over", ParameterDirection.Input, SqlDbType.VarChar, 20, HDStatusTO.Value, typeof(string));
            var1[83] = gb.CreateSPParameter("@input_pembiayaan", ParameterDirection.Input, SqlDbType.VarChar, 20, txtalasanpeminjaman.Text, typeof(string));
            var1[84] = gb.CreateSPParameter("@blokir_angsuran ", ParameterDirection.Input, SqlDbType.Money, 0, txtangsblokir.Text, typeof(decimal));
            var1[85] = gb.CreateSPParameter("@kd_reff2", ParameterDirection.Input, SqlDbType.VarChar, 20, HDAO.Value, typeof(string));
            var1[86] = gb.CreateSPParameter("@kode_siskohat", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var1[87] = gb.CreateSPParameter("@jenis_permohonan", ParameterDirection.Input, SqlDbType.VarChar, 20, ddljenispermohonan.SelectedValue, typeof(string));
            var1[88] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            var1[89] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[90] = gb.CreateSPParameter("@app_date", ParameterDirection.Input, SqlDbType.SmallDateTime, 20, txtrealisasi.Text, typeof(string));
            var1[91] = gb.CreateSPParameter("@siklus_transaksi", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));
            var1[92] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            var1[93] = gb.CreateSPParameter("@angsuran_dimuka ", ParameterDirection.Input, SqlDbType.Money, 0, txtangsurandimuka.Text, typeof(decimal));
            var1[94] = gb.CreateSPParameter("@by_angsurandimuka", ParameterDirection.Input, SqlDbType.Money, 0, txtbyangsurandimuka.Text, typeof(decimal));
            var1[95] = gb.CreateSPParameter("@by_takeover", ParameterDirection.Input, SqlDbType.Money, 0, txtbyto.Text , typeof(decimal));
            var1[96] = gb.CreateSPParameter("@by_angsuranperbulan", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranperbulan.Text , typeof(decimal));
            var1[97] = gb.CreateSPParameter("@baserate", ParameterDirection.Input, SqlDbType.Float, 20,float.Parse(txtbaserate.Text), typeof(float));
            var1[98] = gb.CreateSPParameter("@by_panjar ", ParameterDirection.Input, SqlDbType.Money, 0, txtbypanjar.Text, typeof(decimal));
            var1[99] = gb.CreateSPParameter("@by_admasuransi ", ParameterDirection.Input, SqlDbType.Money, 0, txtadmiasuransi.Text, typeof(decimal));
            var1[100] = gb.CreateSPParameter("@by_admprovisi", ParameterDirection.Input, SqlDbType.Money, 0, txtadmprovisi.Text, typeof(decimal));
            var1[101] = gb.CreateSPParameter("@by_admpencairan", ParameterDirection.Input, SqlDbType.Money, 0, txtadmpencairan.Text, typeof(decimal));
            var1[102] = gb.CreateSPParameter("@by_adminangsuran", ParameterDirection.Input, SqlDbType.Money, 0, txtadmangsuran.Text, typeof(decimal));
            var1[103] = gb.CreateSPParameter("@taksasi", ParameterDirection.Input, SqlDbType.Money, 0, txttaksasi.Text, typeof(decimal));
            var1[104] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
            var1[105] = gb.CreateSPParameter("@alasan_pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalasanpeminjaman.Text, typeof(string));
            var1[106] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttgljt.Text, typeof(string));
            var1[107] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnonasabah.Text, typeof(string));
            var1[108] = gb.CreateSPParameter("@KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlKantorBayar.SelectedValue, typeof(string));
            var1[109] = gb.CreateSPParameter("@BankRekening", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlBankNasabah.SelectedValue, typeof(string));
            var1[110] = gb.CreateSPParameter("@Norekening", ParameterDirection.Input, SqlDbType.VarChar, 250, txtrekeningbankdeb.Text, typeof(string));
            var1[111] = gb.CreateSPParameter("@Namarekening", ParameterDirection.Input, SqlDbType.VarChar, 250, txtrekeningnamadeb.Text, typeof(string));
            var1[112] = gb.CreateSPParameter("@statusSK", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlstatusSK.SelectedValue, typeof(string));
            var1[113] = gb.CreateSPParameter("@PathSK", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathsk.Text, typeof(string));
            var1[114] = gb.CreateSPParameter("@tglSk", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, TextBox32.Text, typeof(string));
            var1[115] = gb.CreateSPParameter("@memo", ParameterDirection.Input, SqlDbType.VarChar, 250, HDMemo.Value, typeof(string));
            var1[116] = gb.CreateSPParameter("@statusnikah", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlstatusnikah.SelectedValue, typeof(string));
            var1[117] = gb.CreateSPParameter("@kodejiwa", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlkodejiwa.SelectedValue, typeof(string));
            var1[118] = gb.CreateSPParameter("@pathfoto1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto1.Text, typeof(string));
            var1[119] = gb.CreateSPParameter("@pathfoto2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto2.Text, typeof(string));
            var1[120] = gb.CreateSPParameter("@pathfoto3", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto3.Text, typeof(string));
            var1[121] = gb.CreateSPParameter("@pathfoto4", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto4.Text, typeof(string));
            var1[122] = gb.CreateSPParameter("@pathfoto5", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto5.Text, typeof(string));
            var1[123] = gb.CreateSPParameter("@GajiPokok", ParameterDirection.Input, SqlDbType.Money, 0,  txtgapok.Text, typeof(decimal));
            var1[124] = gb.CreateSPParameter("@TunjIstri", ParameterDirection.Input, SqlDbType.Money, 0,  txttistri.Text, typeof(decimal));
            var1[125] = gb.CreateSPParameter("@TKD", ParameterDirection.Input, SqlDbType.Money, 0,  txttkd.Text, typeof(decimal));
            var1[126] = gb.CreateSPParameter("@TPP", ParameterDirection.Input, SqlDbType.Money, 0,  txttpp.Text, typeof(decimal));
            var1[127] = gb.CreateSPParameter("@TunjBeras", ParameterDirection.Input, SqlDbType.Money, 0,  txttberas.Text, typeof(decimal));
            var1[128] = gb.CreateSPParameter("@TunjCacat", ParameterDirection.Input, SqlDbType.Money, 0,  txttcacat.Text, typeof(decimal));
            var1[129] = gb.CreateSPParameter("@Pembulatan ", ParameterDirection.Input, SqlDbType.Money, 0,  txtpembulatan.Text, typeof(decimal));
            var1[130] = gb.CreateSPParameter("@TPM", ParameterDirection.Input, SqlDbType.Money, 0,  txttpm.Text, typeof(decimal));
            var1[131] = gb.CreateSPParameter("@TunjKhusus ", ParameterDirection.Input, SqlDbType.Money, 0,  txttkhusus.Text, typeof(decimal));
            var1[132] = gb.CreateSPParameter("@TunjKematian ", ParameterDirection.Input, SqlDbType.Money, 0,  txttkematian.Text, typeof(decimal));
            var1[133] = gb.CreateSPParameter("@TunjPajak", ParameterDirection.Input, SqlDbType.Money, 0,  txttpajak.Text, typeof(decimal));
            var1[134] = gb.CreateSPParameter("@GajiKotor", ParameterDirection.Input, SqlDbType.Money, 0,  txtgajikotor.Text, typeof(decimal));
            var1[135] = gb.CreateSPParameter("@PotAskes", ParameterDirection.Input, SqlDbType.Money, 0,  txtpotaskes.Text, typeof(decimal));
            var1[136] = gb.CreateSPParameter("@PotAssos", ParameterDirection.Input, SqlDbType.Money, 0,  txtpotassos.Text, typeof(decimal));
            var1[137] = gb.CreateSPParameter("@PotPajak", ParameterDirection.Input, SqlDbType.Money, 0,  txtpotpajak.Text, typeof(decimal));
            var1[138] = gb.CreateSPParameter("@PotLain", ParameterDirection.Input, SqlDbType.Money, 0,  txtpotlain.Text, typeof(decimal));
            var1[139] = gb.CreateSPParameter("@SewaRumah", ParameterDirection.Input, SqlDbType.Money, 0,  txtpotsewarmh.Text, typeof(decimal));
            var1[140] = gb.CreateSPParameter("@PotKasda", ParameterDirection.Input, SqlDbType.Money, 0,  txtpotkasda.Text, typeof(decimal));
            var1[141] = gb.CreateSPParameter("@TotPotongan", ParameterDirection.Input, SqlDbType.Money, 0,  txttotal.Text, typeof(decimal));
            var1[142] = gb.CreateSPParameter("@GajiBersih", ParameterDirection.Input, SqlDbType.Money, 0,  txtgajibersih.Text, typeof(decimal));
            var1[143] = gb.CreateSPParameter("@tglLahiranak1", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttgllahir.Text, typeof(string));
            var1[144] = gb.CreateSPParameter("@tglLahiranak2", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtanak2.Text, typeof(string));
            var1[145] = gb.CreateSPParameter("@tglLahiranak3", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtanak3.Text, typeof(string));
            var1[146] = gb.CreateSPParameter("@nonAo", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlnonao.SelectedValue, typeof(string));

            var1[147] = gb.CreateSPParameter("@sts_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlstatupensiun.SelectedValue, typeof(string));
            var1[148] = gb.CreateSPParameter("@tanggal_pensiun", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtkota.Text, typeof(string));
            var1[149] = gb.CreateSPParameter("@tanggal_nikah", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttempatlahir.Text, typeof(string));
            var1[150] = gb.CreateSPParameter("@jenis_pengguna", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlPenggunaan.SelectedValue, typeof(string));
            var1[151] = gb.CreateSPParameter("@jenis_usaha", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox43.Text, typeof(string));
            var1[152] = gb.CreateSPParameter("@lokasi_usaha", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox44.Text, typeof(string));

            var1[153] = gb.CreateSPParameter("@jumlah_tk", ParameterDirection.Input, SqlDbType.Int,0, int.Parse(TextBox45.Text), typeof(int));
            var1[154] = gb.CreateSPParameter("@pendapatan_usaha", ParameterDirection.Input, SqlDbType.Money, 0,  TextBox46.Text, typeof(decimal));
            var1[155] = gb.CreateSPParameter("@by_tk", ParameterDirection.Input, SqlDbType.Money, 0,  TextBox47.Text, typeof(decimal));
            var1[156] = gb.CreateSPParameter("@by_produksi", ParameterDirection.Input, SqlDbType.Money, 0,  TextBox48.Text, typeof(decimal));

            var1[157] = gb.CreateSPParameter("@penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlpenerimapensiun.SelectedValue, typeof(string));
            var1[158] = gb.CreateSPParameter("@nama_penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapenerima.Text, typeof(string));
            var1[159] = gb.CreateSPParameter("@tanggal_lahir_penerima_pensiun", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, TextBox1.Text, typeof(string));
            var1[160] = gb.CreateSPParameter("@status_kawin_penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlstatusperkawinan4.SelectedValue, typeof(string));

            var1[161] = gb.CreateSPParameter("@alamat_penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAlamat.Text, typeof(string));
            var1[162] = gb.CreateSPParameter("@kelurahan_penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkelurahan.Text, typeof(string));
            var1[163] = gb.CreateSPParameter("@kecamatan_penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkecamatan.Text, typeof(string));
            var1[164] = gb.CreateSPParameter("@kota_penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkotapekerjaan.Text, typeof(string));

            var1[165] = gb.CreateSPParameter("@kodepos_penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkodepospekerjaan.Text, typeof(string));
            var1[166] = gb.CreateSPParameter("@telepon_penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, txttlp.Text, typeof(string)); 
            var1[167] = gb.CreateSPParameter("@angsdimuka", ParameterDirection.Input, SqlDbType.Int, 0, ddlkali.SelectedValue, typeof(int));
            var1[168] = gb.CreateSPParameter("@nobuktiplns", ParameterDirection.Input, SqlDbType.VarChar, 250, nobuktipelunasan, typeof(string));
            var1[169] = gb.CreateSPParameter("@nominalpelunasan", ParameterDirection.Input, SqlDbType.Decimal, 0, nominalpelunasan, typeof(decimal));




            ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);


            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                btnSimpan.Enabled = true;
            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
   
    
    
    
    }

    protected void Uploadfoto1(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS1";


            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtfoto1.Text = "";


            fileupload1.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP1 = folderPath + namafileJaminan;

            txtfoto1.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            imgfoto1.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();



        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void Uploadfoto2(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS2";


            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }


            txtfoto2.Text = "";

            fileupload2.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP2 = folderPath + namafileJaminan;

            txtfoto2.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            imgfoto2.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void Uploadfoto3(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS3";


            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }


            txtfoto3.Text = "";

            fileupload3.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP3 = folderPath + namafileJaminan;

            txtfoto3.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            imgfoto3.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void Uploadfoto4(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS4";


            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtfoto4.Text = "";


            fileupload4.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP4 = folderPath + namafileJaminan;

            txtfoto4.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            imgfoto4.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void Uploadfoto5(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS5";


            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtfoto5.Text = "";


            fileupload5.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP5 = folderPath + namafileJaminan;

            txtfoto5.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            imgfoto5.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermohonanPinjamanKresun.aspx");
    }

    protected void txtnonasabah_TextChanged(object sender, EventArgs e)
    {
            string[] splitdata = txtnonasabah.Text.Split('-');
            string nonasabah = splitdata[0];
            HDNominal.Value = splitdata[1];
 

        DefaultClass.Global.SqlHelper[] varg = new DefaultClass.Global.SqlHelper[3];
            varg[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
            varg[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPELUNASANLANJUT", typeof(string));
            varg[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", varg, "constrmf");


        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
        {
            HDnobukti.Value = "0";
            ddlnobukti.Visible = false;
            lblnobukti.Visible = false;
        }
        else {

            HDnobukti.Value = "1";

            ddlnobukti.Visible = true;
            lblnobukti.Visible = true;
             
            ge.ListDataset.Tables[0].DefaultView.Sort = "Nobukti asc";

            ddlnobukti.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlnobukti.DataTextField = "pelunasan";
            ddlnobukti.DataValueField = "pelunasan";
            ddlnobukti.DataBind();


            string[] x = ddlnobukti.Text.Split('-'); 
            HDNominal.Value = x[1];
        }

    }

    
    protected void txtangsurandimuka_TextChanged(object sender, EventArgs e)
    {
        decimal byangsurandimuka = decimal.Parse(txtangsurandimuka.Text) * decimal.Parse(txtangsuranperbulan.Text);

        txtbyangsurandimuka.Text = string.Format("{0:N2}", byangsurandimuka); 
    }
    protected void txtjangka_TextChanged(object sender, EventArgs e)
    {
        if (txtjangka.Text == "0")
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Program Pinjaman belum di pilih atau Tanggal Realisasi waktu kosong');", true);

        }
        else if ( txtrealisasi.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Program Pinjaman belum di pilih atau Tanggal Realisasi waktu kosong');", true);

        }
        else
        {

            decimal nominal_provisi = 0;
            int i = 0;

            int jangka =  int.Parse(txtjangka.Text) / 12;


            //string[] splitdata = ddlprogrampinjaman.Text.Split('-');
            string programpinjaman = ddlprogrampinjaman.SelectedValue;


            //string[] splitdata1 = ddlsumberdana.Text.Split('-');
            string kodebank = ddlsumberdana.SelectedValue;

            string usia = gb.HitungTahunUsia(txttgllahirpensiun.Text);

        
            int jangkabulat = int.Parse(txtjangka.Text) / 12;

            DefaultClass.Global.SqlHelper[] varSD = new DefaultClass.Global.SqlHelper[7];
            varSD[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varSD[1] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangkabulat, typeof(int));
            varSD[2] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(usia), typeof(int));
            varSD[3] = gb.CreateSPParameter("@sd", ParameterDirection.Input, SqlDbType.VarChar, 50, kodebank, typeof(string));
            varSD[4] = gb.CreateSPParameter("@programpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, programpinjaman, typeof(string));
            varSD[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGASD", typeof(string));
            varSD[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", varSD, "constrmf");

            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan dengan kriteria umur = " + usia + ", jangka = " + txtjangka.Text + "dan sumberdana = " + kodebank + " dan program pinjaman = " + programpinjaman + "');", true);
                btnNextPengajuan.Enabled = false;
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[7];
                var1[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangkabulat, typeof(int));
                var1[1] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, kodebank, typeof(string));
                var1[2] = gb.CreateSPParameter("@programpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, programpinjaman, typeof(string));
                var1[3] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGA", typeof(string));
                var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[6] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var1, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                {
                    decimal persenperhitungan = 0;
                    
                    txtbaserate.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                    txtbungas.Text = txtbaserate.Text;
                    txtProvisi.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString());
                    txtpersengaji.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["persen_gaji"].ToString()); 
                    persenperhitungan =decimal.Parse(txtgajibersih.Text) * (decimal.Parse(txtpersengaji.Text) / 100);
                    txtpersenperhitungan.Text = string.Format("{0:N2}", persenperhitungan);
                    //txtpersenperhitungan.Text = txtpersenperhitungan.Text.Replace("Rp", "");
                    

                    DateTime date = Convert.ToDateTime(txttgllahirpensiun.Text.Trim());
                    string dateString = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

                    decimal usia1 = gb.HitungUmur(decimal.Parse(dateString).ToString());

                    txtusia.Text = string.Format("{0:N0}", usia1.ToString());


                    decimal persenprovisi = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString());


                    //decimal bunga = decimal.Parse(txtbaserate.Text) / 100;
                    //decimal admasuransi = decimal.Parse(txtplafon.Text.Replace(".", "")) * bunga;
                    //admasuransi = Math.Round(admasuransi);

                    //txtadmiasuransi.Text = gb.ToRupiah(admasuransi.ToString()).Replace("Rp", "");

                    decimal bunga = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()) / 100;

                    decimal provisi = decimal.Parse(txtplafon.Text) * (persenprovisi);

                    //txtadmprovisi.Text = gb.ToRupiah(provisi.ToString()).Replace("Rp", "");
                    txtadmprovisi.Text = string.Format("{0:N2}", provisi);
                    DateTime tgllahir = Convert.ToDateTime(txttgllahirpensiun.Text.Trim());
                    string tgl = tgllahir.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[6];
                    var2[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtjangka.Text), typeof(int));
                    var2[1] = gb.CreateSPParameter("@programpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, programpinjaman, typeof(string));
                    var2[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, kodebank, typeof(string));
                    var2[3] = gb.CreateSPParameter("@dob", ParameterDirection.Input, SqlDbType.VarChar, 50, tgl, typeof(string));
                    var2[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGASENA", typeof(string));
                    var2[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var2, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                    {

                        decimal z = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                        //txtrate.Text = z.ToString();
                        txtbgc.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                        double bungasena = double.Parse(z.ToString()) / 12;
                        bungasena = bungasena / 100;

                        double totalpokok = double.Parse(txtplafon.Text) / int.Parse(txtjangka.Text);
                        double totalbunga = (double.Parse(txtplafon.Text) * double.Parse(bunga.ToString()) * int.Parse(jangka.ToString())) / int.Parse(txtjangka.Text);
                        //((double.Parse(txtplafon.Text.Replace(".", "")) * jangka) * (double.Parse(z.ToString())/100) / double.Parse(txtjangka.Text));
                        double angsuran = totalbunga + totalpokok;
                        angsuran = Math.Round(angsuran);

                        txtangsuranperbulan.Text = string.Format("{0:N2}", angsuran);
                        DateTime tgl_libur, tgl_realisasi, tgldrop;
                        tgl_realisasi = DateTime.Parse(txtrealisasi.Text);
                        tgl_libur = tgl_realisasi.AddMonths(int.Parse(txtjangka.Text));

                        float bungasrn = float.Parse(txtbgc.Text) / 100;
                        double admasuransi = double.Parse(txtplafon.Text) * bungasrn;
                        admasuransi = Math.Round(admasuransi);
                        
                        txtadmiasuransi.Text = string.Format("{0:N2}", admasuransi);

                        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                        var3[0] = gb.CreateSPParameter("@tglLibur", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tgl_libur, typeof(string));
                        var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("GetTglLibur", var3, "constrmf");
                        int jangkabulan = int.Parse(txtjangka.Text) / 12;
                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                        {

                            tgldrop = tgl_libur;
                        }
                        else
                        {
                            tgldrop = tgl_libur.AddMonths(1);
                        }
                        txttgljt.Text = tgldrop.ToString();


                        DateTime tgljt = Convert.ToDateTime(txttgljt.Text);
                        txttgljt.Text = tgljt.ToString("MM/dd/yyyy");


                        btnNextPengajuan.Enabled = true;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan, periksa kembali inputan anda');", true);
                        txtangsurandimuka.Text = "0";
                        txtbyangsurandimuka.Text = "0";
                        txtbyto.Text = "0";
                        txtangsuranperbulan.Text = "0";
                        txtbaserate.Text = "0";
                        txtbgc.Text = "0";
                        txtspread.Text = "0";
                        txtbungas.Text = "0";
                        txtbypanjar.Text = "0";
                        txtadmiasuransi.Text = "0";
                        txtadmprovisi.Text = "0";
                        txtadmpencairan.Text = "0";
                        txtadmangsuran.Text = "0";
                        txttaksasi.Text = "0";
                        txtangsblokir.Text = "0";

                        btnNextPengajuan.Enabled = false;
                    }


                }
                else
                {
                    string kodearea="";
                    string namaarea = "";
                    try
                    {
                        DefaultClass.Global.SqlHelper[] varA = new DefaultClass.Global.SqlHelper[3];
                        varA[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        varA[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "AMBILAREA", typeof(string));
                        varA[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", varA, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows.Count > 0)
                        {
                            kodearea = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                            namaarea = ge.ListDataset.Tables[0].Rows[0]["nama_area"].ToString();
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bunga Tidak ditemukan dengan jangka = " + txtjangka.Text + ", kodebank = " + kodebank + " program pinjaman = " + programpinjaman + " Area " + kodearea + " " + namaarea + "');", true);
                    }
                    catch (Exception X)
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
                        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }


                    txtangsurandimuka.Text = "0";
                    txtbyangsurandimuka.Text = "0";
                    txtbyto.Text = "0";
                    txtangsuranperbulan.Text = "0";
                    txtbaserate.Text = "0";
                    txtbgc.Text = "0";
                    txtspread.Text = "0";
                    txtbungas.Text = "0";
                    txtbypanjar.Text = "0";
                    txtadmiasuransi.Text = "0";
                    txtadmprovisi.Text = "0";
                    txtadmpencairan.Text = "0";
                    txtadmangsuran.Text = "0";
                    txttaksasi.Text = "0";
                    txtangsblokir.Text = "0";

                    btnNextPengajuan.Enabled = false;
                }

            }



        }


    } 
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {


            PnlFront.Visible = false;
            string jeniskelamin = "";
            ddljenispensiun.Items.Add("Taspen");
            ddljenispensiun.Items.Add("Asabri");

            txtnominalpelunasan.Text = HDNominal.Value;

            DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
            var2[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETSD", typeof(string));
            var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var2, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kodebank asc";

            ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlsumberdana.DataTextField = "namabank";
            ddlsumberdana.DataValueField = "kodeBank";
            ddlsumberdana.DataBind();

            ge.ListDataset = gb.GetDataFunction("dbo.Getkpprogrampinjaman()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

            ddlprogrampinjaman.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlprogrampinjaman.DataTextField = "program";
            ddlprogrampinjaman.DataValueField = "kode";
            ddlprogrampinjaman.DataBind();
            //ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Data", "-"));

            DefaultClass.Global.SqlHelper[] varxx = new DefaultClass.Global.SqlHelper[3];
            varxx[0] = gb.CreateSPParameter("@kodeprogram", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprogrampinjaman.SelectedValue, typeof(string));
            varxx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETSTATUSTO", typeof(string));
            varxx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", varxx, "constrmf");

            txtTO.Text = ge.ListDataset.Tables[0].Rows[0]["STATUS_TO"].ToString();



            // --- Combo Program Pinjaman
            ge.ListDataset = gb.GetDataFunction("dbo.Getmasterjenispensiun()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_pensiun asc";

            ddljenispensiunsk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljenispensiunsk.DataTextField = "pensiun";
            ddljenispensiunsk.DataValueField = "kode_pensiun";
            ddljenispensiunsk.DataBind();
            //ddljenispensiunsk.Items.Insert(0, new ListItem("Pilih Data", "-"));


            // --- Combo  Jenis Penggunaan
            ge.ListDataset = gb.GetDataFunction("dbo.Getmasterkodejiwa()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_jiwa asc";

            ddlkodejiwa.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkodejiwa.DataTextField = "kdjiwa";
            ddlkodejiwa.DataValueField = "kode_jiwa";
            ddlkodejiwa.DataBind();

            //ddlkodejiwa.Items.Insert(0, new ListItem("Pilih Data", "-"));
            btnSimpan.Enabled = true;
            InsertCombo();

            string no_rekening = "";

            string[] splitdata = txtnonasabah.Text.Split('-');
            string nonasabah = splitdata[0];

            txttempatlahir.Text = "1900-01-01 00:00:00";
            txttempatlahir.Enabled = false;
            txttgllahir.Text = "1900-01-01 00:00:00";
            txttgllahir.Enabled = false;
            txtanak2.Text = "1900-01-01 00:00:00";
            txtanak2.Enabled = false;

            txtanak3.Text = "1900-01-01 00:00:00";
            txtanak3.Enabled = false;


            if (txtnonasabah.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah belum di isi');", true);

            }
            else if (txtkodecabang.Text != txtnonasabah.Text.Substring(0, 3))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah yang di input tidak terdapat di cabang Anda');", true);

            }
            else if (txtkodecabang.Text != txtnonasabah.Text.Substring(0, 3))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah yang di input tidak terdapat di cabang Anda');", true);

            }


            else
            {
                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                varx[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLUNAS", typeof(string));
                varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", varx, "constrmf");

                if (int.Parse(ge.ListDataset.Tables[0].Rows[0]["jumlah"].ToString()) == 0)
                {


                    pnladd.Visible = true;
                    //PnlFront.Visible = false;
                    pnlFilter.Visible = false;
                    pnlSK.Visible = true;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETNASABAHBYID", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var, "constrmf");
                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);

                    }
                    else
                    {
                        //Panel Data nasabah 1
                        txtnonasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                        txtktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                        txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();

                        DateTime dtgllahir = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["Tanggal_Lahir"].ToString());
                        txttgllahirpensiun.Text = dtgllahir.ToString("yyyy-MM-dd"); txttgllahirpensiun.Enabled = false;
                        if (ge.ListDataset.Tables[0].Rows[0]["jenkel"].ToString() == "Pria")
                        {
                            ddljeniskelamin.SelectedValue = "0";
                        }
                        else
                        {
                            ddljeniskelamin.SelectedValue = "1";
                        }

                        txtnip.Text = ge.ListDataset.Tables[0].Rows[0]["nip"].ToString();
                        txtnopen.Text = ge.ListDataset.Tables[0].Rows[0]["no_pensiun"].ToString();

                        ddlAO.Items.Add(ge.ListDataset.Tables[0].Rows[0]["sales"].ToString()); ddlAO.Enabled = false;
                        HDAO.Value = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();

                        if (ddljenispermohonan.SelectedValue == "0")
                        {
                            ddlMemo.Visible = false;
                            Label30.Visible = false;
                        }
                        else
                        {
                            string jenispermohonan = "";
                            jenispermohonan = ddljenispermohonan.SelectedValue;


                            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                            var1[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnopen.Text, typeof(string));
                            var1[1] = gb.CreateSPParameter("@kode_Cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                            var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            var1[3] = gb.CreateSPParameter("@typememo", ParameterDirection.Input, SqlDbType.VarChar, 50, jenispermohonan, typeof(string));
                            var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETMEMO", typeof(string));
                            var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                            ge.ListDataset = gb.GetDataJustOutPut("KP_Permohonan_Kredit", var1, "constrmf");

                            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);
                                pnlFilter.Visible = true;
                                PnlFront.Visible = true;
                                pnladd.Visible = false;
                                pnlSK.Visible = false;
                            }
                            else
                            {

                                ge.ListDataset.Tables[0].DefaultView.Sort = "NoMemoInternal asc";
                                ddlMemo.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                                ddlMemo.DataTextField = "memo";
                                ddlMemo.DataValueField = "memo";
                                ddlMemo.DataBind();
                                ddlMemo.Items.Insert(0, new ListItem("Pilih Data", "-"));
                            }
                        }

                    }


                }
                else
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pengajuan sebelumnya belum Lunas, Mohon Lunasi terlebih dahulu');", true);
                    PnlFront.Visible = true;
                    txtnonasabah.Text = "";
                }

            }

        }
          

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    [WebMethod]
    public static string GetNoNasabah(string InputText, string SessionBranchID)
    {
        List<string[]> listResult = new List<string[]>();

        string no_nasabah = InputText;

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessionBranchID, typeof(String));
        Params[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.KP_GetNoNasabahForPermohonan", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

   
    protected void btnBackLaba_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlSK.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlPendapatan.Visible = true;
    }
    protected void btnBackPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlSK.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlPendapatan.Visible = true;
        pnlPengajuan.Visible = false;
    }

    protected void btnNextPengajuan_Click(object sender, EventArgs e)
    {
      
        if (txtrealisasi.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi belum di pilih');", true);

        }else if(txtjaminan.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jaminan Tidak boleh kosong!');", true);

        }
        else if(ddlstatusSK.SelectedValue== "1" && txtpathsk.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('SK Belum di upload!');", true);

        }
        else
        {
            pnladd.Visible = false;
            pnlSK.Visible = false;
            pnlPekerjaan.Visible = false;
            pnlUpdAsuransi.Visible = true;
            pnlPengajuan.Visible = false;
        }
       
    }
    protected void btnNextLaba_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlSK.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPengajuan.Visible = true;
    }
    protected void btnBackIdentitas_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlSK.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPengajuan.Visible = true;
        pnlUpdAsuransi.Visible = false;


    }
    protected void btnBackUsaha_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlSK.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPengajuan.Visible = true;
    }
    protected void txtgapok_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttistri.Focus();

    }
    protected void txttistri_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttkd.Focus();

    }
    protected void txttkd_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttpp.Focus();

    }

    protected void txttpp_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttberas.Focus();


    }

    protected void txttberas_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttcacat.Focus();

    }


    protected void txttcacat_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");
        txtpembulatan.Focus();

    }
    protected void txtpembulatan_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttpm.Focus();

    }

    protected void txttpm_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttkhusus.Focus();

    }


    protected void txttkhusus_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttkematian.Focus();

    }
    protected void txttkematian_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttpajak.Focus();

    }
    protected void txttpajak_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                      decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                      decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

        //txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
        txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);

        grandtotal = pengeluaran - decimal.Parse(txttotal.Text);
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

    }

    protected void txtpotaskes_TextChanged(object sender, EventArgs e)
    {
        decimal potongan = 0, grandtotal = 0;
        potongan = decimal.Parse(txtpotaskes.Text) + decimal.Parse(txtpotassos.Text) + decimal.Parse(txtpotpajak.Text) + decimal.Parse(txtpotlain.Text) +
                      decimal.Parse(txtpotsewarmh.Text) + decimal.Parse(txtpotkasda.Text);

        //txttotal.Text = gb.ToRupiah(potongan.ToString());
        //txttotal.Text = txttotal.Text.Replace("Rp", "");
        txttotal.Text = string.Format("{0:N2}", potongan);

        grandtotal = decimal.Parse(txtgajikotor.Text) - potongan;
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);
        txtpotassos.Focus();

    }

    protected void txtpotassos_TextChanged(object sender, EventArgs e)
    {
        decimal potongan = 0, grandtotal = 0;
        potongan = decimal.Parse(txtpotaskes.Text) + decimal.Parse(txtpotassos.Text) + decimal.Parse(txtpotpajak.Text) + decimal.Parse(txtpotlain.Text) +
                      decimal.Parse(txtpotsewarmh.Text) + decimal.Parse(txtpotkasda.Text);

        //txttotal.Text = gb.ToRupiah(potongan.ToString());
        //txttotal.Text = txttotal.Text.Replace("Rp", "");
        txttotal.Text = string.Format("{0:N2}", potongan);

        grandtotal = decimal.Parse(txtgajikotor.Text) - potongan;
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);

        txtpotpajak.Focus();
    }


    protected void txtpotpajak_TextChanged(object sender, EventArgs e)
    {
        decimal potongan = 0, grandtotal = 0;
        potongan = decimal.Parse(txtpotaskes.Text) + decimal.Parse(txtpotassos.Text) + decimal.Parse(txtpotpajak.Text) + decimal.Parse(txtpotlain.Text) +
                      decimal.Parse(txtpotsewarmh.Text) + decimal.Parse(txtpotkasda.Text);

        //txttotal.Text = gb.ToRupiah(potongan.ToString());
        //txttotal.Text = txttotal.Text.Replace("Rp", "");
        txttotal.Text = string.Format("{0:N2}", potongan);

        grandtotal = decimal.Parse(txtgajikotor.Text) - potongan;
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);

        txtpotlain.Focus();
    }


    protected void txtpotlain_TextChanged(object sender, EventArgs e)
    {
        decimal potongan = 0, grandtotal = 0;
        potongan = decimal.Parse(txtpotaskes.Text) + decimal.Parse(txtpotassos.Text) + decimal.Parse(txtpotpajak.Text) + decimal.Parse(txtpotlain.Text) +
                      decimal.Parse(txtpotsewarmh.Text) + decimal.Parse(txtpotkasda.Text);

        //txttotal.Text = gb.ToRupiah(potongan.ToString());
        //txttotal.Text = txttotal.Text.Replace("Rp", "");
        txttotal.Text = string.Format("{0:N2}", potongan);

        grandtotal = decimal.Parse(txtgajikotor.Text) - potongan;
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);

        txtpotsewarmh.Focus();
    }


    protected void txtpotsewarmh_TextChanged(object sender, EventArgs e)
    {
        decimal potongan = 0, grandtotal = 0;
        potongan = decimal.Parse(txtpotaskes.Text) + decimal.Parse(txtpotassos.Text) + decimal.Parse(txtpotpajak.Text) + decimal.Parse(txtpotlain.Text) +
                      decimal.Parse(txtpotsewarmh.Text) + decimal.Parse(txtpotkasda.Text);

        //txttotal.Text = gb.ToRupiah(potongan.ToString());
        //txttotal.Text = txttotal.Text.Replace("Rp", "");
        txttotal.Text = string.Format("{0:N2}", potongan);

        grandtotal = decimal.Parse(txtgajikotor.Text) - potongan;
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);

        txtpotkasda.Focus();
    }

    protected void txtpotkasda_TextChanged(object sender, EventArgs e)
    {
        decimal potongan = 0, grandtotal = 0;
        potongan = decimal.Parse(txtpotaskes.Text) + decimal.Parse(txtpotassos.Text) + decimal.Parse(txtpotpajak.Text) + decimal.Parse(txtpotlain.Text) +
                      decimal.Parse(txtpotsewarmh.Text) + decimal.Parse(txtpotkasda.Text);

        //txttotal.Text = gb.ToRupiah(potongan.ToString());
        //txttotal.Text = txttotal.Text.Replace("Rp", "");
        txttotal.Text = string.Format("{0:N2}", potongan);

        grandtotal = decimal.Parse(txtgajikotor.Text) - potongan;
        //txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        //txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");
        txtgajibersih.Text = string.Format("{0:N2}", grandtotal);

    }
    protected void txtrealisasi_TextChanged(object sender, EventArgs e)
    {
        if(txtjangka.Text =="")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jangka waktu kosong');", true);

        }
        else
        {
           if(ddljenispermohonan.SelectedValue == "0")
            {

                txtjangka_TextChanged(sender, e);
            }
            else
            {
                DateTime tgl_libur, tgl_realisasi, tgldrop;
                tgl_realisasi = DateTime.Parse(txtrealisasi.Text);
                tgl_libur = tgl_realisasi.AddMonths(int.Parse(txtjangka.Text));
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                var3[0] = gb.CreateSPParameter("@tglLibur", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tgl_libur, typeof(string));
                var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GetTglLibur", var3, "constrmf");


                int jangkabulan = int.Parse(txtjangka.Text) / 12;

                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                {

                    tgldrop = tgl_libur;
                }
                else
                {
                    tgldrop = tgl_libur.AddMonths(1);
                }
                txttgljt.Text = tgldrop.ToString();


                DateTime tgljt = Convert.ToDateTime(txttgljt.Text);
                txttgljt.Text = tgljt.ToString("MM/dd/yyyy");

                DateTime date = Convert.ToDateTime(txttgllahirpensiun.Text.Trim());
                string dateString = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

                decimal usia1 = gb.HitungUmur(decimal.Parse(dateString).ToString());

                txtusia.Text = usia1.ToString();
            }
        }
        
    }
    protected void ddlsumberdana_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtjangka.Text != "")
        {
            txtjangka_TextChanged(sender, e);
        }
    }

        
    protected void ddlprogrampinjaman_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtjangka.Text != "")
        {
            txtjangka_TextChanged(sender, e);
        }


        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@kodeprogram", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprogrampinjaman.SelectedValue, typeof(string));
        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETSTATUSTO", typeof(string));
        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var2, "constrmf");

        txtTO.Text = ge.ListDataset.Tables[0].Rows[0]["STATUS_TO"].ToString();

        if(txtTO.Text == "TIDAK")
        {
            HDStatusTO.Value = "0";
        }
        else
        {
            HDStatusTO.Value = "1";
        }

    }

    protected void ddlstatusnikah_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstatusnikah.SelectedValue == "0")
        {
            txttempatlahir.Text = "1900-01-01 00:00:00";
            txttempatlahir.Enabled = false;
            txttgllahir.Text = "1900-01-01 00:00:00";
            txttgllahir.Enabled = false;
            txtanak2.Text = "1900-01-01 00:00:00";
            txtanak2.Enabled = false;

            txtanak3.Text = "1900-01-01 00:00:00";
            txtanak3.Enabled = false;
        }
        else
        {

            txttempatlahir.Text = "";
            txttgllahir.Text = "";
            txtanak2.Text = "";
            txtanak3.Text = "";

            txttempatlahir.Enabled = true; 
            txttgllahir.Enabled = true; 
            txtanak2.Enabled = true; 
            txtanak3.Enabled = true;
        }
    }
    protected void ddlkodejiwa_SelectedIndexChanged(object sender, EventArgs e)
    {
      
       
            //string[] ambildata = ddlkodejiwa.SelectedValue;
            string kodejiwa = ddlkodejiwa.SelectedValue;

            kodejiwa = kodejiwa.Substring(kodejiwa.Length - 1, 1);

            if (kodejiwa.Equals("0"))
            {
                txttgllahir.Text = "1900-01-01 00:00:00";
                txttgllahir.Enabled = false;
                txtanak2.Text = "1900-01-01 00:00:00";
                txtanak2.Enabled = false;

                txtanak3.Text = "1900-01-01 00:00:00";
                txtanak3.Enabled = false;

            }
            else if (kodejiwa.Equals("1"))
            {
                txttgllahir.Text = "";
                txttgllahir.Enabled = true;
                txtanak2.Text = "1900-01-01 00:00:00";
                txtanak2.Enabled = false;

                txtanak3.Text = "1900-01-01 00:00:00";
                txtanak3.Enabled = false;

            }
            else if (kodejiwa.Equals("2"))
            {

                txttgllahir.Enabled = true;
                txttgllahir.Text = "";
                txtanak2.Text = "";
                txtanak2.Enabled = true;
                txtanak3.Text = "1900-01-01 00:00:00";
                txtanak3.Enabled = false;

            }
            else
            {

                txttgllahir.Text = "";
                txtanak2.Text = "";
                txtanak3.Text = "";
                txttgllahir.Enabled = true;
                txtanak2.Enabled = true;
                txtanak3.Enabled = true;
            }
  
        
      
    }
     

    protected void txtplafon_TextChanged(object sender, EventArgs e)
    {
        txtbendahara.Text = txtplafon.Text;
    }

    public void InsertCombo()
    {

        // --- Combo Bank Debitur
        ge.ListDataset = gb.GetDataFunction("dbo.KPGetNominalTransfer()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_bank asc";
        ddlBankNasabah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlBankNasabah.DataTextField = "nama_bank";
        ddlBankNasabah.DataValueField = "nama_bank";
        ddlBankNasabah.DataBind();
        //ddlBankNasabah.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo  Jenis Kredit
        ge.ListDataset = gb.GetDataFunction("dbo.Getmastergolongan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id_gol asc";

        ddlgolongan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlgolongan.DataTextField = "gol";
        ddlgolongan.DataValueField = "id_gol";
        ddlgolongan.DataBind();
        //ddlgolongan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Golongan penjamin

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETINSTANSI", typeof(string));
        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var2, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kd_instansi asc";

        ddlkodeinstansi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkodeinstansi.DataTextField = "intansi";
        ddlkodeinstansi.DataValueField = "kd_instansi";
        ddlkodeinstansi.DataBind(); 




        // --- Combo Kode Produk
        ge.ListDataset = gb.GetDataFunction("dbo.GetKPProduk()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";

        ddlkdproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdproduk.DataTextField = "produk";
        ddlkdproduk.DataValueField = "kode_produk";
        ddlkdproduk.DataBind(); 


        //// --- Combo Pendidikan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetArea()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "kode_area asc";

        //ddlkdarea.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlkdarea.DataTextField = "nama_area";
        //ddlkdarea.DataValueField = "kode_area";
        //ddlkdarea.DataBind();
        //ddlkdarea.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo  Jenis Penggunaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPenggunaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_penggunaan asc";

        ddlPenggunaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlPenggunaan.DataTextField = "penggunaan";
        ddlPenggunaan.DataValueField = "kode_penggunaan";
        ddlPenggunaan.DataBind();
        //ddlPenggunaan.Items.Insert(0, new ListItem("Pilih Data", "-"));



        //// Combo MO dan REF Non MO
        //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        //var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
        //var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_MO", var1, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "NIK asc";

        //ddlAO.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlAO.DataTextField = "namaAO";
        //ddlAO.DataValueField = "NIK";
        //ddlAO.DataBind();
        //ddlAO.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // Combo MO dan REF Non MO
        DefaultClass.Global.SqlHelper[] var22 = new DefaultClass.Global.SqlHelper[3];
        var22[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var22[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDNONSALES", typeof(string));
        var22[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_MO", var22, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NIK asc";

        ddlnonao.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlnonao.DataTextField = "namaAO";
        ddlnonao.DataValueField = "NIK";
        ddlnonao.DataBind();
        //ddlnonao.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // Combo MO dan REF Non MO
        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
        var3[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJURUBAYAR", typeof(string));
        var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT", var3, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KantorBayar asc";

        ddlKantorBayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKantorBayar.DataTextField = "jurubayar";
        ddlKantorBayar.DataValueField = "KantorBayar";
        ddlKantorBayar.DataBind();
        //ddlKantorBayar.Items.Insert(0, new ListItem("Pilih Data", "-"));
        
        //ddlblokir.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddlblokir.Items.Add("0");
        ddlblokir.Items.Add("1");

        //ddlkali.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddlkali.Items.Add("0");
        ddlkali.Items.Add("1");
        ddlkali.Items.Add("2");
        ddlkali.Items.Add("3"); 

    }

}