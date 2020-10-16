<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="SecureFileUpload.FileUpload" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Local File Storage Provider</h3>
                    </div>
                    <div class="panel-body">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">File List</h3>
                            </div>
                            <div class="panel-body">
                                <div class="container-fluid">
                                    <asp:DataList ID="dlFilesLocal" runat="server" DataKeyField="Value" OnItemCommand="dlFiles_ItemCommand" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <div class="row row-no-gutters">
                                                <div class="col-md-11">- <%# Eval("Value") %></div>
                                                <div class="col-md-1">
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
                                <asp:Label ID="lblFileLocal" runat="server" Text="CSV File:"></asp:Label>
                                <asp:FileUpload ID="fuCsvFileLocal" runat="server" />
                                <br />
                                <asp:Button ID="btnSubmitLocal" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            </div>
                        </div>

                        <div class="panel panel-danger" visible="false" runat="server" id="pnlErrorLocal">
                            <div class="panel-heading">
                                <h3 class="panel-title">File Upload Error</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Literal ID="litErrorLocal" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="panel panel-default" visible="false" runat="server" id="pnlResultLocal">
                            <div class="panel-heading">
                                <h3 class="panel-title">File Upload Results</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Literal ID="litResultsLocal" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Azure Storage Provider</h3>
                    </div>
                    <div class="panel-body">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">File List</h3>
                            </div>
                            <div class="panel-body">
                                <div class="container-<%--fluid--%>">
                                    <asp:DataList ID="dlFilesAzure" runat="server" DataKeyField="Value" OnItemCommand="dlFiles_ItemCommand" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <div class="row row-no-gutters">
                                                <div class="col-md-11">- <%# Eval("Value") %></div>
                                                <div class="col-md-1">
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
                                <asp:FileUpload ID="fuCsvFileAzure" runat="server" />
                                <br />
                                <asp:Button ID="btnSubmitAzure" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            </div>
                        </div>

                        <div class="panel panel-danger" visible="false" runat="server" id="pnlErrorAzure">
                            <div class="panel-heading">
                                <h3 class="panel-title">File Upload Error</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Literal ID="litErrorAzure" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="panel panel-default" visible="false" runat="server" id="pnlResultAzure">
                            <div class="panel-heading">
                                <h3 class="panel-title">File Upload Results</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Literal ID="litResultsAzure" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
