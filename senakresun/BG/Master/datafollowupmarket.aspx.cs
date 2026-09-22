using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_datafollowupmarket : System.Web.UI.Page
{
    public string query = "";
    public string kondisi = "";
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    private Boolean SessIsSuperAdmin
    {
        get { return Boolean.Parse(Session["SessIsSuperAdmin"].ToString()); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
   
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ddlProduk.Enabled = true;
                ddlMarketing.Enabled = true;
                ddlstatusfollow.Enabled = true;
                ddltahun.SelectedValue = String.Format("{0:yyyy}", DateTime.Today);
                ddlbulan.SelectedValue = String.Format("{0:MM}", DateTime.Today);
                string query;

                Lbltaspen.Text = "REPORT DETAIL KUNJUNGAN";

                //ddlProduk.Items.Add("SELECT");
                ddlProduk.Items.Add("TASPEN");
                ddlProduk.Items.Add("ASABRI");

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ALLAREA", typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_area asc";
                ddlAreaSena.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlAreaSena.DataTextField = "nama";
                ddlAreaSena.DataValueField = "kode_area";
                ddlAreaSena.DataBind();
                ddlAreaSena.Items.Insert(0, new ListItem("Filter Area", "ALL"));

              


            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnKembaliasbri_Click(object sender, EventArgs e)
    {
        PnlPeta.Visible = false;
        pnlformatasabri.Visible = false;
        Pnlsearch.Visible = true;
        pnlFollowUpTaspen.Visible = true;
        BindGridDataFollowUpTaspen();

    }


    protected void btnKembalitaspen_Click(object sender, EventArgs e)
    {
        PnlPeta.Visible = false;
        Pnlsearch.Visible = true;
        pnlFollowUpTaspen.Visible = true;
        //Pnltagihan.Visible = true;

        pnlformattaspen.Visible = false;
        BindGridDataFollowUpTaspen();


    }

   
  
   
  



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
   

    protected void btnSearchFollowUp_Click(object sender, EventArgs e)
    {
        
        if (Lbljudul.Text == "REPORT DETAIL KUNJUNGAN")
        {

            if (ddlProduk.SelectedValue == "TASPEN")
            {
                

                BindGridDataFollowUpTaspen();

                pnlFollowUpTaspen.Visible = true;
                pnlAsabriFollowUp.Visible = false;
            }
            else
            {

                BindGridDataFollowUpAsabri();

                pnlAsabriFollowUp.Visible = true;
                pnlFollowUpTaspen.Visible = false;
            }
        }
        else
        {
            

            //GenerateDataRekap();
            BindGridDataRekap();
            Pnlrekap.Visible = true;

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

    protected void grdAsabriFollowUp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string urut;
        urut = "";
       
        switch (e.CommandName)
        {
            case "VIEW":

                urut = ((Label)grdAsabriFollowUp.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourutasabri")).Text;
                //  pnlData.Visible = false;
                pnlAsabriFollowUp.Visible = false;
                Pnlsearch.Visible = false;
                pnlformatasabri.Visible = true;
                TampilFormAsabri(urut);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                break;
           
        }



    }
    public void TampilFormAsabri(string xnourut)
    {
        string query = "";

        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[1];
        Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, xnourut, typeof(string));

        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.BG_Get_Transaksi_TASPEN", Paramsc, "constrmf").Tables[0];
        ge.ListDataset = gb.GetDataJustOutPut("BG_Get_Transaksi_ASABRI", Paramsc, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            txtnopensasbri.Text = ge.ListDataset.Tables[0].Rows[0]["nopens"].ToString();
            txtnrpasbri.Text = ge.ListDataset.Tables[0].Rows[0]["nrp"].ToString();
            txtkdjiwaasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kode_jiwa"].ToString();
            txtnamaasbri.Text = ge.ListDataset.Tables[0].Rows[0]["nama"].ToString();
            txttgllahirasabri.Text = ge.ListDataset.Tables[0].Rows[0]["tgllahir"].ToString();
            txtusiaasbri.Text = ge.ListDataset.Tables[0].Rows[0]["usia"].ToString();
            txtkd_jbyasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kd_jby"].ToString();
            txtpangkatasbri.Text = ge.ListDataset.Tables[0].Rows[0]["pangkat"].ToString();
            txtkodecabangasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
            txtnamacabangasbri.Text = ge.ListDataset.Tables[0].Rows[0]["namacabang"].ToString();
            Txtmitraasbri.Text = ge.ListDataset.Tables[0].Rows[0]["mitra"].ToString();

            txtpendapatanpokokasbri.Text = ge.ListDataset.Tables[0].Rows[0]["pens_pokok"].ToString();
            txttunjistriabsri.Text = ge.ListDataset.Tables[0].Rows[0]["tunj_istri"].ToString();
            txttunjanakasbri.Text = ge.ListDataset.Tables[0].Rows[0]["tunj_anak"].ToString();
            txttunberasbri.Text = ge.ListDataset.Tables[0].Rows[0]["tunj_beras"].ToString();
            txttunjlainasbri.Text = ge.ListDataset.Tables[0].Rows[0]["tunj_lain"].ToString();
            txtbrutoasbri.Text = ge.ListDataset.Tables[0].Rows[0]["jml_bruto"].ToString();
            txtpot_pph21.Text = ge.ListDataset.Tables[0].Rows[0]["pot_pph21"].ToString();
            txtpot_askes.Text = ge.ListDataset.Tables[0].Rows[0]["pot_askes"].ToString();
            txtpot_hutang.Text = ge.ListDataset.Tables[0].Rows[0]["pot_hutang"].ToString();
            txtpot_lain.Text = ge.ListDataset.Tables[0].Rows[0]["pot_lain"].ToString();
            txtjml_potong.Text = ge.ListDataset.Tables[0].Rows[0]["jml_potong"].ToString();
            txtjlm_netto.Text = ge.ListDataset.Tables[0].Rows[0]["jlm_netto"].ToString();
            txtpembulatan.Text = ge.ListDataset.Tables[0].Rows[0]["pembulatan"].ToString();


            txtalamatasbri.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
            txtrtasbri.Text = ge.ListDataset.Tables[0].Rows[0]["rt"].ToString();
            txtrwasbri.Text = ge.ListDataset.Tables[0].Rows[0]["rw"].ToString();
            txtkelurahanasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
            txtkecamatanasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
            txtprovinsiasbri.Text = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();

            txtbank_rekening.Text = ge.ListDataset.Tables[0].Rows[0]["bank_rekening"].ToString();
            txtjenis_rekening.Text = ge.ListDataset.Tables[0].Rows[0]["jenis_rekening"].ToString();
            txtno_rekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();


        }
    }

    protected void grdFollowUpTaspen_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string urut,Data;
        urut = "";
        Data = "";
        switch (e.CommandName)
        {
           
            case "VIEW":

                urut = ((Label)grdFollowUpTaspen.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspen")).Text;
                Data = ((Label)grdFollowUpTaspen.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbldata")).Text;
                //  pnlData.Visible = false;
                TextBox1.Text = ((Label)grdFollowUpTaspen.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllat")).Text;
                TextBox2.Text = ((Label)grdFollowUpTaspen.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllon")).Text;
                PnlPeta.Visible = true;
                if (Data=="TASPEN")
                {
                    pnlFollowUpTaspen.Visible = false;
                    Pnlsearch.Visible = false;
                    pnlformattaspen.Visible = true;
                    TampilFormTaspen(urut);
                }
                else
                {
                    pnlFollowUpTaspen.Visible = false;
                    Pnlsearch.Visible = false;
                    pnlformatasabri.Visible = true;
                    TampilFormAsabri(urut);
                }
                


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                break;

        }



    }
    public void TampilFormTaspen(string xnourut)
    {
        string query = "";

        //numberFormat is the formatter I use for numbers.

        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[1];
        Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, xnourut, typeof(string));

        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.BG_Get_Transaksi_TASPEN", Paramsc, "constrmf").Tables[0];
        ge.ListDataset = gb.GetDataJustOutPut("BG_Get_Transaksi_TASPEN", Paramsc, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            txtnotas.Text = ge.ListDataset.Tables[0].Rows[0]["notas"].ToString();
            Txtjenis.Text = ge.ListDataset.Tables[0].Rows[0]["jenispensiun"].ToString();
            txtnamapensiun.Text = ge.ListDataset.Tables[0].Rows[0]["namapensiunan"].ToString();
            txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir_pensiunan"].ToString();
            txtkodepangkat.Text = ge.ListDataset.Tables[0].Rows[0]["kdpangkat"].ToString();
            txtcabang.Text = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
            txtkodebyr.Text = ge.ListDataset.Tables[0].Rows[0]["kodebyr"].ToString();
            txtnamabyr.Text = ge.ListDataset.Tables[0].Rows[0]["nmkanbyr"].ToString();
            txtjiwa.Text = ge.ListDataset.Tables[0].Rows[0]["ketjiwa"].ToString();
            txtstop.Text = ge.ListDataset.Tables[0].Rows[0]["nmstop"].ToString();
            txttelepon.Text = ge.ListDataset.Tables[0].Rows[0]["telepon"].ToString();

            txtpenpok.Text = ge.ListDataset.Tables[0].Rows[0]["penpok"].ToString();

            txttistri.Text = ge.ListDataset.Tables[0].Rows[0]["tistri"].ToString();
            txttanak.Text = ge.ListDataset.Tables[0].Rows[0]["tanak"].ToString();
            txttpajak.Text = ge.ListDataset.Tables[0].Rows[0]["tpajak"].ToString();
            Txttberas.Text = ge.ListDataset.Tables[0].Rows[0]["tberas"].ToString();
            txtpenyesuaian.Text = ge.ListDataset.Tables[0].Rows[0]["penyesuaian"].ToString();
            txttbulat.Text = ge.ListDataset.Tables[0].Rows[0]["tbulat"].ToString();
            TxtKotor.Text = ge.ListDataset.Tables[0].Rows[0]["kotor"].ToString();
            TxtBersih.Text = ge.ListDataset.Tables[0].Rows[0]["bersih"].ToString();
            TxtRapel.Text = ge.ListDataset.Tables[0].Rows[0]["rapel"].ToString();


            Txtpenerima.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penerima"].ToString();
            Txttgllahirpenerima.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir_penerima"].ToString();
            Txtalamatpenerima.Text = ge.ListDataset.Tables[0].Rows[0]["alm_peserta"].ToString();
            Txtdati4.Text = ge.ListDataset.Tables[0].Rows[0]["nmdati4"].ToString();
            txtkota.Text = ge.ListDataset.Tables[0].Rows[0]["kota"].ToString();


            txtnourutdapem.Text = ge.ListDataset.Tables[0].Rows[0]["norutdapem"].ToString();
            Txttmppensiun.Text = ge.ListDataset.Tables[0].Rows[0]["tmtpensiun"].ToString();
            TxtNoskep.Text = ge.ListDataset.Tables[0].Rows[0]["nomor_skep"].ToString();
            TxttglSkep.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_skep"].ToString();
            Txtnorek.Text = ge.ListDataset.Tables[0].Rows[0]["norek"].ToString();
            Txtpenerbitskep.Text = ge.ListDataset.Tables[0].Rows[0]["penerbit_skep"].ToString();
            txtnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["npwp"].ToString();

        }
    }
   
    public void BindGridDataFollowUpAsabri()
    {
        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BG_ASABRI", typeof(string));
        var[1] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlProduk.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlMarketing.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@statusfollow", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusfollow.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_Get_Data_Report", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nopens asc";


        grdAsabriFollowUp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        grdAsabriFollowUp.DataBind();

        grdAsabriFollowUp.UseAccessibleHeader = true;

        grdAsabriFollowUp.HeaderRow.TableSection = TableRowSection.TableHeader;

       
       


    }
    public void BindGridDataFollowUpTaspen()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BG_TASPEN", typeof(string));
        var[1] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlProduk.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlMarketing.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@statusfollow", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusfollow.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_Get_Data_Report", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "notas asc";


        grdFollowUpTaspen.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        grdFollowUpTaspen.DataBind();

        grdFollowUpTaspen.UseAccessibleHeader = true;

        grdFollowUpTaspen.HeaderRow.TableSection = TableRowSection.TableHeader;



    }


    //public void GenerateDataRekap()
    //{

    //    string query;

    //    query = "SELECT * FROM generate_rekap_kunjungan('" + ddlbulan.SelectedValue + "','" + ddltahun.SelectedValue + "','" + ddlCabangSena.SelectedValue + "','ALL','" + SessLoginID + "')";
    //    DataTable dt = GetData(query);




    //}


    public void BindGridDataRekap()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GENERATE", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlMarketing.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlMarketing.SelectedItem.Text, typeof(string));
        
        ge.ListDataset = gb.GetDataJustOutPut("BG_Get_Data_Report", var, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "namasales asc";


        GrdReport.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdReport.DataBind();

        GrdReport.UseAccessibleHeader = true;

        GrdReport.HeaderRow.TableSection = TableRowSection.TableHeader;



       

    }

 
    protected void ddlAreaSena_SelectedIndexChanged(object sender, EventArgs e)
    {

        var datamarket = "";

      
            datamarket = SessLoginID;
      

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BAHAN", typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlProduk.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlAreaSena.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, datamarket, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_Get_Data_Report", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "username asc";
        ddlMarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlMarketing.DataTextField = "name";
        ddlMarketing.DataValueField = "username";
        ddlMarketing.DataBind();
        ddlMarketing.Items.Insert(0, new ListItem("Pilih Marketing", "ALL"));
        ddlMarketing.Enabled = true;

    }
    //protected void ddlCabangSena_SelectedIndexChanged(object sender, EventArgs e)
    //{


       



    //}

 

    protected void btndetail_Click(object sender, EventArgs e)
    {


        Lbltaspen.Text = "REPORT DETAIL KUNJUNGAN";

        pnlFollowUpTaspen.Visible = false;
        pnlAsabriFollowUp.Visible = false;
        pnlformattaspen.Visible = false;
        pnlformatasabri.Visible = false;
        Pnlrekap.Visible = false;
        Lbljudul.Text = "REPORT DETAIL KUNJUNGAN";
        ddlProduk.Enabled = true;
        ddlMarketing.Enabled = true;
        ddlstatusfollow.Enabled = true;

    }

    protected void btnrekap_Click(object sender, EventArgs e)
    {

 
        Lbltaspen.Text = "REPORT REKAP KUNJUNGAN";

        pnlFollowUpTaspen.Visible = false;
        pnlAsabriFollowUp.Visible = false;
        pnlformattaspen.Visible = false;
        pnlformatasabri.Visible = false;
        Pnlrekap.Visible = false;
        Lbljudul.Text = "REPORT REKAP KUNJUNGAN";

        //ddlProduk.Enabled = false;
        //ddlMarketing.Enabled = false;
        //ddlstatusfollow.Enabled = false;

    }
}