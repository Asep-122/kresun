<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Taspen.aspx.cs" Inherits="BG_Master_Taspen" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>DATA BAHAN PENSIUN / TASPEN</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
    <link rel="stylesheet" href="plugins/tooltipster/tooltipster.bundle.min.css" />
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

            <!-- Plugins css-->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/select.bootstrap4.min.css" rel="stylesheet" type="text/css" />



      <link href="../../plugins/bootstrap-tagsinput/css/bootstrap-tagsinput.css" rel="stylesheet" />
        <link href="../../plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" href="../../plugins/switchery/switchery.min.css" />
    

    <!-- App css -->
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../assets/js/modernizr.min.js"></script>
        <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/waves.js"></script>
    <script src="../../assets/js/jquery.slimscroll.js"></script>

  <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
   
    <%--<script src="../assets/js/bootstrap.min.js"></script>--%>
  

    <script src="../../Javascript/jquery-ui.js"></script>
   
    <script type="text/javascript">
   <%--           function showProgress() {
var updateProgress = $get("<%= UpdateProgress.ClientID %>");
updateProgress.style.display = "block";
}--%>
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
        function redirect() {
		  location.href = 'Produk.aspx ';
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

         //numeric 1
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

        //numeric 2
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


          //numeric 3
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
              $('#datatable').DataTable();

                //Buttons examples
                var table = $('#Grdtagihan').DataTable({
                    lengthChange: false
                    //buttons: ['copy', 'excel', 'pdf']
            });

           
            
            $('#txttglptkp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            $('.selectpicker').selectpicker();

            
                 table.buttons().container()
              .appendTo('#Grdtagihan_wrapper .col-md-6:eq(0)');

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
              $(function () {
             
                var table = $('#Grdtagihan').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



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
                                    <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="Taspen.aspx">Taspen</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">DATA BAHAN PENSIUN / TASPEN</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter"  runat="server">
                 <div class="row">
                          
               
                                  
                    
                    <div class="col-lg-6">

                        <div class="card-box">
                            <h4 class="header-title m-t-0">Search By</h4>
            
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" class="col-3 col-form-label">Area</asp:Label>
                                 <asp:DropDownList ID="ddlcabang" CssClass="selectpicker show-tick btn-dark"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlcabang_SelectedIndexChanged"></asp:DropDownList>                                   
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblcabang" runat="server" class="col-3 col-form-label">Cabang Taspen</asp:Label>
                                     <asp:DropDownList ID="ddlcabangprodukAsrn" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged="ddlcabangprodukAsrn_SelectedIndexChanged"></asp:DropDownList>
                                     
                                </div>
                                <div class="form-group">
                                     <asp:Label ID="Label4" runat="server" class="col-3 col-form-label">Kantor Bayar</asp:Label>
      
                                        <asp:DropDownList ID="ddlKantor" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                            
                                </div>
                                <div class="form-group">
                                     <asp:Label ID="Label6" runat="server" class="col-3 col-form-label">Jenis Usia</asp:Label>
                                         <asp:DropDownList ID="ddljenisusia" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >
                                                                <asp:ListItem Value="ALL">All Jenis Usia</asp:ListItem>
                                                                <asp:ListItem Value="1">Reguler</asp:ListItem>
                                                                <asp:ListItem Value="2">Platinium</asp:ListItem>
                                                               
                                                            </asp:DropDownList>     
                                    </div>
                                 
                         </div> <!-- end card-box -->
                    </div>

                             <div class="col-lg-6">

                        <div class="card-box">
                            <h4 class="header-title m-t-0">Search By</h4>
                          
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" class="col-3 col-form-label">Jenis Pensiun</asp:Label>
                                    <asp:DropDownList ID="ddljenispensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>                                   
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" class="col-3 col-form-label">Jenis Jiwa</asp:Label>
                                     <asp:DropDownList ID="ddljenisjiwa" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                     
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" class="col-3 col-form-label">Jenis Stop</asp:Label>
                                        <asp:DropDownList ID="ddljenisstop" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                            
                                </div>
                                <div class="form-row" runat="server" visible="false">
                                <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Alamat</label>
                                         <asp:TextBox ID="txtalamat" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>

                                <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Daerah Tingkat 4</label>
                                         <asp:TextBox ID="txtnmdati4" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>
                                 <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Kota</label>
                                         <asp:TextBox ID="txtkotasearch" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>
                                     </div>


                        </div> <!-- end card-box -->
                                 <div class="form-group text-right m-b-0">
                                    
                                 <asp:Button ID="btnsearch" class="btn-custom" runat="server" Text="Search" OnClick="lnkCari_Click" />  
                                </div>
                    </div>


                    

                         

                </div>
                </asp:Panel>
                 <!-- Panel Header -->
               <asp:Panel ID="pnlHeader" Visible="false" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                           <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                            <asp:Button Visible="true" ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Produk" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">DATA BAHAN TASPEN</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="Grdtagihan" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"
                                                                      AutoGenerateColumns="False"  AllowSorting="True" OnRowCommand="Grdtagihan_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="+">
                                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <%--<asp:LinkButton ID="Lnkbakanfollwoaaa" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Follow Up" runat="server" CommandName="APPROVE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="icon-like"></i></asp:LinkButton>--%>
                                                                                                    <asp:LinkButton ID="LnkView2" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">View</asp:LinkButton> 
                                                                                            <asp:Label ID="lblnourut" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tanggal Lahir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltgllahirpsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Usia">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="Nama Pensiun">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namapensiunan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                                                                    <asp:TemplateField HeaderText="Alamat">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dati 4">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldati4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "dati4")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Kota">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kota")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Notas">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnotas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notas")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Nama Penerima" Visible="false">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnamapenerima" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_penerima")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                                                                    <asp:TemplateField HeaderText="Gaji">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgaji" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
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
                                         <div class="col-lg-6">

                         <div class="card-box">
                                 <h4 class="m-t-q header-title">Data Pensiun</h4>

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Notas</label>
                                          <asp:TextBox ID="txtnotas" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Jenis Pensiun</label>
                                         <asp:TextBox ID="Txtjenis" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                                   

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Nama Pensiunan</label>
                                          <asp:TextBox ID="txtnamapensiun" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Tgl Lahir Pensiunan</label>
                                          <asp:TextBox ID="txttgllahir" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                   
                                </div>
                                 

                             <div class="form-row">
                                    
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Kode Pangkat</label>
                                         <asp:TextBox ID="txtkodepangkat" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                  <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Ket Jiwa</label>
                                          <asp:TextBox ID="txtjiwa" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Stop</label>
                                         <asp:TextBox ID="txtstop" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Kode Cabang</label>
                                          <asp:TextBox ID="txtcabang" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Telepon</label>
                                         <asp:TextBox ID="txttelepon" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>


                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Kode Bayar</label>
                                          <asp:TextBox ID="txtkodebyr" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Nama Bayar</label>
                                         <asp:TextBox ID="txtnamabyr" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                             
                                                                  
                                    
                                    
                                    
                                </div>

                                 </div>

                

                            <div class="col-lg-6">

                        <div class="card-box">
                             <h4 class="m-t-0 header-title">Pendapatan</h4>
                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Pendapatan Pokok</label>
                                          <asp:TextBox ID="txtpenpok" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tunjangan Istri</label>
                                         <asp:TextBox ID="txttistri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Tunjangan Anak</label>
                                          <asp:TextBox ID="txttanak" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tunjangan Pajak</label>
                                         <asp:TextBox ID="txttpajak" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Tunjangan Beras</label>
                                          <asp:TextBox ID="Txttberas" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Penyesuaian</label>
                                         <asp:TextBox ID="txtpenyesuaian" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                           
                           <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Pembulatan</label>
                                          <asp:TextBox ID="txttbulat" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Kotor</label>
                                         <asp:TextBox ID="TxtKotor" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Bersih</label>
                                          <asp:TextBox ID="TxtBersih" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Rapel</label>
                                         <asp:TextBox ID="TxtRapel" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                             
                           
                        </div> <!-- end card-box -->
                    </div>


                            <div class="col-lg-6">

                        <div class="card-box">
                             <h4 class="m-t-0 header-title">Penerima</h4>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Nama Penerima</label>
                                          <asp:TextBox ID="Txtpenerima" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tanggal Lahir Penerima</label>
                                         <asp:TextBox ID="Txttgllahirpenerima" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                     
                            <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Alamat Penerima</label>
                                            <asp:TextBox ID="Txtalamatpenerima" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>                                                       
                            </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Dati 4</label>
                                          <asp:TextBox ID="Txtdati4" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Kota</label>
                                         <asp:TextBox ID="txtkota" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                         
                  
                        </div> <!-- end card-box -->
                    </div>

                            <div class="col-lg-6">

                        <div class="card-box">
                             <h4 class="m-t-0 header-title">Lain-Lain</h4>
                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">No.Urut.Dapem</label>
                                          <asp:TextBox ID="txtnourutdapem" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tmt.Pensiun</label>
                                         <asp:TextBox ID="Txttmppensiun" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                           <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">No.Skep</label>
                                          <asp:TextBox ID="TxtNoskep" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label for="inputState" class="col-form-label">Tanggal Skep</label>
                                         <asp:TextBox ID="TxttglSkep" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   <div class="form-group col-md-5">
                                        <label for="inputState" class="col-form-label">Penerbit Skep</label>
                                         <asp:TextBox ID="Txtpenerbitskep" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Norek</label>
                                          <asp:TextBox ID="Txtnorek" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                   <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Npwp</label>
                                          <asp:TextBox ID="txtnpwp" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                            
                                   
                                </div>
                         
                         
                  
                        </div> <!-- end card-box -->
                    </div>

                                         </div>        
                  
                                            <div class="row">
                                                <div class="btn-group mb-2">
                                                   <div class="button-list">
                                                   
                                                    
                                                        <asp:Button ID="btnSimpan" Visible="false" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />
                                                        <asp:Button ID="btnCancel" class="btn-custom" runat="server" Text="Back" OnClick="btnCancel_Click" /> 
                                                   </div>
                                                    </div>
                                            </div>
    
            </asp:Panel>
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                  <asp:PostBackTrigger ControlID="ddlcabangprodukAsrn" />
                  <asp:PostBackTrigger ControlID="btnsearch" />
                 
            </Triggers>
        </asp:UpdatePanel>
        <%--<asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
<ProgressTemplate>
<div class="overlay">
<div style=" z-index: 1000; margin: 300px auto;padding: 10px;width: 160px;background-color: White;border-radius: 10px;filter: alpha(opacity=100);opacity: 1;-moz-opacity: 1;">
<asp:Image ID="Image1" ImageUrl="/assets/images/loadx.gif" AlternateText="Processing" runat="server" Width="136px"/>
</div>
</div>
</ProgressTemplate>
</asp:UpdateProgress>--%>
    </form>
</body>
  
    <!-- App js -->
<script src="../../assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>
  <script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<!-- Tooltipster js -->
<script src="../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>  

<script src="../../plugins/tablesaw/js/tablesaw.js"></script>
<script src="../../plugins/tablesaw/js/tablesaw-init.js"></script>
<script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
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

</html>
