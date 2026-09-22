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
using System.Security.Cryptography;
public partial class CL_Process_MasukTrnCashAdvanceNonBiaya : System.Web.UI.Page
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
    string form = "L5";
    string kodesumber = "CL";
    string KodeKonter = "CL02";
    //string xkodejournal = "HTGAS01";
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
     
  
        txtKeterangan.Text = "Kasbon Operational";
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
                //BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttgljthtmp.Text = txttgltrn.Text;

                //get tanggal server

                //KODE Asuransi
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[4];
                var3[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var3[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSDMST", typeof(string));
                var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Cash_Advance", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kodegl asc";
                ddlkdsup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkdsup.DataTextField = "NamaGl";
                ddlkdsup.DataValueField = "kodegl";
                ddlkdsup.DataBind();
                //ddlkdsup.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddlkdsup.Enabled = false;

                //KODEBANK
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Cash_Advance", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "namabank";
                ddlKdBank.DataValueField = "KodeBank";
                ddlKdBank.DataBind();
                //ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));
                txtNmBank.Text = "Kas Head Teller";
               // ddlKdBank.Enabled = false;


                txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
                DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[1];
                Paramx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetTeller", Paramx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlteller, "None", "");
                ddlteller.Items.Insert(0, new ListItem("ALL", "ALL"));

                string tempuser = (SessBranchID == "000" ? "000TL1" : SessLoginID);

                ddlteller.SelectedValue = tempuser;
                if (tempuser.Substring(3, 3) == "TL1")
                {
                    ddlteller.Enabled = true;
                }
                else
                {
                    ddlteller.Enabled = false;
                }

                if (tempuser == "PUSAT01" || tempuser == "APPROVAL1" || tempuser == "Approval1")
                {
                    pnlHeader.Visible = false;
                } else
                {
                    pnlHeader.Visible= true;
                }

                BindGridEntity();


            }


            //pwcabang system kurs single default IDR kurs 1
            if (SessSystemKurs == "False")
            {
               

                ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                ddlkursbank.SelectedValue = "IDR";
                ddlkursbank.Enabled = false;
                txtkursbank.Text = "1";
                txtkursbank.Enabled = false;
            }
            else
            {
                

                ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                ddlkursbank.SelectedValue = "IDR";
                ddlkursbank.Enabled = true;
                txtkursbank.Text = "1";
                txtkursbank.Enabled = true;
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

    protected void ddlkursBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtkursbank.Text = ddlkursbank.SelectedValue;
    }
    protected void ddlKdBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlKdBank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Biaya_Acrual", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();

            //txtkodejournal.Text=ge.ListDataset.Tables[0].Rows[0]["kodejurnal"].ToString();

            txtNmBank.Text = NamaBank;
            ddlkursbank.SelectedValue = kdcrc;
            txtkursbank.Text = "1";

        }
        else
        {
            //txtkodejournal.Text = "";
            txtNmBank.Text = "";
            ddlkursbank.ClearSelection();
            ddlkursbank.SelectedValue = "IDR";
            txtkursbank.Text = "1";
            

        }

    
    }
    public void BindGridEntity()
    {

        if (SessLoginID == "PUSAT01" || SessLoginID == "Approval1" || SessLoginID == "APPROVAL1")
            {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTAPPROVAL", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[5] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Cash_Advance", var, "constrmf");

           } 
        else
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNONBIAYA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[5] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Cash_Advance", var, "constrmf");
        }

        ge.ListDataset.Tables[0].DefaultView.Sort = "Nobukti desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void ddlteller_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridEntity();
    }
    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrnx.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
            BindGridEntity();
        }
        else
        {
            BindGridEntity();
        }
    }

    decimal total = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var slip = (LinkButton)e.Row.FindControl("BtnSlip");
            var pdf = (LinkButton)e.Row.FindControl("BtnPDF");

            if (SessLoginID.Equals("PUSAT01") || SessLoginID.Equals("APPROVAL1") || SessLoginID.Equals("Approval1"))
            {
                slip.Text = "";
                pdf.Text = "";

            }

            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltxttotals");
            lbltotal.Text = string.Format("{0:N2}", total);

        }
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

                

                    ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                    ddlkursbank.SelectedValue = "IDR";
                    ddlkursbank.Enabled = false;
                    txtkursbank.Text = "1";
                    txtkursbank.Enabled = false;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                   
                    txtCabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblCabang")).Text;
                    txtCabang.Enabled = false;

                    //ddlkdsup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeBankTarget")).Text;
                    //ddlkdsup.Enabled = false;
                    //txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaBankTarget")).Text;
                    //txtNmSup.Enabled = false;


                    ddlKdBank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    ddlKdBank.Enabled = false;


                    txtNmBank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaBank")).Text;
                    txtNmBank.Enabled = false;
                  
                    txttransAP.Enabled = false;
                    
                    txttransAP.Text = string.Format("{0:N2}",decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtKeterangan.Enabled = false;

                    hidden_kodecabang.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodecabang")).Text;
                    hidden_user.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblUserentry")).Text;

                    //pnltrn.Visible = true;
                    Pnlfooter4.Visible = true;
                    btndelete.Visible = true;
                    btnSimpan.Visible = false;

                   
                    DefaultClass.Global.SqlHelper[] ParamPosting = new DefaultClass.Global.SqlHelper[4];
                    ParamPosting[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    ParamPosting[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                    ParamPosting[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                    ParamPosting[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKPOSTING", typeof(String));
                    ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", ParamPosting, "constrmf");
                    string pubposting = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                    if (pubposting == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting!');", true);
                        btnApprove.Visible = false;
                        btndelete.Visible = false;
                    }
                    else
                    {
                        if (SessLoginID == "PUSAT01" || SessLoginID == "APPROVAL1" || SessLoginID == "Approval1")
                        {
                            btnApprove.Visible = true;
                            btnTolak.Visible = true;
                            btndelete.Visible = false;
                            btnSimpan.Visible = false;
                            section_cabang.Visible = true;
                            note_ap.Visible = true;
                        } else
                        {
                            btndelete.Visible = true;
                            btnSimpan.Visible = false;
                            btnApprove.Visible = false;
                            btnTolak.Visible = false;
                            section_cabang.Visible = false;
                            note_ap.Visible = false;
                        }

                       
                       
                        btnSimpan.Visible = false;
                    }

                    string xstatus = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    if (xstatus == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah ada pengembalian!');", true);
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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_Antar_Bank", Paramsc, "constrmf").Tables[0];

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
                        GC.Collect();
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



                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSIKASBON", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    //Paramsc[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                   
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                    
                            rprt.Load(Server.MapPath("~/CL/Transaksi/Report/CLKwitansiKeluar.rpt"));
                       

                        rprt.SetDataSource(ge.ListDataTable);

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
                        GC.Collect();
                    } 
                }

                break;

        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {


            decimal jumlahAP = decimal.Parse(txttransAP.Text);



            if (jumlahAP <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi Masih Kosong');", true);
            }
            else if (ddlKdBank.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank Sumber Masih Kosong');", true);
            }
            else if (ddlkdsup.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank Target Masih Kosong');", true);
            }

            else
            {



                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[14];
                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                Params[2] = gb.CreateSPParameter("@namabank", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNmBank.Text, typeof(string));
                Params[3] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkursbank.SelectedValue, typeof(string));
                
                Params[4] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[6] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                Params[7] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[8] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                Params[9] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[10] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[12] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));

                Params[13] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Delete_Cash_Advcance_NON", Params, "constrmf");
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
                    //btnSimpan.Visible = false;
                    //btnCancel.Visible = false;
                    //nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:window.location.reload();", true);
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:window.location.reload();", true);

        // Response.Redirect(Request.Url.AbsoluteUri);


    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {

               
                decimal jumlahAP = decimal.Parse(txttransAP.Text);
            btnSimpan.Enabled = false;

            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();
            
            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi +  "');", true);
                btnSimpan.Enabled = true;
            }
            else
            {
   
            if (jumlahAP<=0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (ddlKdBank.SelectedValue=="-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank Bayar Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }
            else if (ddlkdsup.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Biaya Masih Kosong');", true);
                btnSimpan.Enabled = true;
            }

            else
            {

                               
                
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[13];

                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                Params[2] = gb.CreateSPParameter("@namabank", ParameterDirection.Input, SqlDbType.VarChar, 100, ddlKdBank.SelectedItem.Text, typeof(string));
                Params[3] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkursbank.SelectedValue, typeof(string));
                Params[4] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[6] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                Params[7] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[8] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                Params[9] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[10] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[12] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Insert_Cash_Advance_NON", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                    btnSimpan.Enabled = true;
                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);
                    btnSimpan.Enabled = true;
                }

                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    //btnCancel.Visible = false;
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

    protected void ddlKdSup_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlkdsup.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdbanktujuan", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARISDMST", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Biaya_Acrual", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["Namabiaya"].ToString();
            txtKeterangan.Text = ge.ListDataset.Tables[0].Rows[0]["Namabiaya"].ToString();
            btnSimpan.Visible = true;
        }
       
    }

    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
        }
    }







    protected void GetKurs_textchanged(object sender, EventArgs e)
    {

        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
        }
        //txttgljthtmp.Text = txttgltrn.Text;
    }



    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal bayar, jumlahtrn;
        bayar = decimal.Parse(((TextBox)grow.FindControl("jlhdibayar")).Text);
        jumlahtrn = decimal.Parse(((Label)grow.FindControl("lbljmlhtrn")).Text);
        if (bayar > 0)
        {          

            if (bayar > jumlahtrn)
            {
                ((TextBox)grow.FindControl("jlhdibayar")).Text = ((Label)grow.FindControl("lbljmlhtrn")).Text;
            }
            //subgridAP_Click();

        }
        else
        {
            
            ((TextBox)grow.FindControl("jlhdibayar")).Text = "0";
            //subgridAP_Click();
            //((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
            //((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
        }
        //GRTP.UseAccessibleHeader = true;
        //GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    
    protected void Check_Clicked(object sender,EventArgs e)
    { 
        if (blnadd == 0)
        {

   
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        if (((CheckBox)grow.FindControl("CheckBox1")).Checked)
        {
            ((TextBox)grow.FindControl("jlhdibayar")).Text = ((Label)grow.FindControl("lbljmlhtrn")).Text;
                ((TextBox)grow.FindControl("jlhdibayar")).Enabled = true;
        }
        else
        {
            ((TextBox)grow.FindControl("jlhdibayar")).Text = "0";
                ((TextBox)grow.FindControl("jlhdibayar")).Enabled = false;
            }

        //subgridAP_Click();
        //GRTP.UseAccessibleHeader = true;
        //GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }





    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (txtNoteAP.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Memo tidak boleh kosong');", true);
            return;
        }

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[13];

        Params[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnobukti.Text, typeof(string));
        Params[1] = gb.CreateSPParameter("@AppDate", ParameterDirection.Input, SqlDbType.DateTime, 10, DateTime.Now.ToString("yyyy-MM-dd"), typeof(DateTime));
        Params[2] = gb.CreateSPParameter("@AppBy", ParameterDirection.Input, SqlDbType.VarChar, 20, "PUSAT01", typeof(string));
        Params[3] = gb.CreateSPParameter("@StatusAp", ParameterDirection.Input, SqlDbType.Int, 1, 5, typeof(int));
        Params[4] = gb.CreateSPParameter("@NoteAp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoteAP.Text, typeof(string));
        Params[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        Params[6] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKeterangan.Text, typeof(string));
        Params[7] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKdBank.SelectedValue, typeof(string));
        Params[8] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
        Params[9] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 2, "01", typeof(string));
        Params[10] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, hidden_kodecabang.Value, typeof(string));
        Params[11] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 20, hidden_user.Value, typeof(string));
        Params[12] = gb.CreateSPParameter("@Tgltrn", ParameterDirection.Input, SqlDbType.DateTime, 0, txttgltrn.Text, typeof(DateTime));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Approve_Cash_Advance_NON", Params, "constrmf");
       
        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        {
            btnSimpan.Visible = false;
            //btnCancel.Visible = false;
            nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:window.location.reload();", true);

        }
        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }


    protected void btnTolak_Click(object sender, EventArgs e)
    {
        if (txtNoteAP.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Memo tidak boleh kosong');", true);
            return;
        }

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];

        Params[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnobukti.Text, typeof(string));
        Params[1] = gb.CreateSPParameter("@AppDate", ParameterDirection.Input, SqlDbType.DateTime, 10, DateTime.Now.ToString("yyyy-MM-dd"), typeof(DateTime));
        Params[2] = gb.CreateSPParameter("@AppBy", ParameterDirection.Input, SqlDbType.VarChar, 20, "PUSAT01", typeof(string));
        Params[3] = gb.CreateSPParameter("@StatusAp", ParameterDirection.Input, SqlDbType.Int, 1, 4, typeof(int));
        Params[4] = gb.CreateSPParameter("@NoteAp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoteAP.Text, typeof(string));
        Params[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Tolak_Cash_Advance_NON", Params, "constrmf");

        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        {
            btnSimpan.Visible = false;
            //btnCancel.Visible = false;
            nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:window.location.reload();", true);

        }
        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
}
