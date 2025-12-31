<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewImage.aspx.cs" Inherits="SN_Image_View" %>

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
                                        <li class="breadcrumb-item"><a href="#">Simpanan Nasabah</a></li>
                                        <li class="breadcrumb-item"><a href="#">View</a></li>
                                        <li class="breadcrumb-item"><a href="ViewImage.aspx">View Image Transaction</a></li>
                                    </ol>
                                </div>
                                <%--<h4 class="page-title">Master Kode Currency</h4>--%>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Edit -->
                    <asp:Panel ID="pnladd" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 center-page">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Filter</div>
                                    <br />
                                    <div class="title text-center mb-3">
                                        <h3>Search Image Transaction</h3>
                                    </div>
                                    <br />
                                    <div class="form-row">
                                        <div class="form-group  col-md-12">
                                            
                                        
                                            <div class="form-group  ">
                                                <div class="form-check-inline col-md-12">
                                                    <label for="inputCity" class="col-md-4 col-form-label font-weight-bold">Bulan</label>
                                                    <div class="col-6">
                                                        <asp:DropDownList ID="ddlbulan" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom">
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
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <div class="form-check-inline col-md-12">
                                                    <asp:Label ID="Label6" runat="server" class="col-md-4 col-form-label font-weight-bold">Tahun</asp:Label>
                                                    <div class="col-4">
                                                        <asp:DropDownList ID="ddltahun" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row justify-content-center">
                                            <div class="btn-group mb-2">
                                                <div class="button-list">
                                                    <asp:Button ID="btnsearch" class="btn btn-success waves-light text-white" runat="server" Text="Proses" OnClick="btnSimpan_Click" OnClientClick="btnSimpan_Click();" />
                                                    &nbsp&nbsp&nbsp&nbsp&nbsp
                                                    <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="Back" OnClientClick='redirect()' />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlgrid" runat="server" Visible="false">
                        <div class="row" runat="server" visible="false">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Back" OnClientClick='redirect()' />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom" runat="server" id="titlegrid"></div>
                                    <br />
                                    <br />
                                    <%--<h4 class="m-t-0 header-title" runat="server" id="titlegrid"></h4>--%>
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
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Justify" HeaderText="No.Nasabah">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Justify" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Nama Nasabah">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center"  Height="30px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nasabah")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Process">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljenis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaForm")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Jenis Proses">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsimpanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaGroup_Form")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="View Foto 1" >
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black"  />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <a id="view1" runat="server" class="image-popup" href='<%#DataBinder.Eval(Container.DataItem, "image1")%>'>
                                                                <asp:Label ID="lblimage1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "image1")%>'></asp:Label>
                                                                </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="View Foto 2" >
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black"  />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <a id="view2" runat="server" class="image-popup" href='<%#DataBinder.Eval(Container.DataItem, "image2")%>'>
                                                                <asp:Label ID="lblimage2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "image2")%>'></asp:Label>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="View" Visible="false">
                                                        <HeaderStyle CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <%--<a id="view1" runat="server" class="image-popup"></a>--%>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="View" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><a id="view1" runat="server" class="image-popup" href='<%#DataBinder.Eval(Container.DataItem, "image2")%>'><i class="fa fa-edit"></i>
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

                    <asp:Panel ID="pnldata" Visible="false" runat="server">
                        <div class="row ">
                            <div class="col-lg-12 ">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Data Nasabah</div>
                                    <p class="text-muted font-14 m-b-30"></p>
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <label class="col-form-label">No Nasabah</label>
                                                <asp:TextBox ID="txtnonasabah" CssClass="form-control" required="required" runat="server" Width="219px" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-form-label">Nama Nasabah</label>
                                                <asp:TextBox ID="txtnama" CssClass="form-control" required="required" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblmulaisusut" runat="server" class="col-form-label">Jenis Simpanan</asp:Label>
                                                <asp:TextBox ID="txtjenis" CssClass="form-control" required="required" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblumur" runat="server" class="col-form-label">Jenis Process</asp:Label>
                                                <asp:TextBox ID="txtproses" CssClass="form-control" required="required" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <div>
                                                <asp:Label ID="Label3" runat="server" class="col-form-label">Foto KTP</asp:Label>
                                                <asp:TextBox ID="txtpath1" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                <hr />
                                                <asp:Image ID="Image1" runat="server" Height="140px" Width="280px" />
                                            </div>
                                            <div>
                                                <asp:Label ID="Label4" runat="server" class="col-form-label">Foto NPWP</asp:Label>
                                                <asp:TextBox ID="txtpath2" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                <hr />
                                                <asp:Image ID="Image2" runat="server" Height="140px" Width="280px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="btn-group mb-2">
                                <div class="button-list">
                                    <asp:Button ID="Button1" class="btn btn-success waves-light text-white" runat="server" Text="Back" OnClientClick='redirect()' />
                                    <%--<asp:LinkButton ID="LinkButton2" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />--%>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsearch" />
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