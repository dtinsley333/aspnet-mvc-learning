<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSetUpViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Part SetUp Listing
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
  
   
     
      	<div class='pageheaders'>Part Set Up Listing </div><br />

 
   
      <%
    
 if (Model.PartSetUps != null && Model.PartSetUps.Count() > 0)
      { %>
       
          <table id="PartSetUpsToSort" class="clean-table" width="80%">
            <col style="width:auto" />
            <thead>
                <tr>
                     <td style="background-color:#EDEEF7">&nbsp;</td>
                      <td style="background-color:#EDEEF7">&nbsp;</td>
                    <th style="background-color:#EDEEF7">ID</th>
                    <th>Item ID</th>
                    <th>Category</th>
                    <th>Release Ready</th>
                   <th style="background-color:#EDEEF7">Last Update</th>
                    <th>Last Updated By</th>
                      
                </tr>
            </thead>
            <tbody>
          
   <%int count=0; %>
   <%DateTime? addprocessupdate=null;
     DateTime? partsetupupdate;
  
     DateTime? partspecificationlastupdate = null;
     DateTime? maxDate;
     string lastaddprocessupdater = "";
     string lastspecificationupdater = "";
     string lastpartsetupupdater = "";
     string maxupdater = "";


     string releaseready = "";
     
     %>
    <%foreach(var partsetup in Model.PartSetUps.OrderBy(a=>a.PartCategory.CategoryName))
      {

          releaseready = partsetup.IsReleaseReady ? "Yes" : "No";
          //get the last update.
          int addprocessescount=Model.AdditionalProcesses.Where(a => a.PartSetUpID == partsetup.PartSetUpID).Select(c => c.LastEditDate).Count();
        
          int partspecificationcount=Model.PartSpecifications.Where(a => a.PartSetUpID == partsetup.PartSetUpID).Select(c => c.LastEditDate).Count();
                          addprocessupdate = addprocessescount > 0 ? Model.AdditionalProcesses.Where(a => a.PartSetUpID == partsetup.PartSetUpID).Select(c => c.LastEditDate).Max() : null;
                          if (addprocessupdate != null)
                          {
                              var lastupdaters = Model.AdditionalProcesses.Where(a => a.PartSetUpID == partsetup.PartSetUpID && a.LastEditDate == addprocessupdate).Select(x => x.LastEditedBy);
                              lastaddprocessupdater = lastupdaters.First();
                          }
                          else {
                              lastaddprocessupdater = "";
                          }
                          if (partspecificationcount > 0)
                          {
                              partspecificationlastupdate = Model.PartSpecifications.Where(a => a.PartSetUpID == partsetup.PartSetUpID).Select(c => c.LastEditDate).Max();
                              if (partspecificationlastupdate !=null)
                              {
                                  var lastupdaters = Model.PartSpecifications.Where(a => a.PartSetUpID == partsetup.PartSetUpID && a.LastEditDate == partspecificationlastupdate).Select(c=>c.LastEditBy);

                                  lastspecificationupdater = lastupdaters.First();
                              }
                          }
                          else {
                              lastspecificationupdater = "";
                          
                          }


                          partsetupupdate = partsetup.LastEditDate;
       
          lastpartsetupupdater = partsetup.LastEditBy.ToString();

          List<DateTime?> LastUpdates=new List<DateTime?>();
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
              maxupdater = lastspecificationupdater.ToString() ;

          }

          if (maxDate == partsetupupdate)
          {
            
              maxupdater = lastpartsetupupdater;
          }
          
          count++; 
          var partcategoryname = Model.PartCategories.FirstOrDefault(a => a.CategoryID == partsetup.CategoryID);   
           %>
      <tr>
      <td><%=count.ToString() %></td>
      <td>
   
      
      <%=Html.ActionLink("View", "PartSetUpViewer", "PartSetUp", new { partsetupid_ = partsetup.PartSetUpID }, null)%>
    
      &nbsp</td>
      <td><%=partsetup.PartSetUpID.ToString() %>&nbsp;</td>
       <td><%=partsetup.PartID %>&nbsp;</td>
      <td><%=partcategoryname.CategoryName%>&nbsp;</td>
     
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
   
 
    
 <%  } %></div></td></tr></table>

</asp:Content>



