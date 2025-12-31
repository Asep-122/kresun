<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairData.aspx.cs" Inherits="AE_Transaksi_RepairData" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Repair Data</title>
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
        //function WarningData(errMsg, dateMsg) {

        //    setTimeout(function () {
        //        swal(
        //            {
        //                title: errMsg,
        //                type: 'warning',
        //                text: dateMsg,
        //                confirmButtonClass: 'btn btn-confirm mt-2'
        //            }
        //        ).catch(swal.noop)
        //    }, 100);

        //}
        function WarningData(errMsg) {

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
        function sweetAlertConfirm(btndelete, errMsg) {
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
                splitnya = ',';
                koma = '.';
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
                splitnya = ',';
                koma = '.';
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
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

          {
                $('#<%=Txtcatatan.ClientID%>').prop('maxlength', 300);
                
            }
            $('#txttglmasukanggota').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

           

            $('#txttglpengajuan').datepicker({
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
            $("#txtcustomer").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "RepairData.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                        dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {

                                    Hdnnik: item[0],
                                    label: item[1]


                                }
                            });


                            $("[id$=Hdnnik]").val('');



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


                     $("[id$=Hdnnik]").val(i.item.Hdnnik);



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
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Utility</a></li>
                                    <li class="breadcrumb-item"><a href="#">Transaksi</a></li>
                                    <li class="breadcrumb-item"><a href="AeKodeGaji.aspx">Repair Data</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Repair Data</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" Visible="false" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                            <h4 class="m-t-0 header-title">Filter</h4>
                            <p class=" font-14 m-b-30">
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
                        <div class="form-group col-md-6">
                           <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add New" OnClick="btnAdd_Click" /> 
                        </div>
                   
                    </div>
                   </div>
                </asp:Panel>

  <asp:Panel ID="PnlFrontx" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Repair Data</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"    AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No. Repair" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblnomemo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoRepair")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 

                                    <asp:TemplateField HeaderText="Tanggal Repair">
                                    <ItemTemplate>
                                    <asp:Label ID="lbltgl_pengajuan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglRepair","{0:dd-MM-yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Cabang" >
                                        <ItemTemplate>
                                            <asp:Label ID="LblKodecabang" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodecabang")%>'></asp:Label>
                                            <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namacabang")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 

                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="User Repair">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbluserrepair" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodedev")%>'></asp:Label>
                                            <asp:Label ID="lblnamauser" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namadev")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type Repair">
                                        <ItemTemplate>
                                       <asp:Label ID="Lbltyperepair" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "koderepair")%>'></asp:Label>
                                            <asp:Label ID="lblnamatype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namarepair")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                                   
                                                  
                                                    <asp:TemplateField HeaderText="Note Memo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnotememo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "memorepair")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="100px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="View" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                             <asp:LinkButton ID="btndelete" Visible="false" runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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

                 

                 <!-- Panel Front -->


    <asp:Panel ID="PnlPengajuan" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Entry Data Repair</h4>                           
                              <div class="form-row">
                                  
                                        



                                                    <div class="form-group col-md-4">
                                                            <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                       
                                                                  <asp:label id="Label6" runat="server" class="col-form-label">No. Repair</asp:label>
                                                            <asp:TextBox ID="txtnobukti" CssClass="form-control"  runat="server" MaxLength="30" Text="0" readonly="true" ></asp:TextBox> 
                                                            </div>  
                                                          <div class="form-group col-md-4">
                                                               <asp:label id="Label5" runat="server" class="col-form-label">Tanggal Repair</asp:label>
                                                           
                                                            <asp:TextBox ID="txttglpengajuan" CssClass="form-control"  runat="server"  ></asp:TextBox>  
                                                                </div>
                                                            <div class="form-group col-md-4">
                                                                <asp:label id="Label4" runat="server" class="col-form-label">Kode Cabang</asp:label>
                                                             
                                                                <asp:DropDownList ID="ddlkodecabang" CssClass="selectpicker show-tick" runat="server"  >
                                                                 
                                                                </asp:DropDownList> 
                                                            </div>
                                                                
                                                                 <div class="form-group col-md-4">
                                                                       <asp:label id="Label3" runat="server" class="col-form-label">Permintaan User</asp:label>
                                                                     
                                                                 <asp:DropDownList ID="ddlpermintaanUser" CssClass="selectpicker show-tick" required="required" runat="server" Height="16px">  
                                                                <asp:ListItem Text="Admin" Value="Admin"  Selected="true"/>
                                                                <asp:ListItem Text="Teller" Value="Teller"/>
                                                                <asp:ListItem Text="Head Teller" Value="Head Teller"/>
                                                                <asp:ListItem Text="Accounting" Value="Accounting"/>
                                                                <asp:ListItem Text="Marketing" Value="Marketing"/>
                                                                <asp:ListItem Text="Manager" Value="Manager"/>
                                                                <asp:ListItem Text="Regional Manager" Value="Regional Manager"/>
                                                                <asp:ListItem Text="Korwil" Value="Korwil"/>
                                                                 <asp:ListItem Text="Pusat" Value="Pusat"/>


                                                            </asp:DropDownList> 
                                                                </div>

                                                               <div class="form-group col-md-3">
                                                                <asp:label id="Label24" runat="server" class="col-form-label">User Repair</asp:label>
                                                                    
                                                                    <asp:DropDownList ID="ddluser" CssClass="selectpicker show-tick" runat="server"  >
                                                                 
                                                                        </asp:DropDownList> 
                                                                </div>

                                                               <div class="form-group col-md-3">
                                                                    <asp:label id="Label1" runat="server" class="col-form-label">Jenis Repair</asp:label>
                                                                   
                                                                    <asp:DropDownList ID="ddltypememo" CssClass="selectpicker show-tick" runat="server"  AutoPostBack="true" OnSelectedIndexChanged ="ddlType_Memo_SelectedIndexChanged"  ></asp:DropDownList> 
                                                      
                                                                </div>
                                                             </div>
                                                             
                                                     
                                                        </div>



                                                     <div class="form-group col-md-6">
                                                           
                                                         

                                                                  <div class="form-group col-md-3">
                                                                      <asp:label id="Label2" runat="server" class="col-form-label">Catatan Repair</asp:label>
                                                                   
                                                                    <asp:TextBox CssClass="auto-style2" ID="Txtcatatan" MaxLength="300" runat="server" TextMode="MultiLine" Height="145px" Width="595px"></asp:TextBox>
                                                                    </div>
                                                           
                                                         </div>









                                              
                                                              
                                </div>
                                       

                                 </div>
                          
                        </div>
                    </div>

           
        </asp:Panel>

      
         <asp:Panel ID="PnlChangePassword" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Reset Password</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  

                                                   


                                                  <div class="">
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">KodeUser</p>
                                                            <asp:TextBox ID="txtkodeuser"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                      </div>  

                                                                 

                                                       

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

     
        <asp:Panel ID="PnlClearTeller" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Clear Data Teller</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  

                                                   


                                                  <div class="">
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Norekening</p>
                                                            <asp:TextBox ID="Txtrekeningteller"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                      </div>  

                                                                 

                                                       

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

         <asp:Panel ID="PnlDeleteJournal" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Delete Nobukti Journal</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  

                                                   


                                                  <div class="">
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nobukti</p>
                                                            <asp:TextBox ID="txtnobuktiJournal"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                      </div>  

                                                                 

                                                       

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
                
            <asp:Panel ID="PnlResetBungaJangka" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Reset Bunga Berjangka</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  

                                                   


                                                  <div class="">
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Rekening Berjangka</p>
                                                             <asp:DropDownList ID="ddlrekeningBerjangka" CssClass="selectpicker show-tick" runat="server"   ></asp:DropDownList> 
                                                           
                                                      </div>  

                                                           <div>      
                                                      <p class="mb-1 mt-4 font-weight-bold ">Setelah Proses Reset Bunga Berjangka Pastikan User cabang merubah Bunga Tersebut</p>
                                                       <p class="mb-1 mt-4 font-weight-bold ">Setelah Itu Jalankan Kembali Running Bunga Berjangka No Rekening Tersebut</p>
                                                               </div> 

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


                 <asp:Panel ID="PnlRunningBungaJangka" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Running Bunga Berjangka Per Rekening</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  

                                                   


                                                  <div class="">
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Rekening Berjangka</p>
                                                             <asp:DropDownList ID="ddlrekeningberjangkarunning" CssClass="selectpicker show-tick" runat="server"   ></asp:DropDownList> 
                                                           
                                                      </div>  

                                                           <div>      
                                                      <p class="mb-1 mt-4 font-weight-bold ">Sebelum dijalankan Pastikan User cabang sudah melakukan Perubahan Data seperti Bunga yang seharusnya</p>

                                                               </div> 

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


              <asp:Panel ID="PnlUpdateCadangan" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Update Cadangan Rekening</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  

                                                   


                                                  <div class="">
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Rekening Cadangan</p>
                                                             <asp:TextBox ID="TxtRekeningCadangan"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                           
                                                      </div>  

                                                          <div class="button-list">
                                        
                                                              <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search"  OnClick="btnSearch_Cadangan_Click"/> 
                                                                        
                                                           </div>
                                                        <div class="form-group">
                                                            <asp:label ID="Label131s" runat="server" class="col-form-label">Nama Anggota</asp:label>
                                                      <asp:TextBox ID="txtnamaanggota"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                       </div>
                                                       <div class="form-group">
                                                                <asp:label ID="Label131" runat="server" class="col-form-label">By Cadangan</asp:label>
																<asp:TextBox ID="txtcadangan"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" required="required" runat="server" AutoPostBack="true" OnTextChanged="SaldoCadangan_ontextchanged">0</asp:TextBox> 

															    </div>
                                                       <div class="form-group">
                                                                <asp:label ID="Label7" runat="server" class="col-form-label">Mutasi Cadangan</asp:label>
																<asp:TextBox ID="txtmutasicadangan"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" required="required" runat="server" AutoPostBack="true" OnTextChanged="SaldoCadangan_ontextchanged">0</asp:TextBox> 

															    </div>
                                                      <div class="form-group">
                                                                <asp:label ID="Label8" runat="server" class="col-form-label">Saldo Cadangan</asp:label>
																<asp:TextBox ID="txtsaldocadangan" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" required="required" runat="server">0</asp:TextBox> 

															    </div>

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

                <asp:Panel ID="PnlUpdateSaldoTP" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Update Saldo Anggota Tarik Pusat</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  

                                                   


                                                  <div class="">
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">No Anggota Tarik Pusat</p>
                                                             <asp:TextBox ID="txtanggotatp"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                           
                                                      </div>  

                                                          <div class="button-list">
                                        
                                                              <asp:Button ID="Button1" class="btn btn-success waves-light text-white" runat="server" Text="Search"  OnClick="btnSearch_saldo_TP_Click"/> 
                                                                        
                                                           </div>
                                                        <div class="form-group">
                                                            <asp:label ID="Label9" runat="server" class="col-form-label">Nama Anggota</asp:label>
                                                      <asp:TextBox ID="txtnamatp"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                       </div>
                                                       <div class="form-group">
                                                                <asp:label ID="Label10" runat="server" class="col-form-label">Saldo Akhir</asp:label>
																<asp:TextBox ID="txtsaldoakhir"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" required="required" runat="server" >0</asp:TextBox> 

															    </div>
                                                      

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

    <asp:Panel ID="PnlRegisterDate" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Update Tgl Masuk Anggota</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  

                                                   


                                                  <div class="">
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">No Anggota</p>
                                                             <asp:TextBox ID="txtnoanggotard"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                           
                                                      </div>  

                                                          <div class="button-list">
                                        
                                                              <asp:Button ID="Button2" class="btn btn-success waves-light text-white" runat="server" Text="Search"  OnClick="btnSearch_Rd_Anggota_Click"/> 
                                                                        
                                                           </div>
                                                        <div class="form-group">
                                                            <asp:label ID="Label11" runat="server" class="col-form-label">Nama Anggota</asp:label>
                                                      <asp:TextBox ID="txtnamard"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                       </div>
                                                       <div class="form-group">
                                                                <asp:label ID="Label12" runat="server" class="col-form-label">Tanggal Masuk Anggota</asp:label>
																 <asp:TextBox ID="txttglmasukanggota" CssClass="form-control"  runat="server"  ></asp:TextBox> 

															    </div>
                                                      

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


