
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/WPF.Master"  Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.TravelCardPrintViewModel>" %>
 

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>
 <asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server" >

  <script language="javascript">
      function toggleEN() {
          var ele = document.getElementById("omilist");
          var text = document.getElementById("showpreviousOMIsEN");
          if (ele.style.display == "block") {
              ele.style.display = "none";
              text.innerHTML = "Click to create a continuation Travel Card based on a previously printed Travel; Card.";
           
          }
          else {
              ele.style.display = "block";
              text.innerHTML = "If a previous Travel Card is not selected a new Travel Card will be created. Click to hide.";

          }
      } 
</script>

<script language="javascript">
    function toggleMX() {
        var ele = document.getElementById("omilist");
        var text = document.getElementById("showpreviousOMIsMX");
        if (ele.style.display == "block") {
            ele.style.display = "none";
            text.innerHTML = "Seleccione de Tarjetas de viajes/OMI recientes para esta parte.";

        }
        else {
            ele.style.display = "block";
            text.innerHTML = "Si un OMI/Tarjeta de viaje previo no es seleccionado un nuevo OMI sera creado. Haga click para esconder.";

        }
    } 
</script>



 </asp:Content>  
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">


 
 


<div id="pagecontents" style="width:70%;">



  


 
  <% 
     
      string itemid;
     
          itemid = ViewData["PartID"].ToString();

    
    if (Model.IsDraft)
       { %>
        <h2><%=Quality.Resources.Strings.OMIPreview%></h2><br />
       
      <%}
       else {%>
            <h2><%=Quality.Resources.Strings.CreateOMI%></h2><br />
          <% } %>
    <div class="pageheaders"><%=Quality.Resources.Strings.CreateOMI %></div><br />
     <p style="color:red;"><%= Html.Encode(ViewData["NoResultMessage"]) %></p>
      <% using (Html.BeginForm("TravelCard", "TravelCard"))
         {

         
             %>
        <%= Html.HiddenFor(model => model.IsDraft)%> 
              <%= Html.HiddenFor(model => model.Language)%>    
     <table id="ItemIDSearch" class="standard-table">

    <col class="label-column" />
            <col class="data-column" />
             <tr>
                <td><%=Quality.Resources.Strings.SearchTravelCardByPartNum%>:</td>
                  <td>
                <%= Html.TextBoxFor(a => a.ItemID, new { style = "width:100px;", maxlength = "15", @Value = itemid, @readonly = true })%>
                

                  </td>
                  
             </tr>
             <%if (Model.HasOpCodes) 
               {%>
              <tr><td><%=Quality.Resources.Strings.OperationCode %></td><td><%= Html.DropDownList("OperationCodes",(IEnumerable<SelectListItem>)ViewData["opcodelist"], "") %></td></tr>
              <%} %>
             <tr><td><%=Quality.Resources.Strings.NumberOfCards %>:</td><td><%= Html.DropDownListFor(a=>a.NumContinuationTCs,Model.NumOfContinuationTravelCards) %>&nbsp;(Creates new continuation cards)</td></tr>
             <%if (Model.TravelCards != null && Model.TravelCards.Count() > 0)
               {%>

            <tr><td><%if(Model.Language=="en-US") 
                      {%>
             <a id="showpreviousOMIsEN"style="color:Green;" href="javascript:toggleEN();">Select from previous Travel Cards for this part.</a></td></tr>
             <%}%>
             
              <tr><td><%if(Model.Language=="es-MX") 
                      {%>
             <a id="showpreviousOMIsMX"style="color:Green;" href="javascript:toggleMX();">Seleccione de Tarjetas de viajes recientes para esta parte.</a>
             <%}%>
             </td><td><span id='omilist' style="display:none;"><%=Quality.Resources.Strings.TravelCard %>#:<%= Html.DropDownListFor(a=>a.TravelCard.TCID,Model.TravelCardList,"") %></span></td></tr>

              <% }   
                   %>
            
             
            <tr>
                <td>

                    <input type="submit" class="button" name="SubmitConfirmation" value="<%=Quality.Resources.Strings.CreateOMI %>" onclick="getElementById('progress').parentNode.style.display = 'block';" />
                  <div style="display:none">
<br /><span id='progress' style="position:relative;float:right;font-size:85%;font-weight:bold;padding:5px;
	color:#003300;
	z-index:10;"><%=Quality.Resources.Strings.BuildingCardPleaseWait %></span>
</div>





                </td><td></td>
            </tr>
          
            </table>
            <%} %>


   <br /><br />
 </div>
</asp:Content>
