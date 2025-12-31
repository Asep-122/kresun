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

public partial class BG_Master_Asabri : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

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
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    public string SessIsLevel
    {
        get { return Session["SessIsLevel"].ToString(); }
        set { ViewState["SessIsLevel"] = value; }

    }
    public string SessLevelUser
    {
        get { return Session["SessLevelUser"].ToString(); }
        set { ViewState["SessLevelUser"] = value; }

    }

    public string SessAreaUser
    {
        get { return Session["SessAreaUser"].ToString(); }
        set { ViewState["SessAreaUser"] = value; }
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
        pnlFilter.Visible = true;
        PnlFront.Visible = true;
        pnladd.Visible = false;
        Grdtagihan.UseAccessibleHeader = true;

        Grdtagihan.HeaderRow.TableSection = TableRowSection.TableHeader;
        //Response.Redirect(Request.Url.AbsoluteUri);
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (!Page.IsPostBack)
            //{
            //    CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            //    CultureName = "id-ID";
            //    BindGridEntity();


            //}
            //GRData.UseAccessibleHeader = true;

            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



          


            if (!IsPostBack)
            {


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ALLAREA", typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_area asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "nama";
                ddlcabang.DataValueField = "kode_area";
                ddlcabang.DataBind();
                ddlcabang.Items.Insert(0, new ListItem("Filter Area", "ALL"));

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "JENISPENSIUN", typeof(string));
                var1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var1, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "jenispensiun asc";
                ddljenispensiun.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddljenispensiun.DataTextField = "jenispensiun";
                ddljenispensiun.DataValueField = "kodepensiun";
                ddljenispensiun.DataBind();
                ddljenispensiun.Items.Insert(0, new ListItem("Filter Jenis", "ALL"));



                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "JENISJIWA", typeof(string));
                var2[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var2[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var2, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kodejiwa asc";
                ddljenisjiwa.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddljenisjiwa.DataTextField = "keterangan";
                ddljenisjiwa.DataValueField = "kodejiwa";
                ddljenisjiwa.DataBind();
                ddljenisjiwa.Items.Insert(0, new ListItem("Filter Jiwa", "ALL"));




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

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BG_ASABRI", typeof(string));
        var[1] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangprodukAsrn.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@Kantorbayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantor.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@jenispensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenispensiun.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@jenisjiwa", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisjiwa.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@jenisusia", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisusia.SelectedValue, typeof(string));
        var[6] = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamat.Text, typeof(string));
        var[7] = gb.CreateSPParameter("@nmdati4", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnmdati4.Text, typeof(string));
        var[8] = gb.CreateSPParameter("@kota", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkotasearch.Text, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nopens asc";


        Grdtagihan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtagihan.DataBind();

        Grdtagihan.UseAccessibleHeader = true;

        Grdtagihan.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        btnSimpan.Enabled = true;
    }
    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PRODUKASABARI", typeof(string));
        var1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
        var1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
        var1[3] = gb.CreateSPParameter("@kcu_giga", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlcabang.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kdcabang asc";
        ddlcabangprodukAsrn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabangprodukAsrn.DataTextField = "cabang";
        ddlcabangprodukAsrn.DataValueField = "kdcabang";
        ddlcabangprodukAsrn.DataBind();
        ddlcabangprodukAsrn.Items.Insert(0, new ListItem("Filter Produk", "ALL"));
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "", createuser = "", createddate = "", updateuser = "", updatedate = "";
        //try
        //{
        //    if (txtkodeproduk.Enabled == true)
        //    {
        //        STATE = "INSERT";
        //        createuser = SessLoginID;
        //        createddate = DateTime.Now.ToString();
        //        updatedate = "";
        //        updateuser = "";
        //    }
        //    else
        //    {
        //        STATE = "UPDATE";
        //        createuser = "";
        //        createddate = "";
        //        updatedate = DateTime.Now.ToString();
        //        updateuser = SessLoginID;
        //    }
        //    btnSimpan.Enabled = false;

        //    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
        //    var[0] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodeproduk.Text, typeof(string));
        //    var[1] = gb.CreateSPParameter("@NamaProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaproduk.Text, typeof(string));
        //    var[2] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, createuser, typeof(string));
        //    var[3] = gb.CreateSPParameter("@updateuser", ParameterDirection.Input, SqlDbType.VarChar, 50, updateuser, typeof(string));
        //    var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //    var[5] = gb.CreateSPParameter("@statusaktif", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatus.SelectedValue, typeof(string));
        //    var[6] = gb.CreateSPParameter("@Minimal_Saldo", ParameterDirection.Input, SqlDbType.Decimal, 0, txtminimalsaldo.Text, typeof(decimal));
        //    var[7] = gb.CreateSPParameter("@BungaTahunan", ParameterDirection.Input, SqlDbType.Float, 0, "0", typeof(float));
        //    var[8] = gb.CreateSPParameter("@BungaHarian", ParameterDirection.Input, SqlDbType.Float, 0, "0", typeof(float));
        //    var[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
        //    var[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

          
        //        ge.ListDataset = gb.GetDataJustOutPut("SN_PRODUK", var, "constrmf");
             


        //    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        //    {

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);


        //    }

        //    else
        //    {

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
        //        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //    }

        //}

        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}


    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {
   

      




            if (ddlcabangprodukAsrn.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Taspen tidak boleh kosong');", true);
                return;
            }

            if (ddlcabang.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Area tidak boleh kosong');", true);
                return;
            }
            //if (ddlKantor.Text.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kantor Bayar tidak boleh kosong');", true);
            //    return;
            //}

        BindGridEntity();


   
       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void ddlcabangprodukAsrn_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Pnltagihan.Visible = false;
        //string query;
        //query = "";
        //if (Lbltaspen.Text == "DATA PENSIUN TASPEN") //nama table menggunakan string
        //{


        //    query = "select x.kodebyr || ' - ' || x.nmkanbyr as kantor, x.kodebyr ";
        //    query = query + " from tbl_taspen_jubar x";
        //    query = query + " inner join tbl_taspen_jubar_cabang z on x.kodebyr = z.jubar";
        //    query = query + " where z.kodecabang = '" + ddlcabangprodukAsrn.SelectedValue + "'";

        //    DataTable dxs = GetData(query);
        //    ddlKantor.DataSource = dxs;
        //    ddlKantor.DataTextField = "kantor";
        //    ddlKantor.DataValueField = "kodebyr";
        //    ddlKantor.DataBind();

        //    ddlKantor.Items.Insert(0, new ListItem("Select", ""));

        //}
        //else if (Lbltaspen.Text == "DATA PENSIUN ASABRI")
        //{

        //    query = "";
        //    query = "select x.kodemitra || ' - ' || x.mitrabayar as zmitra_bayar, x.mitrabayar ";
        //    query = query + " from tbl_asabri_mitra_bayar x";
        //    query = query + " where x.kodecabang = '" + ddlcabangprodukAsrn.SelectedValue + "'";

        //    DataTable dxs = GetData(query);
        //    ddlKantor.DataSource = dxs;
        //    ddlKantor.DataTextField = "zmitra_bayar";
        //    ddlKantor.DataValueField = "mitrabayar";
        //    ddlKantor.DataBind();
        //    ddlKantor.Items.Insert(0, new ListItem("Select", ""));

        //}

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KANTORBAYARASABRI", typeof(string));
        var1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
        var1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
        var1[3] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlcabangprodukAsrn.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "mitrabayar asc";
        ddlKantor.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKantor.DataTextField = "zmitra_bayar";
        ddlKantor.DataValueField = "mitrabayar";
        ddlKantor.DataBind();
        ddlKantor.Items.Insert(0, new ListItem("Filter K Bayar", "ALL"));

    }
    public void TampilFormTaspen(string xnourut)
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


    protected void Grdtagihan_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string urut;
        urut = "";
        switch (e.CommandName)
        {

                case "VIEW":

                urut = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourut")).Text;
                pnlFilter.Visible = false;
                PnlFront.Visible = false;
                pnladd.Visible = true;
                Grdtagihan.UseAccessibleHeader = true;

                Grdtagihan.HeaderRow.TableSection = TableRowSection.TableHeader;
                TampilFormTaspen(urut);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                break;


        }
    }

}