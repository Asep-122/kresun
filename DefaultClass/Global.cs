using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EntitiesClass;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.OleDb; 
using System.Globalization;
using System.Text.RegularExpressions;

namespace DefaultClass
{
    public class Global
    {
        private SqlTransaction ST;
        private SqlConnection SCon;
        private SqlCommand SCom;
        private SqlDataAdapter SAd;

        GlobalEntities ge = new GlobalEntities();

        public DataSet GetDataFunction(string SPName, string Akses)
        {

            ge.ListDataTable = new DataTable("dtOutput");
            ge.ListDataset = new DataSet();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Akses].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SPName, con))
                {

                    con.Open();
                    SqlCommand cmds = new SqlCommand("select * from " + SPName + "", con);

                    cmds.ExecuteScalar();
                    
                    //   cmd.ExecuteNonQuery();

                    SqlDataAdapter da = new SqlDataAdapter(cmds);
                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ge.ListDataset);

                    DataRow dr = ge.ListDataTable.NewRow();

                    foreach (DataColumn column in ge.ListDataTable.Columns)
                    {

                        dr[column.ColumnName] = cmd.Parameters["@" + column.ColumnName].Value;

                    }


                    ge.ListDataTable.Rows.Add(dr);


                    con.Close();


                    ge.ListDataset.Tables.Add(ge.ListDataTable);
                }
            }

            return ge.ListDataset;

        }
        string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
        string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
        string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
        string[] ribuan = new string[5] { "", "ribu", "juta", "milyar", "triliyun" };

        public string TerbilangAngka(int x)
        {

            string[] bilangan = { "", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan", "sepuluh", "sebelas" };
            string temp = "";

            if (x < 12)
            {
                temp = " " + bilangan[x];
            }
            else if (x < 20)
            {
                temp = Terbilang(x - 10).ToString() + " belas";
            }
            else if (x < 100)
            {
                temp = Terbilang(x / 10) + " puluh" + Terbilang(x % 10);
            }
            else if (x < 200)
            {
                temp = " seratus" + Terbilang(x - 100);
            }
            else if (x < 1000)
            {
                temp = Terbilang(x / 100) + " ratus" + Terbilang(x % 100);
            }
            else if (x < 2000)
            {
                temp = " seribu" + Terbilang(x - 1000);
            }
            else if (x < 1000000)
            {
                temp = Terbilang(x / 1000) + " ribu" + Terbilang(x % 1000);
            }
            else if (x < 1000000000)
            {
                temp = Terbilang(x / 1000000) + " juta" + Terbilang(x % 1000000);
            }
            return temp;
        }
        public String Terbilang(Decimal d)
        {
            string strHasil = "";
            Decimal frac = d - Decimal.Truncate(d);
           
            int xDigit = 0;
            int xPosisi = 0;

            string strTemp = Decimal.Truncate(d).ToString();
            for (int i = strTemp.Length; i > 0; i--)
            {
                string tmpx = "";
                xDigit = Convert.ToInt32(strTemp.Substring(i - 1, 1));
                xPosisi = (strTemp.Length - i) + 1;
                switch (xPosisi % 3)
                {
                    case 1:
                        bool allNull = false;
                        if (i == 1)
                            tmpx = satuan[xDigit] + " ";
                        else if (strTemp.Substring(i - 2, 1) == "1")
                            tmpx = belasan[xDigit] + " ";
                        else if (xDigit > 0)
                            tmpx = satuan[xDigit] + " ";
                        else
                        {
                            allNull = true;
                            if (i > 1)
                                if (strTemp.Substring(i - 2, 1) != "0")
                                    allNull = false;
                            if (i > 2)
                                if (strTemp.Substring(i - 3, 1) != "0")
                                    allNull = false;
                            tmpx = "";
                        }

                        if ((!allNull) && (xPosisi > 1))
                            if ((strTemp.Length == 4) && (strTemp.Substring(0, 1) == "1"))
                                tmpx = "se" + ribuan[(int)Decimal.Round(xPosisi / 3m)] + " ";
                            else
                                tmpx = tmpx + ribuan[(int)Decimal.Round(xPosisi / 3)] + " ";
                        strHasil = tmpx + strHasil;
                        break;
                    case 2:
                        if (xDigit > 0)
                            strHasil = puluhan[xDigit] + " " + strHasil;
                        break;
                    case 0:
                        if (xDigit > 0)
                            if (xDigit == 1)
                                strHasil = "seratus " + strHasil;
                            else
                                strHasil = satuan[xDigit] + " ratus " + strHasil;
                        break;
                }
            }
            strHasil = strHasil.Trim().ToLower();
            if (strHasil.Length > 0)
            {
                strHasil = strHasil.Substring(0, 1).ToUpper() +
                  strHasil.Substring(1, strHasil.Length - 1);
            }
            return strHasil;
        }
        
        public String ToRupiah(String Angka)
        {
            string strHasil = "";

            CultureInfo culture = new CultureInfo("id-ID");
            Angka = Decimal.Parse(Angka).ToString("C", culture);

            strHasil = Angka;

            return strHasil;

        }


        public String HitungTahunUsia(String tanggallahir)
        {
            string strHasil = ""; 
            DateTime dateOfBirth;
            DateTime.TryParse(tanggallahir, out dateOfBirth);
            DateTime currentDate = DateTime.Now;

            TimeSpan difference = currentDate.Subtract(dateOfBirth);

            // This is to convert the timespan to datetime object
            DateTime age = DateTime.MinValue + difference;

            // Min value is 01/01/0001
            // Actual age is say 24 yrs, 9 months and 3 days represented as timespan
            // Min Valye + actual age = 25 yrs , 10 months and 4 days.
            // subtract our addition or 1 on all components to get the actual date.

            int ageInYears = age.Year - 1;
            int ageInMonths = age.Month - 1;
            int ageInDays = age.Day - 1;

            strHasil = ageInYears.ToString();


            return strHasil;

        }


        public string GetAdmProvisi(Double bunga, int jangka, double plafond)
        {
            double admprovisi = 0;
            string hasil = "";

            admprovisi = Math.Round((plafond + (plafond * (bunga / 1200) * jangka)) / jangka);

            hasil =  admprovisi.ToString();

            return hasil;


        }


        public string GetNominalBunga(Double bunga, int jangka, double plafond, String kode_produk)
        {

            double NominalBunga = 0;
            string hasil = "";

            if (kode_produk == "3B")
            { 

                NominalBunga = Math.Round(plafond * jangka * ((bunga / 100) / 12) / jangka);

                hasil = NominalBunga.ToString();


            }
            else if (kode_produk == "3C")
            {

                NominalBunga = Math.Round(plafond * jangka * ((bunga / 100) / jangka) / jangka);

                hasil = NominalBunga.ToString();
                 
            }

            return hasil;

        } 

        public string GetPokok( int jangka, double plafond)
        {
            double pokok = 0;
            string hasil = "";

            pokok = Math.Round(plafond / jangka);

            hasil = pokok.ToString();

            return hasil;


        }
        public DataSet GetDataJustOutPut(string SPName, SqlHelper[] SPParameter, string Akses)
        {

            ge.ListDataTable = new DataTable("dtOutput");
            ge.ListDataset = new DataSet();

            int i;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Akses].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SPName, con))
                {


                    cmd.CommandType = CommandType.StoredProcedure;

                    // set up the parameters


                    for (i = 0; i <= SPParameter.Length - 1; i++)
                    {

                        if (SPParameter[i].ParDir == System.Data.ParameterDirection.Input)
                        {
                            cmd.Parameters.Add(SPParameter[i].ArgumentName, SPParameter[i].SQLDT, SPParameter[i].sz);
                            cmd.Parameters[SPParameter[i].ArgumentName].Value = SPParameter[i].val;
                        }

                        else
                        {
                            cmd.Parameters.Add(SPParameter[i].ArgumentName, SPParameter[i].SQLDT, SPParameter[i].sz).Direction = SPParameter[i].ParDir;

                            ge.ListDataTable.Columns.Add(new DataColumn(SPParameter[i].ArgumentName.Replace("@", ""), SPParameter[i].tof));

                        }

                    }

                    con.Open();
                    //   cmd.ExecuteNonQuery();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ge.ListDataset);

                    DataRow dr = ge.ListDataTable.NewRow();

                    foreach (DataColumn column in ge.ListDataTable.Columns)
                    {

                        dr[column.ColumnName] = cmd.Parameters["@" + column.ColumnName].Value;

                    }


                    ge.ListDataTable.Rows.Add(dr);


                    con.Close();


                    ge.ListDataset.Tables.Add(ge.ListDataTable);
                }
            }

            return ge.ListDataset;

        }
        public String terbilang(string Akses, string angka)
        {
            string SAIDA = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Akses].ConnectionString);
            SqlDataAdapter da2 = new SqlDataAdapter();
           
            con.Open(); 
            SqlCommand Totalf = new SqlCommand("SELECT dbo.terbilang(" + angka + ")", con);
            SqlParameter code1 = new SqlParameter("@code", SqlDbType.Int);

            code1.Value = angka;
            SAIDA = Totalf.ExecuteScalar().ToString();

            return SAIDA;
        }

       
        public struct SqlHelper
        {
            public string ArgumentName;
            public SqlDbType SQLDT;
            public ParameterDirection ParDir;
            public int sz;
            public object val;
            public Type tof;
        }

        public String ReadOrderData()
        {

            String data="";
            string queryString =
                "select GETDATE()";
            using (SqlConnection connection = new SqlConnection(
                       ConfigurationManager.ConnectionStrings["constrmf"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}",reader[0]));
                        data = String.Format("{0}", reader[0]);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return data;
        }
        public SqlHelper CreateSPParameter(string AN, ParameterDirection PD, SqlDbType SDT, int size, object vls, Type tipe)
        {
            SqlHelper ret = default(SqlHelper);
            ret.ArgumentName = AN;
            ret.SQLDT = SDT;
            ret.ParDir = PD;
            ret.sz = size;
            ret.val = vls;
            ret.tof = tipe;
            return ret;
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
        public void SaveUserLog(string errorTexts, string fileErrors, string fromUsers, string fileName)
        {
            XmlDocument xmlEmloyeeDoc = new XmlDocument();
            xmlEmloyeeDoc.Load(fileName);
            XmlElement ParentElement = xmlEmloyeeDoc.CreateElement("UserLog");

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

        public float Rate_fn(float bunga, float PMTBUNGA)
        {
            string SAIDA = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constrmf"].ConnectionString);
            SqlDataAdapter da2 = new SqlDataAdapter();

            con.Open();
            string pmt = PMTBUNGA.ToString(CultureInfo.CreateSpecificCulture("en-US"));
            SqlCommand Totalf = new SqlCommand("SELECT dbo.Rate_fn(" + bunga + "," + pmt + "," + -1000 + "," + 0 + "," + 0 + ")", con);
            SqlParameter code1 = new SqlParameter("@code", SqlDbType.Int);

            code1.Value = bunga;
            SAIDA = Totalf.ExecuteScalar().ToString();

            return float.Parse(SAIDA);
        }
        public decimal HitungUmur(string tanggallahir)
        {
            string SAID = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constrmf"].ConnectionString);
            SqlDataAdapter da2 = new SqlDataAdapter();

            con.Open();
            SqlCommand Totalf = new SqlCommand("SELECT dbo.HitungUsia('" + tanggallahir + "')", con);
            SqlParameter code1 = new SqlParameter("@tanggallahir", SqlDbType.Int);


            code1.Value = tanggallahir;
            SAID = Totalf.ExecuteScalar().ToString();

            return decimal.Parse(SAID);
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


        public void FillDDL(DataTable dt, DropDownList ddl, string DefaultText, string DefaultValue)
        {

            DataRow dsr = null;

            if (DefaultText.Trim().ToUpper() != "NONE")
            {
                dsr = dt.NewRow();
                dsr["Value"] = DefaultValue;
                dsr["Text"] = DefaultText;
                dt.Rows.InsertAt(dsr, 0);
            }

            ddl.DataSource = dt;
            ddl.DataValueField = "Value";
            ddl.DataTextField = "Text";
            ddl.DataBind();

        }


        public DataTable ErroLogTemplate()
        {

            DataTable dt = new DataTable("ErrorLog");

            dt.Columns.Add(new DataColumn("ID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("ErrorText", typeof(string)));
            dt.Columns.Add(new DataColumn("FileError", typeof(string)));
            dt.Columns.Add(new DataColumn("FromUser", typeof(string)));
            dt.Columns.Add(new DataColumn("TglError", typeof(DateTime)));

            return dt;
        }

        public DataTable ExportExceltodataset(string fileLocation, string fileExtension)
        {

            string connectionString = "";

            string full = System.IO.Path.GetFullPath(fileLocation);
            string file = System.IO.Path.GetFileName(full);
            string dir = System.IO.Path.GetDirectoryName(full);

            if (fileExtension == ".xls")
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            }
            else if (fileExtension == ".xlsx")
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            else if (fileExtension == ".csv")
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"" + dir + "\\\";Extended Properties=\"text;HDR=YES;FMT=Delimited\"";
            }

            //Create OleDB Connection and OleDb Command

            OleDbConnection con = new OleDbConnection(connectionString);
            DataTable dtExcelRecords = new DataTable();
            if (fileExtension != ".csv")
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                con.Open();
                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtExcelRecords);
                con.Close();
                // ds.Tables.Add(dtExcelRecords);
            }
            else
            {
                string query = "SELECT * FROM [" + file + "]";
                //create an OleDbDataAdapter to execute the query
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, con);
                dAdapter.Fill(dtExcelRecords);
                dAdapter.Dispose();
                //  ds.Tables.Add(dtExcelRecords);
            }

            return dtExcelRecords;
        }

        public List<string[]> ConvertTable(DataTable dataTable)
        {
            return dataTable.Rows.Cast<DataRow>()
                .Select(row => dataTable.Columns.Cast<DataColumn>()
                   .Select(col => Convert.ToString(row[col]))
                .ToArray())
             .ToList();
        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }



        public string username
        {
            get
            {
                _username = ConfigurationManager.AppSettings["report.username"].ToString();
                return _username;
            }
        }

        public string password
        {
            get
            {
                _password = ConfigurationManager.AppSettings["report.password"].ToString();
                return _password;
            }
        }

        public string domain
        {
            get
            {
                _domain = ConfigurationManager.AppSettings["report.domain"].ToString();
                return _domain;
            }
        }

        public string url
        {
            get
            {
                _url = ConfigurationManager.AppSettings["report.url"].ToString();
                return _url;
            }
        }

        public string _username { get; set; }

        public string _password { get; set; }

        public string _domain { get; set; }

        public string _url { get; set; }
        public DataSet ListDataset { get; set; }

        public struct StoreProcArgument
        {
            public string ArgumentName;
            public SqlDbType SQLDT;
            public ParameterDirection ParDir;
            public Object value;
        }

        public StoreProcArgument CreateSPParameterBeginTran(string AN, ParameterDirection PD, SqlDbType SDT, object val)
        {
            StoreProcArgument ret = default(StoreProcArgument);
            ret.ArgumentName = AN;
            ret.SQLDT = SDT;
            ret.ParDir = PD;
            ret.value = val;
            return ret;

        }

        public void BeginTran(string Akses)
        {

            SCon = new SqlConnection(ConfigurationManager.ConnectionStrings[Akses].ConnectionString);

            SCon.Open();

            SCom = new SqlCommand();

            SCom.CommandType = CommandType.StoredProcedure;
            SCom.Connection = SCon;

            ST = SCon.BeginTransaction();

            SCom.Transaction = ST;
            SCom.Parameters.Clear();
            SCom.CommandType = CommandType.StoredProcedure;

        }

        public void CommitTran()
        {
            ST.Commit();
            SCon.Close();
            SCon.Dispose();
        }

        public void RollBackTran()
        {
            ST.Rollback();
            SCon.Close();
            SCon.Dispose();
        }

        public DataSet GetDataStoreProcedureCommitRollback(string sp, StoreProcArgument[] SPParameters)
        {
            SAd = default(SqlDataAdapter);
            DataSet ds = new DataSet();


            SCom.Parameters.Clear();

            SqlParameter sqlParam = default(SqlParameter);
            for (int j = 0; j <= SPParameters.Length - 1; j++)
            {
                sqlParam = new SqlParameter(SPParameters[j].ArgumentName, SPParameters[j].value);
                sqlParam.Direction = SPParameters[j].ParDir; SCom.Parameters.Add(sqlParam);
            }


            SCom.CommandTimeout = 0;
            SCom.CommandText = sp;
            //com.ExecuteNonQuery();

            SAd = new SqlDataAdapter(SCom);
            SAd.Fill(ds);
            SAd = null;
            return ds;

        }
         
    }
}
