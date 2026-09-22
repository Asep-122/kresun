<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CetakPK.aspx.cs" Inherits="Kresun_View_CetakPK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Approval Permohonan Gadai</title>
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


                    }
                });
            }, 0);
        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);

        }

        function stringToArray(items) {
            return items.replace(/'/g, '"');
        }

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
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1").split("").reverse().join("");
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

            $('#txttglrealisasi').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('.selectpicker').selectpicker();

            $(function () {
                var table = $('#GRData').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });

            });

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
        }

    </script>
    <style>
        .icon-lg {
            font-size: 32px;
        }
        .icon-md {
            font-size: 24px;
        }
        .icon-sm {
            font-size: 16px;
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
            <contenttemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Kresun</a></li>
                                        <li class="breadcrumb-item"><a href="#">View</a></li>
                                        <li class="breadcrumb-item"><a href="CetakPK.aspx">Cetak Perjanjian Kredit</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Cetak Perjanjian Kredit</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <h4 class="m-t-0 header-title">Filter</h4>
                                    <p class="text-muted font-14 m-b-30">
                                        KeyTable provides Excel like cell navigation on any table. Events (focus, blur, action etc) can be assigned to individual cells, columns, rows or all cells.
                                    </p>


                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box ribbon-box">
                                    <h4 class="m-t-0 header-title">Data Verifikasi</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">

                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="887px" OnRowCommand="GRData_RowCommand">
                                                <columns>
                                                    <asp:TemplateField HeaderText="No Nasabah">
                                                        <itemtemplate>
                                                            <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                                        </itemtemplate>
                                                        <headerstyle height="30px" cssclass="text-center"></headerstyle>
                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Rekening">
                                                        <itemtemplate>
                                                            <asp:Label ID="lblnorekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                        </itemtemplate>
                                                        <headerstyle height="30px" cssclass="text-center"></headerstyle>
                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Nama Nasabah">
                                                        <itemtemplate>
                                                            <asp:Label ID="lblnamanasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                        </itemtemplate>
                                                        <headerstyle height="30px" cssclass="text-center"></headerstyle>
                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Plafon">
                                                        <itemtemplate>
                                                            <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafond")%>'></asp:Label>
                                                        </itemtemplate>
                                                        <headerstyle height="30px" cssclass="text-center"></headerstyle>
                                                        <itemstyle horizontalalign="Right"></itemstyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jangka">
                                                        <itemtemplate>
                                                            <asp:Label ID="lblhp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka")%>'></asp:Label>
                                                        </itemtemplate>

                                                        <headerstyle height="30px" cssclass="text-center"></headerstyle>
                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Pencairan">
                                                        <itemtemplate>
                                                            <asp:Label ID="lbltglpengajuan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tanggal_pengajuan")%>'></asp:Label>
                                                        </itemtemplate>

                                                        <headerstyle height="30px" cssclass="text-center"></headerstyle>
                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Jatuh Tempo">
                                                        <itemtemplate>
                                                            <asp:Label ID="lbltgljatuhtempo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglJatuhTempo")%>'></asp:Label>
                                                        </itemtemplate>
                                                        <headerstyle height="30px" cssclass="text-center"></headerstyle>
                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JenisProduk">
                                                        <itemtemplate>
                                                            <asp:Label ID="lblProduk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jenis_Produk")%>'></asp:Label>
                                                        </itemtemplate>
                                                        <headerstyle height="30px" cssclass="text-center"></headerstyle>
                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SumberDana">
                                                        <itemtemplate>
                                                            <asp:Label ID="lblSumberDana" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "sumberdana")%>'></asp:Label>
                                                        </itemtemplate>
                                                        <headerstyle height="30px" cssclass="text-center"></headerstyle>
                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Print">
                                                        <headerstyle cssclass="align-center" width="10%"></headerstyle>
                                                        <itemstyle horizontalalign="Center"></itemstyle>
                                                        <itemtemplate>
                                                            <%--<asp:LinkButton ID="printPreview" runat="server" data-toggle="tooltip" data-placement="left" title="Print Preview" CommandName='<%#DataBinder.Eval(Container.DataItem, "KantorBayar")%>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="mdi mdi-printer icon-md"></i></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="printPreview" runat="server" data-toggle="tooltip" data-placement="left" title="Print Preview" CommandName="printPreview" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="mdi mdi-printer icon-md"></i></asp:LinkButton>
                                                        </itemtemplate>
                                                    </asp:TemplateField>
                                                </columns>
                                            </asp:GridView>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </asp:Panel>

                    <!-- end wrapper -->
            </contenttemplate>
            <triggers>
                <asp:PostBackTrigger ControlID="GRData" />
            </triggers>
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
