<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeecabangApprove.aspx.cs" Inherits="mikro_FeecabangApprove" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title>Approve Cancel Data Cabang</title>
     <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

        <!-- App icon -->
        <link rel="shortcut icon" href="/assets/images/icon.ico"/>
 <!-- Plugins css-->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

            <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/select.bootstrap4.min.css" rel="stylesheet" type="text/css" />



      <link href="/plugins/bootstrap-tagsinput/css/bootstrap-tagsinput.css" rel="stylesheet" />
        <link href="/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" href="/plugins/switchery/switchery.min.css" />
    

    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="/assets/js/modernizr.min.js"></script>
        <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    
  <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
   
    <%--<script src="../assets/js/bootstrap.min.js"></script>--%>
  

    <script src="/Javascript/jquery-ui.js"></script>
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
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }

                        if (fr == '2') {
                            setTimeout(function () { $('#modalGroup').modal('hide'); <%=btnremodal()%>; }, 10);
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
                ).then(function () {
                    setTimeout(function () { <%=btnremodal()%>; }, 50);
                    }).catch(swal.noop)
            }, 100);

        }
            function Comma(Num) {

            console.log('<%=CultureName%>');
            var splitnya;
            var koma;

            var region = '<%=CultureName%>';

            if (region == 'en-ID') {
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


        function isnumerickey(e) {

            var region = '<%=CultureName%>';


            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {


                if (txt == 44 && region == 'en-ID') {
                    return true;
                }
                if (txt == 46 && region != 'en-ID') {
                    return true;
                }
                else {
                    //alert("Input is Number only")
                    return false;
                }

            }
            return true;
        }

        
        function checkTextField1(field) {

            var region = '<%=CultureName%>';
            var splitnya;
            var dolarnya;
            var koma;

            if (region == 'en-ID') {
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
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
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

            //$(".dataTables_scrollHeadInner").css({ "width": "100%" });

            //$(".table ").css({ "width": "100%" });

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
                    <!-- Form row -->
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                <h4 class="text-uppercase m-b-5 font-13 font-600"> <asp:Label ID="Label1" runat="server" Text="Approve Cancel Data Cabang"></asp:Label></h4>
                                <p class="text-muted m-b-30 font-13">
                                    <asp:LinkButton ID="lnkRefreshModal" runat="server" OnClick="lnkRefreshModal_Click" />



                                    <asp:Panel ID="pnlFront" runat="server">                                        
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <asp:LinkButton ID="btnCancel" CssClass="btn btn-outline-primary" runat="server" Visible="false" Text="Kembali" OnClick="btnCancel_Click" />
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="GRUser" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" OnRowDataBound="GRUser_RowDataBound" OnRowCommand="GRUser_RowCommand" ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False">
                                                                <Columns>
                                                                     <asp:TemplateField HeaderText="Jenis">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbljenisnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaproduk")%>'></asp:Label>
                                                                            <asp:Label ID="lbljenis" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "produk")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="Tanggal Cancel">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltanggal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglcancel","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Note Cancel">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnotecancel" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notecancel")%>'></asp:Label>
                                                                       <asp:Label ID="lblnouruttaspen" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "serial")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <%--   <asp:TemplateField HeaderText="Approve Cancel">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnourut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ApproveCancel")%>'></asp:Label>
                                                                            
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Tgl.Getdata">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltglgetdata" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglgetdata","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                           <asp:TemplateField HeaderText="Tanggal Lahir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltgllahirpsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Usia">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Alamat">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Notas">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnotas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notas")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>    
                                                                    <asp:TemplateField HeaderText="Gaji">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgaji" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                          
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEditUser" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Approve Database" runat="server" CommandName="GRUPAPPROVE1" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-edit"></i>Approve</asp:LinkButton>
                                                                            <asp:LinkButton ID="LinkButton1" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Approve Cancel" runat="server" CommandName="GRUPAPPROVE2" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-edit"></i>Cancel</asp:LinkButton>
                                                                            <asp:LinkButton ID="LinkButton2" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Tolak" runat="server" CommandName="GRUPAPPROVE3" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-edit"></i>Tolak</asp:LinkButton>
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



  

                                    
                                </p>
                            </div>
                        </div>
                    </div>
                    <!-- end row -->

                
                    <!-- /.modal-dialog -->
                </div>
                <!-- /.modal -->

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkRefreshModal" />
                <asp:PostBackTrigger ControlID="GRUser" />
                
            </Triggers>
        </asp:UpdatePanel>

    </form>
</body>

        
<!-- App js -->
<script src="/assets/js/jquery.slimscroll.js"></script>
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

