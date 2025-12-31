<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewHutangSupplier.aspx.cs" Inherits="CL_View_ViewHutangSupplier" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
         <title>View Transaction Hutang Supplier</title>
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
    <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/waves.js"></script>
    <script src="../../assets/js/jquery.slimscroll.js"></script>
    <script src="../../Javascript/jquery-ui.js"></script>

    <!-- DataTables -->
        <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
        <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Sweet Alert css -->
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


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
                function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

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
                var table = $('#GridView3').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
                });
                //var table = $('#GridView2').DataTable({
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
                   table.buttons().container()
                    .appendTo('#GridView3_wrapper .col-md-6:eq(0)');
                   //table.buttons().container()
                   // .appendTo('#GridView2_wrapper .col-md-6:eq(0)');
            $('.selectpicker').selectpicker();

            $('form').parsley();

            
            $("#txtKodepos").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ViewCardGL.aspx/GetTempat",
                        data: "{'InputText':'" + document.getElementById('txtKodepos').value + "','SessKdpershx':'<%=SessKdpersh%>'}",
                         dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[1],
                                    hdnZipCode: item[0]
                                }
                            });

                            $("[id$=hdnZipCode]").val('');

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
                    $("[id$=hdnZipCode]").val(i.item.hdnZipCode);
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
                <div class="container-fluid">

                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">View</a></li>
                                    <li class="breadcrumb-item"><a href="ViewHutangSupplier.aspx">Card Cash Ledger</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">View Transaction Hutang Supplier</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                 <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box ribbon-box">
                                  <div class="ribbon ribbon-custom">Filter</div>
                                    <p class="m-b-0">
                                        <div class="row">
                                                <asp:HiddenField ID="HDNSTATUSGIRO" runat="server" />
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                            <div class="form-group col-md-6"> 
                                                                             <div class="form-group"> 
                                                                                <p class="mb-1 mt-4 col-form-label">Kode Langganan</p>
                                                                                 <asp:TextBox CssClass="form-control" ID="txtkdLgn"  runat="server" required="required" ReadOnly ="true"></asp:TextBox>    
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label for="inputZip" class="col-form-label">Nama Langganan</label>
                                                                                <asp:TextBox CssClass="form-control" ID="txtnmLgn" runat="server" required="required" ReadOnly ="true"></asp:TextBox>                                     
                                                                            </div>
                                                                           <%-- <div class="form-group">
                                                                                <label for="inputZip" class="col-form-label">Saldo Giro</label>
                                                                                <asp:TextBox CssClass="form-control" ID="txtgiro1" runat="server" required="required" ReadOnly ="true"></asp:TextBox>                                      
                                                                                <asp:TextBox CssClass="form-control" ID="txtgiro2" runat="server" required="required" ReadOnly ="true"></asp:TextBox>
                                                                            </div>--%>
                                                                            <div class="form-group">
                                                                                <label for="inputZip" class="col-form-label">KodeCrc</label>  
                                                                                <asp:TextBox CssClass="form-control" ID="txtKodeCrc1" runat="server" required="required" ReadOnly ="true"></asp:TextBox>   
                                                                                <asp:TextBox CssClass="form-control" ID="txtKodeCrc2" runat="server" required="required" ReadOnly ="true"></asp:TextBox>  
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label for="inputZip" class="col-form-label">Kurs</label> 
                                                                                <asp:TextBox CssClass="form-control" ID="txtkurs" runat="server" required="required" ReadOnly ="true"></asp:TextBox>                                     
                                                                            </div> 
                                                                
                                                                         <div class="mb-1 mt-4 col-form-label"> 
                                                                                <p class="mb-1 mt-4 col-form-label">View</p> 
                                                                                <asp:DropDownList ID="ddlView" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" >
                                                                                    <asp:ListItem Text="Silahkan Pilih" Selected="true" />
                                                                                    <asp:ListItem Text="Transaksi Mutasi AP" Value="1" /> 
                                                                                    <asp:ListItem Text="Transaksi Mutasi GL" Value="3"/>
                                                                                </asp:DropDownList>
                                                                             </div> 
                                                                            <div class="form-group">
                                                                                <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSerach_Click" />                                              
                                                                            </div>
                                                            </div>  
                                                            <div class="form-group col-md-6">  
                                                                           <div class="form-group"> 
                                                                                <p class="mb-1 mt-4 col-form-label">No. Bukti</p>
                                                                                 <asp:DropDownList ID="ddlnobukti" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlnobukti_SelectedIndexChanged"></asp:DropDownList>
                                                                             </div>
                                                                            <div class="form-group">
                                                                                <label for="inputZip" class="col-form-label">Type Transaksi</label>
                                                                                <asp:TextBox CssClass="form-control" ID="txttypetrn" runat="server" required="required" ReadOnly ="true"></asp:TextBox>                                     
                                                                            </div>
                                                                            <div class="form-group">
                                                                                    <label for="inputState" class="col-form-label">Tanggal Transaksi</label>
                                                                                    <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"   ReadOnly ="true" ></asp:TextBox>
                                                                                 </div>
                                                                                <div class="form-group">
                                                                                    <label for="inputZip" class="col-form-label">Tanggal Jatuh Tempo</label>
                                                                                    <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server"  ReadOnly ="true" ></asp:TextBox>                                        
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label for="inputZip" class="col-form-label">Bank Giro</label>
                                                                                    <asp:TextBox ID="txtbankGr" CssClass="form-control" runat="server" ReadOnly ="true"> </asp:TextBox>                                        
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label for="inputZip" class="col-form-label">no Giro</label>
                                                                                    <asp:TextBox ID="txtnoGr" CssClass="form-control" runat="server" ReadOnly ="true"></asp:TextBox>                                        
                                                                                </div> 
                                                            </div> 
                                                        </div>  
                                                    </div>
                                                    </div>
                                            </div>
  
                        </div>
              
                    </div>
                     
                    </div>
             </asp:Panel>
