<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.FrequencyViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Maintain Frequency Values
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
   <br /><br /><br />   <br /><br /><br />
 <table><tr><td>
  <div id="frequencies">
   <script type="text/javascript" language="javascript">
       $(document).ready(function () {

           $("#FrequenciesToSort").tablesorter({ headers: { 0: { sorter: false}} });
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
     
      	<div class='pageheaders'>Frequency Look Up </div><br />

 <%if (Model.CanUserEdit)
       { %>
     <div> <%=Html.ActionLink("Add New Frequency", "FrequencyCreate", "Frequency", null)%><br /><br /></div>
    
      <%}
    
 if (Model.Frequencies != null && Model.Frequencies.Count() > 0)
      { %>
          <table id="FrequenciesToSort" class="clean-table" width="95%">
            <col style="width:auto" />
            <thead>
                <tr>
                     <th style="background-color: #e8eef4;">&nbsp;</th>
                    <th>ID</th>
                    <th>Name(English)</th>
                    <th>Name(Spanish)</th>
                    <th>Name(Chinese)</th>
                     <th>Active</th>
                    <th>Notes</th>
                    <th>Last Edited By</th>
                    <th>Last Edit Date</th>
                     <th>Created by</th>
                    <th>Create Date</th>
                      
                </tr>
            </thead>
            <tbody>
          
    <%foreach(var method in Model.Frequencies)
      { %>
      <tr>
      <td>
      <%if (Model.CanUserEdit)
        { %>
      <%= Html.ActionLink("Edit", "FrequencyEdit", new { id_ = method.FrequencyID }, null)%>
      <%} %>
      &nbsp</td>
      <%string freqid = method.FrequencyID.ToString(); %>
      <%string frequencyanchor = "frequency" + freqid; %>
      <td><a name="<%=(frequencyanchor) %>"></a><%=freqid%>&nbsp;</td>
      <td><%=method.Description_EN %>&nbsp;</td>
      <td><%=method.Description_MX %>&nbsp;</td>
      <td><%=method.Description_CN %>&nbsp;</td>
    
      <td><%= method.IsActive.ToString() %>&nbsp;</td>
    
       <%
				if( method.Notes != ""){%>
                 <%string notepadfileimagepath = ConfigurationManager.AppSettings["NotePadImageFilePath"];%>
			  <td> <a href='#'id="tooltip" title='<%=method.Notes%>'><img src="<%=notepadfileimagepath %>" style="border-style: none" /></a></td>
			   <%}
				   else{%><td>&nbsp;</td>
			<%}%>
       <td><%=method.LastEditBy %>&nbsp;</td>
        <td><%=method.LastEditDate %>&nbsp;</td>
      <td><%=method.CreatedBy %>&nbsp;</td>
        <td><%=method.CreateDate.ToString() %>&nbsp;</td>
      </tr>

     <%}%>
   </tbody>
    </table>
   
 
    
 <%  } %></div></td></tr></table>

</asp:Content>

