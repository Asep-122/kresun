<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeFile="UploadDokumentPencairanKreditNasabah.aspx.cs" Inherits="AR_Process_UploadDokumentPencairanKreditNasabah" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pencairan Kredit</title>
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
        function CheckFilePdfNasabah() {

            var filePath = document.getElementById('<%= this.fileuploadpdf1.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Pdf Nasabah belum di pilih!");


                return false;
            }
        }
        function CheckFilePdfSumberDana() {

            var filePath = document.getElementById('<%= this.fileuploadpdf2.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Pdf Sumber Dana belum di pilih!");


                return false;
            }
        }

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


            });



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
                                    <li class="breadcrumb-item"><a href="PencairanKreditNasabah.aspx">Uplaod Dokument Pencairan Kredit Pensiunan</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Uplaod Dokument Pencairan Kredit Pensiunan</h4>
                        </div>
                    </div>
                </div>

  <asp:Panel ID="PnlFront" runat="server">
      <div class="row">
              <div class="col-md-12">
                  <div class="card-box">
                      <h4 class="m-t-0 header-title">  <asp:TextBox ID="txtfocus" CssClass="form-control"  readonly="true" text="Data Pencairan Kredit Pensiunan" runat="server" ></asp:TextBox> </h4>   
                      
                               <div class="form-row">
                            <div class="form-group" style="width: 100%;">



                                <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"   AutoGenerateColumns="False" OnRowCommand="GRData_RowCommand" >
                              <Columns>
                                    <asp:TemplateField HeaderText="Cabang">
                                      <ItemTemplate>
                                             <asp:Label ID="lblnamacabang" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabang_nama")%>'></asp:Label>
                                           <asp:Label ID="lblkodecabang" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_cabang")%>'></asp:Label>
                      
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
                                          <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>
                                               
                                  <asp:TemplateField HeaderText="Nama">
                                      <ItemTemplate>

                                          <asp:Label ID="lblnamanasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namanasabah")%>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                        
                                  
                                  
                                 
                                  <asp:TemplateField HeaderText="Jangka">
                                      <ItemTemplate>
                                          <asp:Label ID="lbljangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "permohonan_jangka_tahun")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Plafon">
                                      <ItemTemplate>
                                          <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "permohonan_plafon", "{0:N0}")%>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                  </asp:TemplateField>
                      
                                 
                            

                                  <asp:TemplateField HeaderText="Action">
                                                  <HeaderStyle CssClass="text-center"></HeaderStyle>
                                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                  <ItemTemplate>
                                                      <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Process Verifikasi" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                      <asp:LinkButton ID="btnApproveAntrian" Visible="false" runat="server" data-toggle="tooltip" data-placement="left" title="Approve Antrian" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-export"></i></asp:LinkButton>
                                                      <%--<asp:LinkButton ID="btnupdate" runat="server" data-toggle="tooltip" data-placement="left" title="Update Data" CommandName="UPDATEX" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-document-edit"></i></asp:LinkButton>--%>
                                                      <%--<asp:LinkButton ID="BtnApproveTeller" runat="server" data-toggle="tooltip" data-placement="left" title="Lanjutkan Ke Teller" CommandName="Lanjutkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-user"></i></asp:LinkButton>--%>
                                                      <asp:LinkButton ID="BtnBatalkanAntrian" Visible="false" runat="server" data-toggle="tooltip" data-placement="left" title="Batalkan Antrian" CommandName="Batalkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-thumbs-down"></i></asp:LinkButton>
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

  <asp:Panel ID="pnlData" Visible="false" runat="server">                                                                     
         <div class="row">

                  <div class="col-lg-12">
                    <div class="col-lg-12">
                       <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Data</div>
                           <p class="text-muted font-14 m-b-30"></p>   
                           <div class="form-row">
                               <div class="form-group col-md-6">
                                <div class="form-group">
                                        <label for="txtIdPermohonan" class="col-3 col-form-label">Id Permohonan</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtIdPermohonan" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                        <label for="inputState" class="col-3 col-form-label">No. Anggota ERP</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNoAnggota" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                    <div class="form-group">
                                           <label for="inputState" class="col-3 col-form-label">No Rekening</label>     
                                       <div class="col-8">
                                           <asp:TextBox ID="txtNoRekening" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false"></asp:TextBox>
                                       </div>
                                   </div>
                                   </div>                               
                               <div class="form-group col-md-6">
                               <div class="form-group">
                                     <label for="inputStates" class="col-3 col-form-label">Nama</label>     
                                 <div class="col-8">
                                     <asp:TextBox ID="txtNama" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false"></asp:TextBox>
                                 </div>
                               </div>
                               <div class="form-group">
                                        <label for="inputStates" class="col-3 col-form-label">Sumber Dana</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtSumberDana" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false"></asp:TextBox>
                               </div>
                                   <div class="form-group">
                                        <label for="inputStates" class="col-3 col-form-label">Plafon</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtPlafon" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false"></asp:TextBox>
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
   <asp:Panel ID="pnlUploadDokument" Visible="false" runat="server">
    <div class="row">
      <div class="col-lg-12">
            <div class="col-lg-12">
                <div class="card-box ribbon-box">
                    <div class="ribbon ribbon-custom">Upload Dokument</div>
                    <p class="text-muted font-14 m-b-30"></p>    
                        <div class="form-group col-md-6">                    
                        <asp:label ID="LabelPdf" runat="server" class="col-form-label">File PDF Pencairan Nasabah</asp:label>
                            <div>
                                <asp:FileUpload ID="fileuploadpdf1" runat="server"/> 
                                <asp:Button ID="btnPdf1" Text="Upload" runat="server"   OnClientClick="return CheckFilePdfNasabah()" OnClick="btnPdf1_Click"/>
                        <hr />
                            <asp:TextBox ID="txtPdf1" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                            <asp:TextBox ID="txtfilename1" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>                                                                                                                             
                            </div>
                        </div>                          
                    <div class="form-group col-md-6">                    
                        <asp:label ID="LabelPdf2" runat="server" class="col-form-label">File PDF Pemberkasan Sumber Dana</asp:label>
                            <div>
                                <asp:FileUpload ID="fileuploadpdf2" runat="server"/> 
                                <asp:Button ID="btnPdf2" Text="Upload" runat="server"   OnClientClick="return CheckFilePdfSumberDana()" OnClick="btnPdf2_Click"/>
                        <hr />
                            <asp:TextBox ID="txtPdf2" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                            <asp:TextBox ID="txtfilename2" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>                                                                                                                             
                            </div>
                    </div>   
                    <div class="form-group col-md-6">                    
                        <asp:label ID="LabelPdf3" runat="server" class="col-form-label">File PDF Take Over / Mutasi Kantor Bayar</asp:label>
                            <div>
                                <asp:FileUpload ID="fileuploadpdf3" runat="server"/> 
                                <asp:Button ID="btnPdf3" Text="Upload" runat="server"   OnClientClick="return CheckFilePdfTakeOver()" OnClick="btnPdf3_Click"/>
                        <hr />
                            <asp:TextBox ID="txtPdf3" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                            <asp:TextBox ID="txtfilename3" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>                                                                                                                             
                            </div>
                    </div>   
                        <div class="form-group">
                           <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white right" runat="server" Text="Process" OnClick="btnSearch_Click" />  
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
                   <asp:PostBackTrigger ControlID="GRData" />  
                   <asp:PostBackTrigger ControlID="btnPdf1" />  
                   <asp:PostBackTrigger ControlID="btnPdf2" />  
                   <asp:PostBackTrigger ControlID="btnPdf3" />                      
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
