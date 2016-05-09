<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Quality.ViewModels.PartSetUpViewModel>"  %>



<div id="partsetupdetails" style="width:100%;">
 <%int setupid = Model.PartSetUp.PartSetUpID;
     string partsetupid = (Model.PartSetUp.PartSetUpID != null) ? Model.PartSetUp.PartSetUpID.ToString() : "";%>
   	<div class='pageheaders'>Part Set Up Details for <%=Model.PartSetUp.PartID %></div><br />
   <span class='container'> 
   <%if (Model.CanUserEdit)
     {%>
     <br /><br />
  <%=Html.ActionLink("Edit Part Set Up", "PartSetUpEdit", "PartSetUp", new { PartSetUpID = setupid }, new { @class = "button" })%>
 
   <%} %>
   
    </span></div>
    
    
          <%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%> 
          <%  string date = (Model.PartSetUp.CreateDate != null) ? Model.PartSetUp.CreateDate.ToString() : "";
              string createdby = (Model.PartSetUp.CreatedBy != null) ? Model.PartSetUp.CreatedBy : "";
              string revision = (Model.PartSetUp.Revision != null) ? Model.PartSetUp.Revision : "";
              string lasteditdate = (Model.PartSetUp.LastEditDate != null) ? Model.PartSetUp.LastEditDate.ToString() : "";
              string lasteditedby = (Model.PartSetUp.LastEditBy != null) ? Model.PartSetUp.LastEditBy : "";
              string drawingfilepath = (Model.PartSetUp.DrawingFile != null) ? Model.PartSetUp.DrawingFile.ToString() : "";
              string setupfilepath = (Model.PartSetUp.SetupDrawingFile != null) ? Model.PartSetUp.SetupDrawingFile.ToString() : "";
             
           
              string diesetupfilepath = (Model.PartSetUp.DieSetUpFile != null) ? Model.PartSetUp.DieSetUpFile.ToString() : "";
              string redlightgreenlightfilepath = (Model.PartSetUp.RedLightGreenLightFile != null) ? Model.PartSetUp.RedLightGreenLightFile.ToString() : "";
              string additionalfilepath = (Model.PartSetUp.AdditionalFile != null) ? Model.PartSetUp.AdditionalFile.ToString() : "";
              string lastapprover = (Model.PartSetUp.LastApprover != null) ? Model.PartSetUp.LastApprover.ToString() : "";
              string lastapproveddatetime = (Model.PartSetUp.LastApprovalDateTime != null) ? Model.PartSetUp.LastApprovalDateTime.ToString() : "";
              string communicationnote = (Model.PartSetUp.CommunicationNote != null) ? Model.PartSetUp.CommunicationNote.ToString() : "";
              string notes = (Model.PartSetUp.Notes != null) ? Model.PartSetUp.Notes : "";
              string partcomments = (Model.PartSetUp.PartComment != null) ? Model.PartSetUp.PartComment : "";
              string partremark = (Model.PartSetUp.PartRemarks != null) ? Model.PartSetUp.PartRemarks : "";
              string drawingnumber = (Model.PartSetUp.DrawingNumber != null) ? Model.PartSetUp.DrawingNumber : "";
              string packcode = (Model.PartSetUp.PackCode != null) ? Model.PartSetUp.PackCode : "";
              string deviationfilepath = (Model.PartSetUp.DeviationFile != null) ? Model.PartSetUp.DeviationFile.ToString() : "";
              string deviationfilestartdate=(Model.PartSetUp.DeviationFileStartDte!=null)?Model.PartSetUp.DeviationFileStartDte.ToString():"Not Set";
              string deviationfileenddate = (Model.PartSetUp.DeviationFileEndDte != null) ? Model.PartSetUp.DeviationFileEndDte.ToString() : "Not Set";
              
              //NOTE: deviation file number 2, requirments were for no more than 2 deviations. File attachments need to be in a seperate table in the database.
              string deviationfile2path = (Model.PartSetUp.DeviationFile2 != null) ? Model.PartSetUp.DeviationFile2.ToString() : "";
              string deviationfile2startdate = (Model.PartSetUp.DeviationFile2StartDte != null) ? Model.PartSetUp.DeviationFile2StartDte.ToString() : "Not Set";
              string deviationfile2enddate = (Model.PartSetUp.DeviationFile2EndDte != null) ? Model.PartSetUp.DeviationFile2EndDte.ToString() : "Not Set";
              
              //Note only Quality alerts file are allowed
              string qualityalertfilepath = (Model.PartSetUp.QualityAlertFile != null) ? Model.PartSetUp.QualityAlertFile.ToString() : "";
              string qualityalert1startdate = (Model.PartSetUp.QualityAlertStartDte != null) ? Model.PartSetUp.QualityAlertStartDte.ToString() : "Not Set";
              string qualityalert1enddate = (Model.PartSetUp.QualityAlertEndDte != null) ? Model.PartSetUp.QualityAlertEndDte.ToString() : "Not Set";
              //Note only Quality alerts file are allowed
              string qualityalert2filepath = (Model.PartSetUp.QualityAlert2 != null) ? Model.PartSetUp.QualityAlert2.ToString() : "";
              string qualityalert2startdate = (Model.PartSetUp.QualityAlert2StartDte != null) ? Model.PartSetUp.QualityAlert2StartDte.ToString() : "Not Set";
              string qualityalert2enddate = (Model.PartSetUp.QualityAlert2EndDte != null) ? Model.PartSetUp.QualityAlert2EndDte.ToString() : "Not Set";
              
              
              string category = (Model.PartCategory != null) ? Model.PartCategory.ToString() : "";
              bool qualityalert = Model.PartSetUp.HasQualityAlert;
              bool isrealeaseready = Model.PartSetUp.IsReleaseReady;
              bool usercanedit = Model.CanUserEdit;

             %>
         
	     <table id="CreateInitialSetup" class="standard-table" style="width:90%;">
          
      
         <tr><td>Part Set Up ID:</td><td><%=Model.PartSetUp.PartSetUpID.ToString() %></td></tr>
      
         <tr><td>Part Category: </td><td><%=category%></td></tr>
          <tr><td>PackCode: </td><td><%=packcode%></td></tr>
          <tr><td>Revision: </td><td><%=revision%></td></tr>
            <tr><td>Drawing Number: </td><td><%= drawingnumber%></td></tr>
        <%   if(usercanedit)
           {%>
           <tr style="background-color:#F2FCEB"><td colspan="2"><span class="pageheaders">Linked Deviation Files (No more than two are permitted)</span></td></tr>
           <tr style="background-color:#F2FCEB"><td style="background-color:#F0F078"; ><%=Html.ActionLink("Link/Unlink 1st Deviation File", "GetDeviationFile", "PartSetUp", new { partsetupid_ = partsetupid }, null)%></td><td style="background-color:#F0F078"; ><a href="<%="file:///"+deviationfilepath%>"target="_blank" ><%=deviationfilepath%></a>&nbsp;  &nbsp;&nbsp;<span style="padding:10px,10px,10px,10px;"></span><br /><br /><b>Effective Start Date:</b> &nbsp; <%=deviationfilestartdate %>&nbsp;&nbsp; <b>Effective End Date:</b> &nbsp; <%=deviationfileenddate %> </td></tr>
         <tr style="background-color:#F2FCEB" ><td style="background-color:#F0F078";><%=Html.ActionLink("Link/Unlink 2nd Deviation File", "GetDeviationFile2", "PartSetUp", new { partsetupid_ = partsetupid }, null)%></td><td style="background-color:#F0F078";><a href="<%="file:///"+deviationfile2path%>"target="_blank" ><%=deviationfile2path%></a>&nbsp;  &nbsp;&nbsp;<span style="padding:10px,10px,10px,10px;"></span><br /><br /><b>Effective Start Date:</b> &nbsp; <%=deviationfile2startdate %>&nbsp;&nbsp; <b>Effective End Date:</b> &nbsp; <%=deviationfile2enddate %> </td></tr>
         <tr style="background-color:#F2FCEB"><td colspan="2"><span class="pageheaders">Linked Quality Alert Files (No more than two are permitted)</span></td></tr>
         <tr style="background-color:#F2FCEB"><td style="background-color:#F0F078"; ><%=Html.ActionLink("Link/Unlink Quality Alert #1", "GetQualityAlertFile", "PartSetUp", new { partsetupid_ = partsetupid }, null)%></td><td style="background-color:#F0F078";><a href="<%="file:///"+qualityalertfilepath%>"target="_blank" ><%=qualityalertfilepath%></a>&nbsp;  &nbsp;&nbsp;<span style="padding:10px,10px,10px,10px;"></span><br /><br /><b>Effective Start Date:</b> &nbsp; <%=qualityalert1startdate %>&nbsp;&nbsp; <b>Effective End Date:</b> &nbsp; <%=qualityalert1enddate %> </td></tr>
          <tr style="background-color:#F2FCEB"><td style="background-color:#F0F078"; ><%=Html.ActionLink("Link/Unlink Quality Alert #2", "GetQualityAlertFile2", "PartSetUp", new { partsetupid_ = partsetupid }, null)%></td><td style="background-color:#F0F078";><a href="<%="file:///"+qualityalert2filepath%>"target="_blank" ><%=qualityalert2filepath%></a>&nbsp;  &nbsp;&nbsp;<span style="padding:10px,10px,10px,10px;"></span><br /><br /><b>Effective Start Date:</b> &nbsp; <%=qualityalert2startdate %>&nbsp;&nbsp; <b>Effective End Date:</b> &nbsp; <%=qualityalert2enddate %> </td></tr>
          <tr style="background-color:#FD750F"><td colspan="2"><span class="pageheaders">Other Linked Files</span></td></tr>
          <tr style="background-color:#F2FCEB"><td class="style1" ><%=Html.ActionLink("Link/Unlink Machine Set Up Instructions", "GetSetUpInstructionsFile", "PartSetUp", new { partsetupid_ = partsetupid }, null)%></td><td ><a href="<%="file:///"+setupfilepath%>"target="_blank" ><%=setupfilepath%></a>&nbsp;  &nbsp;&nbsp;<span style="padding:10px,10px,10px,10px;"></span><br /><br /></td></tr>
              <tr style="background-color:#F2FCEB" ><td class="style1" ><%=Html.ActionLink("Link/Unlink Drawing File", "GetDrawingFile", "PartSetUp", new { partsetupid_ = partsetupid }, null)%></td><td ><a href="<%="file:///"+drawingfilepath%>"target="_blank" ><%=drawingfilepath%></a>&nbsp;  &nbsp;&nbsp;<span style="padding:10px,10px,10px,10px;"></span><br /><br /></td></tr>
           <tr style="background-color:#F2FCEB"><td class="style1" ><%=Html.ActionLink("Link/Unlink Die Set Up File", "GetDieSetUpFile", "PartSetUp", new { partsetupid_ = partsetupid }, null)%></td><td ><a href="<%="file:///"+diesetupfilepath%>"target="_blank" ><%=diesetupfilepath%></a>&nbsp;  &nbsp;&nbsp;<span style="padding:10px,10px,10px,10px;"></span><br /><br /></td></tr>
            <tr style="background-color:#F2FCEB" ><td class="style1" ><%=Html.ActionLink("Link/Unlink Green Light Red Light", "GetGreenLightRedLightFile", "PartSetUp", new { partsetupid_ = partsetupid }, null)%></td><td ><a href="<%="file:///"+redlightgreenlightfilepath%>"target="_blank" ><%=redlightgreenlightfilepath%></a>&nbsp;  &nbsp;&nbsp;<span style="padding:10px,10px,10px,10px;"></span><br /><br /></td></tr>
              <tr style="background-color:#F2FCEB" ><td class="style1" ><%=Html.ActionLink("Link/Unlink Additional(Misc.) PDF file", "GetAdditionalFile", "PartSetUp", new { partsetupid_ = partsetupid }, null)%></td><td ><a href="<%="file:///"+additionalfilepath%>"target="_blank" ><%=additionalfilepath%></a>&nbsp;  &nbsp;&nbsp;<span style="padding:10px,10px,10px,10px;"></span><br /><br /></td></tr>
       <%}
         else{%>
             <tr><td class="style1" >Machine Set Up Instructions</td></tr>   
              <tr><td class="style1">Drawing File</td></tr>   
                <tr><td class="style1">Deviaiton File</td></tr> 
                   <tr><td class="style1">Die Set Up File</td></tr>   
                     <tr><td class="style1">Quality Alert</td></tr>
                          <tr><td class="style1">Red Light Green Light File</td></tr>  
                        
           <%} %>  
       
      
	
     <tr><td class="style1" >Has Quality Alert :</td><td><%=qualityalert%> </td></tr>

         <tr><td>Is Release Ready :</td><td><%=isrealeaseready%></td></tr>
           <tr><td>Last Approved By :</td><td><%=lastapprover%></td></tr>
           <tr><td>Last Approved By :</td><td><%=lastapproveddatetime%></td></tr>
         <tr><td>Initially Created By: </td><td><%= Html.Label(createdby)%></td></tr>
         <tr><td>Initial Create Date: </td><td><%= Html.Label(date)%></td></tr>
         <tr><td>Last Edited By: </td><td><%= Html.Label(lasteditedby)%></td></tr>
         <tr><td>Last Edit Date: </td><td><%= Html.Label(lasteditdate)%></td></tr>
         <tr><td>Part Comment: </td><td><%=partcomments%></td></tr>
         <tr><td>Part Remarks: </td><td><%=partremark%></td></tr>
         <tr><td><%=Html.LabelFor(a=>a.PartSetUp.CommunicationNote) %></td><td><%=communicationnote%></td></tr>
        <tr><td><%=Html.LabelFor(a=>a.PartSetUp.Notes) %>:</td>
	 <td><%if (Model.PartSetUp.Notes != "")
        {%>
			  <%string notepadimagefilepath = ConfigurationManager.AppSettings["NotePaDImageFilePath"];%>
              <a href='#'id="tooltip" title='<%=notes%>'><img src="<%=notepadimagefilepath %>" style="border-style: none" /></a></td></tr>
			   <%}
        else
        {%><td>&nbsp;</td>
   <tr>
				<td colspan="2"">
                <%if(Model.CanUserEdit)
                  { %>
             
				<span style="padding:10px,10px,10px,10px;"><%=Html.ActionLink("Edit Part Set Up", "PartSetUpEdit", "PartSetUp", new { PartSetUpID = partsetupid }, null)%></span><br />

              
               
               

			<%} %>
				</td>
			</tr>
     </table>
bb

<%}
        %>





