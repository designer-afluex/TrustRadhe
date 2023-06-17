<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectKitTransfer.aspx.cs" Inherits="TrustRadhe.DirectKitTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../AssociatePanelcss/bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Direct Kit Transfer
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12 table-responsive">
                                        <div class="row">
                                            <asp:UpdatePanel ID="udp1" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-lg-3">
                                                        <div class="form-group">
                                                            <label>Franchise Login ID  </label>
                                                            <asp:TextBox ID="txtLoginID" runat="server" placeholder="Login ID" MaxLength="10" CssClass="form-control" onchange="return GetFranchiseName();"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-3">
                                                        <div class="form-group">
                                                            <label>Franchise Name</label>
                                                            <asp:TextBox ID="txtFranchiseName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            <input type="hidden" id="hdFranchiseID" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>


                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    Kit Name
                                            <asp:DropDownList ID="ddlKit" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlKit_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    Kit Amount
                                            <asp:TextBox ID="txtKitAmount" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    <label>Quantity </label>
                                                    <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" MaxLength="10" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    <label>Total Amount </label>
                                                    <asp:TextBox ID="txtTotalAmount" CssClass="form-control" runat="server" MaxLength="10" onkeypress="return isNumberKey(event);" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 table-responsive">
                                            <asp:GridView ID="grdKitDetails" runat="server" AutoGenerateColumns="false" Style="width: 100%" class="display nowrap table table-hover table-striped table-bordered"
                                                OnRowCommand="grdKitDetails_RowCommand" OnRowDataBound="grdKitDetails_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductID" runat="server" Text='<%#Bind("PK_ProductId") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Batch Name">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlBatch" runat="server" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged1" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                            <asp:Label ID="lblStockMessage" runat="server" ForeColor="Red" Font-Size="Small" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStock" runat="server" Text='<%#Bind("Stock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12 form-group">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="row" id="dvPaymentMode" runat="server" visible="false">
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    Payment Mode
                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control" onchange="return testChange();">
                                                <asp:ListItem Value="0" Text="Select Payment Mode"></asp:ListItem>
                                                <asp:ListItem Value="Cash" Text="Cash"></asp:ListItem>
                                                <asp:ListItem Value="Cheque" Text="Cheque"></asp:ListItem>
                                                <asp:ListItem Value="NEFT" Text="NEFT"></asp:ListItem>
                                                <asp:ListItem Value="RTGS" Text="RTGS"></asp:ListItem>
                                                <asp:ListItem Value="DD" Text="Demand Draft"></asp:ListItem>
                                            </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="dvPaymentDetails" style="display: none">
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    Transaction No
                                            <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    Transaction Date
                                            <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    Bank Name
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    Bank Branch
                                            <asp:TextBox ID="txtBankBranch" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer" CssClass="btn btn-success" OnClick="btnTransfer_Click" OnClientClick="return fvalidateform();" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnTransfer" />
            </Triggers>
        </asp:UpdatePanel>

    </form>

    <!-- main-panel ends -->
    <input type="hidden" id="txtquantity" />
    <script src="../../AssociatePanelcss/bower_components/jquery/dist/jquery.min.js"></script>
    <script>
        function GetFranchiseName() {
            var LoginID = $('#txtLoginID').val();
            $.ajax({
                url: '/FranchiseAdmin/GetFranchiseName', type: 'post', dataType: 'json',
                data: { 'LoginID': LoginID },
                success: function (data) {
                    if (data.Result == "yes") {
                        $("#txtFranchiseName").val(data.FranchiseName);
                        $("#hdFranchiseID").val(data.PK_FranchiseID);

                    }
                    else {
                        $("#txtLoginID").val('');
                        $("#txtFranchiseName").val('');
                        $("#hdFranchiseID").val('');

                        alert("Invalid Franchise ID");
                    }
                }
            });
        }

        function fvalidateform() {

            $(".errortext").removeClass("errortext");

            if ($('#txtLoginID').val() == '') {
                $("#txtLoginID").addClass('errortext');
                $('#txtLoginID').focus();
                return false;
            }
            if ($('#ddlKit').val() == '0') {
                $("#ddlKit").addClass('errortext');
                $('#ddlKit').focus();
                return false;
            }
            if ($('#txtQuantity').val() == '') {
                $("#txtQuantity").addClass('errortext');
                $('#txtQuantity').focus();
                return false;
            }
            if ($('#txtQuantity').val() == '0') {
                $("#txtQuantity").addClass('errortext');
                $('#txtQuantity').focus();
                return false;
            }
            if ($('#FinalAmount').val() == '') {
                $("#FinalAmount").addClass('errortext');
                $('#FinalAmount').focus();
                return false;
            }
            if ($('#PaymentMode').val() == '') {
                $("#PaymentMode").addClass('errortext');
                $('#PaymentMode').focus();
                return false;
            }

            if ($('#PaymentMode').val() != 'Cash') {
                if ($('#BankName').val() == '') {
                    $("#BankName").addClass('errortext');
                    $('#BankName').focus();
                    return false;
                }
                if ($('#DDChequeNo').val() == '') {
                    $("#DDChequeNo").addClass('errortext');
                    $('#DDChequeNo').focus();
                    return false;
                }
                if ($('#DDChequeDate').val() == '') {
                    $("#DDChequeDate").addClass('errortext');
                    $('#DDChequeDate').focus();
                    return false;
                }
                if ($('#BankBranch').val() == '') {
                    $("#BankBranch").addClass('errortext');
                    $('#BankBranch').focus();
                    return false;
                }
            }
            return true;
        }

        function testChange() {
            if ($('#ddlPaymentMode').val() != '0') {
                if ($('#ddlPaymentMode').val() != 'Cash') {
                    dvPaymentDetails.style.display = "block";
                }
                else {
                    dvPaymentDetails.style.display = "none";
                }
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

    </script>
</body>

</html>
