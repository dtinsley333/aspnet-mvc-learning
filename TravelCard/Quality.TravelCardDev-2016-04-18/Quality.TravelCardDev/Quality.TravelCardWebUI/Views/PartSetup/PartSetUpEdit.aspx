<%@ Page Title="Edit Set Up" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSetUpViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Part Set Up
</asp:Content>
<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <%string pdfImagegfilepath = ConfigurationManager.AppSettings["PDFImageFilePath"];%>
<div id="pagecontents" style="width:100%;">

<br /><br />
	<div class='pageheaders'>Edit Part Set Up for <%=Model.PartSetUp.PartID %></div><br />

	 <% using (Html.BeginForm("PartSetUpEdit", "PartSetUp"))
		{ %>
        <%var partsetupid =Model.PartSetUp.PartSetUpID; %>
   <%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%> 
 
        
                       
               <%= Html.HiddenFor(model => model.ItemID)%> 

     <input type="submit" class="button" name="SubmitConfirmation1" value="Save" />
     <a href="javascript:history.go(-1)">
Cancel
</a>
	 <table id="EditSetup" class="clean-table">
	 <tr><td><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(a=>a.PartSetUp.CategoryID) %> :</span> </td><td><%= Html.DropDownListFor(a=>a.PartSetUp.CategoryID,Model.PartCategorySelectList,"Select One") %> <%= Html.ValidationMessageFor(model => model.PartSetUp.CategoryID)%></td></tr>
	 <tr><td class="style1">Pack Code: </td><td><%= Html.TextBoxFor(a=>a.PartSetUp.PackCode, new {  style="width:35px;", maxlength = "5" }) %></td></tr>	
	
     <tr><td><span style="font-weight:bold;color:Green;">Revision:</span> </td><td><%= Html.TextBoxFor(a=>a.PartSetUp.Revision, new {  style="width:35px;", maxlength = "5" }) %></td></tr>
     <tr><td><span style="font-weight:bold;color:Green;">Drawing Number :</span></td><td><%= Html.TextBoxFor(a=>a.PartSetUp.DrawingNumber, new {  style="width:250px;", maxlength = "150" }) %>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSetUp.DrawingNumber)%></td></tr>
   <%if (Model.CanUserApprove)
     {%>
     <tr><td colspan="2"><span style="font-weight:bold;color:Green;">Is Release Ready? :</span><%= Html.CheckBoxFor(a => a.PartSetUp.IsReleaseReady)%><span style="color:#003300;"><br />NOTE: Check only if the part setup is complete and you <br />have reviewed the travel card produced by this part set up.</span></td></tr>
      <%}
     else
     {%>
      <tr><td colspan="2"><span style="font-weight:bold;color:Green;">Is Release Ready? :</span><%Model.PartSetUp.IsReleaseReady.ToString(); %><span style="color:#003300;"><br />NOTE: You are not in the Travel Card Approver Role. <br />Only users in the Travel Card Approver role can set travel card part set ups to release ready.
 </span></td></tr>
        <%}
           %>
       <tr><td colspan="2"><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(model=>model.PartSetUp.PartComment) %>:</span><span style="color:green">(Displays on the front of the card.)</span></td></tr>
	 <tr><td colspan="2"><%= Html.TextAreaFor(model => model.PartSetUp.PartComment, new { style="width:400px;height:100px;", maxlength = "300" })%></td></tr>

    <tr><td colspan="2"><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(model=>model.PartSetUp.PartRemarks) %>:</span><span style="color:Green;">(Displays at after the specifications on the Travel Card)</span></td></tr>
   <tr><td colspan="2"><%= Html.TextAreaFor(model => model.PartSetUp.PartRemarks, new { style="width:400px;height:100px;", maxlength = "1000" })%></td></tr>
    
     <tr><td colspan="2"><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(a=>a.PartSetUp.CommunicationNote) %>:</span></td></tr>
	 <tr><td colspan="2"><%= Html.TextAreaFor(a=>a.PartSetUp.CommunicationNote, new {  style="width:400px;height:50px;", maxlength = "250" })%></td></tr>
	
	 <tr><td colspan="2"><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(a=>a.PartSetUp.Notes) %>:</span></td></tr>
	 <tr><td colspan="2"><%= Html.TextAreaFor(a=>a.PartSetUp.Notes, new {  style="width:400px;height:100px;", maxlength = "1000" })%></td></tr>
   <%if(Model.CanUserEdit)
     { %>  <tr>
				<td >
				 	 <input type="submit"class="button" value="Save" />
	
			<a href="javascript:history.go(-1)">
Cancel
</a>
				</td><td></td>
			</tr><%} %>
	 </table>



	</div>




<%} %>



</asp:Content>

