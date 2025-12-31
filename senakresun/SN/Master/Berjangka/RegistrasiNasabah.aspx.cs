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

public partial class FA_Master_RegistrasiNasabah : System.Web.UI.Page
{
    String no_nasabah = "", nosebelumnya = "", updateno = "", CRNonasabah = "", STATE = "", kdpersh = "",
           formatnumber = "", msg1 = "", msg2 = "", msg3 = "", msg4 = "", msg5 = "", msg6 = "", msg7 = "", namafileKTP = "", namafileKK = "",
           namafileNPWP = "", namafileLainLain1 = "", namafileLainLain2 = "", namafilektppasangan = "";
    int counter_nonasabah = 0;
    long nosekarang = 0, nonasabah = 0;
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    public string fotopathktp = "", tanggal = "", fotopathnpwp = "";
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
        return Page.ClientScript.GetPostBackEventReference(btnBack, string.Empty, true);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void txtidentitas_TextChanged(object sender, EventArgs e)
    {
        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        //var[0] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
        //var[1] = gb.CreateSPParameter("@nama_ibu_kandung", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaibukandung.Text, typeof(string));
        //var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTP", typeof(string));
        //var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        
        //ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");

        //if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nasabah sudah terdaftar');", true);

        //}

    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
       
    }
 

   
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("RegistrasiNasabah.aspx");
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistrasiNasabah.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
               
