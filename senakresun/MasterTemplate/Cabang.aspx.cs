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

public partial class AR_Master_Cabang : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string tanggal = "";
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


    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("Cabang.aspx");
    }
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Cabang.aspx");
    //}


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cabang.aspx");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(SessLoginID == null)
            {
                Response.Redirect("/Login.aspx");
            } 
            else if (!Page.IsPostBack)
            {
                aktifadd();
                DateTime now = DateTime.Now;
                int year = DateTime.Now.Year;
                for (int i = year - 100; i <= year + 100; i++)
                {
                    ListItem li = new ListItem(i.ToString());
                    ddlfiskaltahunini.Items.Add(li);
                    // txtEnd.Items.Add(li);
                }
                ddlfiskaltahunini.Items.FindByText(year.ToString()).Selected = true;
                int bulan = Convert.ToInt32(now.ToString("MM"));
                ddlfiskalbulanini.SelectedValue = Convert.ToString( bulan);
                //aturbitddl
                //string nyoba = Convert.ToString(Convert.ToByte(false));
                ddlfiskaltahunini_change(ddlfiskaltahunini.SelectedValue);
                ddlfiskalbulanini_change(ddlfiskalbulanini.SelectedValue);
            }
            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void ddlfiskalbulanini_change(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        string ATahun = now.ToString("yyyy");
        string ABulan = ddlfiskalbulanini.SelectedValue;
        string AHari = now.ToString("dd");
        int rubahtahun = Int32.Parse(ATahun);
        int rubahbulan = Int32.Parse(ABulan);
        int rubahhariawal = Int32.Parse(AHari);
        int ambilawalhari = Int32.Parse("01");
        DateTime.DaysInMonth(rubahtahun, rubahbulan);
        String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
        string rubahtanggalakhir = ATahun + "/" + ABulan + "/" + rubahhari;
        string rubahtanggalawal = ATahun + "/" + ABulan + "/" + "01";
        txttanggalawalbi.Text = rubahtanggalawal;
        //DateTime rubahawal = Convert.ToDateTime(txttanggalawalbulan.Text);
        int tambahbulan = rubahbulan + 1;
        txttanggalakhirbi.Text = rubahtanggalakhir;
    }
    public void ddlfiskalbulanini_change(string ddlbullan)
    {
        DateTime now = DateTime.Now;
        string ATahun = now.ToString("yyyy");
        string ABulan = ddlbullan;
        string AHari = now.ToString("dd");
        int rubahtahun = Int32.Parse(ATahun);
        int rubahbulan = Int32.Parse(ABulan);
        int rubahhariawal = Int32.Parse(AHari);
        int ambilawalhari = Int32.Parse("01");
        DateTime.DaysInMonth(rubahtahun, rubahbulan);
        String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
        string rubahtanggalakhir = ATahun + "/" + ABulan + "/" + rubahhari;
        string rubahtanggalawal = ATahun + "/" + ABulan + "/" + "01";
        txttanggalawalbi.Text = rubahtanggalawal;
        //DateTime rubahawal = Convert.ToDateTime(txttanggalawalbulan.Text);
        int tambahbulan = rubahbulan + 1;
        txttanggalakhirbi.Text = rubahtanggalakhir;
    }
    public void ddlfiskaltahunini_change(string ddltahun)
    {
        DateTime now = DateTime.Now;
        string ATahun = ddltahun;
        string ABulan = now.ToString("01");
        string AHari = now.ToString("dd");
        int rubahtahun = Int32.Parse(ATahun);
        int rubahbulan = Int32.Parse(ABulan);
        int rubahhariawal = Int32.Parse(AHari);
        int ambilawalbulan = Int32.Parse("01");
        int ambilaakhirbulan = Int32.Parse("12");
        DateTime.DaysInMonth(rubahtahun, rubahbulan);
        String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
        String rubahhariakhir = DateTime.DaysInMonth(rubahtahun, ambilaakhirbulan).ToString();
        string rubahbulanawal = ATahun + "/" + ABulan + "/" + "01";
        string rubahbulanakhir = ATahun + "/" + ambilaakhirbulan + "/" + rubahhariakhir;
        txttanggalawalfiskal.Text = rubahbulanawal;
        //DateTime rubahawal = Convert.ToDateTime(txttanggalawalbulan.Text);
        int tambahtahun = rubahtahun + 1;
        txttanggalakhirfiskal.Text = rubahbulanakhir;
    }
    public void ddlfiskaltahunini_change(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        string ATahun = ddlfiskaltahunini.SelectedValue;
        string ABulan = now.ToString("01");
        string AHari = now.ToString("dd");
        int rubahtahun = Int32.Parse(ATahun);
        int rubahbulan = Int32.Parse(ABulan);
        int rubahhariawal = Int32.Parse(AHari);
        int ambilawalbulan = Int32.Parse("01");
        int ambilaakhirbulan = Int32.Parse("12");
        DateTime.DaysInMonth(rubahtahun, rubahbulan);
        String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
        String rubahhariakhir = DateTime.DaysInMonth(rubahtahun, ambilaakhirbulan).ToString();
        string rubahbulanawal = ATahun + "/" + ABulan + "/" + "01";
        string rubahbulanakhir = ATahun + "/" + ambilaakhirbulan + "/" + rubahhariakhir;
        txttanggalawalfiskal.Text = rubahbulanawal;
        //DateTime rubahawal = Convert.ToDateTime(txttanggalawalbulan.Text);
        int tambahtahun = rubahtahun + 1;
        txttanggalakhirfiskal.Text = rubahbulanakhir;
    }
    public void BindGridEntity(string kdpersh, string kodecabang)
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        //var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, kodecabang, typeof(string));
        var[0] = gb.CreateSPParameter("@kd_persh", ParameterDirection.Input, SqlDbType.VarChar, 50, kdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBRANCHCABANG", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("GS_Cabang", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void aktifadd()
    {
        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@Id", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        Params[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSESSIONAKTIF", typeof(string));
        Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_COMPANY", Params, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "IsSuperAdmin desc";

        DataTable dt = new DataTable();

        dt = ge.ListDataset.Tables[0].DefaultView.ToTable();
        string a = dt.Rows[0]["IsSuperAdmin"].ToString();
        if (a == "True")
        {
            string b = null;
            pnlHeader.Visible = true;
            BindGridEntity(SessKdpersh, b);
        }
        else if ((a != "True"))
        {
            BindGridEntity(SessKdpersh, SessBranchID);
            pnlHeader.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showNotification('bg-red', 'Invalid Password', 'top', 'right', 'animated fadeInRight', 'animated fadeOutRight')", true);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        aturnext();
        //fungsibulan();
        //fungsitahun();
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    public void cekkodecabang(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, txtKodeCabang.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@kd_persh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("GS_Cabang", var, "constrmf").Tables[0];
            if(ge.ListDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Kode Cabang Yang Anda Maukan Sudah ada...!!');", true);
                txtKodeCabang.Text = "";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (txtKodeCabang.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }

            DateTime now = DateTime.Now;
            tanggal = now.ToString("yyyy/MM/dd HH:MM:ss");
            string bulanawal = txttanggalawalbi.Text;
            string bulanakhir = txttanggalakhirbi.Text;
            string tahunawal = txttanggalawalfiskal.Text;
            string tahunakhir = txttanggalakhirfiskal.Text;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[44];
            var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, txtKodeCabang.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@NamaCabang", ParameterDirection.Input, SqlDbType.VarChar, 250, txtNamaCabang.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@NPWP", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNPWP.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@Alamat1", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalamat1.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@Alamat2", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalamat2.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@Alamat3", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalamat3.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[7] = gb.CreateSPParameter("@TglUpdateData", ParameterDirection.Input, SqlDbType.VarChar, 50, tanggal, typeof(string));
            var[8] = gb.CreateSPParameter("@kepalacabang", ParameterDirection.Input, SqlDbType.VarChar, 150, txtkepalacabang.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@kepalawilayah", ParameterDirection.Input, SqlDbType.VarChar, 150, txtkepalawilayah.Text, typeof(string));
            var[10] = gb.CreateSPParameter("@AlamatFakturPajakLama", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfakturpajak.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 10, txtinisial.Text, typeof(string));
            var[12] = gb.CreateSPParameter("@kd_persh", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKdpersh, typeof(string));
            var[13] = gb.CreateSPParameter("@EmailCompany", ParameterDirection.Input, SqlDbType.VarChar, 50, txtemail.Text, typeof(string));
            var[14] = gb.CreateSPParameter("@FiskalBi", ParameterDirection.Input, SqlDbType.SmallInt, 50, ddlfiskalbulanini.SelectedValue, typeof(int));
            var[15] = gb.CreateSPParameter("@SystemKurs", ParameterDirection.Input, SqlDbType.Bit, 0, aturbitddl(ddlsystemkurs), typeof(Boolean));
            var[16] = gb.CreateSPParameter("@FiskalTi", ParameterDirection.Input, SqlDbType.SmallInt, 10, ddlfiskaltahunini.SelectedValue, typeof(int));
            var[17] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.VarChar, 50, tanggal, typeof(string));
            var[18] = gb.CreateSPParameter("@DateModified", ParameterDirection.Input, SqlDbType.VarChar, 50, tanggal, typeof(string));
            var[19] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[20] = gb.CreateSPParameter("@Flag", ParameterDirection.Input, SqlDbType.Bit, 0, chkcabang.Checked, typeof(Boolean));
            var[21] = gb.CreateSPParameter("@ApNamaTtd", ParameterDirection.Input, SqlDbType.VarChar, 50, txtttdAp.Text, typeof(string));
            var[22] = gb.CreateSPParameter("@ApNamaBgn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamabagianAP.Text, typeof(string));
            var[23] = gb.CreateSPParameter("@ApNoFlexible", ParameterDirection.Input, SqlDbType.Bit, 0, chkAp.Checked, typeof(Boolean));
            var[24] = gb.CreateSPParameter("@ClNamaTtd", ParameterDirection.Input, SqlDbType.VarChar, 50, txtttdCl.Text, typeof(string));
            var[25] = gb.CreateSPParameter("@ClNamaBgn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamabagianCl.Text, typeof(string));
            var[26] = gb.CreateSPParameter("@ClNoFlexible", ParameterDirection.Input, SqlDbType.Bit, 0, chkCl.Checked, typeof(Boolean));
            var[27] = gb.CreateSPParameter("@GlNamaTtd", ParameterDirection.Input, SqlDbType.VarChar, 50, txtttd.Text, typeof(string));
            var[28] = gb.CreateSPParameter("@GlNamaBgn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamabagian.Text, typeof(string));
            var[29] = gb.CreateSPParameter("@GlNoFlexible", ParameterDirection.Input, SqlDbType.Bit, 0, chkGl.Checked, typeof(Boolean));
            var[30] = gb.CreateSPParameter("@PostingLsg", ParameterDirection.Input, SqlDbType.Bit, 0, aturbitddl(ddlposting), typeof(Boolean));
            var[31] = gb.CreateSPParameter("@TutupBuku", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltutupbuku.SelectedValue, typeof(string));
            var[32] = gb.CreateSPParameter("@TutupCabang", ParameterDirection.Input, SqlDbType.Bit, 0, aturbitddl(ddltutupcabang), typeof(Boolean));
            var[33] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[34] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[35] = gb.CreateSPParameter("@kodeglbulanberjalan", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodegl.Text, typeof(string));
            var[36] = gb.CreateSPParameter("@TglAwalfiskal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, Convert.ToDateTime(tahunawal), typeof(DateTime));
            var[37] = gb.CreateSPParameter("@TglAkhirfiskal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, Convert.ToDateTime(tahunakhir), typeof(DateTime));
            var[38] = gb.CreateSPParameter("@TglAwalbi", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, Convert.ToDateTime(bulanawal), typeof(DateTime));
            var[39] = gb.CreateSPParameter("@TglAkhirbi", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, Convert.ToDateTime(bulanakhir), typeof(DateTime));
            var[40] = gb.CreateSPParameter("@nikkepalacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnikkepalacabang.Text, typeof(string));
            var[41] = gb.CreateSPParameter("@nikkepalawilayah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnikkepalawilayah.Text, typeof(string));
            var[42] = gb.CreateSPParameter("@NIKJuruTaksir", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnikjurutaksir.Text, typeof(string));
            var[43] = gb.CreateSPParameter("@JuruTaksir", ParameterDirection.Input, SqlDbType.VarChar, 100, txtjurutaksir.Text, typeof(string));

            if (txtKodeCabang.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("GS_Cabang", var, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("GS_Cabang", var, "constrmf");
            }


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);


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

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {
                    
                    string kdcabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, kdcabang, typeof(string));
                    var[1] = gb.CreateSPParameter("@kd_persh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("GS_Cabang", var, "constrmf");

                    //PnlFront.Visible = false;
                    //pnladd.Visible = true;
                    //pnlHeader.Visible = false;
                    string tesmasuk = Convert.ToString(Convert.ToByte(ge.ListDataset.Tables[0].Rows[0]["SystemKurs"]));
                    txtKodeCabang.Enabled = false;
                    txtKodeCabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;
                    txtNamaCabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacabang")).Text;
                    txtNPWP.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNpwp")).Text;
                    txtalamat1.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat1")).Text;
                    txtalamat2.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat2")).Text;
                    txtalamat3.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat3")).Text;
                    txtkepalacabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkepalacabang")).Text;
                    txtkepalawilayah.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkepalawilayah")).Text;
                    txtfakturpajak.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatFakturPajakLama"].ToString();
                    txtinisial.Text = ge.ListDataset.Tables[0].Rows[0]["inisial"].ToString();
                    txtemail.Text = ge.ListDataset.Tables[0].Rows[0]["EmailCompany"].ToString();
                    chkcabang.Checked = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["Flag"].ToString());
                    string masukbulan = ge.ListDataset.Tables[0].Rows[0]["FiskalBi"].ToString();
                    ddlfiskalbulanini.SelectedValue = masukbulan;
                    string masuktahun = ge.ListDataset.Tables[0].Rows[0]["FiskalTi"].ToString();
                    ddlfiskaltahunini.SelectedValue = masuktahun;
                    ddlsystemkurs.SelectedValue = Convert.ToString(Convert.ToByte(ge.ListDataset.Tables[0].Rows[0]["SystemKurs"]));
                        //aturbitmasukasda( Convert.ToString(Convert.ToByte(ge.ListDataset.Tables[0].Rows[0]["SystemKurs"])));
                    ddltutupbuku.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["TutupBuku"].ToString();
                    ddltutupcabang.SelectedValue = Convert.ToString(Convert.ToByte(ge.ListDataset.Tables[0].Rows[0]["TutupCabang"]));
                    ddlposting.SelectedValue = Convert.ToString(Convert.ToByte(ge.ListDataset.Tables[0].Rows[0]["PostingLsg"]));
                    txtttdAp.Text = ge.ListDataset.Tables[0].Rows[0]["ApNamaTtd"].ToString();
                    txtnamabagianAP.Text = ge.ListDataset.Tables[0].Rows[0]["ApNamaBgn"].ToString();
                    chkAp.Checked = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["ApNoFlexible"].ToString());
                    txtttdCl.Text = ge.ListDataset.Tables[0].Rows[0]["ClNamaTtd"].ToString();
                    txtnamabagianCl.Text = ge.ListDataset.Tables[0].Rows[0]["ClNamaBgn"].ToString();
                    chkCl.Checked = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["ClNoFlexible"].ToString());
                    txtttd.Text = ge.ListDataset.Tables[0].Rows[0]["GlNamaTtd"].ToString();
                    txtnamabagian.Text = ge.ListDataset.Tables[0].Rows[0]["GlNamaBgn"].ToString();
                    chkGl.Checked = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["GlNoFlexible"].ToString());
                    txtkodegl.Text = ge.ListDataset.Tables[0].Rows[0]["kodeglbulanberjalan"].ToString();

                    txtnikkepalacabang.Text = ge.ListDataset.Tables[0].Rows[0]["NIKManager"].ToString();
                    txtnikkepalawilayah.Text= ge.ListDataset.Tables[0].Rows[0]["NIKKepalaWilayah"].ToString();
                    txtnikjurutaksir.Text =   ge.ListDataset.Tables[0].Rows[0]["NIKJuruTaksir"].ToString();
                    txtjurutaksir.Text =      ge.ListDataset.Tables[0].Rows[0]["JuruTaksir"].ToString();


                    ddlfiskaltahunini_change(ddlfiskaltahunini.SelectedValue);
                    ddlfiskalbulanini_change(ddlfiskalbulanini.SelectedValue);
                    //aturback();
                   
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;
                    aturnext();
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
                    txtKodeCabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, txtKodeCabang.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("GS_Cabang", var1, "constrmf");

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
                //}

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

        }
    }

    //function_Next
    public void aturnext()
    {
        if (pnladd.Visible == false && pnlHeader.Visible == true )
        {
            btnBack.Visible = false;
            btnCancel.Visible = true;
            btnNext.Visible = true;
            btnSimpan.Visible = false;
        }else if(pnladd.Visible == true && pnladd2.Visible == false)
        {
            pnladd.Visible = false;
            pnladd2.Visible = true;
            btnBack.Visible = true;
            btnCancel.Visible = false;
            btnNext.Visible = true;
            btnSimpan.Visible = false;
            
        }
        else if (pnladd2.Visible == true && pnladd3.Visible == false)
        {
            var asd = aturbitddl(ddlposting);

            pnladd2.Visible = false;
            pnladd3.Visible = true;
            btnBack.Visible = true;
            btnCancel.Visible = false;
            btnNext.Visible = false;
            btnSimpan.Visible = true;
        }
    }

    private string aturbitmasukasda(String datamasuk)
    {
        String rubahdatamasuk = "";
        if (datamasuk == "false")
        {
            rubahdatamasuk = "0";
        }
        else if (datamasuk == "true")
        {
            rubahdatamasuk = "1";
        }
        return rubahdatamasuk;
    }
    private bool aturbitddl(DropDownList ambilbit)
    {
        Boolean rubahbitrubahbit = new Boolean();
        if (ambilbit.SelectedValue == "0")
        {
            rubahbitrubahbit = Convert.ToBoolean(false);
        }
        else if (ambilbit.SelectedValue == "1")
        {
            rubahbitrubahbit = Convert.ToBoolean(true);
        }
        return rubahbitrubahbit;
    }
    //public static Boolean TestInput(DropDownList ambilbit)
    //{
    //    Boolean rubahbitrubahbit = new Boolean();
    //    if (ambilbit.SelectedValue == "0")
    //    {
    //        rubahbitrubahbit = Convert.ToBoolean(false);
    //    }
    //    else if (ambilbit.SelectedValue == "1")
    //    {
    //        rubahbitrubahbit = Convert.ToBoolean(true);
    //    }
    //    return rubahbitrubahbit;
    //}
    public void aturback()
    {
        if (pnladd.Visible == false && pnladd2.Visible == true)
        {
            pnladd.Visible = true;
            pnladd2.Visible = false;
            btnBack.Visible = false;
            btnCancel.Visible = true;
            btnNext.Visible = true;
            btnSimpan.Visible = false;
        }
        else if (pnladd2.Visible == false && pnladd3.Visible == true)
        {
            pnladd2.Visible = true;
            pnladd3.Visible = false;
            btnBack.Visible = true;
            btnCancel.Visible = false;
            btnNext.Visible = true;
            btnSimpan.Visible = false;
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        //pnladd.Visible = false;
        aturnext();
        //pnlIdentitas.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        aturback();
    }
}