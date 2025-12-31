using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class AR_Process_ApprovalKeanggotaan : System.Web.UI.Page
{
    String no_nasabah = "", nosebelumnya = "", updateno = "", CRNonasabah = "", STATE = "", formatnumber = "";
    int counter_nonasabah = 0, kodeawal = 0;

    long nosekarang = 0, nonasabah = 0;
    Global gb = new DefaultClass.Global();
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
    private string sessKoperasiid
    {
        get { return Session["sessKoperasiID"].ToString(); }
        set { ViewState["sessKoperasiID"] = value; }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e) 
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick",
            "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (SessBranchID != "001")
            {
                btnAdd.Visible = false;
                PnlFront.Visible = true;
                //pnlListReject.Visible = false;
                pnlHeader.Visible = false;

                if (!Page.IsPostBack)
                {
                    BindGridEntity();
                }
            } 
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORAPPROVALANGGOTA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "register_date asc";


            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
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
    protected void Accept(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GRData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkAccept = (CheckBox)row.FindControl("check");
                if (chkAccept.Checked)
                {
             
                     
                    Label nomor_nasabah = (Label)row.FindControl("lblnonasabah");
                    Label nama_nasabah = (Label)row.FindControl("lblnamanasabah");
                    Label AO = (Label)row.FindControl("lblAO");

                    //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
                    //var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string));
                    //var[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nomor_nasabah.Text, typeof(string));
                    //var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                    //var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    //var[4] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    //var[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    //var[6] = gb.CreateSPParameter("@kode_ao", ParameterDirection.Input, SqlDbType.VarChar, 50, AO.Text, typeof(string));
                    //var[7] = gb.CreateSPParameter("@user", ParameterDirection.Input, SqlDbType.VarChar, 100, SessLoginID, typeof(string));
                    //var[8] = gb.CreateSPParameter("@koperasiID", ParameterDirection.Input, SqlDbType.VarChar, 100, sessKoperasiid, typeof(string)); 
                    //ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_TABUNGAN", var, "constrmf");

                    //DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                    //var2[0] = gb.CreateSPParameter("@no_rekening_tabungan", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string));
                    //var2[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nomor_nasabah.Text, typeof(string));
                    //var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEANGGOTAACCEPT", typeof(string));
                    //var2[3] = gb.CreateSPParameter("@register_user", ParameterDirection.Input, SqlDbType.VarChar, 100, SessLoginID, typeof(string));
                    //var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", var2, "constrmf");

                    //DefaultClass.Global.SqlHelper[] varsimpok = new DefaultClass.Global.SqlHelper[4];
                    //varsimpok[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    //varsimpok[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    //varsimpok[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSIMPOK", typeof(string));
                    //varsimpok[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("GETNOMINALSIMPOK", varsimpok, "constrmf");

                    //decimal simpok = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilai_pokok"].ToString()); 
                    //decimal simwa = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilai_smk"].ToString());

                    //INSERT TO TABLE ARMST

                    //DefaultClass.Global.SqlHelper[] varmst = new DefaultClass.Global.SqlHelper[14];
                    //varmst[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, nomor_nasabah.Text, typeof(string));
                    //varmst[1] = gb.CreateSPParameter("@noanggota", ParameterDirection.Input, SqlDbType.VarChar, 250, nomor_nasabah.Text, typeof(string));
                    //varmst[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                    //varmst[3] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                    //varmst[4] = gb.CreateSPParameter("@NamaLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, nama_nasabah.Text, typeof(string));
                    //varmst[5] = gb.CreateSPParameter("@SyaratBayar", ParameterDirection.Input, SqlDbType.VarChar, 250, "D30", typeof(string));
                    //varmst[6] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, AO.Text, typeof(string)); // blm
                    //varmst[7] = gb.CreateSPParameter("@NonAktifFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                    //varmst[8] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 250, "IDR", typeof(string));
                    //varmst[9] = gb.CreateSPParameter("@kurs", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string));
                    //varmst[10] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                    //varmst[11] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                    //varmst[12] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTANGGOTA", typeof(string));
                    //varmst[13] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("AR_ArMst", varmst, "constrmf");

                    ////INSERT INTO ArTrn Simpok 
                    //DefaultClass.Global.SqlHelper[] vartrn = new DefaultClass.Global.SqlHelper[11];
                    //vartrn[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, nomor_nasabah.Text, typeof(string));
                    //vartrn[1] = gb.CreateSPParameter("@NoFaktur", ParameterDirection.Input, SqlDbType.VarChar, 250, nomor_nasabah.Text, typeof(string));
                    //vartrn[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, "V", typeof(string));
                    //vartrn[3] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, AO.Text, typeof(string));
                    //vartrn[4] = gb.CreateSPParameter("@Keterangan ", ParameterDirection.Input, SqlDbType.VarChar, 250, "Simpanan Pokok", typeof(string));
                    //vartrn[5] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Decimal, 250, "-" + simpok, typeof(string));
                    //vartrn[6] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                    //vartrn[7] = gb.CreateSPParameter("@kodecabang ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                    //vartrn[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                    //vartrn[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTSIMPOK", typeof(string));
                    //vartrn[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("AR_TRN", vartrn, "constrmf");

                    ////INSERT INTO ArTrn simwa 
                    //DefaultClass.Global.SqlHelper[] vartrn2 = new DefaultClass.Global.SqlHelper[11];
                    //vartrn2[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, nomor_nasabah.Text, typeof(string));
                    //vartrn2[1] = gb.CreateSPParameter("@NoFaktur", ParameterDirection.Input, SqlDbType.VarChar, 250, nomor_nasabah.Text, typeof(string));
                    //vartrn2[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, "W", typeof(string));
                    //vartrn2[3] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, AO.Text, typeof(string));
                    //vartrn2[4] = gb.CreateSPParameter("@Keterangan ", ParameterDirection.Input, SqlDbType.VarChar, 250, "Simpanan Wajib", typeof(string));
                    //vartrn2[5] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Decimal, 250, "-" + simpok, typeof(string));
                    //vartrn2[6] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                    //vartrn2[7] = gb.CreateSPParameter("@kodecabang ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                    //vartrn2[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                    //vartrn2[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTSIMPOK", typeof(string));
                    //vartrn2[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("AR_TRN", vartrn2, "constrmf");
                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                    var2[0] = gb.CreateSPParameter("@no_rekening_tabungan", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string));
                    var2[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nomor_nasabah.Text, typeof(string));
                    var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEANGGOTAACCEPT", typeof(string));
                    var2[3] = gb.CreateSPParameter("@register_user", ParameterDirection.Input, SqlDbType.VarChar, 100, SessLoginID, typeof(string));
                    var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", var2, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                       

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Approve Success');", true);

                        chkAccept.Checked = false;
                        BindGridEntity();
                    }
                }
            }
        }
    }


    protected void Reject(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GRData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkreject = (CheckBox)row.FindControl("chkreject");
                if (chkreject.Checked)
                {

                    Label nomor_nasabah = (Label)row.FindControl("lblnonasabah"); 
                     
                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
                    var2[0] = gb.CreateSPParameter("@last_user", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var2[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nomor_nasabah.Text, typeof(string));
                    var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEANGGOTAREJECT", typeof(string));
                    var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", var2, "constrmf");


                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tolak berhasil di Simpan');", true);

                        chkreject.Checked = false;
                        BindGridEntity();
                    }
                }
            }
        }
    } 

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        //BindGridEntityReject();
        //pnlListReject.Visible = true;
        btnAdd.Visible = false;
        PnlFront.Visible = false;
    }
}