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

public partial class FA_View_MelihatKartuTM : System.Web.UI.Page
{
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
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
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessIsSuperAdmin
    {
        get { return Session["SessIsSuperAdmin"].ToString(); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }

    Global gb4 = new Global();
    GlobalEntities ge4 = new GlobalEntities();
    Global gb1 = new Global();
    GlobalEntities ge1 = new GlobalEntities();
    Global gb2 = new Global();
    GlobalEntities ge2 = new GlobalEntities();
    Global gb3 = new Global();
    GlobalEntities ge3 = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));


                ge.ListDataset = gb.GetDataJustOutPut("FA_PembuatanAktiva", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAktiva asc";

                ddlgroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlgroup.DataTextField = "aktiva";
                ddlgroup.DataValueField = "KodeAktiva";
                ddlgroup.DataBind();
                ddlgroup.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //BindGridEntity();
                
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
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
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
    }
    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb1.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgroup.SelectedValue, typeof(string));
            var1[1] = gb1.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb1.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[3] = gb1.CreateSPParameter("@statusadmin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessIsSuperAdmin, typeof(string));
            var1[4] = gb1.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "SELECTVIEWTM", typeof(string));
            var1[5] = gb1.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge1.ListDataset = gb1.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", var1, "constrmf");
            ge1.ListDataTable = gb1.GetDataJustOutPut("FA_PENGISIANAKTIVAAWAL", var1, "constrmf").Tables[0];

            ge1.ListDataset.Tables[0].DefaultView.Sort = "KodeAktiva asc";


            GRData.DataSource = ge1.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }



    }
    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            BindGridEntity();
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];

            var[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgroup.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
            var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("FA_PembuatanAktiva", var, "constrmf");
            
            txtnamaaktiva.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAktiva"].ToString();
            txtlokasi.Text = ge.ListDataset.Tables[0].Rows[0]["KodeLokasi"].ToString();
            txtnomoraktiva.Text = ge.ListDataset.Tables[0].Rows[0]["NoAktiva"].ToString();
            txtspek.Text = ge.ListDataset.Tables[0].Rows[0]["Spesifikasi"].ToString();
            txtmerk.Text = ge.ListDataset.Tables[0].Rows[0]["Merek"].ToString();
            txtnomormesin.Text = ge.ListDataset.Tables[0].Rows[0]["NoMesin"].ToString();
            txtnomorchasis.Text = ge.ListDataset.Tables[0].Rows[0]["NoChasis"].ToString();
            txtgolonganaktiva.Text = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
            txtkelompok.Text = ge.ListDataset.Tables[0].Rows[0]["KodeKel"].ToString();
            txtmetodeakuntansi.Text = ge.ListDataset.Tables[0].Rows[0]["MetodeSusutAkt"].ToString();
            txtakuntansi.Text = ge.ListDataset.Tables[0].Rows[0]["TarifSusutAkt"].ToString();
            txtmotedeUU.Text = ge.ListDataset.Tables[0].Rows[0]["MetodeSusutUU"].ToString();
            txtuu.Text = ge.ListDataset.Tables[0].Rows[0]["TarifSusutUU"].ToString();
            txtketerangan.Text = ge.ListDataset.Tables[0].Rows[0]["Keterangan"].ToString();
            txtbarcode.Text = ge.ListDataset.Tables[0].Rows[0]["barcode_id"].ToString();
            Image1.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepathphoto"].ToString();

            if (ge.ListDataset.Tables[0].Rows[0]["AktivaAktif"].Equals(true))
            {
                chkaktiva.Checked = true;
            }
            else
            {
                chkaktiva.Checked = false;
            }

            if (ge.ListDataset.Tables[0].Rows[0]["BolehSusut"].Equals(true))
            {
                chkboleh.Checked = true;
            }
            else
            {
                chkboleh.Checked = false;
            }

            
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
}