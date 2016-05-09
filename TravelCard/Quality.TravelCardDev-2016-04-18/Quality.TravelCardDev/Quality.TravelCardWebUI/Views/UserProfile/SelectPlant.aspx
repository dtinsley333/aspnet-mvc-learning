<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.UserProfileViewModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Select Plant
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Select Plant</h2>



    
        <% using (Html.BeginForm("SetUserPlant", "UserProfile"))
    {%>
 
      Plant:&nbsp;<%= Html.DropDownListFor(a=>a.Plant.PlantCodeID, Model.PlantSelectList, "Change", new { onchange="this.form.submit();" })%>   
      <%} %>

      <br /><br /><br />








</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


