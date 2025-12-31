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

public partial class CL_View_ViewKasHeadTeller : System.Web.UI.Page
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






                BindGridEntityTRNLAINMASUK();
                //BindGridEntity();
                BindGridEntityTLDropping();
                BindGridEntityHTKEMBALIKASBONNON();
                BindGridEntityTLRekonKurang();
                BindGridEntityGRDHTCEKTAMBAH();
                //BindGridEntityTLKEWAJIBAN();

                tambahteller = totalM1 + totalM2 + totalM4 +  totalM3 + totalM5;

                txtkastellerBertambah.Text = string.Format("{0:N2}", tambahteller);

                //BindGridEntityTLDroppingKurang();
                //BindGridEntityTLKembaliCadangan();

                BindGridEntityTLDroppingKurang();
                BindGridEntityTRNLAINKELUAR();
                BindGridEntityTLNONBIAYAKurang();
                BindGridEntityTLBIAYAAPPROVALKurang();
                BindGridEntityHTBIAYANONAPPROVAL();
                BindGridEntityHTBIAYAPENGURUS();
                BindGridEntityTLBIAYABDMKurang();
                BindGridEntityHTSEWAGEDUNG();
                BindGridEntityPAJAKHTSEWAGEDUNG();
                BindGridEntityGRDHTCEKKELUAR();

                btnSaldoawal(sender, e);

                kurangteller = totalHTK1 + totalHTK2 + totalHTK3 + totalHTK4 + totalHTK5 + totalHTK6 + totalHTK7 + totalHTK8 + totalHTK9 + totalHTK10;
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
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

         
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


    decimal Plafon = 0, Potongan = 0, diterimaAnggota = 0, cadangan = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            Potongan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "potongan"));
            diterimaAnggota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jlhditerimaanggota"));
            cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalplafon = (Label)e.Row.FindControl("lbltotalplafon");
            lbltotalplafon.Text = string.Format("{0:N2}", Plafon);
            Label lbltotalPotongan = (Label)e.Row.FindControl("lbltotalPotongan");
            lbltotalPotongan.Text = string.Format("{0:N2}", Potongan);
            Label lbltotalditerimaanggota = (Label)e.Row.FindControl("lbltotalditerimaanggota");
            lbltotalditerimaanggota.Text = string.Format("{0:N2}", diterimaAnggota);
            Label lbltotalcadangan = (Label)e.Row.FindControl("lbltotalcadangan");
            lbltotalcadangan.Text = string.Format("{0:N2}", cadangan);
        }
    }

    decimal total = 0;
    protected void GRDataX_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));
         
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalSetoran = (Label)e.Row.FindControl("lbltotalSetoran");
            lbltotalSetoran.Text = string.Format("{0:N2}", total);
            
        }
    }


    decimal totalx3 = 0;
    protected void GrdKeanggotaanKasTeller_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalx3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "biayaanggota"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalx3);

        }
    }

    decimal totalx5 = 0;
    protected void GrdTambahCadangan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalx5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalx5);

        }
    }
    decimal totalx6 = 0;
    protected void GrdSetoranSimpananWajib_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalx6 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalx6);

        }
    }

    decimal totalHTK1 = 0;
    protected void GrdDropingKasHTKurang_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK1);

        }
    }
    decimal totalHTK2 = 0;
    protected void GRDTRNLAINKELUAR_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK2);

        }
    }

    decimal totalHTK3 = 0;
    protected void GRDKASBONNONBIAYA_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK3);

        }
    }


    decimal totalHTK4 = 0;
    protected void GrdBiayaApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK4 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK4);

        }
    }
    decimal totalHTK5 = 0;
    protected void GrdBiayaNonApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK5);

        }
    }

    decimal totalHTK6 = 0;
    protected void GRDKRGPENGURUS_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK6 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK6);

        }
    }

    decimal totalHTK7 = 0;
    protected void GRDBDMINV_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK7 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK7);

        }
    }

    decimal totalHTK8 = 0;
    protected void GrdSewaBangunan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK8 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK8);

        }
    }
    decimal totalHTK9 = 0;
    protected void GRDPAJAKHT_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK9 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK9);

        }
    }
    decimal totalHTK10 = 0;
    protected void GRDHTCEKKELUAR_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalHTK10 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NilaiAdjust"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalHTK10);

        }
    }




    decimal totalK2 = 0;
    protected void GrdKembaliCadangan_RowDataBound(object sender, GridViewRowEventArgs e)
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

    decimal totalM4 = 0;
    protected void GrdRekonKasTellerKurang_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalM4 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalM4);

        }
    }
    decimal totalM1 = 0;
    protected void GRDTRNLAINMASUK_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalM1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalM1);

        }
    }

    decimal totalM2 = 0;
    protected void GrdKembaliKasbonNon_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalM2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalM2);

        }
    }
    decimal totalM3 = 0;
    protected void GrdDropingKasTeller_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalM3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalM3);

        }
    }
    decimal totalM5 = 0;
    protected void GRDHTCEKTAMBAH_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalM5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NilaiAdjust"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalM5);

        }
    }


    public void BindGridEntityGRDHTCEKTAMBAH()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MASUKHTADJUST", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn ASC";


        GRDHTCEKTAMBAH.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDHTCEKTAMBAH.DataBind();

        GRDHTCEKTAMBAH.UseAccessibleHeader = true;

        GRDHTCEKTAMBAH.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityGRDHTCEKKELUAR()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KELUARHTADJUST", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn ASC";


        GRDHTCEKKELUAR.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDHTCEKKELUAR.DataBind();

        GRDHTCEKKELUAR.UseAccessibleHeader = true;

        GRDHTCEKKELUAR.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityHTKEMBALIKASBONNON()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTHTKEMBALIKASNON", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn ASC";


        GrdKembaliKasbonNon.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdKembaliKasbonNon.DataBind();

        GrdKembaliKasbonNon.UseAccessibleHeader = true;

        GrdKembaliKasbonNon.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    public void BindGridEntityTLDropping()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLDROPPING", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GrdDropingKasTeller.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdDropingKasTeller.DataBind();

        GrdDropingKasTeller.UseAccessibleHeader = true;

        GrdDropingKasTeller.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityTLDroppingKurang()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLDROPPINGKR", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GrdDropingKasHTKurang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdDropingKasHTKurang.DataBind();

        GrdDropingKasHTKurang.UseAccessibleHeader = true;

        GrdDropingKasHTKurang.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityTLNONBIAYAKurang()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KELUARNONBIAYA", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRDKASBONNONBIAYA.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDKASBONNONBIAYA.DataBind();

        GRDKASBONNONBIAYA.UseAccessibleHeader = true;

        GRDKASBONNONBIAYA.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityTLBIAYAAPPROVALKurang()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KELUARBIAYAAPPROVAL", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo asc";


        GrdBiayaApproval.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdBiayaApproval.DataBind();

        GrdBiayaApproval.UseAccessibleHeader = true;

        GrdBiayaApproval.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityHTBIAYANONAPPROVAL()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNONAPP", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti ASC";


        GrdBiayaNonApproval.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdBiayaNonApproval.DataBind();

        GrdBiayaNonApproval.UseAccessibleHeader = true;

        GrdBiayaNonApproval.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityHTBIAYAPENGURUS()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTHTPENGURUS", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti ASC";


        GRDKRGPENGURUS.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDKRGPENGURUS.DataBind();

        GRDKRGPENGURUS.UseAccessibleHeader = true;

        GRDKRGPENGURUS.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    public void BindGridEntityTLBIAYABDMKurang()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KELUARHTBDM", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo asc";


        GRDBDMINV.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDBDMINV.DataBind();

        GRDBDMINV.UseAccessibleHeader = true;

        GRDBDMINV.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityHTSEWAGEDUNG()

    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KELUARHTSEWAGEDUNG", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GrdSewaBangunan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdSewaBangunan.DataBind();

        GrdSewaBangunan.UseAccessibleHeader = true;

        GrdSewaBangunan.HeaderRow.TableSection = TableRowSection.TableHeader;


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    public void BindGridEntityPAJAKHTSEWAGEDUNG()
    {

        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KELUARHTPAJAKGDG", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRDPAJAKHT.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDPAJAKHT.DataBind();

        GRDPAJAKHT.UseAccessibleHeader = true;

        GRDPAJAKHT.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }




    public void BindGridEntityTLRekonKurang()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLRTAMBAHREKON", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GrdRekonKasTellerKurang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdRekonKasTellerKurang.DataBind();

        GrdRekonKasTellerKurang.UseAccessibleHeader = true;

        GrdRekonKasTellerKurang.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityTRNLAINMASUK()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MASUKTRANSAKSILAIN", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRDTRNLAINMASUK.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDTRNLAINMASUK.DataBind();

        GRDTRNLAINMASUK.UseAccessibleHeader = true;

        GRDTRNLAINMASUK.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityTRNLAINKELUAR()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KELUARTRANSAKSILAIN", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRDTRNLAINKELUAR.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDTRNLAINKELUAR.DataBind();

        GRDTRNLAINKELUAR.UseAccessibleHeader = true;

        GRDTRNLAINKELUAR.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    //GRDTRNLAINKELUAR_RowDataBound


    protected void ddlteller_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridEntityTRNLAINMASUK();
        //BindGridEntity();
        BindGridEntityTLDropping();
        BindGridEntityHTKEMBALIKASBONNON();
        BindGridEntityTLRekonKurang();
        BindGridEntityGRDHTCEKTAMBAH();
        //BindGridEntityTLKEWAJIBAN();

        tambahteller = totalM1 + totalM2 + totalM4 + totalM3 + totalM5;

        txtkastellerBertambah.Text = string.Format("{0:N2}", tambahteller);

        //BindGridEntityTLDroppingKurang();
        //BindGridEntityTLKembaliCadangan();

        BindGridEntityTLDroppingKurang();
        BindGridEntityTRNLAINKELUAR();
        BindGridEntityTLNONBIAYAKurang();
        BindGridEntityTLBIAYAAPPROVALKurang();
        BindGridEntityHTBIAYANONAPPROVAL();
        BindGridEntityHTBIAYAPENGURUS();
        BindGridEntityTLBIAYABDMKurang();
        BindGridEntityHTSEWAGEDUNG();
        BindGridEntityPAJAKHTSEWAGEDUNG();
        BindGridEntityGRDHTCEKKELUAR();

        btnSaldoawal(sender, e);

        kurangteller = totalHTK1 + totalHTK2 + totalHTK3 + totalHTK4 + totalHTK5 + totalHTK6 + totalHTK7 + totalHTK8 + totalHTK9 + totalHTK10;
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
        BindGridEntityTRNLAINMASUK();
        //BindGridEntity();
        BindGridEntityTLDropping();
        BindGridEntityHTKEMBALIKASBONNON();
        BindGridEntityTLRekonKurang();
        BindGridEntityGRDHTCEKTAMBAH();
        //BindGridEntityTLKEWAJIBAN();

        tambahteller = totalM1 + totalM2 + totalM4 + totalM3 + totalM5;

        txtkastellerBertambah.Text = string.Format("{0:N2}", tambahteller);

        //BindGridEntityTLDroppingKurang();
        //BindGridEntityTLKembaliCadangan();

        BindGridEntityTLDroppingKurang();
        BindGridEntityTRNLAINKELUAR();
        BindGridEntityTLNONBIAYAKurang();
        BindGridEntityTLBIAYAAPPROVALKurang();
        BindGridEntityHTBIAYANONAPPROVAL();
        BindGridEntityHTBIAYAPENGURUS();
        BindGridEntityTLBIAYABDMKurang();
        BindGridEntityHTSEWAGEDUNG();
        BindGridEntityPAJAKHTSEWAGEDUNG();
        BindGridEntityGRDHTCEKKELUAR();

        btnSaldoawal(sender, e);

        kurangteller = totalHTK1 + totalHTK2 + totalHTK3 + totalHTK4 + totalHTK5 + totalHTK6 + totalHTK7 + totalHTK8 + totalHTK9 + totalHTK10;
        txtKastellerBerkurang.Text = string.Format("{0:N0}", kurangteller);
        txtsaldoawal.Text = string.Format("{0:N0}", saldoawal);
        TxtsaldoKasTeller.Text = string.Format("{0:N0}", saldoawal + (tambahteller - kurangteller));

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

 
   
   


}
