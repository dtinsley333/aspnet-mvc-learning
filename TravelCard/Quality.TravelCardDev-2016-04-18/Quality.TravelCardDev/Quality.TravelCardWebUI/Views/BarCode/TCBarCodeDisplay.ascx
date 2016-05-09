<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Quality.ViewModels.BarCodeViewModel>"  %>
<div>
<%if(!string.IsNullOrEmpty(Model.BarcodeFilePath))
  {%>
  
  <img src="<%=ViewData["BarcodeFilePath"]%>" />

  <%string incrementbarcodepath = Model.BarcodeFilePath; %>


 <% } %>
</div>
