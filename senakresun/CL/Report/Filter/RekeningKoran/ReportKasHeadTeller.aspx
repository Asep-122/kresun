<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportKasHeadTeller.aspx.cs" Inherits="GL_Report_ReportKasHeadTeller" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Report Kas Head Teller</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

  <%--  <!-- App css -->
    <link href="../../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../../../assets/js/modernizr.min.js"></script>






    <!-- Plugins css-->
    <link href="../../../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="../../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../../../assets/js/modernizr.min.js"></script>

    <link href="../../../../assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="../../../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="../../../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../../../assets/js/popper.min.js"></script>
    <script src="../../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../../assets/js/waves.js"></script>
    <script src="../../../../assets/js/jquery.slimscroll.js"></script>

    <script src="../../../../Javascript/jquery-ui.js"></script>--%>



        <!-- Plugins css-->
    <link href="../../../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <!-- App css -->
    <link href="../../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../../../assets/js/modernizr.min.js"></script>
    <link href="../../../../assets/css/jquery-ui.css" rel="stylesheet" />
    <link href="../../../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <!-- jQuery  -->
    <script src="../../../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../../../assets/js/popper.min.js"></script>
    <script src="../../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../../assets/js/waves.js"></script>
    <script src="../../../../assets/js/jquery.slimscroll.js"></script>
    <script src="../../../../Javascript/jquery-ui.js"></script>

    <!-- DataTables -->
        <link href="../../../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
        <link href="../../../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />






    <style type="text/css">
        .form-control {
        }
    </style>

    <script type="text/javascript">

        $(document).ready(InIEvent);
        function InIEvent() {
            // Default Datatable
            $('#datatable').DataTable();

            //Buttons examples
            var table = $('#GRData').DataTable({
                "order": [], "paging": false,
                buttons: ['copy', 'excel', 'pdf'],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            });
            var table = $('#GridView1').DataTable({
                "order": [], "paging": false,
                buttons: ['copy', 'excel', 'pdf'],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            });
            
            var table = $('#GridView2').DataTable({
                "order": [], "paging": false,
                buttons: ['copy', 'excel', 'pdf'],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            });


            $('#key-table').DataTable({
                keys: true
            });

            // Responsive Datatable
            $('#responsive-datatable').DataTable();

            // Multi Selection Datatable
            $('#selection-datatable').DataTable({
                select: {
                    style: 'multi'
                }
            });
            table.buttons().container()
                .appendTo('#GRData_wrapper .col-md-6:eq(0)');
            $('.selectpicker').selectpicker();
            table.buttons().container()
                .appendTo('#GridView1_wrapper .col-md-6:eq(0)');
            table.buttons().container()
                .appendTo('#GridView2_wrapper .col-md-6:eq(0)');

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();



        }


         function showSuccessMessage() {
            try {
                swal({
                    title: "Data tidak ditemukan",
                    text: "",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    closeOnConfirm: true,
                    closeOnCancel: false
                }, function (isConfirm) {
                    if (isConfirm) {

                      <%--  setTimeout(function () {<%= Lumpsum() %>; }, 1); --%>

                        }
                    });
            }
            catch (err) {
                console.log(err.message);
            }

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
                <div class="container-fluid">

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                        <li class="breadcrumb-item"><a href="#">View</a></li>
                                        <li class="breadcrumb-item"><a href="Branch.aspx">Mutasi Cash Head Teller</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Mutasi Cash Head Teller</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->


                    <asp:Panel ID="Panel1" Visible="true" runat="server">                                              
                                
                  <div class="row">

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Periode</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                        <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Periode</label>
                                          <asp:DropDownList ID="ddlSP" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">Januari</asp:ListItem>
                                                        <asp:ListItem Value="2">Februari</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>        
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">This Month From</label>
                                         <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date" AutoPostBack="true" OnTextChanged="ddltgl_SelectedIndexChanged"></asp:TextBox>     
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">This Month To</label>
                                        <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server"  Type="date" AutoPostBack="true" OnTextChanged="ddltgl_SelectedIndexChanged"></asp:TextBox>  
                                    </div>
                                </div>
                    <%--            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">No. Reference from</label>
                                          <asp:DropDownList ID="Ddlreferenceawal" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">No. Reference to</label>
                                         <asp:DropDownList ID="Ddlreferenceakhir" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>                                   
                                </div>--%>
                               
     

                 
                                </div>                                                    
                            </div>
                           </div>

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Setting</div>
                                    <p class="text-muted font-14 m-b-30"></p>                  
                             
                               
                                    <div class="form-group">
                                         <label for="inputpid" class="col-3 col-form-label">Report Type</label>
                                          <asp:DropDownList ID="ddlrpttype" CssClass="form-control"  runat="server" AutoPostBack="true" data-style="btn-custom" OnSelectedIndexChanged="ddlrpttype_SelectedIndexChanged">
                                                                    <asp:ListItem Value="A">Transaksi Lain-Lain</asp:ListItem>
                                                                    <asp:ListItem Value="B">Biaya Approval</asp:ListItem>
                                                                    <asp:ListItem Value="C">Biaya Non Approval</asp:ListItem>
                                                                    <asp:ListItem Value="D">Biaya Bunga/Hutang/Pjak</asp:ListItem>
                                                                    <asp:ListItem Value="E">Biaya BDM/Inventaris</asp:ListItem>
                                                                    <asp:ListItem Value="F">Biaya Sewa Cabang</asp:ListItem>
                                                                    <asp:ListItem Value="G">Biaya Pajak Sewa</asp:ListItem>
                                                                    <asp:ListItem Value="H">Kasbon Operational</asp:ListItem>
                                                                    <asp:ListItem Value="I">Pengembalian Kasbon Operational</asp:ListItem>
                                          </asp:DropDownList>
                                                
                                    </div>
                                                                
                                </div>                                                    
                            </div>
                          </div>              
                    </div>
            </asp:Panel>
                    <asp:Panel ID="Panel2" Visible="true" runat="server">
               
                        <div class="form-group">
                            <div class="button-list">
                                <asp:Button ID="btnsearchdata" class="btn btn-custom waves-light text-white" runat="server" Text="Search" OnClick="btnsearchdata_Click"/> 
                                 <%--<asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Report" OnClick="btnSerach_Click"/>--%>  
                       
                                </div>
                       </div>

                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                              <div class="form-row">
                                                                
															


                                                            
                                                                <div class="form-group col-md-3">
                                                                <asp:label ID="Label41" runat="server" class="col-form-label">Mutasi Debit</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtmutasidebit" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 
                                                                  <div class="form-group col-md-3">
                                                                    <asp:label ID="Label57" runat="server" class="col-form-label">Mutasi Kredit</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtmutasikredit" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 
                                                            
                                                                 <div class="form-group col-md-3">
                                                                    <asp:label ID="Label60" runat="server" class="col-form-label">Saldo Akhir</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtsaldoakhir" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 



                                                            </div>
                                      

                                  

                                 

                             </div>
                         </div>
                     </div>

                 </asp:Panel>

                    <asp:Panel ID="PnlTransaksiLain" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                                 <h4 class="m-t-0 header-title">Mutasi Transaksi Lain-Lain</h4>
                                      

                                  <div class="form-row">
                                        <div class="form-group col-md-3">
                                         <label for="inputpid" class="col-4 col-form-label">Type Transaksi</label>
                                          <asp:DropDownList ID="ddltypetransaksi" CssClass="form-control"  runat="server" AutoPostBack="true" data-style="btn-custom" OnSelectedIndexChanged="ddltypetransaksi_SelectedIndexChanged">
                                                                    <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                                    <asp:ListItem Value="C1">KELUAR</asp:ListItem>
                                                                    <asp:ListItem Value="C2">TERIMA</asp:ListItem>
                                                               
                                          </asp:DropDownList>
                                         </div>       
                                     <div class="form-group col-md-3">
                                         <label for="inputpid" class="col-4 col-form-label">Jenis Transaksi</label>
                                          <asp:DropDownList ID="ddljenistransaksi" CssClass="form-control"  runat="server" AutoPostBack="true" data-style="btn-custom" OnSelectedIndexChanged="ddljenistransaksi_SelectedIndexChanged"></asp:DropDownList>
                                         </div>    
                                      </div>

                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">

                                         <asp:GridView ID="GRData" runat="server" ShowFooter="True" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRData_RowDataBound"  AutoGenerateColumns="False" Width="625px">
                                             <Columns>

                                                   <asp:TemplateField HeaderText="No">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="No Bukti">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Tgl Trn">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Ket Journal">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblketerangan11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJurnal")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Keterangan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblketerangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Jlh Debit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbldebit" CssClass="" runat="server" Text='<%#Eval("Tambah","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                        <FooterTemplate>
                                                         
                                                            <span style="display:block; padding:0; text-align: right"><asp:Label ID="lbldebit" Font-Bold="true" runat="server" Text='0'  /> </span>
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Jlh Kredit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblkredit" CssClass="" runat="server" Text='<%#Eval("Kurang","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                          <span style="display:block; padding:0; text-align: right"><asp:Label ID="lblkredit" Font-Bold="true" runat="server" /> </span>
                                                         
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>                                                 
                                              

                                                   <asp:TemplateField HeaderText="Tgl Entry">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglentrydate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglEntry","{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="User Entry ">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbluserentry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodeuser")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
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
                    
                    <asp:Panel ID="PNLBIAYAAPROVAL" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                                 <h4 class="m-t-0 header-title">Mutasi Biaya Approval</h4>
                                      

                                  <div class="form-row">
                                               
                                     <div class="form-group col-md-3">
                                         <label for="inputpid" class="col-4 col-form-label">Jenis Biaya</label>
                                          <asp:DropDownList ID="ddlbiayaapproval" CssClass="form-control"  runat="server" AutoPostBack="true" data-style="btn-custom" OnSelectedIndexChanged="ddlbiayaapproval_SelectedIndexChanged"></asp:DropDownList>
                                         </div>    
                                      </div>

                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">

                                         <asp:GridView ID="GridView1" runat="server" ShowFooter="True" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GridView1_RowDataBound"  AutoGenerateColumns="False" Width="625px">
                                             <Columns>

                                                   <asp:TemplateField HeaderText="No">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="No Bukti">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="No Journal">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnobukti1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nojurnal")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Tgl Trn">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Ket Journal">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblketerangan11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJurnal")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Keterangan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblketerangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Jlh Debit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbldebit" CssClass="" runat="server" Text='<%#Eval("Tambah","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                        <FooterTemplate>
                                                         
                                                            <span style="display:block; padding:0; text-align: right"><asp:Label ID="lbldebit" Font-Bold="true" runat="server" Text='0'  /> </span>
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Jlh Kredit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblkredit" CssClass="" runat="server" Text='<%#Eval("Kurang","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                          <span style="display:block; padding:0; text-align: right"><asp:Label ID="lblkredit" Font-Bold="true" runat="server" /> </span>
                                                         
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>                                                 
                                              

                                                   <asp:TemplateField HeaderText="Tgl Entry">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglentrydate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglEntry","{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="User Entry ">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbluserentry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodeuser")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
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

                           <asp:Panel ID="PNLBIAYANON" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                                 <h4 class="m-t-0 header-title">Mutasi Biaya Non Approval</h4>
                                      

                                  <div class="form-row">
                                               
                                     <div class="form-group col-md-3">
                                         <label for="inputpid" class="col-4 col-form-label">Jenis Biaya</label>
                                          <asp:DropDownList ID="ddlbiayanon" CssClass="form-control"  runat="server" AutoPostBack="true" data-style="btn-custom" OnSelectedIndexChanged="ddlbiayanon_SelectedIndexChanged"></asp:DropDownList>
                                         </div>    
                                      </div>

                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">

                                         <asp:GridView ID="GridView2" runat="server" ShowFooter="True" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GridView2_RowDataBound"  AutoGenerateColumns="False" Width="625px">
                                             <Columns>

                                                   <asp:TemplateField HeaderText="No">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="No Bukti">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                   
                                                      <asp:TemplateField HeaderText="Tgl Trn">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Ket Journal">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblketerangan11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJurnal")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Keterangan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblketerangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Jlh Debit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbldebit" CssClass="" runat="server" Text='<%#Eval("Tambah","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                        <FooterTemplate>
                                                         
                                                            <span style="display:block; padding:0; text-align: right"><asp:Label ID="lbldebit" Font-Bold="true" runat="server" Text='0'  /> </span>
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Jlh Kredit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblkredit" CssClass="" runat="server" Text='<%#Eval("Kurang","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                          <span style="display:block; padding:0; text-align: right"><asp:Label ID="lblkredit" Font-Bold="true" runat="server" /> </span>
                                                         
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>                                                 
                                              

                                                   <asp:TemplateField HeaderText="Tgl Entry">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglentrydate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglEntry","{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="User Entry ">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbluserentry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodeuser")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
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
                <!-- end container -->

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
           
                  <asp:PostBackTrigger ControlID="btnsearchdata" />
                          <asp:PostBackTrigger ControlID="ddltypetransaksi" />
                
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
<!-- Parsley js -->
<%--<script type="text/javascript" src="../../../../plugins/parsleyjs/parsley.min.js"></script>

<script src="../../../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<script src="../../../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

<!-- Sweet Alert Js  -->
<script src="../../../../plugins/sweet-alert/sweetalert2.min.js"></script>

<script src="../../../../assets/js/jquery.min.js"></script>
<script src="../../../../assets/js/popper.min.js"></script>
<script src="../../../../assets/js/bootstrap.min.js"></script>
<script src="../../../../assets/js/waves.js"></script>
<script src="../../../../assets/js/jquery.slimscroll.js"></script>

<!-- App js -->
<script src="../../../../assets/js/jquery.core.js"></script>
<script src="../../../../assets/js/jquery.app.js"></script>


<script src="../../../../plugins/sweet-alert/sweetalert2.min.js"></script>
         <script src="../../../../plugins/datatables/dataTables.buttons.min.js"></script>

           <script src="../../../../plugins/datatables/buttons.bootstrap4.min.js"></script>
        <script src="../../../../plugins/datatables/jszip.min.js"></script>
        <script src="../../../../plugins/datatables/pdfmake.min.js"></script>
        <script src="../../../../plugins/datatables/vfs_fonts.js"></script>
        <script src="../../../../plugins/datatables/buttons.html5.min.js"></script>
        <script src="../../../../plugins/datatables/buttons.print.min.js"></script>--%>





               <!-- Parsley js -->
    <script type="text/javascript" src="../../../../plugins/parsleyjs/parsley.min.js"></script>

    <script src="../../../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="../../../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>


        <!-- Required datatable js -->
        <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="../../../../plugins/datatables/dataTables.bootstrap4.min.js"></script>
        <!-- Buttons examples -->
        <script src="../../../../plugins/datatables/dataTables.buttons.min.js"></script>
        <script src="../../../../plugins/datatables/buttons.bootstrap4.min.js"></script>
        <script src="../../../../plugins/datatables/jszip.min.js"></script>
        <script src="../../../../plugins/datatables/pdfmake.min.js"></script>
        <script src="../../../../plugins/datatables/vfs_fonts.js"></script>
        <script src="../../../../plugins/datatables/buttons.html5.min.js"></script>
        <script src="../../../../plugins/datatables/buttons.print.min.js"></script>

        <!-- App js -->
        <script src="../../../../assets/js/jquery.core.js"></script>
        <script src="../../../../assets/js/jquery.app.js"></script>
        <!-- plugin js -->
        <script src="../../../../plugins/moment/moment.js"></script>
<%--<script type="text/javascript">

        //  $(document).ready(InIEvent);
        //function InIEvent() {

        $(document).ready(function () {

            // Default Datatable
            $('#datatable').DataTable();

            //Buttons examples
            var table = $('#GRData').DataTable({
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf']
            });
            //var table = $('#GRData').DataTable({
            //    lengthChange: false,
            //    buttons: ['copy', 'excel', 'pdf']
            //});
            // Key Tables

            $('#key-table').DataTable({
                keys: true
            });

            // Responsive Datatable
            $('#responsive-datatable').DataTable();

            // Multi Selection Datatable
            $('#selection-datatable').DataTable({
                select: {
                    style: 'multi'
                }
            });
            table.buttons().container()
                .appendTo('#GRData_wrapper .col-md-6:eq(0)');

        });
</script>--%>
</html>










