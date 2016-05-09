<%@ Page Title="Add Part Process" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSpecificationViewModel>"  ValidateRequest="false" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
Add Additional Part Process
</asp:Content>


<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
  
 
  
    <style type="text/css">
        .style2
        {
            width: 65px;
        }
        .style3
        {
            width: 119px;
        }
    </style>
  
 
  
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br /><br />


	 <% using (Html.BeginForm("PartSpecificationEdit", "PartSpecification"))
     { %>

 <%string itemid = Model.PartSetUp.PartID; %>
    <div class='pageheaders'>Edit Specification for <%=(itemid) %></div><br />
  <%=Html.HiddenFor(model=>model.PartSetUpID)%>
   <%= Html.HiddenFor(model => model.PartSetUp.PartID)%> 
    <%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%> 
    <%=Html.HiddenFor(model=>model.ParentPartID) %>
       <%= Html.HiddenFor(model => model.PartSpecification.SpecificationID)%> 

         
            
         <%=Html.HiddenFor(model=>model.OpCode) %>
      
 <%string id = Model.PartSpecification.SpecificationID.ToString(); %>

 <table id="CreateInitialSetup" class="clean-table">


   <tr><td class="style3" >Part Specification ID</td><td><%=(id) %>
</td></tr>  
  
  <tr><td class="style3"><%=Html.LabelFor(a=>a.PartSpecification.Characteristic) %>:</td>
      <td><span style="color:Green">NOTE: Place &lt;b&gt;Text to bold&lt;/b&gt; around text you wish to <b>Bold</b>.<br /> </span><%=Html.TextBoxFor(a => a.PartSpecification.Characteristic, new { style = "width:450px;", maxlength = "250" })%><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.Characteristic)%></td></tr>
   <tr><td class="style3"><%=Html.LabelFor(a=>a.PartSpecification.CharacteristicES) %>:</td>
       <td><%=Html.TextBoxFor(a=>a.PartSpecification.CharacteristicES, new {  style="width:450px;", maxlength = "250" }) %><span style="color:red;">* Required if OMI will be printed in Spanish</span></td></tr>

         <tr><td class="style3"><%=Html.LabelFor(a=>a.PartSpecification.CharacteristicCN) %>:</td>
       <td><%=Html.TextBoxFor(a=>a.PartSpecification.CharacteristicCN, new {  style="width:450px;", maxlength = "250" }) %><span style="color:red;">* Required if OMI will be printed in Chinese</span></td></tr>
 
  
    <tr><td class="style3">Measurement Method: </td><td><%= Html.DropDownListFor(a=>a.PartSpecification.MeasurementMethodID, Model.MeasurementMethodSelectList,"Select One")%><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.MeasurementMethodID)%></td></tr>
                                                                                                                                                                                                          

 
 
   <tr><td class="style3"><%=Html.LabelFor(a => a.PartSpecification.SampleSize)%>:</td>
       <td><%=Html.TextBoxFor(a=>a.PartSpecification.SampleSize, new {  style="width:100px;", maxlength = "75" }) %></td></tr>
    <tr><td class="style3"><%=Html.LabelFor(a => a.PartSpecification.SampleSizeES)%>:</td>
                                <td><%=Html.TextBoxFor(a=>a.PartSpecification.SampleSizeES, new {  style="width:100px;", maxlength = "75" }) %></td></tr>

                                  <tr><td class="style3">Frequency:</td><td><%= Html.DropDownListFor(a=>a.PartSpecification.FrequencyID,Model.FrequencySelectList,"Select One")%></td></tr>
       <tr><td class="style3">Unit of Measure for High/Low Specs.: </td><td><%= Html.DropDownListFor(a=>a.PartSpecification.unitID, Model.MeasurementUnitSelectList,"Select One")%></td></tr>
     

    <tr><td class="style3"><%=Html.LabelFor(a => a.PartSpecification.LowSpec)%>:</td>
        <td><%=Html.TextBoxFor(a=>a.PartSpecification.LowSpec, new {  style="width:100px;", maxlength = "30" }) %><span style="color:#003300;">(.,-,0-9 Characters Only)</span><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.LowSpec)%></td></tr>
   

    <tr><td class="style3"><%=Html.LabelFor(a => a.PartSpecification.HighSpec)%>:</td>
        <td><%=Html.TextBoxFor(a=>a.PartSpecification.HighSpec, new {  style="width:100px;", maxlength = "30" }) %><span style="color:#003300;">(.,-,0-9 Characters Only)</span><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.HighSpec)%></td></tr>
   
            <tr><td class="style3"><%=Html.LabelFor(a=>a.PartSpecification.OperationCode) %>:</td><td><%=Html.TextBoxFor(a => a.PartSpecification.OperationCode, new { style = "width:50px;", maxlength = "6" })%> <span style="color:#003300;">(Numbers Only)</span><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSpecification.OperationCode)%></td></tr>
  <tr><td class="style3">Active:</td><td class="style2"><%=Html.CheckBoxFor(a=>a.PartSpecification.IsActive) %></td></tr>
  <tr><td class="style3">Part Characteristic:<br />
      (Prints only on the current part travel card.)</td><td class="style2"><%=Html.CheckBoxFor(a=>a.PartSpecification.IsPartCharacteristic) %></td></tr>
   <tr><td class="style3">Component Characteristic:<br />
       (Prints only when a component of another part.)</td><td class="style2"><%=Html.CheckBoxFor(a=>a.PartSpecification.IsComponentCharacteristic) %></td></tr>
      <tr><td class="style3">Machine Set Up Characteristic:</td><td class="style2"><%=Html.CheckBoxFor(a=>a.PartSpecification.IsMachineSetUpCharacteristic) %></td></tr>
         <tr><td class="style3">Die Set Up Characteristic:</td><td class="style2"><%=Html.CheckBoxFor(a=>a.PartSpecification.IsDieSetUpCharcteristic) %></td></tr>
  <tr><td class="style3">Notes:</td><td class="style2"><%= Html.TextAreaFor(a=>a.PartSpecification.Notes, new {  style="width:400px;height:100px;", maxlength = "1000" })%></td></tr>
 </table>
  <tr>
				<td colspan="2">
					   <input type="submit" class="button" name="SubmitConfirmation" value="Save" onclick="getElementById('progress').parentNode.style.display = 'block';" />
                  <div style="display:none">
<br /><span id='progress' style="position:relative;font-size:85%;font-weight:bold;padding:5px;
	color:#003300;
	z-index:10;">Saving Part Specification Changes ... Please Wait.</span></div>
			<a href="javascript:history.go(-1)">
Cancel
</a>	

				</td>
			</tr>

        <%} %>
    

</asp:Content>




