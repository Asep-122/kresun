<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransaksiBiayaApprovalBayar.aspx.cs" Inherits="OE_Process_TransaksiBiayaApprovalBayar" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Pembayaran Pengeluaran Biaya Approval</title>
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
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };
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
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            {
                $('#<%=txtnote.ClientID%>').prop('maxlength', 50);
                
            }
            
            $('#txttglbyr').datepicker({
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
                        url: "TransaksiBiayaApprovalBayar.aspx/GetCustomer",
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
                                        <li class="breadcrumb-item"><a href="#">Operational Expense</a></li>
                                        <li class="breadcrumb-item"><a href="#">Master</a></li>
                                        <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">Pembayaran Biaya Approval</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Pembayaran Biaya Approval</h4>
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
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <label class="col-form-label">Search By</label>
                                                <div class="form-group col-md-6">
                                                    <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                        <asp:ListItem Value=" and a.no_memo ">No Bukti</asp:ListItem>
                                                        <asp:ListItem Value=" and x.tipebiaya ">Type Biaya</asp:ListItem> 
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
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Reload Transaksi" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Pembayaran Biaya Approval</h4>
                                           <div class="form-row" runat="server" visible="false">
                                 <div class="form-group col-md-4">                                
                                    <asp:label ID="Label1" runat="server" class="col-form-label">Tgl Entry</asp:label>
                                     <asp:TextBox ID="txttgltrnx" CssClass="form-control" required="required" runat="server" MaxLength="10"  AutoPostBack="true" OnTextChanged="txtdatex_TextChanged"></asp:TextBox>      
                                                       
                                   
                                 </div>
                             </div>

                                    <div class="form-row">
                                        <div class="form-group" style="width: 95%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No Bukti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_memo")%>'></asp:Label>
                                                         
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="Tanggal Transaksi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_pengajuan","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Group" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgroupnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Namagroup")%>'></asp:Label>
                                                            <asp:Label ID="lblkodegroup" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodegroup")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Biaya" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBiayanama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "groupbiayanama")%>'></asp:Label>
                                                            <asp:Label ID="lblkodeBiaya" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodegroupbiaya")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Keterangan" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnotememo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notememo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Jumlah">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblddlpembayaran" CssClass="" runat="server" Text='<%#Eval("jumlah","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                   
                                                    <asp:TemplateField HeaderText="Type status " Visible="false" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                         


                                                    <asp:TemplateField HeaderText="status" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamastatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namastatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                             
                                                    <asp:TemplateField HeaderText="Approve Pusat" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrnMC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateAddedAP","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Teller Bayar" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrnAP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateAddedmc","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    
                                                      <asp:TemplateField HeaderText="Catatan Pusat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lblcatatpusat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "noteap")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Memo Approve" Visible="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblmemoapprove" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "memoapprove")%>'></asp:Label>--%>
                                                            <asp:Label ID="lblKodenik" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodenik")%>'></asp:Label>
                                                            <asp:Label ID="lblnamakaryawan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakaryawan")%>'></asp:Label>
                                                            <asp:Label ID="lblakumulasibiaya" CssClass="" runat="server" Text='<%#Eval("akumulasibiaya","{0:N0}")%>'></asp:Label>
                                                            <asp:Label ID="lblakumulasikaryawan" CssClass="" runat="server" Text='<%#Eval("akumulasikaryawan","{0:N0}")%>'></asp:Label>
                                                            <asp:Label ID="lblcatatangroup" CssClass="" runat="server" Text='<%#Eval("catatangroup")%>'></asp:Label>
                                                             <asp:Label ID="catatanbiayagroup" CssClass="" runat="server" Text='<%#Eval("catatanbiayagroup")%>'></asp:Label>
                                                            <asp:Label ID="lblakungl" CssClass="" runat="server" Text='<%#Eval("AkunGL")%>'></asp:Label>
                                                            <asp:Label ID="lbljenis" CssClass="" runat="server" Text='<%#Eval("jenis")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnedit" class="btn badge-danger"  type="button" Text="BAYAR" runat="server" OnClientClick="return sweetAlertConfirm(this,Apakah anda yakin ingin Bayar data ini ?');" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                            <%--<asp:button class="btn btn-primary waves-light waves-effect" ID="btnedit" runat="server" type="button" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin Bayar data ini ?');" title="Bayar" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Bayar</asp:button>--%>
                                                            <asp:LinkButton ID="BtnSlip" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="MEMO" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
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
                                                <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                <asp:HiddenField ID="HDNoRekening" runat="server" />



                                                <div class="form-group">
                                                    <asp:HiddenField ID="hdncustomer" runat="server" />
                                                    <asp:HiddenField ID="hdnakun" runat="server" />
                                                    <label for="inputpid" class="col-3 col-form-label">No. Bukti*</label>
                                                    <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox>
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


                                                        <div class="row">
                                                      
                                                <div class="form-group col-md-6">
                                               
                                                   <label for="inputpid" class="col-form-label">Tanggal Transaksi</label>
                                                    <asp:TextBox ID="txttgltrn" CssClass="form-control" required="required" runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                </div>

                                            <div class="form-group col-md-6">
                                                               <label class="col-form-label">Jenis Transaksi</label>
                                                                <asp:DropDownList ID="ddljenis" runat="server" CssClass="form-control" data-style="btn-custom" >
                                                                            <asp:ListItem Value="T">Terima Kas & Bank</asp:ListItem>
                                                                            <asp:ListItem Value="K">Keluar Kas & Bank</asp:ListItem>  
                                                                        
                                                                </asp:DropDownList>
                                                        </div> 
                                         </div>

                                                  <div class="row">
                                                      
                                                <div class="form-group col-md-6">

                                            
                                                    <label for="inputpid" class="col-form-label">Group</label>
                                                    <asp:DropDownList ID="ddlgroup" runat="server" CssClass="form-control" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlgroup_SelectedIndexChanged" >
                                                     </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label for="inputpid" class="col-form-label">Group Detail</label>
                                                    <asp:DropDownList ID="ddlgroupbiaya" runat="server" CssClass="form-control" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlgroupbiaya_SelectedIndexChanged" >
                                                     </asp:DropDownList>
                                                </div>
                                                      </div>
                                                
                                                
                                                
                                               
                                                <div class="row">
                                                <div class="form-group col-md-6">
                                                    <label for="inputpid" class="col-form-label">Kode Nik Karyawan</label>
                                                    <asp:DropDownList ID="ddlKodenik" runat="server" CssClass="form-control" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlkodenik_SelectedIndexChanged"  >
                                                     </asp:DropDownList>
                                                </div>

                                                   <div class="form-group col-md-6">
                                                    <label class="col-form-label">Nama Karyawan</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtnamakaryawan" ReadOnly="true" MaxLength="50" runat="server" type="text"></asp:TextBox>
                                                </div>
                                               </div>
                                               <div class="form-group">
                                                    <label for="inputCity" class="col-form-label">To Cabang</label>
                                                    <asp:DropDownList ID="ddlTokdcabang" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList> 
                                               </div>
                                                <div class="form-group">
                                                    <label for="inputCity" class="col-form-label">Jumlah </label>
                                                    &nbsp;<asp:TextBox ID="txtbayar" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                </div>

                                                  <div class="form-group">
                                                    <label class="col-form-label">Keterangan</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtnote" MaxLength="50" runat="server" ></asp:TextBox>
                                                </div>


                                              


                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Info Group</div>
                                                <p class="text-muted font-14 m-b-30"></p>

                                                <div class="form-group">
                                                    <label class="col-form-label">Catatan Group</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtcatatangroupbiaya" Enabled="false" runat="server" row="2" TextMode="MultiLine" ></asp:TextBox>
                                                </div>

                                                
                                                  <div class="row">
                                                      
                                                <div class="form-group col-md-6">
                                                    <label class="col-form-label">Catatan Detail Group</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtcatatandetailbiaya" Enabled="false"  runat="server" TextMode="MultiLine" ></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label for="inputCity" class="col-form-label">Akumulasi Group Bulan ini </label>
                                                    &nbsp;<asp:TextBox ID="txtakumulasibiaya" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                    <label for="inputCity" class="col-form-label">Akumulasi karyawan Bulan ini </label>
                                                    &nbsp;<asp:TextBox ID="txtakumulasikry" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>

                                                
                                                </div>
                                               </div>
                                               
                                                
                                                  <div class="form-group">
                                                    <label class="col-form-label">Catatan Pusat</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtcatatanpusat" Enabled="false" row="2" MaxLength="50" runat="server" TextMode="MultiLine" ></asp:TextBox>
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
                                        <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Bayar data ini ?');" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btndelete" CausesValidation="TRUE" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin melakukan Pembayaran data ini ?');" CssClass="btn btn-success waves-light text-white" runat="server" Text="Bayar" OnClick="btndelete_click" Enabled="true" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnCancel_Click" Text="Close" Width="65px" />
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
                <asp:PostBackTrigger ControlID="ddlgroup" />
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



