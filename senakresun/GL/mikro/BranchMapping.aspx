<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchMapping.aspx.cs" Inherits="MasterTemplate_BranchMapping" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Setting Mapping User</title>
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
   function ShowPopup() {        
        $("#modalGroup").modal("show");
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
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }

                        if (fr == '2') {
                            setTimeout(function () { $('#modalGroup').modal('hide'); <%=btnremodal()%>; }, 10);
                        }
                    }
                });
            }, 0);

        }

        function redirect() {
            location.href = 'BranchMapping.aspx ';
        }

        function SweetAlert(errMsg) {

            swal({
                title: 'Congratulations!',
                text: errMsg,
                type: 'success'
            });
        }        

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd, #lnkCari, #lnkSaveGR').tooltipster({
                animation: 'grow'
            });

            //$('#modalGroup').on('shown.bs.modal', function () {
            //    $('#modalGroup').animate({ scrollTop: 0 }, 'slow');
            //});
     


            $('.selectpicker').selectpicker();

            $(function () {
                var table = $('#GRUser').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });

                var tableGroup = $('#DGGroup').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    lengthMenu: [
                        [-1],
                        ['All']
                    ]
                });

                $("#DGGroup_filter").hide();

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


        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">--%>
            <%--<ContentTemplate>--%>
                <div class="container-fluid">
                    <!-- Form row -->
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                <div class="ribbon ribbon-custom">Setting Mapping RM/CRM Area</div>
                                <p class="text-muted m-b-30 font-13"></p>
                                    <asp:LinkButton ID="lnkRefreshModal" runat="server" OnClick="lnkRefreshModal_Click" />
                                    <asp:Panel ID="pnlFront" runat="server">
                                   
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-form-label">Pilih Area</label>
                                                                <div class="form-row">
                                                                    <div class="col-sm-12">
                                                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success"  AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>                                                                   
                                                                </div>
                                                            </div>
                                                        </div>
                                                </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                   <div class="form-row">
                                                        <div class="col-sm-12">
                                                            <%--<button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#modalGroup" runat="server">Open Modal</button>--%>
                                                            <button type="button" id="LnkAdd" class="btn btn-custom waves-light waves-effect" href="#"  runat="server" onserverclick="LnkAdd_Click">Tambah Mapping Area RM/CRM</button>
                                                        </div>
                                                    </div>
                                                <div class="card-box">
                                                 
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="GRUser" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" OnRowDataBound="GRUser_RowDataBound" OnRowCommand="GRUser_RowCommand" ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" Width="1235px">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Urut">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "urut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Group Id" Visible="false">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                              <asp:Label ID="lblid"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id_mapping")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Idrm" Visible="false">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblrmid"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_nik")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Nama RM/CRM">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnamarm" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_karyawan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>      
                                                                    
                                                                    <asp:TemplateField HeaderText="Cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabang_nama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 


                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="lnkEditUser" CssClass="btn btn-outline-primary" runat="server" Text="Mapping" title="Mapping" CommandName="GRUPEDT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                           <%-- <asp:LinkButton ID="lnkEditUser" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Edit" runat="server" CommandName="GRUPEDT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                                           <asp:Button ID="lnkGR" CssClass="btn btn-outline-primary" runat="server" Text="Delete" title="Delete" CommandName="GRUPSET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                            <%--<asp:LinkButton ID="lnkGR" runat="server" data-toggle="tooltip" data-placement="top" class="tooltips" data-original-title="Delete" CommandName="GRUPSET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-group"></i></asp:LinkButton>--%>
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
                            </div>
                        </div>
                    </div>
                    <!-- end row -->
                     <%--<button type="button"  ID="lnkopenGroup" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#modalGroup">--%>
                    <asp:LinkButton ID="lnkopenGroup" runat="server" data-toggle="modal" data-target="#modalGroup" OnClientClick ="return false"></asp:LinkButton>
                   
                    <div id="modalGroup" runat="server" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" data-backdrop="static" data-keyboard="false" aria-hidden="true" style="display: none;">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">x</button>
                                    <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="btnclose" runat="server" onclick="ClosePopup()">×</button>--%>
                                    <h4 class="modal-title" id="myLargeModalLabel">Master Mapping Cabang</h4>
                                </div>
                                <div class="modal-body">
                                    <!-- Form row -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <div class="form-group">

                                                                    <label class="col-form-label">Area</label>
                                                                    <asp:DropDownList ID="ddlarea" CssClass="form-control" runat="server" ></asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnmapping" runat="server"></asp:HiddenField>
                                                                    </div>
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <div class="form-group">
                                                                     <label class="col-form-label">RM/CRM</label>
                                                                    <asp:DropDownList ID="ddlrmcrm" CssClass="form-control" runat="server"></asp:DropDownList>
                                                           
                                                                         </div>
                                                            </div>

                                                                <div class="form-group col-md-6">
                                                                <div class="form-group">
                                                                     <label class="col-form-label">No. Urut</label>
                                                                        <asp:DropDownList ID="ddlnourut" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                                            <asp:ListItem Value="A">A</asp:ListItem>
                                                                            <asp:ListItem Value="B">B</asp:ListItem>
                                                                            <asp:ListItem Value="C">C</asp:ListItem>
                                                                            <asp:ListItem Value="D">D</asp:ListItem>
                                                                            <asp:ListItem Value="E">E</asp:ListItem>
                                                                            <asp:ListItem Value="F">F</asp:ListItem>
                                                                            <asp:ListItem Value="G">G</asp:ListItem>
                                                                            <asp:ListItem Value="H">H</asp:ListItem>
                                                                            <asp:ListItem Value="I">I</asp:ListItem>
                                                                            <asp:ListItem Value="J">J</asp:ListItem>
                                                                        </asp:DropDownList>
                                                           
                                                                   </div>
                                                            </div>

                                                                   
                                                        </div>
                                                        <div class="form-row">
                                                            <div class="col-sm-12">
                                                                <div class="text-center m-t-30">
                                                                    <%--<asp:LinkButton runat="server" ID="lnkSaveGR" CssClass="btn btn-icon waves-effect waves-light btn-purple" title="Simpan" OnClick="lnkSaveGR_Click"> <i class="mdi mdi-content-save-all"></i></asp:LinkButton>--%>
                                                                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                                                    <asp:Button ID="btnCancel" Visible="false"  CssClass="btn btn-outline-primary" runat="server" Text="Kembali" title="Cancel" OnClick="btnCancel_Click" />
                                                                    <asp:Button ID="lnkSaveGR" CssClass="btn btn-outline-primary" runat="server" Text="Simpan" title="Save" OnClick="lnkSaveGR_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                            <div class="form-group" style="width: 100%;">
                                                                <asp:GridView ID="DGGroup" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="true"
                                                                    AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField  HeaderText="No">
                                                                            <ItemTemplate>  
                                                                            <%#Container.DataItemIndex+1 %>
                                    	                                    </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Kode Cabang">
                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblkodecabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_cabang")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nama Cabang">
                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabang_nama")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>                                                                        
                                                                        <asp:TemplateField HeaderText="Is Group">
                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chklist" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "checkbranch").ToString() == "1" ? true:false %>' >
                                                                                </asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end row -->
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
         

            <%--</ContentTemplate>--%>

        <%--</asp:UpdatePanel>--%>

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
