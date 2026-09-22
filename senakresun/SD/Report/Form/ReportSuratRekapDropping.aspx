<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeFile="ReportSuratRekapDropping.aspx.cs" Inherits="SD_REPORT_FORM_SuratRekapDropping" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Surat Rekap Dropping</title>
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
            <!-- Sweet Alert css -->
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


   
   
    <script type="text/javascript">
          function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
    
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(-1);
        };
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

        function AlertSuccess(h, t, p, i, fr) {
            console.log(fr)
            setTimeout(function () {


                Swal.fire({
                    title: h,
                    text: t,
                    icon: "success"
                }).then((result) => {
                    if (fr === 'true') {
                        window.location.href = "ReportSuratRekapDropping.aspx"
                    }
                });;
            }, 100);

        }
        function IsValid( args ) {
        if( args.value.length == 0 ) {
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
        function sweetAlerttolak(btnSimpanTolakan, errMsg) {
            if (btnSimpanTolakan.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSimpanTolakan.dataset.confirmed = false;
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
                        btnSimpanTolakan.dataset.confirmed = true;
                        btnSimpanTolakan.click();

                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
            }
        }

        function startDownloadCheck() {
            var checkTimer = setInterval(function () {
                // Cari cookie bernama 'DownloadComplete'
                if (document.cookie.indexOf("DownloadComplete=true") >= 0) {
                    clearInterval(checkTimer);

                    // Hapus cookie kembali agar bisa digunakan pada klik berikutnya
                    document.cookie = "DownloadComplete=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";

                    // Tampilkan SweetAlert Berhasil
                    swal({
                        title: "Download Done!",
                        text: "You successfully Download Zip",
                        type: "success",
                        showConfirmButton: true,
                        confirmButtonText: "OK"
                    }).then(function (result) {
                        // Dipanggil saat user klik "OK" atau menutup alert
                        window.location.href = "ReportSuratRekapDropping.aspx"; // Cara alternatif reload yang lebih agresif
                    }).catch(function () {
                        // Cadangan jika promise catch terpicu
                        window.location.reload();
                    });

                    // Cadangan ekstra: Jika user mendiamkan alert selama 3 detik, halaman tetap reload otomatis
                    setTimeout(function () {
                        window.location.href = "ReportSuratRekapDropping.aspx";
                    }, 3000);
                }
            }, 500); // Cek setiap 0.5 detik
        }

          function sweetAlertsave(btnSimpan,errMsg) {
            if (btnSimpan.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSimpan.dataset.confirmed = false;
                startDownloadCheck();
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

            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

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

            $('#txttgltransfer').datepicker({
                format: 'dd-MM-yyyy',
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

                var table = $('#Grdkartu').dataTable({
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
                                    <li class="breadcrumb-item"><a href="#">Kredit Pensiun</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="PencairanKreditNasabah.aspx">Surat Rekap Dropping</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Surat Rekap Dropping</h4>
                        </div>
                    </div>
                </div>
              
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">  <asp:TextBox ID="txtfocus" CssClass="form-control"  readonly="true" text="Surat Rekap Dropping" runat="server" ></asp:TextBox> </h4>   
                            
                                                <!-- Kode Sumber Dana -->
                                <div class="form-group row">
                                    <label for="ddlsumberdana" class="col-2 col-form-label" style="text-align:right">Sumber Dana</label>     
                                    <div class="col-4">
                                        <asp:DropDownList ID="ddlsumberdana"  CssClass="selectpicker show-tick" data-style="btn-custom" required="required" runat="server" OnSelectedIndexChanged="ddlsumberdana_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <!-- Tombol Process -->
                                <div class="form-group row">
                                    <div class="col-2"></div>
                                    <div class="col-9">
                                        <asp:Button ID="btnSearch" 
                                                    class="btn btn-success waves-light text-white float-left" 
                                                    runat="server" 
                                                    Text="PRINT" 
                                                    OnClick="btnSearch_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin print & download data ini ?');"/>
                                    </div>
                                </div>
                                 <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                      <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderStyle CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPilih" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Cabang">
                                            <ItemTemplate>
                                                   <asp:Label ID="lblnamacabang" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabang_nama")%>'></asp:Label>
                                                 <asp:Label ID="lblkodecabang" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodecabang")%>'></asp:Label>
                            
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamanasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>                                                                                                                       
                                        <asp:TemplateField HeaderText="No Pk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnopk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nopk")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Plafon Kredit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon", "{0:N0}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Biaya Adm">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladminrp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "adminrp", "{0:N0}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Biaya Provisi">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprovisirp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "provisirp", "{0:N0}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jangka Waktu">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Jenis Pinjaman">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljenispinjaman" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jenispinjaman")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="No Rekening Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnorekeningnasabah" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening_Nasabah")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="No Rekening">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnorekening" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Id Permohonan" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegister" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Idpermohonan")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Sumber Dana">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamasd" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaBank")%>'></asp:Label>
                                                 <asp:Label ID="lblsumberdana" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodesumberdana")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        

                                         <asp:TemplateField HeaderText="Tanggal PK">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltanggalpk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_drop","{0:dd/MM/yyyy}")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                                     

                            
                                       
                                  


                                      
                                     
                                            
                                         
                                    </Columns>
                                </asp:GridView>

                                     
                                                            
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
                <asp:PostBackTrigger ControlID="btnSearch" />
                <%--<asp:PostBackTrigger ControlID="BtnTarik" />--%>                
                <asp:PostBackTrigger ControlID="GRData" />                   
                <asp:PostBackTrigger ControlID="ddlsumberdana" />                   
                 
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
