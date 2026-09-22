<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeFile="MappingPengajuanAsuransi.aspx.cs" Inherits="AS_Process_MappingPengajuanAsuransi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Mapping Pengajuan Asuransi</title>
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

       <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

   
   
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

        function Alertfailed(h, t, p, i) {
            setTimeout(function () {
                Swal.fire({
                    title: h,
                    text: t,
                    icon: "error"
                });
            }, 100);

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
                        window.location.href = "MappingPengajuanAsuransi.aspx"
                    }
                });;
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

            $("#txtcustomer").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "MappingPengajuanAsuransi.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
            dataType: "json",
            success: function (data) {
                obj = JSON.parse(stringToArray(data.d));
                console.log(obj);
                var found = $.map(obj, function (item) {
                    return {
                        label: item[3],
                        hdncustomer: item[0],
                        HDNoRekening: item[1],
                        HDNKodeCabang: item[2]
                    }
                });

                $("[id$=hdncustomer]").val('');
                $("[id$=HDNoRekening]").val('');
                $("[id$=HDNKodeCabang]").val('');

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
                       $("[id$=HDNKodeCabang]").val(i.item.HDNKodeCabang);
                   },
                   minLength: 3
               });
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
                                    <li class="breadcrumb-item"><a href="PencairanKreditNasabah.aspx">Mapping Pengajuan Asuransi</a></li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>

    <!-- Panel Header -->
    <asp:Panel ID="pnlHeader" runat="server">
        <div class="row">
        <div class="col-12">
           <h4 class="m-t-0 header-title">  <asp:TextBox ID="txtfocus" CssClass="form-control"  readonly="true" text="Mapping Pengajuan Asuransi Kredit Pensiunan" runat="server" ></asp:TextBox> </h4>   

            <div class="card-box table-responsive">
                <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Transaksi" OnClick="btnAdd_Click"  />
            </div>
        </div>
        </div>
    </asp:Panel>
  <asp:Panel ID="PnlFront" runat="server">
      <div class="row">
              <div class="col-md-12">
                  <div class="card-box">
                      
                               <div class="form-row">
                            <div class="form-group" style="width: 100%;">



                                <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"   AutoGenerateColumns="False" OnRowCommand="GRData_RowCommand" >
                              <Columns>
                                    <asp:TemplateField HeaderText="Id Permohonan">
                                      <ItemTemplate>
                                             <asp:Label ID="lbltxtIdPermohonan" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "idpermohonan")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                     
                                  <asp:TemplateField HeaderText="kodesimulasi" Visible="false">
                                      <ItemTemplate>
                                             <asp:Label ID="lblkodesimulasi" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodeSimulasi")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="usiaasuransi" Visible="false">
                                      <ItemTemplate>
                                             <asp:Label ID="lblusiasuransi" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usiaasuransi")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                  
                                  <asp:TemplateField HeaderText="asuransi" Visible="false">
                                      <ItemTemplate>
                                             <asp:Label ID="lblasuransi" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "asuransirp","{0:N2}")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="jangkatahun" Visible="false">
                                      <ItemTemplate>
                                             <asp:Label ID="lbljangkatahun" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka_tahun")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="kodeasuransi" Visible="false">
                                      <ItemTemplate>
                                             <asp:Label ID="lblkodeasuransi" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodeasuransi")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="namaasuransi" Visible="false">
                                      <ItemTemplate>
                                             <asp:Label ID="lblnamaasuransi" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaasuransi")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="rateasuransivrf" Visible="false">
                                      <ItemTemplate>
                                             <asp:Label ID="lblrateasuransivrf" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rateasuransivrf","{0:N3}")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="premigrossasuransi" Visible="false">
                                      <ItemTemplate>
                                             <asp:Label ID="lblpremigrossasuransi" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "premigrossasuransi","{0:N0}")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="selisihasuransivrf" Visible="false">
                                      <ItemTemplate>
                                             <asp:Label ID="lblselisihasuransivrf" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "selisihasuransivrf","{0:N0}")%>'></asp:Label>                      
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="Cabang">
                                      <ItemTemplate>
                                             <asp:Label ID="lblnamacabang" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>                      
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
                                  <asp:TemplateField HeaderText="Anggota">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Nama">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnamanasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>         
                                    <asp:TemplateField HeaderText="KantorBayar">
                                      <ItemTemplate>
                                           <asp:Label ID="lblkantorbayar" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakantorbayar")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Sumber Dana">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnamasd" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namasumberdana")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                               
                                   <asp:TemplateField HeaderText="Plafon">
                                      <ItemTemplate>
                                          <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon", "{0:N0}")%>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                  </asp:TemplateField>                                   
                                  <asp:TemplateField HeaderText="Plafon">
                                      <ItemTemplate>
                                          <asp:Label ID="lblterimabersih" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "terimabersihrp", "{0:N0}")%>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                  </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Action">
                                                  <HeaderStyle CssClass="text-center"></HeaderStyle>
                                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                  <ItemTemplate>
                                                      <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Process Verifikasi" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                      <asp:LinkButton ID="btnDelete" runat="server" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-trash"></i></asp:LinkButton>
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
    <asp:Panel ID="Pnlcustomer" Visible="true" runat="server">
 <div class="row clearfix">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
          <div class="row">
        
               <div class="col-12">
                   <div class="card-box ribbon-box">
                       <div class="ribbon ribbon-custom">Pencarian Customer</div>
                       <p class="text-muted font-14 m-b-30"></p>                                                                           
                    <div class="form-group col-md-12">
                           <label class="col-form-label">Entry Data Customer</label>
                          <asp:HiddenField ID="HDNoNasabah" runat="server" />
                          <asp:HiddenField ID="HDNoRekening" runat="server" />
                          <asp:HiddenField ID="HDNKodeCabang" runat="server" />
                          <asp:HiddenField ID="hdncustomer" runat="server" />
                           <asp:TextBox CssClass="form-control" ID="txtcustomer" runat="server" required="required"></asp:TextBox>
                    </div>     
                       <div class="form-group">
                           <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSerach_Click"/>                                              
                        </div>
                                                                           
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
                                  <div class="form-group">
                                        <label for="txtIdPermohon" class="col-3 col-form-label">Angsuran Asuransi</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtkodesimulasi" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtusiaasuransi" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtasuransi" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false" Visible="true" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                        <asp:TextBox ID="txtjangkatahun" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false" Visible="false"></asp:TextBox>

                                        <asp:TextBox ID="txtIdPermohonan" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false" Visible="false"></asp:TextBox>
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
                                        <asp:TextBox ID="txtPlafon" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
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
   <asp:Panel ID="pnlAsuransi" Visible="false" runat="server">
             <div class="row">
                                  <div class="col-lg-12"> 
                                             <div class="card-box ribbon-box">
                                              <div class="ribbon ribbon-custom">INFORMASI DATA ASURANSI</div>
                                                 <p class="m-b-0"></p>
                           
                                                           <div class="row">

                              <div class="col-lg-6">
                                  <div class="col-lg-12">
                                      <div class="card-box ribbon-box">
                                          <div class="ribbon ribbon-custom">Data Asuransi</div>
                                           <p class="text-muted font-14 m-b-30"></p>
                                               

                                           <div class ="form-row">
                                                          <div class="col-md-6">
                                                              <p class="form-control">Asuransi</p>
                                                                  <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                          </div>
                                                          <div class="col-md-6">

                                                              <asp:DropDownList ID="ddlkodeasuransi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlkodeasuransi_SelectedIndexChanged"  ></asp:DropDownList>
                                                          </div>
                                                      </div>

                                                      <div class ="form-row">
                                                          <div class="col-md-6">
                                                              <p class="form-control">Nama Asuransi</p>
                                                                  <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                          </div>
                                                          <div class="col-md-6">

                                                               <asp:TextBox CssClass="form-control" ID="txtnamaasuransi" text="0" MaxLength="50" Enabled="false"  runat="server" ></asp:TextBox> 
                                                          </div>
                                                      </div>


                                                      <div class ="form-row">
                                                          <div class="col-md-6">
                                                              <p class="form-control">rate Asuransi</p>
                                                                  <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                          </div>
                                                          <div class="col-md-6">

                                                                <asp:TextBox ID="txtrateasuransipanggil" CssClass="form-control"  runat="server" text="0,00" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  ></asp:TextBox> 
                                                          </div>
                                                      </div>

                                                      <div class ="form-row">
                                                          <div class="col-md-6">
                                                              <p class="form-control">Premi Asuransi</p>
                                                                  <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                          </div>
                                                          <div class="col-md-6">

                                                                <asp:TextBox ID="txtpremiasuransi" CssClass="form-control"  runat="server" text="0,00" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  ></asp:TextBox> 
                                                          </div>
                                                      </div>

                                                  <div class ="form-row">
                                                          <div class="col-md-6">
                                                              <p class="form-control">Selisih Asuransi</p>
                                                                  <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                          </div>
                                                          <div class="col-md-6">

                                                                <asp:TextBox ID="txtselisihasuransi" CssClass="form-control"  runat="server" text="0,00" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  ></asp:TextBox> 
                                                          </div>
                                                      </div>

                                      </div>
                                  </div>

                                  
                              </div>

                          </div>
                               
                                             </div>  

                                      </div>
                                   </div> 
                       <div class="form-group">
                           <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" type="button" Text="BACK" OnClick="btnCancel_Click" />
                           <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white right" runat="server" Text="Simpan" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />  
                           <asp:Button ID="btnDelete" class="btn btn-success waves-light text-white right" runat="server" Text="Delete" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');"  Visible="false" OnClick="btnDelete_Click" />  
                        </div>       
   </asp:Panel>    
            </div> 
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>  
                   <asp:PostBackTrigger ControlID="GRData" />                 
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
