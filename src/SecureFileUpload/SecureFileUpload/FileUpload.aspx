<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="SecureFileUpload.FileUpload" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">File List</h3>
            </div>
            <div class="panel-body">
                <div class="container-fluid">
                    <asp:DataList ID="dlFiles" runat="server" DataKeyField="Value" OnItemCommand="dlFiles_ItemCommand">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-md-6"><%# Eval("Value") %></div>
                                <div class="col-md-6">
                                    <asp:LinkButton ID="lbDeleteFile" runat="server" CommandName="DeleteFile" CommandArgument='<%# Eval("Value") %>' class="btn btn-danger btn-xs">
                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">File Upload</h3>
            </div>
            <div class="panel-body">
                <asp:Label ID="lblFile" runat="server" Text="CSV File:"></asp:Label>
                <asp:FileUpload ID="fuCsvFile" runat="server" />
                <br />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            </div>
        </div>

        <div class="panel panel-danger" visible="false" runat="server" id="pnlError">
            <div class="panel-heading">
                <h3 class="panel-title">File Upload Error</h3>
            </div>
            <div class="panel-body">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </div>
        </div>

        <div class="panel panel-default" visible="false" runat="server" id="pnlResult">
            <div class="panel-heading">
                <h3 class="panel-title">File Upload Results</h3>
            </div>
            <div class="panel-body">
                <asp:Literal ID="litResults" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
