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
using System.Web.Script.Serialization;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class AR_View_CetakKartuAngsuran : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }
    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

                txttgltagih.Text = xtanggal.ToString("dd/MM/yyyy");
                txtkodecabang.Text = SessBranchID;
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnNext);
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
        try
        {
            string[] splitdata = txtno_rekening.Text.Split('-');
            string no_rekening = splitdata[0];
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

            var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTU", typeof(string));
            var[3] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
            //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_CETAKAN", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "angs_ke asc";

            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnprinttagih_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
            string[] splitdata = txtno_rekening.Text.Split('-');
            string no_rekening = splitdata[0];
            string lblProduk = txtjenispinjaman.Text;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];

            var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTUNEW", typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
            //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("AR_CETAKAN", var, "constrmf").Tables[0];
            //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_CETAKAN", var, "constrmf");
            if (ge.ListDataTable.Rows.Count > 0)
            {


                //rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih.rpt"));

                if (SessKoperasiID == "I")
                {
                    if (lblProduk == "3C")
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih3C.rpt"));
                    }
                    else
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih.rpt"));
                    }

                }
                else if (SessKoperasiID == "II")
                {
                    if (lblProduk == "3C")
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih23C.rpt"));
                    }
                    else
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih2.rpt"));
                    }


                }
                else if (SessKoperasiID == "III")
                {

                    if (lblProduk == "3C")
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih33C.rpt"));
                    }
                    else
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih3.rpt"));
                    }
                }
                else if (SessKoperasiID == "IV")
                {

                    if (lblProduk == "3C")
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih43C.rpt"));
                    }
                    else
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih4.rpt"));
                    }
                }
                else if (SessKoperasiID == "V")
                {

                    if (lblProduk == "3C")
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih53C.rpt"));
                    }
                    else
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih5.rpt"));
                    }
                }


                string notransferhp = ge.ListDataTable.Rows[0]["datatransfer"].ToString() == "" ? "" : "Bukti Transfer di kirim ke No : " + ge.ListDataTable.Rows[0]["datatransfer"].ToString() + ".";

            rprt.SetDataSource(ge.ListDataTable);

            //rprt.SetParameterValue("Notes1", notransferhp);
            rprt.SetParameterValue("bagian", ge.ListDataTable.Rows[0]["bagian"].ToString());

            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

            Response.End();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('test3!');", true);
             }
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            }
            else
            {
                rprt.Close();
                rprt.Dispose();
                GC.Collect();
            }
        }
    }

    protected void ddlmajuhari_SelectedIndexChanged(object sender, EventArgs e)


    {

        int x = int.Parse(ddlmajuhari.SelectedValue);
        x = x;

        if (txtjenispinjaman.Text == "3C")
        {
            txttgltagih.Text = DateTime.Parse(txttglrealisasi.Text).AddDays(x).ToString("dddd");
        }
        else
        {
            txttgltagih.Text = DateTime.Parse(txttglrealisasi.Text).AddDays(x).ToString("dd");
        }




    }

    protected void ddltypepilih_SelectedIndexChanged(object sender, EventArgs e)


    {

        if (ddltypepilih.SelectedValue == "ALL")
        {
            PnlMajuHari.Visible = false;
           
            Pnltgltagihmanual.Visible = false;
           
        }
        else if (ddltypepilih.SelectedValue == "1")
        {
            PnlMajuHari.Visible = true;
   
            Pnltgltagihmanual.Visible = false;
          
        }
        else if (ddltypepilih.SelectedValue == "2")
        {
            PnlMajuHari.Visible = false;

            Pnltgltagihmanual.Visible = true;
         
        }




    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
            string[] splitdata = txtno_rekening.Text.Split('-');
            string no_rekening = splitdata[0];
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];

            var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTUNEWJWL", typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("AR_CETAKAN", var, "constrmf").Tables[0];
            //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_CETAKAN", var, "constrmf");
           if (ge.ListDataTable.Rows.Count > 0)
            {

           
                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagihjwl.rpt"));

            rprt.SetDataSource(ge.ListDataTable);

            rprt.SetParameterValue("bagian", ge.ListDataTable.Rows[0]["bagian"].ToString());

            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


            Response.End();
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('test3!');", true);
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            }
            else
            {
                rprt.Close();
                rprt.Dispose();
                GC.Collect();
            }

        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string[] splitdata = txtno_rekening.Text.Trim().Split('-');
            string no_rekening = splitdata[0];

            if (txtno_rekening.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah belum di isi');", true);
            }
            else if (txtkodecabang.Text != txtno_rekening.Text.Substring(0, 3))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah yang di input tidak terdapat di cabang Anda');", true);
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
                var3[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
                var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNASABAHFORINFOKARTU", typeof(string));
                //var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_CETAKAN", var3, "constrmf");
                if (ge.ListDataset.Tables[0].DefaultView.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota yang Anda pilih belum sesuai, silahkan pilih salah satu data terlebih dahulu');", true);
                    txtno_rekening.Focus();
                }
                else
                {
                    txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                    txtinstansi.Text = ge.ListDataset.Tables[0].Rows[0]["instansi"].ToString();
                    txtAo.Text = ge.ListDataset.Tables[0].Rows[0]["MO"].ToString();
                    txtplafon.Text = ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString();
                    txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                    txtNamaNasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtkelurahan.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
                    txtjenispinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                    txtnorek.Text = no_rekening.Trim();
                    txtharitagih.Text = ge.ListDataset.Tables[0].Rows[0]["hari_tagih"].ToString();
                    pnladd.Visible = true;
                    pnlFilter.Visible = false;
                    PnlFront.Visible = true;

                    BindGridEntity();

                    Global.SqlHelper[] Paramsc = new Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorek.Text, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMAXHARI", typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenispinjaman.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AR_CETAKAN", Paramsc, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        
                        txttglrealisasi.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_tagih"].ToString()).ToString("dd/MM/yyyy");
                        txttgltagih.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_tagih"].ToString()).ToString("dddd");
                        
                    }
                    else
                    {
                        txttglrealisasi.Text = "";
                        txttgltagih.Text = txttglrealisasi.Text;

                    }
                }
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txttgltagih.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rubah Tanggal Tagih Masih Kosong!');", true);
            }
            //else if (txtjenispinjaman.Text.Trim() == "3C")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Perubahan Tanggal tagih hanya untuk Product Bulanan!');", true);
            //}
            else
            {
                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[8];
                Paramsc[0] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                Paramsc[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramsc[3] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorek.Text, typeof(string));
                Paramsc[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATETGLTAGIHNEW", typeof(string));
                Paramsc[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                Paramsc[6] = gb.CreateSPParameter("@majuhari", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlmajuhari.SelectedValue), typeof(int));
                Paramsc[7] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, txttgltagih.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_CETAKAN", Paramsc, "constrmf");


                BindGridEntity();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Update Tanggal Tagih Done');", true);
                Global.SqlHelper[] Paramstc = new Global.SqlHelper[6];
                Paramstc[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Paramstc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramstc[2] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorek.Text, typeof(string));
                Paramstc[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMAXHARI", typeof(string));
                Paramstc[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                Paramstc[5] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenispinjaman.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_CETAKAN", Paramstc, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {

                    txttglrealisasi.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_tagih"].ToString()).ToString("dd/MM/yyyy");
                    if (txtjenispinjaman.Text == "3C")
                    {
                        txttgltagih.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_tagih"].ToString()).ToString("dddd");
                    }
                    else
                    {
                        txttgltagih.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_tagih"].ToString()).ToString("dd");
                    }
                   

                }
                else
                {
                    txttglrealisasi.Text = "";
                    txttgltagih.Text = txttglrealisasi.Text;

                }
                ddlmajuhari.SelectedValue = "0";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnupdate1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txttgltagihmanual.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rubah Tanggal Tagih Masih Kosong!');", true);
            }

            else
            {
                var haritagih="";

                if (txtjenispinjaman.Text == "3C")
                {
                    haritagih = DateTime.Parse(txttgltagihmanual.Text).ToString("dddd");
                }
                else
                {
                    haritagih = DateTime.Parse(txttgltagihmanual.Text).ToString("dd");
                }

                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[9];
                Paramsc[0] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                Paramsc[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramsc[3] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorek.Text, typeof(string));
                Paramsc[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATETGLTAGIHNEW1", typeof(string));
                Paramsc[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                Paramsc[6] = gb.CreateSPParameter("@majuhari", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlangsuranke.SelectedValue), typeof(int));
                Paramsc[7] = gb.CreateSPParameter("@tgl_tagih", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttgltagihmanual.Text, typeof(DateTime));
                Paramsc[8] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, haritagih, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_CETAKAN", Paramsc, "constrmf");


                BindGridEntity();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Update Tanggal Tagih Done');", true);
          
                ddltypepilih.SelectedValue = "ALL";
                ddltypepilih_SelectedIndexChanged(sender, e);
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    [WebMethod]
    public static string GetNoNasabah(string InputText, string SessionBranchID)
    {
        List<string[]> listResult = new List<string[]>();

        string no_nasabah = InputText;

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessionBranchID, typeof(String));
        Params[1] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah.Trim(), typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_GetNoRekeningForKartu", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;
    }
}