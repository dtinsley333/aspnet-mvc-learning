<%@ Page Title="Select Specifications"  Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSpecificationViewModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Pick Part Specs.to Clone
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <% using (Html.BeginForm("SaveClonedPartSpecifications", "PartSpecification"))
    {       %>
<%= Html.HiddenFor(model => model.ItemID)%> 

<%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%>  
<%=Html.HiddenFor(model=>model.PartSetupToCloneFrom) %>
<%=Html.HiddenFor(model=>model.PartSetupToCloneTo) %>

    
<%string clonetopart = Model.PartSetupToCloneTo.PartID;%>
<%string clonefrompart = Model.PartSetupToCloneFrom.PartID; %>
<%int partsetupidtoclonefrom=Model.PartSetupToCloneFrom.PartSetUpID; %>

    <h2>Select Part Specifications to Clone</h2>
    <div>Deselect any items you don't wish to have copied to the part setup for <%=clonetopart%>  from <%=clonefrompart%>.</div><br /><br />
     <input type="submit" class="button"  value="Clone Part Specifications to Part" />
     <a href="javascript:history.go(-1)">
Cancel
</a>	
     <br /><br /><br /><br />
    <table><thead><tr>
    <th>&nbsp;</th>
    <th>ID</th>
    <th>Characteristic</th>
    <th>Characteristic(Spanish)</th>
       <th>Characteristic(Chinese)</th>
    <th>Operationcode</th>
    
    <th>Last Edit Date</th>
    <th>Last Editor</th>
    </tr></thead>
    <%foreach (var item in Model.PartSpecifications.Where(a=>a.PartSetUpID==partsetupidtoclonefrom))
      { %>

      <tr><td><input type="checkbox" name="SpecificationtoClone_<%=item.SpecificationID%>" checked='checked' value="<%=item.SpecificationID%>" /></td>
     <td><%=Html.Encode(item.SpecificationID.ToString()) %></td>
      <td><%=item.Characteristic%></td>
     <td><%=item.CharacteristicES%></td>
      <td><%=item.CharacteristicCN%></td>
      <td><%= Html.Encode(item.OperationCode.ToString())%></td>
      <td><%= Html.Encode(item.LastEditDate)%></td>
      <td><%= Html.Encode(item.LastEditBy)%></td>
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
