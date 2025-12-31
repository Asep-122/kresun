<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemofeerewardNonManagementApprove.aspx.cs" Inherits="AE_Process_MemofeerewardNonManagementApprove" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Reward & Fee Cabang</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
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
   
    <script type="text/javascript">
             function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
         function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: '<%=nobuktidb%>',
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
        function peringatan(errMsg) {

            setTimeout(function () {
                swal(
                    {   
                        type : 'warning',
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);

        }
           function stringToArray(items) {
            return items.replace(/'/g, '"');
        }

          //numeric 1
        function Comma(Num) {

            console.log('<%=CultureName%>');
            var splitnya;
            var koma;

            var region = '<%=CultureName%>';

            if (region == 'id-ID') {
                splitnya = '.';
                koma = ',';
            }
            else {
                splitnya = ',';
                koma = '.';
            }

            Num += '';
            Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, '');
            Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, '');
            x = Num.split(koma);
            x1 = x[0];
            x2 = x.length > 1 ? koma + x[1] : '';
            var rgx = /(\d+)(\d{3})/;


            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + splitnya + '$2');


            return x1 + x2;

        }

        //numeric 2
        function isnumerickey(e) {

            var region = '<%=CultureName%>';


            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {


                if (txt == 44 && region == 'id-ID') {
                    return true;
                }
                if (txt == 46 && region != 'id-ID') {
                    return true;
                }
                else {
                    //alert("Input is Number only")
                    return false;
                }

            }
            return true;
        }


          //numeric 3
        function checkTextField1(field) {

            var region = '<%=CultureName%>';
            var splitnya;
            var dolarnya;
            var koma;

            if (region == 'id-ID') {
                splitnya = '.';
                koma = ',';
                dolarnya = '$1.';
            }
            else {
                splitnya = ',';
                koma = '.';
                dolarnya = '$1,';
            }

            if (field.value == '' || field.value == 0 || field.value == koma) {
                field.value = 0;
            }


            var nilai = field.value.split(splitnya).join('');
            var output = nilai;
            if (parseFloat(nilai)) {
                input = new String(nilai); // so you can perform string operations
                var parts = input.split(koma); // remove the decimal part
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, dolarnya).split("").reverse().join("");
                output = parts.join(koma);
            }
            field.value = output;
        }
        var s = 0
        var v = 0
        var x = 0;
        var y = 0;
        var z = 0;
        function calc(obj) {
                var e = obj.id.toString();
            if (e == 'txttransAP') {
                x = Number(obj.value);
                y = Number(document.getElementById('txtkursstd').value);  
                v = Number(document.getElementById('txtkursstdbank').value); 
            }
            else if(e == '')
            {
                x = Number(document.getElementById('txttransAP').value);
                y = Number(obj.value); 
            } 
            z = x * y; 
            s = z / v;
            document.getElementById('txtjmlhGL').value = z; 
            document.getElementById('hfjmlrp').value = z;
            document.getElementById('txtjmlhCL').value = s;
        }
           function sweetAlertConfirm(btndelete,errMsg) {
        if (btndelete.dataset.confirmed) {
            // The action was already confirmed by the user, proceed with server event
            btndelete.dataset.confirmed = false;
            return true;
        } else {
            // Ask the user to confirm/cancel the action
            event.preventDefault();
            swal({
                title: errMsg,
                confirmButtonClass: 'btn btn-confirm mt-2',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: "Yes",
                cancelButtonText: "No",
            })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    btndelete.dataset.confirmed = true;
                    btndelete.click();
                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
        }
        }    



       
        function cancelalert(errMsg) {

            swal({
                title: errMsg,
                // text: 'Your message has been succesfully sent',
                confirmButtonClass: 'btn btn-confirm mt-2',
                type: 'warning'

            })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    alert("CEKDATA");
                    setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                });
        }


