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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
public partial class PN_AhliWaris : System.Web.UI.Page
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

    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "S2";
    string kodesumber = "SN";
    string KodeKonter = "SN02";
    public string nobuktidb;

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        
        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        Pnlfooter4.Visible = true;
        btnSimpan.Visible = false;
        ddlKoperasi.Enabled = false;
        ddlcabang.Enabled = false;

    }
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void UploadFile(object sender, EventArgs e)
    {
        string namafile;
        string fotopath;
        string folderPath = Server.MapPath("~/Files/Tarikan/");
        DateTime now = DateTime.Now;

        namafile = now.ToString("ddMMyyyyhhmmss") + "_" + "BuktiTarikan";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }
        //fileupload.SaveAs(folderPath + namafile + ".jpg");


        fotopath = folderPath + namafile + ".jpg";
        //txtpath.Text = fotopath;


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();
                ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";
                ddlprovinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlprovinsiktp.DataTextField = "nama_provinsi";
                ddlprovinsiktp.DataValueField = "kode_provinsi";
                ddlprovinsiktp.DataBind();


                //KODE LANGGANAN
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKOPERASI", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";
                ddlKoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKoperasi.DataTextField = "nama_koperasi";
                ddlKoperasi.DataValueField = "kode_koperasi";
                ddlKoperasi.DataBind();
                ddlKoperasi.SelectedValue = "I";
                //ddlKoperasi.Items.Insert(0, new ListItem("Pilih Data", "-"));


                var Koperasi = ddlKoperasi.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
              
                    ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlcabang.DataTextField = "nama_cabang";
                    ddlcabang.DataValueField = "kode_cabang";
                    ddlcabang.DataBind();
                //ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlcabang.SelectedValue = "000";


                //KODE Produk
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[3];
                var5[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var5[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPAJAK", typeof(string));
                var5[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var5, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "Pajak asc";
                ddlpajak.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlpajak.DataTextField = "Pajak";
                ddlpajak.DataValueField = "Pajak";
                ddlpajak.DataBind();
                ddlpajak.SelectedValue = "0";
                //ddlpajak.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
            }
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            fileuploadktpwaris1.Dispose();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "Idpermohonan desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":
                try
                {
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
      
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;
                    Pnlfooter4.Visible = true;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");

                    //HDNoNasabah.Value = no_nasabah;
                    txtnonasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                    //txtpath.Text = ge.ListDataset.Tables[0].Rows[0]["PathFile"].ToString();
                    //txtfilename.Text = ge.ListDataset.Tables[0].Rows[0]["filename"].ToString();


                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();

                    txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();

                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBayar")).Text;



                    ddlKoperasi.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["koperasi"].ToString();

                    ddlpajak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString();

                    //path
                    txtpathktpwaris1.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris1")).Value;
                    txtpathktpwaris2.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris2")).Value;


                    txtnamawaris1.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris1"].ToString();
                    txtktpwaris1.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris1"].ToString();
                    txtalamatwaris1.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris1"].ToString();

                    txtnamawaris2.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris2"].ToString();
                    txtktpwaris2.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris2"].ToString();
                    txtalamatwaris2.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris2"].ToString();

                    var cabang= ge.ListDataset.Tables[0].Rows[0]["kode_cabang"].ToString();
                    var jangkabulan= ge.ListDataset.Tables[0].Rows[0]["jangkabulan"].ToString();

                    var norekening = ge.ListDataset.Tables[0].Rows[0]["norekening"].ToString();

                    float bunga = float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString());

                    var Koperasi = ddlKoperasi.SelectedValue;
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                    varx[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                    varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                    varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", varx, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                    ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlcabang.DataTextField = "nama_cabang";
                    ddlcabang.DataValueField = "kode_cabang";
                    ddlcabang.DataBind();
                    ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddlcabang.SelectedValue = cabang;

                    btndelete.Visible = false;

                    DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[5];
                    vart[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    vart[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
                    vart[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    vart[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    vart[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", vart, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah ada transaksi!');", true);
                        btnSimpan.Visible = false;
                    }
                    else
                    {
                        btnSimpan.Visible = true;
                    }
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
                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;
                    Pnlfooter4.Visible = true;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");

                    //HDNoNasabah.Value = no_nasabah;
                    txtnonasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();


                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBayar")).Text;
                    txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;


                    //path
                    txtpathktpwaris1.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris1")).Value;
                    txtpathktpwaris2.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris2")).Value;


                    ddlKoperasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["koperasi"].ToString();

                    ddlpajak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString();


                    txtnamawaris1.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris1"].ToString();
                    txtktpwaris1.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris1"].ToString();
                    txtalamatwaris1.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris1"].ToString();

                    txtnamawaris2.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris2"].ToString();
                    txtktpwaris2.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris2"].ToString();
                    txtalamatwaris2.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris2"].ToString();

                    var cabang = ge.ListDataset.Tables[0].Rows[0]["kode_cabang"].ToString();
                    var jangkabulan = ge.ListDataset.Tables[0].Rows[0]["jangkabulan"].ToString();

                    var norekening = ge.ListDataset.Tables[0].Rows[0]["norekening"].ToString();

                    float bunga = float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString());


                    var Koperasi = ddlKoperasi.SelectedValue;
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                    varx[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                    varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                    varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", varx, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                    ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlcabang.DataTextField = "nama_cabang";
                    ddlcabang.DataValueField = "kode_cabang";
                    ddlcabang.DataBind();
                    ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddlcabang.SelectedValue = cabang;

                    btnSimpan.Visible = false;


                    DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[5];
                    vart[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    vart[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
                    vart[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    vart[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    vart[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", vart, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah ada transaksi!');", true);
                        btndelete.Visible = false;
                    }
                    else
                    {
                        btndelete.Visible = true;
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
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            //string txtpath = txtpathktp.Text;
            string State = "";

            if (txtnobukti.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('File Upload Masih Kosong!');", true);
            }
            else
            {
                if (txtpathktpwaris1.Text != "")
                {
                    string path = Server.MapPath(txtpathktpwaris1.Text);

                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not.
                    {
                        file.Delete();
                    }
                }

                if (txtpathktpwaris2.Text != "")
                {
                    string path = Server.MapPath(txtpathktpwaris2.Text);

                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not.
                    {
                        file.Delete();
                    }
                }
                State = "DELETE";

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
                Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                Params[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, State, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);
                }

                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','Delete Permohonan Done','top-right','success','1');", true);
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
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok+= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pokok"));
            bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bunga"));
            angsuran += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 0)
            {
                

            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 1)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 2)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            }
            
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalPokok = (Label)e.Row.FindControl("lbltotalPokok");
            lbltotalPokok.Text = string.Format("{0:N0}", pokok);

            Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
            lbltotalBunga.Text = string.Format("{0:N0}", bunga);

            Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
            lbltotalAngsuran.Text = string.Format("{0:N0}", angsuran);

            Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
            lbltotaldenda.Text = string.Format("{0:N0}", denda);



        }
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            string State = "";
            int filex = 0;
            string filename = "";
            string fotopath;

            State = "UPDATEAHLIWARIS";
            if(fileuploadktpwaris1.HasFile)
            {
                string namafile;
                //string fotopath;
                string folderPath = Server.MapPath("~/Files/ahliwaris/");

                DateTime now = DateTime.Now;

                namafile = txtnonasabah.Text  + now.ToString("ddMMyyyyhhmmss") + "_" + "Waris1";

                string ambilnama = fileuploadktpwaris1.FileName;

                String FileExtension = Path.GetExtension(ambilnama.ToString()).ToLower();

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }

                fileuploadktpwaris1.SaveAs(folderPath + namafile + FileExtension);
                txtpathktpwaris1.Text = namafile + FileExtension;

                fotopath = folderPath + namafile + FileExtension;
            }
            else
            {
                txtpathktpwaris1.Text = "";
            }

            if (fileuploadktpwaris2.HasFile)
            {
                string namafile;
                //string fotopath;
                string folderPath = Server.MapPath("~/Files/ahliwaris/");
                DateTime now = DateTime.Now;

                namafile = txtnonasabah.Text + now.ToString("ddMMyyyyhhmmss") + "_" + "Waris2";
                string ambilnama = fileuploadktpwaris2.FileName;

                String FileExtension = Path.GetExtension(ambilnama.ToString()).ToLower();

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }

                fileuploadktpwaris2.SaveAs(folderPath + namafile + FileExtension);
                txtpathktpwaris2.Text = namafile + FileExtension;

                fotopath = folderPath + namafile + FileExtension;
            }
            else
            {
                txtpathktpwaris2.Text = "";
            }

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[26];
            Params[0]  = gb.CreateSPParameter("@Idpermohonan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
            Params[1]  = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[2]  = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
            Params[3]  = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnonasabah.Text, typeof(string));
            Params[4]  = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamat.Text, typeof(string));
            Params[5]  = gb.CreateSPParameter("@NamaAhliWaris1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamawaris1.Text, typeof(string));
            Params[6]  = gb.CreateSPParameter("@NoKTPAhliWaris1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtktpwaris1.Text, typeof(string));
            Params[7]  = gb.CreateSPParameter("@AlamatAhliWaris1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamatwaris1.Text, typeof(string));
            Params[8]  = gb.CreateSPParameter("@NamaAhliWaris2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamawaris2.Text, typeof(string));
            Params[9]  = gb.CreateSPParameter("@NoKTPAhliWaris2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtktpwaris2.Text, typeof(string));
            Params[10] = gb.CreateSPParameter("@AlamatAhliWaris2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamatwaris2.Text, typeof(string));
            Params[11] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[12] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[13] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
            Params[14] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEWARIS", typeof(string));
            Params[15] = gb.CreateSPParameter("@pathktpWaris1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpathktpwaris1.Text, typeof(string));
            Params[16] = gb.CreateSPParameter("@pathktpWaris2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpathktpwaris2.Text, typeof(string));

            Params[17] = gb.CreateSPParameter("@kabupaten", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkabupaten.Text, typeof(string));
            Params[18] = gb.CreateSPParameter("@RT", ParameterDirection.Input, SqlDbType.VarChar, 5, txtrtktp.Text, typeof(string));
            Params[19] = gb.CreateSPParameter("@RW", ParameterDirection.Input, SqlDbType.VarChar, 5, txtrwktp.Text, typeof(string));
            Params[20] = gb.CreateSPParameter("@kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkelurahanktp.Text, typeof(string));
            Params[21] = gb.CreateSPParameter("@kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkecamatanktp.Text, typeof(string));
            Params[22] = gb.CreateSPParameter("@provinsi", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlprovinsiktp.SelectedValue, typeof(string));
            Params[23] = gb.CreateSPParameter("@kodepos", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodeposktp.Text, typeof(string));
            Params[24] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidentitas.Text, typeof(string));
            Params[25] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", Params, "constrmf");
            //ge.ListDataTable = gb.GetDataJustOutPut("SN_Permohonan", Params, "constrmf").Tables[0];
            //ge.ListDataTable = gb.GetDataJustOutPut("SN_Permohonan", Params, "constrmf").Tables[1];

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Data Permohonan','top-right','success','1');", true);
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


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void UploadFileKTP(object sender, EventArgs e)
    {

        string namafileKTP;
        string fotopathktp;
        string folderPath = Server.MapPath("~/Files/MENINGGAL/");
        DateTime now = DateTime.Now;

        namafileKTP = HDNoRekening.Value + "_" + now.ToString("ddMMyyyy") + "_" + "DEAD";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }
    }


    public void btnSerach_Click(object sender, EventArgs e)
    {
        var kodelgn = hdncustomer.Value;
        var idpemohon = hdnidpermohonan.Value;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIAHLIWARIS", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@idpermohonan", ParameterDirection.Input, SqlDbType.VarChar, 20, idpemohon, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
        //if (ge.ListDataset.Tables[0].Rows.Count > 0 && ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris1"].ToString() == "-")
        //{
            txtnonasabah.Text = kodelgn;
            txtnobukti.Text = idpemohon;
            txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
            txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
            txtnamawaris11.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris1"].ToString();
            txtktpwaris11.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris1"].ToString();
            txtalamatwaris11.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris1"].ToString();
            txtpathktpwaris11.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktp1"].ToString();

            txtnamawaris22.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris2"].ToString();
            txtktpwaris22.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris2"].ToString();
            txtalamatwaris22.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris2"].ToString();
            txtpathktpwaris22.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktp2"].ToString();

            btnSimpan.Visible = true;
        //}
        //else
        //{
            txtnonasabah.Text = kodelgn;
            txtnobukti.Text = idpemohon;
            txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
            txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
        txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
        txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["rt"].ToString();
        txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["rw"].ToString();
        txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
        txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
        ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
        txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
        txtnamawaris1.Text = "-";
            txtktpwaris1.Text = "0";
            txtalamatwaris1.Text = "-";
            txtpathktpwaris1.Text = "-";

            txtnamawaris2.Text = "-";
            txtktpwaris2.Text = "0";
            txtalamatwaris2.Text = "-";
            txtpathktpwaris2.Text = "-";

        txtnamawaris11.Enabled = false;
        txtktpwaris11.Enabled = false;
        txtalamatwaris11.Enabled = false;
        txtpathktpwaris11.Enabled = false;

        txtnamawaris22.Enabled = false;
        txtktpwaris22.Enabled = false;
        txtalamatwaris22.Enabled = false;
        txtpathktpwaris22.Enabled = false;
        //}
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
  
 
    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
  

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SN_getCustomer_Ahliwaris", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
    #region"Dimas"

    protected void UploadFileKTPwaris1(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/Files/SimpananBerjangka/");
        DateTime now = DateTime.Now;
        string namafile = "", fotopath;

        namafile = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AhliWaris1";
        //File cekfile1 = new FileInfo(folderPath + )
        //Check whether Directory (Folder) exists.
        //////FileInfo file = new FileInfo(folderPath + namafile + ".jpg");
        //////if (file.Exists)
        //////{
        //////    file.Delete();
        //////}
        //////if (!Directory.Exists(folderPath))
        //////{
        //////    //If Directory (Folder) does not exists. Create it.
        //////    Directory.CreateDirectory(folderPath);
        //////}

        //////fileuploadktpwaris1.SaveAs(folderPath + namafile + ".jpg");
        //////txtpathktpwaris1.Text = namafile + ".jpg";

        //fotopath = folderPath + namafile + ".jpg";
        //txtpathktp2.Text = fotopath;
        //Boolean FileOK = false;

        //Boolean FileSaved = false;
        //string tipefile = "";
        //string cekfile = "";
        //string pathfile = "";

        //if (fileuploadktpwaris1.HasFile)
        //{
        //    Session["WorkingImage"] = fileuploadktpwaris1.FileName;

        //    String FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

        //    String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".png" };

        //    for (int i = 0; i < allowedExtensions.Length; i++)
        //    {
        //        if (FileExtension == allowedExtensions[i])
        //        {
        //            FileOK = true;
        //            tipefile = allowedExtensions[i].ToString();
        //            cekfile = "masuk";
        //        }
        //    }
        //}
        //if (FileOK)
        //{
        //    try
        //    {
        //        //Check whether Directory (Folder) exists.
        //        if (!Directory.Exists(folderPath))
        //        {
        //            //If Directory (Folder) does not exists. Create it.
        //            Directory.CreateDirectory(folderPath);
        //        }
        //        //fileuploadktpwaris1.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);

        //        FileInfo file = new FileInfo(folderPath + Session["WorkingImage"]);
        //        //string pathori = "~/Files/SimpananBerjangka/" + Session["WorkingImage"];
        //        //string pathori1 = folderPath + Session["WorkingImage"];
        //        if (file.Exists)
        //        {
        //            file.Delete();
        //            //var stream1 = new FileStream(pathori1, (FileMode)FileAccess.Read);
        //            //var reader1 = new StreamReader(stream1);
        //            //stream1.Close();
        //            //reader1.Close();
        //        }

        //        //var stream = new FileStream(pathori, (FileMode)FileAccess.Read);
        //        //var reader = new StreamReader(stream);
        //        //File.Delete(pathori); 

        //        fileuploadktpwaris1.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);
        //        System.Drawing.Image img = System.Drawing.Image.FromFile(string.Concat(folderPath, Session["WorkingImage"]));
        //        img = resizeImage(img);
        //        img.Save(string.Concat(folderPath, txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"]));
        //        img.Dispose();
        //        //var stream = new FileStream(folderPath + Session["WorkingImage"], (FileMode)FileAccess.ReadWrite);
        //        //var reader = new StreamReader(stream);
        //        fileuploadktpwaris1.PostedFile.InputStream.Dispose();
        //        if (file.Exists)
        //        {
        //            file.Delete();
        //        }
        //        //imgCroppedwaris1.ImageUrl = folderPath + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
        //        pathfile = "~/Files/SimpananBerjangka/" + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
        //        imgCroppedwaris1.Dispose();
        //        img.Dispose();
        //        //StreamReader.Close();

        //        //FileInfo file = new FileInfo(folderPath + Session["WorkingImage"]);
        //        //if (file.Exists)
        //        //{
        //        //    File.Delete(folderPath + Session["WorkingImage"]);
        //        //}
        //        //string folderPathapus = Server.MapPath("~\\Files\\SimpananBerjangka\\" + Session["WorkingImage"]);
        //        //File.Delete(folderPathapus);
        //        //img.de
        //        //File.Delete(fileuploadktpwaris1.FileBytes);

        //        fileuploadktpwaris1.Dispose();
        //        //if (File.Exists(pathori1))
        //        //{
        //        //    File.Delete(pathori1);
        //        //}
        //        FileSaved = true;
        //    }
        //    catch (System.Exception X)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //        FileSaved = false;
        //    }
        //}
        //else if (fileuploadktpwaris1.FileName == "" && cekfile == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih File Terlebih dahulu.');", true);
        //}
        //else if (fileuploadktpwaris1.FileName != "" && cekfile == "masuk")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis File harus image');", true);
        //}
        //if (FileSaved)
        //{
        //    txtpathktpwaris1.Text = pathfile;
        //    imgCroppedwaris1.ImageUrl = pathfile;
        //    //string path1 = Server.MapPath("~/Files/SimpananBerjangka/" + Session["WorkingImage"]);
        //    //FileAccess file1 = new FileAccess();
        //    ////file1.Delete();
        //    ////Bitmap.Dispose();
        //    ////pictureBox1.Image?.Dispose();

        //    //File.file1.
        //    //File.Delete(folderPath + Session["WorkingImage"].ToString());
        //    //fileuploadktpwaris1.(folderPath + Session["WorkingImage"]);
        //}

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
    private System.Drawing.Image resizeImage(System.Drawing.Image img)
    {
        Bitmap b = new Bitmap(img);
        System.Drawing.Image i = resizeImage(b, new Size(60, 60));
        b.Dispose();
        return i;
    }
    protected void UploadFileKTPwaris2(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/Files/SimpananBerjangka/");
        DateTime now = DateTime.Now;
        string namafileKTP = "", fotopathktp;

        namafileKTP = "tes" + "_" + now.ToString("ddMMyyyy") + "_" + "KTP";

        Boolean FileOK = false;

        Boolean FileSaved = false;
        string tipefile = "";
        string cekfile = "";
        string pathfile = "";

        if (fileuploadktpwaris2.HasFile)
        {
            Session["WorkingImage"] = fileuploadktpwaris2.FileName;

            String FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

            String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".png" };

            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (FileExtension == allowedExtensions[i])
                {
                    FileOK = true;
                    tipefile = allowedExtensions[i].ToString();
                    cekfile = "masuk";
                }
            }
        }
        if (FileOK)
        {
            try
            {
                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }
                fileuploadktpwaris2.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);
                System.Drawing.Image img = System.Drawing.Image.FromFile(string.Concat(folderPath, Session["WorkingImage"]));
                img = resizeImage(img);
                img.Save(string.Concat(folderPath, txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"]));
                //imgCroppedwaris2.ImageUrl = folderPath + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
                pathfile = "~/Files/SimpananBerjangka/" + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
                FileSaved = true;
            }
            catch (System.Exception X)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                FileSaved = false;
            }
        }
        else if (fileuploadktpwaris2.FileName == "" && cekfile == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih File Terlebih dahulu.');", true);
        }
        else if (fileuploadktpwaris2.FileName != "" && cekfile == "masuk")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis File harus image');", true);
        }
        if (FileSaved)
        {
            txtpathktpwaris2.Text = pathfile;
            imgCroppedwaris2.ImageUrl = pathfile;
        }

    }
    protected void clearFileInputField1(object sender, EventArgs e)
    {
        try
        {
                //if (txtpathktpwaris1.Text != "")
                //{
                //    string path1 = Server.MapPath(txtpathktpwaris1.Text);
                //    FileInfo file1 = new FileInfo(path1);
                //    if (file1.Exists)
                //    {
                //        file1.Delete();
                        txtpathktpwaris1.Text = "";
            //        }
            //    }
            //}
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void clearFileInputField2(object sender, EventArgs e)
    {
        try
        {

            //if (fileuploadktpwaris2.HasFile)
            //{

            //    if (txtpathktpwaris2.Text != "")
            //    {
            //        string path2 = Server.MapPath(txtpathktpwaris2.Text);
            //        FileInfo file2 = new FileInfo(path2);
            //        if (file2.Exists)
            //        {
            //            file2.Delete();
                        txtpathktpwaris2.Text = "";
            //        }
            //    }
            //}
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion"Dimas"
}
