<%@ Page Title="Link to Die Set File" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSetUpViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
Link to Quality Alert File
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
   <script>
       $(function () {
           $("#startdate").datepicker();
       });
	</script>
    <script>
        $(function () {
            $("#enddate").datepicker();
        });
	</script>
</asp:Content>



<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br /><br /><br /><br /><br />
 
    <div class='pageheaders' style="width:60%;" >Link 2nd Quality Alert File to <%=Model.ItemID%> Part Set Up</div><br />


    <% using (Html.BeginForm("GetQualityAlertFile2", "PartSetUp"))
		{ %>
   <%string pdfImagegfilepath = ConfigurationManager.AppSettings["PDFImageFilePath"];%>
   <%string filesavelocation = ConfigurationManager.AppSettings["QualityAlertFile"]; %>
   <% string alertfilepath = (Model.QualityAlertFilePath != null) ? Model.QualityAlertFilePath : "";%>
    <%= Html.HiddenFor(model => model.ItemID)%> 
    <%= Html.HiddenFor(model => model.PartSetUpID)%> 
 
            
   <br /><br />
     <div style="color:#003300;">Note: In order for a file to print with the travel card it must be located in the following file share: <span style="font-weight:bold;"><%=filesavelocation%></span>

   <br /> to insure that files can be opened by other users as 
    well as being detected by the travel card printing process.
      All Files must be in PDF format. </div>
       
      
  <div style="color:Red;"><%=Model.UserMessage %></div>
<table>
 <tr><td>Quality Alert #2 Effective Start Date:<%=Html.TextBoxFor(model => model.PartSetUp.QualityAlert2StartDte, new { @id = "startdate" })%>&nbsp;(Date you want the Quality Alert to begin printing with the Travel Card.)</td></tr>
    <tr><td>Quality Alert #2 Effective End Date:<%=Html.TextBoxFor(model => model.PartSetUp.QualityAlert2EndDte, new { @id = "enddate" })%>&nbsp;(Date you want the Quality Alert to begin stop printing with the Travel Card. No end date means it will print indefinitely.)</td></tr>
       <tr><td class="style1" bgcolor="#EDEEF7" >Quality Alert #2 :<label for="file">Select File (<img src="<%=pdfImagegfilepath %> "/> PDF only):</label>
    <input type="file" name="QualityAlertFilePath" id="QualityAlertFilePath" accept="application/pdf"/>(Must always reselect the document, unless you are removing it.)</td></tr>
    <tr><td><%if (alertfilepath.Length > 0)
       {%>
       Delete link to current file <%=Html.CheckBoxFor(a => a.QualityAlertFileDelete)%> (Click link to View File). <a href="<%="file:///"+alertfilepath%>"target="_blank" ><%=alertfilepath%></a>
        
      <% }%>
       </td> </tr>
</table>
 <input type="submit" class="button" value="Save" />
		
			<a href="javascript:history.go(-1)">
Cancel
</a>		

<%} %>




	
</asp:Content>