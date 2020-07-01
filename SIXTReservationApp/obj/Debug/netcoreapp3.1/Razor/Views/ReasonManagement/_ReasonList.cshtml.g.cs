#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cdb6ee743e31713888b83d95374e4c33e545bb4d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ReasonManagement__ReasonList), @"mvc.1.0.view", @"/Views/ReasonManagement/_ReasonList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cdb6ee743e31713888b83d95374e4c33e545bb4d", @"/Views/ReasonManagement/_ReasonList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_ReasonManagement__ReasonList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SIXTReservationApp.Models.Reason.ReasonVM[]>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
  
    ViewData["Title"] = "Reason List";


#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""fade-in-up"">
    <div class=""ibox"">
        <div class=""ibox-head"">
            <div class=""ibox-title"">Reason List</div>
        </div>
        <div class=""ibox-body"">
            <table class=""table table-striped table-bordered table-hover"" id=""example-table"" cellspacing=""0"" width=""100%"">
                <thead class=""text-center"">
                    <tr>
                        <th class=""text-left"">Reason Name</th>
                        <th data-sortable=""false"">Reservation Status</th>
                        <th data-sortable=""false"">Category Status</th>
                        <th data-sortable=""false"">Status</th>
                        <th data-sortable=""false"">Action</th>

                    </tr>
                </thead>
                <tbody class=""text-center"">
");
#nullable restore
#line 26 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                     if (Model != null && Model.Length > 0)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                         for (int i = 0; i < Model.Length; i++)
                        {
                            var item = Model[i];

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td class=\"text-left\">\r\n                                    ");
#nullable restore
#line 33 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                               Write(item.Reason);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 36 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                               Write(item.ReservationStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n");
#nullable restore
#line 39 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                     if (item.Status == true)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <span>  <i title=\"Answer\" class=\"fa fa-check text-success\"></i> Answer</span>\r\n");
#nullable restore
#line 42 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                    }
                                    else if (item.Status == false)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <span> <i title=\"No Answer\" class=\"fa fa-times text-danger\"></i> No Answer</span>\r\n");
#nullable restore
#line 46 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <span> <i title=\"None\"");
            BeginWriteAttribute("class", " class=\"", 2177, "\"", 2185, 0);
            EndWriteAttribute();
            WriteLiteral("></i> None</span>\r\n");
#nullable restore
#line 50 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </td>\r\n                                <td>\r\n");
#nullable restore
#line 53 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                     if (item.IsActive ?? false)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <i title=\"Active\" class=\"fa fa-check text-success\"></i>\r\n");
#nullable restore
#line 56 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <i title=\"Inactive\" class=\"fa fa-times text-danger\"></i>\r\n");
#nullable restore
#line 60 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </td>\r\n                                <td>\r\n                                    <button");
            BeginWriteAttribute("onclick", " onclick=\"", 2900, "\"", 2924, 3);
            WriteAttributeValue("", 2910, "Edit(", 2910, 5, true);
#nullable restore
#line 63 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
WriteAttributeValue("", 2915, item.Id, 2915, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2923, ")", 2923, 1, true);
            EndWriteAttribute();
            WriteLiteral(@" class=""material-tooltip-main btn-primary btn btn-floating btn-sm   p-0 m-0"" data-placement=""top"" data-toggle=""tooltip""
                                          title=""Edit"">
                                        <i class=""fa fa-pencil font-14""></i>
                                    </button>
");
#nullable restore
#line 67 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                     if (item.IsActive ?? false)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <button class=\"material-tooltip-main btn btn-danger btn-floating btn-sm my-0 p-0 mx-1\" data-placement=\"top\" data-toggle=\"tooltip\"\r\n                                                title=\"Deactivate\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3570, "\"", 3606, 3);
            WriteAttributeValue("", 3580, "deactivateReason(", 3580, 17, true);
#nullable restore
#line 70 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
WriteAttributeValue("", 3597, item.Id, 3597, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3605, ")", 3605, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <i class=\"fa fa-lock\"></i>\r\n                                        </button>\r\n");
#nullable restore
#line 73 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <button class=\"material-tooltip-main btn btn-success btn-floating btn-sm my-0 p-0 mx-1\" data-placement=\"top\" data-toggle=\"tooltip\"\r\n                                                 title=\"Activate\"");
            BeginWriteAttribute("onclick", " onclick=\"", 4090, "\"", 4124, 3);
            WriteAttributeValue("", 4100, "activateReason(", 4100, 15, true);
#nullable restore
#line 77 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
WriteAttributeValue("", 4115, item.Id, 4115, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4123, ")", 4123, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <i class=\"fas fa-lock-open\"></i>\r\n                                        </button>\r\n");
#nullable restore
#line 80 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <button");
            BeginWriteAttribute("onclick", " onclick=\"", 4339, "\"", 4365, 3);
            WriteAttributeValue("", 4349, "Delete(", 4349, 7, true);
#nullable restore
#line 81 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
WriteAttributeValue("", 4356, item.Id, 4356, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4364, ")", 4364, 1, true);
            EndWriteAttribute();
            WriteLiteral(@" class=""material-tooltip-main btn btn-red btn-floating btn-sm my-0 p-0 m-0"" data-placement=""top"" data-toggle=""tooltip""
                                            title=""Delete"">
                                        <i class=""fa fa-trash ""></i>
                                    </button>
                                </td>
                            </tr>
");
#nullable restore
#line 87 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 87 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\ReasonManagement\_ReasonList.cshtml"
                         
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n \r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SIXTReservationApp.Models.Reason.ReasonVM[]> Html { get; private set; }
    }
}
#pragma warning restore 1591
