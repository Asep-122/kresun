using DefaultClass;
using EntitiesClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FA_Generate_Jurnal : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    Global gb1 = new DefaultClass.Global();
    GlobalEntities ge1 = new GlobalEntities();
    Global gb2 = new DefaultClass.Global();
    GlobalEntities ge2 = new GlobalEntities();
    Global gb3 = new DefaultClass.Global();
    GlobalEntities ge3 = new GlobalEntities();
    Global gb4 = new DefaultClass.Global();
    GlobalEntities ge4 = new GlobalEntities();
    Global gb5 = new DefaultClass.Global();
    GlobalEntities ge5 = new GlobalEntities();
    Global gb6 = new DefaultClass.Global();
    GlobalEntities ge6 = new GlobalEntities();

    string tanggal = "";
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


    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }


    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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
                DateTime now = DateTime.Now;
                txttglAkhir.Text = now.ToString("yyyy-MM-dd");
                string ATahun = now.ToString("yyyy");
                string ABulan = now.ToString("MM");
                int rubahtahun = Int32.Parse(ATahun);
                int rubahbulan = Int32.Parse(ABulan);
                DateTime.DaysInMonth(rubahtahun, rubahbulan);
                String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
                string rubahtanggal = rubahhari + "/" + ABulan + "/" + ATahun;
                txttglAkhir.Text = rubahtanggal;
                getdata();
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void cekddatajurnal()
    {

    }
    protected void btntambah_click(object sender, EventArgs e)
    {

    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[5];
            var5[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var5[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            //var5[2] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
            //var5[2] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, Convert.ToDateTime( txttglAkhir.Text), typeof(DateTime));
            var5[2] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, Convert.ToDateTime(txttglAkhir.Text), typeof(DateTime));
            var5[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKJURNALGLTRNSM", typeof(string));
            var5[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var5, "constrmf");
            ge.ListDataTable = ge.ListDataset.Tables[0];
            //ge.ListDataTable = gb.GetDataJustOutPut("FA_GenerateJurnal", var5, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Data Pada Bulan Ini Sudah Di Proses, Harap Hubungi Admin');", true);
                txtnobukti.Enabled = false;
                //btnSimpan.Enabled = false;				
                btnSimpan.Visible = false;
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                var2[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SENDDATA", typeof(string));
                var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2[4] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglAkhir.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var2, "constrmf");
                //ge.ListDataTable = gb.GetDataJustOutPut("FA_GenerateJurnal", var2, "constrmf").Tables[0];
                ge.ListDataTable = ge.ListDataset.Tables[0];

                int ambilcountdata = ge.ListDataTable.Rows.Count;

                for (int i = 0; ambilcountdata > i; i++)
                {
                    DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[5];
                    var6[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                    var6[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var6[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SENDDATA", typeof(string));
                    var6[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var6[4] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglAkhir.Text, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var6, "constrmf");
                    //ge.ListDataTable = gb.GetDataJustOutPut("FA_GenerateJurnal", var6, "constrmf").Tables[0];
                    ge.ListDataTable = ge.ListDataset.Tables[0];

                    string NoBukti = ge.ListDataset.Tables[0].Rows[i]["NoBukti"].ToString();
                    string KodeAktiva = ge.ListDataset.Tables[0].Rows[i]["KodeAktiva"].ToString();
                    string kdpersh = ge.ListDataset.Tables[0].Rows[i]["kdpersh"].ToString();
                    string kodecabang = ge.ListDataset.Tables[0].Rows[i]["kodecabang"].ToString();
                    string TglTrn = ge.ListDataset.Tables[0].Rows[i]["TglTrn"].ToString();
                    string TypeTrn = ge.ListDataset.Tables[0].Rows[i]["TypeTrn"].ToString();
                    string Keterangan = ge.ListDataset.Tables[0].Rows[i]["Keterangan"].ToString();
                    string JlhPerolehan = ge.ListDataset.Tables[0].Rows[i]["JlhPerolehan"].ToString();
                    string DelIndex = ge.ListDataset.Tables[0].Rows[i]["DelIndex"].ToString();
                    string GenFlag = ge.ListDataset.Tables[0].Rows[i]["GenFlag"].ToString();
                    string TrfFlag = ge.ListDataset.Tables[0].Rows[i]["TrfFlag"].ToString();
                    string TutupBukuFlag = ge.ListDataset.Tables[0].Rows[i]["TutupBukuFlag"].ToString();
                    string TglEntry = ge.ListDataset.Tables[0].Rows[i]["TglEntry"].ToString();
                    string KodeUser = ge.ListDataset.Tables[0].Rows[i]["KodeUser"].ToString();
                    string KodeLgn = ge.ListDataset.Tables[0].Rows[i]["KodeLgn"].ToString();
                    string KodeCrc = ge.ListDataset.Tables[0].Rows[i]["KodeCrc"].ToString();
                    string KursStd = ge.ListDataset.Tables[0].Rows[i]["KursStd"].ToString();
                    string KursPajak = ge.ListDataset.Tables[0].Rows[i]["KursPajak"].ToString();
                    string Jumlah = ge.ListDataset.Tables[0].Rows[i]["Jumlah"].ToString();
                    string KeteranganMemorial = ge.ListDataset.Tables[0].Rows[i]["KeteranganMemorial"].ToString();
                    string AddedByFK = ge.ListDataset.Tables[0].Rows[i]["AddedByFK"].ToString();
                    string DateAdded = ge.ListDataset.Tables[0].Rows[i]["DateAdded"].ToString();
                    string DateModified = ge.ListDataset.Tables[0].Rows[i]["DateModified"].ToString();
                    string LastUserFK = ge.ListDataset.Tables[0].Rows[i]["LastUserFK"].ToString();
                    string PK = ge.ListDataset.Tables[0].Rows[i]["PK"].ToString();
                    string KodeSumber = ge.ListDataset.Tables[0].Rows[i]["KodeSumber"].ToString();
                    string TglMulaiSusut = ge.ListDataset.Tables[0].Rows[i]["TglMulaiSusut"].ToString();
                    string TglAkhirSusut = ge.ListDataset.Tables[0].Rows[i]["TglAkhirSusut"].ToString();
                    string MasaSusut = ge.ListDataset.Tables[0].Rows[i]["MasaSusut"].ToString();
                    string JlhResidu = ge.ListDataset.Tables[0].Rows[i]["JlhResidu"].ToString();
                    string HargaBeli = ge.ListDataset.Tables[0].Rows[i]["HargaBeli"].ToString();
                    string HargaAkumulasi = ge.ListDataset.Tables[0].Rows[i]["HargaAkumulasi"].ToString();
                    string HargaTambah = ge.ListDataset.Tables[0].Rows[i]["HargaTambah"].ToString();
                    string HargaKurang = ge.ListDataset.Tables[0].Rows[i]["HargaKurang"].ToString();
                    string TglAwalBeli = ge.ListDataset.Tables[0].Rows[i]["TglAwalBeli"].ToString();
                    string TglAkhirBeli = ge.ListDataset.Tables[0].Rows[i]["TglAkhirBeli"].ToString();
                    string nama_group = ge.ListDataset.Tables[0].Rows[i]["nama_group"].ToString();
                    string debit_gl = ge.ListDataset.Tables[0].Rows[i]["debit_gl"].ToString();
                    string kredit_gl = ge.ListDataset.Tables[0].Rows[i]["kredit_gl"].ToString();
                    string debit_cc = ge.ListDataset.Tables[0].Rows[i]["debit_cc"].ToString();
                    string kredit_cc = ge.ListDataset.Tables[0].Rows[i]["kredit_cc"].ToString();

                    DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[5];
                    var3[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                    var3[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKGETDATA", typeof(string));
                    var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var3[4] = gb.CreateSPParameter("@nama_group", ParameterDirection.Input, SqlDbType.VarChar, 50, nama_group, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var3, "constrmf");
                    //ge.ListDataTable = gb.GetDataJustOutPut("FA_GenerateJurnal", var3, "constrmf").Tables[0];
                    ge.ListDataTable = ge.ListDataset.Tables[0];


                    float GTotal = 0f;
                    //double ambildebit;
                    int ambilgetdata = ge.ListDataTable.Rows.Count;
                    for (int j = 0; ambilgetdata > j; j++)
                    {
                        int masasusut = Convert.ToInt32(MasaSusut);
                        double rubahmasasusut = Convert.ToDouble(masasusut);
                        double rubahperolehan = Convert.ToDouble(JlhPerolehan);
                        //int jumlahperolehan = Convert.ToInt32(rubahperolehan);
                        double hitungdebit = rubahperolehan / rubahmasasusut;
                        double rubahsemnua = Math.Round(hitungdebit, 3);
                        //string sadasdasdas = Math.Ceiling(rubahsemnua).ToString();
                        //String total = Convert.ToString( rubahperolehan/masasusut);
                        //decimal total2 = Convert.ToDecimal(rubahperolehan / masasusut);
                        GTotal += Convert.ToSingle(rubahsemnua);
                    }
                    string rubahGtotal = Math.Ceiling(GTotal).ToString();

                    DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[7];
                    var4[0] = gb1.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                    var4[1] = gb1.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var4[2] = gb1.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnobukti.Text, typeof(string));
                    var4[3] = gb1.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
                    var4[4] = gb1.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKBULANGLTRNSM", typeof(string));
                    var4[5] = gb1.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var4[6] = gb1.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 20, KodeAktiva, typeof(string));

                    ge1.ListDataTable = gb1.GetDataJustOutPut("FA_GenerateJurnal", var4, "constrmf").Tables[0];
                    int ambilitung = ge1.ListDataTable.Rows.Count;
                    if (ambilitung < 0)
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Data Pada Bulan Ini Sudah Di Proses, Harap Hubungi Admin');", true);
                    }
                    else
                    {

                        DefaultClass.Global.SqlHelper[] var7 = new DefaultClass.Global.SqlHelper[34];
                        var7[0] = gb1.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                        var7[1] = gb1.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 20, debit_gl, typeof(string));
                        var7[2] = gb1.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var7[3] = gb1.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        var7[4] = gb1.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
                        var7[5] = gb1.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 2, KodeSumber, typeof(string));
                        var7[6] = gb1.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 2, TypeTrn, typeof(string));
                        var7[7] = gb1.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, "Jurnal Biaya Penyusutan FA" + " " + txttglAkhir.Text, typeof(string));
                        var7[8] = gb1.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, rubahGtotal, typeof(string));
                        var7[9] = gb1.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(string));
                        var7[10] = gb1.CreateSPParameter("@DelIndex", ParameterDirection.Input, SqlDbType.VarChar, 15, DelIndex, typeof(string));
                        var7[11] = gb1.CreateSPParameter("@CfFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(false), typeof(Boolean));
                        var7[12] = gb1.CreateSPParameter("@HapusFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(false), typeof(Boolean));
                        var7[13] = gb1.CreateSPParameter("@TrfFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(false), typeof(Boolean));
                        var7[14] = gb1.CreateSPParameter("@TutupBukuFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(TutupBukuFlag), typeof(Boolean));
                        var7[15] = gb1.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, TglEntry, typeof(string));
                        var7[16] = gb1.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, KodeUser, typeof(string));
                        var7[17] = gb1.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeLgn, typeof(string));
                        var7[18] = gb1.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
                        var7[19] = gb1.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 3, KodeCrc, typeof(string));
                        var7[20] = gb1.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.Money, 0, KursStd, typeof(string));
                        var7[21] = gb1.CreateSPParameter("@KursPakai", ParameterDirection.Input, SqlDbType.Money, 0, KursPajak, typeof(string));
                        var7[22] = gb1.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(string));
                        var7[23] = gb1.CreateSPParameter("@FlagHutang", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(GenFlag), typeof(Boolean));
                        var7[24] = gb1.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 300, KeteranganMemorial, typeof(string));
                        var7[25] = gb1.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                        var7[26] = gb1.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
                        var7[27] = gb1.CreateSPParameter("@DateModified", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Now, typeof(DateTime));
                        var7[28] = gb1.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, LastUserFK, typeof(string));
                        var7[29] = gb1.CreateSPParameter("@TFlag", ParameterDirection.Input, SqlDbType.Bit, 0, false, typeof(Boolean));
                        var7[30] = gb1.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                        var7[31] = gb1.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var7[32] = gb1.CreateSPParameter("@TypeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 250, rdbtnType.SelectedValue, typeof(string));
                        var7[33] = gb1.CreateSPParameter("@KodeCc", ParameterDirection.Input, SqlDbType.VarChar, 250, debit_cc, typeof(string));

                        DefaultClass.Global.SqlHelper[] var8 = new DefaultClass.Global.SqlHelper[34];
                        var8[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                        var8[1] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 20, kredit_gl, typeof(string));
                        var8[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var8[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        var8[4] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
                        var8[5] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 2, KodeSumber, typeof(string));
                        var8[6] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 2, TypeTrn, typeof(string));
                        var8[7] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, "Jurnal Biaya Penyusutan FA" + " " + txttglAkhir.Text, typeof(string));
                        var8[8] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(string));
                        var8[9] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 0, rubahGtotal, typeof(string));
                        var8[10] = gb.CreateSPParameter("@DelIndex", ParameterDirection.Input, SqlDbType.VarChar, 15, DelIndex, typeof(string));
                        var8[11] = gb.CreateSPParameter("@CfFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(false), typeof(Boolean));
                        var8[12] = gb.CreateSPParameter("@HapusFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(false), typeof(Boolean));
                        var8[13] = gb.CreateSPParameter("@TrfFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(false), typeof(Boolean));
                        var8[14] = gb.CreateSPParameter("@TutupBukuFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(TutupBukuFlag), typeof(Boolean));
                        var8[15] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, TglEntry, typeof(string));
                        var8[16] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, KodeUser, typeof(string));
                        var8[17] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeLgn, typeof(string));
                        var8[18] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
                        var8[19] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 3, KodeCrc, typeof(string));
                        var8[20] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.Money, 0, KursStd, typeof(string));
                        var8[21] = gb.CreateSPParameter("@KursPakai", ParameterDirection.Input, SqlDbType.Money, 0, KursPajak, typeof(string));
                        var8[22] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, "0", typeof(string));
                        var8[23] = gb.CreateSPParameter("@FlagHutang", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(GenFlag), typeof(Boolean));
                        var8[24] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 300, KeteranganMemorial, typeof(string));
                        var8[25] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                        var8[26] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
                        var8[27] = gb.CreateSPParameter("@DateModified", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Now, typeof(DateTime));
                        var8[28] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, LastUserFK, typeof(string));
                        var8[29] = gb.CreateSPParameter("@TFlag", ParameterDirection.Input, SqlDbType.Bit, 0, false, typeof(Boolean));
                        var8[30] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                        var8[31] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var8[32] = gb.CreateSPParameter("@TypeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 250, rdbtnType.SelectedValue, typeof(string));
                        var8[33] = gb.CreateSPParameter("@KodeCc", ParameterDirection.Input, SqlDbType.VarChar, 250, kredit_cc, typeof(string));
                        //ge3.ListDataTable = gb3.GetDataJustOutPut("FA_GenerateJurnal", var7, "constrmf").Tables[0];
                        ge2.ListDataset = gb1.GetDataJustOutPut("FA_GenerateJurnal", var8, "constrmf");
                        ge3.ListDataset = gb3.GetDataJustOutPut("FA_GenerateJurnal", var7, "constrmf");



                    }
                    if (ge3.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        //ge3.ListDataset = gb3.GetDataJustOutPut("FA_GenerateJurnal", var8, "constrmf");
                        //ge3.ListDataset = gb3.GetDataJustOutPut("FA_GenerateJurnal", var7, "constrmf");
                        //ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var5, "constrmf");
                        //ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var5, "constrmf");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:simpan('Data Jurnal Berhasil Disimpan Pada Bulan Ini.');", true);

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }
                    //if(ge.ListDataTable.Rows.Count == (i-1))
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                    //}
                    //else
                    //{
                    //    //lanjut proses
                    //}
                }

            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }


    public void getdata()
    {
        DefaultClass.Global.SqlHelper[] var0 = new DefaultClass.Global.SqlHelper[6];
        var0[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
        var0[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var0[2] = gb.CreateSPParameter("@NoBuktiJurnal", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnobukti.Text, typeof(string));
        var0[3] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
        var0[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKJURNALGLTRNSM", typeof(string));
        var0[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataTable = gb.GetDataJustOutPut("FA_GenerateJurnal", var0, "constrmf").Tables[0];
        if (ge.ListDataTable.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Data Pada Bulan Ini Sudah Di Proses, Harap Hubungi Admin');", true);
            txtnobukti.Enabled = false;
            //btnSimpan.Enabled = false;		
            btnSimpan.Visible = false;

        }
        else
        {
            DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
            var2[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
            var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SENDDATA", typeof(string));
            var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            
            ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var2, "constrmf");
            //ge.ListDataTable = gb.GetDataJustOutPut("FA_GenerateJurnal", var2, "constrmf").Tables[0];
            ge.ListDataTable = ge.ListDataset.Tables[0];

            if (ge.ListDataTable.Rows.Count == 0)
                throw new Exception("No Data Found to Send");

            for (int i = 0; ge.ListDataTable.Rows.Count > i; i++)
            {
                string NoBukti = ge.ListDataset.Tables[0].Rows[i]["NoBukti"].ToString();
                string KodeAktiva = ge.ListDataset.Tables[0].Rows[i]["KodeAktiva"].ToString();
                string kdpersh = ge.ListDataset.Tables[0].Rows[i]["kdpersh"].ToString();
                string kodecabang = ge.ListDataset.Tables[0].Rows[i]["kodecabang"].ToString();
                string TglTrn = ge.ListDataset.Tables[0].Rows[i]["TglTrn"].ToString();
                string TypeTrn = ge.ListDataset.Tables[0].Rows[i]["TypeTrn"].ToString();
                string Keterangan = ge.ListDataset.Tables[0].Rows[i]["Keterangan"].ToString();
                string JlhPerolehan = ge.ListDataset.Tables[0].Rows[i]["JlhPerolehan"].ToString();
                string DelIndex = ge.ListDataset.Tables[0].Rows[i]["DelIndex"].ToString();
                string GenFlag = ge.ListDataset.Tables[0].Rows[i]["GenFlag"].ToString();
                string TrfFlag = "false"; //ge.ListDataset.Tables[0].Rows[i]["TrfFlag"].ToString();
                string TutupBukuFlag = ge.ListDataset.Tables[0].Rows[i]["TutupBukuFlag"].ToString();
                string TglEntry = ge.ListDataset.Tables[0].Rows[i]["TglEntry"].ToString();
                string KodeUser = ge.ListDataset.Tables[0].Rows[i]["KodeUser"].ToString();
                string KodeLgn = ge.ListDataset.Tables[0].Rows[i]["KodeLgn"].ToString();
                string KodeCrc = ge.ListDataset.Tables[0].Rows[i]["KodeCrc"].ToString();
                string KursStd = ge.ListDataset.Tables[0].Rows[i]["KursStd"].ToString();
                string KursPajak = ge.ListDataset.Tables[0].Rows[i]["KursPajak"].ToString();
                string Jumlah = ge.ListDataset.Tables[0].Rows[i]["Jumlah"].ToString();
                string KeteranganMemorial = ge.ListDataset.Tables[0].Rows[i]["KeteranganMemorial"].ToString();
                string AddedByFK = ge.ListDataset.Tables[0].Rows[i]["AddedByFK"].ToString();
                string DateAdded = ge.ListDataset.Tables[0].Rows[i]["DateAdded"].ToString();
                string DateModified = ge.ListDataset.Tables[0].Rows[i]["DateModified"].ToString();
                string LastUserFK = ge.ListDataset.Tables[0].Rows[i]["LastUserFK"].ToString();
                string PK = ge.ListDataset.Tables[0].Rows[i]["PK"].ToString();
                string KodeSumber = ge.ListDataset.Tables[0].Rows[i]["KodeSumber"].ToString();
                string TglMulaiSusut = ge.ListDataset.Tables[0].Rows[i]["TglMulaiSusut"].ToString();
                string TglAkhirSusut = ge.ListDataset.Tables[0].Rows[i]["TglAkhirSusut"].ToString();
                string MasaSusut = ge.ListDataset.Tables[0].Rows[i]["MasaSusut"].ToString();
                string JlhResidu = ge.ListDataset.Tables[0].Rows[i]["JlhResidu"].ToString();
                string HargaBeli = ge.ListDataset.Tables[0].Rows[i]["HargaBeli"].ToString();
                string HargaAkumulasi = ge.ListDataset.Tables[0].Rows[i]["HargaAkumulasi"].ToString();
                string HargaTambah = ge.ListDataset.Tables[0].Rows[i]["HargaTambah"].ToString();
                string HargaKurang = ge.ListDataset.Tables[0].Rows[i]["HargaKurang"].ToString();
                string TglAwalBeli = ge.ListDataset.Tables[0].Rows[i]["TglAwalBeli"].ToString();
                string TglAkhirBeli = ge.ListDataset.Tables[0].Rows[i]["TglAkhirBeli"].ToString();
                string nama_group = ge.ListDataset.Tables[0].Rows[i]["nama_group"].ToString();

                Boolean cobalagi = Convert.ToBoolean(false);
                Boolean lgiasd = Convert.ToBoolean(TrfFlag);

                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[5];
                var3[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                var3[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKGETDATA", typeof(string));
                var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var3[4] = gb.CreateSPParameter("@nama_group", ParameterDirection.Input, SqlDbType.VarChar, 50, nama_group, typeof(string));
                
                ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var3, "constrmf");
                //ge.ListDataTable = gb.GetDataJustOutPut("FA_GenerateJurnal", var3, "constrmf").Tables[0];
                ge.ListDataTable = ge.ListDataset.Tables[0];

                float GTotal = 0f;
                //double ambildebit;
                for (int j = 0; ge.ListDataTable.Rows.Count > j; j++)
                {
                    int masasusut = Convert.ToInt32(MasaSusut);
                    double rubahmasasusut = Convert.ToDouble(masasusut);
                    double rubahperolehan = Convert.ToDouble(JlhPerolehan);
                    //int jumlahperolehan = Convert.ToInt32(rubahperolehan);
                    double hitungdebit = rubahperolehan / rubahmasasusut;
                    double rubahsemnua = Math.Round(hitungdebit, 3);
                    //string sadasdasdas = Math.Ceiling(rubahsemnua).ToString();
                    //String total = Convert.ToString( rubahperolehan/masasusut);
                    //decimal total2 = Convert.ToDecimal(rubahperolehan / masasusut);
                    GTotal += Convert.ToSingle(rubahsemnua);
                }
                string rubahGtotal = Math.Ceiling(GTotal).ToString();

                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[7];
                var4[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                var4[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var4[2] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnobukti.Text, typeof(string));
                var4[3] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
                var4[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKBULANGLTRNSM", typeof(string));
                var4[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var4[6] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 20, KodeAktiva, typeof(string));

                ge.ListDataTable = gb.GetDataJustOutPut("FA_GenerateJurnal", var4, "constrmf").Tables[0];
                if (ge.ListDataTable.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Data Pada Bulan Ini Sudah Di Proses, Harap Hubungi Admin');", true);
                }
                else
                {
                    DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[33];
                    var5[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    var5[1] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 20, KodeAktiva, typeof(string));
                    var5[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var5[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    var5[4] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, TglTrn, typeof(string));
                    var5[5] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 2, KodeSumber, typeof(string));
                    var5[6] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 2, TypeTrn, typeof(string));
                    var5[7] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, Keterangan, typeof(string));
                    var5[8] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, rubahGtotal, typeof(string));
                    var5[9] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 0, rubahGtotal, typeof(string));
                    var5[10] = gb.CreateSPParameter("@DelIndex", ParameterDirection.Input, SqlDbType.VarChar, 15, DelIndex, typeof(string));
                    var5[11] = gb.CreateSPParameter("@CfFlag", ParameterDirection.Input, SqlDbType.Bit, 0, false, typeof(Boolean));
                    var5[12] = gb.CreateSPParameter("@HapusFlag", ParameterDirection.Input, SqlDbType.Bit, 0, false, typeof(Boolean));
                    var5[13] = gb.CreateSPParameter("@TrfFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(TrfFlag), typeof(Boolean));
                    var5[14] = gb.CreateSPParameter("@TutupBukuFlag", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(TutupBukuFlag), typeof(Boolean));
                    var5[15] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, TglEntry, typeof(string));
                    var5[16] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, KodeUser, typeof(string));
                    var5[17] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeLgn, typeof(string));
                    var5[18] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhir.Text, typeof(string));
                    var5[19] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 3, KodeCrc, typeof(string));
                    var5[20] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.Money, 0, KursStd, typeof(string));
                    var5[21] = gb.CreateSPParameter("@KursPakai", ParameterDirection.Input, SqlDbType.Money, 0, KursPajak, typeof(string));
                    var5[22] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, rubahGtotal, typeof(string));
                    var5[23] = gb.CreateSPParameter("@FlagHutang", ParameterDirection.Input, SqlDbType.Bit, 0, Convert.ToBoolean(GenFlag), typeof(Boolean));
                    var5[24] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 300, KeteranganMemorial, typeof(string));
                    var5[25] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var5[26] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Now, typeof(DateTime));
                    var5[27] = gb.CreateSPParameter("@DateModified", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Now, typeof(DateTime));
                    var5[28] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, LastUserFK, typeof(string));
                    var5[29] = gb.CreateSPParameter("@TFlag", ParameterDirection.Input, SqlDbType.Bit, 0, false, typeof(Boolean));
                    var5[30] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                    var5[31] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var5[32] = gb.CreateSPParameter("@TypeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 250, rdbtnType.SelectedValue, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var5, "constrmf");
                    //ge.ListDataset = gb.GetDataJustOutPut("FA_GenerateJurnal", var5, "constrmf");

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
    }
}