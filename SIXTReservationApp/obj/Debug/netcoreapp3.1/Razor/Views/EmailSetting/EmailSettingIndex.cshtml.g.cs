#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\EmailSetting\EmailSettingIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9b7003757ea996b1a3264fd6b67a53176ddda44a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EmailSetting_EmailSettingIndex), @"mvc.1.0.view", @"/Views/EmailSetting/EmailSettingIndex.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9b7003757ea996b1a3264fd6b67a53176ddda44a", @"/Views/EmailSetting/EmailSettingIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_EmailSetting_EmailSettingIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\EmailSetting\EmailSettingIndex.cshtml"
  
    ViewData["Title"] = "Email Setting Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""d-flex mb-2  justify-content-end"">
    <button class=""btn btn-primary btn-md mr-0"" onclick=""Create()"">
        <b>Add Email Setting</b>&nbsp;&nbsp;<i class=""fas fa-plus""></i>
    </button>
</div>

<!--Create Modal -->
<div class=""modal fade"" id=""CreateEmailSettingModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalCenterTitle"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""CreateEmailSettingModalTitle""></h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div id=""resultCreate"">

            </div>

        </div>
    </div>
</div>
<!--Edite Modal -->

<div class=""modal fade"" id=""editEmailSettingModal"" tabindex=""-1"" role=""dialog"" aria-labelle");
            WriteLiteral(@"dby=""exampleModalCenterTitle"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""editEmailSettingModalTitle""></h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>

            <div id=""result"">

            </div>

        </div>
    </div>
</div>
<div id=""DivEmailSetting"">

</div>



");
            WriteLiteral("<script>\r\n\r\n\r\n\r\n    function Create() {\r\n\r\n        $.ajax({\r\n            url: \'");
#nullable restore
#line 63 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\EmailSetting\EmailSettingIndex.cshtml"
             Write(Url.Action("CreateEmailSetting", "EmailSetting"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            type: ""Get"",
            contentType: ""application/json; charset=utf-8"",
            success: function (data) {
                $('#resultCreate').html(data);
                $('#CreateEmailSettingModalTitle').text(""Create Email Setting"");
                $('#CreateEmailSettingModalTitle').css(""text-align"", ""center"")
                $('#CreateEmailSettingModal').modal('show');
                $('#ReservationStatus').materialSelect();
            },
            error: function (x) {
                alert(""error"" + x);
            },
        });

    }

</script>
");
            WriteLiteral("<script>\r\n\r\n    function Edit(id) {\r\n        $.ajax({\r\n            url: \'");
#nullable restore
#line 86 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\EmailSetting\EmailSettingIndex.cshtml"
             Write(Url.Action("UpdateEmailSetting", "EmailSetting"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: { id },
            type: ""Get"",
            contentType: ""application/json; charset=utf-8"",
            success: function (data) {
                $('#result').html('');
                $('#resultCreate').html('');
                 $('#result').html(data);
                 $('#editEmailSettingModalTitle').text(""Update Email Setting"");
                 $('#CreateEmailSetting').attr('action', '");
#nullable restore
#line 95 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\EmailSetting\EmailSettingIndex.cshtml"
                                                     Write(Url.Action("UpdateEmailSetting"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
                 $('#btnSubmit').html('Update');
                 $('#btnSubmit').removeClass(""btn-primary"");
                 $('#btnSubmit').addClass(""btn-success"");
                 $('#editEmailSettingModal').modal('show');
                $('#ReservationStatus').materialSelect();
            },
            error: function (x) {
                alert(""error"" + x);
            },
        });
    }

</script>

<script>
    $(() => {
        loadEmailSettings();
       
    })
    function loadEmailSettings(e) {
        $('#CreateEmailSettingModal').modal('hide'); $('#editEmailSettingModal').modal('hide');
        if (e) {
            e.preventDefault();
            e.returnValue = false;
        }

        $.ajax({
            url: '");
#nullable restore
#line 123 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\EmailSetting\EmailSettingIndex.cshtml"
             Write(Url.Action("_EmailSettingList"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            method: 'GET',
            success: response => {
                if (response) {
                    $('#DivEmailSetting').html(response);
                    $('#example-table').dataTable();
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        text: 'Failed to load roles',
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591