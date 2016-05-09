
<%@ Page Title="Edit Part Category" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartCategoryViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
Edit Part Category
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>



<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br /><br /><br /><br /><br />


	 <% using (Html.BeginForm("PartCategoryEdit", "PartCategory"))
     { %>

 <%string categoryid=Model.PartCategory.CategoryID.ToString(); %>
    <div class='pageheaders' style="width:60%;" >Edit Part Category # <%=categoryid %></div><br /><br />
<%=Html.HiddenFor(model=>model.PartCategory.CategoryID) %>

 <table id="CreateInitialSetup" class="standard-table" style="width:60%;">
 <tr><th colspan="2">English</th></tr>
  <tr><td><%=Html.LabelFor(a=>a.PartCategory.CategoryName) %>:</td><td><%=Html.TextBoxFor(a=>a.PartCategory.CategoryName, new {  style="width:275px;", maxlength = "50" }) %><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartCategory.CategoryName)%></td></tr>
   <tr><td><%=Html.LabelFor(a=>a.PartCategory.CategoryDescription) %>:</td><td><%=Html.TextBoxFor(a=>a.PartCategory.CategoryDescription, new {  style="width:275px;", maxlength = "100" }) %></td></tr>
   <tr><th colspan="2">Spanish</th></tr>
     <tr><td><%=Html.LabelFor(a=>a.PartCategory.CategoryNameES) %>:</td><td><%=Html.TextBoxFor(a=>a.PartCategory.CategoryNameES, new {  style="width:275px;", maxlength = "50" }) %></td></tr>
   <tr><td><%=Html.LabelFor(a=>a.PartCategory.CategoryDescriptionES) %>:</td><td><%=Html.TextBoxFor(a=>a.PartCategory.CategoryDescriptionES, new {  style="width:275px;", maxlength = "100" }) %></td></tr>

   <tr><th colspan="2">Chinese</th></tr>
     <tr><td><%=Html.LabelFor(a=>a.PartCategory.CategoryNameCN) %>:</td><td><%=Html.TextBoxFor(a=>a.PartCategory.CategoryNameCN, new {  style="width:275px;", maxlength = "50" }) %></td></tr>
   <tr><td><%=Html.LabelFor(a=>a.PartCategory.CategoryDescriptionCN) %>:</td><td><%=Html.TextBoxFor(a=>a.PartCategory.CategoryDescriptionCN, new {  style="width:275px;", maxlength = "100" }) %></td></tr>
    <tr><td>Active Flag:(Unchecking will result in the part category no longer being available for new part set ups. )</td><td><%=Html.CheckBoxFor(a=>a.PartCategory.IsActive) %></td></tr>
 
 <tr><th><%=Html.LabelFor(a=>a.PartCategory.Notes) %></th><td><%= Html.TextAreaFor(a=>a.PartCategory.Notes, new {  style="width:300px;height:100px;", maxlength = "1000" })%></td></tr>

 

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
