<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Show PDF
</asp:Content>
<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<%string usermessage = ViewData["usermessage"].ToString(); %>

<%string filepath = ViewData["filename"].ToString(); %>

    <h2><%=Quality.Resources.Strings.OMIPreview %></h2><br /><br /><br /><br />
    
       <% Html.RenderAction("DisplayOutPuttedPDF", "Reports", new { _filename = filepath });%> <br />


</asp:Content>




