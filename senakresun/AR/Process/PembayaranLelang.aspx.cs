using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DefaultClass;
using EntitiesClass;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AR_Transaksi_PembayaranLelang : System.Web.UI.Page
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
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    public string noassetdb;

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        btnSimpan.Visible = true;
        btnDel.Visible = false;
        txtNoLelang.Text = "";
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        pnlPosting.Visible = true;
        pnlFilter.Visible = false;

        txtnobukti.ReadOnly = false;
        txttglbayar.ReadOnly = false;
        ddltypebayar.Enabled = true;
        txtbuktisetoran.ReadOnly = false;
        txtjmlbayar.ReadOnly = false;
        txtKeterangan.ReadOnly = false;
        txtKeterangan.ReadOnly = false;
    }


    protected void gvListBayarLelang_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "SET":
                try
                {
                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    btnSimpan.Visible = false;
                    Pnlcustomer.Visible = false;
                    pnlFilter.Visible = false;
                    pnlPosting.Visible = true;
                    btnDel.Visible = true;
                    pnladd.Visible = true;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@nobayar", ParameterDirection.Input, SqlDbType.VarChar, 30, ((Label)gvListBayarLelang.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobayar")).Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DATAROWBAYARLELANG", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
                    var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("Ar_Bayar_Lelang", var, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {

                        txtnobukti.Text = ge.ListDataset.Tables[0].Rows[0]["nobayar"].ToString();
                        txttglbayar.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglbayar"].ToString()).ToString("dd-MM-yyyy");
                        ddltypebayar.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["typebayar"].ToString();
                        txtbuktisetoran.Text = ge.ListDataset.Tables[0].Rows[0]["buktisetor"].ToString();
                        if (txtbuktisetoran.Text == "")
                        {
                            divBuktisetor.Visible = false;
                        }
                        else
                        {
                            divBuktisetor.Visible= true;
                        }
                        txtjmlbayar.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jumlahbayar"].ToString()));
                        txtKeterangan.Text = ge.ListDataset.Tables[0].Rows[0]["keterangan"].ToString();
                        txtNoLelang.Text = ge.ListDataset.Tables[0].Rows[0]["nolelang"].ToString();
                        txtNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["noanggota"].ToString();
                        txtNoRek.Text = ge.ListDataset.Tables[0].Rows[0]["norekening"].ToString();
                        txtTglLelang.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglelang"].ToString()).ToString("dd-MM-yyyy");
                        txtHargaLelang.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["hargalelang"].ToString()));

                    }
                    txtnobukti.ReadOnly = true;
                    txttglbayar.ReadOnly = true;
                    ddltypebayar.Enabled = false;
                    txtbuktisetoran.ReadOnly = true;
                    txtjmlbayar.ReadOnly = true;
                    txtKeterangan.ReadOnly = true;
                    txtKeterangan.ReadOnly = true;

                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;
            case "PRINT":
                try
                {
                   
                    string typebayar = ((Label)gvListBayarLelang.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypebayar")).Text;

                    if (typebayar == "2")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Journal tidak ditemukan karena rekening belum di WO');", true);
                    }
                    else
                    {
                        
                        var nobukti = ((Label)gvListBayarLelang.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobayar")).Text;

                            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                            Paramsc[0] = gbx.CreateSPParameter("@nobayar", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                            Paramsc[1] = gbx.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                            Paramsc[2] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            Paramsc[3] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTJOURNAL", typeof(string));
                            Paramsc[4] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            Paramsc[5] = gbx.CreateSPParameter("@Kdsumber", ParameterDirection.Input, SqlDbType.VarChar, 50, "AR", typeof(string));
                            gex.ListDataTable = gbx.GetDataJustOutPut("dbo.Ar_Bayar_Lelang", Paramsc, "constrmf").Tables[0];

                            if (gex.ListDataTable.Rows.Count > 0)
                            {
                                rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                                rprt.SetDataSource(gex.ListDataTable);
                                Response.Buffer = false;
                                // Clear the response content and headers
                                Response.ClearContent();
                                Response.ClearHeaders();
                                Response.ContentType = "application/pdf";
                                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            }
                            else
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nomor bukti " + nobukti + " tidak memiliki journal');", true);

                            }
                    }
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;

            case "SLIP":

                try
                {
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)gvListBayarLelang.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobayar")).Text;
                    typetrn = "K";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@NoAsset", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAS", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typebayar", ParameterDirection.Input, SqlDbType.VarChar, 10, '1', typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.Ar_Bayar_Lelang", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/AE/Report/CR/CLKwitansikeluar.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                      
                            //rprt.SetParameterValue("Reportname", "Terima Cash Bank");
                       
                       

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
                        GC.Collect();
                    }
                }

                break;

        }

    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
        //if (txtSearch.Text == "")
        //{
        //    BindGridEntity();
        //}
        //else
        //{
        //    BindGridEntityx();
        //}
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
                BindGridEntity();

                ddltypebayar.Items.Insert(0, new ListItem("Rekening sudah di WO", "1"));
                ddltypebayar.Items.Insert(1, new ListItem("Rekening normal belum di WO", "2"));

                divBuktisetor.Visible = false;
                

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTBAYARLELANG", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Ar_Bayar_Lelang", var, "constrmf");


            gvListBayarLelang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            gvListBayarLelang.DataBind();

            gvListBayarLelang.UseAccessibleHeader = true;
            gvListBayarLelang.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    public void btnSearch_Click(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SEARCHDATA", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@nolelang", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoLelang.Value, typeof(string));
        var[5] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        var[6] = gb.CreateSPParameter("@noanggota", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoAnggota.Value, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Ar_Bayar_Lelang", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            //txtKondisi.Text = (ge.ListDataset.Tables[0].Rows[0]["kondisi"].ToString() == "1")?"BAGUS": (ge.ListDataset.Tables[0].Rows[0]["kondisi"].ToString() == "2") ? "KURANG BAGUS":"TIDAK BAGUS";

            txtNoLelang.Text = ge.ListDataset.Tables[0].Rows[0]["no_lelang"].ToString();
            txtNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
            txtNoRek.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
            txtTglLelang.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lelang"].ToString()));
            txtHargaLelang.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["harga_jual"].ToString()));

            btnSimpan.Visible = true;
        }
        else
        {
            txtNoLelang.Text = "";
            txtNoAnggota.Text = "";
            txtNoRek.Text = "";
            txtTglLelang.Text = "";
            txtHargaLelang.Text = "";


            btnSimpan.Enabled = false;
        }


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNoLelang.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Lelang tidak ada!');", true);
                btnSimpan.Enabled = true;
            }
            else
            {

                string cekdata = "1";

                if (ddltypebayar.SelectedValue == "2")
                {
                    DefaultClass.Global.SqlHelper[] varkop1 = new DefaultClass.Global.SqlHelper[5];
                    varkop1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLUNASAST", typeof(string));
                    varkop1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varkop1[2] = gb.CreateSPParameter("@no_rekening1", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoRek.Text, typeof(string));
                    varkop1[3] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    varkop1[4] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoAnggota.Text, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varkop1, "constrmf");
                    if (ge.ListDataset.Tables[0].DefaultView.Count > 0)

                    {
                        cekdata = "1";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rekening tersebut belum lunas, Lunasi terlebih dahulu!');", true);
                        btnSimpan.Enabled = true;

                    }
                    else

                    {
                        cekdata = "0";


                    }

                }
                else
                {

                    cekdata = "0";



                }

                if (cekdata == "0")
                {


               


                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[17];
                    Params[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTBAYARLELANG", typeof(String));
                    Params[1] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(String));
                    Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(String));
                    Params[3] = gb.CreateSPParameter("@nobayar", ParameterDirection.Input, SqlDbType.VarChar, 20, "", typeof(String));
                    Params[4] = gb.CreateSPParameter("@tglbayar", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglbayar.Text, typeof(DateTime));
                    Params[5] = gb.CreateSPParameter("@typebayar", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypebayar.SelectedValue, typeof(String));
                    Params[6] = gb.CreateSPParameter("@buktisetor", ParameterDirection.Input, SqlDbType.VarChar, 30, txtbuktisetoran.Text, typeof(String));
                    Params[7] = gb.CreateSPParameter("@jmlhbayar", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjmlbayar.Text, typeof(Decimal));
                    Params[8] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKeterangan.Text, typeof(String));
                    Params[9] = gb.CreateSPParameter("@nolelang", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoLelang.Text, typeof(String));
                    Params[10] = gb.CreateSPParameter("@noanggota", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoAnggota.Text, typeof(String));
                    Params[11] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNoRek.Text, typeof(String));
                    Params[12] = gb.CreateSPParameter("@tgllelang", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLelang.Text, typeof(DateTime));
                    Params[13] = gb.CreateSPParameter("@hargalelang", ParameterDirection.Input, SqlDbType.Decimal, 0, txtHargaLelang.Text, typeof(Decimal));
                    Params[14] = gb.CreateSPParameter("@createdby", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
                    Params[15] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 10, "AB01", typeof(String));
                    Params[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(String));


                    ge.ListDataset = gb.GetDataJustOutPut("Ar_Bayar_Lelang", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','2');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        //gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        //PnlFront.Visible = true;
        //Response.Redirect("AssetLelangTarikan.aspx");
        Response.Redirect(Request.Url.AbsoluteUri);

    }

    [WebMethod]
    public static string GetDataLelang(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 200, InputText, typeof(string));
        Params[1] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchIDx, typeof(string));
        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpershx, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Ar_Bayar_Lelang", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];
            Params[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEBAYARLELANG", typeof(string));
            Params[1] = gb.CreateSPParameter("@nobayar", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            Params[2] = gb.CreateSPParameter("@nolelang", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoLelang.Text, typeof(string));
            Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[4] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            Params[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("Ar_Bayar_Lelang", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {


                btnDel.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','2');", true);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(gagal..!'" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                //gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(Error..!'" + X.Message.ToString().Replace("'", "") + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void ddltypebayar_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypebayar.SelectedValue == "2")
        {
            divBuktisetor.Visible = true;
        }
        else
        {
            divBuktisetor.Visible = false;
        }
    }

}