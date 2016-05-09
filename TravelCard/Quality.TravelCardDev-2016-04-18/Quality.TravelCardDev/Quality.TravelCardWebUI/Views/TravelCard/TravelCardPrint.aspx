
    <%@ Page Title="Print Travel Card-English" Language="C#" MasterPageFile="~/Views/Shared/TravelCard.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.TravelCardPrintViewModel>"  %>
    

<asp:Content ID="TravelCardTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Print Travel Card
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server" >


        
       
	
       
    <style type="text/css">

  
       
table 
{
  border: solid 1px #000000;
  border-collapse: collapse;
  font-size:7px;
  font-family: Verdana, Helvetica, Sans-Serif;
}

table td 
{
  padding: 1px;   
  border: solid 1px #000000;
  font-size:7px;
}

table th
{
  padding: 1px 1px;
  text-align: left;
   border: solid 1px #000000; 
   font-family: Verdana, Helvetica, Sans-Serif;
   
}

.ampm
{
   text-align: right;
   font-size:5px;   

}
.timestamp
{
    
 font-size:5px;
 position:absolute; 
 top:5px;  
    
}
#watermark {  color: #d0d0d0; 
                font-size: 60pt; 
                  -webkit-transform: rotate(-45deg);
                     -moz-transform: rotate(-45deg);
                        position: absolute;
                           width: 50%;
                              height: 10%; 
                              right:573px;
                              top:175px;
                                margin: 81px 10 149px 10;
                                   z-index: -1; 
                                    
                                         } 

.FrontContent {
	width:950px;
	margin:1px auto;
	

	}
	
	#draft {  color: #d0d0d0; 
                font-size: 100pt; 
                  -webkit-transform: rotate(-45deg);
                     -moz-transform: rotate(-45deg);
                        position: absolute;
                           width: 35%;
                              height: 1%; 
                              left:567px;
                              top:400px;
                                margin: 0px 10 31px 10;
                                   z-index: -1; 
                                    
                                         } 

.FrontContent {
	width:1016px;
	margin:2px auto;
	right:2px;
	

	}

.TCplaintable
{
    padding:0px 1px 0px 1px;
  text-align: left;

  border: solid 1px #000000;   
   font-size:9px; 
   font-family: Verdana, Helvetica, Sans-Serif;

    }
    

       
        .style1
        {
            width: 68px;
        }
        .style2
        {
            width: 126px;
        }
        .style5
        {
            width: 315px;
        }
    

       
    </style>

    

    
</asp:Content>

<asp:Content ID="FRONTCONTENT" ContentPlaceHolderID="FrontContent" runat="server"  >
<span id='FRONTPAGEBEGIN'></span>
<table id="TABLEFORFRONTCONTENTS"><tr><td>
<div id="FRONTPAGECONTENTS">

    <% string partsetupid = Model.PartSetUp.PartSetUpID.ToString();%>
 <%string ideallogo = ConfigurationManager.AppSettings["ReportIdealLogo"];%>
 <%string barcodefilepath = ConfigurationManager.AppSettings["BarCodeFilePath"];%>
   
       <span id="PartID">  <%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%></span> 
<%string partnumber = Model.Part.Select(x => x.Id).FirstOrDefault();
  partnumber = partnumber.Trim();
 
