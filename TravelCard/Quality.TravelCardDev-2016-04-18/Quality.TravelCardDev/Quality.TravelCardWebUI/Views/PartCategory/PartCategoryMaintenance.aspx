<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartCategoryViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Maintain Part Categories
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
   <br /><br /><br />   <br /><br /><br />
 <table><tr><td>
  <div id="additionalProcesses">
   <script type="text/javascript" language="javascript">
       $(document).ready(function () {

           $("#CategoriesToSort").tablesorter({ headers: { 0: { sorter: false}} });
       });
    </script>
  
   <%if(Model!=null&&Model.returnanchor!=null)
   { %>
 <%string anchor = "#"+Model.returnanchor;

   
   %>
 
  <script type="text/javascript" language="javascript">
      $(document).ready(function () {
          var $theanchor = null;
          theanchor = "<%=anchor%>";
          location.hash = theanchor;

      });
    </script>
     <%} %>
     
      	<div class='pageheaders'>Part Category Look Up </div><br />

 <%if (Model.CanUserEdit)
       { %>
     <div> <%=Html.ActionLink("Add New Part Category", "PartCategoryCreate", "PartCategory", null)%><br /><br /></div>
    
      <%}
    
 if (Model.PartCategories != null && Model.PartCategories.Count() > 0)
      { %>
          <table id="CategoriesToSort" class="clean-table" width="95%">
            <col style="width:auto" />
            <thead>
                <tr>
                     <th style="background-color: #e8eef4;">&nbsp;</th>
                    <th>Category ID</th>
                    <th>Name(English)</th>
                    <th>Name(Spanish)</th>
                    <th>Name(Chinese)</th>
                    <th>Description(English)</th>
                     <th>Description(Spanish)</th>
                       <th>Description(Chinese)</th>
                    <th>Active</th>
                    <th>Notes</th>
                    <th>Last Edited By</th>
                    <th>Last Edit Date</th>
                     <th>Created by</th>
                    <th>Create Date</th>
                      
                </tr>
            </thead>
            <tbody>
          
    <%foreach(var category in Model.PartCategories)
      { %>
      <tr>
      <td>
      <%if (Model.CanUserEdit)
        { %>
      <%= Html.ActionLink("Edit", "PartCategoryEdit", new { id_ = category.CategoryID }, null)%>
      <%} %>
      &nbsp</td>
      <%string catid = category.CategoryID.ToString(); %>
      <%string catanchor = "category" + catid; %>
      <td><a name="<%=(catanchor)%>"><%=(catid)%>&nbsp;</a></td>
      <td><%=category.CategoryName %>&nbsp;</td>
      <td><%=category.CategoryNameES %>&nbsp;</td>
      <td><%=category.CategoryNameCN %>&nbsp;</td>
      <td><%=category.CategoryDescription %>&nbsp;</td>
       <td><%=category.CategoryDescriptionES %>&nbsp;</td>
        <td><%=category.CategoryDescriptionCN %>&nbsp;</td>
      <td><%= category.IsActive.ToString() %>&nbsp;</td>
    
       <%
				if( category.Notes != ""){%>
                 <%string notepadfileimagepath = ConfigurationManager.AppSettings["NotePadImageFilePath"];%>
			  <td> <a href='#'id="tooltip" title='<%=category.Notes%>'><img src="<%=notepadfileimagepath %>" style="border-style: none" /></a></td>
			   <%}
				   else{%><td>&nbsp;</td>
			<%}%>
       <td><%=category.LastEditedBy %>&nbsp;</td>
        <td><%=category.LastEditDate.ToString() %>&nbsp;</td>
      <td><%=category.CreatedBy %>&nbsp;</td>
        <td><%=category.CreateDate.ToString() %>&nbsp;</td>
      </tr>

     <%}%>
   </tbody>
    </table>
   
 
    
 <%  } %></div></td></tr></table>

</asp:Content>

