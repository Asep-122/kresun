<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransaksiPerjalananDinasKembaliBayar.aspx.cs" Inherits="OE_Process_TransaksiPerjalananDinasKembaliBayar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pembayaran Pengembalian Kasbon Biaya Perjalanan Dinas</title>
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

    <style>
        html, body {
            min-height: 100%;
            height: auto !important;
            overflow-y: auto !important;
            overflow-x: hidden;
        }
        .container-fluid, .page-content, form {
            height: auto !important;
            overflow: visible !important;
        }
    </style>

    <script type="text/javascript">
        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
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
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }

                        if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50);
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

        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

        //function Resize() {
        //    setTimeout(function (e) {
        //        //  alert('z');
        //        var y;
        //        y = window.parent.document.getElementById('txtScrollY').value;
        //        setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
        //    }, 50);
        //}

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

        function sweetAlertConfirm(btndelete, errMsg) {
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
                    cancelButtonClass: 'btn btn-confirm mt-2',
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

        function sweetAlertsave(btnSimpan, errMsg) {
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
                    cancelButtonClass: 'btn btn-confirm mt-2',
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
                $('#<%=txtnote.ClientID%>').prop('maxlength', 50);

            }

            //Resize();

            $('#txtTglL').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgltrnx').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('.selectpicker').selectpicker();

            // $('.selectpicker').selectpicker('refresh');

            // alert('');

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

                //var tableX = $('#GRRP').dataTable({
                //    "order": [],
                //    "columnDefs": [{
                //        "targets": 'no-sort',
                //        "orderable": false,
                //    }],
                //    ordering: [[0, 'asc']],
                //    "scrollX": true,
                //    retrieve: true
                //
                //});

            });

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();


            $("#txtcustomer").autocomplete({
                source: function (request, response) {

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "BiayaPerjalananDinasKembali.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                        dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[1],
                                    HDNBUKTI: item[0]
                                }
                            });

                            $("[id$=HDNBUKTI]").val('');


                            response(found);
                        },

                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=HDNBUKTI]").val(i.item.HDNBUKTI);
                },
                minLength: 3
            });

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
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">General Ledger</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="#">Pembayaran Pengembalian Kasbon Biaya Perjalanan Dinas</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Pembayaran Pengembalian Kasbon Biaya Perjalanan Dinas</h4>
                            </div>
                        </div>
                    </div>

                    <!-- Panel Filter -->
                    <%--<asp:Panel ID="pnlFilter" Visible="false" runat="server">
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
                    </asp:Panel>--%>

                    <!-- Panel Header -->
                    <asp:Panel ID="pnlHeader" runat="server">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Reload" OnClick="btnAdd_Click" />

                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    

                                    <%--<h4 class="m-t-0 header-title">Transaksi Pengembalian Kasbon Biaya</h4>--%>
                                    <h4 class="header-title">Pembayaran Pengembalian Kasbon Biaya Perjalanan Dinas</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">

                                            <asp:GridView ID="GRData" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" OnRowDataBound="GRData_RowDataBound" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                               
                                                    <asp:TemplateField HeaderText="No Bukti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>                                                          
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                                        </FooterTemplate>
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

                                                    <asp:TemplateField HeaderText="PIC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpic" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pic")%>'></asp:Label>
                                                            <asp:Label ID="lblanggota" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "anggota")%>'></asp:Label>
                                                            <asp:Label ID="lblpicname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_pic")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Keterangan" >
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblnote"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "noteadvance")%>'></asp:Label>
                                                            <asp:Label ID="lblKeterangan"  Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                        <asp:Label ID="lblNoteApprovalPusat"  Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoteApprovalPusat")%>'></asp:Label>
                                                        
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Jumlah">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblddlpembayaran" CssClass="" runat="server" Text='<%#Eval("JumlahTrn","{0:N2}")%>'></asp:Label>
                                                            <asp:Label ID="lbltambahanbiaya" Visible="false" CssClass="" runat="server" Text='<%#Eval("TambahanBiaya","{0:N2}")%>'></asp:Label>
                                                            <asp:Label ID="lblPengembalianbiaya" Visible="false" CssClass="" runat="server" Text='<%#Eval("JumlahKembali","{0:N2}")%>'></asp:Label>
                                                      
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltxttotals" runat="server" Text="TOTAL" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Total Biaya">
                                                        <ItemTemplate>
                                                         
                                                                                                                   
                                                            <asp:Label ID="lblTotalbiaya"  CssClass="" runat="server" Text='<%#Eval("jumahbiaya","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                        
                                                    </asp:TemplateField>

                                                 <%--    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamastatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namastatus")%>'></asp:Label>
                                                             <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     
                                                     <asp:TemplateField HeaderText="Memo Approve">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoteApprovalPusat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoteApprovalPusat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Tgl Approve date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglapprovepst" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglApprovalPusat","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Tgl bayar Teller">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglapprove" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTellerBayar","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>--%>
                                                 
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin Approve  data ini ?');" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <%--Tidak buat slip karena tidak insert ke CLTrn--%>
                                                            <%--<asp:LinkButton ID="BtnSlip" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Slip" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>--%>
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

                    <asp:Panel ID="Pnlcustomer" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">

                                    <div class="col-12">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">Pencarian Bukti Kasbon Biaya</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <label class="col-form-label">Entry Data Bukti</label>
                                                <asp:TextBox CssClass="form-control" ID="txtcustomer" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Select" OnClick="btnSerach_Click" />
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Panel Edit -->
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box">
                                    <div class="form-row">
                                        <asp:HiddenField ID="HDNBUKTI" runat="server" />
                                        <asp:HiddenField ID="HdnKodeCabang" runat="server" />
                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <label class="col-form-label">No. Bukti</label>
                                                <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                                <asp:TextBox Visible="false" CssClass="form-control" ID="hdnnourut" MaxLength="30" runat="server" data-parsley-type="string" parsley-trigger="change" required="required" data-parsley-maxlength="30" data-parsley-error-message="Huruf  Max 30 Digit"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-form-label">Date Transaction</label>

                                                <div class="input-group-append">
                                                    <span class="input-group-text"><i class="mdi mdi-calendar"></i></span>
                                                    <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="txtTglL" TextMode="DateTime" runat="server" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="row">
                                                  <div class="form-group col-md-6">
                                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Bayar</label>
                                                    <asp:TextBox ID="txttglbyr" CssClass="form-control" required="required" runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-6">
                                                    <label for="inputCity" class="col-form-label">Kode Bank</label>
                                                    <asp:DropDownList ID="ddlKdBank" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                </div>
                                                     </div>

                                            <div class="form-group">
                                                <label class="col-form-label">Note Pengembalian</label>
                                                <asp:TextBox CssClass="form-control" ID="txtnote" MaxLength="50" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-form-label">PIC</label>
                                                <asp:DropDownList ID="ddlPIC" Enabled="false" runat="server" CssClass="show-tick form-control"></asp:DropDownList>
                                            </div>

                                            <asp:Panel ID="pnlAnggota" runat="server" Visible="true" Enabled="false">

                                                <div class="form-group mb-2">
                                                    <div class="d-flex align-items-center justify-content-between">
                                                        <label class="col-form-label mb-0">Anggota</label>
                                                        <asp:LinkButton
                                                            ID="btnAddAnggota"
                                                            runat="server"
                                                            CssClass="btn btn-sm btn-outline-success d-flex align-items-center"
                                                            OnClick="btnAddAnggota_Click"
                                                            ToolTip="Tambah Anggota"
                                                            Visible="false">
                                                            <i class="mdi mdi-plus-circle-outline me-1"></i> Tambah
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                                <asp:GridView
                                                    ID="GridViewAnggota"
                                                    runat="server"
                                                    AutoGenerateColumns="False"
                                                    CssClass="table table-borderless align-middle w-100"
                                                    OnRowCommand="GridViewAnggota_RowCommand"
                                                    OnRowDataBound="GridViewAnggota_RowDataBound"
                                                    ShowHeader="False"
                                                    GridLines="None"
                                                    AllowPaging="false">

                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:DropDownList
                                                                    ID="ddlAnggota"
                                                                    runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="False">
                                                            <ItemStyle HorizontalAlign="Center" Width="45px" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton
                                                                    ID="btnDelAnggota"
                                                                    runat="server"
                                                                    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                                    CommandName="DEL"
                                                                    CssClass="btn btn-outline-danger">
                                                                    <i class="mdi mdi-close-circle-outline"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>

                                        </div>
                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <label class="col-form-label">Note Kasbon Biaya</label>
                                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtnotekasbon" MaxLength="50" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="form-group">

                                                <label for="inputCity" class="col-form-label">Jumlah Kasbon</label>
                                                <asp:TextBox ID="txttransAP" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">

                                                <label for="inputCity" class="col-form-label">Tambahan Biaya</label>
                                                <asp:TextBox ID="txttambahanbiaya" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" AutoPostBack="true" OnTextChanged="txtjlhbayar_TextChanged"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <label for="inputCity" class="col-form-label">Jumlah Pengembalian</label>
                                                <asp:TextBox ID="txtpengembalian" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" AutoPostBack="true" OnTextChanged="txtjlhbayar_TextChanged"></asp:TextBox>

                                            </div>

                                            <div class="form-group">

                                                <label for="inputCity" class="col-form-label">Jumlah Biaya</label>
                                                <asp:TextBox ID="txtbiaya" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txttotal" Visible="false" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                            </div>
                                               <div class="form-group">
                                                    <label class="col-3 col-form-label">Note Approval</label>
                                                    <asp:TextBox ID="txtNoteApproval" Enabled="false" CssClass="form-control" MaxLength="50" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </asp:Panel>

                    <asp:Panel ID="PnlJournal" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="card-box table-responsive">
                                        <asp:Button ID="Btnnextjournal" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="Btnnextjournal_Click" Width="65px" />
                                        <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="PnlGrid" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">

                                    <h4 class="m-t-0 header-title">Biaya Transaction</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <div class="card-box table-responsive">
                                                <asp:Button ID="Button1" Visible="false" class="btn btn-success waves-light text-white" runat="server" Text="Add Detail" OnClick="lnkBT_Click" />
                                            </div>

                                            <asp:GridView ID="GRRP" ShowFooter="True" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRRP_RowCommand" OnRowDataBound="GRRP_RowDataBound" ShowHeaderWhenEmpty="True" AllowPaging="false">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Account code">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <div class="form-group">
                                                                <div class="form-line">
                                                                    <asp:DropDownList ID="ddlKodeGL" Enabled="false" runat="server" CssClass="show-tick form-control"></asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnddlKodeGL" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "KodeGL")%>' />
                                                                    <asp:Label ID="lblNumber" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nomor")%>'></asp:Label>
                                                                    <asp:Label ID="lblnobuktix" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Namagl")%>'></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Jumlah">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <div class="form-group">
                                                                <div class="form-line">
                                                                    <asp:TextBox ID="jlhdebit" enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" AutoPostBack="true" OnTextChanged="XXXTxt012_TextChanged" Text='<%#DataBinder.Eval(Container.DataItem, "jlhdebit", "{0:N2}")%>'></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltxttotalsx" runat="server" Text="TOTAL" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btndelete2" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DEL" data-placement="left" data-toggle="tooltip" > <i class="btn-outline-danger">delete</i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerSettings PageButtonCount="50" />
                                            </asp:GridView>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                         

                    </asp:Panel>

                    <asp:Panel ID="Pnlfooter" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="card-box table-responsive">
                                        <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Bayar" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Bayar data ini ?');" OnClick="btnSimpan_Click" />
                                      
                                        <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />

                                    </div>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>

                    <%--<asp:Panel ID="PnlReport" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="card-box table-responsive">
                                        <asp:LinkButton ID="LinkButton3" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader2_Click" Width="65px" />
                                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>--%>

            </ContentTemplate>
            <Triggers>
                    <asp:PostBackTrigger ControlID="btnSimpan" />

                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:AsyncPostBackTrigger ControlID="button1" />
                <asp:PostBackTrigger ControlID="GRData" />

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

<%--<script>
    function ensureScrollVisible() {
        $('html, body').css({ height: 'auto', overflowY: 'auto' });
        window.dispatchEvent(new Event('resize'));
    }

    $(document).ready(ensureScrollVisible);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ensureScrollVisible);

    // simpan posisi scroll sebelum postback
    window.addEventListener("beforeunload", function () {
        localStorage.setItem("scrollY", window.scrollY);
    });

    // setelah load halaman, kembalikan ke posisi semula
    window.addEventListener("load", function () {
        const y = localStorage.getItem("scrollY");
        if (y !== null) {
            window.scrollTo(0, parseInt(y));
            localStorage.removeItem("scrollY");
        }
    });
</script>--%>

</html>
