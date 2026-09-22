using CrystalDecisions.CrystalReports.Engine;
using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
public partial class Report_datamarketingfollowup : System.Web.UI.Page
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
    private string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    private string SessKoperasiNama
    {
        get { return Session["SessKoperasiNama"].ToString(); }
        set { ViewState["SessKoperasiNama"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    public int bahanhariini = 0;


    [Serializable]
    private class PetugasDto
    {
        public string Nama { get; set; }
        public string NoTelp { get; set; }
    }
    private List<PetugasDto> PetugasList
    {
        get
        {
            if (ViewState["PetugasList"] == null)
                ViewState["PetugasList"] = new List<PetugasDto>();

            return (List<PetugasDto>)ViewState["PetugasList"];
        }
        set
        {
            ViewState["PetugasList"] = value;
        }
    }

    bool IsValidPhone(string phone)
    {
        //@"^(?:\+62|62|0)8[1-9][0-9]{6,9}$"
        return Regex.IsMatch(phone, @"^(?:\+62|62|0)\s?8[1-9](?:[\s\-]?[0-9]){6,9}$");
    }

    private void BindPetugas()
    {
        rptPetugas.DataSource = PetugasList;
        rptPetugas.DataBind();
    }

    protected void btnTambahPetugas_Click(object sender, EventArgs e)
    {
        SyncRepeaterToModel();
        PetugasList.Add(new PetugasDto());
        BindPetugas();
    }

    protected void rptPetugas_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            SyncRepeaterToModel();

            if (PetugasList.Count <= 1)
            {
                // Hard rule: minimal 1 petugas
                return;
            }

            int index = Convert.ToInt32(e.CommandArgument);
            PetugasList.RemoveAt(index);

            BindPetugas();
        }
    }

    private void SyncRepeaterToModel()
    {
        var result = new List<PetugasDto>();

        foreach (RepeaterItem item in rptPetugas.Items)
        {
            var nama = ((TextBox)item.FindControl("txtNamaPetugas")).Text;
            var telp = ((TextBox)item.FindControl("txtNoTelp")).Text;

            result.Add(new PetugasDto
            {
                Nama = nama,
                NoTelp = telp
            });
        }

        PetugasList = result;
    }

    protected void btnCetakSuratPenawaran_Click(object sender, EventArgs e)
    {

        try
        {

            SyncRepeaterToModel();

            if (PetugasList.Count < 1)
                throw new Exception("Minimal satu petugas wajib diisi.");

            using (ReportDocument rprt = new ReportDocument())
            {
                var jsonPetugas = new JavaScriptSerializer().Serialize(PetugasList);
                string notas = txtNotasPenewaran.Text;
                string namaNasabah = txtNamaNasabah.Text;
                string alamatNasabah = txtAlamatNasabah.Text;

                var paramList = new List<DefaultClass.Global.SqlHelper> {
                    gb.CreateSPParameter("@Notas", ParameterDirection.Input, SqlDbType.VarChar, 50, notas, typeof(string)),
                    gb.CreateSPParameter("@PetugasJson", ParameterDirection.Input, SqlDbType.VarChar, 0, jsonPetugas, typeof(string)),
                    gb.CreateSPParameter("@session_id", ParameterDirection.Input, SqlDbType.VarChar, 15, SessLoginID, typeof(string)),
                    gb.CreateSPParameter("@session_branch_id", ParameterDirection.Input, SqlDbType.VarChar, 9, SessBranchID, typeof(string)),
                    gb.CreateSPParameter("@session_koperasi_id", ParameterDirection.Input, SqlDbType.VarChar, 9, SessKoperasiID, typeof(string)),
                    gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, namaNasabah, typeof(string)),
                    gb.CreateSPParameter("@alamat_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 500, alamatNasabah, typeof(string)),
                };

                ge.ListDataTable = gb.GetDataJustOutPut("BG_CETAK_SURAT_PENAWARAN", paramList.ToArray(), "constrmf").Tables[0];

                if (ge.ListDataTable.Rows.Count == 0)
                    throw new Exception("Data Not Found.");

                rprt.Load(Server.MapPath("~/BG/Report/SURAT_PENAWARAN.rpt"));
                rprt.SetDataSource(ge.ListDataTable);

                Response.ClearContent();
                Response.ClearHeaders();

                ReportHelper.ExportPdfInline(rprt, "Surat_Penawaran_" + notas, Response);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
        }

    }

    protected void btnKembaliPenawaran_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = true;

        Pnlakanfollow.Visible = false;
        Pnlcancel.Visible = false;
        if (ddlbahan.SelectedValue == "TASPEN")
        {
            Pnltagihan.Visible = true;
            BindGridEntityTagihan();
        }
        else
        {
            Pnlasabari.Visible = true;
            BindGridEntityAsabri();
        }

        CloseFormCetakPenawaran();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                PetugasList = new List<PetugasDto>
                {
                    new PetugasDto()
                };

                BindPetugas();



                datacancel.Value = "0";
                string query;

                //query = "Select idfollowup || ' - ' || namafollowup as nama,idfollowup from tbl_followup";
                //DataTable areas = GetData(query);

                DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];
                vart[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "FOLLOWUP", typeof(string));
                vart[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                vart[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES", vart, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "idstatus asc";
                ddlstatus.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlstatus.DataTextField = "nama";
                ddlstatus.DataValueField = "idstatus";
                ddlstatus.DataBind();
                ddlstatus.Items.Insert(0, new ListItem("Filter Area", "ALL"));


                //query = "Select idstatus || ' - ' || namastatus as nama,idstatus from tbl_type_status";
                //DataTable areaz = GetData(query);
                //ddlstatus.DataSource = areaz;
                //ddlstatus.DataTextField = "nama";
                //ddlstatus.DataValueField = "idstatus";
                //ddlstatus.DataBind();
                //ddlstatus.Items.Insert(0, new ListItem("Select", ""));
                //ddlstatus.SelectedValue = "";



                //btnproses.Visible = false;
                Lbltaspen.Text = "DATA TASPEN / ASABRI";

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "USERLOCK", typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", var, "constrmf");

                bahanhariini = Convert.ToInt32(ge.ListDataset.Tables[0].Rows[0][0].ToString());
                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                varx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOTALBAHAN", typeof(string));
                varx[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                varx[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx, "constrmf");

                //bahanhariini = Convert.ToInt32(ge.ListDataset.Tables[0].Rows[0][0].ToString());

                LblOutstanding.Text = (bahanhariini + Convert.ToInt32(ge.ListDataset.Tables[0].Rows[0][0].ToString())).ToString();
                LblOutstanding.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblOutstanding.Text));





                //query = " select count(username) as total from tbl_get_bahan ";
                //query = query + " where username = '" + SessLoginID + "' and status = 0 ";

                //DataTable dtwrh = GetData(query);
                //LblOutstanding.Text = dtwrh.Rows[0][0].ToString().ToString();
                //LblOutstanding.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblOutstanding.Text));


                //DataTable dtwrx = GetData(query);
                LblBahan.Text = bahanhariini.ToString();
                LblBahan.Text = String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));


                DefaultClass.Global.SqlHelper[] varx2 = new DefaultClass.Global.SqlHelper[3];
                varx2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MINIMALBAHAN", typeof(string));
                varx2[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                varx2[2] = gb.CreateSPParameter("@kodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_GetDATA_SALES", varx2, "constrmf");

                //DataTable dtwrs = GetData(query);
                Lblminimal.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                Lblminimal.Text = String.Format("{0:#,###0}", Convert.ToInt32(Lblminimal.Text));
            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

        }
    }

    protected void BindDdlKantorBayar()
    {
        //KODEBANK
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_KANTOR_BAYAR", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KantorBayar asc";


        ddlKantorBayarKunjungan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKantorBayarKunjungan.DataTextField = "NamaKantorBayar";
        ddlKantorBayarKunjungan.DataValueField = "KantorBayar";
        ddlKantorBayarKunjungan.DataBind();
        ddlKantorBayarKunjungan.Items.Insert(0, new ListItem("Pilih Data", "-"));
    }

    public void BindGridEntityTagihan()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWTASPEN", typeof(string));
        var[1] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@statusfollow", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlstatusfollow.SelectedValue), typeof(int));

        ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nourut asc";


        Grdtagihan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtagihan.DataBind();

        Grdtagihan.UseAccessibleHeader = true;

        Grdtagihan.HeaderRow.TableSection = TableRowSection.TableHeader;
    }




    public void BindGridEntityAsabri()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWASABRI", typeof(string));
        var[1] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@statusfollow", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlstatusfollow.SelectedValue), typeof(int));

        ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nourut asc";


        Grdasabri.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdasabri.DataBind();

        Grdasabri.UseAccessibleHeader = true;

        Grdasabri.HeaderRow.TableSection = TableRowSection.TableHeader;





    }


    public void BindGridEntitytaspencancel()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWTASPENCANCEL", typeof(string));
        var[1] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@statusfollow", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlstatusfollow.SelectedValue), typeof(int));

        ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nourut asc";


        Grdtaspencancel.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtaspencancel.DataBind();

        Grdtaspencancel.UseAccessibleHeader = true;

        Grdtaspencancel.HeaderRow.TableSection = TableRowSection.TableHeader;


    }
    public void BindGridEntityasabricancel()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWASABRICANCEL", typeof(string));
        var[1] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@statusfollow", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlstatusfollow.SelectedValue), typeof(int));

        ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nourut asc";


        Grdasabricancel.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdasabricancel.DataBind();

        Grdtaspencancel.UseAccessibleHeader = true;

        Grdasabricancel.HeaderRow.TableSection = TableRowSection.TableHeader;




    }
    public void BindGridEntityhistory()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWHISTORY", typeof(string));
        var[1] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        var[2] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtnourutfollow.Text), typeof(int));

        ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nourut asc";


        GrdHistory.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdHistory.DataBind();

        GrdHistory.UseAccessibleHeader = true;

        GrdHistory.HeaderRow.TableSection = TableRowSection.TableHeader;







    }





    protected void lnkCari_Click(object sender, EventArgs e)
    {


        Pnltagihan.Visible = false;

        Pnlasabari.Visible = false;

        Pnltaspencancel.Visible = false;

        Pnlasabricancel.Visible = false;
        Pnlakanfollow.Visible = false;
        Pnlcancel.Visible = false;


        if (datacancel.Value == "0")
        {



            if (ddlbahan.SelectedValue == "TASPEN")
            {



                Pnltagihan.Visible = true;
                BindGridEntityTagihan();


            }
            else if (ddlbahan.SelectedValue == "ASABRI")
            {

                Pnlasabari.Visible = true;
                //Pnltunggakan.Visible = true;

                BindGridEntityAsabri();


            }
        }
        else
        {

            if (ddlbahan.SelectedValue == "TASPEN")
            {


                Pnltaspencancel.Visible = true;
                BindGridEntitytaspencancel();


            }
            else if (ddlbahan.SelectedValue == "ASABRI")
            {

                Pnlasabricancel.Visible = true;
                //Pnltunggakan.Visible = true;

                BindGridEntityasabricancel();


            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

    //protected void Grdasabri_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        for (int i = 0; i < e.Row.Cells.Count; i++)
    //        {
    //            if (ddlstatusfollow.SelectedValue == "0")
    //            {
    //                LinkButton status = (LinkButton)e.Row.FindControl("Lnkakanfollwox");
    //                status.Visible = true;
    //                LinkButton statusx = (LinkButton)e.Row.FindControl("Lnkcancelfollwox");
    //                statusx.Visible = true;
    //                LinkButton statusd = (LinkButton)e.Row.FindControl("LnkView1");
    //                statusd.Visible = false;

    //                LinkButton btnPenawaran = (LinkButton)e.Row.FindControl("lbCetakPenawaranAsabri");
    //                btnPenawaran.Visible = true;
    //            }
    //            else
    //            {
    //                LinkButton status = (LinkButton)e.Row.FindControl("Lnkakanfollwox");
    //                status.Visible = false;
    //                LinkButton statusx = (LinkButton)e.Row.FindControl("Lnkcancelfollwox");
    //                statusx.Visible = false;
    //                LinkButton statusd = (LinkButton)e.Row.FindControl("LnkView1");
    //                statusd.Visible = true;

    //                LinkButton btnPenawaran = (LinkButton)e.Row.FindControl("lbCetakPenawaranAsabri");
    //                btnPenawaran.Visible = false;
    //            }
    //        }
    //    }
    //}

    protected void Grdasabri_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;

        // Ambil kontrol sekali
        LinkButton lnkFollow = e.Row.FindControl("Lnkakanfollwox") as LinkButton;
        LinkButton lnkCancel = e.Row.FindControl("Lnkcancelfollwox") as LinkButton;
        LinkButton lnkHistory = e.Row.FindControl("LnkView1") as LinkButton;
        LinkButton btnOffer = e.Row.FindControl("lbCetakPenawaranAsabri") as LinkButton;

        // ambil nilai statuscancel dari datasource
        int statusCancel = Convert.ToInt32(
            DataBinder.Eval(e.Row.DataItem, "statuscancel")
        );

        // Default rule berdasarkan ddlstatusfollow
        if (ddlstatusfollow.SelectedValue == "0")
        {
            if (lnkFollow != null) lnkFollow.Visible = true;
            if (lnkCancel != null) lnkCancel.Visible = true;
            if (lnkHistory != null) lnkHistory.Visible = false;
            if (btnOffer != null) btnOffer.Visible = true;
        }
        else
        {
            if (lnkFollow != null) lnkFollow.Visible = false;
            if (lnkCancel != null) lnkCancel.Visible = false;
            if (lnkHistory != null) lnkHistory.Visible = true;
            if (btnOffer != null) btnOffer.Visible = false;
        }

        // OVERRIDE : jika statuscancel = 1 → Cancel HARUS hilang
        if (lnkCancel != null)
        {
            lnkCancel.Visible = statusCancel != 1;
            // untuk kunjungan dan penawaran apakah perlu di hide juga ?
        }
    }

    //SELECT KodeBank AS[Value], NamaBank AS[Text] FROM getkantorbayar();


    protected void Grdasabri_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string urut;
        urut = "";
        Txtnamakunjungan.Text = "";
        TxtAlamatKunjungan.Text = "";
        TxtGajikunjungan.Text = "0";
        txttgllahirkunjungan.Text = "";
        txtusia.Text = "0";
        txttelpon.Text = "";
        //txtkantorbayarkunjungan.Text = "";
        txtskonhand.Text = "";
        txtskonto.Text = "";

        txtplafonlama.Text = "0";
        txtJangkawaktulama.Text = "0";
        txtangsuranlama.Text = "0";
        txtplafonsena.Text = "0";
        txtjangkawaktusena.Text = "0";
        txtterimabersih.Text = "0";
        txtcatatan.Text = "";
        DateTime dt = DateTime.Now; // Or whatever      
        txttgllahirkunjungan.Text = dt.ToString("dd-MM-yyyy");
        txtlat.Text = "0";
        txtlon.Text = "0";
        switch (e.CommandName)
        {
            case "APPROVE":

                BindDdlKantorBayar();

                txtnourutfollow.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourut")).Text;
                //  pnlData.Visible = false;
                Pnlasabari.Visible = false;
                Pnlakanfollow.Visible = true;
                Pnlsearch.Visible = false;
                txtnotasmaster.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotas")).Text;
                Txtnama.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                txtalamat.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat")).Text;
                Txtnamakunjungan.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                TxtAlamatKunjungan.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat")).Text;
                break;
            case "CANCEL1":

                txtnourutfollow.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourut")).Text;
                //  pnlData.Visible = false;
                Pnlsearch.Visible = false;
                Pnlasabari.Visible = false;
                Pnlhistory.Visible = false;
                Pnlcancel.Visible = true;
                txtcancel.Text = "";
                txtnotascancel.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotas")).Text;
                txtnamacancel.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                txtalamatcancel.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat")).Text;
                break;
            case "HISTORY":

                txtnourutfollow.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourut")).Text;
                TextBox1.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllat")).Text;
                TextBox2.Text = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllon")).Text;

                Pnlsearch.Visible = false;
                Pnlasabari.Visible = false;
                Pnlhistory.Visible = true;
                BindGridEntityhistory();
                break;
            case "VIEW":

                urut = ((Label)Grdasabri.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourutasabri")).Text;
                //  pnlData.Visible = false;
                Pnlsearch.Visible = false;
                Pnlasabari.Visible = false;
                pnlformatasabri.Visible = true;
                TampilFormAsabri(urut);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                break;
            case "PENAWARAN":
                {
                    GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                    if (row == null)
                        return;

                    Label lblNotas = row.FindControl("lblnotas") as Label;
                    Label lblNama = row.FindControl("lblnama") as Label;
                    Label lblAlamat = row.FindControl("lblalamat") as Label;

                    if (lblNotas != null)
                        txtNotasPenewaran.Text = lblNotas.Text;

                    if (lblNama != null)
                        txtNamaNasabah.Text = lblNama.Text;

                    if (lblAlamat != null)
                        txtAlamatNasabah.Text = lblAlamat.Text;

                    OpenFormCetakPenawaran();

                    break;
                }

            default:

                BindGridEntityAsabri();

                break;
        }



    }
    public void TampilFormAsabri(string xnourut)
    {
        string query = "";

        //numberFormat is the formatter I use for numbers.
        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[2];
        Paramsc[0] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.VarChar, 50, xnourut, typeof(string));
        Paramsc[1] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TAMPILASABRI", typeof(string));

        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.BG_Get_Transaksi_TASPEN", Paramsc, "constrmf").Tables[0];
        ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES", Paramsc, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            txtnopensasbri.Text = ge.ListDataset.Tables[0].Rows[0]["nopens"].ToString();
            txtnrpasbri.Text = ge.ListDataset.Tables[0].Rows[0]["nrp"].ToString();
            txtkdjiwaasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kode_jiwa"].ToString();
            txtnamaasbri.Text = ge.ListDataset.Tables[0].Rows[0]["nama"].ToString();
            txttgllahirasabri.Text = ge.ListDataset.Tables[0].Rows[0]["tgllahir"].ToString();
            txtusiaasbri.Text = ge.ListDataset.Tables[0].Rows[0]["usia"].ToString();
            txtkd_jbyasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kd_jby"].ToString();
            txtpangkatasbri.Text = ge.ListDataset.Tables[0].Rows[0]["pangkat"].ToString();
            txtkodecabangasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
            txtnamacabangasbri.Text = ge.ListDataset.Tables[0].Rows[0]["namacabang"].ToString();
            Txtmitraasbri.Text = ge.ListDataset.Tables[0].Rows[0]["mitra"].ToString();

            txtpendapatanpokokasbri.Text = ge.ListDataset.Tables[0].Rows[0]["pens_pokok"].ToString();
            txttunjistriabsri.Text = ge.ListDataset.Tables[0].Rows[0]["tunj_istri"].ToString();
            txttunjanakasbri.Text = ge.ListDataset.Tables[0].Rows[0]["tunj_anak"].ToString();
            txttunberasbri.Text = ge.ListDataset.Tables[0].Rows[0]["tunj_beras"].ToString();
            txttunjlainasbri.Text = ge.ListDataset.Tables[0].Rows[0]["tunj_lain"].ToString();
            txtbrutoasbri.Text = ge.ListDataset.Tables[0].Rows[0]["jml_bruto"].ToString();
            txtpot_pph21.Text = ge.ListDataset.Tables[0].Rows[0]["pot_pph21"].ToString();
            txtpot_askes.Text = ge.ListDataset.Tables[0].Rows[0]["pot_askes"].ToString();
            txtpot_hutang.Text = ge.ListDataset.Tables[0].Rows[0]["pot_hutang"].ToString();
            txtpot_lain.Text = ge.ListDataset.Tables[0].Rows[0]["pot_lain"].ToString();
            txtjml_potong.Text = ge.ListDataset.Tables[0].Rows[0]["jml_potong"].ToString();
            txtjlm_netto.Text = ge.ListDataset.Tables[0].Rows[0]["jlm_netto"].ToString();
            txtpembulatan.Text = ge.ListDataset.Tables[0].Rows[0]["pembulatan"].ToString();


            txtalamatasbri.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
            txtrtasbri.Text = ge.ListDataset.Tables[0].Rows[0]["rt"].ToString();
            txtrwasbri.Text = ge.ListDataset.Tables[0].Rows[0]["rw"].ToString();
            txtkelurahanasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
            txtkecamatanasbri.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
            txtprovinsiasbri.Text = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();

            txtbank_rekening.Text = ge.ListDataset.Tables[0].Rows[0]["bank_rekening"].ToString();
            txtjenis_rekening.Text = ge.ListDataset.Tables[0].Rows[0]["jenis_rekening"].ToString();
            txtno_rekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();


        }
    }

    //protected void Grdtagihan_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        for (int i = 0; i < e.Row.Cells.Count; i++)
    //        {
    //            if (ddlstatusfollow.SelectedValue == "0")
    //            {
    //                LinkButton status = (LinkButton)e.Row.FindControl("Lnkakanfollwo");
    //                status.Visible = true;
    //                LinkButton statusx = (LinkButton)e.Row.FindControl("Lnkcancel4");
    //                statusx.Visible = true;
    //                LinkButton statusd = (LinkButton)e.Row.FindControl("LnkView2");
    //                statusd.Visible = false;

    //                LinkButton btnPenawaran = (LinkButton)e.Row.FindControl("lbCetakPenawaran");
    //                btnPenawaran.Visible = true;
    //            }
    //            else
    //            {
    //                LinkButton status = (LinkButton)e.Row.FindControl("Lnkakanfollwo");
    //                status.Visible = false;
    //                LinkButton statusx = (LinkButton)e.Row.FindControl("Lnkcancel4");
    //                statusx.Visible = false;
    //                LinkButton statusd = (LinkButton)e.Row.FindControl("LnkView2");
    //                statusd.Visible = true;

    //                LinkButton btnPenawaran = (LinkButton)e.Row.FindControl("lbCetakPenawaran");
    //                btnPenawaran.Visible = false;
    //            }
    //        }
    //    }
    //}

    protected void Grdtagihan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;

        //Lnkakanfollwo = kunjungan
        //lbCetakPenawaran = penawaran
        //Lnkcancel4 = cancel
        //LnkView2 = history
        //LinkButton3 = view

        // ambil kontrol sekali saja
        LinkButton lnkFollow = e.Row.FindControl("Lnkakanfollwo") as LinkButton;
        LinkButton lnkCancel = e.Row.FindControl("Lnkcancel4") as LinkButton;
        LinkButton lnkHistory = e.Row.FindControl("LnkView2") as LinkButton;
        LinkButton btnOffer = e.Row.FindControl("lbCetakPenawaran") as LinkButton;
        LinkButton btnHistory = e.Row.FindControl("LinkButton3") as LinkButton;


        // ambil nilai statuscancel dari datasource
        int statusCancel = Convert.ToInt32(
            DataBinder.Eval(e.Row.DataItem, "statuscancel")
        );

        // logic berdasarkan ddlstatusfollow
        if (ddlstatusfollow.SelectedValue == "0")
        {
            if (lnkFollow != null) lnkFollow.Visible = true;
            if (lnkHistory != null) lnkHistory.Visible = false;
            if (btnOffer != null) btnOffer.Visible = true;
        }
        else
        {
            if (lnkFollow != null) lnkFollow.Visible = false;
            if (lnkHistory != null) lnkHistory.Visible = true;
            if (btnOffer != null) btnOffer.Visible = false;
        }

        // OVERRIDE : jika statuscancel = 1 → Cancel HARUS hilang
        if (lnkCancel != null) lnkCancel.Visible = statusCancel != 1;
        if (lnkFollow != null) lnkFollow.Visible = statusCancel != 1;
        if (btnOffer != null) btnOffer.Visible = statusCancel != 1;
        
    }


    protected void Grdtagihan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string urut;
        urut = "";
        Txtnamakunjungan.Text = "";
        TxtAlamatKunjungan.Text = "";
        TxtGajikunjungan.Text = "0";
        txttgllahirkunjungan.Text = "";
        txtusia.Text = "0";
        txttelpon.Text = "";
        //txtkantorbayarkunjungan.Text = "";
        txtskonhand.Text = "";
        txtskonto.Text = "";

        txtplafonlama.Text = "0";
        txtJangkawaktulama.Text = "0";
        txtangsuranlama.Text = "0";
        txtplafonsena.Text = "0";
        txtjangkawaktusena.Text = "0";
        txtterimabersih.Text = "0";

        txtcatatan.Text = "";
        DateTime dt = DateTime.Now; // Or whatever      
        txttgllahirkunjungan.Text = dt.ToString("dd-MM-yyyy");
        txtlat.Text = "0";
        txtlon.Text = "0";
        switch (e.CommandName)
        {
            case "APPROVE":

                BindDdlKantorBayar();

                txtnourutfollow.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourut")).Text;
                //  pnlData.Visible = false;
                Pnlsearch.Visible = false;
                Pnltagihan.Visible = false;
                Pnlakanfollow.Visible = true;
                txtnotasmaster.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotas")).Text;
                Txtnama.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                txtalamat.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat")).Text;
                Txtnamakunjungan.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                TxtAlamatKunjungan.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat")).Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

                break;
            case "CANCEL3":

                txtnourutfollow.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourut")).Text;
                //  pnlData.Visible = false;
                txtnotascancel.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotas")).Text;
                txtnamacancel.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                txtalamatcancel.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat")).Text;
                txtcancel.Text = "";
                Pnlsearch.Visible = false;
                Pnltagihan.Visible = false;
                Pnlcancel.Visible = true;
                break;
            case "HISTORY":

                txtnourutfollow.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnourut")).Text;
                //  pnlData.Visible = false;
                TextBox1.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllat")).Text;
                TextBox2.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllon")).Text;
                Pnlsearch.Visible = false;
                Pnltagihan.Visible = false;
                Pnlhistory.Visible = true;
                BindGridEntityhistory();
                break;
            case "VIEW":

                urut = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnouruttaspen")).Text;
                //  pnlData.Visible = false;
                Pnlsearch.Visible = false;
                Pnltagihan.Visible = false;
                pnlformattaspen.Visible = true;
                TampilFormTaspen(urut);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                break;
            case "PENAWARAN":

                txtNotasPenewaran.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotas")).Text;
                txtNamaNasabah.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                txtAlamatNasabah.Text = ((Label)Grdtagihan.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat")).Text;

                OpenFormCetakPenawaran();

                break;
            default:

                BindGridEntityTagihan();

                break;

        }

    }

    private void OpenFormCetakPenawaran()
    {
        pnlData.Visible = false;
        Pnlsearch.Visible = false;
        Pnltagihan.Visible = false;
        Pnlasabari.Visible = false;
        Pnltaspencancel.Visible = false;
        Pnlasabricancel.Visible = false;
        Pnlakanfollow.Visible = false;
        Pnlcancel.Visible = false;
        Pnlhistory.Visible = false;
        pnlformattaspen.Visible = false;
        pnlformatasabri.Visible = false;
        PanelSuratPenawaran.Visible = false;

        PanelSuratPenawaran.Visible = true;
    }

    private void CloseFormCetakPenawaran()
    {
        PanelSuratPenawaran.Visible = false;
    }

    public void TampilFormTaspen(string xnourut)
    {
        //numberFormat is the formatter I use for numbers.
        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[2];
        Paramsc[0] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.VarChar, 50, xnourut, typeof(string));
        Paramsc[1] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TAMPILTASPEN", typeof(string));

        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.BG_Get_Transaksi_TASPEN", Paramsc, "constrmf").Tables[0];
        ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES", Paramsc, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {

            txtnotas.Text = ge.ListDataset.Tables[0].Rows[0]["notas"].ToString();
            Txtjenis.Text = ge.ListDataset.Tables[0].Rows[0]["jenispensiun"].ToString();
            txtnamapensiun.Text = ge.ListDataset.Tables[0].Rows[0]["namapensiunan"].ToString();
            txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir_pensiunan"].ToString();
            txtkodepangkat.Text = ge.ListDataset.Tables[0].Rows[0]["kdpangkat"].ToString();
            txtcabang.Text = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
            txtkodebyr.Text = ge.ListDataset.Tables[0].Rows[0]["kodebyr"].ToString();
            txtnamabyr.Text = ge.ListDataset.Tables[0].Rows[0]["nmkanbyr"].ToString();
            txtjiwa.Text = ge.ListDataset.Tables[0].Rows[0]["ketjiwa"].ToString();
            txtstop.Text = ge.ListDataset.Tables[0].Rows[0]["nmstop"].ToString();
            txttelepon.Text = ge.ListDataset.Tables[0].Rows[0]["telepon"].ToString();

            txtpenpok.Text = ge.ListDataset.Tables[0].Rows[0]["penpok"].ToString();

            txttistri.Text = ge.ListDataset.Tables[0].Rows[0]["tistri"].ToString();
            txttanak.Text = ge.ListDataset.Tables[0].Rows[0]["tanak"].ToString();
            txttpajak.Text = ge.ListDataset.Tables[0].Rows[0]["tpajak"].ToString();
            Txttberas.Text = ge.ListDataset.Tables[0].Rows[0]["tberas"].ToString();
            txtpenyesuaian.Text = ge.ListDataset.Tables[0].Rows[0]["penyesuaian"].ToString();
            txttbulat.Text = ge.ListDataset.Tables[0].Rows[0]["tbulat"].ToString();
            TxtKotor.Text = ge.ListDataset.Tables[0].Rows[0]["kotor"].ToString();
            TxtBersih.Text = ge.ListDataset.Tables[0].Rows[0]["bersih"].ToString();
            TxtRapel.Text = ge.ListDataset.Tables[0].Rows[0]["rapel"].ToString();


            Txtpenerima.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penerima"].ToString();
            Txttgllahirpenerima.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir_penerima"].ToString();
            Txtalamatpenerima.Text = ge.ListDataset.Tables[0].Rows[0]["alm_peserta"].ToString();
            Txtdati4.Text = ge.ListDataset.Tables[0].Rows[0]["nmdati4"].ToString();
            txtkota.Text = ge.ListDataset.Tables[0].Rows[0]["kota"].ToString();


            txtnourutdapem.Text = ge.ListDataset.Tables[0].Rows[0]["norutdapem"].ToString();
            Txttmppensiun.Text = ge.ListDataset.Tables[0].Rows[0]["tmtpensiun"].ToString();
            TxtNoskep.Text = ge.ListDataset.Tables[0].Rows[0]["nomor_skep"].ToString();
            TxttglSkep.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_skep"].ToString();
            Txtnorek.Text = ge.ListDataset.Tables[0].Rows[0]["norek"].ToString();
            Txtpenerbitskep.Text = ge.ListDataset.Tables[0].Rows[0]["penerbit_skep"].ToString();
            txtnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["npwp"].ToString();

        }
    }
    protected void btnKembaliasbri_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = true;
        Pnlasabari.Visible = true;

        pnlformatasabri.Visible = false;
        BindGridEntityAsabri();


    }


    protected void btnKembalitaspen_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = true;
        Pnltagihan.Visible = true;

        pnlformattaspen.Visible = false;
        BindGridEntityTagihan();


    }


    protected void btnKembali2_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = true;

        Pnlhistory.Visible = false;
        if (ddlbahan.SelectedValue == "TASPEN") //nama table menggunakan string
        {
            Pnltagihan.Visible = true;
            BindGridEntityTagihan();
        }
        else
        {
            Pnlasabari.Visible = true;
            BindGridEntityAsabri();
        }
    }

    protected void btnKembali_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = true;

        Pnlakanfollow.Visible = false;
        Pnlcancel.Visible = false;
        if (ddlbahan.SelectedValue == "TASPEN") //nama table menggunakan string
        {
            Pnltagihan.Visible = true;
            BindGridEntityTagihan();
        }
        else
        {
            Pnlasabari.Visible = true;
            BindGridEntityAsabri();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btntagihan_Click(object sender, EventArgs e)
    {
        Lbljudul.Text = "DATA FOLLOW UP";
        Lbltaspen.Text = "Data FOLLOW UP";
        datacancel.Value = "0";
        //btnproses.Visible = false;
        Pnltagihan.Visible = false;
        Pnltaspencancel.Visible = false;
        Pnlasabricancel.Visible = false;
        Pnlasabari.Visible = false;

    }
    protected void btntunggakan_Click(object sender, EventArgs e)
    {

        Pnltagihan.Visible = false;

        Pnlasabari.Visible = false;

        Pnltaspencancel.Visible = false;

        Pnlasabricancel.Visible = false;
        Pnlakanfollow.Visible = false;
        Pnlcancel.Visible = false;
        Lbltaspen.Text = "Cancel Data";
        datacancel.Value = "1";
        Lbljudul.Text = "CANCEL DATA";


        //BindGridEntityTunggakan();
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }


    //protected void ddlbahan_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    string query;
    //    query = "";
    //    if (datacancel.Value == "0")
    //    {

    //    if (ddlbahan.SelectedValue == "TASPEN") //nama table menggunakan string
    //    {

    //        DataTable ds = new DataTable();
    //        ds = null;
    //        Grdtagihan.DataSource = ds;
    //        Grdtagihan.DataBind();
    //            Grdasabri.DataSource = ds;
    //            Grdasabri.DataBind();

    //            Pnltagihan.Visible = false;
    //        Pnlasabari.Visible = false;

    //    }
    //    else if (ddlbahan.SelectedValue == "ASABRI")
    //    {

    //            DataTable ds = new DataTable();
    //            ds = null;
    //            Grdtagihan.DataSource = ds;
    //            Grdtagihan.DataBind();
    //            Grdasabri.DataSource = ds;
    //            Grdasabri.DataBind();

    //            Pnltagihan.Visible = false;
    //            Pnlasabari.Visible = false;




    //            Pnltagihan.Visible = false;
    //            Pnlasabari.Visible = false;

    //    }

    //    }
    //    else
    //    {
    //        if (ddlbahan.SelectedValue == "TASPEN") //nama table menggunakan string
    //        {

    //            DataTable ds = new DataTable();
    //            ds = null;
    //            Grdtaspencancel.DataSource = ds;
    //            Grdtaspencancel.DataBind();

    //            Grdasabricancel.DataSource = ds;
    //            Grdasabricancel.DataBind();
    //            Pnltaspencancel.Visible = false;
    //            Pnlasabricancel.Visible = false;

    //        }
    //        else if (ddlbahan.SelectedValue == "ASABRI")
    //        {

    //            DataTable ds = new DataTable();
    //            ds = null;
    //            Grdtaspencancel.DataSource = ds;
    //            Grdtaspencancel.DataBind();

    //            Grdasabricancel.DataSource = ds;
    //            Grdasabricancel.DataBind();
    //            Pnltaspencancel.Visible = false;
    //            Pnlasabricancel.Visible = false;

    //        }
    //    }

    //}

    protected void btnSimpan_followup_Click(object sender, EventArgs e)
    {
        try
        {
            string prospek;
            prospek = "";
            if (Hdnlon.Value == "")
            {
                Hdnlon.Value = "0";
            }
            if (Hdnlat.Value == "")
            {
                Hdnlat.Value = "0";
            }

            if (Hdnlon.Value == "0" || Hdnlat.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anda Belum Mendapatkan Titik Location!','You successfully save this Project','top-right','success','1');", true);
            }
            else if (ddlbahan.SelectedValue == "TASPEN") //nama table menggunakan string
            {
                if (ddlkemampuan.SelectedValue == "0")
                {
                    if (ddlkeinginan.SelectedValue == "0")
                    {
                        prospek = "3";
                    }
                    else if (ddlkeinginan.SelectedValue == "1")
                    {
                        prospek = "3";
                    }
                }
                else if (ddlkemampuan.SelectedValue == "1")
                {
                    if (ddlkeinginan.SelectedValue == "0")
                    {
                        prospek = "2";
                    }
                    else if (ddlkeinginan.SelectedValue == "1")
                    {
                        prospek = "1";
                    }
                }

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[30];
                var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTTASPEN", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtnourutfollow.Text), typeof(int));
                var[5] = gb.CreateSPParameter("@followup", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlfollowup.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@namakunjungan", ParameterDirection.Input, SqlDbType.VarChar, 80, Txtnamakunjungan.Text, typeof(string));

                var[7] = gb.CreateSPParameter("@alamatkunjungan", ParameterDirection.Input, SqlDbType.VarChar, 150, TxtAlamatKunjungan.Text, typeof(string));
                var[8] = gb.CreateSPParameter("@tgllahirkunjungan", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahirkunjungan.Text, typeof(DateTime));
                var[9] = gb.CreateSPParameter("@Gajikunjungan", ParameterDirection.Input, SqlDbType.Money, 0, TxtGajikunjungan.Text, typeof(decimal));
                var[10] = gb.CreateSPParameter("@usiakunjungan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtusia.Text), typeof(int));
                var[11] = gb.CreateSPParameter("@telponkunjungan", ParameterDirection.Input, SqlDbType.VarChar, 20, txttelpon.Text, typeof(string));
                //var[12] = gb.CreateSPParameter("@kantorbayarkunjungan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkantorbayarkunjungan.Text, typeof(string));
                var[12] = gb.CreateSPParameter("@kantorbayarkunjungan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayarKunjungan.SelectedValue, typeof(string));
                var[13] = gb.CreateSPParameter("@skonhand", ParameterDirection.Input, SqlDbType.VarChar, 50, txtskonhand.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@skonto", ParameterDirection.Input, SqlDbType.VarChar, 50, txtskonto.Text, typeof(string));
                var[15] = gb.CreateSPParameter("@pinjamanlama", ParameterDirection.Input, SqlDbType.Int, 0, ddlpinjaman.SelectedValue, typeof(int));
                var[16] = gb.CreateSPParameter("@plafonlama", ParameterDirection.Input, SqlDbType.Money, 0, txtplafonlama.Text, typeof(decimal));
                var[17] = gb.CreateSPParameter("@jangkalama", ParameterDirection.Input, SqlDbType.VarChar, 80, txtJangkawaktulama.Text, typeof(string));
                var[18] = gb.CreateSPParameter("@angsuranlama", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranlama.Text, typeof(decimal));
                var[19] = gb.CreateSPParameter("@plafonsena", ParameterDirection.Input, SqlDbType.Money, 0, txtplafonsena.Text, typeof(decimal));
                var[20] = gb.CreateSPParameter("@jangkasena", ParameterDirection.Input, SqlDbType.VarChar, 20, txtjangkawaktusena.Text, typeof(string));
                var[21] = gb.CreateSPParameter("@angsuransena", ParameterDirection.Input, SqlDbType.Money, 0, txtterimabersih.Text, typeof(decimal));
                var[22] = gb.CreateSPParameter("@kemampuan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkemampuan.SelectedValue, typeof(string));
                var[23] = gb.CreateSPParameter("@keinginan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkeinginan.SelectedValue, typeof(string));
                var[24] = gb.CreateSPParameter("@notekunjungan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtcatatan.Text, typeof(string));
                var[25] = gb.CreateSPParameter("@prospek", ParameterDirection.Input, SqlDbType.VarChar, 10, prospek, typeof(string));
                var[26] = gb.CreateSPParameter("@userfollowup", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[27] = gb.CreateSPParameter("@lat", ParameterDirection.Input, SqlDbType.VarChar, 100, Hdnlat.Value, typeof(string));
                var[28] = gb.CreateSPParameter("@lon", ParameterDirection.Input, SqlDbType.VarChar, 100, Hdnlon.Value, typeof(string));
                var[29] = gb.CreateSPParameter("@tempattgllahir", ParameterDirection.Input, SqlDbType.VarChar, 50, txttempatlahirkunjungan.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES_Pro", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {

                    txtcatatan.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                    Pnlsearch.Visible = true;
                    Pnltagihan.Visible = true;
                    Pnlakanfollow.Visible = false;
                    BindGridEntityTagihan();
                    txtcatatan.Text = "";

                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

            }
            else
            {

                if (ddlkemampuan.SelectedValue == "0")
                {
                    if (ddlkeinginan.SelectedValue == "0")
                    {
                        prospek = "3";
                    }
                    else if (ddlkeinginan.SelectedValue == "1")
                    {
                        prospek = "3";
                    }
                }
                else if (ddlkemampuan.SelectedValue == "1")
                {
                    if (ddlkeinginan.SelectedValue == "0")
                    {
                        prospek = "2";
                    }
                    else if (ddlkeinginan.SelectedValue == "1")
                    {
                        prospek = "1";
                    }
                }

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[30];
                var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTASABRI", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtnourutfollow.Text), typeof(int));
                var[5] = gb.CreateSPParameter("@followup", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlfollowup.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@namakunjungan", ParameterDirection.Input, SqlDbType.VarChar, 80, Txtnamakunjungan.Text, typeof(string));

                var[7] = gb.CreateSPParameter("@alamatkunjungan", ParameterDirection.Input, SqlDbType.VarChar, 150, TxtAlamatKunjungan.Text, typeof(string));
                var[8] = gb.CreateSPParameter("@tgllahirkunjungan", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahirkunjungan.Text, typeof(DateTime));
                var[9] = gb.CreateSPParameter("@Gajikunjungan", ParameterDirection.Input, SqlDbType.Money, 0, TxtGajikunjungan.Text, typeof(decimal));
                var[10] = gb.CreateSPParameter("@usiakunjungan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtusia.Text), typeof(int));
                var[11] = gb.CreateSPParameter("@telponkunjungan", ParameterDirection.Input, SqlDbType.VarChar, 20, txttelpon.Text, typeof(string));
                //var[12] = gb.CreateSPParameter("@kantorbayarkunjungan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkantorbayarkunjungan.Text, typeof(string));
                var[12] = gb.CreateSPParameter("@kantorbayarkunjungan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayarKunjungan.SelectedValue, typeof(string));
                var[13] = gb.CreateSPParameter("@skonhand", ParameterDirection.Input, SqlDbType.VarChar, 50, txtskonhand.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@skonto", ParameterDirection.Input, SqlDbType.VarChar, 50, txtskonto.Text, typeof(string));
                var[15] = gb.CreateSPParameter("@pinjamanlama", ParameterDirection.Input, SqlDbType.Int, 0, ddlpinjaman.SelectedValue, typeof(int));
                var[16] = gb.CreateSPParameter("@plafonlama", ParameterDirection.Input, SqlDbType.Money, 0, txtplafonlama.Text, typeof(decimal));
                var[17] = gb.CreateSPParameter("@jangkalama", ParameterDirection.Input, SqlDbType.VarChar, 80, txtJangkawaktulama.Text, typeof(string));
                var[18] = gb.CreateSPParameter("@angsuranlama", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranlama.Text, typeof(decimal));
                var[19] = gb.CreateSPParameter("@plafonsena", ParameterDirection.Input, SqlDbType.Money, 0, txtplafonsena.Text, typeof(decimal));
                var[20] = gb.CreateSPParameter("@jangkasena", ParameterDirection.Input, SqlDbType.VarChar, 20, txtjangkawaktusena.Text, typeof(string));
                var[21] = gb.CreateSPParameter("@angsuransena", ParameterDirection.Input, SqlDbType.Money, 0, txtterimabersih.Text, typeof(decimal));
                var[22] = gb.CreateSPParameter("@kemampuan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkemampuan.SelectedValue, typeof(string));
                var[23] = gb.CreateSPParameter("@keinginan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkeinginan.SelectedValue, typeof(string));
                var[24] = gb.CreateSPParameter("@notekunjungan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtcatatan.Text, typeof(string));
                var[25] = gb.CreateSPParameter("@prospek", ParameterDirection.Input, SqlDbType.VarChar, 10, prospek, typeof(string));
                var[26] = gb.CreateSPParameter("@userfollowup", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[27] = gb.CreateSPParameter("@lat", ParameterDirection.Input, SqlDbType.VarChar, 100, Hdnlat.Value, typeof(string));
                var[28] = gb.CreateSPParameter("@lon", ParameterDirection.Input, SqlDbType.VarChar, 100, Hdnlon.Value, typeof(string));
                var[29] = gb.CreateSPParameter("@tempattgllahir", ParameterDirection.Input, SqlDbType.VarChar, 50, txttempatlahirkunjungan.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES_Pro", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                    Pnlsearch.Visible = true;
                    Pnlasabari.Visible = true;
                    Pnlakanfollow.Visible = false;
                    BindGridEntityAsabri();
                    txtcatatan.Text = "";

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
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlbahan.SelectedValue == "TASPEN") //nama table menggunakan string
            {

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "HISTORYTASPEN", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtnourutfollow.Text), typeof(int));
                var[5] = gb.CreateSPParameter("@cancel", ParameterDirection.Input, SqlDbType.VarChar, 150, txtcancel.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES_Pro", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                    Pnlsearch.Visible = true;
                    Pnltagihan.Visible = true;
                    Pnlcancel.Visible = false;
                    BindGridEntityTagihan();

                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }



            }
            else
            {

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                var[0] = gb.CreateSPParameter("@createuser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "HISTORYASABRI", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@nourut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtnourutfollow.Text), typeof(int));
                var[5] = gb.CreateSPParameter("@cancel", ParameterDirection.Input, SqlDbType.VarChar, 150, txtcancel.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("BG_FOLLOWUP_SALES_Pro", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                    Pnlsearch.Visible = true;
                    Pnlasabari.Visible = true;
                    Pnlcancel.Visible = false;
                    BindGridEntityAsabri();

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
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
}