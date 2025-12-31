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

public partial class MI_CreateMINew : System.Web.UI.Page
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
                //txtalamat.Attributes.Add("maxlength", txtalamat.MaxLength.ToString());
                BindGridEntity();
                pnladd.Visible = false;
                //divnemo.Visible = true;
                InsertCombo();
                //GetAutoIdNopen();
                //string masuknopen = HDNopen.Value;
                //pnlFilter.Visible = true;
                pnlHeader.Visible = true;
                PnlFront.Visible = true;
                //lblnopenlama.Visible = false;
                //lblnopenbaru.Visible = false;
             
                //idtotalpending.InnerText = DataPending(SessKdpersh);
                //idjudultotal.Visible = true;
                divtotalrequest.Visible = true;
             
                txttglsimulasi.Text = DateTime.Now.ToString("dd-MMMM-yyyy");

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txtmtpensiun.Text = xtanggal.ToString("dd-MM-yyyy");
                txttglsimulasi.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttglsimulasi.Text = DateTime.Now.ToString("dd-MMMM-yyyy");

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

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTALL", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoSimulasi desc";

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
        //switch (e.CommandName)
        //{

        //    case "SET":

        //        try
        //        {
        //            //fungsiAll();

        //            pnlHeader.Visible = false;
        //            PnlFront.Visible = false;
        //            pnladd.Visible = true;
        //            pnlData.Visible = false;

        //            pnlHeader.Visible = false;
        //            PnlFront.Visible = false;
        //            pnladd.Visible = true;
        //            //pnldatapemohon.Visible = true;
        //            //Pnldepan.Visible = true;
        //            ////divhasilsimulasi.Visible = true;
        //            //btncalculate.Visible = false;
        //            //divsimpan.Visible = true;
        //            //btnrequest.Visible = false;
        //            //btnrequest.Visible = false;
        //            txtnosimulasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;
        //            txtnopen.Enabled = false;
        //            //txtusiaasuransi.Text = "0";
        //            //JWdanbungaasuransi();
        //            ddlsumberdana.Enabled = false;
        //            ddlstatustakeover.Enabled = false;
        //            ddlkodeproduk.Enabled = false;
        //            ddlpensiun.Enabled = false;
        //            ddlstatussk.Enabled = false;
        //            ddlstatusnikah.Enabled = false;
        //            ddlkodejiwa.Enabled = false;
        //            ddljenisdeviasi.Enabled = false;
        //            ddljangkawaktu.Enabled = false;
        //            txttgllahir.Enabled = false;
        //            txtnopen.Enabled = false;
        //            txtnamapensiun.Enabled = false;
        //            txtgajibersihdisplay.Enabled = false;
        //            txtgajipokok.Enabled = false;
        //            txttunjangananak.Enabled = false;
        //            txtPlafond.Enabled = false;
                   




        //            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        //            var[0] = gb2.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnosimulasi.Text, typeof(string));
        //            var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
        //            var[2] = gb2.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //            var[3] = gb2.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    
        //            ge2.ListDataset = gb2.GetDataJustOutPut("SI_Request", var, "constrmf");

        //            hdntglpk.Value = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglPk"].ToString()).ToString("dd-MMMM-yyyy");
        //            ddlkodeproduk.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeProduct"].ToString();
        //            ddlsumberdana.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeSumberDana"].ToString();
        //            ddlstatustakeover.SelectedItem.Text = ge2.ListDataset.Tables[0].Rows[0]["StatusTakeOver"].ToString();
        //            //data
        //            txtnopen.Text = ge2.ListDataset.Tables[0].Rows[0]["nopen"].ToString();
        //            txtnamapensiun.Text = ge2.ListDataset.Tables[0].Rows[0]["NamaDebitur"].ToString();
        //            ddlkodejiwa.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["KodeJiwa"].ToString();
        //            ddlstatusnikah.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["Status"].ToString();
        //            ddlpensiun.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["StatusPensiun"].ToString();
        //            ddlstatussk.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["Status_Sk_diterima"].ToString();
        //            txttgllahir.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglLahir"].ToString()).ToString("dd-MMMM-yyyy");
        //            txtgajibersihdisplay.Text =string.Format("{0:N2}",  Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["GajiBersihDisplay"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtgajipokok.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["GajiPokok"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            //PL
        //            txtPLgajipokok.Text = string.Format("{0:N0}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTGajiPokok"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLgajipokok1.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTGajiPokok1"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPlgajipokok2.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTGajiPokokJumlah"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //           txtPLtunjanak.Text = ge2.ListDataset.Tables[0].Rows[0]["JmlAnak"].ToString();
        //                                  //string.Format("{0:N0}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["JmlAnak"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", ""))) :
        //                                  //string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["JmlAnak"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLtunjanak1.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjAnak1"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLtunjanak2.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjAnakJumlah"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLtunjistri.Text = string.Format("{0:N0}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjIstri"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLtunjistri1.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjIstri1"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLtunjistri2.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjIstriJumlah"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLtunjberas.Text = string.Format("{0:N0}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjBeras"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLtunjberas1.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjBeras1"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLtunjberas2.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTunjBerasJumlah"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtPLterimabersih.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PTTGajiTunjangan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));

        //            txttgllahiranak1.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglAnak1"].ToString()).ToString("dd-MMMM-yyyy");
        //            txttgllahiranak2.Text = Convert.ToDateTime(ge2.ListDataset.Tables[0].Rows[0]["TglAnak2"].ToString()).ToString("dd-MMMM-yyyy");

        //            txttahunanak1.Text = ge2.ListDataset.Tables[0].Rows[0]["TahunAnak1"].ToString();
        //            txttahunanak2.Text = ge2.ListDataset.Tables[0].Rows[0]["TahunAnak2"].ToString();


        //            txttunjangananak.Text = ge2.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));
        //            ddljangkawaktu.Items.Insert(0, new ListItem(ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString(), ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString()));
        //            txtjwmax.Text = ge2.ListDataset.Tables[0].Rows[0]["jwmax"].ToString();
        //            txtjwtahun.Text = (Convert.ToInt32(ge2.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString()) /12).ToString();
        //            txtbungaasuransi.Text = ge2.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString();
        //            txtPlafond.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Plafond"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtplafondmax.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtbunga.Text = string.Format("{0:N4}", decimal.Parse(ge2.ListDataset.Tables[0].Rows[0]["bunga"].ToString()));
        //            txtusiaasuransi.Text = ge2.ListDataset.Tables[0].Rows[0]["UsiaAsuransi"].ToString(); 
        //            txtangsuran.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));

        //            txtangsuranmax.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AngsuranMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
        //            txtpelunasantakeover.Text = ge2.ListDataset.Tables[0].Rows[0]["PelunasanTO"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["PelunasanTO"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));

        //            txttahunpengajuan.Text = ge2.ListDataset.Tables[0].Rows[0]["PengajuanTahun"].ToString();
        //            txtbulanpengajuan.Text = ge2.ListDataset.Tables[0].Rows[0]["PengajuanBulan"].ToString();
        //            txtharipengajuan.Text = ge2.ListDataset.Tables[0].Rows[0]["PengajuanHari"].ToString();
        //            txttahuntempo.Text = ge2.ListDataset.Tables[0].Rows[0]["TempoTahun"].ToString();
        //            txtbulantempo.Text = ge2.ListDataset.Tables[0].Rows[0]["TempoBulan"].ToString();
        //            txtharitempo.Text = ge2.ListDataset.Tables[0].Rows[0]["TempoHari"].ToString();

        //            txtketerangan1.Text = ge2.ListDataset.Tables[0].Rows[0]["Keterangan1"].ToString();
        //            txtketerangan2.Text = ge2.ListDataset.Tables[0].Rows[0]["Keterangan2"].ToString();
        //            txtketerangan3.Text = ge2.ListDataset.Tables[0].Rows[0]["Keterangan3"].ToString();
        //            txtcatatan1.Text = ge2.ListDataset.Tables[0].Rows[0]["Catatan1"].ToString();
        //            txtcatatan2.Text = ge2.ListDataset.Tables[0].Rows[0]["Catatan2"].ToString();
        //            txtcatatan3.Text = ge2.ListDataset.Tables[0].Rows[0]["Catatan3"].ToString();


        //            InsertCombo();
        //            ddljenisdeviasi.Items.Insert(0, new ListItem("Pilih Type", ""));
        //            ddljenisdeviasi.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["StatusDeviasi"].ToString() == "No" ? "" : ge2.ListDataset.Tables[0].Rows[0]["StatusDeviasi"].ToString();
        //            txttittlediblokir.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["DeviasiAngsBlokir"].ToString();
        //            txtbungadeviasi.Text =decimal.Parse(ge2.ListDataset.Tables[0].Rows[0]["DeviasiBunga"].ToString()) == 0 ? "" : ge2.ListDataset.Tables[0].Rows[0]["DeviasiBunga"].ToString();
        //            txtdeviasiasuransi.Text = decimal.Parse(ge2.ListDataset.Tables[0].Rows[0]["DeviasiAsuransi"].ToString()) == 0 ? "" : ge2.ListDataset.Tables[0].Rows[0]["DeviasiAsuransi"].ToString();
        //            txttittleprovisi2.Text = decimal.Parse(ge2.ListDataset.Tables[0].Rows[0]["DeviasiProvisi"].ToString()) == 0 ? "" : ge2.ListDataset.Tables[0].Rows[0]["DeviasiProvisi"].ToString();
        //            txttittledimuka.SelectedValue = ge2.ListDataset.Tables[0].Rows[0]["DeviasiAngsMuka"].ToString();
        //            txtangsblokir.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AngsBlokir"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
        //            txtangsdimuka.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["AngsDimuka"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
        //            txtRinstansi.Text = ge2.ListDataset.Tables[0].Rows[0]["Instansi"].ToString();

        //            txtRnamadebitur.Text = txtnamapensiun.Text;
        //            txtRto.Text = ddlstatustakeover.SelectedItem.Text;
        //            txtRdeviasi.Text = ddljenisdeviasi.SelectedValue.ToString() == "" ? "Tidak Ada" : ddljenisdeviasi.SelectedItem.Text;
        //            txtRplafond.Text = txtPlafond.Text;
        //            txtRpotongan.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Potongan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
        //            txtRasuransi.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Asuransi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
        //            txtRprovisi.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Provisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
        //            //txtRtitleasuransi.Text = txtdeviasiasuransi.Text == "" ? txtbungaasuransi.Text : txttittleprovisi2.Text;

        //            if (txtdeviasiasuransi.Text != "")
        //            {
        //                txtRtitleasuransi.Text = txtdeviasiasuransi.Text;

        //            }
        //            else
        //            {
        //                txtRtitleasuransi.Text = txtdeviasiasuransi.Text == "" ? txtbungaasuransi.Text : txttittleprovisi2.Text;

        //            }

        //            if (txttittleprovisi2.Text != "")
        //            {
        //                txtRtittleprovisi.Text = txttittleprovisi2.Text;

        //            }
        //            else
        //            {
        //                 txtRtittleprovisi.Text = ge2.ListDataset.Tables[0].Rows[0]["RateProvisi"].ToString();

        //            }

        //            //txtRtitleasuransi.Text = string.Format("{0:N4}", decimal.Parse(ge2.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString()));
        //            //txtRtittleprovisi.Text = ge2.ListDataset.Tables[0].Rows[0]["RateProvisi"].ToString();
                                        
        //            txtRkeanggotaan.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["Keanggotaan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
        //            ddlangsblokir.SelectedValue = txttittlediblokir.SelectedValue;
        //            ddlangsdimuka.SelectedValue = txttittledimuka.SelectedValue;
        //            txtRangsblokir.Text = txtangsblokir.Text;
        //            txtRangsdimuka.Text = txtangsdimuka.Text;
        //            txtRpelunasanto.Text = txtpelunasantakeover.Text;
                    
        //            txtterimabersih.Text = string.Format("{0:N2}", Convert.ToInt32((ge2.ListDataset.Tables[0].Rows[0]["TerimaBersih"].ToString()).ToString().Replace(".00", "").Replace(",0000", "")));
        //            txtsisagaji.Text = string.Format("{0:N2}", Convert.ToInt32(decimal.Parse(txtPLterimabersih.Text) - decimal.Parse(txtangsuran.Text)));
        //            ddlkodeproduk.Enabled = false;
        //            ddlsumberdana.Enabled = false;
        //            txtnamapensiun.Enabled = false;
        //            ddlkodejiwa.Enabled = false;
        //            ddlstatusnikah.Enabled = false;
        //            ddlpensiun.Enabled = false;
        //            ddlstatussk.Enabled = false;
        //            txttgllahir.Enabled = false;
        //            txtgajibersihdisplay.Enabled = false;
        //            txtgajipokok.Enabled = false;
        //            txttunjangananak.Enabled = false;
        //            txtusiaasuransi.Enabled = false;
        //            ddljangkawaktu.Enabled = false;
        //            txtPlafond.Enabled = false;
        //            txtpelunasantakeover.Enabled = false;
        //            txtdeviasiasuransi.Enabled = false;
        //            txtbungadeviasi.Enabled = false;
        //            txtbungadeviasi.Text = ddljenisdeviasi.SelectedValue != "2" ? "" : txtbungadeviasi.Text;
        //            txttittleprovisi2.Enabled = false;
        //            txttittlediblokir.Enabled = false;
        //            txttittledimuka.Enabled = false;
        //            //txttittlediblokir.SelectedValue = ddljenisdeviasi.SelectedValue != "1" ? "" : txttittlediblokir.Text;
        //            //txttittledimuka.SelectedValue = ddljenisdeviasi.SelectedValue != "5" ? "" : txttittledimuka.Text;
        //            txtsisagaji.Text = string.Format("{0:N2}", Convert.ToInt32(decimal.Parse(txtPLterimabersih.Text) - decimal.Parse(txtangsuran.Text)));
        //            if (txtdeviasiasuransi.Text == "")
        //            {
        //                txtdeviasiasuransi.Text = "0";
        //            }
        //            if (txtbungadeviasi.Text == "")
        //            {
        //                txtbungadeviasi.Text = "0";
        //            }
        //            if (txttittleprovisi2.Text == "")
        //            {
        //                txttittleprovisi2.Text = "0";
        //            }

        //        }
        //        catch (Exception X)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        //            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //        }
        //        break;
        //    case "DEL":

        //        try
        //        {
        //            //fungsiAll();
        //            string xstatus= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;


        //            if (xstatus != "1")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data sudah proses Approval tidak bisa di hapus');", true);
        //            }
        //            else
        //            {

        //                txtnosimulasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;

                    
        //            //JWdanbungaasuransi();
        //            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        //            var[0] = gb2.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnosimulasi.Text, typeof(string));
        //            var[1] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
        //            var[2] = gb2.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //            var[3] = gb2.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        //            ge2.ListDataset = gb2.GetDataJustOutPut("SI_Request", var, "constrmf");


        //            if (ge2.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        //            {

        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);

        //            }

        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
        //                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //            }

        //            }

        //        }
        //        catch (Exception X)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        //            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //        }
        //        break;
        //}
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (ddlsumberdana.SelectedValue == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Sumber Dana Tidak Boleh Kosong');", true);
        //    }
        //    else if (txtnopen.Text == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Nomor Pensiun Tidak Boleh Kosong');", true);
        //    }
        //    else if (Convert.ToInt32(txtgajibersihdisplay.Text.Replace(".", "").Replace(",00", "")) == 0 || txtgajibersihdisplay.Text == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Gaji Bersih Display Tidak Boleh Kosong');", true);
        //    }
        //    else if (Convert.ToInt32(txtgajipokok.Text.Replace(".", "").Replace(",00", "")) == 0 || txtgajipokok.Text == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Gaji Pokok Tidak Boleh Kosong');", true);
        //    }
        //    else if (Convert.ToInt32(txtPlafond.Text.Replace(".", "").Replace(",00", "")) == 0 || txtPlafond.Text == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Plafond Tidak Boleh Kosong');", true);
        //    }
        //    else if (ddljangkawaktu.SelectedValue == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Jangka Waktu Tidak Boleh Kosong');", true);
        //    }
        //    else if (txttgllahir.Text == "" || txttahunpengajuan.Text == "0")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Tanggal Lahir Tidak Boleh Kosong');", true);
        //    }
        //    else if (ddlstatusnikah.SelectedValue == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Salah Satu status Anda');", true);
        //    }
        //    else if (ddlpensiun.SelectedValue == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Salah Satu Status Pensiun Anda');", true);
        //    }
        //    else if (ddlstatussk.SelectedValue == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Salah Satu Status SK Anda');", true);
        //    }
        //    else if (txtnamapensiun.Text == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Masukan Nama Anda sesuai KTP');", true);
        //    }
        //    else if (ddlstatustakeover.SelectedValue == "1" && (txtpelunasantakeover.Text == "0" || txtpelunasantakeover.Text == "0,00" || txtpelunasantakeover.Text == ""))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pelunasan Take Over Tidak Boleh kosong atau 0');", true);
        //    }
        //    else if (txttgllahiranak1.Enabled == true && txttgllahiranak2.Enabled == true && Convert.ToDateTime(txttgllahiranak2.Text) < Convert.ToDateTime(txttgllahiranak1.Text))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Tanggal Lahir anak Tidak Bisa melebihi hari ini atau Tanggal Lahir anak pertama..!!!');", true);
        //    }
        //    else if (txtcatatan1.Text == "KODE JIWA SALAH"
        //            && ddlkodejiwa.SelectedValue != "" && ddlstatusnikah.SelectedValue != "" && ddlpensiun.SelectedValue != "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan rubah Kode Jiwa atau Status atau Status Pensiun Anda ');", true);
        //    }
        //    else if (ddlstatussk.SelectedValue == "")
        //    {
        //        ddlstatussk.Focus();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Pilih Status SK..!!!');", true);
        //    }
        //    else
        //    {
        //        //string className = divcalculate.Attributes["class"].ToString();
        //        //divcalculate.Attributes.Add("class", divcalculate.Attributes["class"].ToString().Replace("form-group col-md-6", "form-group col-md-12"));
        //        Pnldepan.Visible = false;
        //        divhasilsimulasi.Visible = true;
        //        divsimpan.Visible = true;
        //        bunga();
        //        fungsiAll();
        //        txtRnamadebitur.Text = txtnamapensiun.Text;
        //        //txtRinstansi.Text = txtPensiunan.Text;
        //        txtRto.Text = ddlstatustakeover.SelectedItem.Text;
        //        txtRdeviasi.Text = ddljenisdeviasi.SelectedValue.ToString() == "" ? "Tidak Ada" : ddljenisdeviasi.SelectedItem.Text;
        //        txtRplafond.Text = txtPlafond.Text;
        //        //(ddljenisdeviasi.SelectedValue == "3" ? asuransi : 0) -
        //        //        (ddljenisdeviasi.SelectedValue == "4" ? provisi : 0) -
        //        //        (ddljenisdeviasi.SelectedValue != "" ? keanggotaan : 0) -
        //        //        (ddljenisdeviasi.SelectedValue == "1" ? angsblokir : 0) -
        //        //        (ddljenisdeviasi.SelectedValue == "5" ? angsdimuka : 0) -
        //        //        (ddlstatustakeover.SelectedValue == "1" ? pelunasanTO : 0) 

        //        int asuransi = txtasuransi.Text == "" ? 0 : Convert.ToInt32(txtasuransi.Text.Replace(".", "").Replace(",00", ""));
        //        int provisi = txtprovisi.Text == "" ? 0 : Convert.ToInt32(txtprovisi.Text.Replace(".", "").Replace(",00", ""));
        //        int keanggotaan = lblnopenlama.Visible == true ? 0 : Convert.ToInt32(txtdeviasikeanggotaan.Text.Replace(".", "").Replace(",00", ""));
        //        int angsblokir = txtangsblokir.Text == "" ? 0 : Convert.ToInt32(txtangsblokir.Text.Replace(".", "").Replace(",00", ""));
        //        int angsdimuka = txtangsdimuka.Text == "" ? 0 : Convert.ToInt32(txtangsdimuka.Text.Replace(".", "").Replace(",00", ""));
        //        txtRpotongan.Text = string.Format("{0:N2}", asuransi + provisi + keanggotaan + angsblokir + angsdimuka);
        //        txtRasuransi.Text = deviasiasuransi();
        //        txtRprovisi.Text = deviasiprovisi().ToString();
        //        txtRkeanggotaan.Text = lblnopenbaru.Text == "Nasabah Baru" ? "20.000,00" : "0,00";
        //        txtRangsblokir.Text = ddljenisdeviasi.SelectedValue != "1" ? "0,00" : txtangsblokir.Text;
        //        txtRangsdimuka.Text = ddljenisdeviasi.SelectedValue != "5" ? "0,00" : txtangsdimuka.Text;
        //        txtRpelunasanto.Text = txtpelunasantakeover.Text;
        //        txtRtittleprovisi.Text = ddljenisdeviasi.SelectedValue != "4" ? HDNProvisi.Value : (txttittleprovisi2.Text == "" ? HDNProvisi.Value : txttittleprovisi2.Text);
        //        //ddlprovisi.Enabled = true;
        //        fungsiAll();
        //        //txtRtittleprovisi.Text = ddljenisdeviasi.SelectedValue != "4" ? HDNProvisi.Value : txttittleprovisi2.Text;

        //        Pnlhasil.Visible = true;
        //        pnldatapemohon.Visible = false;
        //        pnladd.Visible = false;
        //    }
        //}
        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //}
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
           
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
    //protected void btnApp_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindGridEntityApp(); 
    //        //idjudultotal.Visible = false;
    //        idjudulsemua.Visible = false;
    //        divtotalrequest.Visible = false;
    //        //idjudulapp.Visible = true;
    //        //idjudulpending.Visible = false;
    //        //idjudulreject.Visible = false;
    //        //GrdApp.Visible = true;
    //        //GrdPending.Visible = false;
    //        //GrdReject.Visible = false;
    //        GRData.Visible = true;
    //    }
    //    catch (Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //    }
    //}
    //protected void btnRjt_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindGridEntityRjt(); 
    //        //idjudultotal.Visible = false;
    //        idjudulsemua.Visible = false;
    //        divtotalrequest.Visible = false;
    //        //idjudulapp.Visible = false;
    //        //idjudulpending.Visible = false;
    //        //idjudulreject.Visible = true;
    //        //GrdApp.Visible = false;
    //        //GrdPending.Visible = false;
    //        //GrdReject.Visible = true;
    //        GRData.Visible = false;
    //    }
    //    catch (Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //    }
    //}
    //protected void btnPnd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindGridEntityPnd();
    //        idjudultotal.Visible = false;
    //        idjudulsemua.Visible = false;
    //        divtotalrequest.Visible = false;
    //        idjudulapp.Visible = false;
    //        idjudulpending.Visible = true;
    //        idjudulreject.Visible = false;
    //        GrdApp.Visible = false;
    //        GrdPending.Visible = true;
    //        GrdReject.Visible = false;
    //        GRData.Visible = false;
    //    }
    //    catch (Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //    }
    //}
    //protected void btnRequest_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DateTime lahir = Convert.ToDateTime(txttgllahir.Text);
    //        if (ddljenisdeviasi.SelectedValue == "")
    //        {

    //        }
    //        //INSERT INTO MIBisnis
    //        DefaultClass.Global.SqlHelper[] varIB = new DefaultClass.Global.SqlHelper[82];
    //        varIB[0] = gb1.CreateSPParameter("@Nopen", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnopen.Text, typeof(string));
    //        varIB[1] = gb1.CreateSPParameter("@KodeProduct", ParameterDirection.Input, SqlDbType.VarChar, 16, ddlkodeproduk.SelectedValue, typeof(int));
    //        varIB[2] = gb1.CreateSPParameter("@TglSimulasi", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglsimulasi.Text, typeof(string));
    //        varIB[3] = gb1.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlsumberdana.SelectedValue, typeof(string));
    //        varIB[4] = gb1.CreateSPParameter("@KodeJiwa", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlkodejiwa.Text, typeof(string));
    //        varIB[5] = gb1.CreateSPParameter("@Status", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlstatusnikah.SelectedValue, typeof(string));
    //        varIB[6] = gb1.CreateSPParameter("@Instansi", ParameterDirection.Input, SqlDbType.VarChar, 16, txtRinstansi.Text, typeof(string));
    //        varIB[7] = gb1.CreateSPParameter("@JmlAnak", ParameterDirection.Input, SqlDbType.VarChar, 4, txtPLtunjanak.Text, typeof(string));
    //        varIB[8] = gb1.CreateSPParameter("@TglAnak1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak1.Text, typeof(string));
    //        varIB[9] = gb1.CreateSPParameter("@TglAnak2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak2.Text, typeof(string));
    //        varIB[10] = gb1.CreateSPParameter("@TglAnak3", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak3.Text, typeof(string));
    //        varIB[11] = gb1.CreateSPParameter("@TglAnak4", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak4.Text, typeof(string));
    //        varIB[12] = gb1.CreateSPParameter("@GajiBersihDisplay", ParameterDirection.Input, SqlDbType.Money, 0, txtgajibersihdisplay.Text, typeof(string));
    //        varIB[13] = gb1.CreateSPParameter("@GajiPokok", ParameterDirection.Input, SqlDbType.Money, 0, txtgajipokok.Text, typeof(string));

    //        varIB[14] = gb1.CreateSPParameter("@JangkaWaktu", ParameterDirection.Input, SqlDbType.VarChar, 3, ddljangkawaktu.SelectedValue, typeof(string));
    //        varIB[15] = gb1.CreateSPParameter("@Plafond", ParameterDirection.Input, SqlDbType.Money, 0, txtPlafond.Text.Replace(",00",""), typeof(string));
    //        varIB[16] = gb1.CreateSPParameter("@PlafondMax", ParameterDirection.Input, SqlDbType.Money, 0, txtplafondmax.Text, typeof(string));
    //        varIB[17] = gb1.CreateSPParameter("@SisaGaji", ParameterDirection.Input, SqlDbType.Money, 0, txtgajitunjanak.Text, typeof(string));
    //        varIB[18] = gb1.CreateSPParameter("@Bunga", ParameterDirection.Input, SqlDbType.Float, 0, txtbunga.Text, typeof(string));
    //        varIB[19] = gb1.CreateSPParameter("@Angsuran", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuran.Text, typeof(string));
    //        varIB[20] = gb1.CreateSPParameter("@TunjAnak", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjanak2.Text, typeof(string));
    //        varIB[21] = gb1.CreateSPParameter("@TunjIstri", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjistri2.Text, typeof(string));
    //        varIB[22] = gb1.CreateSPParameter("@TunjBeras", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjberas2.Text, typeof(string));
    //        varIB[23] = gb1.CreateSPParameter("@Potongan", ParameterDirection.Input, SqlDbType.Money, 0, txtRpotongan.Text, typeof(string));
    //        varIB[24] = gb1.CreateSPParameter("@Asuransi", ParameterDirection.Input, SqlDbType.Money, 0, txtRasuransi.Text, typeof(string));
    //        varIB[25] = gb1.CreateSPParameter("@Provisi", ParameterDirection.Input, SqlDbType.Float, 0, txtRprovisi.Text, typeof(string));
    //        varIB[26] = gb1.CreateSPParameter("@Keanggotaan", ParameterDirection.Input, SqlDbType.Money, 0, txtRkeanggotaan.Text, typeof(string));
    //        varIB[27] = gb1.CreateSPParameter("@AngsBlokir", ParameterDirection.Input, SqlDbType.Money, 0, txtRangsblokir.Text, typeof(string));

    //        //INSERT INTO MIdetil
    //        varIB[28] = gb1.CreateSPParameter("@AngsDimuka", ParameterDirection.Input, SqlDbType.Money, 0, txtRangsdimuka.Text, typeof(string));
    //        varIB[29] = gb1.CreateSPParameter("@PelunasanTO", ParameterDirection.Input, SqlDbType.Money, 0, txtpelunasantakeover.Text, typeof(string));
    //        varIB[30] = gb1.CreateSPParameter("@TerimaBersih", ParameterDirection.Input, SqlDbType.Money, 0, txtterimabersih.Text, typeof(string));
    //        varIB[31] = gb1.CreateSPParameter("@Catatan1", ParameterDirection.Input, SqlDbType.VarChar, 255, txtcatatan1.Text, typeof(string));
    //        varIB[32] = gb1.CreateSPParameter("@Catatan2", ParameterDirection.Input, SqlDbType.VarChar, 255, txtcatatan2.Text, typeof(string));
    //        varIB[33] = gb1.CreateSPParameter("@Catatan3", ParameterDirection.Input, SqlDbType.VarChar, 255, txtcatatan3.Text, typeof(string));


    //        varIB[34] = gb1.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        varIB[35] = gb1.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 200, SessBranchID, typeof(string));
    //        varIB[36] = gb1.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
    //        varIB[37] = gb1.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        varIB[38] = gb1.CreateSPParameter("@JenisDeviasi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljenisdeviasi.SelectedValue == "" ? "No" : ddljenisdeviasi.SelectedValue, typeof(string));
    //        varIB[39] = gb1.CreateSPParameter("@JenisTakeOver", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatustakeover.SelectedItem.Text, typeof(int));
    //        varIB[40] = gb1.CreateSPParameter("@TglLahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahir.Text, typeof(string));
    //        varIB[41] = gb1.CreateSPParameter("@PengajuanTahun", ParameterDirection.Input, SqlDbType.VarChar, 4, txttahunpengajuan.Text, typeof(int));
    //        varIB[42] = gb1.CreateSPParameter("@PengajuanBulan", ParameterDirection.Input, SqlDbType.VarChar, 4, txtbulanpengajuan.Text, typeof(string));
    //        varIB[43] = gb1.CreateSPParameter("@PengajuanHari", ParameterDirection.Input, SqlDbType.VarChar, 4, txtharipengajuan.Text, typeof(string));
    //        varIB[44] = gb1.CreateSPParameter("@TempoTahun", ParameterDirection.Input, SqlDbType.VarChar, 4, txttahuntempo.Text, typeof(string));
    //        varIB[45] = gb1.CreateSPParameter("@TempoBulan", ParameterDirection.Input, SqlDbType.VarChar, 4, txtbulantempo.Text, typeof(string));
    //        varIB[46] = gb1.CreateSPParameter("@TempoHari", ParameterDirection.Input, SqlDbType.VarChar, 4, txtharitempo.Text, typeof(string));
    //        varIB[47] = gb1.CreateSPParameter("@Status_Sk_diterima", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatussk.SelectedValue, typeof(string));

    //        //siqre2
    //        varIB[48] = gb1.CreateSPParameter("@NamaDebitur", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamapensiun.Text, typeof(string));

    //        varIB[49] = gb1.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnosimulasi.Text, typeof(string));
    //        varIB[50] = gb1.CreateSPParameter("@TglPk", ParameterDirection.Input, SqlDbType.DateTime, 30, hdntglpk.Value, typeof(string));
    //        varIB[51] = gb1.CreateSPParameter("@statuspensiun", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlpensiun.SelectedValue, typeof(string));
    //        varIB[52] = gb1.CreateSPParameter("@Keterangan1", ParameterDirection.Input, SqlDbType.VarChar, 255, txtketerangan1.Text, typeof(string));
    //        varIB[53] = gb1.CreateSPParameter("@Keterangan2", ParameterDirection.Input, SqlDbType.VarChar, 255, txtketerangan2.Text, typeof(string));
    //        varIB[54] = gb1.CreateSPParameter("@Keterangan3", ParameterDirection.Input, SqlDbType.VarChar, 255, txtketerangan3.Text, typeof(string));

    //        varIB[55] = gb1.CreateSPParameter("@DeviasiBunga", ParameterDirection.Input, SqlDbType.Float, 0, txtbungadeviasi.Text == "" ? 0 : decimal.Parse(txtbungadeviasi.Text), typeof(decimal));
    //        varIB[56] = gb1.CreateSPParameter("@DeviasiProvisi", ParameterDirection.Input, SqlDbType.Float, 0, txttittleprovisi2.Text == "" ? 0 : decimal.Parse(txttittleprovisi2.Text), typeof(decimal));
    //        varIB[57] = gb1.CreateSPParameter("@DeviasiAsuransi", ParameterDirection.Input, SqlDbType.Money, 0, txtdeviasiasuransi.Text == "" ? 0 : decimal.Parse(txtdeviasiasuransi.Text), typeof(decimal));

    //        varIB[58] = gb1.CreateSPParameter("@Rateasuransi", ParameterDirection.Input, SqlDbType.Float, 0, txtbungaasuransi.Text == "" ? 0 : decimal.Parse(txtbungaasuransi.Text), typeof(decimal));
    //        varIB[59] = gb1.CreateSPParameter("@RateProvisi", ParameterDirection.Input, SqlDbType.Float, 20, decimal.Parse(HDNProvisi.Value.Replace(".",",")), typeof(decimal));
    //        varIB[60] = gb1.CreateSPParameter("@AddByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
    //        varIB[61] = gb1.CreateSPParameter("@ModiftByUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
    //        varIB[62] = gb1.CreateSPParameter("@DeviasiAngsBlokir", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlangsblokir.SelectedValue, typeof(string));
    //        varIB[63] = gb1.CreateSPParameter("@DeviasiAngsMuka", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlangsdimuka.SelectedValue, typeof(string));
    //        //tambahan

    //        varIB[64] = gb1.CreateSPParameter("@PTGajiPokok", ParameterDirection.Input, SqlDbType.Money, 0, txtPLgajipokok.Text, typeof(string));
    //        varIB[65] = gb1.CreateSPParameter("@PTGajiPokok1", ParameterDirection.Input, SqlDbType.Money, 0, txtPLgajipokok1.Text, typeof(string));
    //        varIB[66] = gb1.CreateSPParameter("@PTGajiPokokJumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtPlgajipokok2.Text, typeof(string));
    //        varIB[67] = gb1.CreateSPParameter("@PTTunjAnak", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjanak.Text, typeof(string));
    //        varIB[68] = gb1.CreateSPParameter("@PTTunjAnak1", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjanak1.Text, typeof(string));
    //        varIB[69] = gb1.CreateSPParameter("@PTTunjAnakJumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjanak2.Text, typeof(string));
    //        varIB[70] = gb1.CreateSPParameter("@PTTunjIstri", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjistri.Text, typeof(string));
    //        varIB[71] = gb1.CreateSPParameter("@PTTunjIstri1", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjistri1.Text, typeof(string));
    //        varIB[72] = gb1.CreateSPParameter("@PTTunjIstriJumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjistri2.Text, typeof(string));
    //        varIB[73] = gb1.CreateSPParameter("@PTTunjBeras", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjberas.Text, typeof(string));
    //        varIB[74] = gb1.CreateSPParameter("@PTTunjBeras1", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjberas1.Text, typeof(string));
    //        varIB[75] = gb1.CreateSPParameter("@PTTunjBerasJumlah", ParameterDirection.Input, SqlDbType.Money, 0, txtPLtunjberas2.Text, typeof(string));
    //        varIB[76] = gb1.CreateSPParameter("@PTTGajiTunjangan", ParameterDirection.Input, SqlDbType.Money, 0, txtPLterimabersih.Text, typeof(string));

    //        varIB[77] = gb1.CreateSPParameter("@TahunAnak1", ParameterDirection.Input, SqlDbType.Int, 0, txttahunanak1.Text, typeof(string));
    //        varIB[78] = gb1.CreateSPParameter("@TahunAnak2", ParameterDirection.Input, SqlDbType.Int, 0, txttahunanak2.Text, typeof(string));

    //        varIB[79] = gb1.CreateSPParameter("@JwMax", ParameterDirection.Input, SqlDbType.Int, 0, txtjwmax.Text, typeof(string));
    //        varIB[80] = gb1.CreateSPParameter("@UsiaAsuransi", ParameterDirection.Input, SqlDbType.Int, 0, txtusiaasuransi.Text, typeof(string));
    //        varIB[81] = gb1.CreateSPParameter("@AngsuranMax", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranmax.Text, typeof(string));


    //        ge1.ListDataset = gb1.GetDataJustOutPut("SI_Request", varIB, "constrmf");

    //        if (ge1.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //        {
    //            //pnldatapemohon.Visible = false;
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
    //        }

    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge1.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //            gb1.SaveErroLog(ge1.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //        }
    //    }
    //    catch (System.Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //    }
    //}
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

    //protected void btnnextpage2_Click(object sender, EventArgs e)
    //{

    //    if (ddljenisdeviasi.SelectedIndex  == 0)
    //    {

    //    }
    //    Pnldepan.Visible = false;
       
    //    pnldatapemohon.Visible = false;
    //    pnladd.Visible = false;
    //    Pnlhasil.Visible = true;
    //}
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/MI/Master/CreateMI.aspx");
    }
    //protected void btnBacksave_Click(object sender, EventArgs e)
    //{

    //    Pnlhasil.Visible = false;
    //    pnldatapemohon.Visible = true;
    //    pnladd.Visible = true;
    //    Pnldepan.Visible = true;

    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    //}
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
         Response.Redirect("CreateMINEW.aspx");
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
                    txtkeanggotaan.Text = string.Format("{0:N0}", "20000");
                    //lblnopenbaru.Visible = true;
                }
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
            //lblnopenlama.

            if (txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@tgllahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahir.Text, typeof(DateTime));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKUSIAPENSIUN", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtusiapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usia"].ToString());
                    txtusiaasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usia"].ToString());

                    if (txtkodesimulasi.Text == "B")
                    {
                        //= IF((80 - G12) * 12 > 300; 300; (80 - G12) * 12)
                        if ((int.Parse("80") -int.Parse(txtusiapinjaman.Text)) * int.Parse("12") > 300)
                        {
                            txtjangkawaktumax.Text = "300";
                        }
                        else
                        {
                            txtjangkawaktumax.Text = string.Format("{0:N0}", 80 - int.Parse(txtusiapinjaman.Text) * 12);


                        }
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
            //fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

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

        txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["kodesimulasi"].ToString();

        ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";


        ddljangkawaktu.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljangkawaktu.DataTextField = "jangka";
        ddljangkawaktu.DataValueField = "jangka";
        ddljangkawaktu.DataBind();

        ddljangkawaktu.Items.Insert(0, new ListItem("Pilih Jangka", "-"));


    }

    protected void ddljangkawaktu_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (txtkodesimulasi.Text == "B")
        {

       

        if (ddljangkawaktu.SelectedValue == "-")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Anda Belum Memilih Jangka, silahkan di cek kembali');", true);

        }
            else if(ddlinstansi.SelectedValue == "B")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Maaf Instansi Asabri Belum bisa, silahkan di cek kembali');", true);

            }
            else if (int.Parse(ddljangkawaktu.SelectedValue) > int.Parse(txtjangkawaktumax.Text))
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Jangka waktu tidak boleh melebihi jangka waktu max, silahkan di cek kembali');", true);
        }

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

        }



            DefaultClass.Global.SqlHelper[] var2xx = new DefaultClass.Global.SqlHelper[6];
            var2xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, "A", typeof(string));
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
               


                //lblnopenbaru.Visible = true;
            }




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


    public void InsertCombo()
    {
        try
        {
            // --- Combo Kode Produk
            //ge.ListDataset = gb.GetDataFunction("dbo.Getkpprogrampinjaman()", "constrmf");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "kode desc";

            //ddlkodeproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddlkodeproduk.DataTextField = "program";
            //ddlkodeproduk.DataValueField = "kode";
            //ddlkodeproduk.DataBind();
            //ddlkodeproduk.Items.Insert(0, new ListItem("Pilih Kode Product", ""));

            //---Combo Kode Jiwa 
            ge.ListDataset = gb.GetDataFunction("dbo.Getmasterkodejiwa()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_jiwa asc";

            ddlkodejiwa.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkodejiwa.DataTextField = "kdjiwa";
            ddlkodejiwa.DataValueField = "kode_jiwa";
            ddlkodejiwa.DataBind();
            ddlkodejiwa.Items.Insert(0, new ListItem("Pilih Kode Jiwa", ""));

            //// --- Combo Jenis Deviasi
            //ge.ListDataset = gb.GetDataFunction("dbo.GetJenisDeviasi()", "constrmf");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJenis asc";

            //ddljenisdeviasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddljenisdeviasi.DataTextField = "NamaJenis";
            //ddljenisdeviasi.DataValueField = "KodeJenis";
            //ddljenisdeviasi.DataBind();
            //ddljenisdeviasi.Items.Insert(0, new ListItem("Pilih Type", ""));

            // --- Combo Sumberdana
            ge.ListDataset = gb.GetDataFunction("dbo.getsumberdana()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "NamaBank asc";

            ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlsumberdana.DataTextField = "NamaBank";
            ddlsumberdana.DataValueField = "KodeBank";
            ddlsumberdana.DataBind();
            ddlsumberdana.Items.Insert(0, new ListItem("Pilih Sumber Dana", ""));


            ge.ListDataset = gb.GetDataFunction("dbo.getkantorbayar()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "NamaBank asc";

            ddlKantorBayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlKantorBayar.DataTextField = "NamaBank";
            ddlKantorBayar.DataValueField = "KodeBank";
            ddlKantorBayar.DataBind();
            ddlKantorBayar.Items.Insert(0, new ListItem("Pilih Kantor Bayar", ""));


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


 

}

