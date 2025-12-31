<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssetLelangTarikan.aspx.cs" Inherits="AR_Transaksi_AssetLelangBarangTarikan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Status Asset Barang Tarikan Cabang</title>
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
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };

        function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: '<%=noassetdb%>',
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 2000,
                    stack: 1,
                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50);    //Save dan Edit
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
                        btnSimpan.disabled = true;
                        return true;

                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
            }
        }
        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("../../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            $('#txttgllelang').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#testTgl').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('.selectpicker').selectpicker();
            {
                $('#<%=txtNmBarang.ClientID%>').prop('maxlength', 50);
                $('#<%=txtMerek.ClientID%>').prop('maxlength', 50);
                $('#<%=txtUkuran.ClientID%>').prop('maxlength', 50);
                $('#<%=txtKetKondisi.ClientID%>').prop('maxlength', 100);
 

            }

            $(function () {
                var table = $('#gvListLelang').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });
                var table = $('#gvListAngsurLelang').dataTable({
                    "order": [],
                    "paging": false,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });
                $("#dialog").dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 600,
                    title: "Zoomed Image"
                });
                //$("[id*=gvImages] img").click(function () {
                //    $('#dialog').html('');
                //    $('#dialog').append($(this).clone());
                //    $('#dialog').dialog('open');
                //});
            });

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();

            $("#txtNorekSearch").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "AssetLelangTarikan.aspx/GetDataRekening",
                        data: "{'InputText':'" + document.getElementById('txtNorekSearch').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                        dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            //console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[3],
                                    HDNoRekening: item[0],
                                    HDNoNasabah: item[1],
                                    HDNoLelang: item[2]
                                }
                            });
                            $("[id$=HDNoRekening]").val('');
                            $("[id$=HDNoNasabah]").val('');
                            $("[id$=HDNoLelang]").val('');


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
                    $("[id$=HDNoRekening]").val(i.item.HDNoRekening);
                    $("[id$=HDNoNasabah]").val(i.item.HDNoNasabah);
                    $("[id$=HDNoLelang]").val(i.item.HDNoLelang);

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
                                        <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                        <li class="breadcrumb-item"><a href="#">Master</a></li>
                                        <li class="breadcrumb-item"><a href="AssetLelangTarikan.aspx">Status Assets Tarikan</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Status Asset Tarikan Cabang</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <label class="col-form-label">Search By</label>

                                                <div class="form-group col-md-6">
                                                    <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                        <asp:ListItem Value=" and a.NoBukti ">No Nobukti</asp:ListItem>
                                                        <asp:ListItem Value=" and a.nobuktimanual ">No. Reference</asp:ListItem>
                                                        <asp:ListItem Value=" and a.NoFaktur ">No. Rekening</asp:ListItem>
                                                        <asp:ListItem Value=" and a.KodeLgn ">No. Anggota</asp:ListItem>
                                                        <asp:ListItem Value=" and t.no_rekening_cbs ">No. Rekening CBS</asp:ListItem>
                                                        <asp:ListItem Value=" and t.no_nasabah_cbs ">No. Anggota CBS</asp:ListItem>
                                                        <asp:ListItem Value=" and c.nama_nasabah ">Nama Anggota</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>
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
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add" OnClick="btnAdd_Click" />
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
                                            <div class="ribbon ribbon-custom">Pencarian Asset Tarikan</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <label for="txtNorekSearch" class="col-form-label">No. Rekening</label>
                                                <asp:TextBox CssClass="form-control" ID="txtNorekSearch" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                            </div>
                    
                                        </div>
                                    </div>
                    
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Status Asset Tarikan Cabang</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="gvListLelang" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="gvListLelang_RowCommand" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No Lelang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnolelang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_lelang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal lelang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgllelang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tanggal_lelang","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nilai Lelang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnilailelang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "harga_jual", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Tarikan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgltarikan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tarikan_date","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Rekening">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnorekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Anggota">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_anggota")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_anggota")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btndelete" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" title="Delete" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Add -->
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Entry Data</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                <asp:HiddenField ID="HDNoRekening" runat="server" />
                                                <asp:HiddenField ID="HDNoLelang" runat="server" />

                                                <div class="form-group">
                                                    <label for="txtnolelang" class="col-3 col-form-label">No. Lelang</label>
                                                    <asp:TextBox ID="txtnolelang" CssClass="form-control" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="form-row">
                                                      <div class="form-group col-md-6">                                                  
                                                    <label for="ddlKondisi" class="col-form-label">Status Asset</label>
                                                    <asp:DropDownList ID="ddlstatus" CssClass="form-control" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                </div>
                                                    <div class="form-group col-md-6">
                                                        <label for="txttgllelang" class="col-form-label">Tanggal Status</label>
                                                        <asp:TextBox ID="txttgllelang" CssClass="form-control" runat="server" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-6">
                                                        <label for="txtHargaJual" class="col-form-label">Harga Jual</label>
                                                        <asp:TextBox ID="txtHargaJual"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" required="required" runat="server">0</asp:TextBox> 
                                                    </div>
                                                </div>
                                                <div class="form-row">
                                                    <div class="form-group col-md-3">
                                                        <label for="txtnosp1" class="col-form-label">No. SP1</label>
                                                        <asp:TextBox ID="txtnosp1" Enabled="true" CssClass="form-control" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                       <label for="txtnosp2" class="col-form-label">No. SP2</label>
                                                       <asp:TextBox ID="txtnosp2" Enabled="true" CssClass="form-control" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="txtnosp3" class="col-form-label">No. SP3</label>
                                                        <asp:TextBox ID="txtnosp3" Enabled="true" CssClass="form-control" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-row">
                                                    <div class="form-group col-md-6">
                                                        <label for="TextNoSuratTugas" class="col-form-label">No. Surat Tugas</label>
                                                        <asp:TextBox ID="TextNoSuratTugas" Enabled="true" CssClass="form-control" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label for="TextNoBAST" class="col-form-label">No. BAST</label>
                                                        <asp:TextBox ID="TextNoBAST" Enabled="true" CssClass="form-control" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-row">
                                                    <div class="form-group col-md-6">
                                                        <label for="txtNoAsset" class="col-form-label">No. Asset</label>
                                                        <asp:TextBox ID="txtNoAsset" Enabled="true" CssClass="form-control" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label for="txttgltarikan" class="col-form-label">Tanggal Tarikan</label>
                                                        <asp:TextBox ID="txttgltarikan" Enabled="true" CssClass="form-control" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-row">
                                                   <div class="form-group col-md-6">
                                                       <asp:HiddenField ID="HiddenField1" runat="server" />
                                                       <label for="txtKondisi" class="col-form-label">Kondisi</label>
                                                       <asp:TextBox ID="txtKondisi" Enabled="true" CssClass="form-control" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                                                   </div>
                                                   <div class="form-group col-md-6">
                                                       <label for="txtNmBarang" class="col-form-label">Nama Barang</label>
                                                       <asp:TextBox ID="txtNmBarang" Enabled="true" CssClass="form-control" MaxLength="50" ReadOnly="true" runat="server"></asp:TextBox>
                                                   </div>
                                                </div>
                                                <div class="form-row">
                                                   <div class="form-group col-md-3">
                                                      <label for="txtMerek" class="col-form-label">Merek</label>
                                                      <asp:TextBox ID="txtMerek" Enabled="true" CssClass="form-control" MaxLength="50" ReadOnly="true" runat="server"></asp:TextBox>
                                                   </div>
                                                   <div class="form-group col-md-3">
                                                      <label for="txtUkuran" class="col-form-label">Ukuran</label>
                                                      <asp:TextBox ID="txtUkuran" Enabled="true" CssClass="form-control" MaxLength="50" ReadOnly="true" runat="server"></asp:TextBox>
                                                   </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtKetKondisi" class="col-form-label">Ket. Kondisi</label>
                                                    <asp:TextBox ID="txtKetKondisi" Enabled="true" CssClass="form-control" TextMode="MultiLine" MaxLength="100" ReadOnly="true" runat="server"></asp:TextBox>
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
                                                    <label for="txtNoAnggota" class="col-form-label">No Anggota</label>
                                                    <asp:TextBox ID="txtNoAnggota" Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtNama" class="col-3 col-form-label">Nama</label>
                                                    <asp:TextBox ID="txtNama" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtNoRek" class="col-3 col-form-label">No. Rekening</label>
                                                    <asp:TextBox ID="txtNoRek" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtTglPinjam" class="col-3 col-form-label">Tanggal Pinjaman</label>
                                                    <asp:TextBox ID="txtTglPinjam" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtPlafon" class="col-3 col-form-label">Plafon</label>
                                                    <asp:TextBox ID="txtPlafon" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtSisaTotPok" class="col-3 col-form-label">Sisa Total Pokok</label>
                                                    <asp:TextBox ID="txtSisaTotPok" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtSisaTotBung" class="col-3 col-form-label">Sisa Total Bunga</label>
                                                    <asp:TextBox ID="txtSisaTotBung" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="PnlGambar" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="form-row">
                                <div class="col-md-6 col-lg-3">
                                    <div class="card m-b-30">
                                        <asp:Image ID="imgFront" runat="server" class="card-img-top img-fluid" alt="Card image cap" />
                                        <asp:TextBox ID="txtfileUpldFront" CssClass="form-control" Visible="false"  runat="server" MaxLength="400"></asp:TextBox>
                                        <div class="card-body">
                                            <h5 class="card-title">Gambar Depan</h5>
                                            <p class="card-text">Photo Gambar Depan Asset.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="card m-b-30">
                                        <asp:Image ID="imgBack" runat="server" class="card-img-top img-fluid" alt="Card image cap" />
                                        <asp:TextBox ID="txtfileUpldBack" CssClass="form-control" Visible="false"  runat="server" MaxLength="400"></asp:TextBox>
                                        <div class="card-body">
                                            <h5 class="card-title">Gambar Belakang</h5>
                                            <p class="card-text">Photo Gambar Belakang Asset.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="card m-b-30">
                                        <asp:Image ID="imgLeft" runat="server" class="card-img-top img-fluid" alt="Card image cap" />
                                        <asp:TextBox ID="txtfileUpldLeft" CssClass="form-control" Visible="false"  runat="server" MaxLength="400"></asp:TextBox>
                                        <div class="card-body">
                                            <h5 class="card-title">Gambar Kiri</h5>
                                            <p class="card-text">Photo Gambar Kiri Asset.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="card m-b-30">
                                        <asp:Image ID="imgRight" runat="server" class="card-img-top img-fluid" alt="Card image cap" />
                                        <asp:TextBox ID="txtfileUpldRight" CssClass="form-control" Visible="false"  runat="server" MaxLength="400"></asp:TextBox>
                                        <div class="card-body">
                                            <h5 class="card-title">Gambar Kanan</h5>
                                            <p class="card-text">Photo Gambar Kanan Asset.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlkrtasrn" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="auto-style1">
                                <div class="card-box">
                                    <h4 class="Page-title">Kartu Angsuran</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 95%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="gvListAngsurLelang" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                                     <%--<asp:TemplateField HeaderText="No" runat="server" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angs_ke")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Tanggal Tagih">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbltglTagih" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_tagih","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                       </ItemTemplate>
                                                       <HeaderStyle Height="30px" Width="150px" CssClass="align-center"></HeaderStyle>
                                                       <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Jatuh Tempo">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblTglDue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_jth_tempo","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                       </ItemTemplate>
                                                       <HeaderStyle Height="30px" Width="150px" CssClass="align-center"></HeaderStyle>
                                                       <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pokok" runat="server" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pokok", "{0:N2}")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bunga" runat="server" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bunga", "{0:N2}")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Angsuran" runat="server" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAngsuran" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuran", "{0:N2}")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status Bayar" runat="server" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatBayar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status_bayar")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Bayar">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblTglBayar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_angs", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                       </ItemTemplate>
                                                       <HeaderStyle Height="30px" Width="150px" CssClass="align-center"></HeaderStyle>
                                                       <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlPosting" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="card-box table-responsive">
                                           <%--<asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Cancel" OnClick="btnbackheader_Click" Width="65px" />--%>
                                          <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin Save data ini ?');" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                          <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" OnClick="btnbackheader_Click" Text="Cancel" runat="server" />
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
                <asp:PostBackTrigger ControlID="gvListLelang" />
                <asp:PostBackTrigger ControlID="gvListAngsurLelang" />
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

<script src="../../assets/js/jquery.core.js"></script>



<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>



</html>
