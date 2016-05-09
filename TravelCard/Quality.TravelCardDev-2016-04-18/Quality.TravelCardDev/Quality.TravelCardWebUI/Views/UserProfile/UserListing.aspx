<%@ Page Title="User List" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.UserProfileViewModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	User Listing
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br />
    <h2>User Listing</h2>
    <h3>Members of TravelCardUsers Active Directory Group</h3>
    Users can only print the travel cards. They have read only access.
    <table class="standardtable">
    <tr><th>Name</th><th>Work Group</th></tr>

      <%foreach (var member in Model.TravelCardUserList)
      {
          string[] userinfo=member.ToString().Split(',');
          int thelength = userinfo[0].Length;
          string username = userinfo[0];
          string cleanedupname = username.Substring(3, thelength-3);

          string workgroup = userinfo[1];
          string cleanedupworkgroup = workgroup.Substring(3, workgroup.Length - 3);    

          %>
        <tr>
        <td><%=cleanedupname%></td>
        <td><%=cleanedupworkgroup%></td></tr> 
 
     <%}
    
           %>
</table><br /><br /><br /><br />



 <h3>Members of TravelCardMaintenance Active Directory Group</h3>
   Users can print Travel Cards and edit the specifications on the travel card. They cannot set the release ready flag on travel cards or set global updates. 
 They can also print travel cards. 
    <table class="standardtable">
    <tr><th>Name</th><th>Work Group</th></tr>

      <%foreach (var member in Model.TravelCardMaintenanceList)
      {
          string[] userinfo=member.ToString().Split(',');
          int thelength = userinfo[0].Length;
          string username = userinfo[0];
          string cleanedupname = username.Substring(3, thelength-3);

          string workgroup = userinfo[1];
          string cleanedupworkgroup = workgroup.Substring(3, workgroup.Length - 3);    

          %>
        <tr>
        <td><%=cleanedupname%></td>
        <td><%=cleanedupworkgroup%></td></tr> 
 
     <%}
    
           %>
</table><br /><br /><br /><br />


 <h3>Members of TravelCardAdmin Active Directory Group</h3>
            Users can set the release ready flag and make global updates. They can also print travel cards. Currently the TravelCardAdmin and the TravelCardApprover roles have the same rights.
    
    <table class="standardtable">
    <tr><th>Name</th><th>Work Group</th></tr>

      <%foreach (var member in Model.TravelCardAdminList)
      {
          string[] userinfo=member.ToString().Split(',');
          int thelength = userinfo[0].Length;
          string username = userinfo[0];
          string cleanedupname = username.Substring(3, thelength-3);

          string workgroup = userinfo[1];
          string cleanedupworkgroup = workgroup.Substring(3, workgroup.Length - 3);    

          %>
        <tr>
        <td><%=cleanedupname%></td>
        <td><%=cleanedupworkgroup%></td></tr> 
 
     <%}
    
           %>
</table><br /><br /><br /><br />


 <h3>Members of TravelCardApprover Active Directory Group</h3>
        Users can set the release ready flag and make global updates. They can also print travel cards. Currently the TravelCardAdmin and the TravelCardApprover roles have the same rights. In the future some administrative rights
        may be limited to the approver role only. 
 <%if (Model.TravelCardApproverList == null || Model.TravelCardApproverList.Count == 0)
   {%>
   No members were found in the Active Directory TravelCardApprovers group.
   <br /><br /><br /><br />
  <% }
   else
 { %>
    <table class="standardtable">
    <tr><th>Name</th><th>Work Group</th></tr>

      <%foreach (var member in Model.TravelCardApproverList)
        {
            string[] userinfo = member.ToString().Split(',');
            int thelength = userinfo[0].Length;
            string username = userinfo[0];
            string cleanedupname = username.Substring(3, thelength - 3);

            string workgroup = userinfo[1];
            string cleanedupworkgroup = workgroup.Substring(3, workgroup.Length - 3);    

          %>
        <tr>
        <td><%=cleanedupname%></td>
        <td><%=cleanedupworkgroup%></td></tr> 
 
     <%}%>

 
</table><br /><br /><br /><br />

 <% }       %>
</asp:Content>


