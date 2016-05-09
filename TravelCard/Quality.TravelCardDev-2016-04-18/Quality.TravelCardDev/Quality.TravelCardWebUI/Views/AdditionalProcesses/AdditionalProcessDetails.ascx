<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Quality.ViewModels.AdditionalProcessingViewModel>"  %>
 <table><tr><td>
  <div id="additionalProcesses">
  
   <script type="text/javascript" language="javascript">
       $(function () {
           $('.button-removeprocess').click(function () {
               return confirm('Are you sure you wish to set this outside process to inactive? Once inactive it will no longer print on the OMI.');
           });
       });
</script>
  
  <script type="text/javascript">
      $(document).ready(function () {
          $("form[action$='SetAdditionalProcessSequenceOrder']").submit(function () {
              $.ajax({
                  url: $(this).attr("action"),
                  type: "post",
                  data: $(this).serialize(),
                  dataType: "json",
                  success: function (AdditionalProcessingData) {
                      $("#sequenceid" + AdditionalProcessingData.ProcessingID).html(AdditionalProcessingData.SequenceID);

                    

                  }
              });

              return false;
          });
          return false;
      }); 
 </script> 
  
   <script type="text/javascript" language="javascript">
       $(document).ready(function () {

           $("#ProcessesToSort").tablesorter({ headers: { 0: { sorter: false}} });
       });
    </script>
  
    <%  int partsetupid=Model.PartSetUp.PartSetUpID;%>
      <%string partid = Model.PartSetUp.PartID; %>
      	<div class='pageheaders' style="background-color:Orange;color:Black;">Additional Processes for <%= partid %> </div><br />

 <%if (Model.CanUserEdit)
       { %>
       <%string linktoaddprocesses = "Clone Additional Processes From Another " + Model.PartSetUp.PartCategory.CategoryName +" Part Set Up";%>
     <div> <%=Html.ActionLink("Add New Additional Process", "AdditionalProcessessCreate", "AdditionalProcesses", new { PartSetUpID = partsetupid }, null)%><br /><br /></div>
     <%=Html.ActionLink(linktoaddprocesses,"CloneAdditionalProcesses", "AdditionalProcesses", new { partsetupid_ = partsetupid },null)%> <br />
      <%}%>

   
     
     <br />
    
 

 <%if (Model.AdditionalProcesses != null && Model.AdditionalProcesses.Count() > 0)
      { %>
          <%if(Model.AdditionalProcesses.Count()>20)
            {%>
             <span style="color:Red;">**Alert** Having more than 20 additional process may cause the first page of the OMI to print on two pages.
             <br /> </span>
               <% } %>
          <table id="ProcessesToSort" class="clean-table" width="95%">
            <col style="width:auto" />
            <thead>
                <tr>
                     <th style="background-color: #e8eef4;">&nbsp;</th>
                    <th>ID</th>
                    <th>Desc.EN</th>
                    <th>Desc.MX</th>
                    <th>Desc.CN</th>
                    <th>Active</th>
                    <th>Order</th>
                    <th>Notes</th>
                    <th>Last Edited By</th>
                    <th>Last Edit Date</th>
                    
                </tr>
            </thead>
            <tbody>
          
    <%foreach(var process in Model.AdditionalProcesses)
      { %>
      <tr>
      <td>
      <%if (Model.CanUserEdit)
        { %>
      <%= Html.ActionLink("Edit", "AdditionalProcessesEdit", new { processid_ = process.ProcessingID }, null)%>&nbsp;&nbsp;
     <span class="button-removeprocess"> <%= Html.ActionLink("Remove", "AdditionalProcessRemove","AdditionalProcesses", new { processid_ = process.ProcessingID }, null)%></span>
      <%} %>
      &nbsp</td>
      <%int processid = process.ProcessingID; %>
      <%string processanchor = "process" + processid.ToString(); %>
      <%string procid = Convert.ToString(processid).ToString(); %>
      <td><a name="<%=(processanchor)%>"><%=process.ProcessingID.ToString() %>&nbsp;</a></td>
      <td><%=process.Description.ToUpper() %>&nbsp;</td>
      <td><%=process.DescriptionES %>&nbsp;</td>
      <td><%=process.DescriptionCN %>&nbsp;</td>
      <td><%= process.IsActive.ToString() %>&nbsp;</td>
       
          <% using (Html.BeginForm("SetAdditionalProcessSequenceOrder","AdditionalProcesses"))
    {%>
    <%var additionalprocessid=process.ProcessingID; %>
    <%string sequenceidlable = "sequenceid" + additionalprocessid.ToString(); %>
            <td><span id="<%=sequenceidlable%>"><%=process.SequenceID.ToString()%></span>&nbsp; <%=Html.HiddenFor(a=>process.ProcessingID)%> <%= Html.DropDownListFor(a => a.AdditionalProcess.SequenceID, Model.AdditionalProcessingSequence, "Choose", new { id = "sequenceid", onchange = "$(\"#button" + additionalprocessid.ToString() + "\").click(); " })%><input type="submit" id="button<%=additionalprocessid.ToString()%>" style="display: none"  /></td>



      <%} %>
				<%if( process.Notes != ""){%>
                 <%string notepadfileimagepath = ConfigurationManager.AppSettings["NotePadImageFilePath"];%>
			  <td> <a href='#'id="tooltip" title='<%=process.Notes%>'><img src="<%=notepadfileimagepath %>" style="border-style: none" /></a></td>
			   <%}
				   else{%><td>&nbsp;</td>
			<%}%>
       <td><%=process.LastEditedBy %>&nbsp;</td>
        <td><%=process.LastEditDate.ToString() %>&nbsp;</td>
      </tr>

     <%}%>
   </tbody>
    </table>
   
 
    
 <%  } %></div></td></tr></table>