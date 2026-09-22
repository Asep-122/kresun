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
public partial class Kresun_Process_rubahsumberdana : System.Web.UI.Page
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
    string form = "G2";
    string kodesumber = "KP";
    string KodeKonter = "KP13";
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

            }


            if (SessSystemKurs == "False")
            {
                ddlkodecrcsdlama.Items.Insert(0, new ListItem("IDR"));
                ddlkodecrcsdlama.SelectedValue = "IDR";
                ddlkodecrcsdlama.Enabled = false;
                txtkurssdlama.Text = "1";
                txtkurssdlama.Enabled = false;

                ddlcrcsdbaru.Items.Insert(0, new ListItem("IDR"));
                ddlcrcsdbaru.SelectedValue = "IDR";
                ddlcrcsdbaru.Enabled = false;
                txtkurssdbaru.Text = "1";
                txtkurssdbaru.Enabled = false;
            }
            else
            {
                ddlkodecrcsdlama.Items.Insert(0, new ListItem("IDR"));
                ddlkodecrcsdlama.SelectedValue = "IDR";
                ddlkodecrcsdlama.Enabled = true;
                txtkurssdlama.Text = "1";
                txtkurssdlama.Enabled = true;

                ddlcrcsdbaru.Items.Insert(0, new ListItem("IDR"));
                ddlcrcsdbaru.SelectedValue = "IDR";
                ddlcrcsdbaru.Enabled = true;
                txtkurssdbaru.Text = "1";
                txtkurssdbaru.Enabled = true;
            }
            //GRData.UseAccessibleHeader = true;
            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

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
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn desc";
        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void ddlsdbaru_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //var bank = ddlsdbaru.SelectedValue;
            //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            //var[0] = gb.CreateSPParameter("@KodeSumberdanaBaru", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
            //var[1] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnasabah.Text, typeof(decimal));
            //var[2] = gb.CreateSPParameter("@KodeSumberdanaLama",  ParameterDirection.Input, SqlDbType.VarChar, 30, txtKodeSumberdanaLama.Text, typeof(string));
            //var[3] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.Int, 0, 0, typeof(int));
            //var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            //var[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            //var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGA", typeof(string));
            //var[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", var, "constrmf");
            //if (ge.ListDataset.Tables[0].Rows.Count > 0)
            //{
            //    txtNamaSumberdanaBaru.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            //    //ddlcrcsdbaru.SelectedValue = ddlkodecrcsdlama.SelectedValue;
            //    txtbungabaru.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString().Replace(".",",");
            //    txtprovisibaru.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString()));
            //    txtkurssdbaru.Text = string.Format("{0:N2}", 1);//decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kurs"].ToString()));
            //}
            //else
            //{
            //    txtNamaSumberdanaBaru.Text = "";
            //    //ddlcrcsdbaru.SelectedValue = "-";
            //    txtbungabaru.Text = "";
            //    txtprovisibaru.Text = "";
            //    txtkurssdbaru.Text = "1";
            //}
            string[] splitdata = txtProgrampinjaman.Text.Split('-');
            string programpimnjaman = splitdata[0];

            var bank = ddlsdbaru.SelectedValue;

            DefaultClass.Global.SqlHelper[] varsdlama = new DefaultClass.Global.SqlHelper[8];
            varsdlama[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 30, txtjangka.Text, typeof(string));
            varsdlama[1] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.VarChar, 100, txtumur.Text, typeof(decimal));
            varsdlama[2] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
            varsdlama[3] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 100, programpimnjaman, typeof(int));
            varsdlama[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varsdlama[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGALAMA", typeof(string));
            varsdlama[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varsdlama[7] = gb.CreateSPParameter("@kodekategori", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkategori.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", varsdlama, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                txtbungabaru.Text = string.Format("{0:N2}", (decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString())));
                txtNamaSumberdanaBaru.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                txtprovisibaru.Text = string.Format("{0:N2}", decimal.Parse("0"));
            }
            else
            {
                txtNamaSumberdanaBaru.Text = "";
                //ddlcrcsdbaru.SelectedValue = "-";
                txtbungabaru.Text = "";
                txtprovisibaru.Text = "";
                txtkurssdbaru.Text = "1";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
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
                    Pnlfooter4.Visible = true;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    btnSimpan.Visible = true;
                    txttgltrn.Enabled = true;
                    txtKeterangan.Enabled = true;
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

                    insertddl();
                    pnlnasabah.Visible = true;
                    PnlEntry.Visible = true;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    ddlsdbaru.Enabled = false;

                    pnlHeader.Visible = false;
                    blnadd = 1;

                    Pnlfooter4.Visible = true;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    txtnasabah.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    ddlkantorbayarbaru.Enabled = false;
//                    ddlkantorbayarbaru.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeSumberdanaBaru")).Text;

                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[6];
                    varx[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnasabah.Text, typeof(string));
                    varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNASABAH", typeof(string));
                    varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    varx[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    varx[5] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", varx, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtnasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                        txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                        txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                        txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                        txtumur.Text = ge.ListDataset.Tables[0].Rows[0]["umur"].ToString() == "" ? "0" : ge.ListDataset.Tables[0].Rows[0]["umur"].ToString();
                        txtterimachanneling.Text = ge.ListDataset.Tables[0].Rows[0]["terima_channeling"].ToString();
                        HDplafon.Value = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisaplafon"].ToString()));
                        txtidpermohonan.Text = ge.ListDataset.Tables[0].Rows[0]["idpermohonan"].ToString();
                        txtKodeSumberdanaLama.Text = ge.ListDataset.Tables[0].Rows[0]["sumberdana"].ToString();
                        txtProgrampinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["programpinjamanKp"].ToString();
                        txtsisaplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisaplafon"].ToString()));
                        txtttlpokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpokokterbayar"].ToString()));
                        txtbungalama.Text = string.Format("{0:N2}", (decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungas"].ToString())));
                        txtprovisilama.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                        txtNamaSumberdanaLama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                        ddlkodecrcsdlama.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
                        txtkurssdlama.Text = "1";//ge.ListDataset.Tables[0].Rows[0]["kurs"].ToString();
                        btnSimpan.Visible = true;
                        txttgltrn.Enabled = true;
                        txtKeterangan.Enabled = true;
                        hdnkdstaff.Value = ge.ListDataset.Tables[0].Rows[0]["kd_staff"].ToString();

                        txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonx"].ToString()));

                        ddlpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenispensiun"].ToString();
                        ddlkategori.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_kategori"].ToString();
                        ddlKantorBayar.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KantorBayar"].ToString();
                        txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["Kodesimulasi"].ToString();
                        txtidprogrampinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();

                    }
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;

                    txtnorekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                   
                    txtnamanasabah.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaNasabah")).Text;
                    //txtplafon.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPlafon")).Text;
                    //txtsisaplafon.Text = HDplafon.Value;
                    //txtttlpokok.Text = "0";
                    //txtumur.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblUmur")).Text;
                    //txtjangka.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;

                    txtKodeSumberdanaLama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeSumberdanaLama")).Text;
                    txtNamaSumberdanaLama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaSumberdanaLama")).Text;
                    //ddlkodecrcsdlama.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecrcLama")).Text;
                    ddlkodecrcsdlama.SelectedValue = "IDR";
                    txtkurssdlama.Text = "1";
                    //txtkurssdlama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKursLama")).Text;
                    txtbungalama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBungaLama")).Text;
                    txtprovisilama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblProvisiLama")).Text;

                    ddlsdbaru.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeSumberdanaBaru")).Text;
                    txtNamaSumberdanaBaru.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaSumberdanaBaru")).Text;
                    //ddlcrcsdbaru.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecrcbaru")).Text;
                    //txtkurssdbaru.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKursbaru")).Text;
                    ddlcrcsdbaru.SelectedValue = "IDR";
                    txtkurssdbaru.Text = "1";
					// txtkurssdbaru.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKursbaru")).Text;
                    txtbungabaru.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBungaBaru")).Text;
                    txtprovisibaru.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblProvisiBaru")).Text;

                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;

                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;
                    btnSimpan.Visible = false;
                    btndelete.Visible = true;

					DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnasabah.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNASABAH", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[5] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", var, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtterimachanneling.Text = ge.ListDataset.Tables[0].Rows[0]["terima_channeling"].ToString();
                        txtProgrampinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["programpinjamanKp"].ToString();

                    }

                    if (statusclosing == "True")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                        btndelete.Enabled = false;
                    }
                    else
                    {
                        if (statusposting == "True")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting hubungi pusat!');", true);
                            btndelete.Enabled = false;
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
                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.KP_CHANGE_SD", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        Response.Buffer = false;
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
                    typetrn = "B";

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAR", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashAR.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        if (typetrn == "B")
                        {
                            rprt.SetParameterValue("Reportname", "Terima Cash Bank");
                        }
                        else
                        {
                            rprt.SetParameterValue("Reportname", "Keluar Cash Bank");
                        }
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
    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            decimal plafon = 0;
            var kodelgn = hdncustomer.Value;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNASABAH", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[5] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", var, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                txtnasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                txtumur.Text = ge.ListDataset.Tables[0].Rows[0]["umur"].ToString() == "" ? "0" : ge.ListDataset.Tables[0].Rows[0]["umur"].ToString();
                txtterimachanneling.Text = ge.ListDataset.Tables[0].Rows[0]["terima_channeling"].ToString(); 
                HDplafon.Value = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisaplafon"].ToString()));
                txtidpermohonan.Text = ge.ListDataset.Tables[0].Rows[0]["idpermohonan"].ToString();
                txtKodeSumberdanaLama.Text = ge.ListDataset.Tables[0].Rows[0]["sumberdana"].ToString();
                txtProgrampinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["programpinjamanKp"].ToString();
                txtsisaplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisaplafon"].ToString()));
                txtttlpokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpokokterbayar"].ToString()));
                txtbungalama.Text = string.Format("{0:N2}", (decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungas"].ToString())));
                txtprovisilama.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                txtNamaSumberdanaLama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                ddlkodecrcsdlama.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
                txtkurssdlama.Text = "1";//ge.ListDataset.Tables[0].Rows[0]["kurs"].ToString();
                btnSimpan.Visible = true;
                txttgltrn.Enabled = true;
                txtKeterangan.Enabled = true;
                hdnkdstaff.Value = ge.ListDataset.Tables[0].Rows[0]["kd_staff"].ToString();

                ddlpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenispensiun"].ToString();
                ddlkategori.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_kategori"].ToString();
                ddlKantorBayar.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KantorBayar"].ToString();
                txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["Kodesimulasi"].ToString();
                txtidprogrampinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();

                txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonx"].ToString()));

                string[] splitdata = txtProgrampinjaman.Text.Split('-');
                string programpimnjaman = splitdata[0];

                DefaultClass.Global.SqlHelper[] varsdlama = new DefaultClass.Global.SqlHelper[7];
                varsdlama[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 30, txtjangka.Text, typeof(string));
                varsdlama[1] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.VarChar, 100, txtumur.Text, typeof(decimal));
                varsdlama[2] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKodeSumberdanaLama.Text, typeof(string));
                varsdlama[3] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 100, programpimnjaman, typeof(int));
                varsdlama[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string)); 
                varsdlama[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGALAMA", typeof(string));
                varsdlama[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", varsdlama, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtbungalama.Text = string.Format("{0:N2}", (decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaefektif1"].ToString())));
                    
                    DefaultClass.Global.SqlHelper[] varprovisi = new DefaultClass.Global.SqlHelper[4];
                    varprovisi[0] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKodeSumberdanaLama.Text, typeof(string));
                    varprovisi[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    varprovisi[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPROVISILAMA", typeof(string));
                    varprovisi[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", varprovisi, "constrmf");

                    txtprovisilama.Text = string.Format("{0:N2}", decimal.Parse("0"));
                }
                else
                {
                    txtNamaSumberdanaBaru.Text = "";
                    //ddlcrcsdbaru.SelectedValue = "-";
                    txtbungabaru.Text = "";
                    txtprovisibaru.Text = "";
                    txtkurssdbaru.Text = "1";
                }
            }
            else
            {
                txtnasabah.Text = "";
                txtnamanasabah.Text = "";
                txtnorekening.Text = "";
                txtjangka.Text = "";
                txtumur.Text = "";

                txtplafon.Text = "";

                txtKodeSumberdanaLama.Text = "";
                txtbungalama.Text = "";
                txtprovisilama.Text = "";
                txtNamaSumberdanaLama.Text = "";
                ddlkodecrcsdlama.SelectedValue = "IDR";
                txtkurssdlama.Text = "1";

                btnSimpan.Enabled = false;
            }
            insertddl();
            ddlkantorbayarbaru.SelectedValue = ddlKantorBayar.SelectedValue;
            ddlkantor_SelectedIndexChanged(sender, e);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            int jangkalama = 0, jangka = 0;
            double plafon = 0, pokoknasabah = 0, bunganasabah = 0, angsbunganasabah = 0, xangsuran = 0, bungadebitur = 0, jangkabaru = 0;
            decimal totalbunga = 0;
            string State = "";

            if (ddlsdbaru.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Sumberdana yang baru!');", true);
            }
            else
            {


                decimal ttlpokok = decimal.Parse(txtttlpokok.Text);
                if (txtnobukti.Text == "")
                {
                    State = "INSERT";
                }
                if(ttlpokok <= 0)
                {
                    txtplafon.Text = txtplafon.Text;
                }else
                {
                    txtplafon.Text = txtsisaplafon.Text;
                }

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorekening.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMK", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                 
                ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", var, "constrmf");

                decimal jangkabulan = 10;//Convert.ToDecimal(txtjangka.Text) / Convert.ToDecimal(12);

                //bungaFlat = float.Parse(String.Format("{0:0.000}", bungaFlat));      // "123.46"
                float jgka = float.Parse(txtjangka.Text.ToString());

                //float PMTBungaFlat = (1000 * (1 + (bungaFlat) * float.Parse(jangkabulan.ToString()))) / (float.Parse(jangkabulan.ToString()) * 12);
                float bungaefektif = float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungasena"].ToString());

                // POKOK BUNGA KARTU PINJAMAN BIASA 
                plafon = double.Parse(txtplafon.Text.Replace(".", ""));
                double angsuranperbulanefektif = PMT(bungaefektif, int.Parse(txtjangka.Text.ToString()), plafon);

                HDAngsuranPerbulanEfektif.Value = angsuranperbulanefektif.ToString();
                //HDAngsuranPerbulanEfektif.Value = Math.Floor(decimal.Parse(HDAngsuranPerbulanEfektif.Value)).ToString();

                HDBungaEfektif.Value = bungaefektif.ToString();
                //HDBungaBank.Value = bungabank.ToString();

                // PCOF DAN BCOF KARTU PINJAMAN OBS
                //bungaFlat = bungaFlat;
                //double angsuranpcofbcof = PMT(bungabank, int.Parse(txtjangka.Text.ToString().Trim()), plafon);

                //HDAngsuranBank.Value = angsuranpcofbcof.ToString();
                //HDAngsuranBank.Value = Math.Floor(decimal.Parse(HDAngsuranBank.Value)).ToString();
                 
                string[] splitpp = txtProgrampinjaman.Text.Split('-');
                string programpinjaman = splitpp[0];
                string namaprogram = splitpp[1];


                string[] splitsd = ddlsdbaru.Text.Split('-');
                string sd = splitsd[0];

                

                DefaultClass.Global.SqlHelper[] varSD = new DefaultClass.Global.SqlHelper[7];
                varSD[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varSD[1] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjangka.Text, typeof(string));
                varSD[2] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.VarChar, 50, txtumur.Text, typeof(string));
                varSD[3] = gb.CreateSPParameter("@sd", ParameterDirection.Input, SqlDbType.VarChar, 50, sd, typeof(string));
                varSD[4] = gb.CreateSPParameter("@programpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, programpinjaman, typeof(string));
                varSD[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGASD", typeof(string));
                varSD[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", varSD, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan dengan kriteria umur = " + txtumur.Text + ", jangka = " + txtjangka.Text + "dan   program pinjaman = " + namaprogram + "');", true);
                }
                else
                {

                    float bungaFlatOBS = float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungas"].ToString()) / 100;
                    float bungabankOBS = float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungas"].ToString());

                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];
                    varx[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorekening.Text, typeof(string));
                    varx[1] = gb.CreateSPParameter("@kodesumberdanalama", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKodeSumberdanaLama.Text, typeof(string));
                    varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTANGSKELAMA", typeof(string));
                    varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", varx, "constrmf");

                    jangka = int.Parse(txtjangka.Text);
                    jangkabaru =int.Parse(ge.ListDataset.Tables[0].Rows[0]["angs_ke"].ToString());
                    jangkalama = int.Parse(txtjangka.Text);
                    jangkalama = jangkalama - int.Parse(jangkabaru.ToString());
                    txtjangka.Text = jangkalama.ToString();


                    if (jangkabaru == 0)
                    {
                        pokoknasabah = double.Parse(plafon.ToString()) / double.Parse(txtjangka.Text);
                        bunganasabah = double.Parse(plafon.ToString()) * double.Parse(bungaFlatOBS.ToString()) * double.Parse(jangkabulan.ToString());
                        angsbunganasabah = bunganasabah / double.Parse(txtjangka.Text);
                        xangsuran = pokoknasabah + angsbunganasabah;
                    }
                    else
                    {
                        pokoknasabah = double.Parse(plafon.ToString()) / double.Parse(jangkabaru.ToString());
                        bunganasabah = double.Parse(plafon.ToString()) * double.Parse(bungaFlatOBS.ToString()) * double.Parse(jangkabulan.ToString());
                        angsbunganasabah = bunganasabah / double.Parse(jangkabaru.ToString());
                        xangsuran = pokoknasabah + angsbunganasabah;
                    }
                    
                     
                    totalbunga = (decimal.Parse(plafon.ToString()) * jangkabulan) * decimal.Parse(bungaFlatOBS.ToString());
                    //bungaFlatOBS = float.Parse(String.Format("{0:0.000}", bungaFlatOBS));      // "123.46" 

                    float  PMTBungaFlatOBS = (1000 * (1 + (bungaFlatOBS) * float.Parse(jangkabulan.ToString()))) / (float.Parse(jangkabulan.ToString()) * 12);
                    float bungaefektifOBS = gb.Rate_fn(jgka, PMTBungaFlatOBS);

                    // PCOF BCOF KARTU PINJAMAN BIASA  
                    double angsuranperbulanefektifOBS = PMT(bungaefektifOBS, int.Parse(txtjangka.Text.ToString()), plafon);

                    HDAngsuranPerbulanEfektifOBS.Value = angsuranperbulanefektifOBS.ToString();
                    HDAngsuranPerbulanEfektifOBS.Value = Math.Floor(decimal.Parse(HDAngsuranPerbulanEfektifOBS.Value)).ToString();

                    HDBungaEfektifOBS.Value = bungaefektifOBS.ToString();
                    HDBungaBankOBS.Value = bungabankOBS.ToString();
                }

                    String format_no_pk = "", formatnumber = "", formatpknumber = "", nopksebelumnya = "", CRno_pk = "", updatepkno = "";

                long no_pk_sekarang = 0, no_pk = 0;
                //GET NOMER PK

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
                var2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var2[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKONTERNOPK", typeof(string));
                var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", var2, "constrmf");

                no_pk_sekarang = long.Parse(ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString());
                formatpknumber = ge.ListDataset.Tables[0].Rows[0]["NoAwal"].ToString();
                nopksebelumnya = ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString();
                format_no_pk = ge.ListDataset.Tables[0].Rows[0]["format_no_pk"].ToString();

                no_pk = no_pk_sekarang + 1;

                CRno_pk = String.Format("{0:" + formatpknumber + "}", no_pk) + "/" + format_no_pk + SessBranchID + "/" + DateTime.Now.ToString("dd/MM/yyyy").Substring(0, 5).Replace("/", "-") + "/" + DateTime.Now.ToString("dd/MM/yyyy").Substring(6, 4);

                updatepkno = String.Format("{0:" + formatnumber + "}", no_pk);

                string nopk = CRno_pk;

                //ProsesVerifikasi("5", SessLoginID);
                //gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnasabah.Text.Trim() + "KP", typeof(string));
                DefaultClass.Global.SqlHelper[] Params1 = new DefaultClass.Global.SqlHelper[51];
                Params1[0] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 20, txttgltrn.Text, typeof(string));//
                Params1[1] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Money, 0, plafon, typeof(decimal));
                Params1[2] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, "35", typeof(string));
                Params1[3] = gb.CreateSPParameter("@kode_pengguna", ParameterDirection.Input, SqlDbType.VarChar, 3, "-", typeof(string));
                Params1[4] = gb.CreateSPParameter("@no_spk", ParameterDirection.Input, SqlDbType.VarChar, 50, nopk, typeof(string));
                Params1[5] = gb.CreateSPParameter("@tgl_dropx", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Now, typeof(DateTime));
                Params1[6] = gb.CreateSPParameter("@tgl_due", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Now, typeof(DateTime));
                Params1[7] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
                Params1[8] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.Money, 0, 0, typeof(decimal));
                Params1[9] = gb.CreateSPParameter("@taksasi", ParameterDirection.Input, SqlDbType.Money, 0, 0, typeof(decimal));
                Params1[10] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnkdstaff.Value, typeof(string));
                Params1[11] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlsdbaru.SelectedValue, typeof(string));
                Params1[12] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(DateTime));
                Params1[13] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string)); //
                Params1[14] = gb.CreateSPParameter("@cadanganrek", ParameterDirection.Input, SqlDbType.Decimal, 0, 0, typeof(decimal));
                Params1[15] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                Params1[16] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Params1[17] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Params1[18] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                Params1[19] = gb.CreateSPParameter("@AngsuranPerbulanEfektif", ParameterDirection.Input, SqlDbType.Money, 250,  HDAngsuranPerbulanEfektifOBS.Value, typeof(decimal));
                Params1[20] = gb.CreateSPParameter("@BungaEfektif", ParameterDirection.Input, SqlDbType.Float, 250, HDBungaEfektif.Value, typeof(decimal));
                Params1[21] = gb.CreateSPParameter("@AngsuranBank", ParameterDirection.Input, SqlDbType.Float, 250, HDAngsuranPerbulanEfektifOBS.Value, typeof(decimal));
                Params1[22] = gb.CreateSPParameter("@BungaBank", ParameterDirection.Input, SqlDbType.Float, 250, HDBungaEfektifOBS.Value, typeof(decimal));
                Params1[23] = gb.CreateSPParameter("@kodekonter", ParameterDirection.Input, SqlDbType.VarChar, 10, KodeKonter, typeof(string)); // 
                Params1[24] = gb.CreateSPParameter("@KodeSumberdanaBaru", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlsdbaru.SelectedValue, typeof(string)); //
                Params1[25] = gb.CreateSPParameter("@NamaSumberdanaBaru", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNamaSumberdanaBaru.Text, typeof(string)); //
                Params1[26] = gb.CreateSPParameter("@kodecrcbaru", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcrcsdbaru.SelectedValue, typeof(string)); //
                Params1[27] = gb.CreateSPParameter("@Kursbaru", ParameterDirection.Input, SqlDbType.Decimal, 0, Decimal.Parse(txtkurssdbaru.Text), typeof(decimal)); //
                Params1[28] = gb.CreateSPParameter("@BungaBaru", ParameterDirection.Input, SqlDbType.Decimal, 0, Decimal.Parse(txtbungabaru.Text), typeof(decimal)); //
                Params1[29] = gb.CreateSPParameter("@ProvisiBaru", ParameterDirection.Input, SqlDbType.Decimal, 0, Decimal.Parse(txtprovisibaru.Text), typeof(decimal)); //
                Params1[30] = gb.CreateSPParameter("@KodeSumberdanaLama", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKodeSumberdanaLama.Text, typeof(string)); //
                Params1[31] = gb.CreateSPParameter("@NamaSumberdanaLama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNamaSumberdanaLama.Text, typeof(string)); //
                Params1[32] = gb.CreateSPParameter("@kodecrcLama", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkodecrcsdlama.SelectedValue, typeof(string)); //
                Params1[33] = gb.CreateSPParameter("@KursLama", ParameterDirection.Input, SqlDbType.Decimal, 0, Decimal.Parse(txtkurssdlama.Text), typeof(decimal)); // 
                Params1[34] = gb.CreateSPParameter("@BungaLama", ParameterDirection.Input, SqlDbType.Decimal, 0, Decimal.Parse(txtbungalama.Text), typeof(decimal));//
                Params1[35] = gb.CreateSPParameter("@ProvisiLama", ParameterDirection.Input, SqlDbType.Decimal, 0, Decimal.Parse(txtprovisilama.Text), typeof(decimal));//
                Params1[36] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnasabah.Text, typeof(string));//
                Params1[37] = gb.CreateSPParameter("@NamaNasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));//
                Params1[38] = gb.CreateSPParameter("@Plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, Decimal.Parse(txtplafon.Text), typeof(decimal));//
                
                Params1[39] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.Int, 5, txtumur.Text, typeof(int));//
                Params1[40] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnorekening.Text, typeof(string));//
                Params1[41] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 30, kodesumber, typeof(string)); //
                Params1[42] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string)); //
                Params1[43] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)); //
                Params1[44] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string)); //
                Params1[45] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "INSERT", typeof(string)); //
                Params1[46] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string)); //
                Params1[47] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string)); //
                Params1[48] = gb.CreateSPParameter("@totalbungadaridepan", ParameterDirection.Input, SqlDbType.Decimal, 30, decimal.Parse(totalbunga.ToString()), typeof(decimal)); //
                Params1[49] = gb.CreateSPParameter("@xangsuran", ParameterDirection.Input, SqlDbType.Decimal, 30,  xangsuran, typeof(decimal)); //
                Params1[50] = gb.CreateSPParameter("@bngs", ParameterDirection.Input, SqlDbType.Decimal, 30, angsbunganasabah, typeof(decimal)); //
                
                ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", Params1, "constrmf");
                //string asdasd = ge.ListDataset.Tables[1].Rows[0][0].ToString();
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "AlertSuccess('Save Done!','You successfully Approve data','top-right','success','true')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0] [0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {
            string State = "";

            if (ddlsdbaru.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Asuransi Baru Masih Kosong!');", true);
            }
            else
            {
                State = "DELETE";
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[29];
                Params[0] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime)); //
                Params[1] = gb.CreateSPParameter("@kodekonter", ParameterDirection.Input, SqlDbType.VarChar, 10, KodeKonter, typeof(string)); //
                Params[2] = gb.CreateSPParameter("@KodeSumberdanaBaru", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlsdbaru.SelectedValue, typeof(string)); // 
                Params[3] = gb.CreateSPParameter("@NamaSumberdanaBaru", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNamaSumberdanaBaru.Text, typeof(string)); //
                Params[4] = gb.CreateSPParameter("@kodecrcbaru", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcrcsdbaru.SelectedValue, typeof(string)); //
                Params[5] = gb.CreateSPParameter("@Kursbaru", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkurssdbaru.Text, typeof(decimal)); //
                Params[6] = gb.CreateSPParameter("@BungaBaru", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbungabaru.Text, typeof(decimal)); //
                Params[7] = gb.CreateSPParameter("@ProvisiBaru", ParameterDirection.Input, SqlDbType.Decimal, 0, txtprovisibaru.Text, typeof(decimal)); //
                Params[8] = gb.CreateSPParameter("@KodeSumberdanaLama", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKodeSumberdanaLama.Text, typeof(string)); //
                Params[9] = gb.CreateSPParameter("@NamaSumberdanaLama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNamaSumberdanaLama.Text, typeof(string)); //
                Params[10] = gb.CreateSPParameter("@kodecrcLama", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkodecrcsdlama.SelectedValue, typeof(string)); //
                Params[11] = gb.CreateSPParameter("@KursLama", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkurssdlama.Text, typeof(decimal)); //
                Params[12] = gb.CreateSPParameter("@BungaLama", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbungalama.Text, typeof(decimal)); //
                Params[13] = gb.CreateSPParameter("@ProvisiLama", ParameterDirection.Input, SqlDbType.Decimal, 0, txtprovisilama.Text, typeof(decimal)); //
                Params[14] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnasabah.Text, typeof(string)); //
                Params[15] = gb.CreateSPParameter("@NamaNasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string)); //
                Params[16] = gb.CreateSPParameter("@Plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafon.Text, typeof(decimal)); //
                Params[17] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, txtjangka.Text, typeof(int)); //
                Params[18] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.Int, 0, 0, typeof(int)); //
                Params[19] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnorekening.Text, typeof(string)); //
                Params[20] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 30, kodesumber, typeof(string));
                Params[21] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string)); //
                Params[22] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[23] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[24] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[25] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[26] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, State, typeof(string)); //
                Params[27] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string)); //
                Params[28] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", Params, "constrmf");
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
                    btndelete.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Delete Done!','You successfully Approve data','top-right','success','true')", true);
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
        btnSimpan.Visible = false;
        pnlnasabah.Visible = true;
        PnlEntry.Visible = true;
        txtKeterangan.Text = "-";
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
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
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        try
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
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.KP_getSD_Baru", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
    public static double PMT(float yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
    {
        var rate = (double)yearlyInterestRate / 100 / 12;
        var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
        return (rate + (rate / denominator)) * loanAmount;
    }
    public void insertddl()
    {
        //KODE SumberDana
        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[5];
        var3[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var3[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, "000", typeof(string));
        var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSD", typeof(string));
        var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var3[4] = gb.CreateSPParameter("@KodeSumberdanaLama", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKodeSumberdanaLama.Text, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_SD", var3, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "sumberdana asc";
        ddlsdbaru.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsdbaru.DataTextField = "sumberdana";
        ddlsdbaru.DataValueField = "KodeBank";
        ddlsdbaru.DataBind();
        ddlsdbaru.Items.Insert(0, new ListItem("Pilih Data", "-"));

        ge.ListDataset = gb.GetDataFunction("dbo.getkantorbayar()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NamaBank asc"; 

        ddlKantorBayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKantorBayar.DataTextField = "NamaBank";
        ddlKantorBayar.DataValueField = "KodeBank";
        ddlKantorBayar.DataBind();
        ddlKantorBayar.Items.Insert(0, new ListItem("Pilih Kantor Bayar", ""));

        ddlkantorbayarbaru.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkantorbayarbaru.DataTextField = "NamaBank";
        ddlkantorbayarbaru.DataValueField = "KodeBank";
        ddlkantorbayarbaru.DataBind();
        ddlkantorbayarbaru.Items.Insert(0, new ListItem("Pilih Kantor Bayar", ""));
        


        ge.ListDataset = gb.GetDataFunction("dbo.getprodukkategori()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

        ddlkategori.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkategori.DataTextField = "nama";
        ddlkategori.DataValueField = "kode";
        ddlkategori.DataBind();
        ddlkategori.Items.Insert(0, new ListItem("Pilih Kategori", ""));


    }

    protected void ddlkantor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            DefaultClass.Global.SqlHelper[] var22s = new DefaultClass.Global.SqlHelper[3];
            var22s[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SUMBERDANA", typeof(string));
            var22s[1] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkantorbayarbaru.SelectedValue, typeof(string));
            var22s[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var22s, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodesumberDana asc";

            ddlsdbaru.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlsdbaru.DataTextField = "NamaSumberDana";
            ddlsdbaru.DataValueField = "KodesumberDana";
            ddlsdbaru.DataBind();
            ddlsdbaru.Items.Insert(0, new ListItem("Pilih Sumber Dana", "-"));




            if (ddlkantorbayarbaru.SelectedValue == "A009")
            {
                ddlsdbaru.SelectedValue = ddlkantorbayarbaru.SelectedValue;
                ddlsdbaru.Enabled = false;
                txtkodesimulasibaru.Text = "B";

            }
            else
            {
                ddlsdbaru.SelectedValue = "-";
                ddlsdbaru.Enabled = true;
                //DdlAngsurandimuka();
                txtkodesimulasibaru.Text = "A";
              



            }



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    //public void ProsesVerifikasi(string flag, string user)
    //{
    //    string blokir_saldo, blokir_transfer = "";
    //    try
    //    {
    //        blokir_saldo = "0";
    //        blokir_transfer = txtplafon.Text.Replace(".", "");
    //        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[36];


    //        var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnasabah.Text.Trim() + "KP", typeof(string));
    //        var1[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 20, txttgltrn.Text, typeof(string));
    //        var1[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Money, 250, txtplafon.Text, typeof(decimal));
    //        var1[3] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
    //        var1[4] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, "35", typeof(string));
    //        var1[5] = gb.CreateSPParameter("@kode_pengguna", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
    //        var1[6] = gb.CreateSPParameter("@no_spk", ParameterDirection.Input, SqlDbType.VarChar, 50, HDnopk.Value, typeof(string));
    //        var1[7] = gb.CreateSPParameter("@tgl_dropx", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(string));
    //        var1[8] = gb.CreateSPParameter("@tgl_due", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(string)); //
    //        var1[9] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 8, txtjangka.Text, typeof(int));
    //        var1[10] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(decimal));
    //        var1[11] = gb.CreateSPParameter("@taksasi", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(decimal));
    //        var1[12] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[13] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeBank.Value, typeof(string));
    //        var1[14] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglpengajuan.Text, typeof(string)); // 
    //        var1[15] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string)); //
    //        var1[16] = gb.CreateSPParameter("@cadanganrek", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
    //        var1[17] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        var1[18] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        var1[19] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //        var1[20] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //        var1[21] = gb.CreateSPParameter("@nominalprovisi", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(decimal));
    //        var1[22] = gb.CreateSPParameter("@psnprovisi", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(decimal));
    //        var1[23] = gb.CreateSPParameter("@angsuranset", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(decimal)); //
    //        var1[24] = gb.CreateSPParameter("@XID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
    //        var1[25] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
    //        var1[26] = gb.CreateSPParameter("@BlokirTransfer", ParameterDirection.Input, SqlDbType.Money, 250, blokir_transfer, typeof(decimal));
    //        var1[27] = gb.CreateSPParameter("@Asuransi", ParameterDirection.Input, SqlDbType.Money, 250, HDAsuransi.Value, typeof(decimal));
    //        var1[28] = gb.CreateSPParameter("@AngsuranPerbulanEfektif", ParameterDirection.Input, SqlDbType.Money, 250, HDAngsuranPerbulanEfektif.Value, typeof(decimal));
    //        var1[29] = gb.CreateSPParameter("@BungaEfektif", ParameterDirection.Input, SqlDbType.Float, 250, HDBungaEfektif.Value, typeof(decimal));
    //        var1[30] = gb.CreateSPParameter("@KdAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeAsuransi.Value, typeof(decimal));
    //        var1[31] = gb.CreateSPParameter("@kdpersh2", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeBank.Value, typeof(decimal));
    //        var1[32] = gb.CreateSPParameter("@saldoBlokir", ParameterDirection.Input, SqlDbType.Money, 250, blokir_saldo, typeof(decimal));
    //        var1[33] = gb.CreateSPParameter("@AngsuranBank", ParameterDirection.Input, SqlDbType.Money, 250, HDAngsuranBank.Value, typeof(decimal));
    //        var1[34] = gb.CreateSPParameter("@BungaBank", ParameterDirection.Input, SqlDbType.Money, 250, HDBungaBank.Value, typeof(decimal));
    //        var1[35] = gb.CreateSPParameter("@idPermohonan", ParameterDirection.Input, SqlDbType.VarChar, 250, HDid.Value, typeof(decimal));

    //        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KP_Insert_Cair_Kresun", var1, "constrmf");

    //        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //        {

    //            //btnSimpanTolakan.Enabled = false;
    //            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
    //            var[0] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
    //            var[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, user, typeof(string));
    //            var[2] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnonasabah.Text, typeof(string));
    //            var[3] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 200, txtalasantolak.Text, typeof(string));
    //            var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VERIFIKASISTATUS", typeof(string));
    //            var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //            var[6] = gb.CreateSPParameter("@siklus_trx", ParameterDirection.Input, SqlDbType.VarChar, 50, flag, typeof(string));


    //            ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", var, "constrmf");

    //            DefaultClass.Global.SqlHelper[] varSTATE = new DefaultClass.Global.SqlHelper[4];
    //            varSTATE[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
    //            varSTATE[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //            varSTATE[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "UPDATEPKKRESUN", typeof(string));
    //            varSTATE[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //            ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", varSTATE, "constrmf");



    //            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //            {

    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

    //            }

    //            else
    //            {

    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //            }
    //        }

    //        else
    //        {

    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //        }



    //    }

    //    catch (Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //    }


    //}
}
