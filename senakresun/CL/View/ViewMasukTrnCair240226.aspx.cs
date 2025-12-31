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

public partial class CL_ViewMasukTrnCair240226 : System.Web.UI.Page
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

                ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketing.DataTextField = "namaAO";
                ddlmarketing.DataValueField = "NIK";
                ddlmarketing.DataBind();
                ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));

                ddlmarketinganggota.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketinganggota.DataTextField = "namaAO";
                ddlmarketinganggota.DataValueField = "NIK";
                ddlmarketinganggota.DataBind();
                ddlmarketinganggota.Items.Insert(0, new ListItem("ALL", "ALL"));

              
                BindGridEntityX1();
                BindGridEntity();
                BindGridEntityTLDropping();
                BindGridEntityTLKEANGGOTAAN();
                //BindGridEntityTLKEWAJIBAN();
                BindGridEntityTLTambahCadangan();
                BindGridEntityTLSetoranWajib();
                BindGridEntityTLTambahCadanganBaru();
                BindGridEntityTLMATERAI();
                BindGridEntityTLPROVISI();
                btnSaldoawal(sender, e);

                tambahteller = total + totalx2 + totalx3 +  totalx5 + totalx6 + totalx7 + totalx8 + totalx9;

                txtkastellerBertambah.Text = string.Format("{0:N2}", tambahteller);
                TxtsaldoKasTellerawal.Text = string.Format("{0:N2}", saldoawal);

                BindGridEntityTLDroppingKurang();
                BindGridEntityTLKembaliCadangan();
                BindGridEntityTLRekonKurang();
                BindGridEntityTLSetoranWajibTARIK();
                BindGridEntityTLPenutupanAnggota();
                BindGridEntityTLKeluarCadanganlama();

                kurangteller = totalK1 + totalK2 + totalK3 + totalK4 + totalK5 + totalK6 + Plafon;
                txtKastellerBerkurang.Text = string.Format("{0:N2}", kurangteller);
                TxtsaldoKasTeller.Text = string.Format("{0:N2}", saldoawal + (tambahteller - kurangteller));

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
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSALDOAWALTLR", typeof(string));
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
    decimal Biayauangmuka = 0, biayaanggota = 0, biayasimpananwajib = 0, jlhditerimabersih = 0;
    decimal pengembaliancadangan = 0, cadangansebelumnya = 0, provisi = 0,materai = 0, cadx = 0;

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

       
            case "PDF":

                try
                {

                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_rekening")).Text;

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

    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));

            cadx += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cadz"));
            provisi += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "provisi"));
            materai += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "meterai"));
            

            Biayauangmuka += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "biayauangmuka"));
            biayaanggota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "biayaanggota"));
            biayasimpananwajib += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "biayasimpananwajib"));
            jlhditerimabersih += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jlhditerimabersih"));
            pengembaliancadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pengembaliancadangan"));
            cadangansebelumnya += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cadangansebelumnya"));
            cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalplafon = (Label)e.Row.FindControl("lbltotalplafon");
            lbltotalplafon.Text = string.Format("{0:N2}", Plafon);
            Label lbltotalcadangan = (Label)e.Row.FindControl("lbltotalcadangan");
            lbltotalcadangan.Text = string.Format("{0:N2}", cadangan);


            Label lblBiayauangmuka = (Label)e.Row.FindControl("lblBiayauangmuka");
            lblBiayauangmuka.Text = string.Format("{0:N2}", Biayauangmuka);
            Label lblBiayaKeanggotaan = (Label)e.Row.FindControl("lblBiayaKeanggotaan");
            lblBiayaKeanggotaan.Text = string.Format("{0:N2}", biayaanggota);
            Label lblBiayaKewajiban = (Label)e.Row.FindControl("lblBiayaKewajiban");
            lblBiayaKewajiban.Text = string.Format("{0:N2}", biayasimpananwajib);
            Label lblditerimabersih = (Label)e.Row.FindControl("lblditerimabersih");
            lblditerimabersih.Text = string.Format("{0:N2}", jlhditerimabersih);
            Label lblpengembaliancad = (Label)e.Row.FindControl("lblpengembaliancad");
            lblpengembaliancad.Text = string.Format("{0:N2}", pengembaliancadangan);
            Label lblcadsebelumnya = (Label)e.Row.FindControl("lblcadsebelumnya");
            lblcadsebelumnya.Text = string.Format("{0:N2}", cadangansebelumnya);


            Label lbltotalmaterai = (Label)e.Row.FindControl("lbltotalmaterai");
            lbltotalmaterai.Text = string.Format("{0:N2}", materai);
            Label lbltotalprovisi = (Label)e.Row.FindControl("lbltotalprovisi");
            lbltotalprovisi.Text = string.Format("{0:N2}", provisi);
            Label lblBiayacadangan = (Label)e.Row.FindControl("lblBiayacadangan");
            lblBiayacadangan.Text = string.Format("{0:N2}", cadx);
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

    decimal totalx2 = 0;
    protected void GrdDropingKasTeller_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalx2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalx2);

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

    decimal totalx7 = 0;
    protected void GrdCdBaru_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalx7 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalx7);

        }
    }

    decimal totalx8 = 0;
    protected void GrdMaterai_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalx8 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalx8);

        }
    }

    decimal totalx9 = 0;
    protected void GrdProvisi_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalx9 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalx9);

        }
    }

    decimal totalK1 = 0;
    protected void GrdDropingKasTellerKurang_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalK1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalK1);

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

    decimal totalK3 = 0;
    protected void GrdRekonKasTellerKurang_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalK3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalK3);

        }
    }

    decimal totalK4 = 0;
    protected void GRDTARIKSIMPWAJIB_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalK4 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalK4);

        }
    }
    decimal totalK5 = 0;
    protected void GrdPenutupanAnggota_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalK5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalK5);

        }
    }

    decimal totalK6 = 0;
    protected void GrdCdLama_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalK6 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", totalK6);

        }
    }
    public void BindGridEntityX1()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "B3", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTANGSX", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[6] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        var[7] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti desc";


        GRDatax.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDatax.DataBind();

        GRDatax.UseAccessibleHeader = true;
        GRDatax.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTELLER", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_drop desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityTLKEANGGOTAAN()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLKEANGGOTAAN", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        var[6] = gb.CreateSPParameter("@KodeGL", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketinganggota.SelectedValue, typeof(string));
        //ddlmarketing_anggota_SelectedIndexChanged


        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_drop desc";


        GrdKeanggotaanKasTeller.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdKeanggotaanKasTeller.DataBind();

        GrdKeanggotaanKasTeller.UseAccessibleHeader = true;

        GrdKeanggotaanKasTeller.HeaderRow.TableSection = TableRowSection.TableHeader;

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
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

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
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GrdDropingKasTellerKurang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdDropingKasTellerKurang.DataBind();

        GrdDropingKasTellerKurang.UseAccessibleHeader = true;

        GrdDropingKasTellerKurang.HeaderRow.TableSection = TableRowSection.TableHeader;

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
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

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
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo asc";


        GrdKembaliCadangan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdKembaliCadangan.DataBind();

        GrdKembaliCadangan.UseAccessibleHeader = true;

        GrdKembaliCadangan.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityTLRekonKurang()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLRKURANGREKON", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GrdRekonKasTellerKurang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdRekonKasTellerKurang.DataBind();

        GrdRekonKasTellerKurang.UseAccessibleHeader = true;

        GrdRekonKasTellerKurang.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityTLSetoranWajib()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLRTMWAJIB", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GrdSetoranSimpananWajib.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdSetoranSimpananWajib.DataBind();

        GrdSetoranSimpananWajib.UseAccessibleHeader = true;

        GrdSetoranSimpananWajib.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityTLSetoranWajibTARIK()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLRTRKWAJIB", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GRDTARIKSIMPWAJIB.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDTARIKSIMPWAJIB.DataBind();

        GRDTARIKSIMPWAJIB.UseAccessibleHeader = true;

        GRDTARIKSIMPWAJIB.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityTLPenutupanAnggota()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLRTUTUPANG", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GrdPenutupanAnggota.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdPenutupanAnggota.DataBind();

        GrdPenutupanAnggota.UseAccessibleHeader = true;

        GrdPenutupanAnggota.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    public void BindGridEntityTLKeluarCadanganlama()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTLRCDGLAMA", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo asc";


        GrdCdLama.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdCdLama.DataBind();

        GrdCdLama.UseAccessibleHeader = true;

        GrdCdLama.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    public void BindGridEntityTLTambahCadanganBaru()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCDCAIRTMB", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        GrdCdBaru.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdCdBaru.DataBind();

        GrdCdBaru.UseAccessibleHeader = true;

        GrdCdBaru.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    public void BindGridEntityTLMATERAI()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMATERAI", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        GrdMaterai.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdMaterai.DataBind();

        GrdMaterai.UseAccessibleHeader = true;

        GrdMaterai.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityTLPROVISI()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPROVISI", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        GrdProvisi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdProvisi.DataBind();

        GrdProvisi.UseAccessibleHeader = true;

        GrdProvisi.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void ddlteller_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridEntity();
        BindGridEntityX1();
        BindGridEntityTLDropping();
        BindGridEntityTLKEANGGOTAAN();
        //BindGridEntityTLKEWAJIBAN();
        BindGridEntityTLTambahCadangan();
        BindGridEntityTLSetoranWajib();

        BindGridEntityTLTambahCadanganBaru();
        BindGridEntityTLMATERAI();
        BindGridEntityTLPROVISI();
        btnSaldoawal(sender, e);

        tambahteller = total + totalx2 + totalx3 + totalx5 + totalx6 + totalx7 + totalx8 + totalx9;
       

        txtkastellerBertambah.Text = string.Format("{0:N2}", tambahteller);
        TxtsaldoKasTellerawal.Text = string.Format("{0:N2}", saldoawal);


       

        BindGridEntityTLDroppingKurang();
        BindGridEntityTLKembaliCadangan();
        BindGridEntityTLRekonKurang();
        BindGridEntityTLSetoranWajibTARIK();
        BindGridEntityTLPenutupanAnggota();
        BindGridEntityTLKeluarCadanganlama();

        kurangteller = totalK1 + totalK2 + totalK3 + totalK4 + totalK5 + totalK6 + Plafon;
        txtKastellerBerkurang.Text = string.Format("{0:N2}", kurangteller);
        TxtsaldoKasTeller.Text = string.Format("{0:N2}", saldoawal + (tambahteller - kurangteller));
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


    }

    protected void ddlmarketing_SelectedIndexChanged(object sender, EventArgs e)
    {
    
        BindGridEntityX1();
    }

    protected void ddlmarketing_anggota_SelectedIndexChanged(object sender, EventArgs e)
    {

       
        BindGridEntityTLKEANGGOTAAN();
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

        BindGridEntity();
        BindGridEntityX1();
        BindGridEntityTLDropping();
        BindGridEntityTLKEANGGOTAAN();
      
        BindGridEntityTLSetoranWajib();
        BindGridEntityTLTambahCadangan();
        BindGridEntityTLTambahCadanganBaru();
        BindGridEntityTLMATERAI();
        BindGridEntityTLPROVISI();
        btnSaldoawal(sender, e);

        tambahteller = total + totalx2 + totalx3 + totalx5 + totalx6 + totalx7 + totalx8 + totalx9;
    

        txtkastellerBertambah.Text = string.Format("{0:N2}", tambahteller);
        TxtsaldoKasTellerawal.Text = string.Format("{0:N2}", saldoawal);

        BindGridEntityTLDroppingKurang();
        BindGridEntityTLKembaliCadangan();
        BindGridEntityTLRekonKurang();
        BindGridEntityTLSetoranWajibTARIK();
        BindGridEntityTLPenutupanAnggota();
        BindGridEntityTLKeluarCadanganlama();

        kurangteller = totalK1 + totalK2 + totalK3 + totalK4 + totalK5 + totalK6 + Plafon;
        txtKastellerBerkurang.Text = string.Format("{0:N2}", kurangteller);
        TxtsaldoKasTeller.Text = string.Format("{0:N2}", saldoawal + (tambahteller - kurangteller));




        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

 
   
   


}
