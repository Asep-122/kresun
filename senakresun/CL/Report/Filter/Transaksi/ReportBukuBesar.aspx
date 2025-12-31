<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportBukuBesar.aspx.cs" Inherits="GL_Report_CL_ReportBukuBesar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Report Transaction</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!-- App css -->
    <link href="../../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../../../assets/js/modernizr.min.js"></script>






    <!-- Plugins css-->
    <link href="../../../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="../../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../../../assets/js/modernizr.min.js"></script>

    <link href="../../../../assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="../../../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="../../../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../../../assets/js/popper.min.js"></script>
    <script src="../../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../../assets/js/waves.js"></script>
    <script src="../../../../assets/js/jquery.slimscroll.js"></script>

    <script src="../../../../Javascript/jquery-ui.js"></script>






    <style type="text/css">
        .form-control {
        }
    </style>

    <script type="text/javascript">

        $(document).ready(InIEvent);
        function InIEvent() {
            $("#ddlCurrency").change(function () {
                //Todo: write your javascript code here.
                 //$.ajax({
                 //       type: "POST",
                 //    contentType: "application/json; charset=utf-8",                       
                 //       url: "http://www.adisurya.net/kurs-bca/get",
                 //       //data: "{}",
                 //       crossOrigin: true,
                 //       dataType: "json",
                 //       xhrFields: { withCredentials: true },accept: 'application/json'
          
                 //       success: function (data) {
                 //           obj = JSON.parse(stringToArray(data.d));
                 //           console.log(obj);
                 //           //var found = $.map(obj, function (item) {
                 //           //    return {
                 //           //        label: item[1],
                 //           //        Txtkurs: item[0]
                 //           //    }
                 //           //});

                 //           //$("[id$=Txtkurs]").val('');

                 //           //response(found);
                 //       },

                 //       error: function (XMLHttpRequest, textStatus, errorThrown) {
                 //           alert(textStatus);
                 //       },
                 //       failure: function (response) {
                 //           alert(response.responseText);
                 //       }
                 //   });

        //            $.ajax({
        //                url: "http://www.adisurya.net/kurs-bca/get",

        //    crossOrigin: false,
        //    type: 'GET',
        //    xhrFields: { withCredentials: true },
        //    accept: 'application/jsonp'
        //}).done(function (data) {
        //    alert(data);                
        //}).fail(function (xhr, textStatus, error) {
        //    var title, message;
        //    switch (xhr.status) {
        //        case 403:
        //            title = xhr.responseJSON.errorSummary;
        //            message = 'Please login to your server before running the test.';
        //            break;
        //        default:
        //            title = 'Invalid URL or Cross-Origin Request Blocked';
        //            message = 'You must explictly add this site (' + window.location.origin + ') to the list of allowed websites in your server.';
        //            break;
        //    }
        //});
            });
        };
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
                <div class="container-fluid">

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">General Ledger</a></li>
                                        <li class="breadcrumb-item"><a href="#">Report</a></li>
                                        <li class="breadcrumb-item"><a href="Branch.aspx">Report Buku Besar Cash Ledger</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Report Buku Besar Cash Ledger</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    
                    <asp:Panel ID="Panel1" Visible="true" runat="server">                                              
                                
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
                                        <label for="inputState" class="col-form-label">This Month From</label>
                                         <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>     
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">This Month To</label>
                                        <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>  
                                    </div>
                                </div>
                                <%--<div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">No. Reference from</label>
                                          <asp:DropDownList ID="Ddlreferenceawal" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">No. Reference to</label>
                                         <asp:DropDownList ID="Ddlreferenceakhir" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>                                   
                                </div>--%>
                               <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Bank Code from</label>
                                          <asp:DropDownList ID="ddlaccountawal" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Bank Code to</label>
                                         <asp:DropDownList ID="ddlaccountakhir" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>                                   
                                </div>

                                     <div class="form-group">
                                        <label for="inputCity" class="col-form-label">Teller</label>
                                          <asp:DropDownList ID="ddlteller" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>
                                        <%--<div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Department Code from </label>
                                          <asp:DropDownList ID="ddldprtawal" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Department Code to</label>
                                          <asp:DropDownList ID="ddldprtakhir" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>
                                   
                                </div>--%>                              
                                </div>                                                    
                            </div>
                           </div>

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
                                    <div class="form-group">
                                         <label for="inputpid" class="col-3 col-form-label">Report Type</label>
                                          <asp:DropDownList ID="ddlrpttype" CssClass="form-control"  runat="server">
                                                                    <asp:ListItem Value="B">Un-Posting</asp:ListItem>
                                                                    <asp:ListItem Value="A">Posting</asp:ListItem>
                                                                    
                                          </asp:DropDownList>
                                                
                                    </div>
                                <div class="form-row">
                                    <div class="form-group col-md-5">
                                        <label for="inputCity" class="col-form-label">Currency Code</label>
                                          <asp:DropDownList ID="ddlCurrency" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label for="inputState" class="col-form-label">Kurs</label>
                                        <asp:TextBox ID="Txtkurs" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text"  placeholder="" class="form-control autonumber" AutoPostBack="true" runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hdnkurs" runat="server" />
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">Date Kurs</label>
                                        <asp:TextBox ID="DateKurs" CssClass="form-control" runat="server"  Type="date"></asp:TextBox> 
                                    </div>
                                </div>                                
                                </div>                                                    
                            </div>
                          </div>              
                    </div>
            </asp:Panel>
                    <asp:Panel ID="Panel2" Visible="true" runat="server">
               
                        <div class="form-group">
                        <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Process" OnClick="btnSerach_Click"/>  
                        </div>

                    </asp:Panel>

                </div>
                <!-- end container -->

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
<!-- Parsley js -->
<script type="text/javascript" src="../../../../plugins/parsleyjs/parsley.min.js"></script>

<script src="../../../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<script src="../../../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

<!-- Sweet Alert Js  -->
<script src="../../../../plugins/sweet-alert/sweetalert2.min.js"></script>

<script src="../../../../assets/js/jquery.min.js"></script>
<script src="../../../../assets/js/popper.min.js"></script>
<script src="../../../../assets/js/bootstrap.min.js"></script>
<script src="../../../../assets/js/waves.js"></script>
<script src="../../../../assets/js/jquery.slimscroll.js"></script>

<!-- App js -->
<script src="../../../../assets/js/jquery.core.js"></script>
<script src="../../../../assets/js/jquery.app.js"></script>


<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>

<%--<script type="text/javascript">

        //  $(document).ready(InIEvent);
        //function InIEvent() {

        $(document).ready(function () {

            // Default Datatable
            $('#datatable').DataTable();

            //Buttons examples
            var table = $('#GRData').DataTable({
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf']
            });
            //var table = $('#GRData').DataTable({
            //    lengthChange: false,
            //    buttons: ['copy', 'excel', 'pdf']
            //});
            // Key Tables

            $('#key-table').DataTable({
                keys: true
            });

            // Responsive Datatable
            $('#responsive-datatable').DataTable();

            // Multi Selection Datatable
            $('#selection-datatable').DataTable({
                select: {
                    style: 'multi'
                }
            });
            table.buttons().container()
                .appendTo('#GRData_wrapper .col-md-6:eq(0)');

        });
</script>--%>
</html>










