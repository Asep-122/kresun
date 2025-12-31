<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" MaintainScrollPositionOnPostback="true" CodeFile="PermohonanPinjamanKreditMultigunaNew_pk2.aspx.cs" Inherits="AR_Process_PermohonanPinjamanKreditMultigunaNew_pk2" %>

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

        function openModal() {
            $('#ModalChild').modal('show');

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
        function CheckFileFoto1() {

            var filePath = document.getElementById('<%= this.fileupload1.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File 1 belum di pilih!");


                return false;
            }
        }


        function CheckFileFoto2() {

            var filePath = document.getElementById('<%= this.fileupload2.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File 2 belum di pilih!");


                return false;
            }
        }

        function CheckFileFoto3() {

            var filePath = document.getElementById('<%= this.fileupload3.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File 3 belum di pilih!");


                return false;
            }
        }

        function CheckFileFoto4() {

            var filePath = document.getElementById('<%= this.fileupload4.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File 4 belum di pilih!");


                return false;
            }
        }
        function CheckFileFoto5() {

            var filePath = document.getElementById('<%= this.fileupload5.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File 5 belum di pilih!");


                return false;
            }
        }

        function CheckFileFoto6() {

        var filePath = document.getElementById('<%= this.fileupload6.ClientID %>').value;

                if (filePath.length < 1) {

                    alert("File 6 belum di pilih!");


                    return false;
                }
        }

        $(function () {
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                var activeTab = $(e.target).attr("href");
                $('#<%= hfActiveTab.ClientID %>').val(activeTab);
            });
         });

        $(document).ready(InIEvent);
        function InIEvent() {
            $.getScript("/assets/js/jquery.core.js");
            $("#<%= btnNextAhliWaris.ClientID %>").click(function () {
                $('#AhliWaris-tab').tab('show');
            });
            $("#<%= btnBackAnggota.ClientID %>").click(function () {
                $('#DataAnggota-tab').tab('show');
            });
            $("#<%= btnNextSimulasi.ClientID %>").click(function () {
                $('#Simulasi-tab').tab('show');
            });
            $("#<%= btnBackAhliWaris.ClientID %>").click(function () {
                $('#AhliWaris-tab').tab('show');
            });
            $("#<%= btnNextAsuransi.ClientID %>").click(function () {
                $('#Asuransi-tab').tab('show');
            });
            $("#<%= btnBackSimulasi.ClientID %>").click(function () {
                $('#Simulasi-tab').tab('show');
            });

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



                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                     <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                      <div class="ribbon ribbon-custom">Filter</div>
                                        <p class="m-b-0">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                                <div class="form-group col-md-6">
                                                                    <%--<label class="col-form-label">Kode Cabang</label>--%>
                                                                    <asp:TextBox CssClass="form-control" ID="txtkodecabang" Visible="false" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                                    

                                                                        <div class="form-group">
                                                                        <asp:label ID="Label90" runat="server" class="col-form-label">Pilih Cabang Koperasi</asp:label>
                                                                        <asp:DropDownList ID="ddlcabang" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                        </div>
                                                                    <asp:HiddenField ID="hdnZipCode" runat="server" />

                                                                     <div class="form-group">
                                                                        <label for="inputState" class="col-form-label">No Anggota E.R.P</label>
                                                                         <asp:TextBox CssClass="form-control" ID="txtnonasabah" Enabled="true" MaxLength = "250" runat="server" required="required"></asp:TextBox>
                                                                  <asp:DropDownList ID="ddlSurvey" Required="False" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                         </div>  

                                                                    <div class="button-list">
                                        
                                                                        <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search"  OnClick="btnSearchx_Click"/> 
                                                                 </div>




                                                                    <%--<div class="form-group">
                                                                                                                     
                                                                    </div>--%>
                                                                     <%--<div class="form-group">
                                                                        <asp:Button ID="BtnTarik" class="btn btn-success waves-light text-white" runat="server" Text="Tarik"  OnClick="btnSearchx_Click"/>                                              
                                                                    </div>--%>
                                                                </div>
                                                              <%--  <div class="form-group col-md-6">
                                                                    <%--<div class="form-group">
                                                                        <label runat="server" class="col-form-label">Jenis Data</label>
                                                                        
                                                                    </div>--%>
                                                                  
                                                                </div>
                                                          

                                                         </div> 
                                                    </div>
                                                  </div>
                                           </div> 
                            </div> 
                        </div> 
             

             </asp:Panel>

<div id="nav" runat="server" visible="false" style="font-size:medium;">

          <ul class="nav nav-tabs">
            <li class="nav-item">
            <a class="nav-link active" id="DataAnggota-tab" data-toggle="tab" href="#DataAnggota" aria-selected="true">
                <i class="fa-solid fa-user"></i> <b> Anggota</b> 
            </a>
            </li>

            <li class="nav-item">
            <a class="nav-link" id="AhliWaris-tab" data-toggle="tab" href="#AhliWaris">
                <i class="fa-solid fa-users"></i> <b> Ahli Waris</b> 
            </a>
            </li>

            <li class="nav-item">
            <a class="nav-link" id="Simulasi-tab" data-toggle="tab" href="#Simulasi">
                <i class="far fa-file-alt me-1"></i> <b> Simulasi</b> 
            </a>
            </li>

            <li class="nav-item">
            <a class="nav-link" id="Asuransi-tab" data-toggle="tab" href="#Asuransi">
                <i class="fa-solid fa-user-shield"></i> <b> Asuransi</b> 
            </a>
            </li>
        </ul>
</div>

<div class="tab-content mt-3">
    <asp:HiddenField ID="hfActiveTab" runat="server" />

                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Permohonan Pinjaman</h4>   
                            <div class="form-group col-md-4">



                                                                <asp:label ID="Label54" runat="server" class="col-form-label">Status Permohonan</asp:label>
                                                                    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="selectpicker show-tick form-control" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatus_SelectedIndexChanged" > </asp:DropDownList>
                            
                            </div>
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                     <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"     AutoGenerateColumns="False" OnRowCommand="GRData_RowCommand" >
                                    <Columns>
                                     <asp:TemplateField HeaderText="Id Permohonan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegister" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Idpermohonan")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="KantorBayar">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamakb" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KantorBayar")%>'></asp:Label>
                                                 <asp:Label ID="lblkantorbayar" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakantorbayar")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Sumber Dana">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamasd" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namasumberdana")%>'></asp:Label>
                                                 <asp:Label ID="lblsumberdana" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "sumberdana")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Produk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamaproduk" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaproduk")%>'></asp:Label>
                                                 <asp:Label ID="lblproduk" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Kategori">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamakat" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakategori")%>'></asp:Label>
                                                 <asp:Label ID="lblkategori" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_kategori")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Usia">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usiapeminjam")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Tanggal">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltanggal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tanggal_pengajuan","{0:dd/MM/yyyy}")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anggota">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkodeCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Rekening">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRekening" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Nama">
                                            <ItemTemplate>

                                                <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namanasabah")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>                                        
                                        
                                        
                                       
                                        <asp:TemplateField HeaderText="Jangka">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNpwp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "permohonan_jangka_tahun")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Plafon">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "permohonan_plafon", "{0:N0}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatusket" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Tolak">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltolakket" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alasan_tolak")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Print">
                                                        <HeaderStyle CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="BtnBukti" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Cetak Bukti" CommandName="Bukti" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="BtnKwPinjaman" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Kwitansi Pinjaman" CommandName="Kwitansipinjaman" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="BtnAngsuran" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Angsuran Pinjaman" CommandName="Angsuranpinjaman" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Perjanjian Kredit" CommandName="PerjanjianKredit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Tanda Terima Anggota" CommandName="TandaTerimaAnggota" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton4" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Surat Tanda Terima" CommandName="TandaTerimaJaminan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton5" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Surat Penjamin" CommandName="TandaPenjamin" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                      
                                     
                                            <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit Data" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnApproveAntrian" runat="server" data-toggle="tooltip" data-placement="left" title="Approve Antrian" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-export"></i></asp:LinkButton>
                                                            <%--<asp:LinkButton ID="btnupdate" runat="server" data-toggle="tooltip" data-placement="left" title="Update Data" CommandName="UPDATEX" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-document-edit"></i></asp:LinkButton>--%>
                                                            <%--<asp:LinkButton ID="BtnApproveTeller" runat="server" data-toggle="tooltip" data-placement="left" title="Lanjutkan Ke Teller" CommandName="Lanjutkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-user"></i></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="BtnBatalkanAntrian" runat="server" data-toggle="tooltip" data-placement="left" title="Batalkan Antrian" CommandName="Batalkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-thumbs-down"></i></asp:LinkButton>
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
<div id="DataAnggota" class="tab-pane fade show active">
             <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA ANGGOTA</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                        <asp:HiddenField ID="HDStatusDebitur" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodePos" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodeArea" runat="server" />
                                                        <asp:HiddenField ID="HDGender" runat="server" />
                                                        <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                        <asp:HiddenField ID="HDLamaKerja" runat="server" />
                                                        <asp:HiddenField ID="HDRegister" runat="server" />
                                                        <asp:HiddenField ID="HDid" runat="server" />
                                                            <asp:HiddenField ID="HDnopk" runat="server" />
                                                            <asp:HiddenField ID="HDnopksekarang" runat="server" />
                                                            <asp:HiddenField ID="HDNoPKSebelumnya" runat="server" />
                                                             <asp:HiddenField ID="HDNrekeningkreditbo" runat="server" />
                                 
                                                            <div class="form-row">
                                                                 <div class="form-group col-md-5">
                                                            
                                                                        <asp:label ID="Label168" runat="server" class="col-form-label">Tanggal Bergabung</asp:label>
																        <asp:TextBox ID="txttglbergabung" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 
												
                                                                </div>

                                                                <div class="form-group col-md-4" runat="server" visible="false">
                                                                <asp:label ID="Label166" runat="server" class="col-form-label">No. Anggota CBS</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtanggotacbs"  Enabled="false" runat="server"></asp:TextBox>
                                                          
                                                                </div>
                                                            <div class="fform-group col-md-5">
                                                                <asp:label ID="Label167" runat="server" class="col-form-label">No. Anggota ERP</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnoanggota"  Enabled="false" runat="server"></asp:TextBox>
                                                           <asp:TextBox CssClass="form-control" ID="txtnorektabungan" Visible="false"   runat="server" ></asp:TextBox>
                                                                </div>
                                                          
                                                       


													</div>
                                                         
                                                                
                                                        <div>
                                                                <asp:Image ID="imgktp" runat="server" Height="400px" Width="650px" />                                                                       
                                                            </div> 

                                                                  <div runat="server" visible="false">
                                                                  <asp:FileUpload ID="fileuploadktp"  runat="server"/> 
                                                                <asp:Button ID="btnUploadktp"  Text="Upload" runat="server" OnClick="UploadFileKTP" OnClientClick="return CheckFileExistence()" />
                                                                      <asp:TextBox ID="txtpathktp" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>                                                                      
                                                                </div> 

                                                            
                                                            <div class="form-row">
                                                               





                                                           <div class="form-group col-md-5">
                                                                <asp:label ID="Label46" runat="server" class="col-form-label">Kota Terbit Identitas</asp:label>
																<asp:TextBox ID="txtkotaktp" Enabled="false" CssClass="form-control" MaxLength="30"  runat="server" ></asp:TextBox>
															</div> 

                                                           <div class="form-group col-md-5">
                                                                 <asp:label id="Label91" runat="server"  class="col-form-label">Tanggal Terbit Identitas</asp:label>
                                                                <asp:TextBox ID="txttglterbitidentitasktp" Enabled="false"  CssClass="form-control" runat="server"  TextMode="DateTime"  > </asp:TextBox>
                                                            </div>   
                                                   <div class="form-group" runat="server" visible="false">
                                                                 <asp:label id="Label125" runat="server" Visible="false"  class="col-form-label">Berlaku sampai dengan</asp:label>
                                                                 <asp:TextBox ID="txtmasaberakhirKTP" Visible="false" CssClass="form-control" runat="server"  TextMode="DateTime"  ></asp:TextBox>
                                                            </div>   
                                               </div>
                                    
                                    
                                    
                                                   
                                                         
            
                                                             <div class="form-row">
                                                            <div class="form-group col-md-5">
                                                                <asp:label ID="Label31" runat="server" class="col-form-label">No.HP</asp:label>
																<asp:TextBox ID="txthpktp" Enabled="false" CssClass="form-control" MaxLength="13" Text="0"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                               
                                                            
                                                            </div>

                                                            <div class="form-group col-md-5">
                                                                <asp:label ID="Label43" runat="server" class="col-form-label">Email</asp:label>
																<asp:TextBox ID="txtemailsktp" Enabled="false" CssClass="form-control" MaxLength="40" Text="0"   runat="server" ></asp:TextBox>
                                                                             
                                                            </div>
                                                               </div>                                                         
               
                                                            
                                                            
                                                           
                                                          
															
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            

                                                            <div class="form-group">
                                                                <asp:label ID="Label44" runat="server" class="col-form-label">Nomor Identitas KTP (NIK)</asp:label>
																<asp:TextBox ID="txtnoidentitasktp" Enabled="false" CssClass="form-control"  MaxLength="16" onkeypress="return isnumerickey(event);"     runat="server" Width="219px" ></asp:TextBox>
                                                                              
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Nama Lengkap</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamanasabah" Enabled="false" MaxLength="70"   runat="server" Text="" ></asp:TextBox>
                                                            </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label" Visible="false">Nama Panggilan</asp:label>
																<asp:TextBox ID="txtnamapanggilan" Visible="false" CssClass="form-control" MaxLength="40"  Text="-" runat="server"></asp:TextBox> 
															                                                               
                                                            </div>
                                                            <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:DropDownList ID="ddljenkelktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                          </div>
															
															<div class="form-group col-md-4">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Tempat Lahir</asp:label>
																<asp:TextBox ID="txttempatlahirktp" Enabled="false" CssClass="form-control" Text="" MaxLength="30"  runat="server"  ></asp:TextBox>
															</div>
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label3ccc" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
                                                                  <asp:TextBox CssClass="form-control" ID="txttgllahirktp"  Enabled="false" TextMode="DateTime" runat="server"  ></asp:TextBox>
                                                                                  
                                                            </div> 
                                                            </div>
                                                         
                                                            
                                                                            <div class="form-group">
                                                                <asp:label ID="Label127" runat="server" class="col-form-label">Alamat Rumah Sesuai KTP</asp:label>
																<asp:TextBox ID="txtalamatktp"  Enabled="false"  CssClass="form-control" Text="" MaxLength="100" TextMode="MultiLine"  runat="server" ></asp:TextBox>
                                                               
                                                            
                                                            </div>

                                                            <div class="form-row">
                                                               
                                                            
                                                            
															    <div class="form-group col-md-3">
                                                                <asp:label ID="Label130" runat="server" class="col-form-label">RT KTP</asp:label>
																<asp:TextBox ID="txtrtktp" Enabled="false" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"    runat="server" ></asp:TextBox>
											                             
                                                            </div>

															    <div class="form-group col-md-3">
                                                                <asp:label ID="Label132" runat="server" class="col-form-label">RW KTP</asp:label>
																<asp:TextBox ID="txtrwktp" Enabled="false" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"   runat="server" ></asp:TextBox>
												             </div>
                                                                  <div class="form-group col-md-6">
                                                                <asp:label ID="Label128" runat="server" class="col-form-label">Kabupaten</asp:label>
																<asp:TextBox ID="txtkabupatenktp" Enabled="false" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
														                  
                                                            </div>
                                                            </div>
                                                            <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label151" runat="server" class="col-form-label">Kelurahan KTP</asp:label>
																<asp:TextBox ID="txtkelurahanktp" Enabled="false" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
															                                            
                                                            </div> 

															<div class="form-group col-md-6">
                                                                <asp:label ID="Label158" runat="server" class="col-form-label">Kecamatan KTP</asp:label>
																<asp:TextBox ID="txtkecamatanktp" Enabled="false" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
                                                                 
															</div>
                                                            </div>
                                                            <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label159" runat="server" class="col-form-label">Provinsi KTP</asp:label>
																<asp:DropDownList ID="ddlpropinsiktp" Enabled="false" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                            </div>
                                                            
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label160" runat="server" class="col-form-label">Kode Pos KTP</asp:label>
																<asp:TextBox ID="txtkodeposktp" Enabled="false"  CssClass="form-control" Text="0" MaxLength="6"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
											                </div>

                                                            </div>
                                           
                                                            
                                                            
                                                            
                                                            
                                                            <div class="form-row">
                                                             <div class="form-group col-md-6">
                                                                <asp:label ID="Label13" runat="server" class="col-form-label">Agama</asp:label>
																<asp:DropDownList ID="ddlAgamaktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                           
                                                            </div>  

                                                             <div class="form-group col-md-6">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">Status Pernikahan</asp:label>
																<asp:DropDownList ID="ddlstatusnikahktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>

                                                             </div>
                                                               
                                                            </div>
                                                              

                                                            <div class="form-row">
                                                                <div class="form-group col-md-3">
                                                                    <asp:label ID="Label45" runat="server" class="col-form-label">Pekerjaan</asp:label>
																    <asp:DropDownList ID="ddlpekerjaanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                </div>

                                                             <div class="form-group col-md-3">
                                                                <asp:label ID="Label6" runat="server" class="col-form-label">Sektor Usaha</asp:label>
																<asp:DropDownList ID="ddlSektorUsahaKtp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>

                                                             </div>
                                                                  <div class="form-group col-md-6">
                                                                        <asp:label ID="Label11" runat="server" class="col-form-label">Jenis Usaha</asp:label>
																        <asp:TextBox ID="txtjenisusahaKtp" Enabled="false" CssClass="form-control" Text="" MaxLength="50"  runat="server"  ></asp:TextBox>
                                                                </div>
                                                            </div>


                                                             <div class="form-row">
                                                             <div class="form-group col-md-3">
                                                                <asp:label ID="Label22" runat="server" class="col-form-label">Pendidikan</asp:label>
																<asp:DropDownList ID="ddlpendidikanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                            </div>   

                                                             <div class="form-group col-md-6">
                                                                <asp:label ID="Label14xxx" runat="server" class="col-form-label">Kewarganegaraan</asp:label>
                                                                <div class="form-row">
                                                                    <div class="form-group col-md-6">
                                                                        <asp:DropDownList ID="ddlkewarganegaraanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkewarganegaraan_SelectedIndexChanged" ></asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-md-6">
                                                                        <asp:TextBox ID="txtkewarganegaraanktp" Enabled="false" MaxLength="30" CssClass="form-control"  runat="server" ></asp:TextBox>
                                                           
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
             <asp:Panel ID="pnlIdentitas" Visible="false" runat="server"> 
            <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA TEMPAT TINGGAL & DATA PASANGAN</div>
                                                       <p class="m-b-0"></p>
                                 
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Tempat Tinggal</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                      <div class="form-group">
                                                                <asp:label ID="Label104" runat="server" class="col-form-label">Status Rumah</asp:label>
																<asp:DropDownList ID="ddlstatusrumah" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusrumah_SelectedIndexChanged"></asp:DropDownList>
                                                                 <%--<asp:DropDownList ID="DropDownList1" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusrumah_SelectedIndexChanged"  ></asp:DropDownList>--%>
                                                             </div>
                                                             <div class="form-group">
                                                                <asp:label ID="Label105" runat="server" class="col-form-label">Alamat Rumah Tinggal</asp:label>
																<asp:TextBox ID="txtalamatrumahtinggal1" CssClass="form-control" Text="-" MaxLength="100" TextMode="MultiLine"  runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator18" runat="server" SetFocusOnError="True" ControlToValidate="txtalamatrumahtinggal1"  ErrorMessage="Alamat Rumah Tinggal (1) tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>
															
														
                                                          <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label106" runat="server" class="col-form-label">RT</asp:label>
																<asp:TextBox ID="txtrttp" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);" MaxLength="3"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator21" runat="server" SetFocusOnError="True" ControlToValidate="txtrttp"  ErrorMessage="RT tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label113" runat="server" class="col-form-label">RW</asp:label>
																<asp:TextBox ID="txtrwtpt" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"   runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator22" runat="server" SetFocusOnError="True" ControlToValidate="txtrwtpt"  ErrorMessage="RW tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                        </div>

                                                        <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label114" runat="server" class="col-form-label">Kelurahan Tempat Tinggal</asp:label>
																<asp:TextBox ID="txtkelurahantpt" Enabled="true" CssClass="form-control" Text="-" MaxLength="30"  runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator23" runat="server" SetFocusOnError="True" ControlToValidate="txtkelurahantpt"  ErrorMessage="Kelurahan tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
															</div> 
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label115" runat="server" class="col-form-label">Kecamatan Tempat Tinggal</asp:label>
																<asp:TextBox ID="txtkecamatantpt" Enabled="true" CssClass="form-control" Text="-" MaxLength="30"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator24" runat="server" SetFocusOnError="True" ControlToValidate="txtkecamatantpt"  ErrorMessage="Kecamatan tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                         </div>
                                                        <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label116" runat="server" class="col-form-label">Provinsi Tempat Tinggal</asp:label>
																<asp:DropDownList ID="ddlprovinsitpt" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator25" ErrorMessage="Provinsi tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlprovinsitpt" Runat="server" />

															</div>

															<div class="form-group col-md-6">
                                                                <asp:label ID="Label117" runat="server" class="col-form-label">Kode Pos Tempat Tinggal</asp:label>
																<asp:TextBox ID="txtkodepostpt" CssClass="form-control" MaxLength="6" Text="0" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator26" runat="server" SetFocusOnError="True" ControlToValidate="txtkodepostpt"  ErrorMessage="Kodepos tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                        </div>


                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Pasangan</div>
                                                <p class="text-muted font-14 m-b-30"></p>

                                                   <div class="form-group">
                                                                <asp:label ID="Label10" runat="server" Enabled="false" class="col-form-label">Status dalam keluarga</asp:label>
																<asp:DropDownList ID="ddlstatuskawinupload" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatuskawin_SelectedIndexChanged" ></asp:DropDownList>
                                                            </div>
                                                                <div class="form-group">
                                                                <asp:label ID="Label107" runat="server" class="col-form-label">No.Identitas Pasangan</asp:label>
																<asp:TextBox ID="txtidpasangan" Enabled="false" CssClass="form-control"   MaxLength="16"  onkeypress="return isnumerickey(event);"    runat="server" AutoPostBack="true"  OnTextChanged="txtidpasangan_TextChanged"></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator30" runat="server" SetFocusOnError="True" ControlToValidate="txtidpasangan"  ErrorMessage="No. KTP Pasangan tidak boleh kosong (isi '0' jika tidak belum menikah)"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>
                                                             	<div class="form-group">
                                                                <asp:label ID="Label108" runat="server" class="col-form-label">Nama Pasangan</asp:label>
																<asp:TextBox ID="txtnamapasangan" Enabled="false" CssClass="form-control"  MaxLength="70"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator31" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapasangan"  ErrorMessage="Nama Pasangan tidak boleh kosong (isi '-' jika tidak belum menikah)"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                            
															<div class="form-group">
                                                                <asp:label ID="Label109" runat="server" class="col-form-label">Tempat Lahir Pasangan</asp:label>
																<asp:TextBox ID="txttempatlahirpasangan" Enabled="false" CssClass="form-control"  MaxLength="30"  runat="server" ></asp:TextBox>
															</div>

                                                             <div class="form-group">
                                                                 <asp:label id="Label110" runat="server"  class="col-form-label">Tanggal Lahir Pasangan</asp:label>
                                                                 <asp:TextBox ID="txttgllahirpasangan" Enabled="false"  CssClass="form-control" runat="server"  TextMode="DateTime" ></asp:TextBox>
                                                            </div>  
                                                        

                                                       

                                            </div>
                                        </div>
                                    </div>


                                </div>

                                    
                                     
                                                   </div>  

                                            </div>
                                         </div>        

                    </asp:Panel>
        <asp:Panel ID="BackNextAnggota" Visible="false" runat="server"> 
                  <div class="row">
                      <div class="col-lg-12">
                          <div class="card-box ribbon-box">
                              <p class="m-b-0"></p>
                              <div class="form-row">
                                  <div class="form-group col-md-12 text-right">
                                      <div class="form-group">
                                           <asp:LinkButton  ID="btnNextAhliWaris" type="button" align="right" runat="server" class="btn btn-success waves-light text-white" OnClientClick="$('#AhliWaris-tab').tab('show'); return false;">
                                          Next Ahli Waris <i class="fa-solid fa-right-long"></i>
                                          </asp:LinkButton> 

                                      </div>
                                  </div>
                              </div>
                          </div>

                      </div>
                  </div>
        </asp:Panel>
