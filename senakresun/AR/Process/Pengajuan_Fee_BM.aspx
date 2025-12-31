<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pengajuan_Fee_BM.aspx.cs" Inherits="AR_Process_Pengajuan_Fee_BM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pengajuan AO WO Detail </title>
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

            function sweetAlertConfirm(btnDelete, errMsg) {
                if (btnDelete.dataset.confirmed) {
                    // The action was already confirmed by the user, proceed with server event
                    btnDelete.dataset.confirmed = false;
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
                            btnDelete.dataset.confirmed = true;
                            btnDelete.click();
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

                $('#<%=txtFeebaru.ClientID%>').on('change, keyup', function () {
                    var currentInput = $(this).val();
                    var fixedInput = currentInput.replace(/[^0-9 \,]/, '');
                    $(this).val(fixedInput);

                    console.log(fixedInput);
                });

                $('#<%=txtHpbaru.ClientID%>').on('change, keyup', function () {
                    var currentInput = $(this).val();
                    var fixedInput = currentInput.replace(/[^0-9 \,]/, '');
                    $(this).val(fixedInput);
                    console.log(fixedInput);
                });

                $('.mandatory').on('change, keyup', function () {

                    if ($(this).val().length <= 0) {
                        $('#<%=btnSave.ClientID%>').attr('disabled', true);
                    } else {
                        $('#<%=btnSave.ClientID%>').attr('disabled', false);
                    }
                });

                $('#txtparambulan').datepicker({
                    format: 'm',
                    startView: "months",
                    minViewMode: "months",
                    autoclose: true,
                    todayHighlight: true
                });
                $('#txtparamtahun').datepicker({
                    format: 'yyyy',
                    autoclose: true,
                    startView: "years",
                    minViewMode: "years",
                    todayHighlight: true
                });

                $('.selectpicker').selectpicker();

                function BindDataTable() {
                    var table = $('#GRData').dataTable({
                        "order": [],
                        "paging": false,
                        "columnDefs": [{
                            "targets": 'no-sort',
                            "orderable": false,
                        }],
                        ordering: [[0, 'asc']],
                        "scrollX": true,
                        retrieve: true

                    });

                    var tableX = $('#GRDT').dataTable({
                        "order": [],
                        "columnDefs": [{
                            "targets": 'no-sort',
                            "orderable": false,
                        }],
                        ordering: [[0, 'asc']],
                        "scrollX": true,
                        retrieve: true

                    });
                }

                $(function () {
                    BindDataTable();

                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(BindDataTable);
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
             <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>   <!-- delete  -->
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
                                    <li class="breadcrumb-item"><a href="#">Pengajuan Lain-lain </a></li>
                                </ol>
                            </div>
                           
                        </div>
                    </div>
                </div>
                 <!-- Panel Header -->
                 <asp:Panel ID="pnlHeader" runat="server">
                     <div class = "row">
                         <div class="col-12">
                             <div class="form-group col-md-5">
                                 <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                 <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Pengajuan Baru" OnClick="btnAdd_Click" />
                             </div>
                         </div>
                        
                     </div>
                 </asp:Panel>

                 <asp:Panel ID="pnlTambah" runat="server" Visible="False" BackColor="White">
                    
                     <%--<div class="card-header bg-info">
                         <div class="p-2 h-4 text-white">Tambah Pengajuan</div>
                     </div>--%>
                     
                     <div class="row">
                         <div class="col col-md-3">
                             <div class="card-box ribbon-box mb-5">
                                 <div class="ribbon ribbon-custom">Tambah Pengajuan</div>
                            </div>
                             <div class="form-group p-3"> 
                                <label class="text-muted">No. Bukti</label>
                                <asp:TextBox ID="txtNobukti" CssClass="form-control" runat="server" autocomplete="off" Enabled="false"></asp:TextBox>
                             </div>
                        </div>
                      
                         <div class="col col-md-3">
                             <div class="card-box mb-5">
                                
                             </div>
                             <div class="form-group p-3">
                                 <label class="text-muted">Tanggal Pengajuan</label>
                                 <asp:TextBox ID="txtTglpengajuan" CssClass="form-control" runat="server" autocomplete="off" Enabled="false"></asp:TextBox>
                             </div>
                        </div>     
                         
                         <div class="col col-md-4">
                             <div class="card-box mb-5">
                             </div>
                             <div class="form-group p-3">
                                 <label class="text-muted">Jenis Pengajuan</label>
                                 <asp:DropDownList ID="ddlPengajuan" runat="server" AutoPostBack="true" CssClass="ddl selectpicker show-tick" data-style="btn-success" OnSelectedIndexChanged="ddlPengajuan_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="- Pilih salah satu -"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Perubahan Fee BM"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Perubahan No. HP Kartu Angsuran"></asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                         </div>
                     </div>
                 </asp:Panel>

                 <asp:Panel ID="pnlFee" runat="server" Visible="False" BackColor="White">
                      <div class="row">
                         <div class="col col-md-3">
                            <%-- <div class="card-box ribbon-box mb-5">
                                 <div class="ribbon ribbon-custom">Approval</div>
                             </div>--%>

                             <div class="form-group p-3">
                                 <label class="text-muted">% Fee BM Lama</label>
                                 <asp:TextBox ID="txtFeelama" CssClass="form-control" runat="server" autocomplete="off" Enabled="false"></asp:TextBox>
                             </div>
                         </div>

                         <div class="col col-md-3">
                              <%--<div class="card-box ribbon-box mb-5"></div>--%>
                             <div class="form-group p-3">
                                 <label class="text-muted">% Fee BM Baru</label>
                                 <asp:TextBox ID="txtFeebaru" CssClass="form-control" runat="server" autocomplete="off"></asp:TextBox>
                             </div>
                         </div>
                 </asp:Panel>

                 <asp:Panel ID="pnlNohp" runat="server" Visible="False" BackColor="White">
                     <div class="row">            
                         <div class="col col-md-3">
                             <%--<div class="card-box ribbon-box mb-5">
                                 <div class="ribbon ribbon-custom">Approval</div>
                             </div>--%>

                             <div class="form-group p-3">
                                 <label class="text-muted">No. HP di Kartu Lama</label>
                                 <asp:TextBox ID="txtHplama" CssClass="form-control" runat="server" autocomplete="off" Enabled="false"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col col-md-3">
                             <%-- <div class="card-box ribbon-box mb-5"></div>--%>
                             <div class="form-group p-3">
                                 <label class="text-muted">No. HP Baru</label>
                                 <asp:TextBox ID="txtHpbaru" CssClass="form-control" runat="server" autocomplete="off"></asp:TextBox>
                             </div>
                         </div> 
                     </div> 
                 </asp:Panel>

                 <asp:Panel ID="pnlApproval" runat="server" Visible="False" BackColor="White">
                     <div class="row p-2" style="padding: 10px">
                         <div class="col col-md-3 p-2">
                             <div class="form-group p-3">
                                 <label class="text-muted">Memo Cabang</label>
                                 <asp:TextBox ID="txtMemocabang" CssClass="form-control mandatory" runat="server" TextMode="MultiLine" Rows="3" autocomplete="off"></asp:TextBox>
                             </div>
                         </div>

                         <div class="col col-md-3 p-2" id="memo_pusat" runat="server">
                             <div class="form-group p-3">
                                 <label class="text-muted">Memo Pusat</label>
                                 <asp:TextBox ID="txtMemopusat" CssClass="form-control mandatory" runat="server" TextMode="MultiLine" Rows="3" autocomplete="off"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                 </asp:Panel>

                 <asp:Panel ID="pnlButton" runat="server" Visible="false" BackColor="White">
                     <div class="text-right p-3">
                         <asp:HiddenField ID="no_bukti_hidden" runat="server" />
                         <asp:HiddenField ID="jenis_hidden" runat="server" />
                         <asp:HiddenField ID="cabang_hidden" runat="server" />
                         <asp:Button ID="btnApprove" class="btn btn-success waves-light text-white" runat="server" Text="Approve" Visible="false" OnClick="btnApprove_Click" />&nbsp
                         <asp:Button ID="btnTolak" class="btn btn-success waves-light text-white" runat="server" Text="Tolak" Visible="false" OnClick="btnTolak_Click" />&nbsp
                         <asp:Button ID="btnSave" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSave_Click" />
                         &nbsp;<asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                     </div>  
                 </asp:Panel>

                 <asp:Panel ID="pnlGrid" runat="server" CssClass="ml-2 rounded">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                                 <h4 class="m-t-0 header-title text-muted">Pengajuan Perubahan Fee BM & No. HP Kartu Angsuran
                                 </h4>
                                 <div class="form-row p-3">
                                     <div class="table-auto-res" style="width: 100%;">

                                         <asp:GridView ID="GRDT" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="625px" OnRowCommand="GRDT_RowCommand" OnRowDataBound="GRDT_RowDataBound">
                                             <Columns>
                                                 <asp:TemplateField HeaderText="Kode Cabang" Visible="false">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblKodecabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodecabang")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" Width="55px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Cabang">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblNamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" Width="55px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="No Bukti">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" width="120px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Ajukan Ulang">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblajukan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ajukanulang")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" width="55px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Jenis Perubahan" Visible="false">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblJenis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jenis")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" Width="55px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="% Fee Baru">
                                                     <HeaderStyle CssClass="align-center" width="50px" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblFee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "persenfeebm")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="NO. HP Baru">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblNohp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nohptransfer")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" width="100px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Tanggal Pengajuan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblTanggal1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglentry")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" width="80px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Status">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusmemo")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Catatan Cabang">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblKeterangan" CssClass="" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Catatan Approval">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblCatatan" CssClass="" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "noteap")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Tanggal Approval">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblTanggal2" CssClass="" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "dateaddap")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" width="80px" CssClass="align-center"></HeaderStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Action">
                                                     <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                     <ItemTemplate>
                                                         <asp:LinkButton ID="btnApproval" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Approval" CommandName="SETAPP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-check"></i></asp:LinkButton>
                                                         <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-remove"></i></asp:LinkButton>
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
                 <!-- Panel Filter -->
                
                    <asp:Panel ID="Pnlfooter" Visible="false" runat="server">
                               <div class="row">
                                <div class="col-md-12">
                                    <div class="card-box">
                                            <div class="card-box table-responsive">
                                                <%--<asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" OnClientClick="return confirm('Apakah anda yakin ingin menyimpan data ini ?');" runat="server" Text="Save" OnClick="btnSimpan_Click" />--%>
                                               
                                </div>
                                </div>
                             </div>
                             </div>
                       
                       </asp:Panel>
                 
            </div> 
            
        <!-- end wrapper -->
        </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="btnSave" />
<%--                <asp:PostBackTrigger ControlID="btnSearch" />
                <asp:PostBackTrigger ControlID="btnUpdate" />--%>
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="GRDT" />
               
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

