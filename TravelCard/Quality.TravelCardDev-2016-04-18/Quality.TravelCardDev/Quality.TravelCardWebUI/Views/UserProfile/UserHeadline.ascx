<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Quality.ViewModels.UserProfileViewModel>" %>
<div>

<%string username = Model.username;
  string rolenames = string.Empty;

  if (Model.usergroupmembership != null)
  {
      foreach (string role in Model.usergroupmembership)
      {
          rolenames = rolenames + role + ",";

      }
  }
  if (!String.IsNullOrEmpty(rolenames))
  {
      rolenames = rolenames.Substring(0, rolenames.Length - 1);
  }
    %>
  User:&nbsp;<%=username%>.<br />  Role(s):&nbsp; <%=rolenames%><br />
  
      <%if (Model.UserSetting != null)
        {%>
      Plant:&nbsp;<%=Model.Plant.PlantName%>   
  <%}

      




  if (!String.IsNullOrEmpty(rolenames))


      if (Model.usergroupmembership == null || Model.usergroupmembership.Count == 0)
      {%>
     
     <script type="text/javascript">
     window.location = "noaccess.aspx"
     </script>
     
     
    <%
      }
  %>

</div>





