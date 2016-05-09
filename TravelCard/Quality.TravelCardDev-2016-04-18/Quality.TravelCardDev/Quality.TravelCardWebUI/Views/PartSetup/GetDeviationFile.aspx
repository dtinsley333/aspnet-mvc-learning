
<%@ Page Title="Link to Deviation File" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSetUpViewModel>"  %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
Link to Die Set Up File
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
 
    <div class='pageheaders' style="width:60%;" >Link Deviation File to <%=Model.ItemID%> Part Set Up</div><br />


    <% using (Html.BeginForm("GetDeviationFile", "PartSetUp"))
		{ %>
   <%string pdfImagegfilepath = ConfigurationManager.AppSettings["PDFImageFilePath"];%>
   <%string filesavelocation = ConfigurationManager.AppSettings["DeviationFile"]; %>
   <% string deviationfilepath = (Model.DeviationFilePath != null) ? Model.DeviationFilePath : "";%>
    <%= Html.HiddenFor(model => model.ItemID)%> 
    <%= Html.HiddenFor(model => model.PartSetUpID)%> 

            
   <br /><br />
  <div style="color:Green;">Note: In order for a file to print with the travel card it must be located in the following file share: <span style="font-weight:bold;"><%=filesavelocation %></span>
 <br /> to insure that files can be opened by other users as 
    well as being detected by the travel card printing process.
      All Files must be in PDF format. </div>

 


 
      
  <div style="color:Red;"><%=Model.UserMessage %></div>
<table>
    <tr><td>Deviation Effective Start Date:<%=Html.TextBoxFor(model => model.PartSetUp.DeviationFileStartDte, new { @id = "startdate" })%>&nbsp;(Date the deviation file is to  start printing with the Travel Card.)</td></tr>
    <tr><td>Deviation Effective End Date:<%=Html.TextBoxFor(model => model.PartSetUp.DeviationFileEndDte, new { @id = "enddate" })%>&nbsp;(Date the deviation file is to stop printing with the Travel Card. Leaving blank means no end.)</td></tr>
    <tr><td class="style1" bgcolor="#EDEEF7" >Deviation File? :<label for="file">Select File (<img src="<%=pdfImagegfilepath %> "/> PDF only):</label>
    <input type="file" name="DeviationFilePath" id="DeviationFilePath" accept="file/pdf"/>(Must always reselect the document, unless you are removing it.)</tr>
    <tr><td><%if(deviationfilepath.Length>0)
       {%>
       Delete link to current file <%=Html.CheckBoxFor(a => a.DeviationFileDelete)%> (Click link to View File). <a href="<%="file:///"+deviationfilepath%>"target="_blank" ><%=deviationfilepath%></a>
     
      <% }%>
        </tr>
</table>
 <input type="submit" class="button" value="Save" />
			<a href="javascript:history.go(-1)">
Cancel
</a>



<%} %>




	
</asp:Content>