                BindGridEntity();

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
    public void BindGridEntityx()
    {


        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SN_GetNasabah", Params, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "Idpermohonan desc";
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


        ge.ListDataset = gb.GetDataJustOutPut("SN_NASABAH", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_nasabah desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":
                HDState.Value = e.CommandName;

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;
                    pnlFilter.Visible = false;
                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SN_NASABAH", var, "constrmf");
 
                    HDNoNasabah.Value = no_nasabah;
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                   
                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
              
                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
               
                    txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
             
                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    txtpathktp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();
                    txtpathnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString();
                    txtfilename1.Text = ge.ListDataset.Tables[0].Rows[0]["filename1"].ToString();
                    txtfilename2.Text = ge.ListDataset.Tables[0].Rows[0]["filename2"].ToString();


                    HDNoNasabah.Value = "";
                    HDNoNasabah.Value = no_nasabah;
                  

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }


                break;
            case "SETDEL":

                try
                {
                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[3];
                    vart[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    vart[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    vart[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SN_NASABAH", vart, "constrmf");
                    txtpathktp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();
                    txtpathnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString();
                    txtfilename1.Text = ge.ListDataset.Tables[0].Rows[0]["filename1"].ToString();
                    txtfilename2.Text = ge.ListDataset.Tables[0].Rows[0]["filename2"].ToString();

                    //ddlKdWil.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeWil")).Text;
                    //var joinval = ddlKdGroup.SelectedValue + txtCustId.Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, no_nasabah, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDPERMOHONAN", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SN_NASABAH", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nasabah sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {

                        if (txtpathktp.Text != "")
                        {


                            string path = Server.MapPath("~/Files/KTP/");

                            FileInfo file = new FileInfo(path + txtfilename1.Text);
                            if (file.Exists)//check file exsit or not.
                            {
                                file.Delete();
                            }
                        }

                        if (txtpathnpwp.Text != "")
                        {


                            string path = Server.MapPath("~/Files/NPWP/");

                            FileInfo file = new FileInfo(path + txtfilename2.Text);
                            if (file.Exists)//check file exsit or not.
                            {
                                file.Delete();
                            }
                        }



                        //no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        //ddlKdWil.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeGroup")).Text;

                        //var JoinValue = ddlKdGroup.SelectedValue + txtCustId.Text;

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, no_nasabah, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("SN_NASABAH", var, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
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



    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        pnlFilter.Visible=false;


        //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
        //var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKONTERNASABAH", typeof(string));
        //var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", var1, "constrmf");

        //nosekarang = long.Parse(ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString());
        //formatnumber = ge.ListDataset.Tables[0].Rows[0]["NoAwal"].ToString();
        //nosebelumnya = ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString();

        //nonasabah = nosekarang + 1;

        //CRNonasabah = SessBranchID + String.Format("{0:" + formatnumber + "}", nonasabah);

        //updateno = String.Format("{0:" + formatnumber + "}", nonasabah);

        //HDNoNasabah.Value = CRNonasabah;
        //HDNoSebelumnya.Value = nosebelumnya;

        //txtkodepetugas.Text = SessLoginID;

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


                if (fileuploadktp.HasFile)
                {

                    string namafile;
                    string fotopath;
                    DateTime now = DateTime.Now;
                    namafile = now.ToString("ddMMyyyyhhmmss") + "_" + "KTP";
                    txtfilename1.Text = namafile + ".jpg";
                    string folderPath = Server.MapPath("~/Files/KTP/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string folderPathtumb = Server.MapPath("~/Files/KTP/thumb/");
                    if (!Directory.Exists(folderPathtumb))
                    {
                        Directory.CreateDirectory(folderPathtumb);
                    }

                    //create temp
                    string path = Server.MapPath("~/Files/KTP/thumb/" + namafile + ".jpg");
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
                    thumbBitmap.Save(Server.MapPath("~/Files/KTP/" + txtfilename1.Text), System.Drawing.Imaging.ImageFormat.Jpeg);
                    thumbGraph.Dispose();
                    thumbBitmap.Dispose();
                    image.Dispose();
                    fotopath = folderPath + txtfilename1.Text;
                    txtpathktp.Text = fotopath;
                    FileInfo file = new FileInfo(path);                
                    if (file.Exists)//check file exsit or not.
                    {
                        file.Delete();
                    }



                }
                else
                {
                    txtpathktp.Text = "";
                    txtfilename1.Text = "";

                }


                if (fileuploadnpwp.HasFile)
                {

                    string namafile;
                    string fotopath;
                    DateTime now = DateTime.Now;
                    namafile = now.ToString("ddMMyyyyhhmmss") + "_" + "NPWP";
                    txtfilename2.Text = namafile + ".jpg";
                    string folderPath = Server.MapPath("~/Files/NPWP/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string folderPathtumb = Server.MapPath("~/Files/NPWP/thumb/");
                    if (!Directory.Exists(folderPathtumb))
                    {
                        Directory.CreateDirectory(folderPathtumb);
                    }

                    //create temp
                    string path = Server.MapPath("~/Files/NPWP/thumb/" + namafile + ".jpg");
                    fileuploadnpwp.SaveAs(path);
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
                    thumbBitmap.Save(Server.MapPath("~/Files/NPWP/" + txtfilename2.Text), System.Drawing.Imaging.ImageFormat.Jpeg);
                    thumbGraph.Dispose();
                    thumbBitmap.Dispose();
                    image.Dispose();
                    fotopath = folderPath + txtfilename2.Text;
                    txtpathnpwp.Text = fotopath;
               

                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not.
                    {
                        file.Delete();
                    }
                }
                else
                {
                    txtpathnpwp.Text = "";
                    txtfilename2.Text = "";

                }





            }

               

                //INSERT INTO ARNASABAH
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[21];

                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoNasabah.Value, typeof(string));
                var[1] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamanasabah.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 250, txtidentitas.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@no_npwp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtNPWP.Text, typeof(string));
                 var[4] = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamat.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@kabupaten", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkabupaten.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@RT", ParameterDirection.Input, SqlDbType.VarChar, 250, txtrtktp.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@RW", ParameterDirection.Input, SqlDbType.VarChar, 250, txtrwktp.Text, typeof(string));
                var[8] = gb.CreateSPParameter("@kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkelurahanktp.Text, typeof(string));
                var[9] = gb.CreateSPParameter("@kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkecamatanktp.Text, typeof(string));
                var[10] = gb.CreateSPParameter("@provinsi", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlprovinsiktp.SelectedValue, typeof(string));
                var[11] = gb.CreateSPParameter("@kodepos", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkodeposktp.Text, typeof(string));
                var[12] = gb.CreateSPParameter("@file_pathktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathktp.Text, typeof(string));
                var[13] = gb.CreateSPParameter("@file_pathnpwp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathnpwp.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[15] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[16] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                var[17] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                var[18] = gb.CreateSPParameter("@Userlogin", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                var[19] = gb.CreateSPParameter("@filename1", ParameterDirection.Input, SqlDbType.VarChar, 80, txtfilename1.Text, typeof(string));
                var[20] = gb.CreateSPParameter("@filename2", ParameterDirection.Input, SqlDbType.VarChar, 80, txtfilename2.Text, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SN_NASABAH", var, "constrmf");

                msg1 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();






            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                        btnNexIdentitas.Visible = false;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);

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

   
  
    public void InsertCombo()
    {
             
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsiktp.DataTextField = "nama_provinsi";
        ddlprovinsiktp.DataValueField = "kode_provinsi";
        ddlprovinsiktp.DataBind();
       // ddlprovinsiktp.Items.Insert(0, new ListItem("Pilih Data", "-"));

       
    }

    protected void UploadFileKTP(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/KTP/");
        DateTime now = DateTime.Now;

        namafileKTP = txtidentitas.Text + "_" + now.ToString("ddMMyyyy") + "_" + "KTP";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

       
        fileuploadktp.SaveAs(folderPath + namafileKTP + ".jpg");


        fotopathktp = folderPath + Path.GetFileName(fileuploadktp.FileName);
        txtpathktp.Text = "~/Files/KTP/" + namafileKTP + ".jpg";

   

    }

    protected void UploadFileNPWP(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/NPWP/SN/");
        DateTime now = DateTime.Now;

        namafileNPWP = txtNPWP.Text + "_" + now.ToString("ddMMyyyy") + "_" + "NPWP";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

       
        fileuploadnpwp.SaveAs(folderPath + namafileNPWP + ".jpg");



        fotopathnpwp = folderPath + Path.GetFileName(fileuploadnpwp.FileName);
        txtpathnpwp.Text = "~/Files/NPWP/" + namafileNPWP + ".jpg";

    }


        
}