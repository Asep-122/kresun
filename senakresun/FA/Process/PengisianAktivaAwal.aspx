<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PengisianAktivaAwal.aspx.cs" Inherits="FA_Process_PengisianAktivaAwal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Mutasi</title>
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
        var x = 0;
        var y = 0;
        var z = 0;
        function calc(obj) {
            var e = obj.id.toString();
            if (e == 'txtKurs') {
                x = Number(obj.value);
                y = Number(document.getElementById('txtjmltrans').value);
            } else {
                x = Number(document.getElementById('txtKurs').value);
                y = Number(obj.value);
            }
            z = x * y;
            document.getElementById('txtjmlrp').value = z;
            document.getElementById('hfjmlrp').value = z;
        } 

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm'
                    }
                ).catch(swal.noop)
            }, 100);

        }

        function KurangDebitKredit(errMsg, kurang) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        text : "Kekurangan Debit atau kredit anda "+ kurang,
                        type : "warning",
                        confirmButtonClass: 'btn btn-confirm'
                    }
                ).catch(swal.noop)
            }, 100);

        }

           function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

        

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            
            $('#txttglptkp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('#txttgltransaksi').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtmulaisusut').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttglpertama').datepicker({
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

                }); var tableX = $('#GRJR').dataTable({
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

            //$("#rbJurnal").change(function () {
            //    var coba = document.getElementsByName("rbJurnal");
            //    //var coba = $(this).val() 
            //    for (var x = 0; x < coba.length; x++) {
            //        if (coba[x].checked) {
            //            var values = coba[x].value
            //            //alert(coba[x].value);
            //            if (values == '1') {
            //                $('#FileJurnal').show();
            //                $ansPanel.show();
            //                $('#pnlJournal').hide();
            //                $('#Pnlfooter').show();
            //            } else if (values == '2') {
            //                if ()
            //                $('#FileJurnal').hide();
            //                $('#pnlJournal').show();
            //                document.all("Pnlfooter").style.display = "none";
            //                $('#Pnlfooter').hide();
            //            } else {
            //                $('#FileJurnal').hide();
            //                $('#pnlJournal').hide();
            //                $('#Pnlfooter').show();
            //            }
            //        }
            //    }
            //}); 
            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
        }
        function PeringatanKode(a) {

            setTimeout(function () {
                swal(
                    {
                        title: a,
                        type: "warning",
                        position: "top-end",
                        showConfirmButton: false,
                        timer: 2000
                    }
                ).catch(swal.noop)
            }, 100);
            //$("#txtjenisgol").val(""); 
        }
        function Delete(a, b, c, d) {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil di hapus",
                        text: a + ' ' + ':' + ' ' + b + ' ' + c + ' ' + ':' + ' ' + d,
                        type: "success",
                        position: "top-end",
                        showConfirmButton: false,
                        timer: 2000,
                        hideAfter: 2000,
                        afterHidden: function () {
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }
                    }
                ).catch(swal.noop)
            }, 100);
            location.href = 'Lokasi.aspx ';
        }
        function Simpan(a, b, c, d) {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil disimpan",
                        text: a + ' ' + ':' + ' ' + b + ' ' + c + ' ' + ':' + ' ' + d,
                        type: "success",
                        position: "top-end",
                        //   icon: "success",
                        showConfirmButton: false,
                        timer: 2000
                        //title: errMsg,
                        //confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);
            $("#txtkdcabang").val("");
            $("#txtnmcabang").val("");
            $("#txtalmtcabang").val("");
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
                    confirmButtonClass: 'btn btn-confirm',
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
                    confirmButtonClass: 'btn btn-confirm',
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
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9 ]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
        function text(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
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
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
                 <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Fixed Assets</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="PengisianAktivaAwal.aspx">Transaksi Mutasi</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Transaksi Mutasi</h4>
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
                                   <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Aktiva" OnClick="btnAdd_Click" />
                               </div>
                           </div>
                        </div>
                    </asp:Panel>
                     <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Data master Pengisian Aktiva Awal</h4>                           
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Kode Aktiva">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkdaktiva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeAktiva")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Aktiva">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnmaktiva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaAktiva")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. Bukti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnomorbukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Masa Susut (dalam Bulan)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmasasusut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MasaSusut")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TglTrn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgltrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglTrn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TypeTrn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltypetrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jumlah">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljumlah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jumlah", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Harga Perolehan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperolehan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhPerolehan", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nilai Residu">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblresidu" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhResidu", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Harga Akumulasi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblakumulasi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "HargaAkumulasi", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Mulai Susut">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmulaisusut" runat="server" Format="yyyy-mm-dd"  Text='<%#DataBinder.Eval(Container.DataItem, "tglawalSusut")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Akhir Susut">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblakhirsusut" runat="server" Format="dd-mm-yyyy"  Text='<%#DataBinder.Eval(Container.DataItem, "tglsudahSusut")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" >
                                                        <HeaderStyle Width="10%" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <%--<asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" autopostback ="true"> <i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" OnClientClick="return confirm('Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                                
                                <div class="card-box">
                                    <div class="form-row">
                                        <%--//kiri--%>
                                          <asp:HiddenField ID="hdmsusut" runat="server" />
                                        <asp:HiddenField ID="hdmtahun" runat="server" />
                                        <div class="form-group col-md-6">
                                             <div class="form-row">
									        <div class="form-group col-md-6">
                                                <label class="col-form-label">No. Bukti</label>
                                                <asp:TextBox ID="txtNoBukti" CssClass="form-control" runat="server" MaxLength="20" Enabled ="false"></asp:TextBox>
                                            </div>
									        <div class="form-group col-md-6">
                                                <label class="col-form-label">Type Transaksi</label>
                                                <asp:DropDownList ID="ddltype" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true"  OnSelectedIndexChanged ="ddltype_SelectedIndexChanged"   ></asp:DropDownList>   
										        <asp:TextBox ID="txttype" Enabled ="false" CssClass="form-control"  runat="server"></asp:TextBox>
                                            </div>
                                                 </div>

                                             <div class="form-row">
                                            <div class="form-group col-md-3">
                                                <asp:label ID="Label2" runat="server"  class="col-form-label">Tanggal Transaksi</asp:label>
                                                <asp:TextBox ID="txttgltransaksi"  CssClass="form-control" runat="server"  ></asp:TextBox>
                                            </div>
									        <div class="form-group col-md-3">
                                                <asp:label ID="lblmulaisusut" runat="server" Visible="false" class="col-form-label">Tanggal Mulai Susut</asp:label>
                                                <asp:TextBox ID="txtmulaisusut" Visible="false"  CssClass ="form-control" runat="server"  AutoPostBack="true"  OnTextChanged="txtmulaisusut_TextChanged"></asp:TextBox>
                                            
                                            </div>
                                                 <div class="form-group col-md-3">
                                                 <asp:label id="lbltglakhirsusut" runat="server" Visible="false" class="col-form-label">Tanggal Akhir Susut</asp:label>
                                                 <asp:TextBox ID="txtakhirsusut" Visible="false"   CssClass="form-control" runat="server"  enabled ="true" ></asp:TextBox>
                                            </div>
                                                 </div>
                                             <div class="form-row">
                                            <div class="form-group col-md-6">
                                                 <asp:label ID="lblumur" runat="server" Visible="false" class="col-form-label">Umur Ekonomis (dalam Tahun)</asp:label>
                                                 <asp:TextBox ID="txtumur" Visible="false" CssClass="form-control"  runat="server" MaxLength="3"  onkeypress="return isnumerickey(event);"  AutoPostBack="true"   OnTextChanged="txtumur_TextChanged" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <asp:label id="lblmasamanfaat" runat="server" Visible="false" class="col-form-label">Masa Manfaat (dalam Bulan)</asp:label>
                                                <asp:TextBox ID="txtmasa" visible="false"  Enabled="false" CssClass="form-control" onkeypress="return isnumerickey(event);" runat="server" MaxLength="16" AutoPostBack="true"   OnTextChanged="txtmasa_TextChanged"></asp:TextBox>
                                            </div>
                                                 </div>
                                             <div class="form-row">
                                                <div class="form-group col-md-4">
                                                <asp:label id="lblhargaperolehan" runat="server" Visible="false" class="col-form-label" >Harga Perolehan</asp:label>
                                                <asp:TextBox ID="txtharga" Visible ="false" onkeyup="javascript:this.value=Comma(this.value);"  Text="0" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control autonumber"  runat="server" MaxLength="16" AutoPostBack="true"  OnTextChanged="txtharga_TextChanged" ></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:label ID="lblakumulasi" Visible ="false" runat="server" class="col-form-label">Akumulasi Susut</asp:label> 
                                                <asp:TextBox ID="txtakumulasisusut" Visible ="false" onkeyup="javascript:this.value=Comma(this.value);"  Text="0" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control autonumber"  runat="server" MaxLength="16" AutoPostBack="true"  OnTextChanged="txtakumulasisusut_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:label id="lblnilai" runat="server" Visible="false" class="col-form-label">Nilai Residu</asp:label>
                                                <asp:TextBox ID="txtResidu" Visible ="false"  onkeyup="javascript:this.value=Comma(this.value);"   Text="0" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control autonumber"  runat="server" MaxLength="16" AutoPostBack="true"  OnTextChanged="txtresidu_TextChanged" ></asp:TextBox>
                                            </div>
                                            </div>
                                             <div class="form-row">
                                            	    <div class="form-group col-md-6">
                                                <asp:label ID="lbluniversal" Visible ="false" runat="server" class="col-form-label"></asp:label>
                                                <asp:TextBox ID="txtbeli" Visible="false" Text="0" CssClass="form-control" required="required" runat="server" MaxLength="50" onkeypress="return kepo(event);"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <asp:label ID="lbltanggalbeli" Visible ="false" runat="server" class="col-form-label">Tanggal Beli Pertama</asp:label>
                                                <asp:TextBox ID="txttglpertama" Visible ="false"  CssClass="form-control" runat="server"  ></asp:TextBox>
                                            </div>
										    <div class="form-group col-md-6">
                                                <asp:label ID="lbltanggalbeliakhir" Visible ="false" runat="server" class="col-form-label">Tanggal Beli Akhir</asp:label>
                                                <asp:TextBox ID="txttglbeliakhir" Visible ="false"  CssClass="form-control" runat="server"  Type="date"></asp:TextBox>
                                            </div>
                                            </div>

                                              <div class="form-group">
                                                <asp:label ID="lblhargapembelian" Visible ="false" runat="server" class="col-form-label">Harga Pembelian</asp:label>
                                                <asp:TextBox ID="txthargapembelian" Text="0" Visible ="false" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" CssClass="form-control"  runat="server" MaxLength="16" ></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:label ID="lblpenambahanharga" Visible ="false" runat="server" class="col-form-label">Penambahan Harga</asp:label>
                                                <asp:TextBox ID="txtpenambahanharga" Text="0" Visible ="false" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" CssClass="form-control"  runat="server" MaxLength="16">0</asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asplabel ID="lblpenguranganharga" Visible ="false" runat="server" class="col-form-label">Pengurangan Harga</asplabel>
                                                <asp:TextBox ID="txtpenguranganharga" Text="0" Visible ="false" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" CssClass="form-control"  runat="server" MaxLength="16">0</asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:label id="lblketerangan" runat="server" Visible="false" class="col-form-label">Keterangan</asp:label>
                                                <asp:TextBox CssClass="form-control"  Visible="false" ID="txtketerangan" MaxLength="40" parsley-trigger="change"   runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </div>  
                                        </div>
                                        <%--kanan--%>
                                        <div class="form-group col-md-6">
                                            
                                        
                               
                                        
                                      <div class ="form-row">
                                            <div class="form-group col-md-6">
                                                <label class="col-form-label">Kode Aktiva</label>
                                                <asp:DropDownList ID="ddlkdaktiva" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlkdaktiva_SelectedIndexChanged" required="required"></asp:DropDownList> 
                                            </div>
                                            <div class="form-group col-md-6">
                                                <label class="col-form-label">Golongan Aktiva</label>
                                                <asp:DropDownList ID="ddlgolongan" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList> 
                                            </div>

                                     </div>



                                            <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Group Aktiva</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlgroupaktiva"  Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:DropDownList>
                                                                </div>
                                                            </div>


                      <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Nama Aktiva</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtnamaaktiva"  Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>

                                           <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Kode Lokasi</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                       <asp:DropDownList ID="ddllokasi"  Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                            

                                                <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Nomor Assets</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtnomoraktiva"  Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>         

                                                <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Spesifikasi</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtspek"  Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>    

                                                  <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Merk</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtMerk"   Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>   

                                                   <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Nomor Mesin</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtmesin"  Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>  
                                       
                                          
                                                   <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Nomor Chasis</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtchasis"  Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>  
                                            


                                                  <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Catatan Aktiva</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtcatatan"  Enabled="false" CssClass="form-control" runat="server" TextMode="MultiLine" Height="145px" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>  

                                             <div class ="form-row">
                                                    <div class="form-group col-md-6">
                                                  <label class="col-form-label">Tanggal Entry</label>
                                                  <asp:TextBox ID="txttanggalentry" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                             </div>
                                            <div class="form-group col-md-6">
                                                 <label class="col-form-label">Entry Oleh</label>
                                                 <asp:TextBox ID="txtentry" CssClass="form-control" required="required" runat="server" Enabled="false"></asp:TextBox>
                                             </div>

                                           </div>

                                           
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
                                        <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" Enabled="false" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menyimpan data ini ?');" runat="server" Text="Save" OnClick="btnSimpanx_Click" />
                                        <asp:Button ID="btndelete" class="btn btn-success waves-light text-white" Enabled="false" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');" runat="server" Text="Delete" OnClick="btndelete_Click" Visible="false" />
                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader_Click" Width="65px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div> 
            <!-- end wrapper -->
            </ContentTemplate>
            <Triggers> 
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="ddltype" /> 
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
<%--<asp:Panel ID="PnlGridJurnal" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Fix Assets Transaction</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <div class="card-box table-responsive">
                                                <asp:Button ID="Button1" class="btn btn-success waves-light text-white" runat="server" Text="Add Detail" OnClick="lnkBT_Click" />
                                            </div>                                               
                                            <asp:GridView ID="GRJR" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRRP_RowCommand" OnRowDataBound="GRRP_RowDataBound" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Kode GL">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <div class="form-group">
                                                                <div class="form-line">
                                                                    <asp:DropDownList ID="ddlKodeGL" AutoPostBack="true" runat="server" CssClass="show-tick form-control" OnSelectedIndexChanged="ddlKodeGL_SelectedIndexChanged"></asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnddlKodeGL" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "KodeGL")%>' />
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="+">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <div class="form-group">
                                                                <div class="form-line">
                                                                    <asp:DropDownList ID="ddlnamaGL" AutoPostBack="true" runat="server" CssClass="show-tick form-control" OnSelectedIndexChanged="ddlnamaGL_SelectedIndexChanged"></asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnddlnamaGL" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "namagl")%>' />
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Keterangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <div class="form-group">
                                                                <div class="form-line">
                                                                    <asp:TextBox ID="txtKT" runat="server" CssClass="form-control no-resize auto-growth required" Rows="1" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>' MaxLength="150"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kode Cost Center">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <div class="form-group">
                                                                <div class="form-line">
                                                                    <asp:DropDownList ID="ddlcostcenter" runat="server" CssClass="show-tick form-control" Enabled="false">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnddlcostcenter" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "costcenter")%>' />
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jumlah Debit">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <div class="form-group">
                                                                <div class="form-line">                                                           
                                                                    <asp:TextBox CssClass="form-control" ID="jlhdebit" Text='<%#DataBinder.Eval(Container.DataItem, "jlhdebit","{0:N2}")%>' MaxLength="30" runat="server"  onkeypress="return isnumerickey(event);" required="required" onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged ="XXXTxt01_TextChanged" ></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Jumlah Kredit">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <div class="form-group">
                                                                <div class="form-line"> 
                                                                     <asp:TextBox CssClass="form-control" ID="jlhKredit" Text='<%#DataBinder.Eval(Container.DataItem, "jlhkredit","{0:N2}")%>' MaxLength="30" runat="server"  onkeypress="return isnumerickey(event);" required="required" onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged ="XXXTxt02_TextChanged" ></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btndelete" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DEL" data-placement="left" data-toggle="tooltip" OnClientClick="if (!ConfirmDeleteMessage('Are you sure you want to delete ? ')) {  return false; }" title="Hapus"> <i class="btn-outline-danger">delete</i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>--%>