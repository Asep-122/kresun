<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiayaPerjalananDinas.aspx.cs" Inherits="OE_Process_MemoDinas_BiayaPerjalananDinas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Perjalanan Dinas</title>
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
            else if (e == '') {
                x = Number(document.getElementById('txttransAP').value);
                y = Number(obj.value);
            }
            z = x * y;
            s = z / v;
            document.getElementById('txtjmlhGL').value = z;
            document.getElementById('hfjmlrp').value = z;
            document.getElementById('txtjmlhCL').value = s;
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
                $('#<%=txtKeterangan.ClientID%>').prop('maxlength', 100);
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
            $('#txttgltrnx').datepicker({
                format: 'dd-mm-yyyy',
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
                var tableX = $('#GRTP').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var tableX = $('#Grduangmuka').dataTable({
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
                                        <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                        <li class="breadcrumb-item"><a href="#">Master</a></li>
                                        <li class="breadcrumb-item"><a href="#">Perjalanan Dinas</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Transaksi Perjalanan Dinas</h4>
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
                    <asp:Panel ID="pnlHeader" runat="server">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Transaksi" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    
                                    <h4 class="m-t-0 header-title">Transaksi Perjalanan Dinas</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">

                                            <asp:GridView ID="GRData" runat="server" ShowFooter="True" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" OnRowDataBound="GRData_RowDataBound" AutoGenerateColumns="False" Width="625px">
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
                                                               <asp:Label ID="lblnote" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NotePengajuan")%>'></asp:Label>
                                                            <asp:Label ID="lblKeterangan" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Jumlah">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblddlpembayaran" CssClass="" runat="server" Text='<%#Eval("JumlahTrn","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltxttotals" runat="server" Text="TOTAL" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Status">
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
                                                    </asp:TemplateField>
                                                 
                                                   
                                           

                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <%--Tidak buat slip karena tidak insert ke CLTrn--%>
                                                            <%--<asp:LinkButton ID="BtnSlip" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Slip" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="BtnPDF" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
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
                                                    <label for="inputpid" class="col-3 col-form-label">No. Bukti*</label>
                                                    <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                                    <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                    <asp:HiddenField ID="HDNoSebelumnya" runat="server" />
                                                    <asp:HiddenField ID="HDNlasttransaksiap" runat="server" />
                                                    <asp:HiddenField ID="HDNlasttransaksicl" runat="server" />
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Transaksi</label>
                                                    <asp:TextBox ID="txttgltrn" CssClass="form-control" required="required" runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputpid" class="col-3 col-form-label">Keterangan</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtKeterangan" required="required" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputpid" class="col-3 col-form-label">Catatan</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtNote" MaxLength="500" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputCity" class="col-form-label">Jumlah Transaksi</label>
                                                    <asp:TextBox ID="txttransAP" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                                                               

                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Pelaksana</div>
                                                <p class="text-muted font-14 m-b-30"></p>

                                                <div class="form-group">

                                                    <div class="form-group">
                                                        <label class="col-form-label">PIC</label>
                                                        <asp:DropDownList ID="ddlPIC" runat="server" CssClass="show-tick form-control"></asp:DropDownList>
                                                    </div>

                                                    <div class="form-group mb-2">
                                                        <div class="d-flex align-items-center justify-content-between">
                                                            <label class="col-form-label mb-0">Anggota</label>
                                                            <asp:LinkButton
                                                                ID="btnAddAnggota"
                                                                runat="server"
                                                                CssClass="btn btn-sm btn-outline-success d-flex align-items-center"
                                                                OnClick="btnAddAnggota_Click"
                                                                ToolTip="Tambah Anggota">
                                                        <i class="mdi mdi-plus-circle-outline me-1"></i> Tambah
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                    <asp:Panel ID="pnlAnggota" runat="server" Visible="true">
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


                                                                <asp:TemplateField>
                                                                    <ItemStyle HorizontalAlign="Center" Width="45px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton
                                                                            ID="btnDelAnggota"
                                                                            runat="server"
                                                                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                                            CommandName="DEL"
                                                                            CssClass="btn btn-outline-danger"
                                                                            ToolTip="Hapus Anggota">
                                                                    <i class="mdi mdi-close-circle-outline"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>

                                            </div>
                                        </div>



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
                                        <asp:Button ID="btndelete" CausesValidation="TRUE" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');" CssClass="btn btn-success waves-light text-white" runat="server" Text="Delete" OnClick="btndelete_click" Enabled="true" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
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
