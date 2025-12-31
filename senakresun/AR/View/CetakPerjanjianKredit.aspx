<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CetakPerjanjianKredit.aspx.cs" Inherits="AR_View_CetakPerjanjianKredit" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cetak Perjanjian Kredit</title>
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


            $("#txtnorekening").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "CetakPerjanjianKredit.aspx/GetNoNasabah",
                        data: "{'InputText':'" + document.getElementById('txtnorekening').value + "','SessionBranchID':'<%=SessBranchID%>'}",
                            dataType: "json",
                            success: function (data) {
                                obj = JSON.parse(stringToArray(data.d));
                                console.log(obj);
                                var found = $.map(obj, function (item) {
                                    return {
                                        label: item[0],
                                        HDNorekening: item[1],
                                        HDNNONASABAH: item[2],
                                    }
                                });

                                $("[id$=HDNorekening]").val('');
                                $("[id$=HDNNONASABAH]").val('');

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
                         $("[id$=HDNorekening]").val(i.item.HDNorekening);
                         $("[id$=HDNNONASABAH]").val(i.item.HDNNONASABAH);
                     },
                     minLength: 3
                 });


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
                <%--  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
                --%>
                <div class="container-fluid">

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Kredit Mikto</a></li>
                                        <li class="breadcrumb-item"><a href="#">Report</a></li>
                                        <li class="breadcrumb-item"><a href="CetakPerjanjianKredit.aspx">Cetak Perjanjian Kredit</a></li>
                                    </ol>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                        <div class="row justify-content-center">
                            <div class="col-lg-6">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Filter</div>
                                    <p class="m-b-0">
                                        <div class="title text-center mb-3">
                                            <h3><b>Cetak Perjanjian Kredit</b></h3>
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-12 ">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <asp:HiddenField ID="HDKdpersh" runat="server" />
                                                            <asp:HiddenField ID="HDNorekening" runat="server" />
                                                            <asp:HiddenField ID="HDNNONASABAH" runat="server" />
                                                            <br />
                                                            <div class="form-group">
                                                                <div class="form-check-inline col-md-12">
                                                                    <label for="inputState" class="col-md-5  col-form-label font-weight-bold">No Rekening</label>
                                                                    <div class="col-6">
                                                                        <asp:TextBox CssClass="form-control" ID="txtnorekening" Enabled="true" runat="server" required="required">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="form-check-inline col-md-12">
                                                                    <label for="inputState" class="col-md-5  col-form-label font-weight-bold">Format Cetakan</label>
                                                                    <div class="col-6">
                                                                        <asp:DropDownList CssClass="form-control" ID="ddlstatus" runat="server" required="required">
                                                                            <asp:ListItem Value="1">Dengan Kop Surat</asp:ListItem>
                                                                            <asp:ListItem Value="2">Tanpa Kop Surat</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-6" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label for="inputState" class="col-form-label">Penandatangan PK</label>
                                                                <asp:TextBox CssClass="form-control" ID="txttd" Enabled="true" runat="server" required="required"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group" runat="server" visible="false">
                                                                <label for="inputState" class="col-form-label">Jabatan</label>
                                                                <asp:TextBox CssClass="form-control" ID="txtjabatan" Enabled="true" runat="server" required="required"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" style="text-align:center;">
                                                        <asp:Button ID="btnCetakAnggota" class="btn btn-success waves-light text-white" runat="server" Text="Cetak Surat" OnClick="btnCetakAnggota_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>


                </div>
                <!-- end container -->

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnCetakAnggota" />
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