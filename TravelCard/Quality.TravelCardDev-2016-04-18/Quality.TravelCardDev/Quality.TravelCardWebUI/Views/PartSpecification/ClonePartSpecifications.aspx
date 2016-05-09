<%@ Page Title="Clone Part Specifications" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quality.ViewModels.PartSpecificationViewModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Clone Part Specifications
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
     function searchSel() {
         var input = document.getElementById('searchtxt').value.toLowerCase();
         var output = document.getElementById('ddlParts').options;
         for (var i = 0; i < output.length; i++) {
             if (output[i].value.indexOf(input) == 0) {
                 output[i].selected = true;
             }
             if (document.forms[0].searchtxt.value == '') {
                 output[0].selected = true;
             }
         }
     }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br /><br />
<%string itemid = Model.PartSetUp.PartID; %>
  <div class='pageheaders'>Clone Part Specifications to <%=(itemid) %></div><br />
  <br />
   
	 <% using (Html.BeginForm("ClonePartSpecifications", "PartSpecification"))
     {       %>
         
       <%= Html.HiddenFor(model => model.ItemID)%> 
           
       <%= Html.HiddenFor(model => model.PartSetUp.PartSetUpID)%>  
       
  
 

<div><h2>Clone Part Specifications from a previously created <%=Model.PartSetUp.PartCategory.CategoryName %>. </h2> </div> 
<div style="width:60%;border-color:Green;background-color: #e8eef4;">
<br />
<span style="font-weight:bold;color:Green;">&nbsp;&nbsp;Search by Part ID:</span><input type="text" id="searchtxt" onkeyup="searchSel()"/><br /> <br />
<span style="font-weight:bold;color:Green;">&nbsp;&nbsp;Select Part ID:</span><%= Html.DropDownListFor(a => a.PartSetUp.PartID, Model.PartSetUpSelectList, "Select Part ID", new {@id="ddlParts"})%><span style="color:red;">&nbsp;*</span>&nbsp;<%= Html.ValidationMessageFor(model => model.PartSetUp.PartID)%>
<br />
<br />

<br />
</div>

<br />

<br />
	  <span> Note: Once cloned, the part specifications can be edited as needed or set to inactive if not needed. <br /> Once cloned the part specifications belong exclusively to the part setup they have been cloned to.</span><br /><br />
     <input type="submit" class="button" value="Clone Part Specifications to Part # <%=(itemid)%>" /><br />
   	<a href="javascript:history.go(-1)">
Cancel
</a>
<%} %>


</asp:Content>



