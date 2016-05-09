<%@ Page Title="Add Part Process" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.AdditionalProcessingViewModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Add Additional Part Process
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br /><br /><br /><br /><br />
 

	 <% using (Html.BeginForm("AdditionalProcessessCreate", "AdditionalProcesses"))
     { %>
   <%string itemid = Model.PartSetUp.PartID; %>
    <div class='pageheaders'>Add Additional Process  for <%=(itemid) %></div><br />

  <%= Html.HiddenFor(model => model.PartSetUp.PartID)%> 
  <%=Html.HiddenFor(model=>model.PartSetUp.PartSetUpID) %>
 <table id="CreateInitialSetup" class="standard-table">
  <tr><td><%=Html.LabelFor(a=>a.AdditionalProcess.Description) %>:</td><td><%=Html.TextBoxFor(a=>a.AdditionalProcess.Description, new {  style="width:275px;", maxlength = "100" }) %><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.AdditionalProcess.Description)%></td></tr>
    <tr><td><%=Html.LabelFor(a=>a.AdditionalProcess.DescriptionES) %>:</td><td><%=Html.TextBoxFor(a=>a.AdditionalProcess.DescriptionES, new {  style="width:275px;", maxlength = "100" }) %></td></tr>
        <tr><td><%=Html.LabelFor(a => a.AdditionalProcess.DescriptionCN)%>:</td><td><%=Html.TextBoxFor(a=>a.AdditionalProcess.DescriptionCN, new {  style="width:275px;", maxlength = "150" }) %></td></tr>                                                                                                                                                                                                                                                                        
   <tr><td>Display Order: </td><td><%= Html.DropDownListFor(a => a.AdditionalProcess.SequenceID, Model.AdditionalProcessingSequence, "Select One")%><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.AdditionalProcess.SequenceID)%> </td></tr>
  <tr><td>Active:</td><td><%=Html.CheckBoxFor(a=>a.IsActive) %></td></tr>
  <tr><td>Notes:</td><td><%= Html.TextAreaFor(a=>a.AdditionalProcess.Notes, new {  style="width:400px;height:100px;", maxlength = "1000" })%></td></tr>
 
 </table>
  <tr>
				<td colspan="2">
					 <input type="submit" value="Save" class="button" />
				<a href="javascript:history.go(-1)">
Cancel
</a>

				</td>
			</tr>

        <%} %>
    

</asp:Content>

