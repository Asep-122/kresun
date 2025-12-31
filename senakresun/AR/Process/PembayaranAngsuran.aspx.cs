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

public partial class AR_Process_PembayaranAngsuran : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string typetrn = "", keterangantrn = "",    DebitGL = "", KreditGL = "" ;
    double denda = 0;
    int totalangsuran = 0, sisapk = 0, angspokok = 0, angsbunga = 0;

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
        txtkodecabang.Text = SessBranchID;
    }

    protected void btnProses_Click(object sender, EventArgs e)
    {
        if(txtnamanasabah.Text == "" || txtnoresi.Text == "" || txtketerangan.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masih ada data yang kosong');", true);
        }
        else
        {
            string[] splitdata = txtnorekening.Text.Split('-');
            string no_rekening = splitdata[0];


            sisapk = int.Parse(HDsisapk.Value) - int.Parse(txtpokok.Text.Replace(".", ""));
            angspokok = int.Parse(txtpokok.Text.Replace(".", "")) * int.Parse(txtangske.Text); 
            angsbunga = int.Parse(txtbunga.Text.Replace(".", "")) * int.Parse(txtangske.Text);

            //Update TO TABLE ArKartuPinjaman
            DefaultClass.Global.SqlHelper[] varupdkartu = new DefaultClass.Global.SqlHelper[9];
            varupdkartu[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
            varupdkartu[1] = gb.CreateSPParameter("@angs_ke", ParameterDirection.Input, SqlDbType.VarChar, 50, txtangske.Text, typeof(string));
            varupdkartu[2] = gb.CreateSPParameter("@tgl_angs", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            varupdkartu[3] = gb.CreateSPParameter("@pkangs", ParameterDirection.Input, SqlDbType.VarChar, 50, txtpokok.Text.Replace(".", ""), typeof(string));
            varupdkartu[4] = gb.CreateSPParameter("@bng_angs", ParameterDirection.Input, SqlDbType.VarChar, 50, txtbunga.Text.Replace(".", ""), typeof(string));
            varupdkartu[5] = gb.CreateSPParameter("@denda", ParameterDirection.Input, SqlDbType.VarChar, 50, txtdenda.Text.Replace(".", ""), typeof(string)); 
            varupdkartu[6] = gb.CreateSPParameter("@no_tagihan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoresi.Text, typeof(string));
            varupdkartu[7] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEKARTU", typeof(string));
            varupdkartu[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_KARTUPINJAMAN", varupdkartu, "constrmf");

            //Update TO TABLE ArMasterKredit
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[7];
            var1[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
            var1[1] = gb.CreateSPParameter("@angspokok", ParameterDirection.Input, SqlDbType.VarChar, 50, angspokok, typeof(string));
            var1[2] = gb.CreateSPParameter("@angsbunga", ParameterDirection.Input, SqlDbType.VarChar, 250, angsbunga, typeof(string));
            var1[3] = gb.CreateSPParameter("@angs_ke", ParameterDirection.Input, SqlDbType.VarChar, 50, txtangske.Text, typeof(string));
            var1[4] = gb.CreateSPParameter("@bakidb", ParameterDirection.Input, SqlDbType.VarChar, 50, sisapk, typeof(string));
            var1[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEMASTERKREDIT", typeof(string));
            var1[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var1, "constrmf");

            //Select TypeTrn TO TABLE suTypeAr
            DefaultClass.Global.SqlHelper[] varsutype = new DefaultClass.Global.SqlHelper[3];
            varsutype[0] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, "C", typeof(string));
            varsutype[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            varsutype[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_SuTypeAr", varsutype, "constrmf");
             
            typetrn = ge.ListDataset.Tables[0].Rows[0]["TypeTrn"].ToString();
            keterangantrn = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();

            //INSERT INTO ArTrn
            DefaultClass.Global.SqlHelper[] vartrn = new DefaultClass.Global.SqlHelper[34];
            vartrn[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, HDnonasabah.Value, typeof(string));
            vartrn[1] = gb.CreateSPParameter("@NoFaktur", ParameterDirection.Input, SqlDbType.VarChar, 250, no_rekening, typeof(string));
            vartrn[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string));
            vartrn[3] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoresi.Text, typeof(string));
            vartrn[4] = gb.CreateSPParameter("@TglTrn ", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            vartrn[5] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, HDtgl_jwl.Value, typeof(string));
            //vartrn[6] = gb.CreateSPParameter("@TglTrnFaktur ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[7] = gb.CreateSPParameter("@TglJtpFaktur ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[8] = gb.CreateSPParameter("@BankGiro ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[9] = gb.CreateSPParameter("@Nogiro ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[10] = gb.CreateSPParameter("@Statusgiro ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            vartrn[6] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, HDnama_staff.Value, typeof(string));
            vartrn[7] = gb.CreateSPParameter("@Keterangan ", ParameterDirection.Input, SqlDbType.VarChar, 250, "Angsuran Pinjaman " + no_rekening + "." + HDnama_nasabah.Value + ". Angs ke : " + txtangske.Text + " s.d ke " + txtangske.Text + "No Resi : " + txtnoresi.Text + " | " + txtangske.Text, typeof(string));
            vartrn[8] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, txttotal.Text.Replace(".",""), typeof(string));
            vartrn[9] = gb.CreateSPParameter("@TglEntry ", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now.ToString("yyyy-MM-dd"), typeof(string));
            vartrn[10] = gb.CreateSPParameter("@Kodeuser ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            vartrn[11] = gb.CreateSPParameter("@Kodesumber ", ParameterDirection.Input, SqlDbType.VarChar, 250, "MK", typeof(string));
            vartrn[12] = gb.CreateSPParameter("@KodeJurnal ", ParameterDirection.Input, SqlDbType.VarChar, 250, "ARMK01", typeof(string)); //DEFAULT SEMENTARA
            //vartrn[18] = gb.CreateSPParameter("@DelIndex ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            vartrn[13] = gb.CreateSPParameter("@TrfFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[14] = gb.CreateSPParameter("@BayarFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[15] = gb.CreateSPParameter("@HapusFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[16] = gb.CreateSPParameter("@TutupBukuFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[17] = gb.CreateSPParameter("@UangMukaFlag ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[18] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[19] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[20] = gb.CreateSPParameter("@KursAkhir", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[21] = gb.CreateSPParameter("@KursLama ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[22] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            vartrn[23] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now.ToString("yyyy-MM-dd"), typeof(string));
            //vartrn[30] = gb.CreateSPParameter("@DateModified ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[31] = gb.CreateSPParameter("@LastUserFK ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            vartrn[24] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            vartrn[25] = gb.CreateSPParameter("@TFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[26] = gb.CreateSPParameter("@Pk ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[27] = gb.CreateSPParameter("@OverDue", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            vartrn[28] = gb.CreateSPParameter("@RencanaPembayaran", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeProduk.Value, typeof(string));
            //vartrn[37] = gb.CreateSPParameter("@REMARK ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[38] = gb.CreateSPParameter("@TglUpdate", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[39] = gb.CreateSPParameter("@PKPort ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[40] = gb.CreateSPParameter("@@TypeTrnRegister ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[41] = gb.CreateSPParameter("@PKVessel ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[42] = gb.CreateSPParameter("@IdAgency ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[43] = gb.CreateSPParameter("@Timearrival", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[44] = gb.CreateSPParameter("@timedeparture", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[45] = gb.CreateSPParameter("@voyagenumber ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[46] = gb.CreateSPParameter("@Commodity", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            vartrn[29] = gb.CreateSPParameter("@Signatory", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[48] = gb.CreateSPParameter("@RegisterNumber ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[49] = gb.CreateSPParameter("@IdPrincipal", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            vartrn[30] = gb.CreateSPParameter("@RegisterName ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamanasabah.Text, typeof(string));
            vartrn[31] = gb.CreateSPParameter("@TypeAR ", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string));
            vartrn[32] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            vartrn[33] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
             
            ge.ListDataset = gb.GetDataJustOutPut("AR_TRN", vartrn, "constrmf");
            
            //Update TO TABLE ArMst
            DefaultClass.Global.SqlHelper[] varmst = new DefaultClass.Global.SqlHelper[4];
            varmst[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, HDnonasabah.Value, typeof(string));
            varmst[1] = gb.CreateSPParameter("@SaldoJalan", ParameterDirection.Input, SqlDbType.Money, 50, sisapk, typeof(string)); 
            varmst[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEFORPEMBAYARANANGSURAN", typeof(string));
            varmst[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_ArMst", varmst, "constrmf");

            //SELECT TO TABLE CLJurnal
            DefaultClass.Global.SqlHelper[] varcl = new DefaultClass.Global.SqlHelper[3];
            varcl[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, "CL.Mikro", typeof(string)); 
            varcl[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDJURNAL", typeof(string));
            varcl[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", varcl, "constrmf");

            DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGl"].ToString();
            KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGl"].ToString();

            //INSERT INTO gltrnsm Debit
            DefaultClass.Global.SqlHelper[] vartrnsm = new DefaultClass.Global.SqlHelper[16];
            vartrnsm[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoresi.Text, typeof(string));
            vartrnsm[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            vartrnsm[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, DebitGL, typeof(string));
            vartrnsm[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string));
            vartrnsm[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Angsuran Pinjaman " + no_rekening + "." + HDnama_nasabah.Value + ". Angs ke : " + txtangske.Text + " s.d ke " + txtangske.Text + "No Resi : " + txtnoresi.Text + " | " + txtangske.Text, typeof(string));
            vartrnsm[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 250, txttotal.Text.Trim().Replace(".", ""), typeof(string)); //jmldebit
            vartrnsm[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 250, "0", typeof(string)); //jmlkredit
            vartrnsm[7] = gb.CreateSPParameter("@KodeSupplier", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnorekening.Text, typeof(string));
            vartrnsm[8] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            vartrnsm[9] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Angsuran Pinjaman " + no_rekening + "." + HDnama_nasabah.Value + ". Angs ke : " + txtangske.Text + " s.d ke " + txtangske.Text + "No Resi : " + txtnoresi.Text + " | " + txtangske.Text, typeof(string));
            vartrnsm[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
            vartrnsm[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            vartrnsm[12] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            vartrnsm[13] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            vartrnsm[14] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            vartrnsm[15] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AP_Transaksi", vartrnsm, "constrmf");

            //INSERT INTO gltrnsm Kredit
            DefaultClass.Global.SqlHelper[] varkredit = new DefaultClass.Global.SqlHelper[16];
            varkredit[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoresi.Text, typeof(string));
            varkredit[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            varkredit[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, DebitGL, typeof(string));
            varkredit[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string));
            varkredit[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Angsuran Pinjaman " + no_rekening.Trim() + "." + HDnama_nasabah.Value.Trim() + ". Angs ke : " + txtangske.Text.Trim() + " s.d ke " + txtangske.Text.Trim() + "No Resi : " + txtnoresi.Text.Trim() + " | " + txtangske.Text.Trim(), typeof(string));
            varkredit[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 250, "0", typeof(string)); //jmldebit
            varkredit[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 250, txttotal.Text.Trim().Replace(".", ""), typeof(string)); //jmlkredit
            varkredit[7] = gb.CreateSPParameter("@KodeSupplier", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnorekening.Text, typeof(string));
            varkredit[8] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            varkredit[9] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Angsuran Pinjaman " + no_rekening + "." + HDnama_nasabah.Value + ". Angs ke : " + txtangske.Text + " s.d ke " + txtangske.Text + "No Resi : " + txtnoresi.Text + " | " + txtangske.Text, typeof(string));
            varkredit[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
            varkredit[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varkredit[12] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            varkredit[13] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            varkredit[14] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            varkredit[15] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AP_Transaksi", varkredit, "constrmf"); 

            if(int.Parse(txtangske.Text) == int.Parse(HDJangka.Value))
            {
                //Update TO TABLE ArMasterKredit Jika Angsuran sudah lunas
                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                var2[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string)); 
                var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATELUNAS", typeof(string));
                var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var2, "constrmf");
                
            }

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
    }

        
    protected void btnSearch_Click(object sender, EventArgs e)
    { 

        string[] splitdata = txtnorekening.Text.Split('-');
        string no_rekening = splitdata[0];


        if (txtnorekening.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Rekening belum di isi');", true);

        }
        else if (txtkodecabang.Text != txtnorekening.Text.Substring(0, 3))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Rekening yang di input tidak terdapat di cabang Anda');", true);

        }
       
        else
        {
            pnladd.Visible = true;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORPEMBAYARANANGSURAN", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_KARTUPINJAMAN", var, "constrmf");

            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak ditemukan!');", true);

            }
            else
            {
                 
            

                //Panel Data nasabah 1
                txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["picb"].ToString();
                txtangske.Text = ge.ListDataset.Tables[0].Rows[0]["Angs_ke"].ToString();
                txtpokok.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString()).Replace("Rp", "");
                txtbunga.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()).Replace("Rp", "");
                HDsisapk.Value = ge.ListDataset.Tables[0].Rows[0]["sisapk"].ToString();
                HDnonasabah.Value = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                HDselisihdenda.Value = ge.ListDataset.Tables[0].Rows[0]["tglhitungdenda"].ToString();
                HDtgl_jwl.Value = ge.ListDataset.Tables[0].Rows[0]["tgl_jwl"].ToString();
                HDnama_nasabah.Value = ge.ListDataset.Tables[0].Rows[0]["picb"].ToString();
                HDnama_staff.Value = ge.ListDataset.Tables[0].Rows[0]["kode_staff"].ToString();
                HDKodeProduk.Value = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                HDJangka.Value = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();

                if (DateTime.Now.ToString().CompareTo(HDselisihdenda.Value) > 0)
                {
                    denda = double.Parse(ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString()) + double.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                    txtdenda.Text = denda.ToString();
                    txtdenda.Text = gb.ToRupiah(txtdenda.Text).Replace("Rp", "");
                }
                else
                {
                    txtdenda.Text = "0";
                }

                totalangsuran = int.Parse(ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString()) + int.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()) + int.Parse(denda.ToString());

                txttotal.Text = gb.ToRupiah(totalangsuran.ToString()).Replace("Rp","");

            }
        }
    }
    
}