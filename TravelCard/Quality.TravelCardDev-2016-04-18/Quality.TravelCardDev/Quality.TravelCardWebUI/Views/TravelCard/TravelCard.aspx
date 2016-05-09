<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WPF.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.TravelCardViewModel>"  %>


<asp:Content ID="TravelCardTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Travel Card
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server" >


	 <script language="javascript" type="text/javascript">


		 $(this).ready(function () {
			 $("#GetPdf").click(function () {
				 var $theHtml = null;
				 theHtml = $('<html>').append($('#Wrapper').clone()).remove().html();
				 $.download('<%= Url.Action("TravelCardReport","Reports") %>', { filename: "pdf_printout", format: "pdf", html_: theHtml }, "post");
				
			 });
		 });


		</script>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="pagecontents" style="width:95%;">

	  <div class='pageheaders'>Part Details</div><br />
	
	   <table id='ItemIDSearch' class='standard-table'>

			<thead><tr>
			<th>Item ID</th>
			<th>Description</th>
			<th>Alt Description</th>
			<th>Date Created</th>
			
			</tr></thead>
			<tbody>
			
			<% 
				string partid = "";
				
				foreach (var part in Model.Part){
					partid = part.Id;
				   
					 %>
			
			
			 <tr>
			 <td><%=part.Id %></td>
			 <td><%=part.ITMDESC %></td> 
			 <td><%=part.ALTDESC %></td> 
			 <td><%=part.DTECRT %></td>  
				</tr>          
				 
			
			 <%} %>
  </tbody>
  </table>
  <div class='pageheaders'>Sub components for parent part # <%=partid %></div><br />
  <table id='ComponentList' class='clean-table'>
			<thead>
			<tr>
				   <th>Component ID</th>
					<th>Description</th>
					<th>Parent Plant Code</th>
					<th>Component Plant Code</th>
					<th>Usage Rate</th>
					<th>Beginning Date</th>
					<th>Ending Date</th>
			</tr></thead>
			<tbody>
		   <% if (Model.Component.Count() <1)
			  {%>
			
			  No component parts were found for this item
			  
			  <%} %> 
				 <% foreach (var component in Model.Component)
				  {            
	
				%>
			 <tr>
			 <td><%=component.Id%></td> 
			 <td><%=component.ITMDESC %></td>
			 <td><%=component.PRTPLT%></td> 
			 <td><%=component.PLT%></td> 
			 <td><%=component.USGRAT%></td> 
			 <td><%=component.BEGEFF%></td> 
			 <td><%=component.ENDEFF%></td> 
			 </tr>          

			 <%}%>
			 
</tbody>
</table>
</div>

</asp:Content>