string tcbarcodepath="";
 
    %> 
    <%string plant = Model.Plant.PlantCode.ToString();%>
    <%bool isdraft=Model.IsDraft; %>
    <%int? opcodenum = Model.OpCode;%>


 
    <%
      int numtoprint = Model.NumContinuationTCs;%>
 <%= Html.HiddenFor(model => model.NumContinuationTCs)%>

   <%  int partsetup=Convert.ToInt16(partsetupid);
   %>
   <%if (Model.ContinuationBarCodeText == null)
     {
      
         %>
   <% Html.RenderAction("TCBarCodeDisplay", "BarCode", new { opcode = opcodenum, thispartsetupid = partsetup, isdraft = isdraft, iscontinuationcard = false, id = plant + "-" + partsetupid, showText = true, thickness = 1, height = 60 }); %> 

<%}
     else
     {
         string barcodestring = Model.ContinuationBarCodeText;  
         %>
    <% Html.RenderAction("TCContinuationBarCodeDisplay", "BarCode", new { barcodetext=barcodestring, opcode = opcodenum, thispartsetupid = partsetup, isdraft = isdraft, iscontinuationcard = true, id = plant + "-" + partsetupid, showText = true, thickness = 1, height = 60 }); %> 
       
<%} %>
         
   <% Html.RenderAction("BarCodeDisplay", "BarCode", new { id =partnumber, showText = true, thickness = 1, height = 60 }); %> 
      <%tcbarcodepath =Convert.ToString(Session["tcbarcodepath"]);%>     
   
     
   <%string categorydescription;

     if (Model.UsersCulture.Name == "es-MX")
     {
         categorydescription = Model.PartSetUp.PartCategory.CategoryNameES;
     }
     else {
         categorydescription = Model.PartSetUp.PartCategory.CategoryName;
           }
       
       %>     
         <%if(Model.IsDraft)
        { %>
          <span id="draft"><%=Quality.Resources.Strings.Draft %></span> 
       <%  }%>

