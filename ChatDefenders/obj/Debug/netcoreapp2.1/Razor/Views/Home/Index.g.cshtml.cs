#pragma checksum "C:\Users\ben\source\repos\ChatDefenders\ChatDefenders\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "133ea302859624d84254c2955ce601df469e33fb"
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
#line 1 "C:\Users\ben\source\repos\ChatDefenders\ChatDefenders\Views\_ViewImports.cshtml"
using ChatDefenders;

#line default
#line hidden
#line 2 "C:\Users\ben\source\repos\ChatDefenders\ChatDefenders\Views\_ViewImports.cshtml"
using ChatDefenders.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"133ea302859624d84254c2955ce601df469e33fb", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9fa0af9b9064ec845d0370e24f3e3408662b895", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IndexViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(23, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\ben\source\repos\ChatDefenders\ChatDefenders\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home";
    ViewData["Model"] = Model as UserModel;

#line default
#line hidden
            BeginContext(110, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(130, 166, true);
                WriteLiteral("\r\n    <script type=\"module\">\r\n        import user from \'/js/user.js\';\r\n    </script>\r\n    <script>\r\n        (function IIFE () {\r\n\r\n            let data = JSON.parse(\'");
                EndContext();
                BeginContext(297, 39, false);
#line 15 "C:\Users\ben\source\repos\ChatDefenders\ChatDefenders\Views\Home\Index.cshtml"
                              Write(Html.Raw(Model.UserAccount.ToDTOJSON()));

#line default
#line hidden
                EndContext();
                BeginContext(336, 98, true);
                WriteLiteral("\');\r\n            user.data = data;\r\n            console.log(user);\r\n        })();\r\n    </script>\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
