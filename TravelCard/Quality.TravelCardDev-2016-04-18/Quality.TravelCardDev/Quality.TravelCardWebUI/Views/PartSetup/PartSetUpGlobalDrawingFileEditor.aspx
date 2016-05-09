<%@ Page Title="Part Set Up:Global Drawing File Editor" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSetUpViewModel>"  %>


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
         $('.button-updatedrawingFiles').click(function () {
             return confirm('Are you sure you wish to update the drawing file for the selected part set ups? ');
         });
     });
</script>


  <script type="text/javascript">
      function searchSel() {
          var input = document.getElementById('searchtxt').value.toLowerCase();
          var output = document.getElementById('ddlDrawingFiles').options;
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
  
   <h2>Edit Drawing File Links</h2>
     
      	<div class='pageheaders'>Global Update Search </div><br /><br />
        <%using (Html.BeginForm("PartSetUpGlobalDrawingFileUpdate", "PartSetUp"))
          { %>
         
          <table class="clean-table" width="40%" >
            <tr><td><%=Html.LabelFor(a => a.PartSetUp.CategoryID)%> : </td><td><%= Html.DropDownListFor(a => a.PartSetUp.CategoryID, Model.PartCategorySelectList, "Select Part Category")%> </td></tr>
             <tr><td class="style1">Part Set Up ID: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.PartSetUpID, new { style = "width:60px;", maxlength = "50" })%>&nbsp;(Numbers Only)</td></tr>
            <tr><td class="style1">Part ID: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.PartID, new { style = "width:300px;", maxlength = "100" })%>&nbsp;(Full or partial number, don't include dashes)</td></tr>
            <tr><td class="style1">Pack Code: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.PackCode, new { style = "width:35px;", maxlength = "5" })%></td></tr>
            <tr><td class="style1">Revision: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.Revision, new { style = "width:35px;", maxlength = "5" })%></td></tr>
            <tr><td class="style1">Drawing Number :</td><td><%= Html.TextBoxFor(a => a.PartSetUp.DrawingNumber, new { style = "width:250px;", maxlength = "150" })%>&nbsp;(Fulle Number, Include Dashes)</td></tr>

            <tr><td>Search by Drawing File Name:</td><td><input type="text" id="searchtxt" onkeyup="searchSel()"/></td></tr>
            <tr><td>Drawing File Path:</td><td><%= Html.DropDownListFor(a => a.PartSetUp.DrawingFile, Model.DrawingFilePathSelectList, "Search by Drawing File Name", new { @id = "ddlDrawingFiles" })%></td></tr>
  
           
            <tr><td colspan="2">Is Release Ready? :<%= Html.CheckBoxFor(a => a.PartSetUp.IsReleaseReady)%></td></tr>
          
         
        
             <input type="submit"class="button" value="Search" />

			<a href="javascript:history.go(-1)">
Cancel
</a>
    </table>
     

     <%} %>


 
  
    <%using (Html.BeginForm("GlobalDrawingFileEdits", "PartSetUp"))
      {

          if (Model.PartSetUps != null && Model.PartSetUps.Count() > 0 && Model.HasSearchCriteria)
          { %>
       <h2><%=Model.PartSetUps.Count().ToString()%> Part Set Ups Returned</h2>
        <%=Html.HiddenFor(model=>model.PartSetUp.DrawingFile) %>
           <%=Html.HiddenFor(model=>model.PartSetUp.CategoryID) %>
           <%=Html.HiddenFor(model=>model.PartSetUps) %>
           <%=Html.HiddenFor(model=>model.PartSetUp.PartID) %>
           <%=Html.HiddenFor(model=>model.PartSetUp.PartSetUpID) %>
             <%=Html.HiddenFor(model=>model.PartSetUp.Revision) %>
             <%=Html.HiddenFor(model=>model.PartSetUp.PackCode) %>
            <%=Html.HiddenFor(model=>model.PartSetUp.DrawingNumber) %>
           <%=Html.HiddenFor(model=>model.PartSetUp.IsReleaseReady) %>
         <%string pdfImagegfilepath = ConfigurationManager.AppSettings["PDFImageFilePath"];%>
   <%string filesavelocation = ConfigurationManager.AppSettings["DrawingFile"]; %>
   <% string drawingfilepath = (Model.DrawingFilePath != null) ? Model.DrawingFilePath : "";%>
  
  <div class='pageheaders'>Select a Drawing File (PDF)</div>           
<div style="background-color:#EDEEF7;">
 
  <div>Note: In order for a file to print with the travel card it must be located in the following file share: <span style="font-weight:bold;"><%=filesavelocation %></span>
    <br /> to insure that files can be opened by other users as 
    well as being detected by the travel card printing process.
      All Files must be in PDF format. </div>
       
      
  <div style="color:Red;"><%=Model.UserMessage %></div>
<table bgcolor="#EDEEF7" >
 
       <tr><td class="style1" bgcolor="#EDEEF7" >Drawing File :</td>
       <td><label for="file">Select File (<img src="<%=pdfImagegfilepath %> "/> PDF only):</label></td>
 <td> <input type="file" name="DrawingFilePath" id="DrawingFilePath" accept="file/pdf"/></td><td> Remove Links <%=Html.CheckBoxFor(a => a.DrawingFileDelete)%></td><td><span class="button-updatedrawingFiles"></span><input type="submit"class="button" value="Save Drawing File Path" /> <a href="javascript:history.go(-1)">
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
               
                    <th>Drawing File</th>
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
          var drawingfile = partsetup.DrawingFile != null ? partsetup.DrawingFile : "";
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
        <td><%=drawingfile%>&nbsp;</td>
      
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
