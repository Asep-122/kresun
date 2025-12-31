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
using CrystalDecisions.Shared.Json;
using System.Activities.Expressions;

public partial class AE_Process_MemofeerewardManagementVerifikasi: System.Web.UI.Page
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
    DataTable dtRPKU = new DataTable("TransactionKU");
    DataTable dtRPMKT = new DataTable("TransactionMKT");
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
        pnlHeader.Visible = false;
        PnlFront.Visible = false;

        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        //pnladd.Visible = true;

        txtPL.Enabled = true;

        //if (txtnobukti.Text == "")
        //{
        //    btnSimpan.Visible = true;
        //    btnSimpan.Enabled = false;
        //    btndelete.Visible = false;
        //}
        //else
        //{
        //    btnSimpan.Visible = false;
        //    btndelete.Visible = true;
        //}


        //Pnlcustomer.Visible = true;
        //lnkAddRP2_Click();
        txtPL.Text = "0";
        Pnlfooter4.Visible = true;

        btnSimpan.Visible = true;
        btnCancel.Visible = true;
        pnladd.Visible = true;
        

        PNLKU.Visible = false;
        pnladd.Visible = true;
        PnlManagement.Visible = true;
        PNLBO.Visible = false;
        PNLOB.Visible = false;
        PNLMKT.Visible = false;
        PNLMKTINFO.Visible = false;
        btnCancel.Visible = true;
        btnnext1.Visible = true;
        btnnext2.Visible = false;
        btnback2.Visible = false;
        Pnlfooter4.Visible = true;
        Pnlpage1.Visible = false;
        SetDetailRPKU();
        SetDetailRPMKT();
        btnSerach_Click(sender, e);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
 
  public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAFEEREWARDMG", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 5, "B", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_SP_REWARD", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "tglupdatememo asc";


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
                    txtPL.Enabled = false;
                    pnlmemo.Visible = true;

                    Pnlfooter4.Visible = true;
                    //Pnlcustomer.Visible = false;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    //txtkodekaryawan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnik")).Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                    var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAFEEREWARD", typeof(string));
                    var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[5] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 5,"A", typeof(string));
                    //var1[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_SP_REWARD", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        //txtkodekaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["KodeNik"].ToString(); ;
                        //txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
                        //txtbataspinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonkasbon"].ToString()));
                        //txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglmasuk"].ToString()).ToString("dd-MM-yyyy");
                        //txtmasakerja.Text = ge.ListDataset.Tables[0].Rows[0]["masakerja"].ToString();
                        //ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
                        //ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString();
                        //ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                        //ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["golongan"].ToString();
                        //ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statuskaryawan"].ToString();
                        //txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["netgaji"].ToString()));
                        //txtsaldobelumlunas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobelumlunas"].ToString()));
                        //txtlimitkasbon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["limitkasbon"].ToString()));
                        //txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranperbulan"].ToString()));
                       
                        txttgltrn.Text= DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString()).ToString("dd-MM-yyyy");
                       
                        //txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tenor"].ToString()));
                        txtKeterangan.Text = ge.ListDataset.Tables[0].Rows[0]["notememo"].ToString();
                        txtnote.Text = ge.ListDataset.Tables[0].Rows[0]["notememo"].ToString();
                        ddlSP.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Bulan"].ToString();
                        ddltahun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Tahun"].ToString();
                        txtjumlahcollection.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Jlhcollection"].ToString()));
                        txtfeemanagement.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["feepersen"].ToString()));
                        txtPL.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhPL"].ToString()));

                        txtManagement.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhManagement"].ToString()));
                        txtnonManagement.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhNonManagement"].ToString()));
                        txttotalkaryawan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Jlhtotalkry"].ToString()));
                        txtBO.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhBO"].ToString()));
                        txtofficeboy.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhOB"].ToString()));
                        txtku.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhKU"].ToString()));
                        txtmarketing.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhMk"].ToString()));
                        txtplreward.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhPLreward"].ToString()));
                        txttambahanPLreward.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhPLtambahanreward"].ToString()));
                    }




                   
                    txttgltrn.Enabled = false;
                   
                    txtKeterangan.Enabled = false;
                    txttgltrn.Enabled = false;
             
                    btnSimpan.Visible = false;
                   
                    Pnlfooter4.Visible = true;
                    btndelete.Visible = false;
                 
                    btngenerate.Visible = false;



                     PnlManagement.Visible = true;
                    DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[5];
                    Paramsx[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAFEEBAGIANE", typeof(string));
                    Paramsx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    //Paramsx[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("AE_SP_REWARD", Paramsx, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        ViewState["ListDetailRP"] = ge.ListDataTable;

                        GRRP.DataSource = ge.ListDataTable;

                        GRRP.DataBind();

                        GRRP.UseAccessibleHeader = true;
                        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                       
                    }
                    else
                    {
                        
                    }


                    PNLBO.Visible = false;
                   

                    PNLOB.Visible = false;
                   
                    PNLKU.Visible = false;
                   
                    PNLMKT.Visible = false;
                   
                    PNLMKTINFO.Visible = false;
                   
                    GRDMKTINFO.Enabled = false;
                    GRDMKT.Enabled = false;
                    GRDKU.Enabled = false;
                    GRDOB.Enabled = false;
                    GRDBO.Enabled = false;
                    GRRP.Enabled = false;

                    Pnlpage1.Visible = true;
                    btnnext1.Visible = false;
                    Pnlfooter4.Visible = true;
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
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEPINJAMAN", typeof(string));
            var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var1[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
            //var1[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, Convert.ToDecimal(txtjmlpinjaman.Text), typeof(decimal));

            ge.ListDataset = gb.GetDataJustOutPut("AE_PINJAMANKARYAWAN_Delete", var1, "constrmf");
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

    protected void btnnext1_Click(object sender, EventArgs e)
    {
        PNLKU.Visible = false;
        pnladd.Visible = false;
        PnlManagement.Visible = false;
        PNLBO.Visible = false;
        PNLOB.Visible = false;
        PNLMKT.Visible = true;
        btnCancel.Visible = false;
        btnnext1.Visible = false;
        btnnext2.Visible = false;
        btnback2.Visible = true;
        Pnlfooter4.Visible = true;
        Pnlpage1.Visible = false;
    
    }

    protected void btnback2_Click(object sender, EventArgs e)
    {
        PNLKU.Visible = true;
        pnladd.Visible = true;
        PnlManagement.Visible = true;
        PNLBO.Visible = true;
        PNLOB.Visible = true;
        PNLMKT.Visible = false;
        btnCancel.Visible = true;
        btnnext1.Visible = true;
        btnnext2.Visible = false;
        btnback2.Visible = false;
        Pnlfooter4.Visible = false;
        Pnlpage1.Visible = true;
    }


    protected void btnnext2_Click(object sender, EventArgs e)
    {
        PNLKU.Visible = false;
        pnladd.Visible = false;
        PnlManagement.Visible = false;
        PNLBO.Visible = false;
        PNLOB.Visible = false;
        PNLMKT.Visible = true;
        btnCancel.Visible = false;
        btnnext1.Visible = false;
        btnnext2.Visible = false;
        btnback2.Visible = false;
        Pnlfooter4.Visible = true;
        Pnlpage1.Visible = false;
    }

    protected void btnback3_Click(object sender, EventArgs e)
    {
        PNLKU.Visible = true;
        pnladd.Visible = false;
        PnlManagement.Visible = false;
        PNLBO.Visible = true;
        PNLOB.Visible = true;
        PNLMKT.Visible = true;
        btnCancel.Visible = true;
        btnnext1.Visible = true;
        btnnext2.Visible = false;
        btnback2.Visible = false;
        Pnlfooter4.Visible = false;
        Pnlpage1.Visible = true;
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            decimal jlhdibayar = decimal.Parse(txtjumlahcollection.Text);
           
            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Reward Masih Kosong!');", true);
            }
            
            else
            {

                
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                var[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKDATAFEEREWARD", typeof(string));
                var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[6] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, "B", typeof(string));
                
                ge.ListDataset = gb.GetDataJustOutPut("AE_SP_REWARD", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Fee Reward di bulan dan tahun tersebut sudah ada!');", true);
                }
                else
                {
                   


               







                dtRP.Columns.Clear();

                
                dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
                dtRP.Columns.Add(new DataColumn("nik", typeof(string)));
                dtRP.Columns.Add(new DataColumn("nama", typeof(string)));
                dtRP.Columns.Add(new DataColumn("bagian", typeof(string)));
                dtRP.Columns.Add(new DataColumn("jabatan", typeof(string)));
                dtRP.Columns.Add(new DataColumn("ketjabatan", typeof(string)));
                dtRP.Columns.Add(new DataColumn("averagekpi", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("feepersen", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("totalbm", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("PL", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("NEWMEMBERS", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("KMK", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("Tunggakan", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("smkuterbaik", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("totalreward", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("totalbmreward", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("kodeKU", typeof(string)));
                dtRP.Columns.Add(new DataColumn("anggotabaru", typeof(int)));
                dtRP.Columns.Add(new DataColumn("anggotaaktif", typeof(int)));
                dtRP.Columns.Add(new DataColumn("Bakidb", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("bakibulanlalu", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("netbakidb", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("tunggakanPokok", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("tunggakanbulanlalu", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("nettunggakan", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("SMTERBAIK", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("urutterbaik", typeof(int)));
                dtRP.Columns.Add(new DataColumn("totalsm", typeof(int)));


                    //string nomorrekening = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();
                    dtRP.Rows.Clear();
                int xbaris = 0;


                    //Management
                    for (int i = 0; i < GRRP.Rows.Count; i++)

                    {
                        DataRow dr = dtRP.NewRow();
                        xbaris = xbaris + 1;
                        dr["nomor"] = xbaris;
                        dr["nik"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
                        dr["nama"] = ((Label)GRRP.Rows[i].FindControl("lblnama")).Text;
                        dr["bagian"] = "E";
                        dr["jabatan"] = ((Label)GRRP.Rows[i].FindControl("lblkodejab")).Text;
                        dr["ketjabatan"] = ((Label)GRRP.Rows[i].FindControl("lblketjab")).Text;
                        dr["averagekpi"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblaverage")).Text);
                        dr["feepersen"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("txtpsnfee")).Text);
                        dr["totalbm"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblBM")).Text);
                        dr["PL"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("txtpl")).Text);
                        dr["NEWMEMBERS"] = decimal.Parse("0");
                        dr["KMK"] = decimal.Parse("0");
                        dr["Tunggakan"] = decimal.Parse("0");
                        dr["smkuterbaik"] = decimal.Parse("0");
                        dr["totalreward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblReward")).Text);
                        dr["totalbmreward"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblBMReward")).Text);
                        dr["kodeKU"] = ((Label)GRRP.Rows[i].FindControl("lblnoanggota")).Text;
                        dr["anggotabaru"] = 0;
                        dr["anggotaaktif"] = 0;
                        dr["Bakidb"] = decimal.Parse("0");
                        dr["bakibulanlalu"] = decimal.Parse("0");
                        dr["netbakidb"] = decimal.Parse("0");
                        dr["tunggakanPokok"] = decimal.Parse("0");
                        dr["tunggakanbulanlalu"] = decimal.Parse("0");
                        dr["nettunggakan"] = decimal.Parse("0");
                        dr["SMTERBAIK"] = decimal.Parse("0");
                        dr["urutterbaik"] = 0;
                        dr["totalsm"] = 0;
                        dtRP.Rows.Add(dr);
                    }








                //    //BO
                //    for (int i = 0; i < GRDBO.Rows.Count; i++)

                //{
                //    DataRow dr = dtRP.NewRow();
                //    xbaris = xbaris + 1;
                //    dr["nomor"] = xbaris;
                //    dr["nik"] = ((Label)GRDBO.Rows[i].FindControl("lblnoanggota")).Text;
                //    dr["nama"] = ((Label)GRDBO.Rows[i].FindControl("lblnama")).Text;
                //    dr["bagian"] = "D";
                //    dr["jabatan"] = ((Label)GRDBO.Rows[i].FindControl("lblkodejab")).Text;
                //    dr["ketjabatan"] = ((Label)GRDBO.Rows[i].FindControl("lblketjab")).Text;
                //    dr["averagekpi"] = decimal.Parse(((Label)GRDBO.Rows[i].FindControl("lblaverage")).Text);
                //    dr["feepersen"] = decimal.Parse(((Label)GRDBO.Rows[i].FindControl("txtpsnfee")).Text);
                //    dr["totalbm"] = decimal.Parse(((Label)GRDBO.Rows[i].FindControl("lblBM")).Text);
                //    dr["PL"] = decimal.Parse(((Label)GRDBO.Rows[i].FindControl("txtpl")).Text);
                //    dr["NEWMEMBERS"] = decimal.Parse("0");
                //    dr["KMK"] = decimal.Parse("0");
                //    dr["Tunggakan"] = decimal.Parse("0");
                //    dr["smkuterbaik"] = decimal.Parse("0");
                //    dr["totalreward"] = decimal.Parse(((Label)GRDBO.Rows[i].FindControl("lblReward")).Text);
                //    dr["totalbmreward"] = decimal.Parse(((Label)GRDBO.Rows[i].FindControl("lblBMReward")).Text);
                //    dr["kodeKU"] = ((Label)GRDBO.Rows[i].FindControl("lblnoanggota")).Text;
                //    dr["anggotabaru"] = 0;
                //    dr["anggotaaktif"] = 0;
                //    dr["Bakidb"] = decimal.Parse("0");
                //    dr["bakibulanlalu"] = decimal.Parse("0");
                //    dr["netbakidb"] = decimal.Parse("0");
                //    dr["tunggakanPokok"] = decimal.Parse("0");
                //    dr["tunggakanbulanlalu"] = decimal.Parse("0");
                //    dr["nettunggakan"] = decimal.Parse("0");
                //    dr["SMTERBAIK"] = decimal.Parse("0");
                //    dr["urutterbaik"] = 0;
                //    dr["totalsm"] = 0;

                //        dtRP.Rows.Add(dr);
                //}






                ////OB
                //for (int i = 0; i < GRDOB.Rows.Count; i++)

                //{
                //    DataRow dr = dtRP.NewRow();
                //    xbaris = xbaris + 1;
                //    dr["nomor"] = xbaris;
                //    dr["nik"] = ((Label)GRDOB.Rows[i].FindControl("lblnoanggota")).Text;
                //    dr["nama"] = ((Label)GRDOB.Rows[i].FindControl("lblnama")).Text;
                //    dr["bagian"] = "C";
                //    dr["jabatan"] = ((Label)GRDOB.Rows[i].FindControl("lblkodejab")).Text;
                //    dr["ketjabatan"] = ((Label)GRDOB.Rows[i].FindControl("lblketjab")).Text;
                //    dr["averagekpi"] = decimal.Parse(((Label)GRDOB.Rows[i].FindControl("lblaverage")).Text);
                //    dr["feepersen"] = decimal.Parse(((Label)GRDOB.Rows[i].FindControl("txtpsnfee")).Text);
                //    dr["totalbm"] = decimal.Parse(((Label)GRDOB.Rows[i].FindControl("lblBM")).Text);
                //    dr["PL"] = decimal.Parse(((Label)GRDOB.Rows[i].FindControl("txtpl")).Text);
                //    dr["NEWMEMBERS"] = decimal.Parse("0");
                //    dr["KMK"] = decimal.Parse("0");
                //    dr["Tunggakan"] = decimal.Parse("0");
                //    dr["smkuterbaik"] = decimal.Parse("0");
                //    dr["totalreward"] = decimal.Parse(((Label)GRDOB.Rows[i].FindControl("lblReward")).Text);
                //    dr["totalbmreward"] = decimal.Parse(((Label)GRDOB.Rows[i].FindControl("lblBMReward")).Text);
                //    dr["kodeKU"] = ((Label)GRDOB.Rows[i].FindControl("lblnoanggota")).Text;
                //    dr["anggotabaru"] = 0;
                //    dr["anggotaaktif"] = 0;
                //    dr["Bakidb"] = decimal.Parse("0");
                //    dr["bakibulanlalu"] = decimal.Parse("0");
                //    dr["netbakidb"] = decimal.Parse("0");
                //    dr["tunggakanPokok"] = decimal.Parse("0");
                //    dr["tunggakanbulanlalu"] = decimal.Parse("0");
                //    dr["nettunggakan"] = decimal.Parse("0");
                //    dr["SMTERBAIK"] = decimal.Parse("0");
                //    dr["urutterbaik"] = 0;
                //    dr["totalsm"] = 0;

                //        dtRP.Rows.Add(dr);
                //}




                ////KU
                //for (int i = 0; i < GRDKU.Rows.Count; i++)

                //{
                //    DataRow dr = dtRP.NewRow();
                //    xbaris = xbaris + 1;
                //    dr["nomor"] = xbaris;
                //    dr["nik"] = ((Label)GRDKU.Rows[i].FindControl("lblnoanggota")).Text;
                //    dr["nama"] = ((Label)GRDKU.Rows[i].FindControl("lblnama")).Text;
                //    dr["bagian"] = "B";
                //    dr["jabatan"] = ((Label)GRDKU.Rows[i].FindControl("lblkodejab")).Text;
                //    dr["ketjabatan"] = ((Label)GRDKU.Rows[i].FindControl("lblketjab")).Text;
                //    dr["averagekpi"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("txtaverage")).Text);
                //    dr["feepersen"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("txtpsnfee")).Text);
                //    dr["totalbm"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("lblBM")).Text);
                //    dr["PL"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("txtpl")).Text);
                //    dr["NEWMEMBERS"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("txtnewmembers")).Text);
                //    dr["KMK"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("txtKMK")).Text);
                //    dr["Tunggakan"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("txtTP")).Text);
                //    dr["smkuterbaik"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("txtSMTErbaik")).Text);
                //    dr["totalreward"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("lblReward")).Text);
                //    dr["totalbmreward"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("lblBMReward")).Text);
                //    dr["kodeKU"] = ((Label)GRDKU.Rows[i].FindControl("lblnoanggota")).Text;
                //    dr["anggotabaru"] = 0;
                //    dr["anggotaaktif"] = 0;
                //    dr["Bakidb"] = decimal.Parse("0");
                //    dr["bakibulanlalu"] = decimal.Parse("0");
                //    dr["netbakidb"] = decimal.Parse("0");
                //    dr["tunggakanPokok"] = decimal.Parse("0");
                //    dr["tunggakanbulanlalu"] = decimal.Parse("0");
                //    dr["nettunggakan"] = decimal.Parse("0");
                //    dr["SMTERBAIK"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("txtSMTErbaik")).Text);
                //    dr["urutterbaik"] = int.Parse(((Label)GRDKU.Rows[i].FindControl("lblterbaik")).Text);
                //    dr["totalsm"] = int.Parse(((Label)GRDKU.Rows[i].FindControl("lbltotalsm")).Text);

                //        dtRP.Rows.Add(dr);
                //}




                ////marketing
                //for (int i = 0; i < GRDMKT.Rows.Count; i++)

                //{
                //    DataRow dr = dtRP.NewRow();
                //    xbaris = xbaris + 1;
                //    dr["nomor"] = xbaris;
                //    dr["nik"] = ((Label)GRDMKT.Rows[i].FindControl("lblnoanggota")).Text;
                //    dr["nama"] = ((Label)GRDMKT.Rows[i].FindControl("lblnama")).Text;
                //    dr["bagian"] = "A";
                //    dr["jabatan"] = ((Label)GRDMKT.Rows[i].FindControl("lblkodejab")).Text;
                //    dr["ketjabatan"] = ((Label)GRDMKT.Rows[i].FindControl("lblketjab")).Text;
                //    dr["averagekpi"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("txtaverage")).Text);
                //    dr["feepersen"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("txtpsnfee")).Text);
                //    dr["totalbm"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lblBM")).Text);
                //    dr["PL"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("txtpl")).Text);
                //    dr["NEWMEMBERS"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("txtnewmembers")).Text);
                //    dr["KMK"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("txtKMK")).Text);
                //    dr["Tunggakan"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("txtTP")).Text);
                //    dr["smkuterbaik"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("txtSMTErbaik")).Text);
                //    dr["totalreward"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lblReward")).Text);
                //    dr["totalbmreward"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lblBMReward")).Text);
                //    dr["kodeKU"] = ((Label)GRDMKT.Rows[i].FindControl("lblnoanggota")).Text;
                //    dr["anggotabaru"] = int.Parse(((Label)GRDMKT.Rows[i].FindControl("lblanggotabaru")).Text);
                //    dr["anggotaaktif"] = int.Parse(((Label)GRDMKT.Rows[i].FindControl("lblanggotaaktif")).Text);
                //    dr["Bakidb"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lblBakidb")).Text);
                //    dr["bakibulanlalu"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lblbakibulanlalu")).Text);
                //    dr["netbakidb"] = decimal.Parse("0");
                //    dr["tunggakanPokok"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lbltunggakanPokok")).Text);
                //    dr["tunggakanbulanlalu"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lbltunggakanbulanlalu")).Text);
                //    dr["nettunggakan"] = decimal.Parse("0");
                //    dr["SMTERBAIK"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("txtSMTErbaik")).Text);
                //    dr["urutterbaik"] = int.Parse(((Label)GRDMKT.Rows[i].FindControl("lblterbaik")).Text);
                //    dr["totalsm"] = int.Parse(((Label)GRDMKT.Rows[i].FindControl("lbltotalsm")).Text);

                //        dtRP.Rows.Add(dr);
                //}


                //    //List storting marketing
                //    for (int i = 0; i < GRDMKTINFO.Rows.Count; i++)

                //    {
                //        DataRow dr = dtRP.NewRow();
                //        xbaris = xbaris + 1;
                //        dr["nomor"] = xbaris;
                //        dr["nik"] = ((Label)GRDMKTINFO.Rows[i].FindControl("lblnoanggota")).Text;
                //        dr["nama"] = ((Label)GRDMKTINFO.Rows[i].FindControl("lblnama")).Text;
                //        dr["bagian"] = "X";
                //        dr["jabatan"] = "A";
                //        dr["ketjabatan"] = "MARKETING";
                //        dr["averagekpi"] = decimal.Parse(((Label)GRDMKTINFO.Rows[i].FindControl("txtaverage")).Text);
                //        dr["feepersen"] = decimal.Parse("0");
                //        dr["totalbm"] = decimal.Parse("0");
                //        dr["PL"] = decimal.Parse("0");
                //        dr["NEWMEMBERS"] = decimal.Parse("0");
                //        dr["KMK"] = decimal.Parse("0");
                //        dr["Tunggakan"] = decimal.Parse("0");
                //        dr["smkuterbaik"] = decimal.Parse("0");
                //        dr["totalreward"] = decimal.Parse("0");
                //        dr["totalbmreward"] = decimal.Parse("0");
                //        dr["kodeKU"] = ((Label)GRDMKTINFO.Rows[i].FindControl("lblnoanggota")).Text;
                //        dr["anggotabaru"] = int.Parse(((Label)GRDMKTINFO.Rows[i].FindControl("lblanggotabaru")).Text); 
                //        dr["anggotaaktif"] = int.Parse(((Label)GRDMKTINFO.Rows[i].FindControl("lblanggotaaktif")).Text);
                //        dr["Bakidb"] = decimal.Parse(((Label)GRDMKTINFO.Rows[i].FindControl("lblBakidb")).Text);
                //        dr["bakibulanlalu"] = decimal.Parse(((Label)GRDMKTINFO.Rows[i].FindControl("lblbakibulanlalu")).Text);
                //        dr["netbakidb"] = decimal.Parse(((Label)GRDMKTINFO.Rows[i].FindControl("lblKMKNET")).Text);
                //        dr["tunggakanPokok"] = decimal.Parse(((Label)GRDMKTINFO.Rows[i].FindControl("lbltunggakanPokok")).Text);
                //        dr["tunggakanbulanlalu"] = decimal.Parse(((Label)GRDMKTINFO.Rows[i].FindControl("lbltunggakanbulanlalu")).Text);
                //        dr["nettunggakan"] = decimal.Parse(((Label)GRDMKTINFO.Rows[i].FindControl("lbltunggakannet")).Text);
                //        dr["SMTERBAIK"] = decimal.Parse("0");
                //        dr["urutterbaik"] = 0;
                //        dr["totalsm"] = 0;
                //        dtRP.Rows.Add(dr);
                //    }





                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[24];
                var1[0] = gb.CreateSPParameter("@AE_Fee_Reward", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var1[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                var1[4] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                var1[5] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                var1[6] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                var1[7] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                var1[8] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                var1[9] = gb.CreateSPParameter("@Jlhcollection", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahcollection.Text, typeof(decimal));
                var1[10] = gb.CreateSPParameter("@feepersen", ParameterDirection.Input, SqlDbType.Money, 0, txtfeemanagement.Text, typeof(decimal));
                var1[11] = gb.CreateSPParameter("@JlhPL", ParameterDirection.Input, SqlDbType.Money, 0, txtPL.Text, typeof(decimal));
                var1[12] = gb.CreateSPParameter("@JlhManagement", ParameterDirection.Input, SqlDbType.Int, 0, txtManagement.Text, typeof(int));
                var1[13] = gb.CreateSPParameter("@JlhNonManagement", ParameterDirection.Input, SqlDbType.Int, 0, txtManagement.Text, typeof(int));
                var1[14] = gb.CreateSPParameter("@Jlhtotalkry", ParameterDirection.Input, SqlDbType.Int, 0, txtnonManagement.Text, typeof(int));
                var1[15] = gb.CreateSPParameter("@JlhBO", ParameterDirection.Input, SqlDbType.Int, 0, txtBO.Text, typeof(int));
                var1[16] = gb.CreateSPParameter("@JlhOB", ParameterDirection.Input, SqlDbType.Int, 0, txtofficeboy.Text, typeof(int));
                var1[17] = gb.CreateSPParameter("@JlhKU", ParameterDirection.Input, SqlDbType.Int, 0, txtku.Text, typeof(int));
                var1[18] = gb.CreateSPParameter("@JlhMk", ParameterDirection.Input, SqlDbType.Int, 0, txtmarketing.Text, typeof(int));
                var1[19] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 30, "AE", typeof(string));
                var1[20] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                var1[21] = gb.CreateSPParameter("@JlhPLreward", ParameterDirection.Input, SqlDbType.Money, 0, txtplreward.Text, typeof(decimal));
                var1[22] = gb.CreateSPParameter("@JlhPLrewardtambahan", ParameterDirection.Input, SqlDbType.Money, 0, txttambahanPLreward.Text, typeof(decimal));
                var1[23] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, "B", typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_Fee_Reward_INSERT", var1, "constrmf");
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






    private void lnkAddRPKU_Click()
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
   
 


   

   

 
   
     public void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            //decimal pinjaman = decimal.Parse(txtjmlpinjaman.Text.ToString());
            if (txtPL.Text.Length == 0)
            {
                txtPL.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Jumlah PL bulan ini Masih Kosong');", true);
            }
            else if (decimal.Parse(txtPL.Text)== 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Jumlah PL bulan ini  Masih Kosong');", true);
            }
          
            else
           {
                PnlManagement.Visible = true;

                if (decimal.Parse(txtPL.Text) >= 100000000)
                {
                    txtplreward.Text = string.Format("{0:N2}", decimal.Parse("100000"));
                    txttambahanPLreward.Text = string.Format("{0:N2}", decimal.Parse("0"));
                    if (decimal.Parse(txtPL.Text) >= 150000000 && decimal.Parse(txtPL.Text) < 200000000)
                    {
                        txttambahanPLreward.Text = string.Format("{0:N2}", decimal.Parse("50000"));
                    }
                    else if (decimal.Parse(txtPL.Text) >= 200000000)
                    {
                        txttambahanPLreward.Text = string.Format("{0:N2}", decimal.Parse("100000"));
                    }
                }
                else
                {
                    txtplreward.Text = string.Format("{0:N2}", decimal.Parse("0"));
                    txttambahanPLreward.Text = string.Format("{0:N2}", decimal.Parse("0"));

                }
                decimal plreward = decimal.Parse(txtplreward.Text) + decimal.Parse(txttambahanPLreward.Text);

                DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[10];
                Paramsx[0] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 10, "E", typeof(string));
                Paramsx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramsx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Paramsx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIDATAMANAGEMENT", typeof(string));
                Paramsx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Paramsx[5] = gb.CreateSPParameter("@jlhcollection", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahcollection.Text, typeof(decimal));
                Paramsx[6] = gb.CreateSPParameter("@pl", ParameterDirection.Input, SqlDbType.Money, 0, txtplreward.Text, typeof(decimal));
                Paramsx[7] = gb.CreateSPParameter("@feemanagement", ParameterDirection.Input, SqlDbType.Money, 0, txtfeemanagement.Text, typeof(decimal));
                Paramsx[8] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                Paramsx[9] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                ge.ListDataTable = gb.GetDataJustOutPut("AE_SPI_Fee_Reward", Paramsx, "constrmf").Tables[0];

                if (ge.ListDataTable.Rows.Count > 0)
                {

                    ViewState["ListDetailRP"] = ge.ListDataTable;

                    GRRP.DataSource = ge.ListDataTable;
                    GRRP.DataBind();

                    GRRP.UseAccessibleHeader = true;
                    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                }

                PNLBO.Visible = false;


                //DefaultClass.Global.SqlHelper[] Paramstx = new DefaultClass.Global.SqlHelper[8];
                //Paramstx[0] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 10, "D", typeof(string));
                //Paramstx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //Paramstx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //Paramstx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIDATABO", typeof(string));
                //Paramstx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //Paramstx[5] = gb.CreateSPParameter("@jlhcollection", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahcollection.Text, typeof(decimal));
                //Paramstx[6] = gb.CreateSPParameter("@pl", ParameterDirection.Input, SqlDbType.Money, 0, txtplreward.Text, typeof(decimal));
                //Paramstx[7] = gb.CreateSPParameter("@feemanagement", ParameterDirection.Input, SqlDbType.Money, 0, txtfeemanagement.Text, typeof(decimal));
                //ge.ListDataTable = gb.GetDataJustOutPut("AE_SPI_Fee_Reward", Paramstx, "constrmf").Tables[0];

                //if (ge.ListDataTable.Rows.Count > 0)
                //{

                //    ViewState["ListDetailRP"] = ge.ListDataTable;

                //    GRDBO.DataSource = ge.ListDataTable;
                //    GRDBO.DataBind();

                //    GRDBO.UseAccessibleHeader = true;
                //    GRDBO.HeaderRow.TableSection = TableRowSection.TableHeader;

                //}


                PNLOB.Visible = false;


                ////DefaultClass.Global.SqlHelper[] Paramspx = new DefaultClass.Global.SqlHelper[8];
                ////Paramspx[0] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 10, "C", typeof(string));
                ////Paramspx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                ////Paramspx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                ////Paramspx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIDATAOB", typeof(string));
                ////Paramspx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ////Paramspx[5] = gb.CreateSPParameter("@jlhcollection", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahcollection.Text, typeof(decimal));
                ////Paramspx[6] = gb.CreateSPParameter("@pl", ParameterDirection.Input, SqlDbType.Money, 0, txtplreward.Text, typeof(decimal));
                ////Paramspx[7] = gb.CreateSPParameter("@feemanagement", ParameterDirection.Input, SqlDbType.Money, 0, txtfeemanagement.Text, typeof(decimal));
                ////ge.ListDataTable = gb.GetDataJustOutPut("AE_SPI_Fee_Reward", Paramspx, "constrmf").Tables[0];

                ////if (ge.ListDataTable.Rows.Count > 0)
                ////{

                ////    ViewState["ListDetailRP"] = ge.ListDataTable;

                ////    GRDOB.DataSource = ge.ListDataTable;
                ////    GRDOB.DataBind();

                ////    GRDOB.UseAccessibleHeader = true;
                ////    GRDOB.HeaderRow.TableSection = TableRowSection.TableHeader;

                ////}

               

                PNLMKTINFO.Visible = false;


                //DefaultClass.Global.SqlHelper[] ParamsMx1 = new DefaultClass.Global.SqlHelper[9];
                //ParamsMx1[0] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 10, "A", typeof(string));
                //ParamsMx1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //ParamsMx1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //ParamsMx1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIDATAMKTINFO", typeof(string));
                //ParamsMx1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //ParamsMx1[5] = gb.CreateSPParameter("@jlhcollection", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahcollection.Text, typeof(decimal));
                //ParamsMx1[6] = gb.CreateSPParameter("@pl", ParameterDirection.Input, SqlDbType.Money, 0, plreward, typeof(decimal));
                //ParamsMx1[7] = gb.CreateSPParameter("@feemanagement", ParameterDirection.Input, SqlDbType.Money, 0, txtfeemanagement.Text, typeof(decimal));
                //ParamsMx1[8] = gb.CreateSPParameter("@jumlahmarketing", ParameterDirection.Input, SqlDbType.Int, 0, txtmarketing.Text, typeof(int));
                //ge.ListDataTable = gb.GetDataJustOutPut("AE_SPI_Fee_Reward", ParamsMx1, "constrmf").Tables[0];

                //if (ge.ListDataTable.Rows.Count > 0)
                //{

                //    ViewState["ListDetailRP"] = ge.ListDataTable;

                //    GRDMKTINFO.DataSource = ge.ListDataTable;
                //    GRDMKTINFO.DataBind();

                //    GRDMKTINFO.UseAccessibleHeader = true;
                //    GRDMKTINFO.HeaderRow.TableSection = TableRowSection.TableHeader;

                //}


                PNLMKT.Visible = false;


                //DefaultClass.Global.SqlHelper[] ParamsMx = new DefaultClass.Global.SqlHelper[8];
                //ParamsMx[0] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 10, "A", typeof(string));
                //ParamsMx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //ParamsMx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //ParamsMx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIDATAMKT", typeof(string));
                //ParamsMx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //ParamsMx[5] = gb.CreateSPParameter("@jlhcollection", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahcollection.Text, typeof(decimal));
                //ParamsMx[6] = gb.CreateSPParameter("@pl", ParameterDirection.Input, SqlDbType.Money, 0,plreward, typeof(decimal));
                //ParamsMx[7] = gb.CreateSPParameter("@feemanagement", ParameterDirection.Input, SqlDbType.Money, 0, txtfeemanagement.Text, typeof(decimal));
                //ge.ListDataTable = gb.GetDataJustOutPut("AE_SPI_Fee_Reward", ParamsMx, "constrmf").Tables[0];

                //if (ge.ListDataTable.Rows.Count > 0)
                //{

                //    ViewState["ListDetailRP"] = ge.ListDataTable;

                //    GRDMKT.DataSource = ge.ListDataTable;
                //    GRDMKT.DataBind();

                //    GRDMKT.UseAccessibleHeader = true;
                //    GRDMKT.HeaderRow.TableSection = TableRowSection.TableHeader;

                //}


                PNLKU.Visible = false;


                //DefaultClass.Global.SqlHelper[] Paramsvx = new DefaultClass.Global.SqlHelper[8];
                //Paramsvx[0] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 10, "B", typeof(string));
                //Paramsvx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //Paramsvx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //Paramsvx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIDATAKU", typeof(string));
                //Paramsvx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //Paramsvx[5] = gb.CreateSPParameter("@jlhcollection", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahcollection.Text, typeof(decimal));
                //Paramsvx[6] = gb.CreateSPParameter("@pl", ParameterDirection.Input, SqlDbType.Money, 0, plreward, typeof(decimal));
                //Paramsvx[7] = gb.CreateSPParameter("@feemanagement", ParameterDirection.Input, SqlDbType.Money, 0, txtfeemanagement.Text, typeof(decimal));
                //ge.ListDataTable = gb.GetDataJustOutPut("AE_SPI_Fee_Reward", Paramsvx, "constrmf").Tables[0];

                //if (ge.ListDataTable.Rows.Count > 0)
                //{

                //    ViewState["ListDetailRP"] = ge.ListDataTable;

                //    GRDKU.DataSource = ge.ListDataTable;
                //    GRDKU.DataBind();

                //    GRDKU.UseAccessibleHeader = true;
                //    GRDKU.HeaderRow.TableSection = TableRowSection.TableHeader;

                //}

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
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
        try
        {
            //var kodekry = hdnkaryawan.Value;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            //var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, kodekry, typeof(string));
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIDATACABANG", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_SPI_Fee_Reward", var, "constrmf");
            //Boolean status = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["stl"].ToString());
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                txtManagement.Text = ge.ListDataset.Tables[0].Rows[0]["management"].ToString();
                txtnonManagement.Text = ge.ListDataset.Tables[0].Rows[0]["nonmanagement"].ToString();
                txttotalkaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["totalkry"].ToString();
                txtBO.Text = ge.ListDataset.Tables[0].Rows[0]["bo"].ToString();
                txtofficeboy.Text = ge.ListDataset.Tables[0].Rows[0]["ob"].ToString();
                txtku.Text = ge.ListDataset.Tables[0].Rows[0]["ku"].ToString();
                txtmarketing.Text = ge.ListDataset.Tables[0].Rows[0]["mk"].ToString();
                txtjumlahcollection.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["collection"].ToString()));
                
                ddlSP.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["bulan"].ToString();
                
                ddltahun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tahun"].ToString();
                pnladd.Visible = true;
                //Pnlfooter4.Visible = true;
              
                btnSimpan.Enabled = true;
             
                txtKeterangan.Text = "Fee & Reward" + " Bulan " + ddlSP.SelectedItem.Text + " Tahun " + ddltahun.SelectedValue;
            }
            //PnlGrid.Visible = true;
            else
            {
                txtManagement.Text = "0";
                txtnonManagement.Text = "0";
                txttotalkaryawan.Text = "0";
                txtBO.Text = "0";
                txtofficeboy.Text = "0";
                txtku.Text = "0";
                txtmarketing.Text = "0";

                txtjumlahcollection.Text = "0";


                //Pnlfooter4.Visible = true;

                btnSimpan.Enabled = false;

                txtKeterangan.Text = "";
            }


           
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhdebit;
        jlhdebit = decimal.Parse(((TextBox)grow.FindControl("jlhdebit")).Text);
        if (jlhdebit > 0)
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
    protected void Jlhfeemanagement_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhaverage, jlhfee, jlhReward;
        jlhaverage = decimal.Parse(((Label)grow.FindControl("lblaverage")).Text);
        jlhfee = decimal.Parse(((TextBox)grow.FindControl("txtpsnfee")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        if (jlhfee > 0)
        {

            ((Label)grow.FindControl("lblBM")).Text = string.Format("{0:N2}", jlhaverage * (jlhfee/100));
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhReward + (jlhaverage * (jlhfee / 100)));
        
        
        }
        else
        {

            ((Label)grow.FindControl("lblBM")).Text = string.Format("{0:N2}", jlhaverage * (jlhfee / 100));
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhReward + (jlhaverage * (jlhfee / 100)));
        }
        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void JlhrewardPLmanagement_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        if (jlhpl > 0)
        {

            ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl);
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhpl);


        }
        else
        {

            ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl);
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhpl);
        }
        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void JlhrewardKU_KMK_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl, jlhnm, jlhkmk,jlhtp, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        //if (jlhpl > 0)
        //{

            ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhpl + jlhnm + + jlhkmk + jlhtp + jlhSMTErbaik);



        lnkBTxx_Click(sender, e);
        ((TextBox)grow.FindControl("txtKMK")).Focus();
    }

    protected void JlhrewardKU_TP_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl, jlhkmk,jlhnm, jlhtp, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        //if (jlhpl > 0)
        //{

        ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhkmk + jlhnm+ jlhtp + jlhSMTErbaik);
        ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);



        lnkBTxx_Click(sender, e);
        ((TextBox)grow.FindControl("txtTP")).Focus();
    }

    protected void JlhrewardKU_SMTErbaik_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl, jlhkmk, jlhtp,jlhnm, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        //if (jlhpl > 0)
        //{

        ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);
        ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhnm + jlhpl + jlhkmk + jlhtp + jlhSMTErbaik);



        lnkBTxx_Click(sender, e);
        ((TextBox)grow.FindControl("txtSMTErbaik")).Focus();
    }
    protected void JlhrewardKU_PL_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl,jlhnm, jlhkmk, jlhtp, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        //if (jlhpl > 0)
        //{

        ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);
        ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhnm + jlhpl + jlhkmk + jlhtp + jlhSMTErbaik);



        lnkBTxx_Click(sender, e);
        ((TextBox)grow.FindControl("txtpl")).Focus();
    }

    protected void JlhrewardKU_NM_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl, jlhnm, jlhkmk, jlhtp, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        //if (jlhpl > 0)
        //{

        ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);
        ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhnm + jlhpl + jlhkmk + jlhtp + jlhSMTErbaik);



        lnkBTxx_Click(sender, e);
        ((TextBox)grow.FindControl("txtnewmembers")).Focus();
    }

    protected void JlhfeeKU_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhaverage, jlhfee, jlhReward;
        jlhaverage = decimal.Parse(((TextBox)grow.FindControl("txtaverage")).Text);
        jlhfee = decimal.Parse(((TextBox)grow.FindControl("txtpsnfee")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        if (jlhaverage > 0)
        {

            ((Label)grow.FindControl("lblBM")).Text = string.Format("{0:N2}", jlhaverage * (jlhfee / 100));
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhReward + (jlhaverage * (jlhfee / 100)));


        }
        else
        {

            ((Label)grow.FindControl("lblBM")).Text = string.Format("{0:N2}", jlhaverage * (jlhfee / 100));
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhReward + (jlhaverage * (jlhfee / 100)));
        }

        //GRDKU.UseAccessibleHeader = true;
        //GRDKU.HeaderRow.TableSection = TableRowSection.TableHeader;
        //subgridGL_Click();
        lnkBTxx_Click(sender,e);
        ((TextBox)grow.FindControl("txtaverage")).Focus();


    }
    private void SetDetailRPMKT()
    {


        dtRPMKT.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtRPMKT.Columns.Add(new DataColumn("KodeNik", typeof(string)));
        dtRPMKT.Columns.Add(new DataColumn("NamaKry", typeof(string)));
        dtRPMKT.Columns.Add(new DataColumn("Jabatan", typeof(string)));
        dtRPMKT.Columns.Add(new DataColumn("KetJab", typeof(string)));

        dtRPMKT.Columns.Add(new DataColumn("totalaverage", typeof(decimal)));
        dtRPMKT.Columns.Add(new DataColumn("feemrk", typeof(decimal)));
        dtRPMKT.Columns.Add(new DataColumn("totalbm", typeof(decimal)));
        dtRPMKT.Columns.Add(new DataColumn("plreward", typeof(decimal)));
        dtRPMKT.Columns.Add(new DataColumn("NM", typeof(decimal)));
        dtRPMKT.Columns.Add(new DataColumn("KMK", typeof(decimal)));
        dtRPMKT.Columns.Add(new DataColumn("TP", typeof(decimal)));
        dtRPMKT.Columns.Add(new DataColumn("SMTERBAIK", typeof(decimal)));
        dtRPMKT.Columns.Add(new DataColumn("totalreward", typeof(decimal)));
        dtRPMKT.Columns.Add(new DataColumn("totalrewardbm", typeof(decimal)));


        //dr = dtRP.NewRow();
        //dr["nomor"] = 1;
        //dr["noanggota"] = string.Empty;
        //dr["plafon"] = decimal.Zero;
        //dr["psnreward"] = decimal.Zero;
        //dr["reward"] = decimal.Zero;
        //dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["TransactionMKT"] = dtRPMKT;


        GRDMKT.DataSource = dtRPMKT;
        GRDMKT.DataBind();

        GRDMKT.UseAccessibleHeader = true;
        GRDMKT.HeaderRow.TableSection = TableRowSection.TableHeader;

        GRDMKT.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRDKU.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-hide", "phone");
        cells[4].Attributes.Add("data-hide", "phone");
        cells[5].Attributes.Add("data-class", "expand");
        cells[6].Attributes.Add("data-class", "expand");
        cells[7].Attributes.Add("data-class", "expand");
        cells[8].Attributes.Add("data-hide", "phone");
        cells[9].Attributes.Add("data-hide", "phone");
        cells[10].Attributes.Add("data-hide", "phone");
        cells[11].Attributes.Add("data-hide", "phone");
        cells[12].Attributes.Add("data-hide", "phone");
        cells[13].Attributes.Add("data-hide", "phone");
    }
    protected void lnkBTMKT_Click(object sender, EventArgs e)
    {
        int dgcount = GRDMKT.Rows.Count;
        //DataTable dtRPKU = new DataTable("TransactionKU");
        dtRPMKT.Columns.Clear();

        dtRPMKT = (DataTable)ViewState["TransactionMKT"];

        dtRPMKT.Rows.Clear();

        for (int i = 0; i < dgcount; i++)
        {
            DataRow dr = dtRPMKT.NewRow();

            dr["nomor"] = ((Label)GRDMKT.Rows[i].FindControl("lblNo")).Text;
            dr["KodeNik"] = ((Label)GRDMKT.Rows[i].FindControl("lblnoanggota")).Text;
            dr["NamaKry"] = ((Label)GRDMKT.Rows[i].FindControl("lblnama")).Text;
            dr["Jabatan"] = ((Label)GRDMKT.Rows[i].FindControl("lblkodejab")).Text;
            dr["KetJab"] = ((Label)GRDMKT.Rows[i].FindControl("lblketjab")).Text;
            dr["totalaverage"] = decimal.Parse(((TextBox)GRDMKT.Rows[i].FindControl("txtaverage")).Text);
            dr["feemrk"] = decimal.Parse(((TextBox)GRDMKT.Rows[i].FindControl("txtpsnfee")).Text);
            dr["totalbm"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lblBM")).Text);
            dr["plreward"] = decimal.Parse(((TextBox)GRDMKT.Rows[i].FindControl("txtpl")).Text);
            dr["NM"] = decimal.Parse(((TextBox)GRDMKT.Rows[i].FindControl("txtnewmembers")).Text);
            dr["KMK"] = decimal.Parse(((TextBox)GRDMKT.Rows[i].FindControl("txtKMK")).Text);
            dr["TP"] = decimal.Parse(((TextBox)GRDMKT.Rows[i].FindControl("txtTP")).Text);
            dr["SMTERBAIK"] = decimal.Parse(((TextBox)GRDMKT.Rows[i].FindControl("txtSMTErbaik")).Text);
            dr["totalreward"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lblReward")).Text);
            dr["totalrewardbm"] = decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lblBMReward")).Text);

            dtRPMKT.Rows.Add(dr);
        }



        ViewState["TransactionMKT"] = dtRPMKT;

        GRDMKT.DataSource = dtRPMKT;
        GRDMKT.DataBind();

        GRDMKT.UseAccessibleHeader = true;
        GRDMKT.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    private void SetDetailRPKU()
    {

       
        dtRPKU.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtRPKU.Columns.Add(new DataColumn("KodeNik", typeof(string)));
        dtRPKU.Columns.Add(new DataColumn("NamaKry", typeof(string)));
        dtRPKU.Columns.Add(new DataColumn("Jabatan", typeof(string)));
        dtRPKU.Columns.Add(new DataColumn("KetJab", typeof(string)));
        dtRPKU.Columns.Add(new DataColumn("totalaverage", typeof(decimal)));
        dtRPKU.Columns.Add(new DataColumn("feemrk", typeof(decimal)));
        dtRPKU.Columns.Add(new DataColumn("totalbm", typeof(decimal)));
        dtRPKU.Columns.Add(new DataColumn("plreward", typeof(decimal)));
        dtRPKU.Columns.Add(new DataColumn("NM", typeof(decimal)));
        dtRPKU.Columns.Add(new DataColumn("KMK", typeof(decimal)));
        dtRPKU.Columns.Add(new DataColumn("TP", typeof(decimal)));
        dtRPKU.Columns.Add(new DataColumn("SMTERBAIK", typeof(decimal)));
        dtRPKU.Columns.Add(new DataColumn("totalreward", typeof(decimal)));
        dtRPKU.Columns.Add(new DataColumn("totalrewardbm", typeof(decimal)));


        //dr = dtRP.NewRow();
        //dr["nomor"] = 1;
        //dr["noanggota"] = string.Empty;
        //dr["plafon"] = decimal.Zero;
        //dr["psnreward"] = decimal.Zero;
        //dr["reward"] = decimal.Zero;
        //dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["TransactionKU"] = dtRPKU;


        GRDKU.DataSource = dtRPKU;
        GRDKU.DataBind();

        GRDKU.UseAccessibleHeader = true;
        GRDKU.HeaderRow.TableSection = TableRowSection.TableHeader;

        GRDKU.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRDKU.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-hide", "phone");
        cells[4].Attributes.Add("data-hide", "phone");
        cells[5].Attributes.Add("data-class", "expand");
        cells[6].Attributes.Add("data-class", "expand");
        cells[7].Attributes.Add("data-class", "expand");
        cells[8].Attributes.Add("data-hide", "phone");
        cells[9].Attributes.Add("data-hide", "phone");
        cells[10].Attributes.Add("data-hide", "phone");
        cells[11].Attributes.Add("data-hide", "phone");
        cells[12].Attributes.Add("data-hide", "phone");
        cells[13].Attributes.Add("data-hide", "phone");
    }
   
    
    protected void lnkBTxx_Click(object sender, EventArgs e)
    {
        int dgcount = GRDKU.Rows.Count;
        //DataTable dtRPKU = new DataTable("TransactionKU");
        dtRPKU.Columns.Clear();

        dtRPKU = (DataTable)ViewState["TransactionKU"];

        dtRPKU.Rows.Clear();

        for (int i = 0; i < dgcount; i++)
        {
            DataRow dr = dtRPKU.NewRow();

            dr["nomor"] = ((Label)GRDKU.Rows[i].FindControl("lblNo")).Text;
            dr["KodeNik"] = ((Label)GRDKU.Rows[i].FindControl("lblnoanggota")).Text;
            dr["NamaKry"] = ((Label)GRDKU.Rows[i].FindControl("lblnama")).Text;
            dr["Jabatan"] = ((Label)GRDKU.Rows[i].FindControl("lblkodejab")).Text;
            dr["KetJab"] = ((Label)GRDKU.Rows[i].FindControl("lblketjab")).Text;
            dr["totalaverage"] = decimal.Parse(((TextBox)GRDKU.Rows[i].FindControl("txtaverage")).Text);
            dr["feemrk"] = decimal.Parse(((TextBox)GRDKU.Rows[i].FindControl("txtpsnfee")).Text);
            dr["totalbm"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("lblBM")).Text);
            dr["plreward"] = decimal.Parse(((TextBox)GRDKU.Rows[i].FindControl("txtpl")).Text);
            dr["NM"] = decimal.Parse(((TextBox)GRDKU.Rows[i].FindControl("txtnewmembers")).Text);
            dr["KMK"] = decimal.Parse(((TextBox)GRDKU.Rows[i].FindControl("txtKMK")).Text);
            dr["TP"] = decimal.Parse(((TextBox)GRDKU.Rows[i].FindControl("txtTP")).Text);
            dr["SMTERBAIK"] = decimal.Parse(((TextBox)GRDKU.Rows[i].FindControl("txtSMTErbaik")).Text);
            dr["totalreward"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("lblReward")).Text);
            dr["totalrewardbm"] = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("lblBMReward")).Text);
            
            dtRPKU.Rows.Add(dr);
        }

       

        ViewState["TransactionKU"] = dtRPKU;

        GRDKU.DataSource = dtRPKU;
        GRDKU.DataBind();

        GRDKU.UseAccessibleHeader = true;
        GRDKU.HeaderRow.TableSection = TableRowSection.TableHeader;
        
    }


    private void subgridGL_Click()
    {
        
        int dgcount = GRDKU.Rows.Count;
        decimal sumFEE = 0;
        decimal jlhfee = 0;
        if (dgcount > 0)
        {


            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                jlhfee = decimal.Parse(((Label)GRDKU.Rows[i].FindControl("lblBM")).Text);
                sumFEE = sumFEE + jlhfee;
            }



        }


        else
        {
            sumFEE = 0;
        }
        string formatted = string.Format("{0:N2}", sumFEE);

        //GRDKU.Columns[5].FooterText = Convert.ToString(formatted);
        GRDKU.Columns[7].FooterText = Convert.ToString(formatted);

    }
    protected void JlhrewardMK_KMK_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl,jlhnm, jlhkmk, jlhtp, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        
            ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhpl + jlhnm  + jlhkmk + jlhtp + jlhSMTErbaik);

        lnkBTMKT_Click(sender, e);
        ((TextBox)grow.FindControl("txtKMK")).Focus();
    }
    protected void JlhrewardMK_PL_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl,jlhnm, jlhkmk, jlhtp, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);

        ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);
        ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhpl + jlhnm  + jlhkmk + jlhtp + jlhSMTErbaik);

        lnkBTMKT_Click(sender, e);
        ((TextBox)grow.FindControl("txtpl")).Focus();
    }
    protected void JlhrewardMK_NM_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl, jlhnm, jlhkmk, jlhtp, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);

        ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);
        ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);

        lnkBTMKT_Click(sender, e);
        ((TextBox)grow.FindControl("txtnewmembers")).Focus();
    }
    protected void JlhrewardMK_TP_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl, jlhnm, jlhkmk, jlhtp, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);

        ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);
        ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);

        lnkBTMKT_Click(sender, e);
        ((TextBox)grow.FindControl("txtTP")).Focus();
    }
    protected void JlhrewardMK_SMTErbaik_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhBM, jlhpl, jlhnm, jlhkmk, jlhtp, jlhSMTErbaik, jlhReward;
        jlhBM = decimal.Parse(((Label)grow.FindControl("lblBM")).Text);
        jlhpl = decimal.Parse(((TextBox)grow.FindControl("txtpl")).Text);
        jlhnm = decimal.Parse(((TextBox)grow.FindControl("txtnewmembers")).Text);
        jlhkmk = decimal.Parse(((TextBox)grow.FindControl("txtKMK")).Text);
        jlhtp = decimal.Parse(((TextBox)grow.FindControl("txtTP")).Text);
        jlhSMTErbaik = decimal.Parse(((TextBox)grow.FindControl("txtSMTErbaik")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);

        ((Label)grow.FindControl("lblReward")).Text = string.Format("{0:N2}", jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);
        ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhBM + jlhpl + jlhnm + jlhkmk + jlhtp + jlhSMTErbaik);

        lnkBTMKT_Click(sender, e);
        ((TextBox)grow.FindControl("txtSMTErbaik")).Focus();
    }

    protected void JlhfeeMK_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhaverage, jlhfee, jlhReward;
        jlhaverage = decimal.Parse(((TextBox)grow.FindControl("txtaverage")).Text);
        jlhfee = decimal.Parse(((TextBox)grow.FindControl("txtpsnfee")).Text);
        jlhReward = decimal.Parse(((Label)grow.FindControl("lblReward")).Text);
        if (jlhfee > 0)
        {

            ((Label)grow.FindControl("lblBM")).Text = string.Format("{0:N2}", jlhaverage * (jlhfee / 100));
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhReward + (jlhaverage * (jlhfee / 100)));


        }
        else
        {

            ((Label)grow.FindControl("lblBM")).Text = string.Format("{0:N2}", jlhaverage * (jlhfee / 100));
            ((Label)grow.FindControl("lblBMReward")).Text = string.Format("{0:N2}", jlhReward + (jlhaverage * (jlhfee / 100)));
        }
        lnkBTMKT_Click(sender,e);
        ((TextBox)grow.FindControl("txtaverage")).Focus();
    }

    decimal totalAV = 0,totalBM = 0, totalPL=0, totalNM = 0, totalKMK = 0, totalTunggakan = 0,
        totalSMTerbaik = 0, totalReward=0, totalRewardBM=0;
    int anggotabaru = 0, anggotaaktif = 0, smterbaik = 0, kuterbaik = 0, totalsm = 0, totalku = 0,xi = 0;
    decimal feenewmembers = 0;
    decimal Bakidb = 0, bakidbbulanlalu = 0, deltabakidb = 0, feeKMK = 0, tunggakan = 0, tunggakanbulanlalu = 0, feetunggakan = 0,deltatunggakan=0,kmk=0;
    string xkodenik = "";
    decimal totalx1 = 0;
    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalbm"));
            totalPL += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plreward"));
            //totalKMK += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "KMK"));
            //totalTunggakan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TP"));
            //totalSMTerbaik += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SMTERBAIK"));
            totalReward += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalreward"));
            totalRewardBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalrewardbm"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalBM = (Label)e.Row.FindControl("lbltotalBM");
            lbltotalBM.Text = string.Format("{0:N2}", totalBM);

            Label lbltotalPL = (Label)e.Row.FindControl("lbltotalPL");
            lbltotalPL.Text = string.Format("{0:N2}", totalPL);

            //Label lbltotalKMK = (Label)e.Row.FindControl("lbltotalKMK");
            //lbltotalKMK.Text = string.Format("{0:N2}", totalKMK);

            //Label lbltotalTunggakan = (Label)e.Row.FindControl("lbltotalTunggakan");
            //lbltotalTunggakan.Text = string.Format("{0:N2}", totalTunggakan);

            //Label lbltotalSMTerbaik = (Label)e.Row.FindControl("lbltotalSMTerbaik");
            //lbltotalSMTerbaik.Text = string.Format("{0:N2}", totalSMTerbaik);


            Label lbltotalReward = (Label)e.Row.FindControl("lbltotalReward");
            lbltotalReward.Text = string.Format("{0:N2}", totalReward);

            Label lbltotalRewardBM = (Label)e.Row.FindControl("lbltotalRewardBM");
            lbltotalRewardBM.Text = string.Format("{0:N2}", totalRewardBM);


            totalBM = 0; totalPL = 0; totalKMK = 0; totalTunggakan = 0;
            totalSMTerbaik = 0; totalReward = 0; totalRewardBM = 0;

        }
    }

    protected void GRDBO_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalbm"));
            totalPL += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plreward"));
            //totalKMK += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "KMK"));
            //totalTunggakan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TP"));
            //totalSMTerbaik += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SMTERBAIK"));
            totalReward += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalreward"));
            totalRewardBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalrewardbm"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalBM = (Label)e.Row.FindControl("lbltotalBM");
            lbltotalBM.Text = string.Format("{0:N2}", totalBM);

            Label lbltotalPL = (Label)e.Row.FindControl("lbltotalPL");
            lbltotalPL.Text = string.Format("{0:N2}", totalPL);

            //Label lbltotalKMK = (Label)e.Row.FindControl("lbltotalKMK");
            //lbltotalKMK.Text = string.Format("{0:N2}", totalKMK);

            //Label lbltotalTunggakan = (Label)e.Row.FindControl("lbltotalTunggakan");
            //lbltotalTunggakan.Text = string.Format("{0:N2}", totalTunggakan);

            //Label lbltotalSMTerbaik = (Label)e.Row.FindControl("lbltotalSMTerbaik");
            //lbltotalSMTerbaik.Text = string.Format("{0:N2}", totalSMTerbaik);


            Label lbltotalReward = (Label)e.Row.FindControl("lbltotalReward");
            lbltotalReward.Text = string.Format("{0:N2}", totalReward);

            Label lbltotalRewardBM = (Label)e.Row.FindControl("lbltotalRewardBM");
            lbltotalRewardBM.Text = string.Format("{0:N2}", totalRewardBM);
            totalBM = 0; totalPL = 0; totalKMK = 0; totalTunggakan = 0;
            totalSMTerbaik = 0; totalReward = 0; totalRewardBM = 0;

        }
    }

    protected void GRDOB_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalbm"));
            totalPL += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plreward"));
            //totalKMK += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "KMK"));
            //totalTunggakan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TP"));
            //totalSMTerbaik += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SMTERBAIK"));
            totalReward += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalreward"));
            totalRewardBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalrewardbm"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalBM = (Label)e.Row.FindControl("lbltotalBM");
            lbltotalBM.Text = string.Format("{0:N2}", totalBM);

            Label lbltotalPL = (Label)e.Row.FindControl("lbltotalPL");
            lbltotalPL.Text = string.Format("{0:N2}", totalPL);

            //Label lbltotalKMK = (Label)e.Row.FindControl("lbltotalKMK");
            //lbltotalKMK.Text = string.Format("{0:N2}", totalKMK);

            //Label lbltotalTunggakan = (Label)e.Row.FindControl("lbltotalTunggakan");
            //lbltotalTunggakan.Text = string.Format("{0:N2}", totalTunggakan);

            //Label lbltotalSMTerbaik = (Label)e.Row.FindControl("lbltotalSMTerbaik");
            //lbltotalSMTerbaik.Text = string.Format("{0:N2}", totalSMTerbaik);


            Label lbltotalReward = (Label)e.Row.FindControl("lbltotalReward");
            lbltotalReward.Text = string.Format("{0:N2}", totalReward);

            Label lbltotalRewardBM = (Label)e.Row.FindControl("lbltotalRewardBM");
            lbltotalRewardBM.Text = string.Format("{0:N2}", totalRewardBM);

            totalBM = 0; totalPL = 0; totalKMK = 0; totalTunggakan = 0;
            totalSMTerbaik = 0; totalReward = 0; totalRewardBM = 0;
        }
    }

    protected void GRDKU_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalAV += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalaverage"));
            totalBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalbm"));
            totalPL += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plreward"));
         
            //totalSMTerbaik += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SMTERBAIK"));

            smterbaik = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "urutterbaik"));
            //kuterbaik = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "anggotabaru"));
            totalsm = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "totalku"));


            if (txtnobukti.Text.Length == 0)
            {

           

            for (int i = 0; i < GRDMKT.Rows.Count; i++)
            {


                xkodenik = DataBinder.Eval(e.Row.DataItem, "KodeNik").ToString();

                if (xkodenik == ((Label)GRDMKT.Rows[i].FindControl("lblkodeKU")).Text)
                {
                    xi = xi + 1;
                    totalx1 += decimal.Parse(((Label)GRDMKT.Rows[i].FindControl("lblReward")).Text);

                }
               
            }



            ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", totalx1);
            ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", xi);


            if (xi == 0)
            {
                ((Label)e.Row.FindControl("txtTP")).Text = string.Format("{0:N0}", (decimal.Parse("0")));

            }
            else if ( xi==1)
            {
                ((Label)e.Row.FindControl("txtTP")).Text = string.Format("{0:N0}", ((decimal.Parse("100") / 100) * totalReward));
               
            }
            else if (xi == 2)
            {
                ((Label)e.Row.FindControl("txtTP")).Text = string.Format("{0:N0}", ((decimal.Parse("60") / 100) * totalReward));

            }
            else if (xi == 3)
            {
                ((Label)e.Row.FindControl("txtTP")).Text = string.Format("{0:N0}", ((decimal.Parse("45") / 100) * totalReward));

            }
            else if (xi >= 4)
            {
                ((Label)e.Row.FindControl("txtTP")).Text = string.Format("{0:N0}", ( (decimal.Parse("37,5")/ 100) * totalReward));

            }


            if (totalsm == 2)
            {
                if (smterbaik == 0)
                {
                    ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("0"));
                }
                else if (smterbaik == 1)
                {
                    ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("150000"));
                }
                else if (smterbaik == 2)
                {
                    ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }

            }
            else if (totalsm == 1)
            {
                if (smterbaik == 0)
                {
                    ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("0"));
                }
                else if (smterbaik == 1)
                {
                    ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }


            }
            else
            {

                ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("0"));

            }

              ((Label)e.Row.FindControl("lblReward")).Text = string.Format("{0:N0}", decimal.Parse(((Label)e.Row.FindControl("txtpl")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtTP")).Text) + +decimal.Parse(((Label)e.Row.FindControl("txtSMTErbaik")).Text));
              ((Label)e.Row.FindControl("lblBMReward")).Text = string.Format("{0:N0}", decimal.Parse(((Label)e.Row.FindControl("lblBM")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtpl")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtTP")).Text) + +decimal.Parse(((Label)e.Row.FindControl("txtSMTErbaik")).Text));

            }



            totalReward += Convert.ToDecimal(((Label)e.Row.FindControl("lblReward")).Text);
            
            
            totalRewardBM += Convert.ToDecimal(((Label)e.Row.FindControl("lblBMReward")).Text);

            

            totalNM += Convert.ToDecimal(((Label)e.Row.FindControl("txtnewmembers")).Text);
            totalKMK += Convert.ToDecimal(((Label)e.Row.FindControl("txtKMK")).Text);
            totalTunggakan += Convert.ToDecimal(((Label)e.Row.FindControl("txtTP")).Text);
            totalSMTerbaik += Convert.ToDecimal(((Label)e.Row.FindControl("txtSMTErbaik")).Text);
            




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalVG = (Label)e.Row.FindControl("lbltotalVG");
            lbltotalVG.Text = string.Format("{0:N0}", totalAV);



            Label lbltotalBM = (Label)e.Row.FindControl("lbltotalBM");
            lbltotalBM.Text = string.Format("{0:N0}", totalBM);

            Label lbltotalPL = (Label)e.Row.FindControl("lbltotalPL");
            lbltotalPL.Text = string.Format("{0:N0}", totalPL);

            Label lbltotalNM = (Label)e.Row.FindControl("lbltotalnewmembers");
            lbltotalNM.Text = string.Format("{0:N0}", totalNM);

            Label lbltotalKMK = (Label)e.Row.FindControl("lbltotalKMK");
            lbltotalKMK.Text = string.Format("{0:N0}", totalKMK);

            Label lbltotalTunggakan = (Label)e.Row.FindControl("lbltotalTunggakan");
            lbltotalTunggakan.Text = string.Format("{0:N0}", totalTunggakan);

            Label lbltotalSMTerbaik = (Label)e.Row.FindControl("lbltotalSMTerbaik");
            lbltotalSMTerbaik.Text = string.Format("{0:N0}", totalSMTerbaik);


            Label lbltotalReward = (Label)e.Row.FindControl("lbltotalReward");
            lbltotalReward.Text = string.Format("{0:N0}", totalReward);

            Label lbltotalRewardBM = (Label)e.Row.FindControl("lbltotalRewardBM");
            lbltotalRewardBM.Text = string.Format("{0:N0}", totalRewardBM);

            totalBM = 0; totalPL = 0; totalNM = 0; totalKMK = 0; totalTunggakan = 0;
            totalSMTerbaik = 0; totalReward = 0; totalRewardBM = 0; totalAV = 0; xi = 0; totalx1 = 0;
        }
    }

    protected void GRDMKT_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           

            //if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
            //{
            //    ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

            //}
            anggotabaru = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "anggotabaru"));
            anggotaaktif = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "anggotaaktif"));
            smterbaik = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "urutterbaik"));
            //kuterbaik = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "anggotabaru"));
            totalsm = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "totalsm"));
            Bakidb = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bakidb"));
            bakidbbulanlalu = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakibulanlalu"));
            tunggakan = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tunggakanPokok"));
            tunggakanbulanlalu = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tunggakanbulanlalu"));


            if (txtnobukti.Text.Length == 0)
            {



                deltabakidb = Bakidb - bakidbbulanlalu;
                deltatunggakan = tunggakan - tunggakanbulanlalu;
                if (Bakidb == 0)
                {
                    kmk = 0;
                }
                else
                {
                    kmk = (Convert.ToDecimal("0,5") / 100) * Bakidb;
                }

                //decimal Bakidb = 0, bakidbbulanlalu = 0, feeKMK = 0, tunggakan = 0, tunggakanbulanini = 0, feetunggakan = 0;


                if (anggotaaktif <= 20 && anggotabaru >= 12)
                {
                    ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (anggotaaktif > 20 && anggotaaktif <= 30 && anggotabaru >= 11)
                {
                    ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (anggotaaktif > 30 && anggotaaktif <= 40 && anggotabaru >= 10)
                {
                    ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (anggotaaktif > 40 && anggotaaktif <= 50 && anggotabaru >= 9)
                {
                    ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (anggotaaktif > 50 && anggotaaktif <= 60 && anggotabaru >= 8)
                {
                    ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (anggotaaktif > 60 && anggotaaktif <= 80 && anggotabaru >= 7)
                {
                    ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (anggotaaktif > 80 && anggotaaktif <= 100 && anggotabaru >= 6)
                {
                    ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (anggotaaktif > 100 && anggotabaru >= 2)
                {
                    ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else
                {
                    ((Label)e.Row.FindControl("txtnewmembers")).Text = string.Format("{0:N0}", decimal.Parse("0"));
                }



                if (Bakidb <= 20000000 && deltabakidb >= 5000000)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (Bakidb > 20000000 && Bakidb <= 30000000 && deltabakidb >= 4500000)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (Bakidb > 30000000 && Bakidb <= 40000000 && deltabakidb >= 4000000)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (Bakidb > 40000000 && Bakidb <= 50000000 && deltabakidb >= 3500000)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (Bakidb > 50000000 && Bakidb <= 60000000 && deltabakidb >= 3000000)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (Bakidb > 60000000 && Bakidb <= 70000000 && deltabakidb >= 2500000)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (Bakidb > 70000000 && Bakidb <= 80000000 && deltabakidb >= 2000000)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }

                else if (Bakidb > 80000000 && Bakidb <= 90000000 && deltabakidb >= 1500000)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (Bakidb > 90000000 && Bakidb <= 100000000 && deltabakidb >= 1000000)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else if (Bakidb > 100000000 && deltabakidb >= 0)
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else
                {
                    ((Label)e.Row.FindControl("txtKMK")).Text = string.Format("{0:N0}", decimal.Parse("0"));
                }

                //Bakidb = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bakidb"));
                //bakidbbulanlalu = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakibulanlalu"));
                //tunggakan = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tunggakanPokok"));
                //tunggakanbulanlalu = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tunggakanbulanlalu"));

                //deltabakidb = Bakidb - bakidbbulanlalu;
                //deltatunggakan = tunggakan - tunggakanbulanlalu;
                //kmk = (Convert.ToDecimal("0,5") / 100) * Bakidb;


                if (deltatunggakan > 0 && kmk > 0 && deltatunggakan <= kmk)
                {
                    ((Label)e.Row.FindControl("txtTP")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                }
                else
                {
                    ((Label)e.Row.FindControl("txtTP")).Text = string.Format("{0:N0}", decimal.Parse("0"));
                }



                smterbaik = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "urutterbaik"));
                //kuterbaik = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "anggotabaru"));
                totalsm = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "totalsm"));


                if (totalsm == 4)
                {

                    if (smterbaik == 0)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("0"));
                    }
                    else if (smterbaik == 1)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("145000"));
                    }
                    else if (smterbaik == 2)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("130000"));
                    }
                    else if (smterbaik == 3)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("115000"));
                    }
                    else if (smterbaik == 4)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                    }

                }
                else if (totalsm == 3)
                {

                    if (smterbaik == 0)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("0"));
                    }
                    else if (smterbaik == 1)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("130000"));
                    }
                    else if (smterbaik == 2)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("115000"));
                    }
                    else if (smterbaik == 3)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("100000"));
                    }

                }
                else if (totalsm == 2)
                {
                    if (smterbaik == 0)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("0"));
                    }
                    else if (smterbaik == 1)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("130000"));
                    }
                    else if (smterbaik == 2)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("115000"));
                    }

                }
                else if (totalsm == 1)
                {
                    if (smterbaik == 0)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("0"));
                    }
                    else if (smterbaik == 1)
                    {
                        ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("115000"));
                    }


                }
                else
                {

                    ((Label)e.Row.FindControl("txtSMTErbaik")).Text = string.Format("{0:N0}", decimal.Parse("0"));

                }



             ((Label)e.Row.FindControl("lblReward")).Text = string.Format("{0:N0}", decimal.Parse(((Label)e.Row.FindControl("txtpl")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtnewmembers")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtKMK")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtTP")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtSMTErbaik")).Text));
                ((Label)e.Row.FindControl("lblBMReward")).Text = string.Format("{0:N0}", decimal.Parse(((Label)e.Row.FindControl("lblBM")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtpl")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtnewmembers")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtKMK")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtTP")).Text) + decimal.Parse(((Label)e.Row.FindControl("txtSMTErbaik")).Text));
            }
            totalAV += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalaverage"));
            totalBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalbm"));
            totalPL += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plreward"));
            totalNM += Convert.ToDecimal(((Label)e.Row.FindControl("txtnewmembers")).Text);
            totalKMK += Convert.ToDecimal(((Label)e.Row.FindControl("txtKMK")).Text);
            totalTunggakan += Convert.ToDecimal(((Label)e.Row.FindControl("txtTP")).Text);
            totalSMTerbaik += Convert.ToDecimal(((Label)e.Row.FindControl("txtSMTErbaik")).Text);
            totalReward += Convert.ToDecimal(((Label)e.Row.FindControl("lblReward")).Text);
            totalRewardBM += Convert.ToDecimal(((Label)e.Row.FindControl("lblBMReward")).Text);



            //, anggotaaktif = 0;
            //decimal feenewmembers = 0;
            //if (((Label)e.Row.FindControl("lblanggotabaru")).Text)

            //< asp:Label ID = "lblTotalStorting" runat = "server" Text = '<%#DataBinder.Eval(Container.DataItem, "TotalStorting","{0:N0}")%>' ></ asp:Label >
            //                                             < asp:Label ID = "lblanggotabaru" runat = "server" Text = '<%#DataBinder.Eval(Container.DataItem, "anggotabaru","{0:N0}")%>' ></ asp:Label >
            //                                             < asp:Label ID = "lblanggotaaktif" runat = "server" Text = '<%#DataBinder.Eval(Container.DataItem, "anggotaaktif","{0:N0}")%>' ></ asp:Label >
            //                                             < asp:Label ID = "lblBakidb" runat = "server" Text = '<%#DataBinder.Eval(Container.DataItem, "Bakidb","{0:N0}")%>' ></ asp:Label >
            //                                             < asp:Label ID = "lblbakibulanlalu" runat = "server" Text = '<%#DataBinder.Eval(Container.DataItem, "bakibulanlalu","{0:N0}")%>' ></ asp:Label >
            //                                             < asp:Label ID = "lbltunggakanPokok" runat = "server" Text = '<%#DataBinder.Eval(Container.DataItem, "tunggakanPokok","{0:N0}")%>' ></ asp:Label >
            //                                            < asp:Label ID = "lbltunggakanbulanlalu" runat = "server" Text = '<%#DataBinder.Eval(Container.DataItem, "tunggakanbulanlalu","{0:N0}")%>' ></ asp:Label >


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalAVG = (Label)e.Row.FindControl("lbltotalAVG");
            lbltotalAVG.Text = string.Format("{0:N0}", totalAV);

            Label lbltotalBM = (Label)e.Row.FindControl("lbltotalBM");
            lbltotalBM.Text = string.Format("{0:N0}", totalBM);

            Label lbltotalPL = (Label)e.Row.FindControl("lbltotalPL");
            lbltotalPL.Text = string.Format("{0:N0}", totalPL);

            Label lbltotalNM = (Label)e.Row.FindControl("lbltotalnewmembers");
            lbltotalNM.Text = string.Format("{0:N0}", totalNM);

            Label lbltotalKMK = (Label)e.Row.FindControl("lbltotalKMK");
            lbltotalKMK.Text = string.Format("{0:N0}", totalKMK);

            Label lbltotalTunggakan = (Label)e.Row.FindControl("lbltotalTunggakan");
            lbltotalTunggakan.Text = string.Format("{0:N0}", totalTunggakan);

            Label lbltotalSMTerbaik = (Label)e.Row.FindControl("lbltotalSMTerbaik");
            lbltotalSMTerbaik.Text = string.Format("{0:N0}", totalSMTerbaik);


            Label lbltotalReward = (Label)e.Row.FindControl("lbltotalReward");
            lbltotalReward.Text = string.Format("{0:N0}", totalReward);

            Label lbltotalRewardBM = (Label)e.Row.FindControl("lbltotalRewardBM");
            lbltotalRewardBM.Text = string.Format("{0:N0}", totalRewardBM);


            totalBM = 0; totalPL = 0; totalKMK = 0; totalTunggakan = 0;
            totalSMTerbaik = 0; totalReward = 0; totalRewardBM = 0; totalAV = 0;
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";

            if (txtnoteverifikasi.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Request Masih Kosong!');", true);
            }
            else
            {




                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVE", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[5] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnoteverifikasi.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_MEMO_FEE_REWARD_MGM_VERIF", var, "constrmf");


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

            if (txtnoteverifikasi.Text.Length == 0)
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
                var[6] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnoteverifikasi.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_MEMO_FEE_REWARD_MGM_VERIF", var, "constrmf");


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
