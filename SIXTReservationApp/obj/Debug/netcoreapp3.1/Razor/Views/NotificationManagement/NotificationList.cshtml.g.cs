#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\NotificationList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0d2e2987650ed0d04020cbf66eef6df7ee78d71b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_NotificationManagement_NotificationList), @"mvc.1.0.view", @"/Views/NotificationManagement/NotificationList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0d2e2987650ed0d04020cbf66eef6df7ee78d71b", @"/Views/NotificationManagement/NotificationList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_NotificationManagement_NotificationList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("searchNotification"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\NotificationList.cshtml"
  
    ViewData["Title"] = "All Notification";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""ibox"">
    <div class=""ibox-head"">
        <div class=""ibox-title"">Search</div>
        <div class=""ibox-tools"">
            <a class=""ibox-collapse""><i class=""fa fa-minus""></i></a>
            <a class=""fullscreen-link""><i class=""fa fa-expand""></i></a>
        </div>
    </div>
    <div class=""ibox-body"">
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0d2e2987650ed0d04020cbf66eef6df7ee78d71b4781", async() => {
                WriteLiteral(@"
            <div class=""form-group row"">
                <label class=""col-4 col-form-label font-weight-bold"">Date Range</label>
            </div>
            <div class=""form-group row"">
                <div class=""col-4"">
                    <input type=""hidden"" id=""hfBookingDtFrom"" name=""BookingDateFrom"" />
                    <input type=""hidden"" id=""hfBookingDtTo"" name=""BookingDateTo"" />
                    <input id=""calBookingDtRange"" class=""form-control"" autocomplete=""off"" />
                </div>
               
            </div>


            <div class=""form-group row"">
                <div class=""col-12"">
                    <button class=""btn btn-primary btn-md mr-0 float-right waves-effect waves-light"" type=""submit"">Search</button>
                    <button type=""button"" id=""resetBTN"" onclick=""clearSearch()"" class=""btn btn-secondary btn-md mr-0 float-right waves-effect waves-light"">
                        Cancel
                    </button>
                </div>
   ");
                WriteLiteral("         </div>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    </div>
</div>
<div id=""DivNotification"">


</div>

<script>
    async function initDateRange() {
        $('#calBookingDtRange').daterangepicker({
            opens: 'right',
            timePicker: true,
            timePickerIncrement: 15,
            timePicker24Hour: true,
            maxSpan: {
                'years': 1,
            },
            locale: {
                cancelLabel: 'Clear',
                format: 'DD MMM YYYY',
            },
            //ranges: {
            //    'Today': [moment().startOf('day'), moment().endOf('day')],
            //    'Yesterday': [moment().subtract(1, 'days').startOf('day'), moment().subtract(1, 'days').endOf('day')],
            //    'Tomorrow': [moment().add(1, 'days').startOf('day'), moment().add(1, 'days').endOf('day')],
            //    'This Week': [moment().startOf('week'), moment().endOf('week')],
            //    'This Month': [moment().startOf('month'), moment().endOf('month')],
            //},
        }).on(");
            WriteLiteral(@"'apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('DD MMM YYYY') + ' - ' + picker.endDate.format('DD MMM YYYY'));
            $('#hfBookingDtFrom').val(picker.startDate.format('YYYY-MM-DD'));
            $('#hfBookingDtTo').val(picker.endDate.format('YYYY-MM-DD'));
        }).on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
            $('#hfBookingDtFrom').val('');
            $('#hfBookingDtTo').val('');
        });

        $('#calPickUpDtRange').daterangepicker({
            opens: 'right',
            timePicker: true,
            timePickerIncrement: 15,
            timePicker24Hour: true,
            maxSpan: {
                'years': 1,
            },
            locale: {
                cancelLabel: 'Clear',
                format: 'DD MMM YYYY',
            },
            //ranges: {
            //    'Today': [moment().startOf('day'), moment().endOf('day')],
            //    'Yesterday': [mome");
            WriteLiteral(@"nt().subtract(1, 'days').startOf('day'), moment().subtract(1, 'days').endOf('day')],
            //    'Tomorrow': [moment().add(1, 'days').startOf('day'), moment().add(1, 'days').endOf('day')],
            //    'This Week': [moment().startOf('week'), moment().endOf('week')],
            //    'This Month': [moment().startOf('month'), moment().endOf('month')],
            //},
        }).on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('DD MMM YYYY') + ' - ' + picker.endDate.format('DD MMM YYYY'));
            $('#hfPickUpDtFrom').val(picker.startDate.format('YYYY-MM-DD'));
            $('#hfPickUpDtTo').val(picker.endDate.format('YYYY-MM-DD'));
        }).on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
            $('#hfPickUpDtFrom').val('');
            $('#hfPickUpDtTo').val('');
        });

        $('#calCancelDtRange').daterangepicker({
            opens: 'right',
            timePicker: true,
     ");
            WriteLiteral(@"       timePickerIncrement: 15,
            timePicker24Hour: true,
            maxSpan: {
                'years': 1,
            },
            locale: {
                cancelLabel: 'Clear',
                format: 'DD MMM YYYY',
            },
            //ranges: {
            //    'Today': [moment().startOf('day'), moment().endOf('day')],
            //    'Yesterday': [moment().subtract(1, 'days').startOf('day'), moment().subtract(1, 'days').endOf('day')],
            //    'Tomorrow': [moment().add(1, 'days').startOf('day'), moment().add(1, 'days').endOf('day')],
            //    'This Week': [moment().startOf('week'), moment().endOf('week')],
            //    'This Month': [moment().startOf('month'), moment().endOf('month')],
            //},
        }).on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('DD MMM YYYY') + ' - ' + picker.endDate.format('DD MMM YYYY'));
            $('#hfCancelDtFrom').val(picker.startDate.format('YYY");
            WriteLiteral(@"Y-MM-DD'));
            $('#hfCancelDtTo').val(picker.endDate.format('YYYY-MM-DD'));
        }).on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
            $('#hfCancelDtFrom').val('');
            $('#hfCancelDtTo').val('');
        });


    }

     function loadNotification(e) {
        if (e) {
            e.preventDefault();
            e.returnValue = false;
        }

        var form = $('#searchNotification').serialize();

        $.ajax({
            url: '");
#nullable restore
#line 147 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\NotificationManagement\NotificationList.cshtml"
             Write(Url.Action("AllNotification"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"?' + form,
            method: 'GET',
            success: response => {
                if (response) {
                    $('#DivNotification').html(response);
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

    $(() => {

        initDateRange();
        loadNotification();
        $('#searchNotification').on('submit', loadNotification);

    })
    function clearSearch() {
        $('#searchNotification')[0].reset();
        $('#hfBookingDtFrom').val('');
        $('#hfBookingDtTo').val('');
        loadNotification();
    }
</script>");
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
