<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewMasukTrnCair.aspx.cs" Inherits="CL_View_ViewMasukTrnCair" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>View Pencairan Pinjaman</title>
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
                        <%--if (fr == '1') {
                            ////setTimeout(function () { <%=btnreload()%>; }, 50);    //Save dan Edit
                        }--%>

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
                var table = $('#GRDatax').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
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
                                    <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">View Setoran dan Pencairan Hari ini</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">View Setoran dan Pencairan Hari ini</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            
                 <!-- Panel Header -->
               
                 <!-- Panel Front -->
       
                 <asp:Panel ID="pnlFilter"  runat="server">
                       <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    
                                                    <div class="form-row">
                                 <div class="form-group col-md-4">
                                
                                    <asp:label ID="Label1" runat="server" class="col-form-label">Tgl Entry</asp:label>
                                     <asp:TextBox ID="txttgltrnx" CssClass="form-control" required="required" runat="server" MaxLength="10"  AutoPostBack="true" OnTextChanged="txtdatex_TextChanged"></asp:TextBox>      
                                                            
                                   
                                 </div>
                                 <div class="form-group col-md-4">
                                <asp:label ID="Label44" runat="server" class="col-form-label">User Teller</asp:label>
                                <asp:DropDownList ID="ddlteller" CssClass="selectpicker show-tick" data-style="btn-success" runat="server" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlteller_SelectedIndexChanged" ></asp:DropDownList>
                                </div>
                              
                                                       
                                                    
                                 </div>
                                                </div>
                                            </div>
                                        </div>

            </asp:Panel>


                 


                 <asp:Panel ID="Panel2" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                                
                          
                                        <div class="card-box widget-flat border-success bg-success text-white">
                                            <p class="text-uppercase m-b-5 font-13 font-600">Data Setoran</p>
                                        </div>

                                 

                             </div>
                         </div>
                     </div>

        </asp:Panel>


                 


                 <asp:Panel ID="PnlFrontx" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">
                                 <h4 class="m-t-0 header-title">Transaksi Setoran Anggota</h4>
                                         <div class="form-group col-md-4">
                               <asp:label ID="Label2" runat="server" class="col-form-label">Marketing</asp:label>
                               <asp:DropDownList ID="ddlmarketing" runat="server" CssClass="form-control" data-style="btn-custom"  AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlmarketing_SelectedIndexChanged">
                                </asp:DropDownList>
                               
                                </div>
                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">
                                          <%--<asp:GridView ID="GridView1" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRData_RowDataBound" AutoGenerateColumns="False">--%>
                                         <asp:GridView ID="GRDatax" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRDataX_RowDataBound" AutoGenerateColumns="False" >
                                             <Columns>

                                                 <asp:TemplateField HeaderText="No Bukti">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Sales">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblsales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodesales")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No Rekening">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoFaktur")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Rekening CBS">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblfakturCbs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening_cbs")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Anggota CBS">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblAnggotaCBS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah_cbs")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Reference Manual">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnobuktimanual" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobuktimanual")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Tanggal Transaksi">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nama Anggota">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblNmLgn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaLgn")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Setoran">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblddlpembayaran" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JumlahTrn", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lbltotalSetoran" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="User Entry ">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbluserentry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodeuser")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Entry date">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglentrydate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglEntry","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                        
  
                                               





                                                 <asp:TemplateField HeaderText="Action" Visible="false">
                                                     <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                     <ItemTemplate>
                                                      <%--   <asp:LinkButton ID="btnedit" Visible="false"runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                         <asp:LinkButton ID="BtnKwPinjaman" runat="server"CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Bukti" CommandName="Bukti" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                         <asp:LinkButton ID="BtnSlip" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Jurnal" CommandName="Jurnal" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                         <asp:LinkButton ID="BtnPDF" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                  --%>   
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

         
                 
              


                 <asp:Panel ID="Panel8" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                      
                          
                                            <div class="card-box widget-flat border-success bg-success text-white">
                                            <p class="text-uppercase m-b-5 font-13 font-600">Data Pencairan</p>
                                        </div>

                                 

                             </div>
                         </div>
                     </div>

        </asp:Panel>


                 



                 <asp:Panel ID="PnlFront" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                                 <h4 class="m-t-0 header-title">Data Pencairan Pinjaman</h4>
                          
                                  

                                 <div class="form-row">
                                     <div class="form-group" style="width: 100%;">

                                         <asp:GridView ID="GRData" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" OnRowDataBound="GRData_RowDataBound" AutoGenerateColumns="False">
                                             <Columns>
                                                    <asp:TemplateField HeaderText="Action">
                                                     <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                     <ItemTemplate>

                                                        <asp:LinkButton ID="BtnPDF" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                      
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Produk">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
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
                                                 <asp:TemplateField HeaderText="Tgl Cair">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglcair" runat="server" DateFormat="dd-mm-yyyy" Text='<%# Eval("tgl_drop", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Anggota">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField Visible="false">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno_bukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_bukti_pencairan")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Rekening">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno_rekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Anggota CBS">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnonasabahcbs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah_cbs")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Rekening CBS">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno_rekeningcbs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening_cbs")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nama Anggota">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Sales">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblsales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kd_staff")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Jangka">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbljangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Type">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "typeanggota")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lbltype" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Plafon">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblPlafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lbltotalplafon" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Biaya Materai">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblmaterai" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "meterai", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lbltotalmaterai" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Biaya Provisi">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblprovisi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "provisi", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lbltotalprovisi" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Biaya Uang Muka">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblBiayauangmuka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "biayauangmuka", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lblBiayauangmuka" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>                                                                                                       
                                                 <asp:TemplateField HeaderText="Biaya Keanggotaan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblBiayaKeanggotaan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "biayaanggota", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lblBiayaKeanggotaan" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Biaya Simpanan Wajib">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblBiayaKewajiban" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "biayasimpananwajib", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lblBiayaKewajiban" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Biaya Cadangan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblBiayacadangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cadz", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lblBiayacadangan" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>                                                 
                                                  <asp:TemplateField HeaderText="Di Terima Anggota">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblditerimabersih" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhditerimabersih", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lblditerimabersih" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>                                                 
                                                <asp:TemplateField HeaderText="Pengembalian Cadangan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblpengembaliancadangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pengembaliancadangan", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lblpengembaliancad" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Cadangan Sebelumnya">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblcadangansebelumnya" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cadangansebelumnya", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lblcadsebelumnya" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Cadangan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblcadangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan", "{0:N2}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:Label ID="lbltotalcadangan" runat="server" />
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
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
                <asp:PostBackTrigger ControlID="GRData" />
                <%--<asp:PostBackTrigger ControlID="btnCancel" />--%>
                         <asp:PostBackTrigger ControlID="ddlteller" />
                 <asp:PostBackTrigger ControlID="ddlmarketing" />
        
               
                 
                     
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



