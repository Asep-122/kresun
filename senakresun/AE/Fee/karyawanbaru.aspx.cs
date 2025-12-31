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
public partial class mikro_karyawanbaru : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }
    private Boolean SessIsSuperAdmin
    {
        get { return Boolean.Parse(Session["SessIsSuperAdmin"].ToString()); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }
    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessIsLevel
    {
        get { return Session["SessIsLevel"].ToString(); }
        set { ViewState["SessIsLevel"] = value; }
    }
    public string SessAreaUser
    {
        get { return Session["SessAreaUser"].ToString(); }
        set { ViewState["SessAreaUser"] = value; }
    }
    private string SessKdpersh
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
            if (!IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                txttgllahir.Text = DateTime.Now.AddYears(-20).ToString("yyyy-MM-dd");
                Txttglmasuk.Text = DateTime.Now.ToString("yyyy-MM-dd");
                InsertCombo();
                BindGridEntity();
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void BindGridEntity()
    {
        try
        {
            //string query;
            //query = "  SELECT ";
            //query = query + "  nama, kelamin, tempat_lahir, tgl_lahir, tanggal_masuk,";
            //query = query + "  kode_jabatan,";
            //query = query + "  kode_cabang, x.flag,";
            //query = query + "  case when x.flag = 0 then 'Belum di approve' else 'sudah diapprove' end as ketflag , ";
            //query = query + "  groupx, ";
            //query = query + "  kd_staff,nik, ";
            //query = query + "  bos_id, ";
            //query = query + "  idserial, ";
            //query = query + "  user_insert, ";
            //query = query + "  tgl_insert";
            //query = query + "  FROM tbl_karyawan_new x";
            //query = query + " where x.kode_cabang = '" + SessBranchID + "'";
            //DataTable dt = GetDataMikro(query);

            // GRUser.DataSource = ge.ListDataset.Tables[0];

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanBaru", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";

            GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRUser.DataBind();

            GRUser.UseAccessibleHeader = true;
            GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "GRUPSET":
                if (((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblflag")).Text == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak dapat di Delete sudah di approve pusat');", true);

                }
                else
                {
                    string kodenik;

                    kodenik = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnik")).Text;
                    string kodecabang = ((HiddenField)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("hdnkodecabang")).Value;
                    string kdpers = ((HiddenField)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("hdnkdpersh")).Value;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, kdpers, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, kodenik, typeof(string));
                    var[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, kodecabang, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanBaru", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully delete this Project','top-right','success','1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                    BindGridEntity();
                }
                break;
        }
    }
    protected void GRUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }
    }
    protected void tanggallahiir_OnChange(object sender, EventArgs e)
    {
        try
        {
            DateTime now = DateTime.Now;
            DateTime pilihlahir = Convert.ToDateTime(txttgllahir.Text);
            int hitunglahir = Convert.ToInt32((now - pilihlahir).TotalDays/365);
            if (Convert.ToDateTime(txttgllahir.Text) > DateTime.Now.AddYears(-20))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Batas minimal umur karyawan adalah 20 Tahun');", true);

                txttgllahir.Text = DateTime.Now.AddYears(-20).ToString("yyyy-MM-dd");
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            string gaji_pokok;
            string tunjangan_makan;
            string tunjangan_transport;
            string total_gaji;
            gaji_pokok = Convert.ToString(Convert.ToDecimal(txtgajipokok.Text));
            tunjangan_makan = Convert.ToString(Convert.ToDecimal(txtjunganmakan.Text));
            tunjangan_transport = Convert.ToString(Convert.ToDecimal(txttunjangantransport.Text));
            total_gaji = Convert.ToString(Convert.ToDecimal(txtgajipokok.Text) + Convert.ToDecimal(txtjunganmakan.Text) + Convert.ToDecimal(txttunjangantransport.Text));
            //query = " insert into tbl_karyawan_new ";
            //query = query + "  (nama, kelamin, tempat_lahir, tgl_lahir, tanggal_masuk, agama,";
            //query = query + "  marital_status,ptkp,kode_jabatan,kode_cabang,groupx,";
            //query = query + "  jenis_karyawn,email,nomor_hp,kd_staff,bos_id,";
            //query = query + "  gaji_pokok,tunjangan_makan,tunjangan_transport,total_gaji,user_insert,tgl_insert";
            //query = query + "  ) values ";
            //query = query + "(";
            //query = query + "'" + txtnama.Text + "',";
            //query = query + "'" + ddljeniskelamin.SelectedValue + "',";
            //query = query + "'" + txttempat.Text + "',";
            //query = query + "'" + txttgllahir.Text + "',";
            //query = query + "'" + Txttglmasuk.Text + "',";
            //query = query + "'" + ddlagama.SelectedValue + "',";
            //query = query + "'" + ddlmaritalstatus.SelectedValue + "',";
            //query = query + "'" + ddlptkp.SelectedValue + "',";
            //query = query + "'" + ddlkodejabatan.SelectedValue + "',";
            //query = query + "'" + SessBranchID + "',";
            //query = query + "'" + ddlgroup.SelectedValue + "',";
            //query = query + "'" + ddljeniskary.SelectedValue + "',";
            //query = query + "'" + txtemail.Text + "',";
            //query = query + "'" + TxtHP.Text + "',";
            //query = query + "'" + txtkdstaf.Text + "',";
            //query = query + "'" + ddlbosid.SelectedValue + "',";
            //query = query + "'" + gaji_pokok.Replace(",", ".") + "',";
            //query = query + "'" + tunjangan_makan.Replace(",", ".") + "',";
            //query = query + "'" + tunjangan_transport.Replace(",", ".") + "',";
            //query = query + "'" + total_gaji.Replace(",", ".") + "',";
            //query = query + "'" + SessLoginID + "',";
            //query = query + "now())";
            //NpgsqlCommand command = new NpgsqlCommand(query, con);
            //command.ExecuteReader();
            string STATE = ""; 
            if(txtnama.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[28];
            var[0]  = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodenik.Text, typeof(string));
            var[1]  = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var[2]  = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
            var[3]  = gb.CreateSPParameter("@tempat_lahir", ParameterDirection.Input, SqlDbType.VarChar, 50, txttempat.Text, typeof(string));
            var[4]  = gb.CreateSPParameter("@tgl_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, Convert.ToDateTime(txttgllahir.Text), typeof(DateTime));
            var[5]  = gb.CreateSPParameter("@TglTerima", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, Convert.ToDateTime(Txttglmasuk.Text), typeof(DateTime));
            var[6]  = gb.CreateSPParameter("@TglKeluar", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, null, typeof(DateTime));
            var[7] = gb.CreateSPParameter("@bos_id", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlbosid.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@KodeJab", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkodejabatan.SelectedValue, typeof(string));
            var[9] = gb.CreateSPParameter("@KodeBagian", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlgroup.SelectedValue, typeof(string));
            var[10] = gb.CreateSPParameter("@KodeDept", ParameterDirection.Input, SqlDbType.VarChar, 40, "", typeof(string));
            var[11] = gb.CreateSPParameter("@KodeGol", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlgroup.SelectedValue, typeof(string));
            var[12] = gb.CreateSPParameter("@agama", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlagama.SelectedValue, typeof(string));
            var[13] = gb.CreateSPParameter("@status_nikah", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlmaritalstatus.SelectedValue, typeof(string));
            var[14] = gb.CreateSPParameter("@Ptkp", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlptkp.SelectedValue, typeof(string));
            var[15] = gb.CreateSPParameter("@StatusKry", ParameterDirection.Input, SqlDbType.VarChar, 40, ddljeniskary.SelectedValue, typeof(string));
            var[16] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 40, txtemail.Text, typeof(string));
            var[17] = gb.CreateSPParameter("@nomor_hp", ParameterDirection.Input, SqlDbType.VarChar, 40, TxtHP.Text, typeof(string));
            var[18] = gb.CreateSPParameter("@JlhGajiPokok", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(gaji_pokok), typeof(decimal));
            var[19] = gb.CreateSPParameter("@TunMakan", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(tunjangan_makan), typeof(decimal));
            var[20] = gb.CreateSPParameter("@TunTransport", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(tunjangan_transport), typeof(decimal));
            var[21] = gb.CreateSPParameter("@totalgaji", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(total_gaji), typeof(decimal));
            var[22] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[23] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[24] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[25] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[26] = gb.CreateSPParameter("@JenisKelamin", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljeniskelamin.SelectedValue, typeof(string));
            var[27] = gb.CreateSPParameter("@NamaKaryawan", ParameterDirection.Input, SqlDbType.VarChar, 50,txtnama.Text, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanBaru", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        BindGridEntity();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlFront.Visible = false;
        pnlFilter.Visible = true;

        //hdnsave.Value ="true";
        //ddlbulan.Enabled = true;
        //ddlcabang.Enabled = true;
        //ddltahun.Enabled = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void lnkRefreshModal_Click(object sender, EventArgs e)
    {
        BindGridEntity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected string btnremodal()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    }

    #region "Insertdata"
    public void InsertCombo()
    {
        try
        {
            // --- Combo Kode group
            DefaultClass.Global.SqlHelper[] varG = new DefaultClass.Global.SqlHelper[3];
            varG[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varG[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGROUP", typeof(string));
            varG[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanBaru", varG, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGol asc";

            ddlgroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlgroup.DataTextField = "nama";
            ddlgroup.DataValueField = "KodeGol";
            ddlgroup.DataBind();
            ddlgroup.Items.Insert(0, new ListItem("Pilih Group", ""));

            // --- Combo Kode Nik
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNIK", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanBaru", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";

            ddlbosid.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlbosid.DataTextField = "nama";
            ddlbosid.DataValueField = "KodeNik";
            ddlbosid.DataBind();
            ddlbosid.Items.Insert(0, new ListItem("Pilih Karyawan", ""));

            // --- Combo Kode Nik
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJABATAN", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanBaru", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

            ddlkodejabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkodejabatan.DataTextField = "nama";
            ddlkodejabatan.DataValueField = "KodeJab";
            ddlkodejabatan.DataBind();
            ddlkodejabatan.Items.Insert(0, new ListItem("Pilih Jabatan", ""));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion "Insertdata"
}