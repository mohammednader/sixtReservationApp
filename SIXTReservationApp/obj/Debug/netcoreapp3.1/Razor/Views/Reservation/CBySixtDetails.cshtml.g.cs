#pragma checksum "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5323f23b3b89a261c4a9d5e4215f522e8a6fd4dd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reservation_CBySixtDetails), @"mvc.1.0.view", @"/Views/Reservation/CBySixtDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5323f23b3b89a261c4a9d5e4215f522e8a6fd4dd", @"/Views/Reservation/CBySixtDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19513b6bb04e9047c3017a7b65726ff4908642a7", @"/Views/_ViewImports.cshtml")]
    public class Views_Reservation_CBySixtDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SIXTReservationApp.Models.VReservationListVM>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
  
    ViewData["Title"] = "Canceled By Sixt Details";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 9 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"ibox mt-2\">\r\n        <div class=\"ibox-head\"><h4>Reservation ");
#nullable restore
#line 12 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                          Write(Model.ReservationNum);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" Details</h4></div>
        <div class=""ibox-body"">
            <div class=""row"">
                <div class=""col-6"">

                    <table class=""table table-bordered"">
                        <thead>
                            <tr class=""d-none"">
                                <th scope=""col"">Number Of Reservations</th>
                                <th scope=""col"">First</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope=""row"" class=""font-weight-bold"">Number Of Reservations</th>
                                <td>");
#nullable restore
#line 27 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                               Write(Html.DisplayFor(model => model.NumberOfReservations));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Booking Date</th>\r\n                                <td>");
#nullable restore
#line 31 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                               Write(Html.DisplayFor(model => model.BookingDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Reservation Hour</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 35 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.ReservationHour));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">PickUp Date</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 39 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.PickUpDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">PickUp Hour</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 43 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.PickUpHour));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">PickUp Week Day</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 47 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.PickUpweekDay));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Revenue Eur</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 51 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.RevenueEur));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Revenue Per Day Eur</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 55 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.RevenuePerDayEur));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Rental Days</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 59 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.RentalDays));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Lead Time In Days</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 63 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.LeadTimeInDays));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Cancelled After Booking In Days</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 67 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.CancelledAfterBookingInDays));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Cancelled Before PickUp In Days</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 71 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.CancelledBeforeBickUpInDays));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">PickUp Branch</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 75 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.PickUpBranchName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Drop Off Date</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 79 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.DropOffDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Drop Off Hour</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 83 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.DropOffHour));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Drop Off week Day</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 87 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.DropOffweekDay));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Vehicle Acriss</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 91 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.VehicleAcriss));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Reservation Source Channel1</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 95 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.ReservationSourceChannel1));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Reservation Source Channel2</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 99 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.ReservationSourceChannel2));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Reservation Source Channel3</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 103 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.ReservationSourceChannel3));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">RateSegment Name</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 107 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.RateSegmentName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">RateSegment Category</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 111 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.RateSegmentCategoryName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Rate Segment SubCategory</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 115 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.RateSegmentSubCategory));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Reservation Point Of Sale</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 119 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.ReservationPointOfSale));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class=""col-6"">

                    <table class=""table table-bordered"">
                        <thead>
                            <tr class=""d-none"">
                                <th scope=""col"">Number Of Reservations</th>
                                <th scope=""col"">First</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope=""row"" class=""font-weight-bold"">One Way Reservation </th>
                                <td>
");
#nullable restore
#line 137 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                     if (Model.OneWayReservation.HasValue)
                                    {
                                        if (Model.OneWayReservation.Value)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <sapn>True</sapn>\r\n");
#nullable restore
#line 142 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <sapn>False</sapn>\r\n");
#nullable restore
#line 146 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                        }
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">On Request</th>\r\n                                <td>\r\n");
#nullable restore
#line 153 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                     if (Model.OnRequest.HasValue)
                                    {
                                        if (Model.OnRequest.Value)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <sapn>True</sapn>\r\n");
#nullable restore
#line 158 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <sapn>False</sapn>\r\n");
#nullable restore
#line 162 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                        }
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Request Level</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 168 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.RequestLevel));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Reservation Agent</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 172 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.ReservationAgent));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Reservation Status</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 177 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.ReservationStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Cancelled Date</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 181 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.CancelledDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n\r\n\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Rental Agreement Number</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 187 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.RentalAgreementNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Prepaid</th>\r\n                                <td colspan=\"2\">\r\n");
#nullable restore
#line 192 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                     if (Model.Prepaid.HasValue)
                                    {
                                        if (Model.Prepaid.Value)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <sapn>True</sapn>\r\n");
#nullable restore
#line 197 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <sapn>False</sapn>\r\n");
#nullable restore
#line 201 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                        }
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                </td>
                            </tr>
                            <tr>
                                <th scope=""row"" class=""font-weight-bold"">Converted To Rental</th>
                                <td colspan=""2"">
");
#nullable restore
#line 208 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                     if (Model.ConvertedToRental.HasValue)
                                    {
                                        if (Model.ConvertedToRental.Value)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <sapn>True</sapn>\r\n");
#nullable restore
#line 213 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <sapn>False</sapn>\r\n");
#nullable restore
#line 217 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                        }
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">No Show Date</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 223 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.NoShowDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Cd Number</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 227 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.Cdnumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Customer Name</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 231 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.CustomerName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Driver Name</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 235 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.DriverName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Driver Country</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 239 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.DriverCountry));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Customer Card Indicator</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 243 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.CustomerCardIndicator));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Agency Country</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 247 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.AgencyCountry));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Agency Subsidiary Name</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 251 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.AgencySubsidiaryName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Agency Parent Name</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 255 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.AgencyParentName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Business Segment</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 259 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.BusinessSegmentText));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Creation Date</th>\r\n                                <td colspan=\"2\"> ");
#nullable restore
#line 263 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                            Write(Html.DisplayFor(model => model.CreationDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Assigned To Name</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 267 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.AssignedToName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <th scope=\"row\" class=\"font-weight-bold\">Next Step</th>\r\n                                <td colspan=\"2\">");
#nullable restore
#line 271 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"
                                           Write(Html.DisplayFor(model => model.NextStep));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n\r\n                        </tbody>\r\n                    </table>\r\n\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n\r\n\r\n    </div>\r\n");
#nullable restore
#line 284 "E:\TFS\SixtReservation\sixtReservstion\SIXTReservationApp\Views\Reservation\CBySixtDetails.cshtml"


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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SIXTReservationApp.Models.VReservationListVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
