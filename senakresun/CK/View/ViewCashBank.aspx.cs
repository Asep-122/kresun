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


public partial class CL_View_ViewCashBank : System.Web.UI.Page
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
                PnlFront.Visible = false;
                Panel3.Visible = false;

                DateTime now = DateTime.Now;
                txtStart.Text = now.ToString("yyyy-MM-dd");
                txtEnd.Text = now.ToString("yyyy-MM-dd");
                BindGridEntity();

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                var1[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string)); 
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";
                ddlkdbank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkdbank.DataTextField = "kdnmbank";
                ddlkdbank.DataValueField = "KodeBank";
                ddlkdbank.DataBind();
                ddlkdbank.Items.Insert(0, new ListItem("Pilih Data", "-"));
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
        DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[5];
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        Paramsarea[1] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdbank.SelectedValue, typeof(String));
        Paramsarea[2] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        Paramsarea[3] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        Paramsarea[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWCARDCASHBANK", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlView.SelectedValue == "1")
        {
            Panel1.Visible = true;
            PnlFront.Visible = false;

        }
        else if (ddlView.SelectedValue == "2")
        {
            Panel1.Visible = false;
            PnlFront.Visible = true;
        }
        else
        {
            Panel1.Visible = false;
            PnlFront.Visible = false;
        }
         
        if (ddlView.SelectedValue == "2")
        {
            DefaultClass.Global.SqlHelper[] paramtotal = new DefaultClass.Global.SqlHelper[5];
            paramtotal[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            paramtotal[1] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdbank.SelectedValue, typeof(String));
            paramtotal[2] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            paramtotal[3] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            paramtotal[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWTOTALSALDOCASHBANK", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", paramtotal, "constrmf");

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


            DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[5];
            Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
            Paramsarea[1] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdbank.SelectedValue, typeof(String));
            Paramsarea[2] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            Paramsarea[3] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            Paramsarea[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWCARDCASHBANK", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", Paramsarea, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();
            //GRData.HeaderRow.Disposed = 1;
            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        else if (ddlView.SelectedValue == "1")
        { 
        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
        Params[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String)); 
        Params[1] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdbank.SelectedValue, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(String));
        Params[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SALDOCASHBANK", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", Params, "constrmf");
             
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                Txtsaldoawal.Text = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                txtsaldobulanini.Text = ge.ListDataset.Tables[0].Rows[0][2].ToString();
                txtsaldoakhir.Text = ge.ListDataset.Tables[0].Rows[0][3].ToString();
                txtgiroMsk.Text = ge.ListDataset.Tables[0].Rows[0][4].ToString();
                txtgiroMskBlnIni.Text = ge.ListDataset.Tables[0].Rows[0][4].ToString();
                txtgiroMsksldAkh.Text = ge.ListDataset.Tables[0].Rows[0][5].ToString();
                txtgiroKlr.Text = ge.ListDataset.Tables[0].Rows[0][6].ToString();
                txtgiroKlrBlnIni.Text = ge.ListDataset.Tables[0].Rows[0][7].ToString();
                txtgiroKlrAkh.Text = ge.ListDataset.Tables[0].Rows[0][8].ToString();
                txtSG.Text = ge.ListDataset.Tables[0].Rows[0][9].ToString();
                txtSGBlnIni.Text = ge.ListDataset.Tables[0].Rows[0][10].ToString();
                txtSGak.Text = ge.ListDataset.Tables[0].Rows[0][11].ToString();
                txtQGrMsk.Text = ge.ListDataset.Tables[0].Rows[0][12].ToString();
                txtQGrMskBlnIni.Text = ge.ListDataset.Tables[0].Rows[0][13].ToString();
                txtQGrMskAk.Text = ge.ListDataset.Tables[0].Rows[0][14].ToString();
                txtQGrKlr.Text = ge.ListDataset.Tables[0].Rows[0][15].ToString();
                txtQGrKlrBlnIni.Text = ge.ListDataset.Tables[0].Rows[0][16].ToString();
                txtQGrKlrAkh.Text = ge.ListDataset.Tables[0].Rows[0][17].ToString();
                txtDeb.Text = ge.ListDataset.Tables[0].Rows[0][18].ToString();
                txtDebBlnIni.Text = ge.ListDataset.Tables[0].Rows[0][19].ToString();
                txtDebAkh.Text = ge.ListDataset.Tables[0].Rows[0][20].ToString();
                txtKrd.Text = ge.ListDataset.Tables[0].Rows[0][21].ToString();
                txtKrdBlnIni.Text = ge.ListDataset.Tables[0].Rows[0][22].ToString();
                txtKrdAkh.Text = ge.ListDataset.Tables[0].Rows[0][23].ToString();
            }

            else
            {
                Txtsaldoawal.Text = "0";
                txtsaldobulanini.Text = "0";
                txtsaldoakhir.Text = "0";
                txtgiroMsk.Text = "0";
                txtgiroMskBlnIni.Text = "0";
                txtgiroMsksldAkh.Text = "0";
                txtgiroKlr.Text = "0";
                txtgiroKlrBlnIni.Text = "0";
                txtgiroKlrAkh.Text = "0";
                txtSG.Text = "0";
                txtSGBlnIni.Text = "0";
                txtSGak.Text = "0";
                txtQGrMsk.Text = "0";
                txtQGrMskBlnIni.Text = "0";
                txtQGrMskAk.Text = "0";
                txtQGrKlr.Text = "0";
                txtQGrKlrBlnIni.Text = "0";
                txtQGrKlrAkh.Text = "0";
                txtDeb.Text = "0";
                txtDebBlnIni.Text = "0";
                txtDebAkh.Text = "0";
                txtKrd.Text = "0";
                txtKrdBlnIni.Text = "0";
                txtKrdAkh.Text = "0";
            }
        }


        Double value;
        if (Double.TryParse(Txtsaldoawal.Text, out value))
            Txtsaldoawal.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            Txtsaldoawal.Text = String.Empty;

        if (Double.TryParse(txtsaldobulanini.Text, out value))
            txtsaldobulanini.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtsaldobulanini.Text = String.Empty;

        if (Double.TryParse(txtsaldoakhir.Text, out value))
            txtsaldoakhir.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtsaldoakhir.Text = String.Empty;

        if (double.TryParse(txtgiroMsk.Text, out value))
            txtgiroMsk.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtgiroMsk.Text = String.Empty;

        if (double.TryParse(txtgiroMskBlnIni.Text, out value))
            txtgiroMskBlnIni.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtgiroMskBlnIni.Text = String.Empty;

        if (double.TryParse(txtgiroMsksldAkh.Text, out value))
            txtgiroMsksldAkh.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtgiroMsksldAkh.Text = String.Empty;

        if (double.TryParse(txtgiroKlr.Text, out value))
            txtgiroKlr.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtgiroKlr.Text = String.Empty;

        if (double.TryParse(txtgiroKlrBlnIni.Text, out value))
            txtgiroKlrBlnIni.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtgiroKlrBlnIni.Text = String.Empty;

        if (double.TryParse(txtgiroKlrAkh.Text, out value))
            txtgiroKlrAkh.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtgiroKlrAkh.Text = String.Empty;

        if (double.TryParse(txtSG.Text, out value))
            txtSG.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtSG.Text = String.Empty;

        if (double.TryParse(txtSGBlnIni.Text, out value))
            txtSGBlnIni.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtSGBlnIni.Text = String.Empty;

        if (double.TryParse(txtSGak.Text, out value))
            txtSGak.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtSGak.Text = String.Empty;

        if (double.TryParse(txtQGrMsk.Text, out value))
            txtQGrMsk.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtQGrMsk.Text = String.Empty;

        if (double.TryParse(txtQGrMskBlnIni.Text, out value))
            txtQGrMskBlnIni.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtQGrMskBlnIni.Text = String.Empty;

        if (double.TryParse(txtQGrMskAk.Text, out value))
            txtQGrMskAk.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtQGrMskAk.Text = String.Empty;

        if (double.TryParse(txtQGrKlr.Text, out value))
            txtQGrKlr.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtQGrKlr.Text = String.Empty;

        if (double.TryParse(txtQGrKlrBlnIni.Text, out value))
            txtQGrKlrBlnIni.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtQGrKlrBlnIni.Text = String.Empty;

        if (double.TryParse(txtQGrKlrAkh.Text, out value))
            txtQGrKlrAkh.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtQGrKlrAkh.Text = String.Empty;

        if (double.TryParse(txtDeb.Text, out value))
            txtDeb.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtDeb.Text = String.Empty;

        if (double.TryParse(txtDebBlnIni.Text, out value))
            txtDebBlnIni.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtDebBlnIni.Text = String.Empty;

        if (double.TryParse(txtDebAkh.Text, out value))
            txtDebAkh.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtDebAkh.Text = String.Empty;

        if (double.TryParse(txtKrd.Text, out value))
            txtKrd.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtKrd.Text = String.Empty;

        if (double.TryParse(txtKrdBlnIni.Text, out value))
            txtKrdBlnIni.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtKrdBlnIni.Text = String.Empty;

        if (double.TryParse(txtKrdAkh.Text, out value))
            txtKrdAkh.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtKrdAkh.Text = String.Empty;
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

    protected void ddlNamaBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlkdbank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNAMABANK", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", var, "constrmf");

        string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
        string Alamat1 = ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
        string Alamat2 = ge.ListDataset.Tables[0].Rows[0]["Alamat2"].ToString();
        string Alamat3 = ge.ListDataset.Tables[0].Rows[0]["Alamat3"].ToString();
        string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
        string NOREK = ge.ListDataset.Tables[0].Rows[0]["NoRekening"].ToString();
        string btskrdt = ge.ListDataset.Tables[0].Rows[0]["BatasKredit"].ToString();

        string SaldoBerjalan = ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString();
        string saldoGiro = ge.ListDataset.Tables[0].Rows[0]["SaldoAwalGr"].ToString();
        txtnmBank.Text = NamaBank;
        txtalamat1.Text = Alamat1;
        txtalamat2.Text = Alamat2;
        txtalamat3.Text = Alamat3;
        txtnorek.Text = NOREK;
        txtbtskrdit.Text = btskrdt;
        //txtsaldogiro.Text = saldoGiro;
        //txtsaldojln.Text = SaldoBerjalan;
        //txtkurs1.Text = kdcrc;
    }

    protected void ddlView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlView.SelectedValue == "1")
        {
            panel2.Visible = false;
            Panel3.Visible = false;
        }
        else if(ddlView.SelectedValue == "2")
        {
            panel2.Visible = true;
            Panel3.Visible = true;
        }
        else
        {
            panel2.Visible = false;
            Panel3.Visible = false;
        }
    }
}