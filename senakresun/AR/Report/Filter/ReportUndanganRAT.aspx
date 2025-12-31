<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportUndanganRAT.aspx.cs" Inherits="AR_Report_ReportUndanganRAT" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Report Daftar Account</title>
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
    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>
    <script src="/Javascript/jquery-ui.js"></script>

    <!-- DataTables -->
    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />






    <style type="text/css">
        .form-control {
        }
    </style>

    <script type="text/javascript">

        $(document).ready(InIEvent);
        function InIEvent() {
            // Default Datatable
            $('#datatable').DataTable();

            //Buttons examples
            var table = $('#GRData').DataTable({
                "order": [], "paging": false,
                buttons: ['copy', 'excel', 'pdf'],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            });



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
            $('.selectpicker').selectpicker();



            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();



        }


        function showSuccessMessage() {
            try {
                swal({
                    title: "Data tidak ditemukan",
                    text: "",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    closeOnConfirm: true,
                    closeOnCancel: false
                }, function (isConfirm) {
                    if (isConfirm) {

                      <%--  setTimeout(function () {<%= Lumpsum() %>; }, 1); --%>

                    }
                });
            }
            catch (err) {
                console.log(err.message);
            }

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
                <div class="container-fluid">

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Account Receivable</a></li>
                                        <li class="breadcrumb-item"><a href="#">Report</a></li>
                                        <li class="breadcrumb-item"><a href="Branch.aspx">Report Undangan RAT</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Report Undangan RAT</h4>
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

                                          <div class="form-group">
                                            <label for="inputState" class="col-form-label">Pilih Report</label>
                                              <asp:DropDownList ID="ddlpilihreport" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="A">Undangan RAT</asp:ListItem>
                                                <asp:ListItem Value="B">Label Undagan</asp:ListItem>
                                              
                                            </asp:DropDownList>
                                        </div>

                                           <div class="form-group">
                                            <label for="inputState" class="col-form-label">Kode AO</label>
                                             <asp:DropDownList ID="ddlmarketing" runat="server" CssClass="form-control" data-style="btn-custom" AutoPostBack="true" OnTextChanged="ddlAO_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>


                                        <div class="form-row">
                                            <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">No Anggota dari</label>
                                                  <asp:DropDownList ID="ddlanggotadari" runat="server" CssClass="form-control" data-style="btn-custom">
                                                    </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label for="inputZip" class="col-form-label">No Anggota sampai dengan</label>
                                                <asp:DropDownList ID="ddlanggotasd" runat="server" CssClass="form-control" data-style="btn-custom">
                                                    </asp:DropDownList>
                                            </div>
                                        </div>

                                        
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="col-lg-12">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Informasi Undangan RAT</div>
                                        <p class="text-muted font-14 m-b-30"></p>
                                        <div class="form-group">
                                            <label for="inputpid" class="col-form-label">No. Undangan</label>
                                           <asp:TextBox ID="txtnoundangan" visible="true" ReadOnly="true"  CssClass="form-control"  runat="server" maxlength ="100" Text="-"></asp:TextBox> 
                                        </div>
                                         <div class="form-group">
                                            <label for="inputpid" class="col-form-label">Tgl Acara</label>
                                           <asp:TextBox ID="txttglundangan" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 
                                        </div>
                                         <div class="form-group">
                                            <label for="inputpid" class="col-form-label">Judul Undangan</label>
                                           <asp:TextBox ID="txtjudulundangan" visible="true" ReadOnly="true"  CssClass="form-control"  runat="server" maxlength ="100" Text="-"></asp:TextBox> 
                                        </div>
                                         <div class="form-group">
                                            <label for="inputpid" class="col-form-label">Waktu Acara</label>
                                           <asp:TextBox ID="txtwaktuacara" visible="true" ReadOnly="true"  CssClass="form-control"  runat="server" maxlength ="100" Text="-"></asp:TextBox> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" Visible="true" runat="server">

                        <div class="form-group">
                            <div class="button-list">
                                
                                <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Report" OnClick="btnSerach_Click" />

                            </div>
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
<%--<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>

<script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

<!-- Sweet Alert Js  -->
<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

<script src="/assets/js/jquery.min.js"></script>
<script src="/assets/js/popper.min.js"></script>
<script src="/assets/js/bootstrap.min.js"></script>
<script src="/assets/js/waves.js"></script>
<script src="/assets/js/jquery.slimscroll.js"></script>

<!-- App js -->
<script src="/assets/js/jquery.core.js"></script>
<script src="/assets/js/jquery.app.js"></script>


<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>
         <script src="/plugins/datatables/dataTables.buttons.min.js"></script>

           <script src="/plugins/datatables/buttons.bootstrap4.min.js"></script>
        <script src="/plugins/datatables/jszip.min.js"></script>
        <script src="/plugins/datatables/pdfmake.min.js"></script>
        <script src="/plugins/datatables/vfs_fonts.js"></script>
        <script src="/plugins/datatables/buttons.html5.min.js"></script>
        <script src="/plugins/datatables/buttons.print.min.js"></script>--%>





<!-- Parsley js -->
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>

<script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>


<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>
<!-- Buttons examples -->
<script src="/plugins/datatables/dataTables.buttons.min.js"></script>
<script src="/plugins/datatables/buttons.bootstrap4.min.js"></script>
<script src="/plugins/datatables/jszip.min.js"></script>
<script src="/plugins/datatables/pdfmake.min.js"></script>
<script src="/plugins/datatables/vfs_fonts.js"></script>
<script src="/plugins/datatables/buttons.html5.min.js"></script>
<script src="/plugins/datatables/buttons.print.min.js"></script>

<!-- App js -->
<script src="/assets/js/jquery.core.js"></script>
<script src="/assets/js/jquery.app.js"></script>
<!-- plugin js -->
<script src="/plugins/moment/moment.js"></script>
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










