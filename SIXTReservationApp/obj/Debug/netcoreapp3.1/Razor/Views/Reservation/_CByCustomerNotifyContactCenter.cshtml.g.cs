#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "63e2e56baeecb02836b65dfb637f5167c205bc59"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reservation__CByCustomerNotifyContactCenter), @"mvc.1.0.view", @"/Views/Reservation/_CByCustomerNotifyContactCenter.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63e2e56baeecb02836b65dfb637f5167c205bc59", @"/Views/Reservation/_CByCustomerNotifyContactCenter.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_Reservation__CByCustomerNotifyContactCenter : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SIXTReservationApp.Models.CByCustomerReservation.UserNotifications>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
         <!-- step conetnt -->
        <div class=""md-form col-6 m-5"">
            <table class=""table table-bordered"">
                <thead>
                    <tr class=""font-weight-bold"">
                        <td class=""font-weight-bold"">Name</td>
                        <td class=""font-weight-bold"">Email</td>
                        <td class=""font-weight-bold""> Notified Date</td>
                    </tr>
                </thead>
");
#nullable restore
#line 13 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"
                 if (Model?.Count > 0)
                {

                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"
                     for (int i = 0; i < Model.Count; i++)
                    {

                        var item = Model[i];

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 21 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"
                           Write(item.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 22 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"
                           Write(item.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 23 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"
                           Write(item.NotifiedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                        </tr>\r\n");
#nullable restore
#line 26 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"
                     
                }
                else
                {


#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr> <td colspan=\"3\"> No notification send </td> </tr>\r\n");
#nullable restore
#line 32 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n        </div>\r\n\r\n        <!-- step confirmation -->\r\n    \r\n    <div class=\"step-actions\">\r\n        <input hidden");
            BeginWriteAttribute("value", " value=\"", 1270, "\"", 1300, 1);
#nullable restore
#line 40 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"
WriteAttributeValue("", 1278, ViewBag.ReservationId, 1278, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"reservationId\" />\r\n        <input hidden");
            BeginWriteAttribute("value", " value=\"", 1346, "\"", 1374, 1);
#nullable restore
#line 41 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_CByCustomerNotifyContactCenter.cshtml"
WriteAttributeValue("", 1354, ViewBag.CurrentStep, 1354, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"currentStep\" />\r\n    </div>\r\n \r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SIXTReservationApp.Models.CByCustomerReservation.UserNotifications>> Html { get; private set; }
    }
}
#pragma warning restore 1591
