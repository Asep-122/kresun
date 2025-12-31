<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemorewardnewmembersVerif.aspx.cs" Inherits="AE_Process_MemorewardnewmembersVerif" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Verifikasi Manager Cabang Dropping Fee SM</title>
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
            {
                $('#<%=txtnoteverifikasi.ClientID%>').prop('maxlength', 100);

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
                        url: "Memorewardnewmembers.aspx/GetKaryawan",
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
                                        <li class="breadcrumb-item"><a href="TrnPinjaman.aspx">Transaksi Verifikasi Manager Cabang Dropping Fee SM</a></li>
                                    </ol>
                                </div> 
                                <h4 class="page-title">Transaksi Verifikasi Manager Cabang Dropping Fee SM</h4>
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
                    
                     <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Data Pengajuan Dropping Fee SM</h4>                           
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
                                                     
                                                    <asp:TemplateField HeaderText="Jumlah Dropping">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljlhdebit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jlhreward","{0:N2}")%>'></asp:Label>
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
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin Approve data ini ?');" title="Approve" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <asp:TextBox ID="txttgltrn" CssClass="form-control"  runat="server" MaxLength="10"  readonly="true" ></asp:TextBox>      
                                                </div>
                                                <div class="form-group">
                                                       <label for="inputZip" class="col-10 col-form-label">Jumlah Dropping</label>
                                                       <asp:TextBox ID="txtjumlahreward" CssClass="form-control" runat="server" MaxLength="20" readonly="true" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    
                                                    </div>
                                                
                                                <div class="form-group" runat="server" visible="false">
                                                    <label for="inputpid" class="col-3 col-form-label">Keterangan</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtKeterangan" MaxLength="100"  required="required"  runat="server" TextMode="MultiLine"></asp:TextBox>
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
                                                                <p class="mb-1 mt-4 font-weight-bold ">Kantor</p>
                                                                <asp:DropDownList ID="ddloff" Enabled="false" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                                </div>
                                                     <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Status Karyawan</p>
                                                            <asp:DropDownList ID="ddlsttskyn" Enabled="false" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
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
                                     <h4 class="Page-title">Tujuan Dropping</h4>
                                     <div class="form-row">

                                                <div class="form-group col-md-4">
                                                    <label for="inputpid" class="col-form-label">Kode Nik</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtnoanggota" MaxLength="30"  runat="server"  ></asp:TextBox>
                                                </div>

                                                    <div class="form-group col-md-4">
                                                       <label for="inputZip" class="col-form-label">Jumlah Dropping</label>
                                                            <asp:TextBox ID="txtplafon" CssClass="form-control" runat="server" text="0" MaxLength="20" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>    
                                                    </div>
                                                   
                                                    <div class="form-group col-md-4">
                                                        <asp:Button ID="btngenerate"  class="btn btn-success waves-light text-white btn-lg" runat="server" Text="Proses Dropping" OnClick="btnGenerate_Click" />
                                                    </div>
                                                                                             
                                          </div>

                                     <div class="form-row">
                                         <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRRP" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRRP_RowCommand" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                   
                                                     <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Kode Nik">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "noanggota")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                          <asp:TemplateField HeaderText="Nama Karyawan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nama")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Jumlah Dropping">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "dropping","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Reward %" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpsnreward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "psnreward","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Reward" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "reward","{0:N2}")%>'></asp:Label>
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
                                                <div class="">
                                                
                                                        <div class="form-group">
                                                    <label class="col-form-label">Catatan Memo Pengajuan Cabang</label>
                                                    <asp:TextBox CssClass="auto-style2" Enabled="false" ID="txtnote" MaxLength="150" runat="server" TextMode="MultiLine" Height="145px" Width="595px"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                    <label class="col-form-label">Catatan Verifikasi Memo Pengajuan Cabang</label>
                                                    <asp:TextBox CssClass="auto-style2" ID="txtnoteverifikasi" MaxLength="150" runat="server" TextMode="MultiLine" Height="145px" Width="595px"></asp:TextBox>
                                                    </div>
                                                    <div class="form-row" runat="server" visible="false">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Bulan</label>
                                          <asp:DropDownList ID="ddlSP" runat="server"  CssClass="form-control" data-style="btn-custom" >
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
                                        <label for="inputCity" class="col-form-label">Tahun Gaji</label>
                                          <asp:DropDownList ID="ddltahun" runat="server"  CssClass="form-control" data-style="btn-custom" >
                                                        <asp:ListItem Value="2020">2020</asp:ListItem>
                                                        <asp:ListItem Value="2021">2021</asp:ListItem>
                                                        <asp:ListItem Value="2022">2022</asp:ListItem>
                                                        <asp:ListItem Value="2023">2023</asp:ListItem>
                                                        <asp:ListItem Value="2024">2024</asp:ListItem>
                                                        <asp:ListItem Value="2025">2025</asp:ListItem>
                                           </asp:DropDownList>        
                                    </div>


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
                </div>
        </asp:Panel>
                    
                    <asp:Panel ID="Pnlfooter4" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="card-box table-responsive">
                                       <%-- <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btndelete" CausesValidation="TRUE" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');" CssClass="btn btn-success waves-light text-white" runat="server" Text="Delete" OnClick="btndelete_click" Enabled="true"/>--%>
                                      
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
                <%--<asp:PostBackTrigger ControlID="btnSimpan" />--%>
                <asp:PostBackTrigger ControlID="btnCancel" />
               
<%--                <asp:PostBackTrigger ControlID="btnAdd" />  --%> 
                <asp:PostBackTrigger ControlID="GRData" />  
                <asp:PostBackTrigger ControlID="GRRP" />
               
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



