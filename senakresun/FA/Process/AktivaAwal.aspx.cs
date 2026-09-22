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
using DocumentFormat.OpenXml.Bibliography;


public partial class FA_Process_AktivaAwal : System.Web.UI.Page
{
    public double masa = 0;
    double nilai_residu = 0;
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    public string fotopath = "";
    public int counter = 0, counternambah = 0;
    string STATE = "", kodeaktiva = "", kodegol = "", kodekelompok = "", TypeTransaksi = "";

    DataTable dtRP = new DataTable("Transaction");
    DataTable dtRPDetil = new DataTable("Detil");
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }
    private string SessIsSuperAdmin
    {
        get { return Session["SessIsSuperAdmin"].ToString(); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (!Page.IsPostBack)
            {

                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                txtketerangan.Attributes.Add("maxlength", "150");


                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltransaksi.Text = xtanggal.ToString("dd-MM-yyyy");

                //String tahun = DateTime.Now.ToString("yyyy");
                //string bulan = DateTime.Now.ToString("MM");
                //int hari = DateTime.DaysInMonth(int.Parse(tahun), int.Parse(bulan));


                txttgltransaksi.Text = xtanggal.ToString("dd-MM-yyyy");
                txtmulaisusut.Text = xtanggal.ToString("dd-MM-yyyy");
                txttanggalentry.Text = xtanggal.ToString("dd-MM-yyyy");
                txttglbeliakhir.Text = xtanggal.ToString("dd-MM-yyyy");
                txttglpertama.Text = xtanggal.ToString("dd-MM-yyyy");
                txttglsusut.Text = xtanggal.ToString("dd-MM-yyyy");
                txtentry.Text = SessLoginID;


                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                var1[0] = gb.CreateSPParameter("@KodeLokasi", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYKODENOTKNL", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[5] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.FA_PembuatanAktiva", var1, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAktiva asc";
                ddlkdaktiva.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkdaktiva.DataTextField = "aktiva";
                ddlkdaktiva.DataValueField = "KodeAktiva";
                ddlkdaktiva.DataBind();
                ddlkdaktiva.Items.Insert(0, new ListItem("Pilih Data", ""));


                DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[4];
                varx1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYKATIVA", typeof(string));
                varx1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varx1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("FA_LOKASI", varx1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                ddllokasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddllokasi.DataTextField = "Text";
                ddllokasi.DataValueField = "value";
                ddllokasi.DataBind();
                ddllokasi.Items.Insert(0, new ListItem("Pilih Data", ""));


                DefaultClass.Global.SqlHelper[] varx2 = new DefaultClass.Global.SqlHelper[3];
                varx2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGOLONGAN", typeof(string));
                varx2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("FA_JENISGOL", varx2, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                ddlgolongan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlgolongan.DataTextField = "Text";
                ddlgolongan.DataValueField = "value";
                ddlgolongan.DataBind();
                ddlgolongan.Items.Insert(0, new ListItem("Pilih Data", ""));

                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                var4[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("FA_Group", var4, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_group asc";
                ddlgroupaktiva.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlgroupaktiva.DataTextField = "groups";
                ddlgroupaktiva.DataValueField = "kode_group";
                ddlgroupaktiva.DataBind();
                ddlgroupaktiva.Items.Insert(0, new ListItem("Pilih Data", ""));


                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                var2[0] = gb.CreateSPParameter("@KodeType", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkdaktiva.SelectedValue, typeof(string));
                var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var2[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTRX", typeof(string));
                var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                ge.ListDataset = gb.GetDataJustOutPut("FA_TypeTransaksi", var2, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn asc";


                ddltype.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddltype.DataTextField = "Tipe";
                ddltype.DataValueField = "TypeTrn";
                ddltype.DataBind();
                ddltype.Items.Insert(0, new ListItem("Pilih Data", "-"));

                if ((ddltype.Items[1].Value == "A") && (ddltype.Items[2].Value == "C"))
                {
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                    //ddltype.Items.RemoveAt(7);
                }
                else if ((ddltype.Items[1].Value == "B") && (ddltype.Items[2].Value == "C"))
                {
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                    ddltype.Items.RemoveAt(1);
                }
                else if ((ddltype.Items[1].Value == "A") && (ddltype.Items[2].Value == "B"))
                {
                    ddltype.Items.RemoveAt(3);
                    ddltype.Items.RemoveAt(3);
                    ddltype.Items.RemoveAt(3);
                    ddltype.Items.RemoveAt(3);
                    ddltype.Items.RemoveAt(3);
                    //ddltype.Items.RemoveAt(3);
                }

                txttype.Text = "";

                BindGridEntity();

            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
   
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddltype.SelectedValue == "-")
            {
               
                pnlHeader.Visible = false;
                PnlFront.Visible = false;
                pnladd.Visible = true;

                Pnlfooter.Visible = true;
                txttype.Text = "";
                txttgltransaksi.Text = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                lbluniversal.Visible = false;
                lblmulaisusut.Visible = false;
                txtmulaisusut.Visible = false;
                lblumur.Visible = false;
                txtumur.Visible = false;
                txtbeli.Visible = false;
                lbltanggalbeli.Visible = false;
                txttglpertama.Visible = false;
                lbltanggalbeliakhir.Visible = false;
                txttglbeliakhir.Visible = false;
                lblhargapembelian.Visible = false;
                txthargapembelian.Visible = false;
                lblakumulasi.Visible = false;
                txtakumulasisusut.Visible = false;
                lblpenambahanharga.Visible = false;
                txtpenambahanharga.Visible = false;
                lblpenguranganharga.Visible = false;
                txtpenguranganharga.Visible = false;

                lbltglakhirsusut.Visible = false;
                txtakhirsusut.Visible = false;

                txtharga.Visible = false;
                lblnilai.Visible = false;
                txtResidu.Visible = false;
                lblketerangan.Visible = false;
                txtketerangan.Visible = false;
                txtmasa.Visible = false;
                lblmasamanfaat.Visible = false;
                txtbeli.Visible = false;
                lblhargaperolehan.Visible = false;
                lbltglsusut.Visible = false;
                txttglsusut.Visible = false;
            }
            else
            {
                double masa = 0;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@KodeType", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltype.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("FA_TypeTransaksi", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn asc";

                ddltype.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                txttype.Text = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();

                txtumur.Text = hdmtahun.Value;

                //txtmasa.Enabled = true;

                if (ddltype.SelectedValue == "A")
                {
                    lbluniversal.Text = "Dibeli/Diterima Dari:";
                    lbluniversal.Visible = true;
                    lblmulaisusut.Visible = true;
                    txtmulaisusut.Visible = true;
                    lblumur.Visible = true;
                    txtumur.Visible = true;
                    txtbeli.Visible = true;
                    lbltanggalbeli.Visible = true;
                    txttglpertama.Visible = true;
                    lbltanggalbeliakhir.Visible = true;
                    txttglbeliakhir.Visible = true;
                    lblhargapembelian.Visible = true;
                    txthargapembelian.Visible = true;
                    lblakumulasi.Visible = true;
                    txtakumulasisusut.Visible = true;
                    lblpenambahanharga.Visible = true;
                    txtpenambahanharga.Visible = true;
                    lblpenguranganharga.Visible = true;
                    txtpenguranganharga.Visible = true;
                    lblnilaisusut.Visible = true;
                    txtnilaisusut.Visible = true;

                    lbltglakhirsusut.Visible = true;
                    txtakhirsusut.Visible = true;
                    txttglbeliakhir.Visible = false;
                    lbltanggalbeliakhir.Visible = false;
                    lblmasamanfaat.Visible = true;
                    txtmasa.Visible = true;
                    lblhargaperolehan.Visible = true;
                    lblhargaperolehan.Text = "Harga Perolehan";
                    txtharga.Visible = true;
                    lblnilai.Visible = true;
                    txtResidu.Visible = true;
                    lblketerangan.Visible = true;
                    txtketerangan.Visible = true;

                    txtakhirsusut.Enabled = false;
                    lblhargapembelian.Visible = false;
                    lblpenambahanharga.Visible = false;
                    txtpenambahanharga.Visible = false;
                    lblpenguranganharga.Visible = false;
                    txtpenguranganharga.Visible = false;
                    txthargapembelian.Visible = false;

                    txttglsusut.Visible = true;
                    lbltglsusut.Visible = true;
                    txtmasa.Text = hdmsusut.Value;

                    DateTime ct = Convert.ToDateTime(txtmulaisusut.Text);
                    ct = ct.AddMonths(int.Parse(txtmasa.Text));

                    txtakhirsusut.Text = ct.ToString("dd-MM-yyyy");

                    //if (txtumur.Text == "0")
                    //{
                    //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                    //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                    //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                    //    int tahun = int.Parse(year);
                    //    //int bulan = (int.Parse(month) + int.Parse(txtmasa.Text)) - 1;

                    //    //int day = DateTime.DaysInMonth(tahun, (bulan - 1));


                    //    //txtakhirsusut.Text = tahun + "-" + (bulan) + "-" + day;
                    //    var rubahbdate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(hari));
                    //    string hitungbulang = rubahbdate.AddMonths(Convert.ToInt32(txtmasa.Text) - 1).ToString("d");

                    //    txtakhirsusut.Text = Convert.ToString(Convert.ToDateTime(hitungbulang).ToString("dd/MM/yyyy"));
                    //    DateTime  ctx= Convert.ToDateTime(txtmulaisusut.Text);
                    //    ctx = ctx.AddMonths(int.Parse(txtmasa.Text));
                    //}
                    //else
                    //{
                    //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                    //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                    //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                    //    int tahun = int.Parse(year) + int.Parse(txtumur.Text);

                    //    int day = DateTime.DaysInMonth(tahun, (int.Parse(month) - 1));

                    //    txtakhirsusut.Text = tahun + "-" + (int.Parse(month) - 1) + "-" + day;

                    //    masa = double.Parse(hdmtahun.Value) * 12;
                    //    txtmasa.Text = masa.ToString();

                    //}


                }
                else if (ddltype.SelectedValue == "B")
                {
                    lbluniversal.Text = "Dibeli/Diterima Dari:";
                    lbluniversal.Visible = true;
                    txtbeli.Visible = true;
                    lbluniversal.Visible = true;
                    lblmulaisusut.Visible = true;
                    txtmulaisusut.Visible = true;
                    lblumur.Visible = true;
                    txtumur.Visible = true;
                    txtbeli.Visible = true;
                    lbltanggalbeli.Visible = false;
                    txttglpertama.Visible = false;
                    lbltanggalbeliakhir.Visible = false;
                    txttglbeliakhir.Visible = false;
                    lblhargapembelian.Visible = false;
                    txthargapembelian.Visible = false;
                    lblakumulasi.Visible = false;
                    txtakumulasisusut.Visible = false;
                    lblpenambahanharga.Visible = false;
                    txtpenambahanharga.Visible = false;
                    lblpenguranganharga.Visible = false;
                    txtpenguranganharga.Visible = false;

                    lbltglakhirsusut.Visible = true;
                    txtakhirsusut.Visible = true;
                    lblmasamanfaat.Visible = true;
                    txtmasa.Visible = true;
                    lblhargaperolehan.Visible = true;
                    txtharga.Visible = true;
                    lblhargaperolehan.Text = "Harga Perolehan";
                    lblnilai.Visible = true;
                    txtResidu.Visible = true;
                    lblketerangan.Visible = true;
                    txtketerangan.Visible = true;
                    lbltanggalbeli.Visible = true;
                    txttglpertama.Visible = true;
                    txttglsusut.Visible = false;

                    txtakhirsusut.Enabled = false;
                    txtmasa.Enabled = false;

                    txtmasa.Text = hdmsusut.Value;
                    DateTime ct = Convert.ToDateTime(txtmulaisusut.Text);
                    ct = ct.AddMonths(int.Parse(txtmasa.Text));

                    txtakhirsusut.Text = ct.ToString("dd-MM-yyyy");

                    //if (txtumur.Text == "0")
                    //{
                    //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                    //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                    //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                    //    int tahun = int.Parse(year);
                    //    //int bulan = (int.Parse(month) + int.Parse(txtmasa.Text)) - 1;

                    //    //int day = DateTime.DaysInMonth(tahun, (bulan - 1));
                    //    var rubahbdate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(hari));
                    //    string hitungbulang = rubahbdate.AddMonths(Convert.ToInt32( txtmasa.Text)-1).ToString("d");

                    //    //txtakhirsusut.Text = tahun + "-" + (bulan) + "-" + day;

                    //    txtakhirsusut.Text = Convert.ToString(Convert.ToDateTime(hitungbulang).ToString("dd/MM/yyyy"));
                    //}
                    //else
                    //{
                    //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                    //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                    //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                    //    int tahun = int.Parse(year) + int.Parse(txtumur.Text);

                    //    int day = DateTime.DaysInMonth(tahun, (int.Parse(month) - 1));

                    //    txtakhirsusut.Text = tahun + "-" + (int.Parse(month) - 1) + "-" + day;

                    //    masa = double.Parse(hdmtahun.Value) * 12;
                    //    txtmasa.Text = masa.ToString();

                    //}
               
                
                }
                else if (ddltype.SelectedValue == "K")
                {
                    lbluniversal.Text = "Dijual Kepada:";
                    lbluniversal.Visible = true;
                    txtbeli.Visible = true;
                    txtbeli.Visible = true;
                    lbltanggalbeli.Visible = false;
                    txttglpertama.Visible = false;
                    lbltanggalbeliakhir.Visible = false;
                    txttglbeliakhir.Visible = false;
                    lblhargapembelian.Visible = false;
                    txthargapembelian.Visible = false;
                    lblakumulasi.Visible = false;
                    txtakumulasisusut.Visible = false;
                    lblpenambahanharga.Visible = false;
                    txtpenambahanharga.Visible = false;
                    lblpenguranganharga.Visible = false;
                    txtpenguranganharga.Visible = false;

                    lbltglakhirsusut.Visible = false;
                    txtakhirsusut.Visible = false;
                    lblmasamanfaat.Visible = false;
                    txtmasa.Visible = false;
                    lblhargaperolehan.Visible = true;
                    txtharga.Visible = true;
                    lblhargaperolehan.Text = "Harga Penjualan";
                    lblnilai.Visible = false;
                    txtResidu.Visible = false;
                    lblketerangan.Visible = true;
                    txtketerangan.Visible = true;
                    lblumur.Visible = false;
                    txtumur.Visible = false;
                    lblmulaisusut.Visible = false;
                    txtmulaisusut.Visible = false;

                    txtakhirsusut.Enabled = false;
                    txtmasa.Enabled = false;
                }
                else if (ddltype.SelectedValue == "C")
                {
                    lbluniversal.Text = "Dibeli/Diterima Dari:";
                    lbluniversal.Visible = true;
                    txtbeli.Visible = true;
                    lblmulaisusut.Visible = true;
                    txtmulaisusut.Visible = true;
                    txtbeli.Visible = true;
                    lbltanggalbeli.Visible = false;
                    txttglpertama.Visible = false;
                    lbltanggalbeliakhir.Visible = false;
                    txttglbeliakhir.Visible = false;
                    lblhargapembelian.Visible = false;
                    txthargapembelian.Visible = false;
                    lblakumulasi.Visible = false;
                    txtakumulasisusut.Visible = false;
                    lblpenambahanharga.Visible = false;
                    txtpenambahanharga.Visible = false;
                    lblpenguranganharga.Visible = false;
                    txtpenguranganharga.Visible = false;

                    lbltglakhirsusut.Visible = true;
                    txtakhirsusut.Visible = true;
                    lblmasamanfaat.Visible = true;
                    txtmasa.Visible = true;
                    lblhargaperolehan.Visible = true;
                    txtharga.Visible = true;
                    lblhargaperolehan.Text = "Harga Perolehan";
                    lblnilai.Visible = false;
                    txtResidu.Visible = false;
                    lblketerangan.Visible = true;
                    txtketerangan.Visible = true;
                    lblumur.Visible = false;
                    txtumur.Visible = false;
                    lblmulaisusut.Visible = false;
                    txtmulaisusut.Visible = false;

                    txtakhirsusut.Enabled = false;
                    txtmasa.Enabled = false;
                }
                else if (ddltype.SelectedValue == "D")
                {
                    lbluniversal.Text = "Dibeli/Diterima Dari:";
                    lbluniversal.Visible = true;
                    txtbeli.Visible = true;
                    lblmulaisusut.Visible = true;
                    txtmulaisusut.Visible = true;
                    lbltanggalbeli.Visible = false;
                    txttglpertama.Visible = false;
                    lbltanggalbeliakhir.Visible = false;
                    txttglbeliakhir.Visible = false;
                    lblhargapembelian.Visible = false;
                    txthargapembelian.Visible = false;
                    lblakumulasi.Visible = false;
                    txtakumulasisusut.Visible = false;
                    lblpenambahanharga.Visible = false;
                    txtpenambahanharga.Visible = false;
                    lblpenguranganharga.Visible = false;
                    txtpenguranganharga.Visible = false;
                    lblumur.Visible = false;
                    txtumur.Visible = false;
                    lblmulaisusut.Visible = false;
                    txtmulaisusut.Visible = false;


                    lbltglakhirsusut.Visible = true;
                    txtakhirsusut.Visible = true;
                    lblmasamanfaat.Visible = true;
                    txtmasa.Visible = true;
                    lblhargaperolehan.Visible = true;
                    txtharga.Visible = true;
                    lblhargaperolehan.Text = "Harga Perolehan";
                    lblnilai.Visible = false;
                    txtResidu.Visible = false;
                    lblketerangan.Visible = true;
                    txtketerangan.Visible = true;

                    txtakhirsusut.Enabled = false;
                    txtmasa.Enabled = false;
                }

                else if (ddltype.SelectedValue == "L")
                {
                    lbluniversal.Text = "Didisposisi Kepada:";
                    lbluniversal.Visible = true;
                    txtbeli.Visible = true;
                    txtbeli.Visible = true;
                    lbltanggalbeli.Visible = false;
                    txttglpertama.Visible = false;
                    lbltanggalbeliakhir.Visible = false;
                    txttglbeliakhir.Visible = false;
                    lblhargapembelian.Visible = false;
                    txthargapembelian.Visible = false;
                    lblakumulasi.Visible = false;
                    txtakumulasisusut.Visible = false;
                    lblpenambahanharga.Visible = false;
                    txtpenambahanharga.Visible = false;
                    lblpenguranganharga.Visible = false;
                    txtpenguranganharga.Visible = false;

                    lbltglakhirsusut.Visible = false;
                    txtakhirsusut.Visible = false;
                    lblmasamanfaat.Visible = false;
                    txtmasa.Visible = false;
                    lblhargaperolehan.Visible = true;
                    txtharga.Visible = true;
                    lblhargaperolehan.Text = "Harga Disposisi";
                    lblnilai.Visible = false;
                    txtResidu.Visible = false;
                    lblketerangan.Visible = true;
                    txtketerangan.Visible = true;
                    lblumur.Visible = false;
                    txtumur.Visible = false;
                    lblmulaisusut.Visible = false;
                    txtmulaisusut.Visible = false;

                    txtakhirsusut.Enabled = false;
                    txtmasa.Enabled = false;
                }

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtumur_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtmulaisusut.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal mulai susut harus di isi');", true);

            }
            else
            {
                //txtmasa.Text = masamanfaat.ToString();


                string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                int tahun = int.Parse(year);
                int bulan = (int.Parse(month) + int.Parse(txtmasa.Text)) - 1;


                int day = DateTime.DaysInMonth(tahun, bulan);

                txtakhirsusut.Text = tahun + "-" + bulan + "-" + day;


            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void txtmasa_TextChanged(object sender, EventArgs e)
    {
        try { 
        if (txtmasa.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masa Manfaat harus di isi');", true);

        }
        else
        {
            if (txtumur.Text == "0")
            {
                masa = int.Parse(txtmasa.Text);
                if (masa <= 11)
                {

                    //DateTime date = new DateTime();

                    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                    //int tahun = int.Parse(year);
                    //int bulan = (int.Parse(month) + int.Parse(txtmasa.Text)) - 1;

                    //date = date.AddMonths(bulan);

                    //int day = DateTime.DaysInMonth(tahun, date);

                    //txtakhirsusut.Text = tahun + "-" + date + "-" + day;

                    DateTime now = new DateTime(int.Parse(year), int.Parse(month), int.Parse(hari));

                    DateTime akhirmasasusut = now.AddMonths(int.Parse(txtmasa.Text) - 1);


                    txtakhirsusut.Text = akhirmasasusut.ToShortDateString();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masa Manfaat tidak boleh lebih dari 11 bulan');", true);

                }


            }
            else
            {
                string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                int tahun = int.Parse(year) + int.Parse(txtumur.Text);

                int day = DateTime.DaysInMonth(tahun, (int.Parse(month) - 1));

                txtakhirsusut.Text = tahun + "-" + (int.Parse(month) - 1) + "-" + day;

                masa = double.Parse(hdmtahun.Value) * 12;
                txtmasa.Text = masa.ToString();

            }
        }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    protected void ddlkdaktiva_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkdaktiva.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[5] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("FA_PembuatanAktiva", var, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count > 0)

            {


                ddlkdaktiva.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                txtnamaaktiva.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAktiva"].ToString();
                txtMerk.Text = ge.ListDataset.Tables[0].Rows[0]["Merek"].ToString();
                txtspek.Text = ge.ListDataset.Tables[0].Rows[0]["Spesifikasi"].ToString();
                txtchasis.Text = ge.ListDataset.Tables[0].Rows[0]["NoChasis"].ToString();
                txtmesin.Text = ge.ListDataset.Tables[0].Rows[0]["NoMesin"].ToString();
                kodegol = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                kodekelompok = ge.ListDataset.Tables[0].Rows[0]["KodeKel"].ToString();
                txtmasa.Text = ge.ListDataset.Tables[0].Rows[0]["MasaSusutBulan"].ToString();
                txtumur.Text = ge.ListDataset.Tables[0].Rows[0]["MasaSusut"].ToString();
                txtharga.Text = "0";
                ddllokasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeLokasi"].ToString();
                ddlgolongan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                txtnomoraktiva.Text = ge.ListDataset.Tables[0].Rows[0]["NoAktiva"].ToString();
                txtcatatan.Text = ge.ListDataset.Tables[0].Rows[0]["Keterangan"].ToString();

                ddlgroupaktiva.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGroup"].ToString();

            }


            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[12];
            var1[0] = gb.CreateSPParameter("@KodeGolongan", ParameterDirection.Input, SqlDbType.VarChar, 50, kodegol, typeof(string));
            var1[1] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 50, kodekelompok, typeof(string));
            var1[2] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkdaktiva.SelectedValue, typeof(string));
            var1[3] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
            var1[4] = gb.CreateSPParameter("@SL", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
            var1[5] = gb.CreateSPParameter("@DB", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
            var1[6] = gb.CreateSPParameter("@user", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            var1[7] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETMASASUSUT", typeof(string));
            var1[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[9] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
            var1[10] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[11] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)

            {

                hdmsusut.Value = ge.ListDataset.Tables[0].Rows[0]["MasaSusutBulan"].ToString();
                hdmtahun.Value = ge.ListDataset.Tables[0].Rows[0]["MasaSusut"].ToString();
            }
            else
            {
                hdmsusut.Value = "0";
                hdmtahun.Value = "0";


            }

            ddltype.SelectedValue = "A";
            ddltype_SelectedIndexChanged(sender, e);
            txtketerangan.Text = "Transaksi Aktiva Saldo Awal";
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@KodeLokasi", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYKODENOTKNL", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[5] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("dbo.FA_PembuatanAktiva", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAktiva asc";
            ddlkdaktiva.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkdaktiva.DataTextField = "aktiva";
            ddlkdaktiva.DataValueField = "KodeAktiva";
            ddlkdaktiva.DataBind();
            ddlkdaktiva.Items.Insert(0, new ListItem("Pilih Data", ""));

            pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
     
        Pnlfooter.Visible = true;
            btnSimpan.Visible = true;
            btnSimpan.Enabled = true;


            //string autoId = "FA" + DateTime.Now.ToString("yyyyMMdd") + "." + String.Format("{0:" + formatnumber + "}", counternambah);

            //txtNoBukti.Text = autoId;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void txtresidu_TextChanged(object sender, EventArgs e)
    {
        try
        {
                decimal ax = Convert.ToDecimal(txtharga.Text);
                txtharga.Text = string.Format("{0:N2}", ax);
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void txtharga_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime originalDate = DateTime.Parse(txtakhirsusut.Text);
            DateTime tgltrn = DateTime.Parse(txttglsusut.Text);
            decimal total = decimal.Parse(txtResidu.Text);
            decimal penyusutan = 0;
            if (ddltype.SelectedValue == "B")
            {
                txtakumulasisusut.Text = "0";
                txtpenambahanharga.Text = "0";
                txtpenguranganharga.Text = "0";
                //string harga = txtharga.Text.Replace(",", "");

                nilai_residu = Convert.ToDouble(txtharga.Text) - Convert.ToDouble(txtakumulasisusut.Text) - Convert.ToDouble(txtpenambahanharga.Text) - Convert.ToDouble(txtpenguranganharga.Text);
                decimal rubah = Convert.ToDecimal(nilai_residu);
                txtResidu.Text = string.Format("{0:N2}", rubah);
                txtResidu.Enabled = false;
                //decimal ax = Convert.ToDecimal(harga);
                //txtharga.Text = string.Format("{0:N2}", ax);
            }
            else
            {
                //string harga = txtharga.Text.Replace(",", "");
                nilai_residu = Convert.ToDouble(txtharga.Text) - Convert.ToDouble(txtakumulasisusut.Text); 
                decimal rubah = Convert.ToDecimal(nilai_residu);
                txtResidu.Text = string.Format("{0:N2}", rubah);
                txtResidu.Enabled = false;
                int monthsx1 = (originalDate.Year * 12 + originalDate.Month) - (tgltrn.Year * 12 + tgltrn.Month);

                total = decimal.Parse(txtResidu.Text);
                penyusutan = total / (monthsx1);

                txtnilaisusut.Text = string.Format("{0:N2}", penyusutan);
                //decimal ax = Convert.ToDecimal(harga);
                //txtharga.Text = string.Format("{0:N2}", ax);

                //    txtResidu.Text = nilai_residu.ToString();
                //txtResidu.Enabled = false;
            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void txtakumulasisusut_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime originalDate = DateTime.Parse(txtakhirsusut.Text);
            DateTime tgltrn = DateTime.Parse(txttglsusut.Text);
            decimal total = decimal.Parse(txtResidu.Text);
            decimal penyusutan = 0;
            if (ddltype.SelectedValue == "A")
            {
                txtpenambahanharga.Text = "0";
                txtpenguranganharga.Text = "0";
                //txtharga.Text = txtharga.Text.Replace(",", "");

                nilai_residu = Convert.ToDouble(txtharga.Text) - Convert.ToDouble(txtakumulasisusut.Text);
                decimal rubah = Convert.ToDecimal(nilai_residu);
                txtResidu.Text = string.Format("{0:N2}", rubah);
                txtResidu.Enabled = false;
                //decimal ax = Convert.ToDecimal(txtharga.Text);
                //txtharga.Text = string.Format("{0:N2}", ax);
               
                int monthsx1 = (originalDate.Year * 12 + originalDate.Month) - (tgltrn.Year * 12 + tgltrn.Month);

                total = decimal.Parse(txtResidu.Text);
                penyusutan = total / (monthsx1);

                txtnilaisusut.Text = string.Format("{0:N2}", penyusutan);

            }
            if (txtumur.Text == "0")
            {
                masa = int.Parse(txtmasa.Text);
                if (masa <= 11)
                {

                    //DateTime date = new DateTime();

                    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                    //int tahun = int.Parse(year);
                    //int bulan = (int.Parse(month) + int.Parse(txtmasa.Text)) - 1;

                    //date = date.AddMonths(bulan);

                    //int day = DateTime.DaysInMonth(tahun, date);

                    //txtakhirsusut.Text = tahun + "-" + date + "-" + day;

                    DateTime now = new DateTime(int.Parse(year), int.Parse(month), int.Parse(hari));

                    DateTime akhirmasasusut = now.AddMonths(int.Parse(txtmasa.Text) - 1);


                    txtakhirsusut.Text = akhirmasasusut.ToShortDateString();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masa Manfaat tidak boleh lebih dari 11 bulan');", true);

                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void txtmulaisusut_TextChanged(object sender, EventArgs e)
    {
        try 
        { 

            DateTime ct = Convert.ToDateTime(txtmulaisusut.Text);
            ct = ct.AddMonths(int.Parse(txtmasa.Text));

            txtakhirsusut.Text = ct.ToString("dd-MM-yyyy");
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }



    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltransaksi.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!','Cek tanggal!');", true);
            txttgltransaksi.Text = xtanggal.ToString("dd-MM-yyyy");

        }

    }

    protected void btnSimpanx_Click(object sender, EventArgs e)
    {
        {
            string STATE = "";
            try
            {

                btnSimpan.Enabled = false;

                DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
                varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltransaksi.Text, typeof(DateTime));
                varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
                varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

                string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

                if (xsf != "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                    btnSimpan.Enabled = true;
                }
                else
                {



                    Decimal asd = Convert.ToDecimal(txtharga.Text);
                    if (txtharga.Text == null || Convert.ToDecimal(txtharga.Text) == Convert.ToDecimal("0") || Convert.ToDecimal(txtharga.Text) <= Convert.ToDecimal("1"))
                    {
                        if (ddltype.SelectedValue == "A")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Perolehan tidak boleh 0(nol)..!!');", true);
                        }
                        else if (ddltype.SelectedValue == "B")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Perolehan tidak boleh 0(nol)..!!');", true);
                        }
                        else if (ddltype.SelectedValue == "K")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Penjualan tidak boleh 0(nol)..!!');", true);
                        }
                        else if (ddltype.SelectedValue == "L")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Disposisi tidak boleh 0(nol)..!!');", true);
                        }
                    }
                    else
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[26];
                        var[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdaktiva.SelectedValue, typeof(string));
                        var[1] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtNoBukti.Text, typeof(string));
                        var[2] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltransaksi.Text, typeof(string));
                        var[3] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltype.SelectedValue, typeof(string));
                        var[4] = gb.CreateSPParameter("@NamaLgn", ParameterDirection.Input, SqlDbType.VarChar, 40, txtbeli.Text, typeof(string));
                        var[5] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                        var[6] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 20, "FA", typeof(string));
                        var[7] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtketerangan.Text, typeof(string));
                        var[8] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Float, 50, txtmasa.Text, typeof(string));
                        var[9] = gb.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Decimal, 0, txtharga.Text, typeof(decimal));
                        var[10] = gb.CreateSPParameter("@JlhPerolehan", ParameterDirection.Input, SqlDbType.Decimal, 0, txtharga.Text, typeof(decimal));
                        var[11] = gb.CreateSPParameter("@JlhResidu", ParameterDirection.Input, SqlDbType.Decimal, 0, txtResidu.Text, typeof(decimal));
                        var[12] = gb.CreateSPParameter("@HargaBeli", ParameterDirection.Input, SqlDbType.Decimal, 0, txthargapembelian.Text, typeof(decimal));
                        var[13] = gb.CreateSPParameter("@HargaAkumulasi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtakumulasisusut.Text, typeof(decimal));
                        var[14] = gb.CreateSPParameter("@HargaTambah", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpenambahanharga.Text, typeof(decimal));
                        var[15] = gb.CreateSPParameter("@HargaKurang", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpenguranganharga.Text, typeof(decimal));
                        var[16] = gb.CreateSPParameter("@TglMulaiSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtmulaisusut.Text, typeof(DateTime));
                        var[17] = gb.CreateSPParameter("@TglAkhirSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtakhirsusut.Text, typeof(DateTime));
                        var[18] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttanggalentry.Text, typeof(DateTime));
                        var[19] = gb.CreateSPParameter("@TglAwalBeli", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpertama.Text, typeof(DateTime));
                        var[20] = gb.CreateSPParameter("@TglAkhirBeli", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglbeliakhir.Text, typeof(DateTime));
                        var[21] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "INSERT", typeof(string));
                        var[22] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[23] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[24] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var[25] = gb.CreateSPParameter("@tglsusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglsusut.Text, typeof(DateTime));

                        ge.ListDataset = gb.GetDataJustOutPut("FA_Trn_MUTASI_SAVE", var, "constrmf");

                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Sukses','top-right','success','2');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        }


                    }

                }
            }

            catch (Exception X)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        btnSimpan.Enabled = false;
        btnSimpan.Visible = false;
        try
        {
           
            
            
            
            
            
            Decimal asd = Convert.ToDecimal(txtharga.Text);
           if (txtharga.Text == null || Convert.ToDecimal(txtharga.Text) == Convert.ToDecimal("0") || Convert.ToDecimal(txtharga.Text) <= Convert.ToDecimal("1"))
            {
                if (ddltype.SelectedValue == "A")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Perolehan tidak boleh 0(nol)..!!');", true);
                }
                else if (ddltype.SelectedValue == "B")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Perolehan tidak boleh 0(nol)..!!');", true);
                }
                else if (ddltype.SelectedValue == "K")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Penjualan tidak boleh 0(nol)..!!');", true);
                }
                else if (ddltype.SelectedValue == "L")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Harga Disposisi tidak boleh 0(nol)..!!');", true);
                }
            }
            else
            {

                if (ddltype.SelectedValue == "K" || ddltype.SelectedValue == "L")
                {
                    STATE = "INSERTKORL";
                    txtmasa.Text = "0";
                    txtResidu.Text = "0";
                    txtakhirsusut.Text = "1900-01-01 00:00:00";

                    DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[27];
                    var3[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlkdaktiva.SelectedValue, typeof(string));
                    var3[1] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtNoBukti.Text, typeof(string));
                    var3[2] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltransaksi.Text, typeof(string));
                    var3[3] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltype.SelectedValue, typeof(string));
                    var3[4] = gb.CreateSPParameter("@NamaLgn", ParameterDirection.Input, SqlDbType.VarChar, 40, txtbeli.Text, typeof(string));
                    var3[5] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    var3[6] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 20, "FA", typeof(string));
                    var3[7] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtketerangan.Text, typeof(string));
                    var3[8] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Float, 50, txtmasa.Text, typeof(string));
                    var3[9] = gb.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtharga.Text != string.Empty ? txtharga.Text : "0"), typeof(decimal));
                    var3[10] = gb.CreateSPParameter("@JlhPerolehan", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal("0,000"), typeof(decimal));
                    var3[11] = gb.CreateSPParameter("@JlhResidu", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtResidu.Text != string.Empty ? txtResidu.Text : "0"), typeof(decimal));
                    var3[12] = gb.CreateSPParameter("@HargaBeli", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txthargapembelian.Text != string.Empty ? txthargapembelian.Text : "0"), typeof(decimal));
                    var3[13] = gb.CreateSPParameter("@HargaAkumulasi", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtakumulasisusut.Text != string.Empty ? txtakumulasisusut.Text : "0"), typeof(decimal));
                    var3[14] = gb.CreateSPParameter("@HargaTambah", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtpenambahanharga.Text != string.Empty ? txtpenambahanharga.Text : "0"), typeof(decimal));
                    var3[15] = gb.CreateSPParameter("@HargaKurang", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtpenguranganharga.Text != string.Empty ? txtpenguranganharga.Text : "0"), typeof(decimal));
                    var3[16] = gb.CreateSPParameter("@TglMulaiSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtmulaisusut.Text, typeof(DateTime));
                    var3[17] = gb.CreateSPParameter("@TglAkhirSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtakhirsusut.Text, typeof(DateTime));
                    var3[18] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttanggalentry.Text, typeof(DateTime));
                    var3[19] = gb.CreateSPParameter("@TglAwalBeli", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpertama.Text, typeof(DateTime));
                    var3[20] = gb.CreateSPParameter("@TglAkhirBeli", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglbeliakhir.Text, typeof(DateTime));
                    var3[21] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEKORL", typeof(string));
                    var3[22] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var3[23] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var3[24] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    //var3[25] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJurnal.SelectedValue, typeof(string));
                    //var3[26] = gb.CreateSPParameter("@TypeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, rbJurnal.SelectedValue, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", var3, "constrmf");

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[27];
                    var[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdaktiva.SelectedValue, typeof(string));
                    var[1] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtNoBukti.Text, typeof(string));
                    var[2] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltransaksi.Text, typeof(string));
                    var[3] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltype.SelectedValue, typeof(string));
                    var[4] = gb.CreateSPParameter("@NamaLgn", ParameterDirection.Input, SqlDbType.VarChar, 40, txtbeli.Text, typeof(string));
                    var[5] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    var[6] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 20, "FA", typeof(string));
                    var[7] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtketerangan.Text, typeof(string));
                    var[8] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Float, 50, txtmasa.Text, typeof(string));
                    var[9] = gb.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtharga.Text != string.Empty ? txtharga.Text : "0"), typeof(decimal));
                    var[10] = gb.CreateSPParameter("@JlhPerolehan", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal("0,000"), typeof(decimal));
                    var[11] = gb.CreateSPParameter("@JlhResidu", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtResidu.Text != string.Empty ? txtResidu.Text : "0"), typeof(decimal));
                    var[12] = gb.CreateSPParameter("@HargaBeli", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txthargapembelian.Text != string.Empty ? txthargapembelian.Text : "0"), typeof(decimal));
                    var[13] = gb.CreateSPParameter("@HargaAkumulasi", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtakumulasisusut.Text != string.Empty ? txtakumulasisusut.Text : "0"), typeof(decimal));
                    var[14] = gb.CreateSPParameter("@HargaTambah", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtpenambahanharga.Text != string.Empty ? txtpenambahanharga.Text : "0"), typeof(decimal));
                    var[15] = gb.CreateSPParameter("@HargaKurang", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtpenguranganharga.Text != string.Empty ? txtpenguranganharga.Text : "0"), typeof(decimal));
                    var[16] = gb.CreateSPParameter("@TglMulaiSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtmulaisusut.Text, typeof(DateTime));
                    var[17] = gb.CreateSPParameter("@TglAkhirSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtakhirsusut.Text, typeof(DateTime));
                    var[18] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttanggalentry.Text, typeof(DateTime));
                    var[19] = gb.CreateSPParameter("@TglAwalBeli", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpertama.Text, typeof(DateTime));
                    var[20] = gb.CreateSPParameter("@TglAkhirBeli", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglbeliakhir.Text, typeof(DateTime));
                    var[21] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, STATE, typeof(string));
                    var[22] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[23] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[24] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    //var[25] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJurnal.SelectedValue, typeof(string));
                    //var[26] = gb.CreateSPParameter("@TypeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, rbJurnal.SelectedValue, typeof(string));
                    if (txtnamaaktiva.Enabled == false)
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", var, "constrmf");
                    }
                    else
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", var, "constrmf");
                    }
                }
                else
                {
                    /*txthargapembelian.Text != string.Empty ? txthargapembelian.Text : "0"*///;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[25];
                    var[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdaktiva.SelectedValue, typeof(string));
                    var[1] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtNoBukti.Text, typeof(string));
                    var[2] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltransaksi.Text, typeof(string));
                    var[3] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltype.SelectedValue, typeof(string));
                    var[4] = gb.CreateSPParameter("@NamaLgn", ParameterDirection.Input, SqlDbType.VarChar, 40, txtbeli.Text, typeof(string));
                    var[5] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    var[6] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 20, "FA", typeof(string));
                    var[7] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtketerangan.Text, typeof(string));
                    var[8] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Float, 50, txtmasa.Text, typeof(string));
                    var[9] = gb.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal("0,000"), typeof(decimal));
                    var[10] = gb.CreateSPParameter("@JlhPerolehan", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtharga.Text != string.Empty ? txtharga.Text : "0"), typeof(decimal));
                    var[11] = gb.CreateSPParameter("@JlhResidu", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtResidu.Text != string.Empty ? txtResidu.Text : "0"), typeof(decimal));
                    var[12] = gb.CreateSPParameter("@HargaBeli", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txthargapembelian.Text != string.Empty ? txthargapembelian.Text : "0"), typeof(decimal));
                    var[13] = gb.CreateSPParameter("@HargaAkumulasi", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtakumulasisusut.Text != string.Empty ? txtakumulasisusut.Text : "0"), typeof(decimal));
                    var[14] = gb.CreateSPParameter("@HargaTambah", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtpenambahanharga.Text != string.Empty ? txtpenambahanharga.Text : "0"), typeof(decimal));
                    var[15] = gb.CreateSPParameter("@HargaKurang", ParameterDirection.Input, SqlDbType.Decimal, 0, Convert.ToDecimal(txtpenguranganharga.Text != string.Empty ? txtpenguranganharga.Text : "0"), typeof(decimal));
                    var[16] = gb.CreateSPParameter("@TglMulaiSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtmulaisusut.Text, typeof(DateTime));
                    var[17] = gb.CreateSPParameter("@TglAkhirSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtakhirsusut.Text, typeof(DateTime));
                    var[18] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttanggalentry.Text, typeof(DateTime));
                    var[19] = gb.CreateSPParameter("@TglAwalBeli", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpertama.Text, typeof(DateTime));
                    var[20] = gb.CreateSPParameter("@TglAkhirBeli", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglbeliakhir.Text, typeof(DateTime));
                    var[21] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "INSERT", typeof(string));
                    var[22] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[23] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[24] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", var, "constrmf");
                 
                }
                //double counterangka = Convert.ToDouble(txtNoBukti.Text.Substring(11, 5));

                //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                //var1[0] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 20, "FA_AktivaAwal", typeof(string));
                //var1[1] = gb.CreateSPParameter("@counter_number", ParameterDirection.Input, SqlDbType.VarChar, 50, counterangka, typeof(string));
                //var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEAKTIVAAWAL", typeof(string));
                //var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("FA_COUNTER", var1, "constrmf");
                //if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                //    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                //}
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        finally {
            DrawHeader_GRDATA();
        }
    }
    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == true)
        {
            Response.Redirect("AktivaAwal.aspx");
        }
        else
        {
            pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;
            Pnlfooter.Visible = true;
          
            loaddata();
        }


    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            string aktiva = ddlkdaktiva.SelectedValue;
            string nobukti = txtNoBukti.Text;
            //string jlhrawat = txtJurnal.Text;

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[7];
            var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            var1[4] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, aktiva, typeof(string));
            var1[5] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 40, ddltype.SelectedValue, typeof(string));
            var1[6] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("FA_Trn_MUTASI_DELETE", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Sukses','top-right','success','2');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "SETDEL":

                try
                {
                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    //PnlJournal.Visible = false;
                    Pnlfooter.Visible = true;
                    loaddata();

                    kodeaktiva = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdaktiva")).Text;
                    string nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnomorbukti")).Text;
                    string perolehan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperolehan")).Text;
                    string typetrn = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypetrn")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[7];

                    var1[0] = gbs.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTALLDELETE", typeof(string));
                    var1[1] = gbs.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[2] = gbs.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var1[3] = gbs.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));
                    var1[4] = gbs.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 40, kodeaktiva, typeof(string));
                    var1[5] = gbs.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 40, nobukti, typeof(string));
                    var1[6] = gbs.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 40, typetrn, typeof(string));

                    ges.ListDataset = gbs.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", var1, "constrmf");
                    ambiltype(ges.ListDataset.Tables[0].Rows[0]["TypeTrn"].ToString());
                    ddlkdaktiva.Items.Insert(0, new ListItem(kodeaktiva + " - " + ges.ListDataset.Tables[0].Rows[0]["NamaAktiva"].ToString(), kodeaktiva));
                    ddlkdaktiva.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdaktiva")).Text;
                    ddllokasi.SelectedValue = ges.ListDataset.Tables[0].Rows[0]["KodeLokasi"].ToString();
                    txtNoBukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnomorbukti")).Text;
                    txtnamaaktiva.Text = ges.ListDataset.Tables[0].Rows[0]["NamaAktiva"].ToString();
                    txtcatatan.Text = ges.ListDataset.Tables[0].Rows[0]["catatan"].ToString();
                    txtMerk.Text = ges.ListDataset.Tables[0].Rows[0]["Merek"].ToString();
                    txtspek.Text = ges.ListDataset.Tables[0].Rows[0]["Spesifikasi"].ToString();
                    txtchasis.Text = ges.ListDataset.Tables[0].Rows[0]["NoChasis"].ToString();
                    txtmesin.Text = ges.ListDataset.Tables[0].Rows[0]["NoMesin"].ToString();
                    kodegol = ges.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                    kodekelompok = ges.ListDataset.Tables[0].Rows[0]["KodeKel"].ToString();
                    txtmasa.Text = ges.ListDataset.Tables[0].Rows[0]["namabulan"].ToString();
                    txtumur.Text = ges.ListDataset.Tables[0].Rows[0]["namatahun"].ToString();
                    txtnomoraktiva.Text = ges.ListDataset.Tables[0].Rows[0]["NoAktiva"].ToString();
                    txtketerangan.Text = ges.ListDataset.Tables[0].Rows[0]["Keterangan"].ToString();
                    ddlgroupaktiva.SelectedValue = ges.ListDataset.Tables[0].Rows[0]["KodeGroup"].ToString();
                    ddlgolongan.SelectedValue = ges.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                    ddltype.SelectedValue = typetrn;
                         
                    if (ddltype.SelectedValue == "K" || ddltype.SelectedValue == "L")
                    {
                        txtharga.Text = ges.ListDataset.Tables[0].Rows[0]["Jumlah"].ToString();
                    }
                    else
                    {
                        txtharga.Text = ges.ListDataset.Tables[0].Rows[0]["JlhPerolehan"].ToString();
                    }
                    //txtharga.Text = ges.ListDataset.Tables[0].Rows[0]["JlhPerolehan"].ToString();
                    txtResidu.Text = ges.ListDataset.Tables[0].Rows[0]["JlhResidu"].ToString();

                    if (ddltype.SelectedValue == "A" || ddltype.SelectedValue == "B")
                    {
                        txtmulaisusut.Text = Convert.ToDateTime(ges.ListDataset.Tables[0].Rows[0]["TglMulaiSusut"].ToString()).ToString("dd-MM-yyyy");
                        txtakhirsusut.Text = Convert.ToDateTime(ges.ListDataset.Tables[0].Rows[0]["TglAkhirSusut"].ToString()).ToString("dd-MM-yyyy");
                        txttglsusut.Text = Convert.ToDateTime(ges.ListDataset.Tables[0].Rows[0]["Tglsusut"].ToString()).ToString("dd-MM-yyyy");
                        
                    }

                    txttgltransaksi.Text = Convert.ToDateTime(ges.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString()).ToString("dd-MM-yyyy");
                    txttanggalentry.Text = Convert.ToDateTime(ges.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString()).ToString("dd-MM-yyyy");
                    txtakumulasisusut.Text = string.Format("{0:N2}", Convert.ToDecimal(ges.ListDataset.Tables[0].Rows[0]["HargaAkumulasi"].ToString()));
                    txtpenambahanharga.Text = string.Format("{0:N2}", Convert.ToDecimal(ges.ListDataset.Tables[0].Rows[0]["HargaAkumulasi"].ToString()));
                    txtpenguranganharga.Text = string.Format("{0:N2}", Convert.ToDecimal(ges.ListDataset.Tables[0].Rows[0]["HargaAkumulasi"].ToString()));
                    txtnilaisusut.Text = string.Format("{0:N2}", Convert.ToDecimal(ges.ListDataset.Tables[0].Rows[0]["nilaisusut"].ToString()));
                    

                    txtbeli.Text = ges.ListDataset.Tables[0].Rows[0]["NamaLgn"].ToString();
                    txthargapembelian.Text = string.Format("{0:N2}", Convert.ToDecimal(ges.ListDataset.Tables[0].Rows[0]["HargaBeli"].ToString()));
                    txttgltransaksi.Enabled = false;
                    txttanggalentry.Enabled = false;

                    txtbeli.Enabled = false;
                    txtharga.Enabled = false;
                    txtakhirsusut.Enabled = false;
                    txtakumulasisusut.Enabled = false;
                    txtResidu.Enabled = false;
                    ddlkdaktiva.Enabled = false;
                    txttanggalentry.Enabled = false;
                    txttgltransaksi.Enabled = false;
                    txttglpertama.Enabled = false;
                    txtketerangan.Enabled = false;
                    decimal a, b, c, d, f, g, h, j;
                    txtharga.Text = string.Format("{0:N2}", Convert.ToDecimal(txtharga.Text.ToString()));
                    txtResidu.Text = string.Format("{0:N2}", Convert.ToDecimal(txtResidu.Text.ToString()));
                    txtakumulasisusut.Text = string.Format("{0:N2}", Convert.ToDecimal(txtakumulasisusut.Text.ToString()));
                    a = Convert.ToDecimal(txtpenambahanharga.Text != null ? txtpenambahanharga.Text.ToString() : "0");
                    txtpenambahanharga.Text = string.Format("{0:N2}", a);
                    //int pembelian = int.Parse(txthargapembelian.Text);
                    //b = Convert.ToDecimal(txthargapembelian.Text != null || txthargapembelian.Text != string.Empty ? txthargapembelian.Text.ToString() : "0" );
                    //txthargapembelian.Text = string.Format("{0:N2}", Convert.ToDecimal(txthargapembelian.Text.ToString()));

               
                    txtketerangan.Enabled = false;
                    ddltype.Enabled = false;
                    txtakhirsusut.Enabled = false;
                    txtakumulasisusut.Enabled = false;
                    txtmulaisusut.Enabled = false;
                    btndelete.Enabled = true;
     

                
                        btndelete.Enabled = true;
                        btndelete.Visible = true;
                    btnSimpan.Visible = false;
                    lbltglsusut.Visible = false;
                    txttglsusut.Visible = false;

                    if (ddltype.SelectedValue == "A")
                    {
                        lbluniversal.Text = "Dibeli/Diterima Dari:";
                        lbluniversal.Visible = true;
                        lblmulaisusut.Visible = true;
                        txtmulaisusut.Visible = true;
                        lblumur.Visible = true;
                        txtumur.Visible = true;
                        txtbeli.Visible = true;
                        lbltanggalbeli.Visible = true;
                        txttglpertama.Visible = true;
                        lbltanggalbeliakhir.Visible = true;
                        txttglbeliakhir.Visible = true;
                        lblhargapembelian.Visible = true;
                        txthargapembelian.Visible = true;
                        lblakumulasi.Visible = true;
                        txtakumulasisusut.Visible = true;
                        lblpenambahanharga.Visible = true;
                        txtpenambahanharga.Visible = true;
                        lblpenguranganharga.Visible = true;
                        txtpenguranganharga.Visible = true;

                        lbltglakhirsusut.Visible = true;
                        txtakhirsusut.Visible = true;
                        txttglbeliakhir.Visible = false;
                        lbltanggalbeliakhir.Visible = false;
                        lblmasamanfaat.Visible = true;
                        txtmasa.Visible = true;
                        lblhargaperolehan.Visible = true;
                        lblhargaperolehan.Text = "Harga Perolehan";
                        txtharga.Visible = true;
                        lblnilai.Visible = true;
                        txtResidu.Visible = true;
                        lblketerangan.Visible = true;
                        txtketerangan.Visible = true;

                        txtakhirsusut.Enabled = false;
                        lblhargapembelian.Visible = false;
                        lblpenambahanharga.Visible = false;
                        txtpenambahanharga.Visible = false;
                        lblpenguranganharga.Visible = false;
                        txtpenguranganharga.Visible = false;
                        txthargapembelian.Visible = false;
                        lbltglsusut.Visible = true;
                        txttglsusut.Visible = true;
                        lblnilaisusut.Visible = true;
                        txtnilaisusut.Visible = true;
                        txttglsusut.Enabled = false;
                        //if (txtumur.Text == "0")
                        //{
                        //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                        //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                        //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                        //    int tahun = int.Parse(year);
                        //    //int bulan = (int.Parse(month) + int.Parse(txtmasa.Text)) - 1;

                        //    //int day = DateTime.DaysInMonth(tahun, (bulan - 1));


                        //    //txtakhirsusut.Text = tahun + "-" + (bulan) + "-" + day;
                        //    var rubahbdate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(hari));
                        //    string hitungbulang = rubahbdate.AddMonths(Convert.ToInt32(txtmasa.Text) - 1).ToString("d");

                        //    txtakhirsusut.Text = Convert.ToString(Convert.ToDateTime(hitungbulang).ToString("dd/MM/yyyy"));
                        //    DateTime  ctx= Convert.ToDateTime(txtmulaisusut.Text);
                        //    ctx = ctx.AddMonths(int.Parse(txtmasa.Text));
                        //}
                        //else
                        //{
                        //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                        //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                        //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                        //    int tahun = int.Parse(year) + int.Parse(txtumur.Text);

                        //    int day = DateTime.DaysInMonth(tahun, (int.Parse(month) - 1));

                        //    txtakhirsusut.Text = tahun + "-" + (int.Parse(month) - 1) + "-" + day;

                        //    masa = double.Parse(hdmtahun.Value) * 12;
                        //    txtmasa.Text = masa.ToString();

                        //}


                    }
                    else if (ddltype.SelectedValue == "B")
                    {
                        lbluniversal.Text = "Dibeli/Diterima Dari:";
                        lbluniversal.Visible = true;
                        txtbeli.Visible = true;
                        lbluniversal.Visible = true;
                        lblmulaisusut.Visible = true;
                        txtmulaisusut.Visible = true;
                        lblumur.Visible = true;
                        txtumur.Visible = true;
                        txtbeli.Visible = true;
                        lbltanggalbeli.Visible = false;
                        txttglpertama.Visible = false;
                        lbltanggalbeliakhir.Visible = false;
                        txttglbeliakhir.Visible = false;
                        lblhargapembelian.Visible = false;
                        txthargapembelian.Visible = false;
                        lblakumulasi.Visible = false;
                        txtakumulasisusut.Visible = false;
                        lblpenambahanharga.Visible = false;
                        txtpenambahanharga.Visible = false;
                        lblpenguranganharga.Visible = false;
                        txtpenguranganharga.Visible = false;

                        lbltglakhirsusut.Visible = true;
                        txtakhirsusut.Visible = true;
                        lblmasamanfaat.Visible = true;
                        txtmasa.Visible = true;
                        lblhargaperolehan.Visible = true;
                        txtharga.Visible = true;
                        lblhargaperolehan.Text = "Harga Perolehan";
                        lblnilai.Visible = true;
                        txtResidu.Visible = true;
                        lblketerangan.Visible = true;
                        txtketerangan.Visible = true;
                        lbltanggalbeli.Visible = true;
                        txttglpertama.Visible = true;

                        txtakhirsusut.Enabled = false;
                        txtmasa.Enabled = false;


                        //if (txtumur.Text == "0")
                        //{
                        //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                        //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                        //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                        //    int tahun = int.Parse(year);
                        //    //int bulan = (int.Parse(month) + int.Parse(txtmasa.Text)) - 1;

                        //    //int day = DateTime.DaysInMonth(tahun, (bulan - 1));
                        //    var rubahbdate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(hari));
                        //    string hitungbulang = rubahbdate.AddMonths(Convert.ToInt32( txtmasa.Text)-1).ToString("d");

                        //    //txtakhirsusut.Text = tahun + "-" + (bulan) + "-" + day;

                        //    txtakhirsusut.Text = Convert.ToString(Convert.ToDateTime(hitungbulang).ToString("dd/MM/yyyy"));
                        //}
                        //else
                        //{
                        //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                        //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                        //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                        //    int tahun = int.Parse(year) + int.Parse(txtumur.Text);

                        //    int day = DateTime.DaysInMonth(tahun, (int.Parse(month) - 1));

                        //    txtakhirsusut.Text = tahun + "-" + (int.Parse(month) - 1) + "-" + day;

                        //    masa = double.Parse(hdmtahun.Value) * 12;
                        //    txtmasa.Text = masa.ToString();

                        //}


                    }
                    else if (ddltype.SelectedValue == "K")
                    {
                        lbluniversal.Text = "Dijual Kepada:";
                        lbluniversal.Visible = true;
                        txtbeli.Visible = true;
                        txtbeli.Visible = true;
                        lbltanggalbeli.Visible = false;
                        txttglpertama.Visible = false;
                        lbltanggalbeliakhir.Visible = false;
                        txttglbeliakhir.Visible = false;
                        lblhargapembelian.Visible = false;
                        txthargapembelian.Visible = false;
                        lblakumulasi.Visible = false;
                        txtakumulasisusut.Visible = false;
                        lblpenambahanharga.Visible = false;
                        txtpenambahanharga.Visible = false;
                        lblpenguranganharga.Visible = false;
                        txtpenguranganharga.Visible = false;

                        lbltglakhirsusut.Visible = false;
                        txtakhirsusut.Visible = false;
                        lblmasamanfaat.Visible = false;
                        txtmasa.Visible = false;
                        lblhargaperolehan.Visible = true;
                        txtharga.Visible = true;
                        lblhargaperolehan.Text = "Harga Penjualan";
                        lblnilai.Visible = false;
                        txtResidu.Visible = false;
                        lblketerangan.Visible = true;
                        txtketerangan.Visible = true;
                        lblumur.Visible = false;
                        txtumur.Visible = false;
                        lblmulaisusut.Visible = false;
                        txtmulaisusut.Visible = false;

                        txtakhirsusut.Enabled = false;
                        txtmasa.Enabled = false;
                    }
                    else if (ddltype.SelectedValue == "C")
                    {
                        lbluniversal.Text = "Dibeli/Diterima Dari:";
                        lbluniversal.Visible = true;
                        txtbeli.Visible = true;
                        lblmulaisusut.Visible = true;
                        txtmulaisusut.Visible = true;
                        txtbeli.Visible = true;
                        lbltanggalbeli.Visible = false;
                        txttglpertama.Visible = false;
                        lbltanggalbeliakhir.Visible = false;
                        txttglbeliakhir.Visible = false;
                        lblhargapembelian.Visible = false;
                        txthargapembelian.Visible = false;
                        lblakumulasi.Visible = false;
                        txtakumulasisusut.Visible = false;
                        lblpenambahanharga.Visible = false;
                        txtpenambahanharga.Visible = false;
                        lblpenguranganharga.Visible = false;
                        txtpenguranganharga.Visible = false;

                        lbltglakhirsusut.Visible = true;
                        txtakhirsusut.Visible = true;
                        lblmasamanfaat.Visible = true;
                        txtmasa.Visible = true;
                        lblhargaperolehan.Visible = true;
                        txtharga.Visible = true;
                        lblhargaperolehan.Text = "Harga Perolehan";
                        lblnilai.Visible = false;
                        txtResidu.Visible = false;
                        lblketerangan.Visible = true;
                        txtketerangan.Visible = true;
                        lblumur.Visible = false;
                        txtumur.Visible = false;
                        lblmulaisusut.Visible = false;
                        txtmulaisusut.Visible = false;

                        txtakhirsusut.Enabled = false;
                        txtmasa.Enabled = false;
                    }
                    else if (ddltype.SelectedValue == "D")
                    {
                        lbluniversal.Text = "Dibeli/Diterima Dari:";
                        lbluniversal.Visible = true;
                        txtbeli.Visible = true;
                        lblmulaisusut.Visible = true;
                        txtmulaisusut.Visible = true;
                        lbltanggalbeli.Visible = false;
                        txttglpertama.Visible = false;
                        lbltanggalbeliakhir.Visible = false;
                        txttglbeliakhir.Visible = false;
                        lblhargapembelian.Visible = false;
                        txthargapembelian.Visible = false;
                        lblakumulasi.Visible = false;
                        txtakumulasisusut.Visible = false;
                        lblpenambahanharga.Visible = false;
                        txtpenambahanharga.Visible = false;
                        lblpenguranganharga.Visible = false;
                        txtpenguranganharga.Visible = false;
                        lblumur.Visible = false;
                        txtumur.Visible = false;
                        lblmulaisusut.Visible = false;
                        txtmulaisusut.Visible = false;


                        lbltglakhirsusut.Visible = true;
                        txtakhirsusut.Visible = true;
                        lblmasamanfaat.Visible = true;
                        txtmasa.Visible = true;
                        lblhargaperolehan.Visible = true;
                        txtharga.Visible = true;
                        lblhargaperolehan.Text = "Harga Perolehan";
                        lblnilai.Visible = false;
                        txtResidu.Visible = false;
                        lblketerangan.Visible = true;
                        txtketerangan.Visible = true;

                        txtakhirsusut.Enabled = false;
                        txtmasa.Enabled = false;
                    }

                    else if (ddltype.SelectedValue == "L")
                    {
                        lbluniversal.Text = "Didisposisi Kepada:";
                        lbluniversal.Visible = true;
                        txtbeli.Visible = true;
                        txtbeli.Visible = true;
                        lbltanggalbeli.Visible = false;
                        txttglpertama.Visible = false;
                        lbltanggalbeliakhir.Visible = false;
                        txttglbeliakhir.Visible = false;
                        lblhargapembelian.Visible = false;
                        txthargapembelian.Visible = false;
                        lblakumulasi.Visible = false;
                        txtakumulasisusut.Visible = false;
                        lblpenambahanharga.Visible = false;
                        txtpenambahanharga.Visible = false;
                        lblpenguranganharga.Visible = false;
                        txtpenguranganharga.Visible = false;

                        lbltglakhirsusut.Visible = false;
                        txtakhirsusut.Visible = false;
                        lblmasamanfaat.Visible = false;
                        txtmasa.Visible = false;
                        lblhargaperolehan.Visible = true;
                        txtharga.Visible = true;
                        lblhargaperolehan.Text = "Harga Disposisi";
                        lblnilai.Visible = false;
                        txtResidu.Visible = false;
                        lblketerangan.Visible = true;
                        txtketerangan.Visible = true;
                        lblumur.Visible = false;
                        txtumur.Visible = false;
                        lblmulaisusut.Visible = false;
                        txtmulaisusut.Visible = false;

                        txtakhirsusut.Enabled = false;
                        txtmasa.Enabled = false;
                    }




                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                finally
                {
                    DrawHeader_GRDATA();
                }

                break;
            case "SETDEL1":

                try
                {
                    kodeaktiva = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdaktiva")).Text;
                    string nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnomorbukti")).Text;
                    string perolehan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperolehan")).Text;
                    string typetrn = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypetrn")).Text;


                    DefaultClass.Global.SqlHelper[] vardel = new DefaultClass.Global.SqlHelper[7];
                    vardel[0] = gbs.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 20, kodeaktiva, typeof(string));
                    vardel[1] = gbs.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "DELETE", typeof(string));
                    vardel[2] = gbs.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    vardel[3] = gbs.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    vardel[4] = gbs.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, nobukti, typeof(string));
                    vardel[5] = gbs.CreateSPParameter("@Jumlah", ParameterDirection.Input, SqlDbType.Decimal, 0, perolehan, typeof(decimal));
                    vardel[6] = gbs.CreateSPParameter("@Typetrn", ParameterDirection.Input, SqlDbType.VarChar, 20, typetrn, typeof(string));

                    ges.ListDataset = gbs.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", vardel, "constrmf");
                    ges.ListDataTable = gbs.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", vardel, "constrmf").Tables[0];
                    if (ges.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                finally {
                    DrawHeader_GRDATA();
                }

                break;
            case "PDF":

                try
                {

                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnomorbukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, "FA", typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_AR", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count == 0)
                        throw new Exception(string.Format("Journal {0} Not Found! Generate Journal First!", nobukti));

                    rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);
                    Response.Buffer = false;
                    // Clear the response content and headers
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

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
                finally
                {
                    DrawHeader_GRDATA();
                }

                break;

        }
    }

    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "SELECTALL", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[4] = gb.CreateSPParameter("@statusadmin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessIsSuperAdmin, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAktiva asc";


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

    private void DrawHeader_GRDATA() {
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }



    private void loaddata()
    {
        //txtketerangan.Text = ((TextBox)GRRP.Rows[0].FindControl("txtKT")).Text != string.Empty ? ((TextBox)GRRP.Rows[0].FindControl("txtKT")).Text : "";

    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void ambiltype(string typetrans)
    {
        try
        {
            double masa = 0;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@KodeType", ParameterDirection.Input, SqlDbType.VarChar, 50, typetrans, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("FA_TypeTransaksi", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn asc";

            ddltype.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddltype.Items.Insert(0, new ListItem(typetrans +" - "+ ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString(), typetrans));
            txttype.Text = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();
            ddltype.Enabled = false;
            txtumur.Text = hdmtahun.Value;
            //txtmasa.Enabled = true;

            if (ddltype.SelectedValue == "A")
            {
                lbluniversal.Text = "Dibeli/Diterima Dari:";
                lbluniversal.Visible = true;
                lblmulaisusut.Visible = true;
                txtmulaisusut.Visible = true;
                lblumur.Visible = true;
                txtumur.Visible = true;
                txtbeli.Visible = true;
                lbltanggalbeli.Visible = true;
                txttglpertama.Visible = true;
                lbltanggalbeliakhir.Visible = true;
                txttglbeliakhir.Visible = true;
                lblhargapembelian.Visible = true;
                txthargapembelian.Visible = true;
                lblakumulasi.Visible = true;
                txtakumulasisusut.Visible = true;
                lblpenambahanharga.Visible = true;
                txtpenambahanharga.Visible = true;
                lblpenguranganharga.Visible = true;
                txtpenguranganharga.Visible = true;

                lbltglakhirsusut.Visible = true;
                txtakhirsusut.Visible = true;
                txttglbeliakhir.Visible = false;
                lbltanggalbeliakhir.Visible = false;
                lblmasamanfaat.Visible = true;
                txtmasa.Visible = true;
                lblhargaperolehan.Visible = true;
                lblhargaperolehan.Text = "Harga Perolehan";
                txtharga.Visible = true;
                lblnilai.Visible = true;
                txtResidu.Visible = true;
                lblketerangan.Visible = true;
                txtketerangan.Visible = true;

                txtakhirsusut.Enabled = false;
                lblhargapembelian.Visible = false;
                lblpenambahanharga.Visible = false;
                txtpenambahanharga.Visible = false;
                lblpenguranganharga.Visible = false;
                txtpenguranganharga.Visible = false;
                txthargapembelian.Visible = false;

                txtmasa.Text = hdmsusut.Value;

                


            }
            else if (ddltype.SelectedValue == "B")
            {
                lbluniversal.Text = "Dibeli/Diterima Dari:";
                lbluniversal.Visible = true;
                txtbeli.Visible = true;
                lbluniversal.Visible = true;
                lblmulaisusut.Visible = true;
                txtmulaisusut.Visible = true;
                lblumur.Visible = true;
                txtumur.Visible = true;
                txtbeli.Visible = true;
                lbltanggalbeli.Visible = false;
                txttglpertama.Visible = false;
                lbltanggalbeliakhir.Visible = false;
                txttglbeliakhir.Visible = false;
                lblhargapembelian.Visible = false;
                txthargapembelian.Visible = false;
                lblakumulasi.Visible = false;
                txtakumulasisusut.Visible = false;
                lblpenambahanharga.Visible = false;
                txtpenambahanharga.Visible = false;
                lblpenguranganharga.Visible = false;
                txtpenguranganharga.Visible = false;

                lbltglakhirsusut.Visible = true;
                txtakhirsusut.Visible = true;
                lblmasamanfaat.Visible = true;
                txtmasa.Visible = true;
                lblhargaperolehan.Visible = true;
                txtharga.Visible = true;
                lblhargaperolehan.Text = "Harga Perolehan";
                lblnilai.Visible = true;
                txtResidu.Visible = true;
                lblketerangan.Visible = true;
                txtketerangan.Visible = true;
                lbltanggalbeli.Visible = true;
                txttglpertama.Visible = true;

                txtakhirsusut.Enabled = false;
                txtmasa.Enabled = false;

                txtmasa.Text = hdmsusut.Value;

                //if (txtumur.Text == "0")
                //{
                //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                //    int tahun = int.Parse(year);
                //    //int bulan = (int.Parse(month) + int.Parse(txtmasa.Text)) - 1;

                //    //int day = DateTime.DaysInMonth(tahun, (bulan - 1));
                //    var rubahbdate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(hari));
                //    string hitungbulang = rubahbdate.AddMonths(Convert.ToInt32(txtmasa.Text) - 1).ToString("d");

                //    //txtakhirsusut.Text = tahun + "-" + (bulan) + "-" + day;

                //    txtakhirsusut.Text = Convert.ToString(Convert.ToDateTime(hitungbulang).ToString("dd/MM/yyyy"));
                //}
                //else
                //{
                //    string year = txtmulaisusut.Text.ToString().Substring(0, 4);
                //    string month = txtmulaisusut.Text.ToString().Substring(5, 2);
                //    string hari = txtmulaisusut.Text.ToString().Substring(8, 2);

                //    int tahun = int.Parse(year) + int.Parse(txtumur.Text);

                //    int day = DateTime.DaysInMonth(tahun, (int.Parse(month) - 1));

                //    txtakhirsusut.Text = tahun + "-" + (int.Parse(month) - 1) + "-" + day;

                //    masa = double.Parse(hdmtahun.Value) * 12;
                //    txtmasa.Text = masa.ToString();

                //}
            }
            else if (ddltype.SelectedValue == "K")
            {
                lbluniversal.Text = "Dijual Kepada:";
                lbluniversal.Visible = true;
                txtbeli.Visible = true;
                txtbeli.Visible = true;
                lbltanggalbeli.Visible = false;
                txttglpertama.Visible = false;
                lbltanggalbeliakhir.Visible = false;
                txttglbeliakhir.Visible = false;
                lblhargapembelian.Visible = false;
                txthargapembelian.Visible = false;
                lblakumulasi.Visible = false;
                txtakumulasisusut.Visible = false;
                lblpenambahanharga.Visible = false;
                txtpenambahanharga.Visible = false;
                lblpenguranganharga.Visible = false;
                txtpenguranganharga.Visible = false;

                lbltglakhirsusut.Visible = false;
                txtakhirsusut.Visible = false;
                lblmasamanfaat.Visible = false;
                txtmasa.Visible = false;
                lblhargaperolehan.Visible = true;
                txtharga.Visible = true;
                lblhargaperolehan.Text = "Harga Penjualan";
                lblnilai.Visible = false;
                txtResidu.Visible = false;
                lblketerangan.Visible = true;
                txtketerangan.Visible = true;
                lblumur.Visible = false;
                txtumur.Visible = false;
                lblmulaisusut.Visible = false;
                txtmulaisusut.Visible = false;

                txtakhirsusut.Enabled = false;
                txtmasa.Enabled = false;
            }
            else if (ddltype.SelectedValue == "C")
            {
                lbluniversal.Text = "Dibeli/Diterima Dari:";
                lbluniversal.Visible = true;
                txtbeli.Visible = true;
                lblmulaisusut.Visible = true;
                txtmulaisusut.Visible = true;
                txtbeli.Visible = true;
                lbltanggalbeli.Visible = false;
                txttglpertama.Visible = false;
                lbltanggalbeliakhir.Visible = false;
                txttglbeliakhir.Visible = false;
                lblhargapembelian.Visible = false;
                txthargapembelian.Visible = false;
                lblakumulasi.Visible = false;
                txtakumulasisusut.Visible = false;
                lblpenambahanharga.Visible = false;
                txtpenambahanharga.Visible = false;
                lblpenguranganharga.Visible = false;
                txtpenguranganharga.Visible = false;

                lbltglakhirsusut.Visible = true;
                txtakhirsusut.Visible = true;
                lblmasamanfaat.Visible = true;
                txtmasa.Visible = true;
                lblhargaperolehan.Visible = true;
                txtharga.Visible = true;
                lblhargaperolehan.Text = "Harga Perolehan";
                lblnilai.Visible = false;
                txtResidu.Visible = false;
                lblketerangan.Visible = true;
                txtketerangan.Visible = true;
                lblumur.Visible = false;
                txtumur.Visible = false;
                lblmulaisusut.Visible = false;
                txtmulaisusut.Visible = false;

                txtakhirsusut.Enabled = false;
                txtmasa.Enabled = false;
            }
            else if (ddltype.SelectedValue == "D")
            {
                lbluniversal.Text = "Dibeli/Diterima Dari:";
                lbluniversal.Visible = true;
                txtbeli.Visible = true;
                lblmulaisusut.Visible = true;
                txtmulaisusut.Visible = true;
                lbltanggalbeli.Visible = false;
                txttglpertama.Visible = false;
                lbltanggalbeliakhir.Visible = false;
                txttglbeliakhir.Visible = false;
                lblhargapembelian.Visible = false;
                txthargapembelian.Visible = false;
                lblakumulasi.Visible = false;
                txtakumulasisusut.Visible = false;
                lblpenambahanharga.Visible = false;
                txtpenambahanharga.Visible = false;
                lblpenguranganharga.Visible = false;
                txtpenguranganharga.Visible = false;
                lblumur.Visible = false;
                txtumur.Visible = false;
                lblmulaisusut.Visible = false;
                txtmulaisusut.Visible = false;


                lbltglakhirsusut.Visible = true;
                txtakhirsusut.Visible = true;
                lblmasamanfaat.Visible = true;
                txtmasa.Visible = true;
                lblhargaperolehan.Visible = true;
                txtharga.Visible = true;
                lblhargaperolehan.Text = "Harga Perolehan";
                lblnilai.Visible = false;
                txtResidu.Visible = false;
                lblketerangan.Visible = true;
                txtketerangan.Visible = true;

                txtakhirsusut.Enabled = false;
                txtmasa.Enabled = false;
            }

            else if (ddltype.SelectedValue == "L")
            {
                lbluniversal.Text = "Didisposisi Kepada:";
                lbluniversal.Visible = true;
                txtbeli.Visible = true;
                txtbeli.Visible = true;
                lbltanggalbeli.Visible = false;
                txttglpertama.Visible = false;
                lbltanggalbeliakhir.Visible = false;
                txttglbeliakhir.Visible = false;
                lblhargapembelian.Visible = false;
                txthargapembelian.Visible = false;
                lblakumulasi.Visible = false;
                txtakumulasisusut.Visible = false;
                lblpenambahanharga.Visible = false;
                txtpenambahanharga.Visible = false;
                lblpenguranganharga.Visible = false;
                txtpenguranganharga.Visible = false;

                lbltglakhirsusut.Visible = false;
                txtakhirsusut.Visible = false;
                lblmasamanfaat.Visible = false;
                txtmasa.Visible = false;
                lblhargaperolehan.Visible = true;
                txtharga.Visible = true;
                lblhargaperolehan.Text = "Harga Disposisi";
                lblnilai.Visible = false;
                txtResidu.Visible = false;
                lblketerangan.Visible = true;
                txtketerangan.Visible = true;
                lblumur.Visible = false;
                txtumur.Visible = false;
                lblmulaisusut.Visible = false;
                txtmulaisusut.Visible = false;

                txtakhirsusut.Enabled = false;
                txtmasa.Enabled = false;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

}