<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArAdjustmentBulanan.aspx.cs" Inherits="AE_Transaksi_ArAdjustmentBulanan" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
         <title>Adjustment Bulanan</title>
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

      //  $(document).ready(InIEvent);
        //function InIEvent() {
          $(document).ready(InIEvent);
          function InIEvent() {
              $("div").remove(".tooltip");
              $.getScript("/assets/js/jquery.core.js");

          
              $('#txtStart').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

              $('#txtEnd').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            

            $('.selectpicker').selectpicker();


              $(function () {
                  var table = $('#grd_uploaded').dataTable({
                      "order": [],
                      "paging": true,
                      "columnDefs": [{
                          "targets": 'no-sort',
                          "orderable": false,
                      }],
                      ordering: [[0, 'asc']],
                      "scrollX": true,
                      retrieve: true

                  });

                  var tableX = $('#grd_uploaded').dataTable({
                      "order": [],
                      "columnDefs": [{
                          "targets": 'no-sort',
                          "orderable": false,
                      }],
                      ordering: [[0, 'asc']],
                      "scrollX": true,
                      retrieve: true

                  });
              })

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table").css({ "width": "100%" });

            $('form').parsley();
        

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



      </script>

        <style type="text/css">
        .form-control {
        }
    </style>

    </head>
<body>
    <div class="p-3">
            <h4 class="m-t-0 header-title text-muted">ADJUSTMENT BULANAN</h4>
            <form id="frm_upload" runat="server" EncType="multipart/form-data">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Panel ID="section_upload" runat="server">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                    <script type="text/javascript">
                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
                    </script>
                    <div class="card p-2">
                        <div class="card-body form-group p-2">
                            <div class="card-header">Upload Excel File</div>
                            <input type="file" id="file1" runat="server" class="form-control" accept=".xls, .xlsx" />
                        </div>
                        <div class="justify-content-center d-flex w-100">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />&nbsp;
                            <asp:Button ID="btnView" runat="server" Text="View data" OnClick="btnView_Click" />
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="section_data" runat="server" CssClass="bg-white p-2">
                    <asp:GridView ID="grd_uploaded" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="800px" OnRowCommand="grd_uploaded_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="KODE CABANG">
                                <ItemTemplate>
                                    <asp:Label ID="lblKode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodecabang")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PERUSAHAAN">
                                <ItemTemplate>
                                    <asp:Label ID="lblPerusahaan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "company_id")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CABANG">
                                <ItemTemplate>
                                    <asp:Label ID="lblNama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NOMINAL">
                                <ItemTemplate>
                                    <asp:Label ID="lblNominal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nilai")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PERIODE" visible=false>
                                <ItemTemplate>
                                    <asp:Label ID="lblPeriode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "periode")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" visible=false>
                                <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <span class="badge badge-danger p-2">Delete</span></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                 </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnUpload" />
                        <asp:PostBackTrigger ControlID="btnView" />
                        <asp:PostBackTrigger ControlID="grd_uploaded" />
                    </Triggers>
                 </asp:UpdatePanel>
            </form>
     </div>
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