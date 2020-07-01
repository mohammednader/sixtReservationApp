#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c2ff0d9656bd51616d9b8dde78dd53f55f40b000"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_NotificationManagement__NotificationList), @"mvc.1.0.view", @"/Views/NotificationManagement/_NotificationList.cshtml")]
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
#nullable restore
#line 1 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
using SIXTReservationBL.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c2ff0d9656bd51616d9b8dde78dd53f55f40b000", @"/Views/NotificationManagement/_NotificationList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_NotificationManagement__NotificationList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SIXTReservationApp.Models.Notification.NotificationVM[]>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
  
    ViewData["Title"] = "Notification List";


#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""fade-in-up"">
    <div class=""ibox"">
        <div class=""ibox-head"">
            <div class=""ibox-title"">Notification List</div>
        </div>
        <div class=""ibox-body"">
            <table class=""table table-striped table-bordered table-hover"" id=""example-table"" cellspacing=""0"" width=""100%"">
                <thead>
                    <tr>
                        <th  data-sortable=""false"" class=""text-left"">Reservation Status</th>
                        <th data-sortable=""false"" class=""text-center"">Action Step</th>
                        <th data-sortable=""false"" class=""text-center"">Job Title</th>
                        <th data-sortable=""false"" class=""text-center"">Status</th>
                        <th  data-sortable=""false"" class=""text-center"">Action</th>

                    </tr>
                </thead>
                <tbody class=""text-center"">
");
#nullable restore
#line 28 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                     if (Model != null && Model.Length > 0)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                         for (int i = 0; i < Model.Length; i++)
                        {
                            var item = Model[i];

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td class=\"text-left\">\r\n                            ");
#nullable restore
#line 35 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                       Write(item.ReservationStatusText);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 38 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                       Write(item.ActionStepText);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 41 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                       Write(item.JobTitleText);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n");
#nullable restore
#line 44 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                             if (item.IsDisable ?? false)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <i title=\"Disabled\" class=\"fa fa-times text-danger\"></i>\r\n");
#nullable restore
#line 47 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <i title=\"Enabled\" class=\"fa fa-check text-success\"></i>\r\n");
#nullable restore
#line 51 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </td>\r\n                        <td>\r\n                            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 2190, "\"", 2214, 3);
            WriteAttributeValue("", 2200, "Edit(", 2200, 5, true);
#nullable restore
#line 54 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
WriteAttributeValue("", 2205, item.Id, 2205, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2213, ")", 2213, 1, true);
            EndWriteAttribute();
            WriteLiteral(@" class=""material-tooltip-main btn-primary btn btn-floating btn-sm m-0 mr-1-0 p-0"" data-toggle=""tooltip"" title=""Edit"">
                                <i class=""fa fa-pencil font-14""></i>
                            </button>

                            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 2480, "\"", 2506, 3);
            WriteAttributeValue("", 2490, "Delete(", 2490, 7, true);
#nullable restore
#line 58 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
WriteAttributeValue("", 2497, item.Id, 2497, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2505, ")", 2505, 1, true);
            EndWriteAttribute();
            WriteLiteral(@" class=""material-tooltip-main btn btn-red btn-floating btn-sm m-0 p-0"" data-toggle=""tooltip""
                                    data-original-title=""Delete"">
                                <i class=""fa fa-trash""></i>
                            </button>
                        </td>
                    </tr>
");
#nullable restore
#line 64 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\_NotificationList.cshtml"
                         
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n<script>\r\n    $(() => {\r\n        $(\'.material-tooltip-main\').tooltip();\r\n    })\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SIXTReservationApp.Models.Notification.NotificationVM[]> Html { get; private set; }
    }
}
#pragma warning restore 1591