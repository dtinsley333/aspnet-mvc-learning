

<%@ Page Title="Part Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.TravelCardViewModel>"  %>
 
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
Part Details
</asp:Content>
 <asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server" >
 <%if(Model!=null&&Model.returnanchor!=null)
   { %>
 <%string anchor = "#"+Model.returnanchor;

   
   %>
 
  <script type="text/javascript" language="javascript">
      $(document).ready(function () {
          var $theanchor = null;
          theanchor = "<%=anchor%>";
          location.hash = theanchor;

      });
    </script>
     <%} %>
   </asp:Content>  
  
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<div id="pagecontents" style="width:100%;">
 <%=Html.Encode(ViewData["userinfo"])%>

 

	

	  <% using (Html.BeginForm("PartMaintenanceIndex", "TravelCard"))
		 { %>



	<div class='pageheaders'>Part Search</div><br/>
	 <table id="ItemIDSearch" class="standard-table" width="95%">
	<col class="label-column" />
			<col class="data-column" />
			 <tr>
				<td>Part ID:</td>
				  <td>
				   <%= Html.TextBoxFor(a=>a.ItemID, new {  style="width:100px;", maxlength = "15" }) %>&nbsp;<span style="color:Green;">(No Dashes or Spaces.)</span>
				  </td>
			 </tr>
			
			<tr>
				<td>&nbsp;</td><td>
					                   <input type="submit" class="button" name="SubmitConfirmation" value="Search" onclick="getElementById('progress').parentNode.style.display = 'block';" />
                  <div style="display:none">
<br /><span id='progress' style="position:relative;float:left;font-size:85%;font-weight:bold;padding:5px;
	color:#003300;
	z-index:10;">Searching ASI for part number ... Please wait.</span></div><br />
    <p style="color:Red;"><%= Html.Encode(ViewData["NoResultMessage"])%></p>

		<br />
                     <%=Html.ActionLink("Advanced Part Set Up Search", "PartSetUpSearch", "PartSetUp")%> </td>		
			</tr>
		  
			</table>
			<%} %>


 <%if (Model != null && Model.PartSetUp != null)
   { %>
     <%string printerapppath = ConfigurationManager.AppSettings["OMIPrintingAppPath"];%>

  <div class='pageheaders'><img src="http://tn-sqldevel:3333/contents/images/print-icon.png" /> </div> 
  <br />  <br />
  <%string partid = Model.PartSetUp.PartID; %>
  <%if (Model.PartSetUp.IsReleaseReady)
    {%>
  <span>This Travel Card Part Set Up has been set to release ready.</span><br />
    
   <% }
    else
    {%>
     <span>This Travel Card part set up has not been set to release ready.</span><br />
   <% } %>

<span><a href="<%=printerapppath %>?_openapp=1&_language=en-US&_partid=<%=partid%>&_isdraft=1">English/Draft</a> &nbsp;&nbsp;&nbsp;
<%if (Model.PartSetUp.IsReleaseReady)
  { %>
<a href="<%=printerapppath %>?_openapp=1&_language=en-US&_partid=<%=partid%>&_isdraft=0">English/Release Ready</a>&nbsp;&nbsp;&nbsp;
<%} %>



<a href="<%=printerapppath %>?_openapp=1&_language=es-MX&_partid=<%=partid%>&_isdraft=1">Spanish/Draft</a> &nbsp;&nbsp;&nbsp;
<%if (Model.PartSetUp.IsReleaseReady)
  { %>

<a href="<%=printerapppath %>?_openapp=1&_language=es-MX&_partid=<%=partid%>&_isdraft=0">Spanish/Release Ready</a>
<%} %>
  <br />   <br />   
    <%}
 %>


    </span>





	<%if (Model != null && Model.Part.Count()>0)
   { %>
	<h2>Part Details</h2>
    <%string id = Model.ItemID; %>
 <div class='pageheaders'>Part Details for <%=( id) %></div><br />
  
	   <table id='Table1' class='standard-table' style="width:60%;">

			<thead><tr>
			<th>Item ID</th>
			<th>Description</th>
			<th>Alt Description</th>
			<th>Effective Date</th>
            <th>End Date</th>
			<th>&nbsp;</th>
			 <th>&nbsp;</th>
			
			</tr></thead>
			<tbody>
			
			<% 
             
				 string partid = "";

				 foreach (var part in Model.Part)
				 {

                     string formattedeffdate = "No Date Found";
                     string formattedenddate = "No Date Found";
                    
                     partid = part.Id.Trim();
                     string itemid = Model.Part.Select(x => x.Id).First();
                     itemid = itemid.Trim();
                     var thispart = Model.ParentComponent.Where(c => c.Id.Trim() == itemid).Where(b=>b.Id!=null).FirstOrDefault();
                     if (thispart != null)
                     {
                         var partstartdate = thispart.BEGEFF;

                         string effdate = partstartdate.ToString().Trim();

                         DateTime startdate = Quality.Extensions.ConvertAS400Dates.GetDateValue(effdate);
                          formattedeffdate = startdate.ToShortDateString();

                         var partenddate = thispart.ENDEFF;

                         string enddate = partenddate.ToString().Trim();

                         DateTime theenddate = Quality.Extensions.ConvertAS400Dates.GetDateValue(enddate);
                          formattedenddate = theenddate.ToShortDateString();
                     }
  
                      %>
			
			
			 <tr>
			 <td><%=part.Id%></td>
			 <td><%=part.ITMDESC%></td> 
			 <td><%=part.ALTDESC %>e</td> 
			 <td><%=formattedeffdate%></td> 
             <td><%=formattedenddate%></td>
			
				</tr>          
				 
			
			 <%} %>
  </tbody>
  </table>


   <div class='pageheaders'>Sub Components for <%=( id) %></div><br />

 
		   <% if (Model.Component.Count() < 1)
		{%>
			
			 <span style="color:Red;">ATTENTION!!!: No BOM components were found for <%=Model.Part.FirstOrDefault(a=>a.Id.Trim()==id).Id%> at the <%=Model.Plant.PlantName +" Facility."%> Go to user settings and change plant name to the plant where the Travel Card will be printed.</span><br /><br />
			  
			  <%}
		else
		{%>

		  <table id='TableToSort' class='clean-table'>
			<thead>
			<tr>
				    <th style="background-color: #e8eef4;">Component ID</th>
					<th>Description</th>
					<th>Parent Plant Code</th>
					<th>Component Plant Code</th>
					<th>Usage Rate</th>
					<th>Beginning Date</th>
					<th>Ending Date</th>
					
			</tr></thead>
			<tbody>   
			

				 <%
            
                
               
            
            foreach (var component in Model.Component)
		{
     
        DateTime startdate = Quality.Extensions.ConvertAS400Dates.GetDateValue(component.BEGEFF.ToString());
        string formattedstartdate = startdate.ToShortDateString();
        DateTime enddate=Quality.Extensions.ConvertAS400Dates.GetDateValue(component.ENDEFF.ToString());
        string formattedenddate = enddate.ToShortDateString();
           
          //  DateTime convertendingdate = Quality.Extensions.ConvertAS400Dates.GetDateValue(component.ENDEFF.Trim());
        
                	%>
			 <tr>

			 <td><%=Html.ActionLink(component.Id, "PartMaintenanceIndex2", "TravelCard", new { ItemID = component.Id.Trim() }, null)%></td> 
			 <td><%=component.ITMDESC%></td>
			 <td><%=component.PRTPLT%></td> 
			 <td><%=component.PLT%></td> 
			 <td><%=component.USGRAT%></td> 
			 <td><%=formattedstartdate%></td> 
			 <td><%=formattedenddate%></td> 

			 </tr>          

			 <%} } %>
			

			 
</tbody>
</table>

		
			 <%if (Model.PartSetUp == null&& Model.Part.Count()>0)
			   {
         string itemid=Model.Part.Select(x=>x.Id).First();

         itemid = itemid.Trim();
 
          %> 
			   No Set Up has been created for this part.<br /><br />
               <%if (Model.CanUserEdit)
                 {%>
                <%=Html.ActionLink("Create Part Set Up", "PartSetUpCreate", "PartSetUp", new { ItemID = itemid }, new { @class = "button" })%>
                <%} %>
			   <%  }
			   else
			   {
                  int partsetupid = Model.PartSetUp.PartSetUpID;
    %>
       <%if (Model.ShowPartSetUpDetails)
         { %>
            <div id="viewDetails">
                  <% Html.RenderAction("PartSetUpDetails", "PartSetUp", new { partsetup_ = partsetupid }); %><br />
               <% Html.RenderAction("AdditionalProcessDetails", "AdditionalProcesses", new { partsetupid_ = Model.PartSetUp.PartSetUpID });%> <br />
                 <% Html.RenderAction("PartSpecificationDetails", "PartSpecification", new { partsetupid_ = Model.PartSetUp.PartSetUpID });%> <br />
              </div><%}
         else
         {%>
        <%using (Html.BeginForm( "PartMaintenanceIndexDetails", "TravelCard", new { ItemID = Model.PartSetUp.PartID})) {%>   <button type="submit" class="button">View Travel Card Specifications</button> <%}%> 
         <%}%>
           <%  
        %>
    <% }
               }%>    
</div>
<br />
</asp:Content>
