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
using System.Xml;
using System.Data.OleDb;

public partial class mikro_uploadfeemanagement : System.Web.UI.Page
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
    DataTable dtManagement = new DataTable("DataManagement");
    protected void Page_Load(object sender, EventArgs e)
    { 
        try
        {
            if (!IsPostBack)
            {
                ambilTahun();
                ambilBulan();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    #region"Button"
    protected void lnkCari_Click(object sender, EventArgs e)
    {

        if (FileUpload1.PostedFile != null)
        {
            try
            {
                string path = Server.MapPath("~/upload/" + FileUpload1.FileName);
                //FileUpload1.SaveAs(path);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (FileUpload1.FileName == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('file tidak ditemukan!!','top-right');", true);
                }
                // Import_To_Grid(path, ".xlsx", "Yes");
                else if (Extension == ".xlsx")
                {
                    cekdata();
                    if (File.Exists(path) == true)
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        File.Delete(path);
                    }
                    FileUpload1.SaveAs(path);
                    Import_To_Grid_Xlsx(path, ".xlsx", "Yes");
                    if (File.Exists(path) == true)
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        File.Delete(path);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Upload berhasil!','Data anda sudah berhasil di proses','top-right','success','1');", true);

                }
                else if (Extension == ".xls")
                {
                    cekdata();
                    if (File.Exists(path) == true)
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        File.Delete(path);
                    }
                    FileUpload1.SaveAs(path);
                    Import_To_Grid_Xls(path, ".xls", "Yes");
                    if (File.Exists(path) == true)
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        File.Delete(path);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Upload berhasil!','Data anda sudah berhasil di proses','top-right','success','1');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Format file anda tidak sesuai, cek contoh gambar!!','top-right');", true);
                }
            }
            catch (System.Exception X)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
        }
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btncancel, string.Empty, true);
    }
    protected void ddlsumberdana_change(object sender, EventArgs e)
    {

        //if (ddlTahun.SelectedIndex != 0)
        //{
        //    //btnsave.Disabled = false;
        //}
        //else if (ddlTahun.SelectedIndex == 0)
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Pilih Salah Satu Sumber Dana')", true);
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    #endregion"Button"

    #region"Funtion"
    private void Import_To_Grid_Xlsx(string FilePath, string Extension, string isHDR)
    {
        try
        {
            string path = Server.MapPath("~/upload/" + FileUpload1.FileName);
            string conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";

            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection myExcelConn = new OleDbConnection(conStr);
            OleDbCommand myExcelCmd = new OleDbCommand();
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            DataTable mydt = new DataTable();
            myExcelCmd.Connection = myExcelConn;
            myExcelConn.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = myExcelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //Fetch the name of First Sheet
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            myExcelConn.Close();

            //Read Data from First Sheet
            myExcelConn.Open();
            myExcelCmd.CommandText = "SELECT * From [" + SheetName + "]";
            myDataAdapter.SelectCommand = myExcelCmd;
            myDataAdapter.Fill(mydt);


            //  save datatable in a session which we used for pagination
            Session.Add("mySessionTable", mydt);

            int i;
            decimal fee1;
            dtManagement.Columns.Clear();
            dtManagement.Columns.Add(new DataColumn("KodeJabatan", typeof(string)));
            dtManagement.Columns.Add(new DataColumn("kodecabang", typeof(string)));
            dtManagement.Columns.Add(new DataColumn("Bulan", typeof(string)));
            dtManagement.Columns.Add(new DataColumn("Tahun", typeof(string)));
            dtManagement.Columns.Add(new DataColumn("Fee", typeof(decimal)));

            dtManagement.Rows.Clear();

            for (i = 0; i < mydt.Rows.Count; i++)
            {
                DataRow dr = dtManagement.NewRow();
                if (mydt.Rows[i][0].ToString() != null || mydt.Rows[i][0].ToString() != "")
                {
                    fee1 = Convert.ToDecimal(mydt.Rows[i][2].ToString());

                    dr["KodeJabatan"] = mydt.Rows[i][0].ToString();
                    dr["kodecabang"] = mydt.Rows[i][1].ToString();
                    dr["Bulan"] = ddlbulan.SelectedValue;
                    dr["Tahun"] = ddlTahun.SelectedValue;
                    dr["Fee"] = fee1;
                    dtManagement.Rows.Add(dr);
                }
            }
            DefaultClass.Global.SqlHelper[] varS = new DefaultClass.Global.SqlHelper[5];
            varS[0] = gb.CreateSPParameter("@AEFEE_UPLOADMANAGEMENT", ParameterDirection.Input, SqlDbType.Structured, 0, dtManagement, typeof(DataTable));
            varS[1] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            varS[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTMANAGEMENTFEE", typeof(string));
            varS[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)); ;
            varS[4] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeIUDMANAGEMENT", varS, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnsave.Disabled = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Save this Project','top-right','success','2');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
            myExcelConn.Close();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    private void Import_To_Grid_Xls(string FilePath, string Extension, string isHDR)
    {
        try
        {
            string path = Server.MapPath("~/upload/" + FileUpload1.FileName);
            string conStr = @"Provider=Microsoft.ACE.OLEDB.8.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0 Xml;HDR=YES;IMEX=1\";";

            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection myExcelConn = new OleDbConnection(conStr);
            OleDbCommand myExcelCmd = new OleDbCommand();
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            DataTable mydt = new DataTable();
            myExcelCmd.Connection = myExcelConn;
            myExcelConn.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = myExcelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //Fetch the name of First Sheet
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            myExcelConn.Close();

            //Read Data from First Sheet
            myExcelConn.Open();
            myExcelCmd.CommandText = "SELECT * From [" + SheetName + "]";
            myDataAdapter.SelectCommand = myExcelCmd;
            myDataAdapter.Fill(mydt);


            //  save datatable in a session which we used for pagination
            Session.Add("mySessionTable", mydt);

            int i;
            decimal fee1;
            dtManagement.Columns.Clear();
            dtManagement.Columns.Add(new DataColumn("KodeJabatan", typeof(string)));
            dtManagement.Columns.Add(new DataColumn("kodecabang", typeof(string)));
            dtManagement.Columns.Add(new DataColumn("Bulan", typeof(string)));
            dtManagement.Columns.Add(new DataColumn("Tahun", typeof(string)));
            dtManagement.Columns.Add(new DataColumn("Fee", typeof(decimal)));

            dtManagement.Rows.Clear();

            for (i = 0; i < mydt.Rows.Count; i++)
            {
                DataRow dr = dtManagement.NewRow();
                if (mydt.Rows[i][0].ToString() != null || mydt.Rows[i][0].ToString() != "")
                {
                    fee1 = Convert.ToDecimal(mydt.Rows[i][2].ToString());

                    dr["KodeJabatan"] = mydt.Rows[i][0].ToString();
                    dr["kodecabang"] = mydt.Rows[i][1].ToString();
                    dr["Bulan"] = ddlbulan.SelectedValue;
                    dr["Tahun"] = ddlTahun.SelectedValue;
                    dr["Fee"] = fee1;
                    dtManagement.Rows.Add(dr);
                }
            }
            DefaultClass.Global.SqlHelper[] varS = new DefaultClass.Global.SqlHelper[5];
            varS[0] = gb.CreateSPParameter("@AEFEE_UPLOADMANAGEMENT", ParameterDirection.Input, SqlDbType.Structured, 0, dtManagement, typeof(DataTable));
            varS[1] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            varS[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTMANAGEMENTFEE", typeof(string));
            varS[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)); ;
            varS[4] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeIUDMANAGEMENT", varS, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnsave.Disabled = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Save this Project','top-right','success','2');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
            myExcelConn.Close();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    private void cekdata()
    {
        try
        {
            string path = Server.MapPath("~/upload/" + FileUpload1.FileName);
            string conStr = "";

            if (FileUpload1.PostedFile != null)
            {
                DefaultClass.Global.SqlHelper[] varD = new DefaultClass.Global.SqlHelper[5];
                varD[0] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
                varD[1] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTahun.SelectedValue, typeof(string));
                varD[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEMANAGEMENTFEE", typeof(string));
                varD[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string)); ;
                varD[4] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_FeeIUDMANAGEMENT", varD, "constrmf");
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void ambilTahun()
    {
        try
        {
            var currentYear = DateTime.Today.Year;
            for (int i = 3; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                ddlTahun.Items.Add((currentYear - i).ToString());
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void ambilBulan()
    {
        try
        {
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < months.Length; i++)
            {
                ddlbulan.Items.Add(new ListItem { Text = months[i].ToString(), Value = (i + 1).ToString() });
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void SaveErroLog(string errorTexts, string fileErrors, string fromUsers, string fileName)
    {
        XmlDocument xmlEmloyeeDoc = new XmlDocument();
        xmlEmloyeeDoc.Load(fileName);
        XmlElement ParentElement = xmlEmloyeeDoc.CreateElement("ErrorLog");

        XmlElement root = xmlEmloyeeDoc.DocumentElement;
        XmlNodeList elemList = root.GetElementsByTagName("ID");

        XmlElement ID = xmlEmloyeeDoc.CreateElement("ID");
        ID.InnerText = (elemList.Count + 1).ToString();
        XmlElement ErrorText = xmlEmloyeeDoc.CreateElement("ErrorText");
        ErrorText.InnerText = errorTexts;
        XmlElement FileError = xmlEmloyeeDoc.CreateElement("FileError");
        FileError.InnerText = fileErrors;
        XmlElement FromUser = xmlEmloyeeDoc.CreateElement("FromUser");
        FromUser.InnerText = fromUsers;
        XmlElement TglError = xmlEmloyeeDoc.CreateElement("TglError");
        TglError.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


        ParentElement.AppendChild(ID);
        ParentElement.AppendChild(ErrorText);
        ParentElement.AppendChild(FileError);
        ParentElement.AppendChild(FromUser);
        ParentElement.AppendChild(TglError);

        xmlEmloyeeDoc.DocumentElement.AppendChild(ParentElement);
        xmlEmloyeeDoc.Save(fileName);
    }
    #endregion"Funtion"

    #region "Insertdata"
    public void InsertCombo()
    {
        try
        {
            // --- Combo Kode Cabang
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_VFaktorBM", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

            //ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddlcabang.DataTextField = "nama";
            //ddlcabang.DataValueField = "KodeCabang";
            //ddlcabang.DataBind();
            //ddlcabang.Items.Insert(0, new ListItem("Pilih Cabang", ""));

            //ddlcabangx.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddlcabangx.DataTextField = "nama";
            //ddlcabangx.DataValueField = "KodeCabang";
            //ddlcabangx.DataBind();
            //ddlcabangx.Items.Insert(0, new ListItem("Pilih Cabang", ""));

            // --- Combo Kode Jabatan
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJABATAN", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_VFaktorBM", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

            //ddljabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddljabatan.DataTextField = "nama";
            //ddljabatan.DataValueField = "KodeJab";
            //ddljabatan.DataBind();
            //ddljabatan.Items.Insert(0, new ListItem("Pilih Jabatan", ""));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion "Insertdata"
}



