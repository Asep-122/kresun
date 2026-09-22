using System.IO;
using System.Data;
//using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Configuration;
using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FA_Master_trying : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ExportExcel(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["constrmf"].ConnectionString;

        string ambildata = "select count(kode_cabang), kode_cabang from tbl_master_perkiraan_bahan_calculate group by kode_cabang;";

        using (SqlConnection con2 = new SqlConnection(constr))
        {
            using (SqlCommand cmd2 = new SqlCommand(ambildata))
            {
                using (SqlDataAdapter sda2 = new SqlDataAdapter())
                {
                    cmd2.Connection = con2;
                    sda2.SelectCommand = cmd2;
                    using (DataSet ds = new DataSet())
                    {

                        sda2.Fill(ds);
                        ds.Tables[0].TableName = "tbl_master_perkiraan_bahan_calculate";

                        foreach (DataTable dt in ds.Tables)
                        {
                            for(int i = 0; i < dt.Rows.Count; i++)
                            {
                                string query = "SELECT *  FROM tbl_master_perkiraan_bahan_calculate where kode_cabang = '" + dt.Rows[i][1] + "';";

                                query += "SELECT * FROM tbl_master_perkiraan_bahan_calculate where kode_cabang = '" + dt.Rows[i + 1][1] + "'; ";
                                using (SqlConnection con = new SqlConnection(constr))
                                {
                                    using (SqlCommand cmd = new SqlCommand(query))
                                    {
                                        using (SqlDataAdapter sda = new SqlDataAdapter())
                                        {
                                            cmd.Connection = con;
                                            sda.SelectCommand = cmd;
                                            using (DataSet ds2 = new DataSet())
                                            {
                                                sda.Fill(ds2);

                                                //Set Name of DataTables.
                                                ds2.Tables[0].TableName = "CABANG 1";
                                                ds2.Tables[1].TableName = "CABANG 2";

                                                //using (XLWorkbook wb = new XLWorkbook())
                                                //{
                                                //    foreach (DataTable dt2 in ds2.Tables)
                                                //    {
                                                //        //Add DataTable as Worksheet.
                                                //        wb.Worksheets.Add(dt2);
                                                //    }

                                                //    //Export the Excel file.
                                                //    Response.Clear();
                                                //    Response.Buffer = true;
                                                //    Response.Charset = "";
                                                //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                                //    Response.AddHeader("content-disposition", "attachment;filename=DataSet.xlsx");
                                                //    using (MemoryStream MyMemoryStream = new MemoryStream())
                                                //    {
                                                //        wb.SaveAs(MyMemoryStream);
                                                //        MyMemoryStream.WriteTo(Response.OutputStream);
                                                //        Response.Flush();
                                                //        Response.End();
                                                //    }
                                                //}
                                            }
                                        }
                                    }
                                }
                                

                            }



                        }

                    }





                }
            }
        }
    }
}