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
public partial class CL_Process_MasuktrnSuratperingatan : System.Web.UI.Page
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
    string form = "R7";
    string kodesumber = "AR";
    string KodeKonter = "AR12";
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
                BindGridEntity();

               

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

   
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("AR_SURAT_PERINGATAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "peringatan asc,tgl_jatuh_tempo asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":
            
                break;
            case "SETDEL":
                break;
            case "PERINGATAN":
                try
                {
                    string nobukti,peringatan,kodeproduk;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Lblnorekening")).Text;
                    peringatan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperingatan")).Text;
                    kodeproduk = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblproduk")).Text;
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
                    Paramsc[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PERINGATAN", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@peringatan", ParameterDirection.Input, SqlDbType.Int, 0, peringatan, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    Paramsc[6] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 30, kodeproduk, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Surat_Peringatan_report", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/AR/Report/Report/Rptsuratperingataan.Rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
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
                    }


                }
                break;
            case "PERINGATAN1":
                try
                {
                    string nobukti, peringatan, kodeproduk;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Lblnorekening")).Text;
                    peringatan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperingatan")).Text;
                    kodeproduk = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblproduk")).Text;
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    if (int.Parse(peringatan) >= 1)
                    {
                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
                        Paramsc[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PERINGATAN", typeof(string));
                        Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Paramsc[4] = gb.CreateSPParameter("@peringatan", ParameterDirection.Input, SqlDbType.Int, 0, 1, typeof(string));
                        Paramsc[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                        Paramsc[6] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 30, kodeproduk, typeof(string));

                        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Surat_Peringatan_report", Paramsc, "constrmf").Tables[0];
                        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Surat_Peringatan_report", Paramsc, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows.Count > 0)
                        {


                            rprt.Load(Server.MapPath("~/AR/Report/Report/Rptsuratperingataan.Rpt"));
                            rprt.SetDataSource(ge.ListDataset.Tables[0]);
                            Response.Buffer = false;
                            // Clear the response content and headers

                            //rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                            //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                            //rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            //rprt.SetParameterValue("nama_cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                            //rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                            //rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                            //rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            //rprt.SetParameterValue("Bagian", ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString());



                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                            
                            Response.End();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Surat Peringatan Hanya bisa dicetak untuk peringatan ke 1!');", true);
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
                    }


                }
                break;
            case "PERINGATAN2":
                try
                {
                    string nobukti, peringatan, kodeproduk;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Lblnorekening")).Text;
                    peringatan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperingatan")).Text;
                    kodeproduk = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblproduk")).Text;
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    if (int.Parse(peringatan) >= 2)
                    {
                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
                        Paramsc[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PERINGATAN", typeof(string));
                        Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Paramsc[4] = gb.CreateSPParameter("@peringatan", ParameterDirection.Input, SqlDbType.Int, 0, 2, typeof(string));
                        Paramsc[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                        Paramsc[6] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 30, kodeproduk, typeof(string));

                        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Surat_Peringatan_report", Paramsc, "constrmf").Tables[0];
                        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Surat_Peringatan_report", Paramsc, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows.Count > 0)
                        {


                            rprt.Load(Server.MapPath("~/AR/Report/Report/Rptsuratperingataan.Rpt"));
                            rprt.SetDataSource(ge.ListDataset.Tables[0]);
                            Response.Buffer = false;
                            // Clear the response content and headers
                            //rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                            //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                            //rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            //rprt.SetParameterValue("nama_cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                            //rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                            //rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                            //rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            //rprt.SetParameterValue("Bagian", ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString());



                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Surat Peringatan Hanya bisa dicetak untuk peringatan ke 1!');", true);
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
                    }


                }
                break;
            case "PERINGATAN3":
                try
                {
                    string nobukti, peringatan, kodeproduk;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Lblnorekening")).Text;
                    peringatan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperingatan")).Text;
                    kodeproduk = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblproduk")).Text;
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    if (int.Parse(peringatan) >= 3)
                    {
                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
                        Paramsc[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PERINGATAN", typeof(string));
                        Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Paramsc[4] = gb.CreateSPParameter("@peringatan", ParameterDirection.Input, SqlDbType.Int, 0, 3, typeof(string));
                        Paramsc[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                        Paramsc[6] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 30, kodeproduk, typeof(string));

                        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Surat_Peringatan_report", Paramsc, "constrmf").Tables[0];
                        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Surat_Peringatan_report", Paramsc, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows.Count > 0)
                        {


                            rprt.Load(Server.MapPath("~/AR/Report/Report/Rptsuratperingataan.Rpt"));
                            rprt.SetDataSource(ge.ListDataset.Tables[0]);
                            Response.Buffer = false;
                            // Clear the response content and headers
                            //rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                            //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                            //rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            //rprt.SetParameterValue("nama_cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                            //rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                            //rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                            //rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            //rprt.SetParameterValue("Bagian", ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString());

                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Surat Peringatan Hanya bisa dicetak untuk peringatan ke 1 dan ke 2!');", true);
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
                    }


                }
                break;
            case "TUGAS":
                try
                {
                    string nobukti;
                    string peringatan, kodeproduk; 

                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Lblnorekening")).Text;
                    peringatan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperingatan")).Text;
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    kodeproduk = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblproduk")).Text;

                    if (peringatan=="3")
                    {


                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
                        Paramsc[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TARIK", typeof(string));
                        Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Paramsc[4] = gb.CreateSPParameter("@peringatan", ParameterDirection.Input, SqlDbType.Int, 0, peringatan, typeof(string));
                        Paramsc[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                        Paramsc[6] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 30, kodeproduk, typeof(string));

                        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Surat_Peringatan_report", Paramsc, "constrmf").Tables[0];
                        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Surat_Peringatan_report", Paramsc, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows.Count > 0)
                        {


                            rprt.Load(Server.MapPath("~/AR/Report/Report/Rptsurattarik.Rpt"));
                            rprt.SetDataSource(ge.ListDataset.Tables[0]);
                            Response.Buffer = false;
                            //Clear the response content and headers
                            rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                            rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                            rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            rprt.SetParameterValue("nama_cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                            rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                            rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                            rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            rprt.SetParameterValue("Bagian", ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString());

                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Surat Tugas Hanya bisa dicetak untuk peringatan ke 3!');", true);
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
                    }


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
        Response.Redirect(Request.Url.AbsoluteUri);

    }




}
