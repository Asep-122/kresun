<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="datamarketingfollowup.aspx.cs" Inherits="Report_datamarketingfollowup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title>FOLLOW UP DATA BAHAN TASPEN/ASABRI</title>
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
    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>

    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>

    <%--<script src="/assets/js/bootstrap.min.js"></script>--%>

    <script src="/Javascript/jquery-ui.js"></script>

    <style type="text/css">
        .overlay {
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
            background: white url("/assets/images/Cube-1s-30px.gif") right center no-repeat;
        }

        .auto-style1 {
            position: relative;
            width: 100%;
            min-height: 1px;
            top: -20px;
            left: 0px;
            -ms-flex: 0 0 50%;
            flex: 0 0 50%;
            max-width: 50%;
            padding-left: 15px;
            padding-right: 15px;
        }



        #globalLoading .overlay {
            position: fixed;
            z-index: 9998;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: black;
            opacity: 0.6;
        }

        #globalLoading .loading-box {
            position: fixed;
            z-index: 9999;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background: white;
            padding: 20px 30px;
            border-radius: 8px;
            text-align: center;
            font-weight: bold;
        }

    </style>


    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>


    <script type="text/javascript"> 

        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
                //document.getElementById("btnkembali"). = false;

            } else {
                //document.getElementById("txtlat").value = "0";
                //document.getElementById("txtlon").value = "0";

                // alert izinkan akses lokasi tidak diberikan.
                // back/reload/ or just continue/return
            }
            //btnkembali.dataset.confirmed = true;
            //btnkembali.click();
        }
        function showPosition(position) {

            document.getElementById('<%= Hdnlat.ClientID %>').value = position.coords.latitude;
            document.getElementById('<%= Hdnlon.ClientID %>').value = position.coords.longitude;
            document.getElementById('<%= txtlat.ClientID %>').innerHTML = position.coords.latitude;
            document.getElementById('<%= txtlon.ClientID %>').innerHTML = position.coords.longitude;

            //document.getElementById("txtlat").value = position.coords.latitude;
            //document.getElementById("txtlon").value = position.coords.longitude;

        }
        function initializex() {
            var lat = document.getElementById('Hdnlat').value;
            var lon = document.getElementById('Hdnlon').value;
            var myLatlng = new google.maps.LatLng(lat, lon) // This is used to center the map to show our markers
            var mapOptions = {
                center: myLatlng,
                zoom: 6,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                marker: true
            };
            var map = new google.maps.Map(document.getElementById("map_canvas1"), mapOptions);
            var marker = new google.maps.Marker({
                position: myLatlng
            });
            marker.setMap(map);
        }
        function initialize() {
            var lat = document.getElementById('TextBox1').value;
            var lon = document.getElementById('TextBox2').value;
            var myLatlng = new google.maps.LatLng(lat, lon) // This is used to center the map to show our markers
            var mapOptions = {
                center: myLatlng,
                zoom: 6,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                marker: true
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
            var marker = new google.maps.Marker({
                position: myLatlng
            });
            marker.setMap(map);
        }
        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
 <%--                 function showProgress() {
var updateProgress = $get("<%= UpdateProgress.ClientID %>");
updateProgress.style.display = "block";
}--%>

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

        function isnumerickey(e) {
            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {

                //if (txt == 118) {
                //    return true;
                //}
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
            $('#datatable').DataTable();

            //Buttons examples
            //var table = $('#Grdtagihan').DataTable({
            //    lengthChange: false,
            //    buttons: ['copy', 'excel', 'pdf']
            //});
            var table = $('#Grdtagihan').DataTable({
                lengthChange: false,
                dom: 'Bfrtip',
                buttons: [
                    'copy',
                    'excel',
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9] // HANYA kolom ini yang dicetak
                        },
                        customize: function (doc) {
                            doc.pageOrientation = 'landscape';
                            doc.defaultStyle.fontSize = 8;

                            var tableBody = doc.content[1].table.body;

                            tableBody.forEach(function (row, i) {
                                if (i === 0) return; // skip header
                                row[8].noWrap = true; // index kolom (0-based)
                            });
                        }
                    }
                ]
            });


            var table5 = $('#Grdasabri').DataTable({
                lengthChange: false,
                dom: 'Bfrtip',
                buttons: [
                    'copy',
                    'excel',
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9] // HANYA kolom ini yang dicetak
                        },
                        customize: function (doc) {
                            doc.pageOrientation = 'landscape';
                            doc.defaultStyle.fontSize = 8;

                            var tableBody = doc.content[1].table.body;

                            tableBody.forEach(function (row, i) {
                                if (i === 0) return; // skip header
                                row[8].noWrap = true; // index kolom (0-based)
                            });
                        }
                    }
                ]
            });
            



            var table3 = $('#Grdtunggakan').DataTable({
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf']
            });

            //var table5 = $('#Grdasabri').DataTable({
            //    lengthChange: false,
            //    buttons: ['copy', 'excel', 'pdf']
            //});
            var table6 = $('#Grdtaspencancel').DataTable({
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf']
            });
            var table7 = $('#Grdasabricancel').DataTable({
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf']
            });

            var table8 = $('#GrdHistory').DataTable({
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
                .appendTo('#Grdtagihan_wrapper .col-md-6:eq(0)');


            table3.buttons().container()
                .appendTo('#Grdtunggakan_wrapper .col-md-6:eq(0)');


            table5.buttons().container()
                .appendTo('#Grdasabri_wrapper .col-md-6:eq(0)');

            table6.buttons().container()
                .appendTo('#Grdtaspencancel_wrapper .col-md-6:eq(0)');
            table7.buttons().container()
                .appendTo('#Grdasabricancel_wrapper .col-md-6:eq(0)');

            table8.buttons().container()
                .appendTo('#GrdHistory_wrapper .col-md-6:eq(0)');

            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");



            $('#txttgllahirkunjungan').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
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


            $(function () {

                var table = $('#Grdtagihan').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



            });

            $(function () {
                var table = $('#Grdtunggakan').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



            });



            $(function () {
                var table = $('#Grdasabri').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



            });

            $(function () {
                var table = $('#Grdtaspencancel').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



            });

            $(function () {
                var table = $('#Grdasabricancel').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



            });
            $(function () {
                var table = $('#GrdHistory').dataTable({
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
        }
    </script>


</head>
<body>
    <%-- <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script> 
    <script> </script>--%>
    <div id="globalLoading" style="display:none;">
        <div class="overlay"></div>
        <div class="loading-box">
            <img src="/assets/images/loadx.gif" alt="Loading" />
            <div>Processing...</div>
        </div>
    </div>


    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <script type="text/javascript">

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);

        </script>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container-fluid">
                    <asp:Panel ID="pnlData" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box">
                                    <h4 class="text-uppercase m-b-5 font-13 font-600">
                                        <asp:Label ID="Lbltaspen" runat="server" Text="DATA TASPEN/ASABRI"></asp:Label></h4>
                                    <div class="text-center mt-4 mb-4">
                                        <div class="row">
                                            <div class="col-xs-6 col-sm-6">
                                                <div class="card-box widget-flat border-custom bg-custom text-white">
                                                    <i class="fi-archive"></i>
                                                    <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A1" href="#" runat="server" onserverclick="btntagihan_Click">DATA FOLLOW-UP</a></p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-6">
                                                <div class="card-box widget-flat border-custom bg-custom text-white">
                                                    <i class="fi-tag"></i>
                                                    <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A2" href="#" runat="server" onserverclick="btntunggakan_Click">CANCEL DATA</a></p>

                                                </div>
                                            </div>
                                            <%--              <div class="col-xs-6 col-sm-4">
                                                                    <div  class="card-box bg-danger widget-flat border-danger text-white">
                                                                        <i class="fi-help"></i>
                                                                            <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A3"  href="#" runat="server" onserverclick="btnterbayar_Click">DATA FOLLOW UP</a></p>
                                                                    </div>
                                                                </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Pnlsearch" runat="server">
                        <div class="card m-b-10 text-white bg-custom text-xs-center">
                            <div class="text-center mt-2 mb-2">
                                <asp:Label ID="Lbljudul" runat="server" Text="DATA FOLLOW UP"></asp:Label>
                            </div>
                        </div>


                        <div class="row">

                            <div class="col-lg-6">

                                <div class="card-box">
                                    <h4 class="header-title m-t-0">Search By</h4>
                                    <form action="#">
                                        <div class="form-group row">
                                            <label for="inputpid" class="col-3 col-form-label">Pilih DATA</label>
                                            <div class="col-7">
                                                <asp:DropDownList ID="ddlbahan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                    <asp:ListItem>PILIH DATA</asp:ListItem>
                                                    <asp:ListItem>TASPEN</asp:ListItem>
                                                    <asp:ListItem>ASABRI</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:HiddenField ID="datacancel" runat="server" />
                                            </div>

                                        </div>
                                        <div class="form-group row">
                                            <label for="inputpid" class="col-3 col-form-label">Status Follow Up</label>
                                            <div class="col-7">
                                                <asp:DropDownList ID="ddlstatusfollow" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                    <asp:ListItem Value="0">Belum dikunjungi</asp:ListItem>
                                                    <asp:ListItem Value="1">Sudah dikunjungi</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group text-right m-b-0">
                                            <%--<button type="button" id="btnsearch" class="btn btn-outline-custom waves-light waves-effect" href="#" runat="server" onserverclick="lnkCari_Click">Search</button>--%>
                                            <asp:Button ID="btnsearch" class="btn btn-custom waves-light text-white" runat="server" Text="Search" OnClick="lnkCari_Click" />
                                        </div>

                                    </form>
                                </div>
                                <!-- end card-box -->
                            </div>

                            <div class="col-lg-6">

                                <div class="card-box">
                                    <h4 class="header-title m-t-0">DATA yang diambil Hari Ini</h4>
                                    <form action="#">
                                        <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Outstanding</label>
                                            <asp:Label ID="LblOutstanding" runat="server" Text="0"></asp:Label>
                                        </div>

                                        <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">DATA</label>
                                            <asp:Label ID="LblBahan" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Minimal</label>
                                            <asp:Label ID="Lblminimal" runat="server" Text="0"></asp:Label>
                                        </div>

                                        <%--                  <div class="form-group text-right m-b-0">
                                        <button type="button" id="btnproses" class="btn btn-outline-custom waves-light waves-effect" href="#" runat="server" onserverclick="btnproses_Click">Get DATA</button>

                                    </div>--%>
                                    </form>
                                </div>
                                <!-- end card-box -->
                            </div>

                        </div>

                    </asp:Panel>
                    <asp:Panel ID="Pnltagihan" Visible="false" runat="server">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box table-responsive">
                                    <label class="text-uppercase m-b-5 font-13 font-600">DATA FOLLOW-UP TASPEN</label>
                                    <div class="text-center mt-4 mb-4">
                                        <div class="form-row">
                                            <div class="form-group" style="width: 100%;">
                                                <asp:GridView ID="Grdtagihan" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="True"
                                                    AutoGenerateColumns="False" AllowSorting="True" OnRowCommand="Grdtagihan_RowCommand" OnRowDataBound="Grdtagihan_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Process">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Lnkakanfollwo" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Approve Follow Up" runat="server" CommandName="APPROVE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Kunjungan</asp:LinkButton>
                                                                <asp:LinkButton ID="lbCetakPenawaran" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Cetak Surat Penawaran" runat="server" CommandName="PENAWARAN" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Penawaran</i></asp:LinkButton>
                                                                <asp:LinkButton ID="Lnkcancel4" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Cancel data" runat="server" CommandName="CANCEL3" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Cancel</i></asp:LinkButton>
                                                                <asp:LinkButton ID="LnkView2" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="HISTORY" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">History</i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton3" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">View</i></asp:LinkButton>
                                                                <asp:Label ID="lblnouruttaspen" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "serial")%>'></asp:Label>
                                                                <asp:Label ID="lblnourut" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                <asp:Label ID="lbllat" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "lat")%>'></asp:Label>
                                                                <asp:Label ID="lbllon" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "lon")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--     <asp:TemplateField HeaderText="Serial">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                           
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Tgl.Getdata">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltglgetdata" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglgetdata","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanggal Lahir">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltgllahirpsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Usia">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Alamat">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notas">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnotas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notas")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama Penerima">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnamapenerima" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_penerima")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gaji">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgaji" runat="server" Text='<%# Eval("gaji") == DBNull.Value ? "0"
                                                                    : Convert.ToDecimal(Eval("gaji")).ToString("N0",
                                                                      System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %>'
                                                                ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Keterangan">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblketerangantaspen" runat="server" Text=''></asp:Label>
                                                            </ItemTemplate>
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
                    <asp:Panel ID="Pnlasabari" Visible="false" runat="server">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box table-responsive">
                                    <label class="text-uppercase m-b-5 font-13 font-600">DATA FOLLOW-UP ASABRI</label>
                                    <div class="text-center mt-4 mb-4">
                                        <div class="form-row">
                                            <div class="form-group" style="width: 100%;">
                                                <asp:GridView ID="Grdasabri" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="true"
                                                    AutoGenerateColumns="False" OnRowCommand="Grdasabri_RowCommand" OnRowDataBound="Grdasabri_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="+">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Lnkakanfollwox" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Follow Up" runat="server" CommandName="APPROVE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Kunjungan</asp:LinkButton>
                                                                <asp:LinkButton ID="lbCetakPenawaranAsabri" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Cetak Surat Penawaran" runat="server" CommandName="PENAWARAN" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Penawaran</i></asp:LinkButton>
                                                                <asp:LinkButton ID="Lnkcancelfollwox" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Cancel data" runat="server" CommandName="CANCEL1" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Cancel</asp:LinkButton>
                                                                <asp:LinkButton ID="LnkView1" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="HISTORY" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">History</asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton3" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">View</asp:LinkButton>
                                                                <asp:Label ID="lblnourutasabri" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "serial")%>'></asp:Label>
                                                                <asp:Label ID="lblnourut" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                <asp:Label ID="lbllat" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "lat")%>'></asp:Label>
                                                                <asp:Label ID="lbllon" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "lon")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="Serial">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnourut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Tgl.Getdata">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltglgetdata" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglgetdata","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanggal Lahir">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltgllahirpsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Usia">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Alamat">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notas">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnotas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nopens")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gaji">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblgaji" runat="server" Text='<%# Eval("gaji") == DBNull.Value ? "0" : string.Format("{0:N0}", Eval("gaji")) %>'></asp:Label>--%>

                                                                <asp:Label ID="lblgaji" runat="server" Text='<%# Eval("gaji") == DBNull.Value ? "0"
                                                                    : Convert.ToDecimal(Eval("gaji")).ToString("N0",
                                                                      System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %>'
                                                                ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Keterangan">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblketeranganasabri" runat="server" Text=''></asp:Label>
                                                            </ItemTemplate>
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
                    <asp:Panel ID="Pnltaspencancel" Visible="false" runat="server">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box table-responsive">
                                    <label class="text-uppercase m-b-5 font-13 font-600">DATA CANCEL TASPEN</label>
                                    <div class="text-center mt-4 mb-4">
                                        <div class="form-row">
                                            <div class="form-group" style="width: 100%;">
                                                <asp:GridView ID="Grdtaspencancel" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="True"
                                                    AutoGenerateColumns="False" AllowSorting="True">
                                                    <Columns>

                                                        <%--   <asp:TemplateField HeaderText="Serial">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnourut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Tanggal Cancel">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnourut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglcancel","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Note Cancel">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnourut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notecancel")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approve Cancel">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnourutx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ApproveCancel")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tgl.Getdata">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltglgetdata" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglgetdata","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanggal Lahir">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltgllahirpsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Usia">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Alamat">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notas">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnotas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notas")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gaji">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgaji" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
                                                            </ItemTemplate>
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
                    <asp:Panel ID="Pnlasabricancel" Visible="false" runat="server">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box table-responsive">
                                    <label class="text-uppercase m-b-5 font-13 font-600">DATA CANCEL ASABRI</label>
                                    <div class="text-center mt-4 mb-4">
                                        <div class="form-row">
                                            <div class="form-group" style="width: 100%;">
                                                <asp:GridView ID="Grdasabricancel" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="true"
                                                    AutoGenerateColumns="False">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Serial">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnourut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanggal Cancel">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnourut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglcancel","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Note Cancel">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnourut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notecancel")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approve Cancel">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnourut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ApproveCancel")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tgl.Getdata">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltglgetdata" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglgetdata","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanggal Lahir">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltgllahirpsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Usia">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Alamat">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notas">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnotas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nopens")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gaji">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgaji" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
                                                            </ItemTemplate>
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
                    <asp:Panel ID="Pnlakanfollow" Visible="false" runat="server">

                        <div class="row">
                            <div class="col-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Entry Data Kunjungan</h4>
                                    <p class="text-muted m-b-30 font-14">
                                        Dengan Notas,Nama dan Alamat dibawah ini 
                                    </p>

                                    <div class="row">
                                        <div class="col-12">
                                            <div class="p-20">
                                                <form class="form-horizontal" role="form">
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Notas</label>
                                                        <div class="col-10">

                                                            <asp:TextBox ID="txtnotasmaster" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Nama</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="Txtnama" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Alamat</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtalamat" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <p class="text-muted m-b-30 font-14">
                                                        Isi  data-data dibawah ini Tanggal Kunjungan otomatis tanggal sekarang :
                                                    </p>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label" for="example-email">Apakah Anda sudah Ketemu ? </label>
                                                        <div class="col-10">
                                                            <asp:DropDownList ID="ddlfollowup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                <asp:ListItem Value="0">Ketemu</asp:ListItem>
                                                                <asp:ListItem Value="1">Tidak Ketemu</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Nama</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="Txtnamakunjungan" required="true" placeholder="Isi Data Nama Pensiunan (Wajib diisi)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Alamat</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="TxtAlamatKunjungan" required="true" placeholder="Isi Data Alamat Pensiunan (Wajib diisi)" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Gaji</label>
                                                        <div class="col-10">
                                                            <asp:TextBox CssClass="form-control" ID="TxtGajikunjungan" runat="server" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                            <%--<asp:TextBox ID="TxtGajikunjungan"   placeholder="Isi Data Gaji (Optional)"  CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Tempat Lahir</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txttempatlahirkunjungan" placeholder="Isi Data tempat Lahir (Optional)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Tgl Lahir</label>
                                                        <div class="col-10">

                                                            <asp:TextBox ID="txttgllahirkunjungan" placeholder="Isi Data tanggal Lahir (Optional)" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Usia</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtusia" placeholder="Isi Data usia (Optional)" CssClass="form-control" runat="server" onkeypress="return isnumerickey(event);">0</asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">No Telpon atau HP</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txttelpon" placeholder="Isi Data telpon atau HP (Optional)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Kantor Bayar</label>
                                                        <div class="col-10">
                                                            <%--<asp:TextBox ID="txtkantorbayarkunjungan" placeholder="Isi Data Kantor Bayar (Optional)" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlKantorBayarKunjungan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Status SK On Hand</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtskonhand" placeholder="Isi Data Status SK On Hand (Optional)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Status SK On Take Over</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtskonto" placeholder="Isi Data Status SK On To (Optional)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <p class="text-muted m-b-30 font-14">
                                                        Isi  data Pinjaman lama jika ada dibawah ini :
                                                    </p>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Apakah Punya pinjaman ? </label>
                                                        <div class="col-10">
                                                            <asp:DropDownList ID="ddlpinjaman" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                                <asp:ListItem Value="1">Ada</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Kalau Ada pinjaman Plafon Berapa ? </label>
                                                        <div class="col-10">
                                                            <asp:TextBox CssClass="form-control" ID="txtplafonlama" runat="server" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtplafonlama"   placeholder="Isi Data Plafon (Optional)"  CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Kalau Ada pinjaman Jangka Waktu Berapa Lama ? </label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtJangkawaktulama" placeholder="Isi Data Jangka Waktu (Optional)" CssClass="form-control" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Kalau Ada Angsuran Berapa Lama ? </label>
                                                        <div class="col-10">
                                                            <asp:TextBox CssClass="form-control" ID="txtangsuranlama" runat="server" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtangsuranlama"   placeholder="Isi Data Angsuran (Optional)"  CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                    <p class="text-muted m-b-30 font-14">
                                                        Isi  data rencana Pinjaman di sena jika ada dibawah ini :
                                                    </p>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Pinjaman Plafon Berapa di sena ? </label>
                                                        <div class="col-10">
                                                            <asp:TextBox CssClass="form-control" ID="txtplafonsena" runat="server" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtplafonsena"   placeholder="Isi Data Plafon (Optional)"  CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Jangka Waktu Berapa lama Pinjaman di sena ? </label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtjangkawaktusena" placeholder="Isi Data Jangka Waktu (Optional)" CssClass="form-control" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Angsuran Pinjaman di sena ? </label>
                                                        <div class="col-10">
                                                            <asp:TextBox CssClass="form-control" ID="txtterimabersih" runat="server" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtterimabersih"   placeholder="Isi Data Terima Bersih (Optional)"  CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                    <p class="text-muted m-b-30 font-14">
                                                        Isi  data Kategori peminjam dibawah ini :
                                                    </p>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Apakah Peminjam punya Kemampuan (gaji dan usia) ? </label>
                                                        <div class="col-10">
                                                            <asp:DropDownList ID="ddlkemampuan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                                <asp:ListItem Value="1">Ada</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Apakah Peminjam punya Keinginan  ? </label>
                                                        <div class="col-10">
                                                            <asp:DropDownList ID="ddlkeinginan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                                <asp:ListItem Value="1">Ada</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <p class="text-muted m-b-30 font-14">
                                                        Isi  data Catatan peminjam dibawah ini :
                                                    </p>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Catatan</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtnourutfollow" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>

                                                            <asp:TextBox CssClass="auto-style1" ID="txtcatatan" required="required" runat="server" Height="103px" MaxLength="255" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlstatus" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>

                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">latitude</label>
                                                        <div class="col-10">
                                                            <asp:Label ID="txtlat" name="txtlat" Text="0" CssClass="form-control" runat="server" ClientID="txtlat"></asp:Label>
                                                            <asp:HiddenField ID="Hdnlat" runat="server" />
                                                            <asp:HiddenField ID="Hdnlon" runat="server" />
                                                        </div>
                                                        <label class="col-2 col-form-label">longitude</label>
                                                        <div class="col-10">
                                                            <asp:Label ID="txtlon" name="txtlon" Text="0" CssClass="form-control" runat="server" ClientID="txtlon"></asp:Label>
                                                        </div>
                                                        <asp:Button ID="Button2" runat="server" Text="Get latitude & longitude " OnClientClick="getLocation();initializex();return false;" />
                                                    </div>

                                                    <div class="form-group mb-0 justify-content-end row">

                                                        <div class="col-9">

                                                            <asp:Button ID="btnkembali" CssClass="btn btn-outline-primary" runat="server" Text="Simpan" OnClick="btnSimpan_followup_Click" />
                                                            <%--<asp:LinkButton ID="lnkbtn" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembali_Click" />--%>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12">
                                                        <div class="card-box">
                                                            <h4 class="m-t-0 header-title">PETA LOKASI</h4>
                                                            <%-- 
                                                                <asp:TextBox ID="TextBox3" Enabled="false" runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox4" Enabled="false" runat="server"></asp:TextBox>
                                                                <asp:Button ID="Button4" CssClass="btn btn-outline-primary" runat="server" Text="Pencarian" OnClientClick="initialize();return false;"  />
                                                            --%>
                                                            <div class="form-row">

                                                                <div id="map_canvas1" runat="server" style="width: 100%; height: 500px; border: 5px solid #5E5454;"></div>

                                                            </div>

                                                        </div>

                                                    </div>
                                                    <!-- end card-box -->

                                                </form>
                                            </div>
                                        </div>

                                    </div>
                                    <!-- end row -->

                                </div>
                                <!-- end card-box -->
                            </div>
                            <!-- end col -->
                        </div>
                        <!-- end row -->

                    </asp:Panel>
                    <asp:Panel ID="Pnlcancel" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box">
                                    <h4 class="header-title m-t-0 m-b-20">Cancel Data</h4>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="card-box">
                                                <div class="form-horizontal">

                                                    <%--<div class="form-group row">
                                                                                            <label for="inputpid" class="col-3 col-form-label">Jenis Follow Up</label>
                                                                                            <div class="col-9">
                                                                                                <asp:DropDownList ID="ddlcancel" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                                                                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" Visible="false" ></asp:TextBox>
                                                                                            </div>
                                                                                        </div>--%>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Notas</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtnotascancel" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Nama</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtnamacancel" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Alamat</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtalamatcancel" Enabled="false" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputpid" class="col-3 col-form-label">Catatan Cancel</label>
                                                        <div class="col-10">
                                                            <asp:TextBox CssClass="form-control" ID="txtcancel" required="required" runat="server" MaxLength="255" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group mb-0 justify-content-end row">
                                                        <div class="col-9">
                                                            <asp:Button ID="Button1" CssClass="btn btn-outline-primary" runat="server" Text="Save" OnClick="btnCancel2_Click" />
                                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembali_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box -->
                            </div>
                            <!-- end col -->
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Pnlhistory" Visible="false" runat="server">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box table-responsive">
                                    <label class="text-uppercase m-b-5 font-13 font-600">DATA FOLLOW UP</label>
                                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembali2_Click" />
                                    <div class="text-center mt-4 mb-4">
                                        <div class="form-row">
                                            <div class="form-group" style="width: 100%;">
                                                <asp:GridView ID="GrdHistory" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="true"
                                                    AutoGenerateColumns="False">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Tanggal">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltgl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglkunjungan","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakunjungan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Alamat">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblalamatkunjungan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamatkunjungan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Hp">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltelponkunjungan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "telponkunjungan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status SK OnHand">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblskonhand" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "skonhand")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status SK To">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblskonto" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "skonto")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tempat Tanggal Lahir">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltgllahirkunjungan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahirkunjungan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Usia">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusiakunjungan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usiakunjungan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gaji">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgajikunjungan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gajikunjungan","{0:n}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kantor Bayar">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblkantorbayarkunjungan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kantorbayarkunjungan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pinjaman Lama">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpinjamanlama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pinjamanlama")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Plafon Pinjaman Lama">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblplafonlama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafonlama","{0:n}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JW Pinjaman Lama">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljangkalama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangkalama")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Angsuran Pinjaman Lama">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljangkalama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuranlama","{0:n}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Keterangan">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKeterangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notekunjungan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Plafon Pinjaman Sena">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblplafonsena" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafonsena","{0:n}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JW Pinjaman Sena">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljangkasena" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangkasena")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Angsuran Pinjaman Sena">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblangsuransena" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuransena","{0:n}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Kemampuan">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblkemampuan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kemampuan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="keinginan">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblkeinginan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keinginan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prospek">
                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblprospek" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "prospek")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="card-box">
                                <h4 class="m-t-0 header-title">PETA LOKASI</h4>
                                <asp:TextBox ID="TextBox1" Enabled="false" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox2" Enabled="false" runat="server"></asp:TextBox>
                                <asp:Button ID="Button3" CssClass="btn btn-outline-primary" runat="server" Text="Pencarian" OnClientClick="initialize();return false;" />
                                <div class="form-row">

                                    <h4 class="header-title">Detail Location</h4>

                                    <div id="map_canvas" runat="server" style="width: 100%; height: 500px; border: 5px solid #5E5454;"></div>

                                </div>

                            </div>

                        </div>
                        <!-- end card-box -->

                    </asp:Panel>
                    <asp:Panel ID="pnlformattaspen" Visible="false" runat="server">

                        <asp:LinkButton ID="LinkButton4" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembalitaspen_Click" />
                        <div class="ribbon ribbon-custom"></div>
                        <p class="text-muted font-14 m-b-30">
                        </p>
                        <div class="row">

                            <div class="col-lg-6">

                                <div class="card-box">
                                    <h4 class="m-t-q header-title">Data Pensiun</h4>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Notas</label>
                                            <asp:TextBox ID="txtnotas" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Jenis Pensiun</label>
                                            <asp:TextBox ID="Txtjenis" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>


                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Nama Pensiunan</label>
                                            <asp:TextBox ID="txtnamapensiun" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Tgl Lahir Pensiunan</label>
                                            <asp:TextBox ID="txttgllahir" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>


                                    <div class="form-row">

                                        <div class="form-group col-md-4">
                                            <label for="inputState" class="col-form-label">Kode Pangkat</label>
                                            <asp:TextBox ID="txtkodepangkat" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label for="inputCity" class="col-form-label">Ket Jiwa</label>
                                            <asp:TextBox ID="txtjiwa" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label for="inputState" class="col-form-label">Stop</label>
                                            <asp:TextBox ID="txtstop" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Kode Cabang</label>
                                            <asp:TextBox ID="txtcabang" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Telepon</label>
                                            <asp:TextBox ID="txttelepon" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>


                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Kode Bayar</label>
                                            <asp:TextBox ID="txtkodebyr" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Nama Bayar</label>
                                            <asp:TextBox ID="txtnamabyr" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                </div>

                            </div>

                            <div class="col-lg-6">

                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Pendapatan</h4>
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Pendapatan Pokok</label>
                                            <asp:TextBox ID="txtpenpok" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Tunjangan Istri</label>
                                            <asp:TextBox ID="txttistri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Tunjangan Anak</label>
                                            <asp:TextBox ID="txttanak" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Tunjangan Pajak</label>
                                            <asp:TextBox ID="txttpajak" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Tunjangan Beras</label>
                                            <asp:TextBox ID="Txttberas" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Penyesuaian</label>
                                            <asp:TextBox ID="txtpenyesuaian" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Pembulatan</label>
                                            <asp:TextBox ID="txttbulat" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Kotor</label>
                                            <asp:TextBox ID="TxtKotor" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Bersih</label>
                                            <asp:TextBox ID="TxtBersih" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Rapel</label>
                                            <asp:TextBox ID="TxtRapel" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>


                                </div>
                                <!-- end card-box -->
                            </div>

                            <div class="col-lg-6">

                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Penerima</h4>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Nama Penerima</label>
                                            <asp:TextBox ID="Txtpenerima" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Tanggal Lahir Penerima</label>
                                            <asp:TextBox ID="Txttgllahirpenerima" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <label for="inputpid" class="col-3 col-form-label">Alamat Penerima</label>
                                        <asp:TextBox ID="Txtalamatpenerima" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Dati 4</label>
                                            <asp:TextBox ID="Txtdati4" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Kota</label>
                                            <asp:TextBox ID="txtkota" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>


                                </div>
                                <!-- end card-box -->
                            </div>

                            <div class="col-lg-6">

                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Lain-Lain</h4>
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">No.Urut.Dapem</label>
                                            <asp:TextBox ID="txtnourutdapem" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Tmt.Pensiun</label>
                                            <asp:TextBox ID="Txttmppensiun" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-4">
                                            <label for="inputCity" class="col-form-label">No.Skep</label>
                                            <asp:TextBox ID="TxtNoskep" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label for="inputState" class="col-form-label">Tanggal Skep</label>
                                            <asp:TextBox ID="TxttglSkep" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-5">
                                            <label for="inputState" class="col-form-label">Penerbit Skep</label>
                                            <asp:TextBox ID="Txtpenerbitskep" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Norek</label>
                                            <asp:TextBox ID="Txtnorek" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Npwp</label>
                                            <asp:TextBox ID="txtnpwp" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                </div>
                                <!-- end card-box -->
                            </div>

                        </div>

                    </asp:Panel>
                    <asp:Panel ID="pnlformatasabri" Visible="false" runat="server">
                        <asp:LinkButton ID="LinkButton5" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembaliasbri_Click" />

                        <div class="ribbon ribbon-custom"></div>
                        <p class="text-muted font-14 m-b-30">
                        </p>
                        <div class="row">

                            <div class="col-lg-6">

                                <div class="card-box">
                                    <h4 class="m-t-q header-title">Data asabri</h4>
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Nopens</label>
                                            <asp:TextBox ID="txtnopensasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Nrp</label>
                                            <asp:TextBox ID="txtnrpasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Nama Pensiunan</label>
                                            <asp:TextBox ID="txtnamaasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Tgl Lahir Pensiunan</label>
                                            <asp:TextBox ID="txttgllahirasabri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Kode Jiwa</label>
                                            <asp:TextBox ID="txtkdjiwaasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Usia</label>
                                            <asp:TextBox ID="txtusiaasbri" CssClass="form-control" runat="server" onkeypress="return isnumerickey(event);">0</asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-3">
                                            <label for="inputCity" class="col-form-label">kd_jby</label>
                                            <asp:TextBox ID="txtkd_jbyasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label for="inputState" class="col-form-label">Kode Pangkat</label>
                                            <asp:TextBox ID="txtpangkatasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Nama Mitra</label>
                                            <asp:TextBox ID="Txtmitraasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Kode Cabang</label>
                                            <asp:TextBox ID="txtkodecabangasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Nama Cabang</label>
                                            <asp:TextBox ID="txtnamacabangasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <label for="inputpid" class="col-3 col-form-label">Alamat</label>
                                        <asp:TextBox ID="txtalamatasbri" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group col-md-3">
                                            <label for="inputCity" class="col-form-label">Rt</label>
                                            <asp:TextBox ID="txtrtasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label for="inputState" class="col-form-label">RW</label>
                                            <asp:TextBox ID="txtrwasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Kelurahan</label>
                                            <asp:TextBox ID="txtkelurahanasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Kecamatan</label>
                                            <asp:TextBox ID="txtkecamatanasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Provinsi</label>
                                            <asp:TextBox ID="txtprovinsiasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                </div>

                            </div>

                            <div class="col-lg-6">

                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Pendapatan</h4>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Pendapatan Pokok</label>
                                            <asp:TextBox ID="txtpendapatanpokokasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Tunjangan Istri</label>
                                            <asp:TextBox ID="txttunjistriabsri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Tunjangan Anak</label>
                                            <asp:TextBox ID="txttunjanakasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Tunjangan Beras</label>
                                            <asp:TextBox ID="txttunberasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Tunjangan Lain</label>
                                            <asp:TextBox ID="txttunjlainasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Jumlah Bruto</label>
                                            <asp:TextBox ID="txtbrutoasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Potongan pph21</label>
                                            <asp:TextBox ID="txtpot_pph21" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Potongan Askes</label>
                                            <asp:TextBox ID="txtpot_askes" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Potongan Hutang</label>
                                            <asp:TextBox ID="txtpot_hutang" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Potongan lain</label>
                                            <asp:TextBox ID="txtpot_lain" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-4">
                                            <label for="inputCity" class="col-form-label">Jumlah Potong</label>
                                            <asp:TextBox ID="txtjml_potong" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label for="inputState" class="col-form-label">Jumlah Netto</label>
                                            <asp:TextBox ID="txtjlm_netto" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label for="inputState" class="col-form-label">Pembulatan</label>
                                            <asp:TextBox ID="txtpembulatan" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="inputCity" class="col-form-label">Bank Rekening</label>
                                            <asp:TextBox ID="txtbank_rekening" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="inputState" class="col-form-label">Jenis Rekening</label>
                                            <asp:TextBox ID="txtjenis_rekening" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <label for="inputpid" class="col-3 col-form-label">No Rekening</label>
                                        <asp:TextBox ID="txtno_rekening" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                                <!-- end card-box -->
                            </div>

                        </div>

                    </asp:Panel>

                    <asp:Panel ID="PanelSuratPenawaran" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box">
                                    <h4 class="header-title m-t-0 m-b-20">Data Surat Penawaran</h4>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="card-box">
                                                <div class="form-horizontal">

                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Notas</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtNotasPenewaran" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Nama</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtNamaNasabah" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Alamat</label>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="txtAlamatNasabah" Enabled="false" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <%--<div class="form-group row">
                                                        <label for="inputpid" class="col-2 col-form-label">Petugas</label>
                                                        <div class="col-10">
                                                            <asp:TextBox CssClass="form-control" ID="txtPetugas" required="required" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>--%>
                                                    <div class="form-group row">
                                                        <label class="col-2 col-form-label">Petugas</label>
                                                        <div class="col-10">

                                                            <asp:Repeater ID="rptPetugas" runat="server" OnItemCommand="rptPetugas_ItemCommand">
                                                                <ItemTemplate>
                                                                    <div class="row mb-2">
                                                                        <div class="col-4">
                                                                            <asp:TextBox
                                                                                ID="txtNamaPetugas"
                                                                                runat="server"
                                                                                CssClass="form-control"
                                                                                Placeholder="Nama Petugas"
                                                                                Text='<%# Bind("Nama") %>' />
                                                                        </div>

                                                                        <div class="col-4">
                                                                            <asp:TextBox
                                                                                ID="txtNoTelp"
                                                                                runat="server"
                                                                                CssClass="form-control"
                                                                                Placeholder="No. Telp"
                                                                                Text='<%# Bind("NoTelp") %>' />
                                                                            <asp:RegularExpressionValidator
                                                                                ID="revPhone"
                                                                                runat="server"
                                                                                ControlToValidate="txtNoTelp"
                                                                                ValidationExpression="^(?:\+62|62|0)\s?8[1-9](?:[\s\-]?[0-9]){6,9}$"
                                                                                ErrorMessage="Nomor telepon tidak valid"
                                                                                ForeColor="Red" />
                                                                        </div>

                                                                        <div class="col-2">
                                                                            <asp:LinkButton
                                                                                ID="btnHapusPetugas"
                                                                                runat="server"
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                CommandName="delete"
                                                                                CommandArgument="<%# Container.ItemIndex %>"
                                                                                Text="Hapus" />
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                            <asp:Button
                                                                ID="btnTambahPetugas"
                                                                runat="server"
                                                                CssClass="btn btn-outline-secondary btn-sm mt-2"
                                                                Text="+ Tambah Petugas"
                                                                OnClick="btnTambahPetugas_Click" />

                                                        </div>
                                                    </div>


                                                    <div class="form-group mb-0 justify-content-end row">
                                                        <div class="col-9">
                                                            <asp:Button ID="btnCetakSuratPenawaran" CssClass="btn btn-outline-primary" runat="server" Text="Save" OnClick="btnCetakSuratPenawaran_Click" />
                                                            <asp:LinkButton ID="btnKembaliPenawaran" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembaliPenawaran_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box -->
                            </div>
                            <!-- end col -->
                        </div>
                    </asp:Panel>

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnCetakSuratPenawaran" />

                <%--<asp:AsyncPostBackTrigger  ControlID="ddlcabang" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="ddlcabangprodukAsrn" />--%>
                <%--<asp:PostBackTrigger ControlID="Button2" />--%>
                <%--<asp:PostBackTrigger ControlID="Lnkcancelfollwox" />--%>

                <%--Lnkcancelfollwox--%>
                <%--<asp:AsyncPostBackTrigger ControlID="ddlbahan" />--%>
                <%--<asp:PostBackTrigger ControlID="btnproses" />--%>
            </Triggers>
        </asp:UpdatePanel>
        <%--        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
