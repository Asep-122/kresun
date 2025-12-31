<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemoInternalCustomerMikroTambahCadangan.aspx.cs" Inherits="AR_Process_MemoInternalCustomerMikroTambahCadangan" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Pembayaran Hutang</title>
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
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            {
                $('#<%=txtnote.ClientID%>').prop('maxlength', 150);

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
                var tableX = $('#GRList').dataTable({
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

            $("#txtcustomer").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "MemoInternalCustomerMikroTambahCadangan.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                        dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[2],
                                    hdncustomer: item[0],
                                    HDNoRekening: item[1]
                                }
                            });

                            $("[id$=hdncustomer]").val('');
                            $("[id$=HDNoRekening]").val('');

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
                       $("[id$=hdncustomer]").val(i.item.hdncustomer);
                       $("[id$=HDNoRekening]").val(i.item.HDNoRekening);
                   },
                   minLength: 3
               });
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            min-height: 1px;
            -ms-flex: 0 0 100%;
            flex: 0 0 100%;
            max-width: 100%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }

        .auto-style2 {
            display: block;
            font-size: 14px;
            line-height: 1.5;
            color: #495057;
            background-clip: padding-box;
            border-radius: 4px;
            transition: all 300ms linear;
            max-width: 100%;
            -webkit-box-shadow: none;
            box-shadow: none;
            -webkit-transition: all 300ms linear;
            -moz-transition: all 300ms linear;
            -o-transition: all 300ms linear;
            border: 1px solid #d9e3e9;
            background-color: #fff;
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
                                        <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">Transaksi Memo Internal Penambahan Cadangan Anggota</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Transaksi Memo Internal Penambahan Cadangan Anggota</h4>
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
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="card-box">
                                    <div class="form-row">
                                        <div class="form-group col-md-12">
                                            <div class="form-group">
                                                <label class="col-form-label">Search By</label>
                                                <div class="form-group col-md-6">
                                                    <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                        <asp:ListItem Value=" and a.no_memo ">No Bukti</asp:ListItem>
                                                        <asp:ListItem Value=" and a.no_rekening ">No Rekening</asp:ListItem>
                                                        <asp:ListItem Value=" and c.no_nasabah ">No Anggota</asp:ListItem>
                                                        <asp:ListItem Value=" and c.nama_nasabah ">Nama Anggota</asp:ListItem>
                                                        <asp:ListItem Value=" and a.memoapprove ">Memo Approve</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>
                                                    </div>
                                                <div class="form-group col-md-6">
                                                    <asp:Button ID="lnkCari" class="btn btn-success waves-light text-white" runat="server" Text="Cari" OnClick="lnkCari_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Transaksi" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">

                                    <h4 class="m-t-0 header-title">Transaksi Memo internal</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 95%;">

                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="625px">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="No Bukti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_memo")%>'></asp:Label>
                                                            <asp:Label ID="lblnoanggota" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "noanggota")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Rekening">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Transaksi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_pengajuan","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Anggota">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNmLgn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaLgn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jumlah">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblddlpembayaran" CssClass="" runat="server" Text='<%#Eval("jumlahmemo","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Jumlah" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcadangan" CssClass="" runat="server" Text='<%#Eval("cadangan","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="denda" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldenda" CssClass="" runat="server" Text='<%#Eval("denda","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="bayarpinjam" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbayarpinjam" CssClass="" runat="server" Text='<%#Eval("bayarpinjam","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type Memo ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamatype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namatype")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type Memo " Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type status " Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="notememo" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnotememo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notememo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="100px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamastatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namastatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkdsplr" CssClass="" runat="server" Text='<%#Eval("kodelgn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Approve M Cabang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrnMC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateAddedmc","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Approve Cabang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrnAP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateAddedAP","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Memo Approve">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmemoapprove" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "memoapprove")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnKwPinjaman" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Kwitansi" CommandName="Kwitansi" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnSlip" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="MEMO" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
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
                    <asp:Panel ID="PnlListdata"  Visible="false"  runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">List Anggota</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 95%;">
                                            <asp:GridView ID="GRList" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRList_RowCommand" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                                 <asp:TemplateField HeaderText="No Rekening">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnorekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No Anggota">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="No Rekening CBS">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnorekeningcbs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening_cbs")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No Anggota CBS">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnonasabahcbs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah_cbs")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nama Anggota">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnama_nasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Produk">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblkode_produk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Plafon">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblPlafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon","{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Angsuran">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblangsuran" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuran")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Cadangan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblcadangan" CssClass="" runat="server" Text='<%#Eval("cadangan","{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Mutasi">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblMutasi" CssClass="" runat="server" Text='<%#Eval("mutasi_cadangan","{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Action">
                                                     <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                     <ItemTemplate>
                                                         <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin Bayar data ini ?');" title="Setoran" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                             
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
                                            <div class="ribbon ribbon-custom">Pencarian Anggota</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <label class="col-form-label">Entry Data Anggota</label>
                                                <asp:TextBox CssClass="form-control" ID="txtcustomer" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSerach_Click" />
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Entry Data</div>
                                                <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                <asp:HiddenField ID="HDNoRekening" runat="server" />



                                                <div class="form-group">
                                                    <asp:HiddenField ID="hdncustomer" runat="server" />
                                                    <label for="inputpid" class="col-3 col-form-label">No. Bukti*</label>
                                                    <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                                </div>


                                                <div class="form-group">
                                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Pengajuan</label>
                                                    <asp:TextBox ID="txttgltrn" CssClass="form-control" required="required" runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-3 col-form-label">Type Memo</label>
                                                    <asp:DropDownList ID="ddltypememo" runat="server" CssClass="form-control" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddltypememo_SelectedIndexChanged">
                                                        <%--                           <asp:ListItem Value="A1">Pelunasan</asp:ListItem>
                                                        <asp:ListItem Value="B2">Pemakaian Cadangan</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputCity" class="col-form-label">Jumlah </label>
                                                    &nbsp;<asp:TextBox ID="txtbayar" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-form-label">Catatan Memo</label>
                                                    <asp:TextBox CssClass="auto-style2" ID="txtnote" MaxLength="150" runat="server" TextMode="MultiLine" Height="145px" Width="595px"></asp:TextBox>
                                                </div>


                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Anggota</div>
                                                <p class="text-muted font-14 m-b-30"></p>

                                                <div class="form-group">
                                                    <label for="inputCity" class="col-form-label">Kode Anggota</label>
                                                    <%--<asp:DropDownList ID="ddlkdsup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtkodelgn" Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-3 col-form-label">Nama</label>
                                                    <asp:TextBox ID="txtNmSup" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group">
                                                    <label for="inputCity" class="col-form-label">Saldo Cadangan</label>
                                                    <asp:TextBox ID="txtcadangan" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>

                                                </div>
                                                <div class="form-group">
                                                    <label for="inputCity" class="col-form-label">Saldo Denda</label>
                                                    <asp:TextBox ID="txtsaldoDenda" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>

                                                </div>

                                                <div class="form-group">
                                                    <label for="inputCity" class="col-form-label">Pinjaman terbayarkan</label>
                                                    <asp:TextBox ID="txtsaldopinjaman" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>

                                                </div>



                                            </div>
                                        </div>
                                    </div>


                                </div>

                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnltrn" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="auto-style1">
                                <div class="card-box">
                                    <h4 class="Page-title">Transaksi Advance Anggota</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GRTP" runat="server" ShowFooter="True" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRTP_RowCommand" OnRowDataBound="GRTP_RowDataBound" ShowHeaderWhenEmpty="True" AllowSorting="True" PageSize="50">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Nomor Rekening">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Drop">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgljthtmp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_jwl","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Produk">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblproduk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Angs-ke" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblangs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angs_ke")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cadangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pokok","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotalPokok" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mutasi Cadangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bunga","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotalBunga" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sisa Cadangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAngsuran" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuran","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotalAngsuran" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tambah Cadangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldenda" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "denda","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotaldenda" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bayar">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlbayar" CssClass="selectpicker show-tick" Text='<%#DataBinder.Eval(Container.DataItem, "Tipebayar")%>' runat="server" data-style="btn-custom" required="required" AutoPostBack="true" Width="400" OnSelectedIndexChanged="ddlbayar_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">NON</asp:ListItem>
                                                                <asp:ListItem Value="1">Bayar</asp:ListItem>
                                                                <%--<asp:ListItem Value="2">Angsuran+Denda</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                            <%--<asp:CheckBox ID="CheckBox1"  AutoPostBack="true" runat="server" OnCheckedChanged="Check_Clicked"  />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltipebyr" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Tipebayar")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbayar" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhDibayar","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            </asp:GridView>
                                            <%--</div>--%>
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
                <asp:PostBackTrigger ControlID="ddltypememo" />
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="GRData" />
                <%--<asp:PostBackTrigger ControlID="GRRP" />--%>
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



