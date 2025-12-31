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
using System.Web.Script.Serialization;
using System.Web.Services;


public partial class CL_View_ViewPiutangCustomer : System.Web.UI.Page
{
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    //private string SessLoginID
    //{
    //    get { return Session["SessLoginID"].ToString(); }
    //    set { ViewState["SessLoginID"] = value; }
    //
    //}
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {

                Panel1.Visible = false;
                Panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                ddlnobukti.Visible = true;
                ddlView.Visible = true;

                //DateTime now = DateTime.Now;
                //txtStart.Text = now.ToString("yyyy-MM-dd");
                //txtEnd.Text = now.ToString("yyyy-MM-dd");
                BindGridEntity();

            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {
        //DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[5];
        //Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        //Paramsarea[1] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnobukti.SelectedValue, typeof(String));
        //Paramsarea[2] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //Paramsarea[3] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        //Paramsarea[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNOBUKTI", typeof(string));
        //ge.ListDataset = gb.GetDataJustOutPut("CL_ViewHutangSupplier", Paramsarea, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        //GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //GRData.DataBind();


        //GRData.UseAccessibleHeader = true;

        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
        var1[0] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, "-", typeof(string));
        
var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_ViewPiutangCustomer", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";
        ddlnobukti.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlnobukti.DataTextField = "kdbkt";
        ddlnobukti.DataValueField = "NoBukti";
        ddlnobukti.DataBind();
        ddlnobukti.Items.Insert(0, new ListItem("Pilih Data", "-"));

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        //if (ddlView.SelectedValue == "1")
        //{
        //    Panel1.Visible = true;
        //    PnlFront.Visible = false;
        //    panel2.Visible = false;
        //    Panel3.Visible = false;

        //}
        //else if (ddlView.SelectedValue == "2")
        //{
        //    Panel1.Visible = false;
        //    PnlFront.Visible = true;
        //    panel2.Visible = true;
        //    Panel3.Visible = true;
        //}
        //else
        //{
        //    Panel1.Visible = false;
        //    PnlFront.Visible = false;
        //    panel2.Visible = false;
        //    Panel3.Visible = false;
        //}
        if (ddlView.SelectedValue == "1")
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
            DefaultClass.Global.SqlHelper[] paramtotal = new DefaultClass.Global.SqlHelper[3];
            paramtotal[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            paramtotal[1] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnobukti.SelectedValue, typeof(String));
            paramtotal[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWTOTALHUTANGSUPPLIER", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewPiutangCustomer", paramtotal, "constrmf");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";    
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                txtTTLdebit.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString().Replace(",0000", ",00");
                txtTTLkredit.Text = ge.ListDataset.Tables[0].Rows[0][1].ToString().Replace(",0000", ",00");
            }
            else
            {
                txtTTLdebit.Text = "0";
                txtTTLkredit.Text = "0";
            }

            DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[3];
            Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            Paramsarea[1] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnobukti.SelectedValue, typeof(String));
            Paramsarea[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWHUTANGSUPPLIER", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewPiutangCustomer", Paramsarea, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();
            //GRData.HeaderRow.Disposed = 1;
            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        else if (ddlView.SelectedValue == "2")
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = true;

            DefaultClass.Global.SqlHelper[] paramtotal = new DefaultClass.Global.SqlHelper[3];
            paramtotal[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            paramtotal[1] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnobukti.SelectedValue, typeof(String));
            paramtotal[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "JURNALGIROMUDURTOTAL", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewPiutangCustomer", paramtotal, "constrmf");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";    
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                txtTTLdebit.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString().Replace(",0000", ",00");
                txtTTLkredit.Text = ge.ListDataset.Tables[0].Rows[0][1].ToString().Replace(",0000", ",00");
            }
            else
            {
                txtTTLdebit.Text = "0";
                txtTTLkredit.Text = "0";
            }

            DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[3];
            Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            Paramsarea[1] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnobukti.SelectedValue, typeof(String));
            Paramsarea[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "JURNALGIROMUDUR", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewPiutangCustomer", Paramsarea, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


            GridView2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GridView2.DataBind();
            //GRData.HeaderRow.Disposed = 1;
            GridView2.UseAccessibleHeader = true;

            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else if (ddlView.SelectedValue == "3")
        {

            Panel1.Visible = false;
            Panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = true;
            DefaultClass.Global.SqlHelper[] paramtotal = new DefaultClass.Global.SqlHelper[3];
            paramtotal[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            paramtotal[1] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnobukti.SelectedValue, typeof(String));
            paramtotal[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MUTASIGLTOTAL", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewPiutangCustomer", paramtotal, "constrmf");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                txtTTLdebit.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString().Replace(",0000", ",00");
                txtTTLkredit.Text = ge.ListDataset.Tables[0].Rows[0][1].ToString().Replace(",0000", ",00");
            }
            else
            {
                txtTTLdebit.Text = "0";
                txtTTLkredit.Text = "0";
            }

            DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[3];
            Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            Paramsarea[1] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnobukti.SelectedValue, typeof(String));
            Paramsarea[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MUTASIGL", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewPiutangCustomer", Paramsarea, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


            GridView3.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GridView3.DataBind();
            //GRData.HeaderRow.Disposed = 1;
            GridView3.UseAccessibleHeader = true;

            GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }


    [WebMethod]
    public static string GetTempat(string InputText, string SessKdpershx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_getTempat", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

    protected void ddlnobukti_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlnobukti.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNOBUKTI", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_ViewPiutangCustomer", var, "constrmf");

        txtkdLgn.Text = ge.ListDataset.Tables[0].Rows[0]["kodelgn"].ToString();
        txtnmLgn.Text = ge.ListDataset.Tables[0].Rows[0]["namalgn"].ToString();
        txtKodeCrc1.Text = ge.ListDataset.Tables[0].Rows[0]["kodecrc"].ToString();
        if (txtKodeCrc1.Text == "IDR")
        {
            txtKodeCrc2.Text = "Indonesia Rupiah";
        }
        else if (txtKodeCrc1.Text == "USD")
        {
            txtKodeCrc2.Text = "United Stated Dollar";
        }
        txtkurs.Text = ge.ListDataset.Tables[0].Rows[0]["kurs"].ToString();
        txttypetrn.Text = ge.ListDataset.Tables[0].Rows[0]["typetrn"].ToString();
        txtStart.Text = ge.ListDataset.Tables[0].Rows[0]["tgltrn"].ToString().Substring(0, 10);
        txtEnd.Text = ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString().Substring(0, 10);
        txtbankGr.Text = ge.ListDataset.Tables[0].Rows[0]["bankgiro"].ToString();
        txtnoGr.Text = ge.ListDataset.Tables[0].Rows[0]["nogiro"].ToString();

    }
}