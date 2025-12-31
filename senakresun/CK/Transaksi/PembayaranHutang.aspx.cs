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

public partial class CL_Transaksi_PembayaranHutang : System.Web.UI.Page
{
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

    DataTable dtRP = new DataTable("Transaction");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        PnlJournalSupplier.Visible = true;
        ddlnobukti.Enabled = true;
        ddlnobukti.Focus(); 
    }
    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        if(ddlJnsByr.SelectedValue =="3" && ddltypejur.SelectedValue == "3")
        {
            pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = false;
            PnlJournalSupplier.Visible = false;

            pnlIDS.Visible = true;
            PnlIDB.Visible = false;
            PnlJournalBank.Visible = true;
            Pnlfooter.Visible = false;
            txtsupalamat1.Focus();
        }
        else if (ddlJnsByr.SelectedValue == "2" && ddltypejur.SelectedValue == "3")
        {
            pnlIDS.Visible = false;
            PnlIDB.Visible = true;
            PnlJournalSupplier.Visible = false;
            PnlJournalBank.Visible = false;
            //pnlJournalJPF.Visible = true;
            pnljournalUM.Visible = true;
            pnltrn.Visible = false;
            Pnlfooter.Visible = false;

            //Response.Redirect(Request.Url.AbsoluteUri);

        }
        else if(ddlJnsByr.SelectedValue == "2" && ddltypejur.SelectedValue == "1")
        {
            pnltrn.Visible = true;
            PnlGrid.Visible = false;
            pnltypejurnal.Visible = false;
            Pnlfooter.Visible = false;
            pnlJournalJurnal.Visible = true;
        }
        else if (ddlJnsByr.SelectedValue == "2" && ddltypejur.SelectedValue == "2")
        {
            pnltrn.Visible = true;
            PnlGrid.Visible = false;
            pnltypejurnal.Visible = false;
            Pnlfooter.Visible = false;
            pnlJournalJurnal.Visible = true;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    //SETUP KODE KONTER
    int nosekarang = 0;
    string formatnumber = "";
    string nosebelumnya = "";
    int nonasabah = 0;
    string CRNonasabah = "";
    string updateno = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                BindGridEntity();

                //KODEBANK
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDKODEBANK", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "kdnm";
                ddlKdBank.DataValueField = "KodeBank";
                ddlKdBank.DataBind();
                ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //NOBUKTI
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKDBUKTI", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeKonter asc";


                ddlnobukti.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlnobukti.DataTextField = "nobukti";
                ddlnobukti.DataValueField = "KodeKonter";
                ddlnobukti.DataBind();
                ddlnobukti.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //TYPETRANSAKSI
                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                var2[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
                var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANS", typeof(string));
                var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var2, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn asc";

                ddltypetrn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddltypetrn.DataTextField = "TypeTrans";
                ddltypetrn.DataValueField = "TypeTrn";
                ddltypetrn.DataBind();
                ddltypetrn.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //KODE LANGGANAN
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                var3[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
                var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKODESUPPLIER", typeof(string));
                var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var3, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeLgn asc";

                ddlkdsup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkdsup.DataTextField = "knLgn";
                ddlkdsup.DataValueField = "KodeLgn";
                ddlkdsup.DataBind();
                ddlkdsup.Items.Insert(0, new ListItem("Pilih Data", "-"));

                DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[2]; 
                var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORJURNAL", typeof(string));
                var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var6, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";

                ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlJurnal.DataTextField = "kdJurnal";
                ddlJurnal.DataValueField = "KodeJurnal";
                ddlJurnal.DataBind();
                ddlJurnal.Items.Insert(0, new ListItem("Pilih Data", "-"));
            }
            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void ddlNamaBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        //var bankID = ddlBankId.SelectedValue;

        //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[24];
        //var1[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bankID, typeof(string)); 
        //var1[22] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORNAMABANK", typeof(string));
        //var1[23] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var1, "constrmf");

        //string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaGl"].ToString();
        //txtnmBank.Text = NamaBank;
    }


    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

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
                    pnlHeader.Visible = false;

                    ddlnobukti.Enabled = false;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    ddlnobukti.SelectedValue = txtnobukti.Text.Substring(0, 4);
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    ddltypetrn.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypetrns")).Text;
                    if(ddltypetrn.SelectedValue == "K")
                    {
                        txttypetrn.Text = "Keluar";
                    }
                    else if (ddltypetrn.SelectedValue == "L")
                    {
                        txttypetrn.Text = "Keluar cek";
                    }
                    else if (ddltypetrn.SelectedValue == "M")
                    {
                        txttypetrn.Text = "Kleluar Giro";
                    }
                    txttgljthtmp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgljthtmp")).Text;
                    ddlkdsup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    //ddlkursstd.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkursstd")).Text;
                    txtkursstd.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkursstd")).Text;
                    ddlKdBank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    txtNmBank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnmbank")).Text;
                    //ddlkurstdbank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkursstdbank")).Text;
                    txtkursstdbank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkursstdbank")).Text;
                    txtBankGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbankgiro")).Text;
                    txtnoGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnogiro")).Text;
                    ddlStatusGiro.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatusgiro")).Text;
                    if (ddlStatusGiro.SelectedValue == "C")
                    {
                        txtStatusGiro.Text = "Cek/Giro Cair Atau Tunai";
                    }
                    else if (ddlStatusGiro.SelectedValue == "M")
                    {
                        txtStatusGiro.Text = "Cek/Giro Mundur";
                    }
                    //txttransAP.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljmlhtrnsAP")).Text;
                    //txtjmlhGL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljmlhGL")).Text;
                    //txtjmlhCL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljmlhCL")).Text;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    //ddlJnsByr.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text;
                    //if (ddlStatusGiro.SelectedValue == "2")
                    //{
                    //    txtStatusGiro.Text = "Melunasi Faktur Tertentu";
                    //}
                    //else if (ddlStatusGiro.SelectedValue == "3")
                    //{
                    //    txtStatusGiro.Text = "Disimpan sebagai Uang Muka";
                    //}
                    ddltypejur.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypejurnal")).Text;
                    if (ddltypejur.SelectedValue == "1")
                    {
                        txttypeJur.Text = "Double Line Jurnal";
                    }
                    else if (ddltypejur.SelectedValue == "2")
                    {
                        txttypeJur.Text = "Multiple Line Jurnal";
                    }
                    else if (ddltypejur.SelectedValue == "2")
                    {
                        txttypeJur.Text = "Tidak Pakai Jurnal";
                    }

                    
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":

                try
                {
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 8, txtnobukti.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Golongan sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {
                        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[24];
                        //var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlBankId.SelectedValue, typeof(string)); 
                        //var[22] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        //var[23] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                        //ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);

                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        }
                    }
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

        }
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.AbsoluteUri);
        pnlHeader.Visible = true;
        PnlFront.Visible = true;
        pnladd.Visible = false;
        PnlJournalSupplier.Visible = false;
        ddlnobukti.Enabled = true;
        ddlnobukti.Focus();

        PnlJournalSupplier.Visible = false;
        PnlJournalBank.Visible = false;
        PnlIDB.Visible = false;
        pnlIDS.Visible = false;
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        string STATEDL = "";
        var ckbx = "";

        try
        {
            if (ddlnobukti.Enabled == true)
            {
                STATE = "INSERT";
                STATEDL = "INSERTDL";
                string nocountersekarang = HDNoNasabah.Value.Substring(2);
                string nocountersebelumnya = HDNoSebelumnya.Value.Substring(2);

                var selected = ddlnobukti.SelectedValue;
                var KdCompany = SessKdpersh;

                //UPDATE NO BUKTI COUNTER TO TABLE SuKonter
                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                var2[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nocountersekarang, typeof(string));
                var2[1] = gb.CreateSPParameter("@bankGiro", ParameterDirection.Input, SqlDbType.VarChar, 50, nocountersebelumnya, typeof(string));
                var2[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, KdCompany, typeof(string));
                var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEKONTERID", typeof(string));
                var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn", var2, "constrmf");
            }
            else
            {
                STATE = "UPDATE";
                STATEDL = "UPDATEDL";
            }
            if(ddltypejur.SelectedValue == "1")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[21];
                var[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@tglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltypetrn.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@kursstdsup", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkursstd.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@jmlhcl", ParameterDirection.Input, SqlDbType.VarChar, 30, txtjmlhCL.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@kurssup", ParameterDirection.Input, SqlDbType.VarChar, 3, txtkursSup.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@Debit1", ParameterDirection.Input, SqlDbType.VarChar, 30, txtDebit1.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@Debit3", ParameterDirection.Input, SqlDbType.VarChar, 30, txtDebit3.Text, typeof(string));
                var[8] = gb.CreateSPParameter("@Kredit1", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKredit1.Text, typeof(string));
                var[9] = gb.CreateSPParameter("@Kredit3", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKredit3.Text, typeof(string));
                var[10] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtKeterangan.Text, typeof(string));
                var[11] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[12] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
                var[13] = gb.CreateSPParameter("@STATEDL", ParameterDirection.Input, SqlDbType.VarChar, 50, STATEDL, typeof(string));
                var[14] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[15] = gb.CreateSPParameter("@kdsup", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdsup.SelectedValue, typeof(string));
                var[16] = gb.CreateSPParameter("@bankGiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtBankGiro.Text, typeof(string));
                var[17] = gb.CreateSPParameter("@noGiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoGiro.Text, typeof(string));
                var[18] = gb.CreateSPParameter("@statusgiro", ParameterDirection.Input, SqlDbType.VarChar, 1, txtStatusGiro.Text, typeof(string));
                var[19] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                var[20] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtmp.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
            }
            else if(ddltypejur.SelectedValue == "2")
            {
                dtRP.Columns.Clear();

                dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
                dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
                dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));
                dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));

                dtRP.Rows.Clear();


                for (int i = 0; i < GRRP.Rows.Count; i++)
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
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[10];

                Params[0] = gb.CreateSPParameter("@NoBuktiKns", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                Params[1] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypetrn.SelectedValue, typeof(string));
                Params[2] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkursSup.Text, typeof(string));
                Params[3] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkursstd.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[6] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[7] = gb.CreateSPParameter("@Nourut", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                Params[8] = gb.CreateSPParameter("@CL_Transaction_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("cL_Trn_Insert_Detail", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
            }

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[42];
            var1[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@tglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(string));
            var1[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltypetrn.Text, typeof(string));
            var1[3] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtmp.Text, typeof(string));
            var1[4] = gb.CreateSPParameter("@kdsup", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdsup.SelectedValue, typeof(string));
            var1[5] = gb.CreateSPParameter("@nmSup", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNmSup.Text, typeof(string));
            var1[6] = gb.CreateSPParameter("@kursstdsup", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkursstd.Text, typeof(string));
            var1[7] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
            var1[8] = gb.CreateSPParameter("@nmbank", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNmBank.Text, typeof(string));
            var1[9] = gb.CreateSPParameter("@kursstdbank", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkursstdbank.Text, typeof(string));
            var1[10] = gb.CreateSPParameter("@bankGiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtBankGiro.Text, typeof(string));
            var1[11] = gb.CreateSPParameter("@noGiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoGiro.Text, typeof(string));
            var1[12] = gb.CreateSPParameter("@statusgiro", ParameterDirection.Input, SqlDbType.VarChar, 1, txtStatusGiro.Text, typeof(string));
            var1[13] = gb.CreateSPParameter("@jmlhtrnap", ParameterDirection.Input, SqlDbType.Money, 30, txttransAP.Text, typeof(string));
            var1[14] = gb.CreateSPParameter("@jmlhtrngl", ParameterDirection.Input, SqlDbType.Money, 30, txtjmlhGL.Text, typeof(string));
            var1[15] = gb.CreateSPParameter("@jmlhcl", ParameterDirection.Input, SqlDbType.Money, 30, txtjmlhCL.Text, typeof(string));
            var1[16] = gb.CreateSPParameter("@BuyFlag", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlJnsByr.SelectedValue, typeof(string));
            var1[17] = gb.CreateSPParameter("@typejurnal", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltypejur.SelectedValue, typeof(string));
            var1[18] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtKeterangan.Text, typeof(string));
            var1[19] = gb.CreateSPParameter("@Alamat1sup", ParameterDirection.Input, SqlDbType.VarChar, 50, txtsupalamat1.Text, typeof(string));
            var1[20] = gb.CreateSPParameter("@Alamat2sup", ParameterDirection.Input, SqlDbType.VarChar, 50, txtsupalamat2.Text, typeof(string));
            var1[21] = gb.CreateSPParameter("@Alamat3sup", ParameterDirection.Input, SqlDbType.VarChar, 50, txtsupalamat3.Text, typeof(string));
            var1[22] = gb.CreateSPParameter("@kurssup", ParameterDirection.Input, SqlDbType.VarChar, 3, txtkursSup.Text, typeof(string));
            var1[23] = gb.CreateSPParameter("@sldHtg", ParameterDirection.Input, SqlDbType.Money, 30, txtsldHtng.Text, typeof(string));
            var1[24] = gb.CreateSPParameter("@grMndr", ParameterDirection.Input, SqlDbType.Money, 30, txtGrMndr.Text, typeof(string));
            var1[25] = gb.CreateSPParameter("@Alamat1bank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalamatbank1.Text, typeof(string));
            var1[26] = gb.CreateSPParameter("@Alamat2bank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalamatbank2.Text, typeof(string));
            var1[27] = gb.CreateSPParameter("@Alamat3bank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalamatbank3.Text, typeof(string));
            var1[28] = gb.CreateSPParameter("@sldjln", ParameterDirection.Input, SqlDbType.Money, 50, txtsldjln.Text, typeof(string));
            var1[29] = gb.CreateSPParameter("@sldGr", ParameterDirection.Input, SqlDbType.Money, 50, txtsldgr.Text, typeof(string));
            var1[30] = gb.CreateSPParameter("@Jurnal", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlJurnal.SelectedValue, typeof(string));
            var1[31] = gb.CreateSPParameter("@KetJurnal", ParameterDirection.Input, SqlDbType.VarChar, 40, txtJurnal.Text, typeof(string));
            var1[32] = gb.CreateSPParameter("@Debit1", ParameterDirection.Input, SqlDbType.VarChar, 30, txtDebit1.Text, typeof(string));
            var1[33] = gb.CreateSPParameter("@Debit2", ParameterDirection.Input, SqlDbType.VarChar, 30, txtDebit2.Text, typeof(string));
            var1[34] = gb.CreateSPParameter("@Debit3", ParameterDirection.Input, SqlDbType.VarChar, 30, txtDebit3.Text, typeof(string));
            var1[35] = gb.CreateSPParameter("@Kredit1", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKredit1.Text, typeof(string));
            var1[36] = gb.CreateSPParameter("@Kredit2", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKredit2.Text, typeof(string));
            var1[37] = gb.CreateSPParameter("@Kredit3", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKredit3.Text, typeof(string));
            var1[38] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var1[39] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
            var1[40] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var1[41] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var1, "constrmf");
            //if (ddlnobukti.Enabled == true)
            //{
            //    ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var, "constrmf");
            //}
            //else
            //{
            //    ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var, "constrmf");
            //}


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);


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

    protected void ddlNoBukti_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = ddlnobukti.SelectedValue;

        //TEXTBOX AUTO WILAYAH
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[3];
        varT[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKONTERID", typeof(string));
        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", varT, "constrmf");

        nosekarang = int.Parse(ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString());
        formatnumber = ge.ListDataset.Tables[0].Rows[0]["NoAwal"].ToString();
        nosebelumnya = ge.ListDataset.Tables[0].Rows[0]["NoSebelumnya"].ToString();

        nonasabah = nosekarang + 1;

        CRNonasabah = SessKdpersh + String.Format("{0:" + formatnumber + "}", nonasabah);
        var nonow = SessKdpersh + String.Format("{0:" + formatnumber + "}", nosekarang);

        updateno = String.Format("{0:" + formatnumber + "}", nonasabah);

        HDNoNasabah.Value = CRNonasabah;
        HDNoSebelumnya.Value = nonow;

        txtnobukti.Text = "CL" + CRNonasabah;
    }
    protected void ddlTypeTrn_SelectedIndexChanged(object sender, EventArgs e)
    {
        var typtrn = ddltypetrn.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, typtrn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORTYPETRANSKET", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var, "constrmf");

        string NamaBank = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();
        txttypetrn.Text = NamaBank;
        txtKeterangan.Text = NamaBank;
    }
    protected void ddlKdBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlKdBank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNAMABANK", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var, "constrmf");

        string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
        string Alamat1 = ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
        string Alamat2 = ge.ListDataset.Tables[0].Rows[0]["Alamat2"].ToString();
        string Alamat3 = ge.ListDataset.Tables[0].Rows[0]["Alamat3"].ToString();
        string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
        string kursstd = ge.ListDataset.Tables[0].Rows[0]["Kurs"].ToString();

        string SaldoBerjalan = ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString();
        string saldoGiro = ge.ListDataset.Tables[0].Rows[0]["SaldoAwalGr"].ToString();
        txtNmBank.Text = NamaBank;
        txtalamatbank1.Text = Alamat1;
        txtalamatbank2.Text = Alamat2;
        txtalamatbank3.Text = Alamat3;
        txtkursbank.Text = kdcrc;
        txtkursstdbank.Text = kursstd.Replace(",0000", ""); ;
        txtsldjln.Text = SaldoBerjalan.Replace(",0000", ""); ;
        txtsldgr.Text = saldoGiro.Replace(",0000", ""); ;


        //txtn.Text = NamaBank;
        //txtAlamat1.Text = Alamat1;
        //txtAlamat2.Text = Alamat2;
        //txtAlamat3.Text = Alamat3;
        //txtsaldogiro.Text = saldoGiro;
        //txtsaldojln.Text = SaldoBerjalan;
        //txtkurs1.Text = kdcrc;
    }
    protected void ddlStatusGiro_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlStatusGiro.SelectedValue == "C")
        {
            txtStatusGiro.Text = "Cek/Giro Cair Atau Tunai";
        }
        else if (ddlStatusGiro.SelectedValue == "M")
        {
            txtStatusGiro.Text = "Cek/Giro Mundur";
        }
        else
        {
            txtStatusGiro.Text = "";
        }
    }
    protected void ddlJnsByr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJnsByr.SelectedValue == "2")
        {
            txtJnsByr.Text = "Melunasi Faktor Tertentu";
        }
        else if (ddlJnsByr.SelectedValue == "3")
        {
            txtJnsByr.Text = "Disimpan sebagai Uang Muka";
        }
        else
        {
            txtJnsByr.Text = "";
        }
    }
    protected void ddltypejur_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypejur.SelectedValue == "1")
        {
            txttypeJur.Text = "Double Line Jurnal";
        }
        else if (ddltypejur.SelectedValue == "2")
        {
            txttypeJur.Text = "Multiple Line Jurnal";
        }
        else if (ddltypejur.SelectedValue == "3")
        {
            txttypeJur.Text = "Tidak ada Jurnal";
        }
        else
        {
            txttypeJur.Text = "";
        }
    }
    protected void ddlKdSup_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlkdsup.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 15, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKODESUPPLIERKET", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var, "constrmf");

        var Namalangganan = ge.ListDataset.Tables[0].Rows[0]["NamaLgn"].ToString();
        var kursSTDSupp = ge.ListDataset.Tables[0].Rows[0]["Kurs"].ToString();
        string Alamat1 = ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
        string Alamat2 = ge.ListDataset.Tables[0].Rows[0]["Kota"].ToString();
        string Alamat3 = ge.ListDataset.Tables[0].Rows[0]["Propinsi"].ToString();
        string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
        //string kursstd = ge.ListDataset.Tables[0].Rows[0]["Kurs"].ToString();

        string sldhtg = ge.ListDataset.Tables[0].Rows[0]["SaldoAwal"].ToString();
        string saldoGiro = ge.ListDataset.Tables[0].Rows[0]["SaldoGiro"].ToString();

        txtNmSup.Text = Namalangganan;
        txtkursstd.Text = kursSTDSupp.Replace(",0000", "");
        txtsupalamat1.Text = Alamat1;
        txtsupalamat2.Text = Alamat2;
        txtsupalamat3.Text = Alamat3;
        txtkursSup.Text = kdcrc;
        txtsldHtng.Text = sldhtg.Replace(",0000", "");
        txtGrMndr.Text = saldoGiro.Replace(",0000", ""); 
        //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeLgn asc";

    }
    //button ke panel informasi data supplier 
    protected void Btnnextjournal_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = false;
        PnlJournalSupplier.Visible = false;

        pnlIDS.Visible = true;
        PnlIDB.Visible = false;
        PnlJournalBank.Visible = true;
        txtsupalamat1.Focus();
    }

    //button ke panel informasi data Bank 
    protected void BtnnextjournalIDB_Click(object sender, EventArgs e)
    {
        pnlIDS.Visible = false;
        PnlIDB.Visible = true;
        PnlJournalSupplier.Visible = false;
        PnlJournalBank.Visible = false;
        //pnlJournalJPF.Visible = true;
        pnljournalUM.Visible = true;
        if (ddlJnsByr.SelectedValue == "3" && ddltypejur.SelectedValue == "3")
        {

            pnljournalUM.Visible = false;
            Pnlfooter.Visible = true;
        }
    }

    //button balik ke panel informasi data supplier 
    protected void btnCancelIDS_Click(object sender, EventArgs e)
    {
        //pnlIDS.Visible = true;
        //PnlIDB.Visible = false;
        //PnlJournalIDB.Visible = false;
        //PnlJournalIDS.Visible = true;
        //txtsupalamat1.Focus();

        pnladd.Visible = true;
        pnlIDS.Visible = false;
        PnlJournalBank.Visible = false;
        PnlJournalSupplier.Visible = true;
    }
    //Button ke panel jenis pembayaran atau type jurnal
    protected void BtnnextjournalUM_Click(object sender, EventArgs e)
    {
        PnlIDB.Visible = false;
        Pnlfooter.Visible = true;
        pnljournalUM.Visible = false;
        if (ddlJnsByr.SelectedValue == "2" && ddltypejur.SelectedValue == "3")
        {
            //ddlJurnal Jenis Pembayaran tanpa Type Jurnal
            pnltrn.Visible = true;
            lnkAddRTP_Click();
            pnlJournalJurnal.Visible = false;
            Pnlfooter.Visible = true;
        } 
        else if (ddlJnsByr.SelectedValue == "2" && ddltypejur.SelectedValue == "2")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine
            pnltrn.Visible = true;
            lnkAddRTP_Click();
            Pnlfooter.Visible = false;
            pnlJournalJurnal.Visible = true; 
        }
        else if (ddlJnsByr.SelectedValue == "2" &&  ddltypejur.SelectedValue == "1")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine
            pnltrn.Visible = true;
            lnkAddRTP_Click();
            Pnlfooter.Visible = false;
            pnlJournalJurnal.Visible = true;
        }
        else if (ddlJnsByr.SelectedValue == "3" && ddltypejur.SelectedValue =="2")
        {  //ddlJurnal Multiline tanpa jenis pembayaran

            PnlGrid.Visible = true;
            Pnlfooter.Visible = true;
            pnlJournalJurnal.Visible = false;
            lnkAddRP2_Click();
        }
        else if (ddlJnsByr.SelectedValue == "3" && ddltypejur.SelectedValue == "1")
        {
            //ddlJurnal DOUBLE LINE tanpa jenis pembayaran
            //pnltypejurnal.Visible = true;
            //Pnlfooter.Visible = true; 
            pnlIDS.Visible = false;
            PnlIDB.Visible = false;
            PnlJournalSupplier.Visible = false;
            PnlJournalBank.Visible = false;
            pnltypejurnal.Visible = true;
            pnltrn.Visible = false;
            pnlJournalJurnal.Visible = false;
            Pnlfooter.Visible = true;

        }
        
    }

    protected void btnCancelIDback_Click(object sender,EventArgs e)
    {
        pnlIDS.Visible = true;
        PnlIDB.Visible = false;
        PnlJournalSupplier.Visible = false;
        PnlJournalBank.Visible = true;

        pnljournalUM.Visible = false; 
    }
    protected void BtnnextjournalJurnal_Click(object sender,EventArgs e)
    {
        //pnltrn.Visible = true;

        pnlIDS.Visible = false;
        PnlIDB.Visible = false;
        PnlJournalSupplier.Visible = false;
        PnlJournalBank.Visible = false;
        if(ddltypejur.SelectedValue == "1")
        {
            pnltypejurnal.Visible = true;
            pnltrn.Visible = false;
            pnlJournalJurnal.Visible = false;
            Pnlfooter.Visible = true;
        }
        else if(ddltypejur.SelectedValue == "2")
        {
            pnltrn.Visible = false;
            PnlGrid.Visible = true;
            Pnlfooter.Visible = true;
            pnlJournalJurnal.Visible = false;
            lnkAddRP2_Click();
        }
        //lnkAddRP2_Click();
        //lnkViewJDF_Click(); 
        //lnkAddRTP_Click();

    }
    protected void btnCancelJPF_Click (object sender, EventArgs e)
    {
        pnlIDS.Visible = false;
        PnlIDB.Visible = true;
        PnlJournalSupplier.Visible = false;
        PnlJournalBank.Visible = false;
        //pnlJournalJPF.Visible = true;
        pnljournalUM.Visible = true;
        pnltrn.Visible = false;
        Pnlfooter.Visible = false;
        pnlJournalJurnal.Visible = false;
    }
     
    private void lnkAddRTP_Click()
    { 
        int dgcount = GRTP.Rows.Count;
        //if (dgcount > 0)
        //{
        //    dtRP.Columns.Clear();

        //    dtRP = (DataTable)ViewState["ListDetailRTP"];

        //    dtRP.Rows.Clear();

        //    for (int i = 0; i < dgcount; i++)
        //    {
        //        DataRow dr = dtRP.NewRow();

        //        dr["nomor"] = ((Label)GRTP.Rows[i].FindControl("lblNumber")).Text;
        //        dr["TglTrnFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lbltglfaktur")).Text;
        //        dr["NoFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
        //        dr["TypeTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
        //        dr["TglJthTmp"] = ((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text;
        //        dr["Keterangan"] = ((TextBox)GRTP.Rows[i].FindControl("lblketJPF")).Text;
        //        dr["JumlahTrn"] = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lbljmlhtrn")).Text);
        //        dr["KursStd"] = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text);
        //        dtRP.Rows.Add(dr);
        //    }
             
        //    ViewState["ListDetailRTP"] = dtRP;
        //    //var getkdsupplier = ddlkdsup.SelectedValue;
        //    //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        //    //var1[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 15, getkdsupplier, typeof(string));
        //    //var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAFORAP", typeof(string));
        //    //var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //    //ge.ListDataTable = gb.GetDataJustOutPut("CL_Trn_Hutang", var1, "constrmf").Tables[0];

        //    //GRTP.DataSource = ge.ListDataTable;
        //    //GRTP.DataSource = dtRP;
        //    GRTP.DataBind();

        //    GRTP.UseAccessibleHeader = true;
        //    GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //    cssGridRTP();

        //}
        //else
        //{
            SetDetailRTP();
        //}

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
        dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));

        dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));


        dr = dtRP.NewRow();
        dr["nomor"] = 1;
        dr["KodeGL"] = string.Empty;
        dr["NamaGL"] = string.Empty;
        dr["Description"] = string.Empty;
        dr["Costcenter"] = string.Empty;
        dr["jlhdebit"] = decimal.Zero;
        dr["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(dr);

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

            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
            dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
            dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
            dr["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
            dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
            dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            dr["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
            dtRP.Rows.Add(dr);
        }

        DataRow drdnew = dtRP.NewRow();
        drdnew["nomor"] = dgcount + 1;
        drdnew["KodeGL"] = string.Empty;
        drdnew["NamaGL"] = string.Empty;
        drdnew["Description"] = string.Empty;
        drdnew["Costcenter"] = string.Empty;
        drdnew["jlhdebit"] = decimal.Zero;
        drdnew["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(drdnew);

        ViewState["ListDetailRP"] = dtRP;

        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();
    }
    protected void ddlKodeGL_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

        ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_SGetCoacode", Params, "constrmf");



        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";


        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlnamaGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[2];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");


        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");

        ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue;



        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        // UpdatePanel1.Update();
    }
    protected void ddlnamaGL_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

        ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_xSGetCoacode", Params, "constrmf");



        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";


        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlKodeGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[2];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");


        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");

        ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue;


        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        // UpdatePanel1.Update();
    }
    protected void GRRP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "DEL":

                int dgcount = GRRP.Rows.Count;


                ((Label)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRP"];

                DataRow drCurrentRow = null;

                dtRP.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRRP.Rows.Count; i++)
                {

                    if (((Label)GRRP.Rows[i].FindControl("lblNumber")).Text != "")
                    {


                        drCurrentRow = dtRP.NewRow();

                        drCurrentRow["nomor"] = j;
                        drCurrentRow["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                        drCurrentRow["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["jlhdebit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                        drCurrentRow["jlhkredit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text;


                        dtRP.Rows.Add(drCurrentRow);

                        j++;
                    }
                }

                if (dgcount == 1)
                {
                    DataRow drdnew = dtRP.NewRow();

                    drdnew["nomor"] = 1;
                    drdnew["KodeGL"] = string.Empty;
                    drdnew["NamaGL"] = string.Empty;
                    drdnew["Description"] = string.Empty;
                    drdnew["Costcenter"] = string.Empty;
                    drdnew["jlhdebit"] = decimal.Zero;
                    drdnew["jlhkredit"] = decimal.Zero;
                    dtRP.Rows.Add(drdnew);
                }


                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();
                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
                cssGridRP();
                break;
        }
    }
    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            Params[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGLCC", typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("dbo.CL_GetCoa", Params, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlnamaGL")), "-", "-");



            DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[2];
            xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            Params[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGLCC", typeof(String));
            gex.ListDataset = gbx.GetDataJustOutPut("dbo.CL_GetCoa", xParams, "constrmf");
            gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");


            DefaultClass.Global.SqlHelper[] xParamsCC = new DefaultClass.Global.SqlHelper[2];
            xParamsCC[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            xParamsCC[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGLCC", typeof(String));
            gex.ListDataset = gbx.GetDataJustOutPut("dbo.CL_GetCoa", xParamsCC, "constrmf");
            gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "-", "-");




            ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;
            ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlnamaGL")).Value;

            ((DropDownList)e.Row.FindControl("ddlcostcenter")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlcostcenter")).Value;



            if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
            {
                ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

            }

            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
    }
    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        if (decimal.Parse(((TextBox)grow.FindControl("jlhdebit")).Text) > 0)
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

    protected void XXXTxt02_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        if (decimal.Parse(((TextBox)grow.FindControl("jlhkredit")).Text) > 0)
        {

            ((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
        }
        else
        {

            ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
        }

    }

    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DEL":
                int dgcount = GRRP.Rows.Count;


                ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRTP"];

                DataRow drCurrentRow = null;

                dtRP.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {

                    if (((Label)GRRP.Rows[i].FindControl("lblNumber")).Text != "")
                    {


                        drCurrentRow = dtRP.NewRow();

                        drCurrentRow["nomor"] = j;
                        drCurrentRow["TglTrnFaktur"] = ((TextBox)GRRP.Rows[i].FindControl("lbltglfaktur")).Text;
                        drCurrentRow["NoFaktur"] = ((TextBox)GRRP.Rows[i].FindControl("lblnofaktur")).Text;
                        drCurrentRow["TypeTrn"] = ((TextBox)GRRP.Rows[i].FindControl("lbltyptrn")).Text;
                        drCurrentRow["TglJthTmp"] = ((TextBox)GRRP.Rows[i].FindControl("lbltgljthtmp")).Text;
                        drCurrentRow["Keterangan"] = ((TextBox)GRRP.Rows[i].FindControl("lblketJPF")).Text;
                        drCurrentRow["JumlahTrn"] = ((TextBox)GRRP.Rows[i].FindControl("lbljmlhtrn")).Text;
                        drCurrentRow["KursStd"] = ((TextBox)GRRP.Rows[i].FindControl("lblkurstrngrtp")).Text;

                        dtRP.Rows.Add(drCurrentRow);

                        j++;
                    }
                }

                if (dgcount == 1)
                {
                    DataRow drdnew = dtRP.NewRow();

                    drdnew["nomor"] = 1;
                    drdnew["TglTrnFaktur"] = string.Empty;
                    drdnew["NoFaktur"] = string.Empty;
                    drdnew["TypeTrn"] = string.Empty;
                    drdnew["TglJthTmp"] = string.Empty;
                    drdnew["Keterangan"] = string.Empty;
                    drdnew["JumlahTrn"] = decimal.Zero;
                    drdnew["KursStd"] = decimal.Zero;
                    dtRP.Rows.Add(drdnew);
                }


                ViewState["ListDetailRTP"] = dtRP;
                 
                GRTP.DataSource = dtRP;
                GRTP.DataBind();

                GRTP.UseAccessibleHeader = true;
                GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRTP();



                break;


        }
    } 
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //var getkdsupplier = ddlkdsup.SelectedValue;
            //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
            //var1[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 15, getkdsupplier, typeof(string));
            //var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAFORAP", typeof(string));
            //var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var1, "constrmf");

            //((Label)e.Row.FindControl("lbltglfaktur")).Text = ge.ListDataset.Tables[0].Rows[0]["TglTrnFaktur"].ToString();
            //((Label)e.Row.FindControl("lblnofaktur")).Text = ge.ListDataset.Tables[0].Rows[0]["NoFaktur"].ToString();
            //((Label)e.Row.FindControl("lbltyptrn")).Text = ge.ListDataset.Tables[0].Rows[0]["TypeTrn"].ToString();
            //((Label)e.Row.FindControl("lbltgljthtmp")).Text = ge.ListDataset.Tables[0].Rows[0]["TglJthTmp"].ToString();
            //((Label)e.Row.FindControl("lblketJPF")).Text = ge.ListDataset.Tables[0].Rows[0]["Keterangan"].ToString();
            //((Label)e.Row.FindControl("lbljmlhtrn")).Text = ge.ListDataset.Tables[0].Rows[0]["jmlhtrn"].ToString();
            //((Label)e.Row.FindControl("lblkurstrngrtp")).Text = ge.ListDataset.Tables[0].Rows[0]["KursStd"].ToString();
            GRTP.UseAccessibleHeader = true;
            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
    }

    private void cssGridRTP()
    {

        // Attribute to show the Plus Minus Button.
        GRTP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRTP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-class", "expand");
        cells[4].Attributes.Add("data-class", "expand");
        cells[5].Attributes.Add("data-class", "expand");
        cells[6].Attributes.Add("data-class", "expand");
        cells[7].Attributes.Add("data-class", "expand"); 
    }
    private void SetDetailRTP()
    {

        DataRow dr = null;

        dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtRP.Columns.Add(new DataColumn("TglTrnFaktur", typeof(string)));
        dtRP.Columns.Add(new DataColumn("NoFaktur", typeof(string)));
        dtRP.Columns.Add(new DataColumn("TypeTrn", typeof(string)));
        dtRP.Columns.Add(new DataColumn("TglJthTmp", typeof(string)));

        dtRP.Columns.Add(new DataColumn("Keterangan", typeof(string)));
        dtRP.Columns.Add(new DataColumn("jmlhtrn", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("KursStd", typeof(string)));


        dr = dtRP.NewRow();
        //dr["nomor"] = 1;
        dr["TglTrnFaktur"] = string.Empty;
        dr["NoFaktur"] = string.Empty;
        dr["TypeTrn"] = string.Empty;
        dr["TglJthTmp"] = string.Empty;
        dr["Keterangan"] = string.Empty;
        dr["jmlhtrn"] = decimal.Zero; 
        dr["KursStd"] = decimal.Zero;

        dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRTP"] = dtRP;

        var getkdsupplier = ddlkdsup.SelectedValue;
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 15, getkdsupplier, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAFORAP", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", var1, "constrmf");

        GRTP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //GRTP.DataSource = dtRP;
        GRTP.DataBind();

        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRTP();
    }
    protected void ddlJurnal_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Jur = ddlJurnal.SelectedValue;
        //TEXTBOX AUTO CURRENCY
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[3];
        varT[0] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 30, Jur, typeof(string));
        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKETJURNAL", typeof(string));
        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", varT, "constrmf");

        string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["KetJurnal"].ToString();
        string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
        string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
        string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
        string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
        txtJurnal.Text = KetJurnal;
        txtDebit1.Text = DebitGL;
        txtDebit2.Text = DebitCC;
        txtKredit1.Text = KreditGL;
        txtKredit2.Text = KreditCC;

        //GET KETERANGAN GL
        DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[3];
        varGL[0] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 30, DebitGL, typeof(string)); 
        varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
        varGL[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", varGL, "constrmf");
        string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
        txtDebit3.Text = DCC;

        //GET KETERANGAN GL
        DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[3];
        varCC[0] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 30, KreditGL, typeof(string)); 
        varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGLMSTR", typeof(string));
        varCC[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Hutang", varCC, "constrmf");
        string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
        txtKredit3.Text = KGL;
    }
}
