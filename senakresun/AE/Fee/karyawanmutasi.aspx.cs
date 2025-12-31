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
public partial class mikro_karyawanmutasi : System.Web.UI.Page
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
    Global gb2 = new Global();
    GlobalEntities ge2 = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
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
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", var, "constrmf");

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
    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlFront.Visible = false;


        pnlFilter.Visible = true;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }
    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string query;
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
                    DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
                    varT[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varT[3] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnik.SelectedValue, typeof(string));
                    varT[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", varT, "constrmf");
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
            case "GRUPSETVIEW":
                ddltipemutasi.SelectedValue = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltipemutasi")).Text;

                DefaultClass.Global.SqlHelper[] varB = new DefaultClass.Global.SqlHelper[5];
                varB[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varB[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVIEW", typeof(string));
                varB[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varB[3] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnik.SelectedValue, typeof(string));
                varB[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", varB, "constrmf");
                if (ddltipemutasi.SelectedValue == "A1")
                {
                    txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                    InsertCombo();
                    ddlcabang.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Kodecabanglama"].ToString();
                    ddlbosid.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bos_idbaru"].ToString();
                    ddlcabangbaru.Enabled = false;
                   
                    ddlcabangbaru.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Kodecabangbaru"].ToString();
                    ddlbosidbaru.Enabled = true;
                    ddlbosidbaru.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bos_idbaru"].ToString();

                    Pnlmutasicabang.Visible = true;
                    ddltipemutasi.Enabled = false;
                    pnlfooter.Visible = true;
                    btnproses.Enabled = false;
                    pnlFront.Visible = false;

                    ddlnik.Enabled = false;
                    btnSimpan.Enabled = false;
                    ddlbosidbaru.Enabled = false;
                }
                if (ddltipemutasi.SelectedValue == "A4")
                {
                    txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                    InsertCombo();
                    ddljeniskarybaru.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["StatusKryBaru"].ToString();
                    ddljeniskary.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();

                    pnlstatuskry.Visible = true;
                    Pnlmutasicabang.Visible = false;
                    ddltipemutasi.Enabled = false;
                    pnlfooter.Visible = true;
                    btnproses.Enabled = false;
                    pnlFront.Visible = false;

                    ddlnik.Enabled = false;
                    btnSimpan.Enabled = false;
                }
                if (ddltipemutasi.SelectedValue == "A5")
                {
                    txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                    InsertCombo();
                    ddlptkpbaru.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["ptkpbaru"].ToString();
                    ddlptkp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["ptkp"].ToString();

                    Pnlptkp.Visible = true;
                    pnlstatuskry.Visible = false;
                    Pnlmutasicabang.Visible = false;
                    ddltipemutasi.Enabled = false;
                    pnlfooter.Visible = true;
                    btnproses.Enabled = false;
                    pnlFront.Visible = false;

                    ddlnik.Enabled = false;
                    btnSimpan.Enabled = false;
                }
                if (ddltipemutasi.SelectedValue == "A3")
                {
                    txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                    InsertCombo();
                    ddlgroup.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodebagian"].ToString();
                    ddlgroupbaru.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodebagianbaru"].ToString();
                    ddlkodejabatan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodejablama"].ToString();
                    ddlkodejabatanbaru.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodejab"].ToString();
                    ddlbosidjabatan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bos_idlama"].ToString();
                    ddlbosidjabatanbaru.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bos_idbaru"].ToString();

                    Pnljabatan.Visible = true;
                    Pnlptkp.Visible = false;
                    pnlstatuskry.Visible = false;
                    Pnlmutasicabang.Visible = false;
                    ddltipemutasi.Enabled = false;
                    pnlfooter.Visible = true;
                    btnproses.Enabled = false;
                    pnlFront.Visible = false;

                    ddlnik.Enabled = false;
                    btnSimpan.Enabled = false;
                }
                if (ddltipemutasi.SelectedValue == "A2")
                {
                    txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                    txtgajipokok.Text = ge.ListDataset.Tables[0].Rows[0]["JlhGajiPokoklama"].ToString();
                    txtjunganmakan.Text = ge.ListDataset.Tables[0].Rows[0]["TunMakanlama"].ToString();
                    txttunjangantransport.Text = ge.ListDataset.Tables[0].Rows[0]["TunTransportlama"].ToString();

                    txtgajipokokBaru.Text = ge.ListDataset.Tables[0].Rows[0]["JlhGajiPokok"].ToString();
                    txtjunganmakanBaru.Text = ge.ListDataset.Tables[0].Rows[0]["TunMakan"].ToString();
                    txttunjangantransportBaru.Text = ge.ListDataset.Tables[0].Rows[0]["TunTransport"].ToString();


                    Pnlgaji.Visible = true;
                    Pnlmutasicabang.Visible = false;
                    ddltipemutasi.Enabled = false;
                    pnlfooter.Visible = true;
                    btnproses.Enabled = false;
                    pnlFront.Visible = false;

                    ddlnik.Enabled = false;
                    btnSimpan.Enabled = false;
                }
                if (ddltipemutasi.SelectedValue == "A6")
                {
                    DateTime tglresign;

                    txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                    InsertCombo();
                    ddlcabang.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabanglama"].ToString();
                    DateTime tglmasuk = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["Tglmasuk"].ToString());
                    Txttglmasuk.Text = tglmasuk.ToString("yyyy-MM-dd");
                    txtkodestaf.Text = "";
                    txtbosid.Text = ge.ListDataset.Tables[0].Rows[0]["bos_id"].ToString();
                    Txtgroup.Text = ge.ListDataset.Tables[0].Rows[0]["KetBagian"].ToString();
                    TxtJabatan.Text = ge.ListDataset.Tables[0].Rows[0]["KetJab"].ToString();
                    txtkodegroup.Text = ge.ListDataset.Tables[0].Rows[0]["Kodecabang"].ToString();
                    txtkodejabatan.Text = ge.ListDataset.Tables[0].Rows[0]["KodeJab"].ToString();

                    //Txttglmasuk.Text = dtwrh.Rows[0]["tgl_masuk"].ToString();
                    tglresign = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tglkeluar"].ToString());
                    txttglresign.Text = tglresign.ToString("yyyy-MM-dd"); ;
                    Txtgroup.Text = ge.ListDataset.Tables[0].Rows[0]["Ketbagian"].ToString();
                    TxtJabatan.Text = ge.ListDataset.Tables[0].Rows[0]["Ketjab"].ToString();
                    txtkodegroup.Text = ge.ListDataset.Tables[0].Rows[0]["kodebagian"].ToString();
                    txtkodejabatan.Text = ge.ListDataset.Tables[0].Rows[0]["kodejablama"].ToString();


                    Pnlresign.Visible = true;
                    Pnlmutasicabang.Visible = false;
                    ddltipemutasi.Enabled = false;
                    pnlfooter.Visible = true;
                    btnproses.Enabled = false;
                    pnlFront.Visible = false;

                    ddlnik.Enabled = false;
                    btnSimpan.Enabled = false;
                }
                break;
        }
    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {

        BindGridEntity();

    }
    protected void GRUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }
    }
    protected void ddlcabangbaru_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            InsertCombo();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlnik_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] varB = new DefaultClass.Global.SqlHelper[5];
            varB[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varB[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNIKBOS", typeof(string));
            varB[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varB[3] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnik.SelectedValue, typeof(string));
            varB[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", varB, "constrmf");

            txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnproses_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] varB = new DefaultClass.Global.SqlHelper[5];
            varB[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varB[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNIKBOS", typeof(string));
            varB[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varB[3] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnik.SelectedValue, typeof(string));
            varB[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeAppKaryawanMutasi", varB, "constrmf");

            if (ddltipemutasi.SelectedValue=="A1")
            {
                txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                InsertCombo();
                ddlcabang.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Kodecabang"].ToString();
                ddlbosid.Enabled = false;
                ddlbosid.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bos_id"].ToString();
                Pnlmutasicabang.Visible = true;
                ddltipemutasi.Enabled = false;
                pnlfooter.Visible = true;
                btnproses.Enabled = false;

                ddlnik.Enabled = false;
                btnSimpan.Enabled = true;

                ddlcabangbaru.Enabled = true;

            }
            else if (ddltipemutasi.SelectedValue == "A2")
            {
                txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                InsertCombo();
                ddlcabang.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Kodecabang"].ToString();
                txtgajipokok.Text= ge.ListDataset.Tables[0].Rows[0]["JlhGajiPokok"].ToString();
                txtjunganmakan.Text = ge.ListDataset.Tables[0].Rows[0]["TunMakan"].ToString();
                txttunjangantransport.Text = ge.ListDataset.Tables[0].Rows[0]["TunTransport"].ToString();

                Pnlmutasicabang.Visible = false;
                ddltipemutasi.Enabled = false;
                btnproses.Enabled = false;
                ddlnik.Enabled = false;

                Pnlgaji.Visible = true;
                pnlfooter.Visible = true;
                pnlstatuskry.Visible = false;
                btnSimpan.Enabled = true;

            }
            else if (ddltipemutasi.SelectedValue == "A3")
            {
                txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                InsertCombo();
                ddlcabang.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Kodecabang"].ToString();
                ddlgroup.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodegol"].ToString();
                ddlkodejabatan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodejab"].ToString();
                ddlbosidjabatan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bos_id"].ToString();

                Pnljabatan.Visible = true;
                Pnlmutasicabang.Visible = false;
                ddltipemutasi.Enabled = false;
                btnproses.Enabled = false;
                ddlnik.Enabled = false;

                pnlfooter.Visible = true;
                pnlstatuskry.Visible = false;
                btnSimpan.Enabled = true;

            }
            else if (ddltipemutasi.SelectedValue == "A4")
            {
                txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                InsertCombo();
                ddlcabang.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Kodecabang"].ToString();
                ddljeniskary.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();
                Pnlmutasicabang.Visible = false;               
                ddltipemutasi.Enabled = false;                
                btnproses.Enabled = false;
                ddlnik.Enabled = false;

                pnlfooter.Visible = true;
                pnlstatuskry.Visible = true;
                btnSimpan.Enabled = true;

             }
            else if (ddltipemutasi.SelectedValue == "A5")
            {
                txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                InsertCombo();
                ddlcabang.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Kodecabang"].ToString();
                ddlptkp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Ptkp"].ToString();
                Pnlmutasicabang.Visible = false;
                ddltipemutasi.Enabled = false;
                pnlstatuskry.Visible = false;
                btnproses.Enabled = false;
                ddlnik.Enabled = false;

                pnlfooter.Visible = true;
                Pnlptkp.Visible = true;
                btnSimpan.Enabled = true;

            }
            else if (ddltipemutasi.SelectedValue == "A6")
            {
                txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKaryawan"].ToString();

                InsertCombo();
                ddlcabang.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Kodecabang"].ToString();
                DateTime tglmasuk = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["Tglmasuk"].ToString());
                Txttglmasuk.Text = tglmasuk.ToString("yyyy-MM-dd"); 
                txtkodestaf.Text= "";
                txtbosid.Text = ge.ListDataset.Tables[0].Rows[0]["bos_id"].ToString();
                Txtgroup.Text = ge.ListDataset.Tables[0].Rows[0]["KetBagian"].ToString();
                TxtJabatan.Text = ge.ListDataset.Tables[0].Rows[0]["KetJab"].ToString();
                txtkodegroup.Text = ge.ListDataset.Tables[0].Rows[0]["Kodecabang"].ToString();
                txtkodejabatan.Text = ge.ListDataset.Tables[0].Rows[0]["KodeJab"].ToString();

                Pnlmutasicabang.Visible = false;
                ddltipemutasi.Enabled = false;
                btnproses.Enabled = false;
                ddlnik.Enabled = false;

                Pnlresign.Visible = true;
                pnlfooter.Visible = true;
                pnlstatuskry.Visible = false;
                btnSimpan.Enabled = true;
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
            //if (ddltipemutasi.SelectedValue== "A1")
            //{
            //    query = " insert into tbl_karyawan_mutasi ";
            //    query = query + "  (nik,nama,tipemutasi,";
            //    query = query + "  kode_cabang_old,bos_id_old,kode_cabang,";
            //    query = query + "  bos_id,catatan,";
            //    query = query + "  user_insert,tgl_insert";
            //    query = query + "  ) values ";
            //    query = query + "(";
            //    query = query + "'" + ddlnik.SelectedValue + "',";
            //    query = query + "'" + txtnama.Text + "',";
            //    query = query + "'" + ddltipemutasi.SelectedValue + "',";
            //    query = query + "'" + ddlcabang.SelectedValue + "',";
            //    query = query + "'" + ddlbosid.SelectedValue + "',";
            //    query = query + "'" + ddlcabangbaru.SelectedValue + "',";
            //    query = query + "'" + ddlbosidbaru.SelectedValue + "',";                
            //    query = query + "'" + txtcatatan.Text + "',";
            //    query = query + "'" + SessLoginID + "',";
            //    query = query + "now())";
            //    NpgsqlCommand command = new NpgsqlCommand(query, con);
            //    command.ExecuteReader();

            //}
            //if (ddltipemutasi.SelectedValue == "A2")
            //{

            //    query = " insert into tbl_karyawan_mutasi ";
            //    query = query + "  (nik,nama,tipemutasi,kode_cabang_old,";
            //    query = query + "  gaji_pokok,tunjangan_makan,tunjangan_transport,gaji_pokok_old,tunjangan_makan_old,tunjangan_transport_old,catatan,";
            //    query = query + "  user_insert,tgl_insert";
            //    query = query + "  ) values ";
            //    query = query + "(";
            //    query = query + "'" + ddlnik.SelectedValue + "',";
            //    query = query + "'" + txtnama.Text + "',";
            //    query = query + "'" + ddltipemutasi.SelectedValue + "',";
            //    query = query + "'" + ddlcabang.SelectedValue + "',";
            //    query = query + "'" + gaji_pokok_baru.Replace(",", ".") + "',";
            //    query = query + "'" + tunjangan_makan_baru.Replace(",", ".") + "',";
            //    query = query + "'" + tunjangan_transport_baru.Replace(",", ".") + "',";
            //    query = query + "'" + gaji_pokok.Replace(",", ".") + "',";
            //    query = query + "'" + tunjangan_makan.Replace(",", ".") + "',";
            //    query = query + "'" + tunjangan_transport.Replace(",", ".") + "',";
            //    query = query + "'" + txtcatatan.Text + "',";
            //    query = query + "'" + SessLoginID + "',";
            //    query = query + "now())";
            //    NpgsqlCommand command = new NpgsqlCommand(query, con);
            //    command.ExecuteReader();

            //}
            //if (ddltipemutasi.SelectedValue == "A4")
            //{
            //    query = " insert into tbl_karyawan_mutasi ";
            //    query = query + "  (nik,nama,tipemutasi,kode_cabang_old,";
            //    query = query + "  jenis_karyawn_old,jenis_karyawn,catatan,";
            //    query = query + "  user_insert,tgl_insert";
            //    query = query + "  ) values ";
            //    query = query + "(";
            //    query = query + "'" + ddlnik.SelectedValue + "',";
            //    query = query + "'" + txtnama.Text + "',";
            //    query = query + "'" + ddltipemutasi.SelectedValue + "',";
            //    query = query + "'" + ddlcabang.SelectedValue + "',";
            //    query = query + "'" + ddljeniskary.SelectedValue + "',";
            //    query = query + "'" + ddljeniskarybaru.SelectedValue + "',";              
            //    query = query + "'" + txtcatatan.Text + "',";
            //    query = query + "'" + SessLoginID + "',";
            //    query = query + "now())";
            //    NpgsqlCommand command = new NpgsqlCommand(query, con);
            //    command.ExecuteReader();

            //}
            //if (ddltipemutasi.SelectedValue == "A5")
            //{
            //    query = " insert into tbl_karyawan_mutasi ";
            //    query = query + "  (nik,nama,tipemutasi,kode_cabang_old,";
            //    query = query + "  ptkp_old,ptkp,catatan,";
            //    query = query + "  user_insert,tgl_insert";
            //    query = query + "  ) values ";
            //    query = query + "(";
            //    query = query + "'" + ddlnik.SelectedValue + "',";
            //    query = query + "'" + txtnama.Text + "',";
            //    query = query + "'" + ddltipemutasi.SelectedValue + "',";
            //    query = query + "'" + ddlcabang.SelectedValue + "',";
            //    query = query + "'" + ddlptkp.SelectedValue + "',";
            //    query = query + "'" + ddlptkpbaru.SelectedValue + "',";
            //    query = query + "'" + txtcatatan.Text + "',";
            //    query = query + "'" + SessLoginID + "',";
            //    query = query + "now())";
            //    NpgsqlCommand command = new NpgsqlCommand(query, con);
            //    command.ExecuteReader();
            //}
            //if (ddltipemutasi.SelectedValue == "A3")
            //{
            //    query = " insert into tbl_karyawan_mutasi ";
            //    query = query + "  (nik,nama,tipemutasi,kode_cabang_old,";
            //    query = query + "  groupx,bos_id,kode_jabatan,groupx_old,bos_id_old,kode_jabatan_old,catatan,";
            //    query = query + "  user_insert,tgl_insert";
            //    query = query + "  ) values ";
            //    query = query + "(";
            //    query = query + "'" + ddlnik.SelectedValue + "',";
            //    query = query + "'" + txtnama.Text + "',";
            //    query = query + "'" + ddltipemutasi.SelectedValue + "',";
            //    query = query + "'" + ddlcabang.SelectedValue + "',";
            //    query = query + "'" + ddlgroupbaru.SelectedValue + "',";
            //    query = query + "'" + ddlbosidjabatanbaru.SelectedValue + "',";
            //    query = query + "'" + ddlkodejabatanbaru.SelectedValue + "',";
            //    query = query + "'" + ddlgroup.SelectedValue + "',";
            //    query = query + "'" + ddlbosidjabatan.SelectedValue + "',";
            //    query = query + "'" + ddlkodejabatan.SelectedValue + "',";
            //    query = query + "'" + txtcatatan.Text + "',";
            //    query = query + "'" + SessLoginID + "',";
            //    query = query + "now())";
            //    NpgsqlCommand command = new NpgsqlCommand(query, con);
            //    command.ExecuteReader();
            //}
            //if (ddltipemutasi.SelectedValue == "A6")
            //{
            //    DateTime tglmasuk, tglresign;

            //    tglmasuk = Convert.ToDateTime(Txttglmasuk.Text);
            //    tglresign = Convert.ToDateTime(txttglresign.Text);
            //    //= tglmasuk.ToString("dd-MM-yyyy");

            //    query = " insert into tbl_karyawan_mutasi ";
            //    query = query + "  (nik,nama,tipemutasi,kode_cabang_old,";
            //    query = query + "  tgl_masuk,tgl_resign_efektif,groupx_old,bos_id_old,kode_jabatan_old,kd_staff_old,catatan,";
            //    query = query + "  user_insert,tgl_insert";
            //    query = query + "  ) values ";
            //    query = query + "(";
            //    query = query + "'" + ddlnik.SelectedValue + "',";
            //    query = query + "'" + txtnama.Text + "',";
            //    query = query + "'" + ddltipemutasi.SelectedValue + "',";
            //    query = query + "'" + ddlcabang.SelectedValue + "',";
            //    query = query + "'" + tglmasuk + "',";
            //    query = query + "'" + tglresign + "',";
            //    query = query + "'" + txtkodegroup.Text + "',";
            //    query = query + "'" + txtbosid.Text + "',";
            //    query = query + "'" + txtkodejabatan.Text + "',";
            //    query = query + "'" + txtkodestaf.Text + "',";
            //    query = query + "'" + txtcatatan.Text + "',";
            //    query = query + "'" + SessLoginID + "',";
            //    query = query + "now())";
            //    NpgsqlCommand command = new NpgsqlCommand(query, con);
            //    command.ExecuteReader();
            //}

            string gaji_pokok;
            string tunjangan_makan;
            string tunjangan_transport;
            string gaji_pokok_baru;
            string tunjangan_makan_baru;
            string tunjangan_transport_baru;
            string totalgajibaru;
            string total_gaji;

            gaji_pokok = Convert.ToString(Convert.ToDecimal(txtgajipokok.Text));
            tunjangan_makan = Convert.ToString(Convert.ToDecimal(txtjunganmakan.Text));
            tunjangan_transport = Convert.ToString(Convert.ToDecimal(txttunjangantransport.Text));

            gaji_pokok_baru = Convert.ToString(Convert.ToDecimal(txtgajipokokBaru.Text));
            tunjangan_makan_baru = Convert.ToString(Convert.ToDecimal(txtjunganmakanBaru.Text));
            tunjangan_transport_baru = Convert.ToString(Convert.ToDecimal(txttunjangantransportBaru.Text));

            total_gaji = Convert.ToString(Convert.ToDecimal(txtgajipokok.Text) + Convert.ToDecimal(txtjunganmakan.Text) + Convert.ToDecimal(txttunjangantransport.Text));
            totalgajibaru = Convert.ToString(Convert.ToDecimal(txtgajipokokBaru.Text) + Convert.ToDecimal(txtjunganmakanBaru.Text) + Convert.ToDecimal(txttunjangantransportBaru.Text));
            
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[28];
            var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnik.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
            var[3]  = gb.CreateSPParameter("@TglKeluar", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglresign.Text, typeof(string));
            var[4]  = gb.CreateSPParameter("@bos_id", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlbosid.SelectedValue, typeof(string));
            var[5]  = gb.CreateSPParameter("@KodeJab", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlkodejabatanbaru.SelectedValue, typeof(string));
            var[6]  = gb.CreateSPParameter("@KodeBagian", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlgroupbaru.SelectedValue, typeof(string));
            var[7]  = gb.CreateSPParameter("@KodeDept", ParameterDirection.Input, SqlDbType.VarChar, 40, "", typeof(string));
            var[8]  = gb.CreateSPParameter("@KodeGol", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlgroupbaru.SelectedValue, typeof(string));
            var[9]  = gb.CreateSPParameter("@Ptkp", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlptkp.SelectedValue, typeof(string));
            var[10] = gb.CreateSPParameter("@StatusKry", ParameterDirection.Input, SqlDbType.VarChar, 40, ddljeniskary.SelectedValue, typeof(string));
            var[11] = gb.CreateSPParameter("@Ptkpbaru", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlptkpbaru.SelectedValue, typeof(string));
            var[12] = gb.CreateSPParameter("@StatusKrybaru", ParameterDirection.Input, SqlDbType.VarChar, 40, ddljeniskarybaru.SelectedValue, typeof(string)); 
            var[13] = gb.CreateSPParameter("@JlhGajiPokok", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(gaji_pokok), typeof(decimal));
            var[14] = gb.CreateSPParameter("@TunMakan", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(tunjangan_makan), typeof(decimal));
            var[15] = gb.CreateSPParameter("@TunTransport", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(tunjangan_transport), typeof(decimal));
            var[16] = gb.CreateSPParameter("@totalgaji", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(total_gaji), typeof(decimal));
            var[17] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[18] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[19] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            var[20] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[21] = gb.CreateSPParameter("@NamaKaryawan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnama.Text, typeof(string));
            var[22] = gb.CreateSPParameter("@JlhGajiPokoklama", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(gaji_pokok_baru), typeof(decimal));
            var[23] = gb.CreateSPParameter("@TunMakanlama", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(tunjangan_makan_baru), typeof(decimal));
            var[24] = gb.CreateSPParameter("@TunTransportlama", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(tunjangan_transport_baru), typeof(decimal));
            var[25] = gb.CreateSPParameter("@bos_idbaru", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlbosidbaru.SelectedValue, typeof(string));
            var[26] = gb.CreateSPParameter("@catatan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtcatatan.Text, typeof(string));
            var[27] = gb.CreateSPParameter("@jenismutasi", ParameterDirection.Input, SqlDbType.VarChar, 40, ddltipemutasi.SelectedValue, typeof(string));
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancelmutasi_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.AbsoluteUri);
        Pnlresign.Visible = false;
        Pnlgaji.Visible = false;
        Pnljabatan.Visible = false;
        Pnlptkp.Visible = false;
        Pnlmutasicabang.Visible = false;
        pnlstatuskry.Visible = false;
        pnlfooter.Visible = false;
        ddltipemutasi.Enabled = true;
        btnproses.Enabled = true;
        ddlnik.Enabled = true;
        txtnama.Text = "";
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
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", varG, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGol asc";

            ddlgroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlgroup.DataTextField = "nama";
            ddlgroup.DataValueField = "KodeGol";
            ddlgroup.DataBind();
            ddlgroup.Items.Insert(0, new ListItem("Pilih Group", ""));

            DefaultClass.Global.SqlHelper[] varB = new DefaultClass.Global.SqlHelper[5];
            varB[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varB[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNIKBOS", typeof(string));
            varB[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varB[3] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnik.SelectedValue, typeof(string));
            varB[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", varB, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";

            ddlbosid.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlbosid.DataTextField = "nama";
            ddlbosid.DataValueField = "KodeNik";
            ddlbosid.DataBind();
            ddlbosid.Items.Insert(0, new ListItem("Pilih Karyawan", ""));

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNIK", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";
            ddlnik.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlnik.DataTextField = "nama";
            ddlnik.DataValueField = "KodeNik";
            ddlnik.DataBind();
            ddlnik.Items.Insert(0, new ListItem("Pilih Karyawan", ""));

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJABATAN", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

            ddlkodejabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkodejabatan.DataTextField = "nama";
            ddlkodejabatan.DataValueField = "KodeJab";
            ddlkodejabatan.DataBind();
            ddlkodejabatan.Items.Insert(0, new ListItem("Pilih Jabatan", ""));

            // --- Combo Kode Cabang
            DefaultClass.Global.SqlHelper[] varD = new DefaultClass.Global.SqlHelper[3];
            varD[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varD[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANGALL", typeof(string));
            varD[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", varD, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

            ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabang.DataTextField = "nama";
            ddlcabang.DataValueField = "KodeCabang";
            ddlcabang.DataBind();
            ddlcabang.Items.Insert(0, new ListItem("Pilih Cabang", ""));

            // --- Combo Kode CabangBaru
            DefaultClass.Global.SqlHelper[] varC = new DefaultClass.Global.SqlHelper[4];
            varC[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANGBARU", typeof(string));
            varC[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varC[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnik.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeKaryawanMutasi", varC, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";
            ddlcabangbaru.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabangbaru.DataTextField = "nama";
            ddlcabangbaru.DataValueField = "KodeCabang";
            ddlcabangbaru.DataBind();
            ddlcabangbaru.Items.Insert(0, new ListItem("Pilih Cabang", ""));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion "Insertdata"
}