</div>
<div id="AhliWaris" class="tab-pane fade">
                 <asp:Panel ID="Pnlpenerima" Visible="false"  runat="server">
                      
            <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA PENERIMA PENSIUN & AHLI WARIS</div>
                                                       <p class="m-b-0"></p>
                                 
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Penerima Pensiun</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                     <div class="form-group">
                                                                <asp:label ID="Label169" runat="server" class="col-form-label">Penerima Pensiun</asp:label>
																<asp:DropDownList ID="ddlpenerimapensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlpenerimapensiun_SelectedIndexChanged"  >
                                                                     <asp:ListItem Value="0">Pensiunan</asp:ListItem>
                                                                        <asp:ListItem Value="1">Bukan Pensiunan</asp:ListItem>

																</asp:DropDownList>
                                                                

                                                            </div>
                                                          <div class="form-row">
                                                                 <div class="form-group col-md-6">
                                                                <asp:label ID="Label170" runat="server" class="col-form-label">Nomor Identitas KTP (NIK)</asp:label>
																<asp:TextBox ID="txtidktppp" CssClass="form-control"  MaxLength="16" onkeypress="return isnumerickey(event);"  runat="server"  ></asp:TextBox>
                                                                              
                                                            </div> 
                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label171" runat="server" class="col-form-label">Nama Lengkap</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamapp" MaxLength="70"   runat="server" Text="" ></asp:TextBox>
                                                            </div>
                                                        </div>

                                                           


                                                     <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label172" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:DropDownList ID="ddljeniskelaminpp" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                          </div>
															
															<div class="form-group col-md-4">
                                                                <asp:label ID="Label173" runat="server" class="col-form-label">Tempat Lahir</asp:label>
																<asp:TextBox ID="txttempatlahirpp" CssClass="form-control" Text="" MaxLength="30"  runat="server"  ></asp:TextBox>
															</div>
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label174" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
                                                                  <asp:TextBox CssClass="form-control" ID="txttgllahirpp"  TextMode="DateTime" runat="server"  ></asp:TextBox>
                                                                                  
                                                            </div> 
                                                            </div>
                                                         


                                                             <div class="form-row">
                                                                 <div class="form-group col-md-4">
                                                                <asp:label ID="Label183" runat="server" class="col-form-label">Agama</asp:label>
																<asp:DropDownList ID="ddlagamapp" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                           
                                                            </div>
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label184" runat="server" class="col-form-label">No.HP</asp:label>
																<asp:TextBox ID="txtnohppp" CssClass="form-control" MaxLength="13" Text="0"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                               
                                                            
                                                            </div>

                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label185" runat="server" class="col-form-label">Email</asp:label>
																<asp:TextBox ID="txtemailpp" CssClass="form-control" MaxLength="40" Text="0"   runat="server" ></asp:TextBox>
                                                                             
                                                            </div>


                                                               </div>                                                         


                                                   
                                                            
                                                                            <div class="form-group">
                                                                <asp:label ID="Label175" runat="server" class="col-form-label">Alamat Rumah Sesuai KTP</asp:label>
																<asp:TextBox ID="txtalamatpp"   CssClass="form-control" Text="" MaxLength="100" TextMode="MultiLine"  runat="server" ></asp:TextBox>
                                                               
                                                            
                                                            </div>

                                                            <div class="form-row">
                                                               
                                                            
                                                            
															    <div class="form-group col-md-2">
                                                                <asp:label ID="Label176" runat="server" class="col-form-label">RT KTP</asp:label>
																<asp:TextBox ID="txtrtpp" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"    runat="server" ></asp:TextBox>
											                             
                                                            </div>

															    <div class="form-group col-md-2">
                                                                <asp:label ID="Label177" runat="server" class="col-form-label">RW KTP</asp:label>
																<asp:TextBox ID="txtrwpp" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"   runat="server" ></asp:TextBox>
												             </div>
                                                                  <div class="form-group col-md-4">
                                                                <asp:label ID="Label178" runat="server" class="col-form-label">Kabupaten</asp:label>
																<asp:TextBox ID="txtkabupatenpp" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
														                  
                                                            </div>
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label179" runat="server" class="col-form-label">Kelurahan KTP</asp:label>
																<asp:TextBox ID="txtkelurahanpp" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
															                                            
                                                            </div> 
                                                               
                                                            </div>
                                                            
                                                            <div class="form-row">
                                                                 <div class="form-group col-md-4">
                                                                <asp:label ID="Label180" runat="server" class="col-form-label">Kecamatan KTP</asp:label>
																<asp:TextBox ID="txtkecamatanpp" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
                                                                 
															</div>
															<div class="form-group col-md-4">
                                                                <asp:label ID="Label181" runat="server" class="col-form-label">Provinsi KTP</asp:label>
																<asp:DropDownList ID="ddlprovinsipp" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                            </div>
                                                            
															<div class="form-group col-md-4">
                                                                <asp:label ID="Label182" runat="server" class="col-form-label">Kode Pos KTP</asp:label>
																<asp:TextBox ID="txtkodepospp" CssClass="form-control" Text="0" MaxLength="6"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
											                </div>

                                                            </div>
                                           
                                                            
                                                            
                                                            
                                                            
                                                            
                                                              

                                                            


                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Ahli Waris</div>
                                                <p class="text-muted font-14 m-b-30"></p>



                                                            <div class="form-group">
                                                                <asp:label ID="Label186" runat="server" class="col-form-label">Nama Ahli Waris</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamaaw" MaxLength="70"   runat="server" Text="" ></asp:TextBox>
                                                            </div>
                                                            <div class="form-row">
                                                               
                                                            
                                                            
															    <div class="form-group col-md-6">
                                                           
                                                                <asp:label ID="Label187" runat="server" class="col-form-label">Nama Ibu Kandung</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamaibukandungaw" MaxLength="70"   runat="server" Text="" ></asp:TextBox>
                                                            </div>
                                                           <div class="form-group col-md-6">
                                                                <asp:label ID="Label188" runat="server" class="col-form-label">Hubungan Ahli Waris</asp:label>
																<asp:TextBox CssClass="form-control" ID="txthubunganaw" MaxLength="70"   runat="server" Text="" ></asp:TextBox>
                                                            </div>
                                                            </div>

                                               

                                                      <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label197" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:DropDownList ID="ddljeniskelaminaw" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                          </div>
															
															<div class="form-group col-md-4">
                                                                <asp:label ID="Label200" runat="server" class="col-form-label">Tempat Lahir</asp:label>
																<asp:TextBox ID="txttempatlahiraw" CssClass="form-control" Text="" MaxLength="30"  runat="server"  ></asp:TextBox>
															</div>
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label201" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
                                                                  <asp:TextBox CssClass="form-control" ID="txttgllahiraw"  TextMode="DateTime" runat="server"  ></asp:TextBox>
                                                                                  
                                                            </div> 
                                                            </div>
                                                            <div class="form-row">
                                                               
                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label198" runat="server" class="col-form-label">No.HP</asp:label>
																<asp:TextBox ID="txtnohpaw" CssClass="form-control" MaxLength="13" Text="0"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                               
                                                            
                                                            </div>

                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label199" runat="server" class="col-form-label">Email</asp:label>
																<asp:TextBox ID="txtemailaw" CssClass="form-control" MaxLength="40" Text="0"   runat="server" ></asp:TextBox>
                                                                             
                                                            </div>


                                                               </div>

                                               
                                                        
                                                    
                                                                            <div class="form-group">
                                                                <asp:label ID="Label189" runat="server" class="col-form-label">Alamat Rumah Sesuai KTP</asp:label>
																<asp:TextBox ID="txtalamataw"   CssClass="form-control" Text="" MaxLength="100" TextMode="MultiLine"  runat="server" ></asp:TextBox>
                                                               
                                                            
                                                            </div>

                                                            <div class="form-row">
                                                               
                                                            
                                                            
															    <div class="form-group col-md-2">
                                                                <asp:label ID="Label190" runat="server" class="col-form-label">RT KTP</asp:label>
																<asp:TextBox ID="txtrtaw" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"    runat="server" ></asp:TextBox>
											                             
                                                            </div>

															    <div class="form-group col-md-2">
                                                                <asp:label ID="Label191" runat="server" class="col-form-label">RW KTP</asp:label>
																<asp:TextBox ID="txtrwaw" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"   runat="server" ></asp:TextBox>
												             </div>
                                                                  <div class="form-group col-md-4">
                                                                <asp:label ID="Label192" runat="server" class="col-form-label">Kabupaten</asp:label>
																<asp:TextBox ID="txtkabupatenaw" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
														                  
                                                            </div>
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label193" runat="server" class="col-form-label">Kelurahan KTP</asp:label>
																<asp:TextBox ID="txtkelurahanaw" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
															                                            
                                                            </div> 
                                                               
                                                            </div>
                                                            
                                                            <div class="form-row">
                                                                 <div class="form-group col-md-4">
                                                                <asp:label ID="Label194" runat="server" class="col-form-label">Kecamatan KTP</asp:label>
																<asp:TextBox ID="txtkecamatanaw" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
                                                                 
															</div>
															<div class="form-group col-md-4">
                                                                <asp:label ID="Label195" runat="server" class="col-form-label">Provinsi KTP</asp:label>
																<asp:DropDownList ID="ddlprovinsiaw" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                            </div>
                                                            
															<div class="form-group col-md-4">
                                                                <asp:label ID="Label196" runat="server" class="col-form-label">Kode Pos KTP</asp:label>
																<asp:TextBox ID="txtkodeposaw" CssClass="form-control" Text="0" MaxLength="6"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
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
                     <asp:Panel ID="PNLKELUARGA" Visible="false"  runat="server">
                      
                                     <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA KELUARGA PENSIUNAN</div>
                                                       <p class="m-b-0"></p>
                                 
                                                              

                                                                
                                    
                                                                      <div class ="form-row">
                                                                            <div class="form-group col-md-1">
                                                                        <asp:label ID="Label14" runat="server" class="col-form-label">No</asp:label>
                                                                        <asp:TextBox CssClass="form-control" ID="TextBox1" Enabled="false" text="1" runat="server"  ></asp:TextBox> 
                                                           
                                                                        </div>
                                                                <div class="form-group col-md-4">
                                                                        <asp:label ID="Label12" runat="server" class="col-form-label">Nama</asp:label>
                                                                        <asp:TextBox CssClass="form-control" ID="txtnamaanak1" text="-" MaxLength="80"  runat="server"  ></asp:TextBox> 
                                                           
                                                               </div>
                                                                <div class="form-group col-md-2">
                                                                        <asp:label ID="Label15" runat="server" class="col-form-label">Tanggal Lahir </asp:label>
                                                                         <asp:TextBox ID="txttgllahiranak1" CssClass="form-control" TextMode="DateTime" runat="server" AutoPostBack="true" OnTextChanged="txtusia_anak1_OnChange"  ></asp:TextBox>
                                                           
                                                               </div>

                                                                  <div class="form-group col-md-1">
                                                                        <asp:label ID="Label16" runat="server" class="col-form-label">Jenis Kelamin </asp:label>
                                                                        <asp:DropDownList ID="ddljeniskelaminanak1" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                           
                                                               </div>

                                                                 <div class="form-group col-md-1">
                                                                        <asp:label ID="Label17" runat="server" class="col-form-label">Status Pernikahan</asp:label>
                                                                       
                                                                            <asp:DropDownList ID="ddlstatusnikahanak1"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                            <asp:ListItem Value="-">Pilih Status</asp:ListItem>
                                                                            <asp:ListItem Value="0">Tidak Kawin</asp:ListItem>
                                                                            <asp:ListItem Value="1">Kawin</asp:ListItem>
                                                                            </asp:DropDownList> 
                                                           
                                                               </div>
                                                                     <div class="form-group col-md-1">
                                                                        <asp:label ID="Label18" runat="server" class="col-form-label">Usia</asp:label>
                                                                        <asp:TextBox CssClass="form-control" ID="txtusia1" Enabled="false" text="0"  runat="server" AutoPostBack ="true" OnTextChanged="Data_Formula"  ></asp:TextBox> 
                                                           
                                                               </div>

                                                            </div>
                                                        
                                                       <div class ="form-row">
                                                                            <div class="form-group col-md-1">
                                                                        <asp:label ID="Label19" runat="server" class="col-form-label">No</asp:label>
                                                                        <asp:TextBox CssClass="form-control" ID="TextBox2" Enabled="false" text="2" runat="server"  ></asp:TextBox> 
                                                           
                                                                        </div>
                                                                <div class="form-group col-md-4">
                                                                        <asp:label ID="Label20" runat="server" class="col-form-label">Nama</asp:label>
                                                                        <asp:TextBox CssClass="form-control" ID="txtnamaanak2" text="-" MaxLength="80"  runat="server"  ></asp:TextBox> 
                                                           
                                                               </div>
                                                                <div class="form-group col-md-2">
                                                                        <asp:label ID="Label21" runat="server" class="col-form-label">Tanggal Lahir </asp:label>
                                                                         <asp:TextBox ID="txttgllahiranak2" CssClass="form-control" TextMode="DateTime" runat="server" AutoPostBack="true" OnTextChanged="txtusia_anak2_OnChange"  ></asp:TextBox>
                                                           
                                                               </div>

                                                                  <div class="form-group col-md-1">
                                                                        <asp:label ID="Label23" runat="server" class="col-form-label">Jenis Kelamin </asp:label>
                                                                        <asp:DropDownList ID="ddljeniskelaminanak2" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                           
                                                               </div>

                                                                 <div class="form-group col-md-1">
                                                                        <asp:label ID="Label27" runat="server" class="col-form-label">Status Pernikahan</asp:label>
                                                                       
                                                                            <asp:DropDownList ID="ddlstatusnikahanak2"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                            <asp:ListItem Value="-">Pilih Status</asp:ListItem>
                                                                            <asp:ListItem Value="0">Tidak Kawin</asp:ListItem>
                                                                            <asp:ListItem Value="1">Kawin</asp:ListItem>
                                                                            </asp:DropDownList> 
                                                           
                                                               </div>
                                                                     <div class="form-group col-md-1">
                                                                        <asp:label ID="Label28" runat="server" class="col-form-label">Usia</asp:label>
                                                                        <asp:TextBox CssClass="form-control" ID="txtusia2" Enabled="false" text="0"  runat="server"  ></asp:TextBox> 
                                                           
                                                               </div>

                                                            </div>

                     <div class ="form-row">
                                                                            <div class="form-group col-md-1">
                                                                        <asp:label ID="Label29" runat="server" class="col-form-label">No</asp:label>
                                                                        <asp:TextBox CssClass="form-control" ID="TextBox7" Enabled="false" text="3" runat="server"  ></asp:TextBox> 
                                                           
                                                                        </div>
                                                                <div class="form-group col-md-4">
                                                                        <asp:label ID="Label30" runat="server" class="col-form-label">Nama</asp:label>
                                                                        <asp:TextBox CssClass="form-control" ID="txtnamaanak3" text="-" MaxLength="80"  runat="server"  ></asp:TextBox> 
                                                           
                                                               </div>
                                                                <div class="form-group col-md-2">
                                                                        <asp:label ID="Label32" runat="server" class="col-form-label">Tanggal Lahir </asp:label>
                                                                         <asp:TextBox ID="txttgllahiranak3" CssClass="form-control" TextMode="DateTime" runat="server" AutoPostBack="true" OnTextChanged="txtusia_anak3_OnChange"  ></asp:TextBox>
                                                           
                                                               </div>

                                                                  <div class="form-group col-md-1">
                                                                        <asp:label ID="Label34" runat="server" class="col-form-label">Jenis Kelamin </asp:label>
                                                                        <asp:DropDownList ID="ddljeniskelaminanak3" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                           
                                                               </div>

                                                                 <div class="form-group col-md-1">
                                                                        <asp:label ID="Label35" runat="server" class="col-form-label">Status Pernikahan</asp:label>
                                                                       
                                                                            <asp:DropDownList ID="ddlstatusnikahanak3"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                            <asp:ListItem Value="-">Pilih Status</asp:ListItem>
                                                                            <asp:ListItem Value="0">Tidak Kawin</asp:ListItem>
                                                                            <asp:ListItem Value="1">Kawin</asp:ListItem>
                                                                            </asp:DropDownList> 
                                                           
                                                               </div>
                                                                     <div class="form-group col-md-1">
                                                                        <asp:label ID="Label36" runat="server" class="col-form-label">Usia</asp:label>
                                                                        <asp:TextBox CssClass="form-control" ID="txtusia3" Enabled="false" text="0"  runat="server"  ></asp:TextBox> 
                                                           
                                                               </div>

                                                            </div>

                                    
                                     
                                                   </div>  

                                            </div>
                                         </div>        
                             
                    
                    </asp:Panel>

                    <asp:Panel ID="BackNextAhliWaris" Visible="false" runat="server"> 
                          <div class="row">
                              <div class="col-lg-12">
                                  <div class="card-box ribbon-box">
                                      <p class="m-b-0"></p>
                                      <div class="form-row">
                                          <div class="form-group col-md-6 text-left">
                                              <div class="form-group">
                                                   <asp:LinkButton  ID="btnBackAnggota" type="button" align="left" runat="server" class="btn btn-success waves-light text-white" UseSubmitBehavior="false" OnClientClick="return false">
                                                  <i class="fa-solid fa-left-long"></i> Back Anggota
                                                  </asp:LinkButton> 
                                              </div>
                                          </div>
                                          <div class="form-group col-md-6 text-right">
                                              <div class="form-group">
                                                   <asp:LinkButton  ID="btnNextSimulasi" type="button" align="right" runat="server" class="btn btn-success waves-light text-white" UseSubmitBehavior="false" OnClientClick="return false">
                                                  Next Simulasi <i class="fa-solid fa-right-long"></i>
                                                  </asp:LinkButton> 

                                              </div>
                                          </div>
                                      </div>
                                  </div>

                              </div>
                          </div>
                </asp:Panel>
    </div>
