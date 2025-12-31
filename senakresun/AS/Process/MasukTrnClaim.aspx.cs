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
public partial class AS_Process_MasukTrnClaim : System.Web.UI.Page
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
    string form = "A03";
    string kodesumber = "AS";
    string KodeKonter = "AS04";
    public string nobuktidb;
   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        txtKeterangan.Text = "-";
        txtalamat.Text = "-";
        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        Pnlfooter4.Visible = true;
        btnSimpan.Visible = false;
    }
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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
 

    }

    private void SetDetailBayar()
    {

        //DataRow dr = null;     
        dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("tgl_jwl", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("tgl_angs", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("pkangs", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("bng_angs", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("denda_angs", typeof(string)));

        ViewState["ListDetailBayar"] = dtBayar;
        GRTP.DataSource = dtBayar;
        GRTP.DataBind();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridBayar();
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
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Claim", var, "constrmf");

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

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
      
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;
                    Pnlfooter4.Visible = true;
      //              SELECT x.NoBukti, x.KodeAsuransi,a.NamaAsuransi, x.Nobuktistatus, x.TglTrn, x.TglJthTmp, x.Keterangan, x.TglEntry, x.KodeUser, x.TutupBukuFlag, x.JumlahClaim,
					 //k.Norekening, k.KodeLgn, k.NamaNasabah, k.plafon,  k.Nopolis, k.RefPolis, k.tgllahirdebitur, k.PeriodeAsuransiAwal, k.PeriodeAsuransiAkhir, k.alamat, k.TglKematian
      //              FROM            AshdrClaimInvoice x WITH(NOLOCK)

      //              inner join[dbo].[KPTransaksiStatusMeninggal] k WITH(NOLOCK) on k.nobukti=x.Nobuktistatus
      //             inner join asmst a with (nolock) on a.kodeasuransi= x.KodeAsuransi and a.Kdpersh= x.kdpersh

      //             where x.Kdpersh= @kdPersh and x.KodeCabang= @kodecabang






                   txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;

                    txttgljthtmp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglJthTmp")).Text;
                    txttgljthtmp.Enabled = false;

                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    //txtkodelgn.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    Txtnorekening.Text = HDNoRekening.Value;
                    //txtnamask.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaSK")).Text;
                    //txtnosk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoSK")).Text;
                    //txttglsk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglSK")).Text;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    //txtpathktp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Text;
                    //txtlink.Text = "View";
                    //txtlink.NavigateUrl = txtpathktp.Text;
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
                    btnSimpan.Visible = true;
                    
                    //txttglsk.Enabled = true;
                    //txtnamask.Enabled = true;
                    //txtnosk.Enabled = true;
                    txttgltrn.Enabled = true;
                    txtKeterangan.Enabled = true;
                    //fileuploadktp.Enabled = true;
                    //btnUploadktp.Enabled = true;

                    btndelete.Visible = false;


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

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                    txttgljthtmp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglJthTmp")).Text;
                    txttgljthtmp.Enabled = false;
                    txttgllahir.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgllahirdebitur")).Text;
                    txttgllahir.Enabled = false;
                    txtperiodeawal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPeriodeAsuransiAwal")).Text;
                    txtperiodeawal.Enabled = false;
                    txtperiodeakhir.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPeriodeAsuransiAkhir")).Text;
                    txtperiodeakhir.Enabled = false;
                    txttglmeninggal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglKematian")).Text;
                    txttglmeninggal.Enabled = false;

                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;

                    TxtkodeAsuransi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeasuransi")).Text;
                    TxtkodeAsuransi.Enabled = false;

                    TxtnamaAsuransi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaAsuransi")).Text;
                    TxtnamaAsuransi.Enabled = false;

                    txtplafon.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text));
                    txtplafon.Enabled = false;

                    TxtnilaiClaim.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblJumlahClaim")).Text));
                    TxtnilaiClaim.Enabled = false;


                    txtnopolis.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNopolis")).Text;
                    txtnopolis.Enabled = false;


                    txtNoRefPolis.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRefPolis")).Text;
                    txtNoRefPolis.Enabled = false;

                    txtalamat.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat")).Text;
                    txtalamat.Enabled = false;

                    //txtkodelgn.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    Txtnorekening.Text = HDNoRekening.Value;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;

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


                    btnSimpan.Visible =false;
                    btndelete.Visible = true;

                    string customer = hdncustomer.Value;
                    //string norekening = HDNoRekening.Value;
                    pnltrn.Visible = true;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                    var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, customer, typeof(string));
                    var1[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                    var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

                    var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTUPINJAMANASURANSI", typeof(string));
                    var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Claim", var1, "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "angs_ke asc";


                    GRTP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    GRTP.DataBind();

                    GRTP.UseAccessibleHeader = true;

                    GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //GRTP.PageIndex = 50;
                    cssGridBayar();
                    GRTP.Enabled = false;



                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "PDF":

                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTHEADER", typeof(string)); 
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string)); 
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.[AS_Trn_Claim_Report]", Paramsc, "constrmf").Tables[0];


                    if (ge.ListDataTable.Rows.Count > 0)
                    { 
                        rprt.Load(Server.MapPath("~/AS/Process/CA001.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("User", "KSP SERAMBI DANA");
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
                    //typetrn = "B";
                    DefaultClass.Global.SqlHelper[] Paramsc1 = new DefaultClass.Global.SqlHelper[4];
                    Paramsc1[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTDETAIL", typeof(string));
                    Paramsc1[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc1[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.[AS_Trn_Claim_Report]", Paramsc1, "constrmf").Tables[0];
                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/AS/Process/CA002.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("Kantor", "Pusat KSP Giga Grup Putra");
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

        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(TxtnilaiClaim.Text);

            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nilai Claim Masih Kosong!');", true);
            }
            else
            {

                dtBayar.Columns.Clear();




                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("tgl_jwl", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("tgl_angs", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("pkangs", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("bng_angs", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("denda_angs", typeof(decimal)));
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
                        int xAngske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangske")).Text);

                        //decimal xbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbayar")).Text);

                        decimal xpokokhutang = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblPokok")).Text);
                        decimal xbungahutang = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblBunga")).Text);
                        decimal xpokokbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblpkangs")).Text);
                        decimal xbungabayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbng_angs")).Text);
                        decimal xdendabayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lbldenda_angs")).Text);





                        DateTime tgl_jwl = DateTime.Parse(((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text);
                        DateTime tgl_Byr = DateTime.Parse(((TextBox)GRTP.Rows[i].FindControl("lbltgl_angs")).Text);

                        dr1["No"] = xi;
                        dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                        dr1["tgl_jwl"] = tgl_jwl;
                        dr1["Angske"] = xAngske;
                        dr1["Pokok"] = xpokokhutang;
                        dr1["Bunga"] = xbungahutang;
                        dr1["tgl_angs"] = tgl_Byr;
                        dr1["pkangs"] = xpokokbayar;
                        dr1["bng_angs"] = xbungabayar;
                        dr1["denda_angs"] = xdendabayar;
                        dr1["TypeBayar"] = ddlbayar.SelectedValue;
                        dtBayar.Rows.Add(dr1);
                    }
                }

                
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[18];

                    Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                    Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[2] = gb.CreateSPParameter("@JumlahClaim", ParameterDirection.Input, SqlDbType.Decimal, 0, TxtnilaiClaim.Text, typeof(decimal));
                    Params[3] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodelgn.Text, typeof(string));
                    Params[4] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, TxtkodeAsuransi.Text, typeof(string));
                    Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                    Params[6] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[10] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[11] = gb.CreateSPParameter("@AS_CLAIM_Kartu_Pinjaman", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                    Params[12] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                    Params[13] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    Params[14] = gb.CreateSPParameter("@kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 30, "AS002", typeof(string));
                    Params[15] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, "DELETE", typeof(string));
                    Params[16] = gb.CreateSPParameter("@Norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, Txtnorekening.Text, typeof(string));
                    Params[17] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtmp.Text, typeof(DateTime));
                    ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Claim", Params, "constrmf");
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

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','Invoice Claim Asuransi','top-right','success','1');", true);
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

    decimal pokok = 0, bunga = 0, angsuranpokok = 0,angsuranbunga=0, denda = 0, bayar = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok+= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pokok"));
            bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bunga"));
            angsuranpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pkangs"));
            angsuranbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bng_angs"));
            denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda_angs"));
            
            //if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 0)
            //{
                

            //}
            //else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 1)
            //{
            //    bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            //}
            //else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 2)
            //{
            //    bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            //} 
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalPokok = (Label)e.Row.FindControl("lbltotalPokok");
            lbltotalPokok.Text = string.Format("{0:N2}", pokok);

            Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
            lbltotalBunga.Text = string.Format("{0:N2}", bunga);

            Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalpkangs");
            lbltotalAngsuran.Text = string.Format("{0:N2}", angsuranpokok);


            Label lbltotalbungangs = (Label)e.Row.FindControl("lbltotalbng_angs");
            lbltotalbungangs.Text = string.Format("{0:N2}", angsuranbunga);

            Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda_angs");
            lbltotaldenda.Text = string.Format("{0:N2}", denda); 
        }

        decimal nilai_claim = pokok - angsuranpokok;

        TxtnilaiClaim.Text = string.Format("{0:N2}", nilai_claim);

    }
    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal xpokokhutang = 0, xbungahutang = 0,xpokokbayar = 0, xbungabayar = 0, xdendabayar = 0;

        for (int i = 0; i < GRTP.Rows.Count; i++)
        {
           
                //DataRow dr1 = dtBayar.NewRow();
                //xi = xi + 1;
                //int xUsia = int.Parse(((Label)GRTP.Rows[i].FindControl("lblusia")).Text);
                //int xBulan = int.Parse(((Label)GRTP.Rows[i].FindControl("lblbulan")).Text);

            xpokokhutang = xpokokhutang + decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblPokok")).Text);
            xbungahutang = xbungahutang + decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblBunga")).Text);
            xpokokbayar = xpokokbayar + decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblpkangs")).Text);
            xbungabayar = xbungabayar + decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbng_angs")).Text);
            xdendabayar = xdendabayar + decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lbldenda_angs")).Text);

        }

        Label lbltotalPokok = (Label)GRTP.FooterRow.FindControl("lbltotalPokok");
        lbltotalPokok.Text = string.Format("{0:N2}", xpokokhutang);

        Label lbltotalBunga = (Label)GRTP.FooterRow.FindControl("lbltotalBunga");
        lbltotalBunga.Text = string.Format("{0:N2}", xbungahutang);

        Label lbltotalAngsuran = (Label)GRTP.FooterRow.FindControl("lbltotalpkangs");
        lbltotalAngsuran.Text = string.Format("{0:N2}", xpokokbayar);


        Label lbltotalbungangs = (Label)GRTP.FooterRow.FindControl("lbltotalbng_angs");
        lbltotalbungangs.Text = string.Format("{0:N2}", xbungabayar);

        Label lbltotaldenda = (Label)GRTP.FooterRow.FindControl("lbltotaldenda_angs");
        lbltotaldenda.Text = string.Format("{0:N2}", xdendabayar);

        //GRTP.SetPageIndex(100);
       
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        GRTP.PageIndex = 50;

    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(TxtnilaiClaim.Text);

            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nilai Claim Masih Kosong!');", true);
            }
            else
            {

                dtBayar.Columns.Clear();




                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("tgl_jwl", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("tgl_angs", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("pkangs", typeof(decimal)));         
                dtBayar.Columns.Add(new DataColumn("bng_angs", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("denda_angs", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));



                dtBayar.Rows.Clear();
                int xi = 0;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {
                    DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");


                    //if (ddlbayar.SelectedValue != "0")
                    //{
                        DataRow dr1 = dtBayar.NewRow();
                        xi = xi + 1;
                        int xAngske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangske")).Text);                      

                        //decimal xbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbayar")).Text);

                        decimal xpokokhutang = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblPokok")).Text);
                        decimal xbungahutang = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblBunga")).Text);
                        decimal xpokokbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblpkangs")).Text);
                        decimal xbungabayar= decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbng_angs")).Text);
                        decimal xdendabayar= decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lbldenda_angs")).Text);


                    //DateTime tgl_jwl = DateTime.Parse(((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text);
                    //DateTime tgl_Byr = DateTime.Parse(((TextBox)GRTP.Rows[i].FindControl("lbltgl_angs")).Text);

                    DateTime tgl_jwl = DateTime.Now;
                    DateTime tgl_Byr = DateTime.Now;

                    dr1["No"] = xi;
                        dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                        dr1["tgl_jwl"] = tgl_jwl;                       
                        dr1["Angske"] = xAngske;
                        dr1["Pokok"] = xpokokhutang;
                        dr1["Bunga"] = xbungahutang;
                        dr1["tgl_angs"] = tgl_Byr;
                        dr1["pkangs"] = xpokokbayar;
                        dr1["bng_angs"] = xbungabayar;
                        dr1["denda_angs"] = xdendabayar;
                        dr1["TypeBayar"] = ddlbayar.SelectedValue;
                        dtBayar.Rows.Add(dr1);
                    //}
                }


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[18]; 
                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@JumlahClaim", ParameterDirection.Input, SqlDbType.Decimal, 0, TxtnilaiClaim.Text, typeof(decimal));
                Params[3] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodelgn.Text, typeof(string));
                Params[4] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, TxtkodeAsuransi.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[6] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[10] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[11] = gb.CreateSPParameter("@AS_CLAIM_Kartu_Pinjaman", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[12] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                Params[13] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                Params[14] = gb.CreateSPParameter("@kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 30, "AS002", typeof(string));
                Params[15] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, "INSERT", typeof(string));
                Params[16] = gb.CreateSPParameter("@Norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, Txtnorekening.Text, typeof(string));
                Params[17] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtmp.Text, typeof(DateTime));
                //Params[18] = gb.CreateSPParameter("@angsuranke", ParameterDirection.Input, SqlDbType.VarChar, 30, txtangske.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Claim", Params, "constrmf");
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

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Invoice Claim Asuransi','top-right','success','1');", true);
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
        var kodelgn = hdncustomer.Value;


        DefaultClass.Global.SqlHelper[] getdata = new DefaultClass.Global.SqlHelper[5];
        getdata[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETDATAFORANGS", typeof(string));
        getdata[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        getdata[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        getdata[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        getdata[4] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Claim", getdata, "constrmf");

        //txtangske.Text = ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();
        //txtdenda.Text = ge.ListDataset.Tables[0].Rows[0]["denda_angs"].ToString();
        TxtnilaiClaim.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisapk"].ToString()));

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIARMST", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Claim", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)

            
        {
            txtkodelgn.Text = kodelgn;
            txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["NamaNasabah"].ToString();
            //DateTime tglmeninggal;
            //tglmeninggal =;
            txttglmeninggal.Text = String.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglKematian"].ToString()));
            txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()));//;
            TxtkodeAsuransi.Text = ge.ListDataset.Tables[0].Rows[0]["kodeasuransi"].ToString();
            txtnopolis.Text = ge.ListDataset.Tables[0].Rows[0]["NoPolis"].ToString();
            txtNoRefPolis.Text = ge.ListDataset.Tables[0].Rows[0]["REfpolis"].ToString();
            txtperiodeawal.Text = String.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["periodeasuransiawal"].ToString()));//ge.ListDataset.Tables[0].Rows[0]["periodeasuransiawal"].ToString();
            txtperiodeakhir.Text = String.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["periodeasuransiakhir"].ToString()));//ge.ListDataset.Tables[0].Rows[0]["periodeasuransiakhir"].ToString();
            txttgllahir.Text = String.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgllahirdebitur"].ToString()));//ge.ListDataset.Tables[0].Rows[0]["tgllahirdebitur"].ToString();
            txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
            TxtnamaAsuransi.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAsuransi"].ToString();
            Txtnorekening.Text = HDNoRekening.Value;
            txttgljthtmp.Text = txttgltrn.Text;


            

            btnSimpan.Visible = true;

     
            txttgltrn.Enabled = true;
            txtKeterangan.Enabled = true;
            txttgljthtmp.Enabled = true;
            TxtnilaiClaim.Enabled = true;

            string customer = hdncustomer.Value;
            //string norekening = HDNoRekening.Value;
            pnltrn.Visible = true;

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, customer, typeof(string));
            var1[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
            var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTUPINJAMANASURANSI", typeof(string));
            var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Claim", var1, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "angs_ke asc";


            GRTP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRTP.DataBind();

            GRTP.UseAccessibleHeader = true;

            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
            //GRTP.PageIndex = 50;
            cssGridBayar(); 
        }

        else
        {
            txtkodelgn.Text = "";
            txtNmSup.Text = "";
            //DateTime tglmeninggal;
            //tglmeninggal =;
            txttglmeninggal.Text = "";
            txtplafon.Text = "";
            TxtkodeAsuransi.Text = "";
            txtnopolis.Text = "";
            txtNoRefPolis.Text = "";
            txtperiodeawal.Text = "";//ge.ListDataset.Tables[0].Rows[0]["periodeasuransiawal"].ToString();
            txtperiodeakhir.Text = "";//ge.ListDataset.Tables[0].Rows[0]["periodeasuransiakhir"].ToString();
            txttgllahir.Text = "";//ge.ListDataset.Tables[0].Rows[0]["tgllahirdebitur"].ToString();
            txtalamat.Text = "";
            TxtnamaAsuransi.Text = "";
            Txtnorekening.Text = "";
            txttgljthtmp.Text = "";

            txttgltrn.Enabled = false;
            txtKeterangan.Enabled = false;
            txttgljthtmp.Enabled = false;
            TxtnilaiClaim.Enabled = false;

            SetDetailBayar();
            btnSimpan.Visible = false;
   
        }


     


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_getCustomer_Claim", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }



    protected void ddlbayar_SelectedIndexChanged(object sender, EventArgs e)
    {


        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;



        if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "0")
        {
            //((TextBox)grow.FindControl("lblbayar")).Text = "0";
            ((TextBox)grow.FindControl("lbltgljthtmp")).Enabled = false;
            ((TextBox)grow.FindControl("lblPokok")).Enabled = false;
            ((TextBox)grow.FindControl("lblBunga")).Enabled = false;
            ((TextBox)grow.FindControl("lbltgl_angs")).Enabled = false;
            ((TextBox)grow.FindControl("lblpkangs")).Enabled = false;
            ((TextBox)grow.FindControl("lblbng_angs")).Enabled = false;
            ((TextBox)grow.FindControl("lbldenda_angs")).Enabled = false;


        }
        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "1")
        {
            ((TextBox)grow.FindControl("lbltgljthtmp")).Enabled =true;
            ((TextBox)grow.FindControl("lblPokok")).Enabled = true;
            ((TextBox)grow.FindControl("lblBunga")).Enabled = true;
            ((TextBox)grow.FindControl("lbltgl_angs")).Enabled = true;
            ((TextBox)grow.FindControl("lblpkangs")).Enabled = true;
            ((TextBox)grow.FindControl("lblbng_angs")).Enabled = true;
            ((TextBox)grow.FindControl("lbldenda_angs")).Enabled = true;
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


        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        //subgridbayar_Click();

    }
    protected void txttgljthtmp_textchanged(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        try
        {

            TxtnilaiClaim.Text = "0";
            DefaultClass.Global.SqlHelper[] getdata = new DefaultClass.Global.SqlHelper[6];
            getdata[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETNILAICLAIM", typeof(string));
            getdata[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            getdata[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            getdata[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            getdata[4] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
            getdata[5] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtmp.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Claim", getdata, "constrmf");

            //txtangske.Text = ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString();
            //txtdenda.Text = ge.ListDataset.Tables[0].Rows[0]["denda_angs"].ToString();
            var claim = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisapk"].ToString()));

            TxtnilaiClaim.Text = claim;

        }
        catch(Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));


        }
        //}
    }
    //private void subgridbayar_Click()
    //{

    //    int dgcount = GRTP.Rows.Count;
    //    decimal sumjlhgl = 0;
    //    decimal jlhgl = 0;

    //    decimal sumPengajuan = 0;
    //    decimal jlhPengajuan = 0;
    //    if (dgcount > 0)
    //    {


    //        for (int i = 0; i < dgcount; i++)
    //        {
    //            //DataRow dr = dtRP.NewRow();

    //            jlhgl = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbayar")).Text);
    //            sumjlhgl = sumjlhgl + jlhgl;


    //            jlhPengajuan = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblgrosspremi")).Text);
    //            sumPengajuan = sumPengajuan + jlhPengajuan;



    //        }



    //    }


    //    else
    //    {
    //        sumjlhgl = 0;
    //        sumPengajuan = 0;
    //    }
    //    string formatted = string.Format("{0:N2}", sumjlhgl);
    //    //txtbayarinvoice.Text = formatted;

    //    //txtbayar.Text = string.Format("{0:N2}", sumPengajuan);




    //}





}
