using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Globalization;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Activities.Statements;
using iTextSharp.text.pdf;



public partial class AS_Process_MappingPengajuanAsuransi : System.Web.UI.Page
{
    String nosebelumnya = "", updateno = "", CRno_rekening = "", STATE = "", kdpersh = "", format_no_pk = "",
             formatnumber = "", formatpknumber = "", formatnumberbukti = "", nopksebelumnya = "", CRno_pk = "", updatepkno = "", pokok = "", bunga = "",
             typetrn = "", keterangantrn = "", no_bukti_sebelumnya = "", NoBuktiNew = "", DebitGL = "", KreditGL1 = "", KreditGL2 = "",
             KodeJurnal = "", KreditGL3 = "", KreditGL4 = "", kodebank = "";
    double ttlbunga = 0, sisapk = 0, ttlpokok = 0, pokokbunga = 0, ttlkeluarcl = 0;
    int counter_no_rekening = 0, i = 0, x = 0;

    long no_pk_sekarang = 0, no_pk = 0, nosekarang = 0, no_rekening = 0, no_bukti_sekarang = 0, no_bukti = 0;

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    Global gbx = new DefaultClass.Global();
    GlobalEntities gex = new GlobalEntities();

    DateTime tgl_libur, tgl_realisasi, tgldrop;
    double angsuranperbulan = 0;
    string nonasabah = "", agama = "", usia = "", namafileJaminan = "", fotopathJaminan = "";
    public string fotopathktp = "", tanggal = "", fotopathnpwp = "", fotopathKK = "";

    string fotopathKTP1 = "",
       fotopathKTP2 = "", fotopathKTP3 = "", fotopathKTP4 = "", fotopathKTP5 = "";

    public string SessLoginID
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

    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridEntity();

            pnlHeader.Visible = true;
            Pnlcustomer.Visible = false;
            pnlData.Visible = false;
            pnlAsuransi.Visible = false;
            PnlFront.Visible = true;

            DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[3];
            vart1[0] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodesimulasi.Text, typeof(string));
            vart1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MASTERASURANSI", typeof(string));
            vart1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", vart1, "constrmf");


            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAsuransi asc";

            ddlkodeasuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkodeasuransi.DataTextField = "NamaAsuransi";
            ddlkodeasuransi.DataValueField = "KodeAsuransi";
            ddlkodeasuransi.DataBind();
            ddlkodeasuransi.Items.Insert(0, new ListItem("Pilih Kode Asuransi", "-"));

        }


    }

    public void BindGridEntity()
    {


        DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[4];
        x[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        x[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        x[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWMAPPINGPENGAJUANASURANSI", typeof(string));
        x[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("MappingPengajuanAsuransi", x, "constrmf");


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        Pnlcustomer.Visible = true;
        pnlData.Visible = true;
        pnlAsuransi.Visible = true;
        PnlFront.Visible = false;
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":

                try
                {
                    //string register
                    txtIdPermohonan.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltxtIdPermohonan")).Text;
                    txtNoAnggota.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtNoRekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    txtSumberDana.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamasd")).Text;
                    txtPlafon.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text;
                    txtNama.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamanasabah")).Text;
                    txtkodesimulasi.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesimulasi")).Text;
                    txtusiaasuransi.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblusiasuransi")).Text;
                    txtjangkatahun.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangkatahun")).Text;
                    txtasuransi.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblasuransi")).Text;
                    txtnamaasuransi.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamaasuransi")).Text;
                    txtrateasuransipanggil.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrateasuransivrf")).Text;
                    txtpremiasuransi.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpremigrossasuransi")).Text;
                    txtselisihasuransi.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblselisihasuransivrf")).Text;


                    // Combo MO dan REF Non MO
                    PnlFront.Visible = false;
                    pnlData.Visible = true;
                    pnlAsuransi.Visible = true;
                    pnlHeader.Visible = false;
                    btnSimpan.Visible = true;
                    btnDelete.Visible = false;


                    DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[3];
                    vart1[0] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodesimulasi.Text, typeof(string));
                    vart1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MASTERASURANSI", typeof(string));
                    vart1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", vart1, "constrmf");


                    ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAsuransi asc";

                    ddlkodeasuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlkodeasuransi.DataTextField = "NamaAsuransi";
                    ddlkodeasuransi.DataValueField = "KodeAsuransi";
                    ddlkodeasuransi.DataBind();
                    ddlkodeasuransi.Items.Insert(0, new ListItem("Pilih Kode Asuransi", "-"));
                    ddlkodeasuransi.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeasuransi")).Text;




                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                    
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
                    //string register
                    txtIdPermohonan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltxtIdPermohonan")).Text;
                    txtNoAnggota.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtNoRekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    txtSumberDana.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamasd")).Text;
                    txtPlafon.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text;
                    txtNama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamanasabah")).Text;
                    txtkodesimulasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesimulasi")).Text;
                    txtusiaasuransi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblusiasuransi")).Text;
                    txtjangkatahun.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangkatahun")).Text;
                    txtasuransi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblasuransi")).Text;
                    txtnamaasuransi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamaasuransi")).Text;
                    txtrateasuransipanggil.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrateasuransivrf")).Text;
                    txtpremiasuransi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpremigrossasuransi")).Text;
                    txtselisihasuransi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblselisihasuransivrf")).Text;


                    // Combo MO dan REF Non MO
                    PnlFront.Visible = false;
                    pnlData.Visible = true;
                    pnlAsuransi.Visible = true;
                    pnlHeader.Visible = false;
                    btnSimpan.Visible = false;
                    btnDelete.Visible = true;

                    DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[3];
                    vart1[0] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodesimulasi.Text, typeof(string));
                    vart1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MASTERASURANSI", typeof(string));
                    vart1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", vart1, "constrmf");


                    ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAsuransi asc";

                    ddlkodeasuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlkodeasuransi.DataTextField = "NamaAsuransi";
                    ddlkodeasuransi.DataValueField = "KodeAsuransi";
                    ddlkodeasuransi.DataBind();
                    ddlkodeasuransi.Items.Insert(0, new ListItem("Pilih Kode Asuransi", "-"));
                    ddlkodeasuransi.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeasuransi")).Text;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
        }


    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("UploadDokumentPencairanKreditNasabah.aspx");

    }


    protected string btnafterDelete()
    {

        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }


    protected void ddlkodeasuransi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            double rate = 0;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.VarChar, 50, txtusiaasuransi.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjangkatahun.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodeasuransi.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETRATEASURANSI", typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var, "constrmf");

            if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
            {
                if (ddlkodeasuransi.SelectedValue != "-")
                {

                    txtnamaasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["namaasuransi"].ToString();
                    txtrateasuransipanggil.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    txtpremiasuransi.Text = string.Format("{0:N2}", decimal.Parse(txtPlafon.Text) * decimal.Parse(txtrateasuransipanggil.Text) / 1000);

                    txtselisihasuransi.Text = string.Format("{0:N2}", decimal.Parse(txtasuransi.Text) - decimal.Parse(txtpremiasuransi.Text));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan');", true);

                    txtpremiasuransi.Text = "0";
                    txtrateasuransipanggil.Text = "0";
                    txtnamaasuransi.Text = "";
                    txtselisihasuransi.Text = "0";
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan, periksa kembali inputan anda');", true);

                txtpremiasuransi.Text = "0";
                txtrateasuransipanggil.Text = "0";
                txtnamaasuransi.Text = "";
                txtselisihasuransi.Text = "0";
            }

        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlkodeasuransi.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Mohon pilih asuransi terlebih dahulu','Cek Asuransi','top-right','success','0');", true);

                return;
            }

            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[12];
            var88[0] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[1] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkodeasuransi.SelectedValue, typeof(string));
            var88[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var88[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtIdPermohonan.Text, typeof(string));
            var88[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoAnggota.Text, typeof(string));
            var88[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVEASURANSI", typeof(string));
            var88[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[7] = gb.CreateSPParameter("@namaasuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaasuransi.Text, typeof(string));
            var88[8] = gb.CreateSPParameter("@rateasuransivrf", ParameterDirection.Input, SqlDbType.Float, 0, txtrateasuransipanggil.Text, typeof(string));
            var88[9] = gb.CreateSPParameter("@premigrossasuransi", ParameterDirection.Input, SqlDbType.Money, 0, txtpremiasuransi.Text, typeof(string));
            var88[10] = gb.CreateSPParameter("@selisihasuransivrf", ParameterDirection.Input, SqlDbType.Money, 0, txtselisihasuransi.Text, typeof(string));
            var88[11] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("MappingPengajuanAsuransi", var88, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','Data Berhasil Tersimpan','bottom-center','success','true');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnDelete_Click(object sender,EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[13];
            var88[0] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[1] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkodeasuransi.SelectedValue, typeof(string));
            var88[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var88[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtIdPermohonan.Text, typeof(string));
            var88[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoAnggota.Text, typeof(string));
            var88[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEASURANSI", typeof(string));
            var88[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[7] = gb.CreateSPParameter("@namaasuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaasuransi.Text, typeof(string));
            var88[8] = gb.CreateSPParameter("@rateasuransivrf", ParameterDirection.Input, SqlDbType.Float, 0, txtrateasuransipanggil.Text, typeof(string));
            var88[9] = gb.CreateSPParameter("@premigrossasuransi", ParameterDirection.Input, SqlDbType.Money, 0, txtpremiasuransi.Text, typeof(string));
            var88[10] = gb.CreateSPParameter("@selisihasuransivrf", ParameterDirection.Input, SqlDbType.Money, 0, txtselisihasuransi.Text, typeof(string));
            var88[11] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var88[12] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoRekening.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("MappingPengajuanAsuransi", var88, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Delete Done!','Data Berhasil Tersimpan','bottom-center','success','true');", true);

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessBranchIDx)
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
        Params[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETCUSTOMER", typeof(string));
        Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.MappingPengajuanAsuransi", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

    public void btnSerach_Click(object sender, EventArgs e)
    {

        try
        {
            var kodelgn = hdncustomer.Value;
            string norekening = HDNoRekening.Value;
            string kodecabang = HDNKodeCabang.Value;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SEARCHCUSTOMER", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("MappingPengajuanAsuransi", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                txtIdPermohonan.Text = ge.ListDataset.Tables[0].Rows[0]["idpermohonan"].ToString();
                txtNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString(); ;
                txtNoRekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                txtSumberDana.Text = ge.ListDataset.Tables[0].Rows[0]["namasumberdana"].ToString();
                decimal plafon = Convert.ToDecimal(ge.ListDataset.Tables[0].Rows[0]["plafon"]);
                txtPlafon.Text = plafon.ToString("N2");
                txtNama.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["kodesimulasi"].ToString();
                txtusiaasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["usiaasuransi"].ToString();
                txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["jangka_tahun"].ToString();
                decimal asuransi = Convert.ToDecimal(ge.ListDataset.Tables[0].Rows[0]["asuransirp"]);
                txtasuransi.Text = asuransi.ToString("N2");
                // Combo MO dan REF Non MO
                PnlFront.Visible = false;
                pnlData.Visible = true;
                pnlAsuransi.Visible = true;


                DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[3];
                vart1[0] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodesimulasi.Text, typeof(string));
                vart1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MASTERASURANSI", typeof(string));
                vart1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", vart1, "constrmf");


                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAsuransi asc";

                ddlkodeasuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkodeasuransi.DataTextField = "NamaAsuransi";
                ddlkodeasuransi.DataValueField = "KodeAsuransi";
                ddlkodeasuransi.DataBind();
                ddlkodeasuransi.Items.Insert(0, new ListItem("Pilih Kode Asuransi", "-"));
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "redirect",
            "window.location.href='MappingPengajuanAsuransi.aspx';",
            true
        );

    }


}