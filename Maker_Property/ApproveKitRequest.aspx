<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveKitRequest.aspx.cs" Inherits="TrustRadhe.ApproveKitRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-md-4">
                Requested Qty
            @Html.TextBoxFor(m => m.ApprovedQuantity, new { @class = "form-control", @readonly = "readonly" })
            </div>

            <div class="col-lg-4">
                <div class="form-group">
                    Quantity &nbsp;<span style="color: red">*</span>
                    @Html.TextBoxFor(m => m.Quantity, new { @class = "form-control", @placeholder = "Quantity", @onchange = "return validateQty(this);", @onkeypress = "return isNumberKey(event);" })
                @Html.HiddenFor(m => m.RequestID)
                @Html.HiddenFor(m => m.KitID)
                @Html.HiddenFor(m => m.LoginID)
                <input type="hidden" id="hdqty" />
                    <input type="hidden" id="hdreqqty" />
                    <input type="hidden" id="hdkitid" />
                </div>
            </div>
        </div>
        <div class="row">
            <asp:GridView ID="grdKitDetails" runat="server" AutoGenerateColumns="false" Style="width: 100%"
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
                            <asp:DropDownList ID="ddlBatch" runat="server" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                            <br />
                            <asp:Label ID="lblStockMessage" runat="server" ForeColor="Red" Font-Size="Small" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stock">
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
            <div class="col-md-12">
                <div class="form-group">
                    <input type="submit" class="btn btn-sm btn-success pull-right" name="btnApprove" value="Approve" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
