using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;



public partial class AR_Transaksi_AssetLelangBarangTarikan : System.Web.UI.Page
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
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbX = new Global();
    GlobalEntities geX = new GlobalEntities();

    public string noassetdb;
    string pathfotofront = "";
    string pathfotoback = "";
    string pathfotoleft = "";
    string pathfotoright = "";

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        btnSimpan.Visible = true;
        txtNorekSearch.Text = "";
        pnladd.Visible = true;
        PnlGambar.Visible = true;
        Pnlcustomer.Visible = true;
        pnlkrtasrn.Visible = true;
        pnlPosting.Visible = true;
        pnlFilter.Visible = false;
    }


    protected void gvListLelang_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "SET":
                try
                {
                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    btnSimpan.Visible = false;
                    txtNorekSearch.Text = "";
                    pnladd.Visible = true;
                    Pnlcustomer.Visible = false;
                    pnlkrtasrn.Visible = true;
                    pnlPosting.Visible = true;
                    pnlFilter.Visible = false;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@@no_lelang", ParameterDirection.Input, SqlDbType.VarChar, 30, ((Label)gvListLelang.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtnolelang")).Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@@no_rekening", ParameterDirection.Input, SqlDbType.NVarChar, 30, ((Label)gvListLelang.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DATALELANG", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                    var[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Asset_Lelang_Tarikan", var, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {

                        txtnolelang.Text = ge.ListDataset.Tables[0].Rows[0]["no_lelang"].ToString();
                        txttgllelang.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["lelang_date"].ToString()).ToString("dd-MM-yyyy");
                        txtnosp1.Text = ge.ListDataset.Tables[0].Rows[0]["no_sp1"].ToString();
                        txtnosp2.Text = ge.ListDataset.Tables[0].Rows[0]["no_sp2"].ToString();
                        txtnosp3.Text = ge.ListDataset.Tables[0].Rows[0]["no_sp3"].ToString();
                        TextNoSuratTugas.Text = ge.ListDataset.Tables[0].Rows[0]["no_surat_tugas"].ToString();
                        TextNoBAST.Text = ge.ListDataset.Tables[0].Rows[0]["no_penyerahan_barang"].ToString();
                        txtNoAsset.Text = ge.ListDataset.Tables[0].Rows[0]["no_asset"].ToString();
                        txttgltarikan.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tarikan_date"].ToString()).ToString("dd-MM-yyyy");
                        txtNmBarang.Text = ge.ListDataset.Tables[0].Rows[0]["nama_barang"].ToString();
                        txtNoRek.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                        txtMerek.Text = ge.ListDataset.Tables[0].Rows[0]["merek"].ToString();
                        txtHargaJual.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["selling_price"].ToString()));
                        txtUkuran.Text = ge.ListDataset.Tables[0].Rows[0]["ukuran"].ToString();
                        txtKondisi.Text = ge.ListDataset.Tables[0].Rows[0]["condition"].ToString();
                        txtKetKondisi.Text = ge.ListDataset.Tables[0].Rows[0]["condition_desc"].ToString();
                        imgFront.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["front_image"].ToString();
                        imgBack.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["back_image"].ToString();
                        imgLeft.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["left_image"].ToString();
                        imgBack.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["right_image"].ToString();
                        txtNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_member"].ToString();
                        txtNama.Text = ge.ListDataset.Tables[0].Rows[0]["name_member"].ToString();
                        txtTglPinjam.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["loan_date"].ToString()).ToString("dd-MM-yyyy");
                        txtPlafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()));
                        txtSisaTotPok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["remain_principal_tot"].ToString()));
                        txtSisaTotBung.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["remain_interest_tot"].ToString()));

                        //kartu angsuran
                        BindGridEntityxx();
                    }
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;
        }

    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
        //if (txtSearch.Text == "")
        //{
        //    BindGridEntity();
        //}
        //else
        //{
        //    BindGridEntityx();
        //}
    }

    public void BindGridEntityxx()
    {

        try
        {
            //kartu angsuran
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];
            Params[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoRek.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTANGSURAN", typeof(string));
            Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            Params[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            Params[5] = gb.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoAnggota.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_Asset_Lelang_Tarikan", Params, "constrmf");


            //ge.ListDataset.Tables[0].DefaultView.Sort = "Angs_ke ASC";

            gvListAngsurLelang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            gvListAngsurLelang.DataBind();

            gvListAngsurLelang.UseAccessibleHeader = true;
            gvListAngsurLelang.HeaderRow.TableSection = TableRowSection.TableHeader;


        }

        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
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
                Console.WriteLine("test");
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgllelang.Text = xtanggal.ToString("dd-MM-yyyy");

                ddlstatus.Items.Insert(0, new ListItem("LELANG", "LELANG"));
                ddlstatus.Items.Insert(1, new ListItem("Di Ambil Anggota", "ANGGOTA"));        
            }
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
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTASSETLELANG", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Asset_Lelang_Tarikan", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "no_asset desc";


            gvListLelang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            gvListLelang.DataBind();

            gvListLelang.UseAccessibleHeader = true;
            gvListLelang.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected string cekNoLelang()
    {
        string Nextlelang = "";
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKNOASSET", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Asset_Lelang_Tarikan", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "no_lelang desc";

            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                Nextlelang = ge.ListDataset.Tables[0].Rows[0]["no_lelang"].ToString();
                Nextlelang = Nextlelang.Substring(3);
                int iCountLelang = Convert.ToInt32(Nextlelang);
                iCountLelang++;
                if (iCountLelang.ToString().Length == 1)
                {
                    Nextlelang = "ALT" + SessBranchID.Trim() + SessKdpersh.Trim() + ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString() + "00" + iCountLelang.ToString();
                }
                else if (iCountLelang.ToString().Length == 2)
                {
                    Nextlelang = "ALT" + SessBranchID.Trim() + SessKdpersh.Trim() + ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString() + "0" + iCountLelang.ToString();
                }
                else
                {
                    Nextlelang = "ALT" + SessBranchID.Trim() + SessKdpersh.Trim() + ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString() + iCountLelang.ToString();
                }


            }
            else
            {
                Nextlelang = "ALT" + SessBranchID.Trim() + SessKdpersh.Trim() + ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString() + "001";
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return Nextlelang;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    public void btnSearch_Click(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DATALELANG", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        var[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Asset_Lelang_Tarikan", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            txtHargaJual.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["harga_jual"].ToString()));
            txtnosp1.Text = ge.ListDataset.Tables[0].Rows[0]["no_sp1"].ToString();
            txtnosp2.Text = ge.ListDataset.Tables[0].Rows[0]["no_sp2"].ToString();
            txtnosp3.Text = ge.ListDataset.Tables[0].Rows[0]["no_sp3"].ToString();
            TextNoSuratTugas.Text = ge.ListDataset.Tables[0].Rows[0]["no_surat_tugas"].ToString();
            TextNoBAST.Text = ge.ListDataset.Tables[0].Rows[0]["no_penyerahan_barang"].ToString();
            txtNoAsset.Text = ge.ListDataset.Tables[0].Rows[0]["no_asset"].ToString();
            txttgltarikan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_tarikan"].ToString()));
            txtKondisi.Text = (ge.ListDataset.Tables[0].Rows[0]["kondisi"].ToString() == "1")?"BAGUS": (ge.ListDataset.Tables[0].Rows[0]["kondisi"].ToString() == "2") ? "KURANG BAGUS":"TIDAK BAGUS";
            txtNmBarang.Text = ge.ListDataset.Tables[0].Rows[0]["nama_barang"].ToString();
            txtMerek.Text = ge.ListDataset.Tables[0].Rows[0]["merek"].ToString();
            txtUkuran.Text = ge.ListDataset.Tables[0].Rows[0]["ukuran"].ToString();
            txtKetKondisi.Text = ge.ListDataset.Tables[0].Rows[0]["ket_kondisi"].ToString();
            imgFront.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["foto_depan"].ToString();
            imgBack.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["foto_belakang"].ToString();
            imgLeft.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["foto_kiri"].ToString();
            imgRight.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["foto_kanan"].ToString();
            txtfileUpldFront.Text = ge.ListDataset.Tables[0].Rows[0]["foto_depan"].ToString();
            txtfileUpldBack.Text = ge.ListDataset.Tables[0].Rows[0]["foto_belakang"].ToString();
            txtfileUpldLeft.Text = ge.ListDataset.Tables[0].Rows[0]["foto_kiri"].ToString();
            txtfileUpldRight.Text = ge.ListDataset.Tables[0].Rows[0]["foto_kanan"].ToString();
            txtNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
            txtNama.Text = ge.ListDataset.Tables[0].Rows[0]["nama_anggota"].ToString();
            txtNoRek.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
            txtTglPinjam.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_pinjaman"].ToString())); //ge.ListD
            txtPlafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()));
            txtSisaTotPok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisa_total_pokok"].ToString()));
            txtSisaTotBung.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisa_total_bunga"].ToString()));


            btnSimpan.Visible = true;

            BindGridEntityxx();
        }
        else
        {
            txtNoAnggota.Text = "";
            txtNama.Text = "";
            txtNoRek.Text = "";
            txtTglPinjam.Text = "";
            txtPlafon.Text = "0";
            txtSisaTotPok.Text = "0";
            txtSisaTotBung.Text = "0";


            btnSimpan.Enabled = false;
        }


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNoRek.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Anggota tidak ada!');", true);
                btnSimpan.Enabled = true;
            }
            else
            {

                string cekdata = "1";

                if (ddlstatus.SelectedValue == "ANGGOTA")
                {
                    DefaultClass.Global.SqlHelper[] varkop1 = new DefaultClass.Global.SqlHelper[5];
                    varkop1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLUNASAST", typeof(string));
                    varkop1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varkop1[2] = gb.CreateSPParameter("@no_rekening1", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoRek.Text, typeof(string));
                    varkop1[3] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    varkop1[4] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoAnggota.Text, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varkop1, "constrmf");
                    if (ge.ListDataset.Tables[0].DefaultView.Count > 0)

                    {
                        cekdata = "1";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rekening tersebut belum lunas, Lunasi terlebih dahulu!');", true);
                        btnSimpan.Enabled = true;

                    }
                    else

                    {
                        cekdata = "0";


                    }

                }
                else
                {

                    cekdata = "0";



                }


                if (cekdata=="0")

                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rekening tersebut belum lunas, Lunasi terlebih dahulu!');", true);
                //    btnSimpan.Enabled = true;

                //}
                //else

                {

               


                    if (gvListAngsurLelang.Rows.Count > 0)
                {
                    dtBayar.Columns.Clear();
                    dtBayar.Columns.Add(new DataColumn("no", typeof(int)));
                    dtBayar.Columns.Add(new DataColumn("kdcabang", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("kdpersh", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("tgl_tagih", typeof(DateTime)));
                    dtBayar.Columns.Add(new DataColumn("tgl_jth_tempo", typeof(DateTime)));
                    dtBayar.Columns.Add(new DataColumn("pokok", typeof(decimal)));
                    dtBayar.Columns.Add(new DataColumn("bunga", typeof(decimal)));
                    dtBayar.Columns.Add(new DataColumn("angsuran", typeof(decimal)));
                    dtBayar.Columns.Add(new DataColumn("status_bayar", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("tgl_angsur", typeof(DateTime)));

                    dtBayar.Rows.Clear();
                    int xi = 0;

                    for (int i = 0; i < gvListAngsurLelang.Rows.Count; i++)
                    {
                        DataRow dr = dtBayar.NewRow();
                        xi = xi + 1;
                        DateTime dtglangsur;
                        if (((Label)gvListAngsurLelang.Rows[i].FindControl("lblTglBayar")).Text.Length == 0)
                        {
                            dtglangsur = DateTime.Parse("1900-01-01");
                        }
                        else
                        {
                            dtglangsur = DateTime.Parse(((Label)gvListAngsurLelang.Rows[i].FindControl("lblTglBayar")).Text);
                        }

                        dr["no"] = xi;
                        dr["kdcabang"] = SessBranchID;
                        dr["kdpersh"] = SessKdpersh;
                        dr["no_rekening"] = HDNoRekening.Value;
                        dr["tgl_tagih"] = DateTime.Parse(((Label)gvListAngsurLelang.Rows[i].FindControl("lbltglTagih")).Text);
                        dr["tgl_jth_tempo"] = DateTime.Parse(((Label)gvListAngsurLelang.Rows[i].FindControl("lblTglDue")).Text);
                        dr["pokok"] = decimal.Parse(((Label)gvListAngsurLelang.Rows[i].FindControl("lblPokok")).Text);
                        dr["bunga"] = decimal.Parse(((Label)gvListAngsurLelang.Rows[i].FindControl("lblBunga")).Text);
                        dr["angsuran"] = decimal.Parse(((Label)gvListAngsurLelang.Rows[i].FindControl("lblAngsuran")).Text);
                        dr["status_bayar"] = ((Label)gvListAngsurLelang.Rows[i].FindControl("lblStatBayar")).Text;
                        dr["tgl_angsur"] = dtglangsur;

                        dtBayar.Rows.Add(dr);
                    }

                }

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[32];
                Params[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, "INSERTDATALELANG", typeof(String));
                Params[1] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(String));
                Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
                Params[3] = gb.CreateSPParameter("@no_lelang", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnolelang.Text, typeof(String));
                Params[4] = gb.CreateSPParameter("@lelang_date", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllelang.Text, typeof(DateTime));
                Params[5] = gb.CreateSPParameter("@selling_price", ParameterDirection.Input, SqlDbType.Decimal, 0, txtHargaJual.Text, typeof(Decimal));
                Params[6] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoRek.Text, typeof(String));
                Params[7] = gb.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoAnggota.Text, typeof(String));
                Params[8] = gb.CreateSPParameter("@nama_anggota", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNama.Text, typeof(String));
                Params[9] = gb.CreateSPParameter("@loan_date", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglPinjam.Text, typeof(DateTime));
                Params[10] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtPlafon.Text, typeof(Decimal));
                Params[11] = gb.CreateSPParameter("@remain_principal_tot", ParameterDirection.Input, SqlDbType.Decimal, 0, txtSisaTotPok.Text, typeof(Decimal));
                Params[12] = gb.CreateSPParameter("@remain_interest_tot", ParameterDirection.Input, SqlDbType.Decimal, 0, txtSisaTotBung.Text, typeof(Decimal));
                Params[13] = gb.CreateSPParameter("@no_asset", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoAsset.Text, typeof(String));
                Params[14] = gb.CreateSPParameter("@no_sp1", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnosp1.Text, typeof(String));
                Params[15] = gb.CreateSPParameter("@no_sp2", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnosp2.Text, typeof(String));
                Params[16] = gb.CreateSPParameter("@no_sp3", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnosp3.Text, typeof(String));
                Params[17] = gb.CreateSPParameter("@no_assignment", ParameterDirection.Input, SqlDbType.VarChar, 30, TextNoSuratTugas.Text, typeof(String));
                Params[18] = gb.CreateSPParameter("@no_bast", ParameterDirection.Input, SqlDbType.VarChar, 30, TextNoBAST.Text, typeof(String));
                Params[19] = gb.CreateSPParameter("@tarikan_date", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltarikan.Text, typeof(DateTime));
                Params[20] = gb.CreateSPParameter("@nama_barang", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNmBarang.Text, typeof(String));
                Params[21] = gb.CreateSPParameter("@brand", ParameterDirection.Input, SqlDbType.VarChar, 30, txtMerek.Text, typeof(String));
                Params[22] = gb.CreateSPParameter("@size", ParameterDirection.Input, SqlDbType.VarChar, 30, txtUkuran.Text, typeof(String));
                Params[23] = gb.CreateSPParameter("@condition", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKondisi.Text, typeof(String));
                Params[24] = gb.CreateSPParameter("@condition_desc", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKetKondisi.Text, typeof(String));
                Params[25] = gb.CreateSPParameter("@front_image", ParameterDirection.Input, SqlDbType.VarChar, 100, txtfileUpldFront.Text, typeof(String));
                Params[26] = gb.CreateSPParameter("@back_image", ParameterDirection.Input, SqlDbType.VarChar, 100, txtfileUpldBack.Text, typeof(String));
                Params[27] = gb.CreateSPParameter("@left_image", ParameterDirection.Input, SqlDbType.VarChar, 100, txtfileUpldLeft.Text, typeof(String));
                Params[28] = gb.CreateSPParameter("@right_image", ParameterDirection.Input, SqlDbType.VarChar, 100, txtfileUpldRight.Text, typeof(String));
                Params[29] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlstatus.SelectedValue, typeof(String));
                Params[30] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(String));
                Params[31] = gb.CreateSPParameter("@listDataAngsur", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));

                ge.ListDataset = gb.GetDataJustOutPut("AR_Asset_Lelang_Tarikan", Params, "constrmf");
                string hasilsp = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    //gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
            }
            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        //PnlFront.Visible = true;
        //Response.Redirect("AssetLelangTarikan.aspx");
        Response.Redirect(Request.Url.AbsoluteUri);

    }

    [WebMethod]
    public static string GetDataRekening(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 200, InputText, typeof(string));
        Params[1] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchIDx, typeof(string));
        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpershx, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Asset_Lelang_Tarikan", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

   

}