<div class="FrontContent" id="CONTENTONFRONT">
	
    <table id="MYSTERYTABLE">

       <tr><td class="style1">
      
        <table class='TCplaintable' id="TITLETABLE" style="table-layout:auto; width:500px;border:1;border-color:Black;" >
        <%int thispartsetupid = Model.PartSetUp.PartSetUpID; %>
        <%string itemid = Model.PartSetUp.PartID.ToString(); %>
        <% string setupidbarcode = barcodefilepath + plant + "-" + partsetupid+".gif"; %> 
        <tr><td colspan="4">	   <span class="ideallogo"  ><a href="<%=Url.Action("BackToPartSetUp","TravelCard", new { _itemid = itemid,_partsetupid=thispartsetupid }, null) %>" ><img src="<%=ideallogo%> "
         style="text-decoration: none;    
    
                	                                                                                                                                                                 	                                                                                                
 	border: 0 none;" alt="Back to Part Set Up" /></a></span><br/>
   
    <span id="logo" style="font-weight:bold;"><%=categorydescription%>&nbsp;<%=Quality.Resources.Strings.TravelCard %></span><span id="parsetup">&nbsp;&nbsp;<img src="<%=tcbarcodepath%>" />&nbsp;&nbsp;</span></td></tr>

                                                                                                                                                                                                                                             
           <tr> <th ><%=Quality.Resources.Strings.PartNumber %>#</th>
                <th ><%=Quality.Resources.Strings.PackCodeNumber %>#</th>
            <th><%=Quality.Resources.Strings.Revision %></th>
                <th><%=Quality.Resources.Strings.DrawingNumber %></th></tr>
       <tr><td ><span id="partID" style="font-weight:bold;"><%=Model.PartSetUp.PartID %>&nbsp;&nbsp;</span></td>  
           <td style="font-size:11pt;font:weight:bold;"><%=Model.PartSetUp.PackCode %></td>  <td style="font-size:11pt;font:weight:bold;">&nbsp;<%=Model.PartSetUp.Revision %></td>
           <td style="font-size:11pt;font:weight:bold;">&nbsp;<%=Model.PartSetUp.DrawingNumber %></td></tr>      
         
       </table></td><td class="style5"><div id="SUPPLIERCERTIFICATION" style="width:500px;"> 
                     <span style="font-weight:bold; font-size: x-small;"> <%=Quality.Resources.Strings.SupplierCertification %></span></div>
                <span id="suppliername"><%=Quality.Resources.Strings.Supplier %>: ________________</span> <span id="ponum"><%=Quality.Resources.Strings.PONum %>: _________________</span><br /><br />
                <span id="duedate"><%=Quality.Resources.Strings.DueDate %>: __________________</span> <span id="PartNumber"><%=Quality.Resources.Strings.PartNumber %>:<span id='partnum'> <%=Model.PartSetUp.PartID.ToString() %></span></span><br /><br />
                <span id="quantity"><%=Quality.Resources.Strings.Quantity %>: _____________</span> <span id="Weight"> <%=Quality.Resources.Strings.Weight %>: _________________</span><br /><br />
                <span id="Span1"><%=Quality.Resources.Strings.PickUpDriver %>: __________________________________</span><br /><br /></td></tr>
    
   
     <tr><td>

     
     <%string partcategory = Model.PartSetUp.PartCategory.CategoryName.ToLower(); %>
     <%if (partcategory == "carton")
       {%>
           
          <table id="CARTONINFO"class="TCplaintable" 
                style="table-layout:fixed; width:500px;">
          <tr><th>Assembly WO#</th><th >Quantity</th><th>Deviation</th></tr>
          <tr><td height="20"></td><td></td><td></td></tr>
          <tr><td height="20" ></td><td></td><td></td></tr>
          <tr><td height="20"></td><td></td><td></td></tr>
          <tr><td height="20"></td><td></td><td></td></tr>
          <tr><td height="20" ></td><td></td><td></td></tr>
          <tr><td height="20"></td><td></td><td></td></tr>
          <tr><td height="20"></td><td></td><td></td></tr>
      </table>  
           

           <%}
       else
       { %>
         <table id="RAWMATERIALS"class="TCplaintable" 
                style="table-layout:fixed; width:500px;">
      <tr><th><%=Quality.Resources.Strings.Supplier%></th>
          <th ><%=Quality.Resources.Strings.AssemblyWONumber%>:</th>
          <th><%=Quality.Resources.Strings.Quantity%></th></tr>
      <tr><td height="23"><%=Quality.Resources.Strings.Heat%> #</td><td>W O #</td>
          <td height="23"></td></tr>
      <tr><td height="23"><%=Quality.Resources.Strings.Coil%> #</td><td >W O #</td>
          <td height="23"></td></tr>
       <tr><td height="23"><%=Quality.Resources.Strings.Heat%> #</td><td>W O #</td>
           <td height="23"></td></tr>
      <tr><td height="23" ><%=Quality.Resources.Strings.Coil%> #</td><td>W O #</td>
          <td height="23" ></td></tr>
       <tr><td height="23" ><%=Quality.Resources.Strings.RawMaterial%> #</td><td></td>
           <td height="23"></td></tr>
  <tr><td colspan="3" height="23"><span style="font-weight:bold;"><%=Quality.Resources.Strings.Comments %>:&nbsp;&nbsp;<%=Model.PartSetUp.PartComment %></span></td>           </tr>
      </table></td>
      <%} %>
      <td rowspan="3" id="RIGHTCONTAINER" class="style5">
     
     <table class='TCplaintable' id="ADDITIONALPROCESSES" style="table-layout:fixed; width:500px;">
     <thead><tr><th colspan="4"><%=Quality.Resources.Strings.AdditionalProcesses %>&nbsp;&nbsp; 
     <%if (Model.PartSetUp.PartCategory.CategoryName == "Clamp")
       {%>
       <span style="font-weight:bold;">(If Clamp Please Verify By Checking)</span>
       
    <%   }        %>
     
     
     </th></tr></thead>
     <tbody>
      <% if (Model.AdditionalProcesses == null)
         {%>
			
			<tr ><td rowspan="2" height="20"> No additional processes were found for this item</td></tr>
			  
			  <%}
         else
         {      %> 


				 <% foreach (var process in Model.AdditionalProcesses.OrderBy(a=>a.SequenceID))
        {            
	
				%>
			 <tr >
            <% if (Model.UsersCulture.Name == "es-MX")
               {%>
			 <td style='padding:3,3,3,3;'><%=process.DescriptionES%></td> 
             <%} %>

              <% if (Model.UsersCulture.Name == "en-US")
               {%>
			 <td><%=process.Description.ToUpper()%></td> 
             <%} %>
			 <td></td> 
			 <td></td> 
			 <td></td> 
			 
			 </tr>          

			 <%}
         }%>
