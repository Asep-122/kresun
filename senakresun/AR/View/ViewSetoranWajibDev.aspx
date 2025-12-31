<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSetoranWajibDev.aspx.cs" Inherits="CL_Process_ViewSetoranWajibDev" %> 
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
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../assets/js/modernizr.min.js"></script>

    <link href="../../assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/waves.js"></script>
    <script src="../../assets/js/jquery.slimscroll.js"></script>

    <script src="../../Javascript/jquery-ui.js"></script>

    <!-- Toastr css -->
    <link href="../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Modal -->
    <link href="../../plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <!-- Tooltipster css -->
    <link rel="stylesheet" href="../../plugins/tooltipster/tooltipster.bundle.min.css" />
    <!-- DataTables -->
    <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />




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

            $("#txtcustomer").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ViewSetoranWajib.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'" + document.getElementById('ddlcabang').value + "'}",
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
                                        <li class="breadcrumb-item"><a href="#">View</a></li>
                                        <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">View Setoran Pokok,Wajib dan cadangan</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">View Setoran Pokok,Wajib dan cadangan</h4>
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
              

                    <asp:Panel ID="Pnlcustomer" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">

                                    <div class="col-12">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">Pencarian Anggota</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                              <div class="form-group  ">
                                               
                                                    <asp:Label ID="Label4" runat="server" class="col-md-4 col-form-label font-weight-bold">Cabang</asp:Label>
                                                      <asp:DropDownList ID="ddlcabang" runat="server" CssClass="form-control" data-style="btn-custom" ></asp:DropDownList>  
                                               
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label class="col-form-label">Entry Data Anggota</label>
                                                <asp:TextBox CssClass="form-control" ID="txtcustomer" runat="server" required="required"></asp:TextBox>
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
                    <asp:Panel ID="pnladd"  runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">View Data Anggota</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                <asp:HiddenField ID="HDNoRekening" runat="server" />
                                                <asp:HiddenField ID="hdncustomer" runat="server" />
                                                

