<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemoKasbon.aspx.cs" Inherits="AE_Process_MemoKasbon" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Kasbon Karyawan</title>
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

               $("#txtkaryawan").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "MemoKasbon.aspx/GetKaryawan",
                        data: "{'InputText':'" + document.getElementById('txtkaryawan').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                         dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[1],
                                    hdnkaryawan: item[0]
                                }
                            });

                            $("[id$=hdnkaryawan]").val('');

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
                    $("[id$=hdnkaryawan]").val(i.item.hdnkaryawan);
                },
                minLength: 2
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
                                        <li class="breadcrumb-item"><a href="TrnPinjaman.aspx">Transaksi Pengajuan Kasbon Karyawan</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Transaksi Pengajuan Kasbon Karyawan</h4>
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
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Pinjaman" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                     <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Data Pengajuan Kasbon Karyawan</h4>                           
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
                                                    <asp:TemplateField HeaderText="Kode Nik">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnik" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeNik")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Karyawan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamakar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaKry")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Tanggal Transaksi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     
                                                    <asp:TemplateField HeaderText="Jumlah Kasbon">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljlhdebit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jlhkasbon","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>                                     
                                                    <asp:TemplateField HeaderText="Tenor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljlhangsuran" CssClass="" runat="server" Text='<%#Eval("Tenor","{0:N0}")%>'></asp:Label>                           
                                                        </ItemTemplate>                            
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Angsuran Perbulan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblangsuran" CssClass="" runat="server" Text='<%#Eval("Angsuranperbulan","{0:N2}")%>'></asp:Label>                           
                                                        </ItemTemplate>                            
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
                                    
                                                    <asp:TemplateField HeaderText="Approve Pusat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrnAP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateAddedAP","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Memo Approve Pusat">
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
                                                            <asp:LinkButton ID="BtnSlip" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Slip" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
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
                    <asp:Panel ID="Pnlcustomer" Visible="false" runat="server">
                        <div class="row clearfix">
                           <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                              <div class="row">                     
                                   <div class="col-12">
                                       <div class="card-box ribbon-box">
                                           <div class="ribbon ribbon-custom">Pencarian Karyawan</div>
                                           <p class="text-muted font-14 m-b-30"></p>                                                                           
                                           <div class="form-group col-md-8">
                                                <label class="col-form-label">NIK</label>
                                                <asp:TextBox CssClass="form-control" ID="txtkaryawan" runat="server" required="required"></asp:TextBox>
                                                <asp:HiddenField ID="hdnkaryawan" runat="server" />
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
                                                <p class="text-muted font-14 m-b-30"></p>
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-3 col-form-label">No. Bukti*</label>
                                                    <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox> 
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Transaksi</label>
                                                    <asp:TextBox ID="txttgltrn" Enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="10"  AutoPostBack="true" ></asp:TextBox>      
                                                </div>
                                                <div class="form-row">
                                                    <div class="form-group col-md-4">
                                                       <label for="inputZip" class="col-10 col-form-label">Jumlah Kasbon</label>
                                                       <asp:TextBox ID="txtjmlpinjaman" CssClass="form-control" runat="server" MaxLength="20" required="required" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged="txtjmlpinjaman_change"></asp:TextBox>    
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label for="inputState" class="col-10 col-form-label">Tenor</label>
                                                         <asp:TextBox ID="txtangsuran" CssClass="form-control" Enabled="false"  runat="server" MaxLength="3" Type="number" required="required" AutoPostBack="true" OnTextChanged="txtangsuran_change"></asp:TextBox>   
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                       <label for="inputState" class="col-9 col-form-label">Angsuran Perbulan</label>
                                                       <asp:TextBox ID="txtangsuranperbulan" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" required="required" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>    
                                                    </div>
                                                </div>
                                                <div class="form-row" runat="server" visible="false">
                                                    <div class="col-md-5 justify-content-center">
                                                        <label for="inputState" class="col-9 col-form-label">Potong Langsung Gaji</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                       <asp:RadioButtonList ID="rbpotong" CssClass="col-md-12 col-form-label" runat="server" TextAlign="Right">
                                                           <asp:ListItem Text="&nbsp; Ya" Value ="1" Selected="True"></asp:ListItem>
                                                           <asp:ListItem Text="&nbsp; Tidak" Value ="2"></asp:ListItem>
                                                       </asp:RadioButtonList>    
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-3 col-form-label">Keterangan</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtKeterangan" MaxLength="150"  required="required"  runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <br>
                                                <div class="form-row">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btngenerate" class="btn btn-success waves-light text-white" runat="server" Text="Generate Angsuran" OnClick="btnGenerate_Click" />
                                                    </div>
                                                </div>
                                            </div>                                                    
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                                <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Karyawan</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                    <div class="form-row">
                                                <div class="col-md-4">
                                                    <label for="inputCity" class="col-form-label">NIK</label>
                                                    <%--<asp:DropDownList ID="ddlkdsup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtkodekaryawan" Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox> 
                                                </div>
                                                <div class="col-md-6">
                                                    <label for="inputpid" class="col-3 col-form-label">Nama</label>
                                                    <asp:TextBox ID="txtnamakaryawan" Enabled="false" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                </div>
                                                </div>
                                                <div class="form-row">
                                                <div class="col-md-4">
                                                <p class="mb-1 mt-4 font-weight-bold ">Tanggal Masuk</p>
                                                <asp:TextBox ID="txttgljoin" Enabled="false" CssClass="form-control" required="required" runat="server"  ></asp:TextBox>  
                                                </div>
                                                    <div class="col-md-4">
                                                    <p class="mb-1 mt-4 font-weight-bold ">Masa Kerja</label>
                                                    <asp:TextBox ID="txtmasakerja" Enabled="false" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                </div>
                                                     <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Status Karyawan</p>
                                                            <asp:DropDownList ID="ddlsttskyn" Enabled="false" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>           
                                                </div>
                                                <div class ="form-row">
                                                                
                                                                <div class="col-md-4">
                                                                <p class="mb-1 mt-4 font-weight-bold ">Kantor</p>
                                                                <asp:DropDownList ID="ddloff" Enabled="false" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Net Gaji</p>
                                                                     <asp:TextBox ID="txtnetgaji" CssClass="form-control" enabled="false" runat="server" MaxLength="20" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                     <p class="mb-1 mt-4 font-weight-bold ">Batas Plafon Kasbon</label>
                                                                     <asp:TextBox ID="txtbataspinjaman" enabled="false" CssClass="form-control"  runat="server"   required="required" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>   
                                                                </div>
                                                            </div>
                                                    <div class="row">
                                                              <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Bagian</p>
                                                            <asp:DropDownList ID="ddlbag" Enabled="false" CssClass="selectpicker show-tick" required="required" runat="server" ></asp:DropDownList> 
                                                      </div>

                                                           <div class="col-md-4">
                                  
                                                            <p class="mb-1 mt-4 font-weight-bold ">Jabatan</p>
                                                            <asp:DropDownList ID="ddljab" Enabled="false" CssClass="selectpicker show-tick" required="required" runat="server" ></asp:DropDownList> 
                                                            </div>                            
                                                      
                                                             <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Golongan</p>
                                                            <asp:DropDownList ID="ddlgol" Enabled="false" CssClass="selectpicker show-tick" required="required" runat="server" ></asp:DropDownList> 
                                                            </div>                            
                                                        </div>
                                                <div class="form-row">
                                                    
                                                    <div class="form-group col-md-6">
                                                       <p class="mb-1 mt-4 font-weight-bold ">Saldo Kasbon Belum Lunas</label>
                                                       <asp:TextBox ID="txtsaldobelumlunas" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" required="required" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>    
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                       <p class="mb-1 mt-4 font-weight-bold ">Limit Kasbon</label>
                                                       <asp:TextBox ID="txtlimitkasbon" Enabled="false" CssClass="form-control" runat="server" MaxLength="20" required="required" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>    
                                                    </div>
                                                    
                                                </div>
                                                <div class="form-group">
                                                    <%--<label for="inputpid" class="col-9 col-form-label">Type Seleksi Jurnal</label>--%>
                                                    <asp:DropDownList visible="false" ID="ddltypejur" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" required="required"  width ="400" enabled="false">
                                                        <asp:ListItem Value="-">Silahkan Pilih</asp:ListItem>
                                                        <asp:ListItem Value="1">Double Line Jurnal</asp:ListItem>
                                                        <asp:ListItem Value="2">Multipe Line Jurnal</asp:ListItem>
                                                        <asp:ListItem Value="3">Tidak Pakai Jurnal</asp:ListItem>
                                                    </asp:DropDownList>     
                                                </div>
                                                <div class="form-row">
                                                    <div class="form-group col-md-4">
                                                        <%--<label for="inputState" class="col-form-label">Kode Journal</label>--%>
                                                         <asp:TextBox ID="txtkodejournal" visible="false" CssClass="form-control"  runat="server" MaxLength="50" Enabled="false"></asp:TextBox>   
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                       <%--<label for="inputZip" class="col-form-label">Nama Journal</label>--%>
                                                       <asp:TextBox ID="txtnamajurnal" visible="false" CssClass="form-control" runat="server" MaxLength="50" Enabled="false"></asp:TextBox>    
                                                    </div>
                                                </div>
                                                <div class="form-group" hidden="hidden">
                                                   <%--<label for="inputpid" class="col-3 col-form-label">Currency</label>--%>
                                                   <asp:DropDownList ID="ddlkursSup" visible="false" enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkursSup_SelectedIndexChanged"></asp:DropDownList>
                                                   <asp:TextBox ID="txtkursstd" visible="false" CssClass="form-control" enabled="false" runat="server" MaxLength="50" onkeypress="return isnumerickey(event);"></asp:TextBox>                                                                                              
                                                </div>        
                                            </div>                                                    
                                        </div>
                                    </div>
                                    <div class="col-lg-6" hidden="hidden">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Cash Bank</div>
                                                <p class="text-muted font-14 m-b-30"></p>     
                                                <div class="form-group">
                                                   <label for="inputCity" class="col-form-label">Kode Bank</label>
                                                   <asp:DropDownList ID="ddlKdBank" enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdBank_SelectedIndexChanged"></asp:DropDownList> 
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-3 col-form-label">Nama Bank</label>
                                                    <asp:TextBox ID="txtNmBank" Enabled="false" CssClass="form-control"  runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-3 col-form-label">Currency</label>
                                                    <asp:DropDownList ID="ddlkursbank" enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkursBank_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:TextBox ID="txtkursbank" CssClass="form-control" enabled="false" runat="server" MaxLength="50" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                </div>              
                                            </div>                                                    
                                        </div>
                                    </div>
                                    <div class="col-lg-6" hidden="hidden">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="form-row">
                                                    <div class="form-group col-md-4">
                                                        <%--<label for="inputCity" class="col-form-label">Bank Giro</label>--%>
                                                       <asp:TextBox ID="txtBankGiro" CssClass="form-control" required="required" runat="server" MaxLength="30" Visible="false"></asp:TextBox>    
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <%--<label for="inputState" class="col-form-label">No Giro</label>--%>
                                                         <asp:TextBox ID="txtnoGiro" CssClass="form-control"  runat="server" MaxLength="10" Visible="false"></asp:TextBox>
                                                     </div>
                                                </div>
                                                <div class="form-row">
                                                    <div class="form-group col-md-4">
                                                                    <label for="inputCity" class="col-form-label">Simpanan Pokok</label>
                                                                    <asp:TextBox ID="txttransAP" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="false" ></asp:TextBox>      
                                                                </div>
                                                    <div class="form-group col-md-4">
                                                        <label for="inputState" class="col-form-label">Simpanan Wajib</label>
                                                        <asp:TextBox ID="txtjmlhGL" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="false"></asp:TextBox>
                                                        <asp:HiddenField ID="hfjmlrp" runat="server" />     
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label for="inputZip" class="col-form-label">Total Simpanan</label>
                                                        <asp:TextBox ID="txtjmlhCL" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="false"></asp:TextBox>  
                                                    </div>
                                                </div>
                                            </div>                                                    
                                        </div>
                                    </div>
                                </div>      
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PnlGrid" Visible="false" runat="server">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="card-box"> 
                                     <h4 class="Page-title">Data Rincian Jatuh Tempo</h4>
                                     <div class="form-row">
                                         <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRRP" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRRP_RowCommand" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                   <%-- <asp:TemplateField HeaderText="No Rekening">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  --%>
                                                        <asp:TemplateField HeaderText="Bulan Periode">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbulan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bulan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Tahun Periode">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltahun" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tahun")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Tgl Jatuh Tempo">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgljthtempo","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Angs-Ke">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblangske" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angske")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Angsuran">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblangsuran" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuran","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Keterangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketerangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
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
                    <asp:Panel ID="pnltypejurnal" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box">  
                                    <div class="form-row"> 
                                        <div class="form-group col-md-6">
                                            <div class="form-group"> 
                                                <p class="Page-title">4. Double Line Journal</p>
                                                <asp:DropDownList ID="ddlJurnal" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlJurnal_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:TextBox ID="txtJurnal" CssClass="form-control" runat="server" MaxLength="40" Enabled="false"></asp:TextBox>
                                            </div>    
                                        </div>
                                    </div>
                                </div>
                            </div> 
                         </div>
                        <div class="row">

                             <%--debit--%>
                             <div class="col-lg-6">
                                 <div class="col-lg-12">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Debit GL</div>
                                        <p class="text-muted font-14 m-b-30"></p>                                                                           
                                            <div class="form-row">
                                        <div class="form-group col-md-4">
                                            <label for="inputCity" class="col-form-label">Account Debit</label>
                                            <asp:TextBox ID="txtDebit1" CssClass="form-control" runat="server" MaxLength="100" ReadOnly ="true"></asp:TextBox>       
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label for="inputZip" class="col-form-label">Nama Account debit</label>
                                            <asp:TextBox ID="txtDebit3" CssClass="form-control" runat="server" MaxLength="100" ReadOnly ="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label for="inputState" class="col-form-label">Cost Center</label>
                                            <asp:TextBox ID="txtDebit2" CssClass="form-control" runat="server" MaxLength="100" ReadOnly ="true"></asp:TextBox>     
                                        </div>
                                    
                                    </div>
                                        
                                    
                                                                      
                                    </div>                                                    
                                </div>
                               </div>

                             <%--kredit--%>
                             <div class="col-lg-6">
                                 <div class="col-lg-12">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Kredit GL</div>
                                        <p class="text-muted font-14 m-b-30"></p>                                                                           
                                            <div class="form-row">
                                        <div class="form-group col-md-4">
                                            <label for="inputCity" class="col-form-label">Account Kredit</label>
                                            <asp:TextBox ID="txtKredit1" CssClass="form-control" runat="server" MaxLength="100" ReadOnly ="true"></asp:TextBox>     
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label for="inputZip" class="col-form-label">Nama Account Kredit</label>
                                            <asp:TextBox ID="txtKredit3" CssClass="form-control" runat="server" MaxLength="100" ReadOnly ="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label for="inputState" class="col-form-label">Cost Center</label>
                                             <asp:TextBox ID="txtKredit2" CssClass="form-control" runat="server" MaxLength="100" ReadOnly ="true"></asp:TextBox>   
                                        </div>
                                    
                                    </div>
                                        
                                    
                         
                                    </div>                                                    
                                </div>
                               </div>

                         </div>
                    </asp:Panel>
                    <asp:Panel ID="Pnlfooter1" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                               <div class="card-box">
                                   <div class="card-box table-responsive">
                                       <asp:Button ID="btnNext1" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="btnnext1_Click" Width="65px" />
                                       <asp:Button ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
                                   </div>
                               </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Pnlfooter2" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                               <div class="card-box">
                                   <div class="card-box table-responsive">
                                    <asp:Button ID="btnNext2" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="btnnext2_Click" Width="65px" />
                                    <asp:LinkButton ID="btnback2" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnback2_Click" Width="65px" />
                                   </div>
                               </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Pnlfooter3" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                               <div class="card-box">
                                   <div class="card-box table-responsive">
                                       <asp:Button ID="btnnext3" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="btnnext3_Click" Width="65px" />
                                       <asp:LinkButton ID="btnback3" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnback3_Click" Width="65px" />
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
                                        <asp:Button ID="btnback4" CssClass="btn btn-success waves-light text-white" runat="server" Text="back" OnClick="btnback4_Click"  Width="65px" />
                                        <asp:Button ID="btnclose" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnCancel_Click" Text="close" Width="65px" />
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
                <asp:PostBackTrigger ControlID="btnNext2" />
                <asp:PostBackTrigger ControlID="btnAdd" />   
                <asp:PostBackTrigger ControlID="GRData" />  
                <asp:PostBackTrigger ControlID="GRRP" />
                <asp:PostBackTrigger ControlID="btnback3" />
                <asp:PostBackTrigger ControlID="btnback2" />
                <asp:PostBackTrigger ControlID="btnNext1" />
                <asp:PostBackTrigger ControlID="btnNext3" />
                <asp:PostBackTrigger ControlID="btnback4" />
                <asp:PostBackTrigger ControlID="btngenerate" />
                
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



