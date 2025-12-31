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
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class FA_Master_RegistrasiUploadData : System.Web.UI.Page
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
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {

        if (txtSearch.Text == "")
        {
            //BindGridEntity();
        }
        else
        {
            BindGridEntityx();
        }


    }
    public void BindGridEntityx()
    {


        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("KP_GetNasabah", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }
    protected void txtidentitas_TextChanged(object sender, EventArgs e)
    {
        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        //var[0] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
        //var[1] = gb.CreateSPParameter("@nama_ibu_kandung", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaibukandung.Text, typeof(string));
        //var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTP", typeof(string));
        //var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        
        //ge.ListDataset = gb.GetDataJustOutPut("KP_NASABAH", var, "constrmf");

        //if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nasabah sudah terdaftar');", true);

        //}

    }



   
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("RegistrasiUploadData.aspx");
    }
    //protected void txttgllahir_TextChanged(object sender, EventArgs e)
    //{RegistrasiUploadData

    //    DateTime date = Convert.ToDateTime(t.Text.Trim());
    //    string dateString = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);


    //    DateTime date1 = Convert.ToDateTime(DateTime.Now);
    //    string dateString1 = date1.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

    //    if (int.Parse(dateString) > int.Parse(dateString1))
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Lahir debitur tidak boleh melebihi tanggal hari ini');", true);

    //    }
    //    else
    //    {

        

    //        string usia = gb.HitungTahunUsia(txttgllahir.Text);

    //        decimal umur = decimal.Parse(usia);

    //       //string sDate = DateTime.ParseExact(txttgllahir.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
    //        //DateTime date = DateTime.ParseExact(txttgllahir.Text, "yyyy/MM/dd", null);
    //        //DateTime dob = date; 
    //        //int age = CalculateAge(dob);

    //        if (umur < 40 || umur > 80)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Usia Debitur tidak masuk kriteria peminjaman');", true);

    //        }
    //    }
    //}
    private static int CalculateAge(DateTime dateOfBirth)
    {
        int age = 0;
        age = DateTime.Now.Year - dateOfBirth.Year;
        if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            age = age - 1;

        return age;
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistrasiUploadData.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //txtalamatPihakLain.Attributes.Add("maxlength", txtalamatPihakLain.MaxLength.ToString());
                //BindGridEntity();

                InsertCombo();
                //txttgllahirasuransi.Text = DateTime.Now.ToString("dd-MM-yyyy");
                 
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    //public void BindGridEntity()
    //{

    //    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
    //    var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
    //    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


    //    ge.ListDataset = gb.GetDataJustOutPut("KP_NASABAH", var, "constrmf");

    //    ge.ListDataset.Tables[0].DefaultView.Sort = "no_nasabah desc";

    //    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
    //    GRData.DataBind();

    //    GRData.UseAccessibleHeader = true;

    //    GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    //}
  protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":
                //HDState.Value = e.CommandName;

                try

                {

                    PnlFront.Visible = false;
                    //pnladd.Visible = false;
                    pnlHeader.Visible = false;
                    Panel1.Visible = false;
                    pnldataanggota.Visible = true;
                    pnlIdentitas.Visible = true;
                   
                    Pnlfooter4.Visible = true;
                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtnoanggota.Text = no_nasabah;
                    txtnamaanggota.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                    txttanggalbergabung.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_UPLOAD_DATA_NASABAH", var, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                    {
                      
                        txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                        txtpathktp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();
     
                        txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                        txtpathnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString();
                        txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_pasangan"].ToString();
                        txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                        //txtalamatrumahtinggal2.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal2"].ToString();
                        //txtalamatrumahtinggal3.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal3"].ToString();

                        txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                        txtpathKK.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();
                        txtpathlainlain1.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathlainlain1"].ToString();
                        txtlainlain1.Text = ge.ListDataset.Tables[0].Rows[0]["lainlain1"].ToString();
                        txtlainlain2.Text = ge.ListDataset.Tables[0].Rows[0]["lainlain2"].ToString();
                        txtpathlainlain2.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathlainlain2"].ToString();
                       
                     
                        //txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_pasangan"].ToString();




                        //txtkodepetugas.Text = SessLoginID;


                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data nasabah Tidak ditemukan');", true);
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



   

    protected void btnSimpan_Click(object sender, EventArgs e)
    {

        try
        {
           



                STATE = "UPDATE";


              


                btnSimpan.Enabled = false;

                //INSERT INTO ARNASABAH
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[17];

                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 250, txtidentitas.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@no_npwp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtNPWP.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@no_ktp_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtidpasangan.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@file_pathktppasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtppathktppasangan.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@no_kk", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnomerkk.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@file_pathKK", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathKK.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@lainlain1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtlainlain1.Text, typeof(string));
                var[8] = gb.CreateSPParameter("@file_pathlainlain1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathlainlain1.Text, typeof(string));
                var[9] = gb.CreateSPParameter("@lainlain2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtlainlain2.Text, typeof(string));
                var[10] = gb.CreateSPParameter("@file_pathlainlain2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathlainlain2.Text, typeof(string));
                var[11] = gb.CreateSPParameter("@file_pathktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathktp.Text, typeof(string));
                var[12] = gb.CreateSPParameter("@file_pathnpwp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathnpwp.Text, typeof(string));
                var[13] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEDATA", typeof(string));
                var[14] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[15] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                var[16] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_UPLOAD_DATA_NASABAH", var, "constrmf");

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
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

         }


    }

    protected void txtlainlain1_TextChanged(object sender, EventArgs e)
    {
        if (txtlainlain1.Text == "0" || txtlainlain1.Text == "")
        {
            fileuploadlainlain1.Enabled = false;
            btnlainlain1.Enabled = false;
            txtpathlainlain1.Enabled = false;
            txtpathlainlain1.Text = "-";
        }
        else
        {
            fileuploadlainlain1.Enabled = true;
            btnlainlain1.Enabled = true;
            txtpathlainlain1.Text = "";
        }
    }

    protected void txtlainlain2_TextChanged(object sender, EventArgs e)
    {
        if (txtlainlain2.Text == "0" || txtlainlain2.Text == "")
        {
            fileuploadlainlain2.Enabled = false;
            btnlainlain2.Enabled = false;
            txtpathlainlain2.Enabled = false;
            txtpathlainlain2.Text = "-";
        }
        else
        {
            fileuploadlainlain2.Enabled = true;
            btnlainlain2.Enabled = true;
            txtpathlainlain2.Text = "";
        }
    }



    //protected void ddlnpwp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   if(txtNPWP.SelectedValue == "1")
    //   {
    //           txtNPWP.Enabled = false;
    //           txtNPWP.Text = "-";
    //   }else
    //   {
    //           txtNPWP.Enabled = true;
    //           txtNPWP.Text = "";
    //   }

    //}

  
    public void InsertCombo()
    {
       



        // --- Combo Pendidikan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetNPWP()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlNPWP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlNPWP.DataTextField = "keterangan";
        //ddlNPWP.DataValueField = "id";
        //ddlNPWP.DataBind();
        //ddlNPWP.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //// --- Combo Provinsi KTP
        //ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        //ddlprovinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlprovinsiktp.DataTextField = "nama_provinsi";
        //ddlprovinsiktp.DataValueField = "kode_provinsi";
        //ddlprovinsiktp.DataBind();
        ////ddlprovinsiktp.Items.Insert(0, new ListItem("Pilih Data", "-"));

      


        
    }

    protected void UploadFileKTP(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/Kresun/KTP/");
        DateTime now = DateTime.Now;
         
        namafileKTP = txtidentitas.Text + "_" + now.ToString("ddMMyyyy") + "_" + "KTP";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if(Path.GetFileNameWithoutExtension(fileuploadktp.FileName) != namafileKTP)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileKTP + "');", true);

        //}
        //else
        //{
        //Save the File to the Directory (Folder).
        fileuploadktp.SaveAs(folderPath + namafileKTP + ".jpg");

        ////Display the Picture in Image control.
        //imagektp.ImageUrl = "~/Files/Kresun/KTP/" + Path.GetFileName(fileuploadktp.FileName);

        fotopathktp = folderPath + Path.GetFileName(fileuploadktp.FileName);
        txtpathktp.Text = "~/Files/Kresun/KTP/" + namafileKTP + ".jpg";

        //}


    }

    protected void UploadFileNPWP(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/Kresun/NPWP/");
        DateTime now = DateTime.Now;

        namafileNPWP = txtNPWP.Text + "_" + now.ToString("ddMMyyyy") + "_" + "NPWP";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadnpwp.FileName) != namafileNPWP)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileNPWP + "');", true);

        //}
        //else
        //{
        //Save the File to the Directory (Folder).
        fileuploadnpwp.SaveAs(folderPath + namafileNPWP + ".jpg");

        ////Display the Picture in Image control.
        //imagenpwp.ImageUrl = "~/Files/Kresun/NPWP/" + Path.GetFileName(fileuploadnpwp.FileName);

        fotopathnpwp = folderPath + Path.GetFileName(fileuploadnpwp.FileName);
        txtpathnpwp.Text = "~/Files/Kresun/NPWP/" + namafileNPWP + ".jpg";


        //}
    }


    protected void UploadFileKK(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/Kresun/KK/");
        DateTime now = DateTime.Now;

        namafileKK = txtnomerkk.Text + "_" + now.ToString("ddMMyyyy") + "_" + "KK";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadKK.FileName) != namafileKK)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileKK + "');", true);

        //}
        //else
        //{

        //Save the File to the Directory (Folder).
        fileuploadKK.SaveAs(folderPath + namafileKK + ".jpg");

        ////Display the Picture in Image control.
        //imageKK.ImageUrl = "~/Files/Kresun/KK" + Path.GetFileName(fileuploadKK.FileName);

        txtpathKK.Text = "~/Files/Kresun/KK/" + namafileKK + ".jpg";

        //}




    }

    protected void UploadFilelainlain(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/Kresun/LainLain/");
        DateTime now = DateTime.Now;

        namafileLainLain1 = txtlainlain1.Text + "_" + now.ToString("ddMMyyyy") + "_" + "LainLain";

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadlainlain1.FileName) != namafileLainLain1)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileLainLain1 + "');", true);

        //}
        //else
        //{
        //Save the File to the Directory (Folder).
        fileuploadlainlain1.SaveAs(folderPath + namafileLainLain1 + ".jpg");

        //Display the Picture in Image control.
        //imagelainlain.ImageUrl = "~/Files/Kresun/LainLain" + Path.GetFileName(fileuploadlainlain1.FileName);

        txtpathlainlain1.Text = "~/Files/Kresun/LainLain/" + namafileLainLain1 + ".jpg";
        ///}


    }

    protected void UploadFilelainlain2(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/Kresun/LainLain2/");
        DateTime now = DateTime.Now;

        namafileLainLain2 = txtlainlain2.Text + "_" + now.ToString("ddMMyyyy") + "_" + "LainLain2";

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadlainlain2.FileName) != namafileLainLain2)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileLainLain2 + "');", true);

        //}
        //else
        //{
        //Save the File to the Directory (Folder).
        fileuploadlainlain2.SaveAs(folderPath + namafileLainLain2 + ".jpg");

        //Display the Picture in Image control.
        //imagelainlain2.ImageUrl = "~/Files/Kresun/LainLain2" + Path.GetFileName(fileuploadlainlain2.FileName);

        txtpathlainlain2.Text = "~/Files/Kresun/LainLain2/" + namafileLainLain2 + ".jpg";
        //}


    }

    protected void UploadFilektppasangan(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/Kresun/KTP_Pasangan/");
        DateTime now = DateTime.Now;

        namafilektppasangan = txtidpasangan.Text + "_" + now.ToString("ddMMyyyy") + "_" + "KTP_Pasangan";

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadktppasangan.FileName) != namafilektppasangan)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafilektppasangan + "');", true);

        //}
        //else
        //{

        //Save the File to the Directory (Folder).
        fileuploadktppasangan.SaveAs(folderPath + namafilektppasangan + ".jpg");

        //Display the Picture in Image control.
        //imagepasangan.ImageUrl = "~/Files/Kresun/KTP_Pasangan" + Path.GetFileName(fileuploadktppasangan.FileName);

        txtppathktppasangan.Text = "~/Files/Kresun/KTP_Pasangan/" + namafilektppasangan + ".jpg";
        //txtppathktppasangan.Text = imagepasangan.ImageUrl;
        //}

    }
  
        
}