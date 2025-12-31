using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using DefaultClass;
using DocumentFormat.OpenXml.Spreadsheet;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
public partial class AO_PengajuanWOAO : System.Web.UI.Page
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
    string form = "H1";
    string kodesumber = "CL";
    string KodeKonter = "MK07";
    public string nobuktidb;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                //BindGridEntity();
                //DROPDOWNLIST Kode Supplier
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MARKETING", typeof(string));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AO_WO_Ajukan", var1, "constrmf");
                 ge.ListDataset.Tables[0].DefaultView.Sort = "Text asc";

                ddlaolama.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlaolama.DataTextField = "Text";
                ddlaolama.DataValueField = "value";
                ddlaolama.DataBind();
                ddlaolama.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");

                panelPosting.Visible = false;
                pnlFilter.Visible = false;
                PnlFront.Visible = false;
                BindGridEntityT();
            }

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

        }
    }
    protected void ddlmodule_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            BindGridEntity();

       
    }
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
        }
    }



    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {



            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[11];


            Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
            Params[2] = gb.CreateSPParameter("@namalgn", ParameterDirection.Input, SqlDbType.VarChar, 80, "", typeof(string));
            Params[3] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
            Params[4] = gb.CreateSPParameter("@KodeAOLAMA", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            Params[5] = gb.CreateSPParameter("@kodeAOBARU", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            Params[6] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[10] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnotecabang.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AO_WO_Ajukan_Delete_Cabang", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btndelete.Visible = false;


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','2');", true);

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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;

        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";

        Panel1.Visible = false;
        pnlFilter.Visible = false;
        panelPosting.Visible = true;
        pnlFilter.Visible = true;
        PnlFront.Visible = true;
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MARKETING", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AO_WO_Ajukan", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "Text asc";

        ddlaolama.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlaolama.DataTextField = "Text";
        ddlaolama.DataValueField = "value";
        ddlaolama.DataBind();
        ddlaolama.Items.Insert(0, new ListItem("Pilih Data", "-"));

    }

    public void BindGridEntityT()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AO_WO_Ajukan", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti desc";


            GRDT.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRDT.DataBind();

            GRDT.UseAccessibleHeader = true;
            GRDT.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] param = new DefaultClass.Global.SqlHelper[5];
        param[0] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlaolama.SelectedValue, typeof(string));
        param[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSM", typeof(string));
        param[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        param[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        param[4] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AO_WO_Ajukan", param, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_due asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    decimal debet = 0, kredit = 0;
    int datax = 0;
    decimal debetpilihxt = 0, kreditpilihxt = 0, pilihxt = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            debet += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Plafon"));
            kredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));

            //datax = int.Parse(DataBinder.Eval(e.Row.DataItem, "Plafon"));
            //CheckBox ChkBoxRows = (CheckBox)e.Row.FindControl("ckbx");
            //if (ChkBoxRows.Checked == true)
            //{
            //    pilih += 1;
            //    debetpilih += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Plafon"));
            //    kreditpilih += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));

            //}
           

                pilihxt += 1;
                debetpilihxt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Plafon"));
                kreditpilihxt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
               

          
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldebet = (Label)e.Row.FindControl("lbltotalplafon");
            lbltotaldebet.Text = string.Format("{0:N2}", debet);
            Label lbltotalkredit = (Label)e.Row.FindControl("lbltotalsaldoakhir");
            lbltotalkredit.Text = string.Format("{0:N2}", kredit);

            //txtjumlahanggota.Text = string.Format("{0:N0}", pilih);
            //txtjumlahPlafon.Text = string.Format("{0:N2}", debetpilih);
            //txtjumlahsaldoakhir.Text = string.Format("{0:N2}", kreditpilih);
            txtjumlahanggota.Text = string.Format("{0:N0}", pilihxt);
            txtjumlahPlafon.Text = string.Format("{0:N2}", debetpilihxt);
            txtjumlahsaldoakhir.Text = string.Format("{0:N2}", kreditpilihxt);



        }
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
            ReportDocument rprt = new ReportDocument();
            switch (e.CommandName)
            {



                case "SET":

                    try
                    {
                        string nobukti;
                        string typetrn;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                        typetrn = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        //typetrn = "B";
                        string datax = typetrn.Substring(0, 3);
                        //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                        Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        Paramsc[1] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        Paramsc[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, datax, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTPINJAMAN", typeof(string));
                        Paramsc[4] = gb.CreateSPParameter("@databigdata", ParameterDirection.Input, SqlDbType.VarChar, 10, "ALL", typeof(string));
                        Paramsc[5] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, typetrn, typeof(string));
                        //Paramsc[3] = gb.CreateSPParameter("@KodeDeptAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldprtawal.SelectedValue, typeof(string));
                        //Paramsc[4] = gb.CreateSPParameter("@KodeDeptAkhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldprtakhir.SelectedValue, typeof(string));
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_PINJAMAN_ANGGOTA", Paramsc, "constrmf").Tables[0];

                        if (ge.ListDataTable.Rows.Count > 0)
                        {


                            rprt.Load(Server.MapPath("~/AR/Report/Report/STATEMENT/RptRekeningKoranWO.rpt"));
                            rprt.SetDataSource(ge.ListDataTable);
                           


                        //DefaultClass.Global.SqlHelper[] Paramsc1 = new DefaultClass.Global.SqlHelper[6];
                        //Paramsc1[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        //Paramsc1[1] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        //Paramsc1[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, datax, typeof(string));
                        //Paramsc1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTJAMINANMASUK", typeof(string));
                        //Paramsc1[4] = gb.CreateSPParameter("@databigdata", ParameterDirection.Input, SqlDbType.VarChar, 10, "ALL", typeof(string));
                        //Paramsc1[5] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, typetrn, typeof(string));
                        ////Paramsc[3] = gb.CreateSPParameter("@KodeDeptAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldprtawal.SelectedValue, typeof(string));
                        ////Paramsc[4] = gb.CreateSPParameter("@KodeDeptAkhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldprtakhir.SelectedValue, typeof(string));
                        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_PINJAMAN_ANGGOTA", Paramsc1, "constrmf").Tables[0];
                        ////if (ge.ListDataTable.Rows.Count > 0)
                        ////{
                        // rprt.OpenSubreport(Server.MapPath("~/AR/Report/Report/STATEMENT/RptJaaminanMasuk.rpt"));
                        // rprt.Subreports[0].SetDataSource(ge.ListDataTable);


                        //DefaultClass.Global.SqlHelper[] Paramsc2 = new DefaultClass.Global.SqlHelper[6];
                        //Paramsc2[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        //Paramsc2[1] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        //Paramsc2[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, datax, typeof(string));
                        //Paramsc2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAWO", typeof(string));
                        //Paramsc2[4] = gb.CreateSPParameter("@databigdata", ParameterDirection.Input, SqlDbType.VarChar, 10, "ALL", typeof(string));
                        //Paramsc2[5] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, typetrn, typeof(string));
                        ////Paramsc[3] = gb.CreateSPParameter("@KodeDeptAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldprtawal.SelectedValue, typeof(string));
                        ////Paramsc[4] = gb.CreateSPParameter("@KodeDeptAkhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldprtakhir.SelectedValue, typeof(string));
                        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_PINJAMAN_ANGGOTA", Paramsc2, "constrmf").Tables[0];
                        ////if (ge.ListDataTable.Rows.Count > 0)
                        ////{
                        //rprt.OpenSubreport(Server.MapPath("~/AR/Report/Report/STATEMENT/ReportDataTrackhistory.rpt"));
                        //rprt.Subreports[1].SetDataSource(ge.ListDataTable);

                        //}


                        rprt.SetParameterValue("Nama", "WO CABANG");
                        rprt.SetParameterValue("User", SessLoginID);
                        rprt.SetParameterValue("Judul", "KARTU ANGSURAN");


                        //Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            Response.End();

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


        protected void GRDT_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":
                try
                {
                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;

                    btndelete.Visible = false;
                    btnSimpan.Visible = true;
                    txtnobukti.Text = "";

                    Panel1.Visible = false;
                    pnlFilter.Visible = false;
                    panelPosting.Visible = true;
                    pnlFilter.Visible = true;
                    PnlFront.Visible = true;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                    var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MARKETINGALL", typeof(string));
                    var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AO_WO_Ajukan", var1, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "Text asc";

                    ddlaolama.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlaolama.DataTextField = "Text";
                    ddlaolama.DataValueField = "value";
                    ddlaolama.DataBind();
                    ddlaolama.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    txtnobukti.Text = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    //txtnobuktimanual.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobuktimanual")).Text;


                    txttgltrn.Text = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;

                    
                    
                    ddlaolama.SelectedValue = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("LblAOWO")).Text;

                    
                    btnSimpan.Visible = false;



                    txtnotecabang.Text = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketerangan")).Text;

                    txtnotepusat.Text = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblcatapprove")).Text;
                    btndelete.Visible = true;
                    BindGridEntity();


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

                    DefaultClass.Global.SqlHelper[] ParamPosting = new DefaultClass.Global.SqlHelper[4];
                    ParamPosting[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    ParamPosting[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                    ParamPosting[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                    ParamPosting[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKPOSTING", typeof(String));
                    ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", ParamPosting, "constrmf");
                    string pubposting = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                    if (pubposting == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting!');", true);
                        btndelete.Visible = false;
                    }
                    else
                    {
                        btndelete.Visible = true;
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
    
    
    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        PnlFront.Visible = true;
      
        Pnlfooter.Visible = false;
        Response.Redirect("PengajuanWOAO.aspx");

    }
    //protected void btnSimpan_Click(object sender, EventArgs e)
    //{
    //    try
    //    {


    //    if (ddlaolama.SelectedValue == "-")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sales Lama Masih Kosong!');", true);
    //    }
    //    else
    //    {


    //        dtRP.Columns.Clear();
    //        dtRP.Columns.Add(new DataColumn("No", typeof(int)));
    //        dtRP.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
    //            dtRP.Columns.Add(new DataColumn("nonasabah", typeof(string)));
    //            dtRP.Columns.Add(new DataColumn("kodeproduk", typeof(string)));
    //            dtRP.Columns.Add(new DataColumn("catatan", typeof(string)));

    //            dtRP.Rows.Clear();
    //        int xi = 0;

    //        //for (int i = 0; i < GRTP.Rows.Count; i++)
    //        //{
    //        //    DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");


    //        //    if (ddlbayar.SelectedValue != "0")
    //        //    {
    //        //        DataRow dr1 = dtBayar.NewRow();
    //        //        xi = xi + 1;
    //        //        decimal xangske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangs")).Text);
    //        //        decimal xpokok = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblPokok")).Text);
    //        //        decimal xbunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblBunga")).Text);
    //        //        decimal xangsuran = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblAngsuran")).Text);
    //        //        decimal xdenda = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lbldenda")).Text);
    //        //        decimal xbayar = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblbayar")).Text);

    //        //        DateTime tgljthtempo = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text);
    //        //        dr1["No"] = xi;
    //        //        dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
    //        //        dr1["TglJthTmp"] = tgljthtempo;
    //        //        dr1["Produk"] = ((Label)GRTP.Rows[i].FindControl("lblproduk")).Text;
    //        //        dr1["Angske"] = xangske;
    //        //        dr1["Pokok"] = xpokok;
    //        //        dr1["Bunga"] = xbunga;
    //        //        dr1["Angsuran"] = xangsuran;
    //        //        dr1["Denda"] = xdenda;
    //        //        dr1["Bayar"] = xbayar;
    //        //        dr1["TypeBayar"] = ddlbayar.SelectedValue;
    //        //        dtBayar.Rows.Add(dr1);
    //        //    }
    //        //}



    //        foreach (GridViewRow row in GRData.Rows)
    //        {
    //            CheckBox ChkBoxHeader = (CheckBox)row.FindControl("ckbx");


    //            if (ChkBoxHeader.Checked == true)
    //            {




    //              DataRow dr1 = dtRP.NewRow();
    //                xi = xi + 1;

    //                dr1["No"] = xi;
    //                dr1["NomorRekening"] = ((Label)row.FindControl("lblnorekening")).Text;
    //                dr1["nonasabah"] = ((Label)row.FindControl("lblnonasabah")).Text;
    //                dr1["kodeproduk"] = ((Label)row.FindControl("lblProduk")).Text;
    //                dr1["catatan"] = ((TextBox)row.FindControl("txtcatatan")).Text;
    //               dtRP.Rows.Add(dr1);




    //            }
    //        }
    //        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
    //        //Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
    //        Paramsc[0] = gb.CreateSPParameter("@AO_OVERCUP_03", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
    //        Paramsc[1] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
    //        Paramsc[2] = gb.CreateSPParameter("@MarketingLama", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaolama.SelectedValue, typeof(string));
    //        Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //        Paramsc[4] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //        Paramsc[5] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));

    //            ge.ListDataset = gb.GetDataJustOutPut("AR_Over_Cup_XYZ", Paramsc, "constrmf");
    //            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //            {

    //                txtjumlahanggota.Text = string.Format("{0:N0}", 0);
    //                txtjumlahPlafon.Text = string.Format("{0:N2}", 0);
    //                txtjumlahsaldoakhir.Text = string.Format("{0:N2}", 0);
    //                BindGridEntity();
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Over Cup Done');", true);
    //            }

    //            else
    //            {

    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //            }





    //    }

    //    }
    //    catch (Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

    //    }
    //}
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            //decimal jlhdibayar = decimal.Parse(txtbayar.Text);
            btnSimpan.Enabled = false;
            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                btnSimpan.Enabled = true;
            }
            else
            {

                if (txtnotecabang.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Note Cabang Masih Kosong!');", true);
                    btnSimpan.Enabled = true;
                }
                else if(ddlaolama.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('AO Calon Masih Kosong!');", true);
                    btnSimpan.Enabled = true;
                }
                else
                {




                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[11];


                    Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                    Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[2] = gb.CreateSPParameter("@kodeAO", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlaolama.SelectedValue, typeof(string));
                    Params[3] = gb.CreateSPParameter("@bykanggota", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtjumlahanggota.Text), typeof(int));
                    Params[4] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjumlahPlafon.Text, typeof(decimal));
                    Params[5] = gb.CreateSPParameter("@saldoakhir", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjumlahsaldoakhir.Text, typeof(decimal));
                    Params[6] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[10] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnotecabang.Text, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AO_WO_AJUKAN_Insert_Cabang", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;


                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','2');", true);

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
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{


        //    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //    Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    Params[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNMGL", typeof(String));
        //    Params[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


        //    ge.ListDataset = gb.GetDataJustOutPut("dbo.ALL_JURNALPOSTING", Params, "constrmf");
        //    ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //    gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlnamaGL")), "-", "-");



        //    DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
        //    xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    xParams[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKDGL", typeof(String));
        //    xParams[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //    gex.ListDataset = gbx.GetDataJustOutPut("dbo.ALL_JURNALPOSTING", xParams, "constrmf");
        //    gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //    gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");


        //    DefaultClass.Global.SqlHelper[] xParamsCC = new DefaultClass.Global.SqlHelper[3];
        //    xParamsCC[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    xParamsCC[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGLCC", typeof(String));
        //    xParamsCC[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //    gex.ListDataset = gbx.GetDataJustOutPut("dbo.ALL_JURNALPOSTING", xParamsCC, "constrmf");
        //    gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //    gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "-", "-");




        //    ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;
        //    ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlnamaGL")).Value;

        //    ((DropDownList)e.Row.FindControl("ddlcostcenter")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlcostcenter")).Value;



        //    if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
        //    {
        //        ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

        //    }

        //    GRRP.UseAccessibleHeader = true;
        //    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //}
    }

    decimal debetpilih = 0, kreditpilih = 0, pilih = 0;
    protected void ckbx_checked(object sender, EventArgs e)
    {
        

         
        foreach (GridViewRow row in GRData.Rows)
        {

            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbx");
            ChkBoxRows.Checked = true;
            pilih += 1;
            debetpilih += decimal.Parse(((Label)row.FindControl("lblplafon")).Text);
            kreditpilih += decimal.Parse(((Label)row.FindControl("lblsaldoakhir")).Text);

        }
        txtjumlahanggota.Text = string.Format("{0:N0}", pilih);
        txtjumlahPlafon.Text = string.Format("{0:N2}", debetpilih);
        txtjumlahsaldoakhir.Text = string.Format("{0:N2}", kreditpilih);
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void ckbx_unchecked(object sender, EventArgs e)
    {

 

        foreach (GridViewRow row in GRData.Rows)
        {

            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbx");
            ChkBoxRows.Checked = false;

        }
        txtjumlahanggota.Text = string.Format("{0:N0}", 0);
        txtjumlahPlafon.Text = string.Format("{0:N2}", 0);
        txtjumlahsaldoakhir.Text = string.Format("{0:N2}", 0);
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    decimal debetpilihx = 0, kreditpilihx = 0, pilihx = 0;
    protected void chkdtl_checked(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbx");
            if (ChkBoxRows.Checked == true)
            {

                pilihx += 1;
                debetpilihx += decimal.Parse(((Label)row.FindControl("lblplafon")).Text);
                kreditpilihx += decimal.Parse(((Label)row.FindControl("lblsaldoakhir")).Text);
            }



        }
        txtjumlahanggota.Text = string.Format("{0:N0}", pilihx);
        txtjumlahPlafon.Text = string.Format("{0:N2}", debetpilihx);
        txtjumlahsaldoakhir.Text = string.Format("{0:N2}", kreditpilihx);
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //GRData.UseAccessibleHeader = true;
        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


}
