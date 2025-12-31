using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AE_Transaksi_v_TarikBulanan : System.Web.UI.Page
{
    Global gbsearch = new Global();
    GlobalEntities gesearch = new GlobalEntities();
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[1];
        var[0] = gb.CreateSPParameter("@Tanggal", ParameterDirection.Input, SqlDbType.VarChar, 10, txttglentry.Text, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[AR_Tarik_Pusat_Search]", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}