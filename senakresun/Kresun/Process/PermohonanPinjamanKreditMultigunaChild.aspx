<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermohonanPinjamanKreditMultigunaChild.aspx.cs" Inherits="Kresun_Process_PermohonanPinjamanKreditMultigunaChild" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Permohonan Pinjaman Kredit Multiguna</title>
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
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">
   
    <script type="text/javascript">
        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }

        function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: t,
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 500,
                    stack: 1,
                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 10); //Delete
                        }


                    }
                });
            }, 0);

        }
        function IsValid(args) {
            if (args.value.length == 0) {
                return false;
            }
            else {
                return true;
            }
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
       <%--   function CheckFileExistencektppasangan() {

            var filePath = document.getElementById('<%= this.fileuploadktppasangan.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File KTP Pasangan belum di pilih!");


                return false;
            }
        }--%>
       <%--  function CheckFileExistenceNPWP() {

            var filePath = document.getElementById('<%= this.fileuploadnpwp.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto NPWP belum di pilih!");


                return false;
            }
        }--%>
        <%--  function CheckFileExistenceKK() {

            var filePath = document.getElementById('<%= this.fileuploadKK.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto KK belum di pilih!");


                return false;
            }
        }--%>

        function Simpan() {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil disimpan",
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
        //function SweetAlert(errMsg) {

        //    setTimeout(function () {
        //        swal(
        //            {
        //                title: errMsg,
        //                position: 'top',
        //                confirmButtonClass: 'btn btn-confirm mt-2'
        //            }
        //        ).then(function () {
        //            document.getElementById('ddlcabang').focus();
        //        });
        //    }
        //) }

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
        } // "

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

      <%--   function CheckFileExistenceJaminan() {

            var filePath = document.getElementById('<%= this.fileuploadjaminan.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto Jaminan belum di pilih!");


                return false;
            }
        }--%>

        function Comma(Num) { //function to add commas to textboxes

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



        $(document).ready(InIEvent);
        function InIEvent() {
            $.getScript("/assets/js/jquery.core.js");
        
            $("div").remove(".tooltip");
            //            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

              <%--  {
                $('#<%=txtketeranganJaminan.ClientID%>').prop('maxlength', 250);
                
            }--%>
            $('#txttglptkp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahirpasangan').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('#txttglrealisasi').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgltagih').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtglskpensiun').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtmtpensiun').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahir').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiranak1').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiranak2').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiranak3').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahirpp').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiraw').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('.selectpicker').selectpicker();

            $("<i class='mdi mdi-chevron-down'></i>").insertAfter(".filter-option");

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

            });

            $("#txtnonasabah").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "PermohonanPinjamanKreditMultigunaNew.aspx/GetNoNasabah",
                        data: "{'InputText':'" + document.getElementById('txtnonasabah').value + "','SessionBranchID':'" + document.getElementById('ddlcabang').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[0],
                                    hdnZipCode: item[1]
                                }
                            });

                            $("[id$=hdnZipCode]").val('');

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
                    $("[id$=hdnZipCode]").val(i.item.hdnZipCode);
                },
                minLength: 3
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
            
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Kredit Pensiun</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="PermohonanPinjamanKreditMultiguna.aspx">Permohonan Pinjaman Kredit Pensiun</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Permohonan Pinjaman Kredit Pensiun</h4>
                        </div>
                    </div>
                </div>


 
           <asp:Panel ID="PnlSKPensiun" Visible="true"  runat="server">
                      
            <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA SK PENSIUN & PENDAPATAN GAJI</div>
                                                       <p class="m-b-0"></p>
                                 
                                                                 <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data SK Pensiun</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                     

                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Instansi</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:DropDownList ID="ddlinstansi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" ></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">No Pensiun</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:TextBox CssClass="form-control" ID="txtnopen" text="0" MaxLength="50"  runat="server"  ></asp:TextBox> 
                                                                </div>
                                                            </div>


                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">NIP</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:TextBox CssClass="form-control" ID="txtnip" text="0" MaxLength="50"  runat="server"  ></asp:TextBox> 
                                                                </div>
                                                            </div>                                                           
                                                        <div class ="form-row" id="divNik" runat="server" visible="false">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">NIK A</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:TextBox CssClass="form-control" ID="txtnikA" text="0" MaxLength="50"  runat="server"  ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                           

                                                
                                                             <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Nama Sesuai SK</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:TextBox CssClass="form-control" ID="txtnamasesuaisk" text="0" MaxLength="80"  runat="server"  ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                          <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">NO SK</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:TextBox CssClass="form-control" ID="txtnosk" text="0" MaxLength="50"  runat="server"  ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">No Dapem</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:TextBox CssClass="form-control" ID="txtnodapem" text="0" MaxLength="50"  runat="server"  ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                
                                                          <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Tanggal SK Pensiun</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:TextBox CssClass="form-control" ID="txtglskpensiun"   TextMode="DateTime" runat="server"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      
                                                       
                                                         <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Status Pernikahan</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                   <asp:DropDownList ID="ddlstatusnikah"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                            <asp:ListItem Value="-">Pilih Status</asp:ListItem>
                                                                            <asp:ListItem Value="0">Tidak Kawin</asp:ListItem>
                                                                            <asp:ListItem Value="1">Kawin</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                          <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Jenis Pensiun</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:DropDownList ID="ddlpensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack ="true">
                                                                            <asp:ListItem Value="">Pilih Pensiun</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sendiri</asp:ListItem>
                                                                            <asp:ListItem Value="2">Janda</asp:ListItem>
                                                                            <asp:ListItem Value="3">Duda</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                      
                                                          <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Kode Jiwa</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:DropDownList ID="ddlkodejiwa" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                           <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">TMT Pensiun</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:TextBox CssClass="form-control" ID="txtmtpensiun"   TextMode="DateTime" runat="server" AutoPostBack ="true" OnTextChanged="Data_Formula"></asp:TextBox> 
                                                                </div>
                                                            </div>                                                           
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">NO NPWP</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:TextBox CssClass="form-control" ID="txtnonpwp" text="0" MaxLength="50"  runat="server"  ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Jenis Kelamin</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                     <asp:DropDownList ID="ddljeniskelaminpensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                     <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Tanggal Lahir</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                        <asp:TextBox ID="txttgllahir" CssClass="form-control" TextMode="DateTime" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                  
                                                    
                    

                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">PENDAPATAN GAJI</div>
                                                <p class="text-muted font-14 m-b-30"></p>


                                             
                                                     

                                                             <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Gaji Sesuai Display</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txtgajidisplay" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack ="true" OnTextChanged="Data_Formula"  ></asp:TextBox> 
                                                                </div>
                                                            </div>


                                                            <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Gaji Pokok</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txtgapok" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      
                                                            <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-14">Tunjangan Istri/Anak</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList runat="server" CssClass="selectpicker show-tick" ID="ddlTunjanganIstriAnak" data-style="btn-custom" AutoPostBack="true" >
                                                                        <asp:ListItem Text="Ya" Value="Ya"></asp:ListItem>
                                                                        <asp:ListItem Text="Tidak" Value="Tidak"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>                                         
                                                     
                                                            <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-14">Tunjangan Istri</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">

                                                                     <asp:TextBox ID="txttunjistri"  CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                             
                                                    
                                                            <div class ="form-row">
                                                                <div class="col-md-4">
                                                                     <p class="mb-1 mt-4 font-14">Tunjangan Anak</p>
                                                                        <%--<asp:TextBox ID="TextBox7" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Transport" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txttunjangananak"  CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                        <div class ="form-row">
                                                                <div class="col-md-4">
                                                                     <p class="mb-1 mt-4 font-14">Tunjangan Beras</p>
                                                                        <%--<asp:TextBox ID="TextBox7" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Transport" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txttunjanganberas"  CssClass="form-control"  runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged" ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                             <div class ="form-row">
                                                                <div class="col-md-4">
                                                                     <p class="mb-1 mt-4 font-14">Tunjangan Lain-Lain</p>
                                                                        <%--<asp:TextBox ID="TextBox7" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Transport" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txttunjanganlain"  CssClass="form-control"  runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                     
                                                            <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Gaji Kotor</p>
                                                                        <%--<asp:TextBox ID="TextBox3" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Net Gaji" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txtnetgaji" CssClass="form-control" enabled="false" runat="server" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                        <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Potongan Pajak</p>
                                                                        <%--<asp:TextBox ID="TextBox3" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Net Gaji" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txtpotonganpajak" CssClass="form-control" runat="server" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged"  ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                          


                                                         <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-14">Potongan Askes</p>
                                                                        <%--<asp:TextBox ID="TextBox3" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Net Gaji" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txtpotonganaskes" CssClass="form-control"  runat="server" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged"  ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                       <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-14">Potongan Lain-Lain</p>
                                                                        <%--<asp:TextBox ID="TextBox3" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Net Gaji" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txtpotonganlain" CssClass="form-control" runat="server" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="gajipokok_ontextchanged"  ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                        <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Total Potongan</p>
                                                                        <%--<asp:TextBox ID="TextBox3" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Net Gaji" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txttotalpotongan" CssClass="form-control" enabled="false" runat="server" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"   ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                        <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Gaji Bersih</p>
                                                                        <%--<asp:TextBox ID="TextBox3" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Net Gaji" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-8">
                                                                     <asp:TextBox ID="txtgajibersih" CssClass="form-control" enabled="false" runat="server" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  ></asp:TextBox> 
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

                 <asp:Panel ID="pnlpinjaman" visible="true" runat="server">
                      
                                    <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA PINJAMAN</div>
                                                       <p class="m-b-0"></p>
                                 
                                                                 <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">INPUT</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                     
                                                            
                                                               <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Tgl Realisasi</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txttglrealisasi" TextMode="DateTime" Enabled="false" CssClass="form-control" MaxLength="100" runat="server" AutoPostBack ="true" OnTextChanged="Data_Formula"></asp:TextBox>

                                                                </div>
                                                            </div>
                      
                                                                <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Kantor Bayar</p>
                                                                   
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                  <asp:DropDownList ID="ddlKantorBayar" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" ></asp:DropDownList>
                                                                      <asp:TextBox ID="txtkodesimulasi" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                             
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Sumber Dana</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                   <asp:DropDownList ID="ddlsumberdana" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   AutoPostBack ="true" ></asp:DropDownList>

                                                                </div>
                                                            </div>

                                                 <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">ProgramPinjaman</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                   <asp:DropDownList ID="ddlprogrampinjaman" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack ="true" ></asp:DropDownList>

                                                                </div>
                                                            </div>

                                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Produk</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlproduk" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack ="true" OnSelectedIndexChanged="Data_Formula"></asp:DropDownList>

                                                                </div>
                                                            </div>

                                                          <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Kategori</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlkategori" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack ="true" OnSelectedIndexChanged="Data_Formula" ></asp:DropDownList>

                                                                </div>
                                                            </div>
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">PK</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlPk" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" Enabled="false" OnSelectedIndexChanged="Data_Formula">
                                                                        <asp:ListItem Text="1 PK" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2 PK" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                              <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Jangka Waktu Bulan</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                      <asp:DropDownList ID="ddljangkawaktu" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true"  ></asp:DropDownList>

                                                                </div>
                                                            </div>
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Jangka Waktu Tahun</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                  <asp:TextBox ID="txtjangkatahun" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                       <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Tgl Jatuh Tempo</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                <asp:TextBox ID="txttgljatuhtempo" Enabled="false" CssClass="form-control" TextMode="DateTime" MaxLength="100" runat="server"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                   
                                                              <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Plafon</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                              <asp:TextBox ID="txtplafon" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack ="true" OnTextChanged="Data_Formula" ></asp:TextBox> 

                                                                </div>
                                                            </div>
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Pelunasan/Take Over</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                              <asp:TextBox ID="txtpelunasan" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack ="true" OnTextChanged="Data_Formula" ></asp:TextBox> 

                                                                </div>
                                                            </div>

                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Angsuran Blokir</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlangsurandimukablokir" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true"> </asp:DropDownList>

                                                                </div>
                                                            </div>                                                       
                                                  
                                                    
                                                         <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Angsuran Dimuka Angsuran</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlangsurandimuka" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" > </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                 <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Angsuran Dimuka Asuransi</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlangsurandimukaasuransi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" > </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                   <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Materai</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlmaterai" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" > </asp:DropDownList>

                                                                </div>
                                                            </div>

                                                <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Provisi</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlprovisi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" > </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                                                                         
                                                           

                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Mutasi Kantor Bayar</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                       <asp:DropDownList ID="ddlmutasikantorbayar" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >
                                                                            <asp:ListItem Value="-">Pilih Mutasi Kantor Bayar</asp:ListItem>
                                                                            <asp:ListItem Value="1">Ya</asp:ListItem>
                                                                            <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                                          
                                                                            </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Staff Account Office (AO)</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                      <asp:DropDownList ID="ddlAO" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                     <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Staff Non Account Office (AO)</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                      <asp:DropDownList ID="ddlnonao" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                
                                                            
                                                 

                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">OUTPUT</div>
                                                <p class="text-muted font-14 m-b-30"></p>


                                                <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label38" BackColor="#cccccc" runat="server" class="form-control">Nama</asp:label>
                                                                       <asp:TextBox ID="txtnama" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-8">
                                                                         <asp:label ID="Label39" BackColor="#cccccc" runat="server" class="form-control">Status Pinjaman</asp:label>
                                                                       <asp:TextBox ID="txtstatuspinjaman" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                         <div class ="form-row">
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label40" BackColor="#cccccc" runat="server" class="form-control">Usia Pinjaman</asp:label>
                                                                    <asp:TextBox ID="txtusiapinjaman" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                            <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label41" BackColor="#cccccc" runat="server" class="form-control">Usia Asuransi</asp:label>
                                                                       <asp:TextBox ID="txtusiaasuransi" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>  
                                                               <div class="form-group col-md-3">
                                                                         <asp:label ID="Label42" BackColor="#cccccc" runat="server" class="form-control">Usia Anak 1</asp:label>
                                                                       <asp:TextBox ID="txtusiaanak1" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label47" BackColor="#cccccc" runat="server" class="form-control">Usia Anak 2</asp:label>
                                                                          <asp:TextBox ID="txtusiaanak2" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                </div>


                                                  <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label48" BackColor="#cccccc" runat="server" class="form-control">Plafond Max</asp:label>
                                                                       <asp:TextBox ID="txtplafonmax" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label49" BackColor="#cccccc" runat="server" class="form-control">Jangka Waktu Max (Bulan)</asp:label>
                                                                        <asp:TextBox ID="txtjangkawaktumax" Enabled="false" CssClass="form-control"  runat="server" text="0,00"   onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                 <div class="form-group col-md-4">
                                                                         <asp:label ID="Label24" BackColor="#cccccc" runat="server" class="form-control">Usia Jth Tempo</asp:label>
                                                                       <asp:TextBox ID="txtusiajthtempo" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>  
                                                                </div>
                                            
                                                            <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label50"  BackColor="#cccccc" runat="server" class="form-control">Rate Asuransi %</asp:label>
                                                                       <asp:TextBox ID="txtrateasuransi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label51" BackColor="#cccccc" runat="server" class="form-control">Rate Bunga Flat %</asp:label>
                                                                        <asp:TextBox ID="txtratebungapinjaman" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                  <div class="form-group col-md-4">
                                                                         <asp:label ID="Label25" BackColor="#cccccc" runat="server" class="form-control">Rate Bunga Efektif %</asp:label>
                                                                        <asp:TextBox ID="txtratebungaefektif" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                </div>

                                                  

                                                    <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Angsuran Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtangsuran" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtpsnangsuran" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2" style="display:none">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                 <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Asuransi Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtasuransi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtpersenasuransi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2" style="display:none">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                  <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Total Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txttotal" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txttotalpersen" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2" style="display:none">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                    <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Sisa Gaji Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtsisagaji" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtsisagajipersen" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2" style="display:none">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>


                                                   <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Provisi Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtprovisi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtpersenprovisi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2" style="display:none">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>
                                                 <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Admin Bank Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtadminbank" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtpersenadminbank" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2" style="display:none">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>
                                                <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Keanggotaan Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtkeanggotaan" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>
                                                 <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Materai Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtmaterai" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtmateraipcs" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">Pcs</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                  <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Angsuran Blokir Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtangsuranblokir" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtangsuranblokirkali" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">kali</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Angs. Dimuka angs Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtangsurandimuka" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtangsurandimukakali" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">kali</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                        <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Angs. Dimuka asuransi Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtangdimukaasuransi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtangdimukaasuransikali" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">kali</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>


                                                  <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Biaya Admin Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtbiayaadmin" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>                                                  
                                                <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Grace Periode Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtgraceperiode" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>

                                                  <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Pelunasan/Take Over Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtpelunasanRp" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>

                                                 <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Terima Bersih Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtterimabersihrp" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>

                                                    <%--    <div class ="form-row">
                                                                <div class="col-md-6">
                                                                   <asp:Label ID="lblnopenbaru" runat="server" class="col-form-label " Font-Bold="true" ForeColor="Red" Font-Italic="true" Visible="true"></asp:Label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                     <asp:Label ID="lblnopenlama" runat="server" class="col-form-label " Font-Bold="true" ForeColor="Red" Font-Italic="true" Visible="true"></asp:Label>
                                                                </div>
                                                            </div>
                                                     --%>           

                                            </div>
                                        </div>
                                    </div>


                                </div>

                                    
                                     
                                                   </div>  

                                            </div>
                                         </div>        

                    </asp:Panel>

           <asp:Panel runat="server" ID="pnlbutton">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box ribbon-box">
                            <p class="m-b-0"></p>
                            <div class="form-row">
                                <div class="button-list" align="right" >
                          <asp:LinkButton  ID="btnBackSimulasi" type="button" align="left" runat="server" class="btn btn-success waves-light text-white" UseSubmitBehavior="false" OnClientClick="return false">
                        <i class="fa-solid fa-left-long"></i> Back Simulasi
                        </asp:LinkButton> 
                        <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" type="button" Text="Cancel" OnClick="btnCancel_Click" />
                        <asp:Button ID="btnSimpan" CssClass="btn btn-success waves-light text-white"  type="button" runat="server" Text="SAVE" OnClick="btnSimpan_Click"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');"/>
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
                     
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnSimpan" />

                 
            </Triggers>
        </asp:UpdatePanel>
    </form>

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


</body>
</html>
