<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cetaksuratsanggup.aspx.cs" Inherits="SN_Process_Modal_Cetaksuratsanggup" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cetak Surat Sanggup</title>
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
                    text: t,
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

       
            
            $('#txttglsk').datepicker({
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


              

               $("#txtcustomer").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "PermohonanSimpanan.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>'}",
                         dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[1],
                                    hdncustomer: item[0]
                                }
                            });

                            $("[id$=hdncustomer]").val('');
  

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
                },
                minLength: 3
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Simpanan Nasabah</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="StatusMeninggalpdate.aspx">Cetak Surat Sanggup</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Cetak Surat Sanggup</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlHeader"  runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                            <h4 class="m-t-0 header-title">Filter</h4>
                            <p class="text-muted font-14 m-b-30">
                            <div class="form-group">
                                <asp:label ID="Label1" runat="server" class="col-form-label">Koperasi</asp:label>
                                <asp:DropDownList ID="ddlKoperasi" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlKoperasi_SelectedIndexChanged"  ></asp:DropDownList>
                                                                

                                </div>
                                <div class="form-group">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Cabang</asp:label>
																<asp:DropDownList ID="ddlcabang" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlcabang_SelectedIndexChanged"  ></asp:DropDownList>
                                                               

							  </div>    
                            </p> 
                        </div>
                    </div>
                </div>
                </asp:Panel>
                 <!-- Panel Header -->

                 <asp:Panel ID="pnlFilter" visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <div class="card-box table-responsive">
                                                        

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

                            <h4 class="m-t-0 header-title">Data Nasabah</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                    
                                     <asp:TemplateField visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKoperasi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Koperasi")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Cabang">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkodecabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_cabang")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="No Permohonan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Idpermohonan")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanggal Permohonan">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglpermohonan","{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         
                                       


                                    <asp:TemplateField HeaderText="Nama Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNamaNasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>     
                                    
                                       <asp:TemplateField HeaderText="Produk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnama_produk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_produk")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>        
                                           <asp:TemplateField HeaderText="Jangka">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangkabulan")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>      
                                         <asp:TemplateField HeaderText="Pajak">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPajak" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Pajak")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>    
                                      <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBayar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Bayar")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nilai Saldo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnilaisaldo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nilaisaldo","{0:N2}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>    
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                             <asp:LinkButton ID="btndelete"  runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin membuat No Billyed data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Billyed" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                             <asp:LinkButton ID="btnedit" Visible="false" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin merubah data ini ?');" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                             <asp:LinkButton ID="BtnPDF"  Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Formulir" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="BtnSlip" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Slip" CommandName="Bilyed" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                           
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
                                    <div class="ribbon ribbon-custom">Pencarian Nasabah</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                 <div class="form-group col-md-12">
                                        <label class="col-form-label">Entry Data Nasabah</label>
                                        <asp:TextBox CssClass="form-control" ID="txtcustomer" runat="server" ></asp:TextBox>
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
                                    <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                    <asp:HiddenField ID="HDNoRekening" runat="server" />
                                      
                                                

                                    <div class="form-group">
                                        <asp:HiddenField ID="hdncustomer" runat="server" />
                                    <label for="inputpid" class="col-3 col-form-label">No. Permohonan</label>
                                     <asp:TextBox ID="txtnobukti" CssClass="form-control " runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox>        
                                    </div>
                       
                             
                                      <div class="form-group">
                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Permohonan</label>
                                     <asp:TextBox ID="txttgltrn" enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="10"  AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>      
                                    </div>

                                     <div class="form-group">
                                        <asp:HiddenField ID="HiddenField8" runat="server" />
                                    <label for="inputpid" class="col-3 col-form-label">No. Billyed</label>
                                    
                                         
                                        <asp:TextBox ID="txtnobillyeed" CssClass="form-control " runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox> 
                                     </div>

                                    
                                    
                                       
                                      <div class="form-group">
                                    <label for="inputpid" class="col-9 col-form-label">Produk</label>
                                     <asp:DropDownList ID="ddlproduk" CssClass="selectpicker show-tick"  Enabled="false" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlproduk_SelectedIndexChanged"></asp:DropDownList>   
                                   <asp:RequiredFieldValidator id="RequiredFieldValidator1" Enabled="false" ErrorMessage="Produk tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlproduk" Runat="server" />
                                    </div>
                                     <div class="form-row">
                                       <div class="form-group col-md-6">
                                     <asp:label ID="Label9" runat="server" class="col-9 col-form-label">Jangka Bulan</asp:label>
                                     <asp:DropDownList ID="ddljangka" CssClass="selectpicker show-tick" Enabled="false"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddljangka_SelectedIndexChanged" ></asp:DropDownList>   
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator6" Enabled="false" ErrorMessage="Jangka tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddljangka" Runat="server" />
                                       </div>
                                   
                                    <div class="form-group col-md-6">
                                    <asp:label ID="Label5" runat="server" class="col-9 col-form-label">Bunga % Pertahun</asp:label>
                                    <asp:DropDownList ID="ddlbunga" CssClass="selectpicker show-tick" Enabled="false"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlbunga_SelectedIndexChanged"  ></asp:DropDownList>   
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator7" Enabled="false" ErrorMessage="Bunga tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlbunga" Runat="server" />
                                    </div>
                                    </div>


                                     <div class="form-group">
                                    <label for="inputpid" class="col-9 col-form-label">Pajak %</label>
                                     <asp:DropDownList ID="ddlpajak" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>   
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator8" Enabled="false" ErrorMessage="Pajak tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlpajak" Runat="server" />
                                     </div>

                                     <div class="form-group">
                                                        <asp:label ID="Label8" runat="server" class="col-form-label">Nilai Saldo</asp:label>
                                                            <asp:TextBox ID="txtnilaisaldo" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" AutoPostBack="true" OnTextChanged="txtnilaisaldo_TextChanged"></asp:TextBox>
                                        </div>
                                     <div class="form-group">
                                                        <asp:label ID="Label10" runat="server" class="col-form-label">Nilai Bunga Pertahun</asp:label>
                                                            <asp:TextBox ID="txtnilaibungapertahun" readonly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                        </div>

                                    <div class="form-row">
                                    <div class="form-group">
                                    <asp:label ID="Label6" runat="server" class="col-form-label">Bunga % Perhari</asp:label>
                                    <asp:TextBox ID="txtbungaperhari" readonly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                    <asp:label ID="Label12" runat="server" class="col-form-label">Nilai Bunga Perhari</asp:label>
                                    <asp:TextBox ID="txtnilaibungaperhari" readonly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ></asp:TextBox>
                                    </div>
                                   
                                   </div>
                                  
                                </div>                                                    
                            </div>
                           </div>

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Data Diri Nasabah</div>
                                    <p class="text-muted font-14 m-b-30"></p>                  
                           
                                
                                                           <asp:HiddenField ID="HiddenField1" runat="server" />	 
                                                        <asp:HiddenField ID="HDNoSebelumnya" runat="server" />	 
                                                        <asp:HiddenField ID="HDState" runat="server" />

                                                            <div class="form-group">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Nomor Nasabah</asp:label>
																<asp:TextBox ID="txtnonasabah" Enabled="false" CssClass="form-control"  MaxLength="30" onkeypress="return isnumerickey(event);"  Text="0"   runat="server" ></asp:TextBox>
                                                               
                                                            
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label80" runat="server" class="col-form-label">Nomor Identitas KTP (NIK)</asp:label>
																<asp:TextBox ID="txtidentitas" Enabled="false" CssClass="form-control"  MaxLength="16" onkeypress="return isnumerickey(event);"  Text="0"   runat="server"  ></asp:TextBox>
                                                                
                                                            
                                                            </div> 
                                                                <div class="form-group">
                                                                <asp:label ID="Label11" runat="server" class="col-form-label">Nomer NPWP</asp:label>
																 <asp:TextBox ID="txtNPWP" Enabled="false" CssClass="form-control" MaxLength="15"  Text="0"  runat="server"  onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                 <%--<asp:RequiredFieldValidator id="RequiredFieldValidator32" runat="server" SetFocusOnError="True" ControlToValidate="txtNPWP"  ErrorMessage="NPWP tidak boleh kosong (isi '0' jika tidak memiliki NPWP)"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label">Nama Lengkap</asp:label>
																<asp:TextBox CssClass="form-control"  ID="txtnamanasabah" Enabled="false" MaxLength="70"   runat="server" Text="-" ></asp:TextBox>
                                                          
                                                            </div>

                                                             
                                                          

                                                                <div class="form-group">
                                                                <asp:label ID="Label7" runat="server" class="col-form-label">Alamat </asp:label>
																<asp:TextBox ID="txtalamat"   Enabled="false" CssClass="form-control" Text="-" MaxLength="100" TextMode="MultiLine"  runat="server" ></asp:TextBox>
                                                                
                                                            
                                                            </div>
                                                              <div class="form-row">
                                                                 <div class="form-group col-md-6">
                                                                <asp:label ID="Label23" runat="server" class="col-form-label">Kabupaten</asp:label>
																<asp:TextBox ID="txtkabupaten" Enabled="false" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
															    
                                                            
                                                            </div>
                                                            
                                                            
															    <div class="form-group col-md-3">
                                                                <asp:label ID="Label25" runat="server" class="col-form-label">RT</asp:label>
																<asp:TextBox ID="txtrtktp" Enabled="false" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"    runat="server" ></asp:TextBox>
															 
                                                            
                                                            </div>

															    <div class="form-group col-md-3">
                                                                <asp:label ID="Label26" runat="server" class="col-form-label">RW</asp:label>
																<asp:TextBox ID="txtrwktp" Enabled="false" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"   runat="server" ></asp:TextBox>

                                                            
                                                            </div>
                                                            </div>
                                                            
                                                            <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label27" runat="server" class="col-form-label">Kelurahan</asp:label>
																<asp:TextBox ID="txtkelurahanktp" Enabled="false" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
															    
                                                            
                                                            </div> 

															<div class="form-group col-md-6">
                                                                <asp:label ID="Label28" runat="server" class="col-form-label">Kecamatan</asp:label>
																<asp:TextBox ID="txtkecamatanktp" Enabled="false" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
                                                                
                                                            
															</div>
                                                            </div>
                                                            <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label29" runat="server" class="col-form-label">Provinsi</asp:label>
																<asp:DropDownList ID="ddlprovinsiktp" Enabled="false" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                                

															</div>
                                                            
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label30" runat="server" class="col-form-label">Kode Pos</asp:label>
																<asp:TextBox ID="txtkodeposktp" Enabled="false" CssClass="form-control" Text="0" MaxLength="6"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
															    
                                                            
                                                            </div>

                                                                  </div>    



                                </div>                                                    
                            </div>
                          </div>


                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Data Lain-Lain</div>
                                    <p class="text-muted font-14 m-b-30"></p>                  
                           
                                
                                                           <asp:HiddenField ID="HiddenField2" runat="server" />	 
                                                        <asp:HiddenField ID="HiddenField3" runat="server" />	 
                                                        <asp:HiddenField ID="HiddenField4" runat="server" />

                                                            <div class="form-group">
                                                                <asp:label ID="Label32" runat="server" class="col-form-label">Instruktur Promissory Saat Jatuh Tempo</asp:label>
																 <asp:DropDownList ID="ddlInstruktur" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  >   
                                                                <asp:ListItem Value="1">Pencairan Promissory Saat Jatuh Tempo</asp:ListItem>
                                                                <asp:ListItem Value="2">Perpanjangan Otomatis (Roll Over)</asp:ListItem>
                                                                <asp:ListItem Value="3">Lain-Lainnya</asp:ListItem>                                                              
                                                                </asp:DropDownList>   
                                                            
                                                            </div>
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label33" runat="server" class="col-form-label">Penarikan Bunga</asp:label>
																 <asp:DropDownList ID="ddlpenarikanbunga" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlpenarikanbunga_SelectedIndexChanged"  >   
                                                                <asp:ListItem Value="1">Transfer Ke Rekening</asp:ListItem>
                                                                <asp:ListItem Value="2">Di Ambil Langsung Oleh Nasabah</asp:ListItem>
                                                                <asp:ListItem Value="3">Lain-Lain (Roll Over)</asp:ListItem>                                                              
                                                                </asp:DropDownList>   
                                                            
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label35" runat="server" class="col-form-label">Bank Rekening</asp:label>
																<asp:TextBox ID="txtbankrekening" Enabled="true" CssClass="form-control" MaxLength="100"  Text="-"  runat="server"   ></asp:TextBox> 
                                                                
                                                                </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label34" runat="server" class="col-form-label">No Rekening</asp:label>
																<asp:TextBox ID="txtnorekening" Enabled="true" CssClass="form-control" MaxLength="15"  Text="0"  runat="server"  onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label36" runat="server" class="col-form-label">Atas Nama</asp:label>
																<asp:TextBox ID="txtatasnama" Enabled="true" CssClass="form-control" MaxLength="100"  Text="-"  runat="server"  ></asp:TextBox> 
                                                               
                                                             </div> 

                                                                



                                </div>                                                    
                            </div>
                          </div>

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Ahli Waris</div>
                                    <p class="text-muted font-14 m-b-30"></p>                  
                           
                                
                                                           <asp:HiddenField ID="HiddenField5" runat="server" />	 
                                                        <asp:HiddenField ID="HiddenField6" runat="server" />	 
                                                        <asp:HiddenField ID="HiddenField7" runat="server" />

                                                           
                                                            <div class="form-group">
                                                                <asp:label ID="Label18" runat="server" class="col-form-label">Nama Ahli Waris I</asp:label>
																<asp:TextBox ID="txtnamawaris1" Enabled="true" CssClass="form-control" MaxLength="100"  Text="-"  runat="server"  ></asp:TextBox> 
                                                             </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label19" runat="server" class="col-form-label">No KTP Ahli Waris I</asp:label>
																<asp:TextBox ID="txtktpwaris1" Enabled="true" CssClass="form-control" MaxLength="50"  Text="0"  runat="server"  onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                             </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label20" runat="server" class="col-form-label">Alamat Ahli Waris I </asp:label>
																<asp:TextBox ID="txtalamatwaris1"   CssClass="form-control" Text="-" MaxLength="100"  runat="server" ></asp:TextBox>
                                                                
                                                           
                                                            <div class="form-group">
                                                                <asp:label ID="Label22" runat="server" class="col-form-label">Nama Ahli Waris II</asp:label>
																<asp:TextBox ID="txtnamawaris2" Enabled="true" CssClass="form-control" MaxLength="50"  Text="-"  runat="server"  ></asp:TextBox> 
                                                             </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label24" runat="server" class="col-form-label">No KTP Ahli Waris II</asp:label>
																<asp:TextBox ID="txtktpwaris2" Enabled="true" CssClass="form-control" MaxLength="50"  Text="0"  runat="server"  onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                             </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label31" runat="server" class="col-form-label">Alamat Ahli Waris II </asp:label>
																<asp:TextBox ID="txtalamatwaris2"   CssClass="form-control" Text="-" MaxLength="100"  runat="server" ></asp:TextBox>
                                                               
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
                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                    <asp:Button ID="btndelete" CausesValidation="TRUE" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin save dan cetak billyed data ini ?');" CssClass="btn btn-success waves-light text-white" runat="server" Text="Save dan Cetak" OnClick="btndelete_click" Enabled="true"/>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnCancel_Click" Text="Close" Width="65px" />
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
                 <asp:PostBackTrigger ControlID="ddlKoperasi" />
                <asp:PostBackTrigger ControlID="ddlcabang" />   
                <asp:PostBackTrigger ControlID="GRData" />  
                 <asp:PostBackTrigger ControlID="btndelete" />
                 
                 <%--<asp:PostBackTrigger ControlID="GRRP" />--%>
                  <%--<asp:PostBackTrigger ControlID="btnUploadktp" />--%>
                 
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