<tr><td height="15px" colspan='4'style="font-weight:bold;"><%=Quality.Resources.Strings.Comments %>:&nbsp;</td></tr>
     </tbody>
     </table>

      </td></tr>
      
    <tr><td>
  <%if (partcategory != "carton")
    {%>
      <table id='CARTONLIST' class='TCplaintable' style="table-layout:fixed; width:500px;">
			<thead>
			<tr>
				  
                  <th width="30%"><%=Quality.Resources.Strings.Description%></th>
                   <th><%=Quality.Resources.Strings.ComponentPart%></th>
					<th><%=Quality.Resources.Strings.ComponentOMI%> #</th>
					<th ><%=Quality.Resources.Strings.BarCodeNumber%></th>
                    <th><%=Quality.Resources.Strings.ComponentHeat%> #</th>
					
			</tr></thead>
			<tbody>
		   <% if (Model.Component.Count() < 1)
        {%>
			
		 <span style="color:Red;"> No BOM components were found for <%=Model.PartSetUp.PartID%> at the <%=Model.Plant.PlantName + " Facility."%> </span>
			  
			  <%} %> 
				 <% foreach (var component in Model.Component)
        {            
	
				%>
			 <tr>
             <td><span style="font-size:8pt;"><%=component.ITMDESC%></span></td>
			 <td><span style="font-size:8pt;"><%=component.Id%></span></td> 
			 <td></td> 
			 <td></td> 
			 <td></td> 
			 
			 </tr>          
            
			 <%}
    }
    else
    {%>
   
    <table id='COMPONENTLIST' class='TCplaintable' style="table-layout:fixed; width:500px;">
			<thead>
			<tr>
				   <th><%=Quality.Resources.Strings.ComponentPart%></th>
					<th><%=Quality.Resources.Strings.Description%></th>
					<th><%=Quality.Resources.Strings.ComponentOMI%> #</th>
					<th><%=Quality.Resources.Strings.AssemblyWONumber%> #</th>
				
					
			</tr></thead>
			<tbody>
		   <% if (Model.Component.Count() < 1)
        {%>
			
		  No BOM components were found for <%=Model.PartSetUp.PartID%> at the <%=Model.Plant.PlantName + " Facility."%>
			  
			  <%} %> 
				 <% foreach (var component in Model.Component)
        {            
	
				%>
			 <tr>
			 <td><%=component.Id%></td> 
			 <td><%=component.ITMDESC%></td>
			 <td></td> 
			 <td></td> 
		
			 
			 </tr>          
    
   <% 
      }%>
	<tr height="25" >
			 <td></td> 
			 <td></td>
			 <td></td> 
			 <td></td> 
		
			 
			 </tr>   
             
     <tr height="25" >
			 <td></td> 
			 <td></td>
			 <td></td> 
			 <td></td> 
		
			 
			 </tr>         	 
</tbody>

</table>   </table>

<table class="TCplaintable" id="ALTTABLE" style="table-layout:fixed; width:500px;">
 
<tr><td>Alternate Process:</td></tr></table>
<%string category = Model.PartSetUp.PartCategory.CategoryName.ToLower();%>
<%if (category == "carton")
  { %>
<table class="TCplaintable" style="table-layout:fixed; width:500px;">
<tr><th>Date</th><th>Station</th><th>Quantity</th><th>Operator</th></tr>
<tr><td height="25"></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td></tr>

  <%}
  else
  {%>
<table class="TCplaintable" id="TOTE" style="table-layout:fixed; width:500px;">
<tr><th><%=Quality.Resources.Strings.Tote%></th><th><%=Quality.Resources.Strings.Date%></th><th><%=Quality.Resources.Strings.Machine%> #</th><th><%=Quality.Resources.Strings.Quantity%></th><th><%=Quality.Resources.Strings.Operator%></th></tr>
<tr><td height="25"></td><td></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td><td></td></tr>
<tr><td height="25"></td><td></td><td></td><td></td><td></td></tr>
<%}
    }%>

