<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListTagihandanTunggakan.aspx.cs" Inherits="Mikro_ListTagihandanTunggakan" %> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Laporan List Tagihan dan Tunggakan Marketing</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/assets/js/modernizr.min.js"></script>

    <link href="/assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>

    <script src="/Javascript/jquery-ui.js"></script>

        <!-- Toastr css -->
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />
    
        <!-- Modal -->
    <link href="/plugins/custombox/css/custombox.min.css" rel="stylesheet" />
    
    <!-- Tooltipster css -->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />
        <!-- DataTables -->
    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    
   
   
    <script type="text/javascript">

        function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: t,
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 2000,
                    stack: 1,
                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnreload()%>; }, 50);    //Save dan Edit
                        }

                        if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                        }
                    }
                });
            }, 0);

        }

        function redirect() {
            location.href = 'ReportGaji.aspx ';
        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);

        }
        function WarningAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'warning',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);

        }

        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

        function isnumerickey(e) {
            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {

                if (txt == 118) {
                    return true;
                }
                //  else {
                //alert("Input is Number only")
                return false;
                //  }

            }
            return true;
        }

        function Comma(Num) { //function to add commas to textboxes

            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;


            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');


            return x1 + x2;

        }

        function checkTextField1(field) {
            if (field.value == '' || field.value == 0 || field.value == '.') {
                field.value = 0;
            }


            var nilai = field.value.split(',').join('');
            var output = nilai;
            if (parseFloat(nilai)) {
                input = new String(nilai); // so you can perform string operations
                var parts = input.split("."); // remove the decimal part
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
                output = parts.join(".");
            }
            field.value = output;
        }

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });
            $('.selectpicker').selectpicker();
        }
        $('form').parsley();

    </script>
    <script type="text/javascript">
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9 ]/.test(inp))
                return true;
            alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
	</script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">ACCOUNT EMPLOYEE</a></li>
                                    <li class="breadcrumb-item"><a href="LIstTunggakanMarketing.aspx">Report List Tunggakan Marketing</a></li>
                                </ol>
                            </div>
                            <%--<h4 class="page-title">Master Kode Currency</h4>--%>
                        </div>
                    </div>
                </div>
                 <!-- Panel Edit -->
                 <asp:Panel ID="pnlFilter" Visible="true" runat="server">                                              
                                
                  <div class="row">

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Periode</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                        <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Periode</label>
                                          <asp:DropDownList ID="ddlSP" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">Januari</asp:ListItem>
                                                        <asp:ListItem Value="2">Februari</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>        
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Month</label>
                                         <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>     
                                    </div> 
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Produk</label>
                                          <asp:DropDownList ID="ddlproduk" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div> 
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Cabang</label>
                                          <asp:DropDownList ID="ddlcabang" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div> 
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Marketing</label>
                                          <asp:DropDownList ID="DdlMarketing" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div> 
                                </div>
                               <%-- <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Status</label>
                                          <asp:DropDownList ID="ddlstatus" CssClass="form-control" required="required" runat="server">
                                              <asp:ListItem Text="Belum WO" Value ="0" Selected="True"/>
                                              <asp:ListItem Text="Sudah WO" Value ="1"/>
                                          </asp:DropDownList>
                                    </div> 
                                </div>--%>
                                        <%--<div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Customer Code from</label>
                                          <asp:DropDownList ID="ddlaccountawal" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Customer Code to</label>
                                         <asp:DropDownList ID="ddlaccountakhir" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div>
                                   
                                </div>--%>
                                                                      
                                </div>                                                    
                            </div>
                           </div>

                                      
                    </div> 

                     <div class ="row">
                         <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Setting</div>
                                    <p class="text-muted font-14 m-b-30"></p>                  
                                <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Setting Report</label>
                                                     <asp:DropDownList ID="DdlReport" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">View Report</asp:ListItem>
                                                        <asp:ListItem Value="2">Export Word</asp:ListItem>
                                                        <asp:ListItem Value="3">Export PDF</asp:ListItem>
                                                        <asp:ListItem Value="4">Export Excel</asp:ListItem>
                                                        <asp:ListItem Value="5">Export CSV</asp:ListItem>
                                                </asp:DropDownList> 
                                    </div>
                                    <div class="form-group col-md-8">
                                        <label for="inputState" class="col-form-label">Report Title</label>
                                           <asp:TextBox ID="txtrpttitle" CssClass="form-control" runat="server" ></asp:TextBox> 
                                    </div>
                                   
                                </div>
                                <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Report Periode</label>
                                            <asp:TextBox ID="Txtrptperiode" CssClass="form-control" runat="server"></asp:TextBox>                                                       
                                </div>                             
                                </div>                                                    
                            </div>
                          </div>   
                     </div>
            </asp:Panel>      

                 
             <asp:Panel ID="Panel1" Visible="true" runat="server">     
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row justify-content-center">
                        <div class="btn-group mb-2">
                            <div class="button-list">
                                <asp:Button ID="btnSerach" class="btn btn-success waves-light text-white" runat="server" Text="Proses" OnClick="btnSerach_Click"  OnClientClick="btnSerach_Click();" />
                                &nbsp&nbsp&nbsp&nbsp&nbsp
                                <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="Back" onclientclick='redirect()' /> 
                            </div>
                        </div>
                  </div>
                 </asp:Panel>


            <%--<asp:Panel ID="pnladd" runat="server">
              <div class="row clearfix">
                  <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 center-page"> 
                     <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Data</div>
                         <br />
                               <div class="title text-center mb-3">
                                    <h3>Reporting Month Salary</h3>
                               </div>
                         <br />
                       <div class="form-row">
                         <div class="form-group  col-md-12">
                           <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                 <label  runat="server" class="col-md-4 col-form-label font-weight-bold">Periode</label>
                                   <div class="form-group col-md-4">
                                    <asp:DropDownList ID="ddlSP" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" >
                                            <asp:ListItem Value="1">Januari</asp:ListItem>
                                            <asp:ListItem Value="2">Februari</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">Agustus</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList>  
                                       </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">This Month From</label>
                                         <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>     
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">This Month To</label>
                                        <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>  
                                    </div>

                                </div>
                            </div>
                           <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Produk</label>
                                          <asp:DropDownList ID="ddlproduk" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div> 
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Cabang</label>
                                          <asp:DropDownList ID="ddlcabang" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div> 
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Marketing</label>
                                          <asp:DropDownList ID="DdlMarketing" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div> 
                                </div>

                          </div>
                             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row justify-content-center">
                                <div class="btn-group mb-2">
                                    <div class="button-list">
                                        <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Proses" OnClick="btnSimpan_Click"  OnClientClick="btnSimpan_Click();" />
                                        &nbsp&nbsp&nbsp&nbsp&nbsp
                                        <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="Back" onclientclick='redirect()' /> 
                                    </div>
                                </div>
                          </div>
                           </div>
                        </div>
                         
					   </div>
                       </div>
               
            </asp:Panel>  --%>
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnSerach" />
                <%--<asp:PostBackTrigger ControlID="btnCancel" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  
    
    <!-- Parsley js -->
    <script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>

    <script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <!-- Sweet Alert Js  -->
    <script src="/plugins/sweet-alert/sweetalert2.min.js"></script>
    <!-- Tooltipster js -->
<script src="/plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables/responsive.bootstrap4.min.js"></script>

<!-- App js -->
<script src="/assets/js/jquery.app.js"></script>



<!-- Toastr js -->
<script src="/plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>



</html>





