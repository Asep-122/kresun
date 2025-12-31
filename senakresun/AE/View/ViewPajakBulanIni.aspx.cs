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
using System.ServiceModel.Channels;

public partial class SN_View_Berjangka_ViewPajakBulanIni : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLoad();
            DateTime now = DateTime.Now;
            int bulan = now.Month;
            ddlbulan.SelectedValue = bulan.ToString();
            ddlsp_SelectedIndexChanged(sender, e);
        }
    }

    void BindLoad()
    {
        try
        {
     
            string waktuServer = gb.ReadOrderData();
            DateTime sekarang = DateTime.Parse(waktuServer);
            int tahun = sekarang.Year;
            int indexList = 0;
            for (int i = tahun; i >= 2000; i--)
            {
                ddlTahun.Items.Insert(indexList, new ListItem(i.ToString(), i.ToString()));
                indexList++;
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }



    protected void lnkCari_Click(object sender, EventArgs e)
    {
        /*        if(txtSearch.Text.Length != 0)
                {

                }
        */
    }

    [WebMethod]
  


    public void btnSerach_Click(object sender, EventArgs e)
    {



        try
        {


       
            DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
            varx[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            varx[1] = gb.CreateSPParameter("@DteTglAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            varx[2] = gb.CreateSPParameter("@DteTglAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            varx[3] = gb.CreateSPParameter("@bulangaji", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlbulan.SelectedValue), typeof(int));
            varx[4] = gb.CreateSPParameter("@tahungaji", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlTahun.SelectedValue), typeof(int));
            varx[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varx[6] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
           
            ge.ListDataset = gb.GetDataJustOutPut("Search_Gaji_Pajak", varx, "constrmf");

  
            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
          

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



        }

        catch (Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            }

        }

    }


    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbulan.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTANGGAL", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_Closing_Gaji", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {



                txtStart.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglAbsAwal"].ToString()).ToString("dd-MM-yyyy");
                txtEnd.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglAbsAkhir"].ToString()).ToString("dd-MM-yyyy");
            }
            else
            {
                txtStart.Text = "";
                txtEnd.Text = "";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        //DateTime now = DateTime.Now;

        //Txtrptperiode.Text = "Periode : "  + date.ToString("dd-MM-yyyy") +  " s/d " + xdate.ToString("dd-MM-yyyy");
    }
}