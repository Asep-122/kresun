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
public partial class CL_Process_MasukTrnKantorBayar2 : System.Web.UI.Page
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
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    DataTable dtRP = new DataTable("Transaction");
    DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "XXX";
    string kodesumber = "KP";
    string KodeKonter = "KP11";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        GRTP.Enabled = true;
      
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        
        txtnobukti.Text = "";
        pnladd.Visible = true;
        txtKeterangan.Text = "-";
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

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                StartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                EndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                


                //KODEBANK
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_KANTOR_BAYAR", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KantorBayar asc";


                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "NamaKantorBayar";
                ddlKdBank.DataValueField = "KantorBayar";
                ddlKdBank.DataBind();
                ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));


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


    protected void ddlKdBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnltrn.Visible = true;
        GRTP.DataSource = null;
        GRTP.DataBind();

        Pnlfooter4.Visible = false;

        var bank = ddlKdBank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_KANTOR_BAYAR", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            //string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            //string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();



            //txtNmBank.Text = NamaBank;

            txtNmBank.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKantorBayar"].ToString();

            txtnamamitra.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekening"].ToString();
            txtnorek.Text = ge.ListDataset.Tables[0].Rows[0]["RekeningKantorBayar"].ToString();
        }
        else
        {
            txtNmBank.Text = "";
            txtnamamitra.Text = "";
            txtnorek.Text = "";





        }

    }

    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_KANTOR_BAYAR", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti desc";


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

            case "SETDEL":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
      
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    GRTP.Enabled = false;
                    pnltrn.Visible = true;
                    btnSimpan.Visible = false;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    //ddlcabang.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblcabangfilter")).Text;
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                    ddlKdBank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    ddlKdBank.Enabled = false;

                    txtnamamitra.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamamitra")).Text;
                    txtnamamitra.Enabled = false;

                    txtNmBank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaKantor")).Text;
                    txtNmBank.Enabled = false;

                    txtnorek.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorek")).Text;
                    txtnorek.Enabled = false;


                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtKeterangan.Enabled = false;
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    //statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblhapusflag")).Text;
                    if (statusposting == "True")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sudah ada Invoice!');", true);
                        btndelete.Enabled = false;
                    }
                    if (statusclosing == "True")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sudah Di posting Journal!');", true);
                        btndelete.Enabled = false;
                    }
                    //var bank = ddlKdBank.SelectedValue;
                    //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    //var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
                    //var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    //var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    //var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
                    //var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    //ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_KANTOR_BAYAR", var, "constrmf");
                    //if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    //{
                    //    //string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaKantorBayar"].ToString();

                    //    //string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();



                    //    txtNmBank.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKantorBayar"].ToString();

                    //    txtnamamitra.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekening"].ToString();
                    //    txtnorek.Text = ge.ListDataset.Tables[0].Rows[0]["RekeningKantorBayar"].ToString();
                    //    //ddlkursbank.SelectedValue = kdcrc;
                    //    //txtkursbank.Text = "1";

                    //}
                    //else
                    //{
                    //    txtNmBank.Text = "";
                    //    txtnamamitra.Text = "";
                    //    txtnorek.Text = "";
                    //    //ddlkursbank.ClearSelection();
                    //    //ddlkursbank.SelectedValue = "IDR";
                    //    //txtkursbank.Text = "1";


                    //}




                    DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[7];
                    Paramst[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramst[1] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                    Paramst[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTINVOICE", typeof(string));
                    Paramst[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramst[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramst[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                    Paramst[6] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.KP_Trn_KANTOR_BAYAR", Paramst, "constrmf").Tables[0];

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


                    pnltrn.Visible = true;
                    Pnlfooter4.Visible = true;
                    btndelete.Visible = true;
                    btnSearch.Enabled= false;
                    ddlfilter.Enabled= false;


                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SET":

              
                break;
            case "PDF":

                try
                {

                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTLISTTAGIHAN", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.KP_Trn_KANTOR_BAYAR", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/Kresun/Report/ListTagihan.rpt"));
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
                    else
                    {
                        rprt.Close();
                        rprt.Dispose();
                    }


                }

                break;
            case "SLIP":

                try
                { 
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    typetrn = "K";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTLISTTAGIHAN", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.KP_Trn_KANTOR_BAYAR", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/Kresun/Report/ListTagihan.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);


                        Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        ExportFormatType formatType = ExportFormatType.NoFormat;
                        formatType = ExportFormatType.Excel;
                        rprt.ExportToHttpResponse(formatType, Response, true, "ListTagihanExcel");
                        //Response.ContentType = "application/pdf";
                        //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


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

        }
    }
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {




            dtBayar.Columns.Clear();

            dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("Tgljthtempo", typeof(DateTime)));
            dtBayar.Columns.Add(new DataColumn("nonasabah", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("namanasabah", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("kodecabang", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("Namacabang", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("angske", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("angsuran", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("pokok", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("bunga", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("nip", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("nopin", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("jenistagihan", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("plafon", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("pengelolaPensiun", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("namainstansi", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("namamitra", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("NorekeningKB", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("NamaRekeningKB", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("NamaBankRekening", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("VirtualAccount", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("no_spk", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("telepon", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("ketnip", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("kodesumberdana", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("jensigroup", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("ajukanbayar", typeof(decimal)));

            dtBayar.Rows.Clear();
            int xi = 0;

            for (int i = 0; i < GRTP.Rows.Count; i++)
            {
                //                DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");


                TextBox ajukanbayar = (TextBox)GRTP.Rows[i].FindControl("ajukanbayar");
                string typebayar = string.Empty;
                typebayar = (Convert.ToDecimal(ajukanbayar.Text) > 0 ? "1" : "0");

                DataRow dr1 = dtBayar.NewRow();
                xi = xi + 1;
                decimal xangske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangs_ke")).Text);
                decimal xangsuran = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblangsuran")).Text);
                decimal xbayar = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblbayar")).Text);
                decimal xplafon = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblplafon")).Text);
                decimal xbunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblbunga")).Text);
                decimal xpokok = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblpokok")).Text);
                decimal xajukanbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("ajukanbayar")).Text);




                DateTime Tgljthtempo = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgl_jwl")).Text);
                dr1["No"] = xi;
                dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                dr1["Tgljthtempo"] = Tgljthtempo;
                dr1["nonasabah"] = ((Label)GRTP.Rows[i].FindControl("lblno_nasabah")).Text;
                dr1["namanasabah"] = ((Label)GRTP.Rows[i].FindControl("lblnama_nasabah")).Text;
                dr1["kodecabang"] = ((Label)GRTP.Rows[i].FindControl("lblkode_cabang")).Text;
                dr1["Namacabang"] = ((Label)GRTP.Rows[i].FindControl("lblnama_cabang")).Text;
                dr1["angske"] = xangske;
                dr1["angsuran"] = xangsuran;
                dr1["plafon"] = xplafon;
                dr1["bunga"] = xbunga;
                dr1["pokok"] = xpokok;
                dr1["Bayar"] = xbayar;
                dr1["TypeBayar"] = typebayar;
                dr1["nopin"] = ((Label)GRTP.Rows[i].FindControl("lblnopin")).Text;
                dr1["nip"] = ((Label)GRTP.Rows[i].FindControl("lblnip")).Text;
                dr1["jenistagihan"] = ((Label)GRTP.Rows[i].FindControl("lbljenistagihan")).Text;
                dr1["pengelolaPensiun"] = ((Label)GRTP.Rows[i].FindControl("lblketnip")).Text;
                dr1["namainstansi"] = ((Label)GRTP.Rows[i].FindControl("lblNamaBankRekening")).Text;
                dr1["namamitra"] = ((Label)GRTP.Rows[i].FindControl("lblnamamitra")).Text;
                dr1["NorekeningKB"] = ((Label)GRTP.Rows[i].FindControl("lblNorekeningKB")).Text;
                dr1["NamaRekeningKB"] = ((Label)GRTP.Rows[i].FindControl("lblNamaRekeningKB")).Text;
                dr1["NamaBankRekening"] = ((Label)GRTP.Rows[i].FindControl("lblNamaBankRekening")).Text;
                dr1["VirtualAccount"] = ((Label)GRTP.Rows[i].FindControl("lblVirtualAccount")).Text;
                dr1["no_spk"] = ((Label)GRTP.Rows[i].FindControl("lblno_spk")).Text;
                dr1["telepon"] = ((Label)GRTP.Rows[i].FindControl("lbltelepon")).Text;
                dr1["ketnip"] = ((Label)GRTP.Rows[i].FindControl("lblketnip")).Text;
                dr1["kodesumberdana"] = ((Label)GRTP.Rows[i].FindControl("lblkodesumberdana")).Text;
                dr1["jensigroup"] = ((Label)GRTP.Rows[i].FindControl("lbljenistagihan")).Text;
                dr1["ajukanbayar"] = xajukanbayar;





                dtBayar.Rows.Add(dr1);
            }


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[17];


            Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
            Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
            Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayar.Text, typeof(decimal));
            Params[3] = gb.CreateSPParameter("@Kodebank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
            Params[4] = gb.CreateSPParameter("@NmBank", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNmBank.Text, typeof(string));
            Params[5] = gb.CreateSPParameter("@namamitra", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamamitra.Text, typeof(string));
            Params[6] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnorek.Text, typeof(string));


            Params[7] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
            Params[8] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[9] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            Params[10] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[12] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
            Params[13] = gb.CreateSPParameter("@KP_LIST_KANTOR_BAYAR", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
            Params[14] = gb.CreateSPParameter("@Kettransfer", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
            Params[15] = gb.CreateSPParameter("@filtercabang", ParameterDirection.Input, SqlDbType.VarChar, 10, '-', typeof(string));
            Params[16] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_Kantor_Bayar_Delete", Params, "constrmf");



            //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_TRANSFER_DELETE", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Visible = false;
                //nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','2');", true);

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
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
  

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            //bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "blokirtransfer"));
            //angsuran += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Jumlahakantransfer"));
            ////denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));

            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 0)
            {


            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 1)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            }

            string formatted = string.Format("{0:N2}", bayar);
            txtbayar.Text = formatted;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalpencairan = (Label)e.Row.FindControl("lbltotalangsuran");
            lbltotalpencairan.Text = string.Format("{0:N2}", pokok);

          
         


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
                dtBayar.Columns.Add(new DataColumn("Tgljthtempo", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("nonasabah", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("namanasabah", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("kodecabang", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Namacabang", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("angske", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("angsuran", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("pokok", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("bunga", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("nip", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("nopin", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("jenistagihan", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("plafon", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("pengelolaPensiun", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("namainstansi", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("namamitra", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("NorekeningKB", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("NamaRekeningKB", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("NamaBankRekening", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("VirtualAccount", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("no_spk", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("telepon", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("ketnip", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("kodesumberdana", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("jensigroup", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("ajukanbayar", typeof(decimal)));


                 dtBayar.Rows.Clear();
                int xi = 0;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {
                    //DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");
                    TextBox ajukanbayar = (TextBox)GRTP.Rows[i].FindControl("ajukanbayar");
                    string typebayar = string.Empty;
                    typebayar = (Convert.ToDecimal(ajukanbayar.Text) > 0 ? "1" : "0");


                    DataRow dr1 = dtBayar.NewRow();
                    xi = xi + 1;
                    decimal xangske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangs_ke")).Text);
                    decimal xangsuran = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblangsuran")).Text);
                    decimal xbayar = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblbayar")).Text);
                    decimal xplafon = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblplafon")).Text);
                    decimal xbunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblbunga")).Text);
                    decimal xpokok = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblpokok")).Text);
                    decimal xajukanbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("ajukanbayar")).Text);





                    DateTime Tgljthtempo = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgl_jwl")).Text);
                    dr1["No"] = xi;
                    dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                    dr1["Tgljthtempo"] = Tgljthtempo;
                    dr1["nonasabah"] = ((Label)GRTP.Rows[i].FindControl("lblno_nasabah")).Text;
                    dr1["namanasabah"] = ((Label)GRTP.Rows[i].FindControl("lblnama_nasabah")).Text;
                    dr1["kodecabang"] = ((Label)GRTP.Rows[i].FindControl("lblkode_cabang")).Text;
                    dr1["Namacabang"] = ((Label)GRTP.Rows[i].FindControl("lblnama_cabang")).Text;
                    dr1["angske"] = xangske;
                    dr1["angsuran"] = xangsuran;
                    dr1["plafon"] = xplafon;
                    dr1["bunga"] = xbunga;
                    dr1["pokok"] = xpokok;
                    dr1["Bayar"] = xbayar;
                    dr1["TypeBayar"] = typebayar;
                    dr1["nopin"] = ((Label)GRTP.Rows[i].FindControl("lblnopin")).Text;
                    dr1["nip"] = ((Label)GRTP.Rows[i].FindControl("lblnip")).Text;
                    dr1["jenistagihan"] = ((Label)GRTP.Rows[i].FindControl("lbljenistagihan")).Text;
                    dr1["pengelolaPensiun"] = ((Label)GRTP.Rows[i].FindControl("lblketnip")).Text;
                    dr1["namainstansi"] = ((Label)GRTP.Rows[i].FindControl("lblNamaBankRekening")).Text;
                    dr1["namamitra"] = ((Label)GRTP.Rows[i].FindControl("lblnamamitra")).Text;
                    dr1["NorekeningKB"] = ((Label)GRTP.Rows[i].FindControl("lblNorekeningKB")).Text;
                    dr1["NamaRekeningKB"] = ((Label)GRTP.Rows[i].FindControl("lblNamaRekeningKB")).Text;
                    dr1["NamaBankRekening"] = ((Label)GRTP.Rows[i].FindControl("lblNamaBankRekening")).Text;
                    dr1["VirtualAccount"] = ((Label)GRTP.Rows[i].FindControl("lblVirtualAccount")).Text;
                    dr1["no_spk"] = ((Label)GRTP.Rows[i].FindControl("lblno_spk")).Text;
                    dr1["telepon"] = ((Label)GRTP.Rows[i].FindControl("lbltelepon")).Text;
                    dr1["ketnip"] = ((Label)GRTP.Rows[i].FindControl("lblketnip")).Text;
                    dr1["kodesumberdana"] = ((Label)GRTP.Rows[i].FindControl("lblkodesumberdana")).Text;
                    dr1["jensigroup"] = ((Label)GRTP.Rows[i].FindControl("lbljenistagihan")).Text;
                    dr1["ajukanbayar"] = xajukanbayar;





                    dtBayar.Rows.Add(dr1);
                }


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[16];


                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayar.Text, typeof(decimal));
                Params[3] = gb.CreateSPParameter("@Kodebank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                Params[4] = gb.CreateSPParameter("@NmBank", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNmBank.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@namamitra", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamamitra.Text, typeof(string));
                Params[6] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnorek.Text, typeof(string));


                Params[7] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[8] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[9] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[10] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[12] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[13] = gb.CreateSPParameter("@KP_LIST_KANTOR_BAYAR", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[14] = gb.CreateSPParameter("@Kettransfer", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                Params[15] = gb.CreateSPParameter("@filtercabang", ParameterDirection.Input, SqlDbType.VarChar, 10, '-', typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_Kantor_Bayar_Save", Params, "constrmf");
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
        else
        {
            if(ddlKdBank.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kantor Bayar belum di pilih');", true);

            }
            else
            {

                ddlKdBank_SelectedIndexChanged(sender, e);
            }
        }

    }



    public void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        



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
        cells[10].Attributes.Add("data-class", "expand");
        cells[11].Attributes.Add("data-class", "expand");
        cells[12].Attributes.Add("data-class", "expand");
        cells[13].Attributes.Add("data-class", "expand");
        cells[14].Attributes.Add("data-class", "expand");
        cells[15].Attributes.Add("data-class", "expand");
        cells[16].Attributes.Add("data-class", "expand");
        cells[17].Attributes.Add("data-class", "expand");
        cells[18].Attributes.Add("data-class", "expand");
        cells[19].Attributes.Add("data-class", "expand");
        cells[20].Attributes.Add("data-class", "expand");
        cells[21].Attributes.Add("data-class", "expand");
        cells[22].Attributes.Add("data-class", "expand");
        cells[23].Attributes.Add("data-class", "expand");
        

    }

    private void SetDetailBayar()
    {

        //DataRow dr = null;     

        dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Tgljthtempo", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("nonasabah", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("namanasabah", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("kodecabang", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Namacabang", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("angske", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("angsuran", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("pokok", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("bunga", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("nip", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("nopin", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("jenistagihan", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("plafon", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("pengelolaPensiun", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("namainstansi", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("namamitra", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("NorekeningKB", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("NamaRekeningKB", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("NamaBankRekening", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("VirtualAccount", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("no_spk", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("telepon", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("ketnip", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("kodesumberdana", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("jensigroup", typeof(string)));


        ViewState["ListDetailBayar"] = dtBayar;
        GRTP.DataSource = dtBayar;
        GRTP.DataBind();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridBayar();
    }

    protected void ajukanbayar_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal ajukanbayar = 0;
        ajukanbayar = decimal.Parse(((TextBox)grow.FindControl("ajukanbayar")).Text);
        decimal angsuran = 0;
        angsuran = decimal.Parse(((Label)grow.FindControl("lblangsuran")).Text);
        if (ajukanbayar > angsuran)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Bayar Melebihi Angsuran!');", true);
            ((TextBox)grow.FindControl("ajukanbayar")).Text = "0";
            ((Label)grow.FindControl("lblbayar")).Text = "0";
        }
        else
        {
            ((Label)grow.FindControl("lblbayar")).Text = ((TextBox)grow.FindControl("ajukanbayar")).Text;
        }
        subgridbayar_Click();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void ddlbayar_SelectedIndexChanged(object sender, EventArgs e)
    {


        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;



        if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "0")
        {
            ((Label)grow.FindControl("lblbayar")).Text = "0";

        }
        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "1")
        {
            ((Label)grow.FindControl("lblbayar")).Text = ((Label)grow.FindControl("lblangsuran")).Text;
        }

        //else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "2")
        //{
        //    decimal angsuran = 0;
        //    decimal denda = 0;
        //    decimal total = 0;

        //    angsuran = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
        //    denda = decimal.Parse(((Label)grow.FindControl("lbldenda")).Text);
        //    total = angsuran + denda;
        //    ((Label)grow.FindControl("lblbayar")).Text = string.Format("{0:N2}", total);

        //}


      
        subgridbayar_Click();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

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

                jlhgl = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("ajukanbayar")).Text);
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



    protected void ddlfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlfilter.SelectedValue == "ALL")
            {
                DivPeriode.Visible = false;
                GRTP.DataSource = null;
                GRTP.DataBind();
                txtbayar.Text = "0";
                Pnlfooter4.Visible = false;
            }
            else
            {
                DivPeriode.Visible = true;
                GRTP.DataSource = null;
                GRTP.DataBind();
                txtbayar.Text = "0";
                Pnlfooter4.Visible = false;
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var bank = ddlKdBank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_KANTOR_BAYAR", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            //string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            //string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();





            string paramStartDate = StartDate.Text;
            string paramEndtDate = EndDate.Text;
            if(ddlfilter.Text == "ALL")
            {
                paramStartDate = null;
                paramEndtDate = null;
            }

            var Cabang = ddlKdBank.SelectedValue;


            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[7];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var1[1] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 50, Cabang, typeof(String));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLISTTAGIHANKANTORBAYAR", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            var1[5] = gb.CreateSPParameter("@StartDate", ParameterDirection.Input, SqlDbType.SmallDateTime, 8000, paramStartDate, typeof(DateTime));
            var1[6] = gb.CreateSPParameter("@EndDate", ParameterDirection.Input, SqlDbType.SmallDateTime, 8000, paramEndtDate, typeof(DateTime));

            ge.ListDataTable = gb.GetDataJustOutPut("dbo.KP_Trn_KANTOR_BAYAR", var1, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {
                SetDetailBayar();
                ViewState["ListDetailBayar"] = ge.ListDataTable;

                GRTP.DataSource = ge.ListDataTable;
                GRTP.DataBind();

                GRTP.UseAccessibleHeader = true;
                GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridBayar();
                Pnlfooter4.Visible = true;
                subgridbayar_Click();
            }
            else
            {
                SetDetailBayar();
                Pnlfooter4.Visible = false;
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


        }

    }
}
