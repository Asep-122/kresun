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


public partial class AE_Master_KodeGaji : System.Web.UI.Page
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
        ddlpddk.SelectedValue = "-";
        ddltngkrj.SelectedValue = "WNI";
        //ddllvl.Text = "";
        ddloff.SelectedValue = SessBranchID;
        ddlbag.SelectedValue = "-";
        ddlgen.SelectedValue = "L";
        ddlsttptkp.SelectedValue = "-";
        ddljab.SelectedValue = "-";
        ddlgol.SelectedValue = "-";
        ddlsttskyn.SelectedValue = "-";
        ddlsysbyr.SelectedValue = "Bulanan";
        ddlkdmkn.SelectedValue = "-";
        ddlkdtrp.SelectedValue = "-";
        txttgljoin.Text = now.ToString("yyyy-MM-dd");
        txttglout.Text = now.ToString("yyyy-MM-dd");
        ddlbyr.Text = "T";
        txtnmbank.Text = "";
        //ddlrekpersh.Text = "";
        txtnmbakpersh.Text = "";
        txtcbgbank.Text = "";
        txtrekkyn.Text = "";
        txtplafonpnjm.Text = "0,00";
        txtjmlhpnjm.Text = "0,00";
        txtangs.Text = "0,00";
        txtplafonct.Text = "0";
        txtctblnll.Text = "0";
        txttunjplaf.Text = "0,00";
        txtobtpakai.Text = "0,00";
        txtgjmx.Text = "";
        txtgapok.Text = "0,00";
        txttutep.Text = "0,00";
        txttujab.Text = "0,00";
        txttumakan.Text = "0,00";
        txttutrp.Text = "0,00"; 
        txttunj1.Text = "0,00";
        txttunj2.Text = "0,00";
        txttunj3.Text = "0,00";
        txttunj4.Text = "0,00";
        txttunj5.Text = "0,00";
        txttunjhrRaya.Text = "";
        //txtptgSPSI.Text = "";
        //ddlabsensi.SelectedValue = "-";
        //ddlcaraHtglbr.SelectedValue = "";
        //txtkurslembur.Text = "0,00";
        txtmemo.Text = "-"; 
        txtptgpjk.Text = "0,00";
         
        txtnik.Focus();

        //btndelete.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {

        txttglout.Text = txttgljoin.Text;

    }

    protected void btnSearch_Click(string search)
    {
        //var search = "";
        DateTime now;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdnik", ParameterDirection.Input, SqlDbType.VarChar, 50, search, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");

        txtnik.Text = ge.ListDataset.Tables[0].Rows[0]["KodeNik"].ToString();
        txtnmkyn.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
        txtnpwpkyn.Text = ge.ListDataset.Tables[0].Rows[0]["NPWP"].ToString();
        txtnobpjs.Text = ge.ListDataset.Tables[0].Rows[0]["NoAstek"].ToString();

        //now = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglMasukJst"].ToString());

        //txttglbpjs.Text = now.ToString("yyyy-MM-dd");
        ddlpembeli.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pembeli"].ToString();
        ddlmarketing.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["marketing"].ToString();
        ddlpddk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodePddk"].ToString();
        ddltngkrj.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JenisTK"].ToString();
        //ddllvl.Text = ge.ListDataset.Tables[0].Rows[0]["LevelPw"].ToString();
        ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
       
        ddlgen.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JenisKel"].ToString();
        ddlsttptkp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodePtkp"].ToString();
        ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
        ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
        ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();
        ddlsysbyr.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["SystemBayar"].ToString();
        //ddlkdmkn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeMakan"].ToString();
        //ddlkdtrp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeTrp"].ToString();
      
        now = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTerima"].ToString());
        txttgljoin.Text = now.ToString("yyyy-MM-dd");
        now = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglKeluar"].ToString());
        txttglout.Text = now.ToString("yyyy-MM-dd"); 
        ddlbyr.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["CaraBayar"].ToString();
        txtnmbank.Text = ge.ListDataset.Tables[0].Rows[0]["Bank"].ToString();
        ddlrekpersh.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeBank"].ToString();
        txtnmbakpersh.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
        txtcbgbank.Text = ge.ListDataset.Tables[0].Rows[0]["CabangBank"].ToString();
        txtrekkyn.Text = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();
       // txtplafonpnjm.Text = ge.ListDataset.Tables[0].Rows[0]["BatasPinjaman"].ToString();
        txtplafonpnjm.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["BatasPinjaman"].ToString()));
        //txtjmlhpnjm.Text = ge.ListDataset.Tables[0].Rows[0]["JlhPinjamanSdbl"].ToString();
        //txtangs.Text = ge.ListDataset.Tables[0].Rows[0]["AngsuranBln"].ToString();
       // txtplafonct.Text = ge.ListDataset.Tables[0].Rows[0]["PlafonCuti"].ToString();
        txtplafonct.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PlafonCuti"].ToString()));
        //txtptgbiayabank.Text = ge.ListDataset.Tables[0].Rows[0]["biayarek"].ToString();
        txtptgbiayabank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayarek"].ToString()));
        //txtctblnll.Text = ge.ListDataset.Tables[0].Rows[0]["JCutiBln"].ToString();

       // txttunjplaf.Text = ge.ListDataset.Tables[0].Rows[0]["JlhBatasObat"].ToString();
        txttunjplaf.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhBatasObat"].ToString()));
        //txtobtpakai.Text = ge.ListDataset.Tables[0].Rows[0]["JObatBln"].ToString();
        //txtgapok.Text = ge.ListDataset.Tables[0].Rows[0]["JlhGajiPokok"].ToString();
        txtgapok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhGajiPokok"].ToString()));
        //txttutep.Text = ge.ListDataset.Tables[0].Rows[0]["JlhTunj1"].ToString();
        txttutep.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["TunTetap"].ToString()));
        //txttujab.Text = ge.ListDataset.Tables[0].Rows[0]["JlhTunj2"].ToString();
        txttujab.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["TunJabatan"].ToString()));
        //txttuabdi.Text = ge.ListDataset.Tables[0].Rows[0]["JlhTunj3"].ToString();
        txttumakan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["TunMakan"].ToString()));
        // txttutrp.Text = ge.ListDataset.Tables[0].Rows[0]["JlhTunj4"].ToString();
        txttutrp.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["TunTransport"].ToString()));
        // txttunjhrRaya.Text = ge.ListDataset.Tables[0].Rows[0]["JlhTTHR"].ToString();
        txttunjhrRaya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhTTHR"].ToString()));
        ddlunitbisnis.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeDept"].ToString();
        //txtptgSPSI.Text = ge.ListDataset.Tables[0].Rows[0]["JlhPot1"].ToString();
        //ddlabsensi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["LemburEntry"].ToString();
        //ddlcaraHtglbr.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["HitungLembur"].ToString();
        //txtkurslembur.Text = ge.ListDataset.Tables[0].Rows[0]["KursLembur"].ToString();
        txtmemo.Text = ge.ListDataset.Tables[0].Rows[0]["Memo"].ToString();
        //txtptgpjk.Text = ge.ListDataset.Tables[0].Rows[0]["PotPajak"].ToString();
        txtptgpjk.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PotPajak"].ToString()));


        var bagian = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();



        var nnaktif = ge.ListDataset.Tables[0].Rows[0]["NonAktifFlag"].ToString();
        //var pph = ge.ListDataset.Tables[0].Rows[0]["SystemPph"].ToString();
        //ddltunj1.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KetTunj1"].ToString();
        //ddltunj2.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KetTunj2"].ToString();

        //txttunj1.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhTunj1"].ToString()));
        //txttunj2.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhTunj2"].ToString()));
        //txttunj3.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhTunj3"].ToString()));
        //txttunj4.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhTunj4"].ToString()));
        //txttunj5.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhTunj5"].ToString()));

        //ddltunj1.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KetTunj1"].ToString();
        //ddltunj2.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KetTunj2"].ToString();
        //ddltunj3.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KetTunj3"].ToString();
        //ddltunj4.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KetTunj4"].ToString();
        //ddltunj5.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KetTunj5"].ToString();



        if (nnaktif == "True")
        {
            cbxnonaktf.Checked = false;
        }
        else
        {
            cbxnonaktf.Checked = true;
        }
      
        var getgol = ddlgol.SelectedValue;
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GAJIGOLONGAN", typeof(string));
        var1[1] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, getgol, typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var1, "constrmf");

        txtgjmx.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhGajiPokok"].ToString()));



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
        ddlbag.SelectedValue = bagian;

        //btndelete.Visible = true;

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
        ddlpddk.Items.Insert(0, new ListItem("Pilih Data", "-"));


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
        ddlsttskyn.Items.Insert(0, new ListItem("Pilih Data", "-"));

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
        ddlrekpersh.Items.Insert(0, new ListItem("Pilih Data", "-"));

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

        


        //DROPDOWNLIST TYPE KANTOR
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETKANTOR", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

        ddloff.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddloff.DataTextField = "kdnm";
        ddloff.DataValueField = "KodeCabang";
        ddloff.DataBind();
        ddloff.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddloff.SelectedValue = SessBranchID;
        ddloff.Enabled = false;
        //DROPDOWNLIST TYPE BAGIAN


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
        ddlsttptkp.Items.Insert(0, new ListItem("Pilih Data", "-"));


        //DROPDOWNLIST TYPE JABATAN
        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[2];
        var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
        var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

        ddljab.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljab.DataTextField = "kdket";
        ddljab.DataValueField = "KodeJab";
        ddljab.DataBind();
        ddljab.Items.Insert(0, new ListItem("Pilih Data", "-"));

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

        //DROPDOWNLIST TYPE MAKAN
        DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[2];
        var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETMAKAN", typeof(string));
        var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var6, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TypeMknBiasa asc";

        ddlkdmkn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdmkn.DataTextField = "kdket";
        ddlkdmkn.DataValueField = "TypeMknBiasa";
        ddlkdmkn.DataBind();
        ddlkdmkn.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //DROPDOWNLIST TYPE MAKAN
        DefaultClass.Global.SqlHelper[] var7 = new DefaultClass.Global.SqlHelper[2];
        var7[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETTRANSPORT", typeof(string));
        var7[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var7, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrpBiasa asc";

        ddlkdtrp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdtrp.DataTextField = "kdket";
        ddlkdtrp.DataValueField = "TypeTrpBiasa";
        ddlkdtrp.DataBind();
        ddlkdtrp.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //DROPDOWNLIST TYPE TUNJANGAN
        DefaultClass.Global.SqlHelper[] var8 = new DefaultClass.Global.SqlHelper[2];
        var8[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETTUNJANGAN", typeof(string));
        var8[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var8, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTunj asc";

        ddltunj1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltunj1.DataTextField = "tyket";
        ddltunj1.DataValueField = "TypeTunj";
        ddltunj1.DataBind();

        ddltunj2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltunj2.DataTextField = "tyket";
        ddltunj2.DataValueField = "TypeTunj";
        ddltunj2.DataBind();

        ddltunj3.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltunj3.DataTextField = "tyket";
        ddltunj3.DataValueField = "TypeTunj";
        ddltunj3.DataBind();

        ddltunj4.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltunj4.DataTextField = "tyket";
        ddltunj4.DataValueField = "TypeTunj";
        ddltunj4.DataBind();

        ddltunj5.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltunj5.DataTextField = "tyket";
        ddltunj5.DataValueField = "TypeTunj";
        ddltunj5.DataBind();

        ddltunj1.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddltunj2.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddltunj3.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddltunj4.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddltunj5.Items.Insert(0, new ListItem("Pilih Data", "-"));


        //ddl29.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddl29.DataTextField = "tyket";
        //ddl29.DataValueField = "TypeTrpBiasa";
        //ddl29.DataBind();
        //ddl29.Items.Insert(0, new ListItem("Pilih Data", "-"));

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

                    btnSearch_Click(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNik")).Text);
                   
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
            if (cbxnonaktf.Checked == true)
            {
                ckbx = "0";
            }
            else
            {
                ckbx = "1";
            }
            //if (cbxpph.Checked == true)
            //{
            //    ckbx1 = "1";
            //}
            //else
            //{
            //    ckbx1 = "0";
            //}
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
            else
            {



            if (txtnik.Text == "-")
            {
                STATE = "INSERT";
                txttglout.Text = txttgljoin.Text;
            }
            else
            {
                STATE = "UPDATE";
            }
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[57];
            var[0] = gb.CreateSPParameter("@txtnik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnik.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@txtnmkyn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnmkyn.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@txtnpwpkyn", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnpwpkyn.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@txtnobpjs", ParameterDirection.Input, SqlDbType.VarChar, 15, txtnobpjs.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@txttglbpjs", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Now, typeof(DateTime));
            var[5] = gb.CreateSPParameter("@ddlpddk", ParameterDirection.Input, SqlDbType.VarChar, 12, ddlpddk.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@ddltngkrj", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltngkrj.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@ddloff", ParameterDirection.Input, SqlDbType.VarChar, 10, ddloff.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@ddlbag", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbag.SelectedValue, typeof(string));
            var[9] = gb.CreateSPParameter("@ddlgen", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlgen.SelectedValue, typeof(string));
            var[10] = gb.CreateSPParameter("@ddlsttptkp", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlsttptkp.SelectedValue, typeof(string));
            var[11] = gb.CreateSPParameter("@ddljab", ParameterDirection.Input, SqlDbType.VarChar, 18, ddljab.SelectedValue, typeof(string));
            var[12] = gb.CreateSPParameter("@ddlgol", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlgol.SelectedValue, typeof(string));
            var[13] = gb.CreateSPParameter("@ddlsttskyn", ParameterDirection.Input, SqlDbType.VarChar, 12, ddlsttskyn.SelectedValue, typeof(string));
            var[14] = gb.CreateSPParameter("@ddlsysbyr", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlsysbyr.SelectedValue, typeof(string));
            var[15] = gb.CreateSPParameter("@ddlkdmkn", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkdmkn.SelectedValue, typeof(string));
            var[16] = gb.CreateSPParameter("@ddlkdtrp", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkdtrp.SelectedValue, typeof(string));
            var[17] = gb.CreateSPParameter("@txttgljoin", ParameterDirection.Input, SqlDbType.SmallDateTime, 5, txttgljoin.Text, typeof(DateTime));
            var[18] = gb.CreateSPParameter("@txttglout", ParameterDirection.Input, SqlDbType.SmallDateTime, 5, txttglout.Text, typeof(DateTime));
            var[19] = gb.CreateSPParameter("@ddlbyr", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlbyr.SelectedValue, typeof(string));
            var[20] = gb.CreateSPParameter("@txtnmbank", ParameterDirection.Input, SqlDbType.VarChar, 40, txtnmbank.Text, typeof(string));
            var[21] = gb.CreateSPParameter("@ddlrekpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlrekpersh.SelectedValue, typeof(string));
            var[22] = gb.CreateSPParameter("@txtnmbakpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, txtnmbakpersh.Text, typeof(string));
            var[23] = gb.CreateSPParameter("@txtcbgbank", ParameterDirection.Input, SqlDbType.VarChar, 40, txtcbgbank.Text, typeof(string));
            var[24] = gb.CreateSPParameter("@txtrekkyn", ParameterDirection.Input, SqlDbType.VarChar, 20, txtrekkyn.Text, typeof(string));
            var[25] = gb.CreateSPParameter("@txtplafonpnjm", ParameterDirection.Input, SqlDbType.Decimal, 50, txtplafonpnjm.Text, typeof(decimal));
            var[26] = gb.CreateSPParameter("@txtplafonct", ParameterDirection.Input, SqlDbType.Float, 5, txtplafonct.Text, typeof(string));
            var[27] = gb.CreateSPParameter("@txttunjplaf", ParameterDirection.Input, SqlDbType.Decimal, 50, txttunjplaf.Text, typeof(decimal));
            var[28] = gb.CreateSPParameter("@txtgapok", ParameterDirection.Input, SqlDbType.Decimal, 50, txtgapok.Text, typeof(decimal));
            var[29] = gb.CreateSPParameter("@txttutep", ParameterDirection.Input, SqlDbType.Decimal, 50, txttutep.Text, typeof(decimal));
            var[30] = gb.CreateSPParameter("@txttujab", ParameterDirection.Input, SqlDbType.Decimal, 50, txttujab.Text, typeof(decimal));
            var[31] = gb.CreateSPParameter("@txttuabdi", ParameterDirection.Input, SqlDbType.Decimal, 50, txttumakan.Text, typeof(decimal));
            var[32] = gb.CreateSPParameter("@txttutrp", ParameterDirection.Input, SqlDbType.Decimal, 50, txttutrp.Text, typeof(decimal));
            var[33] = gb.CreateSPParameter("@txttunj1", ParameterDirection.Input, SqlDbType.Decimal, 50, txttunj1.Text, typeof(decimal));
            var[34] = gb.CreateSPParameter("@txttunj2", ParameterDirection.Input, SqlDbType.Decimal, 50, txttunj2.Text, typeof(decimal));
            var[35] = gb.CreateSPParameter("@txttunj3", ParameterDirection.Input, SqlDbType.Decimal, 50, txttunj3.Text, typeof(decimal));
            var[36] = gb.CreateSPParameter("@txttunj4", ParameterDirection.Input, SqlDbType.Decimal, 50, txttunj4.Text, typeof(decimal));
            var[37] = gb.CreateSPParameter("@txttunj5", ParameterDirection.Input, SqlDbType.Decimal, 50, txttunj5.Text, typeof(decimal));
            var[38] = gb.CreateSPParameter("@txttunjhrRaya", ParameterDirection.Input, SqlDbType.Decimal, 50, txttunjhrRaya.Text, typeof(decimal));
            var[39] = gb.CreateSPParameter("@txtmemo", ParameterDirection.Input, SqlDbType.VarChar, 150, txtmemo.Text, typeof(string));
            var[40] = gb.CreateSPParameter("@txtptgpjk", ParameterDirection.Input, SqlDbType.Decimal, 50, txtptgpjk.Text, typeof(decimal));
            var[41] = gb.CreateSPParameter("@KetTunj1", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj1.SelectedValue, typeof(string));
            var[42] = gb.CreateSPParameter("@KetTunj2", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj2.SelectedValue, typeof(string));
            var[43] = gb.CreateSPParameter("@KetTunj3", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj3.SelectedValue, typeof(string));
            var[44] = gb.CreateSPParameter("@KetTunj4", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj4.SelectedValue, typeof(string));
            var[45] = gb.CreateSPParameter("@KetTunj5", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj5.SelectedValue, typeof(string));
            var[46] = gb.CreateSPParameter("@NonAktifFlag", ParameterDirection.Input, SqlDbType.VarChar, 50, ckbx, typeof(string));
            var[47] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[48] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            var[49] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[50] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[51] = gb.CreateSPParameter("@kodedept", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
            var[52] = gb.CreateSPParameter("@BiayaRek", ParameterDirection.Input, SqlDbType.Decimal, 50, txtptgbiayabank.Text, typeof(decimal));
            var[53] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[54] = gb.CreateSPParameter("@pembeli", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpembeli.SelectedValue, typeof(string));
            var[55] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlmarketing.SelectedValue, typeof(string));
                var[56] = gb.CreateSPParameter("@bos_id", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbosid.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_INSERT", var, "constrmf");
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
    protected void ddltunj1_onselectedchange(object sender,EventArgs e)
    {

        if(ddltunj1.SelectedValue != "-")
        { 
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETTUNJANGAN2", typeof(string));
            var[1] = gb.CreateSPParameter("@tunj1", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj1.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTunj asc";

            ddltunj2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddltunj2.DataTextField = "tyket";
            ddltunj2.DataValueField = "TypeTunj";
            ddltunj2.DataBind();
            ddltunj2.Items.Insert(0, new ListItem("Pilih Data", "-"));

            txttunj1.Enabled = true;
            ddltunj2.Enabled = true;
        }
        else
        {
            ddltunj2.SelectedValue = "-";
            ddltunj3.SelectedValue = "-";
            ddltunj4.SelectedValue = "-";
            ddltunj5.SelectedValue = "-";
              
            ddltunj2.Enabled = false;
            ddltunj3.Enabled = false;
            ddltunj4.Enabled = false;
            ddltunj5.Enabled = false;


            txttunj2.Text = "0,00";
            txttunj3.Text = "0,00";
            txttunj4.Text = "0,00";
            txttunj5.Text = "0,00"; 

            txttunj2.Enabled = false;
            txttunj3.Enabled = false;
            txttunj4.Enabled = false;
            txttunj5.Enabled = false;

        }
    }

    protected void ddltunj2_onselectedchange(object sender, EventArgs e)
    {

        if (ddltunj2.SelectedValue != "-")
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETTUNJANGAN3", typeof(string));
            var[1] = gb.CreateSPParameter("@tunj1", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj1.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@tunj2", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj2.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTunj asc";

            ddltunj3.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddltunj3.DataTextField = "tyket";
            ddltunj3.DataValueField = "TypeTunj";
            ddltunj3.DataBind();
            ddltunj3.Items.Insert(0, new ListItem("Pilih Data", "-"));

            txttunj2.Enabled = true;
            ddltunj3.Enabled = true;
        }
        else
        { ;
            ddltunj3.SelectedValue = "-";
            ddltunj4.SelectedValue = "-";
            ddltunj5.SelectedValue = "-";

            ddltunj3.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddltunj4.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddltunj5.Items.Insert(0, new ListItem("Pilih Data", "-"));
             
            txttunj3.Enabled = false;
            txttunj4.Enabled = false;
            txttunj5.Enabled = false;
             
            txttunj3.Text = "0,00";
            txttunj4.Text = "0,00";
            txttunj5.Text = "0,00";
        }
    }
    protected void ddltunj3_onselectedchange(object sender, EventArgs e)
    {

        if (ddltunj3.SelectedValue != "-")
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETTUNJANGAN4", typeof(string));
            var[1] = gb.CreateSPParameter("@tunj1", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj1.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@tunj2", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj2.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@tunj3", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj3.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTunj asc";

            ddltunj4.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddltunj4.DataTextField = "tyket";
            ddltunj4.DataValueField = "TypeTunj";
            ddltunj4.DataBind();
            ddltunj4.Items.Insert(0, new ListItem("Pilih Data", "-"));


            txttunj3.Enabled = true;
            ddltunj4.Enabled = true;
            
        }
        else
        { 
            ddltunj4.Enabled = false;
            ddltunj5.Enabled = false;
             
            ddltunj4.SelectedValue = "-";
            ddltunj5.SelectedValue = "-";

            txttunj4.Enabled = false;
            txttunj5.Enabled = false;
            

            txttunj4.Text = "0,00";
            txttunj5.Text = "0,00";
        }
    }
    protected void ddltunj4_onselectedchange(object sender, EventArgs e)
    {

        if (ddltunj4.SelectedValue != "-")
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETTUNJANGAN5", typeof(string));
            var[1] = gb.CreateSPParameter("@tunj1", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj1.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@tunj2", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj2.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@tunj3", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj3.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@tunj4", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltunj4.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTunj asc";

            ddltunj5.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddltunj5.DataTextField = "tyket";
            ddltunj5.DataValueField = "TypeTunj";
            ddltunj5.DataBind();
            ddltunj5.Items.Insert(0, new ListItem("Pilih Data", "-"));

            ddltunj5.Enabled = true;
            txttunj4.Enabled = true;
        }
        else
        { 
            ddltunj5.Enabled = false;
            ddltunj5.SelectedValue = "-";

            txttunj5.Enabled = false; 
            txttunj5.Text = "0,00";
        }
    }
    protected void ddltunj5_onselectedchange(object sender,EventArgs e)
    { 
        if(ddltunj5.SelectedValue != "-")
        { 
            txttunj5.Enabled = true;
        }
        else
        {
            txttunj5.Enabled = false;
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
    protected void lembur_ontextchanged(object sender,EventArgs e)
    {
        decimal a, b, c, d, f, g, h, i, j, k, x, y, z;
        //a = decimal.Parse(txtgjmx.Text);
        b = decimal.Parse(txttutep.Text);
        c = decimal.Parse(txttujab.Text);
        d = decimal.Parse(txttumakan.Text);
        f = decimal.Parse(txttutrp.Text);

        g = decimal.Parse(txtgjmx.Text);
        h = decimal.Parse(txttutep.Text);
        i = decimal.Parse(txttujab.Text);
        j = decimal.Parse(txttumakan.Text);
        k = decimal.Parse(txttutrp.Text);

        x = decimal.Parse(txtgapok.Text);

        if (x > g)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Gaji Pokok tidak boleh lebih dari Maksimal Gaji Pokok Golongan!');", true);
        }
        else
        {
            txttunjhrRaya.Text = string.Format("{0:N2}", x);
        }
       // z = (b + c + d + f + g + h + i + j + k + x)/173;

        //txtkurslembur.Text = z.ToString();
        //txtkurslembur.Text = string.Format("{0:N2}", z);
    }

    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlunitbisnis_SelectedIndexChanged(object sender, EventArgs e)
    {

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


    }
    
    protected void ddlbyr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlbyr.SelectedValue == "C")
        {
            txtnmbank.Text = "-";
            txtnmbank.Enabled = false;
        }else
        {

            txtnmbank.Text = "";
            txtnmbank.Enabled = true;
        }
    }
    protected void ddlrekpersh_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrekpersh.SelectedValue == "1111111111")
        {
            txtnmbakpersh.Text = "-";
            txtnmbakpersh.Enabled = false;
            txtcbgbank.Text = "-";
            txtcbgbank.Enabled = false;
            txtrekkyn.Text = "0";
            txtrekkyn.Enabled = false; 
        }
        else
        {

            txtnmbakpersh.Text = "";
            txtnmbakpersh.Enabled = true;
            txtcbgbank.Text = "";
            txtcbgbank.Enabled = true;
            txtrekkyn.Text = "";
            txtrekkyn.Enabled = true;
        } 
    } 
}




