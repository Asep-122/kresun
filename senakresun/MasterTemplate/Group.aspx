<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Group.aspx.cs" Inherits="MasterTemplate_Group" %>

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
    <link rel="stylesheet" href="../plugins/tooltipster/tooltipster.bundle.min.css" />

    <!-- Plugins css-->
    <link href="../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <!-- App css -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />

    <!-- Toastr css -->
    <link href="../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script src="../assets/js/modernizr.min.js"></script>

    <!-- DataTables -->
    <link href="../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- Responsive datatable examples -->
    <link href="../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery  -->
    <script src="../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../assets/js/popper.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/waves.js"></script>
    <script src="../assets/js/jquery.slimscroll.js"></script>

    <script type="text/javascript">


        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    checkUncheckSwitch = true;
                }
                else //checkbox unchecked
                {
                    var isAllSiblingsUnChecked = AreAllSiblingsUnChecked(srcChild);
                    if (!isAllSiblingsUnChecked)
                        checkUncheckSwitch = true;
                    else
                        checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsUnChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }


        function GetSelectedRow(UserLink) {



            var row = UserLink.offsetParent.parentNode;
            var rowIndex = row.cells[0].getElementsByTagName('span')[0].innerHTML - 1;

            //console.log(rowIndex);
            //console.log(row.cells[1].innerHTML);

            var groupID = document.getElementById('GRGroup_lblIdGroup_' + rowIndex);
            var grounama = document.getElementById('GRGroup_lblGroupName_' + rowIndex);

            //console.log(groupID.innerHTML);

            //document.getElementById('lblJudul').innerHTML = 'Group Member ' + grounama.innerHTML;
            document.getElementById('hdnnmGroup').value = 'Group Member ' + grounama.innerHTML;
            document.getElementById('hdnGroupID').value = groupID.innerHTML;


            <%--<%= GetBindGM() %>;--%>

            //$('#myModal').modal('show');

            return false;
        }

    </script>


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
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }
                    }
                });
            }, 0);

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
            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            $('.selectpicker').selectpicker();

            $(function () {
                var table = $('#GRGroup').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
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
                    <!-- Form row -->
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                <div class="ribbon ribbon-custom">Setting Group User</div>
                                <p class="text-muted m-b-30 font-13">
                                    <asp:Panel ID="pnlFront" runat="server">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group">
                                                        </div>
                                                    </div>
                                                    <div class="form-row">
                                                        <div class="col-sm-12">
                                                            <div class="text-center m-t-30">
                                                                <asp:LinkButton runat="server" ID="LnkAdd" CssClass="btn btn-icon waves-effect waves-light btn-purple" title="Tambah Group" OnClick="LnkAdd_Click"> <i class="mdi mdi-group"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="GRGroup" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="true" OnRowCommand="GRGroup_RowCommand"
                                                                AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No">
                                                                        <HeaderStyle CssClass="align-center" Width="5%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RowNumber")%>'></asp:Label>
                                                                            <asp:Label ID="lblIdGroup" Style="visibility: hidden; position: absolute;" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IdGroup")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Nama Group">
                                                                        <HeaderStyle CssClass="align-center" Width="75%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGroupName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GroupName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Is Active">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chklist" Enabled="false" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "IsActive") == DBNull.Value ? false:DataBinder.Eval(Container.DataItem, "IsActive")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnsetting" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            <%--<asp:LinkButton ID="btngm" runat="server" CommandName="GM" OnClientClick="return GetSelectedRow(this)" data-toggle="tooltip" data-placement="top" title="Group" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-group"></i></asp:LinkButton>--%>
                                                                            <%--<asp:LinkButton ID="btndg" runat="server" CommandName="DEL" data-toggle="tooltip" data-placement="right" title="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="if (!ConfirmDeleteMessage('Are you sure you want to delete ? ')) { return false; }"> <i class="material-icons">delete_forever</i></asp:LinkButton>--%>
                                                                        


                                                                        
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
                                    <asp:Panel ID="pnlGroup" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="card-box">
                                                    <%--  <h4 class="header-title m-t-0 m-b-20">List Menu</h4>--%>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="card-box">
                                                                <div class="form-horizontal">
                                                                    <asp:HiddenField ID="hdnID" runat="server" />
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Nama Group</label>
                                                                        <div class="col-9">
                                                                            <asp:TextBox CssClass="form-control" ID="txtGN" required="required" runat="server"></asp:TextBox>
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
                                                                            <asp:Button ID="btnSimpan" CssClass="btn btn-outline-primary" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                                                            <asp:LinkButton ID="btnCancel" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnCancel_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="card-box">

                                                                <asp:TreeView ID="tvTables" runat="server">
                                                                </asp:TreeView>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- end card-box -->
                                            </div>
                                            <!-- end col -->
                                        </div>
                                    </asp:Panel>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </p>
                            </div>
                        </div>
                    </div>
                    <!-- end row -->
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
            </Triggers>
        </asp:UpdatePanel>

    </form>
</body>

<script src="../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>
<!-- App js -->
<script src="../assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="../plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="../plugins/parsleyjs/parsley.min.js"></script>

<!-- Tooltipster js -->
<script src="../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../plugins/datatables/responsive.bootstrap4.min.js"></script>

</html>
