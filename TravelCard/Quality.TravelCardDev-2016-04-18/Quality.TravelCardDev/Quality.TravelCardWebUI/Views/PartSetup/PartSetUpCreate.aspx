<%@ Page Title="Create Set Up" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSetUpViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Create New Part Set Up
</asp:Content>
<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>


<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <%string pdfImagegfilepath = ConfigurationManager.AppSettings["PDFImageFilePath"];%>
    <div id="pagecontents" style="width:100%;">
    <br /><br />
    <%string itemid = Model.ItemID; %>


	 <% using (Html.BeginForm("PartSetUpCreate", "PartSetUp"))
		{ %>

   <%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%> 
   
            
               <%= Html.HiddenFor(model => model.ItemID)%> 
               <span style="color:Red"><%=Model.UserMessage %></span>
               <br />
	<span class='pageheaders'>Create New Part Set Up for <%=itemid %></span><br />
	 <table id="CreateInitialSetup" class="standard-table">
                                                       
	 <tr><td><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(a=>a.PartSetUp.CategoryID) %> : </span></td><td><%= Html.DropDownListFor(a=>a.PartSetUp.CategoryID,Model.PartCategorySelectList,"Select One") %><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSetUp.CategoryID)%></td></tr>
      <tr><td class="style1"><span style="font-weight:bold;color:Green;">Pack Code: </span></td><td><%= Html.TextBoxFor(a=>a.PartSetUp.PackCode, new {  style="width:35px;", maxlength = "5" }) %></td></tr>
	 <tr><td class="style1"><span style="font-weight:bold;color:Green;">Revision:</span> </td><td><%= Html.TextBoxFor(a=>a.PartSetUp.Revision, new {  style="width:35px;", maxlength = "5" }) %></td></tr>

     <tr><td class="style1"><span style="font-weight:bold;color:Green;"> Drawing Number:</span> </td><td><%= Html.TextBoxFor(a=>a.PartSetUp.DrawingNumber, new {  style="width:250px;", maxlength = "150" }) %>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSetUp.DrawingNumber)%></td></tr>

      <tr><td colspan="2"><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(model=>model.PartSetUp.CommunicationNote) %>:</span> <span style="color:Green;">(Displays on the front of the card.)</span></td></tr>
	 <tr><td colspan="2"><%= Html.TextAreaFor(model=>model.PartSetUp.CommunicationNote, new {  style="width:400px;height:50px;", maxlength = "250" })%></td></tr>
	
     <tr><td colspan="2"><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(model=>model.PartSetUp.PartComment) %>:</span></td></tr>
	 <tr><td colspan="2"><%= Html.TextAreaFor(model => model.PartSetUp.PartComment, new { style="width:400px;height:100px;", maxlength = "300" })%></td></tr>

    <tr><td colspan="2"><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(model=>model.PartSetUp.PartRemarks) %>: </span><span style="color:Green;">(Displays on the back of the Travel Card)</span></td></tr>
   <tr><td colspan="2"><%= Html.TextAreaFor(model => model.PartSetUp.PartRemarks, new { style="width:400px;height:100px;", maxlength = "1000" })%></td></tr>
    


	 <tr><td colspan="2"><span style="font-weight:bold;color:Green;"><%=Html.LabelFor(model=>model.PartSetUp.Notes) %>:</span></td></tr>
	 <tr><td colspan="2"><%= Html.TextAreaFor(model => model.PartSetUp.Notes, new { style="width:400px;height:100px;", maxlength = "1000" })%></td></tr>
   <tr>
				<td colspan="2">
					 <input type="submit" class="button" name="SubmitConfirmation" value="Save" onclick="getElementById('progress').parentNode.style.display = 'block';" />
                  <div style="display:none">
<br /><span id='progress' style="position:relative;float:right;font-size:85%;font-weight:bold;padding:5px;
	color:#003300;
	z-index:10;">Saving New Part Set Up ... Please wait.</span></div>
				
			<a href="javascript:history.go(-1)">
Cancel
</a>
				</td>
			</tr>
	 </table>



	</div>




<%} %>



</asp:Content>

