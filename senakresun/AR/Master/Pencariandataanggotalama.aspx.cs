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

public partial class FA_Master_Pencariandataanggotalama : System.Web.UI.Page
{
    String no_nasabah = "", nosebelumnya = "", updateno = "", CRNonasabah = "", STATE = "", kdpersh = "",
           formatnumber = "", msg1 = "", msg2 = "", msg3 = "", msg4 = "", msg5 = "", msg6 = "", msg7 = "", namafileKTP = "", namafileKK = "",
           namafileNPWP = "", namafileLainLain1 = "", namafileLainLain2 = "", namafilektppasangan = "";
    int counter_nonasabah = 0;
    long nosekarang = 0, nonasabah = 0;
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    public string fotopathktp = "", tanggal = "", fotopathnpwp = "", fotopathKK = "";
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
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

  
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("RegistrasiNasabahNew.aspx");
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistrasiNasabahNew.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                //BindGridEntity();

                //InsertCombo();
                ////txttgllahirasuransi.Text = DateTime.Now.ToString("dd-MM-yyyy");
                ///
                // --- Combo Pendidikan
                ge.ListDataset = gb.GetDataFunction("dbo.GetKoperasix()", "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";

                ddlkoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkoperasi.DataTextField = "nama_koperasi";
                ddlkoperasi.DataValueField = "kode_koperasi";
                ddlkoperasi.DataBind();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {

        //if (txtSearch.Text == "")
        //{
        //    BindGridEntity();
        //}
        //else
        //{
            BindGridEntityx();
        //}
        

    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {



            case "SLIP":

                try
                {
                    Panel1.Visible = true;
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
                    Params[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, nobukti, typeof(String));
                    Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Params[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTREKENING", typeof(string));
                    if (ddlkoperasi.SelectedValue == "I")
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("CL_PINJAMAN_ANGGOTA", Params, "koperasi1");
                    }
                    else if (ddlkoperasi.SelectedValue == "II")
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("CL_PINJAMAN_ANGGOTA", Params, "koperasi2");
                    }
                    else if (ddlkoperasi.SelectedValue == "III")
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("CL_PINJAMAN_ANGGOTA", Params, "koperasi3");
                    }
                    else if (ddlkoperasi.SelectedValue == "IV")
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("CL_PINJAMAN_ANGGOTA", Params, "koperasi4");
                    }
                    else if (ddlkoperasi.SelectedValue == "V")
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("CL_PINJAMAN_ANGGOTA", Params, "koperasi5");
                    }

                    GRData.UseAccessibleHeader = true;
                    GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

                    GRDX.DataSource = ge.ListDataset.Tables[0];
                    GRDX.DataBind();