<tr><td colspan="5"><table id="OPERATOR"class="TCplaintable" 
                style="table-layout:fixed; width:500px;">
              <tr><th><%=Quality.Resources.Strings.Machine+ "#"%></th><th><%=Quality.Resources.Strings.Date%></th><th ><%=Quality.Resources.Strings.Operator+"#" %></th><th><%=Quality.Resources.Strings.Quantity+ "#" %></th><th><%=Quality.Resources.Strings.Date%></th><th ><%=Quality.Resources.Strings.Operator+"#" %></th><th><%=Quality.Resources.Strings.Quantity+ "#" %></tr>
              <tr><td height="20"></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
              <tr><td height="20"></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
              <tr><td height="20"></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
              <tr><td height="20"></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
              <tr><td height="20"></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
              <tr><td height="20" colspan="4"></td><td colspan="3"><%=Quality.Resources.Strings.Total %>#</td></tr>
          
                       
      </table> 

<div  id="AUDIT"style="Padding:1,1,1,1;height:50px;" ><table border="1px"><tr><td><%=Quality.Resources.Strings.InspectionSignOff %>.<br /><br />____________<br /><br /><br /></td>
<td><%=Quality.Resources.Strings.IfRejected %>____________</td><td><%=Quality.Resources.Strings.IfAudited %></td></tr></table></div>

</table>
</table>
</table>
  
   
 
  
 </div>
  
</td> </tr></table>

<span id="BREAKFIRSTPAGE" style='page-break-before:always;'>&nbsp;</span>
    </asp:Content>
<asp:Content ID="BackContent" ContentPlaceHolderID="BackContent" runat="server" >
    <div class="BackContent"> 
   <span id="watermark" style="top:840px;"><%=Model.PartSetUp.CommunicationNote %></span> 