<asp:Panel ID="PnlnobuktikodeUser" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Update Kode User Nobukti Journal</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  

                                                   


                                                  <div class="">
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nobukti Journal</p>
                                                             <asp:TextBox ID="txtnobuktikodeuser"  CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                           
                                                      </div>  

                                                       
                                                        <div class="form-group">
                                                            <asp:label ID="Label13" runat="server" class="col-form-label">Kode User</asp:label>
                                                               <asp:DropDownList ID="ddlkodeuserjournal" CssClass="selectpicker show-tick" required="required" runat="server" Height="16px">  
                                                                <asp:ListItem Text="Teller" Value="TL2"/>
                                                                <asp:ListItem Text="Head Teller" Value="TL1"/>
                                                                <asp:ListItem Text="Accounting" Value="AKT1"/>
                                                                

                                                            </asp:DropDownList> 
                                                       </div>
                                                       
                                                      

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


                        <asp:Panel ID="Pnlfooter" Visible="false" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card-box">
                                        <div class="card-box table-responsive">
                                            <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Process data ini ?');" runat="server" Text="Save" OnClick="btnSimpan_Click" />
                                            <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader_Click" Width="65px"  Visible ="false"/>
                                        </div>
                                    </div>
                                </div>
                            </div> 
                        </asp:Panel> 
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="btnCancel" /> 
              <%--  <asp:PostBackTrigger ControlID="ddlbag" /> 
                 <asp:PostBackTrigger ControlID="ddljab" /> 
                 <asp:PostBackTrigger ControlID="ddlgol" /> --%>
                
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


