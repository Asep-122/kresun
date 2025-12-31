<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PembatalanPencairan.aspx.cs" Inherits="AR_Process_PembatalanPencairan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pembatalan Pinjaman</title>
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


    <style type="text/css">
        .ui-autocomplete {
            max-height: 500px;
            overflow-y: auto;
            /* prevent horizontal scrollbar */
            overflow-x: hidden;
        }
        /* IE 6 doesn't support max-height
   * we use height instead, but this forces the menu to always be this tall
   */
        * html .ui-autocomplete {
            height: 500px;
        }

        .ui-autocomplete-loading {
            background: white url("../../assets/images/Cube-1s-30px.gif") right center no-repeat;
        }
    </style>

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
                    }
                });
            }, 0);

        }


        function sweetAlertsave(btnSimpanTolakan, errMsg) {
            if (btnSimpanTolakan.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSimpanTolakan.dataset.confirmed = false;
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
                        btnSimpanTolakan.dataset.confirmed = true;
                        btnSimpanTolakan.click();

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

        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

        function isnumerickey(e) {
            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {

                if (txt == 118) {
                    return true;
                }
                //  else {
                //alert("Input is Number only")
                return false;
                //  }

            }
            return true;
        }

        function Comma(Num) { //function to add commas to textboxes

            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;


            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');


            return x1 + x2;

        }

        function checkTextField1(field) {
            if (field.value == '' || field.value == 0 || field.value == '.') {
                field.value = 0;
            }


            var nilai = field.value.split(',').join('');
            var output = nilai;
            if (parseFloat(nilai)) {
                input = new String(nilai); // so you can perform string operations
                var parts = input.split("."); // remove the decimal part
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1").split("").reverse().join("");
                output = parts.join(".");
            }
            field.value = output;
        }

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });
            {
                $('#<%=txtalasantolak.ClientID%>').prop('maxlength', 150);

            }
            // Default Datatable
            $('#datatable').DataTable();
            //Buttons examples
            var table = $('#GRData').DataTable({
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf']
            });

            $('.selectpicker').selectpicker();

            $('#key-table').DataTable({
                keys: true
            });

            // Responsive Datatable
            $('#responsive-datatable').DataTable();

            $('#selection-datatable').DataTable({
                select: {
                    style: 'multi'
                }
            });
            table.buttons().container()
                .appendTo('#GRData_wrapper .col-md-6:eq(0)');
            $('.selectpicker').selectpicker();
            $(function () {
                var table = $('#GRData').dataTable({
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
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="PembatalanPencairan.aspx">Pembatalan Pinjaman</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Pembatalan Pinjaman</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <h4 class="m-t-0 header-title">Filter</h4>
                                    <p class="text-muted font-14 m-b-30">
                                        KeyTable provides Excel like cell navigation on any table. Events (focus, blur, action etc) can be assigned to individual cells, columns, rows or all cells.
                                    </p>


                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Panel Front -->
                    <%--<asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Pembatalan Pinjaman</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                     <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" >
                                    <Columns>
                                       <asp:TemplateField HeaderText="No. Urut">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="No. Anggota">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Register" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegister" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Idpermohonan")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Anggota">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamanasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Pihak Lain">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_pihaklain")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="No. TLP Pihak Lain">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltlp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tlp")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Alamat">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Permohonan Plafon">
                                            <ItemTemplate>
                                                <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "permohonan_plafon", "{0:N2}")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jangka">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "permohonan_jangka")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lama hari tidak Cair">
                                            <ItemTemplate>
                                                <asp:Label ID="lblselisih" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "selisih")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                           
                                        <asp:TemplateField HeaderText="Action">
                                                                            <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                      </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                                            
                                </div>
                            </div> 

                        </div>
                    </div>
                </div>

        </asp:Panel>--%>


                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">

                                    <h4 class="m-t-0 header-title">Data Pembatalan Pinjaman</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">

                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="887px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No. Urut">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Pengajuan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblalamat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgldrop","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Kode Produk">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. Anggota">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. Register" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRegister" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Idpermohonan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Anggota">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Usia">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Permohonan Plafon">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jangka">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
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



                    <%-- <asp:Panel ID="pnlPengajuan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENGAJUAN KREDIT NASABAH</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                       <asp:HiddenField ID="HDJenisPinjaman" runat="server"/> 
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label30" runat="server" class="col-form-label">No. Rekening Lama</asp:label>
																<asp:TextBox ID="txtnoreklama" Enabled="false"  CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label34" runat="server" class="col-form-label">Jenis Kredit</asp:label>
																<asp:TextBox ID="txtjeniskredit" Enabled="false"  CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label81" runat="server" class="col-form-label">Jenis Penggunaan</asp:label>
																<asp:TextBox ID="txtjenispenggunaan" Enabled="false"  CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>
                                                             
                                                            <div class="form-group">
                                                                 <asp:label id="Label35" runat="server" class="col-form-label">Tanggal Realisasi</asp:label>
                                                                 <asp:TextBox CssClass="form-control" Enabled="false"  ID="txttglrealisasi" TextMode="DateTime" runat="server"></asp:TextBox>
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label37" runat="server" class="col-form-label">Jenis Pinjaman</asp:label>
																<asp:TextBox ID="txtjenispinjaman" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label55" runat="server" class="col-form-label">Program Pinjaman</asp:label>
																<asp:TextBox ID="txtprogrampinjaman" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label56" runat="server" class="col-form-label">Sumber Dana</asp:label>
																<asp:TextBox ID="txtsumberdana" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>

															<div class="form-group">
                                                                <asp:label ID="Label38" runat="server" class="col-form-label">Plafon</asp:label>
																<asp:TextBox CssClass="form-control" Enabled ="false" ID="txtplafon" runat="server" ></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label39" runat="server" class="col-form-label">Jangka Waktu</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtjangka"  runat="server"  ></asp:TextBox>
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label40" runat="server" class="col-form-label">Bunga</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtbunga"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label41" runat="server" class="col-form-label">Persen Provisi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtProvisi"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label57" runat="server" class="col-form-label">Nominal Provisi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtnominalprovisi"    runat="server" ></asp:TextBox>
                                                            </div> 
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label60" runat="server" class="col-form-label">Angsuran Perbulan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtangsuranperbulan"    runat="server" ></asp:TextBox>
                                                            </div> 
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label61" runat="server" visible="false" class="col-form-label">Admin Pencairan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" visible="false" Enabled ="false" ID="txtadminPencairan"    runat="server" ></asp:TextBox>     
                                                            </div> 
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label62" runat="server" visible="false" class="col-form-label">Admin Angsuran</asp:label>
	                                                            <asp:TextBox CssClass="form-control" visible="false" Enabled ="false" ID="txtadminangsuran"   runat="server" ></asp:TextBox>     
                                                            </div> 
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label63" runat="server" visible="false" class="col-form-label">Sifat</asp:label>
																<asp:TextBox ID="txtsifat" Enabled ="false" visible="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label64" runat="server" class="col-form-label">AO</asp:label>
																<asp:TextBox ID="txtAO" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label65" runat="server" class="col-form-label">Ref Non AO</asp:label>
																<asp:TextBox ID="txtNonAO" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label67" runat="server" visible="false" class="col-form-label">No Tabungan Lain</asp:label>
	                                                            <asp:TextBox CssClass="form-control" visible="false" ID="txttablain" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 
                                                           
                                                            
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                             <div class="form-group">
                                                                <asp:label ID="Label66" runat="server" class="col-form-label">Sektor Ekonomi</asp:label>
																<asp:TextBox ID="txtsektorekonomi" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label68" runat="server" class="col-form-label">Golongan Debitur</asp:label>
																<asp:TextBox ID="txtgolongandebitur" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                 <asp:label id="Label42" runat="server"  class="col-form-label">No KTP Penjamin</asp:label>
                                                                 <asp:TextBox ID="txtktppenjamin"  Enabled ="false" CssClass="form-control" onkeypress="return isnumerickey(event);"    runat="server"   ></asp:TextBox>
                                                            </div>   

                                                              <div class="form-group">
                                                                <asp:label ID="Label48" runat="server" class="col-form-label">Nama Penjamin</asp:label>
																<asp:TextBox ID="TextBox13" Enabled ="false" CssClass="form-control"    runat="server" ></asp:TextBox>
                                                                 
                                                              </div>

                                                              <div class="form-group">
                                                                <asp:label ID="Label50" runat="server" class="col-form-label">Golongan Penjamin</asp:label>
																<asp:TextBox ID="txtgolonganpenjamin" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label52" runat="server" class="col-form-label">Pendapatan/Bulan</asp:label>
																<asp:TextBox ID="txtpendapatanperbulan" Enabled ="false" CssClass="form-control"      onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label53" runat="server" class="col-form-label">Fisik Jaminan</asp:label>
																<asp:TextBox ID="txtfisikjaminan" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>
                                                            
                                                           

                                                            <div class="form-group">
                                                                <asp:label ID="Label69" runat="server" class="col-form-label">Jaminan Tambahan</asp:label>
																<asp:TextBox ID="TextBox1" CssClass="form-control"  Enabled ="false"   runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label70" runat="server" class="col-form-label">Biaya Cadangan</asp:label>
																<asp:TextBox ID="TextBox3" CssClass="form-control" Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label71" runat="server" visible="false" class="col-form-label">Nilai Taksasi</asp:label>
																<asp:TextBox ID="TextBox5" CssClass="form-control" visible="false" Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label72" runat="server" class="col-form-label">Jatuh Tempo</asp:label>
																<asp:TextBox ID="txtjttempo" CssClass="form-control"  Enabled="false"    runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label73" runat="server" visible="false" class="col-form-label">Angsuran Dimuka</asp:label>
																<asp:TextBox ID="txtangsurandimuka" visible="false" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div>  

                                                            <div class="form-group">
                                                                <asp:label ID="Label74" runat="server" class="col-form-label">Biaya Materai</asp:label>
																<asp:TextBox ID="txtbymaterai" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label75" runat="server" visible="false" class="col-form-label">Biaya Asuransi</asp:label>
																<asp:TextBox ID="txtbyasuransi" visible="false" CssClass="form-control" Enabled="false"      onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label76" runat="server" visible="false" class="col-form-label">Biaya Pelunasan</asp:label>
																<asp:TextBox ID="txtbypelunasan" visible="false" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                              <div class="form-group">
                                                                <asp:label ID="Label77" runat="server" visible="false" class="col-form-label">Biaya Notaris</asp:label>
																<asp:TextBox ID="txtbynotaris" CssClass="form-control" visible="false"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label78" runat="server" visible="false" class="col-form-label">Blokir Angs</asp:label>
																<asp:TextBox ID="TextBox14" CssClass="form-control" visible="false" Enabled="false"      onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label79" runat="server" visible="false" class="col-form-label">Nominal Blokir Angs</asp:label>
																<asp:TextBox ID="TextBox15" CssClass="form-control" visible="false"  Enabled="false"     onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label80" runat="server" visible="false" class="col-form-label">Jenis Blokir</asp:label>
																<asp:TextBox ID="txtjenisblokir" visible="false" CssClass="form-control" Enabled ="false" runat="server" ></asp:TextBox> 
															 </div>

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            
    
                </span></span> 
    
            </asp:Panel> --%>


                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA ANGGOTA</div>
                                    <p class="m-b-0"></p>

                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <asp:HiddenField ID="HDStatusDebitur" runat="server" />
                                            <asp:HiddenField ID="HDKodePos" runat="server" />
                                            <asp:HiddenField ID="HDKodeArea" runat="server" />
                                            <asp:HiddenField ID="HDGender" runat="server" />
                                            <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                            <asp:HiddenField ID="HDLamaKerja" runat="server" />
                                            <asp:HiddenField ID="HDRegister" runat="server" />
                                            <asp:HiddenField ID="HDid" runat="server" />

                                            <div class="form-group">
                                                <asp:Label ID="Label43" runat="server" class="col-form-label">No. Anggota</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnoanggota" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                <asp:TextBox CssClass="form-control" ID="txtnorektabungan" Visible="false" runat="server" required="required"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" class="col-form-label">Nama Anggota</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnamanasabah" Enabled="false" runat="server" required="required"></asp:TextBox>
                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">

                                                    <asp:Label ID="Label91" runat="server" class="col-form-label">Tanggal Bergabung</asp:Label>
                                                    <asp:TextBox ID="txttglbergabung" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">


                                                    <asp:Label ID="Label118" runat="server" class="col-form-label">Simpanan Pokok</asp:Label>
                                                    <asp:TextBox ID="txtsimpok" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>



                                                </div>
                                                <div class="form-group col-md-4">


                                                    <asp:Label ID="Label126" runat="server" class="col-form-label">Simpanan Wajib</asp:Label>
                                                    <asp:TextBox ID="txtsimwa" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>




                                                </div>


                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label44" runat="server" class="col-form-label">Pendidikan</asp:Label>
                                                    <asp:DropDownList ID="ddlpendidikanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label31" runat="server" class="col-form-label">No.HP</asp:Label>
                                                    <asp:TextBox ID="txthpktp" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" required="required" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label11" runat="server" class="col-form-label">Email</asp:Label>
                                                    <asp:TextBox ID="txtemailsktp" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>


                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="Label4" runat="server" class="col-form-label">Alamat</asp:Label>
                                                <asp:TextBox ID="txtalamatktp" CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom"></asp:TextBox>
                                                <asp:TextBox CssClass="form-control" ID="txtrtktp" Visible="false" runat="server"></asp:TextBox>
                                                <asp:TextBox CssClass="form-control" ID="txtrwktp" Visible="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label2" runat="server" class="col-form-label">Kelurahan</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtkelurahanktp" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label45" runat="server" class="col-form-label">Kecamatan</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtkecamatanktp" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label46" runat="server" class="col-form-label">Kota</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtkotaktp" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="form-row">

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label125" runat="server" class="col-form-label">Provinsi</asp:Label>
                                                    <asp:DropDownList ID="ddlpropinsiktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label132" runat="server" class="col-form-label">kode Pos</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtkodeposktp" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                </div>

                                            </div>



                                        </div>
                                        <div class="form-group col-md-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" class="col-form-label">No. KTP</asp:Label>
                                                <asp:TextBox ID="txtnoidentitasktp" Enabled="false" CssClass="form-control" MaxLength="16" onkeypress="return isnumerickey(event);" required="required" runat="server"></asp:TextBox>
                                            </div>



                                            <div class="form-group">
                                                <!-- Simple card -->
                                                <div class="card m-b-30">
                                                    <asp:Image ID="imgktp" runat="server" class="card-img-top img-fluid" alt="Card image cap" />
                                                </div>
                                            </div>


                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label127" runat="server" class="col-form-label">Jenis Kelamin</asp:Label>
                                                    <asp:DropDownList ID="ddljenkelktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label3" runat="server" class="col-form-label">Tempat Lahir</asp:Label>
                                                    <asp:TextBox ID="txttempatlahirktp" CssClass="form-control" Enabled="false" required="required" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label5" runat="server" class="col-form-label">Tanggal Lahir</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txttgllahirktp" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label13" runat="server" class="col-form-label">Agama</asp:Label>
                                                    <asp:DropDownList ID="ddlAgamaktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label128" runat="server" class="col-form-label">Status Pernikahan</asp:Label>
                                                    <asp:DropDownList ID="ddlstatusnikahktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label129" runat="server" class="col-form-label">Pekerjaan</asp:Label>
                                                    <asp:DropDownList ID="ddlpekerjaanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label130" runat="server" class="col-form-label">Kewarganegaraan</asp:Label>
                                                    <asp:DropDownList ID="ddlkewarganegaraanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label22" runat="server" class="col-form-label">Masa Berakhir KTP</asp:Label>
                                                    <asp:TextBox ID="txtmasaberakhirKTP" Enabled="false" CssClass="form-control" runat="server" TextMode="DateTime"></asp:TextBox>
                                                </div>

                                            </div>






                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>





                    </asp:Panel>
                    <asp:Panel ID="pnlIdentitas" Visible="false" runat="server">

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">



                            <div class="row">

                                <div class="col-lg-6">
                                    <div class="col-lg-12">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA TEMPAT TINGGAL</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group">
                                                <asp:Label ID="Label7" runat="server" class="col-form-label">Status Rumah</asp:Label>
                                                <asp:DropDownList ID="ddlstatusrumah" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                <%--<asp:DropDownList ID="DropDownList1" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusrumah_SelectedIndexChanged"  ></asp:DropDownList>--%>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label8" runat="server" class="col-form-label">Alamat Rumah Tinggal</asp:Label>
                                                <asp:TextBox ID="txtalamatrumahtinggal1" Enabled="false" CssClass="form-control" Text="-" MaxLength="100" TextMode="MultiLine" runat="server"></asp:TextBox>

                                            </div>



                                            <div class="form-group">
                                                <asp:Label ID="Label106" runat="server" class="col-form-label">RT</asp:Label>
                                                <asp:TextBox ID="txtrttp" Enabled="false" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);" MaxLength="3" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label113" runat="server" class="col-form-label">RW</asp:Label>
                                                <asp:TextBox ID="txtrwtpt" Enabled="false" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);" MaxLength="3" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label114" runat="server" class="col-form-label">Kelurahan Tempat Tinggal</asp:Label>
                                                <asp:TextBox ID="txtkelurahantpt" Enabled="false" CssClass="form-control" Text="-" MaxLength="50" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label115" runat="server" class="col-form-label">Kecamatan Tempat Tinggal</asp:Label>
                                                <asp:TextBox ID="txtkecamatantpt" Enabled="false" CssClass="form-control" Text="-" MaxLength="50" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label116" runat="server" class="col-form-label">Provinsi Tempat Tinggal</asp:Label>
                                                <asp:DropDownList ID="ddlprovinsitpt" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label117" runat="server" class="col-form-label">Kode Pos Tempat Tinggal</asp:Label>
                                                <asp:TextBox ID="txtkodepostpt" Enabled="false" CssClass="form-control" MaxLength="6" Text="0" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>

                                            </div>



                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="col-lg-12">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">Data Upload</div>
                                            <p class="text-muted font-14 m-b-30"></p>

                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" class="col-form-label">Status dalam keluarga</asp:Label>
                                                <asp:DropDownList ID="ddlstatuskawinupload" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label107" runat="server" class="col-form-label">No.Identitas Pasangan</asp:Label>
                                                <asp:TextBox ID="txtidpasangan" Enabled="false" CssClass="form-control" MaxLength="16" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label108" runat="server" class="col-form-label">Nama Pasangan</asp:Label>
                                                <asp:TextBox ID="txtnamapasangan" Enabled="false" CssClass="form-control" MaxLength="70" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label109" runat="server" class="col-form-label">Tempat Lahir Pasangan</asp:Label>
                                                <asp:TextBox ID="txttempatlahirpasangan" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label110" runat="server" class="col-form-label">Tanggal Lahir Pasangan</asp:Label>
                                                <asp:TextBox ID="txttgllahirpasangan" Enabled="false" CssClass="form-control" runat="server" TextMode="DateTime"></asp:TextBox>
                                            </div>



                                            <div class="form-group">
                                                <!-- Simple card -->
                                                <div class="card m-b-30">
                                                    <asp:Image ID="ImageKtppasangan" runat="server" class="card-img-top img-fluid" alt="Card image cap" />
                                                </div>
                                            </div>



                                            <div class="form-group">
                                                <asp:Label ID="Label111" runat="server" class="col-form-label">Nomer NPWP</asp:Label>
                                                <asp:TextBox ID="txtNPWP" Enabled="false" CssClass="form-control" MaxLength="15" runat="server" Width="219px" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <!-- Simple card -->
                                                <div class="card m-b-30">
                                                    <asp:Image ID="Imagenpwp" Enabled="false" runat="server" class="card-img-top img-fluid" alt="Card image cap" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label112" runat="server" class="col-form-label">Nomer Kartu Keluarga (KK)</asp:Label>
                                                <asp:TextBox ID="txtnomerkk" Enabled="false" CssClass="form-control" MaxLength="16" runat="server" Width="219px" onkeypress="return isnumerickey(event);"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <!-- Simple card -->
                                                <div class="card m-b-30">
                                                    <asp:Image ID="Imagekk" runat="server" class="card-img-top img-fluid" alt="Card image cap" />
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>


                            </div>


                        </div>

                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="Button3" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackIdentitas_Click" />


                            </div>
                            <div class="float-right mb-2 ">

                                <asp:Button ID="Button4" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNexIdentitas_Click" Width="65px" />

                            </div>
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="pnlPekerjaan" Visible="false" runat="server">
                        <div class="clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA PEKERJAAN ANGGOTA</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">

                                        <div class="form-group col-md-6">


                                            <div class="form-group">
                                                <asp:Label ID="Label9" runat="server" class="col-form-label">Pekerjaan</asp:Label>
                                                <asp:DropDownList ID="ddlPekerjaan" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label12" runat="server" class="col-form-label">Nama Pekerjaan</asp:Label>
                                                <asp:TextBox ID="txtPekerjaan" Enabled="false" MaxLength="70" CssClass="form-control" runat="server" Text="-"></asp:TextBox>
                                                <%--<span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>--%>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label47" runat="server" class="col-form-label">Jabatan</asp:Label>
                                                <asp:TextBox ID="txtjabatan" Enabled="false" MaxLength="50" CssClass="form-control" runat="server" Text="-"></asp:TextBox>
                                                <%--<span class="font-14 text-muted">isi "-" jika bukan pekerja Formal--%>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label14" runat="server" class="col-form-label">Nama Perusahaan</asp:Label>
                                                <asp:TextBox ID="txtNamaPerusahaan" Enabled="false" MaxLength="100" CssClass="form-control" runat="server" Text="-"></asp:TextBox>
                                                <%--<span class="font-14 text-muted">isi "-" jika bukan pekerja Formal--%>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label15" runat="server" class="col-form-label">Bidang Usaha Perusahaan</asp:Label>
                                                <asp:TextBox ID="txtBidangUsaha" MaxLength="100" CssClass="form-control" runat="server" Text="-"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label49" runat="server" class="col-form-label">Lama Usaha/Bekerja</asp:Label>
                                                <asp:DropDownList ID="ddlLamaBekerja" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator46" ErrorMessage="Agama tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlLamaBekerja" Runat="server" />--%>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label51" runat="server" class="col-form-label">Kepemilikan Tempat Usaha</asp:Label>
                                                <asp:DropDownList ID="ddlkepemilikantempatusaha" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                <asp:TextBox ID="txtkepemilikantempatusaha" Enabled="false" MaxLength="50" CssClass="form-control" runat="server" Text="-"></asp:TextBox>
                                                <%--<span class="font-14 text-muted">isi "-" jika tidak perlu</span>--%>
                                            </div>


                                        </div>
                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label16" runat="server" class="col-form-label">Alamat Perusahaan</asp:Label>
                                                <asp:TextBox ID="txtalamatpekerjaan" Enabled="false" MaxLength="100" CssClass="form-control" runat="server" Text="-" TextMode="MultiLine"></asp:TextBox>
                                                <%--<span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>--%>
                                            </div>


                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label17" runat="server" class="col-form-label">Kabupaten</asp:Label>
                                                    <asp:TextBox ID="txtkabupatenpekerjaan" Enabled="false" MaxLength="50" CssClass="form-control" runat="server" Text="-"></asp:TextBox>
                                                    <%--<span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>--%>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label18" runat="server" class="col-form-label">Kelurahan</asp:Label>
                                                    <asp:TextBox ID="txtkelurahanpekerjaan" Enabled="false" MaxLength="50" CssClass="form-control" runat="server" Text="-"></asp:TextBox>
                                                    <%--<span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>--%>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label19" runat="server" class="col-form-label">Kecamatan</asp:Label>
                                                    <asp:TextBox ID="txtkecamatanpekerjaan" Enabled="false" MaxLength="50" CssClass="form-control" runat="server" Text="-"></asp:TextBox>
                                                    <%--<span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label58" runat="server" class="col-form-label">Provinsi</asp:Label>
                                                    <asp:DropDownList ID="ddlprovinsipekerjaan" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                    <%--<span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>--%>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label59" runat="server" class="col-form-label">Kode Pos</asp:Label>
                                                    <asp:TextBox ID="txtkodepospekerjaan" Enabled="false" MaxLength="6" CssClass="form-control" onkeypress="return isnumerickey(event);" Text="0" runat="server"></asp:TextBox>
                                                    <%--<span class="font-14 text-muted">isi "0" jika bukan pekerja Formal</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label20" runat="server" class="col-form-label">Telepon</asp:Label>
                                                <asp:TextBox ID="txtteleponpekerjaan" Enabled="false" MaxLength="13" CssClass="form-control" onkeypress="return isnumerickey(event);" runat="server" Text="0"></asp:TextBox>
                                                <%--<span class="font-14 text-muted">isi "0" jika bukan pekerja Formal</span>--%>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label92" runat="server" class="col-form-label">Fax</asp:Label>
                                                <asp:TextBox ID="txtFaxPekerjaan" Enabled="false" MaxLength="10" CssClass="form-control" onkeypress="return isnumerickey(event);" runat="server" Text="0"></asp:TextBox>
                                                <%--<span class="font-14 text-muted">isi "0" jika bukan pekerja Formal</span>--%>
                                            </div>


                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="btnBackPekerjaan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPekerjaan_Click" />


                            </div>
                            <div class="float-right mb-2 ">

                                <asp:Button ID="btnNextPekerjaan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPekerjaan_Click" Width="65px" />

                            </div>
                        </div>


                    </asp:Panel>

                    <asp:Panel ID="pnlTambahan" Visible="false" runat="server">

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">



                            <div class="row">

                                <div class="col-lg-6">
                                    <div class="col-lg-12">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA Tambahan Anggota</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group">
                                                <asp:Label ID="Label87" runat="server" class="col-form-label">Alamat Surat menyurat</asp:Label>
                                                <asp:DropDownList ID="ddlAlamatSurat" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label81" runat="server" class="col-form-label">Jenis Penggunaan</asp:Label>
                                                <asp:DropDownList ID="ddlPenggunaan" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label68" runat="server" class="col-form-label">Golongan Debitur</asp:Label>
                                                <asp:DropDownList ID="ddlgolongandebitur" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label93" runat="server" class="col-form-label">Nama Ibu Kandung</asp:Label>
                                                <asp:TextBox ID="txtnamaibukandung" Enabled="false" MaxLength="70" CssClass="form-control" runat="server"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator34" runat="server" SetFocusOnError="True" ControlToValidate="txtnamaibukandung"  ErrorMessage="Nama ibu kandung tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label94" runat="server" class="col-form-label">Jumlah Anak</asp:Label>
                                                <asp:TextBox ID="txtjumlahanak" Enabled="false" MaxLength="2" CssClass="form-control" onkeypress="return isnumerickey(event);" required="required" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="col-lg-12">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">Data Penjamin</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group">
                                                <asp:Label ID="Label42" runat="server" class="col-form-label">No KTP Penjamin</asp:Label>
                                                <asp:TextBox ID="txtktppenjamin" Enabled="false" MaxLength="16" CssClass="form-control" onkeypress="return isnumerickey(event);" runat="server" Text="0"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" SetFocusOnError="True" ControlToValidate="txtktppenjamin"  ErrorMessage="KTP Penjamin tidak boleh kosong" Text="-"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label48" runat="server" class="col-form-label">Nama Penjamin</asp:Label>
                                                <asp:TextBox ID="txtnamapenjamin" CssClass="form-control" MaxLength="80" Enabled="false" runat="server" Text="-"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapenjamin"  ErrorMessage="Nama Penjamin tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label50" runat="server" class="col-form-label">Golongan Penjamin</asp:Label>
                                                <asp:DropDownList ID="ddlgolonganpenjamin" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label52" runat="server" class="col-form-label">Pendapatan/Bulan Penjamin</asp:Label>
                                                <asp:TextBox ID="txtpendapatanperbulan" Enabled="false" CssClass="form-control" MaxLength="18" runat="server" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" Text="0"></asp:TextBox>
                                                <asp:TextBox ID="txtjaminantambahan" Visible="false" CssClass="form-control" MaxLength="100" runat="server" Text="-"></asp:TextBox>
                                                <asp:DropDownList ID="ddlfisikjaminan" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>














                                        </div>
                                    </div>
                                </div>


                            </div>


                        </div>

                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="btnBackTambahan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackTambahan_Click" />


                            </div>
                            <div class="float-right mb-2 ">

                                <asp:Button ID="btnNextTambahan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextTambahan_Click" Width="65px" />

                            </div>
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="pnlPendapatan" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA PENDAPATAN ANGGOTA</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">

                                        <div class="form-group col-md-6">


                                            <div class="form-group">
                                                <asp:Label ID="Label21" runat="server" class="col-form-label">Pendapatan Bersih Usaha</asp:Label>
                                                <asp:TextBox ID="txtpendapatanbersih" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" MaxLength="16">0</asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label23" runat="server" class="col-form-label">Pendapatan Selain Usaha</asp:Label>
                                                <asp:TextBox ID="txtpendapatanselain" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lbltotalpendapatan" runat="server" class="col-form-label">Total Pendapatan</asp:Label>
                                                <asp:TextBox ID="txttotalpendapatan" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                            </div>


                                        </div>
                                        <div class="form-group col-md-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label24" runat="server" class="col-form-label">Pengeluaran Rumah Tangga</asp:Label>
                                                <asp:TextBox ID="txtpengeluaranrumahtangga" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label25" runat="server" class="col-form-label">Tagihan PLN, Listrik, PDAM, Telepon</asp:Label>
                                                <asp:TextBox ID="txttagihanlistrik" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom">0</asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label27" runat="server" class="col-form-label">Pinjaman / Kewajiban Lainnya</asp:Label>
                                                <asp:TextBox ID="txtpinjamanlain" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom">0</asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label28" runat="server" class="col-form-label">Rekomendasi Angsuran</asp:Label>
                                                <asp:TextBox ID="txtangsuran" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom">0</asp:TextBox>
                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="lbltotalPengeluaran" runat="server" class="col-form-label"></asp:Label>
                                                <asp:TextBox ID="txttotal" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" Text="0"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblsisapenghasilan" runat="server" class="col-form-label"></asp:Label>
                                                <asp:TextBox ID="txtSisaPenghasilan" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" Text="0"></asp:TextBox>

                                            </div>

                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="btnBackPendapatan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPendapatan_Click" />


                            </div>
                            <div class="float-right mb-2 ">

                                <asp:Button ID="btnNextPendapatan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPendapatan_Click" Width="65px" />

                            </div>
                        </div>

                        </span></span> 
    
                    </asp:Panel>

                    <asp:Panel ID="pnlPihakYgDapatDiHubungi" Visible="false" runat="server">

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">



                            <div class="row">

                                <div class="col-lg-6">
                                    <div class="col-lg-12">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA PIHAK LAIN YANG DAPAT DI HUBUNGI</div>
                                            <p class="text-muted font-14 m-b-30"></p>

                                            <div class="form-group">
                                                <asp:Label ID="Label53" runat="server" class="col-form-label">Nama</asp:Label>
                                                <asp:TextBox ID="txtnamapihaklain" Enabled="false" MaxLength="70" CssClass="form-control" runat="server" Text="-"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label54" runat="server" class="col-form-label">Hubungan</asp:Label>
                                                <asp:DropDownList ID="ddlHubunganPihakLain" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label69" runat="server" class="col-form-label">Alamat </asp:Label>
                                                <asp:TextBox ID="txtalamatPihakLain" Enabled="false" MaxLength="250" CssClass="form-control" Text="-" runat="server" TextMode="MultiLine" Columns="55"></asp:TextBox>

                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label95" runat="server" class="col-form-label">RT</asp:Label>
                                                    <asp:TextBox ID="txtRTPihakLain" Enabled="false" CssClass="form-control" MaxLength="3" Text="0" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>

                                                </div>


                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label96" runat="server" class="col-form-label">RW</asp:Label>
                                                    <asp:TextBox ID="txtRWPihakLain" Enabled="false" CssClass="form-control" MaxLength="3" Text="0" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="form-row">

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label97" runat="server" class="col-form-label">Kelurahan</asp:Label>
                                                    <asp:TextBox ID="txtKelurahanPihakLain" Enabled="false" MaxLength="50" CssClass="form-control" Text="-" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label98" runat="server" class="col-form-label">Kecamatan</asp:Label>
                                                    <asp:TextBox ID="txtKecamatanPihakLain" Enabled="false" MaxLength="50" CssClass="form-control" Text="-" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label99" runat="server" class="col-form-label">Kota</asp:Label>
                                                    <asp:TextBox ID="txtKotaPihakLain" Enabled="false" MaxLength="50" CssClass="form-control" Text="-" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <asp:Label ID="Label100" runat="server" class="col-form-label">Provinsi</asp:Label>
                                                    <asp:DropDownList ID="ddlProvinsiPihakLain" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label101" runat="server" class="col-form-label">Kode Pos</asp:Label>
                                                    <asp:TextBox ID="txtKodePosPihakLain" Enabled="false" MaxLength="6" CssClass="form-control" Text="0" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label102" runat="server" class="col-form-label">No.Telepon</asp:Label>
                                                <asp:TextBox ID="txtTlpPihakLain" Enabled="false" MaxLength="13" CssClass="form-control" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>

                                            </div>










                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="col-lg-12">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA KRITERIA USAHA ANGGOTA</div>
                                            <p class="text-muted font-14 m-b-30"></p>




                                            <asp:HiddenField ID="HiddenField1" runat="server" />


                                            <div class="form-group">
                                                <asp:Label ID="Label119" runat="server" class="col-form-label">Kondisi Usaha Saat Ini</asp:Label>
                                                <asp:TextBox ID="txtkondisiusaha" Enabled="false" CssClass="form-control" runat="server" MaxLength="200">-</asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label120" runat="server" class="col-form-label">Karakter Anggota</asp:Label>
                                                <asp:TextBox ID="txtKarakterAnggota" Enabled="false" CssClass="form-control" runat="server" MaxLength="200">-</asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label121" runat="server" class="col-form-label">Info Anggota dari Pihak ke 3</asp:Label>
                                                <asp:TextBox ID="txtinfo" Enabled="false" CssClass="form-control" runat="server" MaxLength="200">-</asp:TextBox>
                                            </div>




                                            <div class="form-group">
                                                <asp:Label ID="Label122" runat="server" class="col-form-label">Kondisi Pesaing</asp:Label>
                                                <asp:TextBox ID="txtkondisipesaing" Enabled="false" CssClass="form-control" runat="server" MaxLength="200">-</asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label123" runat="server" class="col-form-label">Resiko Usaha</asp:Label>
                                                <asp:TextBox ID="txtresikousaha" Enabled="false" CssClass="form-control" runat="server" MaxLength="200">-</asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label124" runat="server" class="col-form-label">Upaya Mengatasi Resiko Usaha</asp:Label>
                                                <asp:TextBox ID="txtatasiresiko" Enabled="false" CssClass="form-control" runat="server" MaxLength="250">-</asp:TextBox>

                                            </div>




                                        </div>
















                                    </div>

                                </div>


                            </div>


                        </div>

                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="btnBackPihakLain" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPihakLain_Click" />


                            </div>
                            <div class="float-right mb-2 ">

                                <asp:Button ID="btnNextPihakLain" align="right" class="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPihakLain_Click" />


                            </div>
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="pnlJaminan" Visible="false" runat="server">
                        <div class="clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA JAMINAN DAN AGUNAN</div>
                                    <p class="m-b-0"></p>


                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label135" runat="server" class="col-form-label">Jaminan</asp:Label>
                                                <asp:DropDownList ID="ddljaminan" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom">
                                                    <asp:ListItem Value="Akte Lahir,Buku Nikah dan Ijazah">Akte Lahir,Buku Nikah dan Ijazah</asp:ListItem>
                                                    <asp:ListItem Value="Akte Lahir dan Buku Nikah">Akte Lahir dan Buku Nikah</asp:ListItem>
                                                    <asp:ListItem Value="Akte Lahir dan Ijazah">Akte Lahir dan Ijazah</asp:ListItem>
                                                    <asp:ListItem Value="Buku Nikah dan Ijazah">Buku Nikah dan Ijazah</asp:ListItem>
                                                    <asp:ListItem Value="Akte Lahir">Akte Lahir</asp:ListItem>
                                                    <asp:ListItem Value="Buku Nikah">Buku Nikah</asp:ListItem>
                                                    <asp:ListItem Value="Ijazah">Ijazah</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">


                                                <asp:Label ID="Label85" runat="server" class="col-form-label">Agunan</asp:Label>
                                                <asp:FileUpload ID="fileuploadjaminan" runat="server" />
                                                <%--<asp:Button ID="btnjaminan" Text="Upload" runat="server" OnClick="UploadFileJaminan" OnClientClick="return CheckFileExistenceJaminan()" />--%>
                                                <hr />
                                                <asp:TextBox ID="txtpathjaminan" CssClass="form-control" Enabled="false" runat="server" MaxLength="400"></asp:TextBox>

                                                <hr />
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label141" runat="server" class="col-form-label">Keterangan Agunan</asp:Label>
                                                <asp:TextBox ID="txtketeranganJaminan" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="6" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label26" runat="server" class="col-form-label">Kondisi Agunan</asp:Label>
                                                <asp:TextBox ID="txtkondisiJaminan" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="6" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label131" runat="server" class="col-form-label">Taksasi</asp:Label>
                                                <asp:TextBox ID="txttaksasi" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                            </div>





                                        </div>

                                        <div class="form-group col-md-6">


                                            <div class="form-group">
                                                <!-- Simple card -->
                                                <div class="card m-b-30">
                                                    <asp:Image ID="imgjaminan" runat="server" class="card-img-top img-fluid" />
                                                </div>
                                            </div>







                                        </div>




                                    </div>



                                </div>
                            </div>

                            <div class="hidden-print mt-4">
                                <div class="float-left mb-2">


                                    <asp:Button ID="Button7" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackJaminan_Click" />


                                </div>
                                <div class="float-right mb-2 ">

                                    <asp:Button ID="Button8" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextJaminan_Click" Width="65px" />


                                </div>
                            </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlPengajuan" Visible="false" runat="server">
                        <div class="clearfix">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Detail Pencairan</div>
                                        <p class="m-b-0"></p>

                                        <div class="form-row">

                                            <div class="form-group col-md-6">

                                                <form class="form-horizontal" role="form">
                                                    <asp:DropDownList ID="ddlmajuhari" Visible="false" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        <asp:ListItem Value="0">0 Hari</asp:ListItem>
                                                        <asp:ListItem Value="1">1 Hari</asp:ListItem>
                                                        <asp:ListItem Value="2">2 Hari</asp:ListItem>
                                                        <asp:ListItem Value="3">3 Hari</asp:ListItem>
                                                        <asp:ListItem Value="4">4 Hari</asp:ListItem>
                                                        <asp:ListItem Value="5">5 Hari</asp:ListItem>
                                                        <asp:ListItem Value="6">6 Hari</asp:ListItem>
                                                        <asp:ListItem Value="7">7 Hari</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">No Anggota</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblNoAnggota" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label" for="example-email">Nama Anggota</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblNama" Visible="true" runat="server" class="col-form-label"></asp:Label>

                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">KTP Penjamin</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblktppenjamin" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Nama Penjamin</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblnamapenjamin" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-4 col-form-label">Nama Ibu Kandung</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblnamaibukandung" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Jaminan Surat</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lbljaminansurat" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Agunan</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblagunan" multiline="true" rows="2" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Kondisi Agunan</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblkondisiagunan" multiline="true" rows="2" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Taksasi</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="Lbltaksasi" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>



                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Account Officer</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblao" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Tanggal Realisasi</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lbltglRealisasi" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Jangka Waktu</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lbljangkawaktu" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Jatuh Tempo</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lbljthtempo" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Bunga</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblbunga" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Maju Hari Tagih</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblmajuhari" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>


                                                </form>



                                            </div>

                                            <div class="form-group col-md-6">
                                                <form class="form-horizontal" role="form">
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Jenis Kredit</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lbljeniskredit" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <asp:Label ID="lblangsuran" class="col-4 col-form-label" runat="server"></asp:Label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblangsuranbulan" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="col-4 col-form-label">Besar Pinjaman</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblbesarpinjaman" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Potong / Biaya</label>
                                                        <%-- <div class="col-10">
                                                    <asp:label ID="lblpotongbiaya" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>--%>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Administrasi 3%</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lbladministrasi" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Materai</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblmaterai" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-4 col-form-label">Cadangan Angsuran</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="LblPotonganangsuran" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-3 col-form-label">Anggota</label>
                                                        <div class="col-8">
                                                            <asp:Label ID="LblAnggota" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-4 col-form-label">Total Potong /Biaya </label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblTotalPotongbiaya" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-4 col-form-label">Yang di terima Anggota </label>
                                                        <div class="col-8">
                                                            <asp:Label ID="lblygditerimaanggota" Visible="true" runat="server" class="col-form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </form>

                                            </div>



                                        </div>



                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="btnCancel" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />


                            </div>

                            <div class="float-right mb-2 ">

                                <div class="button-list">

                                    <asp:Button ID="btnTolak" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BATALKAN PENCAIRAN" OnClick="btnTolak_Click" />

                                </div>
                            </div>
                        </div>

                        </span></span> 
    
                    </asp:Panel>








                    <asp:Panel ID="pnlAlasanTolak" Visible="false" runat="server">
                        <div class="clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <p class="m-b-0"></p>
                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label86" runat="server" class="col-form-label">Alasan Pembatalan</asp:Label>
                                                <asp:TextBox ID="txtalasantolak" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server">-</asp:TextBox>
                                            </div>



                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="btnBackTolak" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackTolak_Click" />


                            </div>
                            <div class="float-right mb-2 ">

                                <div class="button-list">

                                    <asp:Button ID="btnSimpanTolakan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="PROSES PEMBATALAN" OnClick="btnSimpanTolakan_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menolak data ini ?');" />

                                </div>
                            </div>
                        </div>

                        </span></span> 
    
                    </asp:Panel>

                </div>

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnCancel" />
                <%--<asp:PostBackTrigger ControlID="btnNext" />--%>
                <asp:PostBackTrigger ControlID="btnTolak" />
                <asp:PostBackTrigger ControlID="btnSimpanTolakan" />
                <%--<asp:PostBackTrigger ControlID="btnSetuju" />--%>
                <asp:PostBackTrigger ControlID="GRData" />

                <asp:PostBackTrigger ControlID="Button7" />
                <asp:PostBackTrigger ControlID="btnBackPekerjaan" />
                <asp:PostBackTrigger ControlID="btnBackTambahan" />
                <asp:PostBackTrigger ControlID="btnBackPendapatan" />

                <asp:PostBackTrigger ControlID="btnCancel" />
                <%--<asp:PostBackTrigger ControlID="btnNext" />--%>
                <asp:PostBackTrigger ControlID="Button8" />
                <asp:PostBackTrigger ControlID="btnNextPekerjaan" />
                <asp:PostBackTrigger ControlID="btnNextPendapatan" />

            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>

<!-- Parsley js -->
<script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>

<script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

<!-- Sweet Alert Js  -->
<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>
<!-- Tooltipster js -->
<script src="../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>

<!-- App js -->
<script src="../../assets/js/jquery.app.js"></script>



<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>


</html>
