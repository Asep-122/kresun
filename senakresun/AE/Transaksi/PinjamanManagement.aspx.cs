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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using CrystalDecisions.Web;

public partial class AE_Process_PinjamanManagement : System.Web.UI.Page
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
    DataTable dtRP = new DataTable("Transaction");
    DataTable dtrpinjaman = new DataTable("Pinjaman");
    DataTable dtrangsuran = new DataTable("Angsuran");
    DataTable dtrJurnal = new DataTable("Jurnal");
    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();
    GlobalEntities gx = new GlobalEntities();
    
    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "B2";
    string kodesumber = "AE";
    string KodeKonter = "AE02";
    public string nobuktidb;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttgljthtmp.Text = txttgltrn.Text;
                txtKeterangan.Attributes.Add("maxlength", "50");


                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STATUSKARY", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

                ddlsttskyn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlsttskyn.DataTextField = "TEXT";
                ddlsttskyn.DataValueField = "VALUE";
                ddlsttskyn.DataBind();

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
                var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));

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
                var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
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
                var1[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var1, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

                ddloff.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddloff.DataTextField = "kdnm";
                ddloff.DataValueField = "KodeCabang";
                ddloff.DataBind();
                //ddloff.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddloff.SelectedValue = SessBranchID;

                //DROPDOWNLIST TYPE KANTOR
                DefaultClass.Global.SqlHelper[] var11 = new DefaultClass.Global.SqlHelper[2];
                var11[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETKOPERASI", typeof(string));
                var11[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var11, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";

                ddlkoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkoperasi.DataTextField = "nama";
                ddlkoperasi.DataValueField = "kode_koperasi";
                ddlkoperasi.DataBind();
                //ddloff.Items.Insert(0, new ListItem("Pilih Data", "-"));
                //ddlkoperasi.SelectedValue = SessBranchID;
            }

            //pwcabang system kurs single default IDR kurs 1

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
            DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
            int dayx = Convert.ToInt32(Convert.ToDateTime(txttgltrn.Text).ToString("dd"));
            txttgltrn.Enabled = false;
            if ((dayx >= 24) && (dayx <= 27))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pinjaman Kasbon tidak boleh diajukan di periode tanggal tersebut!');", true);
            }
            else
            {
                blnadd = 0;
                //GRTP.Enabled = true;
                GRRP.Enabled = true;
                pnlHeader.Visible = false;
                PnlFront.Visible = false;

                btndelete.Visible = false;
                btnSimpan.Visible = true;
                txtnobukti.Text = "";
                //pnladd.Visible = true;

                //txtKeterangan.Text = "Terima Simpok";
                Pnlcustomer.Visible = true;

                if (txtnobukti.Text == "")
                {
                    btnSimpan.Visible = true;
                    btnSimpan.Enabled = false;
                    btndelete.Visible = false;
                }
                else
                {
                    btnSimpan.Visible = false;
                    btndelete.Visible = true;
                }

                //btnback4.Visible = false;
                btnclose.Visible = true;
                Pnlcustomer.Visible = true;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAPINJAMAN", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANMANAGEMENT", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn desc";


            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt  = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":
                try
                {
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    txtangsuran.Enabled = false;
                    txtjmlpinjaman.Enabled = false;
                  
                    txtKeterangan.Enabled = false;
                   
                    txtangsuranperbulan.Enabled = false;
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;
                   
                    Pnlfooter4.Visible = true;
                    Pnlcustomer.Visible = false;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txtkodekaryawan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnik")).Text;
                    string datax = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                    var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, datax, typeof(string));
                    var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAPINJAMANX", typeof(string));
                    var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANMANAGEMENT", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtkodekaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["KodeNik"].ToString(); ;
                        txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
                      
                        txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglmasuk"].ToString()).ToString("dd-MM-yyyy");
                        //txtmasakerja.Text = ge.ListDataset.Tables[0].Rows[0]["masakerja"].ToString();
                        ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
                        ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString();
                        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[4];
                        var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
                        var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
                        var4[3] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbag.SelectedValue, typeof(string));
                        gx.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
                        gx.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";
                      

                        ddljab.DataSource = gx.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljab.DataTextField = "kdket";
                        ddljab.DataValueField = "KodeJab";
                        ddljab.DataBind();
                        ddljab.Items.Insert(0, new ListItem("Pilih Data", "-"));


                        ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                        ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["golongan"].ToString();
                        ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statuskaryawan"].ToString();
                       
                        txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalangsuran"].ToString()));
                        txtangsuranpokokperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranPokok"].ToString()));
                        txtangsuranbungaperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranbunga"].ToString()));
                       

                        txttgltrn.Text= DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString()).ToString("dd-MM-yyyy");
                        txtjmlpinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Jlhpinjaman"].ToString()));
                        txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tenor"].ToString()));
                        txtKeterangan.Text = ge.ListDataset.Tables[0].Rows[0]["notememo"].ToString();
                        txtjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                        txtalamatkaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["alamatkaryawan"].ToString();
                        ddlkoperasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Koperasi"].ToString();
                        txtbunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapsn"].ToString()));
                    }
                    



                    //rbpotong.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString() == "1" ? "1" : "2"; rbpotong.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString() == "0" ? "2" : ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString();
                    //txtkodekaryawan.Text = "";
                    //txtnamakaryawan.Text = "";

                    txtangsuranperbulan.Enabled = false;
                  
                    txttgltrn.Enabled = false;
                  
                    txtKeterangan.Enabled = false;
                    txttgltrn.Enabled = false;
                    txtangsuran.Enabled = false;
                    txtjmlpinjaman.Enabled = false;

                    txtjaminan.Enabled = false;
                    txtalamatkaryawan.Enabled = false;
                  
                    //txtjmlhCL.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                 
                    //Pnlfooter1.Visible = true;
                    btnSimpan.Visible = false;
                    //btnback4.Visible = false;
                    Pnlfooter4.Visible = true;

                    if (decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Mutasiangsuran"].ToString()) > 0)
                    {
                        btndelete.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Pinjaman sudah ada transaksi pembayaran tidak dapat dihapus!');", true);
                    }
                    else
                    {
                        btndelete.Visible = true;
                    }
                    
                    btnclose.Visible = true;
                    btngenerate.Visible = false;

                    PnlGrid.Visible = true;
                    DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[6];
                    Paramsx[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAPINJAMANXDTL", typeof(string));
                    Paramsx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramsx[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("AE_SPINJAMANMANAGEMENT", Paramsx, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        ViewState["ListDetailRP"] = ge.ListDataTable;

                        GRRP.DataSource = ge.ListDataTable;
                        GRRP.DataBind();

                        GRRP.UseAccessibleHeader = true;
                        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                        //cssGridAP();
                        //subgridGL_Click();
                    }
                    else
                    {
                        //SetDetailAP();
                        //subgridGL_Click();
                    }
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;
            case "PK":
                try
                {
                    string nobukti;

                    string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                    var3[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PK", typeof(string));
                    var3[2] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, norek, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_CetakPinjamanMGM", var3, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        //rprt.Load(Server.MapPath(file_report));
                        rprt.Load(Server.MapPath("~/AE/Report/CR/rptPerjanjianKredit.rpt"));
                        rprt.SetParameterValue("kepala_cabang", ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString());
                        rprt.SetParameterValue("alamat_cabang", ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString());
                        rprt.SetParameterValue("nama_nasabah", ge.ListDataset.Tables[0].Rows[0]["picb"].ToString());
                        rprt.SetParameterValue("alamat_nasabah", ge.ListDataset.Tables[0].Rows[0]["alamat_nasabah"].ToString());
                        rprt.SetParameterValue("nm_penjamin", ge.ListDataset.Tables[0].Rows[0]["nmpjmn2"].ToString());
                        rprt.SetParameterValue("plafon", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()).Replace("Rp", ""));
                        rprt.SetParameterValue("jangka", ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString());
                        rprt.SetParameterValue("tgl_drop", ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString());
                        rprt.SetParameterValue("tgl_due", ge.ListDataset.Tables[0].Rows[0]["tgl_due"].ToString());
                        rprt.SetParameterValue("pokok----------10", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString()).Replace("Rp", ""));
                        rprt.SetParameterValue("bunga", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()).Replace("Rp", ""));
                        rprt.SetParameterValue("angsuran_perbulan", string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rek_tabbrx"].ToString())));
                        rprt.SetParameterValue("persen_denda", ge.ListDataset.Tables[0].Rows[0]["persen_denda_pelunasan"].ToString());
                        rprt.SetParameterValue("jaminan", ge.ListDataset.Tables[0].Rows[0]["ket_jmn"].ToString());
                        rprt.SetParameterValue("nama_cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                        rprt.SetParameterValue("terbilangPlafon", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString())));
                        rprt.SetParameterValue("terbilang_angsuran_perbulan", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rek_tabbrx"].ToString())));
                        rprt.SetParameterValue("no_pk", ge.ListDataset.Tables[0].Rows[0]["no_spk"].ToString());
                        rprt.SetParameterValue("no_rekening-----20", norek);
                        rprt.SetParameterValue("nama_koperasi", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                        rprt.SetParameterValue("tanggal_print", DateTime.Now.ToString("dd-MMMM-yyyy"));
                        rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                        rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                        rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                        rprt.SetParameterValue("statuskop", "");
                        rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());
                        rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                        rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                        rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                        rprt.SetParameterValue("cabang", ge.ListDataset.Tables[0].Rows[0]["cabang"].ToString());
                        rprt.SetParameterValue("bagian", ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString());
                        rprt.SetParameterValue("jabatan", ge.ListDataset.Tables[0].Rows[0]["jabatan"].ToString());
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                        Response.End();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != "Thread was being aborted.")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
                    }
                    else
                    {
                        rprt.Dispose();
                        rprt.Close();
                    }
                }
                break;
            case "KA":
                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KARTUANGSURAN", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    //Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AE_CetakPinjamanMGM", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/AE/Report/Cr/rptCetakKartuAngsuran.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    }


                }

                catch (Exception X)
                {

                    if (X.Message != "Thread was being aborted.")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    }


                }
                break;
            case "SK":
                try
                {
                    string nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    string kodecabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;

                    string namakaryawan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamakar")).Text;
                    //string namacabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacabang")).Text;
                    string tenor = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblangsuran")).Text;
                    string totalangsuran = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblangsuran")).Text;
                    string namacabang = "";

                    Global.SqlHelper[] Paramsc = new Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SEELECTSURATKUASA", typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, kodecabang, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, nobukti, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_CetakPinjamanMGM", Paramsc, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        int jumlrow = ge.ListDataset.Tables[0].Rows.Count;
                        string bulanawal = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(ge.ListDataset.Tables[0].Rows[0]["BulanPeriode"].ToString()));
                        string bulanakhir = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(ge.ListDataset.Tables[0].Rows[jumlrow - 1]["BulanPeriode"].ToString()));
                        //if (ge.ListDataset.Tables[0].Rows[0]["cabang_nama"].ToString() == "Head Office")
                        //{
                        //    namacabang = "Jakarta";
                        //}else
                        //{
                        //    namacabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacabang")).Text;
                        //}
                        rprt.Load(Server.MapPath("~/AE/Report/Cr/rptSuratKuasa.rpt"));
                        //rprt.SetDataSource(ge.ListDataset);


                        rprt.SetParameterValue("nobukti", nobukti);
                        rprt.SetParameterValue("namakoperasi", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                        rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());
                        rprt.SetParameterValue("namacabang", namacabang);
                        rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["Badan_hukum"].ToString());
                        rprt.SetParameterValue("pad", "PAD No. : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                        rprt.SetParameterValue("kantorpusat", "Kantor Pusat : " + ge.ListDataset.Tables[0].Rows[0]["Alamat"].ToString());
                        rprt.SetParameterValue("alamatcabang", ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString());
                        rprt.SetParameterValue("nama", namakaryawan);
                        rprt.SetParameterValue("pekerjaan", "Karyawan " + ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString() + " " + ge.ListDataset.Tables[0].Rows[0]["cabang_nama"].ToString());
                        rprt.SetParameterValue("tenor", ge.ListDataset.Tables[0].Rows[0]["tenor"].ToString());
                        rprt.SetParameterValue("terbilangtenor", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tenor"].ToString().Replace(",", "."))));
                        rprt.SetParameterValue("nobukti", nobukti);
                        rprt.SetParameterValue("angsuran", totalangsuran);
                        rprt.SetParameterValue("terbilangjumlah", gb.Terbilang(decimal.Parse(totalangsuran)));
                        rprt.SetParameterValue("awalangsuran", bulanawal); 
                        rprt.SetParameterValue("tahunawal", ge.ListDataset.Tables[0].Rows[0]["TahunPeriode"].ToString());
                        rprt.SetParameterValue("akhirangsuran", bulanakhir);
                        rprt.SetParameterValue("tahunakhir", ge.ListDataset.Tables[0].Rows[jumlrow - 1]["TahunPeriode"].ToString());
                        rprt.SetParameterValue("namajabatan", "Sunyeto, SE");
                        if (ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString() == "IV")
                        {
                            rprt.SetParameterValue("jabatan", "Ketua Koperasi");
                        }
                        else
                        {
                            rprt.SetParameterValue("jabatan", "General Manager");
                        }
                        rprt.SetParameterValue("tanggal", ge.ListDataset.Tables[0].Rows[0]["cabang_nama"].ToString() + ", " + DateTime.Now.ToString("dd MMMM yyyy"));
                        rprt.SetParameterValue("statuskop", "1");

                        //Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                        Response.End();
                    }
                }
                catch (Exception X)
                {
                    if (X.Message != "Thread was being aborted.")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    }
                    else
                    {
                        rprt.Close();
                        rprt.Dispose();
                    }
                }
                break;
            case "KP":
                try
                {
                    string nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    string kodecabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;

                    string namakaryawan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamakar")).Text;
                    //string namacabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacabang")).Text;
                    string totalangsuran = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljlhdebit")).Text;
                    string kodenik = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnik")).Text;

                    Global.SqlHelper[] Paramsc = new Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SEELECTKWITANSI", typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, kodecabang, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, nobukti, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_CetakPinjamanMGM", Paramsc, "constrmf");
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AE_CetakPinjamanMGM", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/AE/Report/CR/rptKwitansi.rpt"));
                        //rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("nobukti", "No Bukti : " + nobukti);
                        rprt.SetParameterValue("namakoperasi", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                        rprt.SetParameterValue("kodenik", "NIK : " + kodenik);
                        rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());
                        //rprt.SetParameterValue("namacabang", namacabang);
                        rprt.SetParameterValue("jumlahpinjaman", totalangsuran);
                        rprt.SetParameterValue("keterangan", ge.ListDataset.Tables[0].Rows[0]["notememo"].ToString());
                        rprt.SetParameterValue("terbilangjumlah", gb.Terbilang(decimal.Parse(totalangsuran)));
                        rprt.SetParameterValue("nama", namakaryawan);
                        rprt.SetParameterValue("tanggal", ge.ListDataset.Tables[0].Rows[0]["cabang_nama"].ToString() + ", " + DateTime.Now.ToString("dd MMMM yyyy"));

                        //Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                    }
                }
                catch (Exception X)
                {
                    if (X.Message != "Thread was being aborted.")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    }
                }
                break;

        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEPINJAMAN", typeof(string));
            var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
           

            ge.ListDataset = gb.GetDataJustOutPut("AE_PINJAMANMANAGEMENT_Delete", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Visible = false;
                //btnCancel.Visible = false;
                nobuktidb = txtnobukti.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','Data Berhasil Di hapus','bottom-center','success','2');", true);

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
        return Page.ClientScript.GetPostBackEventReference(btnclose, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
         {
            decimal jlhdibayar = decimal.Parse(txtjmlpinjaman.Text);
            int Angsuran = int.Parse(txtangsuran.Text);
            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Pinjaman Masih Kosong!');", true);
            }
            else if (Angsuran <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tenor Masih Kosong!');", true);
            }
            else
            {
                dtRP.Columns.Clear();
                dtRP.Columns.Add(new DataColumn("tgljthtempo", typeof(DateTime)));
                dtRP.Columns.Add(new DataColumn("angske", typeof(int)));
                dtRP.Columns.Add(new DataColumn("angsuran", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("keterangan", typeof(string)));
                dtRP.Columns.Add(new DataColumn("bulan", typeof(int)));
                dtRP.Columns.Add(new DataColumn("tahun", typeof(int)));
                dtRP.Columns.Add(new DataColumn("angsuranpokok", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("angsuranbunga", typeof(decimal)));
                //string nomorrekening = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();
                dtRP.Rows.Clear();
                for (int i = 0; i < GRRP.Rows.Count; i++)
                 
                {
                    DataRow dr = dtRP.NewRow();
                    
                    dr["tgljthtempo"] = ((Label)GRRP.Rows[i].FindControl("lbltgl")).Text;
                    dr["angske"] = int.Parse(((Label)GRRP.Rows[i].FindControl("lblangske")).Text); 
                    dr["angsuran"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblangsuran")).Text); 
                    dr["keterangan"] = "INSERT";
                    dr["bulan"] = int.Parse(((Label)GRRP.Rows[i].FindControl("lblbulan")).Text);
                    dr["tahun"] = int.Parse(((Label)GRRP.Rows[i].FindControl("lbltahun")).Text);
                    dr["angsuranpokok"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblPokok")).Text);
                    dr["angsuranbunga"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblBunga")).Text);
                    dtRP.Rows.Add(dr);
                }
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[25];
                var1[0] = gb.CreateSPParameter("@AE_Angsuran_Pinjaman_MGT", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlpinjaman.Text, typeof(decimal));
                var1[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                var1[5] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                var1[6] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                var1[7] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                var1[8] = gb.CreateSPParameter("@tenor", ParameterDirection.Input, SqlDbType.Int, 0, txtangsuran.Text, typeof(int));
                var1[9] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodekaryawan.Text, typeof(string));
                var1[10] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 30, "AE", typeof(string));
                var1[11] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                var1[12] = gb.CreateSPParameter("@NamaKry", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamakaryawan.Text, typeof(string));
                var1[13] = gb.CreateSPParameter("@Jaminan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtjaminan.Text, typeof(string));
                var1[14] = gb.CreateSPParameter("@tglmasuk", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljoin.Text, typeof(DateTime));
                var1[15] = gb.CreateSPParameter("@alamatkaryawan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtalamatkaryawan.Text, typeof(string));
                var1[16] = gb.CreateSPParameter("@statuskry", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlsttskyn.SelectedValue, typeof(string));
                var1[17] = gb.CreateSPParameter("@bagian", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbag.SelectedValue, typeof(string));
                var1[18] = gb.CreateSPParameter("@jabatan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljab.SelectedValue, typeof(string));
                var1[19] = gb.CreateSPParameter("@golongan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgol.SelectedValue, typeof(string));
                var1[20] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbunga.Text, typeof(decimal));
                var1[21] = gb.CreateSPParameter("@angsuranpokok", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranpokokperbulan.Text, typeof(decimal));
                var1[22] = gb.CreateSPParameter("@angsuranbunga", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranbungaperbulan.Text, typeof(decimal));
                var1[23] = gb.CreateSPParameter("@angsuranperbulan", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranperbulan.Text, typeof(decimal));
                var1[24] = gb.CreateSPParameter("@Kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkoperasi.SelectedValue, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_PINJAMANMANAGEMENT_INSERT", var1, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    btnclose.Visible = false;
                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);

                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
            
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


    //protected void txtdate_TextChanged(object sender, System.EventArgs e)
    //{
    //    DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
    //    varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
    //    varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
    //    varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //    varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
    //    ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

    //    string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
    //    DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
    //    if (xstatus == "1")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
    //        txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
    //        txttgljthtmp.Text = txttgltrn.Text;
    //    }
    //    txttgljthtmp.Text = txttgltrn.Text;
    //}

    private void lnkAddRP2_Click()
    {

        int dgcount = GRRP.Rows.Count;
        if (dgcount > 0)
        {
            dtRP.Columns.Clear();

            dtRP = (DataTable)ViewState["ListDetailRP"];

            dtRP.Rows.Clear();

            for (int i = 0; i < dgcount; i++)
            {
                DataRow dr = dtRP.NewRow();

                dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
                dr["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                dr["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
                dtRP.Rows.Add(dr);
            }

            //DataRow drdnew = dtRP.NewRow();
            //drdnew["nomor"] = dgcount + 1;
            //drdnew["KodeGL"] = string.Empty;
            //drdnew["NamaGL"] = string.Empty;
            //drdnew["Description"] = string.Empty;
            //drdnew["Costcenter"] = string.Empty;
            //drdnew["jlhdebit"] = decimal.Zero;
            //drdnew["jlhkredit"] = decimal.Zero;

            //dtRP.Rows.Add(drdnew);

            ViewState["ListDetailRP"] = dtRP;

            GRRP.DataSource = dtRP;
            GRRP.DataBind();

            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

            cssGridRP();

        }


        else
        {
            SetDetailRP();
        }


    }
    private void cssGridRP()
    {

        // Attribute to show the Plus Minus Button.
        GRRP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRRP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-hide", "phone");
        cells[4].Attributes.Add("data-hide", "phone");

    }
    private void SetDetailRP()
    {

        DataRow dr = null;

        dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));

        dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));


        dr = dtRP.NewRow();
        dr["nomor"] = 1;
        dr["KodeGL"] = string.Empty;
        dr["NamaGL"] = string.Empty;
        dr["Description"] = txtKeterangan.Text; 
        dr["Costcenter"] = string.Empty;
        dr["jlhdebit"] = decimal.Zero;
        dr["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRP"] = dtRP;


        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();
    }
    protected void lnkBT_Click(object sender, EventArgs e)
    {
        int dgcount = GRRP.Rows.Count;

        dtRP.Columns.Clear();

        dtRP = (DataTable)ViewState["ListDetailRP"];

        dtRP.Rows.Clear();

        for (int i = 0; i < dgcount; i++)
        {
            DataRow dr = dtRP.NewRow();

            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
            dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
            dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
            dr["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
            dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
            dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            dr["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
            dtRP.Rows.Add(dr);
        }

        DataRow drdnew = dtRP.NewRow();
        drdnew["nomor"] = dgcount + 1;
        drdnew["KodeGL"] = string.Empty;
        drdnew["NamaGL"] = string.Empty;
        drdnew["Description"] = string.Empty;
        drdnew["Costcenter"] = string.Empty;
        drdnew["jlhdebit"] = decimal.Zero;
        drdnew["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(drdnew);

        ViewState["ListDetailRP"] = dtRP;

        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
        //subgridGL_Click();
        cssGridRP();
    }
    protected void GRRP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "DEL":

                int dgcount = GRRP.Rows.Count;


                ((Label)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRP"];

                DataRow drCurrentRow = null;

                dtRP.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRRP.Rows.Count; i++)
                {

                    if (((Label)GRRP.Rows[i].FindControl("lblNumber")).Text != "")
                    {


                        drCurrentRow = dtRP.NewRow();

                        drCurrentRow["nomor"] = j;
                        drCurrentRow["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                        drCurrentRow["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["jlhdebit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                        drCurrentRow["jlhkredit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text;


                        dtRP.Rows.Add(drCurrentRow);

                        j++;
                    }
                }

                if (dgcount == 1)
                {
                    DataRow drdnew = dtRP.NewRow();

                    drdnew["nomor"] = 1;
                    drdnew["KodeGL"] = string.Empty;
                    drdnew["NamaGL"] = string.Empty;
                    drdnew["Description"] = string.Empty;
                    drdnew["Costcenter"] = string.Empty;
                    drdnew["jlhdebit"] = decimal.Zero;
                    drdnew["jlhkredit"] = decimal.Zero;
                    dtRP.Rows.Add(drdnew);
                }


                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRP();
                //subgridGL_Click();

                break;
        }
    }
    //protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
    //        Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //        Params[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoa", Params, "constrmf");
    //        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
    //        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlnamaGL")), "-", "-");



    //        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[2];
    //        xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //        xParams[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //        gex.ListDataset = gbx.GetDataJustOutPut("dbo.GL_GetCoacode", xParams, "constrmf");
    //        gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
    //        gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");


    //        ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;
    //        ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlnamaGL")).Value;


    //        DefaultClass.Global.SqlHelper[] xParamt = new DefaultClass.Global.SqlHelper[3];
    //        xParamt[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value, typeof(String));
    //        xParamt[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
    //        xParamt[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParamt, "constrmf");
    //        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "-", "-");

    //        if (ge.ListDataset.Tables[0].Rows.Count > 0)
    //        {
    //            //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "", "");
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).DataTextField = "Text";
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).DataValueField = "Value";
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).DataBind();

    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).Enabled = true;
    //        }
    //        else
    //        {
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).Items.Insert(0, new ListItem("-", "-"));
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).Enabled = false;
    //        }

    //         ((DropDownList)e.Row.FindControl("ddlcostcenter")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlcostcenter")).Value;



    //        if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
    //        {
    //            ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

    //        }

    //        GRRP.UseAccessibleHeader = true;
    //        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

    //    }
    //}



    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {
        decimal bayar;
        
    }
    protected void txtjmlpinjaman_change(object sender, EventArgs e)
    {
        try
        {
            decimal pinjaman = decimal.Parse(txtjmlpinjaman.Text.ToString());
            decimal batas = decimal.Parse("0");
            decimal angsuran = decimal.Parse(txtangsuran.Text.ToString());
            decimal bunga = decimal.Parse(txtbunga.Text.ToString()) / decimal.Parse("100");



            if (txtangsuran.Text == "0" || txtangsuran.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Tenor tidak Boleh Nol atau Kosong');", true);
                txtangsuran.Focus();
            }
            else if (pinjaman == 0 || txtjmlpinjaman.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Pinjaman tidak Boleh Nol atau Kosong');", true);
                txtjmlpinjaman.Focus();
            }
            else if (pinjaman != 0 && pinjaman != 0)
            {
                if (angsuran > 0)
                {
                    txtangsuranpokokperbulan.Text = string.Format("{0:N2}", (pinjaman / angsuran));
                    txtangsuranbungaperbulan.Text = string.Format("{0:N2}", ((bunga * angsuran) * pinjaman) / angsuran);

                    txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(txtangsuranpokokperbulan.Text) + decimal.Parse(txtangsuranbungaperbulan.Text));
                    btnGenerate_Click(sender, e);
                    txtjmlpinjaman.Focus();
                }
                else
                {
                    txtangsuran.Focus();
                }
              
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void txtangsuran_change(object sender, EventArgs e)
    {
        try
        {
            decimal pinjaman = decimal.Parse(txtjmlpinjaman.Text.ToString());
            decimal batas = decimal.Parse("0");
            decimal angsuran = decimal.Parse(txtangsuran.Text.ToString());
            decimal bunga = decimal.Parse(txtbunga.Text.ToString()) / decimal.Parse("100");



            if (txtangsuran.Text == "0" || txtangsuran.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Tenor tidak Boleh Nol atau Kosong');", true);
                txtangsuran.Focus();
            }
            else if (pinjaman == 0 || txtjmlpinjaman.Text == "" )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Pinjaman tidak Boleh Nol atau Kosong');", true);
                txtjmlpinjaman.Focus();
            }
           else if (pinjaman != 0 && pinjaman != 0)
            {
               
                    txtangsuranpokokperbulan.Text = string.Format("{0:N2}", (pinjaman / angsuran));
                    txtangsuranbungaperbulan.Text = string.Format("{0:N2}",bunga * pinjaman);

                    txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(txtangsuranpokokperbulan.Text) + decimal.Parse(txtangsuranbungaperbulan.Text));
                    btnGenerate_Click(sender, e);
             
                    txtangsuran.Focus();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            decimal pinjaman = decimal.Parse(txtjmlpinjaman.Text.ToString());
            if (txtKeterangan.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Keterangan tidak Lebih Kosong');", true);
            }
            else if (txtangsuran.Text == "0" || txtangsuran.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Angsuran tidak Boleh Nol atau Kosong');", true);
                txtangsuran.Focus();
            }
            else if (pinjaman == 0 || txtjmlpinjaman.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Pinjaman tidak Boleh Nol atau Kosong');", true);
                txtjmlpinjaman.Focus();
            }
            else
            {
                Pnlfooter4.Visible = true;
                PnlGrid.Visible = true;
                DataRow dr = null;

                dtRP.Columns.Add(new DataColumn("nomor", typeof(string)));
                dtRP.Columns.Add(new DataColumn("tgljthtempo", typeof(string)));
                dtRP.Columns.Add(new DataColumn("angske", typeof(string)));
                dtRP.Columns.Add(new DataColumn("angsuran", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("keterangan", typeof(string)));
                dtRP.Columns.Add(new DataColumn("bulan", typeof(int)));
                dtRP.Columns.Add(new DataColumn("tahun", typeof(int)));
                dtRP.Columns.Add(new DataColumn("angsuranPokok", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("angsuranBunga", typeof(decimal)));
                //dtRP.Columns.Add(new DataColumn("bunga", typeof(decimal)));
                //string nomorrekening = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();
                string ambilbulan = Convert.ToString(Convert.ToDateTime(txttgltrn.Text).ToString("MM"));
                string rubahtahun = Convert.ToString(Convert.ToDateTime(txttgltrn.Text).ToString("yyyy"));
                int dayx = Convert.ToInt32(Convert.ToDateTime(txttgltrn.Text).ToString("dd"));
                int ambiltahun ;
                int rubahbulan=0;

                for (int i = 0; i < int.Parse(txtangsuran.Text); i++)
                {
                    dr = dtRP.NewRow();

                    ambiltahun = int.Parse(rubahtahun);                 
                    

                    if (i == 0)
                    {
                        //if (dayx <= 23)
                        //{
                        //    rubahbulan = int.Parse(ambilbulan);
                        //}
                        //else
                        //{
                            rubahbulan = int.Parse(ambilbulan) +  1;
                        //}
                    }
                    else
                    {
                        rubahbulan = rubahbulan +  1;
                    }
                    
                    int bulanPeriode = 0; 
                    int tahunPeriode = 0;
                    string rubahtanggal = "";
                    string datatanggal;
                    DateTime tanggalrubah;
                    if (rubahbulan > 12)
                    {
                        rubahtanggal = dayx + "-" + Convert.ToString(Convert.ToInt32(rubahbulan - 12)) + "-" + Convert.ToString(Convert.ToInt32(ambiltahun + 1));
                        tanggalrubah = Convert.ToDateTime(rubahtanggal);
                        datatanggal = tanggalrubah.ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        rubahtanggal = dayx + "-" + Convert.ToString(rubahbulan) + "-" + Convert.ToString(ambiltahun);
                        tanggalrubah = Convert.ToDateTime(rubahtanggal);
                        datatanggal = tanggalrubah.ToString("dd/MM/yyyy");
                    }
                    bulanPeriode = int.Parse(Convert.ToDateTime(datatanggal).ToString("MM")); 
                    tahunPeriode = int.Parse(Convert.ToDateTime(datatanggal).ToString("yyyy"));
                    dr["nomor"] = i + 1;
                    dr["tgljthtempo"] = datatanggal;
                    dr["angske"] = i + 1;
                    dr["angsuran"] = Convert.ToDecimal(txtangsuranperbulan.Text);
                    dr["keterangan"] = txtKeterangan.Text;
                    dr["bulan"] = bulanPeriode;
                    dr["tahun"] = tahunPeriode;
                    dr["angsuranPokok"] = Convert.ToDecimal(txtangsuranpokokperbulan.Text);
                    dr["angsuranbunga"] = Convert.ToDecimal(txtangsuranbungaperbulan.Text);
                    dtRP.Rows.Add(dr);
                }

                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
                //subgridGL_Click();
                cssGridRP();
                btnSimpan.Enabled = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            var kodekry = hdnkaryawan.Value;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, kodekry, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIAEMSTBARU", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANMANAGEMENT", var, "constrmf");
            //Boolean status = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["stl"].ToString());
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
               
                txtkodekaryawan.Text = kodekry;
                txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
                //txtbataspinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonkasbon"].ToString())); 
                txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTerima"].ToString()).ToString("dd-MM-yyyy");

                ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
                ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();
                //DROPDOWNLIST TYPE JABATAN
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[4];
                var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
                var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
                var4[3] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbag.SelectedValue, typeof(string));
                gx.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
                gx.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

                ddljab.DataSource = gx.ListDataset.Tables[0].DefaultView.ToTable();
                ddljab.DataTextField = "kdket";
                ddljab.DataValueField = "KodeJab";
                ddljab.DataBind();
                ddljab.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();
                ddlkoperasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString();
                txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse("0"));
                pnladd.Visible = true;
                //Pnlfooter4.Visible = true;
                txtangsuran.Text = "0";
                txtjmlpinjaman.Text = "0";
                btnSimpan.Enabled = true;
                txtangsuran.Enabled = false;
                txtKeterangan.Text = "Pinjaman Management Karyawan";
                txtangsuran.Enabled = true;
                txtangsuranpokokperbulan.Text = "0";
                txtangsuranbungaperbulan.Text = "0";
            }
            



        
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    [WebMethod]
    public static string GetKaryawan(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
        Params[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));
        Params[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARINIK", typeof(string));
        Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_SPINJAMANMANAGEMENT", Params, "constrmf");
        
        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);
        return json;
    }






}
