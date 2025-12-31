using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Activities.Expressions;
using System.Runtime.Serialization.Configuration;

public partial class Home : System.Web.UI.Page
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
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    public string Sesstglupdate
    {
        get { return Session["Sesstglupdate"].ToString(); }
        set { ViewState["Sesstglupdate"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();
    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)

            {
                //135MARKETING1
                var xnama = "";
                if (SessLoginID.Length >= 12)
                {
                    xnama = SessLoginID.Substring(3, 9);
                }
                else
                {
                    xnama = SessLoginID.ToUpper();
                }
                
                if (SessBranchID.ToLower() == "000" && (SessLoginID.ToUpper() == "APPROVAL1" || SessLoginID.ToUpper() == "APPROVAL2" || SessLoginID.ToUpper() == "APPROVAL3" || SessLoginID.ToUpper() == "PUSAT01" || SessLoginID.ToUpper() == "GMPUSAT"))
                {
                    apppusat();

                    pnlbank.Visible = false;
                    PnlNotifikasi.Visible = false;
                    pnlData.Visible = false;
                    Pnlapproval.Visible = true;
                }
                else if (SessBranchID.ToLower() != "000" && (xnama.ToUpper() == "MARKETING"))
                {
                    pnlbank.Visible = false;
                    PnlNotifikasi.Visible = false;
                    pnlData.Visible = false;
                    Pnlapproval.Visible = false;
                }
                else if ((SessBranchID != "000" || SessBranchID != "999") && (xnama.ToUpper() != "MARKETING"))
                {
                    cabang();
                    pnlbank.Visible = true;
                    PnlNotifikasi.Visible = true;
                    pnlData.Visible = true;
                    Pnlapproval.Visible = false;
                }
                //myModal.Visible = true;

                DateTime tglupdate;
                tglupdate = DateTime.Parse(Sesstglupdate);
                var currentDate = DateTime.Today;
                TimeSpan daysUntilVacation = currentDate-tglupdate;
                int hari = 31 - daysUntilVacation.Days;
                if (hari <= 7)
                {
                    btnpassword.Visible = true;
                    btnpassword.Text = "Password anda Expired " + hari + " hari segera rubah password anda ";
                    btnpassword.BackColor = Color.Red;
                }
                else
                {
                    btnpassword.Visible = false;
                }

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    //protected void BtnupdateKas_onclick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (SessBranchID.ToLower() == "000" && (SessLoginID.ToUpper() == "APPROVAL1" || SessLoginID.ToUpper() == "APPROVAL2" || SessLoginID.ToUpper() == "APPROVAL3" || SessLoginID.ToUpper() == "PUSAT01" || SessLoginID.ToUpper() == "GMPUSAT"))
    //        {
    //            apppusat();

    //            pnlbank.Visible = false;
    //            PnlNotifikasi.Visible = false;
    //            pnlData.Visible = false;
    //            Pnlapproval.Visible = true;
    //        }
    //        else if (SessBranchID != "000" || SessBranchID != "999")
    //        {
    //            cabang();
    //            pnlbank.Visible = true;
    //            PnlNotifikasi.Visible = true;
    //            pnlData.Visible = true;
    //            Pnlapproval.Visible = false;
    //        }
    //    }
    //    catch (System.Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //    }
    //}
    protected void Btnupdatesaldo_onclick(object sender, EventArgs e)
    {
        try
        {
            if (SessBranchID.ToLower() == "000" && (SessLoginID.ToUpper() == "APPROVAL1" || SessLoginID.ToUpper() == "APPROVAL2" || SessLoginID.ToUpper() == "APPROVAL3" || SessLoginID.ToUpper() == "PUSAT01" || SessLoginID.ToUpper() == "GMPUSAT"))
            {
                apppusat();

                pnlbank.Visible = false;
                PnlNotifikasi.Visible = false;
                pnlData.Visible = false;
                Pnlapproval.Visible = true;
            }
            else if (SessBranchID != "000" || SessBranchID != "999")
            {
                cabang();
                pnlbank.Visible = true;
                PnlNotifikasi.Visible = true;
                pnlData.Visible = true;
                Pnlapproval.Visible = false;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnberjangka_onclick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SN/process/ViewNotifikasiBerjangka.aspx");
            //Session.Contents.Add("NotifFrontBungaBerjangka", "1");
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    
         protected void btnkaryawan_onclick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/MasterTemplate/ViewKaryawanulangtahun.aspx");
            //Session.Contents.Add("NotifFrontBungaBerjangka", "1");
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnlelang_onclick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/MasterTemplate/Viewassetlelang.aspx");
            //Session.Contents.Add("NotifFrontBungaBerjangka", "1");
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
   
    
    
    protected void BtnSukarela_onclick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SN/process/ViewNotifikasiSukarela.aspx");
            //Session.Contents.Add("NotifFrontBungaBerjangka", "1");
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void BtnModal_onclick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SN/process/ViewNotifikasiModal.aspx");
            //Session.Contents.Add("NotifFrontBungaBerjangka", "1");
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void BtnHutangBank_onclick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SN/process/ViewNotifikasiHutangBank.aspx");
            //Session.Contents.Add("NotifFrontBungaBerjangka", "1");
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    #region AmbilData
    public void pusat()
    {
        try
        {
           

            pnlbank.Visible = false;
            PnlNotifikasi.Visible = false;
            pnlData.Visible = false;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void apppusat()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DSH_APPROVE", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                //Manage penutupan
                lblticketAnggotatutup.Text = ge.ListDataset.Tables[0].Rows[0]["total_penutupanout"].ToString();
                Lblticketanggotaopen.Text = ge.ListDataset.Tables[0].Rows[0]["total_penutupanopen"].ToString();
                LblticketAnggotaApprove.Text = ge.ListDataset.Tables[0].Rows[0]["total_penutupan"].ToString();
                LblticketAnggotaTolak.Text = ge.ListDataset.Tables[0].Rows[0]["total_penutupanulang"].ToString();
                           //Pinjaman
                lblpinjamanout.Text = ge.ListDataset.Tables[0].Rows[0]["total_pinjamanout"].ToString();
                lblpinjamanreal.Text = ge.ListDataset.Tables[0].Rows[0]["total_pinjaman"].ToString();
                //lblapppinjamanout.Text = ge.ListDataset.Tables[0].Rows[0]["total_pinjamanout"].ToString();
                lblpinjamanulang.Text = ge.ListDataset.Tables[0].Rows[0]["total_pinjamanulang"].ToString();
                //lblapppinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["total_pinjaman"].ToString();

                //Bdm
                lblbdmout.Text = ge.ListDataset.Tables[0].Rows[0]["total_bdmout"].ToString();
                lblbdmulang.Text = ge.ListDataset.Tables[0].Rows[0]["total_bdmulang"].ToString();
                lblbdmreal.Text = ge.ListDataset.Tables[0].Rows[0]["total_bdm"].ToString();
            
                //Biaya Approval
                lblbiayaout.Text = ge.ListDataset.Tables[0].Rows[0]["total_biayapusatout"].ToString();
                lblbiayaulang.Text = ge.ListDataset.Tables[0].Rows[0]["total_biayapusatulang"].ToString();
                lblbiayareal.Text = ge.ListDataset.Tables[0].Rows[0]["total_biayapusat"].ToString();
            
                //Pelunasan Diskon
                lblpelunasanout.Text = ge.ListDataset.Tables[0].Rows[0]["total_pelunasandskout"].ToString();
                lblpelunasanulang.Text = ge.ListDataset.Tables[0].Rows[0]["total_pelunasandskulang"].ToString();
                lblpelunasanreal.Text = ge.ListDataset.Tables[0].Rows[0]["total_pelunasandsk"].ToString();
         
                //Aktivasi Wo
                LblWoTicket.Text = (int.Parse(ge.ListDataset.Tables[0].Rows[0]["total_aktivasiwoout"].ToString()) + 
                int.Parse(ge.ListDataset.Tables[0].Rows[0]["total_aktivasiwo"].ToString()) + int.Parse(ge.ListDataset.Tables[0].Rows[0]["total_aktivasiwoulang"].ToString())).ToString();
                LBLWOOpen.Text = ge.ListDataset.Tables[0].Rows[0]["total_aktivasiwoout"].ToString();
                LBLWOApprove.Text = ge.ListDataset.Tables[0].Rows[0]["total_aktivasiwo"].ToString();
                LblWOTolak.Text = ge.ListDataset.Tables[0].Rows[0]["total_aktivasiwoulang"].ToString();
                          //Aktivasi KTP
                LblKTPTICKET.Text = (int.Parse(ge.ListDataset.Tables[0].Rows[0]["total_aktivasiktpout"].ToString()) +
                int.Parse(ge.ListDataset.Tables[0].Rows[0]["total_aktivasiktp"].ToString()) + int.Parse(ge.ListDataset.Tables[0].Rows[0]["total_aktivasiktpulang"].ToString())).ToString();
                LBLKTPTICKETOPEN.Text = ge.ListDataset.Tables[0].Rows[0]["total_aktivasiktpout"].ToString();
                LBLKTPAPPROVE.Text = ge.ListDataset.Tables[0].Rows[0]["total_aktivasiktp"].ToString();
                LBLKTPTOLAK.Text = ge.ListDataset.Tables[0].Rows[0]["total_aktivasiktpulang"].ToString();
            }
            else
            {
                lblticketAnggotatutup.Text = "0";
                Lblticketanggotaopen.Text = "0";
                LblticketAnggotaApprove.Text = "0";
                LblticketAnggotaTolak.Text = "0";

                lblpinjamanout.Text = "0";
                lblpinjamanreal.Text = "0";
           
                lblpinjamanulang.Text = "0";

                lblpinjamanulang.Text = "0";

                lblbdmulang.Text = "0";

              

                lblpelunasanulang.Text = "0";
                lblbiayaulang.Text = "0";
            }

          

            pnlbank.Visible = false;
            PnlNotifikasi.Visible = false;
            pnlData.Visible = false;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void cabang()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DSH_Depan", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                Lblanggota.Text = ge.ListDataset.Tables[0].Rows[0]["total_anggota"].ToString();
                LblPermohonan.Text = ge.ListDataset.Tables[0].Rows[0]["total_permohonan"].ToString();
                LblPembatalan.Text = ge.ListDataset.Tables[0].Rows[0]["total_batal"].ToString();
                lblpencairan.Text = ge.ListDataset.Tables[0].Rows[0]["total_Cair"].ToString();
            }
            else
            {
                Lblanggota.Text = "0";
                LblPermohonan.Text = "0";
                LblPembatalan.Text = "0";
                lblpencairan.Text = "0";
            }

            DefaultClass.Global.SqlHelper[] vare = new DefaultClass.Global.SqlHelper[4];
            vare[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            vare[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            vare[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            vare[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DSH_SALDO", vare, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                Lblcashteller.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["teller"].ToString())); ;
                lblheadteller.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["headteller"].ToString()));
                lblbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["mandiri"].ToString()));
                lblcadangan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangan"].ToString()));
                Button3.Text = ge.ListDataset.Tables[0].Rows[0]["fiskal"].ToString();
                Button3.BackColor = Color.WhiteSmoke;
                Button3.ForeColor = Color.Black;
                Button3.Font.Bold = true;
                if (decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["harianpusat"].ToString()) >= 0)
                {
                    BtnAdjusment.Text = "Harian Pusat= " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["harianpusat"].ToString()));
                    BtnAdjusment.BackColor = Color.Green;
                }
                else
                {
                    BtnAdjusment.Text = "Harian Pusat= " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["harianpusat"].ToString()));
                    BtnAdjusment.BackColor = Color.Red;
                }
                //BtnKasTl.Text = "Kas TL Hari ini + " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kastellerdb"].ToString())) + " / - " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kastellerKR"].ToString())) ;
                //BtnKasHT.Text = "Kas HT Hari ini + " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasHTdb"].ToString())) + " / - " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasHTKR"].ToString()));
                //BtnBank.Text = "Bank Hari ini + " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasBKdb"].ToString())) + " / - " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasBKKR"].ToString()));
                //BtnCadangan.Text = "Cadangan Hari ini + " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasCDdb1"].ToString())) + " / - " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasCDKR1"].ToString()));


                ////Btntunggakan.Text = "Tunggakan Hari ini Pokok : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanpokok"].ToString())) + " / Bunga : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanbunga"].ToString())) + " / Total : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totaltunggakan"].ToString()));
                //lbltunggakanpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanpokok"].ToString()));
                //Lbltunggakanbunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanbunga"].ToString()));
                //lbltotaltunggakan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totaltunggakan"].ToString()));

                //Lblterbayarpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarpokok"].ToString()));
                //LblterbayarBunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarbunga"].ToString()));
                //lbltotalterbayar.Text =string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalTerbayar"].ToString()));

                ////Btnterbayar.Text = "Terbayar Hari ini Pokok : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarpokok"].ToString())) + " / Bunga : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarbunga"].ToString())) + " / Total : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalTerbayar"].ToString()));
                //LblrealiasasiAB.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["ABrealisasi"].ToString())) + " / " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonAB"].ToString())) ;
                //lblrealiasiAL.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["ALRealiasasi"].ToString())) + " / " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonAL"].ToString()));
            }
            else
            {
                Lblcashteller.Text = "0";
                lblheadteller.Text = "0";
                lblbank.Text = "0";
                lblcadangan.Text = "0";
                BtnAdjusment.Text = "Harian Pusat= 0";
                BtnAdjusment.BackColor = Color.Gray;
                Button3.Text = "";
                //BtnKasTl.Text = "Kas TL Hari ini + " + string.Format("{0:N0}", decimal.Parse("0")) + " / - " + string.Format("{0:N0}", decimal.Parse("0"));
                //BtnKasHT.Text = "Kas HT Hari ini + " + string.Format("{0:N0}", decimal.Parse("0")) + " / - " + string.Format("{0:N0}", decimal.Parse("0"));
                //BtnBank.Text = "Bank Hari ini + " + string.Format("{0:N0}", decimal.Parse("0")) + " / - " + string.Format("{0:N0}", decimal.Parse("0"));
                //BtnCadangan.Text = "Cadangan Hari ini + " + string.Format("{0:N0}", decimal.Parse("0")) + " / - " + string.Format("{0:N0}", decimal.Parse("0"));

                ////Btntunggakan.Text = "Tunggakan Hari ini Pokok : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanpokok"].ToString())) + " / Bunga : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanbunga"].ToString())) + " / Total : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totaltunggakan"].ToString()));
                //lbltunggakanpokok.Text = string.Format("{0:N0}", decimal.Parse("0"));
                //Lbltunggakanbunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
                //lbltotaltunggakan.Text = string.Format("{0:N0}", decimal.Parse("0"));

                //Lblterbayarpokok.Text = string.Format("{0:N0}", decimal.Parse("0"));
                //LblterbayarBunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
                //lbltotalterbayar.Text = string.Format("{0:N0}", decimal.Parse("0"));

                ////Btnterbayar.Text = "Terbayar Hari ini Pokok : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarpokok"].ToString())) + " / Bunga : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarbunga"].ToString())) + " / Total : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalTerbayar"].ToString()));
                //LblrealiasasiAB.Text = string.Format("{0:N0}", decimal.Parse("0")) + " / " + string.Format("{0:N0}", decimal.Parse("0"));
                //lblrealiasiAL.Text = string.Format("{0:N0}", decimal.Parse("0")) + " / " + string.Format("{0:N0}", decimal.Parse("0"));


            }



        



            lbltotalbungaberjangka.Text = "0";
            lbltotalpajakberjangka.Text = "0";
            lbltotalpencairanberjangka.Text = "0";

            lbltotalbungaModal.Text = "0";
            lbltotalpajakModal.Text = "0";
            lbltotalbungasukarela.Text = "0";
            lbltotalpajaksukarela.Text = "0";
            lbltotalbungahutangbank.Text = "0";


            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNOTIF", typeof(string));
            var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("DSH_NOTIF", var1, "constrmf");


            lbltotalbungaberjangka.Text = ge.ListDataset.Tables[0].Rows[0]["totalbungaberjangka"].ToString();
            lbltotalpajakberjangka.Text = ge.ListDataset.Tables[0].Rows[0]["totalpajakberjangka"].ToString();
            lbltotalpencairanberjangka.Text = ge.ListDataset.Tables[0].Rows[0]["totalpencairanberjangka"].ToString();

            lbltotalbungaModal.Text = ge.ListDataset.Tables[0].Rows[0]["totalbungaModal"].ToString();
            lbltotalpajakModal.Text = ge.ListDataset.Tables[0].Rows[0]["totalpajakModal"].ToString();

            lbltotalbungasukarela.Text = ge.ListDataset.Tables[0].Rows[0]["totalbungasukarela"].ToString();
            lbltotalpajaksukarela.Text = ge.ListDataset.Tables[0].Rows[0]["totalpajaksukarela"].ToString();




            int bunga = 0;
            int pajak = 0;
            int pencairan = 0;

            bunga = int.Parse(lbltotalbungaberjangka.Text);
            pajak = int.Parse(lbltotalpajakberjangka.Text);
            pencairan = int.Parse(lbltotalpencairanberjangka.Text);

        

            pnlbank.Visible = true;
            PnlNotifikasi.Visible = true;
            pnlData.Visible = true;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    public void BtnupdateKas_onclick(object sender, EventArgs e)
    {
        try
        {
            

            DefaultClass.Global.SqlHelper[] vare = new DefaultClass.Global.SqlHelper[4];
            vare[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKAS", typeof(string));
            vare[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            vare[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            vare[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DSH_SALDO", vare, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
              
                BtnKasTl.Text = "Kas TL Hari ini + " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kastellerdb"].ToString())) + " / - " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kastellerKR"].ToString()));
                BtnKasHT.Text = "Kas HT Hari ini + " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasHTdb"].ToString())) + " / - " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasHTKR"].ToString()));
                BtnBank.Text = "Bank Hari ini + " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasBKdb"].ToString())) + " / - " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasBKKR"].ToString()));
                BtnCadangan.Text = "Cadangan Hari ini + " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasCDdb1"].ToString())) + " / - " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kasCDKR1"].ToString()));


                
            }
            else
            {
                
                BtnAdjusment.Text = "Harian Pusat= 0";
                BtnAdjusment.BackColor = Color.Gray;
                Button3.Text = "";
                BtnKasTl.Text = "Kas TL Hari ini + " + string.Format("{0:N0}", decimal.Parse("0")) + " / - " + string.Format("{0:N0}", decimal.Parse("0"));
                BtnKasHT.Text = "Kas HT Hari ini + " + string.Format("{0:N0}", decimal.Parse("0")) + " / - " + string.Format("{0:N0}", decimal.Parse("0"));
                BtnBank.Text = "Bank Hari ini + " + string.Format("{0:N0}", decimal.Parse("0")) + " / - " + string.Format("{0:N0}", decimal.Parse("0"));
                BtnCadangan.Text = "Cadangan Hari ini + " + string.Format("{0:N0}", decimal.Parse("0")) + " / - " + string.Format("{0:N0}", decimal.Parse("0"));

              

            }

           
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    public void Tunggakan(object sender, EventArgs e)
    {
        try
        {


            DefaultClass.Global.SqlHelper[] vare = new DefaultClass.Global.SqlHelper[4];
            vare[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTUNGGAKAN", typeof(string));
            vare[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            vare[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            vare[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DSH_SALDO", vare, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                
                //Btntunggakan.Text = "Tunggakan Hari ini Pokok : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanpokok"].ToString())) + " / Bunga : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanbunga"].ToString())) + " / Total : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totaltunggakan"].ToString()));
                lbltunggakanpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanpokok"].ToString()));
                Lbltunggakanbunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanbunga"].ToString()));
                lbltotaltunggakan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totaltunggakan"].ToString()));

                   
            }
            else
            {
              
                //Btntunggakan.Text = "Tunggakan Hari ini Pokok : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanpokok"].ToString())) + " / Bunga : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunggakanbunga"].ToString())) + " / Total : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totaltunggakan"].ToString()));
                lbltunggakanpokok.Text = string.Format("{0:N0}", decimal.Parse("0"));
                Lbltunggakanbunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
                lbltotaltunggakan.Text = string.Format("{0:N0}", decimal.Parse("0"));

           

            }

        
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void Terbayar(object sender, EventArgs e)
    {
        try
        {


            DefaultClass.Global.SqlHelper[] vare = new DefaultClass.Global.SqlHelper[4];
            vare[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTERBAYAR", typeof(string));
            vare[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            vare[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            vare[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DSH_SALDO", vare, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {

            
                Lblterbayarpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarpokok"].ToString()));
                LblterbayarBunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarbunga"].ToString()));
                lbltotalterbayar.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalTerbayar"].ToString()));

                //Btnterbayar.Text = "Terbayar Hari ini Pokok : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarpokok"].ToString())) + " / Bunga : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarbunga"].ToString())) + " / Total : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalTerbayar"].ToString()));
                LblrealiasasiAB.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["ABrealisasi"].ToString())) + " / " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonAB"].ToString()));
                lblrealiasiAL.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["ALRealiasasi"].ToString())) + " / " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonAL"].ToString()));
            }
            else
            {
               

                Lblterbayarpokok.Text = string.Format("{0:N0}", decimal.Parse("0"));
                LblterbayarBunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
                lbltotalterbayar.Text = string.Format("{0:N0}", decimal.Parse("0"));

                //Btnterbayar.Text = "Terbayar Hari ini Pokok : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarpokok"].ToString())) + " / Bunga : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Terbayarbunga"].ToString())) + " / Total : " + string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalTerbayar"].ToString()));
                LblrealiasasiAB.Text = string.Format("{0:N0}", decimal.Parse("0")) + " / " + string.Format("{0:N0}", decimal.Parse("0"));
                lblrealiasiAL.Text = string.Format("{0:N0}", decimal.Parse("0")) + " / " + string.Format("{0:N0}", decimal.Parse("0"));


            }

          
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }




    #endregion
}