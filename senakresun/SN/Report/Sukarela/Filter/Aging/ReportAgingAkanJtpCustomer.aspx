<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportAgingAkanJtpCustomer.aspx.cs" Inherits="AR_Report_ReportAgingAkanJtpCustomer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
         <title>Report Aging Sukarela</title>
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

        function WarningData(errMsg, dateMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'warning',
                        text: dateMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);

        }
    </script>
    <style type="text/css">
        .form-control {
        }
    </style>

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
                                        <li class="breadcrumb-item"><a href="#">Simpanan Nasabah</a></li>
                                        <li class="breadcrumb-item"><a href="#">Report</a></li>
                                        <li class="breadcrumb-item"><a href="ReportAgingAkanJtpCustomer.aspx">Report Aging Sukarela</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Report Aging Sukarela</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" Visible="true" runat="server">

                        <div class="row">

                            <div class="col-lg-6">
                                <div class="col-lg-12">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Periode</div>
                                        <p class="text-muted font-14 m-b-30"></p>
                                        <div class="form-row">

                                            <div class="form-group col-md-4">
                                                Date Aging Customer
                                         <asp:TextBox ID="txtStart" CssClass="form-control" runat="server" Type="date" OnTextChanged="txtstart_OnChange" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                limit interval
                                        <asp:TextBox ID="txtlimitinternal" CssClass="form-control" runat="server" Type="numeric"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputpid" class="col-form-label">Cabang</label>
                                            <asp:DropDownList ID="ddlcabang" runat="server" CssClass="form-control" data-style="btn-custom"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Report</label>
                                            <asp:DropDownList ID="ddlreport" runat="server" CssClass="form-control" data-style="btn-custom">
                                                <asp:ListItem Value="1">Bunga</asp:ListItem>
                                                <asp:ListItem Value="2">Pajak</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Product</label>
                                            <asp:DropDownList ID="ddlproduct" runat="server" CssClass="form-control" data-style="btn-custom">
                                                <%--<asp:ListItem Value="0">Sukarela Tanpa Pajak</asp:ListItem>--%>
                                                <asp:ListItem Value="1">Sukarela Dengan Pajak</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
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
                                                <asp:DropDownList ID="DdlReportx" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">View Report</asp:ListItem>
                                                    <asp:ListItem Value="2">Export Word</asp:ListItem>
                                                    <asp:ListItem Value="3">Export PDF</asp:ListItem>
                                                    <asp:ListItem Value="4">Export Excel</asp:ListItem>
                                                    <asp:ListItem Value="5">Export CSV</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-8">
                                                <%--<label for="inputState"  class="col-form-label">Report Title</label>--%>
                                                <asp:TextBox ID="txtrpttitle" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Report Type</label>
                                            <asp:DropDownList ID="ddltypereport" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Report Detail</asp:ListItem>
                                                <asp:ListItem Value="2">Report Rekap</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Report Periode</label>
                                            <asp:TextBox ID="Txtrptperiode" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-row">
                                            <div class="form-group col-md-4">
                                                <label for="inputZip" class="col-form-label">Date Kurs</label>
                                                <asp:TextBox ID="DateKurs" CssClass="form-control" runat="server" Type="date"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-5">
                                                <label for="inputCity" class="col-form-label">Currency Code</label>
                                                <asp:DropDownList ID="ddlCurrency" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-3">
                                                <label for="inputState" class="col-form-label">Kurs</label>
                                                <asp:TextBox ID="Txtkurs" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" CssClass="form-control autonumber" AutoPostBack="true" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <asp:Panel ID="Panel1" Visible="true" runat="server">

                        <div class="form-group">
                            <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Process" OnClick="btnSerach_Click" />
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


            <script src="../../../../assets/js/jquery.min.js"></script>
        <script src="../../../../assets/js/popper.min.js"></script>
        <script src="../../../../assets/js/bootstrap.min.js"></script>
        <script src="../../../../assets/js/waves.js"></script>
        <script src="../../../../assets/js/jquery.slimscroll.js"></script>

        <!-- App js -->
        <script src="../../../../assets/js/jquery.core.js"></script>
        <script src="../../../../assets/js/jquery.app.js"></script>

   
    <script src="../../../../plugins/sweet-alert/sweetalert2.min.js"></script>

      <script type="text/javascript">

      //  $(document).ready(InIEvent);
        //function InIEvent() {

          $(document).ready(function() {

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
     
            } );
        </script>

</html>










