<%@ Page Title="Select Processes"  Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.AdditionalProcessingViewModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Pick Additional Processes to Clone
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <% using (Html.BeginForm("SaveClonedProcesses", "AdditionalProcesses"))
    {       %>
<%= Html.HiddenFor(model => model.ItemID)%> 

<%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%>  
<%=Html.HiddenFor(model=>model.PartSetupToCloneFrom) %>
<%=Html.HiddenFor(model=>model.PartSetupToCloneTo) %>

    
<%string clonetopart = Model.PartSetupToCloneTo.PartID;%>
<%string clonefrompart = Model.PartSetupToCloneFrom.PartID; %>
<%int partsetupidtoclonefrom=Model.PartSetupToCloneFrom.PartSetUpID; %>

    <h2>Select Additional Processes to Clone</h2>
    <div>Deselect any items you don't wish to have copied to the part setup for <%=clonetopart%>  from <%=clonefrompart%>.</div><br /><br />
     <input type="submit" class="button" value="Clone Additional Processes to Part" />
     <br />	<a href="javascript:history.go(-1)">
Cancel
</a><br /><br /><br />
    <table><thead><tr>
    <th>&nbsp;</th>
    <th>ID</th>
    <th>Description</th>
    <th>Description(Spanish)</th>
    <th>Description(Chinese)</th>
    <th>Last Edit Date</th>
    <th>Last Editor</th>
    </tr></thead>
    <%foreach (var item in Model.AdditionalProcesses.Where(a=>a.PartSetUp.PartSetUpID==partsetupidtoclonefrom))
      { %>

      <tr><td><input type="checkbox" name="ProcesstoClone_<%=item.ProcessingID%>" checked='checked' value="<%= item.ProcessingID%>" /></td>
        <td><%= Html.Encode(item.ProcessingID.ToString())%></td>
      <td><%= Html.Encode(item.Description)%></td>
     <td><%= Html.Encode(item.DescriptionES)%></td>
      <td><%= Html.Encode(item.DescriptionCN)%></td>
      <td><%= Html.Encode(item.LastEditedBy)%></td>
      <td><%= Html.Encode(item.LastEditDate)%></td>
      </tr>
      
    <%  }
    }%>

</table>
<br /><br />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Main_MenubarContent" runat="server">
</asp:Content>
