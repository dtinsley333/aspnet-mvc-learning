<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.AdditionalProcessingViewModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Edit Additional Part Process
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">



</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br /><br /><br />
    <%string itemid = Model.PartSetUp.PartID; %>
    <div class='pageheaders'>Edit Additional Process for <%=(itemid) %></div>

	 <% using (Html.BeginForm("AdditionalProcessesEdit", "AdditionalProcesses"))
     { %>
  <%= Html.HiddenFor(model => model.PartSetUp.PartID)%> 
    <%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%> 
 <%= Html.HiddenFor(model => model.PartSetUpID)%> 
  <%= Html.HiddenFor(model => model.AdditionalProcess.ProcessingID)%> 
 <% string processingid = Model.AdditionalProcess.ProcessingID.ToString(); %>
 
 <table id="CreateInitialSetup" class="standard-table">

 <tr><td>Process ID:</td><td><%=processingid%></td></tr>
 
 <tr><td><%=Html.LabelFor(a => a.AdditionalProcess.Description)%>:</td><td><%=Html.TextBoxFor(a=>a.AdditionalProcess.Description, new {  style="width:275px;", maxlength = "150" }) %><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.AdditionalProcess.Description)%></td></tr>
  <tr><td><%=Html.LabelFor(a => a.AdditionalProcess.DescriptionES)%>:</td><td><%=Html.TextBoxFor(a=>a.AdditionalProcess.DescriptionES, new {  style="width:275px;", maxlength = "150" }) %></td></tr>   
     <tr><td><%=Html.LabelFor(a => a.AdditionalProcess.DescriptionCN)%>:</td><td><%=Html.TextBoxFor(a=>a.AdditionalProcess.DescriptionCN, new {  style="width:275px;", maxlength = "150" }) %></td></tr>                                                                                                                                                                                                                                                                        
  <tr><td>Active:</td><td><%=Html.CheckBoxFor(a=>a.AdditionalProcess.IsActive) %></td></tr>
 <tr><td>Display Order: </td><td><%= Html.DropDownListFor(a => a.AdditionalProcess.SequenceID, Model.AdditionalProcessingSequence, "Select One")%><span style="color:red;">&nbsp;*</span>&nbsp; <%= Html.ValidationMessageFor(model => model.AdditionalProcess.SequenceID)%> </td></tr>
  <tr><td>Notes:</td><td><%= Html.TextAreaFor(a=>a.AdditionalProcess.Notes, new {  style="width:400px;height:100px;", maxlength = "1000" })%></td></tr>
  
 </table>
  <tr>
				<td colspan="2">
					 <input type="submit" value="Save" />
		
			<a href="javascript:history.go(-1)">
Cancel
</a>
				</td>
			</tr>

        <%} %>
    

</asp:Content>


