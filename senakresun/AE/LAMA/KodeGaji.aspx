<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KodeGaji.aspx.cs" Inherits="AE_Master_KodeGaji" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Account Employee Makan </title>
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
                $('#<%=txtmemo.ClientID%>').prop('maxlength', 150);
                
            }
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
                                    <li class="breadcrumb-item"><a href="#">Account Employee</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="AeKodeGaji.aspx">Account Code Salary</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Data Karyawan</h4>
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
                   <%--     <div class="form-group col-md-6">    
                            <div class ="form-row"> 
                                    <div class="form-group col-md-2">
                                        <asp:Button ID="btnsearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSearch_Click" Visible ="false"/> 
                                    </div>
                                <div class="form-group col-md-4"> 
                                    <asp:TextBox ID="txtsearch" CssClass="form-control" required="required" runat="server"  Text="0" Visible ="false"></asp:TextBox>  
                                </div>
                            </div>
                        </div>--%>
                    </div>
                   </div>
                </asp:Panel>

  <asp:Panel ID="PnlFrontx" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Gaji Karyawan</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="NIK" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblNik" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeNik")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Nama">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaKry")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Dept">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetDept")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                      
                                   <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Bagian">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetBagian")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 

                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Jabatan">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJab")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Golongan">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namastatus")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderStyle-HorizontalAlign ="Center" HeaderText="Tgl Masuk">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTerima","{0:dd-MM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
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

                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server" Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Pembuatan Kode Gaji</h4>                           
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Nik</p>
                                                             <asp:TextBox ID="txtnik" CssClass="form-control" runat="server" MaxLength="30" Text="-" ReadOnly="true"></asp:TextBox>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nama Karyawan</p>
                                                            <asp:TextBox ID="txtnmkyn" CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                      </div>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Npwp Karyawan</p>
                                                            <asp:TextBox ID="txtnpwpkyn" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                      </div>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nomor BPJS KetenagaKerjaan</p>
                                                            <asp:TextBox ID="txtnobpjs" CssClass="form-control" required="required" runat="server"  Text="0" MaxLength="15" onkeypress="return isnumerickey(event);">0</asp:TextBox> 
                                                      </div>    
                                                          
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Pendidikan Terakhir</p>
                                                            <asp:DropDownList ID="ddlpddk" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Type Tenaga Kerja</p>
                                                            <asp:DropDownList ID="ddltngkrj" CssClass="selectpicker show-tick" required="required" runat="server">  
                                                                <asp:ListItem Text="1. WNI" Value="WNI"  Selected="true"/>
                                                                <asp:ListItem Text="2. WNA" Value="WNI"/>
                                                            </asp:DropDownList> 
                                                      </div>             
                                                              
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Unit Bisnis</p>
                                                            <asp:DropDownList ID="ddlunitbisnis" CssClass="selectpicker show-tick" required="required" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlunitbisnis_SelectedIndexChanged"></asp:DropDownList> 
                                                      </div>  

                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kantor</p>
                                                            <asp:DropDownList ID="ddloff" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                    
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Bagian</p>
                                                            <asp:DropDownList ID="ddlbag" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                    
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Jenis Kelamin</p>
                                                            <asp:DropDownList ID="ddlgen" CssClass="selectpicker show-tick" required="required" runat="server" Height="16px">  
                                                                <asp:ListItem Text="1. Laki - Laki" Value="L"  Selected="true"/>
                                                                <asp:ListItem Text="2. Perempuan" Value="P"/>
                                                            </asp:DropDownList> 
                                                      </div>                          
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Status PTKP</p>
                                                            <asp:DropDownList ID="ddlsttptkp" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                            
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Jabatan</p>
                                                            <asp:DropDownList ID="ddljab" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                            
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Golongan</p>
                                                            <asp:DropDownList ID="ddlgol" CssClass="selectpicker show-tick" required="required" AutoPostBack ="true" runat="server" OnSelectedIndexChanged="kdgol_onselectedchange"></asp:DropDownList> 
                                                      </div>                            
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Status Karyawan</p>
                                                            <asp:DropDownList ID="ddlsttskyn" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                      
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">System Bayar</p>
                                                            <asp:DropDownList ID="ddlsysbyr" CssClass="selectpicker show-tick" required="required" runat="server">   
                                                                <asp:ListItem Text="1. Jam" Value="Harian" /> 
                                                                 <asp:ListItem Text="2. Harian" Value="Harian"/> 
                                                                <asp:ListItem Text="3. Bulanan" Value="Bulanan" Selected="true"/>
                                                            </asp:DropDownList> 
                                                      </div> 
                                                       <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Sebagai Pembeli</p>
                                                            <asp:DropDownList ID="ddlpembeli" CssClass="selectpicker show-tick" required="required" runat="server">   
                                                                <asp:ListItem Text="1. Bukan Pembeli" Value="1" Selected="true" /> 
                                                                <asp:ListItem Text="2. Pembeli" Value="2"/>                                                            
                                                            </asp:DropDownList> 
                                                      </div>      
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Sebagai Marketing</p>
                                                            <asp:DropDownList ID="ddlmarketing" CssClass="selectpicker show-tick" required="required" runat="server" OnSelectedIndexChanged="ddlmarketing_OnChange" AutoPostBack="true">   
                                                                <asp:ListItem Text="1. Bukan Marketing" Value="1" Selected="true" /> 
                                                                <asp:ListItem Text="2. Marketing" Value="2"/>                                                            
                                                            </asp:DropDownList> 
                                                      </div>                         
                                                      <div runat="server" visible="false" id="divbos">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Bos ID</p>
                                                            <asp:DropDownList ID="ddlbosid" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>     
                                                      <div>
                                                            <%--<p class="mb-1 mt-4 font-weight-bold ">Kode Makan</p>--%>
                                                            <asp:DropDownList ID="ddlkdmkn" Visible="false" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                            
                                                      <div>
                                                            <%--<p class="mb-1 mt-4 font-weight-bold ">Kode Transport</p>--%>
                                                            <asp:DropDownList ID="ddlkdtrp" Visible="false" CssClass="selectpicker show-tick" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                        
                                                    </div>   
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <div class="">    
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tanggal Diterima</p>
                                                            <asp:TextBox ID="txttgljoin" CssClass="form-control" required="required" runat="server"  type="date" AutoPostBack="true" OnTextChanged="txtjlhbayar_TextChanged"></asp:TextBox>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tanggal Keluar</p>
                                                            <asp:TextBox ID="txttglout" CssClass="form-control" required="required" runat="server"  type="date"></asp:TextBox>   
                                                      </div>                         
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Cara Pembayaran</p>
                                                            <asp:DropDownList ID="ddlbyr" CssClass="selectpicker show-tick" required="required" runat="server" AutoPostBack="true" OnSelectedIndexChanged ="ddlbyr_SelectedIndexChanged"> 
                                                                <asp:ListItem Text="1. Transfer" Value="T"  Selected="true"/> 
                                                                <asp:ListItem Text="2. Cash" Value="C"/> 
                                                            </asp:DropDownList> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nama Bank Perusahaan</p>
                                                            <asp:TextBox ID="txtnmbank" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>   
                                                      </div>                         
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Rek. Bank Perusahaan</p>
                                                            <asp:DropDownList ID="ddlrekpersh" CssClass="selectpicker show-tick" required="required" runat="server" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnSelectedIndexChanged ="ddlrekpersh_SelectedIndexChanged"></asp:DropDownList> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nama Bank Karyawan</p>
                                                            <asp:TextBox ID="txtnmbakpersh" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Cabang Bank Karyawan</p>
                                                            <asp:TextBox ID="txtcbgbank" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Rek. Bank Karyawan</p>
                                                            <asp:TextBox ID="txtrekkyn" CssClass="form-control" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div> 
                                                            <p class="mb-1 mt-4 font-weight-bold">Pinjaman Karyawan</p>
                                                      </div>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Plafon Pinjaman/Thn</p>
                                                            <asp:TextBox ID="txtplafonpnjm" CssClass="form-control" required="required" runat="server" text="0,00" MaxLength="40" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Jumlah Pinjaman</p>
                                                            <asp:TextBox ID="txtjmlhpnjm" Enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="40" text="0,00" onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Angsuran S/d Bln ini</p>
                                                            <asp:TextBox ID="txtangs" Enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="40" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div> 
                                                            <p class="mb-1 mt-4 font-weight-bold">Cuti Karyawan</p>
                                                      </div>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Plafon cuti</p>
                                                            <asp:TextBox ID="txtplafonct" CssClass="form-control" required="required" runat="server" text="0,00" MaxLength="40"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Pakai Cuti S/d Bulan Lalu</p>
                                                            <asp:TextBox ID="txtctblnll" Enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="40" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div>  
                                                      <div> 
                                                            <p class="mb-1 mt-4 font-weight-bold">Tunjangan Obat</p>
                                                      </div>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tunjangan plafon obat/Thn</p>
                                                            <asp:TextBox ID="txttunjplaf" CssClass="form-control" required="required" runat="server" text="0,00" MaxLength="40"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Obat Terpakai S/d Bulan Lalu</p>
                                                            <asp:TextBox ID="txtobtpakai" Enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="40" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
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
        <asp:Panel ID="Panel1" runat="server"  Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">                        
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Gaji Pokok Max</p>
                                                             <asp:TextBox ID="txtgjmx" CssClass="form-control" runat="server" MaxLength="40" text="0,00"  readonly ="true" onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Gaji Pokok/Bln</p>
                                                            <asp:TextBox ID="txtgapok" CssClass="form-control"  runat="server" MaxLength="50" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  AutoPostBack="true" OnTextChanged="lembur_ontextchanged"></asp:TextBox> 
                                                      </div>   
                                                      <div>
                                                            <div class =" mb-1 mt-4 form-row">
                                                                <div class="form-group col-md-5">
                                                                        <asp:TextBox ID="TextBox3" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Tetap" ></asp:TextBox> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttutep" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  AutoPostBack="true" OnTextChanged="lembur_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>       
                                                      <div>
                                                            <div class ="form-row"> 
                                                                 <div class="form-group col-md-5">
                                                                        <asp:TextBox ID="TextBox5" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Jabatan" ></asp:TextBox> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttujab" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true"  OnTextChanged="lembur_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>        
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                        <asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttumakan" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>          
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                        <asp:TextBox ID="TextBox7" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Transport" ></asp:TextBox> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttutrp" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="lembur_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div> 
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5" style="left: 0px; top: 0px">
                                                                      <asp:DropDownList ID="ddltunj1" CssClass="selectpicker show-tick" required="required" runat="server" AutoPostBack="true" OnSelectedIndexChanged ="ddltunj1_onselectedchange"></asp:DropDownList> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttunj1" CssClass="form-control"  runat="server" MaxLength="20" Text="0,00" Enabled ="false"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="lembur_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>    
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:DropDownList ID="ddltunj2" CssClass="selectpicker show-tick" required="required" runat="server" Enabled ="false" AutoPostBack="true" OnSelectedIndexChanged ="ddltunj2_onselectedchange"></asp:DropDownList> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttunj2" CssClass="form-control" runat="server" MaxLength="20" Text="0,00" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="lembur_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>    
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:DropDownList ID="ddltunj3" CssClass="selectpicker show-tick" required="required" runat="server" Enabled ="false" AutoPostBack="true" OnSelectedIndexChanged ="ddltunj3_onselectedchange"></asp:DropDownList> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttunj3" CssClass="form-control"  runat="server" MaxLength="20" Text="0,00" Enabled ="false"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="lembur_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>    
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:DropDownList ID="ddltunj4" CssClass="selectpicker show-tick" required="required" runat="server" Enabled ="false" AutoPostBack="true" OnSelectedIndexChanged ="ddltunj4_onselectedchange"></asp:DropDownList> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttunj4" CssClass="form-control" runat="server" MaxLength="20" Text="0,00" Enabled ="false"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="lembur_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>    
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:DropDownList ID="ddltunj5" CssClass="selectpicker show-tick" required="required" runat="server" Enabled ="false" AutoPostBack="true" OnSelectedIndexChanged ="ddltunj5_onselectedchange"></asp:DropDownList> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttunj5" CssClass="form-control" runat="server" MaxLength="20" Text="0,00" Enabled ="false"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="lembur_ontextchanged"></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>                           
                                                    </div>   
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <div class="">    
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tunjangan Hari Raya</p>
                                                            <asp:TextBox ID="txttunjhrRaya" Enabled="false" CssClass="form-control" required="required" runat="server" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);"  onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>  
                                                     <%-- <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Potongan SPSI</p>
                                                            <asp:TextBox ID="txtptgSPSI" CssClass="form-control" required="required" runat="server" Text="0,00" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox>   
                                                      </div>--%>                         
                                                <%--      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Absensi/Lembur Entry</p>
                                                                  <asp:DropDownList ID="ddlabsensi" CssClass="selectpicker show-tick" required="required" runat="server" ></asp:DropDownList>
                                                      </div>                 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Cara Hitung Lembur</p>
                                                                  <asp:DropDownList ID="ddlcaraHtglbr" CssClass="selectpicker show-tick" required="required" runat="server" ></asp:DropDownList>
                                                      </div>        
                                                      <div>
                                                          <div class ="form-row">
                                                                <div class="form-group col-md-2"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold ">Kurs Lembur</p> 
                                                                </div>
                                                                <div class="form-group col-md-10"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold ">=        (Gajipkk + Tunj)/173</p> 
                                                                </div>
                                                            </div> 
                                                            <asp:TextBox ID="txtkurslembur" CssClass="form-control" required="required" runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox>   
                                                      </div> 
                                                        --%>
                                                       
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Memo</p>
                                                            <asp:TextBox ID="txtmemo" CssClass="form-control" required="required" runat="server" MaxLength="50" TextMode="MultiLine">-</asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Potongan Pajak %</p>
                                                            <asp:TextBox ID="txtptgpjk" CssClass="form-control" required="required" runat="server" MaxLength="40"  onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Potongan Biaya Bank</p>
                                                            <asp:TextBox ID="txtptgbiayabank" CssClass="form-control" required="required" runat="server" MaxLength="40" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                         <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Non Aktif</p>
                                                            <asp:CheckBox ID="cbxnonaktf" runat="server"/>
                                                      </div >
                                                    </div>   
                                                 </div>  
                                            </div>           
                                </div>
                            </div> 

                        </div>
                    </div>
                </div>

        </asp:Panel>    
        <asp:Panel ID="Panel2" runat="server"  Visible ="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">                        
                              <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                               <div class="form-group col-md-6">
                                                  <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Type Potongan BPJS</p>
                                                             <asp:DropDownList ID="ddlpotbpjs"  CssClass="selectpicker show-tick"  runat="server" MaxLength="40" ></asp:DropDownList>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold">Dibayar Perusahaan</p>
                                                      </div>
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label1"  CssClass="form-control" required="required" runat="server" Text="Tunjangan Hari Tua" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txttuntua" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>     
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label2"  CssClass="form-control" required="required" runat="server" Text="Tunjangan BPJS Kematian" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txtbpjsmati" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>  
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label3"  CssClass="form-control" required="required" runat="server" Text="Tunjangan BPJS Kecelakaan" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txtbpjskecelakaan" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label4"  CssClass="form-control" required="required" runat="server" Text="Iuran Pensiun" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txtiuran" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold">Dibayar Karyawan</p>
                                                      </div>
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label5"  CssClass="form-control" required="required" runat="server" Text="Potongan BPJS hari tua" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txtbpjshrtua" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                            </div>
                                                      </div>
                                                      <div>
                                                            <div class ="form-row">
                                                                <div class="form-group col-md-5">
                                                                      <asp:Label ID="label6"  CssClass="form-control" required="required" runat="server" Text="Potongan Iuran Pensiun" ></asp:Label> 
                                                                </div>
                                                                <div class="form-group col-md-7">
                                                                     <asp:TextBox ID="txtpotiuran" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
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
                </div>

        </asp:Panel>

                 
                <%--<!-- Panel Edit --> 
                 <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row clearfix"> 
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row"> 
                                                    <div class="form-group col-md-6">
                                                  <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Nik</p>
                                                             <asp:TextBox ID="txtnik" CssClass="form-control" runat="server" MaxLength="10" Text="-" ReadOnly="true"></asp:TextBox>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nama Karyawan</p>
                                                            <asp:TextBox ID="txtnmkyn" CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox> 
                                                      </div>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Npwp Karyawan</p>
                                                            <asp:TextBox ID="txtnpwpkyn" CssClass="form-control" required="required" runat="server" MaxLength="20" Text="0" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                      </div>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nomor BPJS KetenagaKerjaan</p>
                                                            <asp:TextBox ID="txtnobpjs" CssClass="form-control" required="required" runat="server"  Text="0" MaxLength="15" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div>    
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tanggal Masuk BPJS KetenagaKerjaan</p>
                                                            <asp:TextBox ID="txttglbpjs" CssClass="form-control" required="required" runat="server"  type="date"></asp:TextBox> 
                                                      </div>           
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Pendidikan Terakhir</p>
                                                            <asp:DropDownList ID="ddlpddk" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Type Tenaga Kerja</p>
                                                            <asp:DropDownList ID="ddltngkrj" CssClass="form-control" required="required" runat="server">  
                                                                <asp:ListItem Text="1. WNI" Value="WNI"  Selected="true"/>
                                                                <asp:ListItem Text="2. WNA" Value="WNI"/>
                                                            </asp:DropDownList> 
                                                      </div>             
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Level Akses Terakhir</p>
                                                            <asp:DropDownList ID="ddllvl" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kantor</p>
                                                            <asp:DropDownList ID="ddloff" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                    
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Bagian</p>
                                                            <asp:DropDownList ID="ddlbag" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                    
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Jenis Kelamin</p>
                                                            <asp:DropDownList ID="ddlgen" CssClass="form-control" required="required" runat="server">  
                                                                <asp:ListItem Text="1. Laki - Laki" Value="L"  Selected="true"/>
                                                                <asp:ListItem Text="2. Perempuan" Value="P"/>
                                                            </asp:DropDownList> 
                                                      </div>                          
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Status PTKP</p>
                                                            <asp:DropDownList ID="ddlsttptkp" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                            
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Jabatan</p>
                                                            <asp:DropDownList ID="ddljab" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                           
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Golongan</p>
                                                            <asp:DropDownList ID="ddlgol" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                            
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Status Karyawan</p>
                                                            <asp:DropDownList ID="ddlsttskyn" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                      
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">System Bayar</p>
                                                            <asp:DropDownList ID="ddlsysbyr" CssClass="form-control" required="required" runat="server">   
                                                                <asp:ListItem Text="1. Tahunan" Value="Tahunan"  Selected="true"/> 
                                                                <asp:ListItem Text="2. Bulanan" Value="Bulanan"/>
                                                                <asp:ListItem Text="3. Mingguan" Value="Mingguan"/>
                                                            </asp:DropDownList> 
                                                      </div>                               
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Makan</p>
                                                            <asp:DropDownList ID="ddlkdmkn" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                            
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kode Transport</p>
                                                            <asp:DropDownList ID="ddlkdtrp" CssClass="form-control" required="required" runat="server"></asp:DropDownList> 
                                                      </div>                        
                                                    </div>   
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <div class="">    
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tanggal Diterima</p>
                                                            <asp:TextBox ID="txtjoin" CssClass="form-control" required="required" runat="server"  type="date"></asp:TextBox>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tanggal Keluar</p>
                                                            <asp:TextBox ID="txtout" CssClass="form-control" required="required" runat="server"  type="date"></asp:TextBox>   
                                                      </div>                         
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Cara Pembayaran</p>
                                                            <asp:DropDownList ID="ddlbyr" CssClass="form-control" required="required" runat="server"> 
                                                                <asp:ListItem Text="1. Transfer" Value="T"  Selected="true"/> 
                                                                <asp:ListItem Text="2. Cash" Value="C"/> 
                                                            </asp:DropDownList> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nama Bank</p>
                                                            <asp:TextBox ID="txtnmbank" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>   
                                                      </div>                         
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Rek. Bank Perusahaan</p>
                                                            <asp:DropDownList ID="ddlrekpersh" CssClass="form-control" required="required" runat="server" onkeypress="return isnumerickey(event);"></asp:DropDownList> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Nama Bank Perusahaan</p>
                                                            <asp:TextBox ID="txtnmbakpersh" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Cabang Bank Karyawan</p>
                                                            <asp:TextBox ID="txtcbgbank" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Rek. Bank Karyawan</p>
                                                            <asp:TextBox ID="txtrekkyn" CssClass="form-control" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div> 
                                                            <p class="mb-1 mt-4 font-weight-bold">Pinjaman Karyawan</p>
                                                      </div>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Plafon Pinjaman/Thn</p>
                                                            <asp:TextBox ID="txtplafonpnjm" CssClass="form-control" required="required" runat="server" text="0,00" MaxLength="40" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Jumlah Pinjaman</p>
                                                            <asp:TextBox ID="txtjmlhpnjm" CssClass="form-control" required="required" runat="server" MaxLength="40" text="0,00" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Angsuran S/d Bln ini</p>
                                                            <asp:TextBox ID="txtangs" CssClass="form-control" required="required" runat="server" MaxLength="40" text="0,00" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div> 
                                                            <p class="mb-1 mt-4 font-weight-bold">Cuti Karyawan</p>
                                                      </div>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Plafon cuti</p>
                                                            <asp:TextBox ID="txtplafonct" CssClass="form-control" required="required" runat="server" text="0,00" MaxLength="40" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Pakai Cuti S/d Bulan Lalu</p>
                                                            <asp:TextBox ID="txtctblnll" CssClass="form-control" required="required" runat="server" MaxLength="40" text="0,00" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div>  
                                                      <div> 
                                                            <p class="mb-1 mt-4 font-weight-bold">Tunjangan Obat</p>
                                                      </div>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Tunjangan plafon obat/Thn</p>
                                                            <asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" text="0,00" MaxLength="40" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold ">Obat Terpakai S/d Bulan Lalu</p>
                                                            <asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="40" text="0,00" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                      </div>  
                                                      <div class="form-group">
                                                            <p class="mb-1 mt-4 font-weight-bold ">Kena PPh</p>
                                                            <asp:CheckBox ID="cbNonAktif" runat="server"/>
                                                      </div >  
                                                    </div> 
                                                 </div>  
                                            </div> 
                         </asp:Panel>--%> 
                        <asp:Panel ID="Pnlfooter" Visible="false" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card-box">
                                        <div class="card-box table-responsive">
                                            <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menyimpan data ini ?');" runat="server" Text="Save" OnClick="btnSimpan_Click" />
                                            <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader_Click" Width="65px"  Visible ="false"/>
                                        </div>
                                    </div>
                                </div>
                            </div> 
                        </asp:Panel> 
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <%--<asp:PostBackTrigger ControlID="btnSimpan" />--%>
                <asp:PostBackTrigger ControlID="btnCancel" /> 
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


