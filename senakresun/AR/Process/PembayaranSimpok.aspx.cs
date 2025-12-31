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

public partial class AR_Process_PembayaranSimpok : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    long no_bukti_sekarang = 0, no_bukti = 0;
    string no_bukti_sebelumnya = "", NoBuktiNew = "", formatnumberbukti = "",DebitGL = "", KreditGL1 = "", KreditGL2 = "";

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    } 
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
 
    public void btnSearch_Click(object sender, EventArgs e)
    {
         

        if (SessBranchID != txtnonasabah.Text.Substring(0, 3))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah yang di input tidak terdapat di cabang Anda');", true);

        }
        else
        {
            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
            var3[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnonasabah.Text, typeof(string));
            var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKEANGGOTAAN", typeof(string));
            var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var3, "constrmf");

            if(ge.ListDataset.Tables[0].DefaultView.Count != 0)
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnonasabah.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNONASABAH", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");

                txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                HDNoRekTab.Value = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                //btnSimpan.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Nasabah yang di input sudah melakukan pembayaran simpok');", true);

            }

        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        //GET NOMER BUKTI

        //DefaultClass.Global.SqlHelper[] varbukti = new DefaultClass.Global.SqlHelper[4];
        //varbukti[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //varbukti[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //varbukti[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKONTERNOBUKTI", typeof(string));
        //varbukti[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", varbukti, "constrmf");

        //no_bukti_sekarang = long.Parse(ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString());
        //formatnumberbukti = ge.ListDataset.Tables[0].Rows[0]["NoAwal"].ToString();
        //no_bukti_sebelumnya = ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString();

        //no_bukti = no_bukti_sekarang + 1;
         
        //NoBuktiNew = String.Format("{0:" + formatnumberbukti + "}", no_bukti);

        //HDNoBuktiSekarang.Value = NoBuktiNew;
        //HDNoBuktiSebelumnya.Value = no_bukti_sebelumnya;


        //Select TypeTrn TO TABLE suTypeAr
        DefaultClass.Global.SqlHelper[] varsutype = new DefaultClass.Global.SqlHelper[3];
        varsutype[0] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, "V", typeof(string));
        varsutype[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        varsutype[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_SuTypeAr", varsutype, "constrmf");


        HDtypetrn.Value = ge.ListDataset.Tables[0].Rows[0]["TypeTrn"].ToString();
        HDketerangantrn.Value = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();


        //Select Kode Bank TO TABLE CL_Mst
        DefaultClass.Global.SqlHelper[] varclmst = new DefaultClass.Global.SqlHelper[4];
        varclmst[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varclmst[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        varclmst[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTAR", typeof(string));
        varclmst[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", varclmst, "constrmf");
         
        HDKodeBank.Value = ge.ListDataset.Tables[0].Rows[0]["KodeBank"].ToString(); 

        
    }

    public void btnSimpan_Click(object sender, EventArgs e)
    { 
        //UPDATE TO ARMASTERTABUNGAN
        DefaultClass.Global.SqlHelper[] varupdate = new DefaultClass.Global.SqlHelper[6];
        varupdate[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoRekTab.Value, typeof(string));
        varupdate[1] = gb.CreateSPParameter("@saldo_akhir", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjumlah.Text, typeof(string));
        varupdate[2] = gb.CreateSPParameter("@saldo_awal_hari", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjumlah.Text, typeof(string));
        varupdate[3] = gb.CreateSPParameter("@mutasi_debet", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjumlah.Text, typeof(string));
        varupdate[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATE", typeof(string));
        varupdate[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_TABUNGAN", varupdate, "constrmf");

        //UPDATE TO ARAnggota
        DefaultClass.Global.SqlHelper[] varanggota = new DefaultClass.Global.SqlHelper[3];
        varanggota[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnonasabah.Text, typeof(string));
        varanggota[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATESTATUSSETORANWAJIB", typeof(string));
        varanggota[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", varanggota, "constrmf");
        
 

        //Select TypeTrn TO TABLE suTypeMikro
        DefaultClass.Global.SqlHelper[] varsutype = new DefaultClass.Global.SqlHelper[2];
        varsutype[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORJURNALSIMPOK", typeof(string));
        varsutype[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_JOURNAL", varsutype, "constrmf");

        DebitGL = ge.ListDataset.Tables[0].Rows[0]["debit1"].ToString();
        KreditGL1 = ge.ListDataset.Tables[0].Rows[0]["kredit1"].ToString();
        KreditGL2 = ge.ListDataset.Tables[0].Rows[0]["kredit2"].ToString();

        //INSERT INTO gltrnsm DEBIT KAS

        DefaultClass.Global.SqlHelper[] varkas = new DefaultClass.Global.SqlHelper[16];
        varkas[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
        varkas[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
        varkas[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, DebitGL, typeof(string));
        varkas[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, "A4", typeof(string));
        varkas[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Simpanan Pokok", typeof(string));
        varkas[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 250, "20000", typeof(string)); //jmldebit
        varkas[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 250, "0", typeof(string)); //jmlkredit 
        varkas[7] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
        varkas[8] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pembayaran SimPok No. Nasabah : " + txtnonasabah.Text, typeof(string));
        varkas[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));  
        varkas[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varkas[11] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
        varkas[12] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
        varkas[13] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
        varkas[14] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
        varkas[15] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_JURNAL_MIKRO", varkas, "constrmf");

        //INSERT INTO gltrnsm KREDIT Simpok
        DefaultClass.Global.SqlHelper[] varsimpok = new DefaultClass.Global.SqlHelper[16];
        varsimpok[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
        varsimpok[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
        varsimpok[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, KreditGL1, typeof(string));
        varsimpok[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, "A4", typeof(string));
        varsimpok[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Simpanan Pokok", typeof(string));
        varsimpok[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 250, "0", typeof(string)); //jmldebit
        varsimpok[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 250, "10000", typeof(string)); //jmlkredit 
        varsimpok[7] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
        varsimpok[8] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pembayaran SimPok No. Nasabah : " + txtnonasabah.Text, typeof(string));
        varsimpok[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
        varsimpok[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varsimpok[11] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
        varsimpok[12] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
        varsimpok[13] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
        varsimpok[14] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
        varsimpok[15] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_JURNAL_MIKRO", varsimpok, "constrmf");

        //INSERT INTO gltrnsm KREDIT Materai
        DefaultClass.Global.SqlHelper[] varsimpang = new DefaultClass.Global.SqlHelper[16];
        varsimpang[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
        varsimpang[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
        varsimpang[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, KreditGL2, typeof(string));
        varsimpang[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, "A1", typeof(string));
        varsimpang[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Simpanan Anggota", typeof(string));
        varsimpang[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 250, "0", typeof(string)); //jmldebit
        varsimpang[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 250, "10000", typeof(string)); //jmlkredit 
        varsimpang[7] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
        varsimpang[8] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pembayaran SimPok No. Nasabah : " + txtnonasabah.Text, typeof(string));
        varsimpang[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
        varsimpang[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varsimpang[11] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
        varsimpang[12] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
        varsimpang[13] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
        varsimpang[14] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
        varsimpang[15] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_JURNAL_MIKRO", varsimpang, "constrmf");

        DefaultClass.Global.SqlHelper[] varSTATE = new DefaultClass.Global.SqlHelper[4];
        varSTATE[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
        varSTATE[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
        varSTATE[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "UPDATENOMERBUKTI", typeof(string));
        varSTATE[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", varSTATE, "constrmf");

        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
            txtnonasabah.Text = "";
            txtnamanasabah.Text = "";
            txtjumlah.Text = "";

        }
        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
}