<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div>
<%string userMessage = ViewData["userMessage"].ToString(); %>
<%string success=ViewData["success"].ToString(); %>
<%string seconds = ViewData["seconds"].ToString(); %>
<%string filename = ViewData["filename"].ToString(); %>
<%if(success=="false") 
  {  %>
<span style="color:Red"><%=userMessage %> Timed out after <%=seconds %> second(s).</span><br /><br />

<%} %>

<%if(success=="true") 
  {  %>
<span style="color:#003300">OMI Successfully Downloaded. Download time <%=seconds%> second(s). <br />
If the pdf tool bar does not display click anywhere on the pdf then click F8.</span>
<%} %>
</div>
<div>
<br />
 <iframe src='<%=filename%>' width="800px;" height="1200" style="z-index:-10;" >
</iframe> </div>