

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Quality.ViewModels.PartSpecificationViewModel>"  %>
 <script type="text/javascript" language="javascript">
     $(function () {
        $('.button-removespec').click(function () {
            return confirm('Are you sure you wish to set this specification to inactive? Once inactive it will no longer print on the OMI.');
        });
    });
</script>

<script type="text/javascript">
    $(document).ready(function () {
        $("form[action$='SetSpecificationSequenceOrder']").submit(function () {
            $.ajax({
                url: $(this).attr("action"),
                type: "post",
                data: $(this).serialize(),
                dataType: "json",
                success: function (PartSpecificationData) {
                    $("#sequenceid" + PartSpecificationData.SpecificationID).html(PartSpecificationData.SequenceID);



                }
            });

            return false;
        });
        return false;
    }); 
 </script> 






 <table><tr><td>
  <div id="additionalProcesses">
   <script type="text/javascript" language="javascript">
       $(document).ready(function () {

           $("#SpecificationsToSort").tablesorter({ headers: { 0: { sorter: false}} });
       });
    </script>
  
    <%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%> 
 

   <%string partid = Model.PartSetUp.PartID; %>
      	<div class='pageheaders' style="background-color:Black;color:White;">Part Specifications for <%= partid %> </div><br />
       
   <% if (Model.PartSpecifications != null)
  
       {%>
      <%  int partsetupid = Model.PartSetUp.PartSetUpID;%>
     <br />
    <%if (Model.CanUserEdit)
      { %>
       <%string linktoclonepartspecifications = "Clone Part Specifications From Another " + Model.PartSetUp.PartCategory.CategoryName +" Part Set Up";%>
      <%=Html.ActionLink("Add New Part Specification", "PartSpecificationCreate", "PartSpecification", new { PartSetUpID = partsetupid }, null)%><br />
      <br />
        <%=Html.ActionLink(linktoclonepartspecifications, "ClonePartSpecifications", "PartSpecification", new { partsetupid_ = partsetupid }, null)%> <br />
     <br />
      <%}
       }%>

 <%if (Model.PartSpecifications != null && Model.PartSpecifications.Count() > 0)
   { %>
        <table id="SpecificationsToSort" class="clean-table" width="95%">
            <col style="width:auto" />
            <thead>
                <tr>
                     <th style="background-color: #e8eef4;">&nbsp;</th>
                    <th>ID</th>
                     <td style="background-color: #e8eef4;">&nbsp;&nbsp;&nbsp;<input type="button" value="Refresh Page" onclick="window.location.href=window.location.href"/>
</td>
                    <th>Spec.EN</th>
                    <th>Spec.MX</th>
                    <th>Spec.CN</th>
                    <th>Op. Code</th>
                    <th>Meas. Method</th>
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
      <%= Html.ActionLink("Edit", "PartSpecificationEdit", new { specificationid_ = specification.SpecificationID, parentID_ = Model.PartSetUp.PartID }, null)%><br /><br/>
    <span class="button-removespec"> <%= Html.ActionLink("Remove", "PartSpecificationRemove", "PartSpecification", new { specificationid_ = specification.SpecificationID },null)%></span> 
      <%} %>
      <%
      
     var measurementmethod = Model.MeasurementMethods.FirstOrDefault(a => a.MeasurementMethodID == specification.MeasurementMethodID);
     var measurementunit=Model.MeasurementUnits.FirstOrDefault(a=>a.unitID==specification.unitID);

     var measurmentmethodname = (measurementmethod != null) ? measurementmethod.Description_EN : "";
     var opcode = (specification.OperationCode != null) ? specification.OperationCode.ToString() : "";
     var unitname = (measurementunit != null) ? measurementunit.Abbreviation : "";
          
           %>
      &nbsp</td>
      <%int partspecificationid = specification.SpecificationID;%>
      <%string specificationanchor="specification"+partspecificationid.ToString(); %>
      <%string specid = Convert.ToString(partspecificationid).ToString(); %>
      <td><a name="<%=(specificationanchor)%>">
      <%=(specid)%></a>&nbsp;</td>
    
    
       
 
          <% using (Html.BeginForm("SetSpecificationSequenceOrder","PartSpecification"))
    {%>
    <%string sequenceidlable = "sequenceid" + partspecificationid.ToString(); %>

    <%string sequenceidstring = specification.SequenceID.ToString();%>
    <%if (sequenceidstring.Length < 2)
      {
          sequenceidstring = "0" + sequenceidstring;
      } 

 
   if(Model.CanUserEdit)
   {%>

            <td><span id="<%=sequenceidlable%>"><%=sequenceidstring%></span>&nbsp; <%=Html.HiddenFor(a=>specification.SpecificationID)%> <%= Html.DropDownListFor(a => a.PartSpecification.SequenceID, Model.PartSpecificationSequenceSelectList, "Change Order", new { id = "sequenceid", onchange = "$(\"#button" + partspecificationid.ToString() + "\").click(); " })%><input type="submit" id="button<%=partspecificationid.ToString()%>" style="display: none"  /></td>
<%} 


      } %>
   
      <td><%=specification.Characteristic.ToUpper()%>&nbsp;</td>
       <td><%=specification.CharacteristicES%>&nbsp;</td>
        <td><%=specification.CharacteristicCN%>&nbsp;</td>
          <td><%=opcode%>&nbsp;</td>
      <td><%=measurmentmethodname%>&nbsp;</td>
      <td><%=specification.LowSpec%>&nbsp;<%=unitname%>&nbsp;</td>
      <td><%=specification.HighSpec%>&nbsp;<%=unitname%>&nbsp;</td>
  
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

     
   </tbody>
    </table>
   
 
    
 <% }

    %></div></td></tr></table>
