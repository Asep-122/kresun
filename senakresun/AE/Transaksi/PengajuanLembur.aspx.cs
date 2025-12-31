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
using DocumentFormat.OpenXml.Spreadsheet;

public partial class AE_Process_PengajuanLembur : System.Web.UI.Page
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

    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

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





                DefaultClass.Global.SqlHelper[] vark22 = new DefaultClass.Global.SqlHelper[3];
                vark22[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARYAWAN", typeof(string));
                vark22[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                vark22[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_SP_Lembur", vark22, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                ddlnikkaryawan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlnikkaryawan.DataTextField = "TEXT";
                ddlnikkaryawan.DataValueField = "Value";
                ddlnikkaryawan.DataBind();
                ddlnikkaryawan.Items.Insert(0, new ListItem("Pilih Data", "-"));

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
    protected void ddlKdNIK_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlnikkaryawan.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARINIK", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[5] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AE_SP_Lembur", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            

            //txtkodejournal.Text=ge.ListDataset.Tables[0].Rows[0]["kodejurnal"].ToString();
            txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["namakaryawan"].ToString();
            TxtgajiKaryawan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gaji"].ToString()));
            txtakumulasilembur.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["akumulasilembur"].ToString()));
            ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();

        }
        else
        {
            //txtkodejournal.Text = "";
            txtnamakaryawan.Text = "";
            TxtgajiKaryawan.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtakumulasilembur.Text = string.Format("{0:N2}", decimal.Parse("0"));
            ddlbag.SelectedValue = "-";

        }


    }
    protected void btnAdd_Click(object sender, EventArgs e)
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

   
 

        
  
        lnkAddRP2_Click();
        
        Pnlfooter4.Visible = true;

        btnSimpan.Visible = true;
        btnCancel.Visible = true;

        pnladd.Visible = true;
        PnlGrid.Visible = true;
        Pnlfooter4.Visible = true;
        txtKeterangan.Text = "Lembur Karyawan";
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
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_SP_Lembur", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti desc";


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
                    
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                   
                    txtKeterangan.Enabled = false;
                    //txtbataspinjaman.Enabled = false;
                    //txtsaldobelumlunas.Enabled = false;
                  
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;
                   
                    Pnlfooter4.Visible = true;
                   
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLEMBUR", typeof(string));
                    var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    
                    ge.ListDataset = gb.GetDataJustOutPut("AE_SP_Lembur", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {

        
                        txttgltrn.Text= DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString()).ToString("dd-MM-yyyy");
                        txtjumlahreward.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhLembur"].ToString()));
                        //txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tenor"].ToString()));
                        txtKeterangan.Text = ge.ListDataset.Tables[0].Rows[0]["notememo"].ToString();

                    }


                    txttgltrn.Enabled = false;
                    //rbpotong.Enabled = false;
                    txtKeterangan.Enabled = false;
                    txttgltrn.Enabled = false;
                    //txtangsuran.Enabled = false;
                    //txtjmlpinjaman.Enabled = false;


                  
                    //txtjmlhCL.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    //ddltypejur.Enabled = false;
                    //Pnlfooter1.Visible = true;
                    btnSimpan.Visible = false;
                   
                    Pnlfooter4.Visible = true;
                    btndelete.Visible = false;
                 
                    btngenerate.Visible = false;

                    PnlGrid.Visible = true;
                    DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[5];
                    Paramsx[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDTLLEMBUR", typeof(string));
                    Paramsx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                  
                    ge.ListDataTable = gb.GetDataJustOutPut("AE_SP_Lembur", Paramsx, "constrmf").Tables[0];

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
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, "AE", typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_AR", Paramsc, "constrmf").Tables[0];

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
                    typetrn = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    if (typetrn != "5")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Lembur Belum di approve Pusat');", true);
                    }
                    else
                    {

                    
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIP", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AE_SP_Lembur", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/AE/Report/CR/RptLembur.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                     
                            //rprt.SetParameterValue("Reportname", "Cetak Pinjaman");
                        

                        Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    }

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
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var1[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
            //var1[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, Convert.ToDecimal(txtjmlpinjaman.Text), typeof(decimal));

            ge.ListDataset = gb.GetDataJustOutPut("AE_Lembur_Delete", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Visible = false;
                btnCancel.Visible = false;
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
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            decimal jlhdibayar = decimal.Parse(txtjumlahreward.Text);
           
            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Lembur Masih Kosong!');", true);
            }
            
            else
            {
                dtRP.Columns.Clear();

                dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
                dtRP.Columns.Add(new DataColumn("Kodenik", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Bagian", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Namakaryawan", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Lembur", typeof(decimal)));


                //string nomorrekening = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();
                dtRP.Rows.Clear();
                for (int i = 0; i < GRRP.Rows.Count; i++)

                {
                    DataRow dr = dtRP.NewRow();

                    dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNo")).Text;
                    dr["Kodenik"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
                    dr["Bagian"] = ((Label)GRRP.Rows[i].FindControl("Lblbagian")).Text;
                    dr["Namakaryawan"] = ((Label)GRRP.Rows[i].FindControl("Lblnama")).Text;
                    dr["Lembur"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblLembur")).Text);
                    

                    dtRP.Rows.Add(dr);
                }

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[10];
                var1[0] = gb.CreateSPParameter("@AE_Lembur", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahreward.Text, typeof(decimal));
                var1[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                var1[5] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                var1[6] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                var1[7] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                var1[8] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 30, "AE", typeof(string));
                var1[9] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
               
                ge.ListDataset = gb.GetDataJustOutPut("AE_Lembur_INSERT", var1, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    btnCancel.Visible = false;
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
        dtRP.Columns.Add(new DataColumn("Kodenik", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Bagian", typeof(string)));
        dtRP.Columns.Add(new DataColumn("KetBagian", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Namakaryawan", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Lembur", typeof(decimal)));


        //dr = dtRP.NewRow();
        //dr["nomor"] = 1;
        //dr["noanggota"] = string.Empty;
        //dr["plafon"] = decimal.Zero;
        //dr["psnreward"] = decimal.Zero;
        //dr["reward"] = decimal.Zero;
        //dtRP.Rows.Add(dr);

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

            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNo")).Text;
            dr["Kodenik"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
            dr["Bagian"] = ((Label)GRRP.Rows[i].FindControl("Lblbagian")).Text;
            dr["KetBagian"] = ((Label)GRRP.Rows[i].FindControl("lblketbagian")).Text;
            dr["Namakaryawan"] = ((Label)GRRP.Rows[i].FindControl("Lblnama")).Text;
            dr["Lembur"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblLembur")).Text);
      
           
            dtRP.Rows.Add(dr);
        }

        DataRow drdnew = dtRP.NewRow();
        drdnew["nomor"] = dgcount + 1;
        drdnew["Kodenik"] = string.Empty;
        drdnew["Bagian"] = string.Empty;
        drdnew["KetBagian"] = string.Empty;
        drdnew["Namakaryawan"] = string.Empty;
        drdnew["Lembur"] = decimal.Zero;
       
        dtRP.Rows.Add(drdnew);

        ViewState["ListDetailRP"] = dtRP;

        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
        subgridGL_Click();
        cssGridRP();
    }
    protected void GRRP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "DEL":

                int dgcount = GRRP.Rows.Count;


                ((Label)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNo")).Text = "";

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRP"];

                DataRow drCurrentRow = null;

                dtRP.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRRP.Rows.Count; i++)
                {

                    if (((Label)GRRP.Rows[i].FindControl("lblNo")).Text != "")
                    {


                        drCurrentRow = dtRP.NewRow();

                        drCurrentRow["nomor"] = j;
                        drCurrentRow["Kodenik"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
                        drCurrentRow["Bagian"] = ((Label)GRRP.Rows[i].FindControl("Lblbagian")).Text;
                        drCurrentRow["Ketbagian"] = ((Label)GRRP.Rows[i].FindControl("lblketbagian")).Text;
                        drCurrentRow["Namakaryawan"] = ((Label)GRRP.Rows[i].FindControl("Lblnama")).Text;
                        drCurrentRow["Lembur"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblLembur")).Text);
                        

                        dtRP.Rows.Add(drCurrentRow);

                        j++;
                    }
                }


                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRP();
                subgridGL_Click();

                break;
        }
    }
   
 


   

   

    private void subgridGL_Click()
    {
        int dgcount = GRRP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        decimal plafon = 0;
        decimal sumplafon = 0;
        decimal reward = 0;
        decimal sumreward = 0;
        if (dgcount > 0)
        {
            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                plafon = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblLembur")).Text);
                sumplafon = sumplafon + plafon;
          
            }
        }
        else
        {
            sumplafon = 0;
            sumreward = 0;
        }
        string formatted = string.Format("{0:N2}", sumplafon);
        txtjumlahreward.Text = formatted;
    }

   
     public void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            //decimal pinjaman = decimal.Parse(txtjmlpinjaman.Text.ToString());
            if (ddlnikkaryawan.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Kode Nik Lembur tidak boleh Kosong');", true);
            }
            else if (decimal.Parse(txtjumlahlembur.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Nilai Lembur tidak Lebih boleh 0');", true);
            }
            else if (decimal.Parse(txtjumlahlembur.Text)+ decimal.Parse(TxtgajiKaryawan.Text) + decimal.Parse(txtakumulasilembur.Text) > 4500000)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Jumlah Lembur melebihi Ketentuan');", true);
            }
            else
            {

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
               
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIDATA", typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[4] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnikkaryawan.SelectedValue, typeof(string)) ;
                ge.ListDataset = gb.GetDataJustOutPut("AE_SP_Lembur", var, "constrmf");
                //Boolean status = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["stl"].ToString());
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('NIK tersebut sudah ada di pengajuan Lembur lain di bulan ini yang belum selesai');", true);

                }

                else
                {
                    int dgcount = GRRP.Rows.Count;
                    int datax = 0;
                    for (int i = 0; i < dgcount; i++)
                    {
                       
                        if (((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text == ddlnikkaryawan.SelectedValue)
                        {
                            datax = 1;
                        }

                    }

                    if (datax == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Kode Nik tersebut sudah ada di dalam List ');", true);
                    }
                   else
                    {
                                               

                        dtRP.Columns.Clear();

                        dtRP = (DataTable)ViewState["ListDetailRP"];

                        dtRP.Rows.Clear();

                        for (int i = 0; i < dgcount; i++)
                        {
                            DataRow dr = dtRP.NewRow();

                            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNo")).Text;
                            dr["Kodenik"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
                            dr["Bagian"] = ((Label)GRRP.Rows[i].FindControl("Lblbagian")).Text;
                            dr["KetBagian"] = ((Label)GRRP.Rows[i].FindControl("lblketbagian")).Text;
                            dr["Namakaryawan"] = ((Label)GRRP.Rows[i].FindControl("Lblnama")).Text;
                            dr["Lembur"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblLembur")).Text);
                          
                            dtRP.Rows.Add(dr);
                        }

                        DataRow drdnew = dtRP.NewRow();
                        drdnew["nomor"] = dgcount + 1;
                        drdnew["Kodenik"] = ddlnikkaryawan.SelectedValue;
                        drdnew["Bagian"] = ddlbag.SelectedValue;
                        drdnew["KetBagian"] = ddlbag.SelectedItem.Text;
                        drdnew["Namakaryawan"] = txtnamakaryawan.Text;
                        drdnew["Lembur"] = string.Format("{0:N2}", decimal.Parse(txtjumlahlembur.Text));

                       
                        dtRP.Rows.Add(drdnew);

                        ViewState["ListDetailRP"] = dtRP;

                        GRRP.DataSource = dtRP;
                        GRRP.DataBind();

                        GRRP.UseAccessibleHeader = true;
                        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
                        subgridGL_Click();
                        cssGridRP();

                    }






               
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                }

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
       
    }


  



}
