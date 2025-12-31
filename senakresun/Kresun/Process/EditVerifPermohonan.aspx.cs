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


public partial class Kresun_Process_EditVerifPermohonan : System.Web.UI.Page
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

            BindGridEntity();
             
            btnSimpan.Enabled = true;
            PnlFront.Visible = true;
        }


        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnSimpan);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");


        CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
        CultureName = "id-ID";
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
        Response.Redirect("EditVerifPermohonan.aspx");
    } 
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
        try

        {
            if (txtgajikotor.Text == "0")
            {
                decimal pengeluaran = 0, grandtotal = 0;
                pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                              decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                             decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

                txtgajikotor.Text = string.Format("{0:N2}", pengeluaran);
                //txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

            }
            else if (txttotal.Text == "0")
            {
                decimal potongan = 0, grandtotal = 0;
                potongan = decimal.Parse(txtpotaskes.Text) + decimal.Parse(txtpotassos.Text) + decimal.Parse(txtpotpajak.Text) + decimal.Parse(txtpotlain.Text) +
                              decimal.Parse(txtpotsewarmh.Text) + decimal.Parse(txtpotkasda.Text);

                //txttotal.Text = gb.ToRupiah(potongan.ToString());
                //txttotal.Text = txttotal.Text.Replace("Rp", "");
                txttotal.Text = string.Format("{0:N2}", potongan);
            }
            else if (txtgajibersih.Text == "0")
            {
                decimal potongan = 0, grandtotal = 0, pengeluaran = 0;
                pengeluaran = decimal.Parse(txtgapok.Text) + decimal.Parse(txttistri.Text) + decimal.Parse(txttkd.Text) + decimal.Parse(txttpp.Text) +
                             decimal.Parse(txttberas.Text) + decimal.Parse(txttcacat.Text) + decimal.Parse(txtpembulatan.Text) + decimal.Parse(txttpm.Text) +
                            decimal.Parse(txttkhusus.Text) + decimal.Parse(txttkematian.Text) + decimal.Parse(txttpajak.Text);

                potongan = decimal.Parse(txtpotaskes.Text) + decimal.Parse(txtpotassos.Text) + decimal.Parse(txtpotpajak.Text) + decimal.Parse(txtpotlain.Text) +
                              decimal.Parse(txtpotsewarmh.Text) + decimal.Parse(txtpotkasda.Text);

                grandtotal = pengeluaran - potongan;
                txtgajibersih.Text = string.Format("{0:N2}", grandtotal);

            }
       
                pnl4.Visible = true;
                pnl3.Visible = false;

            
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

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
    }
    protected void btnbackedit_Click(object sender, EventArgs e)
    {
        pnl4.Visible = true; 
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try

        { 
            pnl1.Visible = false;
            pnl2.Visible = false;
            pnl3.Visible = false;
            pnl4.Visible = false;
            pnl5.Visible = false;
            PnlFront.Visible = true; 
            //btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[33];
            var[0] = gb.CreateSPParameter("@nopen", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnopen.Text.Trim(), typeof(string)); //
            var[1] = gb.CreateSPParameter("@idpermohonan", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var[2] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnip.Text, typeof(string)); //
            var[3] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text.Trim(), typeof(string)); //
            var[4] = gb.CreateSPParameter("@kota", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkota.Text, typeof(string)); //
            var[5] = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamat.Text, typeof(string)); //
            var[6] = gb.CreateSPParameter("@kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkelurahan.Text, typeof(string)); //
            var[7] = gb.CreateSPParameter("@kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkecamatan.Text.Trim(), typeof(string)); //
            var[8] = gb.CreateSPParameter("@kodepos", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkodepos.Text, typeof(string)); //
            var[9] = gb.CreateSPParameter("@Gapok", ParameterDirection.Input, SqlDbType.Decimal, 250, txtgapok.Text, typeof(string)); //
            var[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SIMPANEDITVERIF", typeof(string)); //
            var[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)); //
            var[12] = gb.CreateSPParameter("@flagedit", ParameterDirection.Input, SqlDbType.VarChar, 50, "2", typeof(string)); //
            var[13] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string)); //
            var[14] = gb.CreateSPParameter("@TunjIstri", ParameterDirection.Input, SqlDbType.Decimal, 250, txttistri.Text, typeof(string));
            var[15] = gb.CreateSPParameter("@TKD", ParameterDirection.Input, SqlDbType.Decimal, 250, txttkd.Text, typeof(string));
            var[16] = gb.CreateSPParameter("@TPP", ParameterDirection.Input, SqlDbType.Decimal, 250, txttpp.Text, typeof(string));
            var[17] = gb.CreateSPParameter("@TunjBeras", ParameterDirection.Input, SqlDbType.Decimal, 250, txttberas.Text, typeof(string));
            var[18] = gb.CreateSPParameter("@TunjCacat", ParameterDirection.Input, SqlDbType.Decimal, 250, txttcacat.Text, typeof(string));
            var[19] = gb.CreateSPParameter("@Pembulatan ", ParameterDirection.Input, SqlDbType.Decimal, 250, txtpembulatan.Text, typeof(string));
            var[20] = gb.CreateSPParameter("@TPM", ParameterDirection.Input, SqlDbType.Decimal, 250, txttpm.Text, typeof(string));
            var[21] = gb.CreateSPParameter("@TunjKhusus ", ParameterDirection.Input, SqlDbType.Decimal, 250, txttkhusus.Text, typeof(string));
            var[22] = gb.CreateSPParameter("@TunjKematian ", ParameterDirection.Input, SqlDbType.Decimal, 250, txttkematian.Text, typeof(string));
            var[23] = gb.CreateSPParameter("@TunjPajak", ParameterDirection.Input, SqlDbType.Decimal, 250, txttpajak.Text, typeof(string));
            var[24] = gb.CreateSPParameter("@GajiKotor", ParameterDirection.Input, SqlDbType.Decimal, 250, txtgajikotor.Text, typeof(string));
            var[25] = gb.CreateSPParameter("@PotAskes", ParameterDirection.Input, SqlDbType.Decimal, 250, txtpotaskes.Text, typeof(string));
            var[26] = gb.CreateSPParameter("@PotAssos", ParameterDirection.Input, SqlDbType.Decimal, 250, txtpotassos.Text, typeof(string));
            var[27] = gb.CreateSPParameter("@PotPajak", ParameterDirection.Input, SqlDbType.Decimal, 250, txtpotpajak.Text, typeof(string));
            var[28] = gb.CreateSPParameter("@PotLain", ParameterDirection.Input, SqlDbType.Decimal, 250, txtpotlain.Text, typeof(string));
            var[29] = gb.CreateSPParameter("@SewaRumah", ParameterDirection.Input, SqlDbType.Decimal, 250, txtpotsewarmh.Text, typeof(string));
            var[30] = gb.CreateSPParameter("@PotKasda", ParameterDirection.Input, SqlDbType.Decimal, 250, txtpotkasda.Text, typeof(string));
            var[31] = gb.CreateSPParameter("@TotPotongan", ParameterDirection.Input, SqlDbType.Decimal, 250, txttotal.Text, typeof(string));
            var[32] = gb.CreateSPParameter("@GajiBersih", ParameterDirection.Input, SqlDbType.Decimal, 250, txtgajibersih.Text, typeof(string));

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

    protected void txtgapok_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                      Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                      Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttistri.Focus();

    }
    protected void txttistri_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                      Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                      Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttkd.Focus();

    }
    protected void txttkd_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                      Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                      Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttpp.Focus();

    }

    protected void txttpp_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                     Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                     Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttberas.Focus();


    }

    protected void txttberas_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                     Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                     Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttcacat.Focus();

    }


    protected void txttcacat_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                      Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                      Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txtpembulatan.Focus();

    }
    protected void txtpembulatan_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                      Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                      Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttpm.Focus();

    }

    protected void txttpm_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                       Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                       Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttkhusus.Focus();

    }


    protected void txttkhusus_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                      Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                      Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttkematian.Focus();

    }
    protected void txttkematian_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                      Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                      Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txttpajak.Focus();

    }
    protected void txttpajak_TextChanged(object sender, EventArgs e)
    {
        double pengeluaran = 0, grandtotal = 0;
        pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                      Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                      Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

        txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
        txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

    }

    protected void txtpotaskes_TextChanged(object sender, EventArgs e)
    {
        double potongan = 0, grandtotal = 0;
        potongan = Convert.ToDouble(txtpotaskes.Text.Replace(".", "")) + Convert.ToDouble(txtpotassos.Text.Replace(".", "")) + Convert.ToDouble(txtpotpajak.Text.Replace(".", "")) + Convert.ToDouble(txtpotlain.Text.Replace(".", "")) +
                      Convert.ToDouble(txtpotsewarmh.Text.Replace(".", "")) + Convert.ToDouble(txtpotkasda.Text.Replace(".", ""));

        txttotal.Text = gb.ToRupiah(potongan.ToString());
        txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(txtgajikotor.Text) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txtpotassos.Focus();

    }

    protected void txtpotassos_TextChanged(object sender, EventArgs e)
    {
        double potongan = 0, grandtotal = 0;
        potongan = Convert.ToDouble(txtpotaskes.Text.Replace(".", "")) + Convert.ToDouble(txtpotassos.Text.Replace(".", "")) + Convert.ToDouble(txtpotpajak.Text.Replace(".", "")) + Convert.ToDouble(txtpotlain.Text.Replace(".", "")) +
                      Convert.ToDouble(txtpotsewarmh.Text.Replace(".", "")) + Convert.ToDouble(txtpotkasda.Text.Replace(".", ""));

        txttotal.Text = gb.ToRupiah(potongan.ToString());
        txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(txtgajikotor.Text) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txtpotpajak.Focus();
    }


    protected void txtpotpajak_TextChanged(object sender, EventArgs e)
    {
        double potongan = 0, grandtotal = 0;
        potongan = Convert.ToDouble(txtpotaskes.Text.Replace(".", "")) + Convert.ToDouble(txtpotassos.Text.Replace(".", "")) + Convert.ToDouble(txtpotpajak.Text.Replace(".", "")) + Convert.ToDouble(txtpotlain.Text.Replace(".", "")) +
                      Convert.ToDouble(txtpotsewarmh.Text.Replace(".", "")) + Convert.ToDouble(txtpotkasda.Text.Replace(".", ""));

        txttotal.Text = gb.ToRupiah(potongan.ToString());
        txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(txtgajikotor.Text) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txtpotlain.Focus();
    }


    protected void txtpotlain_TextChanged(object sender, EventArgs e)
    {
        double potongan = 0, grandtotal = 0;
        potongan = Convert.ToDouble(txtpotaskes.Text.Replace(".", "")) + Convert.ToDouble(txtpotassos.Text.Replace(".", "")) + Convert.ToDouble(txtpotpajak.Text.Replace(".", "")) + Convert.ToDouble(txtpotlain.Text.Replace(".", "")) +
                     Convert.ToDouble(txtpotsewarmh.Text.Replace(".", "")) + Convert.ToDouble(txtpotkasda.Text.Replace(".", ""));

        txttotal.Text = gb.ToRupiah(potongan.ToString());
        txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(txtgajikotor.Text) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txtpotsewarmh.Focus();
    }


    protected void txtpotsewarmh_TextChanged(object sender, EventArgs e)
    {
        double potongan = 0, grandtotal = 0;
        potongan = Convert.ToDouble(txtpotaskes.Text.Replace(".", "")) + Convert.ToDouble(txtpotassos.Text.Replace(".", "")) + Convert.ToDouble(txtpotpajak.Text.Replace(".", "")) + Convert.ToDouble(txtpotlain.Text.Replace(".", "")) +
                     Convert.ToDouble(txtpotsewarmh.Text.Replace(".", "")) + Convert.ToDouble(txtpotkasda.Text.Replace(".", ""));

        txttotal.Text = gb.ToRupiah(potongan.ToString());
        txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(txtgajikotor.Text) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

        txtpotkasda.Focus();
    }

    protected void txtpotkasda_TextChanged(object sender, EventArgs e)
    {
        double potongan = 0, grandtotal = 0;
        potongan = Convert.ToDouble(txtpotaskes.Text.Replace(".", "")) + Convert.ToDouble(txtpotassos.Text.Replace(".", "")) + Convert.ToDouble(txtpotpajak.Text.Replace(".", "")) + Convert.ToDouble(txtpotlain.Text.Replace(".", "")) +
                     Convert.ToDouble(txtpotsewarmh.Text.Replace(".", "")) + Convert.ToDouble(txtpotkasda.Text.Replace(".", ""));

        txttotal.Text = gb.ToRupiah(potongan.ToString());
        txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = Convert.ToDouble(txtgajikotor.Text) - Convert.ToDouble(txttotal.Text);
        txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
        txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", "");

    }
    protected void btnBackPendapatan_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditVerifPermohonan.aspx");
    }
     
    public void BindGridEntity()
    {


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "2", typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTEDITDATA", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id_permohonan asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        

    } 
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditVerifPermohonan.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditVerifPermohonan.aspx");
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnl1.Visible = true;

                    HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;

                    


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                    var[1] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
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
                    txtkota.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
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

                    txtgapok.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["GajiPokok"].ToString()).Replace("Rp", "");
                    txttistri.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjIstri"].ToString()).Replace("Rp", "");
                    txttkd.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TKD"].ToString()).Replace("Rp", "");
                    txttpp.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TPP"].ToString()).Replace("Rp", "");
                    txttberas.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjBeras"].ToString()).Replace("Rp", "");
                    txttcacat.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjCacat"].ToString()).Replace("Rp", "");
                    txtpembulatan.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["Pembulatan"].ToString()).Replace("Rp", "");
                    txttpm.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TPM"].ToString()).Replace("Rp", "");
                    txttkhusus.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjKhusus"].ToString()).Replace("Rp", "");
                    txttpajak.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjPajak"].ToString()).Replace("Rp", "");
                    txttkematian.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TunjKematian"].ToString()).Replace("Rp", "");
                    txtgajikotor.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["GajiKotor"].ToString()).Replace("Rp", "");
                    txtpotaskes.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotAskes"].ToString()).Replace("Rp", "");
                    txtpotassos.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotAssos"].ToString()).Replace("Rp", "");
                    txtpotpajak.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotPajak"].ToString()).Replace("Rp", "");
                    txtpotlain.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotLain"].ToString()).Replace("Rp", "");
                    txtpotkasda.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["PotKasda"].ToString()).Replace("Rp", "");
                    txttotal.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["TotPotongan"].ToString()).Replace("Rp", "");
                    txtgajibersih.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["GajiBersih"].ToString()).Replace("Rp", "");
                    txtpotsewarmh.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["SewaRumah"].ToString()).Replace("Rp", "");

                    //Panel 4

                    txtkdproduk.Text = txtkodeproduk.Text;
                    txtprogpinj.Text = txtprogrampinjaman.Text;
                    ddlsumberdana.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kdpersh2"].ToString();
                    txtplafon.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()).Replace("Rp", "");
                    txtbendahara.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()).Replace("Rp", "");
                    txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                    txtpersengaji.Text = ge.ListDataset.Tables[0].Rows[0]["persen_gaji"].ToString();
                    txtusia.Text = ge.ListDataset.Tables[0].Rows[0]["usia"].ToString();
                    txtTO.Text = ge.ListDataset.Tables[0].Rows[0]["status_to"].ToString();
                    ddlPenggunaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_pengguna"].ToString();
                    txttglrealisasi.Text = txtrealisasi.Text;
                    txttgljt.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_jatuhtempo"].ToString();
                    txtProvisi.Text = ge.ListDataset.Tables[0].Rows[0]["persenprv"].ToString();
                    txtktrbyr.Text = ge.ListDataset.Tables[0].Rows[0]["ktrbyr"].ToString();
                    ddlBankNasabah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["BankRekening"].ToString();
                    txtrekeningnamadeb.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekening"].ToString();
                    txtjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                    txtangsurandimuka.Text = ge.ListDataset.Tables[0].Rows[0]["angsuran_dimuka"].ToString();
                    txtbyangsurandimuka.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_angsurandimuka"].ToString()).Replace("Rp", "");
                    txtbyto.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_takeover"].ToString()).Replace("Rp", "");
                    txtangsuranperbulan.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_angsuranperbulan"].ToString()).Replace("Rp", "");
                    txtbaserate.Text = ge.ListDataset.Tables[0].Rows[0]["baserate"].ToString();
                    txtbgc.Text = ge.ListDataset.Tables[0].Rows[0]["rateasu"].ToString();
                    txtspread.Text = ge.ListDataset.Tables[0].Rows[0]["spread"].ToString();
                    txtbungas.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    txtbypanjar.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_panjar"].ToString()).Replace("Rp", "");
                    txtadmiasuransi.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_admasuransi"].ToString()).Replace("Rp", "");
                    txtadmprovisi.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_admprovisi"].ToString()).Replace("Rp", "");
                    txtadmpencairan.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_admpencairan"].ToString()).Replace("Rp", "");
                    txtadmangsuran.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_adminangsuran"].ToString()).Replace("Rp", "");
                    txttaksasi.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()).Replace("Rp", "");
                    txtangsblokir.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["blokir_angsuran"].ToString()).Replace("Rp", "");
                    TxtAo1.Text = txtAO.Text;
                    txtnonao1.Text = txtnonAO.Text;
                    txtalasanpeminjaman.Text = ge.ListDataset.Tables[0].Rows[0]["input_pembiayaan"].ToString();
                    txtrekeningbankdeb.Text = ge.ListDataset.Tables[0].Rows[0]["Norekening"].ToString();
                    txtstatussk.Text = ge.ListDataset.Tables[0].Rows[0]["sk"].ToString();
                    txtpathsk.Text = ge.ListDataset.Tables[0].Rows[0]["path_sk"].ToString();
                    TextBox43.Text = ge.ListDataset.Tables[0].Rows[0]["jenis_usaha"].ToString();
                    TextBox44.Text = ge.ListDataset.Tables[0].Rows[0]["lokasi_usaha"].ToString();
                    TextBox45.Text = ge.ListDataset.Tables[0].Rows[0]["jumlah_tk"].ToString();
                    TextBox46.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["pendapatan_usaha"].ToString()).Replace("Rp", "");
                    TextBox47.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_tk"].ToString()).Replace("Rp", "");
                    TextBox48.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["by_produksi"].ToString()).Replace("Rp", "");

                    //panel 5 
                    txtpenerimapensiun.Text = ge.ListDataset.Tables[0].Rows[0]["penerimapens"].ToString();
                    txtPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penerima_pensiun"].ToString();
                    TextBox1.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir_penerima"].ToString();
                    txtstatuskwn.Text = ge.ListDataset.Tables[0].Rows[0]["sts_penerima"].ToString();
                    TextBox2.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_penerima_pensiun"].ToString();
                    TextBox3.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_penerima_pensiun"].ToString();
                    TextBox4.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_penerima_pensiun"].ToString();
                    txtkotapekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kota_penerima_pensiun"].ToString();
                    txtkodepospekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_penerima_pensiun"].ToString();
                    txttlp.Text = ge.ListDataset.Tables[0].Rows[0]["telepon_penerima_pensiun"].ToString();
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

                    

                    double potongan = 0, grandtotal = 0, pengeluaran = 0;
                    pengeluaran = Convert.ToDouble(txtgapok.Text.Replace(".", "")) + Convert.ToDouble(txttistri.Text.Replace(".", "")) + Convert.ToDouble(txttkd.Text.Replace(".", "")) + Convert.ToDouble(txttpp.Text.Replace(".", "")) +
                                 Convert.ToDouble(txttberas.Text.Replace(".", "")) + Convert.ToDouble(txttcacat.Text.Replace(".", "")) + Convert.ToDouble(txtpembulatan.Text.Replace(".", "")) + Convert.ToDouble(txttpm.Text.Replace(".", "")) +
                                 Convert.ToDouble(txttkhusus.Text.Replace(".", "")) + Convert.ToDouble(txttkematian.Text.Replace(".", "")) + Convert.ToDouble(txttpajak.Text.Replace(".", ""));

                    potongan = Convert.ToDouble(txtpotaskes.Text.Replace(".", "")) + Convert.ToDouble(txtpotassos.Text.Replace(".", "")) + Convert.ToDouble(txtpotpajak.Text.Replace(".", "")) + Convert.ToDouble(txtpotlain.Text.Replace(".", "")) +
                                 Convert.ToDouble(txtpotsewarmh.Text.Replace(".", "")) + Convert.ToDouble(txtpotkasda.Text.Replace(".", ""));

                    txtgajikotor.Text = gb.ToRupiah(pengeluaran.ToString());
                    txtgajikotor.Text = txtgajikotor.Text.Replace("Rp", "");
                     
                    txttotal.Text = gb.ToRupiah(potongan.ToString());
                    txttotal.Text = txttotal.Text.Replace("Rp", "");

                    grandtotal = Convert.ToDouble(pengeluaran.ToString()) - Convert.ToDouble(txttotal.Text);
                    txtgajibersih.Text = gb.ToRupiah(grandtotal.ToString());
                    txtgajibersih.Text = txtgajibersih.Text.Replace("Rp", ""); 
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