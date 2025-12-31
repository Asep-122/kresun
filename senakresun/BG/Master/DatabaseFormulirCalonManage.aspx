<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DatabaseFormulirCalonManage.aspx.cs" Inherits="BIGDATA_DatabaseFormulirCalonManage" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />


    <!-- Tooltipster css -->
    <link rel="stylesheet" href="../../plugins/tooltipster/tooltipster.bundle.min.css" />

    <!-- Plugins css-->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <!-- App css -->
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <!-- Modal -->
    <link href="../../plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <!-- Toastr css -->
    <link href="../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script src="../../assets/js/modernizr.min.js"></script>

    <!-- DataTables -->
    <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- Responsive datatable examples -->
    <link href="../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/waves.js"></script>
    <script src="../../assets/js/jquery.slimscroll.js"></script>

    <!-- Modal-Effect -->
    <script src="../../plugins/custombox/js/custombox.min.js"></script>
    <script src="../../plugins/custombox/js/legacy.min.js"></script>
    <link href="../../plugins/tablesaw/css/tablesaw.css" rel="stylesheet" type="text/css" />

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
            $("div").remove(".tooltip");
            $.getScript("../../assets/js/jquery.core.js");

            $('#LnkAdd, #lnkCari, #lnkSaveGR,#btnsearch').tooltipster({
                animation: 'grow'
            });
            
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

            $('.selectpicker').selectpicker();

            $(function () {
                var table = $('#GRUser').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
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
                <div class="container-fluid">

                    <asp:Panel ID="pnlData" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <h4 class="text-uppercase m-b-5 font-13 font-600"> <asp:Label ID="Lbltaspen" runat="server" Text="DASHBOARD CALON ANGGOTA"></asp:Label></h4>
                                        <div class="text-center mt-4 mb-4">
                                            <div class="row">
                                                                <div class="col-xs-6 col-sm-6">
                                                                    <div  class="card-box bg-dark widget-flat border-primary text-white">
                                                                        <i class="fi-archive"></i>
                                                                        <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A1"  href="#" runat="server" onserverclick="btnwilayah_Click" >DATA WILAYAH</a></p>
                                                                    </div>
                                                                </div>
                                                                <div class="col-xs-6 col-sm-6">
                                                                    <div  class="card-box bg-dark widget-flat border-danger text-white">
                                                                        <i class="fi-help"></i>
                                                                            <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A4"  href="#" runat="server" onserverclick="btndatabase_Click">MARKETING</a></p>
                                                                    </div>
                                                                </div>
                             
                                            </div>
                                         </div>
                                    </div>
                            </div>
                        </div>
                    </asp:Panel>

       
        <asp:Panel ID="Pnldatabase" Visible="false" runat="server">
                        
        <div class="row">
                                                
            <div class="form-group col-md-6">                            
            <div class="form-group row">
                                            <label for="inputpid" class="col-3 col-form-label">Marketing</label>
                                            <div class="col-9">
                                                <asp:DropDownList ID="DblMarketing" CssClass="selectpicker show-tick" runat="server" data-style="btn-dark" AutoPostBack="true" OnSelectedIndexChanged="DblMarketing_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                      </div>
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">MARKETING DATA</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 99%;">
                                                            <asp:GridView ID="GRUser" runat="server" CssClass="table table-striped table-bordered"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False">

                                                             <Columns>
                                                                    <asp:TemplateField HeaderText="Marketing">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                            <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "kanwil")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Wilayah">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNOddddd" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namawilayah")%>'></asp:Label>
                                                                            <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "kanwil")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO2222" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namacabang")%>'></asp:Label>
                                                                            <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "kanwil")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Data Calon Anggota">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempidaaaa" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Data")%>'></asp:Label>
                                                                         <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "namawilayah")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Belum Follow Up">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Pending")%>'></asp:Label>
                                                                    
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Sudah Follow-Up">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid111" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "followup")%>'></asp:Label>
                                                                    
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
        
        </asp:Panel>

        <asp:Panel ID="Pnlcabang" Visible="false" runat="server">
                        
        <div class="row">
                                                
            <div class="form-group col-md-6">                            
            <div class="form-group row">
                                            <label for="inputpid" class="col-3 col-form-label">Wilayah</label>
                                            <div class="col-9">
                                                <asp:DropDownList ID="Dblwilayah2" CssClass="selectpicker show-tick" runat="server" data-style="btn-dark" AutoPostBack="true" OnSelectedIndexChanged="ddlwilayah2_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                      </div>
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA / CABANG</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 99%;">
                                                            <asp:GridView ID="Grddatacabang" runat="server" CssClass="table table-striped table-bordered"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False">

                                                             <Columns>
                                                                    <asp:TemplateField HeaderText="Cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "area")%>'></asp:Label>
                                                                            <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "kanwil")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Nama Cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNOcabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namacabang")%>'></asp:Label>
                                                                            <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "kanwil")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Data Calon Anggota">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempidaaaa" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Data")%>'></asp:Label>
                                                                         <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "namawilayah")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Belum Follow-Up">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Pending")%>'></asp:Label>
                                                                    
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Sudah Follow-Up">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid111" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "followup")%>'></asp:Label>
                                                                    
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
        </asp:Panel>
                    <asp:Panel ID="Pnldatawilayah" runat="server">
                        
        <div class="row">
                                                
                                                 <div class="col-md-12">
                                                <div class="card-box">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA / WILAYAH</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 99%;">
                                                            <asp:GridView ID="GrdWilayah" runat="server" CssClass="table table-striped table-bordered"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" OnRowCommand="GrdWilayah_RowCommand">

                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Wilayah">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblxNO" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "area")%>'></asp:Label>
                                                                            <asp:LinkButton ID="lbltest" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "area")%></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama Wilayah">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                             <asp:Label ID="lblnamawilayah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namawilayah")%>'></asp:Label>
                                                                             </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Data Calon Anggota">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempidaaaa" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "Data")%>'></asp:Label>
                                                                         <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "namawilayah")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Belum Follow-Up">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Pending")%>'></asp:Label>
                                                                    
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Sudah Follow-Up">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid111" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "followup")%>'></asp:Label>
                                                                    
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                
                                                                
                                                                 </Columns>



                                                            </asp:GridView>
                                                 </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>

                      <asp:Panel ID="Pnlapprove" Visible="false" runat="server">
                        
        <div class="row">
                                                
            <div class="form-group col-md-6">                            
            <div class="form-group row">
                                            <label for="inputpid" class="col-3 col-form-label">Wilayah</label>
                                            <div class="col-9">
                                                <asp:DropDownList ID="DropDownList1" CssClass="selectpicker show-tick" runat="server" data-style="btn-dark" AutoPostBack="true" OnSelectedIndexChanged="ddlwilayah2_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                      </div>
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA / CABANG</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 99%;">
                                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False">

                                                             <Columns>
                                                                    <asp:TemplateField HeaderText="Cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "lokasi")%>'></asp:Label>
                                                                            <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "kanwil")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Nama Cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNOcabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namacabang")%>'></asp:Label>
                                                                            <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "kanwil")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Data Calon Anggota">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempidaaaa" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Data")%>'></asp:Label>
                                                                         <%--<asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "namawilayah")%></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Belum Follow-Up">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Pending")%>'></asp:Label>
                                                                    
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Sudah Follow-Up">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid111" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "followup")%>'></asp:Label>
                                                                    
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
        </asp:Panel>
         </div>
            </ContentTemplate>
            <Triggers>
                  <asp:PostBackTrigger ControlID="DblMarketing" />
            </Triggers>
        </asp:UpdatePanel>

    </form>
</body>
         <script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>
<!-- App js -->
<script src="../../assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>

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



</html>

