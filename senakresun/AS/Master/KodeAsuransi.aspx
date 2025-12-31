<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KodeAsuransi.aspx.cs" Inherits="AS_Master_KodeAsuransi" %> 

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Kode Asuransi</title>
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

    
    <style type="text/css">
       .modalPopup
        {           
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
               .xcenter
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 160px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
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
            background: white url("/assets/images/Cube-1s-30px.gif") right center no-repeat;
        }

    </style>
   
   
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

        
function sweetAlertsave(btnSimpan,errMsg) {
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

           function stringToArray(items) {
            return items.replace(/'/g, '"');
        }
           function Comma(Num) {

            console.log('<%=CultureName%>');
            var splitnya;
            var koma;

            var region = '<%=CultureName%>';

            if (region == 'id-ID') {
                splitnya = '.';
                koma = ',';
            }
            else {
                splitnya = '.';
                koma = ',';
            }

            Num += '';
            Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, '');
            Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, '');
            x = Num.split(koma);
            x1 = x[0];
            x2 = x.length > 1 ? koma + x[1] : '';
            var rgx = /(\d+)(\d{3})/;


            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + splitnya + '$2');


            return x1 + x2;

        }


        function isnumerickey(e) {

            var region = '<%=CultureName%>';


            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {


                if (txt == 44 && region == 'id-ID') {
                    return true;
                }
                if (txt == 46 && region != 'id-ID') {
                    return true;
                }
                else {
                    //alert("Input is Number only")
                    return false;
                }

            }
            return true;
        }
        function sweetAlertConfirm(btndelete,errMsg) {
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
        function checkTextField1(field) {

            var region = '<%=CultureName%>';
            var splitnya;
            var dolarnya;
            var koma;

            if (region == 'id-ID') {
                splitnya = '.';
                koma = ',';
                dolarnya = '$1.';
            }
            else {
                splitnya = '.';
                koma = ',';
                dolarnya = '$1,';
            }

            if (field.value == '' || field.value == 0 || field.value == koma) {
                field.value = 0;
            }


            var nilai = field.value.split(splitnya).join('');
            var output = nilai;
            if (parseFloat(nilai)) {
                input = new String(nilai); // so you can perform string operations
                var parts = input.split(koma); // remove the decimal part
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, dolarnya).split("").reverse().join("");
                output = parts.join(koma);
            }
            field.value = output;
        }
          
        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

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
            var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_beginRequest(BeginRequestHandler);
prm.add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args) {
//Shows the modal popup - the update progress
    //alert("1");
var popup = $find('<%= modalPopup.ClientID %>');
if (popup != null) {
popup.show();
}
}