function sweetAlertsave(btnSimpan,errMsg) {
        if (btnSimpan.dataset.confirmed) {
            // The action was already confirmed by the user, proceed with server event
            btnSimpan.dataset.confirmed = false;
            return true;
        } else {
            // Ask the user to confirm/cancel the action
            event.preventDefault();
            swal({
                title: errMsg,
                confirmButtonClass: 'btn btn-confirm mt-2',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: "Yes",
                cancelButtonText: "No",
            })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    btnSimpan.dataset.confirmed = true;
                    btnSimpan.click();
                  
                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
        }
        } 
        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            {
                $('#<%=txtKeterangan.ClientID%>').prop('maxlength', 50);
            }
            
            $('#txttgljthtmp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttgltrn').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });  
            $('.selectpicker').selectpicker();
            $('#datatable').DataTable();

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
                var tableX = $('#GRRP').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });

                var table3 = $('#GRDMKT').DataTable({
                    "order": [],
                    "paging": false,
                    buttons: ['copy', 'excel', 'pdf'],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    screenX: true
                    //,
                    //ordering: [[0, 'asc']],
                    //"scrollX": true,
                    //retrieve: true,
                    //onpageshow: true,
                    //AllowPaging=false


                });
                var table4 = $('#GRDMKTINFO').DataTable({
                    "order": [],
                    "paging": false,
                    buttons: ['copy', 'excel', 'pdf'],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    screenX: true
                    //,
                    //ordering: [[0, 'asc']],
                    //"scrollX": true,
                    //retrieve: true,
                    //onpageshow: true,
                    //AllowPaging=false


                });
                var table5 = $('#GRDKU').DataTable({
                    "order": [],
                    "paging": false,
                    buttons: ['copy', 'excel', 'pdf'],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    screenX: true
                    //,
                    //ordering: [[0, 'asc']],
                    //"scrollX": true,
                    //retrieve: true,
                    //onpageshow: true,
                    //AllowPaging=false


                });
                var table6 = $('#GRDBO').DataTable({
                    "order": [],
                    "paging": false,
                    buttons: ['copy', 'excel', 'pdf'],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    screenX: true
                    //,
                    //ordering: [[0, 'asc']],
                    //"scrollX": true,
                    //retrieve: true,
                    //onpageshow: true,
                    //AllowPaging=false


                });

                var table7 = $('#GRDOB').DataTable({
                    "order": [],
                    "paging": false,
                    buttons: ['copy', 'excel', 'pdf'],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    screenX: true
                    //,
                    //ordering: [[0, 'asc']],
                    //"scrollX": true,
                    //retrieve: true,
                    //onpageshow: true,
                    //AllowPaging=false


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

                
             
                table3.buttons().container().appendTo('#GRDMKT_wrapper .col-md-6:eq(0)');
                table4.buttons().container().appendTo('#GRDMKTINFO_wrapper .col-md-6:eq(0)');
                table5.buttons().container().appendTo('#GRDKU_wrapper .col-md-6:eq(0)');
                table6.buttons().container().appendTo('#GRDBO_wrapper .col-md-6:eq(0)');
                table7.buttons().container().appendTo('#GRDOB_wrapper .col-md-6:eq(0)');
                $("div").remove(".tooltip");
                $.getScript("/assets/js/jquery.core.js");


                $('#LnkAdd, #lnkCari, #lnkSaveGR,#btnsearch').tooltipster({
                    animation: 'grow'
                });
                $('#LnkAdd, #lnkCari, #lnkSaveGR,#btncari').tooltipster({
                    animation: 'grow'
                });
                //var tableX = $('#GRDKU').dataTable({
                //    "order": [],
                //    "columnDefs": [{
                //        "targets": 'no-sort',
                //        "orderable": false,
                //    }],
                //    ordering: [[0, 'asc']],
                //    "scrollX": true,
                //    retrieve: true

                //});
                //var tableX = $('#GRDMK').dataTable({
                //    "order": [],
                //    "columnDefs": [{
                //        "targets": 'no-sort',
                //        "orderable": false,
                //    }],
                //    ordering: [[0, 'asc']],
                //    "scrollX": true,
                //    retrieve: true

                //});

            });

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
                                        <li class="breadcrumb-item"><a href="#">Account Employee</a></li>
                                        <li class="breadcrumb-item"><a href="#">Transaction</a></li>
                                        <li class="breadcrumb-item"><a href="TrnPinjaman.aspx">Approval Pusat Reward & Fee Non Management Cabang</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Approval Pusat Reward & Fee Non Management Cabang</h4>
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
                     <!-- Panel Header -->
                    <asp:Panel ID="pnlHeader" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                   <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                    <%--<asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Reward" OnClick="btnAdd_Click" />--%>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                     <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                     <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Reload Data" OnClick="btnreload_Click" />
                                    <h4 class="m-t-0 header-title">Data Pengajuan Fee & Reward  Cabang</h4>                           
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No Bukti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Cabang">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabangnama")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField visible="false">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblkodecabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodecabang")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bulan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBulan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Bulan")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tahun">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTahun" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Tahun")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                     
                                                    <asp:TemplateField HeaderText="Tanggal Transaksi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     
                                                    <asp:TemplateField HeaderText="Jumlah Collection">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJlhcollection" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jlhcollection","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>                                     
                                                     <asp:TemplateField HeaderText="Jumlah Fee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJlhfeepersen" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "feepersen","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Jumlah PL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJlhPL" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhPL","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    
                                                               <asp:TemplateField HeaderText="Type status " Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusmemo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Note Memo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnotememo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notememo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="100px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamastatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namastatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>    
                                                    <asp:TemplateField HeaderText="Verifikasi Manager" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrnMC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateAddedmc","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Memo Verifikasi Manager" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmemoapprovemc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notemc")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                    
                                                    <asp:TemplateField HeaderText="Approve Pusat" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrnAP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateAddedAP","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Memo Approve Pusat" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmemoapprove" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "noteAP")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
 
                                      
                                    


                                                    
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin Melihat data ini ?');" title="View" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnSlip" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Slip" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnPDF" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div> 
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                     <!-- Panel Edit -->
                    
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Entry Data</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                <div class="form-group">
                                                    <asp:HiddenField Visible="false" ID="Hdnkodecabang" runat="server" />
                                                    <label for="inputpid" class="col-3 col-form-label">No. Bukti*</label>
                                                    <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox> 
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Transaksi</label>
                                                    <asp:TextBox ID="txttgltrn" Enabled="false" CssClass="form-control"  runat="server" MaxLength="10"  readonly="true" ></asp:TextBox>      
                                                </div>

                                                <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Bulan</label>
                                          <asp:DropDownList ID="ddlSP" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom" >
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
                                        <label for="inputCity" class="col-form-label">Tahun</label>
                                          <asp:DropDownList ID="ddltahun" Enabled="false" runat="server"  CssClass="form-control" data-style="btn-custom" >
                                                        <asp:ListItem Value="2020">2020</asp:ListItem>
                                                        <asp:ListItem Value="2021">2021</asp:ListItem>
                                                        <asp:ListItem Value="2022">2022</asp:ListItem>
                                                        <asp:ListItem Value="2023">2023</asp:ListItem>
                                                        <asp:ListItem Value="2024">2024</asp:ListItem>
                                                        <asp:ListItem Value="2025">2025</asp:ListItem>
                                           </asp:DropDownList>        
                                    </div>


                                </div>
                                
                                                <div class="form-row">
                                                    <div class="form-group col-md-4">
                                                       <label for="inputZip" class="col-10 col-form-label">Jumlah Collection</label>
                                                       <asp:TextBox ID="txtjumlahcollection" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    
                                                    </div>
                                                     <div class="form-group col-md-4" runat="server" visible="false">
                                                       <label for="inputZip" class="col-10 col-form-label">Fee Management %</label>
                                                       <asp:TextBox ID="txtfeemanagement" Text="0" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    
                                                    </div>
                                                     <div class="form-group col-md-4">
                                                       <label for="inputZip" class="col-10 col-form-label">P&L Bulan ini</label>
                                                       <asp:TextBox ID="txtPL" CssClass="form-control" runat="server" MaxLength="20"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    
                                                    </div>

                                                     <div class="form-group col-md-4">
                                                       <label for="inputZip" class="col-10 col-form-label">PL>100</label>
                                                       <asp:TextBox ID="txtplreward" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    
                                                    </div>
                                                     <div class="form-group col-md-4">
                                                       <label for="inputZip" class="col-10 col-form-label">Tambahan PL</label>
                                                       <asp:TextBox ID="txttambahanPLreward" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    
                                                    </div>

                                                </div>
                                                
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-3 col-form-label">Keterangan</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtKeterangan" MaxLength="100"  required="required"  runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </div>

                                                 <div class="form-group">
                                                        <asp:Button ID="btngenerate"  class="btn btn-success waves-light text-white btn-lg" runat="server" Text="Proses Fee & Reward" OnClick="btnGenerate_Click" />
                                                    </div>
                                            
                                                
                                            </div>                                                    
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                                <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Karyawan Cabang</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                    <div class="form-row">
                                                <div class="col-md-4">
                                                    <label for="inputCity" class="col-form-label">Management</label>
                                                    <%--<asp:DropDownList ID="ddlkdsup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtManagement" Enabled="false" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox> 
                                                </div>
                                                <div class="col-md-4">
                                                    <label for="inputpid" class="col-form-label">Non Management</label>
                                                    <asp:TextBox ID="txtnonManagement" Enabled="false" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <label for="inputpid" class="col-form-label">Total Karyawan</label>
                                                    <asp:TextBox ID="txttotalkaryawan" Enabled="false" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                </div>
                                                </div>
                                                    <p class="mb-1 mt-4 font-weight-bold ">Detail Non Management</p>
                                                <div class="form-row">
                                                
                                                            <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Back Office</p>
                                                            <asp:TextBox ID="txtBO" Enabled="false" CssClass="form-control" required="required" runat="server"  ></asp:TextBox>  
                                                            </div>
                                                                <div class="col-md-4">
                                                                <p class="mb-1 mt-4 font-weight-bold ">Office Boy & Security</p>
                                                                <asp:TextBox ID="txtofficeboy" Enabled="false" CssClass="form-control" required="required" runat="server"  ></asp:TextBox>  
                                                                </div>
                                                                <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kepala Unit / Pengawas</p>
                                                             <asp:TextBox ID="txtku" Enabled="false" CssClass="form-control" required="required" runat="server"  ></asp:TextBox>  
                                                            </div>   
                                                            <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Staf Marketing/Pemasaran</p>
                                                             <asp:TextBox ID="txtmarketing" Enabled="false" CssClass="form-control" required="required" runat="server"  ></asp:TextBox>  
                                                            </div> 
                                                </div>
                                               
                                                    
                                                    
                                            </div>                                                    
                                        </div>
                                    </div>
                                    
                                    
                                </div>      
                            </div>
                        </div>
                    </asp:Panel>
                   
                    
                    
                    
                    
                    <asp:Panel ID="PnlManagement" Visible="false" runat="server">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="card-box"> 
                                     <h4 class="Page-title">Data Management</h4>
                                     

                                     <div class="form-row">
                                         <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRRP" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowDataBound="GRRP_RowDataBound" ShowFooter="True" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                   
                                                     <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="NIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeNik")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Nama">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaKry")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Kode Jabatan" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodejab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jabatan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Jabatan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketjab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJab")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="AVERAGE & KPI">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaverage" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalaverage","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Fee Marketing %">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txtpsnfee" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "feemrk", "{0:N0}")%>' OnTextChanged="Jlhfeemanagement_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="Total BM">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBM" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        <asp:Label ID="lbltotalBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 


                                                   <asp:TemplateField HeaderText="P&L >100">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txtpl" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plreward", "{0:N0}")%>' OnTextChanged="JlhrewardPLmanagement_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>
                                                       <FooterTemplate>
                                                        <asp:Label ID="lbltotalPL" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                 
                                                     <asp:TemplateField HeaderText="TOTAL REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblReward"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalreward","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalReward" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="TOTAL BM + REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblBMReward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalrewardbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalRewardBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                </Columns>
                                            </asp:GridView>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                    </asp:Panel> 
                    
                    <asp:Panel ID="PNLBO" Visible="false" runat="server">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="card-box"> 
                                     <h4 class="Page-title">Data Back Office</h4>
                                       <asp:TextBox ID="txtotalbmbo" Visible="false" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox> 
                                       <asp:TextBox ID="txttotalrewardbo" Visible="false" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    


                                     <div class="form-row">
                                         <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRDBO" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRDBO_RowDataBound" ShowFooter="True">
                                                <Columns>
                                                   
                                                     <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="NIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeNik")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Nama">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaKry")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Kode Jabatan" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodejab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jabatan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Jabatan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketjab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJab")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="AVERAGE & KPI">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaverage" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalaverage","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Fee Marketing %">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpsnfee" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "feemrk", "{0:N3}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="Total BM">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBM" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltotalBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                   <asp:TemplateField HeaderText="P&L >100">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpl" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plreward", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalPL" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                 
                                                     <asp:TemplateField HeaderText="TOTAL REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblReward"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalreward","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalReward" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="TOTAL BM + REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblBMReward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalrewardbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                            <FooterTemplate>
                                                        <asp:Label ID="lbltotalRewardBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                </Columns>
                                            </asp:GridView>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                    </asp:Panel>
                    
                    
                    <asp:Panel ID="PNLOB" Visible="false" runat="server">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="card-box"> 
                                     <h4 class="Page-title">Data Office Boy & Security</h4>
                                       <asp:TextBox ID="txtotalbmob" Visible="false" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox> 
                                       <asp:TextBox ID="txttotalrewardob" Visible="false" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    


                                     <div class="form-row">
                                         <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRDOB" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRDOB_RowDataBound" ShowFooter="True">
                                                <Columns>
                                                   
                                                     <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="NIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeNik")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Nama">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaKry")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Kode Jabatan" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodejab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jabatan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Jabatan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketjab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJab")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="AVERAGE & KPI">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaverage" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalaverage","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Fee Marketing %">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpsnfee" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "feemrk", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="Total BM">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBM" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbm","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltotalBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                   <asp:TemplateField HeaderText="P&L >100">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpl" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plreward", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalPL" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                 
                                                     <asp:TemplateField HeaderText="TOTAL REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblReward"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalreward","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalReward" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="TOTAL BM + REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblBMReward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalrewardbm","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        <asp:Label ID="lbltotalRewardBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                </Columns>
                                            </asp:GridView>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                    </asp:Panel>
                    

                     <asp:Panel ID="PNLKU" Visible="false" runat="server">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="card-box"> 
                                     <h4 class="Page-title">Data Kepala Unit / Pengawas</h4>
                                       <asp:TextBox ID="txtotalbmku" Visible="false" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox> 
                                       <asp:TextBox ID="txttotalrewardku" Visible="false" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    


                                     <div class="form-row">
                                         <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRDKU" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRDKU_RowDataBound" ShowFooter="True">
                                                <Columns>
                                                   
                                                     <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="NIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeNik")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Nama">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaKry")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Kode Jabatan" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodejab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jabatan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Jabatan" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketjab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJab")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="AVERAGE & KPI">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="txtaverage"  Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalaverage", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltotalVG" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Fee Marketing %">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpsnfee" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "feemrk", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="Total Fee BM">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBM" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltotalBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                   <asp:TemplateField HeaderText="Reward P&L">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpl" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plreward", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalPL" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Total Reward SM">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtnewmembers" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NM", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalnewmembers" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Total SM">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtKMK" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalsales", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalKMK" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                   <asp:TemplateField HeaderText="Reward KU">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtTP" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TP", "{0:N0}")%>'></asp:Label>
                                                         
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalTunggakan" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="KU TERBAIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtSMTErbaik" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTERBAIK", "{0:N0}")%>'></asp:Label>
                                                             <asp:Label ID="lblterbaik" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "urutterbaik")%>'></asp:Label>
                                                               <asp:Label ID="lbltotalsm" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalku")%>'></asp:Label>
                                                            
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltotalSMTERBAIK" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                 
                                                     <asp:TemplateField HeaderText="TOTAL REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblReward"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalreward","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalReward" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="TOTAL BM + REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblBMReward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalrewardbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                            <FooterTemplate>
                                                        <asp:Label ID="lbltotalRewardBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                </Columns>
                                            </asp:GridView>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="PNLMKTINFO" Visible="false" runat="server">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="card-box"> 
                                     <h4 class="Page-title">Data Storting/KMK/Tunggakan Staf Marketing / Pemasaran</h4>
                                     

                                     <div class="form-row">
                                         <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRDMKTINFO" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  ShowFooter="False">
                                                <Columns>
                                                   
                                                     <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="NIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeNik")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Nama Karyawan">
                                                        <HeaderStyle CssClass="align-Left" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaKry")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                    </asp:TemplateField> 
                                                 
                                                    <asp:TemplateField HeaderText="Storting">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="txtaverage" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalaverage", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Anggota Baru">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblanggotabaru" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "anggotabaru","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="Anggota Aktif">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblanggotaaktif" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "anggotaaktif","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                       <asp:TemplateField HeaderText="KMK Bulan ini">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblBakidb" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Bakidb","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="KMK Bulan Lalu">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblbakibulanlalu" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakibulanlalu","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Net KMK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblKMKNET" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "netbakidb","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="Tunggakan Bulan Ini">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="lbltunggakanPokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tunggakanPokok","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                   <asp:TemplateField HeaderText="Tunggakan Bulan Lalu">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                              <asp:Label ID="lbltunggakanbulanlalu" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tunggakanbulanlalu","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Net Tunggakan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                              <asp:Label ID="lbltunggakannet" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nettunggakan","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 

                                                       
                                                </Columns>
                                            </asp:GridView>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                    </asp:Panel>

                    <asp:Panel ID="PNLMKT" Visible="false" runat="server">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="card-box"> 
                                     <h4 class="Page-title">Data Staf Marketing / Pemasaran</h4>
                                       <asp:TextBox ID="txtotalbmmkt" Visible="false" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox> 
                                       <asp:TextBox ID="txttotalrewardmkt" Visible="false" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    


                                     <div class="form-row">
                                         <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRDMKT" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRDMKT_RowDataBound" ShowFooter="True">
                                                <Columns>
                                                   
                                                     <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="NIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeNik")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Nama Karyawan">
                                                        <HeaderStyle CssClass="align-Left" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaKry")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    
                                                     <asp:TemplateField HeaderText="Kode KU">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodeKU" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bos_id")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Kode Jabatan" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodejab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jabatan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Jabatan" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketjab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJab")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="AVERAGE & KPI">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="txtaverage" Enabled="false"   runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalaverage", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalAVG" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Fee Marketing %">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpsnfee" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "feemrk", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="Total BM">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBM" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltotalBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                   <asp:TemplateField HeaderText="Reward P&L">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpl" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plreward", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalPL" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Reward New Members">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtnewmembers" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NM", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalnewmembers" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Reward K M K">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtKMK" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KMK", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalKMK" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                   <asp:TemplateField HeaderText="Tunggakan Pokok (TP)">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtTP" Enabled="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TP", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalTunggakan" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="SM/KU TERBAIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtSMTErbaik" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTERBAIK", "{0:N0}")%>'></asp:Label>
                                                          <asp:Label ID="lblterbaik" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "urutterbaik")%>'></asp:Label>
                                                          <asp:Label ID="lbltotalsm" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalsm")%>'></asp:Label>
                                                            
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltotalSMTERBAIK" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                 
                                                     <asp:TemplateField HeaderText="TOTAL REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblReward"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalreward","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalReward" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="TOTAL BM + REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblBMReward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalrewardbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                            <FooterTemplate>
                                                        <asp:Label ID="lbltotalRewardBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                   
                                                      <asp:TemplateField HeaderText="Data" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblTotalStorting" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalaverage","{0:N0}")%>'></asp:Label>--%>
                                                            <asp:Label ID="lblanggotabaru" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "anggotabaru","{0:N0}")%>'></asp:Label>
                                                            <asp:Label ID="lblanggotaaktif" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "anggotaaktif","{0:N0}")%>'></asp:Label>
                                                            <asp:Label ID="lblBakidb" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Bakidb","{0:N0}")%>'></asp:Label>
                                                            <asp:Label ID="lblbakibulanlalu" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakibulanlalu","{0:N0}")%>'></asp:Label>
                                                            <asp:Label ID="lbltunggakanPokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tunggakanPokok","{0:N0}")%>'></asp:Label>
                                                           <asp:Label ID="lbltunggakanbulanlalu" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tunggakanbulanlalu","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          
                                                    </asp:TemplateField> 
                                                </Columns>
                                            </asp:GridView>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                    </asp:Panel>
                    
                    

                    <asp:Panel ID="pnlmemo" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">
                            <asp:HiddenField ID="Hdnnik" runat="server" />
                            <h4 class="m-t-0 header-title">Catatan Pengajuan</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                                  
                                                
                                                 <div class="form-group col-md-6">
                                                           <div class="form-group">
                                                    <label class="col-form-label">Catatan Memo Pengajuan Cabang</label>
                                                    <asp:TextBox CssClass="auto-style2" Enabled="false" ID="txtnote" MaxLength="150" runat="server" TextMode="MultiLine" Height="145px" Width="595px"></asp:TextBox>
                                                    </div>

                                                </div> 
                                                <div class="form-group col-md-6" runat="server" visible="false">
                                                     <div class="form-group">
                                                    <label class="col-form-label">Catatan Verifikasi Memo Pengajuan Cabang</label>
                                                    <asp:TextBox CssClass="auto-style2" Enabled="false" ID="txtnoteverifikasi" MaxLength="150" runat="server" TextMode="MultiLine" Height="145px" Width="595px"></asp:TextBox>
                                                    </div>

                                                    
                                                </div> 
                                                <div class="form-group col-md-6">
                                                      <div class="form-group">
                                                    <label class="col-form-label">Catatan Pusat Memo Pengajuan Cabang</label>
                                                    <asp:TextBox CssClass="auto-style2" ID="txtnotepusat" MaxLength="150" runat="server" TextMode="MultiLine" Height="145px" Width="595px"></asp:TextBox>
                                                    </div>
                                                        
                                                    <div class="card-box table-responsive">
                                                        <asp:Button ID="BtnApprove" class="btn btn-success waves-light text-white" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Approve data ini ?');" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                                                        <asp:Button ID="BtnTolak" class="btn btn-success waves-light text-white" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Tolak data ini ?');" runat="server" Text="Tolak" OnClick="btnTolak_Click" />
                                                    </div>  
                                                
                                                </div>
                                            </div>           
                                </div>
                            </div> 

                        </div>
                    </div>
                </div>
                </div>
        </asp:Panel>


                    <asp:Panel ID="Pnlpage1" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="card-box table-responsive">
                                         <asp:Button ID="btnnext1" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnnext1_Click" Text="Next" Width="65px" />
                                       
                                       
                                      
                                         <asp:Button ID="btnnext2" visible="false" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnnext2_Click" Text="Next" Width="65px" />

                                        <asp:Button ID="btnback2"  visible="false" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnback2_Click" Text="Back" Width="65px" />
                                    </div>
                                </div>
                            </div>
                        </div> 
                    </asp:Panel>


                    <asp:Panel ID="Pnlfooter4" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="card-box table-responsive">
                                        <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btndelete" CausesValidation="TRUE" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');" CssClass="btn btn-success waves-light text-white" runat="server" Text="Delete" OnClick="btndelete_click" Enabled="true"/>
                                        <asp:Button ID="btnback3" visible="false" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnback3_Click" Text="Back" Width="65px" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnCancel_Click" Text="close" Width="65px" />
                                    </div>
                                </div>
                            </div>
                        </div> 
                    </asp:Panel>
                </div> 
            <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
               
                <asp:PostBackTrigger ControlID="btnAdd" />   
                <asp:PostBackTrigger ControlID="GRData" />                
               
                <asp:PostBackTrigger ControlID="btngenerate" />
                <asp:PostBackTrigger ControlID="GRDKU" />
                <asp:PostBackTrigger ControlID="GRDMKT" />
                <asp:PostBackTrigger ControlID="btnback3" />
                 <asp:PostBackTrigger ControlID="btnback2" />
                 <asp:PostBackTrigger ControlID="btnnext2" />
                 <asp:PostBackTrigger ControlID="btnnext1" />
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