<%--                                                <div class="form-group">
                                                    <asp:HiddenField ID="hdncustomer" runat="server" />
                                                    <label for="inputpid" class="col-3 col-form-label">No. Bukti*</label>
                                                    <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                                </div>--%>

                                                <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Register</label>
                                                    <asp:TextBox ID="txttglregister" CssClass="form-control" ReadOnly="true" runat="server" MaxLength="10" ></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-4">
                                                    <label for="inputpid" class="col-9 col-form-label">User Admin</label>
                                                    <asp:TextBox ID="Txtuser" CssClass="form-control" ReadOnly="true" runat="server" MaxLength="30" ></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-4">
                                                    <label for="inputpid" class="col-9 col-form-label">Kode Sales</label>
                                                    <asp:TextBox ID="Txtkodesales" CssClass="form-control" ReadOnly="true" runat="server" MaxLength="30" ></asp:TextBox>
                                                </div>
                                              </div>
                                                 <div class="form-row" runat="server" visible="false">
                                                <div class="form-group col-md-4">
                                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Penutupan</label>
                                                    <asp:TextBox ID="txttglpenutupan" CssClass="form-control" ReadOnly="true" runat="server" MaxLength="10" ></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-4">
                                                    <label for="inputpid" class="col-9 col-form-label">Status</label>
                                                    <asp:TextBox ID="txtstatustutup" CssClass="form-control" ReadOnly="true" runat="server" MaxLength="30" ></asp:TextBox>
                                                </div>
                                             
                                              </div>

                                                            <div class="form-group" runat="server" visible="false">
                                                            <label for="inputpid" class="col-3 col-form-label">Keterangan Penutupan</label>
                                                            <asp:TextBox CssClass="form-control" ID="txtKeterangan" MaxLength="50"  ReadOnly="true"  runat="server" TextMode="MultiLine"></asp:TextBox>
                                        
                                                            </div>
                                                <div class="form-row" runat="server" visible="false">
                                                <div class="form-group col-md-4">
                                                    <label for="inputpid" class="col-9 col-form-label">Tanggal WO</label>
                                                    <asp:TextBox ID="txttglwo" CssClass="form-control" runat="server" ReadOnly="true" MaxLength="10" ></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-4">
                                                    <label for="inputpid" class="col-9 col-form-label">StatusWO</label>
                                                    <asp:TextBox ID="txtstatuswo" CssClass="form-control" runat="server" ReadOnly="true" MaxLength="30" ></asp:TextBox>
                                                </div>
                                             
                                              </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Anggota</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                <div class="form-row">
                                                <div class="form-group col-md-3">
                                                    <label for="inputCity" class="col-form-label">Kode Anggota</label>
                                                    <%--<asp:DropDownList ID="ddlkdsup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtkodelgn" Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-8">
                                                    <label for="inputpid" class="col-3 col-form-label">Nama</label>
                                                    <asp:TextBox ID="txtNmSup" Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                                <div class="form-group" runat="server" visible="false">
                                                    <label for="inputpid" class="col-3 col-form-label">Norekening</label>
                                                    <asp:TextBox ID="txtnorekening" Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="form-row">
                                                    <div class="form-group col-md-4">
                                                        <label for="inputCity" class="col-form-label">Saldo Awal Pokok</label>
                                                        <asp:TextBox ID="txtSaldoPokok" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label for="inputCity" class="col-form-label">Saldo Awal Kewajiban</label>
                                                        <asp:TextBox ID="txtsaldowajib" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                     <div class="form-group col-md-4">
                                                        
                                                         <label for="inputCity" class="col-form-label">Saldo Simpanan Keanggotaan</label>
                                                        <asp:TextBox ID="txtsaldotabungan" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-row">
                                                    <div class="form-group col-md-4">
                                                        <label for="inputCity" class="col-form-label">Masuk Cadangan</label>
                                                        <asp:TextBox ID="txtmasukcadangn" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label for="inputCity" class="col-form-label">Mutasi Cadangan</label>
                                                        <asp:TextBox ID="txtmutasicadangan" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                     <div class="form-group col-md-4">
                                                        
                                                         <label for="inputCity" class="col-form-label">Saldo Cadangan</label>
                                                        <asp:TextBox ID="txtsaldocadangan" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-row" runat="server" visible="false">

                                                    <div class="form-group col-md-4">
                                                        <label for="inputCity" class="col-form-label">Mutasi Debet Bulan Ini</label>
                                                        <asp:TextBox ID="txtmutasidebet" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label for="inputCity" class="col-form-label">Mutasi Kredit Bulan Ini</label>
                                                        <asp:TextBox ID="txtmutasikredit" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label for="inputCity" class="col-form-label">Saldo Akhir Bulan Lalu</label>
                                                        <asp:TextBox ID="txtsaldobulanlalu" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                


                                            </div>
                                        </div>
                                    </div>


                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnltrn" runat="server">
                        <div class="row clearfix">
                            <div class="auto-style1">
                                <div class="card-box">
                                    <h4 class="Page-title">Mutasi Transaksi Saldo Simpanan Pokok dan Wajib</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GRTP" runat="server" ShowFooter="True" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRTP_RowCommand" OnRowDataBound="GRTP_RowDataBound" ShowHeaderWhenEmpty="True" AllowSorting="True" PageSize="50">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Nomor Transaksi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Mutasi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgljthtmp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Keterangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKeterangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Debet">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldebet" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mutasidebet","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotaldebet" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkredit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mutasikredit","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotalkredit" runat="server" />
                                                        </FooterTemplate>
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


                    <asp:Panel ID="Panel1" runat="server">
                        <div class="row clearfix">
                            <div class="auto-style1">
                                <div class="card-box">
                                    <h4 class="Page-title">Mutasi Transaksi Cadangan</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GRDCADANGAN" runat="server" ShowFooter="True" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"  OnRowDataBound="GRDCADANGAN_RowDataBound" ShowHeaderWhenEmpty="True" AllowSorting="True" PageSize="50">

                                                <Columns>
                                                     <asp:TemplateField HeaderText="No">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No Rekening">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Realisasi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgljthtmp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_drop","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AO">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKeterangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kd_staff")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Status">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKeterangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cadangan Masuk">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblby_cadangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblby_cadanganx" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mutasi Cadangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmutasi_cadangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mutasi_cadangan","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblmutasi_cadanganx" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Saldo Cadangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsaldo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "saldo","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblsaldox" runat="server" />
                                                        </FooterTemplate>
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
                </div>

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <%--<asp:PostBackTrigger ControlID="btnSimpan" />--%>
                <%--<asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="txtkelipatan" />--%>
                <%--<asp:PostBackTrigger ControlID="btnAdd" />--%>
                <%--<asp:PostBackTrigger ControlID="GRData" />--%>
                <%--<asp:PostBackTrigger ControlID="GRRP" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>


<!-- Parsley js -->
<script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>

<script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

<!-- Sweet Alert Js  -->
<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>
<!-- Tooltipster js -->
<script src="../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>

<!-- App js -->
<script src="../../assets/js/jquery.app.js"></script>



<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>



</html>



