<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DatabaseFormulirCalon.aspx.cs" Inherits="BIGDATA_DatabaseFormulirCalon" %>

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
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/custombox/css/custombox.min.css" rel="stylesheet" />
    <link href="../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    <script src="../../assets/js/modernizr.min.js"></script>
    <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/waves.js"></script>
    <script src="../../assets/js/jquery.slimscroll.js"></script>
    <script src="../../plugins/custombox/js/custombox.min.js"></script>
    <script src="../../plugins/custombox/js/legacy.min.js"></script>
    <link href="../../plugins/tablesaw/css/tablesaw.css" rel="stylesheet" type="text/css" />

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
                            window.location.href = 'DatabaseFormulirCalon.aspx';
                        }
                    }
                });
            }, 0);

        }

        function SweetAlertAfterSave(errMsg, NanoText) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        html: NanoText,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }).then(function () {
                        window.location.href = 'DatabaseFormulirCalon.aspx';
                    })
            }, 100);

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

               var table = $('#GrdALL').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });

              var table = $('#Grddatacabang').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });

                var table = $('#GrdFollow').dataTable({
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
                                    <h4 class="text-uppercase m-b-5 font-13 font-600"> <asp:Label ID="Lbltaspen" runat="server" Text="Manage Calon Anggota"></asp:Label></h4>
                                        <div class="text-center mt-4 mb-4">
                                            <div class="row">
                                                              <%--  <div class="col-xs-6 col-sm-4">
                                                                    <div  class="card-box bg-dark widget-flat border-primary text-white">
                                                                        <i class="fi-archive"></i>
                                                                         <h3 class="m-b-10"> <asp:Label ID="Lbltotal" runat="server" Text="0"></asp:Label><a id="A1" runat="server" class="text-white" href="#" onserverclick="btnwilayah_Click">Data Calon Anggota</a></h3>
                                                                        <p class="text-uppercase m-b-5 font-13 font-600">&nbsp;</p>
                                                                    </div>
                                                                </div>--%>
                                                                <div class="col-xs-6 col-sm-4">
                                                                    <div  class="card-box bg-dark widget-flat border-primary text-white">
                                                                        <i class="fi-help"></i>
                                                                         <h3 class="m-b-10"> <asp:Label ID="Lbltotal" runat="server" Text="0"></asp:Label></h3>   
                                                                         <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A1"  href="#" runat="server" onserverclick="btnwilayah_Click" >Data Calon Anggota</a></p>
                                           
                                                                     </div>
                                                                </div>
                                                                <div class="col-xs-6 col-sm-4">
                                                                    <div  class="card-box widget-flat border-success bg-dark text-white">
                                                                        <i class="fi-help"></i>
                                                                         <h3 class="m-b-10"> <asp:Label ID="Lblpending" runat="server" Text="0"></asp:Label></h3>   
                                                                         <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A2"  href="#" runat="server" onserverclick="btncabang_Click" >Belum Follow-Up</a></p>
                                           
                                                                     </div>
                                                                </div>
                                                                <div class="col-xs-6 col-sm-4">
                                                                    <div  class="card-box bg-dark widget-flat border-danger text-white">
                                                                        <i class="fi-help"></i>
                                                                           <h3 class="m-b-10"> <asp:Label ID="lblreject" runat="server" Text="0"></asp:Label></h3>
                                                                            <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A4"  href="#" runat="server" onserverclick="btndatabase_Click">Sudah Follow-Up</a></p>
                                                                    </div>
                                                                </div>
                             
                                            </div>
                                         </div>
                                    </div>
                            </div>
                        </div>
                    </asp:Panel>

               <%--<asp:Panel ID="Pnlsearch" Visible="false" runat="server">
                        <div class="row">
                                              <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label class="text-uppercase m-b-5 font-13 font-600">Search By</label>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Wilayah</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlwilayah" CssClass="selectpicker show-tick" runat="server" data-style="btn-dark" AutoPostBack="true" OnSelectedIndexChanged="ddlcabang_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Cabang</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlcabang" CssClass="selectpicker show-tick" runat="server" data-style="btn-dark" ></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                   <div class="form-group row">
                                                                        <div class="col-3">
                                                                            <button type="button" id="btnsearch" class="btn btn-outline-custom waves-light waves-effect" href="#" runat="server" onserverclick="lnkCari_Click">Search</button>
                                                                        </div>
                                                                    </div>
                                                            </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                        </div>
                    </asp:Panel>--%>

        <asp:Panel ID="Pnldatabase" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA YANG SUDAH DI FOLLOW-UP</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 99%;">
                                                            <asp:GridView ID="GRUser" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                 AutoGenerateColumns="False" OnRowCommand="GRUser_RowCommand">
                                                                                    <Columns>
                                                                                            <asp:TemplateField HeaderText="No">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                                                    <asp:Label ID="lblnourut" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>  
                                                                                            <asp:TemplateField HeaderText="Entry Date">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglinsert")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>  
                                                                                            <asp:TemplateField HeaderText="FollowUp">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FollowUp")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Produk">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaproduk")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="referal">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "referal")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="nama">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="telpon">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid222" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "telpon")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                          </asp:TemplateField>                                                                    
                                                                                           <asp:TemplateField HeaderText="alamat">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempidemail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>                                                                    
                                                                                           <asp:TemplateField HeaderText="email">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "email")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="catatan">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "catatan")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Tgl.Lahir">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Gaji">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Plafon">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Tenor">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tenor")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Take.Over">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "takeover")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Asuransi">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "asuransi")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Angsuran">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuran")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="DBR">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "dbr")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Status.Dbr">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A19" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusdbr")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="admin">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "admin")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Asuransi.Jiwa">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "asuransijiwa")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Biaya.Cetak">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "biayacetak")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Blokir">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A23" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "blokir")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Provisi">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "provisi")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Total.Biaya">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbiaya")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Pencairan">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pencairan")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Bunga">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bunga")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Total.Take.over.Bunga">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A28" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totaltakebunga")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Terima.Bersih">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A29" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "terimabersih")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Sisa.Gaji">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A30" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "sisagaji")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Usia">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A31" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Usia.Lunas">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A32" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usialunas")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Keterangan">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833A33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="+">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="LinkButton2" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Follow Up" runat="server" CommandName="APPROVE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Follow-Up</i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkEdit" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="History" runat="server" CommandName="REJECT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">History</asp:LinkButton>
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
                                              <div class="col-md-12">
                                                <div class="card-box">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA YANG BELUM DI FOLLOW UP</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 99%;">
                                                            <asp:GridView ID="Grddatacabang" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" OnRowCommand="Grddatacabang_RowCommand">

                                                            <Columns>
                                                                                        <asp:TemplateField HeaderText="No">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblNOb1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                                                <asp:Label ID="lblnourutb" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>  
                                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglinsert")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>                                                                                           
                                                                                        <asp:TemplateField HeaderText="Produk">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaproduk")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="referal">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "referal")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="nama">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="telpon">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb222" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "telpon")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>                                                                    
                                                                                        <asp:TemplateField HeaderText="alamat">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidbemail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>                                                                    
                                                                                        <asp:TemplateField HeaderText="email">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "email")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="catatan">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempid8331" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "catatan")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Tgl.Lahir">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8332" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Gaji">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8333" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Plafon">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8334" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Tenor">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8335" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tenor")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Take.Over">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8336" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "takeover")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Asuransi">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8337" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "asuransi")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Angsuran">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8338" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuran")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="DBR">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8339" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "dbr")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Status.Dbr">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83310" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusdbr")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="admin">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83311" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "admin")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Asuransi.Jiwa">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83312" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "asuransijiwa")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Biaya.Cetak">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83313" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "biayacetak")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Blokir">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83314" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "blokir")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Provisi">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83315" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "provisi")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Total.Biaya">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83316" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbiaya")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Pencairan">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83317" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pencairan")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Bunga">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83318" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bunga")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Total.Take.over.Bunga">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83319" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totaltakebunga")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Terima.Bersih">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83320" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "terimabersih")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Sisa.Gaji">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83321" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "sisagaji")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Usia">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83322" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Usia.Lunas">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83323" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usialunas")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Keterangan">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid83324" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="+">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="Lnkbakanfollwoaaa" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Follow Up" runat="server" CommandName="APPROVE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="icon-like"></i></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                            </asp:GridView>
                                                 </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>



        <asp:Panel ID="Pnldatawilayah" runat="server">
                        
        <div class="row">
                                                
                                                 <div class="col-md-12">
                                                <div class="card-box">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA ALL</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 99%;">
                                                            <asp:GridView ID="GrdALL" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False">
                                                                                 <Columns>
                                                                                            <asp:TemplateField HeaderText="No">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                                                    <asp:Label ID="lblnourut" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>  
                                                                                            <asp:TemplateField HeaderText="Entry Date">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglinsert")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>                                                                                         
                                                                                           <asp:TemplateField HeaderText="Produk">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaproduk")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                           <asp:TemplateField HeaderText="FollowUp">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FollowUp")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="referal">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "referal")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="nama">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="telpon">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid222" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "telpon")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                          </asp:TemplateField>                                                                    
                                                                                           <asp:TemplateField HeaderText="alamat">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempidemail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>                                                                    
                                                                                           <asp:TemplateField HeaderText="email">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "email")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="catatan">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "catatan")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Tgl.Lahir">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Gaji">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Plafon">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Tenor">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tenor")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Take.Over">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "takeover")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Asuransi">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "asuransi")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Angsuran">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuran")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="DBR">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "dbr")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Status.Dbr">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusdbr")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="admin">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "admin")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Asuransi.Jiwa">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "asuransijiwa")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Biaya.Cetak">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "biayacetak")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Blokir">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "blokir")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Provisi">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "provisi")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Total.Biaya">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbiaya")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Pencairan">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pencairan")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Bunga">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bunga")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Total.Take.over.Bunga">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totaltakebunga")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Terima.Bersih">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "terimabersih")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Sisa.Gaji">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "sisagaji")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Usia">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Usia.Lunas">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usialunas")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Keterangan">
                                                                                                <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblempid833" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                    
                                                                                         </Columns>
                                                <%--                <Columns>
                                                                    <asp:TemplateField HeaderText="No">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpropinsi" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "kanwil")%>'></asp:Label>
                                                                            <asp:LinkButton ID="lnkeee" runat="server" CausesValidation="True" UseSubmitBehavior="False" CommandName="Wilayah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><%#DataBinder.Eval(Container.DataItem, "kanwil")%></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "total","{0:#,###0}")%>'></asp:Label>
                                                                    
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                
                                                                
                                                                 </Columns>--%>
                                                            </asp:GridView>
                                                 </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>

          <asp:Panel ID="pnlfollowup" Visible="false" runat="server" >
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="card-box">
                                                                             <h4 class="header-title m-t-0 m-b-20">Progress Update</h4>
                                                                            <div class="row">
                                                                                <div class="col-md-6">
                                                                                    <div class="card-box">
                                                                                        <div class="form-horizontal">
                                                                                            <div class="form-group row">
                                                                                                <label for="inputpid" class="col-3 col-form-label">Jenis Follow Up</label>
                                                                                                <div class="col-9">
                                                                                                    <asp:DropDownList ID="ddlfollowup" CssClass="selectpicker show-tick" runat="server" data-style="btn-dark"></asp:DropDownList>
                                                                                                    <asp:TextBox CssClass="form-control" ID="hdnidcalon" runat="server" Visible="false" ></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="form-group row">
                                                                                                <label for="inputpid" class="col-3 col-form-label">Catatan</label>
                                                                                                <div class="col-9">
                                                                                                    <asp:TextBox CssClass="auto-style1" ID="txtcatatan" required="required" runat="server" Height="103px" MaxLength="255" TextMode="MultiLine" Width="811px"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="form-group mb-0 justify-content-end row">
                                                                                                <div class="col-9">
                                                                                                    <asp:Button ID="btnSimpan" CssClass="btn btn-outline-primary" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                                                                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnCancel_Click" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <!-- end card-box -->
                                                                    </div>
                                                                    <!-- end col -->
                                                                </div>
            </asp:Panel>   


            <asp:Panel ID="pnlfollowupsudah" Visible="false" runat="server" >
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="card-box">
                                                                             <h4 class="header-title m-t-0 m-b-20">Progress Update</h4>
                                                                            <div class="row">
                                                                                <div class="col-md-6">
                                                                                    <div class="card-box">
                                                                                        <div class="form-horizontal">
                                                                                            <div class="form-group row">
                                                                                                <label for="inputpid" class="col-3 col-form-label">Jenis Follow Up</label>
                                                                                                <div class="col-9">
                                                                                                    <asp:DropDownList ID="ddlfollowupsudah" CssClass="selectpicker show-tick" runat="server" data-style="btn-dark"></asp:DropDownList>
                                                                                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" Visible="false" ></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="form-group row">
                                                                                                <label for="inputpid" class="col-3 col-form-label">Catatan</label>
                                                                                                <div class="col-9">
                                                                                                    <asp:TextBox CssClass="auto-style1" ID="txtcatatansudah" required="required" runat="server" Height="103px" MaxLength="255" TextMode="MultiLine" Width="811px"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="form-group mb-0 justify-content-end row">
                                                                                                <div class="col-9">
                                                                                                    <asp:Button ID="btnsimpansudah" CssClass="btn btn-outline-primary" runat="server" Text="Simpan" OnClick="btnSimpansudah_Click" />
                                                                                                    <asp:LinkButton ID="btncancelsudah" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnCancelsudah_Click" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <!-- end card-box -->
                                                                    </div>
                                                                    <!-- end col -->
                                                                </div>
            </asp:Panel>   


        <asp:Panel ID="PnlHistory" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">History Follow Up</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 99%;">
                                                            <asp:GridView ID="GrdFollow" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" OnRowCommand="Grddatacabang_RowCommand">

                                                            <Columns>
                                                                                        <asp:TemplateField HeaderText="Id">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblNOb1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "idcalonanggota")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>  
                                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglfollowup")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>                                                                                           
                                                                                        <asp:TemplateField HeaderText="Entry User">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "userfollowup")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Jenis">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namafollowup")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="catatan">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblempidb21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "catatan")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    
                                                                                    </Columns>
                                                            </asp:GridView>
                                                 </div>
                                            </div>
                                                    <div class="form-group mb-0 justify-content-end row">
                                                   
                                                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnCancelHst_Click" />
                                                    
                                                    </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>

         </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsimpansudah" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
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



    <script src="../plugins/tablesaw/js/tablesaw.js"></script>
        <script src="../plugins/tablesaw/js/tablesaw-init.js"></script>



</html>

