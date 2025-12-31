using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AS_Process_PengajuanPolis : System.Web.UI.Page
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

    DataTable dtRP = new DataTable("PengajuanPolis");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    int nosekarang = 0;
    string formatnumber = "";
    string nosebelumnya = "";
    int nonasabah = 0;
    string CRNonasabah = "";
    string updateno = "";
    string kodeawal = "";


    protected void btnAdd_Click(object sender, EventArgs e)
    { 
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        //pnladd.Visible = true;
        //txtnopolis.Enabled = true;
        //txtnopolis.Focus(); 
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                //culture
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();


                //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                //DateTime xtanggal = DateTime.Now;
                //txttglaju.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttglawal.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttglakhir.Text = xtanggal.ToString("dd-MM-yyyy");
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
        ////DROPDOWNLIST Id Permohonan
        //DefaultClass.Global.SqlHelper[] varIdpermohonan = new DefaultClass.Global.SqlHelper[4];
        //varIdpermohonan[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTIDPERMOHONAN", typeof(string));
        //varIdpermohonan[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //varIdpermohonan[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //varIdpermohonan[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("As_PengajuanPolis", varIdpermohonan, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "IdPermohonan asc";

        //ddlidpermohonan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlidpermohonan.DataTextField = "idnm";
        //ddlidpermohonan.DataValueField = "idpermohonan";
        //ddlidpermohonan.DataBind();
        //ddlidpermohonan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //DROPDOWNLIST kode asuransi
        DefaultClass.Global.SqlHelper[] varkodeasuransi = new DefaultClass.Global.SqlHelper[4];
        varkodeasuransi[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEASURANSI", typeof(string));
        varkodeasuransi[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varkodeasuransi[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varkodeasuransi[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("As_PengajuanPolis", varkodeasuransi, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAs asc";

        ddlasuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlasuransi.DataTextField = "kdnm";
        ddlasuransi.DataValueField = "KodeAs";
        ddlasuransi.DataBind();
        ddlasuransi.Items.Insert(0, new ListItem("Pilih Data", "-"));


    }
    protected void onselectedchanged_kodeasuransi(object sender, EventArgs e)
    {
        try
        {
            if (ddlasuransi.SelectedValue == "-")
            {
                Pnlfooter.Visible = false;
            }
            else if (txttglawal.Text == "" || txttglakhir.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Isi Tanggal Awal Periode dan Akhir Periode!');", true);
            }
            else
            {
                PnlFront.Visible = true;

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var[1] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlasuransi.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[4] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[5] = gb.CreateSPParameter("@tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglawal.Text, typeof(DateTime));
                var[6] = gb.CreateSPParameter("@tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglakhir.Text, typeof(DateTime));

                ge.ListDataset = gb.GetDataJustOutPut("As_PengajuanPolis", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "idpermohonan asc";


                GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GRData.DataBind();
                GRData.UseAccessibleHeader = true;

                GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

                Pnlfooter.Visible = true;



            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        //econdGrid.Rows[secondGrid.EditItem.Index].Cells[0].FindControl("textboxid").value = "valueToFIeld";
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

             
        }
    }
    protected void ckbx_checked(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GRData.HeaderRow.FindControl("chkHeader");
        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbx");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            STATE = "INSERT";
            dtRP.Columns.Clear();

            //dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
            dtRP.Columns.Add(new DataColumn("Idpermohonan", typeof(string)));
            dtRP.Columns.Add(new DataColumn("reg", typeof(string)));
            dtRP.Columns.Add(new DataColumn("norek", typeof(string)));
            dtRP.Columns.Add(new DataColumn("nama", typeof(string)));
            dtRP.Columns.Add(new DataColumn("Sex", typeof(string)));
            dtRP.Columns.Add(new DataColumn("alamat", typeof(string)));
            dtRP.Columns.Add(new DataColumn("id", typeof(string)));
            dtRP.Columns.Add(new DataColumn("tptlahir", typeof(string)));
            dtRP.Columns.Add(new DataColumn("tgllahir", typeof(DateTime)));
            dtRP.Columns.Add(new DataColumn("Usia", typeof(string)));
            dtRP.Columns.Add(new DataColumn("tglawal", typeof(DateTime)));
            dtRP.Columns.Add(new DataColumn("tglakhir", typeof(DateTime)));
            dtRP.Columns.Add(new DataColumn("bulan", typeof(string)));
            dtRP.Columns.Add(new DataColumn("plafon", typeof(decimal)));
            dtRP.Columns.Add(new DataColumn("rateasuransi", typeof(decimal)));
            dtRP.Columns.Add(new DataColumn("GrossPremi", typeof(decimal)));
            dtRP.Columns.Add(new DataColumn("UnderWriting", typeof(string)));

            dtRP.Rows.Clear();

            for (int i = 0; i < GRData.Rows.Count; i++)
            {
                //foreach (GridViewRow row in GRData.Rows)
                //{
                    CheckBox ChkBoxHeader = (CheckBox)GRData.Rows[i].FindControl("ckbx");
                if (ChkBoxHeader.Checked == true)
                {
                    DataRow dr = dtRP.NewRow();

                    dr["Idpermohonan"] = ((Label)GRData.Rows[i].FindControl("lblidpermohonan")).Text;
                    dr["reg"] = ((Label)GRData.Rows[i].FindControl("lblnonas")).Text;
                    dr["norek"] = ((Label)GRData.Rows[i].FindControl("lblnorek")).Text;
                    dr["nama"] = ((Label)GRData.Rows[i].FindControl("lblnanas")).Text;
                    dr["Sex"] = ((Label)GRData.Rows[i].FindControl("lblsex")).Text;
                    dr["alamat"] = ((Label)GRData.Rows[i].FindControl("lblalamat")).Text;
                    dr["id"] = ((Label)GRData.Rows[i].FindControl("lblktp")).Text;
                    dr["tptlahir"] = ((Label)GRData.Rows[i].FindControl("lbltmptlahir")).Text;
                    dr["tgllahir"] = Convert.ToDateTime(((Label)GRData.Rows[i].FindControl("lbltgllahir")).Text);
                    dr["Usia"] = ((Label)GRData.Rows[i].FindControl("lblusia")).Text;
                    dr["tglawal"] = Convert.ToDateTime(((Label)GRData.Rows[i].FindControl("lblawal")).Text);
                    dr["tglakhir"] = Convert.ToDateTime(((Label)GRData.Rows[i].FindControl("lblakhir")).Text);
                    dr["bulan"] = ((Label)GRData.Rows[i].FindControl("lblbulan")).Text;
                    dr["plafon"] = decimal.Parse(((Label)GRData.Rows[i].FindControl("lblplafon")).Text);// ((Label)GRData.Rows[i].FindControl("lblplafon")).Text;
                    dr["rateasuransi"] = decimal.Parse(((Label)GRData.Rows[i].FindControl("lblrateasuransi")).Text);// ((Label)GRData.Rows[i].FindControl("lblrateasuransi")).Text;
                    dr["GrossPremi"] = decimal.Parse(((Label)GRData.Rows[i].FindControl("lblgross")).Text); //((Label)GRData.Rows[i].FindControl("lblgross")).Text;
                    dr["UnderWriting"] = ((Label)GRData.Rows[i].FindControl("lblUnderWritting")).Text;
                    //decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                    dtRP.Rows.Add(dr);

                    var idpermohonan = ((Label)GRData.Rows[i].FindControl("lblidpermohonan")).Text;
                    var norek = ((Label)GRData.Rows[i].FindControl("lblnorek")).Text;
                    var nonasabah = ((Label)GRData.Rows[i].FindControl("lblnonas")).Text;
                    DefaultClass.Global.SqlHelper[] varUPDATE = new DefaultClass.Global.SqlHelper[7];
                    varUPDATE[0] = gb.CreateSPParameter("@idpermohonan", ParameterDirection.Input, SqlDbType.VarChar, 40, idpermohonan, typeof(DateTime));
                    varUPDATE[1] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlasuransi.SelectedValue, typeof(string));
                    varUPDATE[2] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 40, nonasabah, typeof(string));
                    varUPDATE[3] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 40, norek, typeof(string));
                    varUPDATE[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATE", typeof(string));
                    varUPDATE[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varUPDATE[6] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    //var[8] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    //var[9] = gb.CreateSPParameter("@As_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                    ge.ListDataset = gb.GetDataJustOutPut("As_PengajuanPolis", varUPDATE, "constrmf");
                }
                //}
            }

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
            var[0] = gb.CreateSPParameter("@tglpengajuan", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglaju.Text, typeof(DateTime));
            var[1] = gb.CreateSPParameter("@tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglawal.Text, typeof(DateTime));
            var[2] = gb.CreateSPParameter("@tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglakhir.Text, typeof(DateTime));
            var[3] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlasuransi.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
            var[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[7] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[8] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[9] = gb.CreateSPParameter("@As_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
            var[10] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 40, ddltypeTrn.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("As_PengajuanPolis", var, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true); 
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
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    } 
}