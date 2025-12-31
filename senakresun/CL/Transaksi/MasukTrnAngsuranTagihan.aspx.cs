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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Services;


public partial class CL_Process_MasukTrnAngsuranTagihan : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    DataTable dtBayar = new DataTable("TransactionBayar");

    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    string form = "B3";
    string kodesumber = "CL";
    string KodeKonter = "MK03";
    public string nobuktidb;
   

    protected void txtangssd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int angsd = 0;
        int angske = 0;int jangka; decimal bykangskex = 0;
        angske = int.Parse(txtangsuranke.Text);
        angsd = int.Parse(txtangsuransd.Text);
        jangka = int.Parse(hdnjangka.Value);
        hdncustomer.Value = txtkodelgn.Text;
        HDNoRekening.Value = txtrekening.Text;
        bykangskex = (angsd- angske) + 1;


        if (angsd < angske)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Angke sd tidak boleh lebih kecil dari angske!');", true);
            txtangsuransd.Text = txtangsuranke.Text;
            angske = int.Parse(txtangsuranke.Text);
            angsd = int.Parse(txtangsuransd.Text);
            bykangskex = (angsd - angske) + 1;
            txtbayar.Text = string.Format("{0:N0}", (decimal.Parse(txtdendabyr.Text) * bykangskex) + (decimal.Parse(txtpokok.Text) * bykangskex) + (decimal.Parse(txtbunga.Text) * bykangskex)); //ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();
            txtangsuransd.Focus();
        }
        else if (angsd > jangka)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Angke sd tidak boleh lebih dari dari maksimal jangka !');", true);
            txtangsuransd.Text = jangka.ToString();
            angske = int.Parse(txtangsuranke.Text);
            angsd = int.Parse(txtangsuransd.Text);
            bykangskex = (angsd - angske) + 1;
            txtbayar.Text = string.Format("{0:N0}", (decimal.Parse(txtdendabyr.Text) * bykangskex) + (decimal.Parse(txtpokok.Text) * bykangskex) + (decimal.Parse(txtbunga.Text) * bykangskex)); //ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();

            txtangsuransd.Focus();
        }
        else
        {


            var kodelgn = hdncustomer.Value;
            string norekening = HDNoRekening.Value;

            txtbayar.Text = string.Format("{0:N0}", (decimal.Parse(txtdendabyr.Text) * bykangskex) + (decimal.Parse(txtpokok.Text)  * bykangskex)  + (decimal.Parse(txtbunga.Text) * bykangskex)); //ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();
            btnSimpan.Visible = true;
            txtnobuktimanual.Focus();
            


        }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }
    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {
        decimal dendabyr = 0;
        decimal denda = 0;
        dendabyr = decimal.Parse(txtdendabyr.Text);
        denda = decimal.Parse(txtdenda.Text);

        if (dendabyr > 0)
        {
            if (dendabyr != denda)
            {
                txtdendabyr.Text = txtdenda.Text;
                txtbayar.Text = string.Format("{0:N0}", decimal.Parse(txtdendabyr.Text) + decimal.Parse(txtpokok.Text) + decimal.Parse(txtbunga.Text));
            }
            else
            {
                txtbayar.Text = string.Format("{0:N0}", decimal.Parse(txtdendabyr.Text) + decimal.Parse(txtpokok.Text) + decimal.Parse(txtbunga.Text));
            }

        }
        else
        {
            txtbayar.Text = string.Format("{0:N0}", decimal.Parse(txtdendabyr.Text) + decimal.Parse(txtpokok.Text) + decimal.Parse(txtbunga.Text));
        }

           
            
        //decimal saldoawal;
        //saldoawal = decimal.Parse(txtkelipatan.Text) * decimal.Parse(txtsaldowajib.Text);
        //txtbayar.Text = string.Format("{0:N2}", saldoawal);
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
                //BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");


                txtcustomer.Focus();
                btnSimpan.Visible = false;
            }
			txtangsuransd.Focus();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
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



    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txtbayar.Text);
            int angsd = 0;
            int angske = 0; int jangka;
            angske = int.Parse(txtangsuranke.Text);
            angsd = int.Parse(txtangsuransd.Text);
            jangka = int.Parse(hdnjangka.Value);
            btnSimpan.Enabled = false;


            decimal bykangskex = 0;
            bykangskex = (angsd - angske) + 1;




            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                btnSimpan.Enabled = true;
            }
            else
            {



                if (jlhdibayar <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Bayar Masih Kosong!');", true);
                    btnSimpan.Enabled = true;

                }
                else if (txtrekening.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Rekening Masih Kosong, Cek Kembali penginputannya');", true);
                    btnSimpan.Enabled = true;

                }
                else if (txtnobuktimanual.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Bukti Manual Masih Kosong, Cek Kembali penginputannya');", true);
                    btnSimpan.Enabled = true;
                }
                else
                {
                    hdncustomer.Value = txtkodelgn.Text;
                    HDNoRekening.Value = txtrekening.Text;

                    txtdendabyr.Text = string.Format("{0:N0}", decimal.Parse(txtdendabyr.Text) * bykangskex);
                    txtpokok.Text = string.Format("{0:N0}", decimal.Parse(txtpokok.Text) * bykangskex);
                    txtbunga.Text = string.Format("{0:N0}", decimal.Parse(txtbunga.Text) * bykangskex);

                    //txtangssd_TextChanged(sender, e);

                    btnSimpan.Visible = false;
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[19];
                    Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                    Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                    Params[3] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    Params[4] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                    Params[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[6] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[9] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[10] = gb.CreateSPParameter("@nobukti_manual", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobuktimanual.Text, typeof(string));
                    Params[11] = gb.CreateSPParameter("@angske", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtangsuranke.Text), typeof(int));
                    Params[12] = gb.CreateSPParameter("@angssd", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtangsuransd.Text), typeof(int));
                    Params[13] = gb.CreateSPParameter("@dendabyr", ParameterDirection.Input, SqlDbType.Decimal, 0, txtdendabyr.Text, typeof(decimal));
                    Params[14] = gb.CreateSPParameter("@denda", ParameterDirection.Input, SqlDbType.Decimal, 0, txtdenda.Text, typeof(decimal));
                    Params[15] = gb.CreateSPParameter("@pokok", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpokok.Text, typeof(decimal));
                    Params[16] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbunga.Text, typeof(decimal));
                    Params[17] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayar.Text, typeof(decimal));
                    Params[18] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdBank.SelectedValue, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Insert_Bayar_Angsuran_Setoran_001", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Save Success');", true);

                        txtcustomer.Text = "";
                        txtrekening.Text = "";
                        txtkodelgn.Text = "";
                        txtNmSup.Text = "";
                        txtnobuktimanual.Text = "";
                        txtangsuranke.Text = "0";
                        txtangsuransd.Text = "0";
                        hdnjangka.Value = "0";
                        txtdendabyr.Text = string.Format("{0:N0}", "0");
                        txtpokok.Text = string.Format("{0:N0}", "0"); //;
                        txtbunga.Text = string.Format("{0:N0}", "0"); //ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                        txtdenda.Text = string.Format("{0:N0}", "0"); //ge.ListDataset.Tables[0].Rows[0]["denda"].ToString();
                        txtangsuranke.ReadOnly = true;
                        txtbayar.Text = string.Format("{0:N0}", "0"); //ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();
                        btnSimpan.Visible = false;
                        btnSimpan.Enabled = true;
                        txtcustomer.Focus();
                    }

                    else
                    {
                        btnSimpan.Visible = true;
                        btnSimpan.Enabled = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }



                }





            }
        
        
        }

        catch (Exception X)
        {
            btnSimpan.Visible = true;
            btnSimpan.Enabled = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }



    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
        }
    }

    protected void txtsearch_TextChanged(object sender, System.EventArgs e)
    {
        try
        {


            var kodelgn = hdncustomer.Value;

            if (HDNoRekening.Value == "")
            {
                HDNoRekening.Value = txtcustomer.Text;

            }

            string norekening = HDNoRekening.Value;
            txtrekening.Text = norekening;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            //var[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[4] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_cek_anggota", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)


            {


                hdncustomer.Value = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                kodelgn = hdncustomer.Value;
                txtkodelgn.Text = kodelgn;
                hdnkodeproduk.Value = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();

                txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                txtangsuranke.Text = ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();
                txtangsuranke.ReadOnly = true;
                txtangsuransd.Text = ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();
                hdnjangka.Value = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                txtdendabyr.Text = string.Format("{0:N0}", "0");
                //hdnkodeproduk.Value = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                //txtdendabyr.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["denda"].ToString()));
                txtpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString())); //;
                txtbunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                txtdenda.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["denda"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["denda"].ToString();

                txtbayar.Text = string.Format("{0:N0}", decimal.Parse(txtdendabyr.Text) + decimal.Parse(txtpokok.Text) + decimal.Parse(txtbunga.Text)); //ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();
                btnSimpan.Visible = true;
                btnSimpan.Enabled = true;
                txtangsuransd.Focus();



            }

            else
            {


                hdncustomer.Value = "";
                HDNoRekening.Value = "";
                txtrekening.Text = "";
                txtkodelgn.Text = "";
                txtNmSup.Text = "";
                txtangsuranke.Text = "0";
                txtangsuransd.Text = "0";
                hdnjangka.Value = "0";
                //hdnkodeproduk.Value = "";
                txtdendabyr.Text = string.Format("{0:N0}", "0");
                txtpokok.Text = string.Format("{0:N0}", "0"); //;
                txtbunga.Text = string.Format("{0:N0}", "0"); //ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                txtdenda.Text = string.Format("{0:N0}", "0"); //ge.ListDataset.Tables[0].Rows[0]["denda"].ToString();
                txtangsuranke.ReadOnly = true;
                txtbayar.Text = string.Format("{0:N0}", "0"); //ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();
                btnSimpan.Visible = false;
                txtcustomer.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon Maaf Data tidak ditemukan..cek kembali!');", true);

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_getCustomer_angsuran_setoran", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }






}
