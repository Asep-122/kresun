<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Changepasswordspc.aspx.cs" Inherits="MasterTemplate_Changepasswordspc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title>Change Password</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!-- Tooltipster css -->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />

    <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />

    <!-- Modal -->
    <link href="/plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <!-- Toastr css -->
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script src="/assets/js/modernizr.min.js"></script>

    <!-- DataTables -->
    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- Responsive datatable examples -->
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>

    <!-- Modal-Effect -->
    <script src="/plugins/custombox/js/custombox.min.js"></script>
    <script src="/plugins/custombox/js/legacy.min.js"></script>

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
                            window.location.href = 'Changepassword.aspx';
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
                        window.location.href = '../Login.aspx';
                    })
            }, 100);
       } 

        function SweetAlert(errMsg, NanoText) {
            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        html: NanoText,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);
        }

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd, #lnkCari, #lnkSaveGR').tooltipster({
                animation: 'grow'
            });

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
            });

            $('form').parsley();
        }
    </script>
    <script type="text/javascript">  
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Change the attribute to text  
                $('#txtPassword').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
                function () {
                    //Change the attribute back to password  
                    $('#txtPassword').attr('type', 'password');
                    $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                });
            //CheckBox Show Password  
            $('#ShowPassword').click(function () {
                $('#password').attr('type', $(this).is(':checked') ? 'text' : 'password');
            });

            //Show Password change  2
            $('#show_password1').hover(function show() {
                //Change the attribute to text  
                $('#txtPasswordbaru').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
                function () {
                    //Change the attribute back to password  
                    $('#txtPasswordbaru').attr('type', 'password');
                    $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                });
            //CheckBox Show Password change  
            $('#ShowPassword1').click(function () {
                $('#txtPasswordbaru').attr('type', $(this).is(':checked') ? 'text' : 'password');
            });

            //Show Password change  2
            $('#show_password2').hover(function show() {
                //Change the attribute to text  
                $('#txtPasswordbarukonfirmasi').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
                function () {
                    //Change the attribute back to password  
                    $('#txtPasswordbarukonfirmasi').attr('type', 'password');
                    $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                });
            //CheckBox Show Password change  
            $('#ShowPassword2').click(function () {
                $('#txtPasswordbarukonfirmasi').attr('type', $(this).is(':checked') ? 'text' : 'password');
            });
        });
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
                        <div class="col-lg-6">
                            <div class="card-box ribbon-box">
                                <div class="ribbon ribbon-custom">Change Password</div>
                                <p class="text-muted m-b-30 font-13">
                                </p>
                                </p>
                            <div class="form-row">
                                <%--<div class="form-group col-md-12">
                                    <label for="password" style="margin-bottom: 35px;">Masukkan Password Lama</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" placeholder="Enter your password"></asp:TextBox>
                                        <div class="input-group-append">
                                            <button id="show_password" class="btn btn-primary" type="button">
                                                <span class="fa fa-eye-slash icon"></span>
                                            </button>
                                        </div>
                                    </div>
                                </div>--%>
                                <%--</div>
                                <div class="form-row">--%>
                                <div class="form-group col-md-12">
                                    <label for="password1" style="margin-bottom: 35px;">Masukkan Password Baru</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtPasswordbaru" CssClass="form-control" runat="server" placeholder="Enter your new password" TextMode="Password"></asp:TextBox>
                                        <div class="input-group-append">
                                            <button id="show_password1" class="btn btn-primary" type="button">
                                                <span class="fa fa-eye-slash icon"></span>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <%--</div>--%>
                                <%--<div class="form-row">--%>
                                <div class="form-group col-md-12">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtPasswordbarukonfirmasi" CssClass="form-control" runat="server" placeholder="Enter your password Verifikasi" TextMode="Password"></asp:TextBox>
                                        <div class="input-group-append">
                                            <button id="show_password2" class="btn btn-primary" type="button">
                                                <span class="fa fa-eye-slash icon"></span>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                <div class="text-right m-t-30">
                                    <asp:Button ID="btnSave" CssClass="btn btn-outline-primary" runat="server" Text="Save" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end row -->
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>

    </form>
</body>
<script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>
<!-- App js -->
<script src="/assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="/plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>

<!-- Tooltipster js -->
<script src="/plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables/responsive.bootstrap4.min.js"></script>
</html>
