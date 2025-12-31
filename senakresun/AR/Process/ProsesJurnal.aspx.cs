using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AR_Process_ProsesJurnal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlJurnal.Items.Add("Pilih Data ---");
            ddlJurnal.Items.Add("Setoran Tunai Keanggotaan");
            ddlJurnal.Items.Add("Tutup Keanggotaan");


        }

        pnlSetoran.Visible = false;
        pnlFilterTutupAnggota.Visible = false;
        pnlTutupAnggota.Visible = false;
        pnlGridTutupAnggota.Visible = false;
        pnlFilter.Visible = true;
    }

    protected void ddlJurnal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJurnal.SelectedIndex == 1 )
        {
            pnlSetoran.Visible = true;
            pnlFilterTutupAnggota.Visible = false;
            pnlTutupAnggota.Visible = false;
            pnlGridTutupAnggota.Visible = false;
        }
        else if (ddlJurnal.SelectedIndex == 2)
        { 
            pnlSetoran.Visible = false;
            pnlFilterTutupAnggota.Visible = true;
            pnlTutupAnggota.Visible = true;
            pnlGridTutupAnggota.Visible = true;
        }
        else
        {
            
            pnlSetoran.Visible = false;
            pnlFilterTutupAnggota.Visible = false;
            pnlTutupAnggota.Visible = false;
            pnlGridTutupAnggota.Visible = false;
        }

    }
}