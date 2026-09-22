<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PembuatanKodeAktiva.aspx.cs" Inherits="FA_Master_PembuatanAktiva" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Pembuatan Kode Aktiva</title>
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

       <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>



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

        function AlertSuccess(h, t, p, i, fr) {
            console.log(fr)
            setTimeout(function () {


                Swal.fire({
                    title: h,
                    text: t,
                    icon: "success"
                }).then((result) => {
                    if (fr === 'true') {
                        window.location.href = "PembuatanKodeAktiva.aspx"
                    }
                });;
            }, 100);

        }


        function CheckFileExistence() {

            var filePath = document.getElementById('<%= this.FileUpload1.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto belum di pilih!");


                return false;
            }
        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        //confirmButtonClass: 'btn btn-confirm mt-2'
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
        function PeringatanKode(a) {
            setTimeout(function () {
                swal(
                    {
                        title: a,
                        type: "warning",
                        position: "top-end",
                        showConfirmButton: false,
                        timer: 2000
                    }
                ).catch(swal.noop)
            }, 100);
        }
        function Delete(a, b, c, d) {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil di hapus",
                        text: a + ' ' + ':' + ' ' + b + ' ' + c + ' ' + ':' + ' ' + d,
                        type: "success",
                        position: "top-end",
                        showConfirmButton: false,
                        timer: 2000,
                        hideAfter: 2000,
                        afterHidden: function () {
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }
                    }
                ).catch(swal.noop)
            }, 100);
            location.href = 'Lokasi.aspx ';
        }
        function Simpan(a, b, c, d) {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil disimpan",
                        text: a + ' ' + ':' + ' ' + b + ' ' + c + ' ' + ':' + ' ' + d,
                        type: "success",
                        position: "top-end",
                        //   icon: "success",
                        showConfirmButton: false,
                        timer: 2000
                        //title: errMsg,
                        //confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);
            $("#txtjenisgol").val("");
            $("#txtketerangan").val("");
        }

        function sweetAlertsave(btnSimpan, errMsg) {
            if (btnSimpan.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSimpan.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: errMsg,
                    //confirmButtonClass: 'btn btn-confirm mt-2',
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
                    //confirmButtonClass: 'btn btn-confirm mt-2',
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
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9 ]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
        function text(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
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
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Fixed Assets</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="PembuatanKodeAktiva.aspx">Pembuatan Kode Aktiva</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Master Pembuatan Kode Aktiva</h4>
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
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Aktiva" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">

                                    <h4 class="m-t-0 header-title">Data Buat Kode Aktiva</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">

                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Kode Aktiva">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkdaktiva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeAktiva")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Aktiva">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnmaktiva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaAktiva")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kode Group">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkdgroup" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeGroup")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kode Lokasi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkdlokasi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeLokasi")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nomor Aktiva">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoaktiva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoAktiva")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Spesifikasi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpesifikasi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Spesifikasi")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Merk">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMerek" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Merek")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nomor Mesin">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoMesin" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoMesin")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nomor Chasis">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoChasis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoChasis")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kode Golongan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKodeGol" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeGol")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kode Kelompok">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKodeKel" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeKel")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Keterangan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKeterangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Masa Susut (Bulan)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsusutbulan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaBulan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Masa Susut (Tahun)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsusuttahun" runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>--%>

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
                                             

                                            <div class="row">
                                                                <div class="col-md-4">
                                                                     <p class="mb-1 mt-4 font-weight-bold ">No Aktiva</p>
                                                                  <asp:DropDownList ID="ddlgroup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >  </asp:DropDownList>
                                                      
                                                                </div>

                                                               <div class="col-md-4">
                                                     
                                                     
                                                            <p class="mb-1 mt-4 font-weight-bold ">No Aktiva</p>
                                                             <asp:TextBox ID="txtkodegroup" CssClass="form-control"  runat="server" enabled="false" ></asp:TextBox>
                                                      </div>                          
                                                              
                                                                                                                

                                                         </div>
                                              <div class="form-group">
                                                  </div>
                                            <div class="form-group">
                                                  </div>
                                            <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Nama Aktiva</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtnamaaktiva" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>

                                           <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Kode Lokasi</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                       <asp:DropDownList ID="ddllokasi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                            

                                                <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Nomor Assets</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtnomoraktiva" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>         

                                                <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Spesifikasi</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtspek" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>    

                                                  <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Merk</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtmerk" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>   

                                                   <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Nomor Mesin</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtnomormesin" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>  
                                       
                                          
                                                   <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Nomor Chasis</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtnomorchasis" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>  
                                            

                                                  <div class ="form-row">
                                         
                                                                <div class="col-md-3">
                                                                    <p class="mb-1 mt-3 font-weight-bold ">Catatan</p>

                                                                        <%--<asp:TextBox ID="TextBox2" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok Max" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                 <div class="col-md-8">
                                                                    <asp:TextBox ID="txtketerangan" CssClass="form-control" runat="server" TextMode="MultiLine" Height="145px" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>  
                                       
                                         
                                        </div>
                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label7" runat="server" class="col-form-label">Kelompok Aktiva</asp:Label>
                                                <asp:DropDownList ID="ddlkelompok" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlkelompok_SelectedIndexChanged" required="required"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" class="col-form-label">Golongan Aktiva</asp:Label>
                                                <asp:TextBox ID="txtgolaktiva" CssClass="form-control" runat="server" data-style="btn-custom" Enabled="false" MaxLength="1"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" class="col-form-label">Masa Susut</asp:Label>
                                                <asp:TextBox ID="txtmasasusut" CssClass="form-control" Enabled="false" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label13" runat="server" class="col-form-label">Masa Susut(Bulan)</asp:Label>
                                                <asp:TextBox ID="txtmasasusutbulan" CssClass="form-control" Enabled="false" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label8" runat="server" Visible="false" class="col-form-label">Met. Penyusutan Akuntansi</asp:Label>
                                                <asp:TextBox ID="txtmetodeakuntansi" CssClass="form-control" Visible="false" Enabled="false" runat="server" MaxLength="10"></asp:TextBox>
                                                <asp:TextBox ID="txtakuntansi" CssClass="form-control" Visible="false" Enabled="false" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label9" runat="server" class="col-form-label">Met. Penyusutan UU</asp:Label>
                                                <asp:TextBox ID="txtmotedeUU" CssClass="form-control" Enabled="false" runat="server" MaxLength="20"></asp:TextBox>
                                                <asp:TextBox ID="txtuu" CssClass="form-control" Enabled="false" runat="server" MaxLength="20"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" class="col-form-label">Aktiva Aktif</asp:Label>
                                                <asp:CheckBox ID="chkaktiva" runat="server" />
                                                <asp:Label ID="Label11" runat="server" class="col-form-label">Boleh Disusut</asp:Label>
                                                <asp:CheckBox ID="chkboleh" runat="server" />
                                            </div>
                                            
                                            <div class="form-group" visible="false" id="ir">
                                                <asp:Label ID="Label4" visible="false" runat="server" class="col-form-label">Barcode ID</asp:Label>
                                                <asp:TextBox ID="txtbarcode" visible="false" CssClass="form-control" Enabled="false" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div>
                                                <asp:FileUpload ID="FileUpload1" runat="server" visible="false" />
                                                <asp:Button ID="btnUpload" Visible="false"  Text="Upload" runat="server" OnClick="UploadFile" OnClientClick="return CheckFileExistence()" />
                                                <hr />
                                                <asp:TextBox ID="txtpath" visible="false" CssClass="form-control" Enabled="false" runat="server" MaxLength="400"></asp:TextBox>
                                                <hr />
                                                <asp:Image ID="Image1" visible="false" runat="server" Height="140px" Width="280px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="btn-group mb-2">
                                <div class="button-list">
                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />
                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
                                </div>
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