                    GRDX.UseAccessibleHeader = true;
                    GRDX.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                catch (Exception X)
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);



                }
                break;


        }
 

    }
    public void BindGridEntityx()
    {


        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkoperasi.SelectedValue, typeof(string));

        if (ddlkoperasi.SelectedValue=="I")
        {
            ge.ListDataset = gb.GetDataJustOutPut("AR_GetNasabah_NEW", Params, "koperasi1");
        }
        else if (ddlkoperasi.SelectedValue == "II")
        {
            ge.ListDataset = gb.GetDataJustOutPut("AR_GetNasabah_NEW", Params, "koperasi2");
        }
        else if (ddlkoperasi.SelectedValue == "III")
        {
            ge.ListDataset = gb.GetDataJustOutPut("AR_GetNasabah_NEW", Params, "koperasi3");
        }
        else if (ddlkoperasi.SelectedValue == "IV")
        {
            ge.ListDataset = gb.GetDataJustOutPut("AR_GetNasabah_NEW", Params, "koperasi4");
        }
        else if (ddlkoperasi.SelectedValue == "V")
        {
            ge.ListDataset = gb.GetDataJustOutPut("AR_GetNasabah_NEW", Params, "koperasi5");
        }

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH_NEW", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_nasabah desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

   


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        pnlFilter.Visible=false;

        Pnlfooter4.Visible = true;
       

        txtkodepetugas.Text = SessLoginID;

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {

        try
        {
            if (HDState.Value == "SET")
            {
                STATE = "UPDATE";
                
            }
            else
            {
                STATE = "INSERT";
                HDNoNasabah.Value = "-";
            }

            if (txtidentitas.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Id KTP Masih Kosong');", true);
                
            }
            else if (txtnamanasabah.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama Anggota Masih Kosong');", true);

            }
            else if (txttempatlahir.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tempat Lahir Anggota Masih Kosong');", true);

            }
            else if (txtalamatsesuaiktp1.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Alamat KTP Anggota Masih Kosong');", true);

            }

            else if (txtkabupaten.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kabupaten KTP Anggota Masih Kosong');", true);

            }
            else if (txtkelurahanktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kelurahan KTP Anggota Masih Kosong');", true);

            }
            else if (txtkecamatanktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kecamatan KTP Anggota Masih Kosong');", true);

            }
            else if (txtkodeposktp.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Pos KTP Anggota Masih Kosong');", true);

            }
            else if (txtkotaidentitas.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kota terbit KTP Anggota Masih Kosong');", true);

            }
            else if (txttgllahir.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Lahir Anggota Masih Kosong');", true);

            }
            else if (txttglterbitidentitas.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Terbit Identitas KTP Masih Kosong');", true);

            }
            else if (txtpathktp.Text.Length==0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Upload KTP Anggota Masih Kosong');", true);

            }
            else if (txthp.Text.Length < 11)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Handphone tidak boleh kurang dari 11');", true);

            }

            else
            {
                

                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[5];
                var4[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                var4[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var4[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro", var4, "constrmf");



                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('NIK sudah ada pada koperasi yg sama');", true);


                }

                else
                {

                   
       
                    //INSERT INTO ARNASABAH
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[34];

                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
                var[1] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@nama_panggilan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamapanggilan.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@jenis_kelamin", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljenkel.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@tempat_lahir", ParameterDirection.Input, SqlDbType.VarChar, 50, txttempatlahir.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@tanggal_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttgllahir.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@agama", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlAgama.SelectedValue, typeof(string));
                var[7] = gb.CreateSPParameter("@status_pernikahan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusnikah.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@pendidikan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlpendidikan.SelectedValue, typeof(string));
                var[9] = gb.CreateSPParameter("@kewarganegaraan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkewarganegaraan.SelectedValue, typeof(string));
                var[10] = gb.CreateSPParameter("@kenegaraan_asing", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkewarganegaraan.Text, typeof(string));
                 var[11] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidentitas.Text, typeof(string));
                var[12] = gb.CreateSPParameter("@kota_terbit_identitas", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkotaidentitas.Text, typeof(string));
                var[13] = gb.CreateSPParameter("@tanggal_terbit_identitas", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglterbitidentitas.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@berlaku_sampai_dengan", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglterbitidentitas.Text, typeof(string));
                var[15] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 100, TxtEmail.Text, typeof(string));
                var[16] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 30, txthp.Text, typeof(string));
                var[17] = gb.CreateSPParameter("@alamat_rumah_ktp1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamatsesuaiktp1.Text, typeof(string));
                var[18] = gb.CreateSPParameter("@kabupaten", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkabupaten.Text, typeof(string));
                var[19] = gb.CreateSPParameter("@RT_ktp", ParameterDirection.Input, SqlDbType.VarChar, 100, txtrtktp.Text, typeof(string));
                var[20] = gb.CreateSPParameter("@RW_ktp", ParameterDirection.Input, SqlDbType.VarChar, 100, txtrwktp.Text, typeof(string));
                var[21] = gb.CreateSPParameter("@kelurahan_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkelurahanktp.Text, typeof(string));
                var[22] = gb.CreateSPParameter("@kecamatan_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkecamatanktp.Text, typeof(string));
                var[23] = gb.CreateSPParameter("@provinsi_ktp", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlprovinsiktp.SelectedValue, typeof(string));
                var[24] = gb.CreateSPParameter("@kodepos_ktp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodeposktp.Text, typeof(string));
                var[25] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, STATE, typeof(string));
                var[26] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[27] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                var[28] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var[29] = gb.CreateSPParameter("@Userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                var[30] = gb.CreateSPParameter("@kode_AO", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlAO.SelectedValue, typeof(string));
                var[31] = gb.CreateSPParameter("@pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlPekerjaan.SelectedValue, typeof(string));
                var[32] = gb.CreateSPParameter("@file_pathktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathktp.Text, typeof(string));
                var[33] = gb.CreateSPParameter("@useredit", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));



                    ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH_NEW", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                        btnSimpan.Visible = false;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Anggota','top-right','success','2');", true);

                    }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }



            }

            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }


    }

 


  
 

    protected void ddlkewarganegaraan_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlkewarganegaraan.SelectedValue == "2")
        {
            txtkewarganegaraan.Enabled = true;
            txtkewarganegaraan.Text = "";
        }
        else
        {
            txtkewarganegaraan.Enabled = false;
            txtkewarganegaraan.Text = "-";
        }


    }
    public void InsertCombo()
    {
        // --- Combo Jenis Kelamin
        ge.ListDataset = gb.GetDataFunction("dbo.GetJenisKelamin()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddljenkel.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljenkel.DataTextField = "Keterangan";
        ddljenkel.DataValueField = "id";
        ddljenkel.DataBind();
        //ddljenkel.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Status Pernikahan
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatusnikah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatusnikah.DataTextField = "Keterangan";
        ddlstatusnikah.DataValueField = "id";
        ddlstatusnikah.DataBind();
       // ddlstatusnikah.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Agama
        ge.ListDataset = gb.GetDataFunction("dbo.GetAgama()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlAgama.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAgama.DataTextField = "Keterangan";
        ddlAgama.DataValueField = "id";
        ddlAgama.DataBind();
   

      

        // --- Combo kewarganegaraan
        ge.ListDataset = gb.GetDataFunction("dbo.GetCitizen()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlkewarganegaraan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkewarganegaraan.DataTextField = "keterangan";
        ddlkewarganegaraan.DataValueField = "id";
        ddlkewarganegaraan.DataBind();
    
        // --- Combo Pendidikan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlpendidikan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpendidikan.DataTextField = "keterangan";
        ddlpendidikan.DataValueField = "id";
        ddlpendidikan.DataBind();
      //  ddlpendidikan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Pendidikan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlpendidikan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpendidikan.DataTextField = "keterangan";
        ddlpendidikan.DataValueField = "id";
        ddlpendidikan.DataBind();
    
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsiktp.DataTextField = "nama_provinsi";
        ddlprovinsiktp.DataValueField = "kode_provinsi";
        ddlprovinsiktp.DataBind();
       // ddlprovinsiktp.Items.Insert(0, new ListItem("Pilih Data", "-"));

       
        // -- Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlPekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlPekerjaan.DataTextField = "keterangan";
        ddlPekerjaan.DataValueField = "id";
        ddlPekerjaan.DataBind();

        // COMBO MASTER MO
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_MO", var1, "constrmf");

        ddlAO.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAO.DataTextField = "namaAO";
        ddlAO.DataValueField = "NIK";
        ddlAO.DataBind();
    //    ddlAO.Items.Insert(0, new ListItem("Pilih Data", "-"));

    
        ddlkewarganegaraan.SelectedValue = "1";
        txtkewarganegaraan.Enabled = false;
        txtkewarganegaraan.Text = "-";
    }

    protected void UploadFileKTP(object sender, EventArgs e)
    {



        if (fileuploadktp.HasFile)
        {


            var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

            if (fileuploadktp.PostedFile.ContentLength > maxFileSize)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

            }

            else
            {

                string namafile;
                string filename;
                string filenamex;

                DateTime now = DateTime.Now;
                namafile = txtidentitas.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "KTP";
                string folderPath = Server.MapPath("~/Files/Anggota/thumb");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                filename = namafile + ".jpg";
                filenamex = namafile + "OK.jpg";


                //create temp
                string path = Server.MapPath("~/Files/Anggota/thumb/" + namafile + ".jpg");
                fileuploadktp.SaveAs(path);
                //create temp

                System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                int newHeight = 200;
                int newWidth = Convert.ToInt32(aspectRatio * newHeight);
                System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
                System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
                thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbBitmap.Save(Server.MapPath("~/Files/Anggota/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbGraph.Dispose();
                thumbBitmap.Dispose();
                image.Dispose();
                fotopathktp = "~/Files/Anggota/" + filenamex;
                txtpathktp.Text = fotopathktp;
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not.
                {
                    file.Delete();
                }


            }

        }



    }

  
        
}