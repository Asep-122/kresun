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

public partial class CL_Transaksi_TransaksiLainBeta : System.Web.UI.Page
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

    ////DECLARE A "REPORT" AND A "REPORT VIEWER".
    //protected CrystalDecisions.CrystalReports.Engine.ReportDocument Report =
    //    new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    //protected CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer1 =
    //    new CrystalDecisions.Web.CrystalReportViewer();
    //Reports rpt = new Reports();


    DataTable dtRP = new DataTable("Transaction");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();

    int nosekarang = 0;
    string formatnumber = "";
    string nosebelumnya = "";
    int nonasabah = 0;
    string CRNonasabah = "";
    string updateno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {

                BindGridEntity();

                //KODEBANK
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDKODEBANK", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


                ddlkdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkdBank.DataTextField = "kdnm";
                ddlkdBank.DataValueField = "KodeBank";
                ddlkdBank.DataBind();
                ddlkdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //NOBUKTI
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKDBUKTI", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeKonter asc";


                ddlnoBukti.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlnoBukti.DataTextField = "nobukti";
                ddlnoBukti.DataValueField = "KodeKonter";
                ddlnoBukti.DataBind();
                ddlnoBukti.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //TYPETRANSAKSI
                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                var2[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
                var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANS", typeof(string));
                var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var2, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn asc";

                ddlTypeTrn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlTypeTrn.DataTextField = "TypeTrans";
                ddlTypeTrn.DataValueField = "TypeTrn";
                ddlTypeTrn.DataBind();
                ddlTypeTrn.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //DROPDOWNLIST JURNAL
                DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[3];
                var6[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
                var6[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORJURNAL", typeof(string));
                var6[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var6, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";

                ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlJurnal.DataTextField = "kdJurnal";
                ddlJurnal.DataValueField = "KodeJurnal";
                ddlJurnal.DataBind();
                ddlJurnal.Items.Insert(0, new ListItem("Pilih Data", "-"));
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

    //[WebMethod]
    //public static string GetTempat(string InputText, string SessKdpershx)
    //{
    //    List<string[]> listResult = new List<string[]>();

    //    // empResult.Add(KDBRG);
    //    DefaultClass.Global gb = new DefaultClass.Global();
    //    EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

    //    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
    //    Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
    //    Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
    //    ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_getTempat", Params, "constrmf");

    //    listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

    //    var jsonSerialiser = new JavaScriptSerializer();
    //    var json = jsonSerialiser.Serialize(listResult);

    //    return json;

    //}
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (rbJurnal.SelectedIndex == 0)
        {
            FileJurnal.Visible = true;
            PnlJournal.Visible = false;
            Pnlfooter.Visible = true;
            PnlJournalGRRP.Visible = false;
        }
        else if (rbJurnal.SelectedIndex == 1)
        {
            FileJurnal.Visible = false;
            PnlJournal.Visible = false;
            Pnlfooter.Visible = false;
            PnlJournalGRRP.Visible = true;
        }
        else
        {
            FileJurnal.Visible = false;
            PnlJournal.Visible = false;
            Pnlfooter.Visible = true;
            PnlJournalGRRP.Visible = false;
        }
    }

    protected void ddlNoBukti_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = ddlnoBukti.SelectedValue;

        //TEXTBOX AUTO WILAYAH
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[3];
        varT[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKONTERID", typeof(string));
        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", varT, "constrmf");

        nosekarang = int.Parse(ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString());
        formatnumber = ge.ListDataset.Tables[0].Rows[0]["NoAwal"].ToString();
        nosebelumnya = ge.ListDataset.Tables[0].Rows[0]["NoSebelumnya"].ToString();

        nonasabah = nosekarang + 1;

        CRNonasabah = SessKdpersh + String.Format("{0:" + formatnumber + "}", nonasabah);
        var nonow = SessKdpersh + String.Format("{0:" + formatnumber + "}", nosekarang);

        updateno = String.Format("{0:" + formatnumber + "}", nonasabah);

        HDNoNasabah.Value = CRNonasabah;
        HDNoSebelumnya.Value = nonow;

        txtNoBukti.Text = "CL" + CRNonasabah;
    }

    protected void ddlJurnal_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Jur = ddlJurnal.SelectedValue;
        //TEXTBOX AUTO CURRENCY
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[3];
        varT[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 40, Jur, typeof(string));
        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKETJURNAL", typeof(string));
        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", varT, "constrmf");

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
        DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[3];
        varGL[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 40, DebitGL, typeof(string));
        varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
        varGL[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", varGL, "constrmf");
        string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
        txtDebit3.Text = DCC;

        //GET KETERANGAN CC
        DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[3];
        varCC[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 40, KreditGL, typeof(string));
        varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
        varCC[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", varCC, "constrmf");
        string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
        txtKredit3.Text = KGL;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        PnlJournal.Visible = true;
        //PnlJournal.Visible = true;
        //PnlReport.Visible = false;
        loaddata();


    }
    protected void btnCancelGRRP_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        PnlJournal.Visible = true;
        pnlIDB.Visible = false;
        Pnlfooter.Visible = false;
        PnlJournalGRRP.Visible = false;
        loaddata();

    }
    protected void Btnnextjournal_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = false;
        //PnlReport.Visible = false;

        pnlIDB.Visible = true;
        PnlGrid.Visible = false;
        //PnlReport.Visible = false;
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

        if (ddlkdBank.Enabled == false && rbJurnal.SelectedValue == "2")
        {
            PnlJournal.Visible = false;
            PnlJournalGRRP.Visible = true;
        }
        //  
        lnkAddRP2_Click();
    }
    protected void BtnnextjournalGRRP_Click(object sender, EventArgs e)
    {

        pnlIDB.Visible = false;
        PnlGrid.Visible = true;
        Pnlfooter.Visible = true;
        PnlJournalGRRP.Visible = false;

    }

    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        if (pnlIDB.Visible == true)
        {
            pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;
            PnlJournal.Visible = true;
            pnlIDB.Visible = false;
            Pnlfooter.Visible = false;
            PnlJournalGRRP.Visible = false;
            loaddata();
        }
        else
        {
            pnlIDB.Visible = true;
            PnlGrid.Visible = false;
            if (rbJurnal.SelectedIndex == 1)
            {
                PnlJournalGRRP.Visible = true;
                Pnlfooter.Visible = false;
            }
            else
            {
                Pnlfooter.Visible = true;
                PnlJournal.Visible = false;
            }
        }

    }

    protected void btnbackheader2_Click(object sender, EventArgs e)
    {



        Response.Redirect(Request.Url.AbsoluteUri);


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
        dr["Description"] = string.Empty;
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
    private void loaddata()
    {
        //DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[0];
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetTypeTrn", Paramsx, "constrmf");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypetrn, "None", "");
        //ddltypetrn.Items.Insert(0, new ListItem("-", "-"));
        //DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[0];
        //gex.ListDataset = gbx.GetDataJustOutPut("dbo.GL_Getcurrency", xParams, "constrmf");

        //gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ddlcurrency, "None", "");
        //ddlcurrency.Items.Insert(0, new ListItem("-", "-"));


        //if (ddlcurrency.SelectedValue == "IDR")
        //    Txtkurs.Text = "1";
        //else
        //    Txtkurs.Text = "0";

        //txtnobukti.Text = string.Empty;
        //txtnote.Text = string.Empty;
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
                    PnlJournal.Visible = true;
                    loaddata();
                    ddlkdBank.Enabled = false;
                    ddlkdBank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKdBank")).Text;
                    txtNoBukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    var ddlnobukti = txtNoBukti.Text.Substring(0, 4);
                    ddlnoBukti.SelectedValue = ddlnobukti;
                    ddlTypeTrn.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypetrn")).Text;
                    var TGLTRN = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Text = TGLTRN.Substring(0, 10);
                    var TGLJTHTEMPO = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgljthtmp")).Text;
                    txttgljthtempo.Text = TGLJTHTEMPO.Substring(0, 10);
                    txtbankgiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbankgiro")).Text;
                    txtnogiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoGiro")).Text;
                    ddlStatusGiro.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatusgiro")).Text;
                    if (ddlStatusGiro.SelectedValue == "C")
                    {
                        txtStatusGiro.Text = "Cek/Giro Cair Atau Tunai";
                    }
                    else if (ddlStatusGiro.SelectedValue == "M")
                    {
                        txtStatusGiro.Text = "Cek/Giro Mundur";
                    }
                    txtKurs.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkurs")).Text;
                    txtjmltrans.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljmlhtrn")).Text;
                    var kurs = txtKurs.Text.ToString().Replace(",0000", "");
                    var jmlhtran = txtjmltrans.Text.ToString().Replace(",0000", "");
                    int x, y, z;
                    x = int.Parse(kurs);
                    y = int.Parse(jmlhtran);
                    z = x * y;
                    var jmlrp = Convert.ToString(z);
                    txtKurs.Text = Convert.ToString(kurs);
                    txtjmltrans.Text = Convert.ToString(jmlhtran);
                    txtjmlrp.Text = Convert.ToString(z);
                    //txtjmlrp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljmlhRP")).Text;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtAlamat1.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAlamat1")).Text;
                    txtAlamat2.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAlamat2")).Text;
                    txtAlamat3.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAlamat3")).Text;
                    txtkurs1.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltxtkurs")).Text;
                    var saldojln = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldoawal")).Text;
                    var saldogiro = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldogiro")).Text;
                    txtsaldojln.Text = saldojln.ToString().Replace(",0000", "");
                    txtsaldogiro.Text = saldogiro.ToString().Replace(",0000", "");
                    rbJurnal.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypejurnal")).Text;
                    var tgl = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgladd")).Text;
                    txttgl.Text = tgl.Substring(0, 10);
                    txtpic.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpic")).Text;

                    //GETTRANSTYPEKET
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlTypeTrn.SelectedValue, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANSKET", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var, "constrmf");

                    string typetrn = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();
                    txtTypeTrn.Text = typetrn;
                    //GETNAMABANK
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkdBank.SelectedValue, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNAMABANK", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var1, "constrmf");

                    string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                    txtnmBank.Text = NamaBank;



                    if (rbJurnal.SelectedValue == "1")
                    {
                        FileJurnal.Visible = true;

                        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[3];
                        ddlJurnal.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejurnal")).Text;
                        varT[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlJurnal.SelectedValue, typeof(string));
                        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKETJURNAL", typeof(string));
                        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", varT, "constrmf");

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
                        DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[3];
                        varGL[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 40, DebitGL, typeof(string));
                        varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
                        varGL[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", varGL, "constrmf");
                        string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
                        txtDebit3.Text = DCC;

                        //GET KETERANGAN CC
                        DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[3];
                        varCC[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 40, KreditGL, typeof(string));
                        varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
                        varCC[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", varCC, "constrmf");
                        string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
                        txtKredit3.Text = KGL;
                    }
                    else if (rbJurnal.SelectedValue == "2")
                    {
                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[1];
                        Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoBukti.Text, typeof(string));

                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.GL_Get_Transaksi", Paramsc, "constrmf").Tables[0];

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
                    txtNoBukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 8, txtNoBukti.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
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

                break;
            case "PDF":

                try
                {
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    //DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[3];
                    //Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnnourut.Text, typeof(string));
                    //Paramsc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    //Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_Delete_Transaksi", Paramsc, "constrmf");

                    //CrystalReportSource.repo
                    ReportDocument rprt = new ReportDocument();

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[1];
                    //Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnnourut.Text, typeof(string));

                    //ge.ListDataTable = gb.GetDataJustOutPut("dbo.GL_Report_001", Paramsc, "constrmf").Tables[0];

                    //if (ge.ListDataTable.Rows.Count > 0)
                    //{


                    //    rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                    //    rprt.SetDataSource(ge.ListDataTable);
                    //    Response.Buffer = false;
                    //    // Clear the response content and headers
                    //    Response.ClearContent();
                    //    Response.ClearHeaders();
                    //    Response.ContentType = "application/pdf";
                    //    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    //}


                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    if (e.CommandName != "PDF")
                    {
                        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
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
        string STATEML = "";
        try
        {

            //dtRP.Columns.Clear();

            //dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
            //dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
            //dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
            //dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
            //dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));
            //dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
            //dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));


            //dtRP.Rows.Clear();


            //for (int i = 0; i < GRRP.Rows.Count; i++)
            //{


            //    DataRow dr = dtRP.NewRow();


            //    dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
            //    dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
            //    dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
            //    dr["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
            //    dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
            //    dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            //    dr["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);


            //    dtRP.Rows.Add(dr);

            //}


            if (ddlkdBank.Enabled == true)
            {
                STATE = "INSERT";
                STATEDL = "INSERTDL";
                STATEML = "";
                string nocountersekarang = HDNoNasabah.Value.Substring(2);
                string nocountersebelumnya = HDNoSebelumnya.Value.Substring(2);

                var selected = ddlnoBukti.SelectedValue;
                var KdCompany = SessKdpersh;

                //UPDATE NO BUKTI COUNTER TO TABLE SuKonter
                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                var2[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nocountersekarang, typeof(string));
                var2[1] = gb.CreateSPParameter("@bankGiro", ParameterDirection.Input, SqlDbType.VarChar, 50, nocountersebelumnya, typeof(string));
                var2[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, KdCompany, typeof(string));
                var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEKONTERID", typeof(string));
                var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var2, "constrmf");
            }
            else
            {
                STATE = "UPDATE";
                STATEDL = "UPDATEDL";
                STATEML = "";
            }
            var typejurnal = rbJurnal.SelectedValue;

            if (rbJurnal.SelectedValue == "1")
            {
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[14];
                var1[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoBukti.Text, typeof(string));
                var1[1] = gb.CreateSPParameter("@tglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(string));
                var1[2] = gb.CreateSPParameter("@kurs1", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkurs1.Text, typeof(string));
                var1[3] = gb.CreateSPParameter("@kurs", ParameterDirection.Input, SqlDbType.Money, 10, txtKurs.Text, typeof(string));
                var1[4] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlTypeTrn.SelectedValue, typeof(string));
                var1[5] = gb.CreateSPParameter("@Debit1", ParameterDirection.Input, SqlDbType.VarChar, 30, txtDebit1.Text, typeof(string));
                var1[6] = gb.CreateSPParameter("@Debit3", ParameterDirection.Input, SqlDbType.VarChar, 30, txtDebit3.Text, typeof(string));
                var1[7] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtJurnal.Text, typeof(string));
                var1[8] = gb.CreateSPParameter("@JumlahRupiah", ParameterDirection.Input, SqlDbType.Money, 30, txtjmlrp.Text, typeof(string));
                var1[9] = gb.CreateSPParameter("@Kredit1", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKredit1.Text, typeof(string));
                var1[10] = gb.CreateSPParameter("@Kredit3", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKredit3.Text, typeof(string));
                var1[11] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
                var1[11] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var1[12] = gb.CreateSPParameter("@STATEDL", ParameterDirection.Input, SqlDbType.VarChar, 50, STATEDL, typeof(string));
                var1[13] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var1, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
            }
            else if (rbJurnal.SelectedValue == "2")
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
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[10];

                Params[0] = gb.CreateSPParameter("@NoBuktiKns", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnoBukti.SelectedValue, typeof(string));
                Params[1] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTypeTrn.SelectedValue, typeof(string));
                Params[2] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkurs1.Text, typeof(string));
                Params[3] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.Decimal, 0, txtKurs.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[6] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[7] = gb.CreateSPParameter("@Nourut", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoBukti.Text, typeof(string));
                Params[8] = gb.CreateSPParameter("@GL_Transaction_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GL_Trn_Insert_Detail", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

            }

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[23];
            var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkdBank.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlTypeTrn.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoBukti.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@tglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtempo.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@bankGiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtbankgiro.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@noGiro", ParameterDirection.Input, SqlDbType.VarChar, 10, txtnogiro.Text, typeof(string));
            var[7] = gb.CreateSPParameter("@statusgiro", ParameterDirection.Input, SqlDbType.VarChar, 1, txtStatusGiro.Text, typeof(string));
            var[8] = gb.CreateSPParameter("@kurs", ParameterDirection.Input, SqlDbType.Money, 10, txtKurs.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Money, 30, txtjmltrans.Text, typeof(string));
            var[10] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtKeterangan.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
            var[12] = gb.CreateSPParameter("@Alamat1", ParameterDirection.Input, SqlDbType.VarChar, 50, txtAlamat1.Text, typeof(string));
            var[13] = gb.CreateSPParameter("@Alamat2", ParameterDirection.Input, SqlDbType.VarChar, 50, txtAlamat2.Text, typeof(string));
            var[14] = gb.CreateSPParameter("@Alamat3", ParameterDirection.Input, SqlDbType.VarChar, 50, txtAlamat3.Text, typeof(string));
            var[15] = gb.CreateSPParameter("@kurs1", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkurs1.Text, typeof(string));
            var[16] = gb.CreateSPParameter("@saldoJalan", ParameterDirection.Input, SqlDbType.Money, 30, txtsaldojln.Text, typeof(string));
            var[17] = gb.CreateSPParameter("@saldoAwalGr", ParameterDirection.Input, SqlDbType.Money, 30, txtsaldogiro.Text, typeof(string));
            var[18] = gb.CreateSPParameter("@typejurnal", ParameterDirection.Input, SqlDbType.VarChar, 1, typejurnal, typeof(string));
            var[19] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[20] = gb.CreateSPParameter("@kodejurnal", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlJurnal.SelectedValue, typeof(string));
            var[21] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[22] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            if (ddlkdBank.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var, "constrmf");
            }
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);


            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

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
        Response.Redirect(Request.Url.AbsoluteUri);
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


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoa", Params, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlnamaGL")), "-", "-");



            DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[1];
            xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            gex.ListDataset = gbx.GetDataJustOutPut("dbo.GL_GetCoacode", xParams, "constrmf");
            gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");



            ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;
            ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlnamaGL")).Value;

            ((DropDownList)e.Row.FindControl("ddlcostcenter")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlcostcenter")).Value;



            if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
            {
                ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

            }

            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
    }

    public void ddlNamaBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlkdBank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNAMABANK", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var, "constrmf");

        string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
        string Alamat1 = ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
        string Alamat2 = ge.ListDataset.Tables[0].Rows[0]["Alamat2"].ToString();
        string Alamat3 = ge.ListDataset.Tables[0].Rows[0]["Alamat3"].ToString();
        string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();

        string SaldoBerjalan = ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString();
        string saldoGiro = ge.ListDataset.Tables[0].Rows[0]["SaldoAwalGr"].ToString();
        txtnmBank.Text = NamaBank;
        txtAlamat1.Text = Alamat1;
        txtAlamat2.Text = Alamat2;
        txtAlamat3.Text = Alamat3;
        txtsaldogiro.Text = saldoGiro;
        txtsaldojln.Text = SaldoBerjalan;
        txtkurs1.Text = kdcrc;
    }

    public void ddlStatusGiro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatusGiro.SelectedValue == "C")
        {
            txtStatusGiro.Text = "Cek/Giro Cair Atau Tunai";
        }
        else if (ddlStatusGiro.SelectedValue == "M")
        {
            txtStatusGiro.Text = "Cek/Giro Mundur";
        }
        else
        {
            txtStatusGiro.Text = "";
        }
    }
    public void ddltYPEtRN_SelectedIndexChanged(object sender, EventArgs e)
    {
        var typtrn = ddlTypeTrn.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@KdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, typtrn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANSKET", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var, "constrmf");

        string NamaBank = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();
        txtTypeTrn.Text = NamaBank;
        txtKeterangan.Text = NamaBank;
    }
    protected void ddlKodeGL_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

        ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_SGetCoacode", Params, "constrmf");



        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";


        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlnamaGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[2];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");


        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");

        ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue;



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

        cssGridRP();
    }


    protected void ddlnamaGL_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

        ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_xSGetCoacode", Params, "constrmf");



        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";


        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlKodeGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[2];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");


        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");

        ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue;


        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        // UpdatePanel1.Update();
    }

    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        if (decimal.Parse(((TextBox)grow.FindControl("jlhdebit")).Text) > 0)
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
        if (decimal.Parse(((TextBox)grow.FindControl("jlhkredit")).Text) > 0)
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

}