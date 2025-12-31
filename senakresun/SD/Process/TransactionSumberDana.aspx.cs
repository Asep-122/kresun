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

public partial class SD_Process_TransactionSumberDana : System.Web.UI.Page
{

    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
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
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    ////DECLARE A "REPORT" AND A "REPORT VIEWER".
    //protected CrystalDecisions.CrystalReports.Engine.ReportDocument Report =
    //    new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    //protected CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer1 =
    //    new CrystalDecisions.Web.CrystalReportViewer();
    //Reports rpt = new Reports();


    DataTable dtRP = new DataTable("Transaction");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gb1 = new Global();
    GlobalEntities ge1 = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    string statusclosing, statusposting;
    string form = "D1";
    string kodesumber = "AP";
    string KodeKonter = "AP03";
    public string nobuktidb;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (!Page.IsPostBack)
            //{
            //    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
            //    Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

            //    CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;


            //    BindGridEntity();
            //}
                        //culture
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                //culture
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";

                BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttglTransaksi.Text = xtanggal.ToString("dd-MM-yyyy");
                txtTglJthTempo.Text = xtanggal.ToString("dd-MM-yyyy");
                //get tanggal server

                var KdCompany = SessKdpersh;
                var admin = SessLoginID;



                //DROPDOWNLIST Kode Supplier
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[8];
                var1[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var1[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var1[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var1[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORSUPPLIER", typeof(string));
                var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var1[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";

                ddlkdsupplier.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkdsupplier.DataTextField = "KodeBank";
                ddlkdsupplier.DataValueField = "KodeBank";
                ddlkdsupplier.DataBind();
                ddlkdsupplier.Items.Insert(0, new ListItem("Pilih Data", "-"));

               

                //DROPDOWNLIST Type Transaksi
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[8];
                var3[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var3[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var3[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var3[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var3[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANS", typeof(string));
                var3[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var3[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var3[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", var3, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn asc";

                ddltypetrans.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddltypetrans.DataTextField = "TypeTrans";
                ddltypetrans.DataValueField = "TypeTrn";
                ddltypetrans.DataBind();
                ddltypetrans.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //DROPDOWNLIST NoFaktur
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[8];
                var4[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkdsupplier.SelectedValue, typeof(string));
                var4[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var4[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var4[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var4[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORNOFAKTUR", typeof(string));
                var4[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var4[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var4[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", var4, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "NoFaktur asc";

                ddlNoFaktur.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlNoFaktur.DataTextField = "NoFaktur";
                ddlNoFaktur.DataValueField = "NoFaktur";
                ddlNoFaktur.DataBind();
                ddlNoFaktur.Items.Insert(0, new ListItem("Pilih Data", "-"));
                //txtnofaktur.Text = ddlNoFaktur.SelectedValue;

                //DROPDOWNLIST CURRENCY
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[8];
                var5[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var5[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var5[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var5[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var5[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCY", typeof(string));
                var5[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var5[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var5[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", var5, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";

                ddlCurrency.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlCurrency.DataTextField = "Currency";
                ddlCurrency.DataValueField = "KodeCrc";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ////DROPDOWNLIST JURNAL
                //DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[8];
                //var6[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                //var6[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                //var6[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                //var6[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                //var6[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORJURNAL", typeof(string));
                //var6[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var6[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var6[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", var6, "constrmf");

                //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";

                //ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlJurnal.DataTextField = "kdJurnal";
                //ddlJurnal.DataValueField = "KodeJurnal";
                //ddlJurnal.DataBind();
                //ddlJurnal.Items.Insert(0, new ListItem("Pilih Data", "-"));

                if (SessSystemKurs == "False")
                {
                    ddlCurrency.SelectedValue = "IDR";
                    ddlkurs.Items.Insert(0, new ListItem("1"));
                    txtKurs.Text = "1";
                    ddlCurrency.Enabled = false;
                    ddlkurs.Enabled = false;
                    txtKurs.Enabled = false;

                }
                else
                {
                    ddlCurrency.SelectedValue = "IDR";
                    ddlCurrency.Enabled = true;
                    ddlkurs.Items.Insert(0, new ListItem("1"));
                    txtKurs.Text = "1";
                    ddlkurs.Enabled = true;
                    txtKurs.Enabled = true;
                    ddlCurrency.Enabled = true;

                }
                //if (ddlCurrency.SelectedValue == "IDR")
                //{ 
                //ddlkurs.Items.Insert(0, new ListItem("1"));
                //txtKurs.Text = "1";
                //}
                //else
                //{ 
                //    ddlkurs.Items.Insert(0, new ListItem("1"));
                //    txtKurs.Text = "1";
                //}
            }
            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

            //string JmlhRupiah = txtjmlrp.Text;
            txtjmlrp.Text = hfjmlrp.Value == "" ? "0" : hfjmlrp.Value; 
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }  
   
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (rbJurnal.SelectedIndex == 0)
        {
            FileJurnal.Visible = true;
            Pnlfooter.Visible = true;
            PnlJournal.Visible = false;
        }
        else if (rbJurnal.SelectedIndex == 1)
        {
            FileJurnal.Visible = false;
            Pnlfooter.Visible = false;
            PnlJournal.Visible = true;
        }
        else
        {
            FileJurnal.Visible = false;
            Pnlfooter.Visible = true;
            PnlJournal.Visible = false;
        }
    }
    protected void ddlKdSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlkdsupplier.SelectedValue != "-")
        { 
            var slcWil = ddlkdsupplier.SelectedValue.Substring(0, 3);
            var slcID = ddlkdsupplier.SelectedValue.Substring(3);
            var kdCabang = SessBranchID;
            var PIC = SessLoginID;
            var KdCompany = SessKdpersh;

            //DROPDOWNLIST NoFaktur
            DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[8];
            var4[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkdsupplier.SelectedValue, typeof(string));
            var4[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            var4[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            var4[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            var4[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORNOFAKTUR", typeof(string));
            var4[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var4[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var4[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", var4, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "NoFaktur asc";

            ddlNoFaktur.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlNoFaktur.DataTextField = "NoFaktur";
            ddlNoFaktur.DataValueField = "NoFaktur";
            ddlNoFaktur.DataBind();
            ddlNoFaktur.Items.Insert(0, new ListItem("Pilih Data", "-"));
            //txtnofaktur.Text = ddlNoFaktur.SelectedValue;


            //TEXTBOX AUTO NAMA SUPPLIER
            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[8];
            varT[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkdsupplier.SelectedValue, typeof(string));
            varT[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, slcID, typeof(string));
            varT[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            varT[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            varT[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNAMASUPPLIER", typeof(string));
            varT[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varT[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varT[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varT, "constrmf");

            string SupplierName = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            string kdSupplier = ge.ListDataset.Tables[0].Rows[0]["kodesales"].ToString();
            string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
            string nmcrc = ge.ListDataset.Tables[0].Rows[0]["NamaCrc"].ToString();
            txtnmsupplier.Text = SupplierName;
            hdfKodeSales.Value = kdSupplier;
            ddlCurrency.SelectedValue = kdcrc;
            txtnegara.Text = nmcrc;
            var kdlgn = ddlkdsupplier.SelectedValue;

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KetKonter", ParameterDirection.Input, SqlDbType.VarChar, 40, kdlgn, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATESYARATBAYAR", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("[SD_Transaksi]", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count != 0)
            {
                string SyaratBayar = ge.ListDataset.Tables[0].Rows[0]["SyaratBayar"].ToString();

                var ket = SyaratBayar.Substring(0, 1);
                var dateMonth = SyaratBayar.Substring(1);
                DateTime adddays;
                DateTime addmonth;
                if (ket == "D")
                {
                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(double.Parse(dateMonth));
                    txtTglJthTempo.Text = adddays.ToString("dd-MM-yyyy");
                }
                else if (ket == "M")
                {
                    addmonth = DateTime.Parse(txttglTransaksi.Text).AddMonths(int.Parse(dateMonth));
                    txtTglJthTempo.Text = addmonth.ToString("dd-MM-yyyy");
                }
                else if (ket == "W")
                {
                    int x, y, z;
                    x = int.Parse(dateMonth);
                    y = 7;
                    z = x * y;

                    var jmlhtgl = Convert.ToString(z);

                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(int.Parse(jmlhtgl));
                    txtTglJthTempo.Text = adddays.ToString("dd-MM-yyyy");
                }
            }
   
        }
        else
        {
            txtnmsupplier.Text = "-";
        }
    }
    protected void ddlNoBukti_SelectedIndexChanged(object sender, EventArgs e)
    {
        //var slcWil = ddlNoBukti.SelectedValue; 
        

    }
    protected void ddlTypTrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        var Typetrn = ddltypetrans.SelectedValue;

        if(Typetrn != "-")
        { 

            //TEXTBOX AUTO TYPE TRANSAKSI KETERANGAN
            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[6];
            varT[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, Typetrn, typeof(string));
            varT[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            varT[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            varT[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            varT[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANSKET", typeof(string));
            varT[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varT, "constrmf");

            string KetTypeTransKet = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();
            txttypetrans.Text = KetTypeTransKet;
            txtKet.Text = KetTypeTransKet;
         
            //if(Typetrn == "O" || Typetrn == "P" || Typetrn == "Q" || Typetrn =="S" )
            //{
            //    txtjmltrans.Text = "-";
            //}

            if(Typetrn == "A")
            {
                rbJurnal.Enabled = false;
                rbJurnal.Visible = false;
                typejur.Visible = false;
            }
            else
            { 
                rbJurnal.Enabled = true;
                rbJurnal.Visible = true;
                typejur.Visible = true;
            }
        }
        else
        {
            txttypetrans.Text = "-";
        }

    } 
    protected void ddlcurrency_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlkurs.Items.Clear();
        string cur = ddlCurrency.SelectedValue;
        //TEXTBOX AUTO CURRENCY
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[7];
        varT[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, cur, typeof(string));
        varT[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        varT[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        varT[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        varT[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYKET", typeof(string));
        varT[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varT[6] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varT, "constrmf");

        string CurrencyKet = ge.ListDataset.Tables[0].Rows[0]["NamaCrc"].ToString();
        string Cur = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
        txtnegara.Text = CurrencyKet;
        if (ddlCurrency.SelectedValue == "IDR")
        {
            txtKurs.Text = "1";
            if (txtjmltrans.Text == "")
            {
                txtjmlrp.Text = "0";

            }
            else
            {
                //Int64 x, y, z;
                //x = Int64.Parse(txtKurs.Text);
                //y = Int64.Parse(txtjmltrans.Text);
                //z = x * y;
                //var jmlrp = Convert.ToString(z);
                //txtjmlrp.Text = jmlrp;

                decimal j, k, z;
                j = decimal.Parse(txtKurs.Text);
                k = decimal.Parse(txtjmltrans.Text);
                z = j * k;
                string formatted = string.Format("{0:N2}", z);
                txtjmlrp.Text = formatted;
            }
        }
        else
        {
            try
            {

                var tanggal = txttglTransaksi.Text;

                DefaultClass.Global.SqlHelper[] varCrc = new DefaultClass.Global.SqlHelper[5];
                varCrc[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Parse(txttglTransaksi.Text).ToString("yyyy-MM-dd"), typeof(string));
                varCrc[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlCurrency.SelectedValue, typeof(string));
                varCrc[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYUSDVALUE", typeof(string));
                varCrc[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)); 
                varCrc[4] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varCrc, "constrmf");

                txtKurs.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["KursStd"].ToString()));

                //decimal j, k, z;
                //j = decimal.Parse(txtKurs.Text);
                //k = decimal.Parse(txtjmltrans.Text);
                //z = j * k;
                //string formatted = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["KursStd"].ToString()));
                //txtjmlrp.Text = formatted;

                //if (crc != "0")
                //{
                //    txtKurs.Text = crc.Replace(".0000","").Replace(",0000", "");
                //}
                if (txtjmltrans.Text =="")
                {
                    txtjmlrp.Text = "0";

                }
                else
                {
                    //Int64 x, y, z;
                    //x = Int64.Parse(txtKurs.Text);
                    //y = Int64.Parse(txtjmltrans.Text);
                    //z = x * y;
                    //var jmlrp = Convert.ToString(z);
                    //txtjmlrp.Text = jmlrp;
                    decimal j, k, z;
                    j = decimal.Parse(txtKurs.Text);
                    k = decimal.Parse(txtjmltrans.Text);
                    z = j * k;
                    string formatted = string.Format("{0:N2}", z);
                    txtjmlrp.Text = formatted;



                }
            }
            catch (Exception X)
            {
                txtKurs.Text = "0";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Nilai Kurs tidak ada. Silahkan diisi','top-right','success','2');", true); 
            }


        }
        DefaultClass.Global.SqlHelper[] varCrc1 = new DefaultClass.Global.SqlHelper[5];
        varCrc1[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Parse(txttglTransaksi.Text).ToString("yyyy-MM-dd"), typeof(string));
        varCrc1[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlCurrency.SelectedValue, typeof(string));
        varCrc1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYUSDVALUE", typeof(string));
        varCrc1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varCrc1[4] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varCrc1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";


        if (ge.ListDataset.Tables[0].Rows.Count != 0)
        {
            ddlkurs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkurs.DataTextField = "statuskurs";
            ddlkurs.DataValueField = "KursStd";
            ddlkurs.DataBind();
            ddlkurs.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Kurs Tidak Ditemukan, silahkan isi Manual atau update tanggal di table UmCrcDtl');", true);
        }


    }
    protected void ddlJurnal_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Jur = ddlJurnal.SelectedValue;
        ////TEXTBOX AUTO CURRENCY
        //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[8];
        //varT[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, Jur, typeof(string));
        //varT[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //varT[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //varT[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //varT[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKETJURNAL", typeof(string));
        //varT[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //varT[6] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //varT[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        //ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varT, "constrmf");
        //if (ge.ListDataset.Tables[0].Rows.Count != 0)
        //{
        //    string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["KetJurnal"].ToString();
        //    string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
        //    string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
        //    string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
        //    string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
        //    txtJurnal.Text = KetJurnal;
        //    txtDebit1.Text = DebitGL;
        //    txtDebit2.Text = DebitCC;
        //    txtKredit1.Text = KreditGL;
        //    txtKredit2.Text = KreditCC;

        //    //GET KETERANGAN GL
        //    DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[8];
        //    varGL[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, DebitGL, typeof(string));
        //    varGL[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //    varGL[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //    varGL[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //    varGL[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        //    varGL[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));
        //    varGL[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
        //    varGL[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //    ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varGL, "constrmf");
        //    string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
        //    txtDebit3.Text = DCC;

        //    //GET KETERANGAN GL
        //    DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[8];
        //    varCC[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, KreditGL, typeof(string));
        //    varCC[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //    varCC[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //    varCC[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        //    varCC[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        //    varCC[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));
        //    varCC[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
        //    varCC[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //    ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varCC, "constrmf");
        //    string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
        //    txtKredit3.Text = KGL;
        //}
        //else
        //{
        //    txtJurnal.Text = "";
        //    txtDebit1.Text = "";
        //    txtDebit2.Text = "";
        //    txtKredit1.Text = "";
        //    txtKredit2.Text = "";
        //    txtKredit3.Text = "";
        //    txtDebit3.Text = "";
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionSumberDana.aspx");
    }
    public void BindGridEntity()
    { 
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];

        var1[0] = gb.CreateSPParameter("@KodeSupplier", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string)); 
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SD_Transaksi", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("TransactionAccountPayable.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        Pnlfooter.Visible = true;
        PnlJournal.Visible = false; 
        if (rbJurnal.SelectedIndex == 1)
        {
            PnlJournal.Visible = true;
            Pnlfooter.Visible = false;
        }
        else
        {
            Pnlfooter.Visible = true;
            PnlJournal.Visible = false;
        }

        if (ddlkdsupplier.Enabled == false && rbJurnal.SelectedValue == "2")
        {
            PnlJournal.Visible = false;
            Pnlfooter.Visible = true;
        }
        loaddata();

    }

    protected void Btnnextjournal_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = false;
        PnlJournal.Visible = false; 


        PnlGrid.Visible = true;
        Pnlfooter.Visible = true; 
        if (rbJurnal.SelectedIndex == 1)
        {
            Pnlfooter.Visible = true;
            PnlJournal.Visible = false;
        }
        else
        {
            PnlJournal.Visible = true;
            Pnlfooter.Visible = false;
        }
        //  
        lnkAddRP2_Click();
    }

    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        if(pnladd.Visible == true)
        { 
            Response.Redirect("TransactionAccountPayable.aspx"); 
        }
        else
        {
            pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;
            Pnlfooter.Visible = true;
            PnlJournal.Visible = false;
            PnlGrid.Visible = false; 
            if (rbJurnal.SelectedIndex == 1)
            {
                PnlJournal.Visible = true;
                Pnlfooter.Visible = false;
            }
            else
            {
                Pnlfooter.Visible = true;
                PnlJournal.Visible = false;
            }
          //  loaddata();
        }


    }

    protected void btnbackheader2_Click(object sender, EventArgs e)
    {


        Response.Redirect("TransactionAccountPayable.aspx");


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


        if (GRRP.Rows.Count == 0)
        {
            dr = dtRP.NewRow();
            dr["nomor"] = 1;
            dr["KodeGL"] = string.Empty;
            dr["NamaGL"] = string.Empty;
            dr["Description"] = txtKet.Text;
            dr["Costcenter"] = string.Empty;
            dr["jlhdebit"] = decimal.Zero;
            dr["jlhkredit"] = decimal.Zero;
        }
        else { 
            dr = dtRP.NewRow();
            dr["nomor"] = 1;
            dr["KodeGL"] = string.Empty;
            dr["NamaGL"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Costcenter"] = string.Empty;
            dr["jlhdebit"] = decimal.Zero;
            dr["jlhkredit"] = decimal.Zero;
        }
        dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRP"] = dtRP;


        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();






    }
    private void loaddata()
    {
       // txtjmltrans.Text = "0";
    }


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


    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try 
                { 
                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    PnlJournal.Visible = false;
                    Pnlfooter.Visible = true;
                    loaddata();

                    txtnmsupplier.Enabled = false;
                    ddlkdsupplier.Enabled = false;
                    txtnmsupplier.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnmsupplier")).Text;
                    ddlkdsupplier.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsupplier")).Text;
                    //DROPDOWNLIST NoFaktur
                    DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[8];
                    var4[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkdsupplier.SelectedValue, typeof(string));
                    var4[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                    var4[2] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                    var4[3] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                    var4[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORNOFAKTUR", typeof(string));
                    var4[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var4[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var4[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", var4, "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "NoFaktur asc";

                    ddlNoFaktur.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlNoFaktur.DataTextField = "NoFaktur";
                    ddlNoFaktur.DataValueField = "NoFaktur";
                    ddlNoFaktur.DataBind();
                    ddlNoFaktur.Items.Insert(0, new ListItem("Pilih Data", "-"));
                    //txtnofaktur.Text = ddlNoFaktur.SelectedValue;


                    txtKodeBukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketnobukti")).Text;
                    //ddlNoBukti.SelectedValue = txtKodeBukti.Text.Substring(0, 4); 
                    string ddltypetrn = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypetrans")).Text;
                    ddltypetrans.SelectedValue = ddltypetrn;
                    ddlNoFaktur.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnofaktur")).Text;
                    txtnofaktur.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnofaktur")).Text;
                    txttglTransaksi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgltrn")).Text;
                    txtTglJthTempo.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljthtempo")).Text;
                    txttglentry.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglPIC")).Text;
                    txtEntry.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPIC")).Text;
                    string ddlcrc = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblCurr")).Text;
                    ddlCurrency.Items.Insert(0, new ListItem(ddlcrc));
                    if (ddlcrc == "IDR")
                    {
                        txtnegara.Text = "Indonesia Rupiah";
                    }
                    else
                    {
                        txtnegara.Text = "United Stated Dollar";
                    }

                    //txtKurs.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkurs")).Text;
                    //txtjmltrans.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblJmlTrans")).Text;


                    HdnddlTypetrans.Value = ddltypetrn; //ddltypetrans.SelectedValue;


                    decimal j, k, z;
                    j = decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkurs")).Text);
                    k = decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblJmlTrans")).Text);
                    z = j * k;
                    txtKurs.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkurs")).Text));
                    txtjmlrp.Text = string.Format("{0:N2}", z);
                    txtjmltrans.Text= string.Format("{0:N2}", z);
                    HdnddlJlhrp.Value = txtjmlrp.Text;
                    hfjmlrp.Value = txtjmlrp.Text;
                   
                    txtKet.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblket")).Text;
                    rbJurnal.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypejurnal")).Text;
                    ddlkurs.Items.Insert(0, new ListItem(txtKurs.Text));

                    //GETTRANSTYPEKET
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 8, ddltypetrn, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANSKET", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", var, "constrmf");

                    string typetrn = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();
                    txttypetrans.Text = typetrn;

                    if(ddltypetrans.SelectedValue == "A")
                    {
                        rbJurnal.Enabled = false;
                        rbJurnal.Visible = false;
                        typejur.Visible = false;
                    }
                    else
                    {
                        rbJurnal.Enabled = true;
                        rbJurnal.Visible = true;
                        typejur.Visible = true;
                    }
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;
                   if (statusclosing == "True")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                        btnSimpan.Enabled = false;
                    }
                    else
                    {
                        if (statusposting == "True")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting hubungi pusat!');", true);
                            btnSimpan.Enabled = false;
                        }

                    }

                    if (rbJurnal.SelectedValue == "1")
                    {
                        FileJurnal.Visible = true;

                        string ddljurnal = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljurnal1")).Text;
                        ddlJurnal.SelectedValue = ddljurnal;
                        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5]; 
                        varT[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddljurnal, typeof(string));
                        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKETJURNAL", typeof(string));
                        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varT[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varT[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varT, "constrmf");

                        string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["KetJurnal"].ToString();
                        string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
                        string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
                        string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
                        string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
                        txtJurnal.Text = KetJurnal;
                        txtDebit1.Text = DebitGL;
                        txtDebit2.Text = DebitCC;
                        txtKredit1.Text = KreditGL;
                        txtKredit2.Text = KreditCC;

                        //GET KETERANGAN GL
                        DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[5];
                        varGL[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, DebitGL, typeof(string));
                        varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
                        varGL[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varGL[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        varGL[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varGL, "constrmf");
                        string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
                        txtDebit3.Text = DCC;

                        //GET KETERANGAN CC
                        DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[5];
                        varCC[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, KreditGL, typeof(string));
                        varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
                        varCC[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varCC[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        varCC[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varCC, "constrmf");
                        string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
                        txtKredit3.Text = KGL;

                    }
                    else if (rbJurnal.SelectedValue == "2")
                    {
                        PnlJournal.Visible = true;
                        Pnlfooter.Visible = false;
                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                        Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKodeBukti.Text, typeof(string)); 
                        Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GLGETTRN", typeof(string));
                        Paramsc[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.SD_DDLTransaksi", Paramsc, "constrmf").Tables[0];

                        if (ge.ListDataTable.Rows.Count > 0)
                        {

                            ViewState["ListDetailRP"] = ge.ListDataTable;

                            GRRP.DataSource = ge.ListDataTable;
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

                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;
                    if (statusclosing == "True")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                        //btnSimpan.Enabled = false;
                    }
                    else if(statusposting == "True")
                    {
                        
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting hubungi pusat!');", true);
                            //btnSimpan.Enabled = false;
                       

                    }

                    else
                    {

                    


                    //ddlkdsupplier.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeWil")).Text; 
                    txtKodeBukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketnobukti")).Text;
                    var kdlgn   = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsupplier")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];


                    var1[0] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[1] = gb.CreateSPParameter("@KodeSupplier", ParameterDirection.Input, SqlDbType.VarChar, 40, kdlgn, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[4] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKodeBukti.Text, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AP_Delete_Transaksi", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))

                    { 
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                    }
                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Posting"))
                    {
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut sudah di posting,izin Pusat!');", true);
                    } 
                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully save this Project','top-right','success','2');", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "PDF":

                ReportDocument rprt = new ReportDocument();
                try
                {

                    txtKodeBukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@KetKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKodeBukti.Text, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTTRNAP", typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SD_Transaksi", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    { 
                        rprt.Load(Server.MapPath("~/AP/Process/Report/AP.STD.RPT.010.rpt"));
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    if (e.CommandName != "PDF")
                    {
                        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                    else if (X.Message != "Thread was being aborted.")
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
        }
    }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         

        string STATE = "";
        string STATEDL = "";
        try
        {
            txtjmltrans.Text = txtjmltrans.Text.Replace("-", "");
            decimal jmltrans = decimal.Parse(txtjmltrans.Text);
            if (jmltrans == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi tidak boleh 0!');", true);
            }
            else
            {

            
            var nobukti = txtKodeBukti.Text;
            var Typetrn = ddltypetrans.SelectedValue;
            var typejurnal = rbJurnal.SelectedValue;
            if (rbJurnal.SelectedValue == "1")
            {
                if (ddlJurnal.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Journal Masih Kosong!');", true);
                }
                else
                {
                    dtRP.Columns.Clear();

                    dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
                    dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
                    dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
                    dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
                    dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));
                    dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
                    dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));

                    dtRP.Rows.Clear();


                    DataRow dr = dtRP.NewRow();
                    dr["nomor"] = 1;
                    dr["KodeGL"] = txtDebit1.Text;
                    dr["NamaGL"] = txtDebit3.Text;
                    dr["Description"] = txtKet.Text;
                    dr["Costcenter"] = txtDebit2.Text;
                    dr["jlhdebit"] = decimal.Parse(txtjmltrans.Text);
                    dr["jlhkredit"] = 0;
                    dtRP.Rows.Add(dr);

                    dr = dtRP.NewRow();
                    dr["nomor"] = 2;
                    dr["KodeGL"] = txtKredit1.Text;
                    dr["NamaGL"] = txtKredit3.Text;
                    dr["Description"] = txtKet.Text;
                    dr["Costcenter"] = txtKredit2.Text;
                    dr["jlhdebit"] = 0;
                    dr["jlhkredit"] = decimal.Parse(txtjmltrans.Text);
                    dtRP.Rows.Add(dr);
                }

            }
            else if (rbJurnal.SelectedValue == "2")
            {
                ddlJurnal.SelectedValue = "-";
                decimal jumlahtotal = 0;
                decimal Gdebit = 0, Gkredit = 0;
                jumlahtotal = decimal.Parse(txtjmlrp.Text);
                for (int i = 0; i < GRRP.Rows.Count; i++)
                {
                    Gdebit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                    Gkredit += decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                }
                if ((Gdebit- Gkredit) != 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Selisih debit dan Kredit!');", true);
                }
                else if (Gdebit != jumlahtotal)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Selisih debit dan Kredit!');", true);
                }
                else
                {
                    dtRP.Columns.Clear();

                    dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
                    dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
                    dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
                    dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
                    dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));
                    dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
                    dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));

                    dtRP.Rows.Clear();


                    for (int i = 0; i < GRRP.Rows.Count; i++)
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
                }
                
               


            }
            else if (rbJurnal.SelectedValue == "3")
            {
                ddlJurnal.SelectedValue = "-";
                dtRP.Columns.Clear();

                dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
                dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
                dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));
                dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));

                dtRP.Rows.Clear();
            }

       
            if (ddlkdsupplier.Enabled == true)
            {
                STATE = "INSERT";
                STATEDL = "INSERTDL";
            }
            else
            {
                STATE = "UPDATE";
                STATEDL = "UPDATEDL";
            }



            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[19];

            Params[0] = gb.CreateSPParameter("@Typejournal", ParameterDirection.Input, SqlDbType.VarChar, 50, rbJurnal.SelectedValue, typeof(string));
            Params[1] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
            Params[2] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKet.Text, typeof(string));
            Params[3] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglTransaksi.Text, typeof(DateTime));
            Params[4] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglJthTempo.Text, typeof(DateTime));
            Params[5] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjmlrp.Text, typeof(decimal));
            Params[6] = gb.CreateSPParameter("@Kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlJurnal.SelectedValue, typeof(string));
            Params[7] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
            Params[8] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[9] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            Params[10] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[11] = gb.CreateSPParameter("@GL_Transaction_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
            Params[12] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[13] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlkdsupplier.SelectedValue, typeof(string));
            Params[14] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
            Params[15] = gb.CreateSPParameter("@xnobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKodeBukti.Text, typeof(string));
            Params[16] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddltypetrans.SelectedValue, typeof(string));
            Params[17] = gb.CreateSPParameter("@kodecrc", ParameterDirection.Input, SqlDbType.VarChar, 30, "IDR", typeof(string));
            Params[18] = gb.CreateSPParameter("@nofaktur", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnofaktur.Text, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SD_Trn_SD_Insert", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

            }
            else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Posting"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut sudah di posting,izin Pusat!');", true);

            }
            else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                    btnSimpan.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                //Response.Redirect("TransactionAccountPayable.aspx");
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        } 
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionAccountPayable.aspx");
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    private static int ConvertToInt(string number)
    {
        number = number.Replace(",", "").Replace(".", "");
        return int.Parse(number);
    }

    private static decimal ConvertToDecimal(string number)
    {
        var ns = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
        ns.NumberDecimalSeparator = ".";

        return decimal.Parse(number, ns);
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


                break;
        }
    }

    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            Params[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNMGL", typeof(String));
            Params[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("dbo.CL_GetCoa", Params, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlnamaGL")), "-", "-");



            DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
            xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            xParams[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKDGL", typeof(String));
            xParams[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));
            gex.ListDataset = gbx.GetDataJustOutPut("dbo.CL_GetCoa", xParams, "constrmf");
            gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");

            //ddlkurs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddlkurs.DataTextField = "statuskurs";
            //ddlkurs.DataValueField = "KursStd";
            //ddlkurs.DataBind();
            //ddlkurs.Items.Insert(0, new ListItem("Pilih Data", "-"));

            DefaultClass.Global.SqlHelper[] xParamsCC = new DefaultClass.Global.SqlHelper[3];
            xParamsCC[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            xParamsCC[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGLCC", typeof(String));
            xParamsCC[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));
            gex.ListDataset = gbx.GetDataJustOutPut("dbo.CL_GetCoa", xParamsCC, "constrmf");
            gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "-", "-");

            ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;
            ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlnamaGL")).Value;

            ((DropDownList)e.Row.FindControl("ddlcostcenter")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlcostcenter")).Value;
            //((TextBox)e.Row.FindControl("txtKT")).Text = HdnTxtKet.Value;


            if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
            {
                ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

            }

            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
    }


    protected void ddlKodeGL_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String)); 
        Params[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));
        ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_SGetCoacode", Params, "constrmf");



        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";


        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlnamaGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        xParams[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");


        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");

        ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue;



        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(string));
        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("SD_Transaksi", var, "constrmf").Tables[0];


        string CC = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();


        if (CC == "True")
        {
            ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = true; 
        }
        else
        {
            ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = false;
            ((DropDownList)grow.FindControl("ddlcostcenter")).Items.Insert( 0,new ListItem("-"));
        } 
        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        // UpdatePanel1.Update();
    }

    protected void btnAdddetail_Click(object sender, EventArgs e)
    {

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
        if ( drdnew["Description"].ToString() =="")
        { 
            drdnew["nomor"] = dgcount + 1;
            drdnew["KodeGL"] = string.Empty;
            drdnew["NamaGL"] = string.Empty;
            drdnew["Description"] = txtKet.Text;
            drdnew["Costcenter"] = string.Empty;
            drdnew["jlhdebit"] = decimal.Zero;
            drdnew["jlhkredit"] = decimal.Zero;
        }
        else
        { 
            drdnew["nomor"] = dgcount + 1;
            drdnew["KodeGL"] = string.Empty;
            drdnew["NamaGL"] = string.Empty;
            drdnew["Description"] = string.Empty;
            drdnew["Costcenter"] = string.Empty;
            drdnew["jlhdebit"] = decimal.Zero;
            drdnew["jlhkredit"] = decimal.Zero; 
        }
        dtRP.Rows.Add(drdnew);

        ViewState["ListDetailRP"] = dtRP;

        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();
    }


    protected void ddlnamaGL_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Params[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_xSGetCoacode", Params, "constrmf");



        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";


        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlKodeGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        xParams[2] = gbx.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");


        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");

        ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue;


        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        // UpdatePanel1.Update();
    }

    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        if (decimal.Parse(((TextBox)grow.FindControl("jlhdebit")).Text.Replace(",","")) > 0)
        {

            ((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
            ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
        }
        else
        {

            ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
            ((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
        }
        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void XXXTxt02_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        if (decimal.Parse(((TextBox)grow.FindControl("jlhkredit")).Text.Replace(",", "")) > 0)
        {

            ((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
        }
        else
        {

            ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
        }

    }
    
    protected void tgljthtempo_textchanged(object sender, EventArgs e)
    {
        ddlkurs.Items.Clear();

        //cek tanggal fiskal
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglTransaksi.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
            txttglTransaksi.Text = xtanggal.ToString("dd-MM-yyyy");
            txtTglJthTempo.Text = txttglTransaksi.Text;
            var kdlgn = ddlkdsupplier.SelectedValue;

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KetKonter", ParameterDirection.Input, SqlDbType.VarChar, 40, kdlgn, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATESYARATBAYAR", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("[SD_Transaksi]", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count != 0)
            {
                string SyaratBayar = ge.ListDataset.Tables[0].Rows[0]["SyaratBayar"].ToString();

                var ket = SyaratBayar.Substring(0, 1);
                var dateMonth = SyaratBayar.Substring(1);
                DateTime adddays;
                DateTime addmonth;
                if (ket == "D")
                {
                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(double.Parse(dateMonth));
                    txtTglJthTempo.Text = adddays.ToString("dd-MM-yyyy");
                }
                else if (ket == "M")
                {
                    addmonth = DateTime.Parse(txttglTransaksi.Text).AddMonths(int.Parse(dateMonth));
                    txtTglJthTempo.Text = addmonth.ToString("dd-MM-yyyy");
                }
                else if (ket == "W")
                {
                    int x, y, z;
                    x = int.Parse(dateMonth);
                    y = 7;
                    z = x * y;

                    var jmlhtgl = Convert.ToString(z);

                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(int.Parse(jmlhtgl));
                    txtTglJthTempo.Text = adddays.ToString("dd-MM-yyyy");
                }
            }
        }
        else
        {

            var kdlgn = ddlkdsupplier.SelectedValue;

           // DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KetKonter", ParameterDirection.Input, SqlDbType.VarChar, 40, kdlgn, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATESYARATBAYAR", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("[SD_Transaksi]", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count != 0)
            {
                string SyaratBayar = ge.ListDataset.Tables[0].Rows[0]["SyaratBayar"].ToString();

                var ket = SyaratBayar.Substring(0, 1);
                var dateMonth = SyaratBayar.Substring(1);
                DateTime adddays;
                DateTime addmonth;
                if (ket == "D")
                {
                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(double.Parse(dateMonth));
                    txtTglJthTempo.Text = adddays.ToString("dd-MM-yyyy");
                }
                else if (ket == "M")
                {
                    addmonth = DateTime.Parse(txttglTransaksi.Text).AddMonths(int.Parse(dateMonth));
                    txtTglJthTempo.Text = addmonth.ToString("dd-MM-yyyy");
                }
                else if (ket == "W")
                {
                    int x, y, z;
                    x = int.Parse(dateMonth);
                    y = 7;
                    z = x * y;

                    var jmlhtgl = Convert.ToString(z);

                    adddays = DateTime.Parse(txttglTransaksi.Text).AddDays(int.Parse(jmlhtgl));
                    txtTglJthTempo.Text = adddays.ToString("dd-MM-yyyy");
                }
            }
        }

        if (SessSystemKurs == "False")
        {
            ddlCurrency.SelectedValue = "IDR";
            ddlkurs.Items.Insert(0, new ListItem("1"));
            txtKurs.Text = "1";
            ddlCurrency.Enabled = false;
            ddlkurs.Enabled = false;
            txtKurs.Enabled = false;

        }
        else
        {
            try
            {
                if (ddlCurrency.SelectedValue == "USD")
                {

                    var tanggal = txttglTransaksi.Text;
                    DefaultClass.Global.SqlHelper[] varCrc1 = new DefaultClass.Global.SqlHelper[5];
                    varCrc1[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Parse(txttglTransaksi.Text).ToString("yyyy-MM-dd"), typeof(string));
                    varCrc1[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlCurrency.SelectedValue, typeof(string));
                    varCrc1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYUSDVALUE", typeof(string));
                    varCrc1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varCrc1[4] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varCrc1, "constrmf");

                    string crc = ge.ListDataset.Tables[0].Rows[0]["KursStd"].ToString();
                    if (crc != "0")
                    {
                        txtKurs.Text = crc.Replace(".0000", "");
                    }

                    Int64 x, y, z;
                    x = Int64.Parse(txtKurs.Text);
                    if (txtjmltrans.Text.Replace("-", "") != "")
                    {
                        y = Int64.Parse(txtjmltrans.Text);
                        z = x * y;
                        var jmlrp = Convert.ToString(z);
                        txtjmlrp.Text = jmlrp;
                    }
                }
                else
                {
                    Int64 x, y, z;
                    x = Int64.Parse(txtKurs.Text);
                    y = Int64.Parse(txtjmltrans.Text);
                    z = x * y;
                    var jmlrp = Convert.ToString(z);
                    txtjmlrp.Text = jmlrp;
                }


            }
            catch (Exception X)
            {
                if (ddlCurrency.SelectedValue == "IDR")
                {
                    txtKurs.Text = "1";
                }
                else
                {
                    txtKurs.Text = "0";
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Nilai Kurs tidak ada. Silahkan diisi','top-right','success','2');", true); 
            }

            DefaultClass.Global.SqlHelper[] varCrc = new DefaultClass.Global.SqlHelper[5];
            varCrc[0] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglTransaksi.Text, typeof(string));
            varCrc[1] = gb.CreateSPParameter("@NamaGroup", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlCurrency.SelectedValue, typeof(string));
            varCrc[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCYUSDVALUE", typeof(string));
            varCrc[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varCrc[4] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SD_DDLTransaksi", varCrc, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";


            if (ge.ListDataset.Tables[0].Rows.Count != 0)
            {
                ddlkurs.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkurs.DataTextField = "statuskurs";
                ddlkurs.DataValueField = "KursStd";
                ddlkurs.DataBind();
                ddlkurs.Items.Insert(0, new ListItem("Pilih Data", "-"));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Kurs Tidak Ditemukan, silahkan isi Manual atau update tanggal di table UmCrcDtl');", true);
            }
        }

        
    }

    protected void ddlNoFaktur_SelectedIndexChanged(object sender,EventArgs e)
    {
        txtnofaktur.Text = ddlNoFaktur.SelectedValue;
        if(ddlNoFaktur.SelectedValue != "-")
        { 
            try
            { 
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                var1[0] = gb.CreateSPParameter("@NoFaktur", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlNoFaktur.SelectedValue, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNOFAKTURTRN", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

                ge.ListDataset = gb.GetDataJustOutPut("[SD_Transaksi]", var1, "constrmf");


                string jmlhtrn = ge.ListDataset.Tables[0].Rows[0]["jmlhtrn"].ToString();
                txtjmltrans.Text = jmlhtrn;
                decimal j, k, z;
                j = decimal.Parse(txtKurs.Text);
                k = decimal.Parse(txtjmltrans.Text);
                z = j * k;
                string formatted = string.Format("{0:N2}", z);
                string formatted1 = string.Format("{0:N2}", k);
                txtjmlrp.Text = formatted;
                txtjmltrans.Text = formatted1;
                hfjmlrp.Value = formatted;
            }
            catch
            {
                txtjmltrans.Text = "0";
                decimal j, k, z;
                j = decimal.Parse(txtKurs.Text);
                k = decimal.Parse(txtjmltrans.Text);
                z = j * k;
                string formatted = string.Format("{0:N2}", z);
                txtjmlrp.Text = formatted;
                hfjmlrp.Value = formatted;
            }
        }
        else
        {
            txtnofaktur.Text = "-";
        }
    }

    protected void ddlkurs_SelectedIndexChanged(object sender,EventArgs e)
    {
        txtKurs.Text = ddlkurs.SelectedValue.Replace(".0000","").Replace(",0000", "");
    }

    protected void kurs_textchanged(object sender,EventArgs e)
    {
        //Int64 x, y, z;
        //x = Int64.Parse(txtKurs.Text);
        //y = Int64.Parse(txtjmltrans.Text.Replace(",",""));
        //z = x * y;
        ////int jmlrp = Convert.ToString(z);
        //string formatted = string.Format(CultureInfo.InvariantCulture, "{0:N0}", z);
        //txtjmlrp.Text = formatted;

        //hfjmlrp.Value = formatted;

        decimal j, k, z;
        j = decimal.Parse(txtKurs.Text);
        k = decimal.Parse(txtjmltrans.Text);
        z = j * k;
        string formatted = string.Format("{0:N2}", z);
        string formatted1 = string.Format("{0:N2}", k);
        txtjmlrp.Text = formatted;
        txtjmltrans.Text = formatted1;
        hfjmlrp.Value = formatted;
    }

    protected void Lnk_Supp(object sender,EventArgs e)
    {

        var nmsup = ddlkdsupplier.SelectedValue;
        if (nmsup !="" || nmsup != "-")
        { 
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
            Params[0] = gb.CreateSPParameter("@KodeSupplier", ParameterDirection.Input, SqlDbType.VarChar, 20, nmsup, typeof(string));
            Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Params[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETDETAILSUPPLIER", typeof(String));
            Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SD_Transaksi", Params, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count != 0)
            {
                lblkdsuppu.Text = nmsup;
                lblnmsuppu.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                lblalamat1pu.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
                lblalamat2pu.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat2"].ToString();
                lblalamat3pu.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat3"].ToString();
                lblkdgrouppu.Text = ge.ListDataset.Tables[0].Rows[0]["KodeGroup"].ToString();
                //lblkdwilpu.Text = ge.ListDataset.Tables[0].Rows[0]["Kodewil"].ToString();
                lblcrcpu.Text = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
                var saldojalan = ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString(); 
                 
                decimal j, k;
                j = decimal.Parse(saldojalan); 
                string formatted = string.Format("{0:N2}", j); 

                lblsldjlnpu.Text = formatted; 

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Supplier tidak ditemukan');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pilih Kode Supplier terlebih dahulu');", true);
        }
    }
}