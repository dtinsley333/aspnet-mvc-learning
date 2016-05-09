<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSpecificationViewModel>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Set Order of Part Specifications
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Set Order of Part Specifications </h2>
 <% using (Html.BeginForm("SetSpecificationSequenceOrder", "PartSpecification"))
    {%>
        
       <%= Html.HiddenFor(model => model.PartSpecification.SpecificationID)%>  
        
         %>
     <table><tr><td>
  <div id="additionalProcesses">
   <script type="text/javascript" language="javascript">
       $(document).ready(function () {

           $("#SpecificationsToSort").tablesorter({ headers: { 0: { sorter: false}} });
       });
    </script>
  <span>Warning: Editing the order of a sub component will affect all travel cards using the same sub component.</span>
   <%string partid = Model.PartSetUp.PartID; %>
      	<div class='pageheaders'>Part Specifications for <%= partid%> </div><br/>
         <% if (Model.PartSpecifications != null && Model.PartSpecifications.Count() > 20)
            {%>
            <div class='userwarning'>If there are more than 20 specifications. The travel card may not fit on one 8 X 11 1/2" sheet of paper.<br />
          </div>
           <%  }%>
   
      <%  int partsetupid = Model.PartSetUp.PartSetUpID;%>
     <br />
  

 <%if (Model.PartSpecifications != null && Model.PartSpecifications.Count() > 0)
   { %>
        <table id="SpecificationsToSort" class="clean-table" width="95%">
            <col style="width:auto" />
            <thead>
                <tr>
                     <th >&nbsp;</th>
                    <th>Specification ID</th>
                    <th>Part ID</th>
                    <th>Set Display Order</th>
                    <th>Characteristic</th>
                    <th>Characteristic-ES</th>
                    <th>Measurement Method</th>
                    <th>Guage(s)</th> 
                    <th>Lower Spec.</th>
                    <th>Upper Spec.</th>
                    <th>Notes</th>
                    <th class="style2">Last Edited By</th>
                    <th class="style1">Last Edit Date</th>
                   
                    
                </tr>
            </thead>
            <tbody>
          
    <%foreach (var specification in Model.PartSpecifications)
      { %>
      <tr>
      <td>
      <%if (Model.CanUserEdit)
        { %>
      <%= Html.ActionLink("Edit Parent Part", "PartSpecificationEdit", new { specificationid_ = specification.SpecificationID, parentID_ = Model.PartSetUp.PartID }, null)%>
      <%} %>
      <%
      
     var measurementmethod = Model.MeasurementMethods.FirstOrDefault(a => a.MeasurementMethodID == specification.MeasurementMethodID);

     var measurmentmethodname = (measurementmethod != null) ? measurementmethod.Description_EN : ""; 
          
           %>
      &nbsp</td>
   
      <td><%=specification.SpecificationID.ToString()%>&nbsp;</td>
       <td><%=specification.PartSetUp.PartID.ToString()%>&nbsp;</td>
       <%var sequenceid = specification.SequenceID; %>
      <td><%= Html.DropDownListFor(a=>a.PartSpecification.SequenceID, Model.PartSpecificationSequence, "Select One", new { onchange="this.form.submit();" })%></td>
      <td><%=specification.Characteristic%>&nbsp;</td>
       <td><%=specification.CharacteristicES%>&nbsp;</td>
      <td><%=measurmentmethodname%>&nbsp;</td>
       <td><%=specification.Gauges%>&nbsp;</td>
      <td><%=specification.LowSpec%>&nbsp;</td>
      <td><%=specification.HighSpec%>&nbsp;</td>
     
      <%
     if (specification.Notes != "")
     {%>
    <%string NotePadImageFilePath = ConfigurationManager.AppSettings["NotePadImageFilePath"];%>
			  <td> <a href='#'id="tooltip" title='<%=specification.Notes%>'><img src="<%=NotePadImageFilePath %>" style="border-style: none" /></a></td>
			   <%}
     else
     {%><td class="style2">&nbsp;</td>
			<%}%>
   
      
       <td class="style1"><%=specification.LastEditBy%>&nbsp;</td>
        <td><%=specification.LastEditDate.ToString()%>&nbsp;</td>
      </tr>

     <%}%>

      <%
     if (Model.ComponentPartSpecifications != null)
     {
         foreach (var specification in Model.ComponentPartSpecifications)
         {
             var measurementmethod = Model.MeasurementMethods.FirstOrDefault(a => a.MeasurementMethodID == specification.MeasurementMethodID);

             var measurmentmethodname = (measurementmethod != null) ? measurementmethod.Description_EN : "";    
                  
                   %>
      <tr>
           <td>
           <%if (Model.CanUserEdit)
             { %>
           <%= Html.ActionLink("Edit Sub Component", "PartSpecificationEdit", new { specificationid_ = specification.SpecificationID, parentID_ = Model.PartSetUp.PartID }, null)%>
           <%} %>
           &nbsp</td>
      <td><%=specification.SpecificationID.ToString()%> &nbsp;</td>
       <td><%=specification.PartSetUp.PartID.ToString()%>&nbsp;</td>
      <td><%=specification.SequenceID.ToString()%>&nbsp;</td>
      <td><%=specification.Characteristic%>&nbsp;</td>
       <td><%=specification.CharacteristicES%>&nbsp;</td>
       <td><%=measurmentmethodname%>&nbsp;</td>
       <td><%=specification.Gauges%>&nbsp;</td>
      <td><%=specification.LowSpec%>&nbsp;</td>
      <td><%=specification.HighSpec%>&nbsp;</td>
    
      <%
     if (specification.Notes != "")
     {%>
    <%string NotePadImageFilePath = ConfigurationManager.AppSettings["NotePadImageFilePath"];%>
			  <td> <a href='#'id="A1" title='<%=specification.Notes%>'><img src="<%=NotePadImageFilePath %>" style="border-style: none" /></a></td>
			   <%}
     else
     {%><td class="style2">&nbsp;</td>
			<%}%>
   
      
       <td class="style1"><%=specification.LastEditBy%>&nbsp;</td>
        <td><%=specification.LastEditDate.ToString()%>&nbsp;</td>
      </tr>

     <%}%>
   </tbody>
    </table>
   
 
    
 <% }
   }
    } %></div></td></tr></table>



</asp:Content>


