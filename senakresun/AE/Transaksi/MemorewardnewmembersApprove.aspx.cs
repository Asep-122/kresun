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
public partial class AE_Process_MemorewardnewmembersApprove : System.Web.UI.Page
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;
        GRRP.Enabled = true;
       
        PnlFront.Visible = false;

       
        txtnobukti.Text = "";
        //pnladd.Visible = true;

        
        //txtKeterangan.Text = "Terima Simpok";
        Pnlcustomer.Visible = true;
    
       

        
        Pnlcustomer.Visible = true;
        lnkAddRP2_Click();
        
        Pnlfooter4.Visible = true;

       
        btnCancel.Visible = true;
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
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAPINJAMANAPPROVE", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_SP_REWARD", var, "constrmf");

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

    protected void btnreload_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridEntity();




            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
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
                  
                  
                    blnadd = 1;
                    //GRTP.Enabled = false;
                   
                    Pnlfooter4.Visible = true;
                    Pnlcustomer.Visible = false;
                    pnlmemo.Visible = true;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txtkodekaryawan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnik")).Text;
                    Hdnkodecabang.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                    var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, Hdnkodecabang.Value, typeof(string));
                    var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAREWARDAP", typeof(string));
                    var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_SP_REWARD", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtkodekaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["KodeNik"].ToString(); ;
                        txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
                        //txtbataspinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonkasbon"].ToString()));
                        txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglmasuk"].ToString()).ToString("dd-MM-yyyy");
                        //txtmasakerja.Text = ge.ListDataset.Tables[0].Rows[0]["masakerja"].ToString();
                        ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
                        ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString();
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
                        ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["golongan"].ToString();
                        ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statuskaryawan"].ToString();
                        //txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["netgaji"].ToString()));
                        //txtsaldobelumlunas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobelumlunas"].ToString()));
                        //txtlimitkasbon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["limitkasbon"].ToString()));
                        //txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranperbulan"].ToString()));
                        txtnote.Text= ge.ListDataset.Tables[0].Rows[0]["notememo"].ToString();
                        txtnoteverifikasi.Text = ge.ListDataset.Tables[0].Rows[0]["notemc"].ToString();
                        txttgltrn.Text= DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString()).ToString("dd-MM-yyyy");
                        txtjumlahreward.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jlhreward"].ToString()));
                        //txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tenor"].ToString()));
                        txtKeterangan.Text = ge.ListDataset.Tables[0].Rows[0]["notememo"].ToString();
                        ddlSP.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["bulan"].ToString();
                        ddltahun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tahun"].ToString();
                    }


                   
                    //rbpotong.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString() == "1" ? "1" : "2"; rbpotong.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString() == "0" ? "2" : ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString();
                    //txtkodekaryawan.Text = "";
                    //txtnamakaryawan.Text = "";
                    //ddlkursSup.SelectedValue = "IDR";
                    //txtkursstd.Text = "1";

                    //txtsaldobelumlunas.Enabled = false;
                    //txtangsuranperbulan.Enabled = false;
                    //txtbataspinjaman.Enabled = false;
                    txttgltrn.Enabled = false;
                    //rbpotong.Enabled = false;
                    txtKeterangan.Enabled = false;
                    txttgltrn.Enabled = false;
                    //txtangsuran.Enabled = false;
                    //txtjmlpinjaman.Enabled = false;


                  
                    //txtjmlhCL.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    //ddltypejur.Enabled = false;
                    //Pnlfooter1.Visible = true;
               
                   
                    Pnlfooter4.Visible = true;
                 
                 
                    btngenerate.Visible = false;

                    PnlGrid.Visible = true;
                    DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[6];
                    Paramsx[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, Hdnkodecabang.Value, typeof(string));
                    Paramsx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAREWARDDTLAP", typeof(string));
                    Paramsx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramsx[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("AE_SP_REWARD", Paramsx, "constrmf").Tables[0];

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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Reward New Members Belum di approve Pusat');", true);
                    }
                    else
                    {

                    
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAEREWARDNEWMEMBERS", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AE_Trn_AEreport", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/AE/Report/CR/CLKwitansikeluar.rpt"));
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
        //try
        //{
        //    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
        //    var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
        //    var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //    var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //    var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEPINJAMAN", typeof(string));
        //    var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //    //var1[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
        //    //var1[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, Convert.ToDecimal(txtjmlpinjaman.Text), typeof(decimal));

        //    ge.ListDataset = gb.GetDataJustOutPut("AE_PINJAMANKARYAWAN_Delete", var1, "constrmf");
        //    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        //    {
        //        btnSimpan.Visible = false;
        //        btnCancel.Visible = false;
        //        nobuktidb = txtnobukti.Text;
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','Data Berhasil Di hapus','bottom-center','success','2');", true);

        //    }

        //    else
        //    {

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
        //        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //    }
        //}

        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}
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
        //try
        //{
        //    decimal jlhdibayar = decimal.Parse(txtjumlahreward.Text);
           
        //    if (jlhdibayar <= 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Reward Masih Kosong!');", true);
        //    }
            
        //    else
        //    {
        //        dtRP.Columns.Clear();

                
        //        dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
        //        dtRP.Columns.Add(new DataColumn("noanggota", typeof(string)));
        //        dtRP.Columns.Add(new DataColumn("plafon", typeof(decimal)));
        //        dtRP.Columns.Add(new DataColumn("psnreward", typeof(decimal)));
        //        dtRP.Columns.Add(new DataColumn("reward", typeof(decimal)));

        //        //string nomorrekening = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();
        //        dtRP.Rows.Clear();
        //        for (int i = 0; i < GRRP.Rows.Count; i++)

        //        {
        //            DataRow dr = dtRP.NewRow();

        //            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNo")).Text;
        //            dr["noanggota"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
        //            dr["plafon"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblplafon")).Text);
        //            dr["psnreward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblpsnreward")).Text);
        //            dr["reward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblreward")).Text);


        //            dtRP.Rows.Add(dr);
        //        }

        //        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[17];
        //        var1[0] = gb.CreateSPParameter("@AE_Reward_NewMembers", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
        //        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //        var1[2] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahreward.Text, typeof(decimal));
        //        var1[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
        //        var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        //        var1[5] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
        //        var1[6] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
        //        var1[7] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
        //        var1[8] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodekaryawan.Text, typeof(string));
        //        var1[9] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 30, "AE", typeof(string));
        //        var1[10] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
        //        var1[11] = gb.CreateSPParameter("@NamaKry", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamakaryawan.Text, typeof(string));
        //        var1[12] = gb.CreateSPParameter("@tglmasuk", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljoin.Text, typeof(DateTime));
        //        var1[13] = gb.CreateSPParameter("@statuskry", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlsttskyn.SelectedValue, typeof(string));
        //        var1[14] = gb.CreateSPParameter("@bagian", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbag.SelectedValue, typeof(string));
        //        var1[15] = gb.CreateSPParameter("@jabatan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljab.SelectedValue, typeof(string));
        //        var1[16] = gb.CreateSPParameter("@golongan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgol.SelectedValue, typeof(string));
        //        ge.ListDataset = gb.GetDataJustOutPut("AE_RewardNewMembers_INSERT", var1, "constrmf");
        //        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        //        {
        //            btnSimpan.Visible = false;
        //            btnCancel.Visible = false;
        //            nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);

        //        }

        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
        //            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //        }

        //    }
        //}
        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //}
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
        dtRP.Columns.Add(new DataColumn("noanggota", typeof(string)));
        dtRP.Columns.Add(new DataColumn("plafon", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("psnreward", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("reward", typeof(decimal)));

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
            dr["noanggota"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
            dr["plafon"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblplafon")).Text);
            dr["psnreward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblpsnreward")).Text);
            dr["reward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblreward")).Text);
           
            dtRP.Rows.Add(dr);
        }

        DataRow drdnew = dtRP.NewRow();
        drdnew["nomor"] = dgcount + 1;
        drdnew["noanggota"] = string.Empty;
     
        drdnew["plafon"] = decimal.Zero;
        drdnew["psnreward"] = decimal.Zero;
        drdnew["reward"] = decimal.Zero;
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
                        drCurrentRow["noanggota"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
                        drCurrentRow["plafon"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblplafon")).Text);
                        drCurrentRow["psnreward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblpsnreward")).Text);
                        drCurrentRow["reward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblreward")).Text);


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

                plafon = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblplafon")).Text);
                sumplafon = sumplafon + plafon;
                reward = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblreward")).Text);
                sumreward = sumreward + reward;
            }
        }
        else
        {
            sumplafon = 0;
            sumreward = 0;
        }
        string formatted = string.Format("{0:N2}", sumreward);
        txtjumlahreward.Text = formatted;
    }

   
     public void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            //decimal pinjaman = decimal.Parse(txtjmlpinjaman.Text.ToString());
            if (txtnoanggota.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('No Anggota tidak boleh Kosong');", true);
            }
            else if (decimal.Parse(txtplafon.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Plafon tidak Lebih boleh 0');", true);
            }
            else
            {

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                var[0] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlbag.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIANGGOTA", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_SP_REWARD", var, "constrmf");
                //Boolean status = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["stl"].ToString());
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('No Anggota tersebut sudah ada di pengajuan lain');", true);

                }

                else
                {
                    int dgcount = GRRP.Rows.Count;
                    int datax = 0;
                    for (int i = 0; i < dgcount; i++)
                    {
                       
                        if (((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text == txtnoanggota.Text)
                        {
                            datax = 1;
                        }

                    }

                    if (datax == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('No Anggota tersebut sudah ada di dalam List ');", true);
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
                            dr["noanggota"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
                            dr["plafon"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblplafon")).Text);
                            dr["psnreward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblpsnreward")).Text);
                            dr["reward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblreward")).Text);

                            dtRP.Rows.Add(dr);
                        }

                        DataRow drdnew = dtRP.NewRow();
                        drdnew["nomor"] = dgcount + 1;
                        drdnew["noanggota"] = txtnoanggota.Text;

                        drdnew["plafon"] = string.Format("{0:N2}", decimal.Parse(txtplafon.Text));
                        drdnew["psnreward"] = string.Format("{0:N2}", decimal.Parse("1")); 
                        drdnew["reward"] = string.Format("{0:N2}", (decimal.Parse("1")/100) * decimal.Parse(txtplafon.Text));
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
        //try
        //{
        //    var kodekry = hdnkaryawan.Value;
        //    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        //    var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, kodekry, typeof(string));
        //    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIAEMSTBARU", typeof(string));
        //    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        //    ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", var, "constrmf");
        //    //Boolean status = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["stl"].ToString());
        //    if (ge.ListDataset.Tables[0].Rows.Count > 0)
        //    {
        //        //rbpotong.Enabled = true;
        //        //rbpotong.SelectedValue = "1";
        //        txtkodekaryawan.Text = kodekry;
        //        txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
        //        //txtbataspinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonkasbon"].ToString())); 
        //        txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTerima"].ToString()).ToString("dd-MM-yyyy");
        //        //txtmasakerja.Text = ge.ListDataset.Tables[0].Rows[0]["masakerja"].ToString();
        //        ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
        //        ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();
        //        ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
        //        ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
        //        ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();
        //        //txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhNetGaji"].ToString()));
        //        //txtsaldobelumlunas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["blmlunas"].ToString()));
        //        //txtlimitkasbon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonkasbon"].ToString()) - decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["blmlunas"].ToString()));
                
        //        pnladd.Visible = true;
        //        //Pnlfooter4.Visible = true;
              
        //        btnSimpan.Enabled = true;
             
        //        txtKeterangan.Text = "Reward New Members";
        //    }
        //    PnlGrid.Visible = true;




        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        //}
        //catch (System.Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";

            if (txtnotepusat.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Request Masih Kosong!');", true);
            }
            else
            {




                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, Hdnkodecabang.Value, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVE", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[5] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnotepusat.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_MEMO_REWARD_NEW_MEMBERS_APPROVE", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Verif Pengajuan Sukses','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Selisih dengan Gaji Max');", true);
                //}

            }


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnTolak_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";

            if (txtnotepusat.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Request Masih Kosong!');", true);
            }
            else
            {




                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOLAK", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[5] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnotepusat.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_MEMO_REWARD_NEW_MEMBERS_APPROVE", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Verif Pengajuan Sukses','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Selisih dengan Gaji Max');", true);
                //}

            }


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    [WebMethod]
    public static string GetKaryawan(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
        Params[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));
        Params[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARINIK", typeof(string));
        Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_SP_REWARD", Params, "constrmf");
        
        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);
        return json;
    }






}
