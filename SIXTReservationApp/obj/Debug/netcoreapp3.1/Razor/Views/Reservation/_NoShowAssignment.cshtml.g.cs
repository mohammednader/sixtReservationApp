#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "652112cdbb62a8aa7351a7101a10ff72b047de88"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reservation__NoShowAssignment), @"mvc.1.0.view", @"/Views/Reservation/_NoShowAssignment.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"652112cdbb62a8aa7351a7101a10ff72b047de88", @"/Views/Reservation/_NoShowAssignment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_Reservation__NoShowAssignment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SIXTReservationApp.Models.CByCustomerReservation.UserAssignments>>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div>\r\n");
#nullable restore
#line 3 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
     if (Model?.Count > 0)
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
                            <td class=""font-weight-bold"">From User</td>
                            <td class=""font-weight-bold"">To User</td>
                            <td class=""font-weight-bold"">Date</td>
                        </tr>
                    </thead>

");
#nullable restore
#line 17 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
                     for (int i = 0; i < Model.Count; i++)
                    {

                        var item = Model[i];

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 22 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
                           Write(item.FromUser);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 23 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
                           Write(item.ToUser);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 24 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
                           Write(item.Date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                        </tr>\r\n");
#nullable restore
#line 27 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                </table>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 33 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""row ml-5"">
            <div class=""col-6 form-group"">
                <label><b>Select user</b></label>
                <select id=""assignDDL"" class=""form-control select-dropdown validate"" Searchable=""Search here..."" required>
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "652112cdbb62a8aa7351a7101a10ff72b047de886168", async() => {
                WriteLiteral("select user");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("disabled", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </select>\r\n\r\n            </div>\r\n        </div>\r\n");
            WriteLiteral("        <div class=\"step-actions ml-3\">\r\n            <button class=\"btn btn-primary btn-md mr-0 float-right waves-effect waves-light ml-5\" onclick=\"assignAgent()\">Assign</button>\r\n");
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 50 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n<script>\r\n    $(() => {\r\n");
#nullable restore
#line 54 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
         if (Model?.Count == 0)
            {
              

#line default
#line hidden
#nullable disable
            WriteLiteral(" loadUsers();");
#nullable restore
#line 56 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
                                        
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        })\r\n\r\n    async function loadUsers()\r\n    {\r\n        $(\'#assignDDL\').empty();\r\n        $(\'#assignDDL\').append(\'<option selected disabled > select user</option>\');\r\n\r\n        $.ajax({\r\n            url: \'");
#nullable restore
#line 66 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\_NoShowAssignment.cshtml"
             Write(Url.Action("GetNoShowAssignUsers", "Reservation"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            method: 'GET',
            success: response => {
                if (response && response.length) {
                    response.forEach(r => {
                        var node = $(`<option value=${r.id}>${r.name}</option>`);
                        $('#assignDDL').append(node);
                    });
                }

            }
        });
    }



</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SIXTReservationApp.Models.CByCustomerReservation.UserAssignments>> Html { get; private set; }
    }
}
#pragma warning restore 1591