<ProgressTemplate>
<div class="overlay">
<div style=" z-index: 1000; margin: 300px auto;padding: 10px;width: 160px;background-color: White;border-radius: 10px;filter: alpha(opacity=100);opacity: 1;-moz-opacity: 1;">
<asp:Image ID="Image1" ImageUrl="/assets/images/loadx.gif" AlternateText="Processing" runat="server" Width="136px"/>
</div>
</div>
</ProgressTemplate>
</asp:UpdateProgress>--%>
    </form>
</body>

<!-- App js -->
<script src="/assets/js/jquery.slimscroll.js"></script>
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

    <%--
        versi ini bekerja dengan baik
        <script type="text/javascript">
        (function () {

            function showLoading() {
                $('#globalLoading').show();
            }

            function hideLoading() {
                $('#globalLoading').hide();
            }

            Sys.WebForms.PageRequestManager.getInstance()
                .add_beginRequest(function () {
                    showLoading();
                });

            Sys.WebForms.PageRequestManager.getInstance()
                .add_endRequest(function () {
                    hideLoading();
                });

            // Fallback untuk FULL postback (PostBackTrigger)
            window.onbeforeunload = function () {
                showLoading();
            };

        })();
    </script>--%>

    <%--versi ini bekerja dengan baik dengan tambahan tunning--%>
    <%--<script type="text/javascript">
        (function () {

            function showLoading() {
                $('#globalLoading').show();

                // failsafe: kalau response bukan HTML (PDF / download),
                // overlay auto-hide sendiri
                clearTimeout(window.__loaderTimeout);
                window.__loaderTimeout = setTimeout(function () {
                    $('#globalLoading').hide();
                }, 3000);
            }

            function hideLoading() {
                clearTimeout(window.__loaderTimeout);
                $('#globalLoading').hide();
            }

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_beginRequest(function () {
                showLoading();
            });

            prm.add_endRequest(function () {
                hideLoading();
            });

            // FULL postback fallback (cukup pakai ini, JANGAN override __doPostBack)
            window.onbeforeunload = function () {
                showLoading();
            };

        })();
    </script>--%>

    <%--versi ini bekerja dengan baik dengan tambahan tunning dan block double click--%>
    <script type="text/javascript">
        (function () {

            var isPosting = false;

            function disablePostbackControls() {
                isPosting = true;

                // Disable semua trigger postback
                $('input[type=submit], button, a')
                    .addClass('disabled')
                    .attr('disabled', 'disabled')
                    .css('pointer-events', 'none');
            }

            function enablePostbackControls() {
                isPosting = false;

                $('input[type=submit], button, a')
                    .removeClass('disabled')
                    .removeAttr('disabled')
                    .css('pointer-events', '');
            }

            function showLoading() {
                if (isPosting) return;

                $('#globalLoading').show();
                disablePostbackControls();

                // failsafe untuk response non-HTML (PDF / download)
                clearTimeout(window.__loaderTimeout);
                window.__loaderTimeout = setTimeout(function () {
                    $('#globalLoading').hide();
                    enablePostbackControls();
                }, 3000);
            }

            function hideLoading() {
                clearTimeout(window.__loaderTimeout);
                $('#globalLoading').hide();
                enablePostbackControls();
            }

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            // ===== ASYNC POSTBACK =====
            prm.add_beginRequest(function () {
                showLoading();
            });

            prm.add_endRequest(function () {
                hideLoading();
            });

            // ===== FULL POSTBACK FALLBACK =====
            window.onbeforeunload = function () {
                if (!isPosting) {
                    showLoading();
                }
            };

        })();
    </script>





</html>

