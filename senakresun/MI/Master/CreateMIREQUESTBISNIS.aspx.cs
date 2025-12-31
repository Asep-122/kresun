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

public partial class MI_CreateMIREQUESTBISNIS : System.Web.UI.Page
{
    #region "Declared"
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
    #endregion "Declared"
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
                //txtalamat.Attributes.Add("maxlength", txtalamat.MaxLength.ToString());
                BindGridEntity();
                pnladd.Visible = false;
                //divnemo.Visible = true;
                InsertCombo();
                //GetAutoIdNopen();
                //string masuknopen = HDNopen.Value;
                //pnlFilter.Visible = true;
                //pnlHeader.Visible = true;
                PnlFront.Visible = true;
                //lblnopenlama.Visible = false;
                //lblnopenbaru.Visible = false;
                idtotalreq.Text = DataTotal(SessKdpersh);
                idtotalmemo.Text = DataMemo(SessKdpersh);
                idtotalapp.Text = DataApp(SessKdpersh);
                idtotalreject.Text = DataRejc(SessKdpersh);
                //idtotalpending.InnerText = DataPending(SessKdpersh);
                //idjudultotal.Visible = true;
                divtotalrequest.Visible = true;
                txtbungadeviasi.Text = "0";
                txttglsimulasi.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                //txttglsimulasi.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttittleprovisi.SelectedValue = "1";
                txttittlediblokir.SelectedValue = "1";
                txttittledimuka.SelectedValue = "3";
                txtPLtunjistri.Text = "0";
                txtPLtunjistri1.Text = "0";
                hdntglhariini.Value = DateTime.Now.AddDays(1).ToString("dd-MMMM-yyyy");
                txtgajitunjanak.Text = "0";
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }



    }

    #region "Getnosi/DataDepan/Gridview"
    public void BindGridEntityx()
    {
        try
        {
          

        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        //Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //ge.ListDataset = gb.GetDataJustOutPut("AR_GetNasabah", Params, "constrmf");



        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTWHERE", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[4] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

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

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBISNIS", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
       var[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("SI_Request_CABANG", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoMemoInternal Desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
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
        switch (e.CommandName)
        {

            case "SET":

                try
                {
                    //fungsiAll();

                    //pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlData.Visible = false;

                    //pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnldatapemohon.Visible = true;
                    Pnldepan.Visible = true;
                    //divhasilsimulasi.Visible = true;
                    btncalculate.Visible = false;
                    divsimpan.Visible = true;
                    btnrequest.Visible = false;
                    btnrequest.Visible = false;
                    txtnosimulasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;
                    txtmemointernal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoMemoInternal")).Text;
                    txtnopen.Enabled = false;
                    txtusiaasuransi.Text = "0";
                    //JWdanbungaasuransi();
                    ddlsumberdana.Enabled = false;
                    ddlstatustakeover.Enabled = false;
                    ddlkodeproduk.Enabled = false;
                    ddlpensiun.Enabled = false;
                    ddlstatussk.Enabled = false;
                    ddlstatusnikah.Enabled = false;
                    ddlkodejiwa.Enabled = false;
                    ddljenisdeviasi.Enabled = false;
                    ddljangkawaktu.Enabled = false;
                    txttgllahir.Enabled = false;
                    txtnopen.Enabled = false;
                    txtnamapensiun.Enabled = false;
                    txtgajibersihdisplay.Enabled = false;
                    txtgajipokok.Enabled = false;
                    txttunjangananak.Enabled = false;
                    txtPlafond.Enabled = false;
                   




                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb2.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnosimulasi.Text, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
                    var[2] = gb2.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb2.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    
                    ge2.ListDataset = gb2.GetDataJustOutPut("SI_Request", var, "constrmf");

                    hdntglpk.Value = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglPk"].ToString()).ToString("dd-MMMM-yyyy");
                    ddlkodeproduk.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeProduct"].ToString();
                    ddlsumberdana.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeSumberDana"].ToString();
                    ddlstatustakeover.SelectedItem.Text = ge2.ListDataset.Tables[0].Rows[0]["StatusTakeOver"].ToString();
                    //data
                    txtnopen.Text = ge2.ListDataset.Tables[0].Rows[0]["nopen"].ToString();
                    txtnamapensiun.Text = ge2.ListDataset.Tables[0].Rows[0]["NamaDebitur"].ToString();
                    ddlkodejiwa.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeJiwa"].ToString();
                    ddlstatusnikah.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["Status"].ToString();
                    ddlpensiun.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["StatusPensiun"].ToString();
                    ddlstatussk.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["Status_Sk_diterima"].ToString();
                    txttgllahir.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglLahir"].ToString()).ToString("dd-MMMM-yyyy");
                    txtgajibersihdisplay.Text =string.Format("{0:N2}",  Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["GajiBersihDisplay"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtgajipokok.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["GajiPokok"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    //PL
                    txtPLgajipokok.Text = string.Format("{0:N0}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTGajiPokok"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLgajipokok1.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTGajiPokok1"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPlgajipokok2.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTGajiPokokJumlah"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                   txtPLtunjanak.Text = ge2.ListDataset.Tables[0].Rows[0]["JmlAnak"].ToString();
                                          //string.Format("{0:N0}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["JmlAnak"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", ""))) :
                                          //string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["JmlAnak"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLtunjanak1.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjAnak1"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLtunjanak2.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjAnakJumlah"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLtunjistri.Text = string.Format("{0:N0}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjIstri"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLtunjistri1.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjIstri1"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLtunjistri2.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjIstriJumlah"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLtunjberas.Text = string.Format("{0:N0}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjBeras"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLtunjberas1.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjBeras1"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLtunjberas2.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjBerasJumlah"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtPLterimabersih.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTGajiTunjangan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));

                    txttgllahiranak1.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglAnak1"].ToString()).ToString("dd-MMMM-yyyy");
                    txttgllahiranak2.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglAnak2"].ToString()).ToString("dd-MMMM-yyyy");

                    txttahunanak1.Text = ge2.ListDataset.Tables[0].Rows[0]["TahunAnak1"].ToString();
                    txttahunanak2.Text = ge2.ListDataset.Tables[0].Rows[0]["TahunAnak2"].ToString();


                    txttunjangananak.Text = ge2.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));
                    ddljangkawaktu.Items.Insert(0, new ListItem(ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString(), ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString()));
                    txtjwmax.Text = ge2.ListDataset.Tables[0].Rows[0]["jwmax"].ToString();
                    txtjwtahun.Text = (Convert.ToInt32(ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString()) /12).ToString();
                    txtbungaasuransi.Text = ge2.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString();
                    txtPlafond.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Plafond"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtplafondmax.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtbunga.Text = ge2.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    txtusiaasuransi.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaAsuransi"].ToString(); 
                    txtangsuran.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));

                    txtangsuranmax.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AngsuranMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpelunasantakeover.Text = ge2.ListDataset.Tables[0].Rows[0]["PelunasanTO"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PelunasanTO"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));

                    txttahunpengajuan.Text = ge2.ListDataset.Tables[0].Rows[0]["PengajuanTahun"].ToString();
                    txtbulanpengajuan.Text = ge2.ListDataset.Tables[0].Rows[0]["PengajuanBulan"].ToString();
                    txtharipengajuan.Text = ge2.ListDataset.Tables[0].Rows[0]["PengajuanHari"].ToString();
                    txttahuntempo.Text = ge2.ListDataset.Tables[0].Rows[0]["TempoTahun"].ToString();
                    txtbulantempo.Text = ge2.ListDataset.Tables[0].Rows[0]["TempoBulan"].ToString();
                    txtharitempo.Text = ge2.ListDataset.Tables[0].Rows[0]["TempoHari"].ToString();

                    txtketerangan1.Text = ge2.ListDataset.Tables[0].Rows[0]["Keterangan1"].ToString();
                    txtketerangan2.Text = ge2.ListDataset.Tables[0].Rows[0]["Keterangan2"].ToString();
                    txtketerangan3.Text = ge2.ListDataset.Tables[0].Rows[0]["Keterangan3"].ToString();
                    txtcatatan1.Text = ge2.ListDataset.Tables[0].Rows[0]["Catatan1"].ToString();
                    txtcatatan2.Text = ge2.ListDataset.Tables[0].Rows[0]["Catatan2"].ToString();
                    txtcatatan3.Text = ge2.ListDataset.Tables[0].Rows[0]["Catatan3"].ToString();


                    InsertCombo();
                    ddljenisdeviasi.Items.Insert(0, new ListItem("Pilih Type", ""));
                    ddljenisdeviasi.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["StatusDeviasi"].ToString() == "No" ? "" : ge2.ListDataset.Tables[0].Rows[0]["StatusDeviasi"].ToString();
                    txttittlediblokir.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["DeviasiAngsBlokir"].ToString();
                    txtbungadeviasi.Text =decimal.Parse(ge2.ListDataset.Tables[0].Rows[0]["DeviasiBunga"].ToString()) == 0 ? "" : ge2.ListDataset.Tables[0].Rows[0]["DeviasiBunga"].ToString();
                    txtdeviasiasuransi.Text = decimal.Parse(ge2.ListDataset.Tables[0].Rows[0]["DeviasiAsuransi"].ToString()) == 0 ? "" : ge2.ListDataset.Tables[0].Rows[0]["DeviasiAsuransi"].ToString();
                    txttittleprovisi2.Text = decimal.Parse(ge2.ListDataset.Tables[0].Rows[0]["DeviasiProvisi"].ToString()) == 0 ? "" : ge2.ListDataset.Tables[0].Rows[0]["DeviasiProvisi"].ToString();
                    txttittledimuka.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["DeviasiAngsMuka"].ToString();
                    txtangsblokir.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AngsBlokir"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
                    txtangsdimuka.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AngsDimuka"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
                    txtRinstansi.Text = ge2.ListDataset.Tables[0].Rows[0]["Instansi"].ToString();

                    txtRnamadebitur.Text = txtnamapensiun.Text;
                    txtRto.Text = ddlstatustakeover.SelectedItem.Text;
                    txtRdeviasi.Text = ddljenisdeviasi.SelectedValue.ToString() == "" ? "Tidak Ada" : ddljenisdeviasi.SelectedItem.Text;
                    txtRplafond.Text = txtPlafond.Text;
                    txtRpotongan.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Potongan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
                    txtRasuransi.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Asuransi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
                    txtRprovisi.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Provisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
                    //txtRtitleasuransi.Text = txtdeviasiasuransi.Text == "" ? txtbungaasuransi.Text : txttittleprovisi2.Text;

                    if (txtdeviasiasuransi.Text != "")
                    {
                        txtRtitleasuransi.Text = txtdeviasiasuransi.Text;

                    }
                    else
                    {
                        txtRtitleasuransi.Text = txtdeviasiasuransi.Text == "" ? txtbungaasuransi.Text : txttittleprovisi2.Text;

                    }


                    //txtRtitleasuransi.Text = string.Format("{0:N4}", decimal.Parse(ge2.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString()));
                    //txtRtittleprovisi.Text = txttittleprovisi2.Text == "" ? string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["RateProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")))
                    //                    : txttittleprovisi2.Text;
                    txtRtittleprovisi.Text = txttittleprovisi2.Text == "" ? (ge2.ListDataset.Tables[0].Rows[0]["RateProvisi"].ToString()).ToString()
                    : txttittleprovisi2.Text;
                    txtRkeanggotaan.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Keanggotaan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
                    ddlangsblokir.SelectedValue = txttittlediblokir.SelectedValue;
                    ddlangsdimuka.SelectedValue = txttittledimuka.SelectedValue;
                    txtRangsblokir.Text = txtangsblokir.Text;
                    txtRangsdimuka.Text = txtangsdimuka.Text;
                    txtRpelunasanto.Text = txtpelunasantakeover.Text;
                    
                    txtterimabersih.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["TerimaBersih"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
                    txtsisagaji.Text = string.Format("{0:N2}", Convert.ToInt32(decimal.Parse(txtPLterimabersih.Text) - decimal.Parse(txtangsuran.Text)));
                    ddlkodeproduk.Enabled = false;
                    ddlsumberdana.Enabled = false;
                    txtnamapensiun.Enabled = false;
                    ddlkodejiwa.Enabled = false;
                    ddlstatusnikah.Enabled = false;
                    ddlpensiun.Enabled = false;
                    ddlstatussk.Enabled = false;
                    txttgllahir.Enabled = false;
                    txtgajibersihdisplay.Enabled = false;
                    txtgajipokok.Enabled = false;
                    txttunjangananak.Enabled = false;
                    txtusiaasuransi.Enabled = false;
                    ddljangkawaktu.Enabled = false;
                    txtPlafond.Enabled = false;
                    txtpelunasantakeover.Enabled = false;
                    txtdeviasiasuransi.Enabled = false;
                    txtbungadeviasi.Enabled = false;
                    txtbungadeviasi.Text = ddljenisdeviasi.SelectedValue != "2" ? "" : txtbungadeviasi.Text;
                    txttittleprovisi2.Enabled = false;
                    txttittlediblokir.Enabled = false;
                    txttittledimuka.Enabled = false;
                    //txttittlediblokir.SelectedValue = ddljenisdeviasi.SelectedValue != "1" ? "" : txttittlediblokir.Text;
                    //txttittledimuka.SelectedValue = ddljenisdeviasi.SelectedValue != "5" ? "" : txttittledimuka.Text;
                    txtsisagaji.Text = string.Format("{0:N2}", Convert.ToInt32(decimal.Parse(txtPLterimabersih.Text) - decimal.Parse(txtangsuran.Text)));
                    if (txtdeviasiasuransi.Text == "")
                    {
                        txtdeviasiasuransi.Text = "0";
                    }
                    if (txtbungadeviasi.Text == "")
                    {
                        txtbungadeviasi.Text = "0";
                    }
                    if (txttittleprovisi2.Text == "")
                    {
                        txttittleprovisi2.Text = "0";
                    }

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
                    //fungsiAll();
                    string xstatus= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;


                    if (xstatus != "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data sudah proses Approval tidak bisa di hapus');", true);
                    }
                    else
                    {

                        txtnosimulasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;

                    
                    //JWdanbungaasuransi();
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb2.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnosimulasi.Text, typeof(string));
                    var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var[2] = gb2.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb2.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                    ge2.ListDataset = gb2.GetDataJustOutPut("SI_Request", var, "constrmf");


                    if (ge2.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
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
    #endregion

    #region "Button"
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlsumberdana.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Sumber Dana Tidak Boleh Kosong');", true);
            }
            else if (txtnopen.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Nomor Pensiun Tidak Boleh Kosong');", true);
            }
            else if (Convert.ToInt32(txtgajibersihdisplay.Text.Replace(".", "").Replace(",00", "")) == 0 || txtgajibersihdisplay.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Gaji Bersih Display Tidak Boleh Kosong');", true);
            }
            else if (Convert.ToInt32(txtgajipokok.Text.Replace(".", "").Replace(",00", "")) == 0 || txtgajipokok.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Gaji Pokok Tidak Boleh Kosong');", true);
            }
            else if (Convert.ToInt32(txtPlafond.Text.Replace(".", "").Replace(",00", "")) == 0 || txtPlafond.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Plafond Tidak Boleh Kosong');", true);
            }
            else if (ddljangkawaktu.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Jangka Waktu Tidak Boleh Kosong');", true);
            }
            else if (txttgllahir.Text == "" || txttahunpengajuan.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Tanggal Lahir Tidak Boleh Kosong');", true);
            }
            else if (ddlstatusnikah.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Salah Satu status Anda');", true);
            }
            else if (ddlpensiun.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Salah Satu Status Pensiun Anda');", true);
            }
            else if (ddlstatussk.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Salah Satu Status SK Anda');", true);
            }
            else if (txtnamapensiun.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Masukan Nama Anda sesuai KTP');", true);
            }
            else if (ddlstatustakeover.SelectedValue == "1" && (txtpelunasantakeover.Text == "0" || txtpelunasantakeover.Text == "0,00" || txtpelunasantakeover.Text == ""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pelunasan Take Over Tidak Boleh kosong atau 0');", true);
            }
            else if (txttgllahiranak1.Enabled == true && txttgllahiranak2.Enabled == true && Convert.ToDateTime(txttgllahiranak2.Text) < Convert.ToDateTime(txttgllahiranak1.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Tanggal Lahir anak Tidak Bisa melebihi hari ini atau Tanggal Lahir anak pertama..!!!');", true);
            }
            else if (txtcatatan1.Text == "KODE JIWA SALAH"
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if (ddlstatussk.SelectedValue == "")
            {
                ddlstatussk.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Status SK..!!!');", true);
            }
            else
            {
                //string className = divcalculate.Attributes["class"].ToString();
                //divcalculate.Attributes.Add("class", divcalculate.Attributes["class"].ToString().Replace("form-group col-md-6", "form-group col-md-12"));
                Pnldepan.Visible = false;
                divhasilsimulasi.Visible = true;
                divsimpan.Visible = true;
                bunga();
                fungsiAll();
                txtRnamadebitur.Text = txtnamapensiun.Text;
                //txtRinstansi.Text = txtPensiunan.Text;
                txtRto.Text = ddlstatustakeover.SelectedItem.Text;
                txtRdeviasi.Text = ddljenisdeviasi.SelectedValue.ToString() == "" ? "Tidak Ada" : ddljenisdeviasi.SelectedItem.Text;
                txtRplafond.Text = txtPlafond.Text;
                //(ddljenisdeviasi.SelectedValue == "3" ? asuransi : 0) -
                //        (ddljenisdeviasi.SelectedValue == "4" ? provisi : 0) -
                //        (ddljenisdeviasi.SelectedValue != "" ? keanggotaan : 0) -
                //        (ddljenisdeviasi.SelectedValue == "1" ? angsblokir : 0) -
                //        (ddljenisdeviasi.SelectedValue == "5" ? angsdimuka : 0) -
                //        (ddlstatustakeover.SelectedValue == "1" ? pelunasanTO : 0) 

                int asuransi = txtasuransi.Text == "" ? 0 : Convert.ToInt32(txtasuransi.Text.Replace(".", "").Replace(",00", ""));
                int provisi = txtprovisi.Text == "" ? 0 : Convert.ToInt32(txtprovisi.Text.Replace(".", "").Replace(",00", ""));
                int keanggotaan = lblnopenlama.Visible == true ? 0 : Convert.ToInt32(txtdeviasikeanggotaan.Text.Replace(".", "").Replace(",00", ""));
                int angsblokir = txtangsblokir.Text == "" ? 0 : Convert.ToInt32(txtangsblokir.Text.Replace(".", "").Replace(",00", ""));
                int angsdimuka = txtangsdimuka.Text == "" ? 0 : Convert.ToInt32(txtangsdimuka.Text.Replace(".", "").Replace(",00", ""));
                txtRpotongan.Text = string.Format("{0:N2}", asuransi + provisi + keanggotaan + angsblokir + angsdimuka);
                txtRasuransi.Text = deviasiasuransi();
                txtRprovisi.Text = deviasiprovisi().ToString();
                txtRkeanggotaan.Text = lblnopenbaru.Text == "Nasabah Baru" ? "20.000,00" : "0,00";
                txtRangsblokir.Text = ddljenisdeviasi.SelectedValue != "1" ? "0,00" : txtangsblokir.Text;
                txtRangsdimuka.Text = ddljenisdeviasi.SelectedValue != "5" ? "0,00" : txtangsdimuka.Text;
                txtRpelunasanto.Text = txtpelunasantakeover.Text;
                txtRtittleprovisi.Text = ddljenisdeviasi.SelectedValue != "4" ? HDNProvisi.Value : (txttittleprovisi2.Text == "" ? HDNProvisi.Value : txttittleprovisi2.Text);
                //ddlprovisi.Enabled = true;
                fungsiAll();
                //txtRtittleprovisi.Text = ddljenisdeviasi.SelectedValue != "4" ? HDNProvisi.Value : txttittleprovisi2.Text;

                Pnlhasil.Visible = true;
                pnldatapemohon.Visible = false;
                pnladd.Visible = false;
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            GetAutoNoSi();
            //pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;
            pnlData.Visible = false;
            //pnldatapemohon.Visible = true;
            txtangsuran.Text = "0";
            txtPlafond.Text = "0";
            txtjwmax.Text = "0";
            btnNextpage2.Visible = false;
            Pnldepan.Visible = false;
            //pnlFilter.Visible = false;
            pnldatapemohon.Visible = false;
             Pnldepan.Visible = true;
            
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
   
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (txtnote.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Catatan Masih Kosong..!!!');", true);

            }
           else
            {

           
            DefaultClass.Global.SqlHelper[] varIB = new DefaultClass.Global.SqlHelper[8];        


            varIB[0] = gb1.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            varIB[1] = gb1.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 200, SessBranchID, typeof(string));
            varIB[2] = gb1.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVEBISNIS", typeof(string));
            varIB[3] = gb1.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

           
            varIB[4] = gb1.CreateSPParameter("@NoMemoInternal", ParameterDirection.Input, SqlDbType.VarChar, 30, txtmemointernal.Text, typeof(string));
            varIB[5] = gb1.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            varIB[6] = gb1.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnosimulasi.Text, typeof(string));
                varIB[7] = gb1.CreateSPParameter("@catatan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtnote.Text, typeof(string));

                ge1.ListDataset = gb1.GetDataJustOutPut("SI_Request_CABANG", varIB, "constrmf");

            if (ge1.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                //pnldatapemohon.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge1.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb1.SaveErroLog(ge1.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    protected void BtnTolak_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtnote.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Catatan Masih Kosong..!!!');", true);

            }
            else
            {

                DefaultClass.Global.SqlHelper[] varIB = new DefaultClass.Global.SqlHelper[8];


                varIB[0] = gb1.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                varIB[1] = gb1.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 200, SessBranchID, typeof(string));
                varIB[2] = gb1.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOLAKBISNIS", typeof(string));
                varIB[3] = gb1.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                varIB[4] = gb1.CreateSPParameter("@NoMemoInternal", ParameterDirection.Input, SqlDbType.VarChar, 30, txtmemointernal.Text, typeof(string));
                varIB[5] = gb1.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                varIB[6] = gb1.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnosimulasi.Text, typeof(string));
                varIB[7] = gb1.CreateSPParameter("@catatan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtnote.Text, typeof(string));


                ge1.ListDataset = gb1.GetDataJustOutPut("SI_Request_CABANG", varIB, "constrmf");

                if (ge1.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    //pnldatapemohon.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge1.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb1.SaveErroLog(ge1.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnRequest_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime lahir = Convert.ToDateTime(txttgllahir.Text);
            if (ddljenisdeviasi.SelectedValue == "")
            {

            }
            //INSERT INTO MIBisnis
            DefaultClass.Global.SqlHelper[] varIB = new DefaultClass.Global.SqlHelper[82];
            varIB[0] = gb1.CreateSPParameter("@Nopen", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnopen.Text, typeof(string));
            varIB[1] = gb1.CreateSPParameter("@KodeProduct", ParameterDirection.Input, SqlDbType.VarChar, 16, ddlkodeproduk.SelectedValue, typeof(int));
            varIB[2] = gb1.CreateSPParameter("@TglSimulasi", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglsimulasi.Text, typeof(string));
            varIB[3] = gb1.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlsumberdana.SelectedValue, typeof(string));
            varIB[4] = gb1.CreateSPParameter("@KodeJiwa", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlkodejiwa.Text, typeof(string));
            varIB[5] = gb1.CreateSPParameter("@Status", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlstatusnikah.SelectedValue, typeof(string));
            varIB[6] = gb1.CreateSPParameter("@Instansi", ParameterDirection.Input, SqlDbType.VarChar, 16, txtRinstansi.Text, typeof(string));
            varIB[7] = gb1.CreateSPParameter("@JmlAnak", ParameterDirection.Input, SqlDbType.VarChar, 4, txtPLtunjanak.Text, typeof(string));
            varIB[8] = gb1.CreateSPParameter("@TglAnak1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak1.Text, typeof(string));
            varIB[9] = gb1.CreateSPParameter("@TglAnak2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak2.Text, typeof(string));
            varIB[10] = gb1.CreateSPParameter("@TglAnak3", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak3.Text, typeof(string));
            varIB[11] = gb1.CreateSPParameter("@TglAnak4", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak4.Text, typeof(string));
            varIB[12] = gb1.CreateSPParameter("@GajiBersihDisplay", ParameterDirection.Input, SqlDbType.Money, 0, txtgajibersihdisplay.Text, typeof(string));
            varIB[13] = gb1.CreateSPParameter("@GajiPokok", ParameterDirection.Input, SqlDbType.Money, 0, txtgajipokok.Text, typeof(string));

            varIB[14] = gb1.CreateSPParameter("@JangkaWaktu", ParameterDirection.Input, SqlDbType.VarChar, 3, ddljangkawaktu.SelectedValue, typeof(string));
            varIB[15] = gb1.CreateSPParameter("@Plafond", ParameterDirection.Input, SqlDbType.Money, 0, txtPlafond.Text.Replace(",00",""), typeof(string));
            varIB[16] = gb1.CreateSPParameter("@PlafondMax", ParameterDirection.Input, SqlDbType.Money, 0, txtplafondmax.Text, typeof(string));
            varIB[17] = gb1.CreateSPParameter("@SisaGaji", ParameterDirection.Input, SqlDbType.Money, 0, txtgajitunjanak.Text, typeof(string));
            varIB[18] = gb1.CreateSPParameter("@Bunga", ParameterDirection.Input, SqlDbType.Money, 0, txtbunga.Text, typeof(string));
            varIB[19] = gb1.CreateSPParameter("@Angsuran", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuran.Text, typeof(string));
            varIB[20] = gb1.CreateSPParameter("@TunjAnak", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjanak2.Text, typeof(string));
            varIB[21] = gb1.CreateSPParameter("@TunjIstri", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjistri2.Text, typeof(string));
            varIB[22] = gb1.CreateSPParameter("@TunjBeras", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjberas2.Text, typeof(string));
            varIB[23] = gb1.CreateSPParameter("@Potongan", ParameterDirection.Input, SqlDbType.Money, 0, txtRpotongan.Text, typeof(string));
            varIB[24] = gb1.CreateSPParameter("@Asuransi", ParameterDirection.Input, SqlDbType.Money, 0, txtRasuransi.Text, typeof(string));
            varIB[25] = gb1.CreateSPParameter("@Provisi", ParameterDirection.Input, SqlDbType.Money, 0, txtRprovisi.Text, typeof(string));
            varIB[26] = gb1.CreateSPParameter("@Keanggotaan", ParameterDirection.Input, SqlDbType.Money, 0, txtRkeanggotaan.Text, typeof(string));
            varIB[27] = gb1.CreateSPParameter("@AngsBlokir", ParameterDirection.Input, SqlDbType.Money, 0, txtRangsblokir.Text, typeof(string));

            //INSERT INTO MIdetil
            varIB[28] = gb1.CreateSPParameter("@AngsDimuka", ParameterDirection.Input, SqlDbType.Money, 0, txtRangsdimuka.Text, typeof(string));
            varIB[29] = gb1.CreateSPParameter("@PelunasanTO", ParameterDirection.Input, SqlDbType.Money, 0, txtpelunasantakeover.Text, typeof(string));
            varIB[30] = gb1.CreateSPParameter("@TerimaBersih", ParameterDirection.Input, SqlDbType.Money, 0, txtterimabersih.Text, typeof(string));
            varIB[31] = gb1.CreateSPParameter("@Catatan1", ParameterDirection.Input, SqlDbType.VarChar, 255, txtcatatan1.Text, typeof(string));
            varIB[32] = gb1.CreateSPParameter("@Catatan2", ParameterDirection.Input, SqlDbType.VarChar, 255, txtcatatan2.Text, typeof(string));
            varIB[33] = gb1.CreateSPParameter("@Catatan3", ParameterDirection.Input, SqlDbType.VarChar, 255, txtcatatan3.Text, typeof(string));


            varIB[34] = gb1.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            varIB[35] = gb1.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 200, SessBranchID, typeof(string));
            varIB[36] = gb1.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            varIB[37] = gb1.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            varIB[38] = gb1.CreateSPParameter("@JenisDeviasi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisdeviasi.SelectedValue == "" ? "No" : ddljenisdeviasi.SelectedValue, typeof(string));
            varIB[39] = gb1.CreateSPParameter("@JenisTakeOver", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatustakeover.SelectedItem.Text, typeof(int));
            varIB[40] = gb1.CreateSPParameter("@TglLahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahir.Text, typeof(string));
            varIB[41] = gb1.CreateSPParameter("@PengajuanTahun", ParameterDirection.Input, SqlDbType.VarChar, 4, txttahunpengajuan.Text, typeof(int));
            varIB[42] = gb1.CreateSPParameter("@PengajuanBulan", ParameterDirection.Input, SqlDbType.VarChar, 4, txtbulanpengajuan.Text, typeof(string));
            varIB[43] = gb1.CreateSPParameter("@PengajuanHari", ParameterDirection.Input, SqlDbType.VarChar, 4, txtharipengajuan.Text, typeof(string));
            varIB[44] = gb1.CreateSPParameter("@TempoTahun", ParameterDirection.Input, SqlDbType.VarChar, 4, txttahuntempo.Text, typeof(string));
            varIB[45] = gb1.CreateSPParameter("@TempoBulan", ParameterDirection.Input, SqlDbType.VarChar, 4, txtbulantempo.Text, typeof(string));
            varIB[46] = gb1.CreateSPParameter("@TempoHari", ParameterDirection.Input, SqlDbType.VarChar, 4, txtharitempo.Text, typeof(string));
            varIB[47] = gb1.CreateSPParameter("@Status_Sk_diterima", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatussk.SelectedValue, typeof(string));

            //siqre2
            varIB[48] = gb1.CreateSPParameter("@NamaDebitur", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamapensiun.Text, typeof(string));

            varIB[49] = gb1.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnosimulasi.Text, typeof(string));
            varIB[50] = gb1.CreateSPParameter("@TglPk", ParameterDirection.Input, SqlDbType.DateTime, 30, hdntglpk.Value, typeof(string));
            varIB[51] = gb1.CreateSPParameter("@statuspensiun", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlpensiun.SelectedValue, typeof(string));
            varIB[52] = gb1.CreateSPParameter("@Keterangan1", ParameterDirection.Input, SqlDbType.VarChar, 255, txtketerangan1.Text, typeof(string));
            varIB[53] = gb1.CreateSPParameter("@Keterangan2", ParameterDirection.Input, SqlDbType.VarChar, 255, txtketerangan2.Text, typeof(string));
            varIB[54] = gb1.CreateSPParameter("@Keterangan3", ParameterDirection.Input, SqlDbType.VarChar, 255, txtketerangan3.Text, typeof(string));

            varIB[55] = gb1.CreateSPParameter("@DeviasiBunga", ParameterDirection.Input, SqlDbType.Money, 0, txtbungadeviasi.Text == "" ? 0 : decimal.Parse(txtbungadeviasi.Text), typeof(decimal));
            varIB[56] = gb1.CreateSPParameter("@DeviasiProvisi", ParameterDirection.Input, SqlDbType.Money, 0, txttittleprovisi2.Text == "" ? 0 : decimal.Parse(txttittleprovisi2.Text), typeof(decimal));
            varIB[57] = gb1.CreateSPParameter("@DeviasiAsuransi", ParameterDirection.Input, SqlDbType.Money, 0, txtdeviasiasuransi.Text == "" ? 0 : decimal.Parse(txtdeviasiasuransi.Text), typeof(decimal));

            varIB[58] = gb1.CreateSPParameter("@Rateasuransi", ParameterDirection.Input, SqlDbType.Money, 0, txtbungaasuransi.Text == "" ? 0 : decimal.Parse(txtbungaasuransi.Text), typeof(decimal));
            varIB[59] = gb1.CreateSPParameter("@RateProvisi", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(HDNProvisi.Value), typeof(decimal));
            varIB[60] = gb1.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            varIB[61] = gb1.CreateSPParameter("@ModiftByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            varIB[62] = gb1.CreateSPParameter("@DeviasiAngsBlokir", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlangsblokir.SelectedValue, typeof(string));
            varIB[63] = gb1.CreateSPParameter("@DeviasiAngsMuka", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlangsdimuka.SelectedValue, typeof(string));
            //tambahan

            varIB[64] = gb1.CreateSPParameter("@PTGajiPokok", ParameterDirection.Input, SqlDbType.Money, 0, txtPLgajipokok.Text, typeof(string));
            varIB[65] = gb1.CreateSPParameter("@PTGajiPokok1", ParameterDirection.Input, SqlDbType.Money, 0, txtPLgajipokok1.Text, typeof(string));
            varIB[66] = gb1.CreateSPParameter("@PTGajiPokokJumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtPlgajipokok2.Text, typeof(string));
            varIB[67] = gb1.CreateSPParameter("@PTTunjAnak", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjanak.Text, typeof(string));
            varIB[68] = gb1.CreateSPParameter("@PTTunjAnak1", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjanak1.Text, typeof(string));
            varIB[69] = gb1.CreateSPParameter("@PTTunjAnakJumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjanak2.Text, typeof(string));
            varIB[70] = gb1.CreateSPParameter("@PTTunjIstri", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjistri.Text, typeof(string));
            varIB[71] = gb1.CreateSPParameter("@PTTunjIstri1", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjistri1.Text, typeof(string));
            varIB[72] = gb1.CreateSPParameter("@PTTunjIstriJumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjistri2.Text, typeof(string));
            varIB[73] = gb1.CreateSPParameter("@PTTunjBeras", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjberas.Text, typeof(string));
            varIB[74] = gb1.CreateSPParameter("@PTTunjBeras1", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjberas1.Text, typeof(string));
            varIB[75] = gb1.CreateSPParameter("@PTTunjBerasJumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjberas2.Text, typeof(string));
            varIB[76] = gb1.CreateSPParameter("@PTTGajiTunjangan", ParameterDirection.Input, SqlDbType.Money, 0, txtPLterimabersih.Text, typeof(string));

            varIB[77] = gb1.CreateSPParameter("@TahunAnak1", ParameterDirection.Input, SqlDbType.Int, 0, txttahunanak1.Text, typeof(string));
            varIB[78] = gb1.CreateSPParameter("@TahunAnak2", ParameterDirection.Input, SqlDbType.Int, 0, txttahunanak2.Text, typeof(string));

            varIB[79] = gb1.CreateSPParameter("@JwMax", ParameterDirection.Input, SqlDbType.Int, 0, txtjwmax.Text, typeof(string));
            varIB[80] = gb1.CreateSPParameter("@UsiaAsuransi", ParameterDirection.Input, SqlDbType.Int, 0, txtusiaasuransi.Text, typeof(string));
            varIB[81] = gb1.CreateSPParameter("@AngsuranMax", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranmax.Text, typeof(string));


            ge1.ListDataset = gb1.GetDataJustOutPut("SI_Request", varIB, "constrmf");

            if (ge1.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                //pnldatapemohon.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge1.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb1.SaveErroLog(ge1.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
        }
        catch (System.Exception X)
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

    protected void btnnextpage2_Click(object sender, EventArgs e)
    {


        Pnldepan.Visible = false;
       
        pnldatapemohon.Visible = false;
        pnladd.Visible = false;
        Pnlhasil.Visible = true;
    }
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateMIREQUESTBISNIS.aspx");
    }
    protected void btnBacksave_Click(object sender, EventArgs e)
    {

        Pnlhasil.Visible = false;
        pnldatapemohon.Visible = true;
        pnladd.Visible = true;
        Pnldepan.Visible = true;

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateMIREQUESTBISNIS.aspx");
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
         Response.Redirect("CreateMIREQUESTBISNIS.aspx");
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    #endregion "Button"

    #region "TextChanged"
    protected void txtnopen_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //lblnopenlama.
            lblnopenbaru.Text = "";
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
                    lblnopenbaru.Text = "Nasabah Lama";
                    txtnamapensiun.Focus();
                }
                else
                {
                    lblnopenbaru.Text = "Nasabah Baru";
                    //lblnopenbaru.Visible = true;
                }
            }
            fungsiAll();
        }
        catch(Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }
    protected void txttittleprovisi2_OnChange(object sender, EventArgs e)
    {
        try
        {
            //decimal masuk = decimal.Parse(txttittleprovisi2.Text);
            //if (ddljenisdeviasi.SelectedValue == "4" && Convert.ToInt32(txttittleprovisi2.Text) > Convert.ToInt32(HDNProvisi.Value))
            //{
            //    txttittleprovisi2.Text = HDNProvisi.Value;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Angka provisi harus lebih kecil dari " + txttittleprovisi2.Text + "');", true);
            //}
            //else if (ddljenisdeviasi.SelectedValue != "4" )
            //{
            //    txttittleprovisi2.Text = HDNProvisi.Value;
            //}
            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }
    protected void txttunjangananak_OnChange(object sender, EventArgs e)
    {
        try
        {
            string ambiltunjanak = string.Format("{0:N2}", Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(txttunjangananak.Text))));
            txttunjangananak.Text = ambiltunjanak;
            int tunjanak = Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtPLtunjanak1.Text.Replace(".", "").Replace(",00", "")))).ToString());
            decimal hitungtunj = decimal.Parse(txtPLtunjanak.Text) * decimal.Parse(txttunjangananak.Text);
            txtPLtunjanak2.Text = string.Format("{0:N2}", Convert.ToInt32(txtPLtunjanak.Text == "" ? "0" : Convert.ToString(Math.Ceiling(hitungtunj))));// * Convert.ToInt32(txtPLtunjanak1.Text == "" ? "0" : txtPLtunjanak1.Text));
            txtPLtunjanak2.Text = string.Format("{0:N2}", Convert.ToInt32(txtPLtunjanak2.Text.Replace(".", "").Replace(",00", "")));
            //txtPLtunjanak1.Text = string.Format("{0:N2}", Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(txttunjangananak.Text))));
            txtPLtunjanak1.Enabled = false;
            fungsiAll();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void tgllahiranak1_OnChange(object sender, EventArgs e)
    {
        try
        {
            if(txttgllahiranak1.Text != "")
            {
                if (Convert.ToDateTime(txttgllahiranak1.Text) > Convert.ToDateTime(DateTime.Now.ToString("dd-MMMM-yyyy")))
                {
                    txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttahunanak1.Text = "0";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Tanggal Lahir Tidak Bisa melebihi hari ini..!!!');", true);
                }
                else
                {
                    string ambilbulanhari = DateTime.Now.ToString("dd-MMMM-yyyy");
                    int wholeMonthsDiff = ((Convert.ToDateTime(ambilbulanhari).Year * 12) + (Convert.ToDateTime(ambilbulanhari).Month) - ((Convert.ToDateTime(txttgllahiranak1.Text).Year * 12) + Convert.ToDateTime(txttgllahiranak1.Text).Month));
                    if ((Convert.ToDateTime(ambilbulanhari).Day < Convert.ToDateTime(txttgllahiranak1.Text).Day))
                        wholeMonthsDiff--;
                    var diff = (Convert.ToDateTime(ambilbulanhari) - Convert.ToDateTime(txttgllahiranak1.Text).AddMonths(wholeMonthsDiff));
                    double monthsDifference = wholeMonthsDiff + (diff.Days / 30d);
                    string asdasdasdasd = string.Format("{0:N2}", Convert.ToDecimal(monthsDifference));
                    txttahunanak1.Text = Convert.ToInt16(monthsDifference / 12).ToString();
                }
            }
            fungsiAll();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void tgllahiranak2_OnChange(object sender, EventArgs e)
    {
        try
        {
            if (txttgllahiranak2.Text != "")
            {
                if (Convert.ToDateTime(txttgllahiranak2.Text) > Convert.ToDateTime(DateTime.Now.ToString("dd-MMMM-yyyy")) || Convert.ToDateTime(txttgllahiranak2.Text) < Convert.ToDateTime(txttgllahiranak1.Text))
                {
                    txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttahunanak2.Text = "0";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Tanggal Lahir Tidak Bisa melebihi hari ini atau Tanggal Lahir anak pertama..!!!');", true);
                }
                else
                {
                    string ambilbulanhari = DateTime.Now.ToString("dd-MMMM-yyyy");
                    int wholeMonthsDiff = ((Convert.ToDateTime(ambilbulanhari).Year * 12) + (Convert.ToDateTime(ambilbulanhari).Month) - ((Convert.ToDateTime(txttgllahiranak2.Text).Year * 12) + Convert.ToDateTime(txttgllahiranak2.Text).Month));
                    if ((Convert.ToDateTime(ambilbulanhari).Day < Convert.ToDateTime(txttgllahiranak3.Text).Day))
                        wholeMonthsDiff--;
                    var diff = (Convert.ToDateTime(ambilbulanhari) - Convert.ToDateTime(txttgllahiranak2.Text).AddMonths(wholeMonthsDiff));
                    double monthsDifference = wholeMonthsDiff + (diff.Days / 30d);
                    txttahunanak2.Text = Convert.ToInt16(monthsDifference / 12).ToString();
                }
                fungsiAll();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void tgllahiranak3_OnChange(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txttgllahiranak3.Text) > Convert.ToDateTime(DateTime.Now.ToString("dd-MMMM-yyyy")))
            {
                txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttahunanak3.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Tanggal Lahir Tidak Bisa melebihi hari ini..!!!');", true);
            }
            else
            {
                string ambilbulanhari = DateTime.Now.ToString("dd-MMMM-yyyy");
                int wholeMonthsDiff = ((Convert.ToDateTime(ambilbulanhari).Year * 12) + (Convert.ToDateTime(ambilbulanhari).Month) - ((Convert.ToDateTime(txttgllahiranak3.Text).Year * 12) + Convert.ToDateTime(txttgllahiranak3.Text).Month));
                if ((Convert.ToDateTime(ambilbulanhari).Day < Convert.ToDateTime(txttgllahiranak3.Text).Day))
                    wholeMonthsDiff--;
                var diff = (Convert.ToDateTime(ambilbulanhari) - Convert.ToDateTime(txttgllahiranak3.Text).AddMonths(wholeMonthsDiff));
                double monthsDifference = wholeMonthsDiff + (diff.Days / 30d);
                txttahunanak3.Text = Convert.ToInt16(monthsDifference / 12).ToString();
            }
            fungsiAll();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void tgllahiranak4_OnChange(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txttgllahiranak4.Text) > Convert.ToDateTime(DateTime.Now.ToString("dd-MMMM-yyyy")))
            {
                txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttahunanak4.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Tanggal Lahir Tidak Bisa melebihi hari ini..!!!');", true);
            }
            else
            {
                string ambilbulanhari = DateTime.Now.ToString("dd-MMMM-yyyy");
                int wholeMonthsDiff = ((Convert.ToDateTime(ambilbulanhari).Year * 12) + (Convert.ToDateTime(ambilbulanhari).Month) - ((Convert.ToDateTime(txttgllahiranak4.Text).Year * 12) + Convert.ToDateTime(txttgllahiranak4.Text).Month));
                if ((Convert.ToDateTime(ambilbulanhari).Day < Convert.ToDateTime(txttgllahiranak4.Text).Day))
                    wholeMonthsDiff--;
                var diff = (Convert.ToDateTime(ambilbulanhari) - Convert.ToDateTime(txttgllahiranak4.Text).AddMonths(wholeMonthsDiff));
                double monthsDifference = wholeMonthsDiff + (diff.Days / 30d);
                txttahunanak4.Text = Convert.ToInt16(monthsDifference / 12).ToString();
            }
            fungsiAll();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void tgllahir_OnChange(object sender, EventArgs e)
    {
        try
        {
            string tes1 = DateTime.Now.AddYears(-20).ToString("MMyyyyhh");
            string tes2 = Convert.ToDateTime(txttgllahir.Text).AddYears(-20).ToString("MMyyyyhh");
            int rubahtahun1 = Convert.ToInt32(tes1);
            int rubahtahun2 = Convert.ToInt32(tes2);
            if (Convert.ToDateTime(txttgllahir.Text) >= DateTime.Now || Convert.ToDateTime(txttgllahir.Text) > DateTime.Now.AddYears(-20) )
            {
                string rubahtahun = DateTime.Now.AddYears(-20).ToString("yyyy");
                txttgllahir.Text = "";
                txttahunpengajuan.Text = "0";
                txtbulanpengajuan.Text = "0";
                txtharipengajuan.Text = "0";
                txttahuntempo.Text = "0";
                txtbulantempo.Text = "0";
                txtharitempo.Text = "0";
                txttgllahir.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Minimal umur 21 Tahun');", true);
            }
            else
            {
                fungsiAll();
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void jangkawaktu_OnChange(object sender, EventArgs e)
    {
        try
        {
            if(txttgllahir.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Masukkan Tanggal Lahir Anda..!!!');", true);
            }
            else
            {
                fungsiAll();
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void gajipokok_OnChange(object sender, EventArgs e)
    {
        try
        {

            txtgajipokok.Text = string.Format("{0:N2}", Convert.ToInt32(txtgajipokok.Text.Replace(".", "").Replace(",00", "")));

            fungsiAll();
            txtPLtunjberas1.Enabled = false;
            txttunjangananak.Focus();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlstatusnikah_OnChange(object sender, EventArgs e)
    {
        try
        {

            if (ddlpensiun.SelectedValue == "1" && Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 1)) < 1 && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if (Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 1)) > 1 || Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(1, 1)) > 1 ||
                    Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(2, 1)) > 0 || Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(3, 1)) > 2
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if ((ddlpensiun.SelectedValue == "0100" || (ddlpensiun.SelectedValue == "0101" || (ddlpensiun.SelectedValue == "0102"))) && ddlpensiun.SelectedValue == "1"
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if ((ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3") && Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 2)) == 11
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else

            {

                fungsiAll();
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlpensiun_OnChange(object sender, EventArgs e)
    {
        try
        {


            if (ddlpensiun.SelectedValue == "1" && Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 1)) < 1 && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if (Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 1)) > 1 || Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(1, 1)) > 1 ||
                    Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(2, 1)) > 0 || Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(3, 1)) > 2
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if ((ddlpensiun.SelectedValue == "0100" || (ddlpensiun.SelectedValue == "0101" || (ddlpensiun.SelectedValue == "0102"))) && ddlpensiun.SelectedValue == "1"
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if ((ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3") && Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 2)) == 11
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else

            {

           

            fungsiAll();
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void gajidisplay_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if(txtgajibersihdisplay.Text == "" || txtgajibersihdisplay.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Gaji Bersih Display Tidak boleh 0');", true);
            }
            else
            {
                txtgajibersihdisplay.Text = string.Format("{0:N2}", Convert.ToInt32(txtgajibersihdisplay.Text.Replace(".", "").Replace(",00", "")));

                fungsiAll();
            }
            txtgajipokok.Focus();

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtPLgajipokok1_OnChange(object sender, EventArgs e)
    {
        try
        {
            if (txtgajibersihdisplay.Text == "" || txtgajibersihdisplay.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Gaji Bersih Display Tidak boleh 0');", true);
            }
            else
            {
                fungsiAll();
                txtPLtunjberas.Text = Convert.ToString(Convert.ToInt16(txtPLgajipokok.Text) + Convert.ToInt16(txtPLtunjanak.Text) + Convert.ToInt16(txtPLtunjistri.Text));
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtPLtunjanak1_OnChange(object sender, EventArgs e)
    {
        try
        {
            if (txtgajibersihdisplay.Text == "" || txtgajibersihdisplay.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Gaji Bersih Display Tidak boleh 0');", true);
            }
            else
            {
                int tunjanak = Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtPLtunjanak1.Text.Replace(".", "").Replace(",00", "")))).ToString());
                txtPLtunjanak2.Text = string.Format("{0:N2}", Convert.ToInt32(txtPLtunjanak.Text == "" ? "0" : txtPLtunjanak.Text.Replace(".", "").Replace(",00", "")) * tunjanak  );// * Convert.ToInt32(txtPLtunjanak1.Text == "" ? "0" : txtPLtunjanak1.Text));
                txtPLtunjanak1.Text = string.Format("{0:N2}", Convert.ToInt32(txtPLtunjanak1.Text.Replace(".", "").Replace(",00", "")));

                fungsiAll();
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtPLtinjistri1_OnChange(object sender, EventArgs e)
    {
        try
        {
            if (txtgajibersihdisplay.Text == "" || txtgajibersihdisplay.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Gaji Bersih Display Tidak boleh 0');", true);
            }
            else
            {
                txtPLtunjberas.Text = Convert.ToString(Convert.ToInt16(txtPLgajipokok.Text.Replace(".", "").Replace(",00", "")) + Convert.ToInt16(txtPLtunjanak.Text.Replace(".", "").Replace(",00", "")) + Convert.ToInt16(txtPLtunjistri.Text.Replace(".", "").Replace(",00", "")));
                string istri = txtPLtunjistri1.Text;
                int istri1 = Convert.ToInt32(txtPLtunjistri1.Text.Replace(".", "").Replace(",00", ""));
              
                int hasil = Convert.ToInt32(txtPLtunjistri1.Text.Replace(".", "").Replace(",00", "")) * Convert.ToInt32(txtPLtunjistri.Text == "0" ? "1" : txtPLtunjistri.Text.Replace(".", "").Replace(",00", ""));
                txtPLtunjistri2.Text = string.Format("{0:N2}", hasil);

                fungsiAll();
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtPLtunjberas1_OnChange(object sender, EventArgs e)
    {
        try
        {
            if (txtgajibersihdisplay.Text == "" || txtgajibersihdisplay.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Gaji Bersih Display Tidak boleh 0');", true);
            }
            else
            {

                txtPLtunjberas1.Text = string.Format("{0:N2}", Convert.ToInt32(txtPLtunjberas1.Text.Replace(".", "").Replace(",00", "")));
                int hasil = (Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtPLtunjberas.Text.Replace(".", "").Replace(",00", "")))).ToString())
                            * Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtPLtunjberas1.Text.Replace(".", "").Replace(",00", "")))).ToString()));
                txtPLtunjberas2.Text = string.Format("{0:N2}", hasil); 
                totalgajitunjangan();
            }

            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void angsuran_OnChange(object sender, EventArgs e)
    {
        try
        {
            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void plafond_OnChange(object sender, EventArgs e)
    {
        try
        {
            if((txtgajibersihdisplay.Text == "" ? "0" : txtgajibersihdisplay.Text.Replace(".","").Replace(",00","")) == "0" || (txtgajipokok.Text == "" ? "0" : txtgajipokok.Text.Replace(".", "").Replace(",00", "")) == "0")
            {
                txtPlafond.Text = "0";
                txtgajibersihdisplay.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Isi terlebih dahulu Gaji Pokok Dan Gaji Bersih Display');", true);
            }
            else if (Convert.ToInt32(decimal.Parse(txtPlafond.Text)) > Convert.ToInt32(decimal.Parse(txtplafondmax.Text)))
            {
                txtPlafond.Text = "0";
                txtPlafond.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Jumlah Plafond tidak boleh melebihi Plafond Max');", true);
            }
            else if(Convert.ToInt32(decimal.Parse(txtPlafond.Text)) < 1000000)
            {
                txtPlafond.Text = "0";
                txtPlafond.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Minimal Plafond Rp.1.000.000');", true);
            }
            else
            {
                txtPlafond.Text = string.Format("{0:N2}", Convert.ToInt32(decimal.Parse(txtPlafond.Text)));
                fungsiAll();
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void bunga_OnChange(object sender, EventArgs e)
    {
        try
        {
            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void PLterimabersih_OnChange(object sender, EventArgs e)
    {
        try
        {
            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void PelunasanTOP_OnChange(object sender, EventArgs e)
    {
        try
        {
            txtpelunasantakeover.Text = string.Format("{0:N2}", Convert.ToInt32(txtpelunasantakeover.Text.Replace(".", "").Replace(",00", "")));

            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtbungadeviasi_OnChange(object sender, EventArgs e)
    {
        try
        {
            if(ddljenisdeviasi.SelectedValue == "" || ddljenisdeviasi.SelectedValue != "2")
            {
                txtbungadeviasi.Text = "";
            }
            else if (ddljenisdeviasi.SelectedValue == "2" && (decimal.Parse(txtbunga.Text) < decimal.Parse(txtbungadeviasi.Text)))
            {
                txtbungadeviasi.Text = "0";//; txtbungadeviasi.Text == "" ? "0" : txtbungadeviasi.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Deviasi Bunga tidak boleh melebihi Bunga Normal');", true);
            }

            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtdeviasiasuransi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    #endregion "TextChanged"

    #region "Radio change"
    //protected void OnCheck_jenisnemo(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if(ddljenismemo.SelectedValue == "1")
    //        {
    //            divnemo.Visible = true;
    //            idsimulasideviasi.Attributes.Add("class", "col-lg-12 col-md-12 col-sm-12 col-xs-12");
    //            //divatursimulasi1.Attributes.Add("class", "form-group col-md-6");
    //            //divatursimulasi2.Attributes.Add("class", "form-group col-md-4");
    //            //idddljenisdeviasi.Visible = true;
    //        }
    //        else if (ddljenismemo.SelectedValue == "2")
    //        {
    //            divnemo.Visible = false;
    //            //idddljenisdeviasi.Visible = false;
    //        }
    //    }
    //    catch (System.Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //    }
    //}
    #endregion

    #region "dropdown"
    protected void ddljenisdeviasi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtRangsblokir.Text = "0,00";
            txtRangsdimuka.Text = "0,00";
            //txttittleasuransi.SelectedValue = "0";
            ddlbungadeviasi.SelectedValue = "0";
            txtbungadeviasi.Text = "";
            txttittleprovisi.SelectedValue = "1";
            //txttittleprovisi2.Text = "0";
            txttittlediblokir.SelectedValue = "1";
            txttittledimuka.SelectedValue = "3";
            if (ddljenisdeviasi.SelectedValue == "")
            {
                txttittleprovisi2.Enabled = false;
                txttittleasuransi.Enabled = false;
                txtdeviasiasuransi.Enabled = false;
                txtbungadeviasi.Enabled = false;
                txttittleprovisi.Enabled = false;
                txttittlediblokir.Enabled = false;
                txttittledimuka.Enabled = false;

                txtRangsblokir.Text = "0,00";
                txtRangsdimuka.Text = "0,00";
                txttittleasuransi.SelectedValue = "0";
                ddlbungadeviasi.SelectedValue = "0";
                txtbungadeviasi.Text = "";
                txttittleprovisi.SelectedValue = "1";
                txttittleprovisi2.Text = "0";
                txttittlediblokir.SelectedValue = "1";
                txttittledimuka.SelectedValue = "3";
                txtRtittleprovisi.Text = txttittleprovisi2.Text == "" ? HDNProvisi.Value : txttittleprovisi2.Text;
            }
            else if (ddljenisdeviasi.SelectedValue == "1")
            {
                txttittleprovisi2.Enabled = false;
                txttittleasuransi.Enabled = false;
                txtdeviasiasuransi.Enabled = false;
                txtbungadeviasi.Enabled = false;
                txttittleprovisi.Enabled = false;
                txttittlediblokir.Enabled = false;
                txttittledimuka.Enabled = false;
                txttittlediblokir.Enabled = true;

                txtRangsblokir.Text = txtangsblokir.Text;
                txtRangsdimuka.Text = "0,00";
            }
            else if(ddljenisdeviasi.SelectedValue == "2")
            {
                
                txttittleprovisi2.Enabled = false;
                txttittleasuransi.Enabled = false;
                txtdeviasiasuransi.Enabled = false;
                txtbungadeviasi.Enabled = false;
                txttittleprovisi.Enabled = false;
                txttittlediblokir.Enabled = false;
                txttittledimuka.Enabled = false;
                ddlbungadeviasi.SelectedValue = "1";
                txtbungadeviasi.Enabled = true;
                txttittleprovisi2.Text = HDNProvisi.Value;
                txtRangsblokir.Text = "0,00";
                txtRangsdimuka.Text = "0,00";
            }
            else if (ddljenisdeviasi.SelectedValue == "3")
            {
                txttittleprovisi2.Enabled = false;
                txttittleasuransi.Enabled = false;
                txtdeviasiasuransi.Enabled = false;
                txtbungadeviasi.Enabled = false;
                txttittleprovisi.Enabled = false;
                txttittlediblokir.Enabled = false;
                txttittledimuka.Enabled = false;

                txttittleasuransi.SelectedValue = "1";
                txtdeviasiasuransi.Text = "0";
                txtdeviasiasuransi.Enabled = true;

                txtRangsblokir.Text = "0,00";
                txtRangsdimuka.Text = "0,00";
            }
            else if (ddljenisdeviasi.SelectedValue == "4")
            {
                txttittleprovisi2.Enabled = false;
                txttittleasuransi.Enabled = false;
                txtdeviasiasuransi.Enabled = false;
                txtbungadeviasi.Enabled = false;
                txttittleprovisi.Enabled = false;
                txttittlediblokir.Enabled = false;
                txttittledimuka.Enabled = false;

                txttittleprovisi.Enabled = true;
                txttittleprovisi2.Enabled = true;

                txtRangsblokir.Text = "0,00";
                txtRangsdimuka.Text = "0,00";
            }
            else if (ddljenisdeviasi.SelectedValue == "5")
            {
                txttittleprovisi2.Enabled = false;
                txttittleasuransi.Enabled = false;
                txtdeviasiasuransi.Enabled = false;
                txtbungadeviasi.Enabled = false;
                txttittleprovisi.Enabled = false;
                txttittlediblokir.Enabled = false;
                txttittledimuka.Enabled = false;
                txttittledimuka.Enabled = true;

                txtRangsblokir.Text = "0,00";
                txtRangsdimuka.Text = txtangsdimuka.Text;
            }
            fungsiAll();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlKodeJiwa_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            //string cat1;
            if (ddlpensiun.SelectedValue == "1" && Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 1)) < 1 && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if (Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 1)) > 1 || Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(1, 1)) > 1 ||
                    Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(2, 1)) > 0 || Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(3, 1)) > 2
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if ((ddlpensiun.SelectedValue == "0100" || (ddlpensiun.SelectedValue == "0101" || (ddlpensiun.SelectedValue == "0102"))) && ddlpensiun.SelectedValue == "1"
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if ((ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3") && Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 2)) == 11
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                //cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }

            else

            {



                divanak1.Visible = true;
                divanak2.Visible = true;
                //divanak3.Visible = true;
                //divanak4.Visible = true;

                txttgllahiranak1.Enabled = false;
                txttgllahiranak2.Enabled = false;
                txttgllahiranak3.Enabled = false;
                txttgllahiranak4.Enabled = false;

                txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                txttahunanak1.Text = "0";
                txttahunanak2.Text = "0";
                txttahunanak3.Text = "0";
                txttahunanak4.Text = "0";
                if (ddlkodejiwa.SelectedValue == "0100" || ddlkodejiwa.SelectedValue == "1000")
                {
                    ddlstatusnikah.SelectedValue = "1";
                    txttgllahiranak1.Enabled = false;
                    txttgllahiranak2.Enabled = false;
                    txttgllahiranak3.Enabled = false;
                    txttgllahiranak4.Enabled = false;
                    txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");

                }
                else if (ddlkodejiwa.SelectedValue == "0101" || ddlkodejiwa.SelectedValue == "1001")
                {
                    ddlstatusnikah.SelectedValue = "1";
                    txttgllahiranak1.Enabled = true;
                    txttgllahiranak2.Enabled = false;
                    txttgllahiranak3.Enabled = false;
                    txttgllahiranak4.Enabled = false;
                    txtPLtunjanak1.Enabled = false;
                    //txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    //txtPLtunjanak.Text = "1";
                }
                else if (ddlkodejiwa.SelectedValue == "0102" || ddlkodejiwa.SelectedValue == "1002")
                {
                    ddlstatusnikah.SelectedValue = "1";
                    txttgllahiranak1.Enabled = true;
                    txttgllahiranak2.Enabled = true;
                    txttgllahiranak3.Enabled = false;
                    txttgllahiranak4.Enabled = false;
                    txtPLtunjanak1.Enabled = false;
                    //txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    //txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    //txtPLtunjanak.Text = "2";
                }
                else if (ddlkodejiwa.SelectedValue == "0103" || ddlkodejiwa.SelectedValue == "1003")
                {
                    ddlstatusnikah.SelectedValue = "1";
                    txttgllahiranak1.Enabled = true;
                    txttgllahiranak2.Enabled = true;
                    txttgllahiranak3.Enabled = true;
                    txttgllahiranak4.Enabled = false;
                    txtPLtunjanak1.Enabled = false;
                    txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    //txtPLtunjanak.Text = "3";
                }
                else if (ddlkodejiwa.SelectedValue == "0111" || ddlkodejiwa.SelectedValue == "1011" || ddlkodejiwa.SelectedValue == "1111")
                {
                    ddlstatusnikah.SelectedValue = "1";
                    txttgllahiranak1.Enabled = true;
                    txttgllahiranak2.Enabled = true;
                    txttgllahiranak3.Enabled = true;
                    txttgllahiranak4.Enabled = true;
                    txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                }
                //menikah
                else if (ddlkodejiwa.SelectedValue == "1100")
                {
                    ddlstatusnikah.SelectedValue = "1";
                    txttgllahiranak1.Enabled = false;
                    txttgllahiranak2.Enabled = false;
                    txttgllahiranak3.Enabled = false;
                    txttgllahiranak4.Enabled = false;
                    txtPLtunjistri.Enabled = false;
                    txtPLtunjistri1.Enabled = false;
                }
                else if (ddlkodejiwa.SelectedValue == "1101")
                {
                    ddlstatusnikah.SelectedValue = "1";
                    txttgllahiranak1.Enabled = true;
                    txttgllahiranak2.Enabled = false;
                    txttgllahiranak3.Enabled = false;
                    txttgllahiranak4.Enabled = false;
                    txtPLtunjanak1.Enabled = false;
                    txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    //txtPLtunjanak.Text = "1";
                }
                else if (ddlkodejiwa.SelectedValue == "1102")
                {
                    ddlstatusnikah.SelectedValue = "1";
                    txttgllahiranak1.Enabled = true;
                    txttgllahiranak2.Enabled = true;
                    txttgllahiranak3.Enabled = false;
                    txttgllahiranak4.Enabled = false;
                    txtPLtunjanak1.Enabled = false;
                    txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    //txtPLtunjanak.Text = "2";
                }
                else if (ddlkodejiwa.SelectedValue == "1103")
                {
                    ddlstatusnikah.SelectedValue = "1";
                    txttgllahiranak1.Enabled = true;
                    txttgllahiranak2.Enabled = true;
                    txttgllahiranak3.Enabled = true;
                    txttgllahiranak4.Enabled = false;
                    txtPLtunjanak1.Enabled = false;
                    txttgllahiranak1.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak2.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak3.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    txttgllahiranak4.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    //txtPLtunjanak.Text = "3";
                }
                fungsiAll();
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlkodeproduk_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlkodeproduk.SelectedValue == "" )
            {
                ddlstatustakeover.SelectedValue = "";
                pnldatapemohon.Visible = false;
                btncalculate.Visible = false;
            }
            else if (ddlkodeproduk.SelectedValue == "4" || ddlkodeproduk.SelectedValue == "1")
            {
                ddlstatustakeover.SelectedValue = "1";
                txtpelunasantakeover.Enabled = true;
                if (ddlkodeproduk.SelectedValue == "" || ddlsumberdana.SelectedValue == "")
                {
                    pnldatapemohon.Visible = false;
                    btncalculate.Visible = false;
                }
                else
                {
                    pnldatapemohon.Visible = true;
                    btncalculate.Visible = true;
                }
            }
            else if (ddlkodeproduk.SelectedValue == "11" || ddlkodeproduk.SelectedValue == "2")
            {
                ddlstatustakeover.SelectedValue = "2";
                txtpelunasantakeover.Enabled = false;
                txtpelunasantakeover.Text = "0";
                if (ddlkodeproduk.SelectedValue == "" || ddlsumberdana.SelectedValue == "")
                {
                    pnldatapemohon.Visible = false;
                    btncalculate.Visible = false;
                }
                else
                {
                    pnldatapemohon.Visible = true;
                    btncalculate.Visible = true;
                }
            }
            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlsumberdana_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlkodeproduk.SelectedValue == "" || ddlsumberdana.SelectedValue == "")
            {
                pnldatapemohon.Visible = false;
                //Pnldepan.Visible = false;
            }
            else
            {
                pnldatapemohon.Visible = true;
                //Pnldepan.Visible = true;
            }
            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void asuransi_OnChange(object sender, EventArgs e)
    {
        try
        {
            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void keanggotaan_OnChange(object sender, EventArgs e)
    {
        try
        {
            deviasikeanggotaan();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void angsblokir_OnChange(object sender, EventArgs e)
    {
        try
        {
            deviasiangsblokir();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void provisi_OnChange(object sender, EventArgs e)
    {
        try
        {
            deviasiprovisi();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void angsdimuka_OnChange(object sender, EventArgs e)
    {
        try
        {
            deviasidimuka();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlangsblokir_OnChange(object sender, EventArgs e)
    {
        try
        {
            txtRangsblokir.Text = (Convert.ToInt32(ddlangsblokir.SelectedValue) * Convert.ToInt32(txtangsuran.Text)).ToString(); 
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlstatussk_OnChange(object sender, EventArgs e)
    {
        try
        {
            rubahstatussk();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlangsdimuka_OnChange(object sender, EventArgs e)
    {
        try
        {
            txtRangsdimuka.Text = (Convert.ToInt32(ddlangsdimuka.SelectedValue) * Convert.ToInt32(txtangsuran.Text)).ToString();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddljangkawaktu_OnChange(object sender, EventArgs e)
    {
        try
        {
            txtRangsdimuka.Text = (Convert.ToInt32(ddlangsdimuka.SelectedValue) * Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtangsuran.Text.Replace(".", "").Replace(",00", "")))).ToString())).ToString();
            fungsiAll();
            //int sadasdasdasdasda = Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtangsuran.Text.Replace(".", "").Replace(",00", "")))).ToString());
            if (ddljangkawaktu.SelectedValue != "")
            {
                if (Convert.ToInt32(ddljangkawaktu.SelectedValue) > Convert.ToInt16(txtjwmax.Text))
                {
                    ddljangkawaktu.SelectedValue = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Jangka Waktu tidak boleh melebihi JW Max');", true);
                }
                else if (Convert.ToInt16(txttahuntempo.Text) > 80 && (ddlkodeproduk.SelectedValue == "1" || ddlkodeproduk.SelectedValue == "2"))
                {
                    ddljangkawaktu.SelectedValue = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Umur Jatuh Tempo > 75 Tahun. Silahkan Pilih Kode Produk Platinum');", true);
                }
            }else if(ddljangkawaktu.SelectedValue == "")
            {
                txtbunga.Text = "0";
            }
            fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void bungadeviasi_OnChange(object sender, EventArgs e)
    {
        try
        {
            txtbungadeviasi.Text = txtbungadeviasi.Text == "" ? "0" : string.Format("{0:N2}", txtbungadeviasi.Text);
            angsuran();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    
    #endregion "dropdown"
    //string.Format("{0:N2}", hasil);
    #region "Function"
    public bool fungsiAll()
    {
        try
        {
            string cobacabang = SessBranchID;
            if(ddljangkawaktu.SelectedValue != "" && txttgllahir.Text == "")
            {
                ddljangkawaktu.SelectedValue = "";
            }
            perhitunganjangka(txttgllahir.Text);
            angsuranmax();
            angsuran();
            plafondmax();
            gajitunjanak();
            totalgajitunjangan();
            angsdimuka();
            jwmax();
            perhitungantempo();
            JWdanbungaasuransi();
            instansi();
            bunga();
            if (ddlkodejiwa.SelectedValue == "")
            {
                pengurangantunjanganull();
            }
            if (txttgllahir.Text == "")
            {
                usiapengajuannull();
            }
            fungsideviasi();
            potongan();
            terimabersih();
            terimabersih();
            if (txttgllahir.Text != "" && ddljangkawaktu.SelectedValue != "")
            { 
                Keterangan1(); 
            }
            Keterangan2();
            Keterangan3();
            Catatan1();
            Catatan2();
            Catatan3();
            usiaasuransi();
            hitunganak();
                PLgajipokok();
            PLtunjanak();
            PListri();
            PLberas();
            gajinol();
            sisagaji();
            rubahstatussk();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return true;
    }
    public bool fungsideviasi()
    {
        try
        {
            deviasiangsblokir();
            deviasiasuransi();
            deviasidimuka();
            deviasikeanggotaan();
            deviasiprovisi();
            deviasibunga();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return true;
    }
    public string rubahstatussk()
    {
        try
        {
            if(ddlstatussk.SelectedValue == "1" && txtketerangan1.Text == "LAMPIRKAN SURAT PERNYATAAN TAKE OVER")
            {
                txtketerangan1.Text = "SK SUDAH DI TERIMA";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtketerangan1.Text;
    }
    public bool PLgajipokok()
    {
        try
        {
            if (txtcatatan1.Text == "KODE JIWA SALAH" && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "") 
            {
                txtPLgajipokok.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if (lbltaspenasabri.Text == "Taspen")
            {
                txtPLgajipokok.Text = "0";
                txtPlgajipokok2.Text = "0";
                txtPLgajipokok1.Text = "0";

            }
            else if(ddlpensiun.SelectedValue == "1")
            {
                txtPLgajipokok.Text = "0";
            }
            else if(Convert.ToInt32(ddlpensiun.SelectedValue == "" ? "0" : ddlpensiun.SelectedValue) > 1)
            {
                if (txttahunanak1.Text == "0" && txttahunanak2.Text == "0")
                {
                    txtPLgajipokok.Text = "0";
                }
                else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21 && txttahunanak1.Text == "0")
                {
                    txtPLgajipokok.Text = "1";
                }
                else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21 && txttahunanak1.Text == "0")
                {
                    txtPLgajipokok.Text = "0";
                }
                else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21)
                {
                    txtPLgajipokok.Text = "2";
                }
                else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21)
                {
                    txtPLgajipokok.Text = "1";
                }
                else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21)
                {
                    txtPLgajipokok.Text = "0";
                }
                Pltunjangangajipokok();
                Pltunjangangajipokok2();
            }
            else
            {
                txtPLgajipokok.Text = "0";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return true;
    }
    protected string Pltunjangangajipokok()
    {
        try
        {
            if(txtcatatan1.Text == "KODE JIWA SALAH")
            { 
                txtPLgajipokok1.Text = "0"; 
            }
            else if(ddlkodejiwa.SelectedValue.Substring(3, 1) == "0")
            {
                txtPLgajipokok1.Text = "0";
            }
            else if (lbltaspenasabri.Text == "Asabri" && Convert.ToInt32(ddlpensiun.SelectedValue == "" ? "0" : ddlpensiun.SelectedValue) > 1 && Convert.ToInt32(ddlkodejiwa.SelectedValue == "" ? "0" : ddlkodejiwa.SelectedValue) >= 1 
                && Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21 && txttahunanak1.Text == "")
            {
                txtPLgajipokok1.Text = "0";
            }
            else if (lbltaspenasabri.Text == "Asabri" && Convert.ToInt32(ddlpensiun.SelectedValue == "" ? "0" : ddlpensiun.SelectedValue) > 1 
                && Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(3, 1)) >= 1 && Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21 
                && Convert.ToInt32(txttahunanak2.Text) < 21)
            {
                txtPLgajipokok1.Text = "0";
            }
            else if (lbltaspenasabri.Text == "Asabri" && Convert.ToInt32(ddlpensiun.SelectedValue == "" ? "0" : ddlpensiun.SelectedValue) > 1 &&
               Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(3, 1)) >= 1 && Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21
                && (txttahunanak2.Text == "" || txttahunanak2.Text == "0"))
            { 
                txtPLgajipokok1.Text = txtgajipokok.Text; 
            }
            else if (lbltaspenasabri.Text == "Asabri" && Convert.ToInt32(ddlpensiun.SelectedValue == "" ? "0" : ddlpensiun.SelectedValue) > 1 &&
                Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(3, 1)) >= 1 && Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21
                && Convert.ToInt32(txttahunanak2.Text == "" ? "0" : txttahunanak2.Text) >= 21)
            {
                txtPLgajipokok1.Text = txtgajipokok.Text;
            }
            else if (lbltaspenasabri.Text == "Asabri" && Convert.ToInt32(ddlpensiun.SelectedValue == "" ? "0" : ddlpensiun.SelectedValue) > 1 &&
                Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(3, 1)) >= 1 && Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21
                && Convert.ToInt32(txttahunanak2.Text == "" ? "0" : txttahunanak2.Text) < 21)
            {
                txtPLgajipokok1.Text = txtgajipokok.Text;
            }
            else
            {
                txtPLgajipokok1.Text = "0";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtPLgajipokok1.Text;
    }
    protected string Pltunjangangajipokok2()
    {
        try
        {
            if (txtPLgajipokok.Text == "1")
            {
                txtPlgajipokok2.Text = string.Format("{0:N2}", Convert.ToInt32((decimal.Parse("20") / decimal.Parse("100")) * decimal.Parse(txtPLgajipokok1.Text)));
            }
            else if (txtPLgajipokok.Text == "0")
            {
                txtPlgajipokok2.Text = "0";
            }
            else
            {
                txtPlgajipokok2.Text = string.Format("{0:N2}", Convert.ToInt32((decimal.Parse("30") / decimal.Parse("100")) * decimal.Parse(txtPLgajipokok1.Text)));
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtPlgajipokok2.Text;
    }
    protected string PLtunjanak()
    {
        try
        {
            if (txtcatatan1.Text == "KODE JIWA SALAH" && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                txtPLtunjanak.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if (txttahunanak1.Text == "0" && txttahunanak2.Text == "0" && txttahunanak3.Text == "0" && txttahunanak4.Text == "0")
            {
                txtPLtunjanak.Text = "0";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21 && txttahunanak2.Text == "0" && txttahunanak3.Text == "0" && txttahunanak4.Text == "0")
            {
                txtPLtunjanak.Text = "1";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21 && txttahunanak2.Text == "0" && txttahunanak3.Text == "0" && txttahunanak4.Text == "0")
            {
                txtPLtunjanak.Text = "0";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21)
            {
                txtPLtunjanak.Text = "1";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21)
            {
                txtPLtunjanak.Text = "0,5";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text == "" ? "0" : txtjwtahun.Text) < 21)
            {
                txtPLtunjanak.Text = "0";
            }
            if(txtPLtunjanak.Text == "0")
            {
                txtPLtunjanak1.Text = string.Format("{0:N2}", 0);
            }
            else
            {
                txtPLtunjanak1.Text = string.Format("{0:N2}", Convert.ToInt32(Math.Ceiling(decimal.Parse(txttunjangananak.Text))));
            } 
            txtPLtunjanak2.Text = string.Format("{0:N2}", (decimal.Parse(txtPLtunjanak.Text) * decimal.Parse(txtPLtunjanak1.Text)));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return hdnjumlahanak.Value;
    }
    public bool PListri()
    {   
        try
        {
            if (ddlstatusnikah.SelectedValue == "0" && ddlpensiun.SelectedValue == "1" && txtcatatan1.Text == "KURANGI TUNJ. ISTRI & BERAS" && ddlkodejiwa.SelectedValue.Substring(1, 1) == "1")
            { 
                txtPLtunjistri.Text = "2";
            }
            else
            {
                txtPLtunjistri.Text = "0";
            }
            Pltunjanganistri();
            Pltunjanganistri2();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return true;
    }
    protected string Pltunjanganistri()
    {
        try
        {
            if (txtcatatan1.Text == "KURANGI TUNJ. ISTRI & BERAS")
            { 
                txtPLtunjistri1.Text = string.Format("{0:N2}", (Convert.ToInt32(txtgajipokok.Text.Replace(".", "").Replace(",00", "")) * (decimal.Parse("10") / decimal.Parse("100"))));
            }
            else
            {
                txtPLtunjistri1.Text = string.Format("{0:N2}", 0);
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtPLtunjistri1.Text;
    }
    protected string Pltunjanganistri2()
    {
        try
        {
            txtPLtunjistri2.Text = string.Format("{0:N2}", (Convert.ToInt32(txtPLtunjistri1.Text.Replace(".", "").Replace(",00", "")) * Convert.ToInt32(txtPLtunjistri.Text.Replace(".", "").Replace(",00", ""))));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtPLtunjistri2.Text;
    }
    public bool PLberas()
    {
        try
        {
            if (txtcatatan1.Text == "KODE JIWA SALAH")
            {
                txtPLtunjberas.Text = "0"; 
            }
            else if (txttahunanak1.Text == "0" && txttahunanak2.Text == "0" && txtcatatan1.Text == "KURANGI TUNJ. ISTRI & BERAS" )
            {
                txtPLtunjberas.Text = "2";
            }
            else if (txttahunanak1.Text == "0" && txttahunanak2.Text == "0")
            {
                txtPLtunjberas.Text = "0";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21 
                && txttahunanak2.Text == "0" && txtcatatan1.Text == "KURANGI TUNJ. ISTRI & BERAS")
            {
                txtPLtunjberas.Text = "3";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21
                && txttahunanak2.Text == "0")
            {
                txtPLtunjberas.Text = "1";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) < 21
                && txttahunanak2.Text == "0" && txtcatatan1.Text == "KURANGI TUNJ. ISTRI & BERAS")
            {
                txtPLtunjberas.Text = "2";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) < 21
                && txttahunanak2.Text == "0")
            {
                txtPLtunjberas.Text = "0";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text)  >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21 
                && txtcatatan1.Text == "KURANGI TUNJ. ISTRI & BERAS")
            {
                txtPLtunjberas.Text = "4";
            }
            else if(Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21)
            {
                txtPLtunjberas.Text = "2";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text) < 21 
                && txtcatatan1.Text == "KURANGI TUNJ. ISTRI & BERAS")
            {
                txtPLtunjberas.Text = "3";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text) < 21)
            {
                txtPLtunjberas.Text = "1";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) < 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text) < 21 
                && txtcatatan1.Text == "KURANGI TUNJ. ISTRI & BERAS")
            {
                txtPLtunjberas.Text = "2";
            }
            else if (Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) < 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text) < 21)
            {
                txtPLtunjberas.Text = "0";
            }
            else 
            { 
                txtPLtunjberas.Text = "0"; 
            }
            Pltunjanganberas();
            Pltunjanganberas2();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return true;
    }
    protected string Pltunjanganberas()
    {
        try
        {
            if (txtcatatan1.Text == "KODE JIWA SALAH")
            {
                txtPLtunjberas1.Text = string.Format("{0:N2}", 0);
            }
            else if(txtcatatan1.Text == "KURANGI TUNJ. ISTRI & BERAS")
            {
                txtPLtunjberas1.Text = string.Format("{0:N2}" ,72420);
            }
            else if(txttahunanak1.Text == "0" && txttahunanak2.Text == "0")
            {
                txtPLtunjberas1.Text = string.Format("{0:N2}", 0);
            }
            else if(Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21 && txttahunanak2.Text == "0")
            {
                txtPLtunjberas1.Text = string.Format("{0:N2}", 72420);
            }
            else if(Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) < 21 && txttahunanak2.Text == "0")
            {
                txtPLtunjberas1.Text = string.Format("{0:N2}", 0);
            }
            else if(Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21)
            {
                txtPLtunjberas1.Text = string.Format("{0:N2}", 72420);
            }
            else if(Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) >= 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text) < 21)
            {
                txtPLtunjberas1.Text = string.Format("{0:N2}", 72420);
            }
            else if(Convert.ToInt32(txttahunanak1.Text) + Convert.ToInt32(txtjwtahun.Text) < 21 && Convert.ToInt32(txttahunanak2.Text) + Convert.ToInt32(txtjwtahun.Text) < 21)
            {
                txtPLtunjberas1.Text = string.Format("{0:N2}", 0);
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtPLtunjberas1.Text;
    }
    protected string Pltunjanganberas2()
    {
        try
        {
            txtPLtunjberas2.Text = string.Format("{0:N2}", (Convert.ToInt32(txtPLtunjberas.Text.Replace(".", "").Replace(",00", "")) * Convert.ToInt32(txtPLtunjberas1.Text.Replace(".", "").Replace(",00", ""))));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtPLtunjberas2.Text;
    }
    protected string Catatan1()
    {
        try
        {
            string cat1;
            if (ddlkodejiwa.SelectedValue.Length < 4 || ddlkodejiwa.SelectedValue.Length > 4)
            {
                cat1 = "KODE JIWA SALAH";
            }
            else if (ddlpensiun.SelectedValue == "1" && Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 1)) < 1 && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if (Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 1)) > 1 || Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(1, 1)) > 1 ||
                    Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(2, 1)) > 0 || Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(3, 1)) > 2
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if ((ddlpensiun.SelectedValue == "0100" || (ddlpensiun.SelectedValue == "0101" || (ddlpensiun.SelectedValue == "0102"))) && ddlpensiun.SelectedValue == "1"
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if ((ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3") && Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(0, 2)) == 11
                    && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
            {
                cat1 = "KODE JIWA SALAH";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
            }
            else if ((ddlkodejiwa.SelectedValue == "1100" || ddlkodejiwa.SelectedValue == "1101" || ddlkodejiwa.SelectedValue == "1102") 
                && ddlstatusnikah.SelectedValue == "0" && ddlpensiun.SelectedValue == "1")
            {
                cat1 = "KURANGI TUNJ. ISTRI & BERAS";
            }
            else if ((ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3") && (ddlstatusnikah.SelectedValue == "0" && ddlpensiun.SelectedValue == "1"))
            {
                cat1 = "LAMPIRKAN SUKET KEMATIAN";
            }
            else if (ddlstatusnikah.SelectedValue == "1" && ddlpensiun.SelectedValue == "1")
            {
                cat1 = "LAMPIRKAN BUKU NIKAH";
            }
            else 
            {
                cat1 = "";
            }
            txtcatatan1.Text = cat1;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtcatatan1.Text;
    }
    protected string Catatan2()
    {
        try
        {
            string cat2;
            if((ddlkodeproduk.SelectedValue == "11"|| ddlkodeproduk.SelectedValue == "2") && (lblnopenbaru.Visible = true && txtpelunasantakeover.Text.Replace(".", "").Replace(",00", "") == "0"))
            {
                cat2 = "LAMPIRKAN SK ASLI";
            }
            else if (ddlkodeproduk.SelectedValue == "1" || ddlkodeproduk.SelectedValue == "4")
            {
                cat2 = "LAMP. KWITANSI ANGS. & PEL";
            }
            else
            {
                cat2 = "";
            }
            txtcatatan2.Text = cat2;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtcatatan2.Text;
    }
    protected string Catatan3()
    {
        try
        {
            string cat3;
            cat3 = "LAMPIRKAN NPWP";
            txtcatatan3.Text = cat3;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtcatatan3.Text;
    }
    protected string Keterangan1()
    {
        try
        {
            if (Convert.ToInt32(txtgajitunjanak.Text.Replace(".", "").Replace(",00", "")) < 100000)
            {
                txtketerangan1.Text = "WARNING !!!      SISA GAJI KURANG DARI RP. 100.000";
            }
            else if (ddlkodeproduk.SelectedValue == "4" || ddlkodeproduk.SelectedValue == "1")
            {
                txtketerangan1.Text = "LAMPIRKAN SURAT PERNYATAAN TAKE OVER";
            }
            else
            {
                txtketerangan1.Text = "";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtketerangan1.Text;
    }
    protected string Keterangan2()
    {
        try
        {
            //DateTime coba = Convert.ToDateTime(Convert.ToDateTime(txttgllahir.Text).ToString("dd-MM-" + DateTime.Now.ToString("yyyy")));
            if(txtbulanpengajuan.Text == "11" && Convert.ToInt32(txtharipengajuan.Text) - 1 > 15 )/*&& E5 = 1*/
            {
                Keterangan1();
            }
            else if (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3")
            {
                txtketerangan2.Text = "LAMPIRKAN SUKET TIDAK AKAN MENIKAH LAGI"; 
            }
            else
            {
                txtketerangan2.Text = "";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtketerangan2.Text;
    }
    protected string Keterangan3()
    {
        try
        {
            if(ddlkodeproduk.SelectedValue == "1" || ddlkodeproduk.SelectedValue == "4")
            {
                txtketerangan3.Text = "Lampirkan Suket Pengembalian SK";
            }
            else
            {
                txtketerangan3.Text = "";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtketerangan3.Text;
    }
    protected string TglHariIni()
    {
        try
        {
            string a = "asdas";
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return hdntglhariini.Value;
    }
    protected string hitunganak()
    {
        try
        {
            if(ddlkodejiwa.SelectedValue == "")
            {
                hdnjumlahanak.Value = "0";
            }
            else
            {
                if (Convert.ToInt32(ddlkodejiwa.SelectedValue.Substring(3, 1)) > 2)
                {
                    hdnjumlahanak.Value = "2";
                }
                else
                {
                    hdnjumlahanak.Value = ddlkodejiwa.SelectedValue.Substring(3, 1);
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return hdnjumlahanak.Value;
    }
    protected string instansi()
    {
        try
        {
            txtRinstansi.Text = Convert.ToInt32(txtnopen.Text.Length) == 11 ? "Taspen" : "Asabri";
            //lbltaspenasabri.Visible = Convert.ToInt32(txtnopen.Text.Length) == 11 ? "Taspen" : "Asabri";
            if(txtnopen.Text.Length == 11)
            {
                lbltaspenasabri.Visible = true;
                lbltaspenasabri.Text = "Taspen";
            }else if(txtnopen.Text.Length == 12)
            {
                lbltaspenasabri.Visible = true;
                lbltaspenasabri.Text = "Asabri";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtangsdimuka.Text;
    }
    protected string JWdanbungaasuransi()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljangkawaktu.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.VarChar, 50, txtusiaasuransi.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGAASURANSI", typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");
            if (ge.ListDataset.Tables[0].Columns[0].ColumnName.ToString() == "jangka" || ddljangkawaktu.Items.Count < 0)
            {
                ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

                ddljangkawaktu.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddljangkawaktu.DataTextField = "jangka";
                ddljangkawaktu.DataValueField = "jangka";
                ddljangkawaktu.DataBind();
                ddljangkawaktu.Items.Insert(0, new ListItem("Pilih", ""));
            }
            else if (pnladd.Visible == true && ddljangkawaktu.SelectedValue != "")
            {
                if(txtnopen.Enabled == true)
                {
                    if (Convert.ToInt32(ddljangkawaktu.SelectedValue == "" ? "0" : ddljangkawaktu.SelectedValue) > Convert.ToInt32(txtjwmax.Text))
                    {
                        txtbungaasuransi.Text = "0";
                        txttgllahir.Text = "";
                        txttgllahir.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Jangka Waktu tidak boleh melebihi JW Max');", true);
                    }
                    if (ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString() == "" || ge.ListDataset.Tables[0].Rows.Count <= 0)
                    {
                        txtbungaasuransi.Text = "0";
                        txttgllahir.Text = "";
                        txttgllahir.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('RATE ASURANSI & RATE BUNGA TIDAK TERSEDIA');", true);
                    }
                    else
                    {
                        txtbungaasuransi.Text = string.Format("{0:N3}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                    }
                }
                else
                {
                    txtbungaasuransi.Text = string.Format("{0:N3}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtbungaasuransi.Text;
    }
    protected string bunga()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljangkawaktu.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGA", typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[5] = gb.CreateSPParameter("@KodeProduct", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodeproduk.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");
            if (ddlkodeproduk.SelectedValue != "" && ddljangkawaktu.SelectedValue != "")
            {
                if (ge.ListDataset.Tables[0].Rows.Count <= 0)
                {
                    txtbunga.Text = "0";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Data Bunga pada wilayah belum ada, silahkan hubungi Pusat');", true);
                }
                else
                {
                    txtbunga.Text = string.Format("{0:N2}" ,ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()).Substring(0, 4);
                    if (txtbunga.Text != "0" && txttittleprovisi2.Enabled == false)
                    {
                        HDNProvisi.Value = ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString();
                        txttittleprovisi2.Text = HDNProvisi.Value;
                    }
                    
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtbunga.Text;
    }
    protected string angsdimuka()
    {
        try
        {
            int hasil = (Convert.ToInt32(txttittlediblokir.SelectedValue) * Convert.ToInt32(txtangsuran.Text.Replace(".","").Replace(",00", "")));
            txtangsdimuka.Text = string.Format("{0:N2}", hasil); 
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtangsdimuka.Text;
    }
    protected string angsuranmax()
    {
        try 
        {
            int hasil = (Convert.ToInt32(string.Format("{0:N0}", Convert.ToDecimal(txtPLterimabersih.Text)).Replace(".", "").Replace(",00", "")) * 90 / 100);
            txtangsuranmax.Text = string.Format("{0:N2}", hasil); 
            plafondmax();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtangsuranmax.Text;
    }
    protected string plafondmax()
    {
        try 
        {
            if (txttgllahir.Text != "" && ddljangkawaktu.SelectedValue != "" ) 
            {
                int hitung = 0;
                decimal bunga;
                decimal am = 0;
                decimal hitungangsuranmax = decimal.Parse(txtPLterimabersih.Text) * (decimal.Parse("90") / decimal.Parse("100"));
                //am = decimal.Parse(txtangsuranmax.Text.Replace(".", "").Replace(",00", "")) * decimal.Parse(ddljangkawaktu.SelectedValue);
                am = hitungangsuranmax * decimal.Parse(ddljangkawaktu.SelectedValue);
                if (ddljenisdeviasi.SelectedValue == "" || ddljenisdeviasi.SelectedValue != "2")
                {
                    string ma = string.Format("{0:N2}", am);
                    bunga = (decimal.Parse(txtbunga.Text) / 100) * decimal.Parse(ddljangkawaktu.SelectedValue) + 1;
                    hitung = Convert.ToInt32(Math.Floor( am / bunga ));
                }
                else if (ddljenisdeviasi.SelectedValue == "2")
                {
                    bunga = (decimal.Parse(txtbungadeviasi.Text == "" ? "0" : txtbungadeviasi.Text) / decimal.Parse("100")) * decimal.Parse(ddljangkawaktu.SelectedValue) + 1 ;
                    hitung = Convert.ToInt32(Math.Floor(am / bunga));
                }
                else
                {
                    hitung = 0;
                }
                //if(AND(OR(C9=1;C9=2);E24>350000000)
                //    ;350000000;
                //if(AND(OR(C9=3;C9=4);E24>100000000);
                //100000000;
                //E24))
                if ((ddlkodeproduk.SelectedValue == "4" || ddlkodeproduk.SelectedValue == "11") && hitung > 350000000)
                {
                    hitung = 350000000;
                }
                else if ((ddlkodeproduk.SelectedValue == "1" || ddlkodeproduk.SelectedValue == "2") && hitung > 100000000)
                {
                    hitung = 100000000;
                }
                txtplafondmax.Text = string.Format("{0:N2}", hitung);
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtplafondmax.Text;
    }
    protected string angsuran()
    {
        try 
        {
            if(ddljangkawaktu.SelectedValue != "") 
            {
                bungadeviasi();
            }
        }
        catch (Exception X)
        {   
            if(X.Message != "Attempted to divide by zero.") 
            { 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
        }
        return txtangsuran.Text;
    }
    protected string bungadeviasi()
    {
        try
        {
            int hasil = 0;
            if(ddljangkawaktu.SelectedValue != "")
            {
                if (ddljenisdeviasi.SelectedValue == "" || ddljenisdeviasi.SelectedValue != "2")
                {
                    hasil = Convert.ToInt32((decimal.Parse(txtPlafond.Text) / decimal.Parse(ddljangkawaktu.SelectedValue)
                            + decimal.Parse(txtPlafond.Text) * ((decimal.Parse(txtbunga.Text == "" ? "0" : txtbunga.Text)) / decimal.Parse("100"))));
                }
                else if (ddljenisdeviasi.SelectedValue == "2")
                {
                    if(decimal.Parse(txtbunga.Text) > decimal.Parse(txtbungadeviasi.Text == "" ? "0" : txtbungadeviasi.Text))
                    {
                        string masukbunga = txtbungadeviasi.Text == "" ? "0" : txtbungadeviasi.Text;
                        hasil = Convert.ToInt32((decimal.Parse(txtPlafond.Text) / decimal.Parse(ddljangkawaktu.SelectedValue)
                                   + decimal.Parse(txtPlafond.Text) * ((decimal.Parse(masukbunga)) / decimal.Parse("100"))));
                    }
                    else if (decimal.Parse(txtbunga.Text) < decimal.Parse(txtbungadeviasi.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Deviasi Bunga tidak boleh melebihi Bunga Normal');", true);
                    }
                }
            }
            txtangsuran.Text = string.Format("{0:N2}", hasil);
        }
        catch (Exception X)
        {
            if (X.Message != "Attempted to divide by zero.")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
        }
        return txtangsuran.Text;
    }
    protected string gajitunjanak()
    {
        try
        {
            txtgajitunjanak.Text = txtPLterimabersih.Text; 
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtgajitunjanak.Text;
    }
    protected string gajinol()
    {
        try
        {
            if (decimal.Parse(txtPLgajipokok.Text) >= 1 || decimal.Parse(txtPLtunjanak.Text) >= 1 || decimal.Parse(txtPLtunjistri.Text) >= 1 || decimal.Parse(txtPLtunjberas.Text) >= 1)
            { 
                hdngajinol.Value = "1";
            }
            else
            {
                hdngajinol.Value = "0";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return hdngajinol.Value;
    }

    protected string sisagaji()
    {
        try
        {
            int hasil = 0;
            if (hdngajinol.Value == "0")
            { 
                hasil = Convert.ToInt32(decimal.Parse(txtgajibersihdisplay.Text) - decimal.Parse(txtangsuran.Text));
            }
            else
            {
                hasil = Convert.ToInt32(decimal.Parse(txtgajitunjanak.Text) - decimal.Parse(txtangsuran.Text));
            }
            txtsisagaji.Text = string.Format("{0:N2}", hasil);
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtsisagaji.Text;
    }
    protected string totalgajitunjangan()
    {
        try
        {
            //Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtPLtunjistri2.Text.Replace(".", "").Replace(",00", "")))).ToString())
            int hasil = (Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtgajibersihdisplay.Text.Replace(".", "").Replace(",00", "")))).ToString()) 
                        - (Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtPlgajipokok2.Text.Replace(".", "").Replace(",00", "")))).ToString())
                        + Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtPLtunjanak2.Text.Replace(".", "").Replace(",00", "")))).ToString())
                        + Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtPLtunjistri2.Text.Replace(".", "").Replace(",00", "")))).ToString())
                + Convert.ToInt32((Convert.ToInt32(Convert.ToDecimal(txtPLtunjberas2.Text.Replace(".", "").Replace(",00", "")))).ToString()) ));
            
            txtPLterimabersih.Text = string.Format("{0:N2}", hasil); 
            gajitunjanak();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtPLterimabersih.Text;
    }
    protected string perhitunganjangka(string tanggal)
    {
        string tahun; 
        string bulan;
        string hari;
        try
        {
            //if (ddljangkawaktu_OnChange(ddljangkawaktu, ddljangkawaktu) == true)
            //{

            //}
            
            if (ddlkodeproduk.SelectedValue != "" && ddlsumberdana.SelectedValue != "" && ddlkodejiwa.SelectedValue != "" && txttgllahir.Text != "" )
            {
                string rubahhari = "";
                string tanggalhariini = "";
                string masuk = "";
                DateTime now = new DateTime();
                if (txtnopen.Enabled == false)
                {
                    now = Convert.ToDateTime(hdntglpk.Value); 
                    DateTime ambiltanggal = Convert.ToDateTime(Convert.ToDateTime(txttgllahir.Text).ToString("dd-MM-" + Convert.ToDateTime(txttglsimulasi.Text).ToString("yyyy")));
                    TimeSpan ts1 = new TimeSpan();
                    masuk = "";
                    DateTime sekarang = DateTime.Now;
                    ts1 = ambiltanggal.Subtract(sekarang);
                    rubahhari = Convert.ToString(ts1.Days);
                }
                else
                {

                    DateTime ambiltanggal = Convert.ToDateTime(Convert.ToDateTime(txttgllahir.Text).ToString("dd-MM-" + DateTime.Now.ToString("yyyy")));
                    TimeSpan ts1 = new TimeSpan();
                    masuk = "";
                    DateTime sekarang = DateTime.Now;
                    ts1 = ambiltanggal.Subtract(sekarang);
                    rubahhari = Convert.ToString(ts1.Days);
                    DateTime now1 = Convert.ToDateTime(Convert.ToDateTime(txttgllahir.Text).AddDays(1).ToString("dd-MM-" + DateTime.Now.ToString("yyyy")));
                    string hari1 = now1.ToString("dddd", new System.Globalization.CultureInfo("id-ID"));
                    if (hari1 == "Minggu")
                    {
                        tanggalhariini = now1.AddDays(1).ToString("dd-MMMM-yyyy");
                    }
                    else if (hari1 == "Sabtu")
                    {
                        tanggalhariini = now1.AddDays(2).ToString("dd-MMMM-yyyy");
                    }
                    else
                    {
                        tanggalhariini = now1.ToString("dd-MMMM-yyyy");
                    }
                    //hari ini
                    if (sekarang.AddDays(1).ToString("dddd", new System.Globalization.CultureInfo("id-ID")) == "Minggu")
                    {
                        masuk = sekarang.AddDays(2).ToString("dd-MMMM-yyyy");
                    }
                    else if (sekarang.AddDays(1).ToString("dddd", new System.Globalization.CultureInfo("id-ID")) == "Sabtu")
                    {
                        masuk = sekarang.AddDays(3).ToString("dd-MMMM-yyyy");
                    }
                    else
                    {
                        masuk = sekarang.AddDays(1).ToString("dd-MMMM-yyyy");
                    }
                    if (Convert.ToInt32(rubahhari) < 15)
                    {
                        now = Convert.ToDateTime(tanggalhariini);
                    }
                    else
                    {
                        now = Convert.ToDateTime(masuk);
                    }
                }
   

                DateTime dob = Convert.ToDateTime(txttgllahir.Text);

                hdntglpk.Value = now.ToString("dd-MM-yyyy");

                if (now < dob)
                {
                    DateTime date3 = now;
                    now = dob;
                    dob = date3;
                }
                TimeSpan ts = now - dob;
                //Debug.WriteLine(ts.TotalDays);

                int years = 0;
                int months = 0, days = 0;
                if ((now.Month <= dob.Month) && (now.Day < dob.Day))
                {
                    years = now.Year - dob.Year - 1;
                    months = 12 - dob.Month + now.Month - 1;
                    days = DateTime.DaysInMonth(dob.Year, dob.Month) - dob.Day + now.Day;

                    if (months == 12)
                    {
                        months = 0;
                        years += 1;
                    }
                }
                else if ((now.Month <= dob.Month) && (now.Day >= dob.Day))
                {
                    years = now.Year - dob.Year - 1;
                    months = 12 - dob.Month + now.Month;
                    days = now.Day - dob.Day;
                    if (months == 12)
                    {
                        months = 0;
                        years += 1;
                    }
                }
                else if ((now.Month > dob.Month) && (now.Day < dob.Day))
                {
                    years = now.Year - dob.Year;
                    months = now.Month - dob.Month - 1;
                    days = DateTime.DaysInMonth(dob.Year, dob.Month) - dob.Day + now.Day;
                }
                else if ((now.Month > dob.Month) && (now.Day >= dob.Day))
                {
                    years = now.Year - dob.Year;
                    months = now.Month - dob.Month;
                    days = now.Day - dob.Day;
                }
                Debug.WriteLine("Years: {0}, Months: {1}, Days: {2}", years, months, days);
                tahun = years.ToString();
                bulan = months.ToString();
                hari = days.ToString(); //(days - 1).ToString();

                //int hari = ((ambilhari > ambilhariH ? ambilhari : ambilhariH) - (ambilhari < ambilhariH ? ambilhari : ambilhariH))  + 1;
                //int bulan = ambilbulan - ambilbulanH;
                //int tahun = ambiltahunH - ambiltahun;
                txttahunpengajuan.Text = tahun;
                txtbulanpengajuan.Text = bulan;
                txtharipengajuan.Text = hari;
                //txtusiaasuransi.Text = (years + 1).ToString();
                if(ddljangkawaktu.SelectedValue != "")
                {
                    txtjwtahun.Text = Math.Ceiling(Convert.ToDecimal(ddljangkawaktu.SelectedValue) / 12).ToString();
                }
                if (Convert.ToInt32(rubahhari) < 15)
                {
                    txtusiaasuransi.Text = (Convert.ToInt32(txttahunpengajuan.Text) + 1).ToString();
                }
                else 
                if (txtbulanpengajuan.Text == "0" && txtharipengajuan.Text == "0")
                {
                    txtusiaasuransi.Text = txttahunpengajuan.Text;
                }
                else
                {
                    txtusiaasuransi.Text = (Convert.ToInt32(txttahunpengajuan.Text) + 1).ToString();
                }
                string cekhari = tanggalhariini;
             }
            

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

        return "Bulan";
    }
    protected string jwmax()
    {
        try
        {
            if (txttgllahir.Text == "")
            {
                txtjwmax.Text = "0";
            }
            else
            {
                int K90, E15, J90, G90, F90, F76, F78, D78, E78, F53, D76, E76, D50;
                int K89, J89, D53, G89, F89, I88, F75, H88, D75, E75;
                E76 = 9;
                D76 = 74 * 12;
                F53 = Convert.ToInt32(txtharitempo.Text);
                E78 = Convert.ToInt32(txtbulanpengajuan.Text);
                D50 = Convert.ToInt32(txttahunpengajuan.Text);
                D78 = D50 * 12;
                F78 = D78 + E78;
                if (F53 > 0)
                {
                    F76 = D76 + E76 - 1;
                }
                else
                {
                    F76 = D76 + E76;
                }
                F90 = F76 - F78;
                if (F90 > 180)
                {
                    G90 = 180;
                }
                else
                {
                    G90 = F90;
                }
                if (G90 <= 0)
                {
                    J90 = 0;
                }
                else
                {
                    J90 = G90;
                }
                E15 = Convert.ToInt32(txtusiaasuransi.Text);
                if (J90 < 12 || E15 >= 75)
                {
                    K90 = 0;
                }
                else
                {
                    K90 = J90;
                }

                E75 = 9;
                D75 = 79 * 12;
                H88 = (80 - E15) * 12;
                if (F53 > 0)
                {
                    F75 = D75 + E75 - 1;
                }
                else
                {
                    F75 = D75 + E75;
                }
                if (H88 > 60)
                {
                    I88 = 60;
                }
                else
                {
                    I88 = H88;
                }
                F89 = F75 - F78;
                if (F89 >= I88)
                {
                    G89 = I88;
                }
                else
                {
                    G89 = F89;
                }
                if (G89 <= 0)
                {
                    J89 = 0;
                }
                else
                {
                    J89 = G89;
                }
                D53 = Convert.ToInt32(txttahuntempo.Text);
                if (E15 <= 70 && (J89 < 12 || D53 < 75))
                {
                    K89 = 0;
                }
                else
                {
                    K89 = J89;
                }
                if (ddlkodeproduk.SelectedValue == "11" || ddlkodeproduk.SelectedValue == "4")
                {
                    txtjwmax.Text = K90.ToString();
                }
                else if (ddlkodeproduk.SelectedValue == "1" || ddlkodeproduk.SelectedValue == "2")
                {
                    txtjwmax.Text = K89.ToString();
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

        return txtjwmax.Text;
    }
    protected string usiaasuransi()
    {
        try
        {
            string usiaasuransi = txtusiaasuransi.Text;
            if (ddlkodeproduk.SelectedValue !="" && ddlsumberdana.SelectedValue != "" && ddlkodejiwa.SelectedValue != "" && txttgllahir.Text != "" && ddljangkawaktu.SelectedValue != "" ) 
            {
                if ((ddlkodeproduk.SelectedValue == "1" || ddlkodeproduk.SelectedValue == "2") && Convert.ToInt32(txttahuntempo.Text) < 75 
                     && txttgllahir.Text != "" && ddljangkawaktu.SelectedValue != "")
                {
                    txttgllahir.Text = "";
                    usiapengajuannull();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('UJT < 75 Tahun UBAH KODE PRODUK KE Reguler');", true);
                }
                else if(Convert.ToInt32(txttahuntempo.Text) > 75 &&
                  (ddlkodeproduk.SelectedValue == "4" || ddlkodeproduk.SelectedValue == "11")
                  && txttgllahir.Text != "" && ddljangkawaktu.SelectedValue != "")
                {
                    txttgllahir.Text = "";
                    usiapengajuannull();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('UJT > 75 Tahun UBAH KODE PRODUK KE Platinum');", true);
                }
                else if (Convert.ToInt32(txtusiaasuransi.Text) >= 80 || Convert.ToInt32(txtusiaasuransi.Text) < 40)
                {
                    txttgllahir.Text = "";
                    usiapengajuannull();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('USIA DEBITUR DILUAR KETENTUAN');", true);
                }
                else if ((ddlkodeproduk.SelectedValue == "11" || ddlkodeproduk.SelectedValue == "4") && ((74 * 12) + 9) -
                        (Convert.ToInt32(txttahunpengajuan.Text) > 0 ? ((Convert.ToInt32(txttahunpengajuan.Text) * 12) +
                        Convert.ToInt32(txtbulanpengajuan.Text)) + 1 : (Convert.ToInt32(txttahunpengajuan.Text) * 12)) < 12)
                {
                    txttgllahir.Text = "";
                    usiapengajuannull();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('USIA DEBITUR DILUAR KETENTUAN');", true);
                }
                //(C9=3;C9=4);G75-G78<12);
                else if ((ddlkodeproduk.SelectedValue == "1" || ddlkodeproduk.SelectedValue == "2") &&  (( 79 * 12 ) +  9) -
                         (Convert.ToInt32(txtharipengajuan.Text) > 0 ? (((Convert.ToInt32(txttahunpengajuan.Text) * 12) +
                        Convert.ToInt32(txtbulanpengajuan.Text)) + 1) : (Convert.ToInt32(txttahunpengajuan.Text) * 12)) < 12)
                {

                    txttgllahir.Text = "";
                    usiapengajuannull();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('USIA DEBITUR DILUAR KETENTUAN');", true);
                }
                else if (Convert.ToInt32(txtusiaasuransi.Text) < 55 && ddlpensiun.SelectedValue == "1")
                {
                    txttgllahir.Text = "";
                    usiapengajuannull();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('USIA DEBITUR DILUAR KETENTUAN');", true);
                }
                else if (Convert.ToInt32(txtusiaasuransi.Text) < 60 && ddlpensiun.SelectedValue == "2")
                {
                    txttgllahir.Text = "";
                    usiapengajuannull();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('USIA DEBITUR DILUAR KETENTUAN');", true);
                }
                else if (Convert.ToInt32(txtusiaasuransi.Text) < 70 && ddlpensiun.SelectedValue == "3")
                {
                    txttgllahir.Text = "";
                    usiapengajuannull();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('USIA DEBITUR DILUAR KETENTUAN');", true);
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtusiaasuransi.Text;
    }
    protected string perhitungantempo()
    {
        int dapetbulan;
        int dapettahun;
        try
        {
            if(ddljangkawaktu.SelectedValue != "")
            {
                decimal jangkawaktuup = Math.Ceiling(Convert.ToDecimal(ddljangkawaktu.SelectedValue == "" ? "0" : ddljangkawaktu.SelectedValue) / 12);
                decimal jangkawaktudown = Math.Floor(Convert.ToDecimal(ddljangkawaktu.SelectedValue == "" ? "0" : ddljangkawaktu.SelectedValue) / 12);
                int ambiljangka = Convert.ToInt32(ddljangkawaktu.SelectedValue == "" ? "0" : ddljangkawaktu.SelectedValue);
                int masuk = (12 * Convert.ToInt32(jangkawaktuup)) - (12 * Convert.ToInt32(jangkawaktudown)) - (12 * Convert.ToInt32(jangkawaktuup) - ambiljangka);
                int rubahtempobulan = Convert.ToInt32(txtbulanpengajuan.Text) + masuk;
                if (rubahtempobulan > 11)
                {
                    dapetbulan = rubahtempobulan - 12;
                }
                else
                {
                    dapetbulan = rubahtempobulan;
                }
                if (rubahtempobulan >= 12)
                {
                    dapettahun = Convert.ToInt32(txttahunpengajuan.Text) + Convert.ToInt32(jangkawaktudown) + 1;
                }
                else
                {
                    dapettahun = Convert.ToInt32(txttahunpengajuan.Text) + Convert.ToInt32(jangkawaktudown);
                }
                txttahuntempo.Text = dapettahun.ToString();
                txtbulantempo.Text = dapetbulan.ToString();//Convert.ToString(months);
                txtharitempo.Text = txtharipengajuan.Text;
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtharitempo.Text;
    }
    protected string deviasiasuransi()
    {
        try
        {
            int hasil = 0 ;
            if (ddljenisdeviasi.SelectedValue == "0" || ddljenisdeviasi.SelectedValue != "3"  )
            {
                hasil = (Convert.ToInt32((Convert.ToDecimal(txtbungaasuransi.Text) / 100) * Convert.ToInt32(txtPlafond.Text.Replace(".", "").Replace(",00", ""))));
                txtdeviasiasuransi.Text = "";
                //txtdeviasiasuransi
                txtRtitleasuransi.Text = txtbungaasuransi.Text;
            }
            else if(ddljenisdeviasi.SelectedValue == "3")
            {
                hasil = (Convert.ToInt32((decimal.Parse(txtdeviasiasuransi.Text == "" ? "0" : txtdeviasiasuransi.Text) / 100) * decimal.Parse(txtPlafond.Text)));

                txtRtitleasuransi.Text = txtdeviasiasuransi.Text;
            }
            txtasuransi.Text = string.Format("{0:N2}", hasil);
            txtRasuransi.Text = txtasuransi.Text;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtasuransi.Text;
    }
    protected string deviasikeanggotaan()
    {
        try
        {
            txtdeviasikeanggotaan.Text = string.Format("{0:N2}", 20000);//1000 + 1000);
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtdeviasikeanggotaan.Text;
    }
    protected string deviasiprovisi()
    {
        try
        {
            if(txtnopen.Enabled == true)
            {
                bunga();
            }
            //decimal asdasd = decimal.Parse(4 / 100) / decimal.Parse("100");
            if (ddljenisdeviasi.SelectedValue == "" || ddljenisdeviasi.SelectedValue != "4")
            {
                txttittleprovisi2.Text = HDNProvisi.Value;
                decimal sadasd = decimal.Parse("5") / decimal.Parse("100");
                decimal hitungbunga = decimal.Parse(txttittleprovisi2.Text == "" ? "0" : HDNProvisi.Value.Replace(",00", "").Replace(".00", "")) / decimal.Parse("100") ;
                txtprovisi.Text = string.Format("{0:n2}", Convert.ToInt32(decimal.Parse(txtPlafond.Text) * hitungbunga));
                txttittleprovisi2.Text = "";
                txtRtittleprovisi.Text = HDNProvisi.Value;
            }
            else if (ddljenisdeviasi.SelectedValue == "4")
            {
                txtprovisi.Text = string.Format("{0:n2}", Convert.ToInt32((decimal.Parse(txttittleprovisi2.Text == "" ? "0" : txttittleprovisi2.Text) / decimal.Parse("100"))
                    * decimal.Parse(txtPlafond.Text)));
            }
            txtRprovisi.Text = txtprovisi.Text;

            txtRtittleprovisi.Text = txttittleprovisi2.Text == "" ? HDNProvisi.Value : txttittleprovisi2.Text;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtprovisi.Text;
    }
    protected string deviasiangsblokir()
    {
        try
        {
            int hasil = 0;
            if (ddljenisdeviasi.SelectedValue == "" || ddljenisdeviasi.SelectedValue != "1")
            {
                txttittlediblokir.SelectedValue = "1";
                hasil = Convert.ToInt32(decimal.Parse(txttittlediblokir.SelectedValue) * decimal.Parse(txtangsuran.Text));
                ddlangsblokir.SelectedValue = txttittlediblokir.SelectedValue;
                //txtangsblokir.Text = string.Format("{0:N2}", hasil);
            }
            else if (ddljenisdeviasi.SelectedValue == "1" )
            {
                hasil = Convert.ToInt32(decimal.Parse(txttittlediblokir.SelectedValue) * decimal.Parse(txtangsuran.Text));
                ddlangsblokir.SelectedValue = txttittlediblokir.SelectedValue;
            }
            txtangsblokir.Text = string.Format("{0:N2}", hasil);
            txtRangsblokir.Text = txtangsblokir.Text;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtangsblokir.Text;
    }
    protected string deviasidimuka()
    {
        try
        {
            int hasil = 0;
            if (ddljenisdeviasi.SelectedValue == "" || ddljenisdeviasi.SelectedValue != "5")
            {
                txttittledimuka.SelectedValue = "3";
                hasil = Convert.ToInt32(decimal.Parse(txttittledimuka.SelectedValue) * decimal.Parse(txtangsuran.Text));
                ddlangsdimuka.SelectedValue = txttittledimuka.SelectedValue;
                //txtan09gsblokir.Text = string.Format("{0:N2}", hasil);
            }
            else if (ddljenisdeviasi.SelectedValue == "5")
            {
                hasil = Convert.ToInt32(decimal.Parse(txttittledimuka.SelectedValue) * decimal.Parse(txtangsuran.Text));
                ddlangsdimuka.SelectedValue = txttittledimuka.SelectedValue;
            }
            txtangsdimuka.Text = string.Format("{0:N2}", hasil);
            txtRangsdimuka.Text = txtangsdimuka.Text;

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtangsdimuka.Text;
    }
    protected string deviasibunga()
    {
        try
        {
            int hasil = 0;
            if (ddljangkawaktu.SelectedValue != "")
            {
                if (ddljenisdeviasi.SelectedValue == "" || ddljenisdeviasi.SelectedValue != "2")
                {
                    hasil = Convert.ToInt32((decimal.Parse(txtPlafond.Text) / decimal.Parse(ddljangkawaktu.SelectedValue)
                            + decimal.Parse(txtPlafond.Text) * ((decimal.Parse(txtbunga.Text == "" ? "0" : txtbunga.Text)) / decimal.Parse("100"))));
                }
                else if (ddljenisdeviasi.SelectedValue == "2")
                {
                    if (decimal.Parse(txtbunga.Text) > decimal.Parse(txtbungadeviasi.Text == "" ? "0" : txtbungadeviasi.Text))
                    {
                        string masukbunga = txtbungadeviasi.Text == "" ? "0" : txtbungadeviasi.Text;
                        hasil = Convert.ToInt32((decimal.Parse(txtPlafond.Text) / decimal.Parse(ddljangkawaktu.SelectedValue)
                                   + decimal.Parse(txtPlafond.Text) * ((decimal.Parse(masukbunga)) / decimal.Parse("100"))));
                    }
                    else if (decimal.Parse(txtbunga.Text) < decimal.Parse(txtbungadeviasi.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Deviasi Bunga tidak boleh melebihi Bunga Normal');", true);
                    }
                }
            }else if(ddljangkawaktu.SelectedValue == "")
            {
                txtbungadeviasi.Text = "";
            }
            txtangsuran.Text = string.Format("{0:N2}", hasil);
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtangsuran.Text;
    }
    protected string potongan()
    {
        try
        {
            int asuransi = txtRasuransi.Text == "" ? 0 : Convert.ToInt32(Convert.ToDecimal(txtRasuransi.Text.Replace(".", "").Replace(",00", "")));
            int provisi = txtRprovisi.Text == "" ? 0 : Convert.ToInt32(Convert.ToDecimal(txtRprovisi.Text.Replace(".", "").Replace(",00", "")));
            int keanggotaan = txtRkeanggotaan.Text == "" ? 0 : Convert.ToInt32(Convert.ToDecimal(txtRkeanggotaan.Text.Replace(".", "").Replace(",00", "")));
            int angsblokir = txtRangsblokir.Text == "" ? 0 : Convert.ToInt32(Convert.ToDecimal(txtRangsblokir.Text.Replace(".", "").Replace(",00", "")));
            int angsdimuka = txtRangsdimuka.Text == "" ? 0 : Convert.ToInt32(Convert.ToDecimal(txtRangsdimuka.Text.Replace(".", "").Replace(",00", "")));
            int pelunasanTO = txtRpelunasanto.Text == "" ? 0 : Convert.ToInt32(txtRpelunasanto.Text.Replace(".", "").Replace(",00", ""));
            int hasil = (asuransi + provisi + keanggotaan + angsblokir + angsdimuka + pelunasanTO);
            txtRpotongan.Text = string.Format("{0:N2}", hasil);
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtRpotongan.Text;
    }
    protected string terimabersih()
    {
        try
        {
            //D23 - D32 - D34 - D36 - D38 - D40 - D42
            int plafond = txtRplafond.Text == "" ? 0 : Convert.ToInt32(txtRplafond.Text.Replace(".", "").Replace(",00", ""));
            int asuransi = txtRasuransi.Text == "" ? 0 : Convert.ToInt32(txtRasuransi.Text.Replace(".", "").Replace(",00", ""));
            int provisi = txtRprovisi.Text == "" ? 0 : Convert.ToInt32(txtRprovisi.Text.Replace(".", "").Replace(",00", ""));
            int keanggotaan = txtRkeanggotaan.Text == "" ? 0 : Convert.ToInt32(txtRkeanggotaan.Text.Replace(".", "").Replace(",00", ""));
            int angsblokir = txtRangsblokir.Text == "" ? 0 : Convert.ToInt32(txtRangsblokir.Text.Replace(".", "").Replace(",00", ""));
            int angsdimuka = txtRangsdimuka.Text == "" ? 0 : Convert.ToInt32(txtRangsdimuka.Text.Replace(".", "").Replace(",00", ""));
            int pelunasanTO = txtRpelunasanto.Text == "" ? 0 : Convert.ToInt32(txtRpelunasanto.Text.Replace(".", "").Replace(",00", ""));
            int hasil = 0;
            hasil = (plafond - asuransi - provisi - keanggotaan - angsblokir - angsdimuka - pelunasanTO);
            txtterimabersih.Text = string.Format("{0:N2}", hasil);
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return txtterimabersih.Text;
    }
    protected void usiapengajuannull()
    { 
        try
        {
            txttahunpengajuan.Text = "0";
            txtbulanpengajuan.Text = "0";
            txtharipengajuan.Text = "0";
            txttahuntempo.Text = "0";
            txtbulantempo.Text = "0";
            txtharitempo.Text = "0";
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void pengurangantunjanganull()
    {
        try
        {
            totalgajitunjangan();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    public void InsertCombo()
    {
        // --- Combo Kode Produk
        ge.ListDataset = gb.GetDataFunction("dbo.Getkpprogrampinjaman()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode desc";

        ddlkodeproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkodeproduk.DataTextField = "program";
        ddlkodeproduk.DataValueField = "kode";
        ddlkodeproduk.DataBind();
        ddlkodeproduk.Items.Insert(0, new ListItem("Pilih Kode Product", ""));

        //---Combo Kode Jiwa 
        ge.ListDataset = gb.GetDataFunction("dbo.Getmasterkodejiwa()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_jiwa asc";

        ddlkodejiwa.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkodejiwa.DataTextField = "kdjiwa";
        ddlkodejiwa.DataValueField = "kode_jiwa";
        ddlkodejiwa.DataBind();
        ddlkodejiwa.Items.Insert(0, new ListItem("Pilih Kode Jiwa", ""));

        // --- Combo Jenis Deviasi
        ge.ListDataset = gb.GetDataFunction("dbo.GetJenisDeviasi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJenis asc";

        ddljenisdeviasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljenisdeviasi.DataTextField = "NamaJenis";
        ddljenisdeviasi.DataValueField = "KodeJenis";
        ddljenisdeviasi.DataBind();
        ddljenisdeviasi.Items.Insert(0, new ListItem("Pilih Type", ""));

        // --- Combo Sumberdana
        ge.ListDataset = gb.GetDataFunction("dbo.getSdMst()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NamaBank asc";

        ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberdana.DataTextField = "NamaBank";
        ddlsumberdana.DataValueField = "KodeBank";
        ddlsumberdana.DataBind();
        ddlsumberdana.Items.Insert(0, new ListItem("Pilih Sumber Dana", ""));
    }

    #endregion

}

