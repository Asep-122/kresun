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

public partial class SN_Process_MasukTrnPencairanApprove : System.Web.UI.Page
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
    string form = "A07";
    string kodesumber = "SN";
    string KodeKonter = "SN08";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
       

        pnldatapencairan.Visible = true;
        BindGridEntityDataCair();
        //btnSimpan.Visible = true;
        //txtnobukti.Text = "";
        //pnladd.Visible = true;
        //Pnlcustomer.Visible = true;
        //Pnlfooter4.Visible = true;
        //pnlbunga.Visible = true;
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
                //KODE Produk
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                var4[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPRODUCT", typeof(string));
                var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var4, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";
                ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlproduk.DataTextField = "nama_produk";
                ddlproduk.DataValueField = "kode_produk";
                ddlproduk.DataBind();
                ddlproduk.Items.Insert(0, new ListItem("Pilih Data", "-"));
                blnadd = 0;
                //GRTP.Enabled = true;     
                pnlHeader.Visible = false;


                pnldatapencairan.Visible = true;
                BindGridEntityDataCair();
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

   
  

    public void BindGridEntityDataCair()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATACAIR", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SN_PENCAIRAN_BERJANGKA_APPROVE", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        GRTP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRTP.DataBind();

        GRTP.UseAccessibleHeader = true;

        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

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
        
        switch (e.CommandName)
        {

            case "SET":

                try
                {

                    HDNoRekening.Value= ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_rekening")).Text;
                    HDNoNasabah.Value = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_nasabah")).Text;
                   

                    
                    string norekening = HDNoRekening.Value;
                   
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PENCAIRAN", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("SN_PENCAIRAN_BERJANGKA_APPROVE", var, "constrmf");


                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Pencairan Done!','You successfully','top-right','success','2');", true);

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
                  
                }

                break;
            case "PANJANG":

                try
                {


                    HDNoRekening.Value = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_rekening")).Text;
                    HDNoNasabah.Value = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_nasabah")).Text;

                    ddlInstruktur.SelectedValue = "1";

                    ddlproduk.SelectedValue= ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblproduk")).Text;

                    var Produk = ddlproduk.SelectedValue;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "Jangka asc";
                    ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljangka.DataTextField = "Jangka";
                    ddljangka.DataValueField = "Jangka";
                    ddljangka.DataBind();
                    ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddljangka.SelectedValue = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangkabulan")).Text;
                   
                    int jangka = int.Parse(ddljangka.SelectedValue);
                    DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[5];
                    vart[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlproduk.SelectedValue, typeof(string));
                    vart[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
                    vart[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    vart[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                    vart[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", vart, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
                    ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlbunga.DataTextField = "BungaTahunan";
                    ddlbunga.DataValueField = "BungaTahunan";
                    ddlbunga.DataBind();
                    ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddltypebunga.SelectedValue = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypebunga")).Text;
               
                    ddlbunga.SelectedValue = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbungapertahun")).Text;
                  

                    txtbungaperhari.Text= ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbungaperhari")).Text;
                    ddlpenarikanbunga.SelectedValue = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPenarikanBunga")).Text;
                    txtnorekening.Text = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoRekeningBank")).Text;
                    txtatasnama.Text = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAtasNama")).Text;
                    txtbankrekening.Text = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBankRekening")).Text;
                    
                    ddlrollover.SelectedValue = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpencairanbunga")).Text;

                    PnlRubah.Visible = true;
                    pnldatapencairan.Visible = false;

                    Pnlfooter4.Visible = true;


                    if (ddlpenarikanbunga.SelectedValue == "1")
                    {



                        txtbankrekening.Enabled = true;
                        txtnorekening.Enabled = true;
                        txtatasnama.Enabled = true;
                    }
                    else
                    {

                        if (ddlrollover.SelectedValue == "2")
                        {
                            txtbankrekening.Enabled = true;
                            txtnorekening.Enabled = true;
                            txtatasnama.Enabled = true;
                        }

                        else
                        {
                            txtbankrekening.Text = "-";
                            txtbankrekening.Enabled = false;
                            txtnorekening.Text = "-";
                            txtnorekening.Enabled = false;
                            txtatasnama.Text = "-";
                            txtatasnama.Enabled = false;

                        }




                    }





                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

                }

                break;
        }
    }

    decimal debet = 0, kredit = 0;
    

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {

            string norekening = HDNoRekening.Value;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[16];
            var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PANJANG", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[5] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangka.SelectedValue), typeof(int));
            var[7] = gb.CreateSPParameter("@bungapertahun", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(ddlbunga.SelectedValue), typeof(float));
            var[8] = gb.CreateSPParameter("@bungaperhari", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtbungaperhari.Text), typeof(float));
            var[9] = gb.CreateSPParameter("@PenarikanBunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlpenarikanbunga.SelectedValue), typeof(int));
            var[10] = gb.CreateSPParameter("@BankRekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtbankrekening.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@NoRekeningBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorekening.Text, typeof(string));
            var[12] = gb.CreateSPParameter("@AtasNama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtatasnama.Text, typeof(string));
            var[13] = gb.CreateSPParameter("@perpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlInstruktur.SelectedValue, typeof(string));
            var[14] = gb.CreateSPParameter("@typebungachange", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddltypebunga.SelectedValue), typeof(int));
            var[15] = gb.CreateSPParameter("@pencairanbunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlrollover.SelectedValue), typeof(int));

            ge.ListDataset = gb.GetDataJustOutPut("SN_PENCAIRAN_BERJANGKA_APPROVE", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Perpanjangan Done!','You successfully','top-right','success','2');", true);

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


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

  
  
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        //varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        //varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        //varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        //ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        //string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        //if (xstatus == "1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
        //    txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
       
        //}

    }

    protected void ddlproduk_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var Produk = ddlproduk.SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "Jangka asc";
            ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljangka.DataTextField = "Jangka";
            ddljangka.DataValueField = "Jangka";
            ddljangka.DataBind();
            ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlpenarikanbunga_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpenarikanbunga.SelectedValue == "1")
        {



            txtbankrekening.Enabled = true;
            txtnorekening.Enabled = true;
            txtatasnama.Enabled = true;
        }
        else
        {

            if (ddlrollover.SelectedValue=="2")
            {
                txtbankrekening.Enabled = true;
                txtnorekening.Enabled = true;
                txtatasnama.Enabled = true;
            }

            else
            {
                txtbankrekening.Text = "-";
                txtbankrekening.Enabled = false;
                txtnorekening.Text = "-";
                txtnorekening.Enabled = false;
                txtatasnama.Text = "-";
                txtatasnama.Enabled = false;

            }



       
        }

    }
    protected void ddlbunga_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal jlhbayar = 0;
        if (decimal.Parse(ddlbunga.SelectedValue) >= 0)
        {


            jlhbayar = ((decimal.Parse(ddlbunga.SelectedValue) / 100) / 365) * 100;
            txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
        }
        else
        {
            txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
        }


    }


    protected void ddljangka_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var Produk = ddlproduk.SelectedValue;
            int jangka = int.Parse(ddljangka.SelectedValue);
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
            ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
            ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlbunga.DataTextField = "BungaTahunan";
            ddlbunga.DataValueField = "BungaTahunan";
            ddlbunga.DataBind();
            ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SN_getCustomer_tarikan_berjangka", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }






}
