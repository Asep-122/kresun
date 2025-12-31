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

public partial class AR_Process_ApprovalPinjamanUlangCabang : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string id = "", no_nasabah = "", flag = "";

    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApprovalPinjamanUlangCabang.aspx");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

    }
    protected void btnNextPekerjaan_Click(object sender, EventArgs e)
    {

        pnladd.Visible = false;



    }

    protected void btnNextTambahan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

    }
    protected void btnNextPendapatan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        //pnlLaba.Visible = true;

        pnladd.Visible = false;

    }
    protected void btnBackIdentitas_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;

    }
    protected void btnNexIdentitas_Click(object sender, EventArgs e)
    {


    }
    protected void btnBackPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

    }
    protected void btnNextPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;




    }
    protected void btnNextJaminan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        pnlPengajuan.Visible = true;
 

    }
    protected void btnBackJaminan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        pnlPengajuan.Visible = false;


    }

    protected void btnNextPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
     
        //pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
     
    }

    protected void btnbackawal_Click(object sender, EventArgs e)
    {
        pnlFilter.Visible = true;
        PnlFront.Visible = true;
        pnladd.Visible = false;
    }
    protected void btnBackPekerjaan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;


    }

    protected void btnBackTambahan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;


    }
    protected void btnBackPendapatan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

    }

    protected void btnBackPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;


        pnlPengajuan.Visible = false;
        //pnlKeanggotaan.Visible = false;

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        //pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        //pnlUsaha.Visible = false;
    }


    protected void btnBackTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
          //pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = false;
        //pnlUsaha.Visible = true;
        pnlAlasanTolak.Visible = false;
        //ProsesVerifikasi("0", SessLoginID);
        pnltrn.Visible = true;
        Pndetail.Visible = true;
        Pnlfooter.Visible = true;
    }

    protected void btnTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
          //pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        //pnlKeanggotaan.Visible = false;
        //pnlUsaha.Visible = false;
        pnlAlasanTolak.Visible = true;

        pnltrn.Visible = false;
        Pndetail.Visible = false;
        Pnlfooter.Visible = false;
    }

    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        try
        {

            btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[9];
            var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "2", typeof(string));
            var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
            var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "2", typeof(string));
            var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REJECTSTATUSKEM", typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");


            //DefaultClass.Global.SqlHelper[] var77 = new DefaultClass.Global.SqlHelper[5];
            //var77[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            //var77[1] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            //var77[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            //var77[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEPEMBATALAN", typeof(string));
            //var77[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var77, "constrmf");


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

    protected void btnSetuju_Click(object sender, EventArgs e)
    {
        ProsesApprovalKredit("3", SessLoginID);
    }

    public void ProsesApprovalKredit(string flag, string user)
    {

        try

        {

            btnSetuju.Enabled = false;
            //bunga = gb.GetNominalBunga(Double.Parse(txtbunga.Text.Trim().Replace(".", ",")), int.Parse(txtjangka.Text), double.Parse(txtplafon.Text), txtjeniskredit.Text);

            //ttlbunga = double.Parse(bunga) * double.Parse(txtjangka.Text);

            //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[172];

            //var1[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //var1[1] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[2] = gb.CreateSPParameter("@no_rekening_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "-",typeof(string));
            //var1[3] = gb.CreateSPParameter("@no_rek_tabungan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnorektabungan.Text, typeof(string));
            //var1[4] = gb.CreateSPParameter("@no_rek_tabungan_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[5] = gb.CreateSPParameter("@no_rek_giro", ParameterDirection.Input, SqlDbType.VarChar, 250 , "-", typeof(string));
            //var1[6] = gb.CreateSPParameter("@no_karip", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[7] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
            //var1[8] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjeniskredit.Text, typeof(string));
            //var1[9] = gb.CreateSPParameter("@kode_pengguna", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenispenggunaan.Text.Trim(), typeof(string));
            //var1[10] = gb.CreateSPParameter("@rek_lama", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[11] = gb.CreateSPParameter("@rek_baru", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[12] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //var1[13] = gb.CreateSPParameter("@kddapem", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[14] = gb.CreateSPParameter("@jns_pen", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[15] = gb.CreateSPParameter("@no_spk", ParameterDirection.Input, SqlDbType.VarChar, 50, HDnopk.Value, typeof(string));
            //var1[16] = gb.CreateSPParameter("@no_aksep", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[17] = gb.CreateSPParameter("@rek_tablm", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[18] = gb.CreateSPParameter("@rek_tabbr", ParameterDirection.Input, SqlDbType.VarChar, 50, txtangsuranperbulan.Text.Trim(), typeof(string));
            //var1[19] = gb.CreateSPParameter("@prblkr", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox7.Text.Trim(), typeof(string)); //
            //var1[20] = gb.CreateSPParameter("@tgl_drop", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
            //var1[21] = gb.CreateSPParameter("@tgl_due", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
            //var1[22] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjangka.Text.Trim(), typeof(string));
            //var1[23] = gb.CreateSPParameter("@satuan", ParameterDirection.Input, SqlDbType.VarChar, 250, flag, typeof(string)); //
            //var1[24] = gb.CreateSPParameter("@seqno", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[25] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text.Trim(), typeof(string));
            //var1[26] = gb.CreateSPParameter("@sld_awl", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[27] = gb.CreateSPParameter("@debet", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[28] = gb.CreateSPParameter("@kredit", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[29] = gb.CreateSPParameter("@bunga_flat", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbunga.Text.Trim(), typeof(string));
            //var1[30] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbunga.Text.Trim(), typeof(string)); //
            //var1[31] = gb.CreateSPParameter("@jns_bunga", ParameterDirection.Input, SqlDbType.VarChar, 250, flag, typeof(string)); //
            //var1[32] = gb.CreateSPParameter("@byprov", ParameterDirection.Input, SqlDbType.VarChar, 250, txtProvisi.Text.Trim(), typeof(string));
            //var1[33] = gb.CreateSPParameter("@oscprov", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnominalprovisi.Text.Trim(), typeof(string));
            //var1[34] = gb.CreateSPParameter("@byadm", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[35] = gb.CreateSPParameter("@denda", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[36] = gb.CreateSPParameter("@ngkdenda", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[37] = gb.CreateSPParameter("@bytab", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[38] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbymaterai.Text.Trim(), typeof(string));
            //var1[39] = gb.CreateSPParameter("@taksasi", ParameterDirection.Input, SqlDbType.VarChar, 250, txttotalbiaya.Text.Trim(), typeof(string));
            //var1[40] = gb.CreateSPParameter("@kd_area", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeArea.Value, typeof(string));
            //var1[41] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
            //var1[42] = gb.CreateSPParameter("@tab_drop", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[43] = gb.CreateSPParameter("@tab_angs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[44] = gb.CreateSPParameter("@sifat", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsifat.Text, typeof(string));
            //var1[45] = gb.CreateSPParameter("@sek_ekon", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsektorekonomi.Text, typeof(string));
            //var1[46] = gb.CreateSPParameter("@kd_goldb", ParameterDirection.Input, SqlDbType.VarChar, 250, txtgolongandebitur.Text, typeof(string));
            //var1[47] = gb.CreateSPParameter("@nm_pnjmn", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox13.Text, typeof(string));
            //var1[48] = gb.CreateSPParameter("@golpjmn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtgolonganpenjamin.Text, typeof(string));
            //var1[49] = gb.CreateSPParameter("@nl_jmn", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox7.Text, typeof(string));
            //var1[50] = gb.CreateSPParameter("@kd_fisik", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfisikjaminan.Text, typeof(string));
            //var1[51] = gb.CreateSPParameter("@ttdjmn", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[52] = gb.CreateSPParameter("@ket_jmn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketeranganJaminan.Text, typeof(string));
            //var1[53] = gb.CreateSPParameter("@kd_ktr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); //
            //var1[54] = gb.CreateSPParameter("@ttlpokok", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text.Trim(), typeof(string));
            //var1[55] = gb.CreateSPParameter("@ttlbunga", ParameterDirection.Input, SqlDbType.VarChar, 250, ttlbunga, typeof(string));
            //var1[56] = gb.CreateSPParameter("@angspokok", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[57] = gb.CreateSPParameter("@angsbunga", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); 
            //var1[58] = gb.CreateSPParameter("@bakidb", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text.Trim(), typeof(string));
            //var1[59] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 250, HDStatusDebitur.Value, typeof(string));
            //var1[60] = gb.CreateSPParameter("@droping", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string)); //
            //var1[61] = gb.CreateSPParameter("@angs_ke", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string));
            //var1[62] = gb.CreateSPParameter("@sklsppk", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string)); // 
            //var1[63] = gb.CreateSPParameter("@byrbunga", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string)); //
            //var1[64] = gb.CreateSPParameter("@picb", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamanasabah.Text, typeof(string));
            //var1[65] = gb.CreateSPParameter("@gender", ParameterDirection.Input, SqlDbType.VarChar, 250, HDGender.Value, typeof(string)); 
            //var1[66] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsumberdana.Text, typeof(string));
            //var1[67] = gb.CreateSPParameter("@kcab", ParameterDirection.Input, SqlDbType.VarChar, 250, "000", typeof(string));
            //var1[68] = gb.CreateSPParameter("@channeling", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[69] = gb.CreateSPParameter("@nmlbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[70] = gb.CreateSPParameter("@nmlbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[71] = gb.CreateSPParameter("@oscbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[72] = gb.CreateSPParameter("@oscbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[73] = gb.CreateSPParameter("@prbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[74] = gb.CreateSPParameter("@prbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[75] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[76] = gb.CreateSPParameter("@tgl_sale", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[77] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
            //var1[78] = gb.CreateSPParameter("@act_rra", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[79] = gb.CreateSPParameter("@act_rrp", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[80] = gb.CreateSPParameter("@jkc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[81] = gb.CreateSPParameter("@bgc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[82] = gb.CreateSPParameter("@jbgc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[83] = gb.CreateSPParameter("@no_bukti", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[84] = gb.CreateSPParameter("@no_acc", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[85] = gb.CreateSPParameter("@angspbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); 
            //var1[86] = gb.CreateSPParameter("@angspbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); 
            //var1[87] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 250, txtprogrampinjaman.Text, typeof(string));
            //var1[88] = gb.CreateSPParameter("@kol", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string));
            //var1[89] = gb.CreateSPParameter("@arrear", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[90] = gb.CreateSPParameter("@ttypokok", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[91] = gb.CreateSPParameter("@ttybunga", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[92] = gb.CreateSPParameter("@tgkhari", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[93] = gb.CreateSPParameter("@postgk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[94] = gb.CreateSPParameter("@pkbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[95] = gb.CreateSPParameter("@bgbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[96] = gb.CreateSPParameter("@cadangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[97] = gb.CreateSPParameter("@rekadm", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[98] = gb.CreateSPParameter("@spv", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string));
            //var1[99] = gb.CreateSPParameter("@prkomisi", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[100] = gb.CreateSPParameter("@rekkomisi", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[101] = gb.CreateSPParameter("@actkomisi", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[102] = gb.CreateSPParameter("@actntrs", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[103] = gb.CreateSPParameter("@ntrs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[104] = gb.CreateSPParameter("@actasrn", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[105] = gb.CreateSPParameter("@asrn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbyasuransi.Text.Trim(), typeof(string)); //
            //var1[106] = gb.CreateSPParameter("@prjrbyr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[107] = gb.CreateSPParameter("@rekjrbyr", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[108] = gb.CreateSPParameter("@actjrbyr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); 
            //var1[109] = gb.CreateSPParameter("@nmkel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNamaKeluarga.Value, typeof(string));
            //var1[110] = gb.CreateSPParameter("@stskel", ParameterDirection.Input, SqlDbType.VarChar, 50, HDHubunganKeluarga.Value, typeof(string));
            //var1[111] = gb.CreateSPParameter("@pkjkel", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[112] = gb.CreateSPParameter("@almkel", ParameterDirection.Input, SqlDbType.VarChar, 50, HDAlamatKeluarga.Value, typeof(string));
            //var1[113] = gb.CreateSPParameter("@klrkel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKelurahanKeluarga.Value, typeof(string));
            //var1[114] = gb.CreateSPParameter("@keckel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKecamatanKeluarga.Value, typeof(string));
            //var1[115] = gb.CreateSPParameter("@kotkel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKotaKeluarga.Value.Trim(), typeof(string));
            //var1[116] = gb.CreateSPParameter("@nmpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[117] = gb.CreateSPParameter("@almpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[118] = gb.CreateSPParameter("@klrpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[119] = gb.CreateSPParameter("@kecpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[120] = gb.CreateSPParameter("@kotpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[121] = gb.CreateSPParameter("@pkjpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[122] = gb.CreateSPParameter("@nmpjmn2", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox13.Text, typeof(string));
            //var1[123] = gb.CreateSPParameter("@stspjmn2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[124] = gb.CreateSPParameter("@pkjpjmn2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[125] = gb.CreateSPParameter("@tmp_lahir", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[126] = gb.CreateSPParameter("@tgl_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[127] = gb.CreateSPParameter("@tglproses", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[128] = gb.CreateSPParameter("@tglupd", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
            //var1[129] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //var1[130] = gb.CreateSPParameter("@status_pelunasan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[131] = gb.CreateSPParameter("@jns_pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjenispinjaman.Text, typeof(string));// 
            //var1[132] = gb.CreateSPParameter("@tgl_drop_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[133] = gb.CreateSPParameter("@tgl_due_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[134] = gb.CreateSPParameter("@kol_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[135] = gb.CreateSPParameter("@arrear_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[136] = gb.CreateSPParameter("@sandi_agunan", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[137] = gb.CreateSPParameter("@dati2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[138] = gb.CreateSPParameter("@bagian_dijamin", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[139] = gb.CreateSPParameter("@no_rek_awal", ParameterDirection.Input, SqlDbType.VarChar, 250, HDno_rekening.Value, typeof(string));
            //var1[140] = gb.CreateSPParameter("@flag_proses_aba", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[141] = gb.CreateSPParameter("@noakad_ijarok", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[142] = gb.CreateSPParameter("@noakad_murabahah", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[143] = gb.CreateSPParameter("@noakad_qard", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[144] = gb.CreateSPParameter("@m_produk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[145] = gb.CreateSPParameter("@flag_cetak_anggota", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[146] = gb.CreateSPParameter("@report_dod", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[147] = gb.CreateSPParameter("@dod", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[148] = gb.CreateSPParameter("@user_dod", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[149] = gb.CreateSPParameter("@flag_dod", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[150] = gb.CreateSPParameter("@no_fidusia", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[151] = gb.CreateSPParameter("@input_pembiayaan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[152] = gb.CreateSPParameter("@no_adendum", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[153] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[154] = gb.CreateSPParameter("@asuransi", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[155] = gb.CreateSPParameter("@no_asuransi", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[156] = gb.CreateSPParameter("@asuransi_sebelum", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[157] = gb.CreateSPParameter("@no_asuransi_sebelum", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[158] = gb.CreateSPParameter("@keterangan1", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[159] = gb.CreateSPParameter("@kode_travel", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[160] = gb.CreateSPParameter("@jns_blokir", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[161] = gb.CreateSPParameter("@rek_lain", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[162] = gb.CreateSPParameter("@kd_reff2", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[163] = gb.CreateSPParameter("@kode_siskohat", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[164] = gb.CreateSPParameter("@id_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 50, txtktppenjamin.Text, typeof(string));
            //var1[165] = gb.CreateSPParameter("@by_cadangan", ParameterDirection.Input, SqlDbType.VarChar, 50, TextBox3.Text, typeof(string));
            //var1[166] = gb.CreateSPParameter("@angsbbpr", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string));
            //var1[167] = gb.CreateSPParameter("@angsbbank", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string)); 
            //var1[168] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            //var1[169] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var1[170] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID , typeof(string));
            //var1[171] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh , typeof(string));

            ////save ke table ArMasterKredit
            //ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var1, "constrmf");


            //for (i = 0; i < int.Parse(txtjangka.Text.Trim()); i++)
            //{
            //    if (i == 0)
            //    {
            //        tgl_realisasi = DateTime.Parse(txttglrealisasi.Text);
            //        tgl_libur = tgl_realisasi.AddDays(7);
            //    }
            //    else
            //    {
            //        tgl_libur = tgldrop.AddDays(7);
            //    }

            //    DefaultClass.Global.SqlHelper[] var9 = new DefaultClass.Global.SqlHelper[3];
            //    var9[0] = gb.CreateSPParameter("@tglLibur", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tgl_libur, typeof(DateTime));
            //    var9[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            //    var9[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //    ge.ListDataset = gb.GetDataJustOutPut("GetTglLibur", var9, "constrmf");

            //    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            //    {
            //        tgldrop = tgl_libur;

            //    }
            //    else
            //    {
            //        tgldrop = tgl_libur.AddDays(7);

            //    }

            //    x = i + 1;

            //    pokok = gb.GetPokok(int.Parse(txtjangka.Text), double.Parse(txtplafon.Text));
            //    bunga = gb.GetNominalBunga(Double.Parse(txtbunga.Text.Trim().Replace(".", ",")), int.Parse(txtjangka.Text), double.Parse(txtplafon.Text), txtjeniskredit.Text);

            //    double pembulatanbunga = double.Parse(bunga);
            //    pembulatanbunga = Math.Round(pembulatanbunga);

            //    if(i == 0)
            //    {
            //        sisapk = int.Parse(txtplafon.Text);
            //    }
            //    else
            //    {
            //        sisapk = int.Parse(txtplafon.Text) - ((x - 1) * int.Parse(pokok));
            //    }


            //    DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[55];
            //    var4[0] = gb.CreateSPParameter("@status_pembayaran", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //    var4[2] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //    var4[3] = gb.CreateSPParameter("@no_rekening_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //    var4[4] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjeniskredit.Text, typeof(string));
            //    var4[5] = gb.CreateSPParameter("@angs_ke", ParameterDirection.Input, SqlDbType.VarChar, 250, x, typeof(string));
            //    var4[6] = gb.CreateSPParameter("@tgl_jwl", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgldrop, typeof(string));
            //    var4[7] = gb.CreateSPParameter("@pokok", ParameterDirection.Input, SqlDbType.VarChar, 250, pokok, typeof(string));
            //    var4[8] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.VarChar, 250, bunga, typeof(string));
            //    var4[9] = gb.CreateSPParameter("@tgl_angs", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[10] = gb.CreateSPParameter("@pkangs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[11] = gb.CreateSPParameter("@bng_angs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[12] = gb.CreateSPParameter("@denda", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[13] = gb.CreateSPParameter("@tgl", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[14] = gb.CreateSPParameter("@tgl_prov", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[15] = gb.CreateSPParameter("@provisi", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[16] = gb.CreateSPParameter("@arrear", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[17] = gb.CreateSPParameter("@arrrate", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[18] = gb.CreateSPParameter("@trtpk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[19] = gb.CreateSPParameter("@trtbng", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[20] = gb.CreateSPParameter("@sisapk", ParameterDirection.Input, SqlDbType.VarChar, 250, sisapk, typeof(string));
            //    var4[21] = gb.CreateSPParameter("@sisabng", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[22] = gb.CreateSPParameter("@postgk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[23] = gb.CreateSPParameter("@pkbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[24] = gb.CreateSPParameter("@bgbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[25] = gb.CreateSPParameter("@pkbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[26] = gb.CreateSPParameter("@bngbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[27] = gb.CreateSPParameter("@pkbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[28] = gb.CreateSPParameter("@bngbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[29] = gb.CreateSPParameter("@angspbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[30] = gb.CreateSPParameter("@angsbbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[31] = gb.CreateSPParameter("@angspbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[32] = gb.CreateSPParameter("@angsbbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[33] = gb.CreateSPParameter("@pcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[34] = gb.CreateSPParameter("@bcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[35] = gb.CreateSPParameter("@tgl_cof", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[36] = gb.CreateSPParameter("@angspcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[37] = gb.CreateSPParameter("@angsbcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[38] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[39] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[40] = gb.CreateSPParameter("@rekadm", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[41] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
            //    var4[42] = gb.CreateSPParameter("@tglupd", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgldrop, typeof(string));
            //    var4[43] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //    var4[44] = gb.CreateSPParameter("@tgl_jwl_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[45] = gb.CreateSPParameter("@tgl_angs_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[46] = gb.CreateSPParameter("@arrear_lama ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[47] = gb.CreateSPParameter("@status_channeling ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[48] = gb.CreateSPParameter("@status_pembayaran_channeling", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[49] = gb.CreateSPParameter("@tgl_angsuran_bank", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[50] = gb.CreateSPParameter("@no_tagihan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[51] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            //    var4[52] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            //    var4[53] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            //    var4[54] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //    ge.ListDataset = gb.GetDataJustOutPut("AR_KARTUPINJAMAN", var4, "constrmf");


            //}

            //DefaultClass.Global.SqlHelper[] var00 = new DefaultClass.Global.SqlHelper[8];
            //var00[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "6", typeof(string));
            //var00[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            //var00[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var00[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "6", typeof(string));
            //var00[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            //var00[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            //var00[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVALSTATUS", typeof(string));
            //var00[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var00, "constrmf");

            //DefaultClass.Global.SqlHelper[] var99 = new DefaultClass.Global.SqlHelper[6];
            //var99[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "6", typeof(string));
            //var99[1] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            //var99[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            //var99[3] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, HDno_rekening.Value, typeof(string));
            //var99[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEPERMOHONAN", typeof(string));
            //var99[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var99, "constrmf");




            ////UPDATE NO NASABAH COUNTER TO TABLE ARMst
            //DefaultClass.Global.SqlHelper[] varMst = new DefaultClass.Global.SqlHelper[6];
            //varMst[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            //varMst[1] = gb.CreateSPParameter("@JJualBln", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text, typeof(string));
            //varMst[2] = gb.CreateSPParameter("@JBayarThn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtplafon.Text, typeof(string));
            //varMst[3] = gb.CreateSPParameter("@SaldoJalan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text, typeof(string));
            //varMst[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEFORAPPROVAL", typeof(string));
            //varMst[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_ArMst", varMst, "constrmf");

            ////Select TypeTrn TO TABLE suTypeAr
            //DefaultClass.Global.SqlHelper[] varsutype = new DefaultClass.Global.SqlHelper[3];
            //varsutype[0] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, "C", typeof(string));
            //varsutype[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            //varsutype[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_SuTypeAr", varsutype, "constrmf");


            //typetrn  = ge.ListDataset.Tables[0].Rows[0]["TypeTrn"].ToString();
            //keterangantrn = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();

            ////INSERT INTO ArTrn
            //DefaultClass.Global.SqlHelper[] vartrn = new DefaultClass.Global.SqlHelper[35];
            //vartrn[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[1] = gb.CreateSPParameter("@NoFaktur", ParameterDirection.Input, SqlDbType.VarChar, 250, HDno_rekening.Value, typeof(string));
            //vartrn[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string));
            //vartrn[3] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
            //vartrn[4] = gb.CreateSPParameter("@TglTrn ", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
            //vartrn[5] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
            ////vartrn[6] = gb.CreateSPParameter("@TglTrnFaktur ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[7] = gb.CreateSPParameter("@TglJtpFaktur ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[8] = gb.CreateSPParameter("@BankGiro ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[9] = gb.CreateSPParameter("@Nogiro ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[10] = gb.CreateSPParameter("@Statusgiro ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[6] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
            //vartrn[7] = gb.CreateSPParameter("@Keterangan ", ParameterDirection.Input, SqlDbType.VarChar, 250, "Approval Pinjaman", typeof(string));
            //vartrn[8] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text, typeof(string));
            //vartrn[9] = gb.CreateSPParameter("@TglEntry ", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now.ToString("yyyy-MM-dd"), typeof(string));
            //vartrn[10] = gb.CreateSPParameter("@Kodeuser ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //vartrn[11] = gb.CreateSPParameter("@Kodesumber ", ParameterDirection.Input, SqlDbType.VarChar, 250, "MK", typeof(string));
            //vartrn[12] = gb.CreateSPParameter("@KodeJurnal ", ParameterDirection.Input, SqlDbType.VarChar, 250, "ARMK01", typeof(string));
            ////vartrn[18] = gb.CreateSPParameter("@DelIndex ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[13] = gb.CreateSPParameter("@TrfFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[14] = gb.CreateSPParameter("@BayarFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[15] = gb.CreateSPParameter("@HapusFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[16] = gb.CreateSPParameter("@TutupBukuFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[17] = gb.CreateSPParameter("@UangMukaFlag ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[18] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[19] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[20] = gb.CreateSPParameter("@KursAkhir", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[21] = gb.CreateSPParameter("@KursLama ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[22] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //vartrn[23] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now.ToString("yyyy-MM-dd"), typeof(string));
            ////vartrn[30] = gb.CreateSPParameter("@DateModified ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[31] = gb.CreateSPParameter("@LastUserFK ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[24] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            //vartrn[25] = gb.CreateSPParameter("@TFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[26] = gb.CreateSPParameter("@Pk ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[27] = gb.CreateSPParameter("@OverDue", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[28] = gb.CreateSPParameter("@RencanaPembayaran", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeproduk.Value, typeof(string));
            ////vartrn[37] = gb.CreateSPParameter("@REMARK ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[38] = gb.CreateSPParameter("@TglUpdate", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[39] = gb.CreateSPParameter("@PKPort ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[40] = gb.CreateSPParameter("@@TypeTrnRegister ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[41] = gb.CreateSPParameter("@PKVessel ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[42] = gb.CreateSPParameter("@IdAgency ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[43] = gb.CreateSPParameter("@Timearrival", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[44] = gb.CreateSPParameter("@timedeparture", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[45] = gb.CreateSPParameter("@voyagenumber ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[46] = gb.CreateSPParameter("@Commodity", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[29] = gb.CreateSPParameter("@Signatory", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            ////vartrn[48] = gb.CreateSPParameter("@RegisterNumber ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[49] = gb.CreateSPParameter("@IdPrincipal", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[30] = gb.CreateSPParameter("@RegisterName ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamanasabah.Text, typeof(string));
            //vartrn[31] = gb.CreateSPParameter("@TypeAR ", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string)); 
            //vartrn[32] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            //vartrn[33] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //vartrn[34] = gb.CreateSPParameter("@kodecabang ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));


            //ge.ListDataset = gb.GetDataJustOutPut("AR_TRN", vartrn, "constrmf");

            ////INSERT INTO gltrnsm
            //DefaultClass.Global.SqlHelper[] vartrnsm = new DefaultClass.Global.SqlHelper[17];
            //vartrnsm[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string)); 
            //vartrnsm[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string)); 
            //vartrnsm[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, "ARMK01", typeof(string)); 
            //vartrnsm[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string));
            //vartrnsm[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Approval Pinjaman", typeof(string));
            //vartrnsm[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 250, txtplafon.Text.Trim(), typeof(string)); //jmldebit
            //vartrnsm[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 250, txtplafon.Text.Trim(), typeof(string)); //jmlkredit
            //vartrnsm[7] = gb.CreateSPParameter("@KodeSupplier", ParameterDirection.Input, SqlDbType.VarChar, 250, HDno_rekening.Value, typeof(string));
            //vartrnsm[8] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            //vartrnsm[9] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Approval Pinjaman dengan Plafon : " + txtplafon.Text, typeof(string)); 
            //vartrnsm[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
            //vartrnsm[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //vartrnsm[12] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //vartrnsm[13] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            //vartrnsm[14] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //vartrnsm[15] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            //vartrnsm[16] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));



            //ge.ListDataset = gb.GetDataJustOutPut("AP_Transaksi", vartrnsm, "constrmf");

            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[9];
            var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "8", typeof(string));
            var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
            var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "8", typeof(string));
            var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVALSTATUSKEM", typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BindGridEntity();
            //lbllabakotor.Font.Bold = true;
            //lbltotalbiaya.Font.Bold = true;
            //lbllabasebelumpajak.Font.Bold = true;
            //lbllababersih.Font.Bold = true;


            ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";
            ddlpekerjaanktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlpekerjaanktp.DataTextField = "keterangan";
            ddlpekerjaanktp.DataValueField = "id";
            ddlpekerjaanktp.DataBind();


            ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

            ddlpropinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

            ddlpropinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlpropinsiktp.DataTextField = "nama_provinsi";
            ddlpropinsiktp.DataValueField = "kode_provinsi";
            ddlpropinsiktp.DataBind();



            // --- Combo Jenis Kelamin
            ge.ListDataset = gb.GetDataFunction("dbo.GetJenisKelamin()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddljenkelktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljenkelktp.DataTextField = "Keterangan";
            ddljenkelktp.DataValueField = "id";
            ddljenkelktp.DataBind();
            //ddljenkel.Items.Insert(0, new ListItem("Pilih Data", "-"));

            // --- Combo Status Pernikahan
            ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlstatusnikahktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlstatusnikahktp.DataTextField = "Keterangan";
            ddlstatusnikahktp.DataValueField = "id";
            ddlstatusnikahktp.DataBind();
        
            // ddlstatusnikah.Items.Insert(0, new ListItem("Pilih Data", "-"));

            // --- Combo Agama
            ge.ListDataset = gb.GetDataFunction("dbo.GetAgama()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlAgamaktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlAgamaktp.DataTextField = "Keterangan";
            ddlAgamaktp.DataValueField = "id";
            ddlAgamaktp.DataBind();




            // --- Combo kewarganegaraan
            ge.ListDataset = gb.GetDataFunction("dbo.GetCitizen()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlkewarganegaraanktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkewarganegaraanktp.DataTextField = "keterangan";
            ddlkewarganegaraanktp.DataValueField = "id";
            ddlkewarganegaraanktp.DataBind();

            // --- Combo Pendidikan
            ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlpendidikanktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlpendidikanktp.DataTextField = "keterangan";
            ddlpendidikanktp.DataValueField = "id";
            ddlpendidikanktp.DataBind();

          

            btnSetuju.Enabled = true;
            btnSimpanTolakan.Enabled = true;
        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlPengajuan.Visible = true;
                    pnltrn.Visible = true;
                    Pndetail.Visible = true;
                    Pnlfooter.Visible = true;
                    HdnKodecabang.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    var cabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;

                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                    var[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

                    //data ktp
                    txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtalamatktp.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                    ddlpropinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
                    txtkotaktp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                    txttempatlahirktp.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                    txttgllahirktp.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                    txtnoidentitasktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                    txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();                           
                    ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                    txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                    txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                    ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                    ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                    txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                    txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

                    txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                    txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                    ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                    ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    //data ktp

                    //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                    txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                    ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                    ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    

                    imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();

                    
                    ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                    


                    lblNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    lblNama.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    ddlpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tipeperpanjangan"].ToString();
                    lblrekeningperpanjangan.Text = ge.ListDataset.Tables[0].Rows[0]["norekeningperpanjangan"].ToString();
                    lblcadangansebelumnya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));
                    lblnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();
                    lblktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                    lblnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                    lbljaminansurat.Text = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                    lblagunan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                    lblkondisiagunan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();
                    Lbltaksasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
                    lbljeniskredit.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();
                    lblao.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    lbltglRealisasi.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString())); 
                    lbljangkawaktu.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                    lbljthtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString()));
                    lblbunga.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    lblmajuhari.Text = ddlmajuhari.SelectedItem.Text;
                    lblbesarpinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString()));
                    lblangsuranbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()));
                    lbladministrasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                    lblmaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString()));

                    decimal data1 = 0;
                    data1 = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()) - decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString());
                    if (data1 < 0)
                    {
                        data1 = 0;
                    }
                                            
                    LblPotonganangsuran.Text = string.Format("{0:N2}", data1);
                    LblPotonganangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString()));
                    LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                    lbluangmuka.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayauangmuka"].ToString()));
                    lblTotalPotongbiaya.Text = string.Format("{0:N2}", decimal.Parse(lbladministrasi.Text) + decimal.Parse(lblmaterai.Text) + decimal.Parse(LblPotonganangsuran.Text) + decimal.Parse(LblAnggota.Text) + decimal.Parse(lbluangmuka.Text));
                    lblygditerimaanggota.Text = string.Format("{0:N2}", decimal.Parse(lblbesarpinjaman.Text) - decimal.Parse(lblTotalPotongbiaya.Text));
                    lblkembalicadangan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pengembaliancadangan"].ToString()));
                    if (ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString() == "3C")
                    {
                        lblangsuran.Text = "Angsuran Per-Minggu";
                    }
                    else
                    {
                        lblangsuran.Text = "Angsuran Per-Bulan";
                    }


                    BindGridEntityxx();

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
        }
    }

    public void ProsesVerifikasi(string flag, string user)
    {

        try

        {
            btnSetuju.Enabled = false;
            btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 50, flag, typeof(string));
            var[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, user, typeof(string));
            var[2] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var[3] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVEMANAGER", typeof(string));
            var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[7] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

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

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATATOLAKAN", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_PINJAMAN_APP", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {



            case "SLIP":

                try
                {
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_rekening")).Text;
                    typetrn = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    //typetrn = "B";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, HdnKodecabang.Value, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTPINJAMANXX", typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, typetrn, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AO_WO", Paramsc, "constrmf");

                    if (ge.ListDataset.Tables.Count > 0)
                    {


                        Pndetail.Visible = true;



                        GRTS.DataSource = ge.ListDataset.Tables[0];
                        GRTS.DataBind();


                        GRTS.UseAccessibleHeader = true;
                        GRTS.HeaderRow.TableSection = TableRowSection.TableHeader;


                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);





                    }


                }

                catch (System.Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

                break;


        }
    }

    public void BindGridEntityxx()
    {

        try
        {



            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
            Params[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, HdnKodecabang.Value, typeof(string));
            Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Params[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAAKTVP", typeof(string));
            Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AO_WO", Params, "constrmf");

            GRTP.DataSource = ge.ListDataset.Tables[0];
            GRTP.DataBind();


            GRTP.UseAccessibleHeader = true;
            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;


        }

        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


}