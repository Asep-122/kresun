<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersetujuanPinjamanBank.aspx.cs" Inherits="AR_Process_PermohonanPinjamanKreditMultiguna" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Persetujuan Kredit Bank</title>
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
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                        }
                    }
                });
            }, 0);

        }


          function sweetAlertsave(btnSetuju,errMsg) {
              if (Button4.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                  Button4.dataset.confirmed = false;
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
                        btnSetuju.dataset.confirmed = true;
                        btnSetuju.click();
                  
                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
                }
        }

        function sweetAlerttolak(btnSimpanTolakan,errMsg) {
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

        
        function checkSpecialKeys(e)
        {
            if(e.keyCode !=8 && e.keyCode!=46 && e.keyCode!=37 && e.keyCode!=38 && e.keyCode!=39 && e.keyCode!=40)
                return false;
            else
                return true;
        }
         function checkTextAreaMaxLength(textBox,e, length)
        {

                var mLen = textBox["MaxLength"];
                if(null==mLen)
                    mLen=length;

                var maxLength = parseInt(mLen);
                if(!checkSpecialKeys(e))
                {
                 if(textBox.value.length > maxLength-1)
                 {
                    if(window.event)//IE
                      e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                 }
            }   
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
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            
            $('#txttglptkp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

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
                                        <li class="breadcrumb-item"><a href="#">Kresun</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="PersetujuanPinjamanBank.aspx">Persetujuan Kredit Bank</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Persetujuan Kredit Bank</h4>
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
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">

                                    <h4 class="m-t-0 header-title">Data Persetujuan Pinjaman</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="887px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID Permohonan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id_permohonan")%>'></asp:Label>
                                                            <asp:Label ID="lblcabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_cabang")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Nasabah">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kode Produk">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkdproduk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Nasabah">
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
                                                            <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "permohonan_plafon", "{0:N2}")%>'></asp:Label>
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
                                                             <asp:LinkButton ID="BtnPDF" Visible="True" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                       
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
                    <!-- Panel Edit -->
                    <asp:Panel ID="pnl1" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA NASABAH</div>
                                    <p class="m-b-0"></p>

                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                            <asp:HiddenField ID="HiddenField5" runat="server" />
                                            <asp:HiddenField ID="HiddenField6" runat="server" />
                                            <asp:HiddenField ID="HiddenField7" runat="server" />


                                            <div class="form-group">
                                                <asp:Label ID="Label26" runat="server" class="col-form-label">No. Nasabah</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="TextBox1" Enabled="false" runat="server"></asp:TextBox>
                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="Label38" runat="server" class="col-form-label">No. Identitas</asp:Label>
                                                <asp:TextBox ID="txtidentitas" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label59" runat="server" class="col-form-label">Nama Nasabah</asp:Label>
                                                <asp:TextBox ID="TextBox2" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label30" runat="server" class="col-form-label">Nama Ibu Kandung</asp:Label>
                                                <asp:TextBox ID="txtnamaibukandung" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label85" runat="server" class="col-form-label">Nama Pasangan</asp:Label>
                                                <asp:TextBox ID="txtnamapasangan" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label53" runat="server" class="col-form-label">Jenis Kelamin</asp:Label>
                                                    <asp:TextBox ID="txtjenkel" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label32" runat="server" class="col-form-label">Tempat Lahir</asp:Label>
                                                    <asp:TextBox ID="txtempatlahir" Enabled="false" CssClass="form-control" runat="server" TextMode="DateTime"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label33" runat="server" class="col-form-label">Tanggal Lahir</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="TextBox3" Enabled="false" runat="server"></asp:TextBox>
                                                </div>


                                                <div class="form-group">
                                                    <asp:Label ID="Label29" runat="server" class="col-form-label">NPWP</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtnpwp" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label34" runat="server" class="col-form-label">Alamat</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="TextBox4" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'150');" Rows="3" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-6">

                                            <div class="form-row">

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label35" runat="server" class="col-form-label">Kelurahan</asp:Label>
                                                    <asp:TextBox ID="TextBox5" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label36" runat="server" class="col-form-label">Kecamatan</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="TextBox6" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label37" runat="server" class="col-form-label">Kota</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtkota" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label57" runat="server" class="col-form-label">Kode Pos</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="TextBox7" Enabled="false" runat="server"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label39" runat="server" class="col-form-label">Kode Produk</asp:Label>
                                                <asp:TextBox ID="txtkdproduk" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label40" runat="server" class="col-form-label">Program Pinjaman</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtprogrampinjaman" Enabled="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label41" runat="server" class="col-form-label">Tanggal Pengajuan</asp:Label>
                                                <asp:TextBox ID="txtrealisasi" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="Label55" runat="server" class="col-form-label">Tanggal Lahir Asuransi</asp:Label>
                                                <asp:TextBox ID="txttgllahirasuransi" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label42" runat="server" class="col-form-label">Kode Referensi</asp:Label>
                                                <asp:TextBox ID="txtAO" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label47" runat="server" class="col-form-label">Referensi Non Marketing</asp:Label>
                                                <asp:TextBox ID="txtnonAO" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>


                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>



                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">

                                <asp:Button ID="Button1" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack_Click" Width="100px" />



                            </div>

                            <div class="float-right mb-2 ">


                                <asp:Button ID="btnNext" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext_Click" Width="100px" />

                            </div>
                        </div>



                    </asp:Panel>


                    <!-- Panel Edit -->

                    <!-- Panel Edit -->
                    <asp:Panel ID="pnl2" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA PENSIUN</div>
                                    <p class="m-b-0"></p>

                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <asp:HiddenField ID="HDStatusDebitur" runat="server" />
                                            <asp:HiddenField ID="HiddenField8" runat="server" />
                                            <asp:HiddenField ID="HiddenField9" runat="server" />
                                            <asp:HiddenField ID="HDGender" runat="server" />
                                            <asp:HiddenField ID="HiddenField10" runat="server" />
                                            <asp:HiddenField ID="HDNoSimulasi" runat="server" />
                                            <asp:HiddenField ID="HDNoMemo" runat="server" />



                                            <div class="form-group">
                                                <asp:Label ID="Label48" runat="server" class="col-form-label">Status Pensiun</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtstspens" onkeypress="return isnumerickey(event);" Enabled="false" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label56" runat="server" class="col-form-label">No. KTP</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtktp" onkeypress="return isnumerickey(event);" Enabled="false" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label60" runat="server" class="col-form-label">Nama Pensiun</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnamapensiun" Enabled="false" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label61" runat="server" class="col-form-label">NIP</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnip" onkeypress="return isnumerickey(event);" runat="server" Enabled="false"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label62" runat="server" class="col-form-label">Nopen</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="TextBox8" onkeypress="return isnumerickey(event);" runat="server" Enabled="false"></asp:TextBox>

                                            </div>



                                            <div class="form-group">
                                                <asp:Label ID="Label63" runat="server" class="col-form-label">Jenis Kelamin</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtjeniskelamin" runat="server" Enabled="false"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label64" runat="server" class="col-form-label">Status Pernikahan</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtstatunikah" runat="server" Enabled="false"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">




                                            <div class="form-group">
                                                <asp:Label ID="Label65" runat="server" class="col-form-label">Kode Jiwa</asp:Label>
                                                <asp:DropDownList ID="ddlkodejiwa" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label66" runat="server" class="col-form-label">Tanggal Lahir</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txttgllahirpensiun" Enabled="false" runat="server"></asp:TextBox>


                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label67" runat="server" class="col-form-label">Tanggal Pensiun / TMT</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtpensiun" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label68" runat="server" class="col-form-label">Tanggal Menikah</asp:Label>
                                                <asp:TextBox ID="txtmenikah" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label69" runat="server" class="col-form-label">Tanggal Lahir anak ke 1</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtanak1" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label70" runat="server" class="col-form-label">Tanggal Lahir anak ke 2</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtanak2" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>
                                            </div>



                                            <div class="form-group">
                                                <asp:Label ID="Label71" runat="server" class="col-form-label">Tanggal Lahir anak ke 3</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtanak3" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>

                                            </div>




                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>

                    </asp:Panel>


                    <!-- Panel Edit -->
                    <asp:Panel ID="pnlSK" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA SK PENSIUN</div>
                                    <p class="m-b-0"></p>

                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <asp:HiddenField ID="HDKodeJiwa" runat="server" />
                                            <asp:HiddenField ID="HDKodeProduk" runat="server" />
                                            <asp:HiddenField ID="HDJenisPensiun" runat="server" />
                                            <asp:HiddenField ID="HDSumberDana" runat="server" />
                                            <asp:HiddenField ID="HDBunga" runat="server" />
                                            <asp:HiddenField ID="HDTglJt" runat="server" />
                                            <asp:HiddenField ID="HDMemo" runat="server" />
                                            <asp:HiddenField ID="HDAO" runat="server" />

                                            <div class="form-group">
                                                <asp:Label ID="Label99" runat="server" class="col-form-label">NO SK</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnosk" Enabled="false" runat="server"></asp:TextBox>

                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="Label100" runat="server" class="col-form-label">Nama Sesuai SK</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnamasesuaisk" Enabled="false" runat="server"></asp:TextBox>

                                            </div>




                                        </div>

                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label101" runat="server" class="col-form-label">Tanggal SK Pensiun</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txttglsk" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label102" runat="server" class="col-form-label">No. Dapem</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnodapem" Enabled="false" runat="server"></asp:TextBox>

                                            </div>

                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">
                                <asp:Button ID="btnBack2" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack2_Click" />
                            </div>
                            <div class="float-right mb-2 ">
                                <asp:Button ID="btnNext2" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext2_Click" Width="65px" />
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnl5" Visible="false" runat="server">
                        <div class="clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA PENERIMA PENSIUN</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label97" runat="server" class="col-form-label">Penerima Pensiun</asp:Label>
                                                <asp:TextBox ID="txtpenerimapensiun" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label88" runat="server" class="col-form-label">Nama</asp:Label>
                                                <asp:TextBox ID="txtPekerjaan" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label89" runat="server" class="col-form-label">Tanggal Lahir</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="TextBox9" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label72" runat="server" class="col-form-label">Status Perkawinan</asp:Label>
                                                <asp:TextBox ID="txtstatuskwn" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label91" runat="server" class="col-form-label">Alamat </asp:Label>
                                                <asp:TextBox ID="TextBox10" CssClass="form-control" Enabled="false" onkeyDown="checkTextAreaMaxLength(this,event,'150');" TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label92" runat="server" class="col-form-label">Kelurahan</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="TextBox11" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label73" runat="server" class="col-form-label">Kecamatan</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="TextBox12" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label96" runat="server" class="col-form-label">Kota</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtkotapekerjaan" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label98" runat="server" class="col-form-label">Kode Pos</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtkodepospekerjaan" Enabled="false" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label103" runat="server" class="col-form-label">Telepon</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txttlp" Enabled="false" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">
                                <asp:Button ID="btnback5" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnback5_Click" />
                            </div>
                            <div class="float-right mb-2 ">
                                <asp:Button ID="btnnext5" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnnext5_Click" Width="65px" />
                            </div>
                        </div>
                        </span></span> 
                    </asp:Panel>
                    <asp:Panel ID="pnl4" Visible="false" runat="server">
                        <div class="clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA PENGAJUAN KREDIT NASABAH</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">

                                        <asp:HiddenField ID="HDJenisPinjaman" runat="server" />
                                        <div class="form-group col-md-6">


                                            <div class="form-group">
                                                <asp:Label ID="Label115" runat="server" class="col-form-label">Kode Produk</asp:Label>
                                                <asp:TextBox ID="txtkodeproduk" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>

                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="Label116" runat="server" class="col-form-label">Program Pinjaman</asp:Label>
                                                <asp:TextBox ID="txtprogpinj" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label117" runat="server" class="col-form-label">Sumber Dana</asp:Label>
                                                <asp:DropDownList ID="ddlsumberdana" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label118" runat="server" class="col-form-label">Permohonan Kredit</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="TextBox34" Enabled="false"  runat="server"  onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label119" runat="server" class="col-form-label">Persetujuan Bendahara</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtbendahara" Enabled="false" runat="server"   onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label120" runat="server" class="col-form-label">Jangka Waktu (Bulan)</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="TextBox35" Enabled="false" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label121" runat="server" class="col-form-label">Persen Gaji</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtpersengaji" Enabled="false" runat="server" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label122" runat="server" class="col-form-label">Usia</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtusia" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label123" runat="server" class="col-form-label">Status Take Over</asp:Label>

                                                <asp:TextBox CssClass="form-control" ID="txtTO" Enabled="false" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label124" runat="server" class="col-form-label">Jenis Penggunaan</asp:Label>
                                                <asp:DropDownList ID="ddlPenggunaan" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label125" runat="server" class="col-form-label">Tanggal Realisasi</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txttglrealisasi" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label126" runat="server" class="col-form-label">Tanggal Jatuh Tempo</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="TextBox36" Enabled="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label127" runat="server" class="col-form-label">Persen Provisi</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtProvisi" Enabled="false" runat="server"></asp:TextBox>
                                                </div>

                                                <%--  <div class="form-group col-md-4">
                                                                    <asp:label ID="Label110" Visible="false" runat="server" class="col-form-label">Persen Perhitungan</asp:label>
	                                                                <asp:TextBox CssClass="form-control"  Visible="false" ID="txtpersenperhitungan"  Enabled="false" runat="server" ></asp:TextBox>
                                                                     
                                                                </div> --%>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label128" Font-Bold="true" runat="server" class="col-form-label">JURU BAYAR</asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label129" runat="server" class="col-form-label">Kantor Bayar</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtktrbyr" Enabled="false" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label130" runat="server" class="col-form-label">Bank Rekening Nasabah</asp:Label>
                                                <asp:DropDownList ID="ddlBankNasabah" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label131" runat="server" class="col-form-label">No. Rekening Bank Nasabah</asp:Label>
                                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtrekeningbankdeb" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label132" runat="server" class="col-form-label">Atas Nama Rekening Bank Nasabah</asp:Label>
                                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtrekeningnamadeb" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label133" runat="server" class="col-form-label">jaminan</asp:Label>
                                                <asp:TextBox ID="txtjaminan" CssClass="form-control" Enabled="false" MaxLength='200' onkeyDown="checkTextAreaMaxLength(this,event,'200');" TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">



                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label134" runat="server" class="col-form-label">Angsuran Dimuka</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtangsurandimuka" Enabled="false" runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label135" runat="server" class="col-form-label">Biaya Angsuran Dimuka</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtbyangsurandimuka" Enabled="false" onkeypress="return isnumerickey(event);"  runat="server"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label136" runat="server" class="col-form-label">Biaya Take Over</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtbyto" Enabled="false"  onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>

                                                </div>
                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label137" runat="server" class="col-form-label">Angsuran Perbulan</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" ID="txtangsuranperbulan"  runat="server"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label138" runat="server" class="col-form-label">Baserate</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtbaserate" Enabled="false" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label139" runat="server" class="col-form-label">Rate Asuransi</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtbgc" Enabled="false" runat="server"></asp:TextBox>

                                                </div>
                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label140" runat="server" class="col-form-label">Spread</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtspread" Enabled="false" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label141" runat="server" class="col-form-label">Bunga</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtbungas" Enabled="false" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label142" runat="server" class="col-form-label">Panjar</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" ID="txtbypanjar"  runat="server"></asp:TextBox>

                                                </div>
                                            </div>


                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label143" runat="server" class="col-form-label">Admin Asuransi</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="txtadmiasuransi" onkeypress="return isnumerickey(event);"  runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label144" runat="server" class="col-form-label">Admin Provisi</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="txtadmprovisi" onkeypress="return isnumerickey(event);"  runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label145" runat="server" class="col-form-label">Admin Pencairan</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="txtadmpencairan" onkeypress="return isnumerickey(event);"  runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label146" runat="server" class="col-form-label">Admin Angsuran</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="txtadmangsuran"  onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label147" runat="server" class="col-form-label">Taksasi</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="txttaksasi"  runat="server" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label148" runat="server" class="col-form-label">Angsuran Blokir</asp:Label>

                                                    <asp:TextBox CssClass="form-control" ID="txtangsblokir" Enabled="false"  runat="server" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>

                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label149" Font-Bold="true" runat="server" class="col-form-label">DATA USAHA DEBITUR</asp:Label>
                                            </div>

                                            <div class="form-row">

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label150" runat="server" class="col-form-label">Jenis Usaha</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox43" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label151" runat="server" class="col-form-label">Lokasi Usaha</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox44" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label152" runat="server" class="col-form-label">Jumlah Tenaga Kerja</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox45" MaxLength="5" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label153" runat="server" class="col-form-label">Pendapatan Usaha</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox46" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label154" runat="server" class="col-form-label">Biaya Tenaga Kerja</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox47" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label155" runat="server" class="col-form-label">Biaya Produksi</asp:Label>
                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox48" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>

                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="Label156" Font-Bold="true" runat="server" class="col-form-label">DATA MARKETING</asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label157" runat="server" class="col-form-label">AO</asp:Label>
                                                <asp:TextBox CssClass="form-control" Enabled="false" ID="TxtAo1" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator id="reqFavoriteColor" ErrorMessage="AO tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlAO" Runat="server" />--%>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label158" runat="server" class="col-form-label">Ref Non AO</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnonao1" Enabled="false" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label159" runat="server" class="col-form-label">Alasan Peminjaman</asp:Label>
                                                <asp:TextBox ID="txtalasanpeminjaman" CssClass="form-control" Enabled="false" onkeyDown="checkTextAreaMaxLength(this,event,'150');" TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label160" runat="server" class="col-form-label">Status SK</asp:Label>
                                                <asp:TextBox ID="txtstatussk" Enabled="false" CssClass="form-control" runat="server" data-style="btn-custom"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label74" Font-Bold="true" runat="server" class="col-form-label">Jumlah Pencairan Diterima Nasabah</asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label75" runat="server" class="col-form-label">Total</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txttotalditerima" Enabled="false"  runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">

                                <asp:Button ID="btnBack4" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack4_Click" />


                            </div>
                            <div class="float-right mb-2 ">
                                <asp:Button ID="Button2" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="TOLAK" OnClick="btnTolak_Click" Width="100px" />

                                <asp:Button ID="Button3" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="EDIT" OnClick="btnedit_Click" Width="100px" />

                                <asp:Button ID="Button4" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="SETUJU" OnClick="btnSimpan_Click" Width="100px" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />

                            </div>
                        </div>

                        </span></span> 
    
                    </asp:Panel>

                    <!-- Panel Edit -->
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA NASABAH</div>
                                    <p class="m-b-0"></p>

                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <asp:HiddenField ID="HDAsuransi" runat="server" />
                                            <asp:HiddenField ID="HDKodePos" runat="server" />
                                            <asp:HiddenField ID="HDKodeArea" runat="server" />
                                            <asp:HiddenField ID="HDKodeBank" runat="server" />
                                            <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                            <asp:HiddenField ID="HDid" runat="server" />
                                            <asp:HiddenField ID="HDStatusSK" runat="server" />
                                            <asp:HiddenField ID="HDAngsuranPerbulanEfektif" runat="server" />
                                            <asp:HiddenField ID="HDAngsuranBank" runat="server" />
                                            <asp:HiddenField ID="HDBungaBank" runat="server" />
                                            <asp:HiddenField ID="HDBungaEfektif" runat="server" />
                                            <asp:HiddenField ID="HDKodeAsuransi" runat="server" />
                                            <asp:HiddenField ID="HDnopk" runat="server" />
                                            <asp:HiddenField ID="HDnopksekarang" runat="server" />
                                            <asp:HiddenField ID="HDNoPKSebelumnya" runat="server" />
                                            <asp:HiddenField ID="HDAngsuranBankOBS" runat="server" />
                                            <asp:HiddenField ID="HDAngsuranPerbulanEfektifOBS" runat="server" />
                                            <asp:HiddenField ID="HDBungaEfektifOBS" runat="server" />
                                            <asp:HiddenField ID="HDBungaBankOBS" runat="server" />
                                            <asp:HiddenField ID="HDKtrbyr" runat="server" />
                                            <asp:HiddenField ID="HDNominal" runat="server" />

                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" class="col-form-label">No. Nasabah</asp:Label>
                                                <asp:TextBox ID="txtnonasabah" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label44" runat="server" class="col-form-label">NIP/Nopen</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnopen" Enabled="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label7" runat="server" class="col-form-label">Nama Nasabah</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnamanasabah" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" class="col-form-label">Tanggal Lahir</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txttgllahir" Enabled="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label4" runat="server" class="col-form-label">Usia Masuk</asp:Label>
                                                    <asp:TextBox ID="txtusiamasuk" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label8" runat="server" class="col-form-label">Usia Jatuh Tempo</asp:Label>
                                                    <asp:TextBox ID="txtusiajt" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                                </div>
                                            </div>



                                            <div class="form-group">
                                                <asp:Label ID="Label45" runat="server" class="col-form-label">Alamat</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtalamat" TextMode="MultiLine" Rows="4" Enabled="false" runat="server"></asp:TextBox>
                                            </div>



                                            <div class="form-group">
                                                <asp:Label ID="Label11" runat="server" class="col-form-label">Kelurahan</asp:Label>
                                                <asp:TextBox ID="txtkelurahan" CssClass="form-control" Enabled="false" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label13" runat="server" class="col-form-label">Kecamatan</asp:Label>
                                                <asp:TextBox ID="txtkecamatan" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label46" runat="server" class="col-form-label">Kode Pos</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtkodepos" Enabled="false" runat="server"></asp:TextBox>
                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="Label90" runat="server" class="col-form-label">Nama Ibu Kandung</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnamaibu" Enabled="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label22" runat="server" class="col-form-label">No. HP</asp:Label>
                                                <asp:TextBox ID="txthp" Enabled="false" CssClass="form-control" runat="server" TextMode="DateTime"></asp:TextBox>
                                            </div>



                                        </div>
                                        <div class="form-group col-md-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" class="col-form-label">Plafond</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtplafon" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label14" runat="server" class="col-form-label">Nominal Provisi</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnominalprovisi" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label15" runat="server" class="col-form-label">Persen Provisi</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtpsnprov" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" class="col-form-label">Jangka Waktu</asp:Label>
                                                <asp:TextBox ID="txtjangka" Enabled="false" CssClass="form-control" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label43" runat="server" class="col-form-label">Tanggal Pengajuan</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txttglpengajuan" Enabled="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label12" runat="server" class="col-form-label">Tanggal Jatuh Tempo</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txttgljt" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label2" runat="server" class="col-form-label">Cabang</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtcabang" Enabled="false" runat="server"></asp:TextBox>
                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="Label31" runat="server" class="col-form-label">Kode Refferal</asp:Label>
                                                <asp:TextBox ID="txtkoderefferal" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label54" runat="server" class="col-form-label">Pinjaman Ke</asp:Label>
                                                <asp:TextBox ID="txtpinjamanke" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" required="required" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label16" runat="server" class="col-form-label">Program Pinjaman</asp:Label>
                                                <asp:TextBox ID="txtprojt" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" required="required" runat="server"></asp:TextBox>
                                            </div>

                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">

                                <asp:Button ID="btnBack" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack_Click" Width="100px" />



                            </div>

                            <div class="float-right mb-2 ">

                                <asp:Button ID="btnTolak" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="TOLAK" OnClick="btnTolak_Click" Width="100px" />
                                <asp:Button ID="btnedit" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="EDIT" OnClick="btnedit_Click" Width="100px" />

                                <asp:Button ID="btnSimpan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="SETUJU" OnClick="btnSimpan_Click" Width="100px" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />

                            </div>
                        </div>

                        <%--<div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPendapatan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPendapatan_Click" />
                                                        

                                                    </div>

                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextPendapatan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPendapatan_Click" Width="65px" />

                                                    </div>
                                            </div> --%>
                    </asp:Panel>

                    <asp:Panel ID="pnl3" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA GAJI NASABAH</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">

                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label50" Font-Bold="true" runat="server" class="col-form-label">DATA PENGHASILAN DEBITUR</asp:Label>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label21" runat="server" class="col-form-label">Gaji Pokok</asp:Label>
                                                    <asp:TextBox ID="txtgapok" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label23" runat="server" class="col-form-label">Tunjangan istri / suami</asp:Label>
                                                    <asp:TextBox ID="txttistri" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" AutoPostBack="true">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label9" runat="server" class="col-form-label">TKD</asp:Label>
                                                    <asp:TextBox ID="txttkd" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" AutoPostBack="true">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label10" runat="server" class="col-form-label">TPP</asp:Label>
                                                    <asp:TextBox ID="txttpp" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" AutoPostBack="true">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label87" runat="server" class="col-form-label">Tunjangan Beras</asp:Label>
                                                    <asp:TextBox ID="txttberas" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label18" runat="server" class="col-form-label">Tunjangan cacat</asp:Label>
                                                    <asp:TextBox ID="txttcacat" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label19" runat="server" class="col-form-label">Pembulatan</asp:Label>
                                                    <asp:TextBox ID="txtpembulatan" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label20" runat="server" class="col-form-label">TPM</asp:Label>
                                                    <asp:TextBox ID="txttpm" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label49" runat="server" class="col-form-label">Tunjangan Khusus</asp:Label>
                                                    <asp:TextBox ID="txttkhusus" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label51" runat="server" class="col-form-label">Tunjangan Kematian</asp:Label>
                                                    <asp:TextBox ID="txttkematian" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label58" runat="server" class="col-form-label">Tunjangan pajak</asp:Label>
                                                    <asp:TextBox ID="txttpajak" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                                </div>


                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblGajiKotor" runat="server" class="col-form-label">Gaji Kotor</asp:Label>
                                                <asp:TextBox ID="txtgajikotor" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label52" Font-Bold="true" runat="server" class="col-form-label">DATA POTONGAN PENGHASILAN DEBITUR</asp:Label>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label24" runat="server" class="col-form-label">Potongan Askes</asp:Label>
                                                    <asp:TextBox ID="txtpotaskes" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label25" runat="server" class="col-form-label">Potongan Assos</asp:Label>
                                                    <asp:TextBox ID="txtpotassos" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom">0</asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label27" runat="server" class="col-form-label">Potongan Pajak</asp:Label>
                                                    <asp:TextBox ID="txtpotpajak" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom">0</asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label28" runat="server" class="col-form-label">Potongan Lain</asp:Label>
                                                    <asp:TextBox ID="txtpotlain" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom">0</asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label93" runat="server" class="col-form-label">Sewa Rumah</asp:Label>
                                                    <asp:TextBox ID="txtpotsewarmh" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom">0</asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="Label94" runat="server" class="col-form-label">Potongan kasda</asp:Label>
                                                    <asp:TextBox ID="txtpotkasda" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lbltotalPengeluaran" runat="server" class="col-form-label">Total Potongan</asp:Label>
                                                <asp:TextBox ID="txttotal" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom">0</asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblsisapenghasilan" runat="server" class="col-form-label">Gaji Bersih</asp:Label>
                                                <asp:TextBox ID="txtgajibersih" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server">0</asp:TextBox>

                                            </div>

                                            <%--  <div class="form-group">
                                                                 <asp:label id="Label95" runat="server" class="col-form-label">Tanggal Slip Gaji</asp:label>
                                                                 <asp:TextBox CssClass="form-control" ID="TextBox28" type="Date" runat="server" ></asp:TextBox>
                                                            </div>  --%>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>



                        </span></span> 

                      
                                           <div class="hidden-print mt-4">
                                               <div class="float-left mb-2">

                                                   <asp:Button ID="btnBack3" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack3_Click" Width="100px" />



                                               </div>

                                               <div class="float-right mb-2 ">

                                                   <asp:Button ID="btnNext3" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext3_Click" Width="100px" />


                                               </div>
                                           </div>


                    </asp:Panel>

                    <asp:Panel ID="pnlAlasanTolak" Visible="false" runat="server">
                        <div class="clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <p class="m-b-0"></p>
                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label86" runat="server" class="col-form-label">Alasan Tolak</asp:Label>
                                                <asp:TextBox ID="txtalasantolak" CssClass="form-control" TextMode="MultiLine" MaxLength='200' onkeyDown="checkTextAreaMaxLength(this,event,'200');" Rows="4" runat="server">-</asp:TextBox>
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

                                    <asp:Button ID="btnSimpanTolakan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="SETUJU" OnClick="btnSimpanTolakan_Click" OnClientClick="return sweetAlerttolak(this,'Apakah anda yakin ingin Menolak data ini ?');" />

                                </div>
                            </div>
                        </div>

                        </span></span> 
    
                    </asp:Panel>
                    <asp:Panel ID="pnlEdit" Visible="false" runat="server">
                        <div class="clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <p class="m-b-0"></p>
                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label17" runat="server" class="col-form-label">Alasan Edit</asp:Label>
                                                <asp:TextBox ID="txtalasanedit" CssClass="form-control" TextMode="MultiLine" MaxLength='200' onkeyDown="checkTextAreaMaxLength(this,event,'200');" Rows="4" runat="server">-</asp:TextBox>
                                            </div>



                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="hidden-print mt-4">
                            <div class="float-left mb-2">


                                <asp:Button ID="btnbackedit" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnbackedit_Click" />


                            </div>
                            <div class="float-right mb-2 ">

                                <div class="button-list">

                                    <asp:Button ID="btnsimpanedit" align="right" class="btn btn-success waves-light text-white" runat="server" Text="SIMPAN" OnClick="btnsimpanedit_Click" OnClientClick="return sweetAlerttolak(this,'Apakah anda yakin ingin Mengedit data ini ?');" />

                                </div>
                            </div>
                        </div>

                        </span></span> 
    
                    </asp:Panel>
                </div>

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnTolak" />
                <asp:PostBackTrigger ControlID="btnNext" />
                <asp:PostBackTrigger ControlID="btnNext3" />
                <asp:PostBackTrigger ControlID="btnBack2" />
                <asp:PostBackTrigger ControlID="btnBack3" />
                <asp:PostBackTrigger ControlID="Button4" />
                <asp:PostBackTrigger ControlID="btnsimpanedit" />
                
                <asp:PostBackTrigger ControlID="btnSimpanTolakan" />
                <asp:PostBackTrigger ControlID="GRData" />
            </Triggers>
        </asp:UpdatePanel>
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