#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone2.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "50a6a7ea67f3579505638cbc067161c1434b15ca"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reservation__OpenConfirmedSendEmailDone2), @"mvc.1.0.view", @"/Views/Reservation/_OpenConfirmedSendEmailDone2.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"50a6a7ea67f3579505638cbc067161c1434b15ca", @"/Views/Reservation/_OpenConfirmedSendEmailDone2.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_Reservation__OpenConfirmedSendEmailDone2 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SIXTReservationApp.Models.CByCustomerReservation.CustomerModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div>\r\n");
#nullable restore
#line 4 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone2.cshtml"
     if (Model !=null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""row"">
            <!-- step conetnt -->
            <div class=""md-form col-6 m-5"">
                <table class=""table table-bordered"">
                    <thead>
                        <tr>
                            <td class=""font-weight-bold"">Name</td>
                            <td class=""font-weight-bold"">Email</td>
                            <td class=""font-weight-bold"">Phone</td>
                            <td class=""font-weight-bold"">Comment</td>
                        </tr>
                    </thead>
                        <tr>
                            <td>");
#nullable restore
#line 19 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone2.cshtml"
                           Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 20 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone2.cshtml"
                           Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 21 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone2.cshtml"
                           Write(Model.Phone);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 22 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone2.cshtml"
                           Write(Model.Comment);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n                </table>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 27 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone2.cshtml"
    }
    else
    {

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SIXTReservationApp.Models.CByCustomerReservation.CustomerModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
