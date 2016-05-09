<%@ Page Title="Add Part Process" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSpecificationViewModel>" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Add Additional Part Process
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
  


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br /><br />
<%string itemid = Model.PartSetUp.PartID; %>
 

	 <% using (Html.BeginForm("PartSpecificationCreate", "PartSpecification"))
     { %>
   <div class='pageheaders'>Add Specification to <%=(itemid) %></div><br />
   <%= Html.HiddenFor(model => model.PartSetUp.PartID)%> 
    <%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%> 
  <%= Html.HiddenFor(model => model.PartSetUpID)%> 
  <%= Html.HiddenFor(model => model.MeasurementMethodID)%> 

      <%= Html.HiddenFor(model => model.FrequencyID)%> 
    
 


 <table id="CreateInitialSetup" class="clean-table">
          
  <tr><td><%=Html.LabelFor(a=>a.PartSpecification.Characteristic )%>:</td><td><span style="color:Green">NOTE: Place &lt;b&gt;Text to bold&lt;/b&gt; around text you wish to <b>Bold</b>.<br /> </span><%=Html.TextBoxFor(a=>a.PartSpecification.Characteristic, new {  style="width:400px;", maxlength = "250" }) %><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.Characteristic)%></td></tr>
  <tr><td><%=Html.LabelFor(a=>a.PartSpecification.CharacteristicES )%>:</td><td><%=Html.TextBoxFor(a=>a.PartSpecification.CharacteristicES, new {  style="width:400px;", maxlength = "250" }) %><span style="color:red;">* Required if Travel Card will be printed in Spanish</span></td></tr>

  <tr><td><%=Html.LabelFor(a=>a.PartSpecification.CharacteristicCN) %>:</td><td><%=Html.TextBoxFor(a=>a.PartSpecification.CharacteristicCN, new {  style="width:400px;", maxlength = "250" }) %><span style="color:red;">* Required if Travel Card will be printed in Chinese</span></td></tr>
  
  <tr><td>Display Order: </td><td><%= Html.DropDownListFor(a => a.PartSpecification.SequenceID, Model.PartSpecificationSequenceSelectList, "Select One")%><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.SequenceID)%> </td></tr>
   
   <tr><td>Measurement Method: </td><td><%= Html.DropDownListFor(a=>a.PartSpecification.MeasurementMethodID, Model.MeasurementMethodSelectList,"Select One")%><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.MeasurementMethodID)%></td></tr>

  
   <tr><td><%=Html.LabelFor(a => a.PartSpecification.SampleSize)%>:</td><td><%=Html.TextBoxFor(a=>a.PartSpecification.SampleSize, new {  style="width:100px;", maxlength = "100" }) %></td></tr>
      <tr><td><%=Html.LabelFor(a => a.PartSpecification.SampleSizeES)%>:</td><td><%=Html.TextBoxFor(a=>a.PartSpecification.SampleSizeES, new {  style="width:100px;", maxlength = "100" }) %></td></tr>
  
    
  <tr><td>Frequency:</td><td><%= Html.DropDownListFor(a=>a.PartSpecification.FrequencyID, Model.FrequencySelectList,"Select One")%></td></tr>
                      
                              
   
       <tr><td>Unit of Measure for High/Low Specs.: </td><td><%= Html.DropDownListFor(a=>a.PartSpecification.unitID, Model.MeasurementUnitSelectList,"Select One")%></td></tr>
    <tr><td><%=Html.LabelFor(a => a.PartSpecification.LowSpec)%>:</td><td><%=Html.TextBoxFor(a=>a.PartSpecification.LowSpec, new {  style="width:100px;", maxlength = "100" }) %><span style="color:#003300;">(.,-,0-9 Characters Only)</span><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.LowSpec)%></td></tr>
  
    <tr><td><%=Html.LabelFor(a => a.PartSpecification.HighSpec)%>:</td><td><%=Html.TextBoxFor(a=>a.PartSpecification.HighSpec, new {  style="width:100px;", maxlength = "100" }) %><span style="color:#003300;">(.,-,0-9 Characters Only)</span><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.HighSpec)%></td></tr>
  
 <tr><td><%=Html.LabelFor(a=>a.PartSpecification.OperationCode) %>:</td><td><%=Html.TextBoxFor(a => a.PartSpecification.OperationCode, new { style = "width:50px;", maxlength = "6" })%> <span style="color:#003300;">(Numbers Only)</span><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.OperationCode)%></td></tr>
  <tr><td>Active:</td><td><%=Html.CheckBoxFor(a=>a.IsActive) %></td></tr>
  <tr><td>Part Characteristic:<br />
      (Prints only on the current part travel card)</td><td><%=Html.CheckBoxFor(a=>a.IsPartCharacteristic) %></td></tr>
   <tr><td>Component Characteristic:<br />
       (Print only when a component of another part)</td><td><%=Html.CheckBoxFor(a=>a.PartSpecification.IsComponentCharacteristic) %></td></tr>
      <tr><td>Machine Set Up Characteristic:</td><td><%=Html.CheckBoxFor(a=>a.PartSpecification.IsMachineSetUpCharacteristic) %></td></tr>
         <tr><td>Die Set Up Characteristic:</td><td><%=Html.CheckBoxFor(a=>a.PartSpecification.IsDieSetUpCharcteristic) %></td></tr>
  <tr><td>Notes:</td><td><%= Html.TextAreaFor(a=>a.PartSpecification.Notes, new {  style="width:400px;height:100px;", maxlength = "1000" })%></td></tr>
 
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




