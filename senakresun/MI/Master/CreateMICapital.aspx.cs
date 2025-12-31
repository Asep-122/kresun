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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;
//using DocumentFormat.OpenXml.InkML;
using System.IdentityModel.Protocols.WSTrust;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Asn1.Tsp;
using System.Activities.Statements;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class MI_CreateMICapital: System.Web.UI.Page
{
   
    //String no_nasabah = "", nosebelumnya = "", updateno = "", CRNonasabah = "", STATE = "", kdpersh = "",
    //       formatnumber = "", msg1 = "", msg2 = "", msg3 = "", msg4 = "", msg5 = "", msg6 = "", msg7 = "", namafileKTP = "", namafileKK = "",
    //       namafileNPWP = "", namafileLainLain1 = "", namafileLainLain2 = "", namafilependukung = "";
    int counter_nonasabah = 0;
    long nosekarang = 0, nonasabah = 0;
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    Global gb1 = new DefaultClass.Global();
    GlobalEntities ge1 = new GlobalEntities();
    Global gb2 = new DefaultClass.Global();
    GlobalEntities ge2 = new GlobalEntities();
    Global gb3 = new DefaultClass.Global();
    GlobalEntities ge3 = new GlobalEntities();

    public string fotopathktp = "", tanggal = "", fotopathnpwp = "", fotopathKK = "";
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value;}
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

    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            //txtnote.Text = CultureName;
            CultureName = "id-ID";
            if (!Page.IsPostBack)
            {
                string ambilbranch = SessBranchID;
                BindGridEntity();
                pnladd.Visible = false;
                InsertCombo();
                pnlHeader.Visible = true;
                PnlFront.Visible = true;

                divtotalrequest.Visible = true;
             
                txttglsimulasi.Text = DateTime.Now.ToString("dd-MMMM-yyyy");

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txtmtpensiun.Text = xtanggal.ToString("dd-MM-yyyy");
                txttglsimulasi.Text = xtanggal.ToString("dd-MM-yyyy");

            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }



    }

    public void BindGridEntityx()
    {
        try
        {
          

        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTSIMULASI", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[3] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_SIMULASI_KREDIT", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoSimulasi asc";


        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void GetAutoNoSi()
    {
        /*        string kode = "";
                bool exists = false;

                while (!exists)
                {
                    kode = "SI-" + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                    var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CheckNoSimulasi", typeof(string));
                    var[1] = gb.CreateSPParameter("@No_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, kode, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_SIMULASI_KREDIT", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count < 1)
                    {
                        exists = true;
                    }

                }

                txtnosimulasi.Text = kode;*/

        DefaultClass.Global.SqlHelper[] nopen = new DefaultClass.Global.SqlHelper[4];
        nopen[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        nopen[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        nopen[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        nopen[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETNOSI", typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", nopen, "constrmf");

        txtnosimulasi.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
    }
    public void BindGridEntity()
    {
        try
        {

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTSIMULASI", typeof(string));
            var[1] = gb.CreateSPParameter("@Kode_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, "B", typeof(string));
            var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("KP_SIMULASI_KREDIT", var, "constrmf");

/*            ge.ListDataset.Tables[0].DefaultView.Sort = "NoSimulasi asc";*/


            GRData.DataSource = ge.ListDataset.Tables[0];
            GRData.DataBind();


            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            //divtotalpending.Visible = true;
            //idjudulpending.Visible = true;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
   
    protected string DataTotal(string kdpersh)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, kdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTTOTAL", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");
        string total = ge.ListDataset.Tables[0].Rows[0]["NoSimulasi"].ToString();
        return total;
    }
    protected string DataMemo(string kdpersh)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, kdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTMEMO", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");
        string total = ge.ListDataset.Tables[0].Rows[0]["NoSimulasi"].ToString();
        return total;
    }
    protected string DataApp(string kdpersh)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, kdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTAPP", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");
        string total = ge.ListDataset.Tables[0].Rows[0]["NoSimulasi"].ToString();
        return total;
    }
    protected string DataRejc(string kdpersh)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, kdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTREJECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");
        string total = ge.ListDataset.Tables[0].Rows[0]["NoSimulasi"].ToString();
        return total;
    }
    protected string DataPending(string kdpersh)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, kdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "COUNTPENDING", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");
        string total = ge.ListDataset.Tables[0].Rows[0]["NoSimulasi"].ToString();
        return total;
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "SET":

                try
                {
                    txtnosimulasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;

                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    Pnlsimulasi.Visible = true;
                    idsimpanvisible.Visible = true;
                    btnrequest.Visible = false;
                    btndelete.Visible = false;


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb2.CreateSPParameter("@No_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnosimulasi.Text, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSIMULASI", typeof(string));
                    var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge2.ListDataset = gb2.GetDataJustOutPut("KP_SIMULASI_KREDIT", var, "constrmf");

                    txtkodesimulasi.Text = ge2.ListDataset.Tables[0].Rows[0]["KodeSimulasi"].ToString();
                    ddlKantorBayar.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KantorBayar"].ToString();
                    ddlsumberdana.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["SumberDana"].ToString();
                    ddlprogrampinjaman.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["ProgramPinjaman"].ToString();
                    ddlkategori.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["JenisKategori"].ToString();
                    ddlinstansi.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["Instansi"].ToString();
                    txtnopen.Text = ge2.ListDataset.Tables[0].Rows[0]["NomorPensiun"].ToString();
                    txtnamapensiun.Text = ge2.ListDataset.Tables[0].Rows[0]["NamaPensiun"].ToString();
                    ddlkodejiwa.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeJiwa"].ToString();
                    ddlstatusnikah.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["StatusNikah"].ToString();
                    ddlpensiun.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["Pensiunan"].ToString();
                    txttgllahir.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglLahir"].ToString()).ToString("dd-MMMM-yyyy");
                    txttgllahiranak1.Text = (!string.IsNullOrEmpty(ge2.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()) ? Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                    txttgllahiranak2.Text = (!string.IsNullOrEmpty(ge2.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()) ? Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                    txtmtpensiun.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TmtPensiun"].ToString()).ToString("dd-MMMM-yyyy");
                    txtgajidisplay.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Gaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtplafon.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Plafon"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpelunasan.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Pelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtjangkatahun.Text = ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktuTahun"].ToString();
//                    txttunjangananak.Text = ge2.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));
                    ddljangkawaktu.Items.Insert(0, new ListItem(ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString(), ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString()));
                    ddlangsurandimuka.Items.Insert(0, new ListItem(ge2.ListDataset.Tables[0].Rows[0]["AngsDimuka"].ToString(), ge2.ListDataset.Tables[0].Rows[0]["AngsDimuka"].ToString()));
//                    txtangsuran.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));


                    
                    InsertCombo();
                    //output
                    txtnama.Text = ge2.ListDataset.Tables[0].Rows[0]["StatusAnggota"].ToString();
                    txtstatuspinjaman.Text = ge2.ListDataset.Tables[0].Rows[0]["StatusPinjaman"].ToString();
                    txtusiapinjaman.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaPinjaman"].ToString();
                    txtusiaasuransi.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaAsuransi"].ToString();
                    txtusiaanak1.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaAnak1"].ToString();
                    txtusiaanak2.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaAnak2"].ToString();
                    txtplafonmax.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtjangkawaktumax.Text = ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktuMax"].ToString();
                    txtrateasuransi.Text = ge2.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString();
                    txtratebungapinjaman.Text = ge2.ListDataset.Tables[0].Rows[0]["RateBungaPinjaman"].ToString();
                    txtjatuhtempotahun.Text = ge2.ListDataset.Tables[0].Rows[0]["JthTempoThn"].ToString();
                    txtjatuhtempobulan.Text = ge2.ListDataset.Tables[0].Rows[0]["JthTempoBln"].ToString();
                    txtangsuran.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtasuransi.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["CicilanAsuransi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", ""))); 
                    txttotal.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Total"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", ""))); 
                    txtsisagaji.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["SisaGaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", ""))); 
                    txtprovisi.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["OutputProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", ""))); 
                    txtkeanggotaan.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Keanggotan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtmaterai.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["OutputMaterai"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtangsuranblokir.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AngsuranDiblokir"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtangsurandimuka.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["OutputAngsuranDimuka"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtbiayaadmin.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["BiayaAdmin"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", ""))); 
                    txtpelunasanRp.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["OutputPelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtterimabersihrp.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["TerimaBersih"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtadminbank.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AdminBank"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));

                    PersenAngsuran();
                    PersenSisaGaji();
                    PersenProvisi();
                    Angsuranblokirkali();
                    Angsurandimukakali();
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;
            case "DEL":
                try
                {


                    txtnosimulasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;

                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    Pnlsimulasi.Visible = true;
                    idsimpanvisible.Visible = true;
                    btnrequest.Visible = false;


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb2.CreateSPParameter("@No_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnosimulasi.Text, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSIMULASI", typeof(string));
                    var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge2.ListDataset = gb2.GetDataJustOutPut("KP_SIMULASI_KREDIT", var, "constrmf");

                    txtkodesimulasi.Text = ge2.ListDataset.Tables[0].Rows[0]["KodeSimulasi"].ToString();
                    ddlKantorBayar.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KantorBayar"].ToString();
                    ddlsumberdana.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["SumberDana"].ToString();
                    ddlprogrampinjaman.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["ProgramPinjaman"].ToString();
                    ddlkategori.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["JenisKategori"].ToString();
                    ddlinstansi.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["Instansi"].ToString();
                    txtnopen.Text = ge2.ListDataset.Tables[0].Rows[0]["NomorPensiun"].ToString();
                    txtnamapensiun.Text = ge2.ListDataset.Tables[0].Rows[0]["NamaPensiun"].ToString();
                    ddlkodejiwa.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeJiwa"].ToString();
                    ddlstatusnikah.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["StatusNikah"].ToString();
                    ddlpensiun.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["Pensiunan"].ToString();
                    txttgllahir.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglLahir"].ToString()).ToString("dd-MMMM-yyyy");
                    txttgllahiranak1.Text = (!string.IsNullOrEmpty(ge2.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()) ? Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                    txttgllahiranak2.Text = (!string.IsNullOrEmpty(ge2.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()) ? Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                    txtmtpensiun.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TmtPensiun"].ToString()).ToString("dd-MMMM-yyyy");
                    txtgajidisplay.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Gaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtplafon.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Plafon"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpelunasan.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Pelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtjangkatahun.Text = ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktuTahun"].ToString();
                    //                    txttunjangananak.Text = ge2.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));
                    ddljangkawaktu.Items.Insert(0, new ListItem(ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString(), ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString()));
                    ddlangsurandimuka.Items.Insert(0, new ListItem(ge2.ListDataset.Tables[0].Rows[0]["AngsDimuka"].ToString(), ge2.ListDataset.Tables[0].Rows[0]["AngsDimuka"].ToString()));
                    //                    txtangsuran.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));



                    InsertCombo();
                    //output
                    txtnama.Text = ge2.ListDataset.Tables[0].Rows[0]["StatusAnggota"].ToString();
                    txtstatuspinjaman.Text = ge2.ListDataset.Tables[0].Rows[0]["StatusPinjaman"].ToString();
                    txtusiapinjaman.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaPinjaman"].ToString();
                    txtusiaasuransi.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaAsuransi"].ToString();
                    txtusiaanak1.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaAnak1"].ToString();
                    txtusiaanak2.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaAnak2"].ToString();
                    txtplafonmax.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtjangkawaktumax.Text = ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktuMax"].ToString();
                    txtrateasuransi.Text = ge2.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString();
                    txtratebungapinjaman.Text = ge2.ListDataset.Tables[0].Rows[0]["RateBungaPinjaman"].ToString();
                    txtjatuhtempotahun.Text = ge2.ListDataset.Tables[0].Rows[0]["JthTempoThn"].ToString();
                    txtjatuhtempobulan.Text = ge2.ListDataset.Tables[0].Rows[0]["JthTempoBln"].ToString();
                    txtangsuran.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtasuransi.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["CicilanAsuransi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txttotal.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Total"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtsisagaji.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["SisaGaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtprovisi.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["OutputProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtkeanggotaan.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Keanggotan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtmaterai.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["OutputMaterai"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtangsuranblokir.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AngsuranDiblokir"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtangsurandimuka.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["OutputAngsuranDimuka"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtbiayaadmin.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["BiayaAdmin"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpelunasanRp.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["OutputPelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtterimabersihrp.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["TerimaBersih"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtadminbank.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AdminBank"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));

                    PersenAngsuran();
                    PersenSisaGaji();
                    PersenProvisi();
                    Angsuranblokirkali();
                    Angsurandimukakali();


/*                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                    var[0] = gb2.CreateSPParameter("@No_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnosimulasi.Text, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETESIMULASI", typeof(string));
                    ge2.ListDataset = gb2.GetDataJustOutPut("KP_SIMULASI_KREDIT", var, "constrmf");

                    if (ge2.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Delete Done!','You successfully save this Simulasi','top-right','success','true');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);
                    }*/


                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

                break;
            case "RptDataNasabah":
                try
                {
                    string NoSimulasi = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb2.CreateSPParameter("@No_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, NoSimulasi, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWDATANASABAH", typeof(string));
                    var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.KP_SIMULASI_KREDIT", var, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/MI/Report/CR/SimulasiDataNasabah.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);

/*                        rprt.SetParameterValue("STATE", "VIEWDATANASABAH");
                        rprt.SetParameterValue("No_Simulasi", NoSimulasi);*/

                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                        Response.End();


                    }
                }
                catch(Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
                    gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;             
            case "RptDataSimulasiCapital":
                try
                {
                    string NoSimulasi = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb2.CreateSPParameter("@No_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, NoSimulasi, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWSIMULASI", typeof(string));
                    var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.KP_SIMULASI_KREDIT", var, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/MI/Report/CR/SimulasiCapital.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);


                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                        Response.End();


                    }
                }
                catch(Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
                    gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;            
            case "RptDataDebitur":
                try
                {
                    string NoSimulasi = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb2.CreateSPParameter("@No_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, NoSimulasi, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWDATADEBITUR", typeof(string));
                    var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[3] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.KP_SIMULASI_KREDIT", var, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/MI/Report/CR/SimulasiDataDebitur.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                        Response.End();


                    }
                }
                catch(Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
                    gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
                break;
        }


    }

    public void SelectedData()
    {

    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
      
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            GetAutoNoSi();
            pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;
            //pnlData.Visible = false;
            Pnlsimulasi.Visible = true;
            idsimpanvisible.Visible = true;
            btndelete.Visible = false;

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnTotal_Click(object sender, EventArgs e)
    {
        try
        {
            //idjudultotal.Visible = false;
            idjudulsemua.Visible = true;
            divtotalrequest.Visible = true;
            //idjudulapp.Visible = false;
            //idjudulpending.Visible = false;
            //idjudulreject.Visible = false;
            //GrdApp.Visible = false;
            //GrdPending.Visible = false;
            //GrdReject.Visible = false;
            GRData.Visible = true;
            BindGridEntity();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected string btnreload()
    {

        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void btnBackHome1_Click(object sender, EventArgs e)
    {
        //pnladd.Visible = true;
        //pnlIdentitas.Visible = false;
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/MI/Master/CreateMI.aspx");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateMI.aspx");
    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text == "")
        {
            BindGridEntity();
        }
        else
        {
            BindGridEntityx();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
         Response.Redirect("CreateMICapital.aspx");
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void txtnopen_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //lblnopenlama.
           
            if (Convert.ToInt32(txtnopen.Text.Length) < 11)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Nomor Pensiun Anda kurang dari 11, silahkan di cek kembali');", true);
            }
            else if(txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@nopen", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnopen.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKNOPENKTP", typeof(string));
                var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtnama.Text = "Anggota Terdaftar";
                    txtstatuspinjaman.Text = "LAMA";
                    txtkeanggotaan.Text = string.Format("{0:N0}", "0");
                    txtnamapensiun.Focus();
                }
                else
                {
                    txtnama.Text = "Calon Anggota";
                    txtstatuspinjaman.Text = "BARU";
                    txtkeanggotaan.Text = string.Format("{0:N0}", 20000);
                    //lblnopenbaru.Visible = true;
                }


                string instansi;
                if (txtnopen.Text.Length == 11 || txtnopen.Text.Length == 15)
                {
                    instansi = "A";
                }
                else if (txtnopen.Text.Length == 12 || txtnopen.Text.Length == 14)
                {
                    instansi = "B";
                }
                else
                {
                    instansi = "NOPEN SALAH";
                }

                ddlinstansi.SelectedValue = instansi;
                ddlinstansi.Enabled = false;
            }
            //fungsiAll();
        }
        catch(Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }
  
    protected void txtusia_OnChange(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(ddlKantorBayar.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(ddlsumberdana.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Sumber Dana terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(ddlprogrampinjaman.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Program Pinjam terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(ddlkategori.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Program Pinjam terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(txtnopen.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi no pensiun terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(ddlkodejiwa.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kode Jiwa terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(ddlstatusnikah.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Status Nikah terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(ddlpensiun.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Pensiun terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            //lblnopenlama.

            if (txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@tgllahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahir.Text, typeof(DateTime));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKUSIAPENSIUNB", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@TglSimulasi", ParameterDirection.Input, SqlDbType.SmallDateTime, 8000, txttglsimulasi.Text, typeof(DateTime));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtusiapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usiapinjaman"].ToString());
                    txtusiaasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usia"].ToString());

                    if(int.Parse(txtusiaasuransi.Text) < 50)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Usia minimal 50 tahun');", true);
                        txttgllahir.Text = string.Empty;
                        txtusiapinjaman.Text = string.Empty;
                        txtusiaasuransi.Text = string.Empty;
                        return;

                    }

                    if (txtkodesimulasi.Text == "B")
                    {

                        
                        DateTime startDate = DateTime.Parse(txttgllahir.Text); // misalnya C15
                        DateTime endDate = DateTime.Parse(txttglsimulasi.Text);   // misalnya G6
                        txtjangkawaktumax.Text = JangkaWaktuMax(startDate, endDate);

                        

                        fungsiAllB();

                    }
                    else
                    {
                        string result;



                        var reguler = TipeReguler(txtusiapinjaman.Text);

                        var platinum = TipePlatinum(txtusiapinjaman.Text);

                        if (int.Parse(ddlproduk.SelectedValue) == 1 || int.Parse(ddlproduk.SelectedValue) == 2)
                        {
                            result = reguler;
                        }
                        else if (int.Parse(ddlkategori.SelectedValue) == 3 || int.Parse(ddlkategori.SelectedValue) == 4)
                        {
                            result = platinum;
                        }
                        else
                        {
                            result = "0";//K93;
                        }

                        txtjangkawaktumax.Text = result;
//                        fungsiAll();

                    }
                }
                else
                {
                    txtusiapinjaman.Text = string.Format("{0:N0}", "0");
                    txtusiaasuransi.Text = string.Format("{0:N0}", "0");
                    txtjangkawaktumax.Text = "0";
                    //lblnopenbaru.Visible = true;
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    protected string JatuhTempoTahun(DateTime startDate, DateTime endDate)
    {
        int monthsToAdd = int.Parse(ddljangkawaktu.SelectedValue == "-" ? "0" : ddljangkawaktu.SelectedValue);                             // C17

        DateTime basedate = endDate.AddMonths(monthsToAdd);

        int yearDiff = basedate.Year - startDate.Year;

        if (basedate.Month < startDate.Month || (basedate.Month == startDate.Month && basedate.Day < startDate.Day))
        {
            yearDiff--;
        }
        return yearDiff.ToString();
    }

    protected string JatuhTempoBulan(DateTime startDate, DateTime endDate)
    {
        int monthsToAdd = int.Parse(ddljangkawaktu.SelectedValue == "-" ? "0" : ddljangkawaktu.SelectedValue);                             // C17

        DateTime basedate = endDate.AddMonths(monthsToAdd);

        int totalMonthDiff = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);

        int monthDiff = totalMonthDiff % 12;

        if (endDate.Day < startDate.Day)
        {
            monthDiff--;
            if (monthDiff < 0) monthDiff += 12;
        }
        return monthDiff.ToString();
    }

    public string JangkaWaktuMax(DateTime startDate, DateTime endDate)
    {
        int monthDiff = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;

        // jika tanggal di endDate lebih kecil dari tanggal di startDate, kurangi 1 bulan
        if (endDate.Day < startDate.Day)
        {
            monthDiff--;
        }

        int L12 = 960 - monthDiff;

        int P16 = 0;
        if (ddlkategori.SelectedValue == "B" && (int.Parse(txtusiaasuransi.Text) < 50 || int.Parse(txtusiaasuransi.Text) > 80))
        {
            P16 = 0;
        }
        else if (ddlkategori.SelectedValue == "A" && (int.Parse(txtusiaasuransi.Text) > 69 || int.Parse(txtusiaasuransi.Text) < 52))
        {
            P16 = 0;
        }
        else if (int.Parse(ddljangkawaktu.SelectedValue == "-" ?"0": ddljangkawaktu.SelectedValue) < 24)
        {
            P16 = 0;
        }
        else
        {
            P16 = L12 > 360 ? 360 : L12;
        }


        int G16 = 0;
        if (ddlpensiun.SelectedValue == "3")
        {
            G16 = 0;
        }
        else if (ddlpensiun.SelectedValue == "1")
        {
            G16 = P16;
        }
        else if (ddlpensiun.SelectedValue == "2" && P16 > 360)
        {
            G16 = 360;
        }
        else
        {
            G16 = P16;
        }

        return string.Format("{0:N0}", G16).ToString();

    }

    public string TipeReguler(string usia)
    {
        var J90 = (75 - decimal.Parse(usia)) * 12;

        var reguler = string.Empty;

        if ((ddlsumberdana.SelectedItem.Text == "BPR Menara mas" || ddlsumberdana.SelectedItem.Text == "BPR Dana Mitra" || ddlsumberdana.SelectedItem.Text == "BPR Ramaganda" || ddlsumberdana.SelectedItem.Text == "BPR CAR" || ddlsumberdana.SelectedItem.Text == "BPR Indomitra") && J90 > 60)
        {
            reguler = "60";
        }
        else if (int.Parse(ddlpensiun.SelectedValue) == 3 && J90 > 60)
        {
            reguler = "60";
        }
        else if (int.Parse(ddlpensiun.SelectedValue) == 2 && J90 > 120)
        {
            reguler = "120";
        }
        else if (J90 < 24 || decimal.Parse(usia) >= 75)
        {
            reguler = "0";
        }
        else
        {
            reguler = J90.ToString();
        }
        return reguler;
    }

    public static int DateDifMD(DateTime startDate, DateTime endDate)
    {
        int dayDiff = endDate.Day - startDate.Day;

        if (dayDiff >= 0)
        {
            return dayDiff;
        }
        else
        {
            // Ambil jumlah hari dari bulan sebelum endDate
            DateTime prevMonth = endDate.AddMonths(-1);
            int daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            return daysInPrevMonth + dayDiff; // dayDiff bernilai negatif
        }
    }

    public static int DateDifY(DateTime startDate, DateTime endDate)
    {
        int dayDiff = endDate.Day - startDate.Day;

        if (dayDiff >= 0)
        {
            return dayDiff;
        }
        else
        {
            DateTime prevMonth = endDate.AddMonths(-1);
            int daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            return daysInPrevMonth + dayDiff; 
        }
    }

    public static int DateDifYM(DateTime startDate, DateTime endDate)
    {
        int monthDiff = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);

        if (endDate.Day < startDate.Day)
        {
            monthDiff--;
        }

        return monthDiff % 12; 
    }



    public string TipePlatinum(string usia)
    {
        string platinum = string.Empty;

        if (ddljangkawaktu.SelectedValue == string.Empty || ddljangkawaktu.SelectedValue == "-")
        {
            platinum = "0";
            return platinum;
        }

        decimal C19 = Math.Ceiling(decimal.Parse(ddljangkawaktu.SelectedValue) / 12);
        decimal C18 = Math.Floor(decimal.Parse(ddljangkawaktu.SelectedValue) / 12);
        decimal E54 = int.Parse(usia) + (12 * C19) - (12 * C18) - (12 * C19 - decimal.Parse(ddljangkawaktu.SelectedValue));
        DateTime D15 = DateTime.Parse(txttgllahir.Text);
        DateTime F5 = DateTime.Now;

        decimal D50 = int.Parse(usia);
        decimal D54 = int.Parse(usia) + C18;
        decimal D53 = (E54 >= 12 ? (int.Parse(usia) + C18 + 1) : D54);
        decimal D75 = 79 * 12;
        decimal D78 = D50 * 12;
        decimal E75 = 9;
        decimal E50 = DateDifYM(D15, F5);
        decimal F50 = DateDifMD(D15, F5);
        Decimal F53 = F50;
        decimal E78 = E50;
        decimal F75 = (F53 > 0 ? D75 + E75 - 1 : D75 + E75);
        decimal F78 = D78 + E78;
        decimal F89 = F75 - F78;
        decimal H88 = (80 * int.Parse(usia)) * 12;

        string[] daftarBPR = {
            "BPR Dana Mitra",
            "BPR Menara mas",
            "BPR Ramaganda",
            "BPR CAR",
            "BPR Indomitra"
        };

        decimal I88 = 0;
        if (H88 > 60 && daftarBPR.Contains(ddlsumberdana.SelectedItem.Text))
        {
            I88 = 60;
        }
        else if (ddlsumberdana.SelectedItem.Text == "BANK MNC" && H88 > 36)
        {
            I88 = H88;
        }
        else if (H88 > 120)
        {
            I88 = 120;
        }
        else if (H88 > 84)
        {
            I88 = 84;
        }
        else
        {
            I88 = H88;
        }
        decimal G89 = (F89 >= I88 ? I88 : F89);
        decimal J89 = (G89 <= 0 ? 0 : G89);


        if (ddlsumberdana.SelectedItem.Text == "BANK MNC" && (int.Parse(ddlpensiun.SelectedValue) == 2 || int.Parse(ddlpensiun.SelectedValue) == 3))
        {
            platinum = (J89 > 120) ? "120" : J89.ToString();
        }
        else if ((ddlsumberdana.SelectedItem.Text == "BPR Menara mas" || ddlsumberdana.SelectedItem.Text == "BPR Dana Mitra") && J89 > 60)
        {
            platinum = "60";
        }
        else if ((int.Parse(ddlpensiun.SelectedValue) == 2 || int.Parse(ddlpensiun.SelectedValue) == 3) && J89 > 60)
        {
            platinum = "60";
        }
        else if (int.Parse(usia) <= 70 && (J89 < 24 || D53 < 75))
        {
            platinum = "0";
        }
        else
        {
            platinum = J89.ToString();
        }


        return platinum;
    }

    protected void txtusia_anak1_OnChange(object sender, EventArgs e)
    {
        try
        {
            //lblnopenlama.

            if (txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@tgllahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak1.Text, typeof(DateTime));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKUSIAPENSIUN", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtusiaanak1.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usia"].ToString());
                   
                }
                else
                {
                    txtusiaanak1.Text = string.Format("{0:N0}", "0");
                   
                    //lblnopenbaru.Visible = true;
                }
            }
            //fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    protected void ddlkantorbayar_SelectedIndexChanged(object sender, EventArgs e)
    {

        DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[5];
        var1xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKantorBayar.SelectedValue, typeof(string));
        var1xx[1] = gb.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar,20, ddlproduk.SelectedValue, typeof(string));
        var1xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LSTJANGKA", typeof(string));
        var1xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var1xx, "constrmf");

/*        txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["kodesimulasi"].ToString();*/

        ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";


        ddljangkawaktu.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljangkawaktu.DataTextField = "jangka";
        ddljangkawaktu.DataValueField = "jangka";
        ddljangkawaktu.DataBind();

        ddljangkawaktu.Items.Insert(0, new ListItem("Pilih Jangka", "-"));



        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTPROVISI", typeof(string));
        var[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodesimulasi.Text, typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

        ddlprovisi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovisi.DataTextField = "provisi";
        ddlprovisi.DataValueField = "provisi";
        ddlprovisi.DataBind();
        ddlprovisi.Items.Insert(0, new ListItem("Pilih Provisi", "0"));

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTMATERAI", typeof(string));
        var2[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var2[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodesimulasi.Text, typeof(string));
        var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var2, "constrmf");

        DataTable dt = ge.ListDataset.Tables[0].Copy();
        dt.Columns.Add("totalmaterai_format", typeof(string));

        foreach (DataRow row in dt.Rows)
        {

                row["totalmaterai_format"] = string.Format("{0:N0}", row["totalmaterai"]);

        }

        ddlmaterai.DataSource = dt;
        ddlmaterai.DataTextField = "totalmaterai_format";
        ddlmaterai.DataValueField = "totalmaterai";
        ddlmaterai.DataBind();
        ddlmaterai.Items.Insert(0, new ListItem("Pilih Materai", "0"));



    }



    protected void ddljangkawaktu_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtkodesimulasi.Text == "B")
            {



                if (ddljangkawaktu.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Anda Belum Memilih Jangka, silahkan di cek kembali');", true);

                }
                else if (ddlinstansi.SelectedValue == "B")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Maaf Instansi Asabri Belum bisa, silahkan di cek kembali');", true);

                }
/*                else if (decimal.Parse(ddljangkawaktu.SelectedValue) > decimal.Parse(txtjangkawaktumax.Text))
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Jangka waktu tidak boleh melebihi jangka waktu max, silahkan di cek kembali');", true);
                }*/

                else
                {
                    if (int.Parse(txtusiaasuransi.Text) < 66)
                    {
                        DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[6];
                        var1xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKantorBayar.SelectedValue, typeof(string));
                        var1xx[1] = gb.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));
                        var1xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKJANGKA", typeof(string));
                        var1xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var1xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var1xx[5] = gb.CreateSPParameter("@Jangkabulan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangkawaktu.SelectedValue), typeof(int));
                        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var1xx, "constrmf");
                    }
                    else
                    {


                        DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[6];
                        var1xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKantorBayar.SelectedValue, typeof(string));
                        var1xx[1] = gb.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 20, "2", typeof(string));
                        var1xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKJANGKA", typeof(string));
                        var1xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var1xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var1xx[5] = gb.CreateSPParameter("@Jangkabulan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangkawaktu.SelectedValue), typeof(int));
                        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var1xx, "constrmf");

                    }

                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtjangkatahun.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["Jangkatahun"].ToString());
                        txtratebungapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());

                    }
                    else
                    {


                        txtjangkatahun.Text = string.Format("{0:N0}", "0");
                        txtratebungapinjaman.Text = string.Format("{0:N0}", "0");


                        //lblnopenbaru.Visible = true;
                    }


                    DateTime startDate = DateTime.Parse(txttgllahir.Text); // misalnya C15
                    DateTime endDate = DateTime.Parse(txttglsimulasi.Text);   // misalnya G6
                    txtjangkawaktumax.Text = JangkaWaktuMax(startDate, endDate);
                    txtjatuhtempotahun.Text = JatuhTempoTahun(startDate, endDate);
                    txtjatuhtempobulan.Text = JatuhTempoBulan(startDate, endDate);

                }



                DefaultClass.Global.SqlHelper[] var2xx = new DefaultClass.Global.SqlHelper[6];
                var2xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodesimulasi.Text, typeof(string));
                var2xx[1] = gb.CreateSPParameter("@UsiaAsuransi", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtusiaasuransi.Text), typeof(int));

                var2xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKASURANSI", typeof(string));
                var2xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var2xx[5] = gb.CreateSPParameter("@Jangkatahun", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtjangkatahun.Text), typeof(int));
                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var2xx, "constrmf");


                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtrateasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                }
                else
                {
                    txtrateasuransi.Text = string.Format("{0:N0}", "0");
                }

                fungsiAllB();

            }
            else
            {
                DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[6];
                var1xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKantorBayar.SelectedValue, typeof(string));
                var1xx[1] = gb.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 20, "2", typeof(string));
                var1xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKJANGKA", typeof(string));
                var1xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var1xx[5] = gb.CreateSPParameter("@Jangkabulan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangkawaktu.SelectedValue), typeof(int));
                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var1xx, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtratebungapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                }
                else
                {
                    txtratebungapinjaman.Text = string.Format("{0:N0}", "0");
                }

                txtjangkatahun.Text = string.Format("{0:N0}", Math.Floor(decimal.Parse(ddljangkawaktu.Text)/12));

                DefaultClass.Global.SqlHelper[] var2xx = new DefaultClass.Global.SqlHelper[6];
                var2xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodesimulasi.Text, typeof(string));
                var2xx[1] = gb.CreateSPParameter("@UsiaAsuransi", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtusiaasuransi.Text), typeof(int));

                var2xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKASURANSI", typeof(string));
                var2xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var2xx[5] = gb.CreateSPParameter("@Jangkatahun", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtjangkatahun.Text), typeof(int));
                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var2xx, "constrmf");


                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtrateasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                }
                else
                {
                    txtrateasuransi.Text = string.Format("{0:N0}", "0");
                }

                fungsiAll();
            }


        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }


    }

    protected void txtusia_anak2_OnChange(object sender, EventArgs e)
    {
        try
        {
            //lblnopenlama.



            if (txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@tgllahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak2.Text, typeof(DateTime));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKUSIAPENSIUN", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtusiaanak2.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usia"].ToString());

                }
                else
                {
                    txtusiaanak2.Text = string.Format("{0:N0}", "0");


                }
            }
            //fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    public void InsertCombo()
    {
        try
        {
            //---Combo Kode Jiwa 
            ge.ListDataset = gb.GetDataFunction("dbo.Getmasterkodejiwa()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_jiwa asc";

            ddlkodejiwa.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkodejiwa.DataTextField = "kdjiwa";
            ddlkodejiwa.DataValueField = "kode_jiwa";
            ddlkodejiwa.DataBind();
            ddlkodejiwa.Items.Insert(0, new ListItem("Pilih Kode Jiwa", ""));

            // --- Combo Sumberdana
            ge.ListDataset = gb.GetDataFunction("dbo.getsumberdana()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "NamaBank asc";

            ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlsumberdana.DataTextField = "NamaBank";
            ddlsumberdana.DataValueField = "KodeBank";
            ddlsumberdana.DataBind();
            ddlsumberdana.Items.Insert(0, new ListItem("Pilih Sumber Dana", ""));


/*            ge.ListDataset = gb.GetDataFunction("dbo.getkantorbayar()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "NamaBank asc";

            ddlKantorBayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlKantorBayar.DataTextField = "NamaBank";
            ddlKantorBayar.DataValueField = "KodeBank";
            ddlKantorBayar.DataBind();
            ddlKantorBayar.Items.Insert(0, new ListItem("Pilih Kantor Bayar", ""));*/


            ge.ListDataset = gb.GetDataFunction("dbo.getProdukGroup()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

            ddlprogrampinjaman.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlprogrampinjaman.DataTextField = "namagroup";
            ddlprogrampinjaman.DataValueField = "kode";
            ddlprogrampinjaman.DataBind();
            ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Program Pinjaman", ""));


            ge.ListDataset = gb.GetDataFunction("dbo.getprogrampinjam()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

            ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlproduk.DataTextField = "keterangan";
            ddlproduk.DataValueField = "kode";
            ddlproduk.DataBind();
            ddlproduk.Items.Insert(0, new ListItem("Pilih Produk Pinjaman", ""));
            ddlproduk.SelectedValue = "1";
            ddlproduk.Enabled = false;


            ge.ListDataset = gb.GetDataFunction("dbo.getprodukkategori()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

            ddlkategori.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkategori.DataTextField = "nama";
            ddlkategori.DataValueField = "kode";
            ddlkategori.DataBind();
            ddlkategori.Items.Insert(0, new ListItem("Pilih Kategori", ""));


            ge.ListDataset = gb.GetDataFunction("dbo.getprodukinstansi()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

            ddlinstansi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlinstansi.DataTextField = "namajenis";
            ddlinstansi.DataValueField = "kode";
            ddlinstansi.DataBind();
            ddlinstansi.Items.Insert(0, new ListItem("Pilih Instansi", ""));
                    


        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected string plafondmax()
    {
        try
        {

            decimal plafon = decimal.Parse(txtplafon.Text);
            decimal hasil = 0;

            if (decimal.Parse(txtplafon.Text) == 0)
            {
                hasil = 0;
                txtplafonmax.Text = string.Format("{0:N0}", hasil);
                return string.Format("{0:N0}", hasil);
            }


            decimal temp_plafon = (decimal.Parse(txtgajidisplay.Text) * (decimal)double.Parse(ddljangkawaktu.SelectedValue)) / ((decimal.Parse(txtratebungapinjaman.Text)/100) * decimal.Parse(ddljangkawaktu.SelectedValue) + 1);
  

            if (plafon < 500000)
            {
                hasil = 0;
            }
            else if ((ddlproduk.SelectedValue == "1" || ddlproduk.SelectedValue == "2") && int.Parse(ddljangkawaktu.SelectedValue) < 24)
            {
                hasil = 0;
            }
            else if ((ddlproduk.SelectedValue == "3" || ddlproduk.SelectedValue == "4") && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3") && temp_plafon > 50000000 && ddlsumberdana.SelectedItem.Text != "BANK MNC")
            {
                hasil = 50000000;
            }
            else if ((ddlsumberdana.SelectedItem.Text == "BPR Menara Mas" || ddlsumberdana.SelectedItem.Text == "BPR Dana Mitra" || ddlsumberdana.SelectedItem.Text == "BPR Ramaganda" || ddlsumberdana.SelectedItem.Text == "BPR Indomitra") && temp_plafon > 100000000)
            {
                hasil = 100000000;
            }
            else if ((ddlproduk.SelectedValue == "1" || ddlproduk.SelectedValue == "2") && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3") && temp_plafon > 100000000)
            {
                hasil = 100000000;
            }
            else if ((ddlproduk.SelectedValue == "1" || ddlproduk.SelectedValue == "2") && temp_plafon > 350000000)
            {
                hasil = 350000000;
            }
            else if ((ddlproduk.SelectedValue == "3" || ddlproduk.SelectedValue == "4") && temp_plafon > 100000000 && int.Parse(txtusiapinjaman.Text) > 73)
            {
                hasil = 100000000;
            }
            else if ((ddlproduk.SelectedValue == "3" || ddlproduk.SelectedValue == "4") && temp_plafon > 150000000)
            {
                hasil  = 150000000;
            }
            else if (ddlproduk.SelectedValue == "5" && temp_plafon > 150000000)
            {
                hasil = 150000000;
            }
            else
            {
                hasil = temp_plafon;
            }
            txtplafonmax.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtplafonmax.Text;
    }

    protected string PlafondmaxB()
    {
        try
        {
            decimal hasil = 0;

            if (decimal.Parse(txtgajidisplay.Text) == 0)
            {
                hasil = 0;
                txtplafonmax.Text = string.Format("{0:N0}", hasil);
                return string.Format("{0:N0}", hasil);
            }
/*            decimal J19 = decimal.Parse(txtasuransi.Text) * decimal.Parse(ddljangkawaktu.Text);
            decimal J18 = J19 / decimal.Parse(txtplafon.Text);
*/
            decimal pembagi = ((decimal.Parse(txtratebungapinjaman.Text)/100) * decimal.Parse(ddljangkawaktu.SelectedValue) + 1);
            if (ddlkategori.SelectedValue == "A" && ((decimal.Parse(txtgajidisplay.Text) * (95/100) * decimal.Parse(ddljangkawaktu.SelectedValue)) / pembagi) > 500000000) 
            {
                hasil = 500000000;
            }
            else
            {
                if (decimal.Parse(txtgajidisplay.Text) * (5m / 100) < 100000)
                {
                    hasil = ((decimal.Parse(txtgajidisplay.Text) - 100000) * decimal.Parse(ddljangkawaktu.SelectedValue)) / pembagi;
                }
                else
                {
                    hasil = (decimal.Parse(txtgajidisplay.Text) * (95m / 100) * decimal.Parse(ddljangkawaktu.SelectedValue)) / pembagi;
                }
            }
            txtplafonmax.Text = string.Format("{0:N0}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtplafonmax.Text;
    }


    public bool fungsiAll()
    {
        try
        {
            asuransi();
            plafondmax();
            angsuran();
            Total();
            SisaGaji();
            Provisi();
            angsuranDiblokir();
            angsuranDimuka();
            BiayaAdmin();
            TerimaBersih();
            PersenAngsuran();
            PersenAsuransi();
            PersenTotal();
            PersenSisaGaji();
            PersenProvisi();
            Angsuranblokirkali();
            Angsurandimukakali();
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return true;
    } 
    
    public bool fungsiAllB()
    {
        try
        {
//            jangkawaktuMaxB();
            asuransiB();
            PlafondmaxB();
            angsuranB();
            AdminBankB();
            Total();
            SisaGaji();
            ProvisiB();
            angsuranDiblokirB();
            angsuranDimukaB();
            angsuranDimukaAsuransiB();
            BiayaAdminB();
            TerimaBersih();
            PersenAngsuran();
//            PersenAsuransi();
            PersenTotal();
            PersenSisaGaji();
            PersenProvisi();
            Angsuranblokirkali();
            Angsurandimukakali();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return true;
    }

    public string angsuran()
    {
        try
        {
            if (decimal.Parse(txtplafon.Text) == 0)
            {
                return txtangsuran.Text;
            }

            decimal hasil = 0; 
            hasil = (decimal.Parse(txtplafon.Text) / decimal.Parse(ddljangkawaktu.Text) + decimal.Parse(txtplafon.Text) * (decimal.Parse(txtratebungapinjaman.Text) /100));
            txtangsuran.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtangsuran.Text;
    }

    public string angsuranB()
    {
        try
        {
            if(decimal.Parse(txtplafon.Text) == 0)
            {
                return txtangsuran.Text;
            }

            string hari = DateTime.Now.DayOfWeek.ToString();
            string hasil = string.Empty;

            if (hari == "SABTU" || hari == "MINGGU" || decimal.Parse(txtplafon.Text) > decimal.Parse(txtplafonmax.Text) + 1)
            {
                hasil = "0";
            }
            else
            {
                hasil = string.Format("{0:N0}", Math.Ceiling(decimal.Parse(txtplafon.Text) * (decimal.Parse(txtratebungapinjaman.Text)/100) + decimal.Parse(txtplafon.Text) / decimal.Parse(ddljangkawaktu.SelectedValue)));
            }
            txtangsuran.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtangsuran.Text;
    }   
    
    public string jangkawaktuMaxB()
    {
        try
        {
            string a = string.Empty;
            string hasil = string.Empty;

            string usia = txtusiapinjaman.Text;
            // Perhitungan maksimal tenor dalam bulan: (80 - usia) * 12, maksimal 300
            decimal tenorMaks = (80 - decimal.Parse(usia)) * 12;
            a = (tenorMaks > 300 ? 300 : tenorMaks).ToString();


            if (ddlpensiun.SelectedItem.Text == "DUDA")
            {
                hasil = "0";
            }

            if (ddlpensiun.SelectedItem.Text == "sendiri")
            {
                hasil = a;

            }

            if (ddlpensiun.SelectedItem.Text == "Janda" && decimal.Parse(a) > 300)
            {
                hasil = "300";
            }

            txtjangkawaktumax.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtjangkawaktumax.Text;
    }

    public string asuransi()
    {
        try
        {
            if (decimal.Parse(txtplafon.Text) == 0)
            {
                return txtasuransi.Text;
            }

            decimal hasil = 0;
            if(decimal.Parse(txtrateasuransi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtplafon.Text) * (decimal.Parse(txtrateasuransi.Text)/100);
            }

            txtasuransi.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtasuransi.Text;

    }

    public string asuransiB()
    {
        try
        {
            if(decimal.Parse(txtplafon.Text) == 0)
            {
                return txtasuransi.Text;
            }
            decimal hasil = 0;
            hasil = decimal.Parse(txtplafon.Text) * (15m / 100);
            txtasuransi.Text = string.Format("{0:N0}", hasil);

            /*            
                decimal nilaiDasar = ((decimal.Parse(txtplafon.Text) * (decimal.Parse(txtrateasuransi.Text)/100) * (decimal.Parse(txtratebungapinjaman.Text)/100) * decimal.Parse(ddljangkawaktu.SelectedValue) / 12) + ((decimal.Parse(txtplafon.Text) * (decimal.Parse(txtrateasuransi.Text) / 100))) / decimal.Parse(ddljangkawaktu.SelectedValue));
                decimal dibulatkan = Math.Round(nilaiDasar, 0, MidpointRounding.AwayFromZero); // ROUND(..., 0)

                if (int.Parse(txtusiaasuransi.Text) < 66 && int.Parse(txtusiaasuransi.Text) + decimal.Parse(txtjangkatahun.Text) > 75)
                {
                    hasil =  dibulatkan * (67m/100);
                }
                else if (int.Parse(txtusiaasuransi.Text) < 66)
                {
                    hasil = dibulatkan / 2;
                }
                else
                {
                    hasil = dibulatkan;
                }
              */

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtasuransi.Text;

    }

    public string Total()
    {
        try
        {
            decimal hasil = 0;
            hasil = decimal.Parse(txtangsuran.Text) + decimal.Parse(txtasuransi.Text);
            txttotal.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txttotal.Text;
    }
    
    public string SisaGaji()
    {
        try
        {
            decimal hasil = 0;
            hasil = decimal.Parse(txtgajidisplay.Text) - decimal.Parse(txtangsuran.Text);
            txtsisagaji.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtsisagaji.Text;
    }    
    public string SisaGajiB()
    {
        try
        {
            decimal hasil = 0;
            hasil = decimal.Parse(txtgajidisplay.Text) - decimal.Parse(txttotal.Text);
            txtsisagaji.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtsisagaji.Text;
    } 
    
    public string Provisi()
    {
        try
        {
            decimal hasil = 0;

            if(decimal.Parse(txtrateasuransi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                if(int.Parse(ddlprovisi.SelectedValue) == 0)
                {
                    hasil = 0;
                }
                else
                {
                    hasil = (decimal.Parse(ddlprovisi.SelectedValue) / 100) * decimal.Parse(txtplafon.Text);
                }
            }

            txtprovisi.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtprovisi.Text;
    }   
    public string ProvisiB()
    {
        try
        {
            if(decimal.Parse(txtplafon.Text) == 0)
            {
                return txtprovisi.Text;
            }

            decimal hasil = 0;

            if(decimal.Parse(txtsisagaji.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                if(int.Parse(txtusiapinjaman.Text) + int.Parse(txtjangkatahun.Text) > 75)
                {
                    hasil = decimal.Parse(txtplafon.Text)*(6m/100);
                }
                else
                {
                    hasil = decimal.Parse(txtplafon.Text) * (5m/100);
                }
            }

            txtprovisi.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtprovisi.Text;
    }    
    public string AdminBankB()
    {
        try
        {
            if(decimal.Parse(txtplafon.Text) == 0)
            {
                return txtadminbank.Text;
            }

            decimal hasil = 0;

            if(decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtplafon.Text) * (1.5m/100);
            }

            txtadminbank.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtadminbank.Text;
    }   
    
    public string angsuranDiblokir()
    {
        try
        {
            decimal hasil = 0;

            if(ddlKantorBayar.SelectedValue == "1" || ddlKantorBayar.SelectedValue == "5" || ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3")
            {
                hasil = 1 * decimal.Parse(txtangsuran.Text);
            }
            else
            {
                hasil = 0;
            }

            txtangsuranblokir.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtangsuranblokir.Text;
    }   

    public string angsuranDiblokirB()
    {
        try
        {
            decimal hasil = 0;

            if(decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                if (ddlkategori.SelectedValue == "B")
                {
                    hasil = decimal.Parse(txtangsuran.Text) * 3;
                }
                else
                {
                    hasil = decimal.Parse(txtangsuran.Text);
                }

            }

            txtangsuranblokir.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtangsuranblokir.Text;
    }   
    
    public string angsuranDimuka()
    {
        try
        {
            decimal hasil = 0;

            if(int.Parse(ddlangsurandimuka.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtangsuran.Text) * int.Parse(ddlangsurandimuka.Text);
            }

            txtangsurandimuka.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtangsurandimuka.Text;
    }    

    public string angsuranDimukaAsuransiB()
    {
        try
        {
             decimal hasil = 0;

            if (string.IsNullOrEmpty(ddlangsurandimuka.Text) || int.Parse(ddlangsurandimuka.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtangsuran.Text) * int.Parse(ddlangsurandimuka.Text);
            }

            txtangdimukaasuransi.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtangsurandimuka.Text;
    }    
    public string angsuranDimukaB()
    {
        try
        {
            decimal hasil = 0;

            if(decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                if(int.Parse(ddlangsurandimuka.SelectedValue) < 1)
                {
                    hasil = 0;
                }
                else
                {
                    hasil = decimal.Parse(txtangsuran.Text) * int.Parse(ddlangsurandimuka.SelectedValue);

                }
            }

            txtangsurandimuka.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtangsurandimuka.Text;
    }    
    
    public string BiayaAdminB()
    {
        try
        {
            decimal hasil = 0;

            if(decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
               if(int.Parse(ddljangkawaktu.SelectedValue) < 120)
                {
                    hasil = 1250000;
                }
                else
                {
                    if(int.Parse(ddljangkawaktu.SelectedValue) > 120 && int.Parse(ddljangkawaktu.SelectedValue) < 180)
                    {
                        hasil = 1450000;
                    }
                    else
                    {
                        if(int.Parse(ddljangkawaktu.SelectedValue) > 180)
                        {
                            hasil = 1750000;
                        } 
                    }
                }
            }

            txtbiayaadmin.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtbiayaadmin.Text;
    }    public string BiayaAdmin()
    {
        try
        {
            decimal hasil = 0;

            if(decimal.Parse(txtrateasuransi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
               if(int.Parse(ddljangkawaktu.SelectedValue) > 120)
                {
                    hasil = 1200000;
                }
                else
                {
                    hasil = 1000000;
                }
            }

            txtbiayaadmin.Text = string.Format("{0:N0}", hasil);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtbiayaadmin.Text;
    }

    public string TerimaBersih()
    {
        try
        {

            decimal hasil = 0;

            if (decimal.Parse(txtasuransi.Text) == 0 || decimal.Parse(txtplafon.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtplafon.Text) - decimal.Parse(txtasuransi.Text) - decimal.Parse(txtadminbank.Text) - decimal.Parse(txtprovisi.Text) - decimal.Parse(txtkeanggotaan.Text) - decimal.Parse(txtangsuranblokir.Text) - decimal.Parse(txtangsurandimuka.Text)- decimal.Parse(txtmaterai.Text) - decimal.Parse(txtbiayaadmin.Text) - decimal.Parse(txtpelunasanRp.Text);
            }

            txtterimabersihrp.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtterimabersihrp.Text;
    }

    public string PersenAngsuran()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txtangsuran.Text) / decimal.Parse(txtgajidisplay.Text))*100;
            }
            txtpsnangsuran.Text = string.Format("{0:N2}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtpsnangsuran.Text;
    }
    public string PersenAsuransi()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtasuransi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txtasuransi.Text) / decimal.Parse(txtgajidisplay.Text))* 100;
            }
            txtpersenasuransi.Text = string.Format("{0:N2}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtpersenasuransi.Text;
    }
    public string PersenTotal()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txttotal.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txttotal.Text) / decimal.Parse(txtgajidisplay.Text)) * 100;
            }
            txttotalpersen.Text = string.Format("{0:N2}", hasil);
                
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txttotalpersen.Text;
    }

    public string PersenSisaGaji()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtsisagaji.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txtsisagaji.Text) / decimal.Parse(txtgajidisplay.Text)) * 100;
            }
            txtsisagajipersen.Text = string.Format("{0:N2}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtsisagajipersen.Text;
    }

    public string PersenProvisi()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtprovisi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txtprovisi.Text) / decimal.Parse(txtplafon.Text)) * 100;
            }
            txtpersenprovisi.Text = string.Format("{0:N2}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtpersenprovisi.Text;
    }

    public string Angsuranblokirkali()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtangsuranblokir.Text) == 0 || decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtangsuranblokir.Text) / decimal.Parse(txtangsuran.Text);
            }
            txtangsuranblokirkali.Text = string.Format("{0:N0}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtangsuranblokirkali.Text;
    }
    public string Angsurandimukakali()
    {
        try
        {

            txtangsurandimukakali.Text = ddlangsurandimuka.Text;
            txtangdimukaasuransikali.Text = ddlangsurandimuka.Text;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtangsurandimukakali.Text;
    }

    public string DdlAngsurandimukaB()
    {
        try
        {

            string hasil = string.Empty;
            ddlangsurandimuka.Items.Clear();

            if (ddlkategori.SelectedItem.Text == "Pra Pensiun")
            {
                DateTime tglTmPensiun = DateTime.Parse(txtmtpensiun.Text);
                DateTime sekarang = DateTime.Now;

                int selisihBulan = ((tglTmPensiun.Year - sekarang.Year) * 12) + (tglTmPensiun.Month - sekarang.Month);

                if (tglTmPensiun.Day < sekarang.Day)
                {
                    selisihBulan--;
                }
                hasil = string.Format("{0:N0}", selisihBulan);

            }
            else
            {
                hasil = "1";
            }

            ddlangsurandimuka.Items.Add(new ListItem(hasil, hasil));
            fungsiAllB();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return ddlangsurandimuka.Text;
    }

    public string DdlAngsurandimuka()
    {
        try
        {
            ddlangsurandimuka.Items.Clear();
            for (int i =1; i <= 10; i++)
            {
                ddlangsurandimuka.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return ddlangsurandimuka.Text;
    }

    public string DdlKodeProduk()
    {
        try
        {
            int usiapinjaman = int.Parse((string.IsNullOrEmpty(txtusiapinjaman.Text) ? "0" : txtusiapinjaman.Text));
            string hasil = string.Empty;

            if (txtkodesimulasi.Text == "A")
            {
                if ((ddlprogrampinjaman.SelectedValue == "A" || ddlprogrampinjaman.SelectedValue == "C") && usiapinjaman < 75)
                {
                    hasil = "1";
                }
                else if (ddlprogrampinjaman.SelectedValue == "B" && usiapinjaman < 75)
                {

                    hasil = "3";
                }
                else if ((ddlprogrampinjaman.SelectedValue == "A" || ddlprogrampinjaman.SelectedValue == "C") && usiapinjaman >= 75)
                {
                    hasil = "2";
                }
                else if (ddlprogrampinjaman.SelectedValue == "B" && usiapinjaman >= 75)
                {

                    hasil = "4";
                }
                else
                {
                    hasil = "";
                }
                ddlproduk.SelectedValue = hasil;
                ddlproduk.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return ddlproduk.Text;
    }

    protected void txtplafon_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddljangkawaktu.Text == "-" || string.IsNullOrEmpty(ddljangkawaktu.Text))
            {
                txtkodesimulasi.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sebelum isi Plafon mohon pilih terlebih dahulu jangka waktu');", true);
                return;
            }

            if (txtkodesimulasi.Text == "A")
            {
                fungsiAll();
            }
            else
            {
                fungsiAllB();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void txtgajidisplay_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddljangkawaktu.Text == "-" || string.IsNullOrEmpty(ddljangkawaktu.Text))
            {
                txtgajidisplay.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sebelum isi Gaji mohon pilih terlebih dahulu jangka waktu');", true);
                return;
            }

            if (txtkodesimulasi.Text == "A")
            {
                fungsiAll();
            }
            else
            {
                fungsiAllB();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void ddlmaterai_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtmaterai.Text = string.Format("{0:N0}", decimal.Parse(ddlmaterai.SelectedValue));
        txtmateraipcs.Text = string.Format("{0:N0}",(decimal.Parse(ddlmaterai.SelectedValue) != 0 ? decimal.Parse(ddlmaterai.SelectedValue) : 0) / 10000);
        if (txtkodesimulasi.Text == "A")
        {
            fungsiAll();
        }
        else
        {
            fungsiAllB();
        }
    }

    protected void ddlprovisi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtkodesimulasi.Text == "A")
        {
            fungsiAll();
        }
        else
        {
            fungsiAllB();
        }
    }

    protected void ddlangsurandimuka_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtkodesimulasi.Text == "A")
        {
            fungsiAll();
        }
        else
        {
            fungsiAllB();
        }
    }

    protected void txtpelunasan_TextChanged(object sender, EventArgs e)
    {
        if (ddljangkawaktu.Text == "-")
        {
            txtpelunasan.Text = "0";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sebelum isi Pelunasan mohon pilih terlebih dahulu jangka waktu');", true);
            return;
        }

        txtpelunasanRp.Text = txtpelunasan.Text;
        if (txtkodesimulasi.Text == "A")
        {
            fungsiAll();
        }
        else
        {
            fungsiAllB();
        }
    }

    protected void txtkodesimulasi_TextChanged(object sender, EventArgs e)
    {



    }


    protected void ddlkategori_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(txtkodesimulasi.Text == "B")
        {
            DdlAngsurandimukaB();
            fungsiAllB();
        }
    }

    protected void ddlkantor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlKantorBayar.SelectedItem.Text == "Bank Capital")
            {
                ddlsumberdana.SelectedValue = ddlKantorBayar.SelectedValue;
                ddlsumberdana.Enabled = false;
                txtkodesimulasi.Text = "B";
                fungsiAllB();
            }
            else
            {
                ddlsumberdana.SelectedValue = "";
                ddlsumberdana.Enabled = true;
                DdlAngsurandimuka();
                txtkodesimulasi.Text = "B";
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void btnrequest_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(ddlKantorBayar.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            else if (string.IsNullOrEmpty(ddlsumberdana.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Sumber Dana terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            else if(string.IsNullOrEmpty(ddlprogrampinjaman.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Program Pinjam terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            else if(string.IsNullOrEmpty(txtnopen.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi no pensiun terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            else if(string.IsNullOrEmpty(ddlkodejiwa.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kode Jiwa terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            else if(string.IsNullOrEmpty(ddlstatusnikah.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Status Nikah terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            else if(string.IsNullOrEmpty(ddlpensiun.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Pensiun terlebih dahulu');", true);
                txttgllahir.Text = string.Empty;
                return;
            }
            else
            {
                object tglLahirAnak1Value = CheckTglNull(txttgllahiranak1.Text);
                object tglLahirAnak2Value = CheckTglNull(txttgllahiranak2.Text);


                string STATE = "INSERT";
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[64];
                var1[0] = gb.CreateSPParameter("@Kode_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodesimulasi.Text, typeof(string));
                var1[1] = gb.CreateSPParameter("@Tgl_Simulasi", ParameterDirection.Input, SqlDbType.DateTime, 50, txttglsimulasi.Text, typeof(DateTime));
                var1[2] = gb.CreateSPParameter("@Kantor_Bayar", ParameterDirection.Input, SqlDbType.VarChar, 100, ddlKantorBayar.Text, typeof(string));
                var1[3] = gb.CreateSPParameter("@Sumber_Dana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.Text, typeof(string));
                var1[4] = gb.CreateSPParameter("@Program_Pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 100, ddlprogrampinjaman.Text, typeof(string));
                var1[5] = gb.CreateSPParameter("@Jenis_Produk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlproduk.Text, typeof(string));
                var1[6] = gb.CreateSPParameter("@Jenis_Kategori", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkategori.Text, typeof(string));
                var1[7] = gb.CreateSPParameter("@Instansi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlinstansi.SelectedValue, typeof(string));
                var1[8] = gb.CreateSPParameter("@Nomor_Pensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnopen.Text, typeof(string));
                var1[9] = gb.CreateSPParameter("@Nama_Pensiun", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamapensiun.Text, typeof(string));
                var1[10] = gb.CreateSPParameter("@Kode_Jiwa", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodejiwa.SelectedValue, typeof(string));
                var1[11] = gb.CreateSPParameter("@Status_Pernikahan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatusnikah.SelectedValue, typeof(string));
                var1[12] = gb.CreateSPParameter("@Pensiunan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpensiun.SelectedValue, typeof(string));
                var1[13] = gb.CreateSPParameter("@TglLahir", ParameterDirection.Input, SqlDbType.DateTime, 10, txttgllahir.Text, typeof(DateTime));
                var1[14] = gb.CreateSPParameter("@TglLahirAnakFirst", ParameterDirection.Input, SqlDbType.DateTime, 10, tglLahirAnak1Value, typeof(DateTime));
                var1[15] = gb.CreateSPParameter("@TglLahirAnakSecond", ParameterDirection.Input, SqlDbType.DateTime, 10, tglLahirAnak2Value, typeof(DateTime));
                var1[16] = gb.CreateSPParameter("@JangkaWaktu", ParameterDirection.Input, SqlDbType.Int, 30, ddljangkawaktu.SelectedValue, typeof(int));
                var1[17] = gb.CreateSPParameter("@JangkaWaktuTahun", ParameterDirection.Input, SqlDbType.Int, 10, txtjangkatahun.Text, typeof(int));
                var1[18] = gb.CreateSPParameter("@TmtPensiun", ParameterDirection.Input, SqlDbType.DateTime, 10, txtmtpensiun.Text, typeof(DateTime));
                var1[19] = gb.CreateSPParameter("@Gaji", ParameterDirection.Input, SqlDbType.Decimal, 0, txtgajidisplay.Text, typeof(decimal));
                var1[20] = gb.CreateSPParameter("@Plafon", ParameterDirection.Input, SqlDbType.Decimal, 250, txtplafon.Text, typeof(decimal));
                var1[21] = gb.CreateSPParameter("@Pelunasan", ParameterDirection.Input, SqlDbType.Decimal, 250, txtpelunasan.Text, typeof(decimal));
                var1[22] = gb.CreateSPParameter("@AngsDimuka", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlangsurandimuka.Text, typeof(string));
                var1[23] = gb.CreateSPParameter("@Materai", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmaterai.Text, typeof(string));
                var1[24] = gb.CreateSPParameter("@Provisi", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlprovisi.Text, typeof(string));
                var1[25] = gb.CreateSPParameter("@MutasiKantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmutasikantorbayar.Text, typeof(string));
                var1[26] = gb.CreateSPParameter("@No_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnosimulasi.Text, typeof(string));
                var1[27] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, STATE, typeof(string));

                var1[28] = gb.CreateSPParameter("@StatusPinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, txtstatuspinjaman.Text, typeof(string));
                var1[29] = gb.CreateSPParameter("@UsiaPinjaman", ParameterDirection.Input, SqlDbType.Int, 0, txtusiapinjaman.Text, typeof(int));
                var1[30] = gb.CreateSPParameter("@UsiaAsuransi", ParameterDirection.Input, SqlDbType.Int, 0, txtusiaasuransi.Text, typeof(int));
                var1[31] = gb.CreateSPParameter("@UsiaAnak1", ParameterDirection.Input, SqlDbType.Int, 0, (string.IsNullOrEmpty(txtusiaanak1.Text) ? null : txtusiaanak1.Text), typeof(int));
                var1[32] = gb.CreateSPParameter("@UsiaAnak2", ParameterDirection.Input, SqlDbType.Int, 0, (string.IsNullOrEmpty(txtusiaanak2.Text) ? null : txtusiaanak2.Text), typeof(int));
                var1[33] = gb.CreateSPParameter("@PlafondMax", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafonmax.Text, typeof(decimal));
                var1[34] = gb.CreateSPParameter("@JangkaWaktuMax", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjangkawaktumax.Text, typeof(decimal));
                var1[35] = gb.CreateSPParameter("@RateAsuransi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtrateasuransi.Text, typeof(decimal));
                var1[36] = gb.CreateSPParameter("@RateBungaPinjaman", ParameterDirection.Input, SqlDbType.Decimal, 0, txtratebungapinjaman.Text, typeof(decimal));
                var1[37] = gb.CreateSPParameter("@Angsuran", ParameterDirection.Input, SqlDbType.Decimal, 0, txtangsuran.Text, typeof(decimal));
                var1[38] = gb.CreateSPParameter("@CicilanAsuransi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtasuransi.Text, typeof(decimal));
                var1[39] = gb.CreateSPParameter("@Total", ParameterDirection.Input, SqlDbType.Decimal, 0, txttotal.Text, typeof(decimal));
                var1[40] = gb.CreateSPParameter("@SisaGaji", ParameterDirection.Input, SqlDbType.Decimal, 0, txtsisagaji.Text, typeof(decimal));
                var1[41] = gb.CreateSPParameter("@Keanggotaan", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkeanggotaan.Text, typeof(decimal));
                var1[42] = gb.CreateSPParameter("@AngsuranDiblokir", ParameterDirection.Input, SqlDbType.Decimal, 0, txtangsuranblokir.Text, typeof(decimal));
                var1[43] = gb.CreateSPParameter("@AngsuranDimuka", ParameterDirection.Input, SqlDbType.Decimal, 0, txtangsurandimuka.Text, typeof(decimal));
                var1[44] = gb.CreateSPParameter("@AngsuranDimukaAsuransi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtangdimukaasuransi.Text, typeof(decimal));
                var1[45] = gb.CreateSPParameter("@BiayaAdmin", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbiayaadmin.Text, typeof(decimal));
                var1[46] = gb.CreateSPParameter("@TerimaBersih", ParameterDirection.Input, SqlDbType.Decimal, 0, txtterimabersihrp.Text, typeof(decimal));
                var1[47] = gb.CreateSPParameter("@OutputMaterai", ParameterDirection.Input, SqlDbType.Decimal, 0, txtmaterai.Text, typeof(decimal));
                var1[48] = gb.CreateSPParameter("@OutputProvisi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtprovisi.Text, typeof(decimal));
                var1[49] = gb.CreateSPParameter("@OutputAdminBank", ParameterDirection.Input, SqlDbType.Decimal, 0, txtadminbank.Text, typeof(decimal));

                var1[50] = gb.CreateSPParameter("@StatusAnggota", ParameterDirection.Input, SqlDbType.VarChar, 0, txtnama.Text, typeof(string));
                var1[51] = gb.CreateSPParameter("@UserEntry", ParameterDirection.Input, SqlDbType.VarChar, 0, SessLoginID, typeof(string));
                var1[52] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.DateTime, 0, DateTime.Now, typeof(DateTime));
                var1[53] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 0, SessBranchID, typeof(string));
                var1[54] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 0, SessKdpersh, typeof(string));
                var1[55] = gb.CreateSPParameter("@JthTempoThn", ParameterDirection.Input, SqlDbType.Int, 0, txtjatuhtempotahun.Text, typeof(int));
                var1[56] = gb.CreateSPParameter("@JthTempoBln", ParameterDirection.Input, SqlDbType.Int, 0, txtjatuhtempobulan.Text, typeof(int));
                var1[57] = gb.CreateSPParameter("@PersenAngsuran", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpsnangsuran.Text, typeof(decimal));
                var1[58] = gb.CreateSPParameter("@PersenSisaGaji", ParameterDirection.Input, SqlDbType.Decimal, 0, txtsisagajipersen.Text, typeof(decimal));
                var1[59] = gb.CreateSPParameter("@PersenProvisi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpersenprovisi.Text, typeof(decimal));
                var1[60] = gb.CreateSPParameter("@PersenAdminBank", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpersenadmminbank.Text, typeof(decimal));
                var1[61] = gb.CreateSPParameter("@PersenAsuransi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpersenasuransi.Text, typeof(decimal));
                var1[62] = gb.CreateSPParameter("@AngsBlokirKali", ParameterDirection.Input, SqlDbType.Int, 0, txtangsuranblokirkali.Text, typeof(int));
                var1[63] = gb.CreateSPParameter("@AngsDimukaKali", ParameterDirection.Input, SqlDbType.Int, 0, txtangsurandimukakali.Text, typeof(int));

                ge.ListDataset = gb.GetDataJustOutPut("KP_SIMULASI_KREDIT", var1, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully save this Simulasi','top-right','success','true');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);

                }
            }

        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString()+ "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    public object CheckTglNull(string ParamTanggal)
    {
        object Tanggal;
        if (string.IsNullOrWhiteSpace(ParamTanggal))
        {
            Tanggal = DBNull.Value;
        }
        else
        {
            DateTime parsedDate;
            if (DateTime.TryParse(ParamTanggal, out parsedDate))
            {
                Tanggal = parsedDate;
            }
            else
            {
                Tanggal = DBNull.Value;
            }
        }

        return Tanggal;
    }

    protected void ddlprogrampinjaman_SelectedIndexChanged(object sender, EventArgs e)
    {
        DdlKodeProduk();
        ddlkantorbayar_SelectedIndexChanged(ddlKantorBayar.SelectedValue, EventArgs.Empty);
        fungsiAllB();
    }

    protected void txtmtpensiun_TextChanged(object sender, EventArgs e)
    {
        DateTime tglTmPensiun = DateTime.Parse(txtmtpensiun.Text);
        DateTime sekarang = DateTime.Now;

        int selisihBulan = ((tglTmPensiun.Year - sekarang.Year) * 12) + (tglTmPensiun.Month - sekarang.Month);

        if(selisihBulan > 72)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','Tmt Pensiun tidak boleh lebih dari 72 bulan','success','true');", true);

        }

        DdlAngsurandimukaB();

    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb2.CreateSPParameter("@No_Simulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnosimulasi.Text, typeof(string));
        var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETESIMULASI", typeof(string));
        ge2.ListDataset = gb2.GetDataJustOutPut("KP_SIMULASI_KREDIT", var, "constrmf");

        if (ge2.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Delete Done!','You successfully save this Simulasi','top-right','success','true');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);
        }
    }
}

