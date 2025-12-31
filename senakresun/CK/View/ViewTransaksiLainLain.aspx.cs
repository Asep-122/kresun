
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


public partial class CL_View_ViewTransaksiLainLain : System.Web.UI.Page
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
                panel4.Visible = false;
                ddlnobukti.Visible = true;
                ddlView.Visible = true;

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

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
        var1[0] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, "-", typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_ViewTransaksiLainLain", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";
        ddlnobukti.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlnobukti.DataTextField = "kdbkt";
        ddlnobukti.DataValueField = "NoBukti";
        ddlnobukti.DataBind();
        ddlnobukti.Items.Insert(0, new ListItem("Pilih Data", "-"));

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlView.SelectedValue == "1")
        {
            Panel1.Visible = true;
            Panel2.Visible = false; 
            panel4.Visible = false;
            
            DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[4];
            Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            Paramsarea[1] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnobukti.SelectedValue, typeof(String));
            Paramsarea[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWHUTANGSUPPLIER", typeof(string));
            Paramsarea[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)); 
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewTransaksiLainLain", Paramsarea, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            float totalpenambahan = 0f;
            float totalPengurangan = 0f;

            for (int i = 0; i < GRData.Rows.Count; i++)
            {
                String penambahan = ((Label)GRData.Rows[i].FindControl("lbljlhCairAp")).Text;
                String pengurangan = ((Label)GRData.Rows[i].FindControl("lbljlhMndr")).Text;
                //int n = Int32.Parse(total) + Int32.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                totalpenambahan += Convert.ToSingle(penambahan);
                totalPengurangan += Convert.ToSingle(pengurangan);
            }

            GRData.FooterRow.Cells[0].Text = "Total";
            GRData.FooterRow.Cells[4].Font.Bold = true;
            GRData.FooterRow.Cells[5].Font.Bold = true;
            GRData.FooterRow.Cells[6].Font.Bold = true;
            string formatted = string.Format("{0:N2}", totalpenambahan);
            string formatted1 = string.Format("{0:N2}", totalPengurangan);
            GRData.FooterRow.Cells[5].Text = formatted;
            GRData.FooterRow.Cells[6].Text = formatted1; 

            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        else if (ddlView.SelectedValue == "2")
        {
            Panel1.Visible = false;
            Panel2.Visible = true; 
            panel4.Visible = false;
              
            DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[4];
            Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            Paramsarea[1] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnobukti.SelectedValue, typeof(String));
            Paramsarea[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MUTASIGL", typeof(string));
            Paramsarea[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)); 
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewTransaksiLainLain", Paramsarea, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kodeakun asc";
            if(ge.ListDataset.Tables[0].Rows.Count > 0)
            {  
                GridView2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GridView2.DataBind();

                float totalpenambahan = 0f;
                float totalPengurangan = 0f;

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    String penambahan = ((Label)GridView2.Rows[i].FindControl("lbl8")).Text;
                    String pengurangan = ((Label)GridView2.Rows[i].FindControl("lbl9")).Text;
                    //int n = Int32.Parse(total) + Int32.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                    totalpenambahan += Convert.ToSingle(penambahan);
                    totalPengurangan += Convert.ToSingle(pengurangan);
                } 
                GridView2.FooterRow.Cells[0].Text = "Total";
                GridView2.FooterRow.Cells[4].Font.Bold = true;
                GridView2.FooterRow.Cells[5].Font.Bold = true;
                GridView2.FooterRow.Cells[3].Font.Bold = true;
                string formatted = string.Format("{0:N2}", totalpenambahan);
                string formatted1 = string.Format("{0:N2}", totalPengurangan);

                GridView2.FooterRow.Cells[4].Text = formatted;
                GridView2.FooterRow.Cells[5].Text = formatted1; 

                GridView2.UseAccessibleHeader = true; 
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi GL dengan Nomor Bukti "+ ddlnobukti.SelectedValue + " tidak ditemukan!');", true);
            }
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
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
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
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNOBUKTI", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_ViewTransaksiLainLain", var, "constrmf");
        txtkdLgn.Text = ge.ListDataset.Tables[0].Rows[0]["kdbank"].ToString();
        txtnmLgn.Text = ge.ListDataset.Tables[0].Rows[0]["nmbank"].ToString();
        //decimal X = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldogiro"].ToString());
        //string formatted = string.Format("{0:N2}", X);
        //txtgiro1.Text = formatted;
        //txtgiro2.Text = formatted;
        txtKodeCrc1.Text = ge.ListDataset.Tables[0].Rows[0]["kdcrc"].ToString();
        if (txtKodeCrc1.Text == "IDR")
        {
            txtKodeCrc2.Text = "Indonesia Rupiah";
        }
        else if (txtKodeCrc1.Text == "USD")
        {
            txtKodeCrc2.Text = "United Stated Dollar";
        }
        decimal z = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kurs"].ToString());
        string formatted2 = string.Format("{0:N2}", z);
        txtkurs.Text = formatted2; 
        txttypetrn.Text = ge.ListDataset.Tables[0].Rows[0]["typetrn"].ToString();
        txtStart.Text = ge.ListDataset.Tables[0].Rows[0]["tgltrn"].ToString().Substring(0, 10);
        txtEnd.Text = ge.ListDataset.Tables[0].Rows[0]["tgljthtmp"].ToString().Substring(0, 10);
        txtbankGr.Text = ge.ListDataset.Tables[0].Rows[0]["bankgiro"].ToString();
        txtnoGr.Text = ge.ListDataset.Tables[0].Rows[0]["nogiro"].ToString();

    }

}