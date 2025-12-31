<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportAutoDebet.aspx.cs" Inherits="SD_Report_ReportAutoDebet" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Report Daftar Assets</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />
    <!-- Required datatable js -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!-- Baru DataTables -->
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js"></script>
    <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <!-- Optional: DataTables CSS -->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.4/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <script src="/assets/js/modernizr.min.js"></script>
    <script type="text/javascript">


        $(document).ready(InIEvent);
        function InIEvent() {
            $("[id$='txtno_rekening']").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ReportSumberDana.aspx/GetNoRekening",
                        data: "{'InputText':'" + document.getElementById('txtno_rekening').value + "','sumberdana':'" + document.getElementById('ddlsumberdana').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data);

                            obj = JSON.parse(data.d);

                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[1],
                                    txtno_rekening: item[0] //hdnZipCode1
                                }
                            });

                            //$("[id$=txtno_bukti]").val(''); //hdnZipCode2

                            response(found);
                        },

                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("masuk1");
                        },
                        failure: function (response) {
                            alert("masuk 2");
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=txtno_rekening]").val(i.item.txtno_rekening); //hdnZipCode3 hdnZipCode4 
                    $("[id$=hdnZipCode]").val(i.item.txtno_rekening); //hdnZipCode3 hdnZipCode4 
                },
                minLength: 3
            });
            $('.selectpicker').selectpicker();
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
                                        <li class="breadcrumb-item"><a href="#">Sumber Dana</a></li>
                                        <li class="breadcrumb-item"><a href="#">Report</a></li>
                                        <li class="breadcrumb-item"><a href="Branch.aspx">Report Pembayaran Sumber Dana Berdasarkan Pembeli</a></li>
                                    </ol>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br>
                    <asp:Panel ID="pnlFilter" Visible="true" runat="server">

                        <div class="row">

                            <div class="col-lg-7">
                                <div class="col-lg-12">



                                    <%--<div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Filter Data</div>
                                    <p class="text-muted font-14 m-b-30"></p>    
                                     <div class="form-group row">
                                        <div class="form-inline col-lg-12">
                                                <label for="inputState" class="col-3 col-form-label">Kantor Bayar</label>     
                                            <div class="col-8">
                                                <asp:DropDownList ID="ddlkantorbayar" CssClass="selectpicker show-tick col-11 col-form-label"  data-style="btn-custom" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>
                                     <div class="form-group row">
                                        <div class="form-inline col-lg-12">
                                                <label for="inputState" class="col-3 col-form-label">Tipe Instansi</label>     
                                            <div class="col-8">
                                                <asp:DropDownList ID="ddltipeinstansi" CssClass="selectpicker show-tick col-11 col-form-label"  data-style="btn-custom" required="required" runat="server">
                                                    <asp:ListItem Text="Taspen" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Asabri" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="form-group row">
                                         <div class="form-inline col-lg-12">
                                             <label for="inputState" class="col-3 col-form-label">Periode</label> 
                                            <div class="col-8">
                                                <div class="col-sm-5">
                                                    <asp:DropDownList ID="ddlperiode" CssClass="selectpicker show-tick col-11"  data-style="btn-custom" required="required" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:DropDownList ID="ddlperiodeTahun" CssClass="selectpicker show-tick col-11"  data-style="btn-custom" required="required" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                 <div class="form-group">
                                      <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white right" runat="server" Text="Process" OnClick="btnSearch_Click" />  
                                </div>
                                </div>--%>

                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Filter Data</div>
                                        <p class="text-muted font-14 m-b-30"></p>

                                        <!-- Kantor Bayar -->
                                        <div class="form-group row">
                                            <label for="ddlkantorbayar" class="col-3 col-form-label">Kantor Bayar</label>
                                            <div class="col-9">
                                                <asp:DropDownList ID="ddlkantorbayar" CssClass="selectpicker show-tick form-control" data-style="btn-custom" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <!-- Tipe Instansi -->
                                        <div class="form-group row">
                                            <label for="ddltipeinstansi" class="col-3 col-form-label">Tipe Instansi</label>
                                            <div class="col-9">
                                                <asp:DropDownList ID="ddltipeinstansi" CssClass="selectpicker show-tick form-control" data-style="btn-custom" required="required" runat="server">
                                                    <asp:ListItem Text="Taspen" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Asabri" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <!-- Periode dan Periode Tahun -->
                                        <div class="form-group row">
                                            <label for="ddlperiode" class="col-3 col-form-label">Periode</label>

                                            <div class="col-9 d-flex gap-3">
                                                <asp:DropDownList ID="ddlperiode" CssClass="selectpicker show-tick form-control w-50" data-style="btn-custom" required="required" runat="server"></asp:DropDownList>&nbsp;
           
                                                <asp:DropDownList ID="ddlperiodeTahun" CssClass="selectpicker show-tick form-control w-50" data-style="btn-custom" required="required" runat="server"></asp:DropDownList>
                                            </div>

                                        </div>

                                        <!-- Tombol -->
                                        <%--<div class="form-group">
                                            <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white float-right" runat="server" Text="Process" OnClick="btnSearch_Click" />
                                        </div>--%>

                                        <!-- Tombol -->
                                        <div class="form-group row">
                                            <div class="col-3"></div> <!-- biar sejajar dengan label -->
                                            <div class="col-9">
                                                <asp:Button ID="btnSearch" 
                                                            class="btn btn-success waves-light text-white float-left" 
                                                            runat="server" 
                                                            Text="Process" 
                                                            OnClick="btnSearch_Click" />
                                            </div>
                                        </div>

                                    </div>




                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <asp:Panel ID="Panel1" Visible="true" runat="server">
                    </asp:Panel>



                </div>
                <!-- end container -->

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
                <%-- <asp:PostBackTrigger ControlID="txtgolongan" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>


<script src="/assets/js/popper.min.js"></script>
<script src="/assets/js/bootstrap.min.js"></script>
<script src="/assets/js/waves.js"></script>
<script src="/assets/js/jquery.slimscroll.js"></script>

<!-- App js -->
<script src="/assets/js/jquery.core.js"></script>
<script src="/assets/js/jquery.app.js"></script>

<script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>
<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>
<%--<script type="text/javascript">
          var sessionTimeoutWarning = "System.Configuration.ConfigurationSettings.AppSettings["SessionWarning"].ToString() ";
        var sessionTimeout = "Session.Timeout";

        var sTimeout = parseInt(sessionTimeoutWarning) * 60 * 1000;
        setTimeout('SessionWarning()', sTimeout);

        function SessionWarning()
        {
            var message = "Your session will expire in another " +
                (parseInt(sessionTimeout) - parseInt(sessionTimeoutWarning)) +
                " mins! Please Save the data before the session expires";
            alert(message);
        }
        </script>--%>
</html>










