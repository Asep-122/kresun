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
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;
using System.Text;

public partial class FA_Process_TransaksiPerawatan : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }
    private string SessIsSuperAdmin
    {
        get { return Session["SessIsSuperAdmin"].ToString(); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }

    DataTable dtRP = new DataTable("Transaction");
    DataTable dtRPDetil = new DataTable("Detil");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gb1 = new Global();
    GlobalEntities ge1 = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();

    string statloop = "";
    string nosek = "";
    long nosekarang = 0;
    string formatnumber = "";
    string nosebelumnya = "";
    long nonasabah = 0;
    string CRNonasabah = "";
    string updateno = "";
    string kodekonter = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnSimpan.Enabled = true;
            btnSimpan.Visible = true;
            if (!Page.IsPostBack)
            {
                //culture
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";

                BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttglTransaksi.Text = xtanggal.ToString("yyyy-MM-dd");
                //txttglakhirsusut.Text = xtanggal.ToString("dd-MM-yyyy");
                //get tanggal server

                var KdCompany = SessKdpersh;
                var admin = SessLoginID;


                //DROPDOWNLIST Kode Aktiva
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTALLKODE", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAktiva asc";

                ddlkodeaktiva.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkodeaktiva.DataTextField = "KodeAktiva";
                ddlkodeaktiva.DataValueField = "KodeAktiva";
                ddlkodeaktiva.DataBind();
                ddlkodeaktiva.Items.Insert(0, new ListItem("Pilih Data", "-"));

               

                //DROPDOWNLIST Type Transaksi
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANS", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var3, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn asc";

                ddltypetrans.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddltypetrans.DataTextField = "TypeTrans";
                ddltypetrans.DataValueField = "TypeTrn";
                ddltypetrans.DataBind();
                ddltypetrans.SelectedValue = "E";
                txttypetrans.Text = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();
                               
                //DROPDOWNLIST JURNAL
                DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[3];
                var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEJURNAL", typeof(string));
                var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var6[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var6, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";

                ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlJurnal.DataTextField = "TEXT";
                ddlJurnal.DataValueField = "VALUE";
                ddlJurnal.DataBind();
                ddlJurnal.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //DROPDOWNLIST CURRENCY
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[3];
                var5[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCRC", typeof(string));
                var5[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var5[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var5, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";

                ddlCurrency.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlCurrency.DataTextField = "Currency";
                ddlCurrency.DataValueField = "KodeCrc";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, new ListItem("Pilih Data", "-"));
                txtnegara.Text = ge.ListDataset.Tables[0].Rows[0]["NamaCrc"].ToString();

                if (SessSystemKurs == "False")
                {
                    ddlCurrency.SelectedValue = "IDR";
                    ddlkurs.Items.Insert(0, new ListItem("1"));
                    txtKurs.Text = "1";
                    ddlCurrency.Enabled = false;
                    ddlkurs.Enabled = false;
                    txtKurs.Enabled = false;
                    

                }
                else
                {
                    ddlCurrency.SelectedValue = "IDR";
                    ddlCurrency.Enabled = true;
                    ddlkurs.Items.Insert(0, new ListItem("1"));
                    txtKurs.Text = "1";
                    ddlkurs.Enabled = true;
                    txtKurs.Enabled = true;
                    ddlCurrency.Enabled = true;

                }
            }
            txtketerangan.Attributes["maxlength"] = "150";

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

            //string JmlhRupiah = txtjmlrp.Text;
            //txtjmlrp.Text = hfjmlrp.Value == "" ? "0" : hfjmlrp.Value; 
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];

            var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
            var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));
            var1[4] = gb.CreateSPParameter("@statusadmin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessIsSuperAdmin, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var1, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";

            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (rbJurnal.SelectedValue == "1")
        {
            FileJurnal.Visible = true;
            Pnlfooter.Visible = true;
            PnlJournal.Visible = false;
        }
        else if (rbJurnal.SelectedValue == "2")
        {
            FileJurnal.Visible = false;
            Pnlfooter.Visible = false;
            PnlJournal.Visible = true;
        }
        else
        {
            FileJurnal.Visible = false;
            Pnlfooter.Visible = true;
            PnlJournal.Visible = false;
        }
    }
    protected void ddlkodeaktiva_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlkodeaktiva.SelectedValue != "-")
            {
                //TEXTBOX AUTO NAMA SUPPLIER
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
                varT[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkodeaktiva.SelectedValue, typeof(string));
                varT[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varT[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYKODE", typeof(string));
                varT[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //varT[4] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varT[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("Fa_Perawatan", varT, "constrmf");

                txtnamaaktiva.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAktiva"].ToString();
                txtnoBukti.Text = ge.ListDataset.Tables[0].Rows[0]["NobuktiTransaksi"].ToString();
                txttglTransaksi.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString()).ToString("dd-MM-yyyy");
                txttglmulaisusut.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglMulaiSusut"].ToString()).ToString("dd-MM-yyyy");
                txttglakhirsusut.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglAkhirSusut"].ToString()).ToString("dd-MM-yyyy");
                txtnomorekonomis.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBulan"].ToString();
                txtmasamanfaat.Text = ge.ListDataset.Tables[0].Rows[0]["NamaTahun"].ToString();
                txthargaperolehan.Text = ge.ListDataset.Tables[0].Rows[0]["JlhPerolehan"].ToString();
                txtnilairesidu.Text = ge.ListDataset.Tables[0].Rows[0]["JlhResidu"].ToString();
                txtketerangan.Text = txttypetrans.Text;
                //txtketerangan.MaxLength = 150;
                txthargapembelian.Text = ge.ListDataset.Tables[0].Rows[0]["HargaBeli"].ToString();
                txtakumulasisusut.Text = ge.ListDataset.Tables[0].Rows[0]["HargaAkumulasi"].ToString();
                txtpenambahanharga.Text = ge.ListDataset.Tables[0].Rows[0]["HargaTambah"].ToString();
                txtpenguranganharga.Text = ge.ListDataset.Tables[0].Rows[0]["HargaKurang"].ToString();
                decimal a, b, c, d, f, g, h;
                a = Convert.ToDecimal(txthargaperolehan.Text.ToString());
                txthargaperolehan.Text = string.Format("{0:N2}", a);
                b = Convert.ToDecimal(txthargapembelian.Text.ToString());
                txthargapembelian.Text = string.Format("{0:N2}", b);
                c = Convert.ToDecimal(txtnilairesidu.Text.ToString());
                txtnilairesidu.Text = string.Format("{0:N2}", c);
                d = Convert.ToDecimal(txtakumulasisusut.Text.ToString());
                txtakumulasisusut.Text = string.Format("{0:N2}", d);
                f = Convert.ToDecimal(txtpenambahanharga.Text.ToString());
                txtpenambahanharga.Text = string.Format("{0:N2}", f);
                g = Convert.ToDecimal(txtpenguranganharga.Text.ToString());
                txtpenguranganharga.Text = string.Format("{0:N2}", g);
                h = Convert.ToDecimal("0");
                txtjmlbiaya.Text = string.Format("{0:N2}", h);
                //txtnamaaktiva.Text = SupplierName;
                //hdfKodeSales.Value = kdSupplier;
                //ddlCurrency.SelectedValue = kdcrc;
                //txtnegara.Text = nmcrc;
                //if(ddlCurrency.SelectedValue == "IDR")
                //{
                //    txtKurs.Text = "1";
                //}
                var kdlgn = ddlkodeaktiva.SelectedValue;

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                var1[0] = gb.CreateSPParameter("@KetKonter", ParameterDirection.Input, SqlDbType.VarChar, 40, kdlgn, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATESYARATBAYAR", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("[AP_Transaksi]", var1, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count != 0)
                {
                    string SyaratBayar = ge.ListDataset.Tables[0].Rows[0]["SyaratBayar"].ToString();

                    var ket = SyaratBayar.Substring(0, 1);
                    var dateMonth = SyaratBayar.Substring(1);
                    DateTime adddays;
                    DateTime addmonth;
                    if (ket == "D")
                    {
                        adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(double.Parse(dateMonth));
                        txttglmulaisusut.Text = adddays.ToString("dd-MM-yyyy");
                    }
                    else if (ket == "M")
                    {
                        addmonth = DateTime.Parse(txttglTransaksi.Text).AddMonths(int.Parse(dateMonth));
                        txttglmulaisusut.Text = addmonth.ToString("dd-MM-yyyy");
                    }
                    else if (ket == "W")
                    {
                        int x, y, z;
                        x = int.Parse(dateMonth);
                        y = 7;
                        z = x * y;

                        var jmlhtgl = Convert.ToString(z);

                        adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(int.Parse(jmlhtgl));
                        txttglmulaisusut.Text = adddays.ToString("dd-MM-yyyy");
                    }
                }
                try
                {
                    //if (SessSystemKurs != "False")
                    //{

                    //    if (ddlCurrency.SelectedValue == "USD")
                    //    {

                    //        var tanggal = txttglTransaksi.Text;
                    //        DefaultClass.Global.SqlHelper[] varCrc1 = new DefaultClass.Global.SqlHelper[5];
                    //        varCrc1[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Parse(txttglTransaksi.Text).ToString("yyyy-MM-dd"), typeof(string));
                    //        varCrc1[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlCurrency.SelectedValue, typeof(string));
                    //        varCrc1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYUSDVALUE", typeof(string));
                    //        varCrc1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)); 
                    //        varCrc1[4] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    //        ge.ListDataset = gb.GetDataJustOutPut("AP_DDLTransaksi", varCrc1, "constrmf");

                    //        txtKurs.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["KursStd"].ToString()));

                    //        decimal j, k, z;
                    //        j = decimal.Parse(txtKurs.Text);
                    //        k = decimal.Parse(txtjmltrans.Text);
                    //        z = j * k;
                    //        string formatted = string.Format("{0:N2}", z);
                    //        txtjmlrp.Text = formatted;
                    //    }
                    //    else
                    //    {
                    //        decimal j, k, z;
                    //        j = decimal.Parse(txtKurs.Text);
                    //        k = decimal.Parse(txtjmltrans.Text);
                    //        z = j * k;
                    //        string formatted = string.Format("{0:N2}", z);
                    //        txtjmlrp.Text = formatted;
                    //    }
                    //}
                    //else
                    //{
                    //    decimal j, k, z;
                    //    j = decimal.Parse(txtKurs.Text);
                    //    k = decimal.Parse(txtjmltrans.Text);
                    //    z = j * k;
                    //    string formatted = string.Format("{0:N2}", z);
                    //    txtjmlrp.Text = formatted;
                    //}

                }
                catch (Exception X)
                {
                    //if (ddlCurrency.SelectedValue == "IDR")
                    //{
                    //    txtKurs.Text = "1";
                    //}
                    //else
                    //{
                    //    txtKurs.Text = "0";
                    //}
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Nilai Kurs tidak ada. Silahkan diisi','top-right','success','2');", true); 
                }

                try
                {
                    DefaultClass.Global.SqlHelper[] varCrc = new DefaultClass.Global.SqlHelper[5];
                    varCrc[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglTransaksi.Text, typeof(string));
                    //varCrc[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlCurrency.SelectedValue, typeof(string));
                    varCrc[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYUSDVALUE", typeof(string));
                    varCrc[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varCrc[4] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AP_DDLTransaksi", varCrc, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";


                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        //ddlkurs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                        //ddlkurs.DataTextField = "statuskurs";
                        //ddlkurs.DataValueField = "KursStd";
                        //ddlkurs.DataBind();
                        //ddlkurs.Items.Insert(0, new ListItem("Pilih Data", "-"));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Kurs Tidak Ditemukan, silahkan isi Manual atau update tanggal di table UmCrcDtl');", true);
                    }

                }
                catch
                {

                    //if (ddlCurrency.SelectedValue == "IDR")
                    //{
                    //    ddlkurs.Items.Insert(0, new ListItem("1")); 
                    //}
                    //else
                    //{
                    //    ddlkurs.Items.Insert(0, new ListItem("0")); 
                    //}
                }
            }
        else
            {
                txtnamaaktiva.Text = "-";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        
    }
    protected void ddlNoBukti_SelectedIndexChanged(object sender, EventArgs e)
    {
        //var slcWil = ddlNoBukti.SelectedValue; 
    }
    protected void ddlTypTrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        var Typetrn = ddltypetrans.SelectedValue;

        if(Typetrn != "-")
        { 

            //TEXTBOX AUTO TYPE TRANSAKSI KETERANGAN
            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[6];
            varT[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, Typetrn, typeof(string));
            varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANS", typeof(string));
            varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", varT, "constrmf");

            string KetTypeTransKet = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();
            txttypetrans.Text = KetTypeTransKet;
            txtketerangan.Text = KetTypeTransKet;
            //txtKet.Text = KetTypeTransKet;

            //if(Typetrn == "O" || Typetrn == "P" || Typetrn == "Q" || Typetrn =="S" )
            //{
            //    txtjmltrans.Text = "-";
            //}

            if (Typetrn == "A")
            {
                rbJurnal.Enabled = false;
                rbJurnal.Visible = false;
                typejur.Visible = false;
            }
            else
            { 
                rbJurnal.Enabled = true;
                rbJurnal.Visible = true;
                typejur.Visible = true;
            }
        }
        else
        {
            txttypetrans.Text = "-";
        }

    } 
    protected void ddlcurrency_SelectedIndexChanged(object sender, EventArgs e)
    {

        //ddlkurs.Items.Clear();
        //string cur = ddlCurrency.SelectedValue;
        ////TEXTBOX AUTO CURRENCY
        //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[7];
        //varT[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, cur, typeof(string));
        //varT[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //varT[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //varT[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //varT[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYKET", typeof(string));
        //varT[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //varT[6] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("AP_DDLTransaksi", varT, "constrmf");

        //string CurrencyKet = ge.ListDataset.Tables[0].Rows[0]["NamaCrc"].ToString();
        //string Cur = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
        //txtnegara.Text = CurrencyKet;
        //if (ddlCurrency.SelectedValue == "IDR")
        //{
        //    txtKurs.Text = "1";
        //    if (txtjmltrans.Text == "")
        //    {
        //        txtjmlrp.Text = "0";

        //    }
        //    else
        //    {
        //        //Int64 x, y, z;
        //        //x = Int64.Parse(txtKurs.Text);
        //        //y = Int64.Parse(txtjmltrans.Text);
        //        //z = x * y;
        //        //var jmlrp = Convert.ToString(z);
        //        //txtjmlrp.Text = jmlrp;

        //        decimal j, k, z;
        //        j = decimal.Parse(txtKurs.Text);
        //        k = decimal.Parse(txtjmltrans.Text);
        //        z = j * k;
        //        string formatted = string.Format("{0:N2}", z);
        //        txtjmlrp.Text = formatted;
        //    }
        //}
        //else
        //{
        //    try
        //    {

        //        var tanggal = txttglTransaksi.Text;

        //        DefaultClass.Global.SqlHelper[] varCrc = new DefaultClass.Global.SqlHelper[5];
        //        varCrc[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Parse(txttglTransaksi.Text).ToString("yyyy-MM-dd"), typeof(string));
        //        varCrc[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlCurrency.SelectedValue, typeof(string));
        //        varCrc[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYUSDVALUE", typeof(string));
        //        varCrc[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)); 
        //        varCrc[4] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        //        ge.ListDataset = gb.GetDataJustOutPut("AP_DDLTransaksi", varCrc, "constrmf");

        //        txtKurs.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["KursStd"].ToString()));

        //        //decimal j, k, z;
        //        //j = decimal.Parse(txtKurs.Text);
        //        //k = decimal.Parse(txtjmltrans.Text);
        //        //z = j * k;
        //        //string formatted = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["KursStd"].ToString()));
        //        //txtjmlrp.Text = formatted;

        //        //if (crc != "0")
        //        //{
        //        //    txtKurs.Text = crc.Replace(".0000","").Replace(",0000", "");
        //        //}
        //        if (txtjmltrans.Text =="")
        //        {
        //            txtjmlrp.Text = "0";

        //        }
        //        else
        //        {
        //            //Int64 x, y, z;
        //            //x = Int64.Parse(txtKurs.Text);
        //            //y = Int64.Parse(txtjmltrans.Text);
        //            //z = x * y;
        //            //var jmlrp = Convert.ToString(z);
        //            //txtjmlrp.Text = jmlrp;
        //            decimal j, k, z;
        //            j = decimal.Parse(txtKurs.Text);
        //            k = decimal.Parse(txtjmltrans.Text);
        //            z = j * k;
        //            string formatted = string.Format("{0:N2}", z);
        //            txtjmlrp.Text = formatted;



        //        }
        //    }
        //    catch (Exception X)
        //    {
        //        txtKurs.Text = "0";
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Nilai Kurs tidak ada. Silahkan diisi','top-right','success','2');", true); 
        //    }


        //}
        //DefaultClass.Global.SqlHelper[] varCrc1 = new DefaultClass.Global.SqlHelper[5];
        //varCrc1[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Parse(txttglTransaksi.Text).ToString("yyyy-MM-dd"), typeof(string));
        //varCrc1[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlCurrency.SelectedValue, typeof(string));
        //varCrc1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYUSDVALUE", typeof(string));
        //varCrc1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //varCrc1[4] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("AP_DDLTransaksi", varCrc1, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";


        //if (ge.ListDataset.Tables[0].Rows.Count != 0)
        //{
        //    ddlkurs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //    ddlkurs.DataTextField = "statuskurs";
        //    ddlkurs.DataValueField = "KursStd";
        //    ddlkurs.DataBind();
        //    ddlkurs.Items.Insert(0, new ListItem("Pilih Data", "-"));
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Kurs Tidak Ditemukan, silahkan isi Manual atau update tanggal di table UmCrcDtl');", true);
        //}


    }
    protected void ddlJurnal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJurnal.SelectedValue == "-")
        {
            txtJurnal.Text = "";
            txtDebit1.Text = "";
            txtDebit2.Text = "";
            txtKredit1.Text = "";
            txtKredit2.Text = "";
            txtDebit3.Text = "";
            txtKredit3.Text = "";
        }
        else
        {
			string Jur = ddlJurnal.SelectedValue;
			//TEXTBOX AUTO CURRENCY
			DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[4];
			var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYJURNAL", typeof(string));
			var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
			var6[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
			var6[3] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJurnal.SelectedValue, typeof(string));
	
			ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var6, "constrmf");
	
			string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["Nama"].ToString();
			string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
			string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
			string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
			string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
			txtJurnal.Text = KetJurnal;
			txtDebit1.Text = DebitGL;
			txtDebit2.Text = DebitCC;
			txtKredit1.Text = KreditGL;
			txtKredit2.Text = KreditCC;
			txtDebit3.Text = KetJurnal;
			txtKredit3.Text = KetJurnal;

		}
    }
    protected void ddlKodeGL_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            decimal GTotaldebit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                // String total = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                GTotaldebit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            }
            decimal GTotalkredit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                // String total = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                GTotalkredit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
            }
            if (GTotalkredit > GTotaldebit || GTotalkredit < GTotaldebit || GTotalkredit != Convert.ToDecimal(txtjmlbiaya.Text) || GTotaldebit != Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = false;
            }
            else if (GTotaldebit == GTotalkredit && GTotaldebit == Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = true;
            }
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
            Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            Params[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));
            ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_SGetCoacode", Params, "constrmf");



            ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";


            gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlnamaGL")), "-", "-");

            DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
            xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
            xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            xParams[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");


            //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");

            ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue;



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("FA_Perawatan", var, "constrmf").Tables[0];
            gbs.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "-", "-");

            string CC = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();


            if (CC == "True")
            {
                ((DropDownList)grow.FindControl("ddlcostcenter")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ((DropDownList)grow.FindControl("ddlcostcenter")).DataTextField = "Text";
                ((DropDownList)grow.FindControl("ddlcostcenter")).DataValueField = "Value";
                ((DropDownList)grow.FindControl("ddlcostcenter")).DataBind();
                ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = true;
                //((DropDownList)grow.FindControl("ddlcostcenter")).SelectedValue = CC;
            }
            else
            {
                ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = false;
                ((DropDownList)grow.FindControl("ddlcostcenter")).Items.Insert(0, new ListItem("-"));
            }
            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

            ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
            ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void ddlnamaGL_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Params[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_xSGetCoacode", Params, "constrmf");



        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";


        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlKodeGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        xParams[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");


        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");

        ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue;


        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        // UpdatePanel1.Update();
    }
    protected void ddlNoFaktur_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtnofaktur.Text = ddlNoFaktur.SelectedValue;
        //if(ddlNoFaktur.SelectedValue != "-")
        //{ 
        //    try
        //    { 
        //        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
        //        var1[0] = gb.CreateSPParameter("@NoFaktur", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlNoFaktur.SelectedValue, typeof(string));
        //        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNOFAKTURTRN", typeof(string));
        //        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //        var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //        var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        //        ge.ListDataset = gb.GetDataJustOutPut("[AP_Transaksi]", var1, "constrmf");


        //        string jmlhtrn = ge.ListDataset.Tables[0].Rows[0]["jmlhtrn"].ToString();
        //        txtjmltrans.Text = jmlhtrn;
        //        decimal j, k, z;
        //        j = decimal.Parse(txtKurs.Text);
        //        k = decimal.Parse(txtjmltrans.Text);
        //        z = j * k;
        //        string formatted = string.Format("{0:N2}", z);
        //        string formatted1 = string.Format("{0:N2}", k);
        //        txtjmlrp.Text = formatted;
        //        txtjmltrans.Text = formatted1;
        //        hfjmlrp.Value = formatted;
        //    }
        //    catch
        //    {
        //        txtjmltrans.Text = "0";
        //        decimal j, k, z;
        //        j = decimal.Parse(txtKurs.Text);
        //        k = decimal.Parse(txtjmltrans.Text);
        //        z = j * k;
        //        string formatted = string.Format("{0:N2}", z);
        //        txtjmlrp.Text = formatted;
        //        hfjmlrp.Value = formatted;
        //    }
        //}
        //else
        //{
        //    txtnofaktur.Text = "-";
        //}
    }
    protected void ddlkurs_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtKurs.Text = ddlkurs.SelectedValue.Replace(".0000","").Replace(",0000", "");
    }
    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
            decimal GTotaldebit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                GTotaldebit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            }
            decimal GTotalkredit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                GTotalkredit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
            }
            if (GTotaldebit > Convert.ToDecimal(txtjmlbiaya.Text))
            {
                ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
                ((TextBox)grow.FindControl("jlhdebit")).Text = "0";
                GTotaldebit = 0;
                for (int i = 0; i < GRRP.Rows.Count; i++)
                {
                    GTotaldebit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                    if (GTotaldebit > Convert.ToDecimal(txtjmlbiaya.Text))
                    {
                        GTotaldebit += GTotaldebit - decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text); ;
                    }
                }
                GTotalkredit = 0;
                for (int i = 0; i < GRRP.Rows.Count; i++)
                {
                    GTotalkredit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
                }
                decimal kurang = Convert.ToDecimal(txtjmlbiaya.Text) - GTotaldebit;
                btnSimpan.Visible = false; 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:KurangDebitKredit('Jumlah Debit tidak boleh melebihi Jumlah Biaya!!','" + kurang + "');", true);

            }
            else if (GTotaldebit == GTotalkredit && GTotaldebit == Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = true;
            }
            else if (GTotaldebit != GTotalkredit && GTotaldebit != Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = false;
            }
            else if (GTotaldebit != Convert.ToDecimal(txtjmlbiaya.Text))
            {
                ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
            }
            Decimal sadaasdas = decimal.Parse(((TextBox)grow.FindControl("jlhdebit")).Text.Replace(",", ""));
            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void XXXTxt02_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
            decimal GTotaldebit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                GTotaldebit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            }
            decimal GTotalkredit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                GTotalkredit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
            }
            if (GTotalkredit > Convert.ToDecimal(txtjmlbiaya.Text))
            {
                ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
                ((TextBox)grow.FindControl("jlhkredit")).Text = "0";
                GTotalkredit = 0;
                for (int i = 0; i < GRRP.Rows.Count; i++)
                {
                    GTotalkredit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
                    if (GTotalkredit > Convert.ToDecimal(txtjmlbiaya.Text))
                    {
                        GTotalkredit += GTotaldebit - decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text); ;
                    }
                }
                GTotaldebit = 0;
                for (int i = 0; i < GRRP.Rows.Count; i++)
                {
                    GTotaldebit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                }
                decimal kurang = Convert.ToDecimal(txtjmlbiaya.Text) - GTotaldebit;
                btnSimpan.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:KurangDebitKredit('Jumlah Debit tidak boleh melebihi Jumlah Biaya!!','" + kurang + "');", true);
            }
            else if (GTotaldebit == GTotalkredit && GTotalkredit == Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = true;
            }
            else if (GTotaldebit != GTotalkredit && GTotalkredit != Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = false;
            }
            else if (GTotalkredit != Convert.ToDecimal(txtjmlbiaya.Text))
            {
                ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
            }
            Decimal sadaasdas = decimal.Parse(((TextBox)grow.FindControl("jlhkredit")).Text.Replace(",", ""));
            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void tgljthtempo_textchanged(object sender, EventArgs e)
    {
        //ddlkurs.Items.Clear();

        //cek tanggal fiskal
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglTransaksi.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
            txttglTransaksi.Text = xtanggal.ToString("dd-MM-yyyy");
            txttglmulaisusut.Text = txttglTransaksi.Text;
            var kdlgn = ddlkodeaktiva.SelectedValue;

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KetKonter", ParameterDirection.Input, SqlDbType.VarChar, 40, kdlgn, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATESYARATBAYAR", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("[AP_Transaksi]", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count != 0)
            {
                string SyaratBayar = ge.ListDataset.Tables[0].Rows[0]["SyaratBayar"].ToString();

                var ket = SyaratBayar.Substring(0, 1);
                var dateMonth = SyaratBayar.Substring(1);
                DateTime adddays;
                DateTime addmonth;
                if (ket == "D")
                {
                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(double.Parse(dateMonth));
                    txttglmulaisusut.Text = adddays.ToString("dd-MM-yyyy");
                }
                else if (ket == "M")
                {
                    addmonth = DateTime.Parse(txttglTransaksi.Text).AddMonths(int.Parse(dateMonth));
                    txttglmulaisusut.Text = addmonth.ToString("dd-MM-yyyy");
                }
                else if (ket == "W")
                {
                    int x, y, z;
                    x = int.Parse(dateMonth);
                    y = 7;
                    z = x * y;

                    var jmlhtgl = Convert.ToString(z);

                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(int.Parse(jmlhtgl));
                    txttglmulaisusut.Text = adddays.ToString("dd-MM-yyyy");
                }
            }
        }
        else
        {

            var kdlgn = ddlkodeaktiva.SelectedValue;

            // DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KetKonter", ParameterDirection.Input, SqlDbType.VarChar, 40, kdlgn, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATESYARATBAYAR", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("[AP_Transaksi]", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count != 0)
            {
                string SyaratBayar = ge.ListDataset.Tables[0].Rows[0]["SyaratBayar"].ToString();

                var ket = SyaratBayar.Substring(0, 1);
                var dateMonth = SyaratBayar.Substring(1);
                DateTime adddays;
                DateTime addmonth;
                if (ket == "D")
                {
                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(double.Parse(dateMonth));
                    txttglmulaisusut.Text = adddays.ToString("dd-MM-yyyy");
                }
                else if (ket == "M")
                {
                    addmonth = DateTime.Parse(txttglTransaksi.Text).AddMonths(int.Parse(dateMonth));
                    txttglmulaisusut.Text = addmonth.ToString("dd-MM-yyyy");
                }
                else if (ket == "W")
                {
                    int x, y, z;
                    x = int.Parse(dateMonth);
                    y = 7;
                    z = x * y;

                    var jmlhtgl = Convert.ToString(z);

                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(int.Parse(jmlhtgl));
                    txttglmulaisusut.Text = adddays.ToString("dd-MM-yyyy");
                }
            }
        }
    }
    protected void tgltrn_textchange(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglTransaksi.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
            txttglTransaksi.Text = xtanggal.ToString("dd-MM-yyyy");
        }
    }
    protected void kurs_textchanged(object sender, EventArgs e)
    {
        try
        {
        decimal j, k, z;
        j = decimal.Parse(txtKurs.Text);
        k = decimal.Parse(txtjmlbiaya.Text);
        z = j * k;
        string formatted = string.Format("{0:N2}", z);
        string formatted1 = string.Format("{0:N2}", k);
        txtjmlrp.Text = formatted;
        txtjmlbiaya.Text = formatted1;
        hfjmlrp.Value = formatted;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //txtketerangan.Text = ((Label)GRRP.Rows[0].FindControl("txtKT")).Text != string.Empty ? ((Label)GRRP.Rows[0].FindControl("txtKT")).Text : "";
        Response.Redirect("TransaksiPerawatan.aspx");
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("TransaksiPerawatan.aspx");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnSimpan.Enabled = true;
        btnSimpan.Visible = true;
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        Pnlfooter.Visible = true;
        PnlJournal.Visible = false; 
        if (rbJurnal.SelectedValue == "1")
        {
            PnlJournal.Visible = true;
            Pnlfooter.Visible = false;
        }
        else
        {
            Pnlfooter.Visible = true;
            PnlJournal.Visible = false;
        }

        if (ddlkodeaktiva.Enabled == false && rbJurnal.SelectedValue == "2")
        {
            PnlJournal.Visible = false;
            Pnlfooter.Visible = true;
        }
        loaddata();

    }
    protected void Btnnextjournal_Click(object sender, EventArgs e)
    {
        try
        {
            decimal GTotaldebit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                // String total = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                GTotaldebit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            }
            decimal GTotalkredit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                // String total = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                GTotalkredit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
            }
            if (GTotalkredit > GTotaldebit || GTotalkredit < GTotaldebit || GTotalkredit != Convert.ToDecimal(txtjmlbiaya.Text) || GTotaldebit != Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = false;
            }
            else if (GTotaldebit == GTotalkredit && GTotaldebit == Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = true;
            }

            if (rbJurnal.SelectedValue == "1")
            {
                Pnlfooter.Visible = true;
                PnlJournal.Visible = false;

            }
            else
            {
                Decimal asda = Convert.ToDecimal(txtjmlbiaya.Text);
                if (txtjmlbiaya.Text == null || Convert.ToDecimal(txtjmlbiaya.Text) == Convert.ToDecimal("0") || Convert.ToDecimal(txtjmlbiaya.Text) <= Convert.ToDecimal("1"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Biaya tidak boleh 0(nol)..!!');", true);
                }
                else
                {
                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = false;
                    Pnlfooter.Visible = true;
                    PnlGrid.Visible = true;
                    PnlJournal.Visible = false;
                    Btnnextjournal.Visible = false;
                    btnCancel.Visible = true;
                    btnSimpan.Visible = false;
                }
            }
            //  
            lnkAddRP2_Click();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        if(pnladd.Visible == true)
        { 
            Response.Redirect("TransaksiPerawatan.aspx"); 
        }
        else
        {
            pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;
            Pnlfooter.Visible = true;
            PnlJournal.Visible = false;
            PnlGrid.Visible = false; 
            if (rbJurnal.SelectedIndex == 1)
            {
                PnlJournal.Visible = true;
                Pnlfooter.Visible = false;
            }
            else
            {
                Pnlfooter.Visible = true;
                PnlJournal.Visible = false;
            }
            loaddata();
        }
    }
    protected void btnbackheader2_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransaksiPerawatan.aspx");
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        btnSimpan.Enabled = false;
        btnSimpan.Visible = false;
        try
        {
            Decimal asd = Convert.ToDecimal(txtjmlbiaya.Text);
            if (rbJurnal.SelectedValue == "1" && ddlJurnal.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pilih salah satu Data Jurnal');", true);
            }
            else if (txtjmlbiaya.Text == null || Convert.ToDecimal(txtjmlbiaya.Text) == Convert.ToDecimal("0") || Convert.ToDecimal(txtjmlbiaya.Text) <= Convert.ToDecimal("1"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Biaya tidak boleh 0(nol)..!!');", true);
            }
            else
            {
                string rubahjumlahdebit = "";
                string rubahjumlahkredit = "";
                string rubahjumlah = "";
                dtRP.Columns.Clear();

                dtRP.Columns.Add(new DataColumn("KodeAktiva", typeof(string)));
                dtRP.Columns.Add(new DataColumn("NoBukti", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Kdpersh", typeof(string)));
                dtRP.Columns.Add(new DataColumn("kodecabang", typeof(string)));
                dtRP.Columns.Add(new DataColumn("TglTrn", typeof(DateTime)));
                dtRP.Columns.Add(new DataColumn("TypeTrn", typeof(string)));
                dtRP.Columns.Add(new DataColumn("DelIndex", typeof(string)));
                dtRP.Columns.Add(new DataColumn("KodeUser", typeof(string)));
                dtRP.Columns.Add(new DataColumn("KodeSumber", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Keterangan", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Jumlah", typeof(int)));
                dtRP.Columns.Add(new DataColumn("JlhDebit", typeof(int)));
                dtRP.Columns.Add(new DataColumn("JlhKredit", typeof(int)));

                dtRP.Rows.Clear();

                dtRPDetil.Columns.Clear();

                dtRPDetil.Columns.Add(new DataColumn("nomor", typeof(int)));
                dtRPDetil.Columns.Add(new DataColumn("KodeGL", typeof(string)));
                dtRPDetil.Columns.Add(new DataColumn("NamaGL", typeof(string)));
                dtRPDetil.Columns.Add(new DataColumn("Description", typeof(string)));
                dtRPDetil.Columns.Add(new DataColumn("Costcenter", typeof(string)));
                dtRPDetil.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
                dtRPDetil.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));

                dtRPDetil.Rows.Clear();

                if (rbJurnal.SelectedValue == "1")
                {
                    for (int i = 0; i < 2; i++)
                    {
                        DataRow dr1 = dtRPDetil.NewRow();
                        string rubahi = i.ToString();
                        dr1["nomor"] = i + 1;
                        dr1["KodeGL"] = rubahi == "0" ? txtDebit1.Text : txtKredit1.Text;
                        dr1["NamaGL"] = rubahi == "0" ? txtDebit3.Text : txtKredit3.Text;
                        dr1["Description"] = txtketerangan.Text;
                        dr1["Costcenter"] = rubahi == "0" ? txtDebit2.Text : txtKredit2.Text;
                        dr1["jlhdebit"] = rubahi == "0" ? txtjmlbiaya.Text : "0";
                        dr1["jlhkredit"] = rubahi == "0" ? "0" : txtjmlbiaya.Text;

                        dtRPDetil.Rows.Add(dr1);
                    }

                }
                else
                {
                    for (int i = 0; i < GRRP.Rows.Count; i++)
                    {
                        DataRow dr1 = dtRPDetil.NewRow();

                        dr1["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                        dr1["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                        dr1["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
                        dr1["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                        dr1["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                        dr1["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                        dr1["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);

                        dtRPDetil.Rows.Add(dr1);
                    }
                }
                DefaultClass.Global.SqlHelper[] varNB = new DefaultClass.Global.SqlHelper[5];
                varNB[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkodeaktiva.SelectedValue, typeof(string));
                varNB[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varNB[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYKODE", typeof(string));
                varNB[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varNB[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("Fa_Perawatan", varNB, "constrmf");
                string nobukti = "";
                nobukti = ge.ListDataset.Tables[0].Rows[0]["NobuktiTransaksi"].ToString();
                if (nobukti == "")
                {
                    nobukti = ge.ListDataset.Tables[0].Rows[0]["NobuktiTransaksi"].ToString(); ;
                }
                else
                {
                    nobukti = "";
                }

                //if (HdnddlJlhrp.Value == "") HdnddlJlhrp.Value = "0";
                if (rbJurnal.SelectedValue == "1")
                {
                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                    var2[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkodeaktiva.SelectedValue, typeof(string));
                    var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var2[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYAKTIVA", typeof(string));
                    var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var2, "constrmf");
                    ge.ListDataTable = gb.GetDataJustOutPut("FA_Perawatan", var2, "constrmf").Tables[0];

                    string kodesumber = ge.ListDataset.Tables[0].Rows[0]["KodeSumber"].ToString();
                    bool tutupbukuflag = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["TutupBukuFlag"].ToString());
                    string kodelgn = ge.ListDataset.Tables[0].Rows[0]["KodeLgn"].ToString();
                    for (int i = 0; i < ge.ListDataTable.Rows.Count; i++)
                    {
                        DataRow dr = dtRP.NewRow();

                        dr["KodeAktiva"] = ddlkodeaktiva.SelectedValue;
                        dr["NoBukti"] = txtnoBukti.Text;
                        dr["Kdpersh"] = SessKdpersh;
                        dr["kodecabang"] = SessBranchID;
                        dr["TglTrn"] = Convert.ToDateTime(txttglTransaksi.Text);
                        dr["TypeTrn"] = ddltypetrans.SelectedValue;
                        dr["DelIndex"] = ge.ListDataset.Tables[0].Rows[0]["DelIndex"].ToString();
                        dr["KodeUser"] = ge.ListDataset.Tables[0].Rows[0]["KodeUser"].ToString();
                        dr["KodeSumber"] = ge.ListDataset.Tables[0].Rows[0]["KodeSumber"].ToString();
                        dr["Keterangan"] = txtketerangan.Text;
                        dr["Jumlah"] = Convert.ToDecimal(txtjmlbiaya.Text);
                        dr["JlhDebit"] = Convert.ToDecimal(txtjmlbiaya.Text);
                        dr["JlhKredit"] = Convert.ToDecimal(txtjmlbiaya.Text);

                        dtRP.Rows.Add(dr);
                    }

                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[11];

                    Params[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoBukti.Text, typeof(string));
                    Params[1] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypetrans.SelectedValue, typeof(string));
                    Params[2] = gb.CreateSPParameter("@Currency", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlCurrency.SelectedValue, typeof(string));
                    Params[3] = gb.CreateSPParameter("@kurs", ParameterDirection.Input, SqlDbType.Decimal, 0, txtKurs.Text, typeof(decimal));
                    Params[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtketerangan.Text, typeof(string));
                    Params[5] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglTransaksi.Text, typeof(DateTime));
                    Params[6] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[7] = gb.CreateSPParameter("@GL_Transaction_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRPDetil, typeof(DataTable));
                    Params[8] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[10] = gb.CreateSPParameter("@STATEDL", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTDL", typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[19];
                        var1[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlkodeaktiva.SelectedValue, typeof(string));
                        var1[1] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnoBukti.Text, typeof(string));
                        var1[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                        var1[4] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglTransaksi.Text, typeof(string));
                        var1[5] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 2, ddltypetrans.SelectedValue, typeof(string));
                        var1[6] = gb.CreateSPParameter("@DelIndex", ParameterDirection.Input, SqlDbType.VarChar, 30, kodesumber, typeof(string));
                        var1[7] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                        var1[8] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 2, kodesumber, typeof(string));
                        var1[9] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtketerangan.Text, typeof(string));
                        var1[10] = gb.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlbiaya.Text, typeof(string));
                        var1[11] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlbiaya.Text, typeof(string));
                        var1[12] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlbiaya.Text, typeof(string));
                        var1[13] = gb.CreateSPParameter("@TutupBukuFlag", ParameterDirection.Input, SqlDbType.Bit, 30, tutupbukuflag, typeof(bool));
                        var1[14] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                        var1[15] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var1[16] = gb.CreateSPParameter("@TypeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 20, rbJurnal.SelectedValue, typeof(string));
                        var1[17] = gb.CreateSPParameter("@HapusFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(0), typeof(bool));
                        var1[18] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJurnal.SelectedValue, typeof(string));
                        //var1[3] = gb.CreateSPParameter("@FA_Transaksi_Perawatan", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                        ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var1, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
                else if (rbJurnal.SelectedValue == "2")
                {
                    decimal GTotaldebit = 0;
                    for (int i = 0; i < GRRP.Rows.Count; i++)
                    {
                        // String total = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                        GTotaldebit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                    }
                    decimal GTotalkredit = 0;
                    for (int i = 0; i < GRRP.Rows.Count; i++)
                    {
                        // String total = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                        GTotalkredit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
                    }
                    if (GTotaldebit == GTotalkredit && GTotaldebit == Convert.ToDecimal(txtjmlbiaya.Text))
                    {
                        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                        var2[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkodeaktiva.SelectedValue, typeof(string));
                        var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                        var2[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYAKTIVA", typeof(string));
                        var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var2, "constrmf");
                        ge.ListDataTable = gb.GetDataJustOutPut("FA_Perawatan", var2, "constrmf").Tables[0];
                        string kodesumber = ge.ListDataset.Tables[0].Rows[0]["KodeSumber"].ToString();
                        bool tutupbukuflag = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["TutupBukuFlag"].ToString());
                        for (int i = 0; i < 1; i++)
                        {
                            DataRow dr = dtRP.NewRow();

                            dr["KodeAktiva"] = ddlkodeaktiva.SelectedValue;
                            dr["NoBukti"] = txtnoBukti.Text;
                            dr["Kdpersh"] = SessKdpersh;
                            dr["kodecabang"] = SessBranchID;
                            dr["TglTrn"] = Convert.ToDateTime(txttglTransaksi.Text);
                            dr["TypeTrn"] = ddltypetrans.SelectedValue;
                            dr["DelIndex"] = ge.ListDataset.Tables[0].Rows[0]["DelIndex"].ToString();
                            dr["KodeUser"] = ge.ListDataset.Tables[0].Rows[0]["KodeUser"].ToString();
                            dr["KodeSumber"] = ge.ListDataset.Tables[0].Rows[0]["KodeSumber"].ToString();
                            dr["Keterangan"] = txtketerangan.Text;
                            dr["Jumlah"] = Convert.ToDecimal(txtjmlbiaya.Text);
                            dr["JlhDebit"] = GTotaldebit;
                            dr["JlhKredit"] = GTotalkredit;//decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);

                            dtRP.Rows.Add(dr);
                        }
                        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[11];

                        Params[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoBukti.Text, typeof(string));
                        Params[1] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypetrans.SelectedValue, typeof(string));
                        Params[2] = gb.CreateSPParameter("@Currency", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlCurrency.SelectedValue, typeof(string));
                        Params[3] = gb.CreateSPParameter("@kurs", ParameterDirection.Input, SqlDbType.Decimal, 0, txtKurs.Text, typeof(decimal));
                        Params[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtketerangan.Text, typeof(string));
                        Params[5] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglTransaksi.Text, typeof(DateTime));
                        Params[6] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Params[7] = gb.CreateSPParameter("@GL_Transaction_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRPDetil, typeof(DataTable));
                        Params[8] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        Params[10] = gb.CreateSPParameter("@STATEDL", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTDL", typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", Params, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {
                            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[19];
                            var1[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlkodeaktiva.SelectedValue, typeof(string));
                            var1[1] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnoBukti.Text, typeof(string));
                            var1[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                            var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                            var1[4] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglTransaksi.Text, typeof(string));
                            var1[5] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 2, ddltypetrans.SelectedValue, typeof(string));
                            var1[6] = gb.CreateSPParameter("@DelIndex", ParameterDirection.Input, SqlDbType.VarChar, 30, kodesumber, typeof(string));
                            var1[7] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                            var1[8] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 2, kodesumber, typeof(string));
                            var1[9] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtketerangan.Text, typeof(string));
                            var1[10] = gb.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlbiaya.Text, typeof(string));
                            var1[11] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlbiaya.Text, typeof(string));
                            var1[12] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlbiaya.Text, typeof(string));
                            var1[13] = gb.CreateSPParameter("@TutupBukuFlag", ParameterDirection.Input, SqlDbType.Bit, 30, tutupbukuflag, typeof(bool));
                            var1[14] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                            var1[15] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            var1[16] = gb.CreateSPParameter("@TypeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 20, rbJurnal.SelectedValue, typeof(string));
                            var1[17] = gb.CreateSPParameter("@HapusFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(0), typeof(bool));
                            var1[18] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJurnal.SelectedValue, typeof(string));
                            //var1[3] = gb.CreateSPParameter("@FA_Transaksi_Perawatan", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                            ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var1, "constrmf");
                            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        }
                    }
                    else if (GTotalkredit > GTotaldebit || GTotalkredit < GTotaldebit || GTotalkredit != Convert.ToDecimal(txtjmlbiaya.Text) || GTotaldebit != Convert.ToDecimal(txtjmlbiaya.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi dan Jumlah Debit Tidak Sama!');", true);
                    }
                }
                else if (rbJurnal.SelectedValue == "3")
                {
                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                    var2[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkodeaktiva.SelectedValue, typeof(string));
                    var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var2[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYAKTIVA", typeof(string));
                    var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var2, "constrmf");
                    ge.ListDataTable = gb.GetDataJustOutPut("FA_Perawatan", var2, "constrmf").Tables[0];

                    string kodesumber = ge.ListDataset.Tables[0].Rows[0]["KodeSumber"].ToString();
                    bool tutupbukuflag = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["TutupBukuFlag"].ToString());
                    string kodelgn = ge.ListDataset.Tables[0].Rows[0]["KodeLgn"].ToString();
                    for (int i = 0; i < ge.ListDataTable.Rows.Count; i++)
                    {
                        DataRow dr = dtRP.NewRow();

                        dr["KodeAktiva"] = ddlkodeaktiva.SelectedValue;
                        dr["NoBukti"] = txtnoBukti.Text;
                        dr["Kdpersh"] = SessKdpersh;
                        dr["kodecabang"] = SessBranchID;
                        dr["TglTrn"] = Convert.ToDateTime(txttglTransaksi.Text);
                        dr["TypeTrn"] = ddltypetrans.SelectedValue;
                        dr["DelIndex"] = ge.ListDataset.Tables[0].Rows[0]["DelIndex"].ToString();
                        dr["KodeUser"] = ge.ListDataset.Tables[0].Rows[0]["KodeUser"].ToString();
                        dr["KodeSumber"] = ge.ListDataset.Tables[0].Rows[0]["KodeSumber"].ToString();
                        dr["Keterangan"] = txtketerangan.Text;
                        dr["Jumlah"] = Convert.ToDecimal(txtjmlbiaya.Text);
                        dr["JlhDebit"] = Convert.ToDecimal(txtjmlbiaya.Text);
                        dr["JlhKredit"] = Convert.ToDecimal(txtjmlbiaya.Text);

                        dtRP.Rows.Add(dr);
                    }

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[19];
                    var1[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlkodeaktiva.SelectedValue, typeof(string));
                    var1[1] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnoBukti.Text, typeof(string));
                    var1[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                    var1[4] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglTransaksi.Text, typeof(string));
                    var1[5] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 2, ddltypetrans.SelectedValue, typeof(string));
                    var1[6] = gb.CreateSPParameter("@DelIndex", ParameterDirection.Input, SqlDbType.VarChar, 30, ge.ListDataset.Tables[0].Rows[0]["KodeSumber"].ToString(), typeof(string));
                    var1[7] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    var1[8] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 2, ge.ListDataset.Tables[0].Rows[0]["KodeSumber"].ToString(), typeof(string));
                    var1[9] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtketerangan.Text, typeof(string));
                    var1[10] = gb.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlbiaya.Text, typeof(string));
                    var1[11] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlbiaya.Text, typeof(string));
                    var1[12] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlbiaya.Text, typeof(string));
                    var1[13] = gb.CreateSPParameter("@TutupBukuFlag", ParameterDirection.Input, SqlDbType.Bit, 30, Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["TutupBukuFlag"].ToString()), typeof(bool));
                    var1[14] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                    var1[15] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[16] = gb.CreateSPParameter("@TypeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 20, rbJurnal.SelectedValue, typeof(string));
                    var1[17] = gb.CreateSPParameter("@HapusFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(0), typeof(bool));
                    var1[18] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJurnal.SelectedValue, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransaksiPerawatan.aspx");
    }
    protected void btnAdddetail_Click(object sender, EventArgs e)
    {

    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            string aktiva = ddlkodeaktiva.SelectedValue;
            string nobukti = txtnoBukti.Text;
            string jlhrawat = txtjmlrp.Text;

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];

            var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[4] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, aktiva, typeof(string));
            var1[5] = gb.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Money, 0, jlhrawat, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("Fa_Perawatan", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully save this Project','top-right','success','2');", true);
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
    protected void lnkBT_Click(object sender, EventArgs e)
    {
        try
        {
            decimal GTotaldebit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                GTotaldebit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            }
            decimal GTotalkredit = 0;
            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                GTotalkredit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
            }
            if (GTotaldebit == GTotalkredit && GTotaldebit == Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Debit dan Kredit anda sudah Balance dengan Jumlah Harga Biaya');", true);
            }
            //else if (GTotaldebit == Convert.ToDecimal(txtharga.Text) && GTotalkredit == Convert.ToDecimal(txtharga.Text))
            //{

            //}
            else if (GTotaldebit < Convert.ToDecimal(txtjmlbiaya.Text) || GTotalkredit < Convert.ToDecimal(txtjmlbiaya.Text))
            {
                btnSimpan.Visible = false;
                int dgcount = GRRP.Rows.Count;

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRP"];

                dtRP.Rows.Clear();

                for (int i = 0; i < dgcount; i++)
                {
                    DataRow dr = dtRP.NewRow();

                    dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                    dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                    dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
                    dr["Description"] = ((TextBox)GRRP.Rows[0].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                    dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                    dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                    dr["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
                    dtRP.Rows.Add(dr);
                }
                DataRow drdnew = dtRP.NewRow();
                if (drdnew["Description"].ToString() == "")
                {
                    drdnew["nomor"] = dgcount + 1;
                    drdnew["KodeGL"] = string.Empty;
                    drdnew["NamaGL"] = string.Empty;
                    drdnew["Description"] = ((TextBox)GRRP.Rows[0].FindControl("txtKT")).Text;
                    drdnew["Costcenter"] = string.Empty;
                    drdnew["jlhdebit"] = decimal.Zero;
                    drdnew["jlhkredit"] = decimal.Zero;
                }
                else
                {
                    drdnew["nomor"] = dgcount + 1;
                    drdnew["KodeGL"] = string.Empty;
                    drdnew["NamaGL"] = string.Empty;
                    drdnew["Description"] = string.Empty;
                    drdnew["Costcenter"] = string.Empty;
                    drdnew["jlhdebit"] = decimal.Zero;
                    drdnew["jlhkredit"] = decimal.Zero;
                }
                dtRP.Rows.Add(drdnew);

                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRP();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    private void SetDetailRP()
    {

        DataRow dr = null;

        dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));

        dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));


        if (GRRP.Rows.Count == 0)
        {
            dr = dtRP.NewRow();
            dr["nomor"] = 1;
            dr["KodeGL"] = string.Empty;
            dr["NamaGL"] = string.Empty;
            dr["Description"] = txtketerangan.Text;
            dr["Costcenter"] = string.Empty;
            dr["jlhdebit"] = decimal.Zero;
            dr["jlhkredit"] = decimal.Zero;
        }
        else { 
            dr = dtRP.NewRow();
            dr["nomor"] = 1;
            dr["KodeGL"] = string.Empty;
            dr["NamaGL"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Costcenter"] = string.Empty;
            dr["jlhdebit"] = decimal.Zero;
            dr["jlhkredit"] = decimal.Zero;
        }
        dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRP"] = dtRP;


        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();

    }
    private void loaddata()
    {
        //txtketerangan.Text = ((TextBox)GRRP.Rows[0].FindControl("txtKT")).Text != string.Empty ? ((TextBox)GRRP.Rows[0].FindControl("txtKT")).Text : "";

    }
    private void lnkAddRP2_Click()
    { 
        int dgcount = GRRP.Rows.Count;
        if (dgcount > 0)
        {
            dtRP.Columns.Clear();

            dtRP = (DataTable)ViewState["ListDetailRP"];

            dtRP.Rows.Clear();

            for (int i = 0; i < dgcount; i++)
            {
                DataRow dr = dtRP.NewRow();

                dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
                dr["Description"] = txtketerangan.Text;  //((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text; 
                dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                dr["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
                dtRP.Rows.Add(dr);
            }


            ViewState["ListDetailRP"] = dtRP;

            GRRP.DataSource = dtRP;
            GRRP.DataBind();

            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

            cssGridRP();
            if(rbJurnal.Enabled == false)
            {
                for (int i = 0; i < GRRP.Rows.Count; i++)
                {
                    ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).Attributes["disabled"] = "disabled";
                    ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Attributes["disabled"] = "disabled";
                    ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Attributes["disabled"] = "disabled";
                    ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).Attributes["disabled"] = "disabled";
                    ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Attributes["disabled"] = "disabled";
                    ((TextBox)GRRP.Rows[i].FindControl("jlhKredit")).Attributes["disabled"] = "disabled";
                    //((LinkButton)GRRP.Rows[i].FindControl("btndeletegrrp")).Attributes["disabled"] = "disabled";
                    //btndelete.Visible = false;
                    //LinkButton lnk2 = (LinkButton)GRRP.Rows[i].FindControl("btndeletegrrp");
                    //lnk2.Enabled = false;
                    //btnde

                    //DropDownList yourDropdown = (DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL");
                    //yourDropdown.Attributes.Add("readonly", "readonly");
                    //(GRRP.Rows[i].FindControl("ddlKodeGL") as DropDownList).Items.FindByValue(dtRP.Rows[i][1].ToString()).Attributes.Add("Disabled", "Disabled");

                }
                btnSimpan.Visible = false;
                GRRP.Enabled = false;
            }
            

            //DataRow drdnew = dtRP.NewRow();
            //drdnew["nomor"] = dgcount + 1;
            //drdnew["KodeGL"] = string.Empty;
            //drdnew["NamaGL"] = string.Empty;
            //drdnew["Description"] = string.Empty;
            //drdnew["Costcenter"] = string.Empty;
            //drdnew["jlhdebit"] = decimal.Zero;
            //drdnew["jlhkredit"] = decimal.Zero;

            //dtRP.Rows.Add(drdnew);
        }


        else
        {
            SetDetailRP();

            
        }


    }
    private void cssGridRP()
    {

        // Attribute to show the Plus Minus Button.
        GRRP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRRP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand"); 
        cells[3].Attributes.Add("data-hide", "phone");
        cells[4].Attributes.Add("data-hide", "phone");

    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "SETDEL":

                try 
                {
                    string kodeaktiva;
                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    PnlJournal.Visible = false;
                    Pnlfooter.Visible = true;
                    loaddata();

                    txtnamaaktiva.Enabled = false;
                    ddlkodeaktiva.Enabled = false;
                    //ddlkodeaktiva.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeaktiva")).Text;

                    //string nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    kodeaktiva = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeaktiva")).Text;
                    txtnoBukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];

                    var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTALLDELETE", typeof(string));
                    var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));
                    var1[4] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, kodeaktiva, typeof(string));
                    var1[5] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 40, txtnoBukti.Text, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", var1, "constrmf");

                    ddlkodeaktiva.Items.Insert(0, new ListItem(kodeaktiva + " - " + ge.ListDataset.Tables[0].Rows[0]["aktiva"].ToString(), kodeaktiva));
                    ddlkodeaktiva.SelectedValue = kodeaktiva;
                    txtnamaaktiva.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAktiva"].ToString();
                    txtnoBukti.Text = ge.ListDataset.Tables[0].Rows[0]["Nobukti"].ToString();
                    txtnomorekonomis.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBulan"].ToString();
                    txtmasamanfaat.Text = ge.ListDataset.Tables[0].Rows[0]["NamaTahun"].ToString();
                    txthargaperolehan.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["JlhPerolehan"].ToString());
                    txtnilairesidu.Text = ge.ListDataset.Tables[0].Rows[0]["JlhResidu"].ToString();
                    txtketerangan.Text = ge.ListDataset.Tables[0].Rows[0]["Keterangan"].ToString();
                    //txtketerangan.MaxLength = 150;
                    txthargapembelian.Text = ge.ListDataset.Tables[0].Rows[0]["HargaBeli"].ToString();
                    txtakumulasisusut.Text = ge.ListDataset.Tables[0].Rows[0]["HargaAkumulasi"].ToString();
                    txtpenambahanharga.Text = ge.ListDataset.Tables[0].Rows[0]["HargaTambah"].ToString();
                    txtpenguranganharga.Text = ge.ListDataset.Tables[0].Rows[0]["HargaKurang"].ToString();
                    txtjmlbiaya.Text = ge.ListDataset.Tables[0].Rows[0]["Jumlah"].ToString();
                    txtjmlrp.Text = ge.ListDataset.Tables[0].Rows[0]["Jumlah"].ToString();
                    ddltypetrans.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["TypeTrn"].ToString();
                    rbJurnal.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["TypeJurnal"].ToString();

                    txttglTransaksi.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString()).ToString("yyyy-MM-dd");
                    txttglmulaisusut.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglMulaiSusut"].ToString()).ToString("yyyy-MM-dd");
                    txttglakhirsusut.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglAkhirSusut"].ToString()).ToString("yyyy-MM-dd");

                    decimal a, b, c, d, f, g, h, j;
                    a = Convert.ToDecimal(txthargaperolehan.Text.ToString());
                    txthargaperolehan.Text = string.Format("{0:N2}", a);
                    b = Convert.ToDecimal(txthargapembelian.Text.ToString());
                    txthargapembelian.Text = string.Format("{0:N2}", b);
                    c = Convert.ToDecimal(txtnilairesidu.Text.ToString());
                    txtnilairesidu.Text = string.Format("{0:N2}", c);
                    d = Convert.ToDecimal(txtakumulasisusut.Text.ToString());
                    txtakumulasisusut.Text = string.Format("{0:N2}", d);
                    f = Convert.ToDecimal(txtpenambahanharga.Text.ToString());
                    txtpenambahanharga.Text = string.Format("{0:N2}", f);
                    g = Convert.ToDecimal(txtpenguranganharga.Text.ToString());
                    txtpenguranganharga.Text = string.Format("{0:N2}", g);
                    h = Convert.ToDecimal(txtjmlbiaya.Text.ToString());
                    txtjmlbiaya.Text = string.Format("{0:N2}", h);
                    j = Convert.ToDecimal(txtjmlrp.Text.ToString());
                    txtjmlrp.Text = string.Format("{0:N2}", j);

                    rbJurnal.Enabled = false;
                    txtketerangan.Enabled = false;
                    ddltypetrans.Enabled = false;
                    txtjmlrp.Enabled = false;
                    txtjmlbiaya.Enabled = false;
                    btndelete.Enabled = true;
                    txttglTransaksi.Enabled = false;

                    if (rbJurnal.SelectedValue == "1")
                    {
                        FileJurnal.Visible = true;

                        string nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                        //ddlJurnal.Items.Insert(0, new ListItem(ddljurnal));  
                        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5]; 
                        varT[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 40, txtnoBukti.Text, typeof(string));
                        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKETJURNAL", typeof(string));
                        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varT[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varT[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", varT, "constrmf");

                        string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["Nama"].ToString();
                        string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
                        string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
                        string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
                        string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
                        ddlJurnal.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeJurnal"].ToString();
                        txtJurnal.Text = KetJurnal;
                        txtDebit1.Text = DebitGL;
                        txtDebit2.Text = DebitCC;
                        txtKredit1.Text = KreditGL;
                        txtKredit2.Text = KreditCC;
                        ddlJurnal.Enabled = false;

                        //GET KETERANGAN GL
                        DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[5];
                        varGL[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, DebitGL, typeof(string));
                        varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
                        varGL[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varGL[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        varGL[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", varGL, "constrmf");
                        string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
                        txtDebit3.Text = DCC;

                        //GET KETERANGAN CC
                        DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[5];
                        varCC[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, KreditGL, typeof(string));
                        varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
                        varCC[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varCC[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        varCC[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("FA_Perawatan", varCC, "constrmf");
                        string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
                        txtKredit3.Text = KGL;

                    }
                    else if (rbJurnal.SelectedValue == "2")
                    {
                        PnlJournal.Visible = true;
                        Pnlfooter.Visible = false;
                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                        Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoBukti.Text, typeof(string)); 
                        Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GLGETTRN", typeof(string));
                        Paramsc[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.FA_Perawatan", Paramsc, "constrmf").Tables[0];

                        if (ge.ListDataTable.Rows.Count > 0)
                        {
                            ViewState["ListDetailRP"] = ge.ListDataTable;
                            

                            GRRP.DataSource = ge.ListDataTable;
                            GRRP.DataBind();

                            GRRP.UseAccessibleHeader = true;
                            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
                            
                                
                            //GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
                            //((DropDownList)grow.FindControl("ddlKodeGL")).Enabled = false;
                            //((DropDownList)grow.FindControl("ddlnamaGL")).Enabled = false;
                            //((TextBox)grow.FindControl("txtKT")).Enabled = false;
                            //((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = false;
                            //((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
                            //((TextBox)grow.FindControl("jlhKredit")).Enabled = false;
                            cssGridRP();
                            
                            //GRRP.Columns[2][2].

                        }
                        else
                        { 
                            SetDetailRP();
                        }


                    } 
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                btnSimpan.Visible = false;
                btndelete.Visible = true;
                break;
            case "SETDEL1":

                try
                {
                    string aktiva = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeaktiva")).Text;
                    string nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    string jlhrawat = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypetrans")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];

                    //var1[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, aktiva, typeof(string));
                    var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[4] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, aktiva, typeof(string));
                    var1[5] = gb.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Money, 0, jlhrawat, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("Fa_Perawatan", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    //DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[3];
                    //Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnnourut.Text, typeof(string));
                    //Paramsc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    //Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_Delete_Transaksi", Paramsc, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully save this Project','top-right','success','2');", true);
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

                break;
            case "PDF":

                try
                {

                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, "FA", typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_AR", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    }


                }

                catch (Exception X)
                {

                    if (X.Message != "Thread was being aborted.")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    }
                    else
                    {
                        rprt.Close();
                        rprt.Dispose();
                    }


                }

                break;
        }
    }
    protected void GRRP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "DEL":

                    int dgcount = GRRP.Rows.Count;


                    ((Label)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                    dtRP.Columns.Clear();

                    dtRP = (DataTable)ViewState["ListDetailRP"];

                    DataRow drCurrentRow = null;

                    dtRP.Rows.Clear();

                    int j = 1;

                    for (int i = 0; i < GRRP.Rows.Count; i++)
                    {

                        if (((Label)GRRP.Rows[i].FindControl("lblNumber")).Text != "")
                        {


                            drCurrentRow = dtRP.NewRow();

                            drCurrentRow["nomor"] = j;
                            drCurrentRow["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                            drCurrentRow["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue : (object)DBNull.Value;
                            drCurrentRow["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                            drCurrentRow["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                            drCurrentRow["jlhdebit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                            drCurrentRow["jlhkredit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text;


                            dtRP.Rows.Add(drCurrentRow);

                            j++;
                        }
                    }

                    if (dgcount == 1)
                    {
                        DataRow drdnew = dtRP.NewRow();

                        drdnew["nomor"] = 1;
                        drdnew["KodeGL"] = string.Empty;
                        drdnew["NamaGL"] = string.Empty;
                        drdnew["Description"] = string.Empty;
                        drdnew["Costcenter"] = string.Empty;
                        drdnew["jlhdebit"] = decimal.Zero;
                        drdnew["jlhkredit"] = decimal.Zero;
                        dtRP.Rows.Add(drdnew);
                    }


                    ViewState["ListDetailRP"] = dtRP;

                    GRRP.DataSource = dtRP;
                    GRRP.DataBind();

                    GRRP.UseAccessibleHeader = true;
                    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                    cssGridRP();


                break;
        }
    }
    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            Params[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNMGL", typeof(String));
            Params[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("dbo.CL_GetCoa", Params, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlnamaGL")), "-", "-");



            DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
            xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            xParams[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKDGL", typeof(String));
            xParams[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));
            gex.ListDataset = gbx.GetDataJustOutPut("dbo.CL_GetCoa", xParams, "constrmf");
            gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");

            //ddlkurs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddlkurs.DataTextField = "statuskurs";
            //ddlkurs.DataValueField = "KursStd";
            //ddlkurs.DataBind();
            //ddlkurs.Items.Insert(0, new ListItem("Pilih Data", "-"));

            DefaultClass.Global.SqlHelper[] xParamsCC = new DefaultClass.Global.SqlHelper[3];
            xParamsCC[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            xParamsCC[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGLCC", typeof(String));
            xParamsCC[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));
            gex.ListDataset = gbx.GetDataJustOutPut("dbo.CL_GetCoa", xParamsCC, "constrmf");
            gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "-", "-");

            ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;
            ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlnamaGL")).Value;

            ((DropDownList)e.Row.FindControl("ddlcostcenter")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlcostcenter")).Value;
            //((TextBox)e.Row.FindControl("txtKT")).Text = HdnTxtKet.Value;


            if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
            {
                ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

            }

            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
    }
    protected void Lnk_Supp(object sender,EventArgs e)
    {

        var nmsup = ddlkodeaktiva.SelectedValue;
        if (nmsup !="" || nmsup != "-")
        { 
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
            Params[0] = gb.CreateSPParameter("@KodeSupplier", ParameterDirection.Input, SqlDbType.VarChar, 20, nmsup, typeof(string));
            Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Params[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETDETAILSUPPLIER", typeof(String));
            Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AP_Transaksi", Params, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count != 0)
            {
                lblkdsuppu.Text = nmsup;
                lblnmsuppu.Text = ge.ListDataset.Tables[0].Rows[0]["NamaLgn"].ToString();
                lblalamat1pu.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
                lblalamat2pu.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat2"].ToString();
                lblalamat3pu.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat3"].ToString();
                lblkdgrouppu.Text = ge.ListDataset.Tables[0].Rows[0]["KodeGroup"].ToString();
                lblkdwilpu.Text = ge.ListDataset.Tables[0].Rows[0]["Kodewil"].ToString();
                lblcrcpu.Text = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
                var saldojalan = ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString(); 
                 
                decimal j, k;
                j = decimal.Parse(saldojalan); 
                string formatted = string.Format("{0:N2}", j); 

                lblsldjlnpu.Text = formatted; 

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Supplier tidak ditemukan');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pilih Kode Supplier terlebih dahulu');", true);
        }
    }
    private static int ConvertToInt(string number)
    {
        number = number.Replace(",", "").Replace(".", "");
        return int.Parse(number);
    }
    private static decimal ConvertToDecimal(string number)
    {
        var ns = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
        ns.NumberDecimalSeparator = ".";

        return decimal.Parse(number, ns);
    }
}

