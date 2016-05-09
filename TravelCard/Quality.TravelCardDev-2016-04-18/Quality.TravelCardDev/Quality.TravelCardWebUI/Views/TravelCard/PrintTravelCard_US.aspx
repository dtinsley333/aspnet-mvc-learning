<%@ Page Title="Part Details" Language="C#" MasterPageFile="~/Views/Shared/WPF.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.TravelCardViewModel>"  %>
 <%@ Import Namespace="Quality.Extensions" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
Part Details
</asp:Content>
 <asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server" >
<script language="javascript" type="text/javascript">
    //refresh page every 15 minutes. Session times out after 20 minutes of idle time. If
    //user attempts to do a search after the session is gone an error will be thrown.
    //This was added because floor users use the application open and my not return 
    //to it for several hours.
    var timeout = setTimeout("location.reload(true);", 1000 * 60 * 15);
    function resetTimeout() {
        clearTimeout(timeout);
        timeout = setTimeout("location.reload(true);", 1000 * 60 * 15);
    }
 </script> 
   </asp:Content>  
  
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
   <% string ideallogo = ConfigurationManager.AppSettings["ideallogo"];%>
    	<%  
		 
				string userversion = ConfigurationManager.AppSettings["userversion"];
				%>
          <br />  
  <div>
				  <div id="header">
		   <span class="ideallogo"  ><a href="<%=Url.Action("PartMaintenanceIndex","TravelCard") %>" ><img src="<%=ideallogo%> "  style="text-decoration: none;
	border: 0 none;" /> </a> </span><br /><br /> <span class="headertitle"><%=Quality.Resources.Strings.AppTitle%>-<%=userversion%>&nbsp;&nbsp;Version: <%=Html.CurrentVersion()%></span>
     
   </div>
<div id="pagecontents" style="width:100%;">
 <%=Html.Encode(ViewData["userinfo"])%>

 

	

	  <% using (Html.BeginForm("PrintTravelCard_US", "TravelCard"))
		 { %>



	<div class='pageheaders'>Part Search</div><br/>
	 <table id="ItemIDSearch" class="standard-table" width="95%">
	<col class="label-column" />
			<col class="data-column" />
			 <tr>
				<td>Part ID:</td>
				  <td>
				   <%= Html.TextBoxFor(a=>a.ItemID, new {  style="width:100px;", maxlength = "15" }) %>&nbsp;<span style="color:Green;">No Dashes or Spaces.</span>
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
               
			</tr>
		  
			</table>
		


 <%if (Model != null && Model.PartSetUp != null)
   { %>
     <%string printerapppath = ConfigurationManager.AppSettings["OMIPrintingAppPath"];%>

  <div class='pageheaders'><img src="http://tn-sqldevel:3333/contents/images/print-icon.png" /> </div> 
  <br />  <br />
  <%string partid = Model.PartSetUp.PartID; %>
  <%if (Model.PartSetUp.IsReleaseReady)
    {%>
  <span>This Travel Card has been set to release ready.</span><br />
    
   <% }
    else
    {%>
     <span style="color:Red;">This travel card has not been set to release ready. Please contact the Quality Department.</span><br />
   <% } %>


<%if (Model.PartSetUp.IsReleaseReady)
  {

    

      Response.Redirect(printerapppath +"?_openapp=1&_language=en-US&_partid="+partid+"&_isdraft=0");
       %>

<%} 
  }
       %>

  <br />   <br />   
    <%}
 %>


</asp:Content>
