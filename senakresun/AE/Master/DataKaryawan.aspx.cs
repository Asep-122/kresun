using DefaultClass;
using DocumentFormat.OpenXml.Spreadsheet;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AE_Master_DataKaryawan : System.Web.UI.Page
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
    Global gbsearch = new Global();
    GlobalEntities gesearch = new GlobalEntities();
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime now = DateTime.Now;
        Panel1.Visible = true;
        //Panel2.Visible = true;
        PnlFront.Visible = true;
        Pnlfooter.Visible = true;
        PnlFrontx.Visible = false;
        PnlFront.Visible = true;
        //pnladd.Visible = true;

        pnlHeader.Visible = false;
        //btnsearch.Visible = true;
        //txtsearch.Visible = true;
        //btndelete.Visible = false;
        txtnik.Text = "-";
        txtnmkyn.Text = "";
        txtnpwpkyn.Text = "0";
        txtnobpjs.Text = "0"; 
        //txttglbpjs.Text = now.ToString("yyyy-MM-dd");
        //ddlpddk.SelectedValue = "-";
        ddltngkrj.SelectedValue = "WNI";
        //ddllvl.Text = "";
        ddloff.SelectedValue = SessBranchID;
        ddlbag.SelectedValue = "-";
        ddlgen.SelectedValue = "L";
        //ddlsttptkp.SelectedValue = "-";
        ddljab.SelectedValue = "-";
        ddlgol.SelectedValue = "-";
        //ddlsttskyn.SelectedValue = "-";
        ddlsysbyr.SelectedValue = "Bulanan";
        
        txttgljoin.Text = now.ToString("dd-MM-yyyy");
        txttgljamsostek.Text = now.ToString("dd-MM-yyyy");
            txttglkesehatan.Text = now.ToString("dd-MM-yyyy");
            ddlbyr.SelectedValue = "T";
        //txtnmbank.Text = "";
        //    //ddlrekpersh.Text = "";
        //    txtnmbakkyn.Text = "";
        //txtcbgbank.Text = "";
        //txtrekkyn.Text = "";
       
        txtgjmx.Text = "";
        txtgapok.Text = "0,00";
       
      
        txttumakan.Text = "0,00";
        txttutrp.Text = "0,00"; 
        
        txttunjhrRaya.Text = "0";
            //txtptgSPSI.Text = "";
            //ddlabsensi.SelectedValue = "-";
            //ddlcaraHtglbr.SelectedValue = "";
            //txtkurslembur.Text = "0,00";
            Panel3.Visible = true;

            ddlbyr.SelectedValue = "C";
            ddlrekpersh.SelectedValue = "1111111111";
            ddlbyr_SelectedIndexChanged(sender, e);
            ddlrekpersh_SelectedIndexChanged(sender, e);
            ddlbpjskesehatan.SelectedValue = "B";
            ddlbpjskesehatan_gaji_SelectedIndexChanged(sender, e);

            ddlJHTPrs.SelectedValue = "B";
            ddlJKKPrs.SelectedValue = "B";
            ddlJKMPrs.SelectedValue = "B";
            ddlJPPrs.SelectedValue = "C";


            ddlJHTKry.SelectedValue = "B";
            ddlJPkry.SelectedValue = "C";
            ddlsttskyn.SelectedValue = "C";
            ddlsttptkp.SelectedValue = "K/0";
            //ddlpddk.SelectedValue = "S1";
            txtnik.Focus();

        //btndelete.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {

        //txttglout.Text = txttgljoin.Text;

    }

    protected void btnSearch_Click(string search)
    {
        try
        {
            //var search = "";
            DateTime now;
        string gajistd = "";
        string gol = "";
        string bag = "";
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdnik", ParameterDirection.Input, SqlDbType.VarChar, 50, search, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {


            //data karyawan


            txtnik.Text = ge.ListDataset.Tables[0].Rows[0]["KodeNik"].ToString();
            txtnmkyn.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
            txtnpwpkyn.Text = ge.ListDataset.Tables[0].Rows[0]["NPWP"].ToString();
            txtnikktp.Text = ge.ListDataset.Tables[0].Rows[0]["NIKKTP"].ToString();
            txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTerima"].ToString()).ToString("dd-MM-yyyy");
            txttgljamsostek.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglMasukJst"].ToString()).ToString("dd-MM-yyyy");
            
            ddlpddk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodePddk"].ToString();
            ddltngkrj.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JenisTK"].ToString();
            ddlgen.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JenisKel"].ToString();
            ddlsttptkp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodePtkp"].ToString();
            ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
            ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();
            ddlsysbyr.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["SystemBayar"].ToString();





            txtnobpjs.Text = ge.ListDataset.Tables[0].Rows[0]["NoAstek"].ToString();
            
            
            //Jabatan/bagian/
            
            ddlpembeli.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pembeli"].ToString();
            ddlmarketing.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["marketing"].ToString();
            ddlunitbisnis.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeDept"].ToString();



            ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
            ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
            ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();



                //jab = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                // gol = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                //bag = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();

                //data gaji std


                gajistd = ge.ListDataset.Tables[0].Rows[0]["GajiStd"].ToString();
               
                txtgjmx.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhGajiMax"].ToString()));
                txtgapok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhGajiPokok"].ToString()));
                txttumakan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["TunMakan"].ToString()));
                txttutrp.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["TunTransport"].ToString()));
                txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhNetGaji"].ToString()));
                txttunjhrRaya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhTTHR"].ToString()));
                txtnetgajitenagakerja.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["netgajitenagakerja"].ToString()));

                //cara bayar bank pt

                ddlbyr.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["CaraBayar"].ToString();

                ddlrekpersh.SelectedValue= ddlrekpersh.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Bank"].ToString();
                txtnmbankprsh.Text = ge.ListDataset.Tables[0].Rows[0]["CabangBank"].ToString();
                txtrekbankprsh.Text = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();


                txtnmbakkyn.Text = ge.ListDataset.Tables[0].Rows[0]["NAMABANKKRY"].ToString();
                txtcbgbankkyn.Text = ge.ListDataset.Tables[0].Rows[0]["CABANGBANKKRY"].ToString();
                txtrekkyn.Text = ge.ListDataset.Tables[0].Rows[0]["REKBANKKRY"].ToString();


                //BPJS KESEHATAN
                txtnobpjskesehatan.Text = ge.ListDataset.Tables[0].Rows[0]["nobpjskesehatan"].ToString();
                txttglkesehatan.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglMasukKesehatan"].ToString()).ToString("dd-MM-yyyy");
                txtnetgajiKesehatan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["netgajikesehatan"].ToString()));

                ddlbpjskesehatan.SelectedValue =  ge.ListDataset.Tables[0].Rows[0]["typebpjskesehatan"].ToString();

                txtbpjskesehatanpersenprs.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bpjskesperusahaan"].ToString()));
                txtbpjskesehatanpersenkry.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bpjskeskaryawan"].ToString()));

                //BPJS Tenage Kerja
                txtnobpjs.Text = ge.ListDataset.Tables[0].Rows[0]["NoAstek"].ToString();
                txttgljamsostek.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglMasukJst"].ToString()).ToString("dd-MM-yyyy");
                txtnetgajitenagakerja.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["netgajitenagakerja"].ToString()));
                //dibayar perusahaan
                ddlJHTPrs.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JHTPrs"].ToString();
                ddlJKMPrs.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JKMPrs"].ToString();
                ddlJKKPrs.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JKKPrs"].ToString();
                ddlJPPrs.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JPPrs"].ToString();
                //dibayar karyawan
                ddlJHTKry.SelectedValue =  ge.ListDataset.Tables[0].Rows[0]["JHTKry"].ToString();
                ddlJPkry.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JPKry"].ToString();


                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[8];
                var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETSTDGAJI", typeof(string));
                var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgol.SelectedValue, typeof(string));
                var2[3] = gb.CreateSPParameter("@unitbisnis", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
                var2[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddloff.SelectedValue, typeof(string));
                var2[5] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var2[6] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbag.SelectedValue, typeof(string)); ;
                var2[7] = gb.CreateSPParameter("@kdjab", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljab.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "Value asc";

                ddlstdgaji.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlstdgaji.DataTextField = "TEXT";
                ddlstdgaji.DataValueField = "Value";
                ddlstdgaji.DataBind();
                ddlstdgaji.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlstdgaji.SelectedValue = gajistd;



               
            
            



        }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }
    protected void btnbackheader_Click(object sender, EventArgs e)
    {

        //pnladd.Visible = true;
        //pnl1.Visible = true; 
        Pnlfooter.Visible = false;
    }
    protected void btnback1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                DateTime now = DateTime.Now;
                BindGridEntity();
                BindGridEntitydata();
            }

            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
             
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGRID", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntitydata()
    {



        //DROPDOWNLIST TYPE PENDIDIKAN
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETPDDK", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

        ddlpddk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpddk.DataTextField = "TEXT";
        ddlpddk.DataValueField = "VALUE";
        ddlpddk.DataBind();
        //ddlpddk.Items.Insert(0, new ListItem("Pilih Data", "-"));


        //DROPDOWNLIST TYPE PENDIDIKAN
        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
        var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STATUSKARY", typeof(string));
        var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var3, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

        ddlsttskyn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsttskyn.DataTextField = "TEXT";
        ddlsttskyn.DataValueField = "VALUE";
        ddlsttskyn.DataBind();
        //ddlsttskyn.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //DROPDOWNLIST TYPE PENDIDIKAN
        DefaultClass.Global.SqlHelper[] var4x = new DefaultClass.Global.SqlHelper[2];
        var4x[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BANKPT", typeof(string));
        var4x[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4x, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

        ddlrekpersh.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlrekpersh.DataTextField = "TEXT";
        ddlrekpersh.DataValueField = "VALUE";
        ddlrekpersh.DataBind();
        //ddlrekpersh.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //DROPDOWNLIST TYPE PENDIDIKAN
        DefaultClass.Global.SqlHelper[] var4xx = new DefaultClass.Global.SqlHelper[2];
        var4xx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DEPT", typeof(string));
        var4xx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4xx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

        ddlunitbisnis.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlunitbisnis.DataTextField = "TEXT";
        ddlunitbisnis.DataValueField = "VALUE";
        ddlunitbisnis.DataBind();
        ddlunitbisnis.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddlunitbisnis.SelectedValue = "MK";

        //ddlunitbisnis_SelectedIndexChanged(sender, e);

     


        //DROPDOWNLIST TYPE PTKP
        DefaultClass.Global.SqlHelper[] var3x = new DefaultClass.Global.SqlHelper[2];
        var3x[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETSTTPTKP", typeof(string));
        var3x[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var3x, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "Ptkp asc";

        ddlsttptkp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsttptkp.DataTextField = "ptket";
        ddlsttptkp.DataValueField = "Ptkp";
        ddlsttptkp.DataBind();
        //ddlsttptkp.Items.Insert(0, new ListItem("Pilih Data", "-"));


        //DROPDOWNLIST TYPE GOLONGAN
        DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[2];
        var5[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETGOLONGAN", typeof(string));
        var5[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var5, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGol asc";

        ddlgol.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlgol.DataTextField = "kdket";
        ddlgol.DataValueField = "KodeGol";
        ddlgol.DataBind();
        ddlgol.Items.Insert(0, new ListItem("Pilih Data", "-"));

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBAGIAN", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBagian asc";

        ddlbag.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlbag.DataTextField = "kdket";
        ddlbag.DataValueField = "KodeBagian";
        ddlbag.DataBind();
        ddlbag.Items.Insert(0, new ListItem("Pilih Data", "-"));


        //DROPDOWNLIST TYPE JABATAN
        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
        var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
        var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

        ddljab.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljab.DataTextField = "kdket";
        ddljab.DataValueField = "KodeJab";
        ddljab.DataBind();
        ddljab.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //DROPDOWNLIST TYPE KANTOR
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETKANTOR", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

        ddloff.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddloff.DataTextField = "kdnm";
        ddloff.DataValueField = "KodeCabang";
        ddloff.DataBind();
        //ddloff.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddloff.SelectedValue = SessBranchID;


        DefaultClass.Global.SqlHelper[] vart2 = new DefaultClass.Global.SqlHelper[3];
        vart2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBPJSKESEHATAN", typeof(string));
        vart2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        vart2[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", vart2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

        ddlbpjskesehatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlbpjskesehatan.DataTextField = "TEXT";
        ddlbpjskesehatan.DataValueField = "Value";
        ddlbpjskesehatan.DataBind();
        ddlbpjskesehatan.Items.Insert(0, new ListItem("Pilih Data", "-"));



        DefaultClass.Global.SqlHelper[] vark1 = new DefaultClass.Global.SqlHelper[3];
        vark1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJHT", typeof(string));
        vark1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        vark1[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", vark1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

        ddlJHTPrs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlJHTPrs.DataTextField = "TEXT";
        ddlJHTPrs.DataValueField = "Value";
        ddlJHTPrs.DataBind();
        ddlJHTPrs.Items.Insert(0, new ListItem("Pilih Data", "-"));

        ddlJHTKry.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlJHTKry.DataTextField = "TEXT";
        ddlJHTKry.DataValueField = "Value";
        ddlJHTKry.DataBind();
        ddlJHTKry.Items.Insert(0, new ListItem("Pilih Data", "-"));



        DefaultClass.Global.SqlHelper[] vark2 = new DefaultClass.Global.SqlHelper[3];
        vark2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJKK", typeof(string));
        vark2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        vark2[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", vark2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

        ddlJKKPrs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlJKKPrs.DataTextField = "TEXT";
        ddlJKKPrs.DataValueField = "Value";
        ddlJKKPrs.DataBind();
        ddlJKKPrs.Items.Insert(0, new ListItem("Pilih Data", "-"));



        DefaultClass.Global.SqlHelper[] vark3 = new DefaultClass.Global.SqlHelper[3];
        vark3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJKM", typeof(string));
        vark3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        vark3[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", vark3, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

        ddlJKMPrs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlJKMPrs.DataTextField = "TEXT";
        ddlJKMPrs.DataValueField = "Value";
        ddlJKMPrs.DataBind();
        ddlJKMPrs.Items.Insert(0, new ListItem("Pilih Data", "-"));

        DefaultClass.Global.SqlHelper[] vark4 = new DefaultClass.Global.SqlHelper[3];
        vark4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJP", typeof(string));
        vark4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        vark4[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", vark4, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

        ddlJPPrs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlJPPrs.DataTextField = "TEXT";
        ddlJPPrs.DataValueField = "Value";
        ddlJPPrs.DataBind();
        ddlJPPrs.Items.Insert(0, new ListItem("Pilih Data", "-"));

        ddlJPkry.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlJPkry.DataTextField = "TEXT";
        ddlJPkry.DataValueField = "Value";
        ddlJPkry.DataBind();
        ddlJPkry.Items.Insert(0, new ListItem("Pilih Data", "-"));

    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {
                    PnlFrontx.Visible = false;
                    PnlFront.Visible = true;
                    //pnladd.Visible = true;
                    Pnlfooter.Visible = true;
                    pnlHeader.Visible = false;
                    Panel1.Visible = true;
                    Panel3.Visible = true;
                    btnSearch_Click(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNik")).Text);
                    ddlbyr_SelectedIndexChanged(sender, e);
                    Pnlfooter.Visible = true;
                    //btndelete.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                } 

                break;
            case "SETDEL":
                    
                try
                {
                    string nikx = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNik")).Text;

                    DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[3];
                    x[0] = gb.CreateSPParameter("@kdnik", ParameterDirection.Input, SqlDbType.VarChar, 50, nikx, typeof(string));
                    x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKSALES", typeof(string));
                    x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", x, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {
                        btnDelete_Click(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNik")).Text, ddlpembeli.SelectedValue, ddlmarketing.SelectedValue);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data sudah ada pada transaksi tidak dapat dihapus!');", true);

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
        string STATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";
           
            if (ddlunitbisnis.SelectedValue=="-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Unit Bisnis Masih Kosong!');", true);
            }
              else if(ddlbag.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bagian Masih Kosong!');", true);
            }
            else if (ddljab.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jabatan Masih Kosong!');", true);
            }
            else if (ddlgol.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('golongan Masih Kosong!');", true);
            }
            else if (ddlsttskyn.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Status Karyawan Masih Kosong!');", true);
            }
            else if (txtnmkyn.Text.Length==0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama Karyawan Masih Kosong!');", true);
            }
            else if (txtnikktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nik KTP Karyawan Masih Kosong!');", true);
            }
            else if (txttgljoin.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Bergabung Karyawan Masih Kosong!');", true);
            }
            else if (txttgljoin.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Bergabung Karyawan Masih Kosong!');", true);
            }
            else if (ddlstdgaji.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Standar Gaji Masih Kosong!');", true);
            }
            else
            {



            if (txtnik.Text == "-")
            {
                STATE = "INSERT";
                //txttglout.Text = txttgljoin.Text;
            }
            else
            {
                STATE = "UPDATE";
            }
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[53];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnik.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@Bank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlrekpersh.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@RekBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtrekbankprsh.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@NamaBank", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnmbankprsh.Text, typeof(string));
                var[8] = gb.CreateSPParameter("@NPWP", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnpwpkyn.Text, typeof(string));
                var[9] = gb.CreateSPParameter("@NoAstek", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobpjs.Text, typeof(string));
                var[10] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlsysbyr.SelectedValue, typeof(string));
                var[11] = gb.CreateSPParameter("@KodePtkp", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlsttptkp.SelectedValue, typeof(string));
                var[12] = gb.CreateSPParameter("@JenisTK", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltngkrj.SelectedValue, typeof(string));
                var[13] = gb.CreateSPParameter("@KodeBagian", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbag.SelectedValue, typeof(string));
                var[14] = gb.CreateSPParameter("@KodeDept", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlunitbisnis.SelectedValue, typeof(string));
                var[15] = gb.CreateSPParameter("@KodePddk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlpddk.SelectedValue, typeof(string));
                var[16] = gb.CreateSPParameter("@JenisKel", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgen.SelectedValue, typeof(string));
                var[17] = gb.CreateSPParameter("@Jabatan", ParameterDirection.Input, SqlDbType.VarChar, 18, ddljab.SelectedValue, typeof(string));
                var[18] = gb.CreateSPParameter("@KodeGol", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgol.SelectedValue, typeof(string));
                var[19] = gb.CreateSPParameter("@StatusKry", ParameterDirection.Input, SqlDbType.VarChar, 12, ddlsttskyn.SelectedValue, typeof(string));
                var[20] = gb.CreateSPParameter("@SystemBayar", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlsysbyr.SelectedValue, typeof(string));
                var[21] = gb.CreateSPParameter("@TglTerima", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljoin.Text, typeof(DateTime));
                var[22] = gb.CreateSPParameter("@TglMasukJst", ParameterDirection.Input, SqlDbType.SmallDateTime, 0,txttgljamsostek.Text, typeof(DateTime));
                var[23] = gb.CreateSPParameter("@CaraBayar", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlbyr.SelectedValue, typeof(string));
                var[24] = gb.CreateSPParameter("@CABANGBANKKRY", ParameterDirection.Input, SqlDbType.VarChar, 40, txtcbgbankkyn.Text, typeof(string));
                var[25] = gb.CreateSPParameter("@NAMABANKKRY", ParameterDirection.Input, SqlDbType.VarChar, 40, txtnmbakkyn.Text, typeof(string));
                var[26] = gb.CreateSPParameter("@REKBANKKRY", ParameterDirection.Input, SqlDbType.VarChar, 40, txtrekkyn.Text, typeof(string));
                var[27] = gb.CreateSPParameter("@GajiStd", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlstdgaji.SelectedValue, typeof(string));
                var[28] = gb.CreateSPParameter("@JlhGajiMax", ParameterDirection.Input, SqlDbType.Decimal, 0, txtgjmx.Text, typeof(decimal));
                var[29] = gb.CreateSPParameter("@JlhGajiPokok", ParameterDirection.Input, SqlDbType.Decimal, 0, txtgapok.Text, typeof(decimal));
                var[30] = gb.CreateSPParameter("@TunTransport", ParameterDirection.Input, SqlDbType.Decimal, 0,txttutrp.Text, typeof(decimal));
                var[31] = gb.CreateSPParameter("@TunMakan", ParameterDirection.Input, SqlDbType.Decimal, 0, txttumakan.Text, typeof(decimal));
                var[32] = gb.CreateSPParameter("@JlhNetGaji", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnetgaji.Text, typeof(decimal));
                var[33] = gb.CreateSPParameter("@JlhTTHR", ParameterDirection.Input, SqlDbType.Decimal, 0, txttunjhrRaya.Text, typeof(decimal));
                var[34] = gb.CreateSPParameter("@pembeli", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpembeli.SelectedValue, typeof(string));
                var[35] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlmarketing.SelectedValue, typeof(string));
                var[36] = gb.CreateSPParameter("@bos_id", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlbosid.SelectedValue, typeof(string));
                var[37] = gb.CreateSPParameter("@userentry", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[38] = gb.CreateSPParameter("@typebpjskesehatan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbpjskesehatan.SelectedValue, typeof(string));
                var[39] = gb.CreateSPParameter("@bpjskesperusahaan", ParameterDirection.Input, SqlDbType.Float, 0, txtbpjskesehatanpersenprs.Text, typeof(float));
                var[40] = gb.CreateSPParameter("@bpjskeskaryawan", ParameterDirection.Input, SqlDbType.Float, 0, txtbpjskesehatanpersenkry.Text, typeof(float));
                var[41] = gb.CreateSPParameter("@JHTPrs", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlJHTPrs.SelectedValue, typeof(string));
                var[42] = gb.CreateSPParameter("@JKKPrs", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlJKKPrs.SelectedValue, typeof(string));
                var[43] = gb.CreateSPParameter("@JKMPrs", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlJKMPrs.SelectedValue, typeof(string));
                var[44] = gb.CreateSPParameter("@JPPrs", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlJPPrs.SelectedValue, typeof(string));
                var[45] = gb.CreateSPParameter("@JHTKry", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlJHTKry.SelectedValue, typeof(string));
                var[46] = gb.CreateSPParameter("@JPKry", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlJPkry.SelectedValue, typeof(string));
                var[47] = gb.CreateSPParameter("@NamaKry", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnmkyn.Text, typeof(string));
                var[48] = gb.CreateSPParameter("@TglMasukKesehatan", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglkesehatan.Text, typeof(DateTime));
                var[49] = gb.CreateSPParameter("@gajinetkesehatan", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnetgajiKesehatan.Text, typeof(decimal));
                var[50] = gb.CreateSPParameter("@gajinettenagakerja", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnetgajitenagakerja.Text, typeof(decimal));
                var[51] = gb.CreateSPParameter("@NIKKTP", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnikktp.Text, typeof(string));
                var[52] = gb.CreateSPParameter("@nobpjskesehatan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobpjskesehatan.Text, typeof(string));
                
                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_NEW_INSERT", var, "constrmf");
               
                
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Duplicate"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Gaji " + txtnik.Text + " already Exists!');", true);
                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Selisih dengan Gaji Max');", true);
                //}

            }


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void ddlmarketing_OnChange(object sender, EventArgs e)
    {
        try
        {
            //string asdas = ddloff.SelectedValue;
            if (ddlmarketing.SelectedValue == "2")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBOSID", typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddloff.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

                ddlbosid.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlbosid.DataTextField = "TEXT";
                ddlbosid.DataValueField = "VALUE";
                ddlbosid.DataBind();
                ddlbosid.Items.Insert(0, new ListItem("Pilih Data", "-"));

                divbos.Visible = true;
            }
            else
            {
                divbos.Visible = false;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

  protected void btnDelete_Click(string nik, string pembeli, string marketing)
    {
        var del = nik;
        var pembelix = pembeli;
        var marketingx = marketing;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@kdnik", ParameterDirection.Input, SqlDbType.VarChar, 50, del, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@pembeli", ParameterDirection.Input, SqlDbType.VarChar, 50, pembelix, typeof(string));
        var[3] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 50, marketingx, typeof(string));
        var[4] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_DELETE", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Ada"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningData('Data sudah ada pada transaksi tidak dapat dihapus!');", true);
        }
        else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void kdgol_onselectedchange(object sender,EventArgs e)
    {
        var getgol = ddlgol.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GAJIGOLONGAN", typeof(string));
        var[1] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, getgol, typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");

        txtgjmx.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhGajiPokok"].ToString()));
    }
    protected void gajipokok_ontextchanged(object sender,EventArgs e)
    {
        decimal a, b, c, d, f, g, h, i, j, k, x, y, z;
        a = decimal.Parse(txtgjmx.Text);
        b = decimal.Parse(txtgapok.Text);
        c = decimal.Parse(txttumakan.Text);        
        d = decimal.Parse(txttutrp.Text);
               

        if ((b+c+d) > a)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Net Gaji tidak boleh lebih dari Maksimal Gaji !');", true);
            txtgapok.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtnetgaji.Text= string.Format("{0:N2}", decimal.Parse(txtgapok.Text) + c +d);
            txttunjhrRaya.Text = string.Format("{0:N2}", decimal.Parse(txtgapok.Text) + c + d);
        }
        else
        {
            
            txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(txtgapok.Text) + c + d);
            txttunjhrRaya.Text = string.Format("{0:N2}", decimal.Parse(txtgapok.Text) + c + d);
        }
    }
   

  

   
    protected void ddlstdgaji_view_SelectedIndexChanged(object sender, EventArgs e)
    {

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[8];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETSTDGAJI", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgol.SelectedValue, typeof(string));
        var2[3] = gb.CreateSPParameter("@unitbisnis", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
        var2[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddloff.SelectedValue, typeof(string));
        var2[5] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var2[6] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbag.SelectedValue, typeof(string)); ;
        var2[7] = gb.CreateSPParameter("@kdjab", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljab.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "Value asc";

        ddlstdgaji.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstdgaji.DataTextField = "TEXT";
        ddlstdgaji.DataValueField = "Value";
        ddlstdgaji.DataBind();
        ddlstdgaji.Items.Insert(0, new ListItem("Pilih Data", "-"));


    }
    protected void ddlbyr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlbyr.SelectedValue == "C")
        {
            ddlrekpersh.SelectedValue = "1111111111";
            ddlrekpersh.Enabled = false;
            txtnmbankprsh.Text = "CASH";
            txtnmbankprsh.Enabled = false;
            txtrekbankprsh.Text = "0";
            txtrekbankprsh.Enabled = false;

            txtnmbakkyn.Enabled = false;
            txtnmbakkyn.Text = "-";
            txtcbgbankkyn.Enabled = false;
            txtcbgbankkyn.Text = "-";
            txtrekkyn.Enabled = false;
            txtrekkyn.Text = "-";



        }
        else
        {

            ddlrekpersh.SelectedValue = "-";
            ddlrekpersh.Enabled = true;
            txtnmbankprsh.Text = "-";
            txtnmbankprsh.Enabled = true;
            txtrekbankprsh.Text = "-";
            txtrekbankprsh.Enabled = true;

            txtnmbakkyn.Enabled = true;
            txtnmbakkyn.Text = "-";
            txtcbgbankkyn.Enabled = true;
            txtcbgbankkyn.Text = "-";
            txtrekkyn.Enabled = true;
            txtrekkyn.Text = "-";
        }
    }
    protected void ddlrekpersh_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrekpersh.SelectedValue == "1111111111")
        {
            //ddlrekpersh.Text = "-";
            //ddlrekpersh.Enabled = false;
            txtnmbankprsh.Text = "-";
            txtnmbankprsh.Enabled = false;
            txtrekbankprsh.Text = "0";
            txtrekbankprsh.Enabled = false; 
        }
        else
        {

            //ddlrekpersh.Text = "";
            //ddlrekpersh.Enabled = true;
            txtnmbankprsh.Text = "";
            txtnmbankprsh.Enabled = true;
            txtrekbankprsh.Text = "";
            txtrekbankprsh.Enabled = true;
        } 
    }

    protected void ddlstd_gaji_SelectedIndexChanged(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[9];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETSTDGAJIVIEW", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgol.SelectedValue, typeof(string));
        var2[3] = gb.CreateSPParameter("@unitbisnis", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
        var2[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddloff.SelectedValue, typeof(string));
        var2[5] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var2[6] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbag.SelectedValue, typeof(string)); ;
        var2[7] = gb.CreateSPParameter("@kdjab", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljab.SelectedValue, typeof(string));
        var2[8] = gb.CreateSPParameter("@kdnik", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstdgaji.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");

        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            //string NamaBank = ;
            //string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();

            txtgjmx.Text= string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["NetGajiPokok"].ToString()));
            txtgapok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["GajiPokok"].ToString()));
            txttumakan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["TunjMakan"].ToString()));
            txttutrp.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["TunjTransport"].ToString()));
            txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["NetGajiPokok"].ToString()));
            txttunjhrRaya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["NetGajiPokok"].ToString()));
            //txtNmlgn.Text = NamaBank;
            //ddlkursbank.SelectedValue = kdcrc;
            //txtkursbank.Text = "1";

        }
        else
        {
            txtgjmx.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtgapok.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txttumakan.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txttutrp.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txttunjhrRaya.Text = string.Format("{0:N2}", decimal.Parse("0"));


        }
    }
    protected void ddlbpjskesehatan_gaji_SelectedIndexChanged(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBPJSKESEHATANVIEW", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbpjskesehatan.SelectedValue, typeof(string));
        var2[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");

        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            //string NamaBank = ;
            //string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();

            txtbpjskesehatanpersenprs.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Perusahaan"].ToString()));
            txtbpjskesehatanpersenkry.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Karyawan"].ToString()));
            //txtNmlgn.Text = NamaBank;
            //ddlkursbank.SelectedValue = kdcrc;
            //txtkursbank.Text = "1";

        }
        else
        {
            txtbpjskesehatanpersenprs.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtbpjskesehatanpersenkry.Text = string.Format("{0:N2}", decimal.Parse("0"));
          


        }
    }

}




