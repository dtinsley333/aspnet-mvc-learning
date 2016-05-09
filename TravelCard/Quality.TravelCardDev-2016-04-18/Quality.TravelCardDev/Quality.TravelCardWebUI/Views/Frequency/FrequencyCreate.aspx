<%@ Page Title="Add Frequency" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.FrequencyViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
Create New Frequency
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>



<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br /><br /><br /><br /><br />


	 <% using (Html.BeginForm("FrequencyCreate", "Frequency"))
     { %>
 
    <div class='pageheaders' style="width:60%;" >Create New Frequency</div><br />
 <table id="CreateInitialSetup" class="standard-table" style="width:60%;">
 <tr><th colspan="2">English</th></tr>
  <tr><td><%=Html.LabelFor(a=>a.Frequency.Description_EN) %>:</td><td><%=Html.TextBoxFor(a=>a.Frequency.Description_EN, new {  style="width:450px;", maxlength = "120" }) %><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.Frequency.Description_EN)%></td></tr>
 
   <tr><th colspan="2">Spanish</th></tr>
     <tr><td><%=Html.LabelFor(a=>a.Frequency.Description_MX) %>:</td><td><%=Html.TextBoxFor(a=>a.Frequency.Description_MX, new {  style="width:450px;", maxlength = "120" }) %></td></tr>

   <tr><th colspan="2">Chinese</th></tr>
     <tr><td><%=Html.LabelFor(a=>a.Frequency.Description_CN) %>:</td><td><%=Html.TextBoxFor(a=>a.Frequency.Description_CN, new {  style="width:450px;", maxlength = "120" }) %></td></tr>

 <tr><th><%=Html.LabelFor(a=>a.Frequency.Notes) %></th><td><%= Html.TextAreaFor(a=>a.Frequency.Notes, new {  style="width:300px;height:100px;", maxlength = "1000" })%></td></tr>

 

  <tr>
				<td colspan="2">
					 <input type="submit" value="Save" class="button" />
				<a href="javascript:history.go(-1)">
Cancel
</a>

				</td>
			</tr>

        <%} %>
    
</table>
</asp:Content>
