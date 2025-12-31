using DefaultClass;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using EntitiesClass;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AR_Process_Pengajuan_Fee_BM : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }

    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];

        var[0] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
        var[1] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Get_Fee_BM", var, "constrmf");

        
        GRDT.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRDT.DataBind();

        GRDT.UseAccessibleHeader = true;

        GRDT.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public decimal Get_fee_bm(string kode_cabang)
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, kode_cabang, typeof(string));
        var[1] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "FEE", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Get_fee_bm_lama", var, "constrmf");

       // ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + string.Format("{0}", ge.ListDataset.Tables[0].Rows[0][0]) + "');", true);

        return decimal.Parse(ge.ListDataset.Tables[0].Rows[0][0].ToString());
    }

    public string Get_no_hp(string kode_cabang)
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, kode_cabang, typeof(string));
        var[1] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "HP", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Get_fee_bm_lama", var, "constrmf");

        return ge.ListDataset.Tables[0].Rows[0][0].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //txtFeelama.Text = Get_fee_bm(SessBranchID).ToString("##.####");
            //txtHplama.Text = Get_no_hp(SessBranchID).ToString();

            //txtFeebaru.Text = Get_fee_bm(SessBranchID).ToString("G");
            //txtHpbaru.Text = Get_no_hp(SessBranchID).ToString();

            if (!SessLoginID.Equals("PUSAT01"))
            {
                memo_pusat.Visible = false;
                txtFeebaru.Enabled = true;
                txtHpbaru.Enabled = true;
                btnAdd.Visible = true;
            } else
            {
                memo_pusat.Visible = true;
                txtFeebaru.Enabled = false;
                txtHpbaru.Enabled = false;
                btnAdd.Visible = false;
            }

            BindGridEntity();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlTambah.Visible = true;
        pnlButton.Visible = true;
        pnlGrid.Visible = false;
        pnlHeader.Visible = false;
        memo_pusat.Visible=false;

        txtMemocabang.Text = "";
        ddlPengajuan.SelectedIndex = 0;

        txtTglpengajuan.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtFeelama.Text = Get_fee_bm(SessBranchID).ToString("##.####");
        txtHplama.Text = Get_no_hp(SessBranchID).ToString();

        txtFeebaru.Text = Get_fee_bm(SessBranchID).ToString("##.####");
        txtHpbaru.Text = Get_no_hp(SessBranchID).ToString();
        ddlPengajuan.SelectedIndex = 0;

        if (DateTime.Today.Day.Equals(DayOfWeek.Saturday) || DateTime.Today.Day.Equals(DayOfWeek.Sunday))
        {
            btnSave.Enabled = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlTambah.Visible = false;
        pnlButton.Visible = false;
        pnlFee.Visible = false;
        pnlNohp.Visible = false;
        pnlApproval.Visible = false;
        pnlGrid.Visible = true;
        pnlHeader.Visible = true;
        pnlFee.Visible = false;
        pnlNohp.Visible = false;

        btnSave.Visible = true;
        btnApprove.Visible = false;
        btnTolak.Visible = false;
      
        GRDT.UseAccessibleHeader = true;

        GRDT.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var jenis = ddlPengajuan.SelectedValue;

        if (jenis == "1")
        {
            txtHpbaru.Text = "-";
        } else
        {
            txtFeebaru.Text = "0";
        }

        try
        {
            
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];

            var[0] = gb.CreateSPParameter("@no_bukti", ParameterDirection.Input, SqlDbType.VarChar, 20, null, typeof(string));
            var[1] = gb.CreateSPParameter("@kode_perusahaan", ParameterDirection.Input, SqlDbType.VarChar, 60, "01", typeof(string));
            var[2] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 60, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.Int, 1, jenis, typeof(int));
            var[4] = gb.CreateSPParameter("@fee_bm", ParameterDirection.Input, SqlDbType.Decimal, 18, decimal.Parse(txtFeebaru.Text), typeof(decimal));
            var[5] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 13, txtHpbaru.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtMemocabang.Text, typeof(string));
            var[7] = gb.CreateSPParameter("@note_app", ParameterDirection.Input, SqlDbType.VarChar, 50, null, typeof(string));
            var[8] = gb.CreateSPParameter("@kode_user", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            var[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "INSERT", typeof(string));
            var[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Fee_BM_Insert", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Saved successfully','top-right','success','2');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                return;
                // gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }

            pnlHeader.Visible = true;
            pnlTambah.Visible = false;
            pnlFee.Visible = false;
            pnlNohp.Visible = false;
            pnlApproval.Visible = false;
            pnlButton  .Visible = false;
            pnlGrid.Visible = true;

            BindGridEntity();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnDelete_click(string no_bukti)
    {
       
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[1];
        var[0] = gb.CreateSPParameter("@no_bukti", ParameterDirection.Input, SqlDbType.VarChar, 20, no_bukti, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Fee_bm_delete", var, "constrmf");

        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        {
            BindGridEntity();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
            // gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnApproval_click(string no_bukti, int jenis)
    {
        pnlHeader.Visible = false;
        pnlGrid.Visible = false;
        pnlApproval.Visible = true;
        pnlButton.Visible = true;

        memo_pusat.Visible = true;
        btnApprove.Visible = true;
        btnTolak.Visible = true;
        btnSave.Visible = false;

        if (jenis == 1)
        {
            pnlFee.Visible = true;
            pnlNohp.Visible = false;
        }
        else if (jenis == 2)
        {
            pnlFee.Visible = false;
            pnlNohp.Visible = true;

        }
    }

    protected void ddlPengajuan_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList comboBox = (DropDownList)sender;

        var pilihan = comboBox.SelectedValue;

        btnSave.Enabled = true;

        if (pilihan == "1")
        {
            pnlFee.Visible = true;
            pnlApproval.Visible = true;
            pnlNohp.Visible = false;
        }
        else if (pilihan == "2")
        {
            pnlFee.Visible = false;
            pnlNohp.Visible = true;
            pnlApproval.Visible = true;
        } else
        {
            pnlFee.Visible = false;
            pnlNohp.Visible = false;
            pnlApproval.Visible = false;
            btnSave.Enabled = false;
        }
    }

    protected void GRDT_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SETAPP":

                try
                {
                    string nobukti = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    int jenis = int.Parse(((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblJenis")).Text);

                    btnApproval_click(nobukti, jenis);

                    no_bukti_hidden.Value = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    jenis_hidden.Value = jenis.ToString();

                    cabang_hidden.Value = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodecabang")).Text;

                    txtFeelama.Text = Get_fee_bm(cabang_hidden.Value).ToString("##.####");
                    txtFeebaru.Text = ( jenis == 1 ? ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfee")).Text.Replace(".",",") : "0" );

                    txtHplama.Text = Get_no_hp(cabang_hidden.Value);
                    txtHpbaru.Text = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoHp")).Text;

                    txtMemocabang.Text = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":

                try
                {
                    string nobukti = ((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[1];
                    x[0] = gb.CreateSPParameter("@no_bukti", ParameterDirection.Input, SqlDbType.VarChar, 20, nobukti, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Fee_bm_delete", x, "constrmf");

                    btnDelete_click(((Label)GRDT.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text);

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

        }
    }

    protected void GRDT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        
        {
            var del = (LinkButton)e.Row.FindControl("btnDelete");
            var apv = (LinkButton)e.Row.FindControl("btnApproval");

            Label statusmemo = (Label)e.Row.FindControl("lblstatus");

            // Jika bukan Approver, tombol approval tidak tampil

            if (!SessLoginID.Equals("PUSAT01"))
            {
                // Jika sudah diapprove / tolak tidak bisa diubah / hapus

                apv.Text = "";

                if (!statusmemo.Text.Equals("Menunggu Approval"))
                {
                    del.Text = "";
                }

            }
            else
            {
                del.Text = "";

                if (!statusmemo.Text.Equals("Menunggu Approval"))
                {
                    apv.Text = "";
                }
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
      
        try
           {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];

            var[0] = gb.CreateSPParameter("@no_bukti", ParameterDirection.Input, SqlDbType.VarChar, 20, no_bukti_hidden.Value, typeof(string));
            var[1] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 3, cabang_hidden.Value, typeof(string));
            var[2] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.Int, 1, jenis_hidden.Value, typeof(int));
            var[3] = gb.CreateSPParameter("@fee_bm", ParameterDirection.Input, SqlDbType.Decimal, 18, decimal.Parse(txtFeebaru.Text), typeof(decimal));
            var[4] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 13, txtHpbaru.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@note_app", ParameterDirection.Input, SqlDbType.VarChar, 50, txtMemopusat.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "APPROVE", typeof(string));
            var[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Fee_BM_Insert", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Saved successfully','top-right','success','2');", true);
            }
            else
            {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                return;
                // gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }

            pnlHeader.Visible = true;
            pnlTambah.Visible = false;
            pnlFee.Visible = false;
            pnlNohp.Visible = false;
            pnlApproval.Visible = false;
            pnlButton.Visible = false;
            pnlGrid.Visible = true;

            BindGridEntity();
        }
        catch (Exception X)
        {
           ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnTolak_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];

            var[0] = gb.CreateSPParameter("@no_bukti", ParameterDirection.Input, SqlDbType.VarChar, 20, no_bukti_hidden.Value, typeof(string));
            var[1] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 3, cabang_hidden.Value, typeof(string));
            var[2] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.Int, 1, jenis_hidden.Value, typeof(int));
            var[3] = gb.CreateSPParameter("@fee_bm", ParameterDirection.Input, SqlDbType.Decimal, 18, decimal.Parse(txtFeebaru.Text), typeof(decimal));
            var[4] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 13, txtHpbaru.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@note_app", ParameterDirection.Input, SqlDbType.VarChar, 50, txtMemopusat.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "TOLAK", typeof(string));
            var[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Fee_BM_Insert", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Saved successfully','top-right','success','2');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                return;
                // gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }

            pnlHeader.Visible = true;
            pnlTambah.Visible = false;
            pnlFee.Visible = false;
            pnlNohp.Visible = false;
            pnlApproval.Visible = false;
            pnlButton.Visible = false;
            pnlGrid.Visible = true;

            BindGridEntity();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
}