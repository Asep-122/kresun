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

public partial class Report_datamarketing : System.Web.UI.Page
{
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
  

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    public int bahanhariini = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //BindGrddatawilayahEntity();
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

                string query;


                btnproses.Visible = false;
                Lbltaspen.Text = "DATA TASPEN / ASABRI";

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "USERLOCK", typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", var, "constrmf");
                bahanhariini = Convert.ToInt32(ge.ListDataset.Tables[0].Rows[0][0].ToString());

                DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];
                vart[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ALLAREA", typeof(string));
                vart[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                vart[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", vart, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_area asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "nama";
                ddlcabang.DataValueField = "kode_area";
                ddlcabang.DataBind();
                ddlcabang.Items.Insert(0, new ListItem("Filter Area", "ALL"));

      
                if (bahanhariini < 5)
                {

                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                    varx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOTALBAHAN", typeof(string));
                    varx[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                    varx[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx, "constrmf");

                    //bahanhariini = Convert.ToInt32(ge.ListDataset.Tables[0].Rows[0][0].ToString());

                    LblOutstanding.Text = (bahanhariini + Convert.ToInt32(ge.ListDataset.Tables[0].Rows[0][0].ToString())).ToString();
                    LblOutstanding.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblOutstanding.Text));



                    DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
                    varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOTALBAHANTMP", typeof(string));
                    varx1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                    varx1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx1, "constrmf");


                    //DataTable dtwrx = GetData(query);
                    LblBahan.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                    LblBahan.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));

                    DefaultClass.Global.SqlHelper[] varx2 = new DefaultClass.Global.SqlHelper[3];
                    varx2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MINIMALBAHAN", typeof(string));
                    varx2[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                    varx2[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx2, "constrmf");

                    //DataTable dtwrs = GetData(query);
                    Lblminimal.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                    Lblminimal.Text = String.Format("{0:#,###0}", Convert.ToInt32(Lblminimal.Text));
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Bahan sudah melebihi dari ketentuan per hari!');", true);
                }
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

        }
    }



    public void BindGridEntityTagihan()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[10];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWTASPEN", typeof(string));
        var[1] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangprodukAsrn.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@Kantorbayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantor.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@jenispensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenispensiun.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@jenisjiwa", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisjiwa.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@jenisstop", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
        var[6] = gb.CreateSPParameter("@jenisusia", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisusia.SelectedValue, typeof(string));
        var[7] = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamat.Text, typeof(string));
        var[8] = gb.CreateSPParameter("@nmdati4", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnmdati4.Text, typeof(string));
        var[9] = gb.CreateSPParameter("@kota", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkotasearch.Text, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "notas asc";


        Grdtagihan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtagihan.DataBind();

        Grdtagihan.UseAccessibleHeader = true;

        Grdtagihan.HeaderRow.TableSection = TableRowSection.TableHeader;




    }

    
    public void BindGridEntityTunggakan()
    {
        string query;
        query = "";

        query = " select d.nourut, d.produk,x.tgl_lahir_penerima as tgllahir,EXTRACT(year from Age(x.tgl_lahir_penerima)) as usia,x.alm_peserta || ' ' || x.nmdati4 || ' ' || x.kota as alamat ,x.notas, x.nama_penerima as nama,bersih as gaji";
        query = query + " FROM tbl_taspen x";
        query = query + " inner join tbl_get_bahan_tmp d on cast(x.nourut as TEXT) = d.nourut";
        query = query + " where d.username ='" + SessLoginID + "' and d.produk = '1'";
        query = query + " UNION ALL";
        query = query + " select d.nourut, d.produk, x.tgllahir as tgllahir,x.usia,x.alamat || ' ' || x.rt || ' ' || x.rw || ' ' || x.kelurahan || ' ' || x.kecamatan || ' ' || x.provinsi as alamat ,x.nopens, x.nama,x.bersih as gaji ";
        query = query + " FROM tbl_asabri x";
        query = query + " inner join tbl_get_bahan_tmp d on cast(x.nourut as TEXT) = d.nourut";
        //query = query + " inner join tbl_asabri_cabang z on x.kodecabang = z.kdcabang";
        query = query + " where d.username ='" + SessLoginID + "' and d.produk = '2'";



        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWALL", typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nourut asc";


        Grdtunggakan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtunggakan.DataBind();

        Grdtunggakan.UseAccessibleHeader = true;

        Grdtunggakan.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    public void BindGridEntityAsabri()
    {
       

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[10];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWASABRI", typeof(string));
        var[1] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangprodukAsrn.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@Kantorbayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKantor.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@jenispensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenispensiun.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@jenisjiwa", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisjiwa.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@jenisstop", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
        var[6] = gb.CreateSPParameter("@jenisusia", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisusia.SelectedValue, typeof(string));
        var[7] = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamat.Text, typeof(string));
        var[8] = gb.CreateSPParameter("@nmdati4", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnmdati4.Text, typeof(string));
        var[9] = gb.CreateSPParameter("@kota", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkotasearch.Text, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nourut asc";


        Grdasabri.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdasabri.DataBind();

        Grdasabri.UseAccessibleHeader = true;

        Grdasabri.HeaderRow.TableSection = TableRowSection.TableHeader;



    }
    protected void btnproses_Click(object sender, EventArgs e)
    {
       
        try
        {
            if (Convert.ToInt32(LblBahan.Text) < Convert.ToInt32(Lblminimal.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bahan kurang dari batas minimal');", true);
                Grdtunggakan.UseAccessibleHeader = true;
                Grdtunggakan.HeaderRow.TableSection = TableRowSection.TableHeader;
                return;
            }
            if (Convert.ToInt32(LblBahan.Text) > Convert.ToInt32("10"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bahan tidak boleh lebih dari 10 setiap hari ');", true);
                Grdtunggakan.UseAccessibleHeader = true;
                Grdtunggakan.HeaderRow.TableSection = TableRowSection.TableHeader;
                return;
            }
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("BG_get_bahan", var, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Save Get Bahan Success');", true);
                Pnltunggakan.Visible = false;
                btnproses.Visible = false;
                LblOutstanding.Text = "0";
                LblBahan.Text = "0";

            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }

       
    protected void lnkCari_Click(object sender, EventArgs e)
    {
     
        //DataTable ds = new DataTable();
        //ds = null;

      
        //Grdtagihan.DataSource = ds;
        //Grdtagihan.DataBind();
      
        //Grdtunggakan.DataSource = ds;
        //Grdtunggakan.DataBind();


        //Grdasabri.DataSource = ds;
        //Grdasabri.DataBind();

        if (ddlbahan.SelectedValue == "TASPEN")
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
            Pnltagihan.Visible = true;
             BindGridEntityTagihan();


        }
        else if (ddlbahan.SelectedValue == "ASABRI")
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
            Pnlasabari.Visible = true;
            //Pnltunggakan.Visible = true;

            BindGridEntityAsabri();


        }
       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

    
    protected void Grdasabri_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            string urut;
            urut = "";
            switch (e.CommandName)
            {
                case "APPROVE":
                   
                    string nouruta;
                    nouruta = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourutasabri")).Text;
                    



                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEASABRI", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0,int.Parse(nouruta), typeof(int));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_get_bahan", var, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Ambil Done!','You successfully save this Project','top-right','success','1');", true);
                        Pnlasabari.Visible = true;
                        BindGridEntityAsabri();
                        DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
                        varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOTALBAHANTMP", typeof(string));
                        varx1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                        varx1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx1, "constrmf");


                        //DataTable dtwrx = GetData(query);
                        LblBahan.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                        LblBahan.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));


                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }

                   
                    break;
                case "VIEW":

                    urut = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourutasabri")).Text;
                    //  pnlData.Visible = false;
                    Pnlsearch.Visible = false;
                    Pnlasabari.Visible = false;
                    pnlformatasabri.Visible = true;
                    LinkButton4.Visible = false;
                    LinkButton2.Visible = true;
                    TampilFormAsabri(urut);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                    break;

            }


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
     


    }

    protected void Grdtagihan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string urut;
            urut = "";
            switch (e.CommandName)
            {
                case "APPROVE":
                    string nouruta;
                    nouruta = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspen")).Text;




                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATETASPEN", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(nouruta), typeof(int));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_get_bahan", var, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Ambil Done!','You successfully save this Project','top-right','success','1');", true);
                        Pnltagihan.Visible = true;
                        BindGridEntityTagihan();
                        DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
                        varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOTALBAHANTMP", typeof(string));
                        varx1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                        varx1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx1, "constrmf");


                        //DataTable dtwrx = GetData(query);
                        LblBahan.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                        LblBahan.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));


                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }





                    break;

                case "VIEW":

                    urut = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspen")).Text;
                    //  pnlData.Visible = false;
                    Pnlsearch.Visible = false;
                    Pnltagihan.Visible = false;
                    pnlformattaspen.Visible = true;
                    TampilFormTaspen(urut);
                    LinkButton3.Visible = false;
                    LinkButton1.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                    break;
            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        


    }
    public void TampilFormTaspen(string xnourut)
    {
       

            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[2];
        Paramsc[0] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.VarChar, 50, xnourut, typeof(string));
        Paramsc[1] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TAMPILTASPEN", typeof(string));

        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.BG_Get_Transaksi_TASPEN", Paramsc, "constrmf").Tables[0];
        ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", Paramsc, "constrmf");
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
    protected void Grdtunggakan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string urut;
            urut = "";
            string nourutproduk;
            nourutproduk = "";
            switch (e.CommandName)
            {
                case "APPROVE":
                    string querya;
                    string nouruta;

                    nouruta = ((Label)Grdtunggakan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspenx")).Text;
                    nourutproduk = ((Label)Grdtunggakan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourutproduk")).Text;
                    if (nourutproduk == "1")
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETETASPEN", typeof(string));
                        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[4] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(nouruta), typeof(int));

                        ge.ListDataset = gb.GetDataJustOutPut("BG_get_bahan", var, "constrmf");



                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Delete Done!','You successfully save this Project','top-right','success','1');", true);
                            

                        }

                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                        }




                    }
                    else if (nourutproduk == "2")
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEASABRI", typeof(string));
                        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[4] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(nouruta), typeof(int));

                        ge.ListDataset = gb.GetDataJustOutPut("BG_get_bahan", var, "constrmf");



                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Delete Done!','You successfully save this Project','top-right','success','1');", true);


                        }

                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                        }



                        

                    }

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Ambil Done!','You successfully save this Project','top-right','success','1');", true);

                    Pnltunggakan.Visible = true;
                    BindGridEntityTunggakan();
                    DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
                    varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOTALBAHANTMP", typeof(string));
                    varx1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                    varx1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx1, "constrmf");


                    //DataTable dtwrx = GetData(query);
                    LblBahan.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                    LblBahan.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));




                    break;

                case "VIEW":
                    nourutproduk = ((Label)Grdtunggakan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourutproduk")).Text;
                    if (nourutproduk == "1")

                    {
                        urut = ((Label)Grdtunggakan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspenx")).Text;
                        //  pnlData.Visible = false;
                        Pnlsearch.Visible = false;
                        Pnltunggakan.Visible = false;
                        pnlformattaspen.Visible = true;
                        TampilFormTaspen(urut);
                        LinkButton3.Visible = true;
                        LinkButton1.Visible = false;
                    }

                    else if (nourutproduk == "2")
                    {
                        urut = ((Label)Grdtunggakan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspenx")).Text;
                        //  pnlData.Visible = false;
                        Pnlsearch.Visible = false;
                        Pnltunggakan.Visible = false;
                        pnlformatasabri.Visible = true;
                        LinkButton2.Visible = false;
                        LinkButton4.Visible = true;
                        TampilFormAsabri(urut);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

                    break;

            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
      



    }
    public void TampilFormAsabri(string xnourut)
    {
       

        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[2];
        Paramsc[0] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.VarChar, 50, xnourut, typeof(string));
        Paramsc[1] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TAMPILASABRI", typeof(string));

        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.BG_Get_Transaksi_TASPEN", Paramsc, "constrmf").Tables[0];
        ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", Paramsc, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {



            txtnotas.Text = ge.ListDataset.Tables[0].Rows[0]["notas"].ToString();
         

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
    protected void btnKembaliasbri_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = true;
        Pnlasabari.Visible = true;

        pnlformatasabri.Visible = false;
        BindGridEntityAsabri();


    }

    protected void btnKembaliasbriget_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = true;
        Pnltunggakan.Visible = true;

        pnlformatasabri.Visible = false;
        BindGridEntityTunggakan();


    }

    protected void btnKembalitaspen_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = true;
        Pnltagihan.Visible = true;

        pnlformattaspen.Visible = false;
        BindGridEntityTagihan();


    }
    protected void btnKembalitaspenget_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = true;
        Pnltunggakan.Visible = true;

        pnlformattaspen.Visible = false;
        BindGridEntityTunggakan();


    }    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btntagihan_Click(object sender, EventArgs e)
    {

      
        ddlcabangprodukAsrn.Items.Clear();
        ddlKantor.Items.Clear();
        Lbltaspen.Text = "DATA TASPEN / ASABRI";
        ddlcabang.Enabled = true;
        ddlcabangprodukAsrn.Enabled = true;
        ddlbahan.Enabled = true;
        ddlKantor.Enabled = true;
        btnsearch.Visible = true;
        btnproses.Visible = false;
        Pnlsearch.Visible = true;
        Pnltagihan.Visible = false;
        Pnltunggakan.Visible = false;
        Pnlasabari.Visible = false;
        PnlManual.Visible = false;
        Lbljudul.Text = "DATA SYSTEM";


    }

    
    protected void btnmanual_Click(object sender, EventArgs e)
    {
        Lbltaspen.Text = "MANUAL DATA";
        Lbljudul.Text = "MANUAL DATA";
        ddlcabangprodukAsrn.Items.Clear();

        Pnlsearch.Visible = false;
        Pnltagihan.Visible = false;
        Pnlasabari.Visible = false;
        Pnltunggakan.Visible = false;
        PnlManual.Visible = true;
        //BindGridEntityTunggakan();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void btntunggakan_Click(object sender, EventArgs e)
    {
        
        ddlcabangprodukAsrn.Items.Clear();
        ddlKantor.Items.Clear();
        Lbltaspen.Text = "GET DATA";
        Lbljudul.Text = "GET DATA";
        ddlcabang.Enabled = false;
        ddlcabangprodukAsrn.Enabled = false;
        ddlbahan.Enabled = false;
        ddlKantor.Enabled = true;
        Pnlsearch.Visible = true;
        btnsearch.Visible = false;
        btnproses.Visible = true;
        Pnltagihan.Visible = false;
        Pnlasabari.Visible = false;
        Pnltunggakan.Visible = true;
        PnlManual.Visible = false;
        BindGridEntityTunggakan();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }



   
   

    protected void ddlcabangprodukAsrn_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pnltagihan.Visible = false;
        string query;
        query = "";
        if (ddlbahan.SelectedValue == "TASPEN") //nama table menggunakan string
        {
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
            ddlKantor.Items.Insert(0, new ListItem("Filter", "ALL"));

            

        }
        else if (ddlbahan.SelectedValue == "ASABRI")
        {

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
            ddlKantor.Items.Insert(0, new ListItem("Filter", "ALL"));


        }
       
    }
    
   protected void ddldatamanual_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        string query;
        query = "";
        if (ddldatamanual.SelectedValue == "TASPEN") //nama table menggunakan string
        {
            //query = "Select b.kode_area || ' - ' || b.nama_area as nama, b.kode_area  from tbl_area b where b.kode_area='" + SessAreaUser + "' ";
            DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];
            vart[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "USERTASPEN", typeof(string));
            vart[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            vart[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));
            //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", vart, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kdcabang asc";
            ddlcabangmanual.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabangmanual.DataTextField = "cabang";
            ddlcabangmanual.DataValueField = "kdcabang";
            ddlcabangmanual.DataBind();
            ddlcabangmanual.Items.Insert(0, new ListItem("Filter Cabang", "ALL"));
        }
        else if (ddldatamanual.SelectedValue == "ASABRI")
        {
            DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];
            vart[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "USERASABRI", typeof(string));
            vart[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            vart[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));
            //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", vart, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kdcabang asc";
            ddlcabangmanual.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabangmanual.DataTextField = "cabang";
            ddlcabangmanual.DataValueField = "kdcabang";
            ddlcabangmanual.DataBind();
            ddlcabangmanual.Items.Insert(0, new ListItem("Filter Cabang", "ALL"));

        }

       // DataTable dx = GetData(query);
       // ddlcabangmanual.DataSource = dx;
       // ddlcabangmanual.DataTextField = "cabang";
       // ddlcabangmanual.DataValueField = "kdcabang";
       // ddlcabangmanual.DataBind();
       // //ddlcabangmanual.Items.Insert(0, new ListItem("Select", "--"));
       //// ddlcabangmanual.Enabled = true;
    }
    protected void btnSimpan_manual_Click(object sender, EventArgs e)
    {
        try
        {


            if (ddldatamanual.SelectedValue == "TASPEN") //nama table menggunakan string
            {

                if (Txtnamamanual.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama tidak boleh Kosong!!','top-right');", true);
                }

                else
                {


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
                    var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTTASPEN", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@notas", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnotasmanual.Text, typeof(string));
                    var[5] = gb.CreateSPParameter("@namapensiunan", ParameterDirection.Input, SqlDbType.VarChar, 80, Txtnamamanual.Text, typeof(string));
                    var[6] = gb.CreateSPParameter("@alm_peserta", ParameterDirection.Input, SqlDbType.VarChar, 150, txtalamatmanual.Text, typeof(string));
                    var[7] = gb.CreateSPParameter("@cabangmanual", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangmanual.SelectedValue, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_get_bahan_Manual", var, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Manual Done!','You successfully save this Project','top-right','success','1');", true);
                        txtnotasmanual.Text = "";
                        Txtnamamanual.Text = "";
                        txtalamatmanual.Text = "";
                        DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
                        varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOTALBAHANTMP", typeof(string));
                        varx1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                        varx1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx1, "constrmf");


                        //DataTable dtwrx = GetData(query);
                        LblBahan.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                        LblBahan.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));


                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }



                                   
                    

                    }
               
                


            }
            else if(ddldatamanual.SelectedValue == "ASABRI")
            {

                if (Txtnamamanual.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama tidak boleh Kosong!!','top-right');", true);
                }
                else
                {

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
                    var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTASABRI", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@notas", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnotasmanual.Text, typeof(string));
                    var[5] = gb.CreateSPParameter("@namapensiunan", ParameterDirection.Input, SqlDbType.VarChar, 80, Txtnamamanual.Text, typeof(string));
                    var[6] = gb.CreateSPParameter("@alm_peserta", ParameterDirection.Input, SqlDbType.VarChar, 150, txtalamatmanual.Text, typeof(string));
                    var[7] = gb.CreateSPParameter("@cabangmanual", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangmanual.SelectedValue, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("BG_get_bahan_Manual", var, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Manual Done!','You successfully save this Project','top-right','success','1');", true);
                        txtnotasmanual.Text = "";
                        Txtnamamanual.Text = "";
                        txtalamatmanual.Text = "";
                        DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
                        varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOTALBAHANTMP", typeof(string));
                        varx1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                        varx1[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx1, "constrmf");


                        //DataTable dtwrx = GetData(query);
                        LblBahan.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                        LblBahan.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));


                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Kosong!','Data Kosong','top-right','Kosong','1');", true);
            }


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pnltagihan.Visible = false;
        string query;
        query = "";
        if (ddlbahan.SelectedValue == "TASPEN") //nama table menggunakan string
        {
            DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];
            vart[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "USERTASPEN", typeof(string));
            vart[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            vart[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
            //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", vart, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kdcabang asc";
            ddlcabangprodukAsrn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabangprodukAsrn.DataTextField = "cabang";
            ddlcabangprodukAsrn.DataValueField = "kdcabang";
            ddlcabangprodukAsrn.DataBind();
            ddlcabangprodukAsrn.Items.Insert(0, new ListItem("Filter Cabang", "ALL"));



        }
        else if (ddlbahan.SelectedValue == "ASABRI")
        {

            //query = "select kdcabang || ' - ' || nmcabang as cabang, kdcabang from tbl_asabri_cabang where kcu_giga = '" + ddlcabang.SelectedValue + "'";
            DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];
            vart[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "USERASABRI", typeof(string));
            vart[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            vart[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
            //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", vart, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kdcabang asc";
            ddlcabangprodukAsrn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabangprodukAsrn.DataTextField = "cabang";
            ddlcabangprodukAsrn.DataValueField = "kdcabang";
            ddlcabangprodukAsrn.DataBind();
            ddlcabangprodukAsrn.Items.Insert(0, new ListItem("Filter Cabang", "ALL"));


        }

        
        ddlcabangprodukAsrn.Enabled = true;
    }

    protected void ddlbahan_SelectedIndexChanged(object sender, EventArgs e)
    {

        string query;
        query = "";
        if (ddlbahan.SelectedValue == "TASPEN") //nama table menggunakan string
        {
            DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];
            vart[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "USERTASPEN", typeof(string));
            vart[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            vart[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
            //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", vart, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kdcabang asc";
            ddlcabangprodukAsrn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabangprodukAsrn.DataTextField = "cabang";
            ddlcabangprodukAsrn.DataValueField = "kdcabang";
            ddlcabangprodukAsrn.DataBind();
            ddlcabangprodukAsrn.Items.Insert(0, new ListItem("Filter Cabang", "ALL"));

            ddlcabangprodukAsrn.SelectedValue = "ALL";
            ddlcabang.SelectedValue = "ALL";
            Pnlasabari.Visible = false;
            Pnltagihan.Visible = false;



        }
        else if (ddlbahan.SelectedValue == "ASABRI")
        {

            //query = "select kdcabang || ' - ' || nmcabang as cabang, kdcabang from tbl_asabri_cabang where kcu_giga = '" + ddlcabang.SelectedValue + "'";
            DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];
            vart[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "USERASABRI", typeof(string));
            vart[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            vart[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));
            //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", vart, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kdcabang asc";
            ddlcabangprodukAsrn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabangprodukAsrn.DataTextField = "cabang";
            ddlcabangprodukAsrn.DataValueField = "kdcabang";
            ddlcabangprodukAsrn.DataBind();
            ddlcabangprodukAsrn.Items.Insert(0, new ListItem("Filter Cabang", "ALL"));
            Pnlasabari.Visible = false;
            Pnltagihan.Visible = false;
            ddlcabangprodukAsrn.SelectedValue = "ALL";
            ddlcabang.SelectedValue = "ALL";
        }
        //Page.SetFocus(ddlcabangprodukAsrn);
        ddlcabangprodukAsrn.Enabled = true;




    }


}