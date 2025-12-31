<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermohonanPinjamanKresun.aspx.cs" Inherits="AR_Process_PermohonanPinjamanKreditMultiguna" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Permohonan Pinjaman (Registrasi Pensiun)</title>
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
         function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: t,
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 2000,
                    stack: 1,
                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
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
         function checkTextAreaMaxLength(textBox,e, length)
        {

                var mLen = textBox["MaxLength"];
                if(null==mLen)
                    mLen=length;

                var maxLength = parseInt(mLen);
                if(!checkSpecialKeys(e))
                {
                 if(textBox.value.length > maxLength-1)
                 {
                    if(window.event)//IE
                      e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                 }
            }   
        }
        function checkSpecialKeys(e)
        {
            if(e.keyCode !=8 && e.keyCode!=46 && e.keyCode!=37 && e.keyCode!=38 && e.keyCode!=39 && e.keyCode!=40)
                return false;
            else
                return true;
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
         
        function CheckFileExistenceNPWP() {

            var filePath = document.getElementById('<%= this.fileuploadSK.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto SK belum di pilih!");


                return false;
            }
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
        $(document).ready(InIEvent);
        function InIEvent() { 

            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            
            $('#txttglptkp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

             $('#txtkota').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            // $('#txtrealisasi').datepicker({
            //    format: 'dd-MM-yyyy',
            //    autoclose: true,
            //    todayHighlight: true
            //});
            
             $('#txttempatlahir').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

             $('#txttgllahir').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

             $('#txtanak2').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

             $('#txtanak3').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

             $('#TextBox32').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

             $('#TextBox1').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

             $('#TextBox28').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            //  $('#txttglrealisasi').datepicker({
            //    format: 'dd-MM-yyyy',
            //    autoclose: true,
            //    todayHighlight: true
            //});

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
                                url: "PermohonanPinjamanKresun.aspx/GetNoNasabah",
                                data: "{'InputText':'" + document.getElementById('txtnonasabah').value + "','SessionBranchID':'<%=SessBranchID%>'}",
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
                                    <li class="breadcrumb-item"><a href="PermohonanPinjamanKresun.aspx">Permohonan Pinjaman (Registrasi Pensiun)</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Permohonan Pinjaman (Registrasi Pensiun)</h4>
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
                                                                    <label class="col-form-label">Kode Cabang</label>
                                                                    <asp:TextBox CssClass="form-control" ID="txtkodecabang"  runat="server" Enabled="false" ></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnZipCode" runat="server" />
                                                                    <asp:HiddenField ID="HDnobukti" runat="server" />

                                                                     <div class="form-group">
                                                                         <asp:label ID="lblnonasabah" runat="server" class="col-form-label">No. Nasabah</asp:label>
																
                                                                         <asp:TextBox CssClass="form-control" ID="txtnonasabah"   MaxLength = "200" runat="server"></asp:TextBox>

                                                                         <asp:label ID="lblnobukti" runat="server" class="col-form-label" visible="false">No Bukti Pelunasan</asp:label> 
                                                                        <%--<asp:DropDownList ID="ddljenispermohonan"   CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  ></asp:DropDownList>--%>
                                                                        <asp:DropDownList ID="ddlnobukti" visible="false" runat="server" CssClass="form-control" data-style="btn-custom" ></asp:DropDownList> 
                                                                    
                                                                   </div>  

                                                                     
                                                                </div>
                                                                   <div class="form-group col-md-6">
                                                                    <div class="form-group">
                                                                        <label runat="server" class="col-form-label">Jenis Permohonan Kredit</label>
                                                                        <%--<asp:DropDownList ID="ddljenispermohonan"   CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  ></asp:DropDownList>--%>
                                                                        <asp:DropDownList ID="ddljenispermohonan" runat="server" CssClass="form-control" data-style="btn-custom" >
                                                                        <asp:ListItem Value="0">Permohonan Normal</asp:ListItem>
                                                                        <asp:ListItem Value="1">Permohonan Deviasi</asp:ListItem>
                                                                        <asp:ListItem Value="2">Permohonan Take Over/Dantal</asp:ListItem>
                                                                        <asp:ListItem Value="3">Permohonan Deviasi & Dantal</asp:ListItem>                                                                    
                                                                        </asp:DropDownList> 
                                                                    
                                                                    </div>

                                                                     
                                                                    <div class="form-group">
                                                                        <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search"  OnClick="btnSearch_Click"/>                                              
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
              
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront"   runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Status Transaksi</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                     <asp:GridView ID="GRData" runat="server"  CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"  ShowHeaderWhenEmpty="True"   AutoGenerateColumns="False" Width="887px">
                                    <Columns> 
                                         <asp:TemplateField HeaderText="ID Permohonan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "idpermohonan")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="No Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkodeCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField HeaderText="Nama Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
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
                                       
<%--                                        <asp:TemplateField HeaderText="Tanggal Pengajuan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tanggal_pengajuan")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
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
                 <!-- Panel Edit -->
             <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENSIUN</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                        <asp:HiddenField ID="HDStatusDebitur" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodePos" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodeArea" runat="server" />
                                                        <asp:HiddenField ID="HDGender" runat="server" />
                                                        <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                        <asp:HiddenField ID="HDNoSimulasi" runat="server" />
                                                        <asp:HiddenField ID="HDNoMemo" runat="server" />
                                                        <asp:HiddenField ID="HDStatusTO" runat="server" />
                                                        <asp:HiddenField ID="HDNominal" runat="server" />
                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="Label30" runat="server" class="col-form-label">No. Memo Internal</asp:label>
																<asp:DropDownList ID="ddlMemo"   CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlMemo_SelectedIndexChanged"></asp:DropDownList>
                                                                
                                                           </div> 

                                                           <div class="form-group">
                                                                <asp:label ID="Label6" runat="server" class="col-form-label">Status Pensiun</asp:label>
																<asp:DropDownList ID="ddlstatupensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >
                                                                    <asp:ListItem Value="0">Sendiri</asp:ListItem>
                                                                    <asp:ListItem Value="1">Janda</asp:ListItem>
                                                                    <asp:ListItem Value="2">Duda</asp:ListItem>
																</asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator20" ErrorMessage="Status Pensiun tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlstatupensiun" Runat="server" />

                                                           </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label46" runat="server" class="col-form-label">Tanggal Pensiun / TMT</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkota"   TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                  <%--<asp:RequiredFieldValidator id="RequiredFieldValidator11" runat="server" SetFocusOnError="True" ControlToValidate="txtkota"  ErrorMessage="Tanggal Pensiun tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            
                                                                  
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label59" runat="server" class="col-form-label">No. KTP</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtktp"  MaxLength="16" onkeypress="return isnumerickey(event);"  Enabled="false" runat="server"  ></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator28" runat="server" SetFocusOnError="True" ControlToValidate="txtktp"  ErrorMessage="No. KTP tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            
                                                            </div>
                                                           
                                                            <div class="form-group">
                                                                <asp:label ID="Label44" runat="server" class="col-form-label">Nama Pensiun</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamanasabah" Enabled="false"  runat="server" ></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator48" runat="server" SetFocusOnError="True" ControlToValidate="txtnamanasabah"  ErrorMessage="Nama tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label43" runat="server" class="col-form-label">NIP</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnip"  MaxLength="16" onkeypress="return isnumerickey(event);"  runat="server" Enabled="false"  ></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator17" runat="server" SetFocusOnError="True" ControlToValidate="txtnip"  ErrorMessage="NIP tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Nopen</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnopen"  MaxLength="16" onkeypress="return isnumerickey(event);"  runat="server"   Enabled="false"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator49" runat="server" SetFocusOnError="True" ControlToValidate="txtnopen"  ErrorMessage="Nopen tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            
                                                            </div> 

                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label54" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:DropDownList ID="ddljeniskelamin" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >

                                                                <asp:ListItem Value="0">Pria</asp:ListItem>
                                                                <asp:ListItem Value="1">Wanita</asp:ListItem>
																</asp:DropDownList>
                                                            
                                                           </div>
                                                           <div class="form-group">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Pangkat / Golongan</asp:label>
																<asp:DropDownList ID="ddlgolongan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator50" ErrorMessage="Golongan tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlgolongan" Runat="server" />--%>

                                                           </div>

															
 
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                           <div class="form-group">
                                                                <asp:label id="Label22" runat="server"  class="col-form-label">Status Pernikahan</asp:label>
                                                                <asp:DropDownList ID="ddlstatusnikah" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusnikah_SelectedIndexChanged" >
                                                                <asp:ListItem Value="0">Tidak Kawin</asp:ListItem>
                                                                <asp:ListItem Value="1">Kawin</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator4"  ControlToValidate="ddlstatusnikah" ErrorMessage="Status pernikahan tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" Runat="server" />--%>

                                                            </div> 
                                                            

                                                            <div class="form-group">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label">Kode Jiwa</asp:label>
																<asp:DropDownList ID="ddlkodejiwa" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkodejiwa_SelectedIndexChanged"></asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator51"  ControlToValidate="ddlkodejiwa" ErrorMessage="Kode Jiwa tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" Runat="server" />--%>

                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label45" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txttgllahirpensiun" Enabled="false" type="Date"  runat="server" ></asp:TextBox>
                                                                
                                                            
                                                            </div> 

                                                            

                                                            <div class="form-group">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Tanggal Menikah</asp:label>
																<asp:TextBox ID="txttempatlahir" CssClass="form-control" TextMode="DateTime"  runat="server" ></asp:TextBox>
															</div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">Tanggal Lahir anak ke 1</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txttgllahir"  TextMode="DateTime" runat="server"></asp:TextBox> 
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator12" runat="server" SetFocusOnError="True" ControlToValidate="txttgllahir"  ErrorMessage="Tanggal Lahir anak ke 1 tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            
                                                            </div> 
                                                          
                                                              <div class="form-group">
                                                                <asp:label ID="Label11" runat="server" class="col-form-label">Tanggal Lahir anak ke 2</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txtanak2"   TextMode="DateTime" runat="server"></asp:TextBox> 
                                                                  <%--<asp:RequiredFieldValidator id="RequiredFieldValidator18" runat="server" SetFocusOnError="True" ControlToValidate="txtanak2"  ErrorMessage="Tanggal Lahir anak ke 2 tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            </div> 
                                                          
                                                           

                                                               <div class="form-group">
                                                                <asp:label ID="Label86" runat="server" class="col-form-label">Tanggal Lahir anak ke 3</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txtanak3"   TextMode="DateTime" runat="server"></asp:TextBox> 
                                                                    <%--<asp:RequiredFieldValidator id="RequiredFieldValidator19" runat="server" SetFocusOnError="True" ControlToValidate="txtanak3"  ErrorMessage="Tanggal Lahir anak ke 3 tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                           
                                                            </div> 
                                                          
                                                         


                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                   
            </asp:Panel> 

            
                 <!-- Panel Edit -->
             <asp:Panel ID="pnlSK" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA SK PENSIUN</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                        <asp:HiddenField ID="HDKodeJiwa" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodeProduk" runat="server" />	 
                                                        <asp:HiddenField ID="HDJenisPensiun" runat="server" />
                                                        <asp:HiddenField ID="HDSumberDana" runat="server" />
                                                        <asp:HiddenField ID="HDBunga" runat="server" />
                                                        <asp:HiddenField ID="HDTglJt" runat="server" />
                                                        <asp:HiddenField ID="HDMemo" runat="server" />
                                                        <asp:HiddenField ID="HDAO" runat="server" />
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label99" runat="server" class="col-form-label">NO SK</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnosk" text="0" MaxLength="50"  runat="server"  ></asp:TextBox>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" SetFocusOnError="True" ControlToValidate="txtnosk"  ErrorMessage="No. SK Pensiun tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>


                                                            <div class="form-group">
                                                                <asp:label ID="Label100" runat="server" class="col-form-label">Nama Sesuai SK</asp:label>
																<asp:TextBox CssClass="form-control" ID="TextBox31" text="-" MaxLength="100"  runat="server"  ></asp:TextBox>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" SetFocusOnError="True" ControlToValidate="TextBox31"  ErrorMessage="Nama tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label101" runat="server" class="col-form-label">Tanggal SK Pensiun</asp:label>
                                                                <asp:TextBox CssClass="form-control" ID="TextBox32"   TextMode="DateTime" runat="server"></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" SetFocusOnError="True" ControlToValidate="TextBox32"  ErrorMessage="Nama tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                        
                                                           
                                                      
                                                        </div> 
                                                        
                                                        <div class="form-group col-md-6">
                                                           <div class="form-group">
                                                                <asp:label ID="Label102" runat="server" class="col-form-label">No. Dapem</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnodapem" text="0" MaxLength="50"  runat="server" ></asp:TextBox>
                                                               <asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" SetFocusOnError="True" ControlToValidate="txtnodapem"  ErrorMessage="No. Dapem tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label106" runat="server" class="col-form-label">Jenis Pensiun</asp:label>
																<asp:DropDownList ID="ddljenispensiunsk" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator1" ErrorMessage="Jenis Pensiun tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddljenispensiunsk" Runat="server" />

                                                           </div> 
                                                         
                                                         
                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                           
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnCancel" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />
                                                        


                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNext" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext_Click" Width="65px" />


                                                    </div>
                                                
                                            </div> 
                                               
    
            </asp:Panel> 
                 

            <asp:Panel ID="pnlPekerjaan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENERIMA PENSIUN</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              <div class="form-group">
                                                                <asp:label ID="Label97" runat="server" class="col-form-label">Penerima Pensiun</asp:label>
																<asp:DropDownList ID="ddlpenerimapensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  >
                                                                     <asp:ListItem Value="0">Pensiunan</asp:ListItem>
                                                                        <asp:ListItem Value="1">Bukan Pensiunan</asp:ListItem>

																</asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator21" ControlToValidate="ddlpenerimapensiun"  ErrorMessage="Penerima Pensiun tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" Runat="server" />

                                                            </div>

                                                          
															
															<div class="form-group">
                                                                <asp:label ID="Label8" runat="server" class="col-form-label">Nama</asp:label>
																<asp:TextBox ID="txtnamapenerima" text="-" CssClass="form-control"  MaxLength="100"   runat="server" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapenerima"  ErrorMessage="Nama tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
															</div>
                                                             
                                                            <div class="form-group">
                                                                 <asp:label id="Label47" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="TextBox1" TextMode="DateTime"  runat="server" ></asp:TextBox>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator22" runat="server" SetFocusOnError="True" ControlToValidate="TextBox1"  ErrorMessage="Tanggal Lahir tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label12" runat="server" class="col-form-label">Status Perkawinan</asp:label>
																<asp:DropDownList ID="ddlstatusperkawinan4" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >
                                                                     <asp:ListItem Value="0">Belum Menikah</asp:ListItem>
                                                        <asp:ListItem Value="1">Menikah</asp:ListItem>
                                                        <asp:ListItem Value="2">Janda</asp:ListItem>
                                                        <asp:ListItem Value="3">Duda</asp:ListItem>


																</asp:DropDownList>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator23" ControlToValidate="ddlstatusperkawinan4"  ErrorMessage="Status Perkawinan tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" Runat="server" />

                                                            </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label7" runat="server" class="col-form-label">Alamat </asp:label>
																<asp:TextBox ID="txtAlamat" text="-" CssClass="form-control" Enabled="true" MaxLength='150' onkeyDown="checkTextAreaMaxLength(this,event,'150');"  TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" SetFocusOnError="True" ControlToValidate="txtAlamat"  ErrorMessage="Alamat  tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
														
                                                         
                                                        </div>
                                                        
                                                        <div class="form-group col-md-6">
                                                            
                                                           	<div class="form-group">
                                                                <asp:label ID="Label14" runat="server" class="col-form-label">Kelurahan</asp:label>
																<asp:TextBox CssClass="form-control" text="-" ID="txtkelurahan"  MaxLength="50"  runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator13" runat="server" SetFocusOnError="True" ControlToValidate="txtkelurahan"  ErrorMessage="Kelurahan  tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 
                                                           

                                                            <div class="form-group">
                                                                <asp:label ID="Label15" runat="server" class="col-form-label">Kecamatan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" text="-" ID="txtkecamatan"  MaxLength="50"  runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" SetFocusOnError="True" ControlToValidate="txtkecamatan"  ErrorMessage="Kecamatan  tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label16" runat="server" class="col-form-label">Kota</asp:label>
	                                                            <asp:TextBox CssClass="form-control" text="-" ID="txtkotapekerjaan" MaxLength="50"  runat="server" ></asp:TextBox>
                                                                  <asp:RequiredFieldValidator id="RequiredFieldValidator15" runat="server" SetFocusOnError="True" ControlToValidate="txtkotapekerjaan"  ErrorMessage="Kota  tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label17" runat="server" class="col-form-label">Kode Pos</asp:label>
	                                                            <asp:TextBox CssClass="form-control" text="0" ID="txtkodepospekerjaan" MaxLength="6"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator16" runat="server" SetFocusOnError="True" ControlToValidate="txtkodepospekerjaan"  ErrorMessage="Kode Pos  tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 

                                                              <div class="form-group">
                                                                <asp:label ID="Label92" runat="server" class="col-form-label">Telepon</asp:label>
	                                                            <asp:TextBox CssClass="form-control" text="0" ID="txttlp"   MaxLength="13"  runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                                  <asp:RequiredFieldValidator id="RequiredFieldValidator45" runat="server" SetFocusOnError="True" ControlToValidate="txttlp"  ErrorMessage="No. Tlp pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 
                                                        </div>

													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPekerjaan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPekerjaan_Click" />
                                                        


                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextPekerjaan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPekerjaan_Click" Width="65px" />


                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 
            <asp:Panel ID="pnlPendapatan" Visible="false" runat="server">
                <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA GAJI NASABAH</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           <div class="form-group">
                                                                <asp:label ID="Label50" Font-Bold="true" runat="server" class="col-form-label">DATA PENGHASILAN DEBITUR</asp:label>
														    </div>  
                                                            <div class="form-row">
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label21" runat="server" class="col-form-label">Gaji Pokok</asp:label>
																<asp:TextBox ID="txtgapok"   onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" runat="server"     AutoPostBack="true"  OnTextChanged="txtgapok_TextChanged">0</asp:TextBox>  
                                                                
                                                             </div>
															
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label23" runat="server" class="col-form-label">Tunjangan istri / suami</asp:label>
																<asp:TextBox ID="txttistri"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control"    runat="server" AutoPostBack="true"  OnTextChanged="txttistri_TextChanged">0</asp:TextBox>  
                                                                
															</div>
                                                                
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label9" runat="server" class="col-form-label">TKD</asp:label>
																<asp:TextBox ID="txttkd"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control"    runat="server"   AutoPostBack="true"  OnTextChanged="txttkd_TextChanged">0</asp:TextBox>  
                                                                                                                                                                                                                                                                                              
															</div>                                                                                                                                                                                                                     
                                                                                         
                                                            <div class="form-group col-md-4">                                                                                                                                                                                               
                                                                <asp:label ID="Label10" runat="server" class="col-form-label">TPP</asp:label>                                                                                                                                          
																<asp:TextBox ID="txttpp"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  AutoPostBack="true"  OnTextChanged="txttpp_TextChanged">0</asp:TextBox>  
                                                                                                                                                                                                                                                                                       
															</div>                                                                                                                                                                                                                     
                                                                                           
                                                            <div class="form-group col-md-4">                                                                                                                                                                                             
                                                                <asp:label ID="Label87" runat="server" class="col-form-label">Tunjangan Beras</asp:label>                                                                                                                              
																<asp:TextBox ID="txttberas"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  AutoPostBack="true"  OnTextChanged="txttberas_TextChanged">0</asp:TextBox> 
                                                                 
															</div>
                                                                
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label18" runat="server" class="col-form-label">Tunjangan cacat</asp:label>
																<asp:TextBox ID="txttcacat"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server" AutoPostBack="true"  OnTextChanged="txttcacat_TextChanged">0</asp:TextBox> 
                                                                                                                                                                                                         
															</div>                                                                                                                                                                                                                     
                                                                                        
                                                            <div class="form-group col-md-4">                                                                                                                                                                                                
                                                                <asp:label ID="Label19" runat="server" class="col-form-label">Pembulatan</asp:label>                                                                                                                                   
																<asp:TextBox ID="txtpembulatan"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  AutoPostBack="true"  OnTextChanged="txtpembulatan_TextChanged">0</asp:TextBox> 
                                                                   
															</div>
                                                                
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label20" runat="server" class="col-form-label">TPM</asp:label>
																<asp:TextBox ID="txttpm"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"   AutoPostBack="true"  OnTextChanged="txttpm_TextChanged">0</asp:TextBox> 
                                                                                                                                                                                                                                                                                
															</div>                                                                                                                                                                                                                     
                                                                                         
                                                            <div class="form-group col-md-4">                                                                                                                                                                                               
                                                                <asp:label ID="Label49" runat="server" class="col-form-label">Tunjangan Khusus</asp:label>                                                                                                                             
																<asp:TextBox ID="txttkhusus"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control"    runat="server"   AutoPostBack="true"  OnTextChanged="txttkhusus_TextChanged">0</asp:TextBox> 
                                                                 
															</div>
                                                                
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label51" runat="server" class="col-form-label">Tunjangan Kematian</asp:label>
																<asp:TextBox ID="txttkematian"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  AutoPostBack="true"  OnTextChanged="txttkematian_TextChanged">0</asp:TextBox> 
                                                                                                                                                                                                                                               
															</div>                                                                                                                                                                                                      
                                                                                                                                                                                                                                                                        
                                                            <div class="form-group col-md-4">                                                                                                                                                                           
                                                                <asp:label ID="Label58" runat="server" class="col-form-label">Tunjangan pajak</asp:label>                                                                                                               
																<asp:TextBox ID="txttpajak"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"   AutoPostBack="true"  OnTextChanged="txttpajak_TextChanged">0</asp:TextBox> 
                                                                
															</div>
                                                                
                                                           
                                                             </div>
                                                            
                                                        <div class="form-group">
                                                            <asp:label ID="lblGajiKotor" runat="server" class="col-form-label">Gaji Kotor</asp:label>
															<asp:TextBox ID="txtgajikotor" Enabled ="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  >0</asp:TextBox> 
                                                                
														</div>
                                                        </div> 
                                                            
                                                        <div class="form-group col-md-6">
                                                             <div class="form-group">
                                                                <asp:label ID="Label52" Font-Bold="true" runat="server" class="col-form-label">DATA POTONGAN PENGHASILAN DEBITUR</asp:label>
														    </div>  
                                                    <div class="form-row">
                                                            <div class="form-group col-md-4"> 
                                                                 <asp:label id="Label24" runat="server" class="col-form-label">Potongan Askes</asp:label>
                                                                 <asp:TextBox ID="txtpotaskes" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" runat="server"  AutoPostBack="true"  OnTextChanged="txtpotaskes_TextChanged">0</asp:TextBox>    
                                                                 
                                                            </div>   
                                                        
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label25" runat="server" class="col-form-label">Potongan Assos</asp:label>
																<asp:TextBox ID="txtpotassos" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  CssClass="form-control" runat="server" data-style="btn-custom"  AutoPostBack="true"  OnTextChanged="txtpotassos_TextChanged">0</asp:TextBox>        
                                                                 
                                                           </div>                                                                                                                                                                                                                                
                                                                                      
                                                            <div class="form-group col-md-4">                                                                                                                                                                                                            
                                                                <asp:label ID="Label27" runat="server" class="col-form-label">Potongan Pajak</asp:label>                                                                                                                                         
																<asp:TextBox ID="txtpotpajak" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"  onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  OnTextChanged="txtpotpajak_TextChanged">0</asp:TextBox>      
                                                                 
                                                           </div>                                                                                                                                                                                                                           
                                                                                                                                                                                                                                                                                            
                                                            <div class="form-group col-md-4">                                                                                                                                                                                               
                                                                <asp:label ID="Label28" runat="server" class="col-form-label">Potongan Lain</asp:label>                                                                                                                                     
																<asp:TextBox ID="txtpotlain" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"  AutoPostBack="true"  OnTextChanged="txtpotlain_TextChanged">0</asp:TextBox>       
                                                                
                                                           </div>
                                                        
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label93" runat="server" class="col-form-label">Sewa Rumah</asp:label>
																<asp:TextBox ID="txtpotsewarmh" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"  AutoPostBack="true"  OnTextChanged="txtpotsewarmh_TextChanged">0</asp:TextBox>       
                                                                
                                                           </div>
                                                        
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label94" runat="server" class="col-form-label">Potongan kasda</asp:label>
																<asp:TextBox ID="txtpotkasda" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"   AutoPostBack="true"  OnTextChanged="txtpotkasda_TextChanged">0</asp:TextBox>     
                                                                </div>
                                                            </div>
                                                           <div class="form-group">
                                                                <asp:label ID="lbltotalPengeluaran" runat="server" class="col-form-label">Total Potongan</asp:label>
																<asp:TextBox ID="txttotal"  Enabled ="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" >0</asp:TextBox>      
                                                           </div>
                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="lblsisapenghasilan" runat="server" class="col-form-label">Gaji Bersih</asp:label>
																<asp:TextBox ID="txtgajibersih" Enabled ="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" >0</asp:TextBox>      
                                                                
                                                           </div>

                                                            <div class="form-group">
                                                                 <asp:label id="Label95" runat="server" class="col-form-label">Tanggal Slip Gaji</asp:label>
                                                                 <asp:TextBox CssClass="form-control" ID="TextBox28" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                  <asp:RequiredFieldValidator id="RequiredFieldValidator40" runat="server" SetFocusOnError="True" ControlToValidate="TextBox28"  ErrorMessage="Tanggal Slip gaji tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                         
                                                                 </div>  
                                                            </div>  

													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPendapatan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPendapatan_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextPendapatan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPendapatan_Click" Width="65px" />

                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 

                

            <asp:Panel ID="pnlPengajuan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENGAJUAN KREDIT NASABAH</div>
                                                       <p class="m-b-0"></p>

                                                    <div class="form-row">
                                                         
                                                       <asp:HiddenField ID="HDJenisPinjaman" runat="server"/> 
                                                          <asp:DropDownList Visible="false" ID="ddlkodeinstansi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                               <asp:DropDownList ID="ddlkdproduk" Visible="false" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                          <asp:TextBox CssClass="form-control" visible="false" ID="txtspread" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                <asp:TextBox CssClass="form-control" visible="false" ID="txtbaserate" Enabled="false"  runat="server" >0</asp:TextBox>
                                                  
                                                        <div class="form-group col-md-6">
                                                           
                                                             <%--<div class="form-group">
                                                                <asp:label ID="Label30" runat="server" class="col-form-label">No. Nasabah</asp:label>
																<asp:TextBox ID="txtnoreklama" CssClass="form-control"  runat="server" maxlength ="40"></asp:TextBox> 
															 </div>--%>
                                                             <div class="form-group">
                                                                <asp:label ID="Label151" Font-Bold="true" runat="server" class="col-form-label">DATA KREDIT</asp:label>
														    </div>  
                                                           <div class="form-row">
                                                               <div class="form-group col-md-6">
                                                                <asp:label ID="Label98" runat="server" class="col-form-label">Jenis Pensiun</asp:label>
																<asp:DropDownList ID="ddljenispensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                      
                                                                 
                                                                </div>
                                                                 <div class="form-group col-md-6">
                                                                <asp:label ID="Label81" runat="server" class="col-form-label">Jenis Penggunaan</asp:label>
																<asp:DropDownList ID="ddlPenggunaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                               
                                                           
                                                             </div>
                                                            </div>
                                                           
                                                             <div class="form-row">
                                                       
                                                             <div class="form-group col-md-6">
                                                                <asp:label ID="Label55" runat="server" class="col-form-label">Program Pinjaman</asp:label>
																<asp:DropDownList ID="ddlprogrampinjaman" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlprogrampinjaman_SelectedIndexChanged" ></asp:DropDownList>
                                                                

                                                             </div>

                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label56" runat="server" class="col-form-label">Sumber Dana</asp:label>
																<asp:DropDownList ID="ddlsumberdana" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlsumberdana_SelectedIndexChanged" ></asp:DropDownList>
                                                              
                                                             </div>
                                                            </div>
                                                            
                                                           <div class="form-row">
                                                               <div class="form-group col-md-6">
                                                                    <asp:label ID="Label38" runat="server" class="col-form-label">Permohonan Kredit</asp:label>
																    <asp:TextBox CssClass="form-control" ID="txtplafon" Text="0"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" AutoPostBack="true"  OnTextChanged="txtplafon_TextChanged"></asp:TextBox>
                                                                  
                                                           
                                                                </div> 
                                                                <div class="form-group col-md-6">
                                                                    <asp:label ID="Label29" runat="server" class="col-form-label">Persetujuan Bendahara</asp:label>
																    <asp:TextBox CssClass="form-control" ID="txtbendahara" Text="0" Enabled ="false"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>
        
                                                                </div> 
                                                              
                                                            
                                                              
                                                            </div>
                                                             <div class="form-row">
                                                                 <div class="form-group col-md-6">
                                                                 <asp:label id="Label109" runat="server" class="col-form-label">Tanggal Realisasi</asp:label>
                                                                 
	                                                            <asp:TextBox CssClass="form-control" ID="txtrealisasi"   TextMode="Date" runat="server" AutoPostBack="true"  OnTextChanged="txtrealisasi_TextChanged"></asp:TextBox>
                                                                
                                                         
                                                              </div>  
                                                                   <div class="form-group col-md-6">
                                                                    <asp:label ID="Label39" runat="server" class="col-form-label">Jangka Waktu (Bulan)</asp:label>
	                                                                <asp:TextBox CssClass="form-control" text="0" ID="txtjangka" MaxLength="3" onkeypress="return isnumerickey(event);" runat="server"  AutoPostBack="true"  OnTextChanged="txtjangka_TextChanged"></asp:TextBox>
                                                                </div> 
                                                           </div>  
                                                              
                                                            
                                                

                                                        </div>
                                                        <div class="form-group col-md-6">
                                                                
                                                            <div class="form-group">
                                                                <asp:label ID="Label152" Font-Bold="true" runat="server" class="col-form-label">DATA BIAYA DAN POTONGAN</asp:label>
														    </div>  
                                                            
                                                           <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label36" runat="server" class="col-form-label">Angsuran Dimuka</asp:label>
                                                                <asp:DropDownList ID="ddlkali" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlkali_SelectedIndexChanged" ></asp:DropDownList>
                                                                                                                          
                                                            </div> 
                                                               <div class="form-group col-md-6">
                                                                     <asp:label ID="Label26" runat="server" class="col-form-label">Jumlah Angsuran Dimuka</asp:label>
                                                               <asp:TextBox CssClass="form-control" Text="0" ID="txtangsurandimuka" Enabled="false" runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" AutoPostBack="true"  OnTextChanged="txtangsurandimuka_TextChanged"></asp:TextBox>
                                                                   </div> 
                                                            </div> 


                                                             <div class="form-row">
                                                             <div class="form-group col-md-6">
                                                                    <asp:label ID="Label53" runat="server" class="col-form-label">Angsuran Blokir</asp:label>
                                                                   
                                                                    <asp:DropDownList ID="ddlblokir" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlblokir_SelectedIndexChanged" ></asp:DropDownList>
                                                              
																    
                                                                 
                                                               
                                                                </div> 
                                                                  <div class="form-group col-md-6">
                                                                     <asp:label ID="Label61" runat="server" class="col-form-label">Jumlah Angsuran Blokir</asp:label>
                                                                       <asp:TextBox CssClass="form-control" ID="txtangsblokir" Enabled="false"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);">0</asp:TextBox>
                                                                   </div> 
                                                            </div> 

                                                               <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label57" runat="server" class="col-form-label">Biaya Angsuran Dimuka</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbyangsurandimuka" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  runat="server" >0</asp:TextBox>
                                                                
                                                               
                                                            </div> 
                                                               
                                                              <div class="form-group col-md-4">
                                                                <asp:label ID="Label108" runat="server" class="col-form-label">Biaya Take Over</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbyto"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  runat="server" >0</asp:TextBox>
                                                                
                                                               
                                                                </div> 
                                                                   <div class="form-group col-md-4">
                                                                <asp:label ID="Label114" runat="server" class="col-form-label">Panjar</asp:label>
	                                                            <asp:TextBox CssClass="form-control" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  ID="txtbypanjar"   runat="server" >0</asp:TextBox>
                                                                
                                                               
                                                            </div> 
                                                            </div>

                                                          
                                                

                                                            
                                                           <div class="form-row">
                                                            
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label115" runat="server" class="col-form-label">Admin Pencairan</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtadmpencairan" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"    runat="server" >0</asp:TextBox>
                                                                    <asp:RequiredFieldValidator id="RequiredFieldValidator43" runat="server" SetFocusOnError="True" ControlToValidate="txtadmpencairan"  ErrorMessage="Admin Pencairan tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                                
                                                                      <div class="form-group">
                                                                   <asp:label ID="Label62" runat="server" class="col-form-label">Nominal Pelunasan</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtnominalpelunasan" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"    runat="server" >0</asp:TextBox>
                                                                    <asp:RequiredFieldValidator id="RequiredFieldValidator24" runat="server" SetFocusOnError="True" ControlToValidate="txtadmpencairan"  ErrorMessage="Admin Pencairan tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               
                                                                 
                                                                </div>        
                                                                    

                                                             
                                                                </div> 
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label116" runat="server" class="col-form-label">Admin Angsuran</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtadmangsuran"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  runat="server" >0</asp:TextBox>
                                                                    <asp:RequiredFieldValidator id="RequiredFieldValidator44" runat="server" SetFocusOnError="True" ControlToValidate="txtadmangsuran"  ErrorMessage="Admin Angsuran tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               
                                                                </div> 
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label35" runat="server" class="col-form-label">Taksasi</asp:label>
																    <asp:TextBox CssClass="form-control" ID="txttaksasi"   runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);">0</asp:TextBox>
                                                                     <asp:RequiredFieldValidator id="RequiredFieldValidator46" runat="server" SetFocusOnError="True" ControlToValidate="txttaksasi"  ErrorMessage="Taksasi tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    
                                                                   
                                                                </div>

                                                                                                                     
                                                               
                                                           
                                                            </div> 

													</div> 
                                                </div>
													</div> 
               
              
              
              
              
              </div>


                   <div class="row clearfix">
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                       <div class="row">                          

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Informasi Data Kredit Dan Status SK</div>
                                    <p class="text-muted font-14 m-b-30"></p>   
                                
                                      <div class="form-row">                                                     
                                                             <div class="form-group col-md-6">
                                                                <asp:label ID="Label34" runat="server" class="col-form-label">Status Take Over</asp:label>
                                                                 
                                                                 <asp:TextBox CssClass="form-control" ID="txtTO" Enabled="false"    runat="server"></asp:TextBox>
																<%--<asp:DropDownList ID="ddlTO" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom" ></asp:DropDownList>--%>
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator3" ErrorMessage="Status TO tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlTO" Runat="server" />--%>

                                                             </div> 
                                                          
                                                              

                                                            <div class="form-group col-md-6">
                                                                 <asp:label id="Label66" runat="server" class="col-form-label">Tanggal Jatuh Tempo</asp:label>
                                                                 <asp:TextBox CssClass="form-control" ID="txttgljt" Enabled="false"     runat="server"></asp:TextBox>
                                                            </div>  
                                                             </div>  
                                                           <div class="form-row">
                                                                 <div class="form-group col-md-4">
                                                                    <asp:label ID="Label32" runat="server" class="col-form-label">Persen Gaji</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtpersengaji" Enabled="false" runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" ></asp:TextBox>
                                                                     <%--<asp:RequiredFieldValidator id="RequiredFieldValidator38" runat="server" SetFocusOnError="True" ControlToValidate="txtpersengaji"  ErrorMessage="Persen Gaji tidak boleh kosong, isi '0' jika tidak ada"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                         
                                                                </div>

                                                                 <div class="form-group col-md-4">
                                                                    <asp:label ID="Label76" runat="server" class="col-form-label">Usia</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtusia"  Enabled="false" runat="server"  ></asp:TextBox>
                                                                </div> 
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label41" runat="server" class="col-form-label">Persen Provisi</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtProvisi"  Enabled="false" runat="server" ></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator id="RequiredFieldValidator41" runat="server" SetFocusOnError="True" ControlToValidate="txttglrealisasi"  ErrorMessage="Persen Provisi tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                </div> 
                                                               
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label110" Visible="false" runat="server" class="col-form-label">Persen Perhitungan</asp:label>
	                                                                <asp:TextBox CssClass="form-control"  Visible="false" ID="txtpersenperhitungan"  Enabled="false" runat="server" ></asp:TextBox>
                                                                     <%--<asp:RequiredFieldValidator id="RequiredFieldValidator42" runat="server" SetFocusOnError="True" ControlToValidate="txtpersenperhitungan"  ErrorMessage="Persen Perhitungan tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                               
                                                                </div> 
                                                            </div>
                                                                <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label60" runat="server" class="col-form-label">Angsuran Perbulan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" ID="txtangsuranperbulan"   runat="server" >0</asp:TextBox>
                                                               
                                                               
                                                            </div> 
                                                            
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label111" runat="server" class="col-form-label">Bunga</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbungas" Enabled="false"   runat="server" >0</asp:TextBox>
                                                                 
                                                               
                                                            </div> 
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label40" runat="server" class="col-form-label">Rate Asuransi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbgc"  Enabled="false"  runat="server" >0</asp:TextBox>
                                                               
                                                               
                                                            </div> 
                                                            </div>
                                                                <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label112" runat="server" class="col-form-label">Admin Asuransi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="txtadmiasuransi" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"   runat="server" >0</asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator41" runat="server" SetFocusOnError="True" ControlToValidate="txtadmiasuransi"  ErrorMessage="Admin Asuransi tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                               
                                                            </div> 
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label113" runat="server" class="col-form-label">Admin Provisi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="txtadmprovisi"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"   runat="server" >0</asp:TextBox>
                                                                  <%--<asp:RequiredFieldValidator id="RequiredFieldValidator42" runat="server" SetFocusOnError="True" ControlToValidate="txtadmprovisi"  ErrorMessage="Admin Provisi tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                               
                                                            </div> 
                                                             </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label72" runat="server" class="col-form-label">Status SK</asp:label>
																<asp:DropDownList ID="ddlstatusSK" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusSK_SelectedIndexChanged" >
                                                                    <asp:ListItem Value="0">SK Belum Di Terima</asp:ListItem>
                                                                    <asp:ListItem Value="1">SK Sudah Di Terima</asp:ListItem>
                                                      

																</asp:DropDownList>
                                                                
                                                                 <%--<asp:RequiredFieldValidator id="RequiredFieldValidator58" ErrorMessage="Status SK tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlstatusSK" Runat="server" />--%>

                                                             </div>

                                                             <div>
                                                                <asp:FileUpload ID="fileuploadSK" runat="server"/> 
                                                                <asp:Button ID="btnUploadktp" Text="Upload" runat="server" OnClick="UploadFileSK" OnClientClick="return CheckFileExistenceNPWP()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtpathsk" CssClass="form-control"   Enabled="false" runat="server" MaxLength="400"></asp:TextBox>
                                                                     
                                                                <hr /> 
                                                            </div>
                          

                                                           

                                                                                           
                            </div>
                           </div>
                              </div>

                           
                            <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Juru Bayar</div>
                                    <p class="text-muted font-14 m-b-30"></p>   
                                
                                                              <div class="form-group">
                                                                <asp:label ID="Label68" runat="server" class="col-form-label">Kantor Bayar</asp:label>
																<asp:DropDownList ID="ddlKantorBayar" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                  <asp:RequiredFieldValidator id="RequiredFieldValidator111" ErrorMessage="Kantor Bayar tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlKantorBayar" Runat="server" />
                                                           
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label69" runat="server" class="col-form-label">Bank Rekening Nasabah</asp:label>
																<asp:DropDownList ID="ddlBankNasabah" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                 
                                                                  <asp:RequiredFieldValidator id="RequiredFieldValidator1111" ErrorMessage="Bank Rek.Nasabah tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlBankNasabah" Runat="server" />
                                                            </div>
                                                             <div class="form-group">
                                                                    <asp:label ID="Label70" runat="server" class="col-form-label">No. Rekening Bank Nasabah</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtrekeningbankdeb" onkeypress="return isnumerickey(event);"  MaxLength="50"  runat="server" ></asp:TextBox>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator2222" runat="server" SetFocusOnError="True" ControlToValidate="txtrekeningbankdeb"  ErrorMessage="No. Rek Bank Nasabah tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               
                                                                </div> 
                                                             <div class="form-group">
                                                                    <asp:label ID="Label71" runat="server" class="col-form-label">Atas Nama Rekening Bank Nasabah</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtrekeningnamadeb"  MaxLength="100"  runat="server" ></asp:TextBox>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidatora" runat="server" SetFocusOnError="True" ControlToValidate="txtrekeningbankdeb"  ErrorMessage="A/N Nasabah tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label13" runat="server" class="col-form-label">jaminan</asp:label>
																<asp:TextBox ID="txtjaminan"  CssClass="form-control" Enabled="true" MaxLength='200' onkeyDown="checkTextAreaMaxLength(this,event,'200');"  TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator59" runat="server" SetFocusOnError="True" ControlToValidate="txtalasanpeminjaman"  ErrorMessage="jaminan tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator><asp:RequiredFieldValidator id="RequiredFieldValidator60" runat="server" SetFocusOnError="True" ControlToValidate="txtAlamat"  ErrorMessage="Alamat  tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            
                                                            </div>


                                                                                           
                            </div>
                           </div>
                              </div>
                           
               </div>      
                                   
                </div>
              </div>


                <div class="row clearfix">
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                       <div class="row">                          

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA USAHA DEBITUR</div>
                                    <p class="text-muted font-14 m-b-30"></p>   
                                <div class="form-row">
                                                                
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label63" runat="server" class="col-form-label">Jenis Usaha</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="TextBox43" MaxLength="50"  runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator52" runat="server" SetFocusOnError="True" ControlToValidate="TextBox43"  ErrorMessage="Jenis Usaha tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label117" runat="server" class="col-form-label">Lokasi Usaha</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="TextBox44" MaxLength="50"   runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator53" runat="server" SetFocusOnError="True" ControlToValidate="TextBox44"  ErrorMessage="Lokasi Usaha tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label118" runat="server" class="col-form-label">Jumlah Tenaga Kerja</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="TextBox45" MaxLength="5"  onkeypress="return isnumerickey(event);" runat="server" ></asp:TextBox>
                                                               <asp:RequiredFieldValidator id="RequiredFieldValidator54" runat="server" SetFocusOnError="True" ControlToValidate="TextBox45"  ErrorMessage="Jumlah Tenaga Kerja tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label119" runat="server" class="col-form-label">Pendapatan Usaha</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="TextBox46" MaxLength="16"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" runat="server" >0</asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator55" runat="server" SetFocusOnError="True" ControlToValidate="TextBox46"  ErrorMessage="Pendapatan Usaha tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label120" runat="server" class="col-form-label">Biaya Tenaga Kerja</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="TextBox47" MaxLength="16"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" runat="server" >0</asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator56" runat="server" SetFocusOnError="True" ControlToValidate="TextBox47"  ErrorMessage="Biaya Tenaga Kerja tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label121" runat="server" class="col-form-label">Biaya Produksi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="TextBox48"  MaxLength="16" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" runat="server" >0</asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator57" runat="server" SetFocusOnError="True" ControlToValidate="TextBox48"  ErrorMessage="Biaya Produksi tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               
                                                            </div>  
                                                            </div>
                             

                                                                                           
                            </div>
                           </div>
                              </div>

                           
                            <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA MARKETING</div>
                                    <p class="text-muted font-14 m-b-30"></p>   
                                
                                   
                                                              <div class="form-group">
                                                                <asp:label ID="Label64" runat="server" class="col-form-label">AO</asp:label>
																<asp:DropDownList ID="ddlAO" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                 <%--<asp:RequiredFieldValidator id="reqFavoriteColor" ErrorMessage="AO tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlAO" Runat="server" />--%>

                                                             </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label65" runat="server" class="col-form-label">Ref Non AO</asp:label>
																<asp:DropDownList ID="ddlnonao" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                 <asp:RequiredFieldValidator id="reqFavoriteColor" ErrorMessage="Ref Non AO tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlnonao" Runat="server" />

                                                             </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label37" runat="server" class="col-form-label">Alasan Peminjaman</asp:label>
																<asp:TextBox ID="txtalasanpeminjaman"  CssClass="form-control" Enabled="true" MaxLength='150' onkeyDown="checkTextAreaMaxLength(this,event,'150');"  TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidators" runat="server" SetFocusOnError="True" ControlToValidate="txtalasanpeminjaman"  ErrorMessage="Alamat Rumah Sesuai KTP (1) tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>

                                    

                                                                                           
                            </div>
                           </div>
                              </div>
                           
               </div>      
                                   
                </div>
              </div>
                                            
                                       <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPengajuan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPengajuan_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextPengajuan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPengajuan_Click" Width="65px" />

                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 
           
                  <asp:Panel ID="pnlUpdAsuransi" Visible="false" runat="server">
                    <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA UPLOAD FOTO ASURANSI</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">

                                                          <div class="form-group">
                                                                <asp:label ID="Label31" runat="server" class="col-form-label">Foto 1</asp:label>
                                                                 
                                                            </div> 
                                                            <div>
                                                                <asp:FileUpload ID="fileupload1" runat="server"/> 
                                                                <asp:Button ID="btnfoto1" Text="Upload" runat="server" OnClick="Uploadfoto1" OnClientClick="return CheckFileFoto1()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtfoto1" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                   
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
                                                                <asp:Button ID="btnfoto3" Text="Upload" runat="server" OnClick="Uploadfoto3" OnClientClick="return CheckFileFoto3()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtfoto3" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                   
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
                                                                <asp:Button ID="btnfoto4" Text="Upload" runat="server" OnClick="Uploadfoto4" OnClientClick="return CheckFileFoto4()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtfoto4" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                                                                   
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
                                                                <asp:Button ID="btnfoto5" Text="Upload" runat="server" OnClick="Uploadfoto5" OnClientClick="return CheckFileFoto5()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtfoto5" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                                                                   
                                                                <hr /> 

                                                          
                                                            </div>
                                                             <div>
                                                                  <asp:Image ID="imgfoto5" runat="server" Height="300px" Width="550px" />
                                                            </div>

                                                        </div>  



													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackIdentitas" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackIdentitas_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnSimpan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="SAVE" OnClick="btnSimpan_Click" Width="65px" />

                                                    </div>
                                            </div> 
     
    
            </asp:Panel> 
           
            </div> 
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>    
                <asp:PostBackTrigger ControlID="btnBackPekerjaan" /> 
                <asp:PostBackTrigger ControlID="btnBackPendapatan" />  
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnNext" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnNextPekerjaan" />
                <asp:PostBackTrigger ControlID="btnNextPendapatan" />  
                <asp:PostBackTrigger ControlID="GRData" />  
                <asp:AsyncPostBackTrigger ControlID="ddlprogrampinjaman" /> 
                <asp:PostBackTrigger ControlID="ddljenispermohonan" /> 
                <asp:PostBackTrigger ControlID="btnUploadktp" />
                <asp:PostBackTrigger ControlID="btnSearch" />
                <asp:PostBackTrigger ControlID="txtjangka" />
                <asp:PostBackTrigger ControlID="btnfoto1" /> 
                <asp:PostBackTrigger ControlID="btnfoto2" />
                <asp:PostBackTrigger ControlID="btnfoto3" />
                <asp:PostBackTrigger ControlID="btnfoto4" />
                <asp:PostBackTrigger ControlID="btnfoto5" />
                <asp:PostBackTrigger ControlID="txtrealisasi" />
                <asp:PostBackTrigger ControlID="txtnonasabah" />
                 
                 
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
