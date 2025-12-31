<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MelihatKartuSupplier.aspx.cs" Inherits="SD_View_MelihatKartuSupplier" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Melihat Kartu Sumber Dana</title>
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
                background: white url("/assets/images/Cube-1s-30px.gif") right center no-repeat;
            }
        </style>

        <script type="text/javascript">
            function checkTextField1(field) {
            if (field.value == '' || field.value == 0 || field.value == '.') {
                field.value = 0;
            }


            var nilai = field.value.split(',').join('');
            var output = nilai;
            if (parseFloat(nilai)) {
                input = new String(nilai); // so you can perform string operations
                var parts = input.split("."); // remove the decimal part
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1").split("").reverse().join("");
                output = parts.join(".");
            }
            field.value = output;
        }

        
        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

        function Simpan() {

            setTimeout(function () {
                swal(
                    {
                         title: "Data berhasil disimpan",  
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

       

        $(document).ready(InIEvent);
        function InIEvent() { 

            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            
                $("#txtkodesupplier").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "MelihatKartuSupplier.aspx/GetKodeLgn",
                            data: "{'InputText':'" + document.getElementById('txtkodesupplier').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
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
                                    <li class="breadcrumb-item"><a href="#">Home</a></li>
                                    <li class="breadcrumb-item"><a href="#">Sumber Dana</a></li>
                                    <li class="breadcrumb-item"><a href="#">View</a></li>
                                    <li class="breadcrumb-item"><a href="MelihatKartuSupplier.aspx">Melihat Kartu Sumber Dana</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Melihat Sederhana Berdasarkan Kode ID Sumber Dana</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
               <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                     <div class="row">
                        <div class="col-lg-6">
                            <div class="card-box ribbon-box">
                              <div class="ribbon ribbon-custom">Filter</div>
                                <p class="text-muted font-14 m-b-30">
                                  <div class="form-row">
                                    <div class="form-group col-md-3">
                                         <label for="inputState" class="col-form-label">Kode Sumber Dana</label>
                                         <asp:HiddenField ID="hdnZipCode" runat="server" />
                                     </div>
                                     <div class="form-group col-md-12">
                                         <asp:TextBox CssClass="form-control" ID="txtkodesupplier" Enabled="true" runat="server" required="required" ></asp:TextBox>
                                         <span id="lblsupplier" visible="false" class="text-muted" style="font-size: 10px"></span>
                                     </div>  
                                     <div class="form-group col-md-6">
                                        <label for="inputPassword2" class="sr-only">Search</label>
                                        <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search"  OnClick="btnSearch_Click"/>                                              
                                     </div>
                                 </div>
                            </div> 
                        </div> 
                    </div>

                 </asp:Panel>
                 <!-- Panel Header -->
               <asp:Panel ID="Panel1" Visible="false" runat="server">
                     <div class="row">
                        <div class="col-12">
                            <div class="card-box ribbon-box">
                                <div class="ribbon ribbon-custom">DATA SUMBER DANA</div>
                                <p class="m-b-0">
                                <div class="form-row">
                                        <div class="form-group col-md-6">  
                                            <asp:HiddenField ID="HDlast_date" runat="server" />	 
                                            <asp:HiddenField ID="HDkepala_cabang" runat="server" />	 
                                            <asp:HiddenField ID="HDNamaCabang" runat="server" />
                                            <div class="form-group">
                                                    <asp:label id="Label1" runat="server"   class="col-form-label">Nama Sumber Dana</asp:label>
                                                    <asp:TextBox ID="txtnamasupplier"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                            </div>
                                            <div class="form-group">
                                                    <asp:label id="Label6" runat="server"  class="col-form-label">Alamat</asp:label>
													<asp:TextBox ID="txtalamat"  CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="5" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                            </div>
                                            <%--<div class="form-group">
                                                    <asp:label id="Label7" runat="server"   class="col-form-label">Nama</asp:label>
                                                    <asp:TextBox ID="txtnama"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                            </div>--%>

                                            <div class="form-group">
                                                <asp:label id="Label11" runat="server"  class="col-form-label">Syarat Bayar</asp:label>
                                                <asp:TextBox ID="txtsyaratbayar"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>   
                                            </div>

                                            <div class="form-group">
                                                <asp:label id="Label9" runat="server"  class="col-form-label">Saldo Awal</asp:label>
                                                <asp:TextBox ID="txtsaldoawal"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                            </div>
                                            <div class="form-group">
                                                <asp:label id="Label2" runat="server"  class="col-form-label">Kode CRC</asp:label>
                                                <asp:TextBox ID="txtkodecrc"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>   
                                            </div>
                                          </div>
                                          <div class="form-group col-md-6"> 

                                                <div class="form-group">
                                                        <asp:label id="Label4" runat="server"   class="col-form-label">Saldo Awal</asp:label>
                                                        <asp:TextBox ID="txtsaldoawal1"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                                <div class="form-group">
                                                        <asp:label id="Label5" runat="server"   class="col-form-label">Saldo Awal Uang Muka</asp:label>
                                                        <asp:TextBox ID="txtsaldoawalum"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                                <div class="form-group">
                                                        <asp:label id="Label3" runat="server"   class="col-form-label">Saldo Berjalan</asp:label>
                                                        <asp:TextBox ID="txtsaldoberjalan"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                         
                                                <div class="form-group">
                                                    <label class="col-form-label">Saldo Uang Muka</label>
                                                    <asp:TextBox ID="txtsaldouangmuka"  CssClass="form-control"  Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>      
                                                </div>
                                              <div class="form-group">
                                                    <label class="col-form-label">Saldo Uang Muka Akhir</label>
                                                    <asp:TextBox ID="Txtsaldoumakhir"  CssClass="form-control"  Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>      
                                                </div>
                                              <div class="form-group">
                                                    <label class="col-form-label">Saldo Netto</label>
                                                    <asp:TextBox ID="txtsaldonetto"  CssClass="form-control"  Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>      
                                                </div>
                                              <div class="form-group">
                                                    <label class="col-form-label">Batas Credit</label>
                                                    <asp:TextBox ID="txtbataskredit"  CssClass="form-control"  Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>      
                                                </div>
                                              <div class="form-group">
                                                    <label class="col-form-label">Over Credit</label>
                                                    <asp:TextBox ID="txtoverkredit"  CssClass="form-control"  Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>      
                                                </div>
                                        </div>
                                </div> 
                            </div>
                        </div>
                    </div>
            </asp:Panel>
                  <!-- Panel Filter date -->
               <asp:Panel ID="Panel3" Visible="false" runat="server">
                     <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                               <div class="ribbon ribbon-custom">Filter Date</div>
                                 <p class="m-b-0">
                                     <div class="row">
                                         <div class="col-md-12">
                                             <div class="card-box">
                                                 <div class="form-row">
                                                      <div class="form-group col-md-4">
                                                         <label for="inputState" class="col-form-label">This Month From</label>
                                                          <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>     
                                                     </div>
                                                     <div class="form-group col-md-4">
                                                         <label for="inputZip" class="col-form-label">This Month To</label>
                                                         <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>  
                                                     </div>
                                                  </div>
                                                     <div class="form-group"> 
                                                             <asp:Button ID="btnDetilSupplier" class="btn btn-success waves-light text-white" runat="server" Text="Detail Supllier"  OnClick="btnDetilSupplier_Click" /> 
                                                     </div>  
                                             </div>
                                           </div>
                                    </div> 
                            </div> 
                        </div> 
                    </div>

                 </asp:Panel>
                 <!-- Panel Front -->
       <asp:Panel ID="PnlFront" runat="server" Visible="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Melihat Kartu Supplier</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRData_RowCommand" OnRowDataBound="GRData_RowDataBound" ShowHeaderWhenEmpty ="True" AutoGenerateColumns="False" Width="625px" ShowFooter="True">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="Tgl.Faktur">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljenisgol" runat="server"  Text='<%# Eval("TglTrnFaktur", "{0:dd-MM-yyyy}") %>' ></asp:Label>
                                         </ItemTemplate>
                                        <FooterTemplate>
                                         <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                         </FooterTemplate>
                                        <HeaderStyle Height="5px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nomor Faktur">
                                        <ItemTemplate>
                                            <asp:Label ID="Lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoFaktur")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="5px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nomor Bukti">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tgl. Transaksi">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltgltransaksi" runat="server" DateFormat="dd-mm-yyyy" Text='<%# Eval("TglTrn", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tgl Jth Tmp">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltgljthtempo" runat="server" DateFormat="dd-mm-yyyy" Text='<%# Eval("TglJthTmp", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jumlah Cair">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmlcair" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JumlahTrn","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                          <FooterTemplate>
                                          <asp:Label ID="lbltotalcair" runat="server"  />
                                          </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                  <%--  <asp:TemplateField HeaderText="Jumlah Giro">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmlgiro" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JumlahTrn")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Jlh Uang Muka">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmluangmnuka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jumlahumk","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Label ID="lbltotalumk" runat="server"  />
                                          </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Keterangan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblketerangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Bank Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbankcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BankGiro")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No Giro">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgiro" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nogiro")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="JumlahTrn" HeaderText="Class" FooterText="Total" />
                                    <asp:BoundField DataField="JumlahTrn" HeaderText="Students" />--%>
                                </Columns>
                            </asp:GridView>                           
                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>
                 <!-- Panel Edit -->
              
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
                 <Triggers>
                   <asp:PostBackTrigger ControlID="txtkodesupplier" />
                     <asp:PostBackTrigger ControlID="btnSearch" />
                   <asp:PostBackTrigger ControlID="btnDetilSupplier" /> 
                </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  <script>
      function deletealert(ctl, event) {
    // STORE HREF ATTRIBUTE OF LINK CTL (THIS) BUTTON
          var defaultAction = $(ctl).prop("href");
          var number=document.getElementById("txtjenisgol").value;
    // CANCEL DEFAULT LINK BEHAVIOUR
    event.preventDefault();
    swal({
        
                     title: "Data berhasil disimpan",
                     text: 'Nomer nasabah anda :' + number ,
        type: "success",
        position: "top-end",
               //   icon: "success",
                  showConfirmButton: false,
                  timer: 1500
        //showCancelButton: true,
        //confirmButtonColor: "#DD6B55",
        //confirmButtonText: "Yes, delete it!",
        //cancelButtonText: "No, cancel plx!",
        //closeOnConfirm: false,
        //closeOnCancel: false
    },
    function () {
      // if (isConfirm) {
        //    swal({ title: "Deleted!", text: "Your imaginary file has been deleted.", type: "success", confirmButtonText: "OK!", closeOnConfirm: false },
           // function (isConfirm) {
                // RESUME THE DEFAULT LINK ACTION
                window.location.href = defaultAction;
                return true;
            //});
       // } else {
        //    swal("Cancelled", "Your imaginary file is safe :)", "error");
         //   return false;
       // }
    });
}
      //function myFunction(ctl, event) {
      //            Swal.fire({
      //              position: "top-end",
      //              icon: "success",
      //                title: "Data berhasil disimpan",
      //                text: 'Nomer nasabah anda :' + document.getElementById("#txtjenisgol").value(),
      //              showConfirmButton: false,
      //              timer: 1500
      //            });
      //          }
      </script>
    
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
