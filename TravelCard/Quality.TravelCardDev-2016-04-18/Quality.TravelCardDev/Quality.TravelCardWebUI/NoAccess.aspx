<%@ Page Title="No Access" Language="C#" MasterPageFile="~/Views/Shared/WPF.Master"   %>


<asp:Content ID="NoAccess" ContentPlaceHolderID="TitleContent" runat="server">
	No Access
</asp:Content>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server" >


</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
 
  
   
    <div id="pagecontents" style="width:95%;">

 <div id="header">
      <% string ideallogo = ConfigurationManager.AppSettings["ideallogo"];%>
		   <span class="ideallogo"><img src="<%=ideallogo%> "/>  <span class="headertitle">Ideal Quality Application</span></span> 
               
			<div id="title">
			</div>
<br /><br />
<div class='userinfo'>Your Id was not found in any of the Travel Card Active Directory Roles. If you feel this is in error or you would like
to request access please contact the I.T.help desk.<br /><br /><a href="http://sf-spps/sites/IT/helpdesk/default.aspx">Help Desk</a>  </div><br />



	 
</div>
</asp:Content>








