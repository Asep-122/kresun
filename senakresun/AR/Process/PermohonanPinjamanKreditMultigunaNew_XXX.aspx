<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeFile="PermohonanPinjamanKreditMultigunaNew_XXX.aspx.cs" Inherits="AR_Process_PermohonanPinjamanKreditMultigunaNew_XXX" %>

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

                {
                $('#<%=txtketeranganJaminan.ClientID%>').prop('maxlength', 250);

                
            }
            {
                $('#<%=txtjaminansurat.ClientID%>').prop('maxlength', 60);


            }

            
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
                                    <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="PermohonanPinjamanKreditMultiguna.aspx">Permohonan Pinjaman Kredit Multiguna</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Permohonan Pinjaman Kredit Multiguna</h4>
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
                                                                        <asp:Button ID="BtnTarik"  CssClass="btn btn-warning waves-light text-white" runat="server" Text="Tarik-TP" OnClick="btntarik_Click" OnClientClick="return sweetAlertsave(this,'Khusus Anggota FULL-UP Apakah anda yakin ingin Melakukan Perubahan Menjadi Tarik Pusat, SIMPOK dan SIMWA akan 0 ?');"/>
                                                                        <asp:Button ID="BtnTarikRlb"  CssClass="btn btn-primary waves-light text-white" runat="server" Text="Roll Back - Tarik TP" OnClick="btntarikrlb_Click" OnClientClick="return sweetAlertsave(this,'Khusus Anggota Tarik Pusat Yang dilakukan di Permohonan ini, Apakah anda yakin ingin Melakukan Roll Back ?');"/>
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
              
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Permohonan Pinjaman</h4>   
                            <div class="form-group col-md-4">



                                                                <asp:label ID="Label54" runat="server" class="col-form-label">Status Permohonan</asp:label>
                                                                    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatus_SelectedIndexChanged">
                                                                    <asp:ListItem Value="3">Approve Antrian</asp:ListItem>
                                                                    <asp:ListItem Value="4">Interview Admin</asp:ListItem>  
                                                                    <asp:ListItem Value="5">Teller</asp:ListItem> 
                                                                    <asp:ListItem Value="1">Approval Pusat</asp:ListItem>
                                                                    <asp:ListItem Value="2">Reject Approval Pusat</asp:ListItem>
                                                                    <asp:ListItem Value="9">Pencairan</asp:ListItem>
                                                                    <asp:ListItem Value="10">Batal Permohonan</asp:ListItem>
                                                                    </asp:DropDownList>
                            
                            </div>
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                     <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"     AutoGenerateColumns="False" Width="887px" OnRowCommand="GRData_RowCommand">
                                    <Columns>
                                     <asp:TemplateField HeaderText="Id Permohonan" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegister" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Idpermohonan")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anggota ERP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkodeCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_registrasi")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Rekening ERP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRekening" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening1")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Anggota CBS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblanggotacbs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah_cbs")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rekening CBS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRekeningcbs" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening1cbs")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="KTP" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LblKTP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_identitas")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Anggota">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Produk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProduk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Tipe">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltipe" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tipeperpanjangan")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jangka">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNpwp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plafon">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Plafon", "{0:N2}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanggal Pengajuan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrekeningkreditbo" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening1")%>'></asp:Label>
                                                
                                                
                                                <asp:Label ID="lblnonasabah" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                                 <asp:Label ID="lblid" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                                 <asp:Label ID="lblTypebiayakeanggotaan" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Typebiayakeanggotaan")%>'></asp:Label>
                                           <asp:Label ID="lblalamat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgldrop","{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Print">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="BtnBukti" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Cetak Bukti" CommandName="Bukti" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="BtnKwPinjaman" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Kwitansi Pinjaman" CommandName="Kwitansipinjaman" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="BtnAngsuran" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Angsuran Pinjaman" CommandName="Angsuranpinjaman" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Perjanjian Kredit" CommandName="PerjanjianKredit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Tanda Terima Anggota" CommandName="TandaTerimaAnggota" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Surat Tanda Terima" CommandName="TandaTerimaJaminan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Surat Penjamin" CommandName="TandaPenjamin" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                      
                                     
                                            <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit Data" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnApproveAntrian" runat="server" data-toggle="tooltip" data-placement="left" title="Approve Antrian" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-export"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnupdate" runat="server" data-toggle="tooltip" data-placement="left" title="Update Data" CommandName="UPDATEX" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-document-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnApproveTeller" runat="server" data-toggle="tooltip" data-placement="left" title="Lanjutkan Ke Teller" CommandName="Lanjutkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-user"></i></asp:LinkButton>
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
                                 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label151" runat="server" class="col-form-label">No. Anggota CBS</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtanggotacbs"  Enabled="false" runat="server" required="required"></asp:TextBox>
                                                          
                                                                </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label43" runat="server" class="col-form-label">No. Anggota ERP</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnoanggota"  Enabled="false" runat="server" required="required"></asp:TextBox>
                                                           <asp:TextBox CssClass="form-control" ID="txtnorektabungan" Visible="false"   runat="server" required="required"></asp:TextBox>
                                                                </div>
                                                           
                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Nama Anggota</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamanasabah"  Enabled="false" runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-row">
                                                                 <div class="form-group col-md-4">
                                                            
                                                                        <asp:label ID="Label91" runat="server" class="col-form-label">Tanggal Bergabung</asp:label>
																        <asp:TextBox ID="txttglbergabung" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 
												
                                                                </div>
                                                          
                                                       


													</div>
                                                  
                                                            <div class="form-row">
                                                                 <div class="form-group col-md-4">
                                                                <asp:label ID="Label44" runat="server" class="col-form-label">Pendidikan</asp:label>
																<asp:DropDownList ID="ddlpendidikanktp" Enabled="false" CssClass="form-control" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                            </div>
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label31" runat="server" class="col-form-label">No.HP</asp:label>
																<asp:TextBox ID="txthpktp" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);"  runat="server" ></asp:TextBox>
                                                            </div> 
                                                               <div class="form-group col-md-4">
                                                                <asp:label ID="Label11" runat="server" class="col-form-label">Email</asp:label>
																<asp:TextBox ID="txtemailsktp" Enabled="false" CssClass="form-control" MaxLength="100"   runat="server"  AutoPostBack="true"  OnTextChanged="txtemails_TextChanged"></asp:TextBox>
                                                                 
                                                            
                                                            </div>
                                                            </div>
                                                            

                                                           <div class="form-group">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Alamat</asp:label>
																<asp:TextBox ID="txtalamatktp"  CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                                <asp:TextBox CssClass="form-control" ID="txtrtktp" visible="false"  runat="server"></asp:TextBox>
                                                                <asp:TextBox CssClass="form-control" ID="txtrwktp" visible="false"  runat="server"></asp:TextBox>
                                                           </div>

                                                                
                                                               
															
                                                            
                                                           
                                                          
															
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label6" runat="server" class="col-form-label">No. KTP</asp:label> 
                                                                <asp:TextBox ID="txtnoidentitasktp" Enabled="false" CssClass="form-control" MaxLength="16" onkeypress="return isnumerickey(event);"  required="required" runat="server"></asp:TextBox>
                                                           </div> 
                                                           
                                                          

                                                        <div class="form-group">
                                                        <!-- Simple card -->
                                                        <div class="card m-b-30">
                                                        <asp:Image ID="imgktp" Visible="false" runat="server" class="card-img-top img-fluid"   alt="Card image cap" />
                                                        </div>
                                                        </div>

                                                        
                                                            <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label127" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:DropDownList ID="ddljenkelktp" Enabled="false" CssClass="form-control" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Tempat Lahir</asp:label>
																<asp:TextBox ID="txttempatlahirktp" CssClass="form-control" Enabled="false" required="required" runat="server" ></asp:TextBox>
															</div>

                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">Tanggal Lahir</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txttgllahirktp"  Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox> 
                                                            </div> 
                                                            </div>
                                                             <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label13" runat="server" class="col-form-label">Agama</asp:label>
																<asp:DropDownList ID="ddlAgamaktp" Enabled="false" CssClass="form-control" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                           
                                                            </div>  

                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label128" runat="server" class="col-form-label">Status Pernikahan</asp:label>
																<asp:DropDownList ID="ddlstatusnikahktp" Enabled="false" CssClass="form-control" runat="server" data-style="btn-custom"   ></asp:DropDownList>

                                                             </div>
                                                                  <div class="form-group col-md-4">
                                                                <asp:label ID="Label129" runat="server" class="col-form-label">Pekerjaan</asp:label>
																<asp:DropDownList ID="ddlpekerjaanktp" Enabled="false" CssClass="form-control" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                            </div>
                                                            </div>
                                                              <div class="form-row">
                                                                    <div class="form-group col-md-4">
                                                                    <asp:label id="Label130" runat="server"  class="col-form-label">Kewarganegaraan</asp:label>
                                                                      <asp:DropDownList ID="ddlkewarganegaraanktp" Enabled="false" CssClass="form-control" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                           </div> 
                                                            <div class="form-group col-md-4">
                                                                    <asp:label id="Label22" runat="server"  class="col-form-label">Masa Berakhir KTP</asp:label>
                                                                    <asp:TextBox ID="txtmasaberakhirKTP" Enabled="false" CssClass="form-control" runat="server"  TextMode="DateTime"></asp:TextBox>
                                                           </div> 

                                                        </div> 
                                                            <div class="form-row">
                                                          <div class="form-group col-md-4">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label">Kelurahan</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtkelurahanktp" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label45" runat="server" class="col-form-label">Kecamatan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkecamatanktp" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label46" runat="server" class="col-form-label">Kota</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkotaktp" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div> 
                                                                  
                                                            </div>
                                                  
                                                            <div class="form-row">
                                                             
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label125" runat="server" class="col-form-label">Provinsi</asp:label>
																<asp:DropDownList ID="ddlpropinsiktp" enabled="false" CssClass="form-control"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                            </div>
                                                                 <div class="form-group col-md-4">
                                                                <asp:label ID="Label132" runat="server" class="col-form-label">kode Pos</asp:label>
																 <asp:TextBox CssClass="form-control" ID="txtkodeposktp" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div>

                                                                </div>
                                                           

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                           
                                  
    
            </asp:Panel> 

             <asp:Panel ID="pnlIdentitas" Visible="false" runat="server">
                      
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                
               
                                 
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA TEMPAT TINGGAL</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                      <div class="form-group">
                                                                <asp:label ID="Label104" runat="server" class="col-form-label">Status Rumah</asp:label>
																<asp:DropDownList ID="ddlstatusrumah" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusrumah_SelectedIndexChanged"></asp:DropDownList>
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
                                                                <asp:label ID="Label10" runat="server" class="col-form-label">Status dalam keluarga</asp:label>
																<asp:DropDownList ID="ddlstatuskawinupload" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatuskawin_SelectedIndexChanged" ></asp:DropDownList>
                                                            </div>
                                                                <div class="form-group">
                                                                <asp:label ID="Label107" runat="server" class="col-form-label">No.Identitas Pasangan</asp:label>
																<asp:TextBox ID="txtidpasangan" CssClass="form-control"   MaxLength="16"  onkeypress="return isnumerickey(event);"    runat="server" AutoPostBack="true"  OnTextChanged="txtidpasangan_TextChanged"></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator30" runat="server" SetFocusOnError="True" ControlToValidate="txtidpasangan"  ErrorMessage="No. KTP Pasangan tidak boleh kosong (isi '0' jika tidak belum menikah)"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>
                                                             	<div class="form-group">
                                                                <asp:label ID="Label108" runat="server" class="col-form-label">Nama Pasangan</asp:label>
																<asp:TextBox ID="txtnamapasangan" CssClass="form-control"  MaxLength="70"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator31" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapasangan"  ErrorMessage="Nama Pasangan tidak boleh kosong (isi '-' jika tidak belum menikah)"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                            
															<div class="form-group">
                                                                <asp:label ID="Label109" runat="server" class="col-form-label">Tempat Lahir Pasangan</asp:label>
																<asp:TextBox ID="txttempatlahirpasangan" CssClass="form-control"  MaxLength="30"  runat="server" ></asp:TextBox>
															</div>

                                                             <div class="form-group">
                                                                 <asp:label id="Label110" runat="server"  class="col-form-label">Tanggal Lahir Pasangan</asp:label>
                                                                 <asp:TextBox ID="txttgllahirpasangan"  CssClass="form-control" runat="server"  TextMode="DateTime" ></asp:TextBox>
                                                            </div>  
                                                        

                                                       

                                            </div>
                                        </div>
                                    </div>


                                </div>

                                    
                            </div>

                             
                    
                    </asp:Panel>
            
                      <asp:Panel ID="Pnlupdatedata" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">Update Data Jaminan dan Anggota</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                       <form class="form-horizontal" role="form">
                                                
                                       
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">No Rek. Pinjaman</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblrekening" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                           <div class="form-group row">
                                                <label class="col-3 col-form-label">Jaminan Surat</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbljaminansuratx" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                                <div class="form-group row">
                                                                <asp:label ID="Label148" runat="server" class="col-3 col-form-label">Detail Jaminan</asp:label>
													<div class="col-8">		
                                                    <asp:TextBox ID="txtketeranganJaminanupdate"  CssClass="form-control"  TextMode="MultiLine" Rows="7" runat="server" data-style="btn-custom" MaxLength="250" ></asp:TextBox>          
                                                    </div>
                                                    </div>
                                             <div class="form-group row">
                                                <label class="col-3 col-form-label">No Anggota</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblanggotax" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label" for="example-email">Nama Anggota</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblnamaanggota" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                    
                                                </div>
                                            </div>

                                                            <div class="form-group row">
                                                                <asp:label ID="Label149" runat="server" class="col-3 col-form-label">No.HP</asp:label>
																<div class="col-8">
                                                                <asp:TextBox ID="txtnohpx" CssClass="form-control" MaxLength="20" Enabled="true" onkeypress="return isnumerickey(event);"  required="required" runat="server" ></asp:TextBox>
                                                                </div>
                                                                </div> 
                                                               <div class="form-group row">
                                                                <asp:label ID="Label150" runat="server" class="col-3 col-form-label">Email</asp:label>
																<div class="col-8">
                                                                   <asp:TextBox ID="txtemailx" Enabled="true" CssClass="form-control" MaxLength="40"   runat="server"  ></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <asp:label ID="Label154" runat="server" class="col-3 col-form-label">Nomor Identitas KTP</asp:label>
																<div class="col-8">
                                                                   <asp:TextBox ID="txtidentitasx" CssClass="form-control"  MaxLength="16" onkeypress="return isnumerickey(event);"     runat="server" Width="219px" ></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            

                                   
                                        </form>
                                                           
															
                                                         
                                                        </div>

                                                          
                                                           
															
                                                         
                                                        </div>
                                                        


													</div>
												</div>  

              </div>
                                         </div>        
                  
                                <div class="hidden-print mt-4">
                                    <div class="row">
                                    <div class="col-md-12">
                                    <div class="card-box">
                                    <div class="card-box table-responsive">
                                    <asp:Button ID="Button1" class="btn btn-success waves-light text-white" type="button" runat="server" Text="BACK" OnClick="btnCancel_Click" />
                                    <asp:Button ID="Button4"  class="btn btn-success waves-light text-white" type="button" runat="server" Text="UPDATE" OnClick="btnupdate_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Update data ini ?');"/>
                                   
                                        </div>
                                    </div>
                                    </div>
                                    </div> 
                                </div>            
        
            </asp:Panel>
       

             <asp:Panel ID="pnlPekerjaan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PEKERJAAN ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              
                                                            
															
															<div class="form-group">
                                                                <asp:label ID="Label8" runat="server" class="col-form-label">Nama Pekerjaan</asp:label>
																<asp:TextBox ID="txtPekerjaan"  MaxLength="70" CssClass="form-control"  runat="server" text="-"></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
															</div>
                                                             
                                                            <div class="form-group">
                                                                 <asp:label id="Label47" runat="server" class="col-form-label">Jabatan</asp:label>
                                                                 <asp:TextBox ID="txtjabatan"  MaxLength="50"  CssClass="form-control"  runat="server" text="-"  ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal
                                                            </div>  

                                                            <div class="form-group">
                                                                 <asp:label id="Label9" runat="server"  class="col-form-label">Nama Perusahaan</asp:label>
                                                                 <asp:TextBox ID="txtNamaPerusahaan"  MaxLength="100" CssClass="form-control"  runat="server" text="-"  ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal
                                                            </div>   

                                                            <div class="form-group">
                                                                 <asp:label id="Label12" runat="server"  class="col-form-label">Bidang Usaha Perusahaan</asp:label>
                                                                 <asp:TextBox ID="txtBidangUsaha"  MaxLength="100" CssClass="form-control"  runat="server" text="-"  ></asp:TextBox>
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label49" runat="server" class="col-form-label">Lama Usaha/Bekerja</asp:label>
																<asp:DropDownList ID="ddlLamaBekerja" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator46" ErrorMessage="Agama tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlLamaBekerja" Runat="server" />

                                                                 
                                                            </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label51" runat="server" class="col-form-label">Kepemilikan Tempat Usaha</asp:label>
																<asp:DropDownList ID="ddlkepemilikantempatusaha" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   AutoPostBack="true" OnSelectedIndexChanged ="ddlkepemilikantempatusaha_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:TextBox ID="txtkepemilikantempatusaha" Enabled="false" MaxLength="50" CssClass="form-control" runat="server"  text="-"  ></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "-" jika tidak perlu</span>
                                                            </div> 
                                                            
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                              <div class="form-group">
                                                                <asp:label ID="Label14" runat="server" class="col-form-label">Alamat Perusahaan</asp:label>
																<asp:TextBox ID="txtalamatpekerjaan"  MaxLength="100" CssClass="form-control"  runat="server" text="-" TextMode="MultiLine"></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                              </div>

                                                          
                                                            <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label15" runat="server" class="col-form-label">Kabupaten</asp:label>
                                                                 <asp:TextBox ID="txtkabupatenpekerjaan" MaxLength="50" CssClass="form-control"  runat="server" text="-" ></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                            </div>  
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label16" runat="server" class="col-form-label">Kelurahan</asp:label>
                                                                 <asp:TextBox ID="txtkelurahanpekerjaan" MaxLength="50" CssClass="form-control"  runat="server" text="-" ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                            </div>  
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label17" runat="server" class="col-form-label">Kecamatan</asp:label>
                                                                 <asp:TextBox ID="txtkecamatanpekerjaan" MaxLength="50" CssClass="form-control"  runat="server" text="-" ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                            </div>  
                                                            </div>  
                                                            <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label58" runat="server" class="col-form-label">Provinsi</asp:label>
																<asp:DropDownList ID="ddlprovinsipekerjaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                                <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
															</div>
                                                            
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label59" runat="server" class="col-form-label">Kode Pos</asp:label>
																<asp:TextBox ID="txtkodepospekerjaan" MaxLength="6" CssClass="form-control" onkeypress="return isnumerickey(event);" text="0"   runat="server" ></asp:TextBox>
															    <span class="font-14 text-muted">isi "0" jika bukan pekerja Formal</span>
                                                             </div>
                                                            </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label86" runat="server" class="col-form-label">Telepon</asp:label>
																<asp:TextBox ID="txtteleponpekerjaan" MaxLength="13" CssClass="form-control" onkeypress="return isnumerickey(event);"   runat="server" text="0" ></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "0" jika bukan pekerja Formal</span>
                                                              </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label92" runat="server" class="col-form-label">Fax</asp:label>
																<asp:TextBox ID="txtFaxPekerjaan" MaxLength="10" CssClass="form-control" onkeypress="return isnumerickey(event);"   runat="server" text="0" ></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "0" jika bukan pekerja Formal</span>
                                                              </div>
                                                            

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                       <%--     <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPekerjaan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPekerjaan_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextPekerjaan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPekerjaan_Click" Width="65px" />

                                                    </div>
                                            </div> --%>
     
    
            </asp:Panel> 

                 <asp:Panel ID="pnlTambahan" Visible="false" runat="server">
                      
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                
               
                                 
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA Tambahan Anggota</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                          <div class="form-group">
                                                                <asp:label ID="Label87" runat="server" class="col-form-label">Alamat Surat menyurat</asp:label>
																<asp:DropDownList ID="ddlAlamatSurat" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                                 
                                                            </div> 
                                                           <div class="form-group">
                                                                <asp:label ID="Label81" runat="server" class="col-form-label">Jenis Penggunaan</asp:label>
																<asp:DropDownList ID="ddlPenggunaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                            </div>
                                                             <div class="form-group">
                                                                <asp:label ID="Label68" runat="server" class="col-form-label">Golongan Debitur</asp:label>
																<asp:DropDownList ID="ddlgolongandebitur" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>
                                                            
                                                               <div class="form-group">
                                                                <asp:label ID="Label94" runat="server" class="col-form-label">Jumlah Anak</asp:label>
																<asp:TextBox ID="txtjumlahanak" MaxLength="2" CssClass="form-control"  onkeypress="return isnumerickey(event);"  required="required" runat="server" ></asp:TextBox>
                                                            </div> 

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Penjamin</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                
                                                  <div class="form-group">
                                                                <asp:label ID="Label50" runat="server" class="col-form-label">Golongan Penjamin</asp:label>
																<asp:DropDownList ID="ddlgolonganpenjamin" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label52" runat="server" class="col-form-label">Pendapatan/Bulan Penjamin</asp:label>
																<asp:TextBox ID="txtpendapatanperbulan" CssClass="form-control" MaxLength="18"  required="required" runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" Text="0"   ></asp:TextBox>
                                                                 <asp:TextBox ID="txtjaminantambahan" visible="false" CssClass="form-control" maxlength ="100"   required="required" runat="server" Text="-" ></asp:TextBox>
                                                                <asp:DropDownList ID="ddlfisikjaminan"  visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                            </div> 

                                                  
                                                            
                                                          

                                                            
                                                             

                                                              

                                                            
                                                            
                                                          

                                            </div>
                                        </div>
                                    </div>


                                </div>

                                    
                            </div>

                                <%--<div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackTambahan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackTambahan_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextTambahan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextTambahan_Click" Width="65px" />

                                                    </div>
                                            </div>--%>
                    
                    </asp:Panel>
             

            <asp:Panel ID="pnlPendapatan" Visible="false" runat="server">
                <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENDAPATAN ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              
                                                             <div class="form-group">
                                                                <asp:label ID="Label21" runat="server" class="col-form-label">Pendapatan Bersih Usaha</asp:label>
																<asp:TextBox ID="txtpendapatanbersih" Required="required" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" runat="server" MaxLength="16" AutoPostBack="true"  OnTextChanged="txtpendapatanbersih_TextChanged" >0</asp:TextBox>  
                                                   
                                                             </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label23" runat="server" class="col-form-label">Pendapatan Selain Usaha</asp:label>
																<asp:TextBox ID="txtpendapatanselain"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"   required="required" runat="server" AutoPostBack="true"  OnTextChanged="txtpendapatanselain_TextChanged" >0</asp:TextBox> 

															</div>

                                                            <div class="form-group">
                                                                 <asp:label id="lbltotalpendapatan" runat="server" class="col-form-label">Total Pendapatan</asp:label>
                                                                 <asp:TextBox ID="txttotalpendapatan" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"   required="required" runat="server" Text="0"  ></asp:TextBox> 
                                                            </div>   
                                                             
                                                     
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                 <asp:label id="Label24" runat="server" class="col-form-label">Pengeluaran Rumah Tangga</asp:label>
                                                                 <asp:TextBox ID="txtpengeluaranrumahtangga" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server"  AutoPostBack="true"  OnTextChanged="txtpengeluaranrumahtangga_TextChanged">0</asp:TextBox>   
                                                            
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label25" runat="server" class="col-form-label">Tagihan PLN, Listrik, PDAM, Telepon</asp:label>
																<asp:TextBox ID="txttagihanlistrik" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  OnTextChanged="txttagihanlistrik_TextChanged">0</asp:TextBox>      
                                                           </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label27" runat="server" class="col-form-label">Pinjaman / Kewajiban Lainnya</asp:label>
																<asp:TextBox ID="txtpinjamanlain" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  OnTextChanged="txtpinjamanlain_TextChanged">0</asp:TextBox>      
                                                           </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label28" runat="server" class="col-form-label">Rekomendasi Angsuran</asp:label>
																<asp:TextBox ID="txtangsuran" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"  AutoPostBack="true"  OnTextChanged="txtangsuran_TextChanged" >0</asp:TextBox>      
                                                           </div>

                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="lbltotalPengeluaran" runat="server" class="col-form-label"></asp:label>
																<asp:TextBox ID="txttotal" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" Text="0" ></asp:TextBox>      
                                                           </div>
                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="lblsisapenghasilan" runat="server" class="col-form-label"></asp:label>
																<asp:TextBox ID="TextBox7" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" Text="0"></asp:TextBox>      
                                                                
                                                           </div>

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                 <%--           <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPendapatan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPendapatan_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextPendapatan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPendapatan_Click" Width="65px" />

                                                    </div>
                                            </div> --%>
    
                </span></span> 
    
            </asp:Panel> 

                 <asp:Panel ID="pnlLaba" Visible="false" runat="server">
                 <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA LABA ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              
                                                             <div class="form-group">
                                                                <asp:label ID="Label26" runat="server" class="col-form-label">Pendapatan Usaha</asp:label>
																<asp:TextBox ID="txtpendapatanusaha"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" required="required" runat="server" MaxLength="16" AutoPostBack="true"  OnTextChanged="txtpendapatanusaha_TextChanged" >0</asp:TextBox>  
                                                   
                                                             </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label29" runat="server" class="col-form-label">Harga Pokok</asp:label>
																<asp:TextBox ID="txthargapokok"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"   required="required" runat="server"   AutoPostBack="true"  OnTextChanged="txthargapokok_TextChanged">0</asp:TextBox> 

															</div>

                                                            <div class="form-group">
                                                                 <asp:label id="lbllabakotor" runat="server" class="col-form-label">Laba Kotor</asp:label>
                                                                 <asp:TextBox ID="txtlabakotor" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"   required="required" runat="server"  >0</asp:TextBox> 
                                                            </div>   
                                                             
                                                     
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                 <asp:label id="Label32" runat="server" class="col-form-label">Biaya Pegawai</asp:label>
                                                                 <asp:TextBox ID="txtbiayapegawai" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" AutoPostBack="true"  OnTextChanged="txtbiayapegawai_TextChanged" >0</asp:TextBox>   
                                                            
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label33" runat="server" class="col-form-label">Biaya Lain-lain</asp:label>
																<asp:TextBox ID="txtbiayalain" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  OnTextChanged="txtbiayalain_TextChanged">0</asp:TextBox>      
                                                           </div>

                                                             <div class="form-group">
                                                                <asp:label ID="lbltotalbiaya" runat="server" class="col-form-label">Total Biaya</asp:label>
																<asp:TextBox ID="txttotalbiaya" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" >0</asp:TextBox>      
                                                           </div>
                                                             

                                                              <div class="form-group">
                                                                <asp:label ID="lbllabasebelumpajak" runat="server" class="col-form-label">Laba Bersih Sebelum Pajak</asp:label>
																<asp:TextBox ID="txtlababersihsebelumpajak" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"   >0</asp:TextBox>      
                                                           </div>

                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="Label36" runat="server" class="col-form-label">Pajak Penghasilan</asp:label>
																<asp:TextBox ID="txtpajakpenghasilan" Required ="required" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  OnTextChanged="txtpajakpenghasilan_TextChanged">0</asp:TextBox>      
                                                           </div> 

                                                           <div class="form-group">
                                                                <asp:label ID="lbllababersih" runat="server" class="col-form-label">Laba Bersih Setelah Pajak</asp:label>
																<asp:TextBox ID="txtlababersihsetelahpajak" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" >0</asp:TextBox>      
                                                                
                                                           </div>

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <%--<div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackLaba" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackLaba_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextLaba" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextLaba_Click" Width="65px" />

                                                    </div>
                                            </div>--%> 
    
                </span></span> 
    
            </asp:Panel> 




            <asp:Panel ID="pnlPihakYgDapatDiHubungi" Visible="false" runat="server">
                      
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                
               
                                 
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA PIHAK LAIN YANG DAPAT DI HUBUNGI</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                           
                                                                 <div class="form-group">
                                                                <asp:label ID="Label18" runat="server" class="col-form-label">Nama</asp:label>
                                                                 <asp:TextBox ID="txtnamapihaklain" MaxLength="70" CssClass="form-control"  runat="server" Text="-" ></asp:TextBox> 
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator35" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapihaklain"  ErrorMessage="Nama pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>  
															
															 <div class="form-group">
                                                                <asp:label ID="Label19" runat="server" class="col-form-label">Hubungan</asp:label>
																<asp:DropDownList ID="ddlHubunganPihakLain" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>                                                                
                                                             </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label20" runat="server" class="col-form-label">Alamat </asp:label>
                                                                 <asp:TextBox ID="txtalamatPihakLain" MaxLength="250" CssClass="form-control" Text="-" runat="server" TextMode ="MultiLine" Columns="55" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator37" runat="server" SetFocusOnError="True" ControlToValidate="txtalamatPihakLain"  ErrorMessage="Alamat pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                              </div> 
                                                            
                                                              <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label95" runat="server" class="col-form-label">RT</asp:label>
                                                                 <asp:TextBox ID="txtRTPihakLain" CssClass="form-control" MaxLength="3" Text="0" runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator38" runat="server" SetFocusOnError="True" ControlToValidate="txtRTPihakLain"  ErrorMessage="RT pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>  

                                                            
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label96" runat="server" class="col-form-label">RW</asp:label>
                                                                 <asp:TextBox ID="txtRWPihakLain" CssClass="form-control"  MaxLength="3" Text="0" runat="server" onkeypress="return isnumerickey(event);"  ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator39" runat="server" SetFocusOnError="True" ControlToValidate="txtRWPihakLain"  ErrorMessage="RW pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>
                                                            </div>
                                                            <div class="form-row">

                                                                           <div class="form-group col-md-4">
                                                                <asp:label ID="Label97" runat="server" class="col-form-label">Kelurahan</asp:label>
                                                                 <asp:TextBox ID="txtKelurahanPihakLain" MaxLength="50" CssClass="form-control" Text="-"  runat="server" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator40" runat="server" SetFocusOnError="True" ControlToValidate="txtKelurahanPihakLain"  ErrorMessage="Kelurahan pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label98" runat="server" class="col-form-label">Kecamatan</asp:label>
                                                                 <asp:TextBox ID="txtKecamatanPihakLain" MaxLength="50" CssClass="form-control" Text="-"  runat="server" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator41" runat="server" SetFocusOnError="True" ControlToValidate="txtKecamatanPihakLain"  ErrorMessage="Kecamatan pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label99" runat="server" class="col-form-label">Kota</asp:label>
                                                                 <asp:TextBox ID="txtKotaPihakLain" MaxLength="50" CssClass="form-control" Text="-"  runat="server" ></asp:TextBox> 
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator42" runat="server" SetFocusOnError="True" ControlToValidate="txtKotaPihakLain"  ErrorMessage="Kota pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                              </div>
                                                            <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label100" runat="server" class="col-form-label">Provinsi</asp:label>
                                                                <asp:DropDownList ID="ddlProvinsiPihakLain"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                               
                                                            </div>  
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label101" runat="server" class="col-form-label">Kode Pos</asp:label>
                                                                 <asp:TextBox ID="txtKodePosPihakLain" MaxLength="6" CssClass="form-control" Text="0"  runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator44" runat="server" SetFocusOnError="True" ControlToValidate="txtKodePosPihakLain"  ErrorMessage="kodepos pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                            </div>  
                                                            <div class="form-group">
                                                                <asp:label ID="Label102" runat="server" class="col-form-label">No.Telepon</asp:label>
                                                                 <asp:TextBox ID="txtTlpPihakLain" MaxLength="13" CssClass="form-control"  runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator45" runat="server" SetFocusOnError="True" ControlToValidate="txtTlpPihakLain"  ErrorMessage="No. Tlp pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                         
                                                         
                                                             
                                                              
                                                           
															
															
                                                              
                                                                

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA KRITERIA USAHA ANGGOTA</div>
                                                <p class="text-muted font-14 m-b-30"></p>                                             

                                                
                                                  
                                                         
                                                       <asp:HiddenField ID="HiddenField1" runat="server"/> 
                                                  
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label119" runat="server" class="col-form-label">Kondisi Usaha Saat Ini</asp:label>
																<asp:TextBox ID="txtkondisiusaha" CssClass="form-control" runat="server" maxlength ="200">-</asp:TextBox> 
															 </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label120" runat="server" class="col-form-label">Karakter Anggota</asp:label>
																<asp:TextBox ID="txtKarakterAnggota" CssClass="form-control" runat="server" maxlength ="200">-</asp:TextBox> 
                                                            </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label121" runat="server" class="col-form-label">Info Anggota dari Pihak ke 3</asp:label>
																<asp:TextBox ID="txtinfo" CssClass="form-control" runat="server" maxlength ="200" >-</asp:TextBox> 
                                                            </div>
                                                              
                                                    
                                                       
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label122" runat="server" class="col-form-label">Kondisi Pesaing</asp:label>
																<asp:TextBox ID="txtkondisipesaing" CssClass="form-control"  runat="server" maxlength ="200">-</asp:TextBox> 
                                                             </div>

                                                             <div class="form-group">
                                                                 <asp:label id="Label123" runat="server"  class="col-form-label">Resiko Usaha</asp:label>
                                                                 <asp:TextBox ID="txtresikousaha" CssClass="form-control"  runat="server" maxlength ="200">-</asp:TextBox> 
                                                             </div>   

                                                              <div class="form-group">
                                                                <asp:label ID="Label124" runat="server" class="col-form-label">Upaya Mengatasi Resiko Usaha</asp:label>
																<asp:TextBox ID="txtatasiresiko" CssClass="form-control"    runat="server" maxlength ="250">-</asp:TextBox>
                                                                 
                                                              </div>




													</div>

                                                             

                                                  
                                                            
                                                          

                                                            
                                                             

                                                              

                                                            
                                                            
                                                          

                                            </div>
                                        
                                    </div>


                                </div>

                                    
                            </div>

                                <%--<div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPihakLain" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPihakLain_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                        <asp:Button ID="btnNextPihakLain" align="right" class="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPihakLain_Click"     /> 
                                                   

                                                    </div>
                                            </div>--%>
                    
                    </asp:Panel>


             

                 <asp:Panel ID="pnlJaminan" Visible="false" runat="server">
                      
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                
               
                                 
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box"> 
                                                    <div class="ribbon ribbon-custom">DATA JAMINAN DAN AGUNAN</div>
                                                       <p class="m-b-0"></p>     
                                                       

                                                      
                                                                <div class="form-group">
                                                                    <asp:label ID="Label37" runat="server" Visible="false" class="col-form-label">Jenis Pinjaman</asp:label>
																    <asp:DropDownList ID="ddljenispinjaman" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" OnSelectedIndexChanged ="ddljenispinjaman_SelectedIndexChanged" ></asp:DropDownList>
                                                                </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label34" runat="server" class="col-form-label">Jenis Kredit</asp:label>
																<asp:DropDownList ID="ddlprodukpengajuan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlprodukpengajuan_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label89" runat="server" class="col-form-label">Jenis Permohonan</asp:label>
																
                                                                <asp:DropDownList ID="ddlpengajuan" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddljenispermohonan_SelectedIndexChanged">
                                                                    <asp:ListItem Value="B">Pinjaman Anggota Baru</asp:ListItem>
                                                                    <asp:ListItem Value="X">Pinjaman Anggota Lama</asp:ListItem>  
                                                                    </asp:DropDownList>
                                                                </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label145" runat="server" Visible="false" class="col-form-label">Rekening Lama</asp:label>
																<asp:DropDownList ID="ddlrekening" Visible="true" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlrekening_SelectedIndexChanged"></asp:DropDownList>
                                                                <%--<asp:DropDownList ID="txt" Visible="true" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlrekening_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                 <%--<asp:TextBox ID="ddlrekening" maxlength="30" CssClass="form-control"   runat="server" AutoPostBack="true" OnTextChanged="ddlrekening_SelectedIndexChanged"></asp:TextBox>--%>      
                                                            </div>
                                                             <div class="form-group">
                                                           <asp:label ID="Label135" runat="server" class="col-form-label">Jaminan</asp:label>
                                                                <asp:DropDownList ID="ddljaminan" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" >
                                                               </asp:DropDownList>
                                                               </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label158" runat="server" class="col-form-label">Jaminan Surat (pemisah surat gunakan simbol koma contoh: Akte Lahir ,Ijazah ,KTP )</asp:label>
																<asp:TextBox ID="txtjaminansurat"  CssClass="form-control"  runat="server" MaxLength="60" ></asp:TextBox>          
                                                                </div>
                                                          
                                                            <div class="form-group">
                                                                <asp:label ID="Label141" runat="server" class="col-form-label">Keterangan Agunan</asp:label>
																<asp:TextBox ID="txtketeranganJaminan"  CssClass="form-control"  TextMode="MultiLine" Rows="7" runat="server" MaxLength="250" ></asp:TextBox>          
                                                                </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label38" runat="server" class="col-form-label">Kondisi Agunan</asp:label>
																<asp:TextBox ID="txtkondisiJaminan" maxlength="30" CssClass="form-control"   runat="server"></asp:TextBox>          
                                                               
                                                                <%--<asp:TextBox ID="TextBox16" CssClass="form-control" maxlength ="80"  required="required" runat="server" Text="-" ></asp:TextBox>--%>
                                                             </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label131" runat="server" class="col-form-label">Taksasi</asp:label>
																<asp:TextBox ID="txttaksasi"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" required="required" runat="server">0</asp:TextBox> 

															    </div>

                                                          
															
															 
                                                         
                                                      
                                                           

                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA PENJAMIN</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                     <div class="form-group">
                                                                 <asp:label id="Label42" runat="server"  class="col-form-label">No KTP Penjamin</asp:label>
                                                                 <asp:TextBox ID="txtktppenjamin" maxlength ="16"  CssClass="form-control" onkeypress="return isnumerickey(event);"   required="required" runat="server" Text="0"  ></asp:TextBox>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" SetFocusOnError="True" ControlToValidate="txtktppenjamin"  ErrorMessage="KTP Penjamin tidak boleh kosong" Text="-"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                           
                                                            </div>  

                                                            <div class="form-group">
                                                                <asp:label ID="Label48" runat="server" class="col-form-label">Nama Penjamin</asp:label>
																<asp:TextBox ID="txtnamapenjamin" CssClass="form-control" maxlength ="70"  required="required" runat="server" Text="-" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapenjamin"  ErrorMessage="Nama Penjamin tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                          
                                                             
                                                            </div>                                                          

                                            </div>
                                        </div>

                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA TAMBAHAN</div>
                                                 <p class="text-muted font-14 m-b-30"></p>

                                                
                                                            <div class="form-group">
                                                                <asp:label ID="Label7" runat="server" class="col-form-label">Pekerjaan</asp:label>
																<asp:DropDownList ID="ddlPekerjaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                            </div>
                                                             <div class="form-group">
                                                                <asp:label ID="Label93" runat="server" class="col-form-label">Nama Ibu Kandung</asp:label>
                                                                 <asp:TextBox ID="txtnamaibukandung" MaxLength="70" CssClass="form-control"  runat="server" ></asp:TextBox> 
                                                             <asp:RequiredFieldValidator id="RequiredFieldValidator34" runat="server" SetFocusOnError="True" ControlToValidate="txtnamaibukandung"  ErrorMessage="Nama ibu kandung tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                          
                                                            </div>  
                                                                      <div class="form-row">
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label111" runat="server" class="col-form-label">Nomer NPWP</asp:label>
																 <asp:TextBox ID="txtNPWP" CssClass="form-control" MaxLength="15"  runat="server" Width="219px" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                 <%--<asp:RequiredFieldValidator id="RequiredFieldValidator32" runat="server" SetFocusOnError="True" ControlToValidate="txtNPWP"  ErrorMessage="NPWP tidak boleh kosong (isi '0' jika tidak memiliki NPWP)"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            
                                                            </div> 
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label112" runat="server" class="col-form-label">No.Kartu Keluarga</asp:label>
																<asp:TextBox ID="txtnomerkk" CssClass="form-control"  MaxLength="16"  runat="server" Width="219px" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator33" runat="server" SetFocusOnError="True" ControlToValidate="txtnomerkk"  ErrorMessage="No.KK tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 
                                                         </div> 
                                                        
                                                             <div class="form-group">
                                                                <asp:label ID="Label30" visible="false" runat="server" class="col-form-label">No. Rekening Lama</asp:label>
																<asp:TextBox ID="txtnoreklama" visible="false" CssClass="form-control" required="required" runat="server" maxlength ="40" Text="-"></asp:TextBox> 
															 </div>
                                                                                                          

                                            </div>
                                        </div>
                                    </div>
                                 

                                </div>

                                    
                            </div>

                             
                    
                    </asp:Panel>
            
             
            <asp:Panel ID="pnlPengajuan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENGAJUAN KREDIT ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                       <asp:HiddenField ID="HDJenisPinjaman" runat="server"/> 
                                                        <div class="form-group col-md-6">
                                                           
															
															

                                                            
                                                             
                                                            <div class="form-group">
                                                                 <asp:label id="Label35" runat="server" class="col-form-label">Tanggal Realisasi</asp:label>
                                                                 <asp:TextBox CssClass="form-control" ID="txttglrealisasi" runat="server" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                                 <%--<asp:TextBox CssClass="form-control" ID="txttglrealisasi" type="Date" runat="server" AutoPostBack="true"  OnTextChanged="txttglrealisasi_TextChanged"></asp:TextBox>--%>
                                                            
                                                            </div>   

                                                            
                                                             <div class="form-group">
                                                                <label runat="server" class="col-form-label">Jenis Tagihan</label>
                                                                <%--<asp:DropDownList ID="ddlbycad" Required="False" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlbycad_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                <asp:DropDownList ID="ddljenistagihan" Enabled="false" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddljangka_SelectedIndexChanged" >
                                                                <asp:ListItem Value="0">System Lama</asp:ListItem>
                                                                <asp:ListItem Value="1">Tagihan Dengan Collection</asp:ListItem>                                                               
                                                                <asp:ListItem Value="2">Bayar Di Cabang</asp:ListItem>    
                                                                </asp:DropDownList>
                                                            
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label55" runat="server" Visible="false" class="col-form-label">Program Pinjaman</asp:label>
																<asp:DropDownList ID="ddlprogrampinjaman" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlprogrampinjaman_SelectedIndexChanged"></asp:DropDownList>
                                                             </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label56" runat="server" visible="false" class="col-form-label">Sumber Dana</asp:label>
																<asp:DropDownList ID="ddlsumberdana" visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>
                                                            <div class="form-group">
                                                            <asp:label ID="Label142" runat="server" class="col-form-label">Plafon</asp:label>
                                                            </div>
                                                            <div class="form-row">
                                                                
															<div class="form-group col-md-6">
                                                             <asp:TextBox CssClass="form-control" ID="txtplafon" Text="750000" runat="server" readonly="true" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>
                                                            </div> 
                                                             <div class="form-group col-md-3">
                                                                <asp:Button ID="btntambah" Text="Tambah" runat="server" OnClick="tambahplafon"/>
                                                                 <asp:Button ID="btnminus" Text="Kurang" runat="server" OnClick="kurangplafon"/>
                                                            </div> 
                                                           
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label39" runat="server" class="col-form-label">Jangka Waktu</asp:label>
                                                                <asp:DropDownList ID="ddljangka" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddljangka_SelectedIndexChanged"></asp:DropDownList>
	                                                            <%--<asp:TextBox CssClass="form-control" ID="txtjangka"  runat="server" required="required" AutoPostBack="true"  OnTextChanged="txtjangka_TextChanged"></asp:TextBox>--%>
                                                            </div> 


                                                            <div class="form-row">
                                                                
															


                                                               <div class="form-group col-md-3">
                                                                <asp:label ID="Label40" runat="server" class="col-form-label">Bunga</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbunga" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 

                                                                <div class="form-group col-md-3">
                                                                <asp:label ID="Label41" runat="server" class="col-form-label">Persen Provisi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtProvisi" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 
                                                                  <div class="form-group col-md-3">
                                                                    <asp:label ID="Label57" runat="server" class="col-form-label">Nominal Provisi</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtnominalprovisi" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 
                                                            
                                                                 <div class="form-group col-md-3">
                                                                    <asp:label ID="Label60" runat="server" class="col-form-label">Angsuran</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtangsuranperbulan" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 



                                                            </div>


                                                           

                                                            

                                                           

                                                             
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label61" Visible="false" runat="server" class="col-form-label">Admin Pencairan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Visible="false" ID="txtadminPencairan" Enabled="false"  runat="server" >0</asp:TextBox>
                                                            </div> 
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label62" runat="server" Visible="false" class="col-form-label">Admin Angsuran</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Visible="false" ID="txtadminangsuran" Enabled="false"  runat="server" >0</asp:TextBox>
                                                            </div> 
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label63" Visible="false" runat="server" class="col-form-label">Sifat</asp:label>
																<asp:DropDownList ID="ddlSifat" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>
                                                             <div class="form-group">
                                                                <asp:label ID="Label65" runat="server" Visible="false" class="col-form-label">Ref Non AO</asp:label>
																<asp:DropDownList ID="ddlnonao" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>
                                                             <div class="form-group">
                                                                <asp:label ID="Label66" runat="server" Visible="false" class="col-form-label">Sektor Ekonomi</asp:label>
																<asp:DropDownList ID="ddlsektorekonomi" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>

                                                         <div class="form-group" runat="server" id="discountmingguan" Visible ="false">
                                                        <asp:label ID="Label157" runat="server" class="col-form-label">Discount Angsuran</asp:label>
                                                       
                                                             <asp:TextBox ID="txtdiscountangsuran" Enabled="false" CssClass="form-control"    onkeypress="return isnumerickey(event);" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);" AutoPostBack="true"  OnTextChanged="txtdiscountangsuran_TextChanged">0</asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                        <asp:label ID="Label118" runat="server" class="col-form-label">Saldo Simpanan Pokok Anggota</asp:label>
                                                        <asp:TextBox ID="txtsimpok" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 															 
                                                        </div>
                                                        <div class="form-group">
                                                            
                                                      
                                                                <asp:label ID="Label126" runat="server" class="col-form-label">Saldo Simpanan Wajib Anggota</asp:label>
																<asp:TextBox ID="txtsimwa" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 
                                                        </div>

                                                                
															<div class="form-group">

                                                               
                                                                <label runat="server" class="col-form-label">Type Biaya Keanggotaan</label>
                                                                <%--<asp:DropDownList ID="ddlbycad" Required="False" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlbycad_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                <asp:DropDownList ID="ddlbiayaanggota" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlbiayaanggota_SelectedIndexChanged" >
                                                                <asp:ListItem Value="0">Sudah Menjadi Anggota</asp:ListItem>
                                                                <asp:ListItem  Value="1">Anggota Bayar</asp:ListItem>    
                                                                <asp:ListItem  Value="2">Cabang Bayar</asp:ListItem>
                                                                </asp:DropDownList>
                                                            
                                                            </div> 
                                                            <div class="form-group">

                                                                <asp:label ID="Label85" runat="server" class="col-form-label">Biaya Keanggotaan</asp:label>
																<asp:TextBox ID="LblAnggota" Enabled="false" CssClass="form-control"    onkeypress="return isnumerickey(event);" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                            </div>
                                                  

                                                            <div class="form-group">
                                                                <asp:label ID="Label153" runat="server" class="col-form-label">Biaya Perpanjangan Simpanan Wajib</asp:label>
																<asp:TextBox ID="lblsimpananwajib" CssClass="form-control"    onkeypress="return isnumerickey(event);" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox> 
                                                                 
                                                            </div>

                                                                <div class="form-group">
                                                                <asp:label ID="Label74" runat="server" class="col-form-label">Biaya Materai</asp:label>
																<asp:TextBox ID="txtbymaterai" enabled="false" CssClass="form-control"    onkeypress="return isnumerickey(event);" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                            </div>
                                                               <div class="form-group">
                                                                <asp:label ID="Label152" visible="true" runat="server" class="col-form-label">Info Materai</asp:label>
																<asp:TextBox ID="txtinfox" visible="true" ReadOnly="true"  CssClass="form-control"  runat="server" maxlength ="100" Text="-"></asp:TextBox> 
															 </div>

                                                           

                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                             <div class="form-row">


                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label156" visible="true" runat="server" class="col-form-label">No.Rekening Lama</asp:label>
																<asp:TextBox ID="txtcadanganRekening" visible="true" ReadOnly="true"  CssClass="form-control"  runat="server" maxlength ="100" Text="-"></asp:TextBox> 
															 </div>

                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label67" runat="server" class="col-form-label">Saldo Cadangan Sebelumnya</asp:label>
																<asp:TextBox ID="txtcadsebelumnya" ReadOnly="true" CssClass="form-control" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" AutoPostBack="true" OnTextChanged="txtkalicadangan_TextChanged">0</asp:TextBox>
                                                                 
                                                            </div> 
                                                                   <div class="form-group col-md-4">
                                                                  <asp:Button ID="btncekcadangan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="Cek Cadangan" OnClick="btnCekcadangan_Click" />
                                                                  
                                                            </div> 
                                                          

                                                             </div>
                                                           
                                                            <div class="form-group">
                                                                <label runat="server" class="col-form-label">Jenis Biaya Cadangan</label>
                                                                <%--<asp:DropDownList ID="ddlbycad" Required="False" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlbycad_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                <asp:DropDownList ID="ddlbycad" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlbycad_SelectedIndexChanged" >
                                                                <asp:ListItem Value="0">Tanpa Cadangan</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="1">Dengan Cadangan</asp:ListItem>   
                                                                 <%--<asp:ListItem Selected="True" Value="2">Dengan Cadangan Tanpa Potong Cadangan Sebelumnya</asp:ListItem>--%>  
                                                                </asp:DropDownList>
                                                            
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label70" runat="server" class="col-form-label">Biaya Cadangan</asp:label>
                                                                  <div class="form-row">
                                                                    <div class="form-group col-md-6">
                                                                         <asp:TextBox ID="txtkalicadangan" MaxLength="2" CssClass="form-control"  runat="server" onkeypress="return isnumerickey(event);" AutoPostBack="true"  OnTextChanged="txtkalicadangan_TextChanged" ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-5">
                                                                        <asp:TextBox ID="TextBox2" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                                    </div>
                                                                </div> 
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label88" runat="server" class="col-form-label">Biaya Cadangan yang harus di bayarkan</asp:label>
																<asp:TextBox ID="TextBox3" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" MaxLength="18"   runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                  
                                                            </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label146" runat="server" class="col-form-label">Biaya Cadangan yang harus di kembalikan</asp:label>
																<asp:TextBox ID="txtkembalicadangan" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" MaxLength="18"   runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                  
                                                            </div> 
                                                          


                                                            
                                                              

                                                           
                                                            <div class="form-group" runat="server" visible="false">
                                                                <label runat="server" class="col-form-label">Uang Muka</label>                                                                
                                                                <asp:DropDownList ID="ddluangmuka" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddluangmuka_SelectedIndexChanged" >
                                                                <asp:ListItem Value="0">Tanpa Uang Muka</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="1">Dengan Uang Muka</asp:ListItem>                                                               
                                                                </asp:DropDownList>
                                                            
                                                            </div> 

                                                            <div class="form-group" runat="server" visible="false">
                                                                <asp:label ID="Label143" runat="server" class="col-form-label">Biaya Uang Muka</asp:label>
                                                                  <div class="form-row">
                                                                    <div class="form-group col-md-6">
                                                                         <asp:TextBox ID="txtkaliuangmuka" MaxLength="2" CssClass="form-control"  runat="server" onkeypress="return isnumerickey(event);" AutoPostBack="true"  OnTextChanged="txtkaliuangmuka_TextChanged" ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-5">
                                                                        <asp:TextBox ID="txtpokokuangmuka" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                                    </div>
                                                                </div> 
                                                            </div> 

                                                             <div class="form-group" runat="server" visible="false">
                                                                <asp:label ID="Label144" runat="server" class="col-form-label">Biaya uang muka yang harus di bayarkan</asp:label>
																<asp:TextBox ID="txtuangmuka" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" MaxLength="18"   runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                  
                                                            </div> 


                                                             <div class="form-group">
                                                                <asp:label ID="Label71" visible="false" runat="server" class="col-form-label">Nilai Taksasi</asp:label>
																<asp:TextBox ID="TextBox5" visible="false" CssClass="form-control"  MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"   >0</asp:TextBox>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label72" runat="server" class="col-form-label">Jatuh Tempo</asp:label>
																<asp:TextBox ID="txtjttempo" CssClass="form-control"  Enabled="false"    runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label73" runat="server" visible="false" class="col-form-label">Angsuran Dimuka</asp:label>
																<asp:TextBox ID="txtangsurandimuka" visible="false" CssClass="form-control"   onkeypress="return isnumerickey(event);" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                            </div>  

                                                          
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label64" runat="server" class="col-form-label">Staff Account Office (AO)</asp:label>
																<asp:DropDownList ID="ddlAO" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                 <asp:RequiredFieldValidator id="reqFavoriteColor" ErrorMessage="AO tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlAO" Runat="server" />

                                                             </div>

                                                          
                                                            <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <asp:DropDownList ID="ddlharitagih" visible="false" runat="server" CssClass="form-control" data-style="btn-custom" >
                                                                <asp:ListItem Value="Senin">Senin</asp:ListItem>
                                                                <asp:ListItem Value="Selasa">Selasa</asp:ListItem>
                                                                <asp:ListItem Value="Rabu">Rabu</asp:ListItem>
                                                                <asp:ListItem Value="Kamis">Kamis</asp:ListItem>
                                                                <asp:ListItem Value="Jumat">Jumat</asp:ListItem>
                                                                <asp:ListItem Value="Sabtu">Sabtu</asp:ListItem>
                                                                <asp:ListItem Value="Minggu">Minggu</asp:ListItem>                                                               
                                                                </asp:DropDownList>
                                                                <asp:label ID="Label103" runat="server" class="col-form-label">Maju Hari Tagih</asp:label>
                                                                <asp:DropDownList ID="ddlmajuhari" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlmajuhari_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">0 Hari</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="1" >1 Hari</asp:ListItem>
                                                                <asp:ListItem Value="2">2 Hari</asp:ListItem>
                                                                <asp:ListItem Value="3">3 Hari</asp:ListItem>
                                                                <asp:ListItem Value="4">4 Hari</asp:ListItem>
                                                                <asp:ListItem Value="5">5 Hari</asp:ListItem>
                                                                <asp:ListItem Value="6">6 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="7">7 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="8">8 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="9">9 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="10">10 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="11">11 Hari</asp:ListItem>
                                                                <asp:ListItem Value="12">12 Hari</asp:ListItem>
                                                                <asp:ListItem Value="13">13 Hari</asp:ListItem>
                                                                <asp:ListItem Value="14">14 Hari</asp:ListItem>
                                                                <asp:ListItem Value="15">15 Hari</asp:ListItem>
                                                                <asp:ListItem Value="16">16 Hari</asp:ListItem>
                                                                <asp:ListItem Value="17">17 Hari</asp:ListItem>
                                                                <asp:ListItem Value="18">18 Hari</asp:ListItem>
                                                                <asp:ListItem Value="19">19 Hari</asp:ListItem>
                                                                <asp:ListItem Value="20">20 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="21">21 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="22">22 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="23">23 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="24">24 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="25">25 Hari</asp:ListItem>
                                                                <asp:ListItem Value="26">26 Hari</asp:ListItem>
                                                                <asp:ListItem Value="27">27 Hari</asp:ListItem>
                                                                <asp:ListItem Value="28">28 Hari</asp:ListItem>
                                                                <asp:ListItem Value="29">29 Hari</asp:ListItem>
                                                                <asp:ListItem Value="30">30 Hari</asp:ListItem>
                                                                </asp:DropDownList>
                                                                 

                                                             </div>

                                                              <div class="form-group col-md-6">
                                                                 <asp:label id="Label155" runat="server" class="col-form-label">Tanggal Tagih</asp:label>
                                                                 <asp:TextBox CssClass="form-control" ID="txttgltagih" enabled="false" runat="server" ></asp:TextBox>
                                                                 <%--<asp:TextBox CssClass="form-control" ID="txttglrealisasi" type="Date" runat="server" AutoPostBack="true"  OnTextChanged="txttglrealisasi_TextChanged"></asp:TextBox>--%>
                                                            
                                                            </div>   
                                                            </div>   

                                                             <div class="form-group">
                                                                <asp:label ID="Label75" visible="false" runat="server" class="col-form-label">Biaya Asuransi</asp:label>
																<asp:TextBox ID="txtbyasuransi" visible="false" CssClass="form-control"    onkeypress="return isnumerickey(event);" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label76"  visible="false" runat="server" class="col-form-label">Biaya Pelunasan</asp:label>
																<asp:TextBox ID="txtbypelunasan" visible="false" CssClass="form-control"    onkeypress="return isnumerickey(event);" MaxLength="18"   runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                            </div> 

                                                              <div class="form-group">
                                                                <asp:label ID="Label77" runat="server" visible="false"  class="col-form-label">Biaya Notaris</asp:label>
																<asp:TextBox ID="txtbynotaris" visible="false" CssClass="form-control"    onkeypress="return isnumerickey(event);" MaxLength="18"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                            </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label78"  visible="false" runat="server" class="col-form-label">Blokir Angs</asp:label>
																<asp:TextBox ID="TextBox14" visible="false" CssClass="form-control"   onkeypress="return isnumerickey(event);" MaxLength="18"   runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label79" runat="server" visible="false" class="col-form-label">Nominal Blokir Angs</asp:label>
																<asp:TextBox ID="TextBox15"  visible="false" CssClass="form-control"    onkeypress="return isnumerickey(event);" MaxLength="18"   runat="server"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);">0</asp:TextBox>
                                                                 
                                                            </div> 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label80" runat="server" visible="false" class="col-form-label">Jenis Blokir</asp:label>
																<asp:DropDownList ID="ddljenisblokir" visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>

                                                       

                                                        </div>


													</div>
												</div>  

                                            </div>    
                  
                                        <div class="hidden-print mt-4">
                                             <div class="row">
                                   
                                    <div class="button-list">
                                        
                                    <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" type="button" Text="BACK" OnClick="btnCancel_Click" />
                                    <asp:Button ID="BtnApproveAntrian"  CssClass="btn btn-success waves-light text-white" type="button" runat="server" Text="Approve Antrian" OnClick="BtnApproveAntrian_Click"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Approve Antrian data ini ?');"/>
                                    <asp:Button ID="BtnBatalkanmohon"  CssClass="btn btn-success waves-light text-white" type="button" runat="server" Text="Batalkan Permohonan" OnClick="btnTolak_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Batalkan Permohonan ini ?');"/>
                                    <asp:Button ID="btnSimpan" CssClass="btn btn-success waves-light text-white"  type="button" runat="server" Text="SAVE" OnClick="btnSimpan_Click"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');"/>
                                    <asp:Button ID="btnlanjutkan" CssClass="btn btn-success waves-light text-white"  type="button" runat="server" Text="LANJUTKAN KE TELLER" OnClick="btnlanjutkan_Click"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Lanjutkan Ke Teller ?');"/>
                                    <asp:Button ID="btnTolakAntrian"  class="btn btn-success waves-light text-white" type="button" runat="server" Text="BATALKAN ANTRIAN" OnClick="btnbatalkanantrian_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Batalkan Antrian data ini ?');"/>
                                    </div>
                                    </div> 




                                       

                                            </div>    
    
                </span></span> 
    
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


