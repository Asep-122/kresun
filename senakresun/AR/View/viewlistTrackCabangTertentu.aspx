<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewlistTrackCabangTertentu.aspx.cs" Inherits="Report_viewlistTrackCabangTertentu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title>List Track Cabang</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!-- App icon -->
    <link rel="shortcut icon" href="/assets/images/icon.ico" />

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
        .modalPopup {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .xcenter {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 160px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

        .ui-autocomplete {
            max-height: 500px;
            overflow-y: auto;
            /* prevent horizontal scrollbar */
            overflow-x: hidden;
        }
        /* IE 6 doesn't support max-height
   * we use height instead, but this forces the menu to always be this tall
   */
        * html .ui-autocomplete {
            height: 500px;
        }

        .ui-autocomplete-loading {
            background: white url("../assets/images/Cube-1s-30px.gif") right center no-repeat;
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
            $('#datatable').DataTable();

            //Buttons examples
            var tes1 = $('#GRDataList').DataTable({
                "order": [],
                "paging": false,
                buttons: ['copy', 'excel', 'pdf'],
                screenX: true,
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            });
            var table = $('#GRDataRekap').DataTable({
                "order": [],
                "paging": false,
                buttons: ['copy', 'excel', 'pdf'],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }],
                screenX: true
            });

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

            tes1.buttons().container().appendTo('#GRDataList_wrapper .col-md-6:eq(0)');
            table.buttons().container().appendTo('#GRDataRekap_wrapper .col-md-6:eq(0)');

            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd, #lnkCari, #lnkSaveGR,#btnsearch').tooltipster({
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

            //$(function () {
            //    var table = $('#Grdtagihank').dataTable({
            //        "order": [],
            //        "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
            //        { responsivePriority: 2, targets: -1 }],
            //        ordering: [[0, 'asc']],
            //        "scrollX": true,
            //        retrieve: true
            //    });
            //});

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);
            function BeginRequestHandler(sender, args) {
                //Shows the modal popup - the update progress
                //alert("1");
                var popup = $find('<%= modalPopup.ClientID %>');
                if (popup != null) {
                    popup.show();
                }
            }
            function EndRequestHandler(sender, args) {
                //Hide the modal popup - the update progress
                //alert("2");
                var popup = $find('<%= modalPopup.ClientID %>');
                if (popup != null) {
                    popup.hide();
                }
            }
        </script>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <div class="xcenter">
                    <asp:Image ID="Image1" ImageUrl="/assets/images/loadx.gif" AlternateText="Processing" runat="server" Width="136px" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container-fluid">
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                 <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                    <asp:Button ID="btnAdd" Visible="false" class="btn btn-success waves-light text-white" runat="server" Text="Reload Transaction" OnClick="btnAdd_Click" />
                                    
                                </div>
                            </div>
                        </div>
                                <div class="card-box ribbon-box" runat="server" id="datagrid">
                                   <div class="ribbon ribbon-custom">Data List Tracking Cabang</div>
                                    <p class="m-b-0">
                                        <div class="card-box table-responsive">
                                            <div class="form-row">
                                                <div class="form-group" style="width: 95%;">
                                                    
                                                    <asp:GridView ID="GRDataList" runat="server" ShowFooter="True" AllowPaging="false" AllowCustomPaging="true" AllowSorting="false" ShowHeaderWhenEmpty="true" CssClass="table tes1 table-bordered table-striped table-hover js-basic-example dataTable" AutoGenerateColumns="False" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblno1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Tgl System">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltgljth1" CssClass="" runat="server" Text='<%#Eval("tglsystem","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Area">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblao1x" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodearea")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Kode Cabang">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblao1x" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodecabang")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cabang Nama">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblao1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabangnama")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            </asp:TemplateField>
                                                         
                                                          <%--  <asp:TemplateField HeaderText="Kopeerasi">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakoperasi")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Plafon">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblplafon1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                             
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="bakidb">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblplafon1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakidb","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Pinjaman Terbayarkan">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblplafon1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pinjamanTerbayarkan","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Tunggakan">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltunggakan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tunggakan","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Plafon Harian">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblplafonharian" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafonharian","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Pencairan Baru">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpencairanbaru" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pencairanbaru","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Pencairan Lama">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpencairanlama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pencairanlama","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Simpanan Pokok">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSimpananPokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SimpananPokok","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Simpanan Wajib">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSimpananWajib" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SimpananWajib","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Kas Teller">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblKasTeller" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KasTeller","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Kas Head Teller">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblKasHeadTeller" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KasHeadTeller","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Persediaan Materai">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPersediaanMaterai" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PersediaanMaterai","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Kasbon Operational">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblKasbonOperational" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KasbonOperational","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Kasbon Karyawan Bermasalah">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblKasbonKaryawanBermasalah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KasbonKaryawanBermasalah","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Mandiri Tabungan Cabang">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMandiriTabunganCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MandiriTabunganCabang","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pendapatan Provisi Anggota">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanProvisiAnggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanProvisiAnggota","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Pendapatan Jasa Pinjaman Anggota">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanJasaPinjamanAnggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanJasaPinjamanAnggota","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Pendapatan Giro Bank Mandiri Cabang">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanGiroBankMandiriCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanGiroBankMandiriCabang","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Pendapatan Adm Simpanan">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanAdmSimpanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanAdmSimpanan","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Pendapatan Adm Simpanan Berjangka">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanAdmSimpananBerjangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanAdmSimpananBerjangka","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pendapatan Jasa Pelayanan Lainnya Anggota">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanJasaPelayananLainnyaAnggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanJasaPelayananLainnyaAnggota","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Pendapatan Denda Anggota">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanDendaAnggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanDendaAnggota","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pendapatan Biaya Pelunasan Pinjaman">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanBiayaPelunasanPinjaman" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanBiayaPelunasanPinjaman","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Pendapatan Penjualan Jaminan Anggota">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanPenjualanJaminanAnggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanPenjualanJaminanAnggota","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Pendapatan Atas Penjualan Inventaris">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanAtasPenjualanInventaris" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanAtasPenjualanInventaris","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Pendapatan Selisih Kas">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanSelisihKas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanSelisihKas","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Pendapatan Lain Lain Anggota">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanLainLainAnggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanLainLainAnggota","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pendapatan Non Operasional Lainnya">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPendapatanNonOperasionalLainnya" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PendapatanNonOperasionalLainnya","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Discount Bunga Anggota">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiscountBungaAnggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DiscountBungaAnggota","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Angsuran Real">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAngsuranReal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AngsuranReal","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Anggota Bulanan">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAnggotabulanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "anggotabulan","{0:N0}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
            <Triggers>
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


