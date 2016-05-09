<%@ Page Title="Part SetUpSearch" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSetUpViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Part Set Up Search
</asp:Content>
<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
 
 <table class="standard-table"><tr><td>
  <div id="partsetups">
   <script type="text/javascript" language="javascript">
       $(document).ready(function () {

           $("#PartSetUpsToSort").tablesorter({ headers: { 0: { sorter: false}} });
       });
    </script>
  
   
     
      	<span class='pageheaders'>Advanced Part Set Up Search </span><br />
        <%using (Html.BeginForm("PartSetUpSearchResult", "PartSetUp"))
          { %>
          <table class="clean-table" width="40%" >
            <tr><td><%=Html.LabelFor(a => a.PartSetUp.CategoryID)%> : </td><td><%= Html.DropDownListFor(a => a.PartSetUp.CategoryID, Model.PartCategorySelectList, "Select Part Category")%> </td></tr>
             <tr><td class="style1">Part Set Up ID: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.PartSetUpID, new { style = "width:60px;", maxlength = "50" })%>&nbsp;(Numbers Only)</td></tr>
            <tr><td class="style1">Part ID: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.PartID, new { style = "width:300px;", maxlength = "100" })%>&nbsp;(Full or partial number, don't include dashes)</td></tr>
            <tr><td class="style1">Pack Code: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.PackCode, new { style = "width:35px;", maxlength = "5" })%></td></tr>
            <tr><td class="style1">Revision: </td><td><%= Html.TextBoxFor(a => a.PartSetUp.Revision, new { style = "width:35px;", maxlength = "5" })%></td></tr>
            <tr><td class="style1">Drawing Number :</td><td><%= Html.TextBoxFor(a => a.PartSetUp.DrawingNumber, new { style = "width:250px;", maxlength = "150" })%>&nbsp;(Full Number, Include Dashes)</td></tr>
            <tr><td colspan="2">Is Release Ready? :<%= Html.CheckBoxFor(a => a.PartSetUp.IsReleaseReady)%></td></tr>
          
         
        
             <input type="submit"class="button" value="Search" />
	
			<a href="javascript:history.go(-1)">
Cancel
</a>
    </table>
     
    
           
    
<% if (Model.PartSetUps != null && Model.PartSetUps.Count() > 0 && Model.HasSearchCriteria)
   { %>
       <h2><%=Model.PartSetUps.Count().ToString() %> Part Set Ups Returned</h2>
          <table id="PartSetUpsToSort" class="clean-table" width="80%">
            <col style="width:auto" />
            <thead>
                <tr>
                     <td style="background-color:#EDEEF7">&nbsp;</td>
                      <td style="background-color:#EDEEF7">&nbsp;</td>
                    <th style="background-color:#EDEEF7">ID</th>
                    <th>Item ID</th>
                    <th>Category</th>
                    <th>Drawing #</th>
                    <th>Pack Code</th>
                    <th>Revision</th>
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
      <td>
   
      
      <%=Html.ActionLink("View", "PartSetUpViewer", "PartSetUp", new { partsetupid_ = partsetup.PartSetUpID }, null)%>
    
      &nbsp</td>
      <td><%=partsetup.PartSetUpID.ToString()%>&nbsp;</td>
       <td><%=partsetup.PartID%>&nbsp;</td>
      <td><%=partcategoryname.CategoryName%>&nbsp;</td>

      <td><%=drawingnumber%>&nbsp;</td>
      <td><%=packcode%>&nbsp;</td>
      <td><%=revision%>&nbsp;</td>
      <%if (partsetup.IsReleaseReady)
        { %>
      <td><span style="color:#003300;font-weight:bold;"><%=releaseready%>&nbsp;</span></td>
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
   
 
    
 <%  }
          } %></div></td></tr></table>

</asp:Content>
