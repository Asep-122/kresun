<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewUpdateMasterKaryawan.aspx.cs" Inherits="CL_Process_ViewUpdateMasterKaryawan" %> 
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
                        url: "MemoKaryawanExisting.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                        dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {

                                    Hdnnik: item[0],
                                    label: item[1]


                                }
                            });


                            $("[id$=Hdnnik]").val('');



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


              $("[id$=Hdnnik]").val(i.item.Hdnnik);



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
                                        <li class="breadcrumb-item"><a href="#">Account Employee</a></li>
                                        <li class="breadcrumb-item"><a href="#">View</a></li>
                                        <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">Update Data Karyawan</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Update Data Karyawan</h4>
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
                                            <div class="ribbon ribbon-custom">Pencarian NIK</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <label class="col-form-label">Entry Data NIK</label>
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

                                <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Pembuatan Data Karyawan</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  
                                                       <asp:HiddenField ID="Hdnnik" runat="server" />
                                                   


                                                  <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Nik</p>
                                                             <asp:TextBox ID="txtnik" CssClass="form-control" runat="server" MaxLength="30" Text="-" ReadOnly="true"></asp:TextBox>
                                                      <div>
                                                              <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Urut Pajak</p>
                                                            <asp:TextBox ID="txturutpajak" CssClass="form-control" Enabled="True"  runat="server" MaxLength="3" Type="number" ></asp:TextBox>   
                                                      </div>  
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nama Karyawan</p>
                                                            <asp:TextBox ID="txtnmkyn" CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                      </div>  

                                                    

                                                       <div class="row">
                                                                <%--<div class="col-md-6">--%>

                                                            <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Npwp Karyawan</p>
                                                            <asp:TextBox ID="txtnpwpkyn" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                            </div>  
                                                           <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">NIK KTP</p>
                                                            <asp:TextBox ID="txtnikktp" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                            </div>
                                                                 <div class="col-md-4">
                                                         <p class="mb-1 mt-4 font-weight-bold ">Tanggal Diterima</p>
                                                            <asp:TextBox ID="txttgljoin" CssClass="form-control" required="required" runat="server"  ></asp:TextBox>  
                                                                </div>
                                                             

                                                        </div>   
                                                       <div class="row">
                                                                <div class="col-md-4">
                                                                     <p class="mb-1 mt-4 font-weight-bold ">Tanggal Lahir</p>
                                                                    <asp:TextBox ID="txttgllahir" CssClass="form-control" required="required" runat="server"  ></asp:TextBox> 
                                                      
                                                                </div>

                                                               <div class="col-md-4">
                                                     
                                                     
                                                            <p class="mb-1 mt-4 font-weight-bold ">No Telpon</p>
                                                             <asp:TextBox ID="txtnotelpon" CssClass="form-control"  runat="server" MaxLength="20" Text="0" onkeypress="return isnumerickey(event);" ></asp:TextBox>
                                                      </div>                          
                                                              <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Email</p>
                                                                     <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" MaxLength="50" ></asp:TextBox> 
                                                              </div>   
                                                                                                                

                                                         </div>

                                                                                                

                                                          <div class="row">
                                                                <div class="col-md-4">
                                                                     <p class="mb-1 mt-4 font-weight-bold ">Pendidikan Terakhir</p>
                                                                    <asp:DropDownList ID="ddlpddk" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      
                                                                </div>

                                                               <div class="col-md-4">
                                                     
                                                     
                                                            <p class="mb-1 mt-4 font-weight-bold ">Jenis Kelamin</p>
                                                            <asp:DropDownList ID="ddlgen" CssClass="selectpicker show-tick" required="required" runat="server" Height="16px">  
                                                                <asp:ListItem Text="1. Laki - Laki" Value="L"  Selected="true"/>
                                                                <asp:ListItem Text="2. Perempuan" Value="P"/>
                                                            </asp:DropDownList> 
                                                      </div>                          
                                                              <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Status PTKP</p>
                                                                    <asp:DropDownList ID="ddlsttptkp" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                              </div>   
                                                                                                                

                                                         </div>

                                                                  
                                                               
                                                               <div class="row">
                                                               
                                                               </div> 

                                                        <div class="row">

                                                              <div class="col-md-4">
                                                                   <p class="mb-1 mt-4 font-weight-bold ">Type Tenaga Kerja</p>
                                                            <asp:DropDownList ID="ddltngkrj" CssClass="selectpicker show-tick" required="required" runat="server">  
                                                                <asp:ListItem Text="1. WNI" Value="WNI"  Selected="true"/>
                                                                <asp:ListItem Text="2. WNA" Value="WNI"/>
                                                            </asp:DropDownList> 
                                                               </div>

                                                                <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Status Karyawan</p>
                                                            <asp:DropDownList ID="ddlsttskyn" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                      
                                                       <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">System Bayar</p>
                                                            <asp:DropDownList ID="ddlsysbyr" CssClass="selectpicker show-tick" required="required" runat="server">   
                                                                <asp:ListItem Text="1. Jam" Value="Jam" /> 
                                                                 <asp:ListItem Text="2. Harian" Value="Harian"/> 
                                                                <asp:ListItem Text="3. Bulanan" Value="Bulanan" Selected="true"/>
                                                            </asp:DropDownList> 
                                                      </div> 
                                                   </div>
                                                      </div>   
                                                </div>
                                                   <div class="form-group col-md-6">
                                                    <div class="">  
                                                         <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Unit Bisnis</p>
                                                            <asp:DropDownList ID="ddlunitbisnis" Enabled="false" CssClass="selectpicker show-tick" required="required" runat="server" ></asp:DropDownList> 
                                                      </div>  

                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kantor</p>
                                                            <asp:DropDownList ID="ddloff" Enabled="false" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                    
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Bagian</p>
                                                            <asp:DropDownList ID="ddlbag" CssClass="selectpicker show-tick" required="required" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlbagian_view_SelectedIndexChanged"></asp:DropDownList> 
                                                      </div>
                                                        

                                                          <div class="row">

                                                           <div class="col-md-6">
                                  
                                                            <p class="mb-1 mt-4 font-weight-bold ">Jabatan</p>
                                                            <asp:DropDownList ID="ddljab" CssClass="selectpicker show-tick" required="required" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlstdgaji_view_SelectedIndexChanged"></asp:DropDownList> 
                                                            </div>                            
                                                      
                                                             <div class="col-md-6">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Golongan</p>
                                                            <asp:DropDownList ID="ddlgol" CssClass="selectpicker show-tick" required="required" AutoPostBack ="true" runat="server" OnSelectedIndexChanged="ddlstdgaji_view_SelectedIndexChanged"></asp:DropDownList> 
                                                            </div>                            
                                                        </div>   
                                                      
                                                    
                                                      <div class="row">

                                                           <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Type Penggajian</p>
                                                            <asp:DropDownList ID="ddlpembeli" CssClass="selectpicker show-tick" required="required" runat="server">   
                                                            <asp:ListItem Text="1. Bukan Penggajian" Value="1" Selected="true" /> 
                                                            <asp:ListItem Text="2. Penggajian" Value="2"/>                                                            
                                                            </asp:DropDownList> 
                                                            </div>     
                                                              <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Sebagai Marketing</p>
                                                            <asp:DropDownList ID="ddlmarketing" CssClass="selectpicker show-tick" required="required" runat="server" OnSelectedIndexChanged="ddlmarketing_OnChange" AutoPostBack="true">   
                                                                <asp:ListItem Text="1. Bukan Marketing" Value="1" Selected="true" /> 
                                                                <asp:ListItem Text="2. Marketing" Value="2"/>                                                            
                                                            </asp:DropDownList> 
                                                                </div>                         
                                                      <div runat="server" visible="false" id="divbos">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Bos ID</p>
                                                            <asp:DropDownList ID="ddlbosid" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>     
                                                                            
                                                  
         
                                                    </div>
                                                   
                                                           <div runat="server" id="divsales">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Sales System</p>
                                                               <%--<asp:TextBox ID="ddlsales" CssClass="form-control" runat="server" MaxLength="30" ></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlsales" CssClass="selectpicker show-tick"  runat="server"></asp:DropDownList> 
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
     
       <asp:Panel ID="Panel1" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Pembuatan Data Gaji dan Pembayaran</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                    <div class="">    
                                                                    

                                                             <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Type THR</p>
                                                            <asp:DropDownList ID="ddltypethr" CssClass="selectpicker show-tick" required="required" runat="server" > 
                                                                <asp:ListItem Text="Tunjangan Hari Raya" Value="THR"  Selected="true"/> 
                                                                <asp:ListItem Text="Tunjangan Hari Natal" Value="THN"/> 
                                                            </asp:DropDownList> 
                                                            </div> 


                                                            <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Standar Gaji</p>
                                                            <asp:DropDownList ID="ddlstdgaji" CssClass="selectpicker show-tick" runat="server"  AutoPostBack="true" OnSelectedIndexChanged ="ddlstd_gaji_SelectedIndexChanged"></asp:DropDownList> 
                                                                </div> 
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Gaji Pokok Max</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-6">
                                                                     <asp:TextBox ID="txtgjmx" Enabled="false" CssClass="form-control" runat="server" MaxLength="40" text="0,00"  readonly ="true" onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                     
                                                          
                                                     
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Gaji Pokok/Bln</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                     <asp:TextBox ID="txtgapok" CssClass="form-control"  runat="server" MaxLength="50" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      
                                         
                                                     
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Tunjangan Makan</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:TextBox ID="txttumakan"  CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                             
                                                    
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                     <p class="mb-1 mt-4 font-weight-bold ">Tunjangan Transport</p>
                                                                        <%--<asp:TextBox ID="TextBox7" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Transport" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                     <asp:TextBox ID="txttutrp"  CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                     
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Net Gaji</p>
                                                                        <%--<asp:TextBox ID="TextBox3" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Net Gaji" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                     <asp:TextBox ID="txtnetgaji" CssClass="form-control" enabled="false" runat="server" MaxLength="20" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                     
                                                            <div class ="form-row"> 
                                                                 <div class="col-md-6">
                                                                     <p class="mb-1 mt-4 font-weight-bold ">Tunjangan Hari Raya</p>
                                                                        <%--<asp:TextBox ID="TextBox5" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Hari Raya" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                     <asp:TextBox ID="txttunjhrRaya" Enabled="false" CssClass="form-control" required="required" runat="server" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>  
                                                                </div>
                                                            </div>
                                                      </div>        
                                                     

                                                          
                                                    </div>   
                                                
                                          
                                                   <div class="form-group col-md-6">
                                                    <div class="">    
                                                                    
                                                         <div class="row">
                                                                <div class="col-md-6">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Cara Pembayaran</p>
                                                            <asp:DropDownList ID="ddlbyr" CssClass="selectpicker show-tick" required="required" runat="server" AutoPostBack="true" OnSelectedIndexChanged ="ddlbyr_SelectedIndexChanged"> 
                                                                <asp:ListItem Text="1. Transfer" Value="T"  Selected="true"/> 
                                                                <asp:ListItem Text="2. Cash" Value="C"/> 
                                                            </asp:DropDownList> 
                                                      </div> 
                                                              
                                                         </div> 


                                                      <div class="row">
                                                                <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Bank Perusahaan</p>
                                                            <asp:DropDownList ID="ddlrekpersh" CssClass="selectpicker show-tick" required="required" runat="server" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnSelectedIndexChanged ="ddlrekpersh_SelectedIndexChanged"></asp:DropDownList> 
                                                              </div> 

                                                            <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nama Bank Perusahaan</p>
                                                            <asp:TextBox ID="txtnmbankprsh" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>   
                                                            </div>     
                                                            <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Rek Bank Perusahaan</p>
                                                            <asp:TextBox ID="txtrekbankprsh" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>   
                                                            </div>  
                                                             
                                                      </div>


                                                         <div class="row">
                                                               

                                                            <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Nama Bank Karyawan</p>
                                                                    <asp:TextBox ID="txtnmbakkyn" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox> 
                                                              </div> 
                                                              <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Cabang Bank Karyawan</p>
                                                            <asp:TextBox ID="txtcbgbankkyn" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox> 
                                                            </div> 
                                                            <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Rek. Bank Karyawan</p>
                                                            <asp:TextBox ID="txtrekkyn" CssClass="form-control" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);"></asp:TextBox> 
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
                </div>
        </asp:Panel>          
       
 
