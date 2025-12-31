<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportDaftarAnggotaMarketing.aspx.cs" Inherits="AR_Report_Filter_ReportDaftarAnggotaMarketing" %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Laporan Anggota Marketing</title>
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
    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>
    <script src="/Javascript/jquery-ui.js"></script>

    <!-- DataTables -->
    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- Sweet Alert css -->
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


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
        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "
        function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

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


        $(document).ready(InIEvent);
        function InIEvent() {
            // Default Datatable
            $('#datatable').DataTable();

            //Buttons examples
            var table = $('#GRData').DataTable({
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf']
            });
            //var table = $('#GRData').DataTable({
            //    lengthChange: false,
            //    buttons: ['copy', 'excel', 'pdf']
            //});
            // Key Tables

            $('#key-table').DataTable({
                keys: true
            });

            // Responsive Datatable
            $('#responsive-datatable').DataTable();

            // Multi Selection Datatable
            $('#selection-datatable').DataTable({
                select: {
                    style: 'multi'
                }
            });
            table.buttons().container()
                .appendTo('#GRData_wrapper .col-md-6:eq(0)');
            $('.selectpicker').selectpicker();

            $('form').parsley();


            $("#txtnonasabah").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ReportJaminan.aspx/GetNoNasabah",
                        data: "{'InputText':'" + document.getElementById('txtnonasabah').value + "','SessionBranchID':'" + document.getElementById('ddlcabang').value + "'}",
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
                               // alert(textStatus);
                                alert("masuk2");
                                console.log("masukatas");
                            },
                            failure: function (response) {
                               // alert(response.responseText);
                                alert("masuk");
                                console.log("masukatas123");
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hdnZipCode]").val(i.item.hdnZipCode);
                        //$("[id$=txtnomornasabah]").val(e.item.hdnZipCode);
                       // document.getElementById("txtnomornasabah").value = i.item.hdnZipCode;
                    },
                minLength: 3,
                //alert(e.item.hdnZipCode + i.item.hdnZipCode );

            });
            //$("input[type='radio']").mousedown(function (e) {
            //    if ($(this).attr("checked") == true) {
            //        setTimeout("$('input[id=" + $(this).attr('RbtAll') + "]').removeAttr('checked');", 200);
            //    }
            //    else {
            //        return true
            //    }
            //});
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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Kredi Mikro</a></li>
                                        <li class="breadcrumb-item"><a href="#">Report</a></li>
                                        <li class="breadcrumb-item"><a href="ReportDaftarAnggotaMarketing.aspx">Report Anggota Marketing</a></li>
                                    </ol>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                        <div class="row">
                            <div class="col-lg-6  center-page">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Filter</div>
                                    <br />
                                    <div class="title text-center mb-3">
                                        <h3>Laporan Anggota Marketing</h3>
                                    </div>
                                    <br />
                                    <p class="m-b-0">
                                        <div class="row">
                                            <div class="form-group  col-md-12">
                                                <div class="card-box">
                                                    <div class="form-group">
                                                        <div class="form-group">
                                                            <div class="form-check-inline col-md-12">
                                                                <asp:Label ID="Label12" runat="server" class="col-md-4 col-form-label font-weight-bold">Marketing</asp:Label>
                                                                <div class="col-6">
                                                                    <asp:DropDownList ID="ddlmarketing" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-12 justify-content-center">
                                                            <div class="hidden-print mt-4" style="text-align: center;">
                                                                <div class="btn-group mb-2 row">
                                                                    <asp:Button ID="btnCetakAnggota" align="right" class="btn btn-success waves-light text-white" runat="server" Text="Tanpa Penjamin" OnClick="btnCetakAnggota_Click" OnClientClick="return confirm('Apakah anda yakin ingin mencetak data ini ?');" />
                                                                    &nbsp<asp:Button ID="btnCetakPenjamin" align="right" class="btn btn-success waves-light text-white" runat="server" Text="Dengan Penjamin" OnClick="btnCetakAnggota_Click" OnClientClick="return confirm('Apakah anda yakin ingin mencetak data ini ?');" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <div class="form-group">
                                                <label for="inputCity" class="col-form-label">Setting Report</label>
                                                <asp:DropDownList ID="DdlReport" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">View Report</asp:ListItem>
                                                    <asp:ListItem Value="2">Export Word</asp:ListItem>
                                                    <asp:ListItem Value="3">Export PDF</asp:ListItem>
                                                    <asp:ListItem Value="4">Export Excel</asp:ListItem>
                                                    <asp:ListItem Value="5">Export CSV</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlAdd" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA NASABAH</div>
                                    <p class="m-b-0">
                                        <div class="form-row">
                                            <div class="form-group col-md-6">
                                                <asp:HiddenField ID="HDlast_date" runat="server" />
                                                <asp:HiddenField ID="HDkepala_cabang" runat="server" />
                                                <asp:HiddenField ID="HDNamaCabang" runat="server" />
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" class="col-form-label">No. Nasabah</asp:Label>
                                                    <asp:TextBox ID="txtnomornasabah" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                                </div>

                                                <div class="form-group">
                                                    <asp:Label ID="Label7" runat="server" class="col-form-label">Nama</asp:Label>
                                                    <asp:TextBox ID="txtnama" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                                </div>

                                                <div class="form-group">
                                                    <asp:Label ID="Label11" runat="server" class="col-form-label">Tanggal Lahir</asp:Label>
                                                    <asp:TextBox ID="txttgllahir" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>

                                                </div>

                                                <div class="form-group">
                                                    <asp:Label ID="Label6" runat="server" class="col-form-label">Alamat</asp:Label>
                                                    <asp:TextBox ID="txtalamat" CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="5" runat="server" data-style="btn-custom"></asp:TextBox>

                                                </div>

                                                <div class="form-group">
                                                    <asp:Label ID="Label9" runat="server" class="col-form-label">Kelurahan/Kecamatan</asp:Label>
                                                    <asp:TextBox ID="txtkelurahan" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <div class="form-group">
                                                    <asp:Label ID="Label3" runat="server" class="col-form-label">Simpanan Pokok</asp:Label>
                                                    <asp:TextBox ID="txtsimpok" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                                </div>

                                                <div class="form-group">
                                                    <asp:Label ID="Label4" runat="server" class="col-form-label">Simpanan Wajib</asp:Label>
                                                    <asp:TextBox ID="txtsimwa" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-form-label">Bulan</label>
                                                    <asp:TextBox ID="txtBulan" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>

                        <%--                        <div class="hidden-print mt-4">
                            <div class="float-right mb-2 ">
                                <asp:Button ID="btnCetakAnggota" align="right" class="btn btn-success waves-light text-white" runat="server" Text="CETAK HISTORY CADANGAN" OnClick="btnCetakAnggota_Click" OnClientClick="return confirm('Apakah anda yakin ingin mencetak data ini ?');" />
                            </div>
                        </div>--%>
                    </asp:Panel>
                </div>
                <!-- end container -->

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnCetakAnggota" />
                <asp:PostBackTrigger ControlID="btnCetakPenjamin" />
            </Triggers>
        </asp:UpdatePanel>
    </form>



    <!-- Parsley js -->
    <script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>

    <script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <!-- Sweet Alert Js  -->
    <script src="/plugins/sweet-alert/sweetalert2.min.js"></script>
    <!-- Required datatable js -->
    <script src="/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>
    <!-- Buttons examples -->
    <script src="/plugins/datatables/dataTables.buttons.min.js"></script>
    <script src="/plugins/datatables/buttons.bootstrap4.min.js"></script>
    <script src="/plugins/datatables/jszip.min.js"></script>
    <script src="/plugins/datatables/pdfmake.min.js"></script>
    <script src="/plugins/datatables/vfs_fonts.js"></script>
    <script src="/plugins/datatables/buttons.html5.min.js"></script>
    <script src="/plugins/datatables/buttons.print.min.js"></script>

    <!-- App js -->
    <script src="/assets/js/jquery.core.js"></script>
    <script src="/assets/js/jquery.app.js"></script>
    <!-- plugin js -->
    <script src="/plugins/moment/moment.js"></script>



</body>



</html>
