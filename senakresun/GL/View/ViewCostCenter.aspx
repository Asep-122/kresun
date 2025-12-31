<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCostCenter.aspx.cs" Inherits="GL_View_ViewCostCenter" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
         <title>View Transaction Cost Center</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
        <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
        <meta content="Coderthemes" name="author" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <!-- DataTables -->
        <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
        <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
        <!-- Responsive datatable examples -->
        <link href="../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

        <!-- Multi Item Selection examples -->
        <link href="../../plugins/datatables/select.bootstrap4.min.css" rel="stylesheet" type="text/css" />

        <!-- App css -->
        <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
        <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />

        <script src="../../assets/js/modernizr.min.js"></script>

     <link href="../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

     
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
                                    <li class="breadcrumb-item"><a href="#">General Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">View</a></li>
                                    <li class="breadcrumb-item"><a href="Branch.aspx">Transaction Cost Center</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">View Transaction Cost Center</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                            <h4 class="m-t-0 header-title">Filter</h4>
<%--                            <p class="text-muted font-14 m-b-30">
                                KeyTable provides Excel like cell navigation on any table. Events (focus, blur, action etc) can be assigned to individual cells, columns, rows or all cells.
                            </p>--%>
                            <%--<div class="row m-b-30">
                                <div class="col-sm-12">
                                    <form class="form-inline">
                                        <div class="form-group">
                                            <label for="staticEmail2" class="sr-only">Email</label>
                                            <input type="text" class="form-control-plaintext" id="staticEmail2" value="email@example.com">
                                        </div>
                                        <div class="form-group mx-sm-3">
                                            <label for="inputPassword2" class="sr-only">Password</label>
                                            <input type="password" class="form-control" id="inputPassword2" placeholder="Password">
                                        </div>
                                        <button type="submit" class="btn btn-primary">Confirm identity</button>
                                    </form>
                                </div>

                            </div>--%>
                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Cost Center</label>
                                       <asp:DropDownList ID="ddlcostcenter" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Start Date</label>
                                        <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label for="inputZip" class="col-form-label">End Date</label>
                                        <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server" Type="date"></asp:TextBox>
                                        
                                    </div>
                                       <div class="form-group col-md-2">
                                           <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSerach_Click" />                                              
                                    </div>
                                 
                                </div>
              
                    </div>
                </div>
                </asp:Panel>
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box table-responsive">
                                        <h4 class="m-t-0 header-title">Data Transaction Cost Center</h4>  
                                    <div class="form-row">
                                        <div class="form-group" style="width: 99%;">
                                            <asp:GridView ID="GRData" runat="server" ShowHeaderWhenEmpty="true"  CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"  AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <HeaderStyle CssClass="align-center" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RowNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="No. Reference System">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. Reference Manual">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBuktiKns")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl3" runat="server" DateFormat="dd-mm-yyyy" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrn")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Source">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeSumber")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodegl")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaGl")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>     
                                    <asp:TemplateField HeaderText="Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhDebit", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhKredit", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </asp:Panel>


          


            </div> <!-- end container -->
      
   <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
<%--                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />--%>
                 <asp:PostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>

        <!-- jQuery  -->
        <script src="../../assets/js/jquery.min.js"></script>
        <script src="../../assets/js/popper.min.js"></script>
        <script src="../../assets/js/bootstrap.min.js"></script>
        <script src="../../assets/js/waves.js"></script>
        <script src="../../assets/js/jquery.slimscroll.js"></script>

        <!-- Required datatable js -->
        <script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>
        <!-- Buttons examples -->
        <script src="../../plugins/datatables/dataTables.buttons.min.js"></script>
        <script src="../../plugins/datatables/buttons.bootstrap4.min.js"></script>
        <script src="../../plugins/datatables/jszip.min.js"></script>
        <script src="../../plugins/datatables/pdfmake.min.js"></script>
        <script src="../../plugins/datatables/vfs_fonts.js"></script>
        <script src="../../plugins/datatables/buttons.html5.min.js"></script>
        <script src="../../plugins/datatables/buttons.print.min.js"></script>

        <!-- Key Tables -->
        <script src="../../plugins/datatables/dataTables.keyTable.min.js"></script>

        <!-- Responsive examples -->
        <script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
        <script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>

        <!-- Selection table -->
        <script src="../../plugins/datatables/dataTables.select.min.js"></script>

        <!-- App js -->
        <script src="../../assets/js/jquery.core.js"></script>
        <script src="../../assets/js/jquery.app.js"></script>
        <!-- plugin js -->
        <script src="../../plugins/moment/moment.js"></script>
     <script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
        <!-- Init js -->
       <%-- <script src="../../assets/pages/jquery.form-pickers.init.js"></script>--%>

        <!-- Parsley js -->
    <script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>

    <script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <!-- Sweet Alert Js  -->
    <script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>

      <script type="text/javascript">

      //  $(document).ready(InIEvent);
        //function InIEvent() {

          $(document).ready(function() {

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
     
            } );



                   
        </script>

</html>










