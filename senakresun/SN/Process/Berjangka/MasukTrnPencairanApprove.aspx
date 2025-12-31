<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasukTrnPencairanApprove.aspx.cs" Inherits="SN_Process_MasukTrnPencairanApprove" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Pencairan Promissory Note</title>
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

<%--            {
                $('#<%=txtKeterangan.ClientID%>').prop('maxlength', 150);
                
            }--%>
            
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
                        url: "MasukTrnPencairanBerjangka.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                         dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[2],
                                    hdncustomer: item[0],
                                    HDNoRekening : item[1]
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
    </head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Simpanan Berjangka</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="MasukTrnPencairanBerjangka.aspx">Transaksi Approval Pencairan Simpanan Berjangka</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Transaksi Approval Pencairan Simpanan Berjangka</h4>
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
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Transaksi" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
                 
                 <!-- Panel Edit -->

                 <asp:Panel ID="pnldatapencairan" Visible="false" runat="server">
                     <div class="row clearfix">
                         <div class="col-md-12">
                             <div class="card-box">
                                 <h4 class="Page-title">Data Nasabah Simpanan Berjangka Yang akan Jth Tempo Pencairan</h4>
                                 <div class="form-row">
                                     <div class="form-group" style="width: 100%;">
                                         <%--<div class="card-box table-responsive">--%>
                                         <asp:GridView ID="GRTP" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRTP_RowCommand" ShowHeaderWhenEmpty="True">
                                             <Columns>
                                                 <asp:TemplateField HeaderText="No">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblNumber1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rownumber")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Rekening">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno_rekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField visible="false">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnobillyed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobillyed")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField Visible="false">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno_nasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                                        <asp:Label ID="lblproduk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "produk")%>'></asp:Label>
                                                         <asp:Label ID="lblbungaperhari" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bungaperhari","{0:N6}")%>'></asp:Label>
                                                         <asp:Label ID="lblPenarikanBunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PenarikanBunga")%>'></asp:Label>
                                                         <asp:Label ID="lblNoRekeningBank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoRekeningBank")%>'></asp:Label>
                                                        <asp:Label ID="lblAtasNama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AtasNama")%>'></asp:Label>
                                                         <asp:Label ID="lblBankRekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BankRekening")%>'></asp:Label>
                                                          <asp:Label ID="lbltypebunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "typebunga")%>'></asp:Label>
                                                         <asp:Label ID="lblpencairanbunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pencairanbunga")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nama Nasabah">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnama_nasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Produk">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnama_produk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_produk")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Tgl Drop">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltgl_awal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_awal","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Tgl Due">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltgl_akhir" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_akhir","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Jangka">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbljangkabulan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangkabulan")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Bunga">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblbungapertahun" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bungapertahun")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Saldo Pokok">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblsaldopokok" CssClass="" runat="server" Text='<%#Eval("saldopokok","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Saldo Bunga">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblsaldobunga" CssClass="" runat="server" Text='<%#Eval("saldobunga","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Saldo Pencairan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblsaldojalan" CssClass="" runat="server" Text='<%#Eval("saldojalan","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Action">
                                                     <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                     <ItemTemplate>
                                                         <asp:LinkButton ID="btncair" Visible="false" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin mencairkan data ini ?');" title="Pencairan" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-vcard"></i></asp:LinkButton>
                                                         <asp:LinkButton ID="LinkButton2" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin Perpanjangan data ini ?');" title="Perpanjangan" CommandName="PANJANG" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-vcard"></i></asp:LinkButton>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                             </Columns>
                                         </asp:GridView>
                                     </div>
                                 </div>
                             </div>
                         </div>
                         <div class="form-group">
                             <asp:Button ID="BtnCancelCair" class="btn btn-success waves-light text-white" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

                         </div>
                     </div>
                 </asp:Panel>

                 


            


                 <asp:Panel ID="PnlRubah" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Perubahan Perpanjangan</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                <asp:HiddenField ID="HDNoRekening" runat="server" />
                                                <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                
                                                 <div class="form-group">
                                                    <asp:Label ID="Label32" runat="server" class="col-form-label">Pilih Perpanjangan</asp:Label>
                                                    <asp:DropDownList ID="ddlInstruktur" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                        <asp:ListItem Value="1">Perpanjangan Dengan Data sama</asp:ListItem>
                                                        <asp:ListItem Value="2">Perpanjangan Perubahan Data</asp:ListItem>                                                   
                                                    </asp:DropDownList>
                                                </div>
                                                
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-9 col-form-label">Jenis Produk</label>
                                                    <asp:DropDownList ID="ddlproduk" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlproduk_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Enabled="false" ErrorMessage="Produk tidak boleh kosong" ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlproduk" runat="server" />
                                                </div>
                                                 <div class="form-group">
                                                            <asp:Label ID="Label1" runat="server" class="col-9 col-form-label">Type Bunga</asp:Label>                                                       
                                                        <asp:DropDownList ID="ddltypebunga" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >
                                                            <asp:ListItem Value="1">Harian</asp:ListItem>
                                                            <asp:ListItem Value="2">Bulanan</asp:ListItem>                                                            
                                                        </asp:DropDownList>
                                                </div>
                                                <div class="form-row">
                                                    <div class="form-group col-md-6">
                                                        <asp:Label ID="Label13" runat="server" class="col-9 col-form-label">Jangka Bulan</asp:Label>
                                                        <asp:DropDownList ID="ddljangka" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddljangka_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Enabled="false" ErrorMessage="Jangka tidak boleh kosong" ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddljangka" runat="server" />
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <asp:Label ID="Label14" runat="server" class="col-9 col-form-label">Bunga % Pertahun</asp:Label>
                                                        <asp:DropDownList ID="ddlbunga" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlbunga_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Enabled="false" ErrorMessage="Bunga tidak boleh kosong" ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlbunga" runat="server" />
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <asp:Label ID="Label17" runat="server" class="col-form-label">Bunga % Perhari</asp:Label>
                                                        <asp:TextBox ID="txtbungaperhari" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                  <div class="form-group">
                                                    <asp:Label ID="Label33" runat="server" class="col-form-label">Penarikan Bunga</asp:Label>
                                                    <asp:DropDownList ID="ddlpenarikanbunga" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlpenarikanbunga_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">Transfer Ke Rekening</asp:ListItem>
                                                        <asp:ListItem Value="2">Di Ambil Langsung Oleh Nasabah</asp:ListItem>
                                                        <asp:ListItem Value="3">Lain-Lain (Roll Over)</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                   <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" class="col-form-label">Roll Over</asp:Label>
                                                    <asp:DropDownList ID="ddlrollover" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged="ddlpenarikanbunga_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">Bunga tidak ditarik</asp:ListItem>
                                                        <asp:ListItem Value="2">Bunga ditarik</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                       <div class="form-group">
                                                    <asp:Label ID="Label35" runat="server" class="col-form-label">Bank Rekening</asp:Label>
                                                    <asp:TextBox ID="txtbankrekening"  CssClass="form-control" MaxLength="50" Text="-" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label34" runat="server" class="col-form-label">No Rekening</asp:Label>
                                                    <asp:TextBox ID="txtnorekening" CssClass="form-control" MaxLength="50" Text="0" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label36" runat="server" class="col-form-label">Atas Nama</asp:Label>
                                                    <asp:TextBox ID="txtatasnama"  CssClass="form-control" MaxLength="100" Text="-" runat="server"></asp:TextBox>
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
                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Proses Perpanjangan ?');" OnClick="btnSimpan_Click" />
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
                <asp:PostBackTrigger ControlID="ddlproduk" />  
                 <asp:PostBackTrigger ControlID="ddljangka" /> 
                 <asp:PostBackTrigger ControlID="ddlbunga" /> 
                 <asp:PostBackTrigger ControlID="ddlpenarikanbunga" /> 
           
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



