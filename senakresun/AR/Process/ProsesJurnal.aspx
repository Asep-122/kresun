<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProsesJurnal.aspx.cs" Inherits="AR_Process_ProsesJurnal" %>


<!DOCTYPE html>


    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="utf-8" />
            <title>Proses Jurnal</title>
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
                    function Resize() {
                setTimeout(function (e) {
                    //  alert('z');
                    window.top.cekResize();
                    var y;
                    y = window.parent.document.getElementById('txtScrollY').value;
                    setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
                }, 50);

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

                
           $(document).ready(InIEvent);
            function InIEvent() {
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
                $('.selectpicker').selectpicker();

                $('form').parsley();

            
            <%--    $("#txtnonasabah").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "CetakKeanggotaan.aspx/GetNoNasabah",
                            data: "{'InputText':'" + document.getElementById('txtnonasabah').value + "','SessionBranchID':'<%=SessBranchID%>'}",
                             dataType: "json",
                            success: function (data) {
                                obj = JSON.parse(stringToArray(data.d));
                                console.log(obj);
                                var found = $.map(obj, function (item) {
                                    return {
                                        label: item[0],  
                                        hdnZipCode: item[1]
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
                });--%>
          

            
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
                  <%--  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
                   --%>                             
                    <div class="container-fluid">

                     <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Account Receivable</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="ProsesJurnal.aspx">Proses Jurnal</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Proses Jurnal</h4>
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
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                                <div class="form-group col-md-6">
                                                                    <label class="col-form-label">Jenis Jurnal</label>
                                                                    <asp:DropDownList ID="ddlJurnal" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlJurnal_SelectedIndexChanged" ></asp:DropDownList>
                                                                 
                                                                </div> 
                                                         </div> 
                                                    </div>
                                                  </div>
                                           </div> 
                            </div> 
                        </div> 
                    </div>

                 </asp:Panel>

            <asp:Panel ID="pnlSetoran" Visible="true" runat="server">
                     <div class="row">
                        <div class="col-12">
                            <div class="card-box ribbon-box">
                                <div class="ribbon ribbon-custom">Jurnal Setoran Tunai Keanggotaan</div>
                                <p class="m-b-0">
                                <div class="form-row">
                                        <div class="form-group col-md-6">   
                                            <div class="form-group">
                                                    <asp:label id="Label1" runat="server"   class="col-form-label">No. Rekening</asp:label>
                                                    <asp:TextBox ID="txtnorekening"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                            </div>

                                            <div class="form-group">
                                                    <asp:label id="Label7" runat="server"   class="col-form-label">Nama</asp:label>
                                                    <asp:TextBox ID="txtnama"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                            </div>

                                            <div class="form-group">
                                                <asp:label id="Label11" runat="server"  class="col-form-label">Jumlah</asp:label>
                                                <asp:TextBox ID="txtjumlah"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>   
                                                        
                                            </div>

                                           

                                           
                                          </div> 
                                         <div class="form-group col-md-6">   
                                              <div class="form-group">
                                                    <asp:label id="Label6" runat="server"  class="col-form-label">Keterangan</asp:label>
													<asp:TextBox ID="txtketerangan"  CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="5" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                            
                                            </div>
                                         </div>
                                </div> 
                            </div>
                        </div>
                    </div>
        
                <div class="hidden-print mt-4"> 
                    <div class="float-right mb-2 ">  
                        <asp:Button ID="btnCetakAnggota" align="right" class="btn btn-success waves-light text-white" runat="server" Text="PROSES"   OnClientClick="return confirm('Apakah anda yakin ingin mencetak data ini ?');"/> 
                    </div>
                </div> 
            </asp:Panel> 

              <asp:Panel ID="pnlFilterTutupAnggota" Visible="true" runat="server">
                     <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                      <div class="ribbon ribbon-custom">Filter</div>
                                        <p class="m-b-0">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                                <div class="form-group col-md-6"> 
                                                                
                                                                    <div class="form-group">
                                                                        <label for="inputState" class="col-form-label">No Nasabah</label>
                                                                         <asp:TextBox CssClass="form-control" ID="txtnonasabah" Enabled="true" runat="server" required="required"></asp:TextBox>
                                                                   </div>  
                                                                    <div class="form-group">
                                                                        <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search"   />                                              
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-md-6"> 
                                                                    
                                                                </div> 

                                                         </div> 
                                                    </div>
                                                  </div>
                                           </div> 
                            </div> 
                        </div> 
                    </div>

                 </asp:Panel>
                 <asp:Panel ID="pnlTutupAnggota" Visible="true" runat="server">
                     <div class="row">
                        <div class="col-12">
                            <div class="card-box ribbon-box">
                                <div class="ribbon ribbon-custom">Tutup Keanggotaan</div>
                                <p class="m-b-0">
                                <div class="form-row">
                                        <div class="form-group col-md-6"> 
                                            
                                                <div class="form-group">
                                                        <asp:label id="Label2" runat="server"   class="col-form-label">Nama</asp:label>
                                                        <asp:TextBox ID="TextBox1"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group">
                                                    <asp:label id="Label3" runat="server"  class="col-form-label">Tanggal Lahir</asp:label>
                                                    <asp:TextBox ID="txttgllahir"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>   
                                                        
                                                </div>

                                                <div class="form-group">
                                                        <asp:label id="Label4" runat="server"  class="col-form-label">Alamat</asp:label>
														<asp:TextBox ID="txtalamat"  CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="5" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                            
                                                </div>

                                                <div class="form-group">
                                                    <asp:label id="Label9" runat="server"  class="col-form-label">Kelurahan/Kecamatan</asp:label>
                                                    <asp:TextBox ID="txtkelurahan"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>   
                                                        
                                                </div>
                                          </div>
                                          <div class="form-group col-md-6"> 
                                                <div class="form-group">
                                                        <asp:label id="Label5" runat="server"   class="col-form-label">Tanggal Registrasi</asp:label>
                                                        <asp:TextBox ID="txtsimpok"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group">
                                                        <asp:label id="Label8" runat="server"  class="col-form-label">Nama Ibu</asp:label>
													    <asp:TextBox ID="txtsimwa"  CssClass="form-control"  Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                 </div> 

                                                <div class="form-group">
                                                    <asp:label id="Label10" runat="server"  class="col-form-label">Jenis Kelamin</asp:label>
                                                    <asp:TextBox ID="txttotal"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>   
                                                </div>

                                              <div class="form-group">
                                                    <asp:label id="Label12" runat="server"  class="col-form-label">Alasan Tutup</asp:label>
													<asp:TextBox ID="TextBox2"  CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="5" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                            
                                            </div>
                                               
                                        </div>
                                </div>

    
                            
                        </div>
                    </div>
                    </div>
        
                                    <div class="hidden-print mt-4"> 
                                        <div class="float-right mb-2 "> 
                                            <%--       
                                            <asp:Button ID="btnSimpan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="SIMPAN" OnClick="btnSimpan_Click"   OnClientClick="return confirm('Apakah anda yakin ingin menyimpan data ini ?');"/> 
                                              --%>     

                                        </div>
                                     </div> 
                </asp:Panel> 

             <asp:Panel ID="pnlGridTutupAnggota" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title"></h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                     <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"   AutoGenerateColumns="False" Width="887px">
                                    <Columns>
                                    
                                        <asp:TemplateField HeaderText="Kode Cabang">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblkodeCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCabang")%>'></asp:Label>--%>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Cabang">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaCabang")%>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NPWP">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblNpwp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NPWP")%>'></asp:Label>--%>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Alamat 1">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblalamat1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat1")%>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Alamat 2">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblalamat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat2")%>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Alamat 3">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblalamat3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat3")%>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                     
                                        <asp:TemplateField HeaderText="Kepala Cabang">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblkepalacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kepala_cabang")%>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kepala Wilayah">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblkepalawilayah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kepala_wilayah")%>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                                                                                        
                                        <asp:TemplateField HeaderText="Action">
                                                                            <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                         
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
                   <asp:PostBackTrigger ControlID="btnCetakAnggota" /> 
                </Triggers>
            </asp:UpdatePanel>
        </form>

         

                     <!-- Parsley js -->
        <script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>

        <script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

        <script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

        <!-- Sweet Alert Js  -->
        <script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>
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

        <!-- App js -->
        <script src="../../assets/js/jquery.core.js"></script>
        <script src="../../assets/js/jquery.app.js"></script>
        <!-- plugin js -->
        <script src="../../plugins/moment/moment.js"></script>



     </body>




    </html>