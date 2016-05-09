<%@ Page Title="Part Set Up:Global Deviation File Editor" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSetUpViewModel>"  %>


<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Part Set Up Search
</asp:Content>
<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {

         $("#PartSetUpsToSort").tablesorter({ headers: { 0: { sorter: false}} });
     });
    </script>

 <script type="text/javascript" language="javascript">
     $(function () {
         $('.button-updatedeviationFiles').click(function () {
             return confirm('Are you sure you wish to update the deviation file for the selected part set ups? ');
         });
     });
</script>

    <script>
        $(function () {
            $("#startdate2").datepicker();
        });
	</script>
    <script>
        $(function () {
            $("#enddate2").datepicker();
        });
	</script>


  <script type="text/javascript">
      function searchSel() {
          var input = document.getElementById('searchtxt').value.toLowerCase();
          var output = document.getElementById('ddlDeviation2Files').options;
          for (var i = 0; i < output.length; i++) {
              if (output[i].value.indexOf(input) > 0) {
                  output[i].selected = true;
              }

              if (document.forms[0].searchtxt.value == '') {
                  output[0].selected = true;
              }
          }
      }
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.check:button').toggle(function () {
            $('.cb-element').attr('checked', 'checked');
            $(this).val('De-Select All')
        }, function () {
            $('.cb-element').removeAttr('checked');
            $(this).val('Select All');
        })
    })

</script>





</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
 

  <div id="partsetups">
  
   <h2>Edit Secondary Deviation File Links</h2>
     
      	<div class='pageheaders'>Global Update Search </div><br /><br />
        <%using (Html.BeginForm("PartSetUpGlobalDeviationFile2Update", "PartSetUp"))
          { %>
         
          <table class="clean-table" width="40%" >
            <tr><td><%=Html.LabelFor(a => a.PartSetUp.CategoryID)%> : </td><td><%= Html.DropDownListFor(a => a.PartSetUp.CategoryID, Model.PartCategorySelectList, "Select Part Category")%> </td></tr>
             <tr><td class="style1">Part Set Up ID: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.PartSetUpID, new { style = "width:60px;", maxlength = "50" })%>&nbsp;(Numbers Only)</td></tr>
            <tr><td class="style1">Part ID: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.PartID, new { style = "width:300px;", maxlength = "100" })%>&nbsp;(Full or partial number, don't include dashes)</td></tr>
            <tr><td class="style1">Pack Code: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.PackCode, new { style = "width:35px;", maxlength = "5" })%></td></tr>
            <tr><td class="style1">Revision: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.Revision, new { style = "width:35px;", maxlength = "5" })%></td></tr>
            <tr><td class="style1">Drawing Number :</td><td><%= Html.TextBoxFor(a => a.PartSetUp.DrawingNumber, new { style = "width:250px;", maxlength = "150" })%>&nbsp;(Full Number, Include Dashes)</td></tr>

            <tr><td>Search by Deviation File Name:</td><td><input type="text" id="searchtxt" onkeyup="searchSel()"/></td></tr>
            <tr><td>Deviation File Path:</td><td><%= Html.DropDownListFor(a => a.PartSetUp.DeviationFile2, Model.DeviationFile2PathSelectList, "Search by Deviation File Name", new { @id = "ddlDeviation2Files" })%></td></tr>
  
           
            <tr><td colspan="2">Is Release Ready? :<%= Html.CheckBoxFor(a => a.PartSetUp.IsReleaseReady)%></td></tr>
          
         
        
             <input type="submit"class="button" value="Search" />

			<a href="javascript:history.go(-1)">