<asp:Panel ID="BPJSKESEHATANV" runat="server" >
               <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Perubahan Data BPJS Kesehatan</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                            
                                                     <div class="">
                                                      
                                                          <div class="row">
                                                                 <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nomor BPJS Kesehatan</p>
                                                            <asp:TextBox ID="txtnobpjskesehatan" CssClass="form-control" required="required" runat="server"  Text="0" MaxLength="15" onkeypress="return isnumerickey(event);">0</asp:TextBox> 
                                                            </div>
                                                               <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tanggal BPJS Kesehatan</p>
                                                            <asp:TextBox ID="txttglkesehatan"  CssClass="form-control" runat="server"  ></asp:TextBox>   
                                                            </div>  
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Net Gaji Kesehatan</p>
                                                                       <asp:TextBox ID="txtnetgajiKesehatan" CssClass="form-control" runat="server" MaxLength="20" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  ></asp:TextBox> 
                                                                </div>


                                                         </div>

                                                             
                                                          
                                                      </div>
                                                   
                                                          
                                                </div>   
                                                
                                          
                                                   <div class="form-group col-md-6">
                                                            <div class="">
                                                           <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Type Potongan BPJS Kesehatan</p>
                                                             <asp:DropDownList ID="ddlbpjskesehatan"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" AutoPostBack="true" OnSelectedIndexChanged ="ddlbpjskesehatan_gaji_SelectedIndexChanged"></asp:DropDownList>
                                                          </div>
                                                          
                                                         <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label7"  CssClass="form-control" required="required" runat="server" Text="Dibayar Perusahaan" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txtbpjskesehatanpersenprs" Enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>
                                                                     <%--<asp:DropDownList ID="DropDownList1"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" ></asp:DropDownList>--%>
                                                                </div>
                                                            </div>
                                                         <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label8"  CssClass="form-control" required="required" runat="server" Text="Dibayar Karyawan" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txtbpjskesehatanpersenkry" Enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>
                                                                     <%--<asp:DropDownList ID="DropDownList1"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" ></asp:DropDownList>--%>
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
                </div>
        </asp:Panel>

               <asp:Panel ID="BPJSTENAGAKERJAV" runat="server" >
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data BPJS Tenaga Kerja</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                            
                                                            <div class="">
                                                            <div class ="form-row">
                                                                 <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nomor BPJS KetenagaKerjaan</p>
                                                            <asp:TextBox ID="txtnobpjs" CssClass="form-control" required="required" runat="server"  Text="0" MaxLength="15" onkeypress="return isnumerickey(event);">0</asp:TextBox> 
                                                            </div>  
                                                           
                                                          <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tanggal Jamsostek</p>
                                                            <asp:TextBox ID="txttgljamsostek"  CssClass="form-control"  runat="server"></asp:TextBox>   
                                                            </div> 

                                                                 <div class="col-md-4">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Net Gaji Tenaga Kerja</p>
                                                                    <asp:TextBox ID="txtnetgajitenagakerja" CssClass="form-control" runat="server" MaxLength="20" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>  
                                                            </div> 

                                                                 </div>
                                                          

                                                           <p class="mb-1 mt-4 font-weight-bold ">Type Potongan BPJS Tenaga Kerja</p>

                                                        
                                                                
                                                     <div>
                                                            <p class="mb-1 mt-4 font-weight-bold">Dibayar Karyawan</p>
                                                      </div>
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label5"  CssClass="form-control" required="required" runat="server" Text="Potongan BPJS hari tua" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <%--<asp:TextBox ID="txtbpjshrtua" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>--%> 
                                                                    <asp:DropDownList ID="ddlJHTKry"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" ></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                      </div>
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label6"  CssClass="form-control" required="required" runat="server" Text="Potongan Iuran Pensiun" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <%--<asp:TextBox ID="txtpotiuran" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>--%> 
                                                                    <asp:DropDownList ID="ddlJPkry"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" ></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                      </div>                           
                                                    </div> 
                                                </div>   
                                                
                                          
                                                   <div class="form-group col-md-6">
                                                        
                                                            <div class="">
                                                         <p class="mb-1 mt-4 font-weight-bold">Dibayar Perusahaan</p> 
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label1"  CssClass="form-control" required="required" runat="server" Text="Tunjangan Hari Tua" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <%--<asp:TextBox ID="txttuntua" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>--%> 
                                                                     <asp:DropDownList ID="ddlJHTPrs"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" ></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                      </div>     
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label2"  CssClass="form-control" required="required" runat="server" Text="Tunjangan BPJS Kematian" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <%--<asp:TextBox ID="txtbpjsmati" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>--%> 
                                                                        <asp:DropDownList ID="ddlJKMPrs"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" ></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                      </div>  
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label3"  CssClass="form-control" required="required" runat="server" Text="Tunjangan BPJS Kecelakaan" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <%--<asp:TextBox ID="txtbpjskecelakaan" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>--%> 
                                                                            <asp:DropDownList ID="ddlJKKPrs"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" ></asp:DropDownList>
                                                                    </div>
                                                            </div>
                                                      </div>
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label4"  CssClass="form-control" required="required" runat="server" Text="Iuran Pensiun" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <%--<asp:TextBox ID="txtiuran" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>--%> 
                                                                        <asp:DropDownList ID="ddlJPPrs"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" ></asp:DropDownList>
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
                </div>
                </div>
        </asp:Panel>

                    
                    
                                               <div class="hidden-print mt-4">
                                                    <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnCancels" align="right" class="btn btn-success waves-light text-white" runat="server" Text="Update" OnClick="btnSimpan_Click" />
                                                        

                                                    </div>
                                            </div>   


                  
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