<table class='TCplaintable' style="table-layout:auto;">
<%string partid = Model.PartSetUp.PartID; %>
   <%int speccount=0; %>  
   <%string omiID = "";%>
    <%omiID =Convert.ToString(Session["barcodetext"]);%>  
    <%string op_code = "";
     op_code=(Model.OpCode != null ? Model.OpCode.ToString() : "All"); %>
     <thead><tr><th colspan="14"><%=partid%> - <%=Quality.Resources.Strings.TestCriteria %>&nbsp;&nbsp;Travel Card# <%=omiID%>&nbsp;&nbsp;<%=Quality.Resources.Strings.OperationCode.ToString() %>: <%=op_code%><span id='ccMessage'></span><span>&nbsp; PAGE 1</span></th></tr></thead>
  
     <tbody>
      <tr><td colspan="14">  <table class='TCplaintable' style="width:100%;">  
       <tr><td style="font-weight:bold;"></td></tr>
      <tr><td style="height:30px;"><%=Quality.Resources.Strings.Remarks%>:&nbsp;<%=Model.PartSetUp.PartRemarks %></td></tr>
    </table></td></tr>
     <tr style="font-weight:bold;height:25px;">
			 <td><%=Quality.Resources.Strings.Characteristic %></td> 
			 <td><%=Quality.Resources.Strings.MeasurmentTool %></td> 
                      <td><%=Quality.Resources.Strings.LowSpec %></td> 
             <td><%=Quality.Resources.Strings.HighSpec %></td> 
             <td><%=Quality.Resources.Strings.Frequency %></td> 
             <td><%=Quality.Resources.Strings.SampleSz %></td> 
             <%string ampm = Quality.Resources.Strings.AMPM; %>
			 <td>Set Up &nbsp;<span class="ampm"><br /><%=Quality.Resources.Strings.Frequency %>&nbsp;</span></td> 
			 <td>Ins.#1 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
             <td>Ins.#2 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
             <td>Ins.#3 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
			 <td>Ins.#4 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
             <td>Ins.#5 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
             <td>&nbsp;Final</td> 
			 </tr> 
                  
      <% if (Model.PartSpecifications == null)
         {%>
			
			<tr><td rowspan="2" class="style2"> No inspection characteristics were found for this item</td></tr>
			  
			  <%}
         else
         {      %> 

        
				 <% foreach (var process in Model.PartSpecifications.Where(a => a.IsActive))
        {
            speccount++;
            if (speccount==20)
            { break; }
            var measurementmethod = Model.MeasurementMethods.FirstOrDefault(a => a.MeasurementMethodID == process.MeasurementMethodID);
            var measurementunit = Model.MeasurementUnits.FirstOrDefault(a => a.unitID == process.unitID);

            string measurementmethoddesc = (measurementmethod != null) ? measurementmethod.Description_EN.ToString().ToUpper() : "";
            string measearmentmethoddescEs = (measurementmethod != null) ? measurementmethod.Description_MX : "";
            string abbreviation = (measurementunit != null) ? measurementunit.Abbreviation : "";
            var frequency = Model.Frequencies.FirstOrDefault(a => a.FrequencyID == process.FrequencyID);
            string frequencydesc = (frequency != null) ? frequency.Description_EN.ToString().ToUpper() : "";
            string frequencydescEs = (frequency != null) ? frequency.Description_MX : "";
            string sequenceorder = process.SequenceID != null ? process.SequenceID + "-" : "";
         
            string precedingletter = "";
           
            
            if (process.SequenceID != null)
            {
              
                
                if (process.SequenceID.IndexOf('0') == 0)
                {

                    string formattedsequencid = process.SequenceID.Remove(0, 1);
                  var letterorder = Model.PartSpecificationSequences.Where(a => a.SequenceOrder.ToString() == formattedsequencid);
                    precedingletter = letterorder.FirstOrDefault(a => Convert.ToString(a.SequenceOrder) == formattedsequencid).SequenceText+" - ";
                }

                int index;
                bool result = Int32.TryParse(process.SequenceID, out index);
                if (process.SequenceID.IndexOf('0') != 0 && result)
                {
                    string formattedsequencid = process.SequenceID.ToString();
                    var letterorder = Model.PartSpecificationSequences.Where(a => a.SequenceOrder.ToString() == formattedsequencid);
                    precedingletter = letterorder.FirstOrDefault(a => Convert.ToString(a.SequenceOrder) == formattedsequencid).SequenceText + " - ";
                
                
                }


            }
		
            	%>
			 <tr>
             <%if (Model.UsersCulture.Name == "en-US")
               {%>
			
             <td style="height:32px;" ><b><%=precedingletter%></b>&nbsp;<%=process.Characteristic.ToUpper()%></td> 
             <td style="height:32px;"><%=measurementmethoddesc%></td> 
            
             <%} %>
              <%if (Model.UsersCulture.Name == "es-MX")
                {%>
			 <td style="height:32px;" ><b><%=precedingletter%></b>&nbsp;<%=process.CharacteristicES%></td> 
                 <td style="height:32px;"><%=measearmentmethoddescEs%></td> 
               
             <%} %>

			 
            
             <td style="height:32px;"><%=process.LowSpec%>&nbsp;<%=abbreviation%></td> 
             <td style="height:32px;"><%=process.HighSpec%>&nbsp;<%=abbreviation%></td> 
             <%if (Model.UsersCulture.Name == "en-US")
               { %>
             <td style="height:32px;"><%=frequencydesc%></td> 
              <td style="height:32px;" >&nbsp;&nbsp;<%=process.SampleSize%></td> 
             <%} %>
               <%if (Model.UsersCulture.Name == "es-MX")
                 { %>
             <td style="height:32px;"><%=frequencydescEs%></td> 
             <td style="height:32px;"><%=process.SampleSizeES%></td> 
             <%} %>
            
			 <td style="height:32px;"></td> 
			 <td style="height:32px;"></td> 
             <td style="height:32px;"></td> 
             <td style="height:32px;"></td> 
			 <td style="height:32px;"></td> 
             <td style="height:32px;"></td> 
             <td style="height:32px;"></td> 
            
         
     <%   }
         }%>
			 </tr>          


