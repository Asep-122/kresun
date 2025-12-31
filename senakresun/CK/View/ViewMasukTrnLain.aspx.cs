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
public partial class CL_Process_ViewMasukTrnLain : System.Web.UI.Page
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
    string form = "A2";
    string kodesumber = "CL";
    string KodeKonter = "CL02";
    public string nobuktidb;


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
                txttgljthtmp.Text = txttgltrn.Text;

               
               
                

                DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[4]; 
                var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLJURNAL", typeof(string));
                var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var6[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var6[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", var6, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";
                ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlJurnal.DataTextField = "NamaJurnal";
                ddlJurnal.DataValueField = "KodeJurnal";
                ddlJurnal.DataBind(); 

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
            //GRData.UseAccessibleHeader = true;
            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            string nowx;
            nowx = gb.ReadOrderData();
            DateTime now = DateTime.Parse(nowx);
            int tahun;
            tahun = now.Year;
            DateTime date = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
            txtStart.Text = date.ToString("yyyy-MM-dd");
            DateTime xdate = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
            xdate = date.AddMonths(1);
            xdate = xdate.AddDays(-1);
            txtEnd.Text = xdate.ToString("yyyy-MM-dd");

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

  
  

   
    public void btnSerach_Click(object sender, EventArgs e)
    {
        var kodelgn = hdncustomer.Value;
        string norekening = HDNoRekening.Value;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_View", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)


        {
                txtnobukti.Text = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
                txttgltrn.Text = ge.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString();
                ddlKdBank.Text = ge.ListDataset.Tables[0].Rows[0]["kodebank"].ToString();
                txttransAP.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JumlahTrn"].ToString()));
                txtjmlhGL.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JumlahTrn"].ToString()));
                
                txtjmlhCL.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JumlahTrn"].ToString()));
            txtKeterangan.Text = ge.ListDataset.Tables[0].Rows[0]["Keterangan"].ToString();
                txtBankGiro.Text = ge.ListDataset.Tables[0].Rows[0]["bankgiro"].ToString();
            txtnoGiro.Text = ge.ListDataset.Tables[0].Rows[0]["nogiro"].ToString();

            txtNmBank.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            ddlkursbank.SelectedValue = "IDR";
            txtkursbank.Text = "1";


            statusclosing = ge.ListDataset.Tables[0].Rows[0]["TutupBukuFlag"].ToString();
            statusposting = ge.ListDataset.Tables[0].Rows[0]["HapusFlag"].ToString();

            ddltypejur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["FlagKodeJurnal"].ToString();

            //ddlJurnal.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeJurnal"].ToString();

           



        }

        else
        {
            
        }

      

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
        var2[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
        var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        var2[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTCL", typeof(string));
        var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_View", var2, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GCL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GCL.DataBind();

        GCL.UseAccessibleHeader = true;

        GCL.HeaderRow.TableSection = TableRowSection.TableHeader;

        if (ddltypejur.SelectedValue == "3")
        {
            txtpostingjurnal.Text = "Tidak ada journal";
        }
        else
        {
            if (statusposting == "True")
            {
                txtpostingjurnal.Text = "Data Sudah Di Posting";
            }
            else
            {
                txtpostingjurnal.Text = "Data Belum Di Posting";
            }

            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[5];
            var3[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
            var3[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var3[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            var3[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTGL", typeof(string));
            var3[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_View", var3, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kodegl asc";


            GGL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GGL.DataBind();

            GGL.UseAccessibleHeader = true;

            GGL.HeaderRow.TableSection = TableRowSection.TableHeader;

            
        }
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
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
   
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

 
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
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
            txttgljthtmp.Text = txttgltrn.Text;
        }
        txttgljthtmp.Text = txttgltrn.Text;
    }

   
    [WebMethod]
    public static string GetBukti(string InputText, string SessKdpershx, string SessBranchIDx, string tglawal, string tglakhir)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));
        Params[3] = gb.CreateSPParameter("@tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglawal, typeof(DateTime));
        Params[4] = gb.CreateSPParameter("@tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglakhir, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.CL_get_CL", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }


}
