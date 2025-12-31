<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Company.aspx.cs" Inherits="AR_Master_Company" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Company</title>
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
                            setTimeout(function () { <%=btnreload()%>; }, 50);    //Save dan Edit
                        }

                         if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                        }
                    }
                });
            }, 0);

        }
        function CheckFileExistence() {

            var filePath = document.getElementById('<%= this.FileUpload1.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto belum di pilih!");


                return false;
            }
        }

        function redirect() {
		  location.href = 'Company.aspx ';
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
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
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
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Configuration</a></li>
                                    <li class="breadcrumb-item"><a href="Company.aspx">Company</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Master Company</h4>
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
                 <!-- Panel Header -->
               <asp:Panel ID="pnlHeader" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                           <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Company" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Company</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False">
                                <Columns>
                                     <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Nama Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnamacompany" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaCompany")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alamat 1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamat1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AlamatCompany1")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="alamat2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AlamatCompany2")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kota">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KotaCompany")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Negara">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnegara" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NegaraCompany")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tlp">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltlp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TeleponCompany")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Fax">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FaxCompany")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Email")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Website">
                                        <ItemTemplate>
                                            <asp:Label ID="lblweb" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Website")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Motto">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmotto" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MottoCompany")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                                                                                        
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                             <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" OnClientClick="return confirm('Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
            <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box">
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                          
															 
                                                        <asp:HiddenField ID="HFID" runat="server" />
                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Nama Company</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamacompany" MaxLength="100"  runat="server" required="required"></asp:TextBox>
                                                            </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label">Alamat Company1</asp:label>
																<asp:TextBox ID="txtalamat1" CssClass="form-control" MaxLength="100" required="required" runat="server" ></asp:TextBox>
                                                            </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Alamat Company2</asp:label>
																<asp:TextBox ID="txtalamat2" CssClass="form-control" MaxLength="100" required="required" runat="server" ></asp:TextBox>
															</div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Kota Company</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtkota"  MaxLength="100" runat="server" required="required"></asp:TextBox>
                                                            </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">Negara Company</asp:label>
																<asp:TextBox ID="txtnegara" CssClass="form-control" MaxLength="50" required="required" runat="server" ></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:label id="Label11" runat="server"  class="col-form-label">NPWP</asp:label>
                                                                <asp:TextBox ID="txtnpwp"  CssClass="form-control" runat="server" data-style="btn-custom"  MaxLength="30" onkeypress="return isnumerickey(event);"></asp:TextBox>   
                                                            </div>
                                                            <div class="form-group">
																<asp:FileUpload ID="FileUpload1" runat="server"/> 
                                                                <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadFile" OnClientClick="return CheckFileExistence()" />
                                                                <hr />
                                                                <asp:TextBox ID="txtpath" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                 <hr />
                                                                <%--<asp:Image ID="Image2" runat="server" Height="140px" Width="280px" />--%>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <asp:label ID="Label8" runat="server" class="col-form-label">Telepon Company</asp:label>
																<asp:TextBox ID="txttlp" CssClass="form-control" required="required" runat="server" MaxLength="30" onkeypress="return isnumerickey(event);"></asp:TextBox>
															</div>
															<div class="form-group">
                                                                <asp:label ID="Label10" runat="server" class="col-form-label">Fax Company</asp:label>
																<asp:TextBox ID="txtfax" CssClass="form-control" required="required" runat="server" MaxLength="30" onkeypress="return isnumerickey(event);"></asp:TextBox>
															</div>
                                                            <div class="form-group">
                                                                 <asp:label id="Label7" runat="server"   class="col-form-label">Email</asp:label>
                                                                 <asp:TextBox ID="txtemail"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="100"></asp:TextBox>   
                                                            </div>
                                                            <div class="form-group">
                                                                 <asp:label id="Label6" runat="server"  class="col-form-label">Website</asp:label>
																 <asp:TextBox ID="txtweb"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="100"></asp:TextBox>      
                                                            
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:label id="Label9" runat="server"  class="col-form-label">Motto Company</asp:label>
                                                                <asp:TextBox ID="txtmoto"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="250"></asp:TextBox>   
                                                            </div>
                                                             <div class="form-group">
																<%--<asp:FileUpload ID="FileUpload1" runat="server"/> --%>
                                                                <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadFile" OnClientClick="return CheckFileExistence()" />--%>
                                                                <%--<hr />--%>
                                                                <%--<asp:TextBox ID="txtpath" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>--%>
                                                                 <%--<hr />--%>
                                                                 <div class="form-check-inline col-md-12">
                                                                    <asp:Image ID="Image1" runat="server" Height="140px" Width="280px" />
                                                                     &nbsp&nbsp&nbsp&nbsp&nbsp
                                                                    <div class="btn-group mb-2 col-7">
                                                                        <div class="button-list">
                                                                            <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click"  OnClientClick="return confirm('Apakah anda yakin ingin menyimpan data ini ?');" />
                                                                            <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" OnClientClick="redirect()" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                          
                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="row">
                                                <div class="btn-group mb-2">
                                                   <%--<div class="button-list">
                                                        <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click"  OnClientClick="return confirm('Apakah anda yakin ingin menyimpan data ini ?');" />
                                                        <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" />
                                                       </div>--%>
                                                    </div>
                                            </div>
    
            </asp:Panel>
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="btnUpload" />  
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                <%--<asp:AsyncPostBackTrigger ControlID="pnladd" />--%>
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
