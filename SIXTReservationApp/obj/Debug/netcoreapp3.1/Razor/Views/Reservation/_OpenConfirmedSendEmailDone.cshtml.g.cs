#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "591f841b44605a99e44e4080a093cc2be104fe9f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reservation__OpenConfirmedSendEmailDone), @"mvc.1.0.view", @"/Views/Reservation/_OpenConfirmedSendEmailDone.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"591f841b44605a99e44e4080a093cc2be104fe9f", @"/Views/Reservation/_OpenConfirmedSendEmailDone.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_Reservation__OpenConfirmedSendEmailDone : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SIXTReservationApp.Models.CByCustomerReservation.CustomerModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-light d-flex flex-column align-items-center\" role=\"alert\">\r\n        <h2 class=\"alert-heading\">Alertify</h2>\r\n        <p>Email was sent to ");
#nullable restore
#line 7 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone.cshtml"
                        Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("  : ");
#nullable restore
#line 7 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone.cshtml"
                                       Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n    </div>\r\n");
#nullable restore
#line 9 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_OpenConfirmedSendEmailDone.cshtml"
}
else
{
    
}

#line default
#line hidden
#nullable disable
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
