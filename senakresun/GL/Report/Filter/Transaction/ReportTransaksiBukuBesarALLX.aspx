<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportTransaksiBukuBesarALLX.aspx.cs" Inherits="GL_Report_ReportTransaksiBukuBesarALLX" %>

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
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/select.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <link href="/plugins/bootstrap-tagsinput/css/bootstrap-tagsinput.css" rel="stylesheet" />
    <link href="/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/plugins/switchery/switchery.min.css" />

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






    <style type="text/css">
        .form-control {
        }
    </style>

    
    <script type="text/javascript">

        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }

        function SweetAlert(errMsg) {

            setTimeout(function () {

                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).then(function () {
                }).catch(swal.noop)
            }, 100);

        }

        $(document).ready(InIEvent);
        function InIEvent() {
            //(
            //    function () {

            //    });
            $('#datatable').DataTable();

            //Buttons examples
            //var table = $('#Grdtagihank').DataTable({
            //    lengthChange: false,
            //    buttons: ['copy', 'excel', 'pdf']
            //});
            var tes1 = $('#GRData').DataTable({
                "order": [],
                "paging": false,
                buttons: ['copy', 'excel', 'pdf'],
                screenX: true,
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
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

            tes1.buttons().container().appendTo('#GRData_wrapper .col-md-6:eq(0)');
            table.buttons().container().appendTo('#GRData_wrapper .col-md-6:eq(0)');

            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");


            $('#LnkAdd, #lnkCari, #lnkSaveGR,#btnsearch').tooltipster({
                animation: 'grow'
            });
            $('#LnkAdd, #lnkCari, #lnkSaveGR,#btncari').tooltipster({
                animation: 'grow'
            });
            $('.selectpicker').selectpicker();
            document.onclick = function (event) {
                // Compensate for IE<9's non-standard event model
                //
                if (event === undefined) event = window.event;
                var target = 'target' in event ? event.target : event.srcElement;
                // alert(event.clientY);
                if (target.getAttribute('tabindex') == '0') {
                    window.top.cekResize();

                    var x = event.clientX, y = event.clientY;
                    var papaWindow = parent.window;
                    setTimeout(function (e) {
                        papaWindow.scrollBy(0, y / 1.25);
                    }, 50);

                }
            };
            $('#txtTglL').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('#txtTglLto').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $(function () {
                var table = $('#GRData').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });
            });

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();

            //table.buttons().container()
            //    .appendTo('#GRDataRekap .col-md-6:eq(0)');
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
                                        <li class="breadcrumb-item"><a href="#">General Ledger</a></li>
                                        <li class="breadcrumb-item"><a href="#">Report</a></li>
                                        <li class="breadcrumb-item"><a href="Branch.aspx">Report Buku Besar Rekap</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Report Buku Besar Rekap</h4>
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
                                                <asp:TextBox ID="txtStart" CssClass="form-control" runat="server" Type="date" AutoPostBack="true" OnTextChanged="ddltgl_SelectedIndexChanged"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label for="inputZip" class="col-form-label">This Month To</label>
                                                <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server" Type="date" AutoPostBack="true" OnTextChanged="ddltgl_SelectedIndexChanged"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="inputState" class="col-form-label">Kode Account</label>
                                            <asp:DropDownList ID="ddlaccountakhir" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="form-row" runat="server" visible="false">
                                            <div class="form-group col-md-6">
                                                <label for="inputCity" class="col-form-label">Type Journal</label>
                                                <asp:DropDownList ID="ddltypejournalawal" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddltypejournalakhir" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlaccountawal" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlsourceawal" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlsourceakhir" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlcostcenterawal" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlcostcenterakhir" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <label for="inputCity" class="col-form-label">User Entry</label>
                                                <asp:DropDownList ID="ddlteller" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6" runat="server" visible="false">
                                <div class="col-lg-12">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Setting</div>
                                        <p class="text-muted font-14 m-b-30"></p>
                                        <div class="form-group">
                                            <label for="inputpid" class="col-form-label">Report Periode</label>
                                            <asp:TextBox ID="Txtrptperiode" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtrpttitle" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Report Type</label>
                                            <asp:DropDownList ID="ddlrpttype" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                <asp:ListItem Value="A">Posting</asp:ListItem>
                                                <asp:ListItem Value="B">Un-Posting</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" Visible="true" runat="server">

                        <div class="form-group">
                            <div class="button-list">
                                <asp:Button ID="btnsearchdata" class="btn btn-custom waves-light text-white" runat="server" Text="Search" OnClick="btnsearchdata_Click" />
                                <asp:Button ID="btnSearch" Visible="false" class="btn btn-custom waves-light text-white" runat="server" Text="Report" OnClick="btnSerach_Click" />

                            </div>
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Transaksi Buku Besar Rekap</h4>
                                    
                                    
                                    <div class="form-row">
                                        <div class="form-group" style="width: 95%;">

                                            <asp:GridView ID="GRData" runat="server" Visible="false" CssClass="table tes1 table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Kode Cabang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKodecabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_cabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Cabang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Saldo Awal">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldebit" CssClass="" runat="server" Text='<%#Eval("saldoawal","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>

                                                            <span style="display: block; padding: 0; text-align: right">
                                                                <asp:Label ID="lbldebit" Font-Bold="true" runat="server" Text='0' />
                                                            </span>
                                                        </FooterTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Mutasi Debit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDebit" CssClass="" runat="server" Text='<%#Eval("mutasidebit","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <span style="display: block; padding: 0; text-align: right">
                                                                <asp:Label ID="lblkredit" Font-Bold="true" runat="server" />
                                                            </span>

                                                        </FooterTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Mutasi Kredit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkredit" CssClass="" runat="server" Text='<%#Eval("mutasikredit","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <span style="display: block; padding: 0; text-align: right">
                                                                <asp:Label ID="lblkredit" Font-Bold="true" runat="server" />
                                                            </span>

                                                        </FooterTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>
                                            
                                                    <asp:TemplateField HeaderText="Saldo Akhir">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkredit" CssClass="" runat="server" Text='<%#Eval("saldoX","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <span style="display: block; padding: 0; text-align: right">
                                                                <asp:Label ID="lblsaldoakhir" Font-Bold="true" runat="server" />
                                                            </span>

                                                        </FooterTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>

                                                  <%--  <asp:TemplateField HeaderText="Tgl Entry">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglentrydate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglEntry","{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="User Entry ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluserentry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodeuser")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>--%>





                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </asp:Panel>


                </div>
                <!-- end container -->

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
                <%--<asp:PostBackTrigger ControlID="btndata" />--%>
                <asp:PostBackTrigger ControlID="btnsearchdata" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
<!-- App js -->
<script src="/assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="/plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>
<script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<!-- Tooltipster js -->
<script src="/plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables/responsive.bootstrap4.min.js"></script>

<script src="/plugins/tablesaw/js/tablesaw.js"></script>
<script src="/plugins/tablesaw/js/tablesaw-init.js"></script>
<script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<!-- Buttons examples -->
<script src="/plugins/datatables/dataTables.buttons.min.js"></script>
<script src="/plugins/datatables/buttons.bootstrap4.min.js"></script>
<script src="/plugins/datatables/jszip.min.js"></script>
<script src="/plugins/datatables/pdfmake.min.js"></script>
<script src="/plugins/datatables/vfs_fonts.js"></script>
<script src="/plugins/datatables/buttons.html5.min.js"></script>
<script src="/plugins/datatables/buttons.print.min.js"></script>
<!-- Key Tables -->
<script src="/plugins/datatables/dataTables.keyTable.min.js"></script>
<script src="/assets/js/jquery.core.js"></script>
<!-- plugin js -->
<script src="/plugins/moment/moment.js"></script>
</html>










