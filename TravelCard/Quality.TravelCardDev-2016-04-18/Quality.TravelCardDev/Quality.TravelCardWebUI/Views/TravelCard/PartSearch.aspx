<%@ Page Title="Part Search" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>
 <asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server" >
  
   </asp:Content>  
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="pagecontents" style="width:70%;">
 
<br />
  
	<div class="pageheaders"><%= Html.Encode(ViewData["Message"]) %></div><br />
	 <p><%= Html.Encode(ViewData["NoItemIDMessage"]) %></p>
	  <% using (Html.BeginForm("TravelCard", "TravelCard"))
		 { %>

	 <table id="ItemIDSearch" class="standard-table">
	<col class="label-column" />
			<col class="data-column" />
			 <tr>
				<td>Part ID:</td>
				  <td>
				  <input type="text" id="ItemID" name="ItemID" style="width: 200px" 
						  alt="" maxlength="50" /> 
				  <span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessage("Part ID is required")%></td>
			 </tr>
             <col class="data-column" />
			 <tr>
				<td>Part Category:</td>
				  <td>
				  <input type="text" id="Text1" name="ItemID" style="width: 200px" 
						  alt="" maxlength="50" /> 
				  </td>
			 </tr>
              <tr>
				<td>Plant #:</td>
				  <td>
				  <input type="text" id="Text2" name="ItemID" style="width: 200px" 
						  alt="" maxlength="50" /> 
				  </td>
			 </tr>

              <tr>
				<td>Begin Date #:</td>
				  <td>
				  <input type="text" id="Text3" name="ItemID" style="width: 200px" 
						  alt="" maxlength="50" /> 
				  </td>
			 </tr>
             <tr>
				<td>End Date #:</td>
				  <td>
				  <input type="text" id="Text4" name="ItemID" style="width: 200px" 
						  alt="" maxlength="50" /> 
				  </td>
			 </tr>
             <tr>
				<td>Active:</td>
				  <td>
				  <input type="text" id="Text5" name="ItemID" style="width: 200px" 
						  alt="" maxlength="50" /> 
				  </td>
			 </tr>
			<tr>
				<td colspan="2">
					<input type="submit" class="button" value="Search" />
				</td>
			</tr>
		  
			</table>
			<%} %>
   
   <br /><br />
 </div>
</asp:Content>
