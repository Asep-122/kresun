using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;
using System.Text;
using System.Drawing;
using DefaultClass;
using EntitiesClass;
using System.Security.Cryptography;
using System.IO;
using System;
using System.Data;

public partial class MasterTemplate_Changepasswordspc : System.Web.UI.Page
{
    public string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }
    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string password, passwordbaru = "", passwordbaruverifikasi = "";
            //password = Encrypt(txtPassword.Text);
            passwordbaru = Encrypt(txtPasswordbaru.Text);
            passwordbaruverifikasi = Encrypt(txtPasswordbarukonfirmasi.Text);

            if (txtPasswordbaru.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Password baru tidak boleh kosong');", true);
                //return;
            }
            else if (txtPasswordbarukonfirmasi.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Password baru verifikasi tidak boleh kosong');", true);
                //return;
            }
            else if (txtPasswordbaru.Text != txtPasswordbarukonfirmasi.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Password baru dan verifikasi Tidak Sama');", true);
                //return;
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@SessLoginID", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSPC", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                
                ge.ListDataset = gb.GetDataJustOutPut("MF_User", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
                    Params[0] = gb.CreateSPParameter("@SessLoginID", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    Params[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATE", typeof(string));
                    Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[3] = gb.CreateSPParameter("@passwordbaru", ParameterDirection.Input, SqlDbType.VarChar, 100, passwordbaru, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("MF_User", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlertAfterSave('Change Password Done');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Password lama salah');", true);
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public string Encrypt(string str)
    {
        string EncrptKey = "2013;[pnuLIT)X";
        byte[] byKey = { };
        byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
        byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return Convert.ToBase64String(ms.ToArray());
    }
    public string Decrypt(string str)
    {
        str = str.Replace(" ", "+");
        string DecryptKey = "2013;[pnuLIT)Y";
        byte[] byKey = { };
        byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
        byte[] inputByteArray = new byte[str.Length];

        byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        return encoding.GetString(ms.ToArray());
    }

   }