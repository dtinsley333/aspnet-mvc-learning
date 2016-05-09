<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Quality.ViewModels.UserProfileViewModel>" %>




            <a tabindex="0" href="#TravelCardMaintenanceMenu" class="fg-button fg-button-icon-right ui-widget ui-state-default ui-corner-all" id="TravelCardMaintenanceMenuButton">
                <span class="ui-icon ui-icon-triangle-1-s"></span><span style="color:Orange;"><%=Quality.Resources.Strings.PartSetUp %></span>
            </a>
           <%string partsetupsearch = Quality.Resources.Strings.SearchPartSetUps;%>
            <div id="TravelCardMaintenanceMenu" class="hidden">
                
                <ul>
                    <li><%= Html.ActionLink(partsetupsearch, "PartMaintenanceIndex", "TravelCard")%></li>
                     <li><%= Html.ActionLink("Part Set Up Listing(All)", "PartSetUpListing", "PartSetup")%></li>
                   <li><%= Html.ActionLink("Advanced Part Set Up Search", "PartSetUpSearch", "PartSetup")%></li>
                 
                </ul>
            </div>

             <a tabindex="0" href="#AdminMenu" class="fg-button fg-button-icon-right ui-widget ui-state-default ui-corner-all " id="AdminMenuButton">
                <span class="ui-icon ui-icon-triangle-1-s"></span><span style="color:Orange;">Admin Functions</span>
            </a>
            <div id="AdminMenu" class="hidden">
                
                <ul>
                     <li><%= Html.ActionLink("User Listing", "UserListing", "UserProfile")%></li>
                     <li><%= Html.ActionLink("Part Category List", "PartCategoryMaintenance", "PartCategory")%></li>
                     <li><%= Html.ActionLink("Measurement Method List", "MeasurementMethodMaintenance", "MeasurementMethod")%></li>
                     <li><%= Html.ActionLink("Frequency List", "FrequencyMaintenance", "Frequency")%></li>
                 
              
                   </ul>
            </div>
            <%if(Model.CanUserApprove)
              { %>
            <a tabindex="0" href="#GlobalUpDatesMenu" class="fg-button fg-button-icon-right ui-widget ui-state-default ui-corner-all " id="GlobalUpDatesMenuButton">
                <span class="ui-icon ui-icon-triangle-1-s"></span><span style="color:Orange;">Global Updates</span>
            </a>
            <div id="GlobalUpdates" class="hidden">
                
                  <ul>
                  <li style="background-color:Orange;"><span style="color:Orange;"><%= Html.ActionLink("Set Release Ready Flags", "PartSetUpGlobalReleaseReadyEditor", "PartSetUp")%></span></li>
                  <li style="background-color:Orange;"><span style="color:Orange;"><%= Html.ActionLink("Edit Deviation File #1 Links", "PartSetUpGlobalDeviationFileEditor", "PartSetUp")%></span></li>
                  <li style="background-color:Orange;"><span style="color:Orange;"><%= Html.ActionLink("Edit Deviation File #2 Links", "PartSetUpGlobalDeviation2FileEditor", "PartSetUp")%></span></li>
                  <li style="background-color:Orange;"><span style="color:Orange;"><%= Html.ActionLink("Edit Quality Alert File #1 Links", "PartSetUpGlobalQualityAlertFileEditor", "PartSetUp")%></span></li>
                  <li style="background-color:Orange;"><span style="color:Orange;"><%= Html.ActionLink("Edit Quality Alert File #2 Links", "PartSetUpGlobalQualityAlertFile2Editor", "PartSetUp")%></span></li>
                  <li style="background-color:Orange;"><span style="color:Orange;"><%= Html.ActionLink("Edit Drawing File Links", "PartSetUpGlobalDrawingFileEditor", "PartSetUp")%></span></li>
                  <li style="background-color:Orange;"><span style="color:Orange;"><%= Html.ActionLink("Edit Travel Card Water Marks", "PartSetUpGlobalRemarkEditor", "PartSetUp")%></span></li>
                   </ul>
            </div>
            <%} %>
              <a tabindex="0" href="#UserSettings" class="fg-button fg-button-icon-right ui-widget ui-state-default ui-corner-all " id="UserSettingsButton">
                <span class="ui-icon ui-icon-triangle-1-s"></span><span style="color:Orange;">User Settings</span>
            </a>
            <div id="UserSettings" class="hidden">
                
                <ul>
                    <li><%= Html.ActionLink("Select Plant", "SelectPlant", "UserProfile")%></li>
                     
                     </ul>
            </div>
      
