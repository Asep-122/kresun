<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateFeeSalesKu.aspx.cs" Inherits="AE_Transaksi_GenerategFeeSalesKu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Generate Fee Sales & Ku</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/assets/js/modernizr.min.js"></script>

    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {

            // Default Datatable
            $('#datatable').DataTable();

            //Buttons examples
            var table = $('#GRData').DataTable({
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf']
            });

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

        });
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
        function WarningAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type:warning,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);

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
                                        <li class="breadcrumb-item"><a href="#">Account Employee</a></li>
                                        <li class="breadcrumb-item"><a href="#">Transaksi</a></li>
                                        <li class="breadcrumb-item"><a href="GenerateFeeSalesKu.aspx">Generate Fee Sales & KU</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Generate Fee Sales & KU</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                        <div class="row">
                            <div class="col-lg-6 center-page">
                                <div class="col-lg-12">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Periode</div>
                                        <br />
                                        <div class="title text-center mb-3">
                                            <h3 style="font-weight: bold;">Generate Per Bulan Fee Sales & KU</h3>
                                       </div>
                                        <p class="text-muted font-14 m-b-30"></p>
                                        <div class="form-row">
                                            <div class="form-group col-md-4" hidden="hidden">
                                                <label for="inputCity" class="col-form-label">Bulan Fee</label>
                                                <asp:DropDownList ID="ddlSP" runat="server" CssClass="form-control" data-style="btn-custom">
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
                                            <div class="form-group col-md-4" hidden="hidden">
                                                <label for="inputCity" class="col-form-label">Tahun Fee</label>
                                                <asp:DropDownList ID="ddltahun" runat="server" CssClass="form-control" data-style="btn-custom">
                                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                                    <asp:ListItem Value="2021">2021</asp:ListItem>
                                                    <asp:ListItem Value="2022">2022</asp:ListItem>
                                                    <asp:ListItem Value="2023">2023</asp:ListItem>
                                                    <asp:ListItem Value="2024">2024</asp:ListItem>
                                                    <asp:ListItem Value="2025">2025</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="form-group col-md-4">
                                                <label for="inputState" class="col-md-12 col-form-label font-weight-bold">Tanggal Closing</label>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtStart" CssClass="form-control" runat="server" Type="date"></asp:TextBox>
                                            </div>
                                            <%--<div class="form-group col-md-4">
                                                <label for="inputZip" class="col-form-label">Tanggal Abensi Akhir</label>
                                                <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server" Type="date"></asp:TextBox>
                                            </div>--%>
                                        </div>
                                        <div class="form-row">
                                            <div class="form-group col-md-4">
                                                <label for="inputCity" class="col-md-12 col-form-label font-weight-bold">Cabang</label>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <asp:DropDownList ID="ddlcabang" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-row justify-content-center">
                                            <div class="form-group center">
                                                <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Process" OnClick="btnSerach_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <%--<asp:Panel ID="Panel1" Visible="true" runat="server">
                        <div class="form-group">
                            <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Process" OnClick="btnSerach_Click" />
                        </div>
                    </asp:Panel>--%>
                </div>
                <!-- end container -->

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>


<script src="/assets/js/jquery.min.js"></script>
<script src="/assets/js/popper.min.js"></script>
<script src="/assets/js/bootstrap.min.js"></script>
<script src="/assets/js/waves.js"></script>
<script src="/assets/js/jquery.slimscroll.js"></script>

<!-- App js -->
<script src="/assets/js/jquery.core.js"></script>
<script src="/assets/js/jquery.app.js"></script>


<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

</html>