<asp:Panel ID="Panel1" Visible="false" runat="server">
                <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENDAPATAN ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              
                                                             <div class="form-group">
                                                                <asp:label ID="Label53" runat="server" class="col-form-label">Pendapatan Bersih Usaha</asp:label>
																<asp:TextBox ID="TextBox1" Required="required" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" runat="server" MaxLength="16" AutoPostBack="true"  OnTextChanged="txtpendapatanbersih_TextChanged" >0</asp:TextBox>  
                                                   
                                                             </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label69" runat="server" class="col-form-label">Pendapatan Selain Usaha</asp:label>
																<asp:TextBox ID="TextBox4"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"   required="required" runat="server" AutoPostBack="true"  OnTextChanged="txtpendapatanselain_TextChanged" >0</asp:TextBox> 

															</div>

                                                            <div class="form-group">
                                                                 <asp:label id="Label133" runat="server" class="col-form-label">Total Pendapatan</asp:label>
                                                                 <asp:TextBox ID="TextBox6" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"   required="required" runat="server" Text="0"  ></asp:TextBox> 
                                                            </div>   
                                                             
                                                     
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                 <asp:label id="Label134" runat="server" class="col-form-label">Pengeluaran Rumah Tangga</asp:label>
                                                                 <asp:TextBox ID="TextBox8" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server"  AutoPostBack="true"  OnTextChanged="txtpengeluaranrumahtangga_TextChanged">0</asp:TextBox>   
                                                            
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label136" runat="server" class="col-form-label">Tagihan PLN, Listrik, PDAM, Telepon</asp:label>
																<asp:TextBox ID="TextBox9" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  OnTextChanged="txttagihanlistrik_TextChanged">0</asp:TextBox>      
                                                           </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label137" runat="server" class="col-form-label">Pinjaman / Kewajiban Lainnya</asp:label>
																<asp:TextBox ID="TextBox10" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  OnTextChanged="txtpinjamanlain_TextChanged">0</asp:TextBox>      
                                                           </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label138" runat="server" class="col-form-label">Rekomendasi Angsuran</asp:label>
																<asp:TextBox ID="TextBox11" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"  AutoPostBack="true"  OnTextChanged="txtangsuran_TextChanged" >0</asp:TextBox>      
                                                           </div>

                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="Label139" runat="server" class="col-form-label"></asp:label>
																<asp:TextBox ID="TextBox12" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" Text="0" ></asp:TextBox>      
                                                           </div>
                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="Label140" runat="server" class="col-form-label"></asp:label>
																<asp:TextBox ID="TextBox13" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" Text="0"></asp:TextBox>      
                                                                
                                                           </div>

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="Button5" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPendapatan_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="Button6" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPendapatan_Click" Width="65px" />

                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel>


             

            <asp:Panel ID="PnlUsaha" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENDAPATAN / SUMBER DANA ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label82" runat="server" class="col-form-label">Penghasilan Gaji Kotor Per Tahun</asp:label>
																<asp:DropDownList ID="ddlgajikotor" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>
															
															 <div class="form-group">
                                                                <asp:label ID="Label83" runat="server" class="col-form-label">Sumber Penghasilan</asp:label>
																<asp:DropDownList ID="ddlsumberpenghasilan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlsumberpenghasilan_SelectedIndexChanged"  ></asp:DropDownList>
                                                                <asp:TextBox ID="txtsumberpenghasilan" MaxLength="50" CssClass="form-control" runat="server"   ></asp:TextBox>
                                                                
                                                             </div>
                                                          </div>
                                                        <div class="form-group col-md-6">
                                                              <div class="form-group">
                                                                <asp:label ID="Label84" runat="server" class="col-form-label">Transaksi Per Hari</asp:label>
																<asp:DropDownList ID="ddltransaksiperhari" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>

                                                         
                                                         
                                                        </div>  


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnCancels" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 

                                                     <asp:Button ID="Button2" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPendapatan_Click" Width="65px" />
                                                   

                                                    </div>
                                            </div>  
    
            </asp:Panel> 
                 
            
                 

            </div> 
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>  
                <asp:PostBackTrigger ControlID="btnSearch" />
                   <asp:PostBackTrigger ControlID="BtnTarik" />
                  <%--<asp:PostBackTrigger ControlID="btncekcadangan" />--%>
                 
             <asp:PostBackTrigger ControlID="GRData" />
                 
                 
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                 <asp:PostBackTrigger ControlID="ddlstatusrumah" />
                 <asp:PostBackTrigger ControlID="ddlstatuskawinupload" />
    
                 
                 
                 <%--<asp:AsyncPostBackTrigger ControlID="ddlprogrampinjaman" />--%>
                <asp:PostBackTrigger ControlID="ddlstatus" />
<%--                 <asp:PostBackTrigger ControlID="ddljangka"/>
                 <asp:PostBackTrigger ControlID="ddlbycad"/>--%>
                 <%--<asp:AsyncPostBackTrigger ControlID="ddlrekening" EventName="SelectedIndexChanged" />--%>
                 
                  <asp:AsyncPostBackTrigger ControlID="ddluangmuka" EventName="SelectedIndexChanged" />
                  <asp:AsyncPostBackTrigger ControlID="ddljangka" EventName="SelectedIndexChanged" />
                 <asp:AsyncPostBackTrigger ControlID="ddlbycad" EventName="SelectedIndexChanged" />
                  <asp:AsyncPostBackTrigger ControlID="btntambah" /> 
                 <asp:AsyncPostBackTrigger ControlID="btnminus" /> 


                  <%--<asp:PostBackTrigger ControlID="ddljenistagihan" />--%>
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
