#pragma checksum "C:\Users\Lenovo\source\repos\Proiect medii\Bondane_Carmen_Proiect\Views\Home\Statistics.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c731c0bd8b20d391899e284c3598bfedf17af1d3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Statistics), @"mvc.1.0.view", @"/Views/Home/Statistics.cshtml")]
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
#line 1 "C:\Users\Lenovo\source\repos\Proiect medii\Bondane_Carmen_Proiect\Views\_ViewImports.cshtml"
using Bondane_Carmen_Proiect;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lenovo\source\repos\Proiect medii\Bondane_Carmen_Proiect\Views\_ViewImports.cshtml"
using Bondane_Carmen_Proiect.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c731c0bd8b20d391899e284c3598bfedf17af1d3", @"/Views/Home/Statistics.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8246986667f4dd7408554208b33562bc57da2ee7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Statistics : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Bondane_Carmen_Proiect.Models.HospitalViewModels.AppointmentGroup>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Lenovo\source\repos\Proiect medii\Bondane_Carmen_Proiect\Views\Home\Statistics.cshtml"
  
    ViewData["Title"] = "Number of pacients / date";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h2>Number of pacients / date</h2>\r\n<table>\r\n    <tr>\r\n        <th>\r\n            Date\r\n        </th>\r\n        <th>\r\n            Number of pacients\r\n        </th>\r\n    </tr>\r\n");
#nullable restore
#line 15 "C:\Users\Lenovo\source\repos\Proiect medii\Bondane_Carmen_Proiect\Views\Home\Statistics.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 19 "C:\Users\Lenovo\source\repos\Proiect medii\Bondane_Carmen_Proiect\Views\Home\Statistics.cshtml"
           Write(Html.DisplayFor(modelItem => item.AppointmentDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 22 "C:\Users\Lenovo\source\repos\Proiect medii\Bondane_Carmen_Proiect\Views\Home\Statistics.cshtml"
           Write(item.PacientCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 25 "C:\Users\Lenovo\source\repos\Proiect medii\Bondane_Carmen_Proiect\Views\Home\Statistics.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Bondane_Carmen_Proiect.Models.HospitalViewModels.AppointmentGroup>> Html { get; private set; }
    }
}
#pragma warning restore 1591