<div id="Simulasi" class="tab-pane fade">
                 <asp:Panel ID="Pnlsimulasi" Visible="false"  runat="server">
                      
            <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA SIMULASI</div>
                                                       <p class="m-b-0"></p>
                                 
                                                                 <div class="row">

                                    

                                                        <div class="form-group col-md-6">
                                                                        <asp:label ID="Label8" runat="server" class="col-form-label">Data Simulasi</asp:label>
                                                                        <asp:DropDownList ID="ddldatasimulasi" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddldatasimulasi_SelectedIndexChanged" >
                                                                        <asp:ListItem Value="0">Tanpa Data Simulasi</asp:ListItem>
                                                                        <asp:ListItem Value="A">Data Simulasi Universal</asp:ListItem>              
                                                                        <asp:ListItem Value="B">Data Simulasi Capital</asp:ListItem>        
                                                                        </asp:DropDownList> 
                                                             <asp:TextBox CssClass="form-control"  Visible="false" ID="txtidpermohonan" text="0" MaxLength="50"  runat="server" ></asp:TextBox> 
                                                               </div>
                                                        <div class="form-group col-md-6" runat="server" id="simulasi">
                                                                      <div class="form-group">
                                                                     <asp:label ID="Label9" runat="server" class="col-form-label">No simulasi</asp:label>
                                                                       
                                                                         <%--<asp:TextBox CssClass="form-control" ID="Txtnosimulasi" Enabled="true" MaxLength = "250" runat="server" ></asp:TextBox>--%>
                                                                       
                                                                      
                                                                       <asp:DropDownList ID="ddllistsimulasi" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" >
                                                                          
                                                                        </asp:DropDownList>
                                                                      
                                                                      </div>  

                                                                    <div class="button-list">
                                        
                                                                        <asp:Button ID="Button2" class="btn btn-success waves-light text-white" runat="server" Text="Search"  OnClick="ddldatasimulasipilih_SelectedIndexChanged"/> 
                                                                       
                                                                    </div>
                                                                             </div>


                                </div>

                                    
                                     
                                                   </div>  

                                            </div>
                                         </div>        
                             
                    
                    </asp:Panel>
 
                 <asp:Panel ID="PnlSKPensiun" Visible="false"  runat="server">
                      
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

                                                                    <asp:DropDownList ID="ddlinstansi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlinstansi_selected_index"></asp:DropDownList>
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

                                                                        <asp:TextBox ID="txttgllahir" CssClass="form-control" TextMode="DateTime" runat="server" AutoPostBack="true" OnTextChanged="txtusia_OnChange" ></asp:TextBox>
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
                                                                    <asp:DropDownList runat="server" CssClass="selectpicker show-tick" ID="ddlTunjanganIstriAnak" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlTunjanganIStriAnak_SelectedIndexChanged">
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

                 <asp:Panel ID="pnlpinjaman" visible="false" runat="server">
                      
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
                                                                  <asp:DropDownList ID="ddlKantorBayar" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkantor_SelectedIndexChanged" ></asp:DropDownList>
                                                                      <asp:TextBox ID="txtkodesimulasi" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                             
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Sumber Dana</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                   <asp:DropDownList ID="ddlsumberdana" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   AutoPostBack ="true" OnSelectedIndexChanged="Data_Formula"></asp:DropDownList>

                                                                </div>
                                                            </div>

                                                 <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">ProgramPinjaman</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                   <asp:DropDownList ID="ddlprogrampinjaman" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack ="true" OnSelectedIndexChanged="ddlprogrampinjaman_SelectedIndexChanged" ></asp:DropDownList>

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
                                                                    <asp:DropDownList ID="ddlPk" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" Enabled="false" AutoPostBack ="true" OnSelectedIndexChanged="Data_Formula">
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
                                                                      <asp:DropDownList ID="ddljangkawaktu" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddljangkawaktu_SelectedIndexChanged" ></asp:DropDownList>

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
                                                                                    <asp:DropDownList ID="ddlangsurandimukablokir" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlangsurandimukablokir_SelectedIndexChanged" > </asp:DropDownList>

                                                                </div>
                                                            </div>                                                       
                                                  
                                                    
                                                         <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Angsuran Dimuka Angsuran</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlangsurandimuka" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlangsurandimuka_SelectedIndexChanged" > </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                 <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Angsuran Dimuka Asuransi</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlangsurandimukaasuransi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlangsurandimukaasuransi_SelectedIndexChanged" > </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                   <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Materai</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlmaterai" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlmaterai_SelectedIndexChanged" > </asp:DropDownList>

                                                                </div>
                                                            </div>

                                                <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Provisi</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlprovisi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlprovisi_SelectedIndexChanged" > </asp:DropDownList>

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
                                                                <div class="col-md-4">

