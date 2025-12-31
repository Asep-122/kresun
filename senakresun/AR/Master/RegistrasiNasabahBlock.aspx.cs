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
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Drawing.Charts;


public partial class FA_Master_RegistrasiNasabahBlock : System.Web.UI.Page
{
    String no_nasabah = "", nosebelumnya = "", updateno = "", CRNonasabah = "", STATE = "", kdpersh = "",
           formatnumber = "", msg1 = "", msg2 = "", msg3 = "", msg4 = "", msg5 = "", msg6 = "", msg7 = "", namafileKTP = "", namafileKK = "",
           namafileNPWP = "", namafileLainLain1 = "", namafileLainLain2 = "", namafilektppasangan = "";
    int counter_nonasabah = 0;
    String XYXKODEAO = "";
    long nosekarang = 0, nonasabah = 0;
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    public string fotopathktp = "", tanggal = "", fotopathnpwp = "", fotopathKK = "";
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
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

  
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("RegistrasiNasabahBlock.aspx");
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistrasiNasabahBlock.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
               
                BindGridEntity();

                InsertCombo();
                //txttgllahirasuransi.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {

        if (txtSearch.Text == "")
        {
            BindGridEntity();
        }
        else
        {
            BindGridEntityx();
        }
        

    }
    public void BindGridEntityx()
    {


        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AR_GetNasabah", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH_NEW", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_nasabah desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":
                int btndata = 0;
                int btnlunas = 0;
                HDState.Value = e.CommandName;
                no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                XYXKODEAO =((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAO")).Text;
                txtkodeao.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAO")).Text;
                try

                {
                   
                    DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[5];
                    var2x[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPERMOHONANADA", typeof(string));
                    var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var2x[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var2x[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2x, "constrmf");

                        if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota untuk Permohonan tersebut sudah ada dan belum di proses');", true);

                    }

                    else
                    {
                        btndata = 1;

                        DefaultClass.Global.SqlHelper[] var2tx = new DefaultClass.Global.SqlHelper[5];
                        var2tx[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                        var2tx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKWOSTATUSXX", typeof(string));
                        var2tx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var2tx[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var2tx[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2tx, "constrmf");


                        if (ge.ListDataset.Tables[0].Rows.Count > 0)

                        {

                            if (ge.ListDataset.Tables[0].Rows[0]["status_WO"].ToString() == "1")
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota Tersebut Sudah Di WO, silahkan AJukan Ke Pusat melalui Pembukuan');", true);
                                btndata =0;
                            }
                            else
                            {
                                btndata = 1;
                            }

                        }

                        else
                        {
                            btndata = 1;

                        }



                        btnlunas = 1;

                        if (btndata == 1)
                            {


                            btnlunas = 1;

                            DefaultClass.Global.SqlHelper[] var2Wtx = new DefaultClass.Global.SqlHelper[6];
                            var2Wtx[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                            var2Wtx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKAOBELUMLUNAS", typeof(string));
                            var2Wtx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            var2Wtx[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            var2Wtx[4] = gb.CreateSPParameter("@kd_reff2", ParameterDirection.Input, SqlDbType.VarChar, 50, XYXKODEAO, typeof(string));
                            var2Wtx[5] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2Wtx, "constrmf");


                            if (ge.ListDataset.Tables[0].Rows.Count > 0)

                            {

                                if (int.Parse(ge.ListDataset.Tables[0].Rows[0]["BLMLUNAS"].ToString()) > 0)
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota Tersebut Sudah ada transaksi Blm Lunas, Hanya alamat yang bisa dirubah');", true);
                                    btnlunas = 0;
                                }
                                else
                                {
                                    btnlunas = 1;
                                }

                            }

                            else
                            {
                                btnlunas = 1;

                            }







                            if (btndata == 1)
                            {




                                PnlFront.Visible = false;
                                pnladd.Visible = true;
                                pnlHeader.Visible = false;
                                pnlFilter.Visible = false;
                                Pnlfooter4.Visible = true;

                                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH_NEW", var, "constrmf");

                                HDNoNasabah.Value = no_nasabah;
                                txtanggotacbs.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah_cbs"].ToString();
                                TxtEmail.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                                txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                                txtnamapanggilan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_panggilan"].ToString();
                                ddljenkel.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                                txttempatlahir.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                                txttgllahir.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse((ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())));
                                //txttgllahirasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_asuransi"].ToString();
                                ddlAgama.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                                ddlstatusnikah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                                ddlpendidikan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                                ddlkewarganegaraan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                                txtkewarganegaraan.Text = ge.ListDataset.Tables[0].Rows[0]["kenegaraan_asing"].ToString();
                                ddlSektorUsaha.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                                txtjenisusaha.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();
                                txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                                txtkotaidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["kota_terbit_identitas"].ToString();

                                txttglterbitidentitas.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse((ge.ListDataset.Tables[0].Rows[0]["tanggal_terbit_identitas"].ToString())));

                                txtberlakuidentitas.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse((ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString())));
                                //txtberlakuidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dgn"].ToString();
                                txthp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                                txtalamatsesuaiktp1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();
                                txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                                txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                                txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                                txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                                txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
                                ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
                                txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                                txtpathktp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();
                                ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();

                                txtkodepetugas.Text = ge.ListDataset.Tables[0].Rows[0]["AddedByFK"].ToString();
                                txtaddentry.Text = ge.ListDataset.Tables[0].Rows[0]["DateAdded"].ToString();
                                txtpetugasupdate.Text = ge.ListDataset.Tables[0].Rows[0]["LastUserFK"].ToString();
                                txtupdateentry.Text = ge.ListDataset.Tables[0].Rows[0]["DateModified"].ToString();


                                HDNoNasabah.Value = "";
                                HDNoNasabah.Value = no_nasabah;
                                txtkodepetugas.Text = SessLoginID;
                                btnSimpan.Visible = true;

                                if (ddlkewarganegaraan.SelectedValue == "2")
                                {
                                    txtkewarganegaraan.Enabled = true;
                                    //txtkewarganegaraan.Text = "";
                                    txtidentitas.Enabled = false;
                                }
                                else
                                {
                                    txtkewarganegaraan.Enabled = false;
                                    //txtkewarganegaraan.Text = "-";
                                    txtidentitas.Enabled = false;
                                }
                                txtidentitas.Enabled = true;







                                var xkodeaox = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();
                                var flagxbyr = ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString();
                                var flagsalah = ge.ListDataset.Tables[0].Rows[0]["flagsalah"].ToString();

                                if (flagxbyr == "0")
                                {
                                    if (flagsalah=="1")
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data ini sudah Menjadi anggota hanya beberapa data yang dapat dirubah');", true);
                                        //btnSimpan.Visible = false;
                                        txtanggotacbs.Enabled = false;
                                        txtnamanasabah.Enabled = false;
                                        ddljenkel.Enabled = false;
                                        txttempatlahir.Enabled = false;
                                        txttgllahir.Enabled = false;


                                        txtidentitas.Enabled = false;


                                        txtkotaidentitas.Enabled = false;
                                        txttglterbitidentitas.Enabled = false;
                                        txtberlakuidentitas.Enabled = false;
                                        ddlAO.Enabled = false;
                                    }
                                    else
                                    {
                                        txtanggotacbs.Enabled = true;
                                        txtnamanasabah.Enabled = true;
                                        ddljenkel.Enabled = true;
                                        txttempatlahir.Enabled = true;
                                        txttgllahir.Enabled = true;


                                        txtidentitas.Enabled = true;


                                        txtkotaidentitas.Enabled = true;
                                        txttglterbitidentitas.Enabled = true;
                                        txtberlakuidentitas.Enabled = true;
                                        ddlAO.Enabled = true;

                                    }

                                }
                                else
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data ini sudah Menjadi anggota hanya beberapa data yang dapat dirubah');", true);
                                    //btnSimpan.Visible = false;
                                    txtanggotacbs.Enabled = false;
                                    txtnamanasabah.Enabled = false;
                                    ddljenkel.Enabled = false;
                                    txttempatlahir.Enabled = false;
                                    txttgllahir.Enabled = false;


                                    txtidentitas.Enabled = false;


                                    txtkotaidentitas.Enabled = false;
                                    txttglterbitidentitas.Enabled = false;
                                    txtberlakuidentitas.Enabled = false;
                                    ddlAO.Enabled = false;


                                }
								 ddlAO.Enabled = true;
								txtnamanasabah.Enabled = true;
								ddljenkel.Enabled = true;
								txttempatlahir.Enabled = true;
								txttgllahir.Enabled = true;


								txtidentitas.Enabled = true;


								txtkotaidentitas.Enabled = true;
								txttglterbitidentitas.Enabled = true;
								txtberlakuidentitas.Enabled = true;


                                if (btnlunas == 0)
                                {
                                    ddlAO.Enabled = false;
                                    txtnamanasabah.Enabled = false;
                                    ddljenkel.Enabled = false;
                                    txttempatlahir.Enabled = false;
                                    txttgllahir.Enabled = false;


                                    txtidentitas.Enabled = false;


                                    txtkotaidentitas.Enabled = false;
                                    txttglterbitidentitas.Enabled = false;
                                    txtberlakuidentitas.Enabled = false;
                                }
                                else
                                {

                                    ddlAO.Enabled = true;
                                    txtnamanasabah.Enabled = true;
                                    ddljenkel.Enabled = true;
                                    txttempatlahir.Enabled = true;
                                    txttgllahir.Enabled = true;


                                    txtidentitas.Enabled = true;


                                    txtkotaidentitas.Enabled = true;
                                    txttglterbitidentitas.Enabled = true;
                                    txtberlakuidentitas.Enabled = true;


                                }


                                //DefaultClass.Global.SqlHelper[] var2txs = new DefaultClass.Global.SqlHelper[5];
                                //var2txs[0] = gb.CreateSPParameter("@kode_AO", ParameterDirection.Input, SqlDbType.VarChar, 50, xkodeaox, typeof(string));
                                //var2txs[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCHKAO", typeof(string));
                                //var2txs[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                //var2txs[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                //var2txs[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                                //ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH_NEW", var2txs, "constrmf");


                                    //if (ge.ListDataset.Tables[0].Rows.Count > 0)

                                    //{
                                    //    ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["nik"].ToString();

                                    //    if (flagxbyr == "1")
                                    //    {
                                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data ini sudah Menjadi anggota hanya beberapa data yang dapat dirubah');", true);
                                    //        //btnSimpan.Visible = false;
                                    //        txtanggotacbs.Enabled = true;
                                    //        txtnamanasabah.Enabled = true;
                                    //        ddljenkel.Enabled = true;
                                    //        txttempatlahir.Enabled = true;
                                    //        txttgllahir.Enabled = true;


                                    //        txtidentitas.Enabled = true;


                                    //        txtkotaidentitas.Enabled = true;
                                    //        txttglterbitidentitas.Enabled = true;
                                    //        txtberlakuidentitas.Enabled = true;


                                    //    }
                                    //}
                                    //else
                                    //{

                                    //    //ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();
                                    //    if (flagxbyr == "1")
                                    //    {
                                    //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data ini sudah Menjadi anggota hanya beberapa data yang dapat dirubah');", true);
                                    //        //btnSimpan.Visible = false;
                                    //        txtanggotacbs.Enabled = true;
                                    //        txtnamanasabah.Enabled = true;
                                    //        ddljenkel.Enabled = true;
                                    //        txttempatlahir.Enabled = true;
                                    //        txttgllahir.Enabled = true;


                                    //        txtidentitas.Enabled = true;


                                    //        txtkotaidentitas.Enabled = true;
                                    //        txttglterbitidentitas.Enabled = true;
                                    //        txtberlakuidentitas.Enabled = true;


                                    //    }
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode AO sebelumnya tidak termasuk kedalam Ao yang aktif silahkan anda rubah.');", true);

                                    //}

                            }


                        }




                       



                    }
                
                
                
                
                
                
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }


                break;

            case "DELX":
                HDState.Value = e.CommandName;

                try

                {

                    //PnlFront.Visible = false;
                    //pnladd.Visible = true;
                    //pnlHeader.Visible = false;
                    //pnlFilter.Visible = false;
                    //Pnlfooter4.Visible = true;
                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[5];
                    var2x[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPERMOHONANADA", typeof(string));
                    var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var2x[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var2x[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2x, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota untuk Permohonan tersebut sudah ada dan belum di proses');", true);

                    }
                    else
                    {


                       
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSTATUS", typeof(string));
                        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH_NEW", var, "constrmf");


                        if (ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString() == "0")
                        {
                            //INSERT INTO ARNASABAH
                            DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];

                            vart[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, no_nasabah, typeof(string));
                            vart[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, "DELETE", typeof(string));
                            vart[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH_NEW_DELETE", vart, "constrmf");

                            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                            {


                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Anggota','top-right','success','2');", true);
                                GRData.UseAccessibleHeader = true;

                                GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data ini sudah Menjadi anggota tidak dapat di hapus');", true);

                        }
                    }

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }


                break;

        }
    }
    

 
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        pnlFilter.Visible=false;

        Pnlfooter4.Visible = true;
       

        txtkodepetugas.Text = SessLoginID;
        txtanggotacbs.Text = "0";

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {

        try
        {

           int blnsimpan = 0;
            string nobuktiktp = "";
            btnSimpan.Enabled = false;
            int usia;
            usia =  int.Parse(gb.HitungTahunUsia(txttgllahir.Text));

            string xkodex;
            if (ddlAO.Enabled == false)
            {
                xkodex = txtkodeao.Text;
            }
            else
            {
                xkodex = ddlAO.SelectedValue;

            }

            if (HDState.Value == "SET")
            {
                STATE = "UPDATE";
                
            }
            else
            {
                STATE = "INSERT";
                HDNoNasabah.Value = "-";
            }

            if (txtidentitas.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Id KTP Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtnamanasabah.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama Anggota Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txttempatlahir.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tempat Lahir Anggota Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtalamatsesuaiktp1.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Alamat KTP Anggota Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }

            else if (txtkabupaten.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kabupaten KTP Anggota Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtkelurahanktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kelurahan KTP Anggota Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtkecamatanktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kecamatan KTP Anggota Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtkodeposktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Pos KTP Anggota Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtkotaidentitas.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kota terbit KTP Anggota Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txttgllahir.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Lahir Anggota Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txttglterbitidentitas.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Terbit Identitas KTP Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtrtktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('RT Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtrwktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('RW Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtkodeposktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Pos Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (txthp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. HP Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }

            else if (TxtEmail.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Email Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }



            //else if (txtpathktp.Text.Length==0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Upload KTP Anggota Masih Kosong');", true);

            //}
            //else if (txthp.Text.Length < 11)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Handphone tidak boleh kurang dari 11');", true);

            //}
            else if (txtidentitas.Text.Length < 16)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No KTP tidak boleh kurang dari 16 Digit');", true);
                btnSimpan.Enabled = true;
            }
            else if (usia < 17)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tgl Lahir Calon tidak boleh kurang dari 17 tahun');", true);
                btnSimpan.Enabled = true;
            }
            else if (txtjenisusaha.Text.Length < 4)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tolong Jenis usaha lebih detail');", true);
                btnSimpan.Enabled = true;
            }
            else if (txttempatlahir.Text.Length < 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tempat Lahir belum diisi');", true);
                btnSimpan.Enabled = true;
            }

            else
            {
                if (STATE == "INSERT")
                {


                    //cekcabang sendiri
                    DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[6];
                    var4[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                    var4[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var4[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                    var4[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTPCABANGSENDIRI", typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", var4, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows.Count == 0)

                    {

                        //cek pemohon

                        DefaultClass.Global.SqlHelper[] var24 = new DefaultClass.Global.SqlHelper[6];
                        var24[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                        var24[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var24[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var24[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var24[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                        var24[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPEMOHON", typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", var24, "constrmf");

                        if (ge.ListDataset.Tables[0].Rows.Count > 0)

                        {
                            var datax = ge.ListDataset.Tables[0].Rows[0]["datax"].ToString();


                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "');", true);
                            blnsimpan = 0;
                            btnSimpan.Enabled = true;




                        }

                        else
                        {

                            //CEKKTPKOPERASISAMA;

                            DefaultClass.Global.SqlHelper[] var34 = new DefaultClass.Global.SqlHelper[7];
                            var34[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                            var34[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                            var34[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            var34[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            var34[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                            var34[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKoperasiID, typeof(string));
                            var34[6] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTPKOPERASISAMA", typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", var34, "constrmf");

                            if (ge.ListDataset.Tables[0].Rows.Count > 0)

                            {
                                var datax = ge.ListDataset.Tables[0].Rows[0]["datax"].ToString();


                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "');", true);
                                //blnsimpan = 0;
                                //btnSimpan.Enabled = true;

                                DefaultClass.Global.SqlHelper[] vart4 = new DefaultClass.Global.SqlHelper[6];
                                vart4[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                                vart4[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                                vart4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                vart4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                vart4[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                                vart4[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKAJUKAN", typeof(string));
                                ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", vart4, "constrmf");

                                if (ge.ListDataset.Tables[0].Rows.Count > 0)

                                {
                                    //nobuktiktp = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
                                    blnsimpan = 1;

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "  - Ajukan Approval Aktivasi Nik Kepusat');", true);
                                    blnsimpan = 0;
                                    btnSimpan.Enabled = true;
                                }







                            }

                            else
                            {
                                //CEKKTPKOPERASIBEDA
                                DefaultClass.Global.SqlHelper[] var54 = new DefaultClass.Global.SqlHelper[7];
                                var54[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                                var54[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                                var54[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                var54[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                var54[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                                var54[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKoperasiID, typeof(string));
                                var54[6] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTPKOPERASIBEDA", typeof(string));
                                ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", var54, "constrmf");

                                if (ge.ListDataset.Tables[0].Rows.Count > 0)

                                {
                                    var datax = ge.ListDataset.Tables[0].Rows[0]["datax"].ToString();

                                    DefaultClass.Global.SqlHelper[] vart4 = new DefaultClass.Global.SqlHelper[6];
                                    vart4[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                                    vart4[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                                    vart4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                    vart4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                    vart4[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                                    vart4[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKAJUKAN", typeof(string));
                                    ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", vart4, "constrmf");

                                    if (ge.ListDataset.Tables[0].Rows.Count > 0)

                                    {
                                        //nobuktiktp = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
                                        blnsimpan = 1;

                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "  - Ajukan Approval Aktivasi Nik Kepusat');", true);
                                        blnsimpan = 0;
                                        btnSimpan.Enabled = true;
                                    }

                                }
                                else
                                {

                                    //nobuktiktp = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
                                    blnsimpan = 1;


                                }



                            }


                        }





                    }
                    else
                    {
                        var datax = ge.ListDataset.Tables[0].Rows[0]["datax"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "  - Lakukan Penutupan Anggota Terlebih Dahulu');", true);
                        blnsimpan = 0;
                        btnSimpan.Enabled = true;



                    }

               
                }

                else if (STATE == "UPDATE")
                {




                    DefaultClass.Global.SqlHelper[] var14 = new DefaultClass.Global.SqlHelper[6];
                    var14[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                    var14[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var14[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var14[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var14[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                    var14[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKCALONANGGOTA", typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", var14, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows.Count > 0)

                    {


                        //cekcabang sendiri
                        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[6];
                        var4[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                        var4[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var4[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                        var4[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTPCABANGSENDIRI", typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", var4, "constrmf");

                        if (ge.ListDataset.Tables[0].Rows.Count == 0)

                        {

                            //cek pemohon

                            DefaultClass.Global.SqlHelper[] var24 = new DefaultClass.Global.SqlHelper[6];
                            var24[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                            var24[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                            var24[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            var24[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            var24[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                            var24[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPEMOHON", typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", var24, "constrmf");

                            if (ge.ListDataset.Tables[0].Rows.Count > 0)

                            {
                                var datax = ge.ListDataset.Tables[0].Rows[0]["datax"].ToString();


                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "');", true);
                                blnsimpan = 0;
                                btnSimpan.Enabled = true;




                            }

                            else
                            {

                                //CEKKTPKOPERASISAMA;

                                DefaultClass.Global.SqlHelper[] var34 = new DefaultClass.Global.SqlHelper[7];
                                var34[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                                var34[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                                var34[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                var34[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                var34[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                                var34[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKoperasiID, typeof(string));
                                var34[6] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTPKOPERASISAMA", typeof(string));
                                ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", var34, "constrmf");

                                if (ge.ListDataset.Tables[0].Rows.Count > 0)

                                {
                                    var datax = ge.ListDataset.Tables[0].Rows[0]["datax"].ToString();


                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "');", true);
                                    //blnsimpan = 0;
                                    //btnSimpan.Enabled = true;

                                    DefaultClass.Global.SqlHelper[] vart4 = new DefaultClass.Global.SqlHelper[6];
                                    vart4[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                                    vart4[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                                    vart4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                    vart4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                    vart4[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                                    vart4[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKAJUKAN", typeof(string));
                                    ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", vart4, "constrmf");

                                    if (ge.ListDataset.Tables[0].Rows.Count > 0)

                                    {
                                        //nobuktiktp = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
                                        blnsimpan = 1;

                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "  - Ajukan Approval Aktivasi Nik Kepusat');", true);
                                        blnsimpan = 0;
                                        btnSimpan.Enabled = true;
                                    }







                                }

                                else
                                {
                                    //CEKKTPKOPERASIBEDA
                                    DefaultClass.Global.SqlHelper[] var54 = new DefaultClass.Global.SqlHelper[7];
                                    var54[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                                    var54[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                                    var54[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                    var54[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                    var54[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                                    var54[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKoperasiID, typeof(string));
                                    var54[6] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTPKOPERASIBEDA", typeof(string));
                                    ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", var54, "constrmf");

                                    if (ge.ListDataset.Tables[0].Rows.Count > 0)

                                    {
                                        var datax = ge.ListDataset.Tables[0].Rows[0]["datax"].ToString();

                                        DefaultClass.Global.SqlHelper[] vart4 = new DefaultClass.Global.SqlHelper[6];
                                        vart4[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                                        vart4[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                                        vart4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                        vart4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                        vart4[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                                        vart4[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKAJUKAN", typeof(string));
                                        ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro_NEW_CUS", vart4, "constrmf");

                                        if (ge.ListDataset.Tables[0].Rows.Count > 0)

                                        {
                                            //nobuktiktp = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
                                            blnsimpan = 1;

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "  - Ajukan Approval Aktivasi Nik Kepusat');", true);
                                            blnsimpan = 0;
                                            btnSimpan.Enabled = true;
                                        }

                                    }
                                    else
                                    {

                                        //nobuktiktp = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
                                        blnsimpan = 1;


                                    }



                                }


                            }





                        }
                        else
                        {
                            var datax = ge.ListDataset.Tables[0].Rows[0]["datax"].ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info : " + datax + "  - Lakukan Penutupan Anggota Terlebih Dahulu');", true);
                            blnsimpan = 0;
                            btnSimpan.Enabled = true;



                        }




                    }

                    else
                    {

                        blnsimpan = 1;



                    }

                }


                else
                {
                    blnsimpan = 1;
                }



                //if (blnsimpan == 1)
                //{

                   


                //}






                if (blnsimpan == 1)
                {
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[37];

                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                    var[1] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));
                    var[2] = gb.CreateSPParameter("@nama_panggilan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamapanggilan.Text, typeof(string));
                    var[3] = gb.CreateSPParameter("@jenis_kelamin", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljenkel.SelectedValue, typeof(string));
                    var[4] = gb.CreateSPParameter("@tempat_lahir", ParameterDirection.Input, SqlDbType.VarChar, 50, txttempatlahir.Text, typeof(string));
                    var[5] = gb.CreateSPParameter("@tanggal_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttgllahir.Text, typeof(string));
                    var[6] = gb.CreateSPParameter("@agama", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlAgama.SelectedValue, typeof(string));
                    var[7] = gb.CreateSPParameter("@status_pernikahan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusnikah.SelectedValue, typeof(string));
                    var[8] = gb.CreateSPParameter("@pendidikan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlpendidikan.SelectedValue, typeof(string));
                    var[9] = gb.CreateSPParameter("@kewarganegaraan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkewarganegaraan.SelectedValue, typeof(string));
                    var[10] = gb.CreateSPParameter("@kenegaraan_asing", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkewarganegaraan.Text, typeof(string));
                    var[11] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidentitas.Text, typeof(string));
                    var[12] = gb.CreateSPParameter("@kota_terbit_identitas", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkotaidentitas.Text, typeof(string));
                    var[13] = gb.CreateSPParameter("@tanggal_terbit_identitas", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglterbitidentitas.Text, typeof(string));
                    var[14] = gb.CreateSPParameter("@berlaku_sampai_dengan", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglterbitidentitas.Text, typeof(string));
                    var[15] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 100, TxtEmail.Text, typeof(string));
                    var[16] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 30, txthp.Text, typeof(string));
                    var[17] = gb.CreateSPParameter("@alamat_rumah_ktp1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamatsesuaiktp1.Text, typeof(string));
                    var[18] = gb.CreateSPParameter("@kabupaten", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkabupaten.Text, typeof(string));
                    var[19] = gb.CreateSPParameter("@RT_ktp", ParameterDirection.Input, SqlDbType.VarChar, 100, txtrtktp.Text, typeof(string));
                    var[20] = gb.CreateSPParameter("@RW_ktp", ParameterDirection.Input, SqlDbType.VarChar, 100, txtrwktp.Text, typeof(string));
                    var[21] = gb.CreateSPParameter("@kelurahan_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkelurahanktp.Text, typeof(string));
                    var[22] = gb.CreateSPParameter("@kecamatan_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkecamatanktp.Text, typeof(string));
                    var[23] = gb.CreateSPParameter("@provinsi_ktp", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlprovinsiktp.SelectedValue, typeof(string));
                    var[24] = gb.CreateSPParameter("@kodepos_ktp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodeposktp.Text, typeof(string));
                    var[25] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, STATE, typeof(string));
                    var[26] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[27] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    var[28] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var[29] = gb.CreateSPParameter("@Userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    var[30] = gb.CreateSPParameter("@kode_AO", ParameterDirection.Input, SqlDbType.VarChar, 30, xkodex, typeof(string));
                    var[31] = gb.CreateSPParameter("@pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlPekerjaan.SelectedValue, typeof(string));
                    var[32] = gb.CreateSPParameter("@file_pathktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathktp.Text, typeof(string));
                    var[33] = gb.CreateSPParameter("@useredit", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    var[34] = gb.CreateSPParameter("@anggotacbs", ParameterDirection.Input, SqlDbType.VarChar, 30, txtanggotacbs.Text, typeof(string));
                    var[35] = gb.CreateSPParameter("@sektorusaha", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlSektorUsaha.SelectedValue, typeof(string));
                    var[36] = gb.CreateSPParameter("@jenisusaha", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenisusaha.Text, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH_NEW_UPGRADE", var, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Anggota','top-right','success','2');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }
                }





                }

            //}
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }


    }





    protected void ddlkewarganegaraan_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlkewarganegaraan.SelectedValue == "2")
        {
            txtkewarganegaraan.Enabled = true;
            txtkewarganegaraan.Text = "";
        }
        else
        {
            txtkewarganegaraan.Enabled = false;
            txtkewarganegaraan.Text = "-";
        }


    }
    public void InsertCombo()
    {
        // --- Combo Jenis Kelamin
        ge.ListDataset = gb.GetDataFunction("dbo.GetJenisKelamin()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddljenkel.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljenkel.DataTextField = "Keterangan";
        ddljenkel.DataValueField = "id";
        ddljenkel.DataBind();
        //ddljenkel.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Status Pernikahan
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatusnikah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatusnikah.DataTextField = "Keterangan";
        ddlstatusnikah.DataValueField = "id";
        ddlstatusnikah.DataBind();
       // ddlstatusnikah.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Agama
        ge.ListDataset = gb.GetDataFunction("dbo.GetAgama()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlAgama.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAgama.DataTextField = "Keterangan";
        ddlAgama.DataValueField = "id";
        ddlAgama.DataBind();
   

      

        // --- Combo kewarganegaraan
        ge.ListDataset = gb.GetDataFunction("dbo.GetCitizen()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlkewarganegaraan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkewarganegaraan.DataTextField = "keterangan";
        ddlkewarganegaraan.DataValueField = "id";
        ddlkewarganegaraan.DataBind();
    
        // --- Combo Pendidikan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlpendidikan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpendidikan.DataTextField = "keterangan";
        ddlpendidikan.DataValueField = "id";
        ddlpendidikan.DataBind();
      //  ddlpendidikan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //// --- Combo Pendidikan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlpendidikan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlpendidikan.DataTextField = "keterangan";
        //ddlpendidikan.DataValueField = "id";
        //ddlpendidikan.DataBind();
    
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsiktp.DataTextField = "nama_provinsi";
        ddlprovinsiktp.DataValueField = "kode_provinsi";
        ddlprovinsiktp.DataBind();
       // ddlprovinsiktp.Items.Insert(0, new ListItem("Pilih Data", "-"));

       
        // -- Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlPekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlPekerjaan.DataTextField = "keterangan";
        ddlPekerjaan.DataValueField = "id";
        ddlPekerjaan.DataBind();


        // -- Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetSektorUsaha()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_sektor_usaha asc";

        ddlSektorUsaha.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlSektorUsaha.DataTextField = "keterangan";
        ddlSektorUsaha.DataValueField = "kode_sektor_usaha";
        ddlSektorUsaha.DataBind();
        

        

        // COMBO MASTER MO
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_MO", var1, "constrmf");

        ddlAO.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAO.DataTextField = "namaAO";
        ddlAO.DataValueField = "NIK";
        ddlAO.DataBind();
    //    ddlAO.Items.Insert(0, new ListItem("Pilih Data", "-"));

    
        ddlkewarganegaraan.SelectedValue = "1";
        txtkewarganegaraan.Enabled = false;
        txtkewarganegaraan.Text = "-";
    }

    protected void UploadFileKTP(object sender, EventArgs e)
    {



        if (fileuploadktp.HasFile)
        {


            var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

            if (fileuploadktp.PostedFile.ContentLength > maxFileSize)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

            }

            else
            {

                string namafile;
                string filename;
                string filenamex;

                DateTime now = DateTime.Now;
                namafile = txtidentitas.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "KTP";
                string folderPath = Server.MapPath("~/Files/Anggota/thumb");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                filename = namafile + ".jpg";
                filenamex = namafile + "OK.jpg";


                //create temp
                string path = Server.MapPath("~/Files/Anggota/thumb/" + namafile + ".jpg");
                fileuploadktp.SaveAs(path);
                //create temp

                System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                int newHeight = 200;
                int newWidth = Convert.ToInt32(aspectRatio * newHeight);
                System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
                System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
                thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbBitmap.Save(Server.MapPath("~/Files/Anggota/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbGraph.Dispose();
                thumbBitmap.Dispose();
                image.Dispose();
                fotopathktp = "~/Files/Anggota/" + filenamex;
                txtpathktp.Text = fotopathktp;
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not.
                {
                    file.Delete();
                }


            }

        }



    }

  
        
}