function EndRequestHandler(sender, args) {
//Hide the modal popup - the update progress
    //alert("2");
var popup = $find('<%= modalPopup.ClientID %>');
if (popup != null) {
popup.hide();
}
}
</script>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
<div class="xcenter">
<asp:Image ID="Image1" ImageUrl="/assets/images/L6.gif" AlternateText="Processing" runat="server" Width="136px"/>
</div>
</ProgressTemplate>
</asp:UpdateProgress>
<ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate> 
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  --> 
                <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Asuransi</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="KodeAsuransi.aspx">Kode Asuransi</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Kode Asuransi</h4>
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
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Asuransi" OnClick="btnAdd_Click" />
                        </div>
                     </div>
                 </div> 
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Kode Asuransi</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                <Columns>

                                    <%--Hidden Value--%>
                                    <asp:TemplateField Visible ="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnonaktif" CssClass=""  runat="server" Text='<%#Eval("NonAktifFlag")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSyaratBayar" CssClass="" runat="server" Text='<%#Eval("SyaratBayar")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbataskredit" CssClass="" runat="server" Text='<%#Eval("BatasKredit")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTeleponHD" CssClass="" runat="server" Text='<%#Eval("Telepon")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoFax" CssClass="" runat="server" Text='<%#Eval("NoFax")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" CssClass="" runat="server" Text='<%#Eval("Email")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodePembeli" CssClass="" runat="server" Text='<%#Eval("KodeSales")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatusperusahaan" CssClass="" runat="server" Text='<%#Eval("StatusPersh")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemo" CssClass="" runat="server" Text='<%#Eval("Memo")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAkunGL" CssClass="" runat="server" Text='<%#Eval("KodeGl")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrency" CssClass="" runat="server" Text='<%#Eval("KodeCrc")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBagian" CssClass="" runat="server" Text='<%#Eval("Jabatan")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnorek" CssClass="" runat="server" Text='<%#Eval("Norekening")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnmrek" CssClass="" runat="server" Text='<%#Eval("NamaRekening")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <%--<asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodeLgn1" CssClass="" runat="server" Text='<%#Eval("CustomerID")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>--%>



                                    <%--DISPLAY--%>
                                    <asp:TemplateField HeaderText="Kode Asuransi">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodeAsuransi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeAs")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField> 
                                   <%-- <asp:TemplateField HeaderText="Kode Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodeGroup" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeGroup")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Nama Asuransi">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNamaAsuransi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaAs")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <%--<asp:TemplateField HeaderText="Nomor Rekening">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoRek" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoRekening")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat1")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
									
                                    <asp:TemplateField HeaderText="Bunga">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Bunga")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat2")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProvince" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat3")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodePos" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodePos")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNPWP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NPWP")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHubungan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "HubungDengan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTelepon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoHp")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                             <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                                            <div class="row">
                                                <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                <asp:HiddenField ID="HDNoSebelumnya" runat="server" />
                                               <div class="form-group col-md-6">
                                                        <div class="form-group"> 
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Asuransi Id</p>
                                                               <asp:TextBox ID="txtAsuransiId" CssClass="form-control" runat="server" MaxLength="40" Enabled="false" ReadOnly ="true"></asp:TextBox>
                                                        </div> 
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Name</p>
                                                           <asp:TextBox ID="txtNameAsuransi" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>
                                                        </div>
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Address</p>
                                                           <asp:TextBox ID="txtAddress" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>
                                                        </div>
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">City/Town</p>
                                                           <asp:TextBox ID="txtCity" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>
                                                        </div>
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Province</p>
                                                           <asp:TextBox ID="txtPropinsi" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>
                                                        </div>
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Zip Code</p>
                                                           <asp:TextBox ID="txtZipCode" CssClass="form-control" required="required" runat="server" data-parsley-type="number" onkeypress="return isnumerickey(event);"   data-parsley-trigger="keyup"  data-parsley-length="[0, 10]" data-parsley-error-message="Huruf angka Max 8 Digit" ></asp:TextBox>
                                                           </div>
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">NPWP</p>
                                                           <asp:TextBox ID="txtNPWP" CssClass="form-control" required="required" runat="server" data-parsley-type="number" onkeypress="return isnumerickey(event);"  data-parsley-trigger="keyup"  data-parsley-maxlength="15" data-parsley-error-message="Huruf angka Max 15 Digit"></asp:TextBox>
                                                        </div> 
                                                        <div class="form-group">
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Syarat Bayar</p>
                                                                <asp:DropDownList ID="ddlSyaratBayar" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                        </div>
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Batas Kredit</p>
                                                            <asp:TextBox ID="txtBatasKredit" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" MaxLength="10"></asp:TextBox>
                                                          </div>
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Telepon</p>
                                                           <asp:TextBox ID="txtTelp" CssClass="form-control" required="required" runat="server" data-parsley-type="number" data-parsley-trigger="keyup"  onkeypress="return isnumerickey(event);"   data-parsley-maxlength="20" data-parsley-error-message="Huruf angka Max 20 Digit"></asp:TextBox>
                                                        </div>
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">No. Fax</p>
                                                           <asp:TextBox ID="txtNoFax" CssClass="form-control" required="required" runat="server" data-parsley-type="number" data-parsley-trigger="keyup"   onkeypress="return isnumerickey(event);"  data-parsley-maxlength="20" data-parsley-error-message="Huruf angka Max 20 Digit"></asp:TextBox>
                                                        </div>
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Email</p>
                                                           <asp:TextBox ID="txtEmail" data-parsley-type="email" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>
                                                        </div>
                                                   </div>
                                                   <div class="form-group col-md-6"> 
                                                        <div class="form-group">
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Status Perusahaan</p>
                                                                <asp:DropDownList ID="ddlStatusPerusahaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                        </div>
                                                       <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Memo</p>
                                                           <asp:TextBox ID="txtMemo" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>
                                                        </div> 
                                                        <div class="form-group">
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Kode Akun GL</p>
                                                                <asp:DropDownList ID="ddlKodeAkunGL" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                        </div>
                                                        <div class="form-group">
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Currency</p>
                                                                <asp:DropDownList ID="ddlCurrency" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" required="required" ></asp:DropDownList>
                                                        </div>  
                                                        <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Non Aktif</p>
                                                            <asp:CheckBox ID="cbNonAktif" runat="server"/>
                                                        </div  >
                                                        <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Contact Person</p>
                                                           <asp:TextBox ID="txtKontakPerson" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                                                        </div> 
                                                         <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Contact Telepon</p>
                                                           <asp:TextBox ID="txtKontakTelp" CssClass="form-control" runat="server" data-parsley-type="number"  onkeypress="return isnumerickey(event);"  data-parsley-trigger="keyup"  data-parsley-maxlength="20" data-parsley-error-message="Huruf angka Max 20 Digit"></asp:TextBox>
                                                        </div>  
                                                        <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Bagian</p>
                                                           <asp:TextBox ID="txtBagian" CssClass="form-control"  runat="server" MaxLength="40"></asp:TextBox>
                                                        </div>
                                                       <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">No Rekening</p>
                                                           <asp:TextBox ID="txtNoRek" CssClass="form-control"  runat="server" MaxLength="40" onkeypress="return isnumerickey(event);"  ></asp:TextBox>
                                                        </div>
                                                       <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Nama Rekening</p>
                                                           <asp:TextBox ID="txtNmRek" CssClass="form-control"  runat="server" MaxLength="40"></asp:TextBox>
                                                        </div>
														
                                                          <div class="form-group">
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Bunga Asuransi</p>
                                                              <div class="row">
                                                                  <div class ="form-group col-md-2">
                                                                    <asp:TextBox ID="txtbungaAs" CssClass="form-control" required="required" runat="server"  data-parsley-trigger="keyup"  onkeypress="return isnumerickey(event);"    ></asp:TextBox>
                                                                  </div>
                                                                   <div class ="form-group">
                                                                    <p class ="font-weight-bold">%</p>  
                                                                   </div>
                                                            </div>
                                                              <%--data-parsley-error-message="Huruf angka Max 20 Digit"--%>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>
                                         </div>        
                  
                                            <div class="row">
                                                <div class="btn-group mb-2">
                                                   <div class="button-list">
                                                       <br>
                                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" runat="server" Text="Save" OnClick="btnSimpan_Click" />
                                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
                                                   </div>
                                                    </div>
                                            </div>
                    
    
            </asp:Panel>
                

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers> 
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="GRData" /> 
            </Triggers>
        </asp:UpdatePanel>
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