<tr><td colspan="14"> <%DateTime thedate=DateTime.Now; %>
 <%string timestamp = thedate.ToShortDateString(); %>
 <%string username = Model.UserSetting.UserName; %>
 <%timestamp = "Printed:" +thedate +". By "+ username; %>
 <span class="ampm"><%=timestamp %>&nbsp;Number printed:&nbsp;  <span id="Span4"><%=Model.NumContinuationTCs.ToString() %></span> </span></td></tr>

    
     </tbody>
     </table>


  
<span id='FRONTPAGEEND'></span>
     
     <%if (Model.PartSetUp.PartSpecifications.Where(a => a.IsActive).Count() > 19)
       {%>
<span style="page-break-before:always;">&nbsp;</span>
     <table class='TCplaintable' style="table-layout:auto;">  
       
      <%string partid2 = Model.PartSetUp.PartID; %>
   <%int speccount2=0; %>  
   <%string omiID2 = "";%>
    <%omiID2 =Convert.ToString(Session["barcodetext"]);%>  
    <%string op_code2 = "";
     op_code2=(Model.OpCode != null ? Model.OpCode.ToString() : Quality.Resources.Strings.All); %>
     <thead><tr><th colspan="14"><%=partid2%> - <%=Quality.Resources.Strings.TestCriteria %>&nbsp;&nbsp;Travel Card# <%=omiID2%>&nbsp;&nbsp;<%=Quality.Resources.Strings.OperationCode.ToString() %>: <%=op_code2%><span id='ccMessage2'></span><span>&nbsp;PAGE 2</span></th></tr></thead>
      
    
   
     <tbody style="font-size:6pt">
      <tr><td colspan="14">  <table class='TCplaintable' style="width:100%;">  
       <tr><td style="font-weight:bold;"></td></tr>
      <tr><td style="height:30px;"><%=Quality.Resources.Strings.Remarks%>:&nbsp;<%=Model.PartSetUp.PartRemarks %></td></tr>
    </table></td></tr>
     <tr style="font-weight:bold;height:25px;">
			 <td><%=Quality.Resources.Strings.Characteristic %></td> 
			 <td><%=Quality.Resources.Strings.MeasurmentTool %></td> 
          
             <td><%=Quality.Resources.Strings.LowSpec %></td> 
             <td><%=Quality.Resources.Strings.HighSpec %></td> 
             <td><%=Quality.Resources.Strings.Frequency %></td> 
             <td><%=Quality.Resources.Strings.SampleSz %></td> 
             <%string ampm2 = Quality.Resources.Strings.AMPM; %>
			 <td>Set Up &nbsp;<span class="ampm"><br /><%=Quality.Resources.Strings.Frequency %></span></td> 
			 <td>Ins.#1 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
             <td>Ins.#2 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
             <td>Ins.#3 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
			 <td>Ins.#4 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
             <td>Ins.#5 &nbsp;<span class="ampm"><br /><%=ampm %></span></td> 
             <td>&nbsp;Final</td> 
			 </tr> 
                  
      <% if (Model.PartSpecifications != null&&Model.PartSpecifications.Count()>19)
         

        foreach (var process in Model.PartSpecifications.Where(a => a.IsActive))
        {
            speccount2++;
            if (speccount2 >= 20)
            {

                var measurementmethod = Model.MeasurementMethods.FirstOrDefault(a => a.MeasurementMethodID == process.MeasurementMethodID);
                var measurementunit = Model.MeasurementUnits.FirstOrDefault(a => a.unitID == process.unitID);

                string measurementmethoddesc = (measurementmethod != null) ? measurementmethod.Description_EN.ToString().ToUpper() : "";
                string measearmentmethoddescEs = (measurementmethod != null) ? measurementmethod.Description_MX : "";
                string abbreviation = (measurementunit != null) ? measurementunit.Abbreviation : "";
                var frequency = Model.Frequencies.FirstOrDefault(a => a.FrequencyID == process.FrequencyID);
                string frequencydesc = (frequency != null) ? frequency.Description_EN.ToString().ToUpper() : "";
                string frequencydescEs = (frequency != null) ? frequency.Description_MX : "";
                string sequenceorder = process.SequenceID != null ? process.SequenceID + "-" : "";
                var letterorder = Model.PartSpecificationSequences.Where(a => a.SequenceOrder.ToString() == process.SequenceID);
               
                string precedingletter = "";
                if (process.SequenceID != null)
                {


                    if (process.SequenceID.IndexOf('0') == 0)
                    {

                        string formattedsequencid = process.SequenceID.Remove(0, 1);
                        letterorder = Model.PartSpecificationSequences.Where(a => a.SequenceOrder.ToString() == formattedsequencid);
                        precedingletter = letterorder.FirstOrDefault(a => Convert.ToString(a.SequenceOrder) == formattedsequencid).SequenceText + " - ";
                    }

                    int index;
                    bool result = Int32.TryParse(process.SequenceID, out index);
                    if (process.SequenceID.IndexOf('0') != 0 && result)
                    {
                        string formattedsequencid = process.SequenceID.ToString();
                        letterorder = Model.PartSpecificationSequences.Where(a => a.SequenceOrder.ToString() == formattedsequencid);
                        precedingletter = letterorder.FirstOrDefault(a => Convert.ToString(a.SequenceOrder) == formattedsequencid).SequenceText + " - ";


                    }

                }
		
            	%>
			 <tr>
             <%if (Model.UsersCulture.Name == "en-US")
               {%>
			<td style="height:32px;" ><b><%=precedingletter%></b>&nbsp;<%=process.Characteristic.ToUpper()%></td> 
          
            <td style="height:32px;"><%=measurementmethoddesc%></td> 
           
             <%} %>
              <%if (Model.UsersCulture.Name == "es-MX")
                {%>
			 <td style="height:32px;" ><b><%=precedingletter%></b>&nbsp;<%=process.CharacteristicES%></td> 
             <td style="height:32px;"><%=measearmentmethoddescEs%></td> 
                
             <%} %>

			 
            
             <td style="height:32px;"><%=process.LowSpec%>&nbsp;<%=abbreviation%></td> 
             <td style="height:32px;"><%=process.HighSpec%>&nbsp;<%=abbreviation%></td> 
             <%if (Model.UsersCulture.Name == "en-US")
               { %>
             <td style="height:32px;"><%=frequencydesc%></td> 
              <td style="height:32px;">&nbsp;&nbsp;<%=process.SampleSize%></td> 
             <%} %>
               <%if (Model.UsersCulture.Name == "es-MX")
                 { %>
             <td style="height:32px;"><%=frequencydescEs%></td> 
             <td style="height:32px;"><%=process.SampleSizeES%></td> 
             <%} %>
            
			 <td style="height:32px;"></td> 
			 <td style="height:32px;"></td> 
             <td style="height:32px;"></td> 
             <td style="height:32px;"></td> 
			 <td style="height:32px;"></td> 
             <td style="height:32px;"></td> 
             <td style="height:32px;" ></td> 

         
     <%   }
        }
        %>
			 </tr>          

<tr><td colspan="14"> 
 <%timestamp = "Printed:" +thedate +". By "+ username; %>
 <span class="ampm"><%=timestamp %>&nbsp;Number printed:&nbsp;  <span id='CCards'><%=Model.NumContinuationTCs.ToString() %></span> </span></td></tr>

    
     </tbody>
     </table> 
       
       
       
       
       
       
       
        <%} %>
    

 </div>

 
    </asp:Content>