<%--                                                                    <a href="#" onclick="openModal(); return false;" class="btn btn-custom">Open Form B</a>--%>
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
                                                                <div class="col-md-4">
                                                                    <asp:Button ID="ShowPK2" Enabled ="false" class="btn btn-success waves-light text-white" runat="server" type="button" Text="Show Pinjaman Ke 2" OnClick="ShowPk2_Click" />
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
                     <asp:Panel ID="pnlpinjaman2" visible="false" runat="server">
                      
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
                                                                    <asp:TextBox ID="txttglrealisasiChild" TextMode="DateTime" Enabled="false" CssClass="form-control" MaxLength="100" runat="server" AutoPostBack ="true" OnTextChanged="Data_Formula_Child"></asp:TextBox>

                                                                </div>
                                                            </div>
                      
                                                                <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Kantor Bayar</p>
                                                                   
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                  <asp:DropDownList ID="ddlKantorBayarChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" ></asp:DropDownList>
                                                                      <asp:TextBox ID="txtkodesimulasiChild" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                             
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Sumber Dana</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                   <asp:DropDownList ID="ddlsumberdanaChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   AutoPostBack ="true" ></asp:DropDownList>

                                                                </div>
                                                            </div>

                                                 <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">ProgramPinjaman</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                   <asp:DropDownList ID="ddlprogrampinjamanChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack ="true"  ></asp:DropDownList>

                                                                </div>
                                                            </div>

                                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Produk</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlprodukChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack ="true"></asp:DropDownList>

                                                                </div>
                                                            </div>

                                                          <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Kategori</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlkategoriChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack ="true" ></asp:DropDownList>

                                                                </div>
                                                            </div>
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">PK</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlPkChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" Enabled="false">
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
                                                                      <asp:DropDownList ID="ddljangkawaktuChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true"></asp:DropDownList>

                                                                </div>
                                                            </div>
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Jangka Waktu Tahun</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                  <asp:TextBox ID="txtjangkatahunChild" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                       <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Tgl Jatuh Tempo</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                <asp:TextBox ID="txttgljatuhtempoChild" Enabled="false" CssClass="form-control" TextMode="DateTime" MaxLength="100" runat="server"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                   
                                                              <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Plafon</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                              <asp:TextBox ID="txtplafonChild" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack ="true" OnTextChanged="Data_Formula_Child" ></asp:TextBox> 

                                                                </div>
                                                            </div>
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Pelunasan/Take Over</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                              <asp:TextBox ID="txtpelunasanChild" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack ="true" OnTextChanged="Data_Formula_Child" ></asp:TextBox> 

                                                                </div>
                                                            </div>

                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Angsuran Blokir</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlangsurandimukablokirChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true"> </asp:DropDownList>

                                                                </div>
                                                            </div>                                                       
                                                  
                                                    
                                                         <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Angsuran Dimuka Angsuran</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlangsurandimukaChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" > </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                 <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Angsuran Dimuka Asuransi</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlangsurandimukaasuransiChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" > </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                   <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Materai</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlmateraiChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true"> </asp:DropDownList>

                                                                </div>
                                                            </div>

                                                <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Provisi</p>
                                                                    
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="ddlprovisiChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" > </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                                                                         
                                                           

                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Mutasi Kantor Bayar</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                       <asp:DropDownList ID="ddlmutasikantorbayarChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >
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
                                                                      <asp:DropDownList ID="ddlAOChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                     <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Staff Non Account Office (AO)</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                      <asp:DropDownList ID="ddlnonaoChild" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
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
                                                                         <asp:label ID="Label26" BackColor="#cccccc" runat="server" class="form-control">Nama</asp:label>
                                                                       <asp:TextBox ID="txtnamaChild" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-8">
                                                                         <asp:label ID="Label37" BackColor="#cccccc" runat="server" class="form-control">Status Pinjaman</asp:label>
                                                                       <asp:TextBox ID="txtstatuspinjamanChild" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                         <div class ="form-row">
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label52" BackColor="#cccccc" runat="server" class="form-control">Usia Pinjaman</asp:label>
                                                                    <asp:TextBox ID="txtusiapinjamanChild" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                            <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label53" BackColor="#cccccc" runat="server" class="form-control">Usia Asuransi</asp:label>
                                                                       <asp:TextBox ID="txtusiaasuransiChild" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>  
                                                               <div class="form-group col-md-3">
                                                                         <asp:label ID="Label55" BackColor="#cccccc" runat="server" class="form-control">Usia Anak 1</asp:label>
                                                                       <asp:TextBox ID="txtusiaanak1Child" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label56" BackColor="#cccccc" runat="server" class="form-control">Usia Anak 2</asp:label>
                                                                          <asp:TextBox ID="txtusiaanak2Child" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                </div>


                                                  <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label57" BackColor="#cccccc" runat="server" class="form-control">Plafond Max</asp:label>
                                                                       <asp:TextBox ID="txtplafonmaxChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label58" BackColor="#cccccc" runat="server" class="form-control">Jangka Waktu Max (Bulan)</asp:label>
                                                                        <asp:TextBox ID="txtjangkawaktumaxChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"   onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                 <div class="form-group col-md-4">
                                                                         <asp:label ID="Label59" BackColor="#cccccc" runat="server" class="form-control">Usia Jth Tempo</asp:label>
                                                                       <asp:TextBox ID="txtusiajthtempoChild" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>  
                                                                </div>
                                            
                                                            <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label60"  BackColor="#cccccc" runat="server" class="form-control">Rate Asuransi %</asp:label>
                                                                       <asp:TextBox ID="txtrateasuransiChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label61" BackColor="#cccccc" runat="server" class="form-control">Rate Bunga Flat %</asp:label>
                                                                        <asp:TextBox ID="txtratebungapinjamanChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                  <div class="form-group col-md-4">
                                                                         <asp:label ID="Label62" BackColor="#cccccc" runat="server" class="form-control">Rate Bunga Efektif %</asp:label>
                                                                        <asp:TextBox ID="txtratebungaefektifChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                </div>

                                                  

                                                    <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Angsuran Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtangsuranChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtpsnangsuranChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txtasuransiChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtpersenasuransiChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txttotalChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txttotalpersenChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txtsisagajiChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-4">
