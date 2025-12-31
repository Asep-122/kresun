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
using DocumentFormat.OpenXml.EMMA;
using System.Security.AccessControl;

public partial class CL_View_ViewCadangan_X : System.Web.UI.Page
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
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A2";
    string kodesumber = "MK";
    string KodeKonter = "AP03";
    public string nobuktidb;
    string no_bukti;
    decimal tambahteller = 0;
    decimal kurangteller = 0;
    decimal saldoawal = 0;
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
               

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
              
               
               txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
                DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[1];
                Paramx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetTeller", Paramx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlteller, "None", "");
                ddlteller.Items.Insert(0, new ListItem("ALL", "ALL"));
                ddlteller.SelectedValue = SessLoginID;


                // COMBO MASTER MO
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_REPORT_KODEMARKETING", var1, "constrmf");

 

                ddlmarketinganggota.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketinganggota.DataTextField = "namaAO";
                ddlmarketinganggota.DataValueField = "NIK";
                ddlmarketinganggota.DataBind();
                ddlmarketinganggota.Items.Insert(0, new ListItem("ALL", "ALL"));


                ddlmarketinganggota1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketinganggota1.DataTextField = "namaAO";
                ddlmarketinganggota1.DataValueField = "NIK";
                ddlmarketinganggota1.DataBind();
                ddlmarketinganggota1.Items.Insert(0, new ListItem("ALL", "ALL"));


                //BindGridEntityTRNLAINMASUK();
                ////BindGridEntity();
                //BindGridEntityTLDropping();
                //BindGridEntityHTKEMBALIKASBONNON();

                //BindGridEntityX1();

                BindGridEntityTCDGBARU();
                BindGridEntityTCDGLAMA();
                BindGridEntityTLTambahCadangan();

                //BindGridEntityTLRekonKurang();
                //BindGridEntityTLKEWAJIBAN();

                tambahteller = totalS1 + totalS2 + totalS3;

                txtkastellerBertambah.Text = string.Format("{0:N2}", tambahteller);

                //BindGridEntityTLDroppingKurang();
                //BindGridEntityTLKembaliCadangan();

                //BindGridEntityTLDroppingKurang();
                //BindGridEntityTRNLAINKELUAR();
                //BindGridEntityHTBIAYAPENGURUS();
                //BindGridEntityHTSEWAGEDUNG();
                //BindGridEntityPAJAKHTSEWAGEDUNG();
                BindGridEntityTLKembaliCadangan();
                BindGridEntityCDGTGK();
                BindGridEntityGRDCDGAKHR();

                btnSaldoawal(sender, e);

                kurangteller = totalK1 + totalK2 + totalK3;
                txtKastellerBerkurang.Text = string.Format("{0:N0}", kurangteller);
                txtsaldoawal.Text = string.Format("{0:N0}", saldoawal);
                TxtsaldoKasTeller.Text = string.Format("{0:N0}", saldoawal + (tambahteller - kurangteller));

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

            }


          
            


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnSaldoawal(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSALDOAWAL", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CADANGAN_CABANG_X", var, "constrmf");

         
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                saldoawal = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldokemarin"].ToString());
                
            

            }

            else
            {
                saldoawal = 0;
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

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }





    decimal totalS1 = 0;
    protected void GrdTCDGBARU_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalS1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cadangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalS1);

        }
    }
    decimal totalS2 = 0;
    protected void GrdTCDGLAMA_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalS2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cadangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalS2);

        }
    }

    decimal totalS3 = 0;
    protected void GrdTambahCadangan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalS3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalS3);

        }
    }

    decimal totalK1 = 0;
    protected void GrdKembaliCadangan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalK1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalK1);

        }
    }

    decimal totalK2 = 0;
    protected void GRDCDGTGK_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalK2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalK2);

        }
    }

    decimal totalK3 = 0;
    protected void GRDCDGAKHR_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalK3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalK3);

        }
    }
    

    public void BindGridEntityTCDGBARU()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCDGBARU", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        var[6] = gb.CreateSPParameter("@KodeGL", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketinganggota.SelectedValue, typeof(string));
        //ddlmarketing_anggota_SelectedIndexChanged


        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CADANGAN_CABANG_X", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_drop desc";


        GrdTCDGBARU.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdTCDGBARU.DataBind();

        GrdTCDGBARU.UseAccessibleHeader = true;

        GrdTCDGBARU.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityTCDGLAMA()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCDGLAMA", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        var[6] = gb.CreateSPParameter("@KodeGL", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketinganggota.SelectedValue, typeof(string));
        //ddlmarketing_anggota_SelectedIndexChanged


        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CADANGAN_CABANG_X", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_drop desc";


        GrdTCDGLAMA.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdTCDGLAMA.DataBind();

        GrdTCDGLAMA.UseAccessibleHeader = true;

        GrdTCDGLAMA.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    public void BindGridEntityTLTambahCadangan()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLRTMBCDG", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CADANGAN_CABANG_X", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo asc";


        GrdTambahCadangan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdTambahCadangan.DataBind();

        GrdTambahCadangan.UseAccessibleHeader = true;

        GrdTambahCadangan.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityTLKembaliCadangan()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLRKMBCDG", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CADANGAN_CABANG_X", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo asc";


        GrdKembaliCadangan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdKembaliCadangan.DataBind();

        GrdKembaliCadangan.UseAccessibleHeader = true;

        GrdKembaliCadangan.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityCDGTGK()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];

        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTPAKAICDGTGK", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CADANGAN_CABANG_X", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo desc";

        GRDCDGTGK.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDCDGTGK.DataBind();

        GRDCDGTGK.UseAccessibleHeader = true;
        GRDCDGTGK.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityGRDCDGAKHR()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];

        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTPAKAICDGAKR", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CADANGAN_CABANG_X", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo desc";

        GRDCDGAKHR.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDCDGAKHR.DataBind();

        GRDCDGAKHR.UseAccessibleHeader = true;
        GRDCDGAKHR.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    
    protected void ddlmarketing_anggota_SelectedIndexChanged(object sender, EventArgs e)
    {


        BindGridEntityTCDGBARU();
    }

    protected void ddlmarketinganggota1_anggota_SelectedIndexChanged(object sender, EventArgs e)
    {


        BindGridEntityTCDGLAMA();
    }


    protected void ddlteller_SelectedIndexChanged(object sender, EventArgs e)
    {



        BindGridEntityTCDGBARU();
        BindGridEntityTCDGLAMA();
        BindGridEntityTLTambahCadangan();
  
        //BindGridEntityTLRekonKurang();
        //BindGridEntityTLKEWAJIBAN();

        tambahteller = totalS1 + totalS2 + totalS3;

        txtkastellerBertambah.Text = string.Format("{0:N2}", tambahteller);

        //BindGridEntityTLDroppingKurang();
        //BindGridEntityTLKembaliCadangan();

        //BindGridEntityTLDroppingKurang();
        //BindGridEntityTRNLAINKELUAR();
        //BindGridEntityHTBIAYAPENGURUS();
        //BindGridEntityHTSEWAGEDUNG();
        //BindGridEntityPAJAKHTSEWAGEDUNG();

        BindGridEntityTLKembaliCadangan();
        BindGridEntityCDGTGK();
        BindGridEntityGRDCDGAKHR();

        btnSaldoawal(sender, e);

        kurangteller = totalK1 + totalK2 + totalK3;

  

        txtKastellerBerkurang.Text = string.Format("{0:N0}", kurangteller);
        txtsaldoawal.Text = string.Format("{0:N0}", saldoawal);
        TxtsaldoKasTeller.Text = string.Format("{0:N0}", saldoawal + (tambahteller - kurangteller));

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }





  


    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }

   
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {


        BindGridEntityTCDGBARU();
        BindGridEntityTCDGLAMA();
        BindGridEntityTLTambahCadangan();

        //BindGridEntityTLRekonKurang();
        //BindGridEntityTLKEWAJIBAN();

        tambahteller = totalS1 + totalS2 + totalS3;

        txtkastellerBertambah.Text = string.Format("{0:N2}", tambahteller);

        //BindGridEntityTLDroppingKurang();
        //BindGridEntityTLKembaliCadangan();

        //BindGridEntityTLDroppingKurang();
        //BindGridEntityTRNLAINKELUAR();
        //BindGridEntityHTBIAYAPENGURUS();
        //BindGridEntityHTSEWAGEDUNG();
        //BindGridEntityPAJAKHTSEWAGEDUNG();
        BindGridEntityTLKembaliCadangan();
        BindGridEntityCDGTGK();
        BindGridEntityGRDCDGAKHR();

        btnSaldoawal(sender, e);

        kurangteller = totalK1 + totalK2 + totalK3;
        txtKastellerBerkurang.Text = string.Format("{0:N0}", kurangteller);
        txtsaldoawal.Text = string.Format("{0:N0}", saldoawal);
        TxtsaldoKasTeller.Text = string.Format("{0:N0}", saldoawal + (tambahteller - kurangteller));

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

 
   
   


}
