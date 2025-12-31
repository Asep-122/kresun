<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="MasterTemplate_Menu" %>

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


    <!-- Plugins css-->
    <link href="../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <!-- App css -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />

    <!-- Toastr css -->
    <link href="../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Modal -->
    <link href="../plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <script src="../assets/js/modernizr.min.js"></script>

    <!-- Sweet Alert css -->
    <link href="../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery  -->
    <script src="../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../assets/js/popper.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/waves.js"></script>
    <script src="../assets/js/jquery.slimscroll.js"></script>

    <!-- Modal-Effect -->
    <script src="../plugins/custombox/js/custombox.min.js"></script>
    <script src="../plugins/custombox/js/legacy.min.js"></script>



    <script type="text/javascript">


        function jv_openModal_root() {

            setTimeout(function () { $('#kemodal').click(); $("div").remove(".tooltip"); }, 300);
        }
           function jv_openModal_root_x() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
        function jv_openModalpath_root() {

            $('#mdlPath').click();

        }

        function RootSave() {
            Custombox.close();
            setTimeout(function () {   <%= btnsave() %>; }, 500);
        }

        function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: t,
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 1000,
                    stack: 1,
                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }
                    }
                });
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


        function callbtn(icn, pn) {


            document.getElementById('<%= hdnMenuid.ClientID%>').value = pn;

            if (icn == 'Edit') {
                setTimeout(function () { <%= btnEdit() %>; }, 100);
            }

            else if (icn == 'add') {

                setTimeout(function () { <%= btnAdd() %>; }, 100);

            }

            else if (icn == 'show') {
                document.getElementById('<%= hdnParentMenuID.ClientID%>').value = pn;
                setTimeout(function () {<%= btnShow() %>; }, 50);
            }

            else if (icn == 'EditSM') {

                setTimeout(function () { <%= btnEditSM() %>; }, 50);
            }

            else if (icn == 'AddSM') {
                setTimeout(function () { <%= btnAddSM() %>; }, 50);
            }
        }

        $(document).ready(InIEvent);
        function InIEvent() {

            $("div").remove(".tooltip");

            $.getScript("../assets/js/jquery.core.js");

            $('.selectpicker').selectpicker();

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


                <asp:LinkButton ID="lnAdd" runat="server" OnClick="lnAdd_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnkReload" runat="server" OnClick="lnkReload_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnSave" runat="server" OnClick="lnSave_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnEdit" runat="server" OnClick="lnEdit_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnShow" runat="server" OnClick="lnShow_Click"></asp:LinkButton>

                <asp:LinkButton ID="lnkAddSM" runat="server" OnClick="lnkAddSM_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnEditSM" runat="server" OnClick="lnEditSM_Click"></asp:LinkButton>

                <asp:HiddenField ID="hdnParentMenuID" runat="server" />
                <asp:HiddenField ID="hdnMenuid" runat="server" />
                <!-- Modal -->
                <div id="modalroot" class="modal-demo">
                    <%--    <button type="button" class="close" onclick="Custombox.close();">
                        <span>&times;</span><span class="sr-only">Close</span>
                    </button>--%>
                    <h4 class="custom-modal-title">Add Project</h4>
                    <div class="custom-modal-text text-left">

                        <div class="form-group">
                            <label for="txtNamaMenu">Nama Menu</label>
                            <asp:TextBox ID="txtNamaMenu" CssClass="form-control" placeholder="Enter name" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtorder">Urutan</label>
                            <asp:TextBox ID="txtorder" CssClass="form-control" placeholder="Urutan Project" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class=" col-9">
                                <div class="checkbox checkbox-primary">
                                    <asp:CheckBox ID="chkrooaktiv" runat="server" />
                                    <label for="chkrooaktiv">
                                        Is Active
                                    </label>
                                </div>
                            </div>
                        </div>

                        <div class="text-right">
                            <button type="submit" class="btn btn-success waves-effect waves-light" onclick="javascript:RootSave();">Save</button>
                            <button type="button" class="btn btn-danger waves-effect waves-light m-l-10" onclick="Custombox.close();">Cancel</button>
                        </div>

                    </div>
                </div>
                <div id="modalPath" class="modal-demo">
                    <h4 class="custom-modal-title">Path Menu</h4>
                    <div class="custom-modal-text text-left">

                        <div class="form-group">
                            <asp:TreeView ID="TreeViewFolders" OnSelectedNodeChanged="TreeViewFolders_SelectedNodeChanged" runat="server" Width="100%" Height="100%">
                            </asp:TreeView>
                        </div>
                        <div class="text-right">
                            <button type="button" class="btn btn-danger waves-effect waves-light m-l-10" onclick="Custombox.close();">Cancel</button>
                        </div>

                    </div>
                </div>
                <asp:Panel ID="pnMenu" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box">
                                <h4 class="header-title m-t-0 m-b-20">Root</h4>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="aap" runat="server" class="btn btn-custom waves-effect waves-light mb-4" OnClientClick="callbtn('add','')"><i class="mdi mdi-plus"></i>Add Project</asp:LinkButton>
                                        <a id="kemodal" href="#modalroot" style="display: none;" class="btn btn-custom waves-effect waves-light mb-4" data-animation="fadein" data-plugin="custommodal"
                                            data-overlayspeed="200" data-overlaycolor="#36404a"><i class="mdi mdi-plus"></i></a>
                                    </div>
                                    <!-- end col -->
                                </div>
                                <asp:Literal ID="litMenu" runat="server"></asp:Literal>
                            </div>
                            <!-- end card-box -->
                        </div>
                        <!-- end col -->
                    </div>
                    <!-- end row -->


                </asp:Panel>
                <asp:Panel ID="pnlsubmenu" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box">
                                <h4 class="header-title m-t-0 m-b-20">Sub Menu</h4>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card-box">
                                            <ul>
                                                <li>Root&nbsp;
                                                  
                                                    <asp:LinkButton ID="lnkAddSB" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Add" OnClick="lnkAddSB_Click" runat="server"><i class="mdi mdi-folder-plus"></i></asp:LinkButton>
                                                    <asp:Literal ID="ltlSubMenu" runat="server"></asp:Literal>
                                                    <%--                                                <li>Nolink&nbsp;
                                                    <a href="#" title="" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Edit"><i class="dripicons-document-edit"></i></a>
                                                    <a href="#" title="" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Add"><i class="mdi mdi-folder-plus"></i></a>
                                                    <ul>
                                                        <li>Nolink&nbsp;
                                                            <a href="#" title="" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Edit"><i class="dripicons-document-edit"></i></a>
                                                            <a href="#" title="" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Add"><i class="mdi mdi-folder-plus"></i></a>
                                                            <ul>
                                                                <li>Link&nbsp;<a href="#" title="" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Edit"><i class="dripicons-document-edit"></i></a></li>
                                                                <li><a href="#">Real Life</a></li>
                                                                <li><a href="#">Secondhand Daylight</a></li>
                                                                <li><a href="#">The Correct Use</a></li>
                                                            </ul>
                                                        </li>
                                                        <li><a href="#">Buzzcocks</a>
                                                            <ul>
                                                                <li><a href="#">Time's Up</a></li>
                                                                <li><a href="#">Another Music in A Different Kitchen</a></li>
                                                                <li><a href="#">Love Bites</a></li>
                                                                <li><a href="#">A Different Kind Of Tension</a></li>
                                                            </ul>
                                                        </li>
                                                        <li><a href="#">Joy Division</a>
                                                            <ul>
                                                                <li><a href="#">Unknown Pleasures</a></li>
                                                                <li><a href="#">Closer</a></li>
                                                                <li><a href="#">Still</a></li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li><a href="#">Liverpool</a>
                                                    <ul>
                                                        <li><a href="#">OMD</a>
                                                            <ul>
                                                                <li><a href="#">OMD</a></li>
                                                                <li><a href="#">Organisation</a></li>
                                                            </ul>
                                                        </li>
                                                        <li><a href="#">Echo & the Bunnymen</a>
                                                            <ul>
                                                                <li><a href="#">Crocodiles</a></li>
                                                                <li><a href="#">Heaven Up Here</a></li>
                                                                <li><a href="#">Porcupine</a></li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                </li>--%>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card-box">
                                            <div class="form-horizontal">
                                                <div class="form-group row">
                                                    <label for="dlltype" class="col-3 col-form-label">Type</label>
                                                    <div class="col-9">
                                                        <asp:DropDownList ID="ddltype" runat="server" CssClass="selectpicker show-tick" data-style="btn-success" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">Root</asp:ListItem>
                                                            <asp:ListItem Value="2">Menu</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="inputpid" class="col-3 col-form-label">Parent ID</label>
                                                    <div class="col-9">
                                                        <asp:TextBox CssClass="form-control" ID="txtParent" runat="server" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="inputpn" class="col-3 col-form-label">Parent Name</label>
                                                    <div class="col-9">
                                                        <asp:TextBox CssClass="form-control" ID="txtPname" runat="server" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="inputNM" class="col-3 col-form-label">Nama Menu</label>
                                                    <div class="col-9">
                                                        <asp:TextBox CssClass="form-control" ID="txtmn" runat="server" required="required"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="inputPM" class="col-3 col-form-label">Path Menu</label>
                                                    <div class="col-9">
                                                        <div class="input-group">
                                                            <asp:TextBox CssClass="form-control" ID="txtPM" runat="server" Enabled="false"></asp:TextBox>
                                                            <div class="input-group-append">
                                                                <asp:LinkButton ID="lnlLoop" runat="server" Visible="false" OnClientClick="jv_openModalpath_root()"><span class="input-group-text"><i class="mdi mdi-magnify"></i></span></asp:LinkButton>
                                                                <a id="mdlPath" href="#modalPath" style="display: none;" data-animation="fadein" data-plugin="custommodal"
                                                                    data-overlayspeed="200" data-overlaycolor="#36404a"></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="inputUR" class="col-3 col-form-label">Urutan</label>
                                                    <div class="col-9">
                                                        <asp:TextBox CssClass="form-control required" ID="txturutan" TextMode="Number" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row justify-content-end">

                                                    <div class=" col-9">
                                                        <div class="checkbox checkbox-primary">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" />
                                                            <label for="chkIsActive">
                                                                Is Active
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group mb-0 justify-content-end row">
                                                    <div class="col-9">
                                                        <asp:Button ID="btnSimpan" Enabled="false" CssClass="btn btn-outline-primary" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
            </Triggers>
        </asp:UpdatePanel>

    </form>

    <!-- App js -->

    <script src="../assets/js/jquery.app.js"></script>

    <!-- Toastr js -->
    <script src="../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>

    <script src="../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <!-- Parsley js -->
    <script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>

    <!-- Sweet Alert Js  -->
    <script src="../plugins/sweet-alert/sweetalert2.min.js"></script>

</body>
</html>
