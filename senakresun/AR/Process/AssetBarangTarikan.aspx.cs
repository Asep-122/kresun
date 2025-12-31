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


public partial class AR_Transaksi_AssetBarangTarikan : System.Web.UI.Page
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
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;

        //btndelete.Visible = false;
        btnSimpan.Visible = true;
        btnDel.Visible = false;
        txtNorekSearch.Text = "";
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        pnlkrtasrn.Visible = true;
        pnlPosting.Visible = true;
        pnlFilter.Visible = false;
    }


    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "SET":
                try
                {
                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    btnSimpan.Visible = false;
                    btnDel.Visible = true;
                    txtNorekSearch.Text = "";
                    pnladd.Visible = true;



                    Pnlcustomer.Visible = false;
                    pnlkrtasrn.Visible = true;
                    pnlPosting.Visible = true;
                    pnlFilter.Visible = false;


                    txtasset.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoasset")).Text;
                    txtNoRek.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorek")).Text;



                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@NoAsset", ParameterDirection.Input, SqlDbType.VarChar, 30, txtasset.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.NVarChar, 30, txtNoRek.Text, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DATAASSETTARIKAN", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                    var[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("Ar_Trn_Asset_Tarikan", var, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtnosp1.Text = ge.ListDataset.Tables[0].Rows[0]["no_sp1"].ToString();
                        txtnosp2.Text = ge.ListDataset.Tables[0].Rows[0]["no_sp2"].ToString();
                        txtnosp3.Text = ge.ListDataset.Tables[0].Rows[0]["no_sp3"].ToString();
                        TextNoSuratTugas.Text = ge.ListDataset.Tables[0].Rows[0]["no_surat_tugas"].ToString();
                        TextNoBAST.Text = ge.ListDataset.Tables[0].Rows[0]["no_penyerahan_barang"].ToString();
                        txttgltarik.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_tarikan"].ToString()).ToString("dd-MM-yyyy");
                        txtNmBarang.Text = ge.ListDataset.Tables[0].Rows[0]["nama_barang"].ToString();
                        txtMerek.Text = ge.ListDataset.Tables[0].Rows[0]["merek"].ToString();
                        txtUkuran.Text = ge.ListDataset.Tables[0].Rows[0]["ukuran"].ToString();
                        ddlKondisi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kondisi"].ToString();
                        txtKetKondisi.Text = ge.ListDataset.Tables[0].Rows[0]["ket_kondisi"].ToString();
                        txtHargaJual.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["harga_jual"].ToString()));
                        imgFront.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["foto_depan"].ToString();
                        imgBack.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["foto_belakang"].ToString();
                        imgLeft.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["foto_kiri"].ToString();
                        imgRight.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["foto_kanan"].ToString();
                        txtNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                        txtNama.Text = ge.ListDataset.Tables[0].Rows[0]["nama_anggota"].ToString();
                        txtTglPinjam.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_pinjaman"].ToString()).ToString("dd-MM-yyyy");
                        txtPlafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()));
                        txtSisaTotPok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisa_total_pokok"].ToString()));
                        txtSisaTotBung.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisa_total_bunga"].ToString()));
                        //imgktp.ImageUrl = "https://gadai.gigaku.id" + namafile + "?r=" + DateTime.Now.Ticks.ToString();

                        //kartu angsuran
                        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
                        Params[0] = gb.CreateSPParameter("@NoAsset", ParameterDirection.Input, SqlDbType.VarChar, 30, txtasset.Text, typeof(String));
                        Params[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DATAASSETTARIKANDTL", typeof(string));
                        Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        Params[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                        Params[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("Ar_Trn_Asset_Tarikan", Params, "constrmf");

                        //GRKA.DataSource = ge.ListDataset.Tables[0];
                        //GRKA.DataBind();


                        // GRKA.UseAccessibleHeader = true;
                        // GRKA.HeaderRow.TableSection = TableRowSection.TableHeader;

                        ge.ListDataset.Tables[0].DefaultView.Sort = "Angs_ke ASC";

                        GRKA.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                        GRKA.DataBind();

                        GRKA.UseAccessibleHeader = true;
                        GRKA.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            Params[0] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(String));
            Params[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTANGSURAN", typeof(string));
            Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            Params[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            Params[5] = gb.CreateSPParameter("@NoAnggota", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("Ar_Trn_Asset_Tarikan", Params, "constrmf");

            //GRKA.DataSource = ge.ListDataset.Tables[0];
            //GRKA.DataBind();


            // GRKA.UseAccessibleHeader = true;
            // GRKA.HeaderRow.TableSection = TableRowSection.TableHeader;

            ge.ListDataset.Tables[0].DefaultView.Sort = "Angs_ke ASC";

            GRKA.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRKA.DataBind();

            GRKA.UseAccessibleHeader = true;
            GRKA.HeaderRow.TableSection = TableRowSection.TableHeader;


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
        Console.WriteLine("first");
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
                txttgltarik.Text = xtanggal.ToString("dd-MM-yyyy");

                ddlKondisi.Items.Insert(0, new ListItem("Bagus", "1"));
                ddlKondisi.Items.Insert(1, new ListItem("Kurang Bagus", "2"));
                ddlKondisi.Items.Insert(2, new ListItem("Tidak Bagus", "3"));
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

            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTASSETTARIKAN", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Ar_Trn_Asset_Tarikan", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "no_asset desc";


            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected string cekNoAsset()
    {
        string NextAsset = "";
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKNOASSET", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Ar_Trn_Asset_Tarikan", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "no_asset desc";

            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                NextAsset = ge.ListDataset.Tables[0].Rows[0]["no_asset"].ToString();
                NextAsset = NextAsset.Substring(3);
                int iCountAsset = Convert.ToInt32(NextAsset);
                iCountAsset++;
                if (iCountAsset.ToString().Length == 1)
                {
                    NextAsset = "ABT" + SessBranchID.Trim() + SessKdpersh.Trim() + "00" + iCountAsset.ToString();
                }
                else if (iCountAsset.ToString().Length == 2)
                {
                    NextAsset = "ABT" + SessBranchID.Trim() + SessKdpersh.Trim() + "0" + iCountAsset.ToString();
                }
                else
                {
                    NextAsset = "ABT" + SessBranchID.Trim() + SessKdpersh.Trim() + iCountAsset.ToString();
                }


            }
            else
            {
                NextAsset = "ABT" + SessBranchID.Trim() + SessKdpersh.Trim() + "001";
            }


            //GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //GRData.DataBind();

            //GRData.UseAccessibleHeader = true;
            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return NextAsset;
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;

        txtNorekSearch.Text = "";
        pnladd.Visible = false;

        Pnlakhir.Visible = true;
        PnlGambar.Visible = true;
        Pnlcustomer.Visible = false;
        pnlkrtasrn.Visible = false;
        pnlPosting.Visible = false;
        pnlFilter.Visible = false;
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        txtNorekSearch.Text = "";
        pnladd.Visible = true;

        Pnlakhir.Visible = false;
        PnlGambar.Visible = false;
        Pnlcustomer.Visible = true;
        pnlkrtasrn.Visible = true;
        pnlPosting.Visible = true;
        pnlFilter.Visible = false;
    }
    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];

            Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtasset.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[2] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            Params[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_TRN_ASSET_TARIKAN_Delete_X", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {


                btnDel.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','2');", true);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(gagal..!'" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                //gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(Error..!'" + X.Message.ToString().Replace("'", "") + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

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

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            decimal sistopo = decimal.Parse(txtSisaTotPok.Text);
            decimal sistobung = decimal.Parse(txtSisaTotBung.Text);
            decimal hargajual = decimal.Parse(txtHargaJual.Text);
            decimal sistoang = sistopo + sistobung;

            //CultureInfo cultures = new CultureInfo("en-US");
            if (txtNoRek.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Anggota tidak ada!');", true);
                btnSimpan.Enabled = true;
            }
            else if (hargajual < sistoang)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Harga Jual tidak bisa kurang dari sisa total angsuran..!');", true);
                txtfileUpldFront.Focus();
            }
            //else if (txtfileUpldBack.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('File Gambar Belakang belum di upload!');", true);
            //    txtfileUpldBack.Focus();
            //}
            //else if (txtfileUpldLeft.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('File Gambar Kiri belum di upload!');", true);
            //    txtfileUpldLeft.Focus();
            //}
            //else if (txtfileUpldRight.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('File Gambar Kanan belum di upload!');", true);
            //    txtfileUpldRight.Focus();
            //}
            else
            {
                //string NewNoAsset = cekNoAsset();

                if (GRKA.Rows.Count > 0)
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

                    for (int i = 0; i < GRKA.Rows.Count; i++)
                    {
                        DataRow dr = dtBayar.NewRow();
                        xi = xi + 1;
                        DateTime dtgltagih = DateTime.Parse(((Label)GRKA.Rows[i].FindControl("lbltglTagih")).Text);
                        DateTime dtgljthtempo = DateTime.Parse(((Label)GRKA.Rows[i].FindControl("lblTglDue")).Text);
                        DateTime dtglangsur;
                        if (((Label)GRKA.Rows[i].FindControl("lblTglBayar")).Text.Length == 0)
                        {
                            dtglangsur = DateTime.Parse("1900-01-01");
                        }
                        else
                        {
                            dtglangsur = DateTime.Parse(((Label)GRKA.Rows[i].FindControl("lblTglBayar")).Text);
                        }


                        decimal fpokok = decimal.Parse(((Label)GRKA.Rows[i].FindControl("lblPokok")).Text);
                        decimal fbunga = decimal.Parse(((Label)GRKA.Rows[i].FindControl("lblBunga")).Text);
                        decimal fangsuran = decimal.Parse(((Label)GRKA.Rows[i].FindControl("lblAngsuran")).Text);

                        dr["no"] = xi;
                        dr["kdcabang"] = SessBranchID;
                        dr["kdpersh"] = SessKdpersh;
                        dr["no_rekening"] = HDNoRekening.Value;
                        dr["tgl_tagih"] = dtgltagih;
                        dr["tgl_jth_tempo"] = dtgljthtempo;
                        dr["pokok"] = fpokok;
                        dr["bunga"] = fbunga;
                        dr["angsuran"] = fangsuran;
                        dr["status_bayar"] = ((Label)GRKA.Rows[i].FindControl("lblStatBayar")).Text;
                        dr["tgl_angsur"] = dtglangsur;

                        dtBayar.Rows.Add(dr);
                    }

                }

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[29];
                Params[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.NVarChar, 30, "INSERTASSETTARIKAN", typeof(String));
                Params[1] = gb.CreateSPParameter("@NoAsset", ParameterDirection.Input, SqlDbType.NVarChar, 30, txtasset.Text, typeof(String));
                Params[2] = gb.CreateSPParameter("@NoSp1", ParameterDirection.Input, SqlDbType.NVarChar, 30, txtnosp1.Text, typeof(String));
                Params[3] = gb.CreateSPParameter("@NoSp2", ParameterDirection.Input, SqlDbType.NVarChar, 30, txtnosp2.Text, typeof(String));
                Params[4] = gb.CreateSPParameter("@NoSp3", ParameterDirection.Input, SqlDbType.NVarChar, 30, txtnosp3.Text, typeof(String));
                Params[5] = gb.CreateSPParameter("@NoSuratTugas", ParameterDirection.Input, SqlDbType.NVarChar, 30, TextNoSuratTugas.Text, typeof(String));
                Params[6] = gb.CreateSPParameter("@NoBAST", ParameterDirection.Input, SqlDbType.NVarChar, 30, TextNoBAST.Text, typeof(String));
                Params[7] = gb.CreateSPParameter("@TglTarik", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltarik.Text, typeof(DateTime));
                Params[8] = gb.CreateSPParameter("@NmBarang", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNmBarang.Text, typeof(String));
                Params[9] = gb.CreateSPParameter("@Merek", ParameterDirection.Input, SqlDbType.VarChar, 30, txtMerek.Text, typeof(String));
                Params[10] = gb.CreateSPParameter("@Ukuran", ParameterDirection.Input, SqlDbType.VarChar, 20, txtUkuran.Text, typeof(String));
                Params[11] = gb.CreateSPParameter("@Kondisi", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKondisi.Text, typeof(String));
                Params[12] = gb.CreateSPParameter("@KetKondisi", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKetKondisi.Text, typeof(String));
                Params[13] = gb.CreateSPParameter("@HargaJual", ParameterDirection.Input, SqlDbType.Decimal, 0, txtHargaJual.Text, typeof(Decimal));
                Params[14] = gb.CreateSPParameter("@PicDpn", ParameterDirection.Input, SqlDbType.NVarChar, 100, txtfileUpldFront.Text, typeof(String));
                Params[15] = gb.CreateSPParameter("@PicBack", ParameterDirection.Input, SqlDbType.NVarChar, 100, txtfileUpldBack.Text, typeof(String));
                Params[16] = gb.CreateSPParameter("@PicKiri", ParameterDirection.Input, SqlDbType.NVarChar, 100, txtfileUpldLeft.Text, typeof(String));
                Params[17] = gb.CreateSPParameter("@PicKanan", ParameterDirection.Input, SqlDbType.NVarChar, 100, txtfileUpldRight.Text, typeof(String));
                Params[18] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.NVarChar, 30, HDNoRekening.Value, typeof(String));
                Params[19] = gb.CreateSPParameter("@NoAnggota", ParameterDirection.Input, SqlDbType.VarChar, 15, txtNoAnggota.Text, typeof(String));
                Params[20] = gb.CreateSPParameter("@NmAnggota", ParameterDirection.Input, SqlDbType.VarChar, 70, txtNama.Text, typeof(String));
                Params[21] = gb.CreateSPParameter("@TglPinjaman", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglPinjam.Text, typeof(DateTime));
                Params[22] = gb.CreateSPParameter("@Plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtPlafon.Text, typeof(Decimal));
                Params[23] = gb.CreateSPParameter("@SisaTotPokok", ParameterDirection.Input, SqlDbType.Decimal, 0, txtSisaTotPok.Text, typeof(Decimal));
                Params[24] = gb.CreateSPParameter("@SisaTotBunga", ParameterDirection.Input, SqlDbType.Decimal, 0, txtSisaTotBung.Text, typeof(Decimal));
                Params[25] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(String));
                Params[26] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                Params[27] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                Params[28] = gb.CreateSPParameter("@listDataAngsur", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));

                ge.ListDataset = gb.GetDataJustOutPut("AR_TRN_ASSET_TARIKAN_Save_X", Params, "constrmf");
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

    public void btnSearch_Click(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DATAREKENING", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        var[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Ar_Trn_Asset_Tarikan", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)


        {
            txtNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
            txtNama.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txtNoRek.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();


            txtTglPinjam.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString())); //ge.ListD
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


    protected void UploadFront(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/BarangTarikan/");
            DateTime now = DateTime.Now;

            string namafileJaminan = (txtNoRek.Text) + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "FRONT";


            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }



            fileUpldFront.SaveAs(folderPath + namafileJaminan + ".jpg");
            //System.Drawing.Image img = System.Drawing.Image.FromFile(string.Concat(folderPath, namafileJaminan + ".jpg"));
            //img = resizeImage(img);
            //img.Save(string.Concat(folderPath, namafileJaminan + "_resize" + ".jpg"));
            //string fotopathfront = folderPath + namafileJaminan;

            txtfileUpldFront.Text = "~/Files/BarangTarikan/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            //imgktp.ImageUrl = "http://anditeam.kspserambidana.com/Files/Gadai/KTP/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            imgFront.ImageUrl = "~/Files/BarangTarikan/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();



        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    private System.Drawing.Image resizeImage(System.Drawing.Image img)
    {
        Bitmap b = new Bitmap(img);
        System.Drawing.Image i = resizeImage(b, new Size(60, 60));
        return i;
    }

    private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    {
        //Get the image current width
        int sourceWidth = imgToResize.Width;
        //Get the image current height
        int sourceHeight = imgToResize.Height;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;
        //Calulate  width with new desired size
        nPercentW = ((float)size.Width / (float)sourceWidth);
        //Calculate height with new desired size
        nPercentH = ((float)size.Height / (float)sourceHeight);


        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;
        //New Width
        int destWidth = (int)(sourceWidth * nPercent);
        //New Height
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap b = new Bitmap(60, 60);
        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        // Draw image with new width and height
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return (System.Drawing.Image)b;
    }
    /// <summary>
    /// Function for resize image
    /// </summary>
    /// <param name="img">Image</param>
    /// <returns></returns>
    protected void UploadBack(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/BarangTarikan/");
            DateTime now = DateTime.Now;

            string namafileJaminan = (txtNoRek.Text) + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "BACK";


            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }



            fileUpldBack.SaveAs(folderPath + namafileJaminan + ".jpg");

            string fotopathfront = folderPath + namafileJaminan;

            txtfileUpldBack.Text = "~/Files/BarangTarikan/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            //imgktp.ImageUrl = "http://anditeam.kspserambidana.com/Files/Gadai/KTP/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            imgBack.ImageUrl = "~/Files/BarangTarikan/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();



        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void UploadLeft(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/BarangTarikan/");
            DateTime now = DateTime.Now;

            string namafileJaminan = (txtNoRek.Text) + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "LEFT";


            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }



            fileUpldLeft.SaveAs(folderPath + namafileJaminan + ".jpg");

            string fotopathfront = folderPath + namafileJaminan;

            txtfileUpldLeft.Text = "~/Files/BarangTarikan/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            //imgktp.ImageUrl = "http://anditeam.kspserambidana.com/Files/Gadai/KTP/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            imgLeft.ImageUrl = "~/Files/BarangTarikan/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();



        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void UploadRight(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/BarangTarikan/");
            DateTime now = DateTime.Now;

            string namafileJaminan = (txtNoRek.Text) + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "RIGHT";


            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }



            fileUpldRight.SaveAs(folderPath + namafileJaminan + ".jpg");

            string fotopathfront = folderPath + namafileJaminan;

            txtfileUpldRight.Text = "~/Files/BarangTarikan/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            //imgktp.ImageUrl = "http://anditeam.kspserambidana.com/Files/Gadai/KTP/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            imgRight.ImageUrl = "~/Files/BarangTarikan/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();



        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        PnlFront.Visible = true;
        Response.Redirect("AssetBarangTarikan.aspx");

    }

    [WebMethod]
    public static string GetDataRekening(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 200, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchIDx, typeof(string));
        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpershx, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Ar_Trn_Asset_Tarikan", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
}