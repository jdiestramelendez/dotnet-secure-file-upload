<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="SecureFileUpload.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2><%: Title %>.</h2>
        <div>This demo showcases an approach to secure file uploads that include virus scanning using <a href="https://cloudmersive.com/virus-api">Cloudmersive Virus Scan API</a> and parsing and validation using <a href="https://bytefish.github.io/TinyCsvParser/index.html">TinyCsvParser</a> powered by Azure services like:</div>
        <ul>
            <li><a href="https://docs.microsoft.com/azure/storage/blobs/">Azure Storage Blobs</a></li>
            <li><a href="https://docs.microsoft.com/azure/service-bus-messaging/">Azure Service Bus</a></li>
            <li><a href="https://docs.microsoft.com/azure/azure-functions/">Azure Functions</a></li>
        </ul>
    </div>
</asp:Content>
