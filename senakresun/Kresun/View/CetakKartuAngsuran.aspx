<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CetakKartuAngsuran.aspx.cs" Inherits="Kresun_View_CetakKartuAngsuran" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cetak Kartu Angsuran</title>
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


        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

        function Simpan() {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil disimpan",
                        type: "success",
                        position: "top-end",
                        //   icon: "success",
                        showConfirmButton: false,
                        timer: 2000
                        //title: errMsg,
                        //confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);
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



        $(document).ready(InIEvent);
        function InIEvent() {

            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });



            $("#txtno_rekening").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "CetakKartuAngsuran.aspx/GetNoNasabah",
                        data: "{'InputText':'" + document.getElementById('txtno_rekening').value + "','SessionBranchID':'<%=SessBranchID%>'}",
                                dataType: "json",
                                success: function (data) {
                                    obj = JSON.parse(stringToArray(data.d));
                                    console.log(obj);
                                    var found = $.map(obj, function (item) {
                                        return {
                                            label: item[0],
                                            hdnZipCode: item[1]
                                        }
                                    });

                                    $("[id$=hdnZipCode]").val('');

                                    response(found);
                                },

                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    alert(textStatus);
                                },
                                failure: function (response) {
                                    alert(response.responseText);
                                }
                            });
                },
                select: function (e, i) {
                    $("[id$=hdnZipCode]").val(i.item.hdnZipCode);
                },
                minLength: 3
            });

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
        }

    </script>
    <style type="text/css">
        .form-control {
        }
    </style>
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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Kresun</a></li>
                                        <li class="breadcrumb-item"><a href="#">View</a></li>
                                        <li class="breadcrumb-item"><a href="CetakKartuAngsuran.aspx">Cetak Kartu Angsuran</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Cetak Kartu Angsuran</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Filter</div>
                                    <p class="m-b-0">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <label class="col-form-label">Kode Cabang</label>
                                                            <asp:TextBox CssClass="form-control" ID="txtkodecabang" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnZipCode" runat="server" />


                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label for="inputState" class="col-form-label">No Rekening</label>
                                                                <asp:TextBox CssClass="form-control" ID="txtno_rekening" Enabled="true" runat="server" required="required" MaxLength="200"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA NASABAH</div>
                                    <p class="m-b-0"></p>

                                    <div class="form-row">

                                        <div class="form-group col-md-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" class="col-form-label">No. Rekening</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtnorek" Enabled="false" runat="server" required="required" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label43" runat="server" class="col-form-label">Nominal Pinjaman</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtplafon" Enabled="false" runat="server" required="required"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" class="col-form-label">Jangka Waktu</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtjangka" Enabled="false" runat="server" required="required"></asp:TextBox>
                                            </div>


                                        </div>
                                        <div class="form-group col-md-6">


                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" class="col-form-label">Nama Nasabah</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtNamaNasabah" Enabled="false" runat="server" required="required"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" class="col-form-label">Alamat</asp:Label>
                                                <asp:TextBox ID="txtalamat" CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label22" runat="server" class="col-form-label">Kecamatan/Kelurahan</asp:Label>
                                                <asp:TextBox ID="txtkelurahan" Enabled="false" CssClass="form-control" runat="server" TextMode="DateTime"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="Label11" runat="server" class="col-form-label">Jenis Pinjaman</asp:Label>
                                                <asp:TextBox ID="txtjenispinjaman" CssClass="form-control" Enabled="false" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label45" runat="server" class="col-form-label">Keloktor / AO</asp:Label>
                                                <asp:TextBox CssClass="form-control" ID="txtAo" Enabled="false" runat="server" required="required"></asp:TextBox>
                                            </div>


                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>

                    </asp:Panel>


                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box  ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA KARTU PINJAMAN NASABAH</div>

                                    <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">

                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="887px">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Angsuran Ke">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodeCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angs_ke")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status Bayar">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status_pembayaran")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Jatuh Tempo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNpwp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tanggal_jt")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pokok">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblalamat1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pokok", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bunga">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblalamat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bunga", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Angsuran">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblalamat3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuran")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admin Angsuran">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblalamat3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "adm")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Angsuran">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblalamat3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalAngsuran")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    </asp:TemplateField>




                                                </Columns>
                                            </asp:GridView>
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

                                <asp:Button ID="btnNext" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="Print" OnClick="btnprint_Click" Width="65px" />


                            </div>
                        </div>

                    </asp:Panel>
                    <!-- Panel Edit -->


                </div>
                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
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
