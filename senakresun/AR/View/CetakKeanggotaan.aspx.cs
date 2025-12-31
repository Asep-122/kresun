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

public partial class AR_Process_CetakKeanggotaan : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global(); 
    GlobalEntities ge = new GlobalEntities();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        txtkodecabang.Text = SessBranchID;
    }

    public void btnSearch_Click(object sender, EventArgs e)
    {
        string[] splitdata = txtnonasabah.Text.Split('-');
        string nonasabah = splitdata[0];
        

        if (txtnonasabah.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah belum di isi');", true);

        }

        else if (txtkodecabang.Text != txtnonasabah.Text.Substring(0, 3))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah yang di input tidak terdapat di cabang Anda');", true);

        }
        else
        {

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCETAKKEANGGOTAAN", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");

            txtnomornasabah.Text  = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
            txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
            txtkelurahan.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
            txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
            txtsimpok.Text = ge.ListDataset.Tables[0].Rows[0]["simpanan_pokok"].ToString().Replace(",", ".");
            txtsimwa.Text =  ge.ListDataset.Tables[0].Rows[0]["simpanan_smk"].ToString().Replace(",",".");
            txtBulan.Text = ge.ListDataset.Tables[0].Rows[0]["lembar_smk"].ToString();
            HDlast_date.Value = ge.ListDataset.Tables[0].Rows[0]["last_date"].ToString();
            HDkepala_cabang.Value = ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString();
            HDNamaCabang.Value =  ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString();
            HDTgl.Value = ge.ListDataset.Tables[0].Rows[0]["tgl"].ToString();
            HDProvinsi.Value = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
            HDKecamatan.Value = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
            HDKabupaten.Value = ge.ListDataset.Tables[0].Rows[0]["kabupaten1"].ToString();
            HDkelurahan.Value = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
            HDKodepos.Value = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
            HDbadanhukum.Value = "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString();
            HDpad.Value = "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString();
            HDalamatkoperasi.Value = ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString();
            HDKoperasi.Value = ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString();
            HDNamaKoperasi.Value = ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString();
            HDizincabang.Value = ge.ListDataset.Tables[0].Rows[0]["izincabang"].ToString();
            HDNNAMACABANGX.Value = ge.ListDataset.Tables[0].Rows[0]["cabang_nama"].ToString();
            HDBagian.Value = ge.ListDataset.Tables[0].Rows[0]["TTKNamaBgn"].ToString();
			HDtelponx.Value = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
			
        }


    }
    

    protected void btnCetakAnggota_Click(object sender, EventArgs e)
    {
		ReportDocument rprt = new ReportDocument();
        try
        {
			//DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
			//var1[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnomornasabah.Text, typeof(string));
			//var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATESTATUSCETAK", typeof(string));
			//var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

			//ge.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", var1, "constrmf");

            rprt.Load(Server.MapPath("~/AR/Report/rptCetakAnggota.rpt")); 
			rprt.SetParameterValue("noAnggota", txtnomornasabah.Text);
			rprt.SetParameterValue("nama", txtnama.Text);
			rprt.SetParameterValue("alamat", txtalamat.Text + " " + HDkelurahan.Value);
            rprt.SetParameterValue("kabupaten", HDKecamatan.Value + "," + HDKabupaten.Value + "," + HDProvinsi.Value + " " + HDKodepos.Value);
             

            rprt.SetParameterValue("pokok", gb.ToRupiah(txtsimpok.Text));
			rprt.SetParameterValue("wajib", gb.ToRupiah(txtsimwa.Text));
			rprt.SetParameterValue("tanggal", HDTgl.Value);
			rprt.SetParameterValue("kepalaPos", HDkepala_cabang.Value);
			rprt.SetParameterValue("cabang", HDNamaCabang.Value.ToUpper());
			rprt.SetParameterValue("nomPokok", gb.Terbilang(decimal.Parse(txtsimpok.Text)));
			rprt.SetParameterValue("nomWajib", gb.Terbilang(decimal.Parse(txtsimwa.Text)));
			rprt.SetParameterValue("nama kantor", HDNamaCabang.Value);

            rprt.SetParameterValue("badanhukum", HDbadanhukum.Value);
            rprt.SetParameterValue("pad", HDpad.Value);
            rprt.SetParameterValue("alamatpusat", HDalamatkoperasi.Value);
            rprt.SetParameterValue("statuskop", ddlstatus.SelectedValue);
            rprt.SetParameterValue("kode_koperasi", HDKoperasi.Value);
            rprt.SetParameterValue("nama_perusahaan", HDNamaKoperasi.Value);
            rprt.SetParameterValue("izin_cabang", HDizincabang.Value);
            rprt.SetParameterValue("alamat2", HDtelponx.Value);
            rprt.SetParameterValue("namacabang", HDNNAMACABANGX.Value);
            rprt.SetParameterValue("bagian", HDBagian.Value);
			
			
            Response.ClearContent();
			Response.ClearHeaders(); 

			Response.ContentType = "application/pdf";
			rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


			Response.End();

			pnlFilter.Visible = true;
			
		}catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            }else
			{
                rprt.Close();
                rprt.Dispose();
                GC.Collect();
            }

        }
    }


    [WebMethod]
    public static string GetNoNasabah(string InputText, string SessionBranchID)
    {
        List<string[]> listResult = new List<string[]>();

        string no_nasabah =  InputText;

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessionBranchID, typeof(String));
        Params[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(String));
        
        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_GetNoNasabahForCetak", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
}