Cancel
</a>
    </table>
     

     <%} %>


 
  
    <%using (Html.BeginForm("GlobalDeviationFile2Edits", "PartSetUp"))
      {

          if (Model.PartSetUps != null && Model.PartSetUps.Count() > 0 && Model.HasSearchCriteria)
          { %>
       <h2><%=Model.PartSetUps.Count().ToString()%> Part Set Ups Returned</h2>
    
           <%=Html.HiddenFor(model=>model.PartSetUp.CategoryID) %>
           <%=Html.HiddenFor(model=>model.PartSetUps) %>
           <%=Html.HiddenFor(model=>model.PartSetUp.PartID) %>
           <%=Html.HiddenFor(model=>model.PartSetUp.PartSetUpID) %>
             <%=Html.HiddenFor(model=>model.PartSetUp.Revision) %>
             <%=Html.HiddenFor(model=>model.PartSetUp.PackCode) %>
            <%=Html.HiddenFor(model=>model.PartSetUp.DrawingNumber) %>
           <%=Html.HiddenFor(model=>model.PartSetUp.IsReleaseReady) %>
         <%string pdfImagegfilepath = ConfigurationManager.AppSettings["PDFImageFilePath"];%>
   <%string filesavelocation = ConfigurationManager.AppSettings["DeviationFile"]; %>
   <% string deviationfilepath = (Model.DeviationFile2Path != null) ? Model.DeviationFile2Path : "";%>
  
  <div class='pageheaders'>Select a Secondary Deviation File (PDF)</div>           
<div style="background-color:#EDEEF7;">
 
  <div>Note: In order for a file to print with the travel card it must be located in the following file share: <span style="font-weight:bold;"><%=filesavelocation %></span>
    <br /> to insure that files can be opened by other users as 
    well as being detected by the travel card printing process.
      All Files must be in PDF format. </div>
       
      
  <div style="color:Red;"><%=Model.UserMessage %></div>
<table bgcolor="#EDEEF7" >
 
    <tr><td class="style1" bgcolor="#EDEEF7" >Deviation File :</td></tr>
   <tr><td>Deviation #2 Effective Start Date:<%=Html.TextBoxFor(model => model.PartSetUp.DeviationFile2StartDte, new { @id = "startdate2" })%>&nbsp;(Date the deviation file is to  start printing with the Travel Card.)</td></tr>
    <tr><td>Deviation #2 Effective End Date:<%=Html.TextBoxFor(model => model.PartSetUp.DeviationFile2EndDte, new { @id = "enddate2" })%>&nbsp;(Date the deviation file is to stop printing with the Travel Card. Leaving blank means no end.)</td></tr>

      <tr> <td><label for="file">Select File (<img src="<%=pdfImagegfilepath %> "/> PDF only):</label>
 <td> <input type="file" name="DeviationFile2Path" id="DeviationFile2Path" accept="file/pdf"/></td><td> Remove Links <%=Html.CheckBoxFor(a => a.DeviationFileDelete)%></td><td><span class="button-updatedeviationFiles"></span><input type="submit"class="button" value="Save Deviation File Changes" /> <a href="javascript:history.go(-1)">
Cancel
</a></td></tr>
    
         </table>
                          
         
         
         </div></div>
         <br /><br />
          <input type="button" class="check" value="Select All" /> 
          <table id="PartSetUpsToSort" class="clean-table" width="80%">
            <col style="width:auto" />
            <thead>
                <tr>
                    <td style="background-color:#EDEEF7">&nbsp;</td>
                     <td style="background-color:#EDEEF7">&nbsp;</td>
                    <td style="background-color:#EDEEF7">&nbsp;</td>
                   
                    <th style="background-color:#EDEEF7">ID</th>
                    <th>Item ID</th>
                    <th>Category</th>
                    <th>Drawing #</th>
                    <th>Deviation#1 File</th>
                    <th>Deviation#1 Start Date</th>
                    <th>Deviation#1 End Date</th>
                     <th>Deviation#2 File</th>
                    <th>Deviation#2 Start Date</th>
                    <th>Deviation#2 End Date</th>
                    <th>Release Ready</th>
                    <th style="background-color:#EDEEF7">Last Update</th>
                    <th>Last Updated By</th>
                      
                </tr>
            </thead>
            <tbody>
          
   <%int count = 0; %>
   <%DateTime? addprocessupdate = null;
     DateTime? partsetupupdate;

     DateTime? partspecificationlastupdate = null;
     DateTime? maxDate;
     string lastaddprocessupdater = "";
     string lastspecificationupdater = "";
     string lastpartsetupupdater = "";
     string maxupdater = "";


     string releaseready = "";
     
     %>
    <%foreach (var partsetup in Model.PartSetUps.OrderBy(a => a.PartCategory.CategoryName))
      {


          var drawingnumber = partsetup.DrawingNumber != null ? partsetup.DrawingNumber.ToUpper() : "";
          var deviationfile1 = partsetup.DeviationFile != null ? partsetup.DeviationFile.ToLower() : "";
          var deviationfile1start = partsetup.DeviationFileStartDte!= null ? partsetup.DeviationFileStartDte.ToString() : "";
          var deviationfile1end = partsetup.DeviationFileEndDte != null ? partsetup.DeviationFileEndDte.ToString() : "";
           var deviationfile2 = partsetup.DeviationFile2 != null ? partsetup.DeviationFile2.ToString() : "";
          var deviationfile2start = partsetup.DeviationFile2StartDte!= null ? partsetup.DeviationFile2StartDte.ToString() : "";
          var deviationfile2end = partsetup.DeviationFile2EndDte != null ? partsetup.DeviationFile2EndDte.ToString() : "";
          var revision = partsetup.Revision != null ? partsetup.Revision.ToUpper() : "";
          var packcode = partsetup.PackCode != null ? partsetup.PackCode.ToUpper() : "";


          releaseready = partsetup.IsReleaseReady ? "Yes" : "No";
          //get the last update.
          int addprocessescount = Model.AdditionalProcesses.Where(a => a.PartSetUpID == partsetup.PartSetUpID).Select(c => c.LastEditDate).Count();

          int partspecificationcount = Model.PartSpecifications.Where(a => a.PartSetUpID == partsetup.PartSetUpID).Select(c => c.LastEditDate).Count();
          addprocessupdate = addprocessescount > 0 ? Model.AdditionalProcesses.Where(a => a.PartSetUpID == partsetup.PartSetUpID).Select(c => c.LastEditDate).Max() : null;
          if (addprocessupdate != null)
          {
              var lastupdaters = Model.AdditionalProcesses.Where(a => a.PartSetUpID == partsetup.PartSetUpID && a.LastEditDate == addprocessupdate).Select(x => x.LastEditedBy);
              lastaddprocessupdater = lastupdaters.First();
          }
          else
          {
              lastaddprocessupdater = "";
          }
          if (partspecificationcount > 0)
          {
              partspecificationlastupdate = Model.PartSpecifications.Where(a => a.PartSetUpID == partsetup.PartSetUpID).Select(c => c.LastEditDate).Max();
              if (partspecificationlastupdate != null)
              {
                  var lastupdaters = Model.PartSpecifications.Where(a => a.PartSetUpID == partsetup.PartSetUpID && a.LastEditDate == partspecificationlastupdate).Select(c => c.LastEditBy);

                  lastspecificationupdater = lastupdaters.First();
              }
          }
          else
          {
              lastspecificationupdater = "";

          }


          partsetupupdate = partsetup.LastEditDate;

          lastpartsetupupdater = partsetup.LastEditBy.ToString();

          List<DateTime?> LastUpdates = new List<DateTime?>();
          LastUpdates.Add(addprocessupdate);
          LastUpdates.Add(partspecificationlastupdate);

          LastUpdates.Add(partsetupupdate);
          maxDate = LastUpdates.Max();

          if (maxDate == addprocessupdate)
          {
              maxupdater = lastaddprocessupdater;

          }

          if (maxDate == partspecificationlastupdate)
          {
              maxupdater = lastspecificationupdater.ToString();

          }

          if (maxDate == partsetupupdate)
          {

              maxupdater = lastpartsetupupdater;
          }

          count++;
          var partcategoryname = Model.PartCategories.FirstOrDefault(a => a.CategoryID == partsetup.CategoryID);
       
           %>
      <tr>
      <td><%=count.ToString()%></td>
      <td><input type="checkbox" class="cb-element" name="PartSetUptoUpdate_<%=partsetup.PartSetUpID%>"  value="<%=partsetup.PartSetUpID%>" /></td>
                 
      <td>
   
      <%=Html.ActionLink("View", "PartSetUpViewer", "PartSetUp", new { partsetupid_ = partsetup.PartSetUpID }, null)%>
    
      &nbsp</td>
      <td><%=partsetup.PartSetUpID.ToString()%>&nbsp;</td>
       <td><%=partsetup.PartID%>&nbsp;</td>
      <td><%=partcategoryname.CategoryName%>&nbsp;</td>

      <td><%=drawingnumber%>&nbsp;</td>
        <td><%=deviationfile1%>&nbsp;</td>
         <td><%=deviationfile1start%>&nbsp;</td>
          <td><%=deviationfile1end%>&nbsp;</td>

          <td><%=deviationfile2%>&nbsp;</td>
         <td><%=deviationfile2start%>&nbsp;</td>
          <td><%=deviationfile2end%>&nbsp;</td>
      
      <%if (partsetup.IsReleaseReady)
        { %>
      <td><span style="color:#003300;font-weight:bold;"><%=releaseready%></span>-&nbsp;by&nbsp;<%=partsetup.LastApprover%>&nbsp; on <%=partsetup.LastApprovalDateTime%></td>
        <%}
        else
        {%>
         <td><span style="color:Red;font-weight:bold"><%=releaseready%>&nbsp;</span></td>
         <%} %>   
    
  <td><%=maxDate.ToString()%>&nbsp;</td>

   <td><%=maxupdater.ToString()%>&nbsp;</td>
      
      </tr>

     <%}%>
   </tbody>
    </table>
   
 
    
 <%  
        }    }
   %></div>

</asp:Content>
