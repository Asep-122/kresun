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

public partial class AR_View_CetakPerjanjianKredit : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string no_rekening = "", kepala_cabang = "", alamat = "", picb = "", alamat_nasabah = "", nmpjmn2 = "", plafon = "", jangka = "", tgl_drop = "", 
           tgl_due = "",pokok = "", bunga = "", rek_tabbr = "", persen_denda_pelunasan = "", ket_jmn = "", NamaCabang = "", kode_produk = "",
           file_report = "";

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

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void txtnorekening_TextChanged(object sender, EventArgs e)
    {
        //string[] splitdata = txtnorekening.Text.Split('-');
        //HDNorekening.Value = splitdata[0];

        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        //var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNorekening.Value, typeof(string));
        //var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPERJANJIANKREDIT", typeof(string));
        //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("AR_CETAKPK", var, "constrmf");

        //if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
        //{
        //    HDKdpersh.Value = ge.ListDataset.Tables[0].Rows[0]["kodesumberdana"].ToString();

        //    txttd.Text = "-";
        //    txtjabatan.Text = "-";
           
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak ditemukan!');", true);

        //} 
    }

    protected void btnCetakAnggota_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
            if(txtnorekening.Text.Substring(0,3) == SessBranchID)
            {
                


						string nobukti;
                      
                        string norek =HDNorekening.Value;
                        string nonasabah = HDNNONASABAH.Value;

                        string kode_produk, file_report;


                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                        var[1] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
                        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKPKREAL", typeof(string));
			
                        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_PENCAIRAN", var, "constrmf");
                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No rekening ini tidak dapat dicetak!');", true);
                }
                else
                {

                
                    kode_produk = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                        file_report = ge.ListDataset.Tables[0].Rows[0]["path_filereport"].ToString();
                               string status = ddlstatus.SelectedValue;
                decimal biayacollection = 0;
                decimal bungaxr = 0;
                    string xjenistagihan = ge.ListDataset.Tables[0].Rows[0]["jenistagihan"].ToString();
                    string datatr = "";
                    if (kode_produk == "3C") //Pembiayaan sendiri mingguan
                        {
                            rprt.Load(Server.MapPath(file_report));
                            rprt.SetParameterValue("kepala_cabang", ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString());
                            rprt.SetParameterValue("alamat_cabang", ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString());
                            rprt.SetParameterValue("nama_nasabah", ge.ListDataset.Tables[0].Rows[0]["picb"].ToString());
                            rprt.SetParameterValue("alamat_nasabah", ge.ListDataset.Tables[0].Rows[0]["alamat_nasabah"].ToString());
                            rprt.SetParameterValue("nm_penjamin", ge.ListDataset.Tables[0].Rows[0]["nmpjmn2"].ToString());
                            rprt.SetParameterValue("plafon", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("jangka", ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString());
                            rprt.SetParameterValue("tgl_drop", ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString());
                            rprt.SetParameterValue("tgl_due", ge.ListDataset.Tables[0].Rows[0]["tgl_due"].ToString());
                            rprt.SetParameterValue("pokok----------10", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("bunga", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("angsuran_perbulan", string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rek_tabbrx"].ToString())));
                            rprt.SetParameterValue("persen_denda", ge.ListDataset.Tables[0].Rows[0]["persen_denda_pelunasan"].ToString());
                            //rprt.SetParameterValue("jaminan", ge.ListDataset.Tables[0].Rows[0]["ket_jmn"].ToString());
                            rprt.SetParameterValue("jaminan", ge.ListDataset.Tables[0].Rows[0]["ket_jmn"].ToString().Replace("\r\n", "" + " /tidak terlampir" + " \r\n").Remove(ge.ListDataset.Tables[0].Rows[0]["ket_jmn"].ToString().Replace("\r\n", "" + " /tidak terlampir" + " \r\n").Length - 1, 0) + " /tidak terlampir");
                            rprt.SetParameterValue("nama_cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                            rprt.SetParameterValue("terbilangPlafon", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString())));
                            rprt.SetParameterValue("terbilang_angsuran_perbulan", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rek_tabbrx"].ToString())));
                            rprt.SetParameterValue("no_pk", ge.ListDataset.Tables[0].Rows[0]["no_spk"].ToString());
                            rprt.SetParameterValue("no_rekening-----20", norek);
                            rprt.SetParameterValue("nama_koperasi", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            rprt.SetParameterValue("tanggal_print", DateTime.Now.ToString("dd-MMMM-yyyy"));
                            rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                        //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                        //if (SessBranchID != "301")
                        //{
                        //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                        //}
                        //else
                        //{

                        //rprt.SetParameterValue("pad", "");
                        //}
                        rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                        if (SessBranchID != "301")
                        {
                            rprt.SetParameterValue("izin_cabang", "Izin Cabang Nomor : " + ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                            //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                        }
                        else
                        {
                            rprt.SetParameterValue("izin_cabang", "");
                        }

                        rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            rprt.SetParameterValue("statuskop", status);
                            rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());
                            rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            //rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                            rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                            rprt.SetParameterValue("cabang", ge.ListDataset.Tables[0].Rows[0]["cabang"].ToString());
                            rprt.SetParameterValue("bagian", ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString());
                        //biayacollection = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()) * decimal.Parse("0,3");
                        //bungaxr = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()) * decimal.Parse("0,7");

                        datatr = ge.ListDataset.Tables[0].Rows[0]["cetaklama"].ToString();

                        rprt.SetParameterValue("collection", string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungacollection"].ToString())));
                        rprt.SetParameterValue("bungabaru", string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaR"].ToString())));


                        //if (xjenistagihan == "3")
                        //{

                        //}
                        //else
                        //{
                        //    bungaxr = decimal.Parse("0,02") * decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString()) * decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()) / decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString()) / decimal.Parse("4");
                        //    biayacollection = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()) - bungaxr;

                        //}

                        //    if (datatr == "1")
                        //    {


                        //        bungaxr = decimal.Parse("0,02") * decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString()) * decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()) / decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString()) / decimal.Parse("4");

                        //    }

                        //    else
                        //    {
                        //        if (xjenistagihan == "3")
                        //        {
                        //            bungaxr = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                        //        }
                        //        else
                        //        {
                        //            bungaxr = decimal.Parse("0,0175") * decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString()) * decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()) / decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString()) / decimal.Parse("4");

                        //        }

                        //    }

                        //    if (xjenistagihan == "3")
                        //    {
                        //        biayacollection = decimal.Parse("0");
                        //    }
                        //    else
                        //    {
                        //        biayacollection = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()) - bungaxr;
                        //    }




                        //    rprt.SetParameterValue("collection", gb.ToRupiah(string.Format("{0:N0}", biayacollection).ToString()).Replace("Rp", ""));
                        //rprt.SetParameterValue("bungabaru", gb.ToRupiah(string.Format("{0:N0}", bungaxr).ToString()).Replace("Rp", ""));


                        Response.ClearContent();
                            Response.ClearHeaders();

                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                            Response.End();
                        }
                        else if (kode_produk == "3B") //pembiayaan sendiri bulanan
                        {
                            //string bunga_terbilang = gb.terbilang("constrmf", ge.ListDataset.Tables[0].Rows[0]["persen_bunga"].ToString().Trim());
                            //string SanksiAdministrasi_terbilang = gb.terbilang("constrmf", ge.ListDataset.Tables[0].Rows[0]["sanksi_administrasi_perbulan"].ToString().Trim().Replace(",", "."));
                            //string potonganbunga_terbilang = gb.terbilang("constrmf", ge.ListDataset.Tables[0].Rows[0]["PotonganBunga"].ToString().Trim().Replace(",", "."));
                            string bunga_terbilang = "";
                            string SanksiAdministrasi_terbilang = "";
                            string potonganbunga_terbilang = "";

                            //ReportDocument rprt = new ReportDocument();
                            rprt.Load(Server.MapPath(file_report));
                            rprt.SetParameterValue("No.PK", ge.ListDataset.Tables[0].Rows[0]["no_spk"].ToString());
                            rprt.SetParameterValue("nama_koperasi", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            rprt.SetParameterValue("kepala_cabang", ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString());
                            rprt.SetParameterValue("NamaPejabat", ge.ListDataset.Tables[0].Rows[0]["Kepala_koperasi"].ToString());
                            rprt.SetParameterValue("NamaAnggota", ge.ListDataset.Tables[0].Rows[0]["picb"].ToString().Trim());
                            rprt.SetParameterValue("NamaAnggotaBesar", ge.ListDataset.Tables[0].Rows[0]["namaanggota"].ToString().Trim());
                            rprt.SetParameterValue("NamaPenjamin", ge.ListDataset.Tables[0].Rows[0]["nmpjmn2"].ToString());
                            rprt.SetParameterValue("AlamatAnggota---------05", ge.ListDataset.Tables[0].Rows[0]["alamat_nasabah"].ToString());
                            rprt.SetParameterValue("Plafon", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("PlafonTerbilang", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString())));
                            rprt.SetParameterValue("AngsTotal", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["rek_tabbr"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("AngsTotalTerbilang", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rek_tabbr"].ToString())));
                            rprt.SetParameterValue("JangkaWaktu-----------10", ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString());
                            rprt.SetParameterValue("JangkaWaktuTerbilang", gb.TerbilangAngka(int.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString())));
                            rprt.SetParameterValue("TanggalDrop", ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString());
                            rprt.SetParameterValue("TanggalDue", ge.ListDataset.Tables[0].Rows[0]["tgl_due"].ToString());
                            rprt.SetParameterValue("Jaminan1", ge.ListDataset.Tables[0].Rows[0]["ket_jmn"].ToString());
                            rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            rprt.SetParameterValue("Jabatan", ge.ListDataset.Tables[0].Rows[0]["jabatan"].ToString());
                            rprt.SetParameterValue("Perwakilan/Sendiri", ge.ListDataset.Tables[0].Rows[0]["Perwakilan"].ToString());
                            rprt.SetParameterValue("NoRekening", norek);
                            rprt.SetParameterValue("Keperluan", ge.ListDataset.Tables[0].Rows[0]["keterangan"].ToString());
                            //string asda = string.Format("{0:N2}",decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persen_bunga"].ToString()));
                            rprt.SetParameterValue("Bunga%---------------20", string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persen_bunga"].ToString())));
                            //rprt.SetParameterValue("BungaTerbilang", bunga_terbilang);
                            rprt.SetParameterValue("SanksiAdministrasi%", ge.ListDataset.Tables[0].Rows[0]["persen_denda_tunggakan"].ToString());
                            rprt.SetParameterValue("SanksiAdministrasi/bulan", ge.ListDataset.Tables[0].Rows[0]["sanksi_administrasi_perbulan"].ToString());//blm ada
                            rprt.SetParameterValue("SanksiAdministrasi/bulanTerbilang", ge.ListDataset.Tables[0].Rows[0]["SanksiAdministrasi_terbilang"].ToString());  //SanksiAdministrasi_terbilang + " Persen");//blm ada
                            rprt.SetParameterValue("Provisi%", ge.ListDataset.Tables[0].Rows[0]["PsnProvisi"].ToString());
                            rprt.SetParameterValue("PotonganBunga%", ge.ListDataset.Tables[0].Rows[0]["PotonganBunga"].ToString());
                            rprt.SetParameterValue("PotonganBungaTerbilang", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PotonganBunga"].ToString())));
                            rprt.SetParameterValue("PekerjaanPenjamin", "-");
                            rprt.SetParameterValue("PekerjaanAnggota", ge.ListDataset.Tables[0].Rows[0]["pekerjaan_anggota"].ToString());
                            rprt.SetParameterValue("AlamatPenjamin-------30", ge.ListDataset.Tables[0].Rows[0]["alamat_penjamin"].ToString());
                            rprt.SetParameterValue("HubunganPenjamin", ge.ListDataset.Tables[0].Rows[0]["keterangan"].ToString());
                            rprt.SetParameterValue("DendaJaminan%", ge.ListDataset.Tables[0].Rows[0]["dendajaminan"].ToString());
                            rprt.SetParameterValue("Satuan", ge.ListDataset.Tables[0].Rows[0]["satuan"].ToString());
                            rprt.SetParameterValue("Nama Cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                            rprt.SetParameterValue("Tanggal Hari Ini", DateTime.Now.ToString("dd MMMM yyyy"));
                            rprt.SetParameterValue("Agunan", ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString());
                            rprt.SetParameterValue("pengadilan", ge.ListDataset.Tables[0].Rows[0]["pengadilan"].ToString());
                            rprt.SetParameterValue("Tandatangannama", "");
                            rprt.SetParameterValue("Tandatanganjab", "");
                            rprt.SetParameterValue("BungaTerbilang", ge.ListDataset.Tables[0].Rows[0]["BungaTerbilang"].ToString());
                            rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                        rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                        if (SessBranchID != "301")
                        {
                            rprt.SetParameterValue("izin_cabang", "Izin Cabang Nomor : " +  ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                            //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                        }
                        else
                        {
                            rprt.SetParameterValue("izin_cabang", "");
                        }

                        rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            rprt.SetParameterValue("statuskop", status);
                            rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());

                        
                            rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                            rprt.SetParameterValue("cabang", ge.ListDataset.Tables[0].Rows[0]["cabang"].ToString());
                            rprt.SetParameterValue("kepala_cabang", ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString());


                            Response.ClearContent();
                            Response.ClearHeaders();

                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                            Response.End();
                        }
			    }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Rekening tidak terdapat di cabang anda!');", true);
            } 

        }catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            }else
			{
				rprt.Dispose();
                rprt.Close();
                GC.Collect();
			}
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
        Params[1] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_GetNoRekeningForCetakPK", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }


}