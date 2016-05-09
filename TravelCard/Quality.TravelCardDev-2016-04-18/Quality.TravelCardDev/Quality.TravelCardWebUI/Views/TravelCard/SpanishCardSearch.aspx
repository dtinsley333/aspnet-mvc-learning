<%@ Page Language="C#" MasterPageFile="~/Views/Shared/wpf.Master" Inherits="System.Web.Mvc.ViewPage" %>
 

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>
 <asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server" >
  
   </asp:Content>  
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="pagecontents" style="width:70%;">
 
<br />
  
    <div class="pageheaders"><%= Html.Encode(ViewData["Message"]) %></div><br />
     <p><%= Html.Encode(ViewData["NoItemIDMessage"]) %></p>
      <% using (Html.BeginForm("TravelCardPrint_Spanish", "TravelCard"))
         { %>
     <table id="ItemIDSearch" class="standard-table">
    <col class="label-column" />
            <col class="data-column" />
             <tr>
                <td>Búsqueda de la tarjeta de viaje por el ID del artículo:</td>
                  <td>
                  <input type="text" id="ItemID" name="ItemID" style="width: 200px" 
                          alt="Introduzca la ID del artículo." maxlength="50" /> 
                  </td>
             </tr>
            <tr>
                <td colspan="2">
                    <input type="submit" class="button" value="búsqueda" />
                </td>
            </tr>
          
            </table>
            <%} %>
   
   <br /><br />
 </div>
</asp:Content>
