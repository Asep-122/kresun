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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text.pdf;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

public partial class Kresun_Process_PermohonanPinjamanKreditMultigunaChild : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    Global gbx = new DefaultClass.Global();
    GlobalEntities gex = new GlobalEntities();

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

    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string btnafterDelete()
    {

        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermohonanPinjamanKreditMultigunaNew.aspx");
    }

    protected void Data_Formula(object sender, EventArgs e)
    {
        try
        {


            if (ddlinstansi.SelectedValue != "" && ddlkategori.SelectedValue != "" && ddlKantorBayar.SelectedValue != "" && ddlsumberdana.SelectedValue != "" && ddlpensiun.SelectedValue != "" && ddlprogrampinjaman.SelectedValue != "" && txttgllahir.Text != "" && ddljangkawaktu.SelectedValue != "0")
            {

                if (Convert.ToDecimal(txtgajidisplay.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon isi gaji display terlebih dahulu');", true);
                    txtplafon.Text = "0";
                    return;
                }

                object tgltmt = CheckTglNull(txtmtpensiun.Text);
                object tglrealisasi = CheckTglNull(txttglrealisasi.Text);
                object tgllahir = CheckTglNull(txttgllahir.Text);


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[21];
                var[0] = gb.CreateSPParameter("@instansix", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlinstansi.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@Tglrealisasi", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglrealisasi, typeof(DateTime));
                var[2] = gb.CreateSPParameter("@C13Kategori", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlkategori.SelectedValue, typeof(string));
                var[3] = gb.CreateSPParameter("@C11KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayar.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@F9SUMBERDANA", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlsumberdana.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@C17jangkawaktubulan", ParameterDirection.Input, SqlDbType.Int, 0, ddljangkawaktu.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@C21gajidiplay", ParameterDirection.Input, SqlDbType.Money, 0, txtgajidisplay.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@C19jenispensiun", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpensiun.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@C12ProdukGroup", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlprogrampinjaman.SelectedValue, typeof(string));
                var[9] = gb.CreateSPParameter("@G14Programpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlproduk.SelectedValue, typeof(string));
                var[10] = gb.CreateSPParameter("@C20Statusnikah", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatusnikah.SelectedValue, typeof(string));
                var[11] = gb.CreateSPParameter("@C24tmtpensiun", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tgltmt, typeof(DateTime));
                var[12] = gb.CreateSPParameter("@C25Plafon", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(string));
                var[13] = gb.CreateSPParameter("@kodesimulasi", ParameterDirection.Input, SqlDbType.VarChar, 2, txtkodesimulasi.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@TglLahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tgllahir, typeof(DateTime));
                var[15] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                //var[16] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KP_HITUNG_KANTOR_BAYAR_POS", typeof(string));
                var[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[17] = gb.CreateSPParameter("@C25Pelunasan", ParameterDirection.Input, SqlDbType.Money, 0, txtpelunasan.Text, typeof(string));
                var[18] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, /*txtnoanggota.Text*/ string.Empty, typeof(string));
                var[19] = gb.CreateSPParameter("@PK", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlPk.SelectedValue, typeof(string));
                var[20] = gb.CreateSPParameter("@TunjanganIstriAnak", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlTunjanganIstriAnak.SelectedValue, typeof(string));
                if (ddlKantorBayar.SelectedValue == "A014" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_POS", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A009" && ddlsumberdana.SelectedValue == "A009" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_CAPITAL", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A015" && ddlsumberdana.SelectedValue != "A009" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_Capital_A", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A013" && ddlsumberdana.SelectedValue != "A009" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_MANTAP", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A014" && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3"))
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_JANDA_DUDA", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A015" && ddlsumberdana.SelectedValue != "A009" && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3"))
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_JANDA_DUDA", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A013" && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3"))
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_JANDA_DUDA", var, "constrmf");
                }

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    if (ge.ListDataset.Tables[0].Rows[0]["statusmsg"].ToString() == "1")
                    {
                        txtplafon.Text = "0";
                        var datamsg = ge.ListDataset.Tables[0].Rows[0]["msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + datamsg + "');", true);
                        return;
                    }
                    /*                    if(ge.ListDataset.Tables[0].Rows[0]["bungasenaefektif"].ToString() == "0")
                                        {
                                            txtplafon.Text = "0";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rate Bunga Efektif tidak boleh 0 mohon di isi Master terlebih dahulu ');", true);
                                            return;
                                        }*/
                    if (ge.ListDataset.Tables[0].Rows[0]["bungasenaflat"].ToString() == "0")
                    {
                        txtplafon.Text = "0";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rate Bunga Flat tidak boleh 0 mohon di isi Master terlebih dahulu ');", true);
                        return;
                    }

                    if (decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()) > decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonmax"].ToString()))
                    {

                        txtplafon.Text = "0";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Plafon tidak boleh lebih besar dari Palfon Max');", true);
                        return;
                    }

                    else
                    {
                        if (txtkodesimulasi.Text == "A")
                        {
                            txtusiapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["UsiaPeminjam"].ToString());
                            txtusiaasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["Usiaasuransi"].ToString());
                            txtjangkatahun.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktutahun"].ToString());
                            txttgljatuhtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljatuhtempo"].ToString()));
                            ddlangsurandimukablokir.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString());
                            ddlangsurandimuka.Enabled = true;
                            ddlmaterai.Enabled = true;

                            //                        ddlangsurandimuka.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsurandimuka"].ToString());
                            //                        ddlangsurandimukaasuransi.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString());
                            //                        ddlmaterai.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString());
                            //                        ddlprovisi.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["persenProvisi"].ToString());
                            //ddlmutasikantorbayar.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["@@I23persenProvisi"].ToString());
                            txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["ketanggota"].ToString();
                            txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["ketrekening"].ToString();
                            txtusiajthtempo.Text = ge.ListDataset.Tables[0].Rows[0]["usiajatuhtempo"].ToString();
                            txtplafonmax.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonmax"].ToString()));
                            txtjangkawaktumax.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktumax"].ToString());
                            txtratebungapinjaman.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaflat"].ToString());
                            txtratebungaefektif.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaefektif"].ToString());
                            txtrateasuransi.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString());
                            txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran"].ToString()));
                            txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenangsuran"].ToString()));
                            txtasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString()));
                            txtpersenasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString()));
                            txttotal.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["total"].ToString()));
                            txttotalpersen.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpersen"].ToString()));
                            txtsisagaji.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagaji"].ToString()));
                            txtsisagajipersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Persensisagaji"].ToString()));
                            //                        txtprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString()));
                            txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenprovisi"].ToString()));
                            txtadminbank.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbank"].ToString()));
                            txtpersenadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenadminbank"].ToString()));
                            txtkeanggotaan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()));
                            //                        txtmaterai.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["materai"].ToString()));
                            //txtmateraipcs.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString()));
                            txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokir"].ToString()));
                            txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString()));
                            //                        txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimuka"].ToString()));
                            //txtangsurandimukakali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandimuka"].ToString()));
                            txtangdimukaasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranAsuransidiblokir"].ToString()));
                            //txtangdimukaasuransikali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString()));
                            txtbiayaadmin.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tatalaksana"].ToString()));
                            txtpelunasanRp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pelunasanRp"].ToString()));
                            //                        txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["terimabersih"].ToString()));
                            decimal terimabersih = decimal.Parse(txtplafon.Text) - decimal.Parse(txtprovisi.Text) - decimal.Parse(txtadminbank.Text) - decimal.Parse(txtasuransi.Text) - decimal.Parse(txtkeanggotaan.Text) - decimal.Parse(txtangsuranblokir.Text) - decimal.Parse(txtangsurandimuka.Text) - decimal.Parse(txtangdimukaasuransi.Text) - decimal.Parse(txtmaterai.Text) - decimal.Parse(txtbiayaadmin.Text);
                            txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(terimabersih.ToString()));

                        }
                        else
                        {
                            ddlangsurandimuka.Items.Clear();
                            for (int i = 1; i <= 30; i++)
                            {
                                ddlangsurandimuka.Items.Add(new ListItem(i.ToString(), i.ToString()));

                            }
                            ddlangsurandimuka.Items.Insert(0, new ListItem("Pilih Angsuran Dimuka Angsuran", "0"));


                            txtusiapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["UsiaPeminjam"].ToString());
                            txtusiaasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["Usiaasuransi"].ToString());
                            txtjangkatahun.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktutahun"].ToString());
                            txttgljatuhtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljatuhtempo"].ToString()));
                            ddlangsurandimukablokir.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString());
                            ddlangsurandimukablokir.Enabled = false;
                            ddlangsurandimuka.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsurandimuka"].ToString());
                            ddlangsurandimuka.Enabled = false;
                            ddlangsurandimukaasuransi.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString());
                            ddlmaterai.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString());
                            ddlmaterai.Enabled = false;
                            string persenprovisi = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["persenProvisi"].ToString());
                            ddlprovisi.Items.Clear();
                            ddlprovisi.Items.Insert(0, new ListItem(persenprovisi, persenprovisi));

                            ddlprovisi.SelectedValue = persenprovisi;
                            //ddlmutasikantorbayar.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["@@I23persenProvisi"].ToString());
                            txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["ketanggota"].ToString();
                            txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["ketrekening"].ToString();
                            txtusiajthtempo.Text = ge.ListDataset.Tables[0].Rows[0]["usiajatuhtempo"].ToString();
                            txtplafonmax.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonmax"].ToString()));
                            txtjangkawaktumax.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktumax"].ToString());
                            txtratebungapinjaman.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaflat"].ToString());
                            txtratebungaefektif.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaefektif"].ToString());
                            txtrateasuransi.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString());
                            txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran"].ToString()));
                            txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenangsuran"].ToString()));
                            txtasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString()));
                            txtpersenasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString()));
                            txttotal.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["total"].ToString()));
                            txttotalpersen.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpersen"].ToString()));
                            txtsisagaji.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagaji"].ToString()));
                            txtsisagajipersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Persensisagaji"].ToString()));
                            txtprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString()));
                            txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenprovisi"].ToString()));
                            txtadminbank.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbank"].ToString()));
                            txtpersenadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenadminbank"].ToString()));
                            txtkeanggotaan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()));
                            txtmaterai.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["materai"].ToString()));
                            txtmateraipcs.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString()));
                            txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokir"].ToString()));
                            txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString()));
                            txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimuka"].ToString()));
                            txtangsurandimukakali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandimuka"].ToString()));
                            txtangdimukaasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranAsuransidiblokir"].ToString()));
                            txtangdimukaasuransikali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString()));
                            txtbiayaadmin.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tatalaksana"].ToString()));
                            txtpelunasanRp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pelunasanRp"].ToString()));
                            txtgraceperiode.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gracePeriode"].ToString()));
                            //                            txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["terimabersih"].ToString()));
                            decimal terimabersih = decimal.Parse(txtplafon.Text) - decimal.Parse(txtasuransi.Text) - decimal.Parse(txtprovisi.Text) - decimal.Parse(txtadminbank.Text) - decimal.Parse(txtkeanggotaan.Text) -
                                decimal.Parse(txtangsuranblokir.Text) - decimal.Parse(txtangsurandimuka.Text) - decimal.Parse(txtmaterai.Text) - decimal.Parse(txtbiayaadmin.Text) - decimal.Parse(txtgraceperiode.Text);
                            txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(terimabersih.ToString()));

                        }



                    }



                }
                else
                {
                    txtusiapinjaman.Text = string.Format("{0:N0}", "0");
                    txtusiaasuransi.Text = string.Format("{0:N0}", "0");
                    ddlangsurandimukablokir.SelectedValue = "0";
                    ddlangsurandimuka.SelectedValue = "0";
                    ddlangsurandimukaasuransi.SelectedValue = "0";
                    ddlmaterai.SelectedValue = "0";
                    txtnama.Text = "";
                    txtstatuspinjaman.Text = "";
                    txtusiapinjaman.Text = "0";
                    txtusiaasuransi.Text = "0";
                    txtusiaanak1.Text = "0";
                    txtusiaanak2.Text = "0";
                    txtplafonmax.Text = "0";
                    txtjangkawaktumax.Text = "0";
                    txtusiajthtempo.Text = "0";
                    txtrateasuransi.Text = "0";
                    txtratebungapinjaman.Text = "0";
                    txtangsuran.Text = "0";
                    txtpsnangsuran.Text = "0";
                    txtasuransi.Text = "0";
                    txtpersenasuransi.Text = "0";
                    txttotal.Text = "0";
                    txttotalpersen.Text = "0";
                    txtsisagaji.Text = "0";
                    txtsisagajipersen.Text = "0";
                    txtprovisi.Text = "0";
                    txtpersenprovisi.Text = "0";
                    txtadminbank.Text = "0";
                    txtpersenadminbank.Text = "0";
                    txtkeanggotaan.Text = "0";
                    txtmaterai.Text = "0";
                    txtmateraipcs.Text = "0";
                    txtangsuranblokir.Text = "0";
                    txtangsuranblokirkali.Text = "0";
                    txtangsurandimuka.Text = "0";
                    txtangsurandimukakali.Text = "0";
                    txtangdimukaasuransi.Text = "0";
                    txtangdimukaasuransikali.Text = "0";
                    txtbiayaadmin.Text = "0";
                    txtpelunasanRp.Text = "0";
                    txtterimabersihrp.Text = "0";


                }



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

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {

    }

    protected void gajipokok_ontextchanged(object sender, EventArgs e)
    {
        try
        {

            decimal a, b, c, d, f, g, h, i, j, k, x, y, z;
            a = decimal.Parse(txtgapok.Text);
            b = decimal.Parse(txttunjistri.Text);
            c = decimal.Parse(txttunjangananak.Text);
            d = decimal.Parse(txttunjanganberas.Text);
            f = decimal.Parse(txttunjanganlain.Text);

            g = decimal.Parse(txtpotonganpajak.Text);
            h = decimal.Parse(txtpotonganaskes.Text);
            i = decimal.Parse(txtpotonganlain.Text);

            txtnetgaji.Text = string.Format("{0:N0}", a + b + c + d + f);
            txttotalpotongan.Text = string.Format("{0:N0}", g + h + i);
            txtgajibersih.Text = string.Format("{0:N0}", decimal.Parse(txtnetgaji.Text) - decimal.Parse(txttotalpotongan.Text));


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public object CheckTglNull(string ParamTanggal)
    {
        object Tanggal;
        if (string.IsNullOrWhiteSpace(ParamTanggal))
        {
            Tanggal = DBNull.Value;
        }
        else
        {
            DateTime parsedDate;
            if (DateTime.TryParse(ParamTanggal, out parsedDate))
            {
                Tanggal = parsedDate;
            }
            else
            {
                Tanggal = DBNull.Value;
            }
        }

        return Tanggal;
    }
    public object ChecknumberNull(string ParamTanggal)
    {
        object number;
        if (string.IsNullOrWhiteSpace(ParamTanggal))
        {
            number = 0;
        }
        else
        {
            number = ParamTanggal;
        }

        return number;
    }


}
