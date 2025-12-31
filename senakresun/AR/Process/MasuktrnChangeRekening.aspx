<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasuktrnChangeRekening.aspx.cs" Inherits="GL_MasuktrnChangeRekening" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Over Cup Marketing AO </title>
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

            function SweetAlert(errMsg) {

                setTimeout(function () {

                    swal(
                        {
                            title: errMsg,
                            confirmButtonClass: 'btn btn-confirm mt-2'
                        }
                    ).then(function () {
                    }).catch(swal.noop)
                }, 100);

            }

            $(document).ready(InIEvent);
            function InIEvent() {
                $('#datatable').DataTable();

                //Buttons examples
                var table = $('#GRData').DataTable({
                    "order": [],
                    "paging": false,
                    buttons: ['copy', 'excel', 'pdf'],
                    screenX: true,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }]
                });
               

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

                table.buttons().container().appendTo('#GRData_wrapper .col-md-6:eq(0)');
             

                $("div").remove(".tooltip");
                $.getScript("/assets/js/jquery.core.js");

                $('#LnkAdd, #lnkCari, #lnkSaveGR,#btnsearch').tooltipster({
                    animation: 'grow'
                });

                $('.selectpicker').selectpicker();

                document.onclick = function (event) {
                    // Compensate for IE<9's non-standard event model
                    //
                    if (event === undefined) event = window.event;
                    var target = 'target' in event ? event.target : event.srcElement;
                    // alert(event.clientY);
                    if (target.getAttribute('tabindex') == '0') {
                        window.top.cekResize();

                        var x = event.clientX, y = event.clientY;
                        var papaWindow = parent.window;
                        setTimeout(function (e) {
                            papaWindow.scrollBy(0, y / 1.25);
                        }, 50);

                    }
                };

                $('#txtTglL').datepicker({
                    format: 'dd-mm-yyyy',
                    autoclose: true,
                    todayHighlight: true
                });

                $('#txtTglLto').datepicker({
                    format: 'dd-mm-yyyy',
                    autoclose: true,
                    todayHighlight: true
                });

                //$(function () {
                //    var table = $('#Grdtagihank').dataTable({
                //        "order": [],
                //        "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                //        { responsivePriority: 2, targets: -1 }],
                //        ordering: [[0, 'asc']],
                //        "scrollX": true,
                //        retrieve: true
                //    });
                //});

                $(".dataTables_scrollHeadInner").css({ "width": "100%" });

                $(".table ").css({ "width": "100%" });

                $('form').parsley();
            }


        </script>

 
    
    
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            min-height: 1px;
            -ms-flex: 0 0 100%;
            flex: 0 0 100%;
            max-width: 100%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
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
            <%--<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>--%>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="MasuktrnChangeRekening.aspx">Over Cup Marketing AO</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Over Cup Marketing AO</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" runat="server">
                 <div class="row">
                    <div class="auto-style1">
                        <div class="card-box table-responsive">
                            <%--<h4 class="m-t-0 header-title">Filter</h4>--%>
                           <%-- <p class="text-muted font-14 m-b-30">
                                KeyTable provides Excel like cell navigation on any table. Events (focus, blur, action etc) can be assigned to individual cells, columns, rows or all cells.
                            </p>--%>
                            <div class="form-group">
                                    <label class="col-form-label">Pilih Account Officer Sumber</label>
                                    <asp:DropDownList ID="ddlaolama" CssClass="form-control" required="required" runat="server" AutoPostBack="true"  OnSelectedIndexChanged ="ddlmodule_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                              <div class="form-group">
                                    <label class="col-form-label">Pilih Account Officer Tujuan</label>
                                    <asp:DropDownList ID="ddlAobaru" CssClass="form-control" required="required" runat="server" ></asp:DropDownList>
                            </div>
                            
                        </div>
                    </div>
                </div>
                </asp:Panel>
                 <!-- Panel Header -->
               <%--<asp:Panel ID="pnlHeader" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                           <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Kas Bank" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>--%>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Rekening Account Officer Lama</h4>    
                            
                            <div class="form-row">
                                             <div class="form-group col-md-4">
                                                    <label for="inputpid" class="form-control">Jumlah Anggota dipilih</label>
                                                        <asp:TextBox ID="txtjumlahanggota" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>      
                                                  </div>
                                                <div class="form-group col-md-4">
                                                    <label for="inputpid" class="form-control">Jumlah Plafon dipilih</label>
                                                    <asp:TextBox ID="txtjumlahPlafon" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>
                                                </div>
                                               <div class="form-group col-md-4">
                                                    <label for="inputpid" class="form-control">Jumlah Saldo Akhir dipilih</label>
                                                      <asp:TextBox ID="txtjumlahsaldoakhir" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>
                                                </div>
                                                  
                                                </div>
                            <div class="form-row">

                                <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="Button1" class="btn btn-success waves-light text-white"  runat="server" Text="Check List All" OnClick="ckbx_checked" />
                                <asp:Button ID="Button2" class="btn btn-success waves-light text-white" runat="server" Text="Uncheck List All" OnClick="ckbx_unchecked" />

                            </div>
                            
                        </div>
                                
                                   
                                 </div>
                             
                            <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand" OnRowDataBound="GRData_RowDataBound" AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                     <asp:TemplateField HeaderText="No" runat="server" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="No Rekening" runat="server" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnorekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Produk" runat="server" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProduk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nama Anggota" runat="server" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnamaanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                             <asp:Label ID="lblnonasabah" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>                                                                    
                                    <asp:TemplateField HeaderText="Plafon" runat="server" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblplafon"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Plafon", "{0:N2}")%>'></asp:Label>
                                         </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lbltotalplafon" runat="server"  />
                                        </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Saldo Akhir" runat="server" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsaldoakhir"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakidb", "{0:N2}")%>'></asp:Label>
                                         </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lbltotalsaldoakhir" runat="server"  />
                                         </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                   <asp:TemplateField HeaderText="Keterangan" runat="server" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                                <asp:TextBox ID="txtcatatan" Enabled="true" MaxLength="50" CssClass="form-control"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alasanwo")%>'></asp:TextBox>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            <asp:CheckBox Visible="false" Text ="Select ALL" ID="chkHeader" runat="server" OnCheckedChanged ="ckbx_checked" AutoPostBack ="true"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckbx" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged ="chkdtl_checked"></asp:CheckBox>
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

        </asp:Panel>     <!-- Panel View -->
            
            
                    <asp:Panel ID="Pnlfooter" Visible="false" runat="server">
                               <div class="row">
                                <div class="col-md-12">
                                    <div class="card-box">
                                            <div class="card-box table-responsive">
                                                <%--<asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" OnClientClick="return confirm('Apakah anda yakin ingin menyimpan data ini ?');" runat="server" Text="Save" OnClick="btnSimpan_Click" />--%>
                                                <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader_Click" Width="65px" />
                                </div>
                                </div>
                             </div>
                             </div>

                       
                       </asp:Panel>
                    <asp:Panel ID="panelPosting" Visible="false" runat="server">
                               <div class="row">
                                <div class="col-md-12">
                                    <div class="card-box">
                                            <div class="card-box table-responsive">
                                                <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Over Cup data ini ?');" runat="server" Text="Over Cup" OnClick="btnSimpan_Click" />
                                                <%--<asp:LinkButton ID="LinkButton1" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader_Click" Width="65px" />--%>
                                </div>
                                </div>
                             </div>
                             </div>

                       
                       </asp:Panel>
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <%--<asp:PostBackTrigger ControlID="btnSimpan" />--%>
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="ddlaolama" /> 
           <%--     <asp:PostBackTrigger ControlID="GRData" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  <!-- App js -->
<script src="../../assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>
<script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<!-- Tooltipster js -->
<script src="../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>

<script src="../../plugins/tablesaw/js/tablesaw.js"></script>
<script src="../../plugins/tablesaw/js/tablesaw-init.js"></script>
<script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<!-- Buttons examples -->
<script src="../../plugins/datatables/dataTables.buttons.min.js"></script>
<script src="../../plugins/datatables/buttons.bootstrap4.min.js"></script>
<script src="../../plugins/datatables/jszip.min.js"></script>
<script src="../../plugins/datatables/pdfmake.min.js"></script>
<script src="../../plugins/datatables/vfs_fonts.js"></script>
<script src="../../plugins/datatables/buttons.html5.min.js"></script>
<script src="../../plugins/datatables/buttons.print.min.js"></script>
<!-- Key Tables -->
<script src="../../plugins/datatables/dataTables.keyTable.min.js"></script>
<script src="../../assets/js/jquery.core.js"></script>
<!-- plugin js -->
<script src="../../plugins/moment/moment.js"></script>



</html>

