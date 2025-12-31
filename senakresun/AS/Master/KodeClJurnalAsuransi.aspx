<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KodeClJurnalAsuransi.aspx.cs" Inherits="AS_Master_KodeClJurnalAsuransi" %>
 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Kode Cl Jurnal</title>
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

      

        function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

        }

          //numeric 1
       

   function sweetAlertconfirmx(btndelete,errMsg) {
        if (btndelete.dataset.confirmed) {
            // The action was already confirmed by the user, proceed with server event
            btndelete.dataset.confirmed = false;
            return true;
        } else {
            // Ask the user to confirm/cancel the action
            event.preventDefault();
            swal({
                title: errMsg,
                confirmButtonClass: 'btn btn-confirm mt-2',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: "Yes",
                cancelButtonText: "No",
            })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    btndelete.dataset.confirmed = true;
                    btndelete.click();
                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
        }
        }    

        function cancelalert(errMsg) {
               
               swal({
                   title: errMsg,
                   // text: 'Your message has been succesfully sent',
                   confirmButtonClass: 'btn btn-confirm mt-2',
                   type: 'warning'

               })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    alert("CEKDATA");
                    setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                });
        }

function sweetAlertSavex(btnSimpan,errMsg) {
        if (btnSimpan.dataset.confirmed) {
            // The action was already confirmed by the user, proceed with server event
            btnSimpan.dataset.confirmed = false;
            return true;
        } else {
            // Ask the user to confirm/cancel the action
            event.preventDefault();
            swal({
                title: errMsg,
                confirmButtonClass: 'btn btn-confirm mt-2',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: "Yes",
                cancelButtonText: "No",
            })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    btnSimpan.dataset.confirmed = true;
                    btnSimpan.click();
                  
                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
        }
        } 

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });
   
            
        Resize();
            $('#txtTglL').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('.selectpicker').selectpicker();

            // $('.selectpicker').selectpicker('refresh');

            // alert('');

            $(function () {
                var table = $('#GRData').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });

                //var tableX = $('#GRRP').dataTable({
                //    "order": [],
                //    "columnDefs": [{
                //        "targets": 'no-sort',
                //        "orderable": false,
                //    }],
                //    ordering: [[0, 'asc']],
                //    "scrollX": true,
                //    retrieve: true

                //});

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
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="KodeJurnal.aspx">Kode Cl Jurnal</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Kode Cl Jurnal Cash Ledger</h4>
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
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Cl Jurnal" OnClick="btnAdd_Click" visible="false"/>
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                                                     
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="Kode Cl Jurnal">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdJurnal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeJurnal")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kode Bank">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdSumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodebank")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Kode type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeJournal")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Ket Jurnal">
                                        <ItemTemplate>
                                            <asp:Label ID="lblketjurnal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJurnal")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Debit GL1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitGL1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DebitGL")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Debit CC1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitCC1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DebitCC")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Kredit GL1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKreditGL1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KreditGL")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kredit CC1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKreditCC1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KreditCC")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Debit GL2" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitGL2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DebitGL1")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Debit CC2" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitCC2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DebitCC1")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Kredit GL2" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKreditGL2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KreditGL1")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kredit CC2" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKreditCC2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KreditCC1")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                                              
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" autopostback ="true"> <i class="fa fa-edit"></i></asp:LinkButton>
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
        <asp:Panel ID="Pnlheaderx" Visible="false" runat="server">
             
              <div class="row clearfix">
                   <div class="col-md-12">
                        <div class="card-box">  
                   <div class="row">  

                       <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                  <div class="ribbon ribbon-custom">Entry Data</div>
                                    <p class="text-muted font-14 m-b-30"></p> 
                                     <div class="form-group col-md-6">
                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kode Jurnal</p>
                                            <asp:TextBox CssClass="form-control" ID="txtkdJurnal"  MaxLength="8" runat="server"  parsley-trigger="change" required="required" data-parsley-maxlength="1"  ></asp:TextBox>
                                              
                                    </div> 
                                     <div class="form-group col-md-6">
                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Type Journal</p>
                                            <asp:TextBox ID="txttypejurnal" CssClass="form-control" required="required"  MaxLength="50" runat="server" Enabled="false"></asp:TextBox> 
                                    </div>
                                     <div class="form-group col-md-6">
                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Ket Jurnal</p>
                                            <asp:TextBox ID="txtketJurnal" CssClass="form-control" required="required"  MaxLength="50" runat="server"></asp:TextBox>
                                    </div>                                                             
                                </div>                                                    
                            </div>
                           </div>

                       <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                  <div class="ribbon ribbon-custom">Kode Bank</div>
                                    <p class="text-muted font-14 m-b-30"></p>  
                                    <div class="form-group col-md-6">
                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kode Bank</p>
                                            <asp:DropDownList ID="ddlkodebank" CssClass="selectpicker show-tick"  enable ="false" runat="server" required="required" data-style="btn-custom" OnSelectedIndexChanged="ddlkode_onselectedindexchanged" AutoPostBack="true"> 
                                            </asp:DropDownList>
                                    </div>
                                     <div class="form-group col-md-6">
                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Nama Bank</p>
                                            <asp:TextBox ID="txtnamabank" CssClass="form-control" required="required"  MaxLength="50" runat="server"></asp:TextBox>
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
              <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                   <div class="row">


                       <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                  <div class="ribbon ribbon-custom">Debit I</div>
                                    <p class="text-muted font-14 m-b-30"></p> 
                                            <div class="form-group">
                                                <label for="inputCity" class="col-form-label">Debit</label>
                                                <asp:DropDownList ID="ddlDebitGL1" CssClass="selectpicker show-tick" required="required" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged="DebitGL1_OnSelectedIndexChanged"></asp:DropDownList>
                                             </div>
                                             <div class="form-group">
                                                <label for="inputpid" class="col-3 col-form-label">Cost Center</label>
                                                <asp:DropDownList ID="ddlDebitCC1" CssClass="selectpicker show-tick" required="required"  runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" ></asp:DropDownList>
                                             </div>                                                              
                                </div>                                                    
                            </div>
                           </div>

                       <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                  <div class="ribbon ribbon-custom">Credit I</div>
                                    <p class="text-muted font-14 m-b-30"></p> 
                                            <div class="form-group">
                                                <label for="inputCity" class="col-form-label">Credit</label>
                                                <asp:DropDownList ID="ddlKreditGL1" CssClass="selectpicker show-tick" required="required" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged="KreditGL1_OnSelectedIndexChanged" ></asp:DropDownList> 
                                             </div>
                                             <div class="form-group">
                                                <label for="inputpid" class="col-3 col-form-label">Cost Center</label>
                                                <asp:DropDownList ID="ddlKreditCC1" CssClass="selectpicker show-tick" required="required" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" ></asp:DropDownList> 
                                             </div>                                                              
                                </div>                                                    
                            </div>
                           </div>

                       <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                  <div class="ribbon ribbon-custom">Debit II</div>
                                    <p class="text-muted font-14 m-b-30"></p> 

                                            <div class="form-group">
                                                <label for="inputCity" class="col-form-label">Debit</label>
                                                <%--<asp:DropDownList ID="ddlDebitGL2" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>--%>
                                                <asp:DropDownList ID="ddlDebitGL2" CssClass="selectpicker show-tick" required="required" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged="DebitGL2_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                             <div class="form-group">
                                                <label for="inputpid" class="col-3 col-form-label">Cost Center</label>
                                                <asp:DropDownList ID="ddlDebitCC2" CssClass="selectpicker show-tick" required="required"  runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" ></asp:DropDownList>
                                             </div>                                                              
                                </div>                                                    
                            </div>
                           </div>

                         <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                  <div class="ribbon ribbon-custom">Credit II</div>
                                    <p class="text-muted font-14 m-b-30"></p> 
                                            <div class="form-group">
                                                <label for="inputCity" class="col-form-label">Credit</label>
                                                <asp:DropDownList ID="ddlKreditGL2" CssClass="selectpicker show-tick" required="required" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged="KreditGL2_OnSelectedIndexChanged" ></asp:DropDownList> 
                                             </div>
                                             <div class="form-group">
                                                <label for="inputpid" class="col-3 col-form-label">Cost Center</label>
                                                <asp:DropDownList ID="ddlKreditCC2" CssClass="selectpicker show-tick" required="required" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" ></asp:DropDownList> 
                                             </div>                                                              
                                </div>                                                    
                            </div>
                           </div>
                        





                                               

                                            </div>
                                         </div>        
                  
                                            <div class="row">
                                                <div class="btn-group mb-2">
                                                   <div class="button-list">
                                                    
                                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" OnClientClick="return sweetAlertSavex(this,'Apakah anda yakin ingin menyimpan data ini ?');" runat="server" Text="Save" OnClick="btnSimpan_Click" />
                                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
                                                   </div>
                                                    </div>
                                            </div>
                                        </div>
    
            </asp:Panel>
                 
            </div> 

        <!-- end wrapper -->
      <%--  </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnAdd" />   
                <asp:PostBackTrigger ControlID="GRData" /> 
                <asp:PostBackTrigger ControlID="DDLKdSumber" /> 
                 
            </Triggers>
        </asp:UpdatePanel>--%>
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