<asp:Panel ID="Panel1" Visible="false" runat="server">
                 <div class="row">
                            <div class="col-md-12">
                                <div class="card-box table-responsive">
                                        <h4 class="m-t-0 header-title">Data Transaksi Mutasi AP</h4>  
                                     
                                    <div class="form-row">
                                        <div class="form-group" style="width: 99%;">
                                            <asp:GridView ID="GRData" runat="server" ShowHeaderWhenEmpty="true"  CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"  AutoGenerateColumns="False" ShowFooter ="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <HeaderStyle CssClass="align-center" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RowNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="No Faktur">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnofakturAp" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "NoFaktur")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltypeAp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Keterangan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblketeranganAp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>     
                                    <asp:TemplateField HeaderText="Tanggal Transaksi">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTglTrnAp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrn")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tanggal Jatuh Tempo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTglJthTmpAp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglJthTmp")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jumlah Cair">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljlhCairAp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jmlhcair", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljlhMndr" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jmlhmndur", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kurs Transaksi">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKursTrnAp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KursStd", "{0:N2}")%>'></asp:Label>
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
               
                    <%--<asp:Panel ID="Panel2" Visible="false" runat="server">
                         <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <div class ="form-row">   
                                                                <div class="form-group col-md-4"">
                                                                    <label for="inputState" class="col-form-label">Start Date</label>
                                                                    <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date" ></asp:TextBox>
                                                                 </div>
                                                                <div class="form-group col-md-4"">
                                                                    <label for="inputZip" class="col-form-label">End Date</label>
                                                                    <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server" Type="date"></asp:TextBox>                                        
                                                                </div>  
                                        </div>
                                    </div>
                                </div>
                             </div>
                        </asp:Panel>--%>


                    <asp:Panel ID="Panel2" runat="server" Visible ="false">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box table-responsive">
                                        <h4 class="m-t-0 header-title">Jurnal Giro Mundur</h4>  
                                     
                                    <div class="form-row">
                                        <div class="form-group" style="width: 99%;">
                                            <asp:GridView ID="GridView2" runat="server" ShowHeaderWhenEmpty="true"  CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"  AutoGenerateColumns="False" ShowFooter ="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <HeaderStyle CssClass="align-center" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RowNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="No. Rekening">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl3" runat="server" DateFormat="dd-mm-yyyy" Text='<%#DataBinder.Eval(Container.DataItem, "NoRekening")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nama Rekening">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaPendek")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Kode Cc">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCrc")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>     
                                    <asp:TemplateField HeaderText="Jumlah Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JDebitBln", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jumlah Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JKreditBln", "{0:N2}")%>'></asp:Label>
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
                    <asp:Panel ID="panel3" runat="server" Visible ="false">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box table-responsive">
                                        <h4 class="m-t-0 header-title">Transaksi Mutasi GL</h4>  
                                     
                                    <div class="form-row">
                                        <div class="form-group" style="width: 99%;">
                                            <asp:GridView ID="GridView3" runat="server" ShowHeaderWhenEmpty="true"  CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"  AutoGenerateColumns="False" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <HeaderStyle CssClass="align-center" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RowNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Kode Akun">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl3" runat="server" DateFormat="dd-mm-yyyy" Text='<%#DataBinder.Eval(Container.DataItem, "kodeakun")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nama Akun">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaakun")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Kode Cc">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "costcenter")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>     
                                    <asp:TemplateField HeaderText="Jumlah Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhdebit", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jumlah Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhkredit", "{0:N2}")%>'></asp:Label>
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


                     <asp:Panel ID="panel4" Visible="true" runat="server">
                 <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box ribbon-box"> 
                                    <p class="m-b-0">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                             <h4 class="m-t-0 header-title">Total Mutasi</h4>
                                                    <div class="form-row">  
                                                                            <div class="form-group col-md-4">       
                                                                                <label for="txtmutas" class="col-form-label">Penambahan</label>
                                                                                <asp:TextBox CssClass="form-control" ID="txtTTLdebit" runat="server"></asp:TextBox>                                     
                                                                            </div>  
                                                                            <div class="form-group col-md-4">       
                                                                                <label for="txtmutas" class="col-form-label">Pengurangan</label>
                                                                                <asp:TextBox CssClass="form-control" ID="txtTTLkredit" runat="server" ></asp:TextBox>                                     
                                                                            </div>  
                                                        </div>  
                                                    </div>
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
                 <%--<asp:PostBackTrigger ControlID="ddlkdbank" />--%>
                 <asp:PostBackTrigger ControlID="ddlView" />
                 <asp:PostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </form>

         

                 <!-- Parsley js -->
    <script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>

    <script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>


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
    <!-- Sweet Alert Js  -->
    <script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

        <!-- App js -->
        <script src="../../assets/js/jquery.core.js"></script>
        <script src="../../assets/js/jquery.app.js"></script>
        <!-- plugin js -->
        <script src="../../plugins/moment/moment.js"></script>



 </body>




</html>











