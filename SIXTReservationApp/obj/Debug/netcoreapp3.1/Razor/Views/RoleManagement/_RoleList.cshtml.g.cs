#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eb17997064c5a9d27b3bd6f4d3511d69e7dcfc4b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RoleManagement__RoleList), @"mvc.1.0.view", @"/Views/RoleManagement/_RoleList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\_ViewImports.cshtml"
using SIXTReservationApp.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\_ViewImports.cshtml"
using SIXTReservationApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eb17997064c5a9d27b3bd6f4d3511d69e7dcfc4b", @"/Views/RoleManagement/_RoleList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_RoleManagement__RoleList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SIXTReservationApp.Models.RoleManagement.RoleVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
  
    ViewData["Title"] = "Role List";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""page-content fade-in-up"">
    <div class=""ibox"">
        <div class=""ibox-head"">
            <div class=""ibox-title"">Role List</div>
        </div>
        <div class=""ibox-body"">
            <table class=""table table-striped table-bordered table-hover"" id=""tblRoles"" cellspacing=""0"" width=""100%"">
                <thead class=""text-center"">
                    <tr>
                        <th  data-sortable=""false"" class=""text-left"">Role Name</th>
                        <th  data-sortable=""false"">Description</th>
                        <th  data-sortable=""false"">Created By</th>
                        <th>Creation Date</th>
                        <th>Last Modified By</th>
                        <th>Last Modified Date</th>
                        <th  data-sortable=""false"">Actions</th>
                    </tr>
                </thead>
                <tbody class=""text-center"">
");
#nullable restore
#line 25 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
                     if (Model?.Count > 0)
                    {
                        for (int i = 0; i < Model.Count; i++)
                        {
                            var item = Model[i];

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td  class=\"text-left\"><b>");
#nullable restore
#line 31 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
                                                     Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n                                <td>");
#nullable restore
#line 32 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
                               Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 33 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
                               Write(item.CreatedBy);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 34 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
                               Write(item.CreationDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 35 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
                               Write(item.LastModifiedBy);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 36 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
                               Write(item.LastModificationDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>\r\n\r\n                                    <button class=\"material-tooltip-main btn-primary btn btn-floating btn-sm my-0 p-0 m-0\" data-toggle=\"tooltip\" title=\"Edit\">\r\n                                        <a");
            BeginWriteAttribute("href", " href=\"", 1907, "\"", 1961, 1);
#nullable restore
#line 40 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
WriteAttributeValue("", 1914, Url.Action("UpdateRole", new { Id = item.Id }), 1914, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""btn-link"">
                                            <i class=""fa fa-pencil font-14""></i>
                                        </a>
                                    </button>
                                    <button class=""material-tooltip-main btn btn-red btn-floating btn-sm my-0 p-0 m-0 ml-1"" data-placement=""top"" data-toggle=""tooltip"" title=""Delete""");
            BeginWriteAttribute("onclick", " onclick=\"", 2338, "\"", 2368, 3);
            WriteAttributeValue("", 2348, "deleteRole(", 2348, 11, true);
#nullable restore
#line 44 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
WriteAttributeValue("", 2359, item.Id, 2359, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2367, ")", 2367, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <i class=\"fa fa-trash \"></i>\r\n                                    </button>\r\n\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 50 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
                        }
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td colspan=\"8\" class=\"text-center\">No roles found</td>\r\n                        </tr>\r\n");
#nullable restore
#line 57 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\RoleManagement\_RoleList.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SIXTReservationApp.Models.RoleManagement.RoleVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
