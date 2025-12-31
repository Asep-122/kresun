<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Viewassetlelang.aspx.cs" Inherits="SN_Image_View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>View Image Transaction</title>
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

    
    <!--venobox lightbox-->
    <link href="/assets/css/magnific-popup/css/magnific-popup.css" rel="stylesheet" type="text/css""/>
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
                   

                        if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                        }
                    }
                });
            }, 0);

        }

        function redirect() {
            location.href = 'ViewImage.aspx ';
        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type : 'success',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);

        }

        function simpan(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'warning',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);
            $('#txtnobukti').value("");
        }
        function WarningAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'warning',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);

        }

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            $('.selectpicker').selectpicker();


            $(function () {
                var table = $('#GRDD').dataTable({
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
    <script type="text/javascript">
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9]/.test(inp))
                return true;
            alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
	</script>
    
    <script src="/Javascript/jquery-ui.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Lelang Asset</a></li>
                                        <li class="breadcrumb-item"><a href="#">View</a></li>
                                        <li class="breadcrumb-item"><a href="ViewImage.aspx">View Lelang Transaction</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">View Lelang Transaction</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Edit -->
                    

                    <asp:Panel ID="pnlgrid" runat="server" >
                       
                                            
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">View Lelang Transaction</div>
                                    <br />
                                    <br />
                                    <h4 class="m-t-0 header-title">Data lelang Asset Tarikan Cabang</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRDD" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRDD_RowCommand" AutoGenerateColumns="false" Width="625px">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="No" >
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <%--<asp:Label ID="lblNumber1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no")%>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Justify" HeaderText="Nama Cabang">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Justify" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodecabang" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kd_cabang")%>'></asp:Label>
                                                            <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabang_nama")%>'></asp:Label>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="No Lelang">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center"  Height="30px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_lelang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Tgl Lelang">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center"  Height="30px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgllelang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tanggal_lelang","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Harga Jual">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center"  Height="30px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHargaJual" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "harga_jual", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Nama Barang">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsimpanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_barang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Brand">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsimpanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "brand")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="size">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsimpanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "size")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="condition">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsimpanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "condition")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="condition desc">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsimpanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "condition_desc")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="View Foto 1" >
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black"  />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                          
                                                           <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="View" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><a id="view1" runat="server" class="image-popup" href='<%#DataBinder.Eval(Container.DataItem, "front_image")%>'><i class="fa fa-edit"></i>
                                                            </a></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="View Foto 2" >
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black"  />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="View" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><a id="view1" runat="server" class="image-popup" href='<%#DataBinder.Eval(Container.DataItem, "back_image")%>'><i class="fa fa-edit"></i>
                                                            </a></asp:LinkButton>
                                                         </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="View Foto 3" >
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black"  />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="View" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><a id="view1" runat="server" class="image-popup" href='<%#DataBinder.Eval(Container.DataItem, "left_image")%>'><i class="fa fa-edit"></i>
                                                            </a></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="View Foto 4" >
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black"  />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="View" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><a id="view1" runat="server" class="image-popup" href='<%#DataBinder.Eval(Container.DataItem, "right_image")%>'><i class="fa fa-edit"></i>
                                                            </a></asp:LinkButton>
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

                    
                    <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
               
                <asp:PostBackTrigger ControlID="GRDD" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
    <%--#buat popup--%>
    <script type="text/javascript" src="/assets/css/magnific-popup/js/jquery.magnific-popup.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.image-popup').magnificPopup({
                type: 'image',
                closeOnContentClick: true,
                mainClass: 'mfp-fade',
                gallery: {
                    enabled: true,
                    navigateByImgClick: true,
                    preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
                }
            });
        });
    </script>
</body>
<!-- App js -->
<script src="/assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="/plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>
  <script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<!-- Tooltipster js -->
<script src="/plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables/responsive.bootstrap4.min.js"></script>  

        <script src="/plugins/tablesaw/js/tablesaw.js"></script>
        <script src="/plugins/tablesaw/js/tablesaw-init.js"></script>
        <script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
            <!-- Buttons examples -->
        <script src="/plugins/datatables/dataTables.buttons.min.js"></script>
        <script src="/plugins/datatables/buttons.bootstrap4.min.js"></script>
        <script src="/plugins/datatables/jszip.min.js"></script>
        <script src="/plugins/datatables/pdfmake.min.js"></script>
        <script src="/plugins/datatables/vfs_fonts.js"></script>
        <script src="/plugins/datatables/buttons.html5.min.js"></script>
        <script src="/plugins/datatables/buttons.print.min.js"></script>
           <!-- Key Tables -->
        <script src="/plugins/datatables/dataTables.keyTable.min.js"></script>
</html>