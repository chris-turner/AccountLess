#pragma checksum "C:\Users\chris\source\repos\AccountLess\AccountLess\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f147f246eae34352abc7da2af176f3d50546a952"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "C:\Users\chris\source\repos\AccountLess\AccountLess\Views\_ViewImports.cshtml"
using AccountLess;

#line default
#line hidden
#line 2 "C:\Users\chris\source\repos\AccountLess\AccountLess\Views\_ViewImports.cshtml"
using AccountLess.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f147f246eae34352abc7da2af176f3d50546a952", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"44597728cf4b64ec61f7c403d4d6b78a13ef4003", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\chris\source\repos\AccountLess\AccountLess\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 307, true);
            WriteLiteral(@"

<div class=""row"">
    <div class=""col-md-3"">
        <h1>AccountLess</h1>
    </div>
</div>
        <div class=""row"">
            <div class=""col-md-3"">
                <h2>Sites</h2>
            </div>
            <div class=""row"">
                <div class=""col-md-3"">
                    ");
            EndContext();
            BeginContext(353, 44, false);
#line 17 "C:\Users\chris\source\repos\AccountLess\AccountLess\Views\Home\Index.cshtml"
               Write(Html.ActionLink("Reddit", "Reddit", "Sites"));

#line default
#line hidden
            EndContext();
            BeginContext(397, 129, true);
            WriteLiteral("\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col-md-3\">\r\n                        ");
            EndContext();
            BeginContext(527, 60, false);
#line 21 "C:\Users\chris\source\repos\AccountLess\AccountLess\Views\Home\Index.cshtml"
                   Write(Html.ActionLink("YouTube (coming soon)", "YouTube", "Sites"));

#line default
#line hidden
            EndContext();
            BeginContext(587, 92, true);
            WriteLiteral("\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
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