<%--                                                                        <asp:Button runat="server" ID="btnSimulasiChild" OnClick="ModalChild" Text="Show Child"/>--%>
<%--                                                                    <a href="#" onclick="openModal(); return false;" class="btn btn-custom">Open Form B</a>--%>
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtsisagajipersenChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txtprovisiChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtpersenprovisiChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txtadminbankChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2" style="display:none">
                                                                      <asp:TextBox ID="txtpersenadminbankChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txtkeanggotaanChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>
                                                 <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Materai Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtmateraiChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtmateraipcsChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txtangsuranblokirChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtangsuranblokirkaliChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txtangsurandimukaChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtangsurandimukakaliChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txtangdimukaasuransiChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtangdimukaasuransikaliChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                      <asp:TextBox ID="txtbiayaadminChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>                                                  
                                                <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Grace Periode Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtgraceperiodeChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>

                                                  <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Pelunasan/Take Over Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtpelunasanRpChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>

                                                 <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Terima Bersih Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtterimabersihrpChild" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                                                                        

                        <asp:Panel ID="BackNextSimulasi" Visible="false" runat="server"> 
                          <div class="row">
                              <div class="col-lg-12">
                                  <div class="card-box ribbon-box">
                                      <p class="m-b-0"></p>
                                      <div class="form-row">
                                          <div class="form-group col-md-6 text-left">
                                              <div class="form-group">
                                                   <asp:LinkButton  ID="btnBackAhliWaris" type="button" align="left" runat="server" class="btn btn-success waves-light text-white" UseSubmitBehavior="false" OnClientClick="return false">
                                                  <i class="fa-solid fa-left-long"></i> Back Ahli Waris
                                                  </asp:LinkButton> 
                                              </div>
                                          </div>
                                          <div class="form-group col-md-6 text-right">
                                              <div class="form-group">
                                                   <asp:LinkButton  ID="btnNextAsuransi" type="button" align="right" runat="server" class="btn btn-success waves-light text-white" UseSubmitBehavior="false" OnClientClick="return false">
                                                  Next Asuransi <i class="fa-solid fa-right-long"></i>
                                                  </asp:LinkButton> 

                                              </div>
                                          </div>
                                      </div>
                                  </div>

                              </div>
                          </div>
                </asp:Panel>

       </div>
 <div id="Asuransi" class="tab-pane fade">           
                       <asp:Panel ID="PnlRekeningBank" Visible="false"  runat="server">
                      
            <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI REKENING TRANSFER PENCAIRAN & REKENING KANTOR BAYAR</div>
                                                       <p class="m-b-0"></p>
                                 
                                                                 <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Rekening Transfer Pencairan</div>
                                                <p class="text-muted font-14 m-b-30"></p>


                                             
                                                     
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Nama Bank Rekening</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                        <asp:TextBox CssClass="form-control" ID="TxtNamaBankRekeningTransfer" text="-" MaxLength="50"  runat="server" ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">No Rekening Bank</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                      <asp:TextBox CssClass="form-control" ID="TxtNomorBankRekeningTransfer" text="-" MaxLength="50"  runat="server" ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Atas Nama Rekening Bank</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox CssClass="form-control" ID="TxtAtasNamaBankRekeningTransfer" text="-" MaxLength="50"  runat="server" ></asp:TextBox> 

                                                                     
                                                                </div>
                                                            </div>

                                                
                                                             

                                          

                                                       

                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Rekening Kantor Bayar</div>
                                                <p class="text-muted font-14 m-b-30"></p>


                                             
                                                     
                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Nama Bank Rekening</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                        <asp:TextBox CssClass="form-control" ID="txtnamaBankkantorbayar" text="-" MaxLength="50"  runat="server" ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                            <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">No Rekening Bank</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">

                                                                      <asp:TextBox CssClass="form-control" ID="txtnorekeningkantorbayar" text="-" MaxLength="50"  runat="server" ></asp:TextBox> 
                                                                </div>
                                                            </div>

                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Atas Nama Rekening Bank</p>
                                                                        <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox CssClass="form-control" ID="txtatasnamarekeningkantorbayar" text="-" MaxLength="50"  runat="server" ></asp:TextBox> 

                                                                     
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

                      <asp:Panel ID="pnlUpdAsuransi" Visible="false" runat="server">
                    <div class="clearfix">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA UPLOAD FOTO ASURANSI</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">

                                                          <div class="form-group">
                                                                <asp:label ID="Label7" runat="server" class="col-form-label">Foto 1</asp:label>
                                                                 
                                                            </div> 
                                                            <div>
                                                                <asp:FileUpload ID="fileupload1" runat="server"/> 
                                                                <asp:Button ID="btnfoto1" Text="Upload" runat="server" OnClick="Uploadfoto1"  OnClientClick="return CheckFileFoto1()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtfoto1" CssClass="form-control" readonly="true"   runat="server" MaxLength="400"></asp:TextBox>
                                                                    <asp:TextBox ID="txtfilename1" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>
                                                                   
                                                                <hr /> 

                                                          
                                                            </div>
                                                             <div>
                                                                  <asp:Image ID="imgfoto1" runat="server" Height="300px" Width="550px" />
                                                            </div>

                                                              
                                                         
                                                            <div class="form-group">
                                                                <asp:label ID="Label33" runat="server" class="col-form-label">Foto 2</asp:label>
                                                                
                                                            </div> 
                                                            <div>
                                                                <asp:FileUpload ID="fileupload2" runat="server"/> 
                                                                <asp:Button ID="btnfoto2" Text="Upload" runat="server" OnClick="Uploadfoto2" OnClientClick="return CheckFileFoto2()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtfoto2" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                    <asp:TextBox ID="txtfilename2" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>
                                                                   
                                                                <hr /> 

                                                          
                                                            </div>
                                                             <div>
                                                                  <asp:Image ID="imgfoto2" runat="server" Height="300px" Width="550px" />
                                                            </div>

                                                               <div class="form-group">
                                                                <asp:label ID="Label73" runat="server" class="col-form-label">Foto 3</asp:label>
                                                                
                                                            </div> 
                                                            <div>
                                                                <asp:FileUpload ID="fileupload3" runat="server"/> 
                                                                <asp:Button ID="btnfoto3" Text="Upload" runat="server" OnClick="Uploadfoto3"  OnClientClick="return CheckFileFoto3()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtfoto3" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                    <asp:TextBox ID="txtfilename3" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>
                                                                   
                                                                <hr /> 

                                                          
                                                            </div>
                                                             <div>
                                                                  <asp:Image ID="imgfoto3" runat="server" Height="300px" Width="550px" />
                                                            </div>

                                                         
														



                                                          </div>

                                                         <div class="form-group col-md-6">
        
                                                            <div class="form-group">
                                                                <asp:label ID="Label74" runat="server" class="col-form-label">Foto 4</asp:label>
                                                                
                                                            </div> 
                                                            <div>
                                                                <asp:FileUpload ID="fileupload4" runat="server"/> 
                                                                <asp:Button ID="btnfoto4" Text="Upload" runat="server" OnClick="Uploadfoto4"  OnClientClick="return CheckFileFoto4()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtfoto4" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                                                                    <asp:TextBox ID="txtfilename4" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>
                                                                   
                                                                <hr /> 

                                                          
                                                            </div>
                                                             <div>
                                                                  <asp:Image ID="imgfoto4" runat="server" Height="300px" Width="550px" />
                                                            </div>

                                                           
                                                            <div class="form-group">
                                                                <asp:label ID="Label75" runat="server" class="col-form-label">Foto 5</asp:label>
                                                                
                                                            </div> 
                                                            <div>
                                                                <asp:FileUpload ID="fileupload5" runat="server"/> 
                                                                <asp:Button ID="btnfoto5" Text="Upload" runat="server" OnClick="Uploadfoto5"  OnClientClick="return CheckFileFoto5()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtfoto5" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                                                                    <asp:TextBox ID="txtfilename5" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>
                                                                   
                                                                <hr /> 

                                                          
                                                            </div>
                                                             <div>
                                                                  <asp:Image ID="imgfoto5" runat="server" Height="300px" Width="550px" />
                                                            </div>

                                                        </div>  
                                                         <div class="form-group col-md-6">
														        <hr />
														 <asp:label ID="LabelPdf" runat="server" class="col-form-label">File PDF</asp:label>
                                                            <div>
                                                                <asp:FileUpload ID="fileupload6" runat="server"/> 
                                                                <asp:Button ID="btnPdf6" Text="Upload" runat="server" OnClick="Uploadfoto6"  OnClientClick="return CheckFileFoto6()"/>
                                                                <hr />
                                                                    <asp:TextBox ID="txtPdf6" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                                                                    <asp:TextBox ID="txtfilename6" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>                                                                                                                             
                                                            </div>
                                                         </div>



													</div>
												</div>  

                                            </div>
                                         </div>        
                                                                            
            </asp:Panel>       
             <asp:Panel ID="pnlAlasanTolak" Visible="false" runat="server">
                        <div class="clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <p class="m-b-0"></p>
                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label147" runat="server" class="col-form-label">Alasan Pembatalan</asp:Label>
                                                <asp:TextBox ID="txtalasantolak" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server">-</asp:TextBox>
                                            </div>



                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="btnBackTolak" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackTolak_Click" />


                            </div>
                            <div class="float-right mb-2 ">

                                <div class="button-list">

                                    <asp:Button ID="btnSimpanTolakan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="PROSES PEMBATALAN" OnClick="btnSimpanTolakan_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menolak data ini ?');" />

                                </div>
                            </div>
                        </div>

                        </span></span> 
    
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
                        <asp:Button ID="BtnApproveAntrian"  CssClass="btn btn-success waves-light text-white" type="button" runat="server" Text="Approve Antrian" OnClick="BtnApproveAntrian_Click"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Approve Antrian data ini ?');"/>
                        <asp:Button ID="BtnBatalkanmohon"  CssClass="btn btn-success waves-light text-white" type="button" runat="server" Text="Batalkan Permohonan" OnClick="btnTolak_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Batalkan Permohonan ini ?');"/>
                        <asp:Button ID="btnSimpan" CssClass="btn btn-success waves-light text-white"  type="button" runat="server" Text="SAVE" OnClick="btnSimpan_Click"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');"/>
                        <asp:Button ID="btnTolakAntrian"  class="btn btn-success waves-light text-white" type="button" runat="server" Text="BATALKAN ANTRIAN" OnClick="btnbatalkanantrian_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Batalkan Antrian data ini ?');"/>
                            </div>
                    </div>
                    </div> 
                  </div>
                </div>    
          </asp:Panel>

     </div>
</div>
            </div> 
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>  
                <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="GRData" />               
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="ddlstatusrumah" />
                <asp:PostBackTrigger ControlID="ddlstatuskawinupload" />
                 <asp:PostBackTrigger ControlID="btnfoto1" /> 
                <asp:PostBackTrigger ControlID="btnfoto2" />
                <asp:PostBackTrigger ControlID="btnfoto3" />
                <asp:PostBackTrigger ControlID="btnfoto4" />
                <asp:PostBackTrigger ControlID="btnfoto5" />
                <asp:PostBackTrigger ControlID="btnPdf6" />
               
     
                  
    
                 
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
