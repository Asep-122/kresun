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

public partial class BG_Master_Taspen : System.Web.UI.Page
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
        //Response.Redirect(Request.Url.AbsoluteUri);
        pnlFilter.Visible = true;
        PnlFront.Visible = true;
        pnladd.Visible = false;
        Grdtagihan.UseAccessibleHeader = true;

        Grdtagihan.HeaderRow.TableSection = TableRowSection.TableHeader;
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

                //query = "Select b.kode_area || ' - ' || b.nama_area as nama, b.kode_area  from users a inner join tbl_area b on A.nik = B.nik_area where username = '" + SessLoginID + "'";
                //DataTable area = GetData(query);
                //ddlcabang.DataSource = area;
                //ddlcabang.DataTextField = "nama";
                //ddlcabang.DataValueField = "kode_area";
                //ddlcabang.DataBind();
                //ddlcabang.Items.Insert(0, new ListItem("Select", ""));

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ALLAREA", typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                var[3] = gb.CreateSPParameter("@SessBranchId", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));
                var[4] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, "I", typeof(string));

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

                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TASPENSTOP", typeof(string));
                var3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kdstop asc";
                ddljenisstop.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddljenisstop.DataTextField = "nmstop";
                ddljenisstop.DataValueField = "kdstop";
                ddljenisstop.DataBind();
                ddljenisstop.Items.Insert(0, new ListItem("Filter ", "ALL"));




           


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

      
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[10];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BG_TASPEN", typeof(string));
        var[1] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangprodukAsrn.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@Kantorbayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantor.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@jenispensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenispensiun.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@jenisjiwa", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisjiwa.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@jenisstop", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisstop.SelectedValue, typeof(string));
        var[6] = gb.CreateSPParameter("@jenisusia", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisusia.SelectedValue, typeof(string));
        var[7] = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamat.Text, typeof(string));
        var[8] = gb.CreateSPParameter("@nmdati4", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnmdati4.Text, typeof(string));
        var[9] = gb.CreateSPParameter("@kota", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkotasearch.Text, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "notas asc";


        Grdtagihan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtagihan.DataBind();

        Grdtagihan.UseAccessibleHeader = true;

        Grdtagihan.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
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
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PRODUKTASPEN", typeof(string));
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

        try
        {
            //DataTable ds = new DataTable();
            //ds = null;

            //Grdtagihan.DataSource = ds;
            //Grdtagihan.DataBind();




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

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
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
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KANTORBAYAR", typeof(string));
        var1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
        var1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
        var1[3] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlcabangprodukAsrn.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("BG_GetCabang_Login", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kodebyr asc";
        ddlKantor.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKantor.DataTextField = "kantor";
        ddlKantor.DataValueField = "kodebyr";
        ddlKantor.DataBind();
        ddlKantor.Items.Insert(0, new ListItem("Filter K Bayar", "ALL"));

    }
    public void TampilFormTaspen(string xnourut)
    {
        string query = "";

        
        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[1];
        Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, xnourut, typeof(string));

        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.BG_Get_Transaksi_TASPEN", Paramsc, "constrmf").Tables[0];
        ge.ListDataset = gb.GetDataJustOutPut("BG_Get_Transaksi_TASPEN", Paramsc, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {        
            txtnotas.Text = ge.ListDataset.Tables[0].Rows[0]["notas"].ToString();
            Txtjenis.Text = ge.ListDataset.Tables[0].Rows[0]["jenispensiun"].ToString(); //dt.Rows[0]["jenispensiun"].ToString();
            txtnamapensiun.Text = ge.ListDataset.Tables[0].Rows[0]["namapensiunan"].ToString(); //dt.Rows[0]["namapensiunan"].ToString();
            txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir_pensiunan"].ToString(); //dt.Rows[0]["tgl_lahir_pensiunan"].ToString();
            txtkodepangkat.Text = ge.ListDataset.Tables[0].Rows[0]["kdpangkat"].ToString(); //dt.Rows[0]["kdpangkat"].ToString();
            txtcabang.Text = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString(); //dt.Rows[0]["kodecabang"].ToString();
            txtkodebyr.Text = ge.ListDataset.Tables[0].Rows[0]["kodebyr"].ToString(); //dt.Rows[0]["kodebyr"].ToString();
            txtnamabyr.Text = ge.ListDataset.Tables[0].Rows[0]["nmkanbyr"].ToString(); //dt.Rows[0]["nmkanbyr"].ToString();
            txtjiwa.Text = ge.ListDataset.Tables[0].Rows[0]["ketjiwa"].ToString(); //dt.Rows[0]["ketjiwa"].ToString();
            txtstop.Text = ge.ListDataset.Tables[0].Rows[0]["nmstop"].ToString(); ///dt.Rows[0]["nmstop"].ToString();
            txttelepon.Text = ge.ListDataset.Tables[0].Rows[0]["telepon"].ToString(); //dt.Rows[0]["telepon"].ToString();

            txtpenpok.Text = ge.ListDataset.Tables[0].Rows[0]["penpok"].ToString(); //dt.Rows[0]["penpok"].ToString();

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