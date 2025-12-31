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
public partial class AE_Process_MasukTrnAngsuranKasbon : System.Web.UI.Page
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
    string form = "R3";
    string kodesumber = "AE";
    string KodeKonter = "AE03";
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
    }
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
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
        try

        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("AE_Trn_Kasbon_Bayar", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


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
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    blnadd = 1;
                    GRTP.Enabled = false;


                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                   
                        GRTP.Enabled = true;
                        btndelete.Visible = true;
                        txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                        txttgltrn.Enabled = false;
                        var hapusflag= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;

                    Hdncabang.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                        hdncustomer.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    txtKeterangan.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                        txtbayar.Enabled = false;
                        btnSimpan.Visible = false;


                        PnlFront.Visible = false;
                        pnladd.Visible = true;

                        pnlHeader.Visible = false;



                        pnltrn.Visible = true;
                        Pnlfooter4.Visible = true;


                    var kodelgn = hdncustomer.Value;
                    txtnorekening.Text = HDNoRekening.Value;


                    var kodekry = hdncustomer.Value;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, kodekry, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIAEMSTBARU", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, Hdncabang.Value, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", var, "constrmf");
                    //Boolean status = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["stl"].ToString());
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {

                        txtkodekaryawan.Text = kodekry;
                        txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
                        txtbataspinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonkasbon"].ToString()));
                        txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTerima"].ToString()).ToString("dd-MM-yyyy");
                        txtmasakerja.Text = ge.ListDataset.Tables[0].Rows[0]["masakerja"].ToString();
                        ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
                        ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();
                        //DROPDOWNLIST TYPE JABATAN
                        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[4];
                        var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
                        var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
                        var4[3] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbag.SelectedValue, typeof(string));
                        gex.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
                        gex.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

                        ddljab.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljab.DataTextField = "kdket";
                        ddljab.DataValueField = "KodeJab";
                        ddljab.DataBind();
                        ddljab.Items.Insert(0, new ListItem("Pilih Data", "-"));

                        ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                  
                        ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                        ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();
                        txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhNetGaji"].ToString()));
                        txtsaldobelumlunas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["blmlunas"].ToString()));
                        txtlimitkasbon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonkasbon"].ToString()) - decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["blmlunas"].ToString()));

                        pnladd.Visible = true;
                        //Pnlfooter4.Visible = true;
                        txtKeterangan.Text = "Bayar Kasbon";
                        btnSimpan.Enabled = true;

                    }


                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTINVOICE", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, Hdncabang.Value, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AE_Trn_Kasbon_Bayar", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        ViewState["ListDetailBayar"] = ge.ListDataTable;

                        GRTP.DataSource = ge.ListDataTable;
                        GRTP.DataBind();

                        GRTP.UseAccessibleHeader = true;
                        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                        cssGridBayar();
                    }
                    else
                    {
                        SetDetailBayar();
                    }


                    GRTP.Enabled = false;

                    if (hapusflag == "False")
                    {
                        btndelete.Visible = true;

                    }
                    else
                    {
                        btndelete.Visible = false;
                    }

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":

              
                break;
            case "PDF":

                try
                {

                    ReportDocument rprt = new ReportDocument();
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_AR_Karyaawan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
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
            case "SLIP":

                try
                {

                    ReportDocument rprt = new ReportDocument();
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    typetrn = "B";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAEBYR", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AE_Trn_AEreport", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/AE/Report/CR/CLKwitansimasuk.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                     
                            //rprt.SetParameterValue("Reportname", "Pembayaran Karyawan");
                      

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

        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txtbayar.Text);

            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Bayar Masih Kosong!');", true);
            }
            else
            {

                dtBayar.Columns.Clear();
                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("TglJthTmp", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Bulan", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Tahun", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Angsuran", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Denda", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));

                dtBayar.Rows.Clear();
                int xi = 0;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {
                    DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");


                    if (ddlbayar.SelectedValue != "0")
                    {
                        DataRow dr1 = dtBayar.NewRow();
                        xi = xi + 1;
                        int xangske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangs")).Text);
                        decimal xpokok = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblPokok")).Text);
                        decimal xbunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblBunga")).Text);
                        decimal xangsuran = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblAngsuran")).Text);
                        decimal xdenda = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lbldenda")).Text);
                        decimal xbayar = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblbayar")).Text);
                        int xbulan = int.Parse(((Label)GRTP.Rows[i].FindControl("Lblbulan")).Text);
                        int xtahun = int.Parse(((Label)GRTP.Rows[i].FindControl("Lbltahun")).Text);

                        DateTime tgljthtempo = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text);
                        dr1["No"] = xi;
                        dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                        dr1["TglJthTmp"] = tgljthtempo;
                        dr1["Produk"] = "";
                        dr1["Angske"] = xangske;
                        dr1["Bulan"] = xbulan;
                        dr1["Tahun"] = xtahun;
                        dr1["Pokok"] = xpokok;
                        dr1["Bunga"] = xbunga;
                        dr1["Angsuran"] = xangsuran;
                        dr1["Denda"] = xdenda;
                        dr1["Bayar"] = xbayar;
                        dr1["TypeBayar"] = ddlbayar.SelectedValue;
                        dtBayar.Rows.Add(dr1);
                    }
                }


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[14];
                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayar.Text, typeof(decimal));
                Params[3] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                Params[4] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[6] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, Hdncabang.Value, typeof(string));
                Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[10] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[11] = gb.CreateSPParameter("@AE_Kasbon_bayar", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[12] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtKeterangan.Text, typeof(string));
                Params[13] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("Ae_Kasbon_Bayar_Delete", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;

                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','KasBon','bottom-center','success','1');", true);

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
        //switch (e.CommandName)
        //{
        //    case "DEL":
        //        int dgcount = GRRP.Rows.Count;


        //        ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

        //        dtRP.Columns.Clear();

        //        dtRP = (DataTable)ViewState["ListDetailRTP"];

        //        DataRow drCurrentRow = null;

        //        dtRP.Rows.Clear();

        //        int j = 1;

        //        for (int i = 0; i < GRTP.Rows.Count; i++)
        //        {

        //            if (((Label)GRTP.Rows[i].FindControl("lblNumber")).Text != "")
        //            {


        //                drCurrentRow = dtRP.NewRow();

        //                drCurrentRow["nomor"] = j;
        //                drCurrentRow["TglTrnFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lbltglfaktur")).Text;
        //                drCurrentRow["NoFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
        //                drCurrentRow["TypeTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
        //                drCurrentRow["TglJthTmp"] = ((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text;
        //                drCurrentRow["Keterangan"] = ((TextBox)GRTP.Rows[i].FindControl("lblketJPF")).Text;
        //                drCurrentRow["JumlahTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbljmlhtrn")).Text;
        //                drCurrentRow["KursStd"] = ((TextBox)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text;

        //                dtRP.Rows.Add(drCurrentRow);

        //                j++;
        //            }
        //        }

        //        if (dgcount == 1)
        //        {
        //            DataRow drdnew = dtRP.NewRow();

        //            drdnew["nomor"] = 1;
        //            drdnew["TglTrnFaktur"] = string.Empty;
        //            drdnew["NoFaktur"] = string.Empty;
        //            drdnew["TypeTrn"] = string.Empty;
        //            drdnew["TglJthTmp"] = string.Empty;
        //            drdnew["Keterangan"] = string.Empty;
        //            drdnew["JumlahTrn"] = decimal.Zero;
        //            drdnew["KursStd"] = decimal.Zero;
        //            dtRP.Rows.Add(drdnew);
        //        }


        //        ViewState["ListDetailRTP"] = dtRP;

        //        GRTP.DataSource = dtRP;
        //        GRTP.DataBind();

        //        GRTP.UseAccessibleHeader = true;
        //        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //        cssGridBayar();



        //        break;


        //}
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
            lbltotalPokok.Text = string.Format("{0:N2}", pokok);

            Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
            lbltotalBunga.Text = string.Format("{0:N2}", bunga);

            Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
            lbltotalAngsuran.Text = string.Format("{0:N2}", angsuran);

            Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
            lbltotaldenda.Text = string.Format("{0:N2}", denda);



        }
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txtbayar.Text);

            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Bayar Masih Kosong!');", true);
            }
            else
            {

            dtBayar.Columns.Clear();
                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("TglJthTmp", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Bulan", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Tahun", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Angsuran", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Denda", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));

                dtBayar.Rows.Clear();
                int xi = 0;

            for (int i = 0; i < GRTP.Rows.Count; i++)
            {
                 DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");


                if (ddlbayar.SelectedValue !="0")
                {
                    DataRow dr1 = dtBayar.NewRow();
                        xi = xi + 1;
                    int xangske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangs")).Text);
                    decimal xpokok = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblPokok")).Text);
                    decimal xbunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblBunga")).Text);
                    decimal xangsuran = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblAngsuran")).Text);
                    decimal xdenda = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lbldenda")).Text);
                    decimal xbayar = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblbayar")).Text);
                    int xbulan = int.Parse(((Label)GRTP.Rows[i].FindControl("Lblbulan")).Text);
                        int xtahun = int.Parse(((Label)GRTP.Rows[i].FindControl("Lbltahun")).Text);

                        DateTime tgljthtempo = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text);
                    dr1["No"] = xi;
                    dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                    dr1["TglJthTmp"] = tgljthtempo;
                    dr1["Produk"] = "";
                    dr1["Angske"] = xangske;
                    dr1["Bulan"] = xbulan;
                    dr1["Tahun"] = xtahun;
                    dr1["Pokok"] = xpokok;
                    dr1["Bunga"] = xbunga;
                    dr1["Angsuran"] = xangsuran;
                    dr1["Denda"] = xdenda;
                    dr1["Bayar"] = xbayar;
                    dr1["TypeBayar"] = ddlbayar.SelectedValue;
                    dtBayar.Rows.Add(dr1);
                }
            }


                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[13];
                    Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                    Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayar.Text, typeof(decimal));
                    Params[3] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                    Params[4] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                    Params[6] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, Hdncabang.Value, typeof(string));
                    Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));               
                    Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[10] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[11] = gb.CreateSPParameter("@AE_Kasbon_bayar", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                    Params[12] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtKeterangan.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("Ae_Kasbon_Bayar_Insert", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;
                       
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

  
  
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
       
        }

    }

  
 
    public void btnSerach_Click(object sender, EventArgs e)
    {

        try
        {

       
        var kodelgn = hdncustomer.Value;
        txtnorekening.Text = HDNoRekening.Value;
        

        var kodekry = hdncustomer.Value;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, kodekry, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIAEMSTBARU", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, Hdncabang.Value, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", var, "constrmf");
        //Boolean status = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["stl"].ToString());
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
           
            txtkodekaryawan.Text = kodekry;
            txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
            txtbataspinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonkasbon"].ToString()));
            txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTerima"].ToString()).ToString("dd-MM-yyyy");
            txtmasakerja.Text = ge.ListDataset.Tables[0].Rows[0]["masakerja"].ToString();
            ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
            ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();

                //DROPDOWNLIST TYPE JABATAN
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[4];
                var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
                var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
                var4[3] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbag.SelectedValue, typeof(string));
                gex.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
                gex.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

                ddljab.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                ddljab.DataTextField = "kdket";
                ddljab.DataValueField = "KodeJab";
                ddljab.DataBind();
                ddljab.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
              
            ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
            ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();
            txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhNetGaji"].ToString()));
            txtsaldobelumlunas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["blmlunas"].ToString()));
            txtlimitkasbon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonkasbon"].ToString()) - decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["blmlunas"].ToString()));
            
            pnladd.Visible = true;
            //Pnlfooter4.Visible = true;
            txtKeterangan.Text = "Bayar Kasbon";
            btnSimpan.Enabled = true;
           
        }
        SetDetailBayar();

        string customer = hdncustomer.Value;
        string norekening = HDNoRekening.Value;
        pnltrn.Visible = true;

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
        var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
        var1[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, Hdncabang.Value, typeof(String));

        var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTRANSAKSIPINJAMAN", typeof(string));
        var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AE_Trn_Kasbon_Bayar", var1, "constrmf").Tables[0];

        if (ge.ListDataTable.Rows.Count > 0)
        {

            ViewState["ListDetailBayar"] = ge.ListDataTable;

            GRTP.DataSource = ge.ListDataTable;
            GRTP.DataBind();

            GRTP.UseAccessibleHeader = true;
            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

            cssGridBayar();
        }
        else
        {
            SetDetailBayar();
        }


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    private void cssGridBayar()
    {

        // Attribute to show the Plus Minus Button.
        GRTP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRTP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-class", "expand");
        cells[4].Attributes.Add("data-class", "expand");
        cells[5].Attributes.Add("data-class", "expand");
        cells[6].Attributes.Add("data-class", "expand");
        cells[7].Attributes.Add("data-class", "expand");
        cells[8].Attributes.Add("data-class", "expand");
        cells[9].Attributes.Add("data-class", "expand");

    }

    private void SetDetailBayar()
    {

        //DataRow dr = null;     
        dtBayar.Columns.Clear();
        dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("TglJthTmp", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));

        dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Angsuran", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Denda", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Bayar", typeof(string)));



        ViewState["ListDetailBayar"] = dtBayar;
        GRTP.DataSource = dtBayar;
        GRTP.DataBind();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridBayar();
    }

    protected void ddlbayar_SelectedIndexChanged(object sender, EventArgs e)
    {

       
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;



        if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "0")
        {
            ((Label)grow.FindControl("lblbayar")).Text = "0";
 
        }
        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue=="1")
        {
            ((Label)grow.FindControl("lblbayar")).Text = ((Label)grow.FindControl("lblAngsuran")).Text;
        }

        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "2")
        {
            decimal angsuran = 0;
            decimal denda = 0;
            decimal total = 0;
            
            angsuran = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
            denda = decimal.Parse(((Label)grow.FindControl("lbldenda")).Text);
            total = angsuran + denda;
            ((Label)grow.FindControl("lblbayar")).Text = string.Format("{0:N2}", total);

        }

         
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        subgridbayar_Click();

    }
    private void subgridbayar_Click()
    {

        int dgcount = GRTP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        if (dgcount > 0)
        {


            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                jlhgl = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblbayar")).Text);
                sumjlhgl = sumjlhgl + jlhgl;
            }



        }


        else
        {
            sumjlhgl = 0;
        }
        string formatted = string.Format("{0:N2}", sumjlhgl);
        txtbayar.Text = formatted;




    }
    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_getkaryawan_angsuran_kasbon", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }







    protected void hdncustomer_ValueChanged(object sender, EventArgs e)
    {

    